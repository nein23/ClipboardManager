using Microsoft.Win32;
using System;
using System.Reflection;

namespace ClipboardManager
{
    public class Settings
    {
        public static readonly String REG_KEY_SETTINGS = @"Software\KaeDoWare\" + ClipboardManager.APPLICATION_NAME;
        public static readonly String REG_KEY_SETTINGS_NUM_HISTORY = "NumHistory";
        public static readonly String REG_KEY_SETTINGS_STORE_AT_CLEAR = "StoreAtClear";
        public static readonly String REG_KEY_WIN_AUTOSTART = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        public static readonly String REG_KEY_WIN_AUTOSTART_VALUE = "\"" + Assembly.GetExecutingAssembly().Location + "\"";
        public static readonly int DEFAULT_NUM_HISTORY = 10;
        public static readonly bool DEFAULT_STORE_AT_CLEAR = false;

        private bool autostart;
        private int numHistory;
        private bool storeAtClear;

        public bool Autostart { get { return autostart; } set { setAutostart(value); } }
        public int NumHistory { get { return numHistory; } set { setNumHistory(value); } }
        public bool StoreAtClear { get { return storeAtClear; } set { setStoreAtClear(value); } }

        public Settings()
        {
            cleanUp();
            loadAutostart();
            loadNumHistory();
            loadStoreAtClear();
        }

        private void cleanUp()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REG_KEY_SETTINGS, true);
            if (regKey != null)
            {
                foreach (string value in regKey.GetValueNames())
                {
                    if (!REG_KEY_SETTINGS_NUM_HISTORY.Equals(value) && !REG_KEY_SETTINGS_STORE_AT_CLEAR.Equals(value))
                    {
                        regKey.DeleteValue(value, false);
                    }
                }
            }
        }

        private void loadAutostart()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REG_KEY_WIN_AUTOSTART, true);
            if (regKey != null)
            {
                Object value = regKey.GetValue(ClipboardManager.APPLICATION_NAME);
                if (value != null && value.GetType() == typeof(String))
                {
                    String str = (String)value;
                    if (REG_KEY_WIN_AUTOSTART_VALUE.Equals(str, StringComparison.CurrentCultureIgnoreCase)) autostart = true;
                    else autostart = false;
                }
                else autostart = false;
            }
            else autostart = false;
        }

        private void loadNumHistory()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                Object value = regKey.GetValue(REG_KEY_SETTINGS_NUM_HISTORY);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val > 0 && val <= 20) numHistory = val;
                    else numHistory = DEFAULT_NUM_HISTORY;
                }
                else numHistory = DEFAULT_NUM_HISTORY;
            }
            else numHistory = DEFAULT_NUM_HISTORY;
        }

        private void loadStoreAtClear()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                Object value = regKey.GetValue(REG_KEY_SETTINGS_STORE_AT_CLEAR);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val == 0) storeAtClear = false;
                    else storeAtClear = true;
                }
                else storeAtClear = DEFAULT_STORE_AT_CLEAR;
            }
            else storeAtClear = DEFAULT_STORE_AT_CLEAR;
        }

        private RegistryKey getSettingsRegKey(bool writeAccess)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REG_KEY_SETTINGS, true);
            if (regKey == null)
            {
                regKey = Registry.CurrentUser.CreateSubKey(REG_KEY_SETTINGS);
                regKey = Registry.CurrentUser.OpenSubKey(REG_KEY_SETTINGS, writeAccess);
            }
            return regKey;
        }
        private void setAutostart(bool enable)
        {
            if (enable)
            {
                setCurUserRegValue(REG_KEY_WIN_AUTOSTART, ClipboardManager.APPLICATION_NAME, REG_KEY_WIN_AUTOSTART_VALUE);
            }
            else
            {
                deleteCurUserRegValue(REG_KEY_WIN_AUTOSTART, ClipboardManager.APPLICATION_NAME);
            }
            loadAutostart();
        }

        private void setNumHistory(int num)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_NUM_HISTORY, num);
            loadNumHistory();
        }

        private void setStoreAtClear(bool enable)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_STORE_AT_CLEAR, enable ? 1 : 0);
            loadStoreAtClear();
        }

        private void setCurUserRegValue(string key, string name, object value)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(key, true);
            if (regKey != null)
            {
                try { regKey.SetValue(name, value); }
                catch { }
            }
        }

        private void deleteCurUserRegValue(string key, string name)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(key, true);
            if (regKey != null)
            {
                if(regKey.GetValue(name) != null)
                try { regKey.DeleteValue(name); }
                catch { }
            }
        }
    }
}
