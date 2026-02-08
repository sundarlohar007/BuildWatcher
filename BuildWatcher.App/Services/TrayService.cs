using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace BuildWatcher.App.Services
{
    public class TrayService : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly Window _mainWindow;

        public TrayService(Window mainWindow)
        {
            _mainWindow = mainWindow;

            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Text = "BuildWatcher",
                Visible = true
            };

            _notifyIcon.DoubleClick += (_, __) => ShowMainWindow();

            var menu = new ContextMenuStrip();
            menu.Items.Add("Open", null, (_, __) => ShowMainWindow());
            menu.Items.Add("Exit", null, (_, __) => ExitApp());

            _notifyIcon.ContextMenuStrip = menu;
        }

        public void ShowNotification(string title, string message)
        {
            _notifyIcon.BalloonTipTitle = title;
            _notifyIcon.BalloonTipText = message;
            _notifyIcon.ShowBalloonTip(3000);
        }

        private void ShowMainWindow()
        {
            _mainWindow.Dispatcher.Invoke(() =>
            {
                _mainWindow.Show();
                _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.Activate();
            });
        }

        private void ExitApp()
        {
            _notifyIcon.Visible = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                Application.Current.Shutdown();
            });
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }
    }
}
