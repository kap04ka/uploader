using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;


namespace uploader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string rootPath;
        private string versionPath;
        private string zipPath;

        private string currentVersion;
        private string newVersion;

        public MainWindow()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            versionPath = Path.Combine(rootPath, "Version.txt");
            currentVersion = File.ReadAllText(versionPath);
            zipPath = Path.Combine(rootPath, "Build.zip");

            curVer.Text = "Текущая версия: " + currentVersion;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient wc = new WebClient())
            {
                currentVersion = File.ReadAllText(versionPath);
                newVersion = wc.DownloadString("https://drive.google.com/uc?export=download&id=1I-ggSNmhWfuP8TKR58MhBcjbHPcMtbK8");

                if (int.Parse(newVersion.Replace(".", "")) > int.Parse(currentVersion.Replace(".", "")))
                {
                    var result = MessageBox.Show($"Доступна версия {newVersion}. Обновить?", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        UpdatingProcess loadingWindow = new UpdatingProcess(newVersion, rootPath, zipPath, versionPath);

                        loadingWindow.Owner = this;

                        loadingWindow.Show();
                    }

                }
                else
                {
                    MessageBox.Show($"Версия актуальна");
                }

            }
        }
    }
}
