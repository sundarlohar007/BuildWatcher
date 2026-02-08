using System;
using System.IO;
using BuildWatcher.Models.History;

namespace BuildWatcher.Core.Metadata
{
    public static class BuildMetadataExtractor
    {
        public static BuildInfo Extract(string fullPath)
        {
            var file = new FileInfo(fullPath);

            return new BuildInfo
            {
                FileName = file.Name,
                FullPath = fullPath,
                FileSizeBytes = file.Length,
                DetectedAt = DateTime.Now,
                BuildFolder = file.Directory?.Name ?? string.Empty,
                Platform = DetectPlatform(file.Name)
            };
        }

        private static string DetectPlatform(string fileName)
        {
            var name = fileName.ToLower();

            if (name.Contains("android")) return "Android";
            if (name.Contains("ios")) return "iOS";
            if (name.Contains("ps5")) return "PS5";
            if (name.Contains("xbox")) return "Xbox";
            if (name.Contains("switch")) return "Nintendo";
            if (name.Contains("pc") || name.Contains("win")) return "PC";

            return "Unknown";
        }
    }
}
