using BuildWatcher.Models.Configs;
using BuildWatcher.Models.Hierarchy;

namespace BuildWatcher.Core.Scanners
{
    public static class NasHierarchyScanner
    {
        public static List<PlatformNode> Scan(ProjectConfig config)
        {
            var result = new List<PlatformNode>();

            foreach (var platformDir in Directory.GetDirectories(config.RootPath))
            {
                var platform = new PlatformNode
                {
                    Name = Path.GetFileName(platformDir)
                };

                foreach (var branchDir in Directory.GetDirectories(platformDir))
                {
                    var branch = new BranchNode
                    {
                        Name = Path.GetFileName(branchDir)
                    };

                    foreach (var file in Directory.GetFiles(branchDir))
                    {
                        var ext = Path.GetExtension(file).ToLowerInvariant();

                        if (!config.AllowedExtensions.Contains(ext))
                            continue;

                        branch.Builds.Add(new BuildNode
                        {
                            FileName = Path.GetFileName(file),
                            FullPath = file,
                            DetectedAt = DateTime.Now
                        });
                    }

                    if (branch.Builds.Any())
                        platform.Branches.Add(branch);
                }

                if (platform.Branches.Any())
                    result.Add(platform);
            }

            return result;
        }
    }
}
