using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SpectrogramGenerator
{
    public partial class ViewerForm : Form
    {
        private List<string> _imagePaths;
        private int _currentIndex;

        public ViewerForm(List<string> imagePaths)
        {
            InitializeComponent();
            _imagePaths = imagePaths;
            _currentIndex = 0;
            DisplayImage();
        }

        // Affiche l'image actuelle
        private void DisplayImage()
        {
            if (_imagePaths.Count == 0)
            {
                MessageBox.Show("Aucune image à afficher.");
                return;
            }

            if (_currentIndex < 0) _currentIndex = 0;
            if (_currentIndex >= _imagePaths.Count) _currentIndex = _imagePaths.Count - 1;

            if (File.Exists(_imagePaths[_currentIndex]))
            {
                pictureBox.Image = Image.FromFile(_imagePaths[_currentIndex]);
            }
            else
            {
                MessageBox.Show("Le fichier image n'existe pas.");
            }
        }

        // Gestionnaire de l'événement Click pour le bouton Précédent
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentIndex--;
            DisplayImage();
        }

        // Gestionnaire de l'événement Click pour le bouton Suivant
        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentIndex++;
            DisplayImage();
        }
    }
}