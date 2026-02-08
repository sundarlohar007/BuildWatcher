using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using BuildWatcher.Core.History;
using BuildWatcher.Models.History;

namespace BuildWatcher.App.ViewModels
{
    public class BuildHistoryViewModel
    {
        private readonly BuildHistoryStore _store;
        private readonly Dispatcher _ui;
        private readonly FileSystemWatcher _watcher;

        public ObservableCollection<BuildInfo> Builds { get; }

        public event Action<BuildInfo>? NewBuildArrived;

        public BuildHistoryViewModel()
        {
            _store = new BuildHistoryStore();
            _ui = Dispatcher.CurrentDispatcher;

            Builds = new ObservableCollection<BuildInfo>(_store.LoadAll());

            var historyFile = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "build-history.json"
            );

            _watcher = new FileSystemWatcher(
                Path.GetDirectoryName(historyFile)!,
                Path.GetFileName(historyFile)!
            )
            {
                EnableRaisingEvents = true
            };

            _watcher.Changed += OnHistoryChanged;
            _watcher.Created += OnHistoryChanged;
        }

        private void OnHistoryChanged(object sender, FileSystemEventArgs e)
        {
            _ui.Invoke(() =>
            {
                var latest = _store.LoadAll();
                var lastKnown = Builds.LastOrDefault();

                Builds.Clear();
                foreach (var b in latest)
                    Builds.Add(b);

                var newest = latest.LastOrDefault();
                if (newest != null &&
                    (lastKnown == null || newest.DetectedAt > lastKnown.DetectedAt))
                {
                    NewBuildArrived?.Invoke(newest);
                }
            });
        }
    }
}
