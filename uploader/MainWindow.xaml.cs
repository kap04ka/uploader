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

        View view;

        public MainWindow()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            versionPath = Path.Combine(rootPath, "Version.txt");
            currentVersion = File.ReadAllText(versionPath);
            zipPath = Path.Combine(rootPath, "Build.zip");
            view = new View();
            DataContext = view;

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
                        uploadingBar.Visibility = Visibility.Visible;
                        Upload();
                        
                    }

                }
                else
                {
                    MessageBox.Show($"Версия актуальна");
                }

            }
        }

        public void Upload()
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) => view.progress = e.ProgressPercentage;
                client.DownloadProgressChanged += (s, e) => view.speedCalculate(e.BytesReceived);
                client.DownloadFileCompleted += (s, e) =>
                {
                    UploadCompleted();
                    MessageBox.Show("Загрузка завершена");
                    UpdateForm();
                };

                client.DownloadFileAsync(new System.Uri("https://drive.google.com/uc?export=download&id=1wShEa8ju41uabVel8N0iHXuokWWyZdoi"), zipPath);
            }
        }

        public void UploadCompleted()
        {
            ZipFile.ExtractToDirectory(zipPath, rootPath, true);
            File.Delete(zipPath);
            File.WriteAllText(versionPath, newVersion);
        }

        public void UpdateForm()
        {
            curVer.Text = "Текущая версия: " + newVersion;
            uploadingBar.Visibility = Visibility.Hidden;
            uploadingSpeed.Text = "";
        }
    }
}
