using ClipboardManager.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace ClipboardManager
{
    public class Updater
    {
        public static readonly string VERSION_URL = "https://raw.githubusercontent.com/nein23/ClipboardManager/master/version";
        public static readonly string UPDATER_FILE_NAME = "ClipboardManagerUpdater.exe";

        private string title;
        private string text;
        private Version newestVersion;
        private bool updateAvailable = false;

        public string Title { get { return title; } }
        public string Text { get { return text; } }
        public string NewestVersion { get { return newestVersion == null ? null : TrimVersion(newestVersion); } }
        public bool UpdateAvailable { get { return updateAvailable; } }


        internal void checkForUpdate()
        {
            WebClient client = new WebClient();
            newestVersion = getNewestVersion();
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            if (newestVersion == null)
            {
                title = "Connection Error";
                text = "Unable to request information";
            }
            else if (newestVersion > version)
            {
                title = "New Version available";
                text = "Version " + NewestVersion + " is available";
                updateAvailable = true;
            }
            else
            {
                title = "No Updates available";
                text = "Version " + TrimVersion(version) + " is up-to-date";
                newestVersion = version;
            }
        }

        private Version getNewestVersion()
        {
            try
            {
                WebClient client = new WebClient();
                string str = client.DownloadString(VERSION_URL);
                Version newestVersion = null;
                Version.TryParse(str, out newestVersion);
                return newestVersion;
            }
            catch
            {
                return null;
            }
        }

        private string TrimVersion(Version v)
        {
            string s = v.ToString();
            while (s.EndsWith(".0"))
            {
                s = s.Substring(0, s.Length - 2);
            }
            return s;
        }

        private string getTempFile()
        {
            try { return Path.GetTempFileName(); }
            catch { return null; }
        }

        private string getTempFolder()
        {
            try { return Path.GetTempPath(); }
            catch { return null; }
        }

        internal void update()
        {
            string tmpPath = getTempFolder();
            if (tmpPath != null)
            {
                try
                {
                    tmpPath += "\\" + ClipboardManagerTray.APPLICATION_NAME;
                    Directory.CreateDirectory(tmpPath);
                    string tmpFile = tmpPath + "\\" + UPDATER_FILE_NAME;
                    System.IO.File.WriteAllBytes(tmpFile, Resources.ClipboardManagerUpdater);
                    ProcessStartInfo info = new ProcessStartInfo(tmpFile);
                    string processName = Process.GetCurrentProcess().ProcessName;
                    string applicationPath = "\"" + Assembly.GetExecutingAssembly().Location + "\"";
                    info.Arguments = processName + " " + applicationPath + " " + NewestVersion;
                    info.UseShellExecute = true;
                    info.Verb = "runas";
                    Process process = Process.Start(info);
                }
                catch { }
            }
        }
    }
}
