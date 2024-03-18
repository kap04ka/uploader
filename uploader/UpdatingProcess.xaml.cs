using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace uploader
{
    /// <summary>
    /// Логика взаимодействия для UpdatingProcess.xaml
    /// </summary>
    public partial class UpdatingProcess : Window
    {
        Uploader uploader;
        View view;
        public UpdatingProcess(string newVersion, string rootPath, string zipPath, string versionPath)
        {
            InitializeComponent();
            view = new View();
            DataContext = view;
            uploader = new Uploader(view, newVersion, rootPath, zipPath, versionPath);
        }

    }
}
