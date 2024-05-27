using System.ComponentModel;

namespace SpectrogramGenerator
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.lblSoxPath = new System.Windows.Forms.Label();
            this.txtSoxPath = new System.Windows.Forms.TextBox();
            this.lblSoxOptionsFull = new System.Windows.Forms.Label();
            this.txtSoxOptionsFull = new System.Windows.Forms.TextBox();
            this.groupBoxSoxOptionsZoom = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbZoomWindowFunction = new System.Windows.Forms.ComboBox();
            this.lblZoomWindowFunction = new System.Windows.Forms.Label();
            this.numZoomResolution = new System.Windows.Forms.NumericUpDown();
            this.lblZoomResolution = new System.Windows.Forms.Label();
            this.btnBrowseSoxPath = new System.Windows.Forms.Button();
            this.btnBrowseOutputRoot = new System.Windows.Forms.Button();
            this.lblOutputRootDirectory = new System.Windows.Forms.Label();
            this.txtOutputRootDirectory = new System.Windows.Forms.TextBox();
            this.groupBoxSoxOptionsZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numZoomResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSoxPath
            // 
            this.lblSoxPath.Location = new System.Drawing.Point(65, 54);
            this.lblSoxPath.Name = "lblSoxPath";
            this.lblSoxPath.Size = new System.Drawing.Size(125, 38);
            this.lblSoxPath.TabIndex = 0;
            this.lblSoxPath.Text = "Chemin de SoX.exe :";
            // 
            // txtSoxPath
            // 
            this.txtSoxPath.Location = new System.Drawing.Point(235, 54);
            this.txtSoxPath.Name = "txtSoxPath";
            this.txtSoxPath.Size = new System.Drawing.Size(298, 20);
            this.txtSoxPath.TabIndex = 1;
            // 
            // lblSoxOptionsFull
            // 
            this.lblSoxOptionsFull.Location = new System.Drawing.Point(15, 114);
            this.lblSoxOptionsFull.Name = "lblSoxOptionsFull";
            this.lblSoxOptionsFull.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSoxOptionsFull.Size = new System.Drawing.Size(225, 33);
            this.lblSoxOptionsFull.TabIndex = 3;
            this.lblSoxOptionsFull.Text = "Options Sox pour spectrogramme complet :";
            // 
            // txtSoxOptionsFull
            // 
            this.txtSoxOptionsFull.Location = new System.Drawing.Point(246, 114);
            this.txtSoxOptionsFull.Name = "txtSoxOptionsFull";
            this.txtSoxOptionsFull.Size = new System.Drawing.Size(344, 20);
            this.txtSoxOptionsFull.TabIndex = 4;
            // 
            // groupBoxSoxOptionsZoom
            // 
            this.groupBoxSoxOptionsZoom.Controls.Add(this.btnSave);
            this.groupBoxSoxOptionsZoom.Controls.Add(this.cmbZoomWindowFunction);
            this.groupBoxSoxOptionsZoom.Controls.Add(this.lblZoomWindowFunction);
            this.groupBoxSoxOptionsZoom.Controls.Add(this.numZoomResolution);
            this.groupBoxSoxOptionsZoom.Controls.Add(this.lblZoomResolution);
            this.groupBoxSoxOptionsZoom.Location = new System.Drawing.Point(106, 254);
            this.groupBoxSoxOptionsZoom.Name = "groupBoxSoxOptionsZoom";
            this.groupBoxSoxOptionsZoom.Size = new System.Drawing.Size(444, 184);
            this.groupBoxSoxOptionsZoom.TabIndex = 5;
            this.groupBoxSoxOptionsZoom.TabStop = false;
            this.groupBoxSoxOptionsZoom.Text = "Options Sox pour spectrogramme zoomé :";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(25, 156);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(142, 27);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Sauvegarder";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbZoomWindowFunction
            // 
            this.cmbZoomWindowFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZoomWindowFunction.FormattingEnabled = true;
            this.cmbZoomWindowFunction.Items.AddRange(new object[] { "Hann", "Hamming", "Kaiser" });
            this.cmbZoomWindowFunction.Location = new System.Drawing.Point(197, 110);
            this.cmbZoomWindowFunction.Name = "cmbZoomWindowFunction";
            this.cmbZoomWindowFunction.Size = new System.Drawing.Size(246, 21);
            this.cmbZoomWindowFunction.TabIndex = 3;
            // 
            // lblZoomWindowFunction
            // 
            this.lblZoomWindowFunction.Location = new System.Drawing.Point(22, 111);
            this.lblZoomWindowFunction.Name = "lblZoomWindowFunction";
            this.lblZoomWindowFunction.Size = new System.Drawing.Size(155, 22);
            this.lblZoomWindowFunction.TabIndex = 2;
            this.lblZoomWindowFunction.Text = "Fonction de fenêtre:";
            // 
            // numZoomResolution
            // 
            this.numZoomResolution.Location = new System.Drawing.Point(99, 43);
            this.numZoomResolution.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numZoomResolution.Name = "numZoomResolution";
            this.numZoomResolution.Size = new System.Drawing.Size(69, 20);
            this.numZoomResolution.TabIndex = 1;
            // 
            // lblZoomResolution
            // 
            this.lblZoomResolution.Location = new System.Drawing.Point(18, 41);
            this.lblZoomResolution.Name = "lblZoomResolution";
            this.lblZoomResolution.Size = new System.Drawing.Size(160, 28);
            this.lblZoomResolution.TabIndex = 0;
            this.lblZoomResolution.Text = "Résolution :";
            // 
            // btnBrowseSoxPath
            // 
            this.btnBrowseSoxPath.Location = new System.Drawing.Point(562, 43);
            this.btnBrowseSoxPath.Name = "btnBrowseSoxPath";
            this.btnBrowseSoxPath.Size = new System.Drawing.Size(155, 48);
            this.btnBrowseSoxPath.TabIndex = 6;
            this.btnBrowseSoxPath.Text = "Parcourir...";
            this.btnBrowseSoxPath.UseVisualStyleBackColor = true;
            this.btnBrowseSoxPath.Click += new System.EventHandler(this.btnBrowseSoxPath_Click);
            // 
            // btnBrowseOutputRoot
            // 
            this.btnBrowseOutputRoot.Location = new System.Drawing.Point(618, 151);
            this.btnBrowseOutputRoot.Name = "btnBrowseOutputRoot";
            this.btnBrowseOutputRoot.Size = new System.Drawing.Size(114, 31);
            this.btnBrowseOutputRoot.TabIndex = 7;
            this.btnBrowseOutputRoot.Text = "Parcourir...";
            this.btnBrowseOutputRoot.UseVisualStyleBackColor = true;
            this.btnBrowseOutputRoot.Click += new System.EventHandler(this.btnBrowseOutputRoot_Click);
            // 
            // lblOutputRootDirectory
            // 
            this.lblOutputRootDirectory.Location = new System.Drawing.Point(38, 160);
            this.lblOutputRootDirectory.Name = "lblOutputRootDirectory";
            this.lblOutputRootDirectory.Size = new System.Drawing.Size(167, 26);
            this.lblOutputRootDirectory.TabIndex = 8;
            this.lblOutputRootDirectory.Text = "Répertoire racine de sortie :";
            // 
            // txtOutputRootDirectory
            // 
            this.txtOutputRootDirectory.Location = new System.Drawing.Point(232, 157);
            this.txtOutputRootDirectory.Name = "txtOutputRootDirectory";
            this.txtOutputRootDirectory.Size = new System.Drawing.Size(380, 20);
            this.txtOutputRootDirectory.TabIndex = 9;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtOutputRootDirectory);
            this.Controls.Add(this.lblOutputRootDirectory);
            this.Controls.Add(this.btnBrowseOutputRoot);
            this.Controls.Add(this.btnBrowseSoxPath);
            this.Controls.Add(this.groupBoxSoxOptionsZoom);
            this.Controls.Add(this.txtSoxOptionsFull);
            this.Controls.Add(this.lblSoxOptionsFull);
            this.Controls.Add(this.txtSoxPath);
            this.Controls.Add(this.lblSoxPath);
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this.groupBoxSoxOptionsZoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numZoomResolution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtOutputRootDirectory;

        private System.Windows.Forms.Label lblOutputRootDirectory;

        private System.Windows.Forms.Button btnSave;

        private System.Windows.Forms.ComboBox cmbZoomWindowFunction;

        private System.Windows.Forms.Label lblZoomWindowFunction;

        private System.Windows.Forms.NumericUpDown numZoomResolution;

        private System.Windows.Forms.Button btnBrowseSoxPath;
        private System.Windows.Forms.Button btnBrowseOutputRoot;

        private System.Windows.Forms.Label lblZoomResolution;

        private System.Windows.Forms.GroupBox groupBoxSoxOptionsZoom;

        private System.Windows.Forms.TextBox txtSoxOptionsFull;

        private System.Windows.Forms.Label lblSoxOptionsFull;

        private System.Windows.Forms.TextBox txtSoxPath;
      //  private System.Windows.Forms.Button btnBrowseSoxPath_Click;

        private System.Windows.Forms.Label lblSoxPath;

        #endregion
    }
}