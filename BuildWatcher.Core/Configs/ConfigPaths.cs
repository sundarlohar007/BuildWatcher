using System;
using System.IO;

namespace BuildWatcher.Core.Configs
{
    public static class ConfigPaths
    {
        public static string AppDataRoot =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "BuildWatcher"
            );

        public static string ProjectConfigPath =>
            Path.Combine(AppDataRoot, "project.json");
    }
}
