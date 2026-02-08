using System;

namespace BuildWatcher.Models.History
{
    public class BuildInfo
    {
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;

        public string Platform { get; set; } = "Unknown";
        public string BuildFolder { get; set; } = string.Empty;

        public DateTime DetectedAt { get; set; }

        public long FileSizeBytes { get; set; }
    }
}
