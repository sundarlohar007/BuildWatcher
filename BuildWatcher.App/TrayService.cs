using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace BuildWatcher.App
{
    public class TrayService : IDisposable
    {
        private static TrayService? _instance;
        private readonly NotifyIcon _notifyIcon;

        public static TrayService Create(Window mainWindow)
        {
            if (_instance != null)
                return _instance;

            _instance = new TrayService(mainWindow);
            return _instance;
        }

        private TrayService(Window mainWindow)
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Text = "BuildWatcher",
                Visible = true
            };

            var menu = new ContextMenuStrip();

            menu.Items.Add("Open", null, (_, _) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    mainWindow.Show();
                    mainWindow.WindowState = WindowState.Normal;
                    mainWindow.Activate();
                });
            });

            menu.Items.Add("Exit", null, (_, _) =>
            {
                System.Windows.Application.Current.Shutdown();
            });

            _notifyIcon.ContextMenuStrip = menu;

            _notifyIcon.DoubleClick += (_, _) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    mainWindow.Show();
                    mainWindow.WindowState = WindowState.Normal;
                    mainWindow.Activate();
                });
            };
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            _instance = null;
        }
    }

}
