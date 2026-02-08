# BuildWatcher 🚀
BuildWatcher is a lightweight C# WPF application designed to monitor local or network directories (like a NAS) for new file changes. It was built to streamline the QA workflow by providing instant notifications whenever a new game build is dropped.

✨ Features
Real-time Monitoring: Watches specific folders for new .zip, .exe, or .apk files.
NAS Compatibility: Handles network path monitoring for team-shared builds.
Instant Notifications: Sends a desktop alert as soon as a build is detected, so you don't have to keep refreshing the folder.
Clean UI: A simple WPF interface to manage your watch list.

🛠️ Tech Stack
Language: C#
Framework: WPF (.NET 8.0/9.0)
IDE: Visual Studio 2026

🚀 Getting Started
Prerequisites
.NET Runtime (matching your project version).
Access to the target directory or NAS folder you want to monitor.
Installation
Clone the repo:
Bash
git clone https://github.com/sundarlohar007/BuildWatcher.git
Open the Solution:
Launch BuildWatcher.sln in Visual Studio 2026.
Build & Run:
Press F5 to start the application.

📖 Usage
Launch the app.
Enter the path of the NAS folder or local directory you want to watch.
Minimize the app to the system tray.
Get notified the second a new build is uploaded!

📜 License
This project is licensed under the MIT License.
