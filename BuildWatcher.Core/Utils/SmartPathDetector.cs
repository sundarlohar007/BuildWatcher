using System;
using System.Linq;

namespace BuildWatcher.Core.Utils
{
    public static class SmartPathDetector
    {
        public static string DetectPlatform(string path)
        {
            var p = path.ToLowerInvariant();

            if (p.Contains("ps5") || p.Contains("playstation") || p.Contains("sony"))
                return "PS5";
            if (p.Contains("xbox"))
                return "Xbox";
            if (p.Contains("android") || p.EndsWith(".apk"))
                return "Android";
            if (p.Contains("ios") || p.EndsWith(".ipa"))
                return "iOS";
            if (p.Contains("switch") || p.EndsWith(".nsp") || p.EndsWith(".xci"))
                return "Switch";
            if (p.EndsWith(".exe") || p.EndsWith(".msi"))
                return "PC";

            return "Unknown";
        }

        public static string DetectBranch(string[] pathParts)
        {
            foreach (var part in pathParts.Select(p => p.ToLowerInvariant()))
            {
                if (part.Contains("release") || part.Contains("final") || part.Contains("prod"))
                    return "Release";
                if (part.Contains("debug") || part.Contains("dev"))
                    return "Debug";
                if (part.Contains("qa") || part.Contains("test"))
                    return "QA";
                if (part.Contains("staging"))
                    return "Staging";
            }

            return "Unknown";
        }
    }
}
