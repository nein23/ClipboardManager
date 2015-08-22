using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace ClipboardManager
{
    public class Settings
    {
        public static readonly String REG_KEY_SETTINGS = @"Software\KaeDoWare\" + ClipboardManager.APPLICATION_NAME;
        public static readonly String REG_KEY_SETTINGS_NUM_HISTORY = "NumHistory";
        public static readonly String REG_KEY_SETTINGS_STORE_AT_CLEAR = "StoreAtClear";
        public static readonly String REG_KEY_SETTINGS_LIFE_TIME_ENABLED = "LifeTimeEnabled";
        public static readonly String REG_KEY_SETTINGS_LIFE_TIME_VALUE = "LifeTimeValue";
        public static readonly String REG_KEY_SETTINGS_FILTER_TEXT = "FilterText";
        public static readonly String REG_KEY_SETTINGS_FILTER_FILES = "FilterFiles";
        public static readonly String REG_KEY_SETTINGS_FILTER_IMAGES = "FilterImages";
        public static readonly String REG_KEY_SETTINGS_FILTER_AUDIO = "FilterAudio";
        public static readonly String REG_KEY_SETTINGS_FILTER_MULTI = "FilterMulti";
        public static readonly String REG_KEY_SETTINGS_FILTER_UNKNOWN = "FilterUnknown";
        public static readonly String REG_KEY_SETTINGS_HK_MOD = "HK_Mod";
        public static readonly String REG_KEY_SETTINGS_HK_KEY = "HK_Key";
        public static readonly String REG_KEY_WIN_AUTOSTART = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        public static readonly String REG_KEY_WIN_AUTOSTART_VALUE = "\"" + Assembly.GetExecutingAssembly().Location + "\"";
        public static readonly int DEFAULT_NUM_HISTORY = 10;
        public static readonly bool DEFAULT_STORE_AT_CLEAR = false;
        public static readonly bool DEFAULT_LIFE_TIME_ENABLED = false;
        public static readonly int DEFAULT_LIFE_TIME_VALUE = 5;
        public static readonly bool DEFAULT_FILTER_TEXT = true;
        public static readonly bool DEFAULT_FILTER_FILES = true;
        public static readonly bool DEFAULT_FILTER_IMAGES = true;
        public static readonly bool DEFAULT_FILTER_AUDIO = false;
        public static readonly bool DEFAULT_FILTER_MULTI = true;
        public static readonly bool DEFAULT_FILTER_UNKNOWN = false;
        public static readonly int DEFAULT_HK_MOD = (int)Util.KeyModifier.Control | (int)Util.KeyModifier.Shift;
        public static readonly int DEFAULT_HK_KEY = (int)Keys.C;

        private bool autostart;
        private int numHistory;
        private bool storeAtClear;
        private bool lifeTimeEnabled;
        private int lifeTimeValue;
        private bool filterText;
        private bool filterFiles;
        private bool filterImages;
        private bool filterAudio;
        private bool filterMulti;
        private bool filterUnknown;
        private int hk_mod;
        private int hk_key;

        internal bool Autostart { get { return autostart; } set { setAutostart(value); } }
        internal int NumHistory { get { return numHistory; } set { setNumHistory(value); } }
        internal bool StoreAtClear { get { return storeAtClear; } set { setStoreAtClear(value); } }
        internal bool LifeTimeEnabled { get { return lifeTimeEnabled; } set { setLifeTimeEnabled(value); } }
        internal int LifeTimeValue { get { return lifeTimeValue; } set { setLifeTimeValue(value); } }
        internal bool FilterText { get { return filterText; } set { setFilterText(value); } }
        internal bool FilterFiles { get { return filterFiles; } set { setFilterFiles(value); } }
        internal bool FilterImages { get { return filterImages; } set { setFilterImages(value); } }
        internal bool FilterAudio { get { return filterAudio; } set { setFilterAudio(value); } }
        internal bool FilterMulti { get { return filterMulti; } set { setFilterMulti(value); } }
        internal bool FilterUnknown { get { return filterUnknown; } set { setFilterUnknown(value); } }
        internal int HK_Mod { get { return hk_mod; } set { setHK_Mod(value); } }
        internal int HK_Key { get { return hk_key; } set { setHK_Key(value); } }


        public Settings()
        {
            cleanUp();
            loadAutostart();
            loadNumHistory();
            loadStoreAtClear();
            loadLifeTimeEnabled();
            loadLifeTimeValue();
            loadFilter();
            loadHotkey();
        }

        public void loadFilter()
        {
            loadFilterText();
            loadFilterFiles();
            loadFilterImages();
            loadFilterAudio();
            loadFilterMulti();
            loadFilterUnknown();
        }

        public void loadHotkey()
        {
            loadHK_Mod();
            loadHK_Key();
        }

        private void cleanUp()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REG_KEY_SETTINGS, true);
            if (regKey != null)
            {
                foreach (string value in regKey.GetValueNames())
                {
                    if (!REG_KEY_SETTINGS_NUM_HISTORY.Equals(value)
                        && !REG_KEY_SETTINGS_STORE_AT_CLEAR.Equals(value)
                        && !REG_KEY_SETTINGS_FILTER_TEXT.Equals(value)
                        && !REG_KEY_SETTINGS_FILTER_FILES.Equals(value)
                        && !REG_KEY_SETTINGS_FILTER_IMAGES.Equals(value)
                        && !REG_KEY_SETTINGS_FILTER_AUDIO.Equals(value)
                        && !REG_KEY_SETTINGS_FILTER_MULTI.Equals(value)
                        && !REG_KEY_SETTINGS_FILTER_UNKNOWN.Equals(value)
                        && !REG_KEY_SETTINGS_HK_MOD.Equals(value)
                        && !REG_KEY_SETTINGS_HK_KEY.Equals(value))
                    {
                        regKey.DeleteValue(value, false);
                    }
                }
            }
        }

        #region Load

        private void loadAutostart()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REG_KEY_WIN_AUTOSTART, true);
            if (regKey != null)
            {
                object value = regKey.GetValue(ClipboardManager.APPLICATION_NAME);
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
                object value = regKey.GetValue(REG_KEY_SETTINGS_NUM_HISTORY);
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
                object value = regKey.GetValue(REG_KEY_SETTINGS_STORE_AT_CLEAR);
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

        private void loadLifeTimeEnabled()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_LIFE_TIME_ENABLED);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val == 0) lifeTimeEnabled = false;
                    else lifeTimeEnabled = true;
                }
                else lifeTimeEnabled = DEFAULT_LIFE_TIME_ENABLED;
            }
            else lifeTimeEnabled = DEFAULT_LIFE_TIME_ENABLED;
        }

        private void loadLifeTimeValue()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_LIFE_TIME_VALUE);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val >= 1 && val <= 60) lifeTimeValue = val;
                    else lifeTimeValue = DEFAULT_LIFE_TIME_VALUE;
                }
                else lifeTimeValue = DEFAULT_LIFE_TIME_VALUE;
            }
            else lifeTimeValue = DEFAULT_LIFE_TIME_VALUE;
        }

        private void loadFilterText()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_FILTER_TEXT);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val == 0) filterText = false;
                    else filterText = true;
                }
                else filterText = DEFAULT_FILTER_TEXT;
            }
            else filterText = DEFAULT_FILTER_TEXT;
        }

        private void loadFilterFiles()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_FILTER_FILES);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val == 0) filterFiles = false;
                    else filterFiles = true;
                }
                else filterFiles = DEFAULT_FILTER_FILES;
            }
            else filterFiles = DEFAULT_FILTER_FILES;
        }

        private void loadFilterImages()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_FILTER_IMAGES);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val == 0) filterImages = false;
                    else filterImages = true;
                }
                else filterImages = DEFAULT_FILTER_IMAGES;
            }
            else filterImages = DEFAULT_FILTER_IMAGES;
        }

        private void loadFilterAudio()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_FILTER_AUDIO);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val == 0) filterAudio = false;
                    else filterAudio = true;
                }
                else filterAudio = DEFAULT_FILTER_AUDIO;
            }
            else filterAudio = DEFAULT_FILTER_AUDIO;
        }

        private void loadFilterMulti()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_FILTER_MULTI);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val == 0) filterMulti = false;
                    else filterMulti = true;
                }
                else filterMulti = DEFAULT_FILTER_MULTI;
            }
            else filterMulti = DEFAULT_FILTER_MULTI;
        }

        private void loadFilterUnknown()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_FILTER_UNKNOWN);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    if (val == 0) filterUnknown = false;
                    else filterUnknown = true;
                }
                else filterUnknown = DEFAULT_FILTER_UNKNOWN;
            }
            else filterUnknown = DEFAULT_FILTER_UNKNOWN;
        }

        private void loadHK_Mod()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_HK_MOD);
                if (value != null && value.GetType() == typeof(Int32)) hk_mod = (int)value;
                else hk_mod = DEFAULT_HK_MOD;
            }
            else hk_mod = DEFAULT_HK_MOD;
        }

        private void loadHK_Key()
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(REG_KEY_SETTINGS_HK_KEY);
                if (value != null && value.GetType() == typeof(Int32)) hk_key = (int)value;
                else hk_key = DEFAULT_HK_KEY;
            }
            else hk_key = DEFAULT_HK_KEY;
        }

        #endregion

        #region Save

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

        private void setLifeTimeEnabled(bool enable)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_LIFE_TIME_ENABLED, enable ? 1 : 0);
            loadLifeTimeEnabled();
        }

        private void setLifeTimeValue(int num)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_LIFE_TIME_VALUE, num);
            loadLifeTimeValue();
        }

        private void setFilterText(bool enable)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_FILTER_TEXT, enable ? 1 : 0);
            loadFilterText();
        }

        private void setFilterFiles(bool enable)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_FILTER_FILES, enable ? 1 : 0);
            loadFilterFiles();
        }

        private void setFilterImages(bool enable)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_FILTER_IMAGES, enable ? 1 : 0);
            loadFilterImages();
        }

        private void setFilterAudio(bool enable)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_FILTER_AUDIO, enable ? 1 : 0);
            loadFilterAudio();
        }

        private void setFilterMulti(bool enable)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_FILTER_MULTI, enable ? 1 : 0);
            loadFilterMulti();
        }

        private void setFilterUnknown(bool enable)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_FILTER_UNKNOWN, enable ? 1 : 0);
            loadFilterUnknown();
        }

        private void setHK_Mod(int mod)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_HK_MOD, mod);
            loadHK_Mod();
        }

        private void setHK_Key(int key)
        {
            setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_HK_KEY, key);
            loadHK_Key();
        }

        #endregion

        #region Helper

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
                if (regKey.GetValue(name) != null)
                    try { regKey.DeleteValue(name); }
                    catch { }
            }
        }

        #endregion
    }
}
