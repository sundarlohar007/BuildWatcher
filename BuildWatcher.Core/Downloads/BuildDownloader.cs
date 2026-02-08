using System;
using System.IO;
using System.Security.Cryptography;
using BuildWatcher.Models.History;

namespace BuildWatcher.Core.Downloads
{
    public static class BuildDownloader
    {
        // OPTION A
        public static void SimpleCopy(BuildInfo build, string targetRoot)
        {
            Directory.CreateDirectory(targetRoot);
            var dest = Path.Combine(targetRoot, build.FileName);
            File.Copy(build.FullPath, dest, overwrite: false);
        }

        // OPTION B
        public static void SmartCopy(BuildInfo build, string targetRoot)
        {
            var folder = Path.Combine(
                targetRoot,
                build.DetectedAt.ToString("yyyy-MM-dd_HH-mm-ss")
            );

            Directory.CreateDirectory(folder);
            var dest = Path.Combine(folder, build.FileName);

            using var source = new FileStream(build.FullPath, FileMode.Open, FileAccess.Read);
            using var target = new FileStream(dest, FileMode.CreateNew, FileAccess.Write);

            source.CopyTo(target);

            if (!HashesMatch(build.FullPath, dest))
                throw new IOException("Hash mismatch after copy");
        }

        private static bool HashesMatch(string a, string b)
        {
            using var sha = SHA256.Create();
            using var fa = File.OpenRead(a);
            using var fb = File.OpenRead(b);

            return Convert.ToHexString(sha.ComputeHash(fa)) ==
                   Convert.ToHexString(sha.ComputeHash(fb));
        }
    }
}
