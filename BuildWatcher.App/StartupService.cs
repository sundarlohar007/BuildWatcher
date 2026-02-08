using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;

namespace BuildWatcher.App
{
    public static class StartupService
    {
        private const string RUN_KEY =
            @"Software\Microsoft\Windows\CurrentVersion\Run";

        private const string APP_NAME = "BuildWatcher";

        public static void Enable()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, true);
            if (key == null)
                throw new Exception("Startup registry key not found");

            // 🔥 Correct EXE path (works in Debug + Release)
            var exePath = Process.GetCurrentProcess().MainModule?.FileName;

            if (string.IsNullOrWhiteSpace(exePath) || !File.Exists(exePath))
                throw new Exception("Executable path could not be resolved");

            key.SetValue(APP_NAME, $"\"{exePath}\"");
        }

        public static void Disable()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, true);
            key?.DeleteValue(APP_NAME, false);
        }

        public static bool IsEnabled()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, false);
            return key?.GetValue(APP_NAME) != null;
        }
    }
}
