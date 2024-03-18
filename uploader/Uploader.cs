using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;

namespace uploader
{
    class Uploader
    {
        private View view;
        private string rootPath;
        private string zipPath;
        private string newVersion;
        private string versionPath;
        

        public Uploader(View _view, string _newVersion, string _rootPath, string _zipPath, string _versionPath) {

            view = _view;
            rootPath = _rootPath;
            zipPath = _zipPath;
            newVersion = _newVersion;
            versionPath = _versionPath;
            Upload();
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
    }
}
