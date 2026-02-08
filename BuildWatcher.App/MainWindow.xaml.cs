using System;
using System.IO;
using System.Text.Json;
using System.Windows;

using BuildWatcher.Models.Configs;
using BuildWatcher.Models.Hierarchy;
using BuildWatcher.Core.Scanners;

namespace BuildWatcher.App
{
    public partial class MainWindow : Window
    {
        public BuildHierarchy Hierarchy { get; set; }

        private const string CONFIG_FILE = "SampleProject.json";

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(CONFIG_FILE))
                {
                    System.Windows.MessageBox.Show(
                        $"Config file not found:\n{Path.GetFullPath(CONFIG_FILE)}",
                        "Startup Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }

                var json = File.ReadAllText(CONFIG_FILE);
                var config = JsonSerializer.Deserialize<ProjectConfig>(json);

                if (config == null || string.IsNullOrWhiteSpace(config.RootPath))
                {
                    System.Windows.MessageBox.Show(
                        "Invalid project config (RootPath empty)",
                        "Startup Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }

                var scanner = new NasHierarchyScanner();
                Hierarchy = scanner.Scan(config.RootPath, config.ProjectName);

                DataContext = null;
                DataContext = this;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    ex.Message,
                    "Hierarchy Load Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
