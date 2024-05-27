using System;
using System.Windows.Forms;

namespace SpectrogramGenerator
{
    public partial class OptionsForm : Form
    {
        private AppSettings settings;

        public OptionsForm(AppSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            LoadSettings();
        }

        // Charge les paramètres depuis l'objet settings
        private void LoadSettings()
        {
            txtSoxPath.Text = settings.SoxPath;
            txtOutputRootDirectory.Text = settings.OutputRootDirectory;
            txtSoxOptionsFull.Text = settings.SoxOptionsFull;
            numZoomResolution.Value = settings.ZoomResolution;
            cmbZoomWindowFunction.SelectedItem = settings.ZoomWindowFunction;
        }

        // Sauvegarde les paramètres dans l'objet settings et ferme le formulaire
        private void btnSave_Click(object sender, EventArgs e)
        {
            settings.SoxPath = txtSoxPath.Text;
            settings.OutputRootDirectory = txtOutputRootDirectory.Text;
            settings.SoxOptionsFull = txtSoxOptionsFull.Text;
            settings.ZoomResolution = (int)numZoomResolution.Value;
            settings.ZoomWindowFunction = cmbZoomWindowFunction.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        // Ouvre une boîte de dialogue pour sélectionner le chemin de sox.exe
        private void btnBrowseSoxPath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Fichiers exécutables|*.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtSoxPath.Text = ofd.FileName;
                }
            }
        }

        // Ouvre une boîte de dialogue pour sélectionner le répertoire de sortie
        private void btnBrowseOutputRoot_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtOutputRootDirectory.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
