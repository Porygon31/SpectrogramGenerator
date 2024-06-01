using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpectrogramGenerator
{
    public partial class ViewerForm : Form
    {
        private List<string> _imagePaths;
        private int _currentIndex;
        private float _zoomFactor;
        private Point _mouseDownLocation;

        public ViewerForm(List<string> imagePaths)
        {
            InitializeComponent();
            _imagePaths = imagePaths;
            _currentIndex = 0;
            _zoomFactor = 1.0f;
            DisplayImage();
        }

        private void DisplayImage()
        {
            if (_imagePaths.Count == 0)
            {
                MessageBox.Show("Aucune image à afficher.");
                return;
            }

            if (_currentIndex < 0) _currentIndex = 0;
            if (_currentIndex >= _imagePaths.Count) _currentIndex = _imagePaths.Count - 1;

            if (System.IO.File.Exists(_imagePaths[_currentIndex]))
            {
                Image image = Image.FromFile(_imagePaths[_currentIndex]);
                pictureBox.Image = image;
                pictureBox.Size = new Size((int)(image.Width * _zoomFactor), (int)(image.Height * _zoomFactor));
                lblImageInfo.Text = $"Image {_currentIndex + 1}/{_imagePaths.Count}: {_imagePaths[_currentIndex]}";
            }
            else
            {
                MessageBox.Show("Le fichier image n'existe pas.");
            }

            // Mettre à jour les boutons
            btnPrevious.Enabled = _currentIndex > 0;
            btnNext.Enabled = _currentIndex < _imagePaths.Count - 1;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                DisplayImage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentIndex < _imagePaths.Count - 1)
            {
                _currentIndex++;
                DisplayImage();
            }
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            _zoomFactor += 0.1f;
            DisplayImage();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (_zoomFactor > 0.1f)
            {
                _zoomFactor -= 0.1f;
                DisplayImage();
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
                pictureBox.Cursor = Cursors.Hand;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                panel.AutoScrollPosition = new Point(
                    panel.AutoScrollPosition.X - (e.X - _mouseDownLocation.X),
                    panel.AutoScrollPosition.Y - (e.Y - _mouseDownLocation.Y));
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox.Cursor = Cursors.Default;
            }
        }
    }
}
