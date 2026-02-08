using System.Collections.Generic;

namespace BuildWatcher.Models.Configs
{
    // 🔥 THIS IS FOR projects.json (MULTI-PROJECT)
    public class ProjectIndex
    {
        public string DefaultProject { get; set; } = string.Empty;
        public List<ProjectEntry> Projects { get; set; } = new();
    }

    public class ProjectEntry
    {
        public string Name { get; set; } = string.Empty;
        public string ConfigFile { get; set; } = string.Empty;
    }
}
