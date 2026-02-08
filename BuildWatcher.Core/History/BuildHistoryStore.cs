using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using BuildWatcher.Models.History;

namespace BuildWatcher.Core.History
{
    public class BuildHistoryStore
    {
        private readonly string _historyFile;

        public BuildHistoryStore()
        {
            _historyFile = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "build-history.json"
            );
        }

        public void Add(BuildInfo build)
        {
            var all = LoadAll();
            all.Add(build);
            SaveAll(all);
        }

        public List<BuildInfo> LoadAll()
        {
            if (!File.Exists(_historyFile))
                return new List<BuildInfo>();

            var json = File.ReadAllText(_historyFile);
            return JsonSerializer.Deserialize<List<BuildInfo>>(json)
                   ?? new List<BuildInfo>();
        }

        private void SaveAll(List<BuildInfo> builds)
        {
            var json = JsonSerializer.Serialize(
                builds,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(_historyFile, json);
        }
    }
}
