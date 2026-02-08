using System;
using System.Collections.Generic;

namespace BuildWatcher.Models.Hierarchy
{
    public class ProjectNode
    {
        public string RootPath { get; set; } = string.Empty;
        public List<PlatformNode> Platforms { get; set; } = new();
    }

    public class PlatformNode
    {
        public string Name { get; set; } = string.Empty;
        public List<BranchNode> Branches { get; set; } = new();
    }

    public class BranchNode
    {
        public string Name { get; set; } = string.Empty;
        public List<BuildNode> Builds { get; set; } = new();
    }

    public class BuildNode
    {
        public string FileName { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public DateTime DetectedAt { get; set; }
    }
}
