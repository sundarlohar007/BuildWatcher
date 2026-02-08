using System;
using BuildWatcher.Models.History;

namespace BuildWatcher.Models.History
{
    public class BuildDownloadJob
    {
        public BuildInfo Build { get; set; } = null!;
        public DateTime QueuedAt { get; set; } = DateTime.Now;
    }
}
