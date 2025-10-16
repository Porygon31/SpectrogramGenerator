namespace SpectrogramGenerator
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblOutputDirectory;
        private System.Windows.Forms.TextBox txtInputDirectory;
        private System.Windows.Forms.Button btnBrowseInput;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblOutputDirectory = new System.Windows.Forms.Label();
            this.txtInputDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.lblSoxStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 420);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(760, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 395);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(68, 13);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "Progression :";
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Location = new System.Drawing.Point(12, 370);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(105, 13);
            this.lblOutputDirectory.TabIndex = 2;
            this.lblOutputDirectory.Text = "Répertoire de sortie :";
            // 
            // txtInputDirectory
            // 
            this.txtInputDirectory.Location = new System.Drawing.Point(12, 25);
            this.txtInputDirectory.Name = "txtInputDirectory";
            this.txtInputDirectory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtInputDirectory.Size = new System.Drawing.Size(660, 20);
            this.txtInputDirectory.TabIndex = 3;
            this.txtInputDirectory.Text = "Dossier à analyser";
            // 
            // btnBrowseInput
            // 
            this.btnBrowseInput.Location = new System.Drawing.Point(678, 23);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(94, 23);
            this.btnBrowseInput.TabIndex = 4;
            this.btnBrowseInput.Text = "Parcourir";
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(12, 60);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(660, 20);
            this.txtFolderName.TabIndex = 5;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 100);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(760, 23);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Générer";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(12, 140);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(760, 23);
            this.btnOptions.TabIndex = 7;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // lblSoxStatus
            // 
            this.lblSoxStatus.AutoSize = true;
            this.lblSoxStatus.Location = new System.Drawing.Point(17, 326);
            this.lblSoxStatus.Name = "lblSoxStatus";
            this.lblSoxStatus.Size = new System.Drawing.Size(59, 13);
            this.lblSoxStatus.TabIndex = 8;
            this.lblSoxStatus.Text = "Sox Path";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(12, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(760, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblSoxStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblOutputDirectory);
            this.Controls.Add(this.txtInputDirectory);
            this.Controls.Add(this.btnBrowseInput);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnOptions);
            this.Name = "MainForm";
            this.Text = "Spectrogram Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblSoxStatus;
    }
}
