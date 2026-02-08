using BuildWatcher.Core;
using BuildWatcher.Models.Configs;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace BuildWatcher.App
{
    public partial class FirstRunWizard : Window
    {
        public FirstRunWizard()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            if (dialog.ShowDialog() == true)
            {
                PathBox.Text = dialog.FolderName;
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PathBox.Text) || !Directory.Exists(PathBox.Text))
            {
                System.Windows.MessageBox.Show("Please select a valid NAS folder.");
                return;
            }

            ConfigPaths.EnsureFolders();

            var config = new ProjectConfig
            {
                ProjectName = "Default",
                RootPath = PathBox.Text,
                AutoDownload = true
            };

            File.WriteAllText(
                ConfigPaths.ConfigFile,
                JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true })
            );

            DialogResult = true;
            Close();
        }
    }
}
