using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// using System.Diagnostics;
using System.IO;

namespace wpf_gallery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string imagesPath = @"C:\Users\kudlaty\Repos\wpf-gallery\images";
        public string[] files;

        public static string NormalizePath(string path)
        {
            return System.IO.Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        public MainWindow()
        {
            InitializeComponent();

            var files = Directory.GetFiles(imagesPath, "*.png");
            image.Source = new BitmapImage(
                new Uri(files[0]));
            locationText.Text = NormalizePath(files[0]);
        }

        public int getImageIndex()
        {
            files = Directory.GetFiles(imagesPath, "*.png");
            files = files.Select(p => NormalizePath(p)).ToArray();

            return Array.IndexOf(files, NormalizePath(image.Source.ToString()));
        }

        public void replaceImage(string path)
        {
            image.Source = new BitmapImage(
            new Uri(path));
            locationText.Text = NormalizePath(path);
        }

        private void rightBtn_Click(object sender, RoutedEventArgs e)
        {
            int imageIndex = getImageIndex();
            int nextImageIndex = (imageIndex + 1 < files.Length) ? imageIndex + 1 : 0;

            replaceImage(files[nextImageIndex]);
        }

        private void leftBtn_Click(object sender, RoutedEventArgs e)
        {
            int imageIndex = getImageIndex();
            int nextImageIndex = (imageIndex - 1 >= 0) ? imageIndex - 1 : files.Length - 1;

            replaceImage(files[nextImageIndex]);
        }

        private void folderButton_Click(object sender, RoutedEventArgs e)
        {
            //Warning	MSB3290	Failed to create the wrapper assembly for type library. Type library 'System_Windows_Forms' was exported from a CLR assembly and cannot be re-imported as a CLR assembly.
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            }
        }
    }
}
