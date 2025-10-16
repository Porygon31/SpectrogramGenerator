using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SpectrogramGenerator
{
    public partial class MainForm : Form
    {
        private AppSettings settings;
        private int totalFiles;
        private CancellationTokenSource _cts;
        private bool _isRunning;

        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        // Charge les paramètres à partir d'un fichier JSON
        private void LoadSettings()
        {
            if (File.Exists("settings.json"))
            {
                string json = File.ReadAllText("settings.json");
                settings = JsonConvert.DeserializeObject<AppSettings>(json);
            }
            else
            {
                settings = new AppSettings();
            }
        }

        // Sauvegarde les paramètres dans un fichier JSON
        private void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText("settings.json", json);
        }

        // Gestionnaire de l'événement Click pour le bouton Parcourir
        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtInputDirectory.Text = fbd.SelectedPath;
                }
            }
        }

        // Gestionnaire de l'événement Click pour le bouton Générer
        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (_isRunning)
                return;

            string inputDirectory = txtInputDirectory.Text;
            string folderName = txtFolderName.Text;

            // Si l'utilisateur laisse la TextBox vide, définir une valeur par défaut
            if (string.IsNullOrWhiteSpace(folderName))
            {
                // Récupérer le nom du dossier parent du répertoire de musique
                string parentDirectoryName = new DirectoryInfo(inputDirectory).Name;
                // Définir la valeur par défaut basée sur le nom du dossier et la date actuelle
                folderName = $"{parentDirectoryName}_{DateTime.Now:yyyyMMdd_HHmmss}";
            }

            string outputDirectory = Path.Combine(settings.OutputRootDirectory, folderName);
            Directory.CreateDirectory(outputDirectory);

            if (!Directory.Exists(inputDirectory))
            {
                MessageBox.Show("Le répertoire d'entrée spécifié n'existe pas.");
                return;
            }

            // Vérifie que SoX est disponible et fonctionnel avant de lancer les traitements
            if (!EnsureSoxIsAvailable())
            {
                return;
            }

            var flacFiles = Directory.GetFiles(inputDirectory, "*.flac");
            if (flacFiles.Length == 0)
            {
                MessageBox.Show("Aucun fichier .flac trouvé dans le répertoire d'entrée.");
                return;
            }

            totalFiles = flacFiles.Length * 2; // Chaque fichier flac génère 2 fichiers (full et zoom)
            progressBar.Maximum = totalFiles;
            progressBar.Value = 0;
            lblProgress.Text = $"Progression : 0/{totalFiles}";

            int successCount = 0;
            int failCount = 0;
            bool canceled = false;
            StringBuilder errorReport = new StringBuilder();

            // Désactivation/activation des contrôles et création du CTS
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            SetUiRunning(true);

            try
            {
                foreach (string file in flacFiles)
                {
                    if (token.IsCancellationRequested)
                    {
                        canceled = true;
                        break;
                    }

                    string fileName = Path.GetFileName(file);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);

                    // FULL
                    string fullSpectralOutput = Path.Combine(outputDirectory, $"{fileNameWithoutExtension}-full.png");
                    var fullResult = await RunSoxAsync(settings.SoxPath, file, fullSpectralOutput, settings.SoxOptionsFull, $"{fileName} FULL", token);
                    if (fullResult.Success)
                    {
                        successCount++;
                    }
                    else
                    {
                        if (token.IsCancellationRequested)
                        {
                            canceled = true;
                            break;
                        }
                        failCount++;
                        AppendError(errorReport, file, "FULL", fullResult);
                    }
                    UpdateProgress();

                    if (token.IsCancellationRequested)
                    {
                        canceled = true;
                        break;
                    }

                    // ZOOM
                    string zoomSpectralOutput = Path.Combine(outputDirectory, $"{fileNameWithoutExtension}-zoom.png");
                    var zoomResult = await RunSoxAsync(settings.SoxPath, file, zoomSpectralOutput, settings.GetZoomOptions(), $"{fileName} ZOOM (1:00 to 1:02)", token);
                    if (zoomResult.Success)
                    {
                        successCount++;
                    }
                    else
                    {
                        if (token.IsCancellationRequested)
                        {
                            canceled = true;
                            break;
                        }
                        failCount++;
                        AppendError(errorReport, file, "ZOOM", zoomResult);
                    }
                    UpdateProgress();
                }
            }
            finally
            {
                SetUiRunning(false);
                _cts.Dispose();
                _cts = null;
            }

            if (canceled)
            {
                MessageBox.Show("Génération annulée par l'utilisateur.");
            }
            else
            {
                // Écrit un log d’erreurs si nécessaire
                if (failCount > 0)
                {
                    string logPath = Path.Combine(outputDirectory, "errors.log");
                    File.WriteAllText(logPath, errorReport.ToString());
                    MessageBox.Show(
                        $"Génération terminée avec {failCount} échec(s) et {successCount} succès.\n" +
                        $"Un journal détaillé a été créé:\n{logPath}",
                        "Spectrogrammes - Résumé",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Génération des spectrogrammes terminée avec succès.");
                }

                // N’ouvre le viewer que s’il y a des images
                var generatedImages = Directory.GetFiles(outputDirectory, "*.png").ToList();
                if (generatedImages.Count > 0)
                {
                    var viewerForm = new ViewerForm(generatedImages);
                    viewerForm.Show();
                }
            }
        }

        private void SetUiRunning(bool running)
        {
            _isRunning = running;
            btnGenerate.Enabled = !running;
            btnOptions.Enabled = !running;
            btnBrowseInput.Enabled = !running;
            txtInputDirectory.Enabled = !running;
            txtFolderName.Enabled = !running;
            if (this.Controls.ContainsKey("btnCancel"))
            {
                var b = this.Controls["btnCancel"] as Button;
                if (b != null) b.Enabled = running;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        // Exécute la commande Sox pour générer le spectrogramme (retourne un résultat structuré) en asynchrone avec annulation
        private async Task<SoxResult> RunSoxAsync(string soxPath, string inputFile, string outputFile, string options, string title, CancellationToken token)
        {
            // Construction sûre des arguments
            string args = $"\"{inputFile}\" -n remix 1 spectrogram {options} -t \"{title}\" -o \"{outputFile}\"";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = soxPath,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            int exitCode = -1;
            var stdOutSb = new StringBuilder();
            var stdErrSb = new StringBuilder();

            try
            {
                using (var process = new Process { StartInfo = psi, EnableRaisingEvents = true })
                {
                    var tcs = new TaskCompletionSource<int>();

                    process.OutputDataReceived += (s, e) => { if (e.Data != null) stdOutSb.AppendLine(e.Data); };
                    process.ErrorDataReceived += (s, e) => { if (e.Data != null) stdErrSb.AppendLine(e.Data); };
                    process.Exited += (s, e) =>
                    {
                        try { exitCode = process.ExitCode; }
                        catch { exitCode = -1; }
                        tcs.TrySetResult(exitCode);
                    };

                    if (!process.Start())
                    {
                        return new SoxResult
                        {
                            Success = false,
                            ExitCode = -1,
                            StdOut = string.Empty,
                            StdErr = "Le processus SoX n'a pas démarré.",
                            Command = $"{soxPath} {args}",
                            OutputFilePath = outputFile,
                            FriendlyMessage = "Impossible de démarrer SoX."
                        };
                    }

                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    using (token.Register(() =>
                    {
                        try { if (!process.HasExited) process.Kill(); } catch { /* ignore */ }
                    }))
                    {
                        await tcs.Task.ConfigureAwait(true);
                        // S'assure que tout le flux est lu
                        try { process.WaitForExit(); } catch { }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                return new SoxResult
                {
                    Success = false,
                    ExitCode = -1,
                    StdOut = stdOutSb.ToString(),
                    StdErr = stdErrSb.ToString(),
                    Command = $"{soxPath} {args}",
                    OutputFilePath = outputFile,
                    FriendlyMessage = "Opération annulée."
                };
            }
            catch (Exception ex)
            {
                return new SoxResult
                {
                    Success = false,
                    ExitCode = exitCode,
                    StdOut = stdOutSb.ToString(),
                    StdErr = ex.Message,
                    Command = $"{soxPath} {args}",
                    OutputFilePath = outputFile,
                    FriendlyMessage = "Échec de l'exécution de SoX (exception). Vérifiez le chemin et les droits d'accès."
                };
            }

            // Vérifie l’exit code et la sortie produite
            bool producedFile = File.Exists(outputFile) && new FileInfo(outputFile).Length > 0;
            bool success = exitCode == 0 && producedFile;

            return new SoxResult
            {
                Success = success,
                ExitCode = exitCode,
                StdOut = stdOutSb.ToString(),
                StdErr = stdErrSb.ToString(),
                Command = $"{soxPath} {args}",
                OutputFilePath = outputFile,
                FriendlyMessage = GetFriendlySoxError(stdErrSb.ToString(), exitCode, producedFile)
            };
        }

        // Fournit un message plus lisible selon l’erreur SoX
        private string GetFriendlySoxError(string stderr, int exitCode, bool producedFile)
        {
            if (exitCode == 0 && !producedFile)
                return "SoX a terminé sans erreur, mais aucun fichier de sortie n'a été produit.";

            if (string.IsNullOrEmpty(stderr))
            {
                if (exitCode != 0) return $"SoX a retourné le code {exitCode} sans message d’erreur.";
                return string.Empty;
            }

            string s = stderr.ToLowerInvariant();

            if (s.Contains("invalid option") || s.Contains("unrecognized option"))
                return "Options SoX invalides. Vérifiez vos options dans le menu Options.";
            if (s.Contains("can't open input file") || s.Contains("cannot open input file"))
                return "Impossible d’ouvrir le fichier d’entrée. Vérifiez le chemin et les permissions.";
            if (s.Contains("no such file") || s.Contains("not found"))
                return "Fichier introuvable. Vérifiez le chemin du fichier ou de sox.exe.";
            if (s.Contains("formats:") && s.Contains("unsupported"))
                return "Format audio non supporté par SoX.";
            if (s.Contains("permission denied"))
                return "Accès refusé. Lancez l’application avec les droits nécessaires ou changez de dossier.";
            if (s.Contains("remix") && s.Contains("channels"))
                return "Problème avec l'effet 'remix'. Vérifiez le nombre de canaux de l'audio.";
            if (s.Contains("fail") && s.Contains("spectrogram"))
                return "Échec lors de la génération du spectrogramme. Vérifiez les options du spectrogramme.";

            return "Erreur SoX: " + stderr.Trim();
        }

        // Ajoute une ligne d’erreur détaillée au rapport
        private void AppendError(StringBuilder sb, string inputFile, string variant, SoxResult result)
        {
            sb.AppendLine("------------------------------------------------------------");
            sb.AppendLine($"Entrée: {inputFile}");
            sb.AppendLine($"Variante: {variant}");
            sb.AppendLine($"Commande: {result.Command}");
            sb.AppendLine($"Sortie attendue: {result.OutputFilePath}");
            sb.AppendLine($"ExitCode: {result.ExitCode}");
            if (!string.IsNullOrWhiteSpace(result.FriendlyMessage))
                sb.AppendLine($"Message: {result.FriendlyMessage}");
            if (!string.IsNullOrWhiteSpace(result.StdErr))
            {
                sb.AppendLine("Stderr:");
                sb.AppendLine(result.StdErr.Trim());
            }
            if (!string.IsNullOrWhiteSpace(result.StdOut))
            {
                sb.AppendLine("Stdout:");
                sb.AppendLine(result.StdOut.Trim());
            }
        }

        // Gestionnaire de l'événement Click pour le bouton Options
        private void btnOptions_Click(object sender, EventArgs e)
        {
            ShowOptionsDialog();
        }

        private void ShowOptionsDialog()
        {
            using (OptionsForm optionsForm = new OptionsForm(settings))
            {
                if (optionsForm.ShowDialog() == DialogResult.OK)
                {
                    SaveSettings();
                    lblOutputDirectory.Text = settings.OutputRootDirectory;
                }
            }
        }

        // Vérifie que SoX est présent et exécutable
        private bool EnsureSoxIsAvailable()
        {
            if (settings == null)
            {
                MessageBox.Show("Les paramètres de l'application ne sont pas chargés.");
                return false;
            }

            string soxPath = settings.SoxPath;
            if (string.IsNullOrWhiteSpace(soxPath))
            {
                MessageBox.Show("Le chemin de SoX n'est pas configuré. Veuillez le définir dans les options.");
                ShowOptionsDialog();
                soxPath = settings.SoxPath;
                if (string.IsNullOrWhiteSpace(soxPath))
                    return false;
            }

            string stdout, stderr;
            int exitCode;

            // 1) Teste le chemin configuré
            bool started = TryRunProcess(soxPath, "--version", out stdout, out stderr, out exitCode, 3000);
            if (started && exitCode == 0)
            {
                return true;
            }

            // 2) Fallback: tente via le PATH système
            bool startedFallback = TryRunProcess("sox", "--version", out stdout, out stderr, out exitCode, 3000);
            if (startedFallback && exitCode == 0)
            {
                // Si OK via PATH, on met à jour le paramètre pour utiliser "sox"
                settings.SoxPath = "sox";
                SaveSettings();
                return true;
            }

            // 3) Informe l'utilisateur et propose de corriger
            bool fileExists = !string.IsNullOrWhiteSpace(soxPath) && File.Exists(soxPath);
            string existenceInfo = fileExists
                ? "Le fichier existe mais l'exécution a échoué."
                : "Le fichier est introuvable à l'emplacement configuré.";
            MessageBox.Show(
                "SoX est introuvable ou ne fonctionne pas.\n\n" +
                $"Chemin configuré: {soxPath}\n{existenceInfo}\n\n" +
                $"Détails: {stderr}\n\n" +
                "Veuillez corriger le chemin dans les options."
            );

            ShowOptionsDialog();
            return false;
        }

        // Lance un processus et récupère stdout/stderr/exitCode avec timeout
        private static bool TryRunProcess(string fileName, string arguments, out string stdOut, out string stdErr, out int exitCode, int timeoutMs = 10000)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = psi })
            {
                stdOut = string.Empty;
                stdErr = string.Empty;
                exitCode = -1;

                try
                {
                    if (!process.Start())
                    {
                        stdErr = "Le processus n'a pas démarré.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    stdErr = ex.Message;
                    return false;
                }

                if (!process.WaitForExit(timeoutMs))
                {
                    try { process.Kill(); } catch { /* ignore */ }
                    stdOut = SafeRead(process.StandardOutput);
                    stdErr = "Timeout d'exécution de SoX.";
                    exitCode = -1;
                    return false;
                }

                stdOut = SafeRead(process.StandardOutput);
                stdErr = SafeRead(process.StandardError);
                exitCode = process.ExitCode;
                return true;
            }
        }

        private static string SafeRead(StreamReader reader)
        {
            try { return reader.ReadToEnd(); }
            catch { return string.Empty; }
        }

        // Met à jour la barre de progression (sans Application.DoEvents)
        private void UpdateProgress()
        {
            progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
            lblProgress.Text = $"Progression : {progressBar.Value}/{totalFiles}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblOutputDirectory.Text = settings.OutputRootDirectory;
        }

        // Résultat d'une exécution SoX
        private class SoxResult
        {
            public bool Success { get; set; }
            public int ExitCode { get; set; }
            public string StdOut { get; set; }
            public string StdErr { get; set; }
            public string Command { get; set; }
            public string OutputFilePath { get; set; }
            public string FriendlyMessage { get; set; }
        }
    }
}
