using BuildWatcher.Models.Configs;

namespace BuildWatcher.Core.Watchers
{
    public class NasWatcherService
    {
        private readonly ProjectConfig _config;
        private FileSystemWatcher? _watcher;

        public NasWatcherService(ProjectConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Start()
        {
            if (string.IsNullOrWhiteSpace(_config.RootPath))
                throw new Exception("RootPath EMPTY in config");

            _watcher = new FileSystemWatcher(_config.RootPath)
            {
                IncludeSubdirectories = true,
                EnableRaisingEvents = true
            };

            _watcher.Created += OnCreated;
            _watcher.Changed += OnChanged;
            _watcher.Renamed += OnRenamed;
        }

        public void Stop()
        {
            if (_watcher == null) return;

            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
            _watcher = null;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Log($"CREATED: {e.FullPath}");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Log($"CHANGED: {e.FullPath}");
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Log($"RENAMED: {e.OldFullPath} -> {e.FullPath}");
        }

        private static void Log(string msg)
        {
            var logPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "watcher.log");

            File.AppendAllText(
                logPath,
                $"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
        }
    }
}
