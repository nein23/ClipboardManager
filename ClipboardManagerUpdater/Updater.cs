using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ClipboardManagerUpdater
{
    public class Updater : ApplicationContext
    {
        private static readonly string UPDATE_URL = "https://github.com/nein23/ClipboardManager/releases/download/";
        private static readonly string UPDATE_URL_FILENAME = "/ClipboardManager.exe";

        private static readonly string TITLE_TMP_EX = "Access Error";
        private static readonly string TEXT_TMP_EX = "Error accessing temp folder";
        private static readonly string TITLE_CONNECTION_ERRROR = "Connection Error";
        private static readonly string TEXT_CONNECTION_ERRROR = "Check your internet connection";
        private static readonly string TITLE_REPLACE_EX = "Access Error";
        private static readonly string TEXT_REPLACE_EX = "Error patching executable";
        private static readonly string TITLE_SUCCESS = "Update successful";
        private static readonly string TEXT_SUCCESS = "Updated to Version ";

        private ToastForm toast = null;

        public Updater(string processName, string appPath, string version)
        {
            closeClipboardManager(processName);

            string tmpFile = getTempFile();
            if(tmpFile == null)
            {
                showToast(TITLE_TMP_EX, TEXT_TMP_EX, appPath);
            }
            WebClient client = new WebClient();
            try { client.DownloadFile(UPDATE_URL + version + UPDATE_URL_FILENAME, tmpFile); }
            catch
            {
                showToast(TITLE_CONNECTION_ERRROR, TEXT_CONNECTION_ERRROR, appPath);
            }

            try {
                byte[] content = File.ReadAllBytes(tmpFile);
                File.WriteAllBytes(appPath, content);
            }
            catch
            {
                showToast(TITLE_REPLACE_EX, TEXT_REPLACE_EX, appPath);
            }
            showToast(TITLE_SUCCESS, TEXT_SUCCESS + version, appPath);
        }

        private string getTempFile()
        {
            try { return Path.GetTempFileName(); }
            catch { return null; }
        }

        private void showToast(string title, string text, string fileName)
        {
            toast = new ToastForm(title, text, fileName);
            toast.ShowDialog();
            exit();
        }

        private void closeClipboardManager(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                process.Kill();
                process.WaitForExit();
            }
        }

        private void exit()
        {
            if (Application.MessageLoop) Application.Exit();
            else Environment.Exit(1);
        }
    }
}
