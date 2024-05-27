namespace SpectrogramGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFolderName = new System.Windows.Forms.Label();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.lblInputDirectory = new System.Windows.Forms.Label();
            this.txtInputDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblFolderName
            // 
            this.lblFolderName.Location = new System.Drawing.Point(41, 137);
            this.lblFolderName.Name = "lblFolderName";
            this.lblFolderName.Size = new System.Drawing.Size(99, 39);
            this.lblFolderName.TabIndex = 0;
            this.lblFolderName.Text = "Nom du Dossier: ";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(303, 156);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(220, 20);
            this.txtFolderName.TabIndex = 1;
            // 
            // lblInputDirectory
            // 
            this.lblInputDirectory.Location = new System.Drawing.Point(41, 206);
            this.lblInputDirectory.Name = "lblInputDirectory";
            this.lblInputDirectory.Size = new System.Drawing.Size(104, 35);
            this.lblInputDirectory.TabIndex = 2;
            this.lblInputDirectory.Text = "Répertoire d\'entrée";
            // 
            // txtInputDirectory
            // 
            this.txtInputDirectory.Location = new System.Drawing.Point(248, 214);
            this.txtInputDirectory.Name = "txtInputDirectory";
            this.txtInputDirectory.Size = new System.Drawing.Size(229, 20);
            this.txtInputDirectory.TabIndex = 3;
            // 
            // btnBrowseInput
            // 
            this.btnBrowseInput.Location = new System.Drawing.Point(518, 213);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(126, 27);
            this.btnBrowseInput.TabIndex = 4;
            this.btnBrowseInput.Text = "Parcourir";
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(557, 377);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(146, 48);
            this.btnGenerate.TabIndex = 5;
            this.btnGenerate.Text = "Générer";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(631, 19);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(135, 30);
            this.btnOptions.TabIndex = 6;
            this.btnOptions.Text = "Options";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnBrowseInput);
            this.Controls.Add(this.txtInputDirectory);
            this.Controls.Add(this.lblInputDirectory);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.lblFolderName);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnOptions;

        private System.Windows.Forms.Button btnGenerate;

        private System.Windows.Forms.Button btnBrowseInput;

        private System.Windows.Forms.TextBox txtInputDirectory;

        private System.Windows.Forms.Label lblInputDirectory;

        private System.Windows.Forms.TextBox txtFolderName;

        private System.Windows.Forms.Label lblFolderName;

        #endregion
    }
}