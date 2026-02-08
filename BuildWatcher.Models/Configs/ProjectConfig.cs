using System.Collections.Generic;

namespace BuildWatcher.Models.Configs
{
    public class ProjectConfig
    {
        public string ProjectName { get; set; } = string.Empty;
        public string RootPath { get; set; } = string.Empty;

        public List<string> AllowedExtensions { get; set; } = new();

        public bool AutoDownload { get; set; }
        public string DownloadMode { get; set; } = "Manual";
        public string LocalDownloadPath { get; set; } = string.Empty;
    }
}
