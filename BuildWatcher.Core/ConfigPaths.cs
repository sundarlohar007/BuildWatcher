using System;
using System.IO;

namespace BuildWatcher.Core
{
    public static class ConfigPaths
    {
        public static string AppFolder =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "BuildWatcher"
            );

        public static string ConfigFile =>
            Path.Combine(AppFolder, "config.json");

        public static void EnsureFolders()
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
        }
    }
}
