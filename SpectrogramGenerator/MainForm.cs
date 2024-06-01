using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SpectrogramGenerator
{
    public partial class MainForm : Form
    {
        private AppSettings settings;
        private int totalFiles;

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
        private void btnGenerate_Click(object sender, EventArgs e)
        {
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

            var flacFiles = Directory.GetFiles(inputDirectory, "*.flac");
            totalFiles = flacFiles.Length * 2; // Chaque fichier flac génère 2 fichiers (full et zoom)
            progressBar.Maximum = totalFiles;
            progressBar.Value = 0;
            lblProgress.Text = $"Progression : 0/{totalFiles}";

            foreach (string file in flacFiles)
            {
                string fileName = Path.GetFileName(file);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);

                string fullSpectralOutput = Path.Combine(outputDirectory, $"{fileNameWithoutExtension}-full.png");
                RunSox(settings.SoxPath, file, fullSpectralOutput, settings.SoxOptionsFull, $"{fileName} FULL");
                UpdateProgress();

                string zoomSpectralOutput = Path.Combine(outputDirectory, $"{fileNameWithoutExtension}-zoom.png");
                RunSox(settings.SoxPath, file, zoomSpectralOutput, settings.GetZoomOptions(), $"{fileName} ZOOM (1:00 to 1:02)");
                UpdateProgress();
            }

            MessageBox.Show("Génération des spectrogrammes terminée.");
            var viewerForm = new ViewerForm(Directory.GetFiles(outputDirectory, "*.png").ToList());
            viewerForm.Show();
        }

        // Exécute la commande Sox pour générer le spectrogramme
        private void RunSox(string soxPath, string inputFile, string outputFile, string options, string title)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = soxPath,
                Arguments = $"\"{inputFile}\" -n remix 1 spectrogram {options} -t \"{title}\" -o \"{outputFile}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = processStartInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    MessageBox.Show($"Erreur lors de la génération du spectrogramme pour {inputFile}: {error}");
                }
            }
        }

        // Gestionnaire de l'événement Click pour le bouton Options
        private void btnOptions_Click(object sender, EventArgs e)
        {
            using (OptionsForm optionsForm = new OptionsForm(settings))
            {
                if (optionsForm.ShowDialog() == DialogResult.OK)
                {
                    SaveSettings();
                }
            }
        }

        // Met à jour la barre de progression
        private void UpdateProgress()
        {
            progressBar.Value += 1;
            lblProgress.Text = $"Progression : {progressBar.Value}/{totalFiles}";
            Application.DoEvents();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblOutputDirectory.Text = settings.OutputRootDirectory;
        }
    }
}
