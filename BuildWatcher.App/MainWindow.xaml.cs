using BuildWatcher.Core.Scanning;
using BuildWatcher.Models.Configs;
using BuildWatcher.Models.Hierarchy;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace BuildWatcher.App
{
    public partial class MainWindow : Window
    {
        public BuildHierarchy Hierarchy { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(ConfigPaths.ConfigFile))
                {
                    MessageBox.Show("Project config not found", "Startup Error");
                    return;
                }

                var json = File.ReadAllText(ConfigPaths.ConfigFile);
                var config = JsonSerializer.Deserialize<ProjectConfig>(json);

                if (config == null || string.IsNullOrWhiteSpace(config.RootPath))
                {
                    MessageBox.Show("Invalid project config", "Startup Error");
                    return;
                }

                var scanner = new NasHierarchyScanner();
                Hierarchy = scanner.Scan(config.RootPath, config.ProjectName);

                DataContext = null;
                DataContext = this;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Hierarchy Load Failed");
            }
        }
    }
}