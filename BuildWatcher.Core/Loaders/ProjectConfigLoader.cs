using System.Text.Json;
using BuildWatcher.Models.Configs;

namespace BuildWatcher.Core.Loaders
{
    public static class ProjectConfigLoader
    {
        private const string ConfigFileName = "SampleProject.json";

        public static ProjectConfig Load()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var configPath = Path.Combine(baseDir, ConfigFileName);

            if (!File.Exists(configPath))
                throw new FileNotFoundException("Project config not found", configPath);

            var json = File.ReadAllText(configPath);

            var config = JsonSerializer.Deserialize<ProjectConfig>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (config == null)
                throw new Exception("Failed to parse project config");

            if (string.IsNullOrWhiteSpace(config.RootPath))
                throw new Exception("RootPath EMPTY in config");

            if (!Directory.Exists(config.RootPath))
                throw new DirectoryNotFoundException(config.RootPath);

            return config;
        }
    }
}
