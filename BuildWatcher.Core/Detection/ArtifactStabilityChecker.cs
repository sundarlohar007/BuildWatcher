using System;
using System.IO;
using System.Threading;

namespace BuildWatcher.Core.Detection
{
    public static class ArtifactStabilityChecker
    {
        public static bool IsFileStable(string filePath, int waitMs = 2000)
        {
            if (!File.Exists(filePath))
                return false;

            try
            {
                var size1 = new FileInfo(filePath).Length;
                Thread.Sleep(waitMs);
                var size2 = new FileInfo(filePath).Length;

                return size1 == size2;
            }
            catch
            {
                return false;
            }
        }
    }
}
