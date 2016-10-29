using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Reflection;

namespace ClipboardManager
{
    public static class Settings
    {
        private static readonly string SettingsFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + ClipboardManagerTray.ApplicationName + "\\config.xml";
        private static readonly string RegKeyWinAutostart = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private static readonly string RegKeyWinAutostartValue = "\"" + Assembly.GetExecutingAssembly().Location + "\"";

        public static event EventHandler SettingsChanged;
        
        public static int MaxEntries { get; set; }
        public static int HkMod { get; set; }
        public static int HkKey { get; set; }
        
        public static bool Autostart
        {
            get { return IsAutostart(); }
            set { SetAutostart(value); }
        }

        private static bool IsAutostart()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(RegKeyWinAutostart, true);
            object value = regKey?.GetValue(ClipboardManagerTray.ApplicationName);
            if (value is string)
            {
                var str = value as string;
                return RegKeyWinAutostartValue.Equals(str, StringComparison.CurrentCultureIgnoreCase);
            }
            return false;
        }

        private static void SetAutostart(bool enable)
        {
            if (enable)
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(RegKeyWinAutostart, true);
                if (regKey != null)
                {
                    try
                    {
                        regKey.SetValue(ClipboardManagerTray.ApplicationName, RegKeyWinAutostartValue);
                    }
                    catch
                    {
                        Debug.WriteLine("Unable to write autostart settings");
                    }
                }
            }
            else
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(RegKeyWinAutostart, true);
                if (regKey?.GetValue(ClipboardManagerTray.ApplicationName) != null)
                    try { regKey.DeleteValue(ClipboardManagerTray.ApplicationName); }
                    catch
                    {
                        Debug.WriteLine("Unable to delete autostart settings");
                    }
            }
        }
        
        public static void Save()
        {
            string dir = Path.GetDirectoryName(SettingsFile);
            if (dir != null)
            {
                Directory.CreateDirectory(dir);
            }
            using (XmlWriter writer = XmlWriter.Create(SettingsFile))
            {
                writer.WriteStartElement("Settings");
                writer.WriteElementString("MaxEntries", Convert.ToString(MaxEntries));
                writer.WriteElementString("HkMod", Convert.ToString(HkMod));
                writer.WriteElementString("HkKey", Convert.ToString(HkKey));
                writer.WriteEndElement();
            }
            OnSettingsChanged();
        }

        public static void Load()
        {
            if (File.Exists(SettingsFile))
            {
                using (XmlReader reader = XmlReader.Create(SettingsFile))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            try
                            {
                                switch (reader.Name)
                                {
                                    case "MaxEntries":
                                        MaxEntries = Convert.ToInt32(reader.Value);
                                        break;
                                    case "HkMod":
                                        HkMod = Convert.ToInt32(reader.Value);
                                        break;
                                    case "HkKey":
                                        HkKey = Convert.ToInt32(reader.Value);
                                        break;
                                }
                            }
                            catch
                            {
                                LoadDefault();
                                return;
                            }
                        }
                    }
                }
                if (MaxEntries <= 0 || HkMod <= 0 || HkKey <= 0)
                {
                    LoadDefault();
                    return;
                }
                else
                {
                    OnSettingsChanged();
                    return;
                }
            }
            LoadDefault();
        }

        private static void LoadDefault()
        {
            MaxEntries = 10;
            HkMod = 0;
            HkKey = 0;
            OnSettingsChanged();
        }

        private static void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
