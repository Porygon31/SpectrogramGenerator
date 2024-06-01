using System;
using System.IO;

namespace SpectrogramGenerator
{
    public class AppSettings
    {
        public string SoxPath { get; set; } = @"C:\sox-14.4.2\sox.exe";
        public string OutputRootDirectory { get; set; } = Path.Combine(GetPicturesDefaultDirectory(), "Spectrograms"); //@"C:\Music\_Spectrals\";
        public string SoxOptionsFull { get; set; } = "-x 3000 -y 513 -z 120 -w Kaiser";
        public int ZoomResolution { get; set; } = 500;
        public string ZoomWindowFunction { get; set; } = "Kaiser";
        public string SoxOptionsZoom { get; set; } = "-X 500 -y 1025 -z 120 -w Kaiser -S 1:00 -d 0:02";

        public string GetZoomOptions()
        {
            return $"-X {ZoomResolution} -y 1025 -z 120 -w {ZoomWindowFunction} -S 1:00 -d 0:02";
        }

        public static string GetPicturesDefaultDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }
    }
}