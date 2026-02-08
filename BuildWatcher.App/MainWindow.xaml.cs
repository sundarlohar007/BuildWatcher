using BuildWatcher.Core;
using BuildWatcher.Core.Watchers;
using BuildWatcher.Models.Configs;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace BuildWatcher.App
{
    public partial class MainWindow : Window
    {
        private NasWatcherService? _watcher;
        private TrayService? _trayService; // Add this field to store the tray service instance

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(ConfigPaths.ConfigFile))
            {
                var wizard = new FirstRunWizard();
                if (wizard.ShowDialog() != true)
                {
                    System.Windows.Application.Current.Shutdown();
                    return;
                }
            }

            var json = File.ReadAllText(ConfigPaths.ConfigFile);
            var config = JsonSerializer.Deserialize<ProjectConfig>(json);

            _watcher = new NasWatcherService(config!);
            _watcher.Start();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (_trayService == null)
                {
                    _trayService = TrayService.Create(this);
                }
                // No Show() method, so just ensure the tray icon is created.
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _watcher?.Stop();
            base.OnClosed(e);
        }
    }
}
