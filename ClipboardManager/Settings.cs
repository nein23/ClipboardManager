using ClipboardManager.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace ClipboardManager
{
    public class Settings
    {
        #region Reg Keys

        public static readonly String REG_KEY_SETTINGS = @"Software\KaeDoWare\" + ClipboardManagerTray.APPLICATION_NAME;
        public static readonly String REG_KEY_SETTINGS_NUM_HISTORY = "NumHistory";
        public static readonly String REG_KEY_SETTINGS_STORE_AT_CLEAR = "StoreAtClear";
        public static readonly String REG_KEY_SETTINGS_LIFE_TIME_ENABLED = "LifeTimeEnabled";
        public static readonly String REG_KEY_SETTINGS_LIFE_TIME_VALUE = "LifeTimeValue";

        public static readonly String REG_KEY_SETTINGS_TYPEFILTER = "TypeFilter";

        public static readonly String REG_KEY_SETTINGS_SIZEFILTER_GLOBAL = "SizeFilterGlobal";
        public static readonly String REG_KEY_SETTINGS_SIZEFILTER_TEXT = "SizeFilterText";
        public static readonly String REG_KEY_SETTINGS_SIZEFILTER_FILES = "SizeFilterFiles";
        public static readonly String REG_KEY_SETTINGS_SIZEFILTER_IMAGES = "SizeFilterImages";
        public static readonly String REG_KEY_SETTINGS_SIZEFILTER_AUDIO = "SizeFilterAudio";
        public static readonly String REG_KEY_SETTINGS_SIZEFILTER_MULTI = "SizeFilterMulti";
        public static readonly String REG_KEY_SETTINGS_SIZEFILTER_UNKNOWN = "SizeFilterUnknown";

        public static readonly String REG_KEY_SETTINGS_HK_OPEN_MOD = "HK_Open_Mod";
        public static readonly String REG_KEY_SETTINGS_HK_OPEN_KEY = "HK_Open_Key";
        public static readonly String REG_KEY_SETTINGS_HK_PAUSE_MOD = "HK_Pause_Mod";
        public static readonly String REG_KEY_SETTINGS_HK_PAUSE_KEY = "HK_Pause_Key";
        public static readonly String REG_KEY_SETTINGS_HK_CLEAR_MOD = "HK_Clear_Mod";
        public static readonly String REG_KEY_SETTINGS_HK_CLEAR_KEY = "HK_Clear_Key";

        public static readonly String REG_KEY_WIN_AUTOSTART = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        public static readonly String REG_KEY_WIN_AUTOSTART_VALUE = "\"" + Assembly.GetExecutingAssembly().Location + "\"";

        #endregion

        #region Defaults

        public static readonly int DEFAULT_NUM_HISTORY = 10;
        public static readonly bool DEFAULT_STORE_AT_CLEAR = false;
        public static readonly bool DEFAULT_LIFE_TIME_ENABLED = false;
        public static readonly int DEFAULT_LIFE_TIME_VALUE = 5;

        public static readonly int DEFAULT_TYPEFILTER = 23;

        public static readonly int DEFAULT_SIZEFILTER = 2;

        public static readonly int DEFAULT_HK_OPEN_MOD = (int)HotkeyUtil.KeyModifier.Control | (int)HotkeyUtil.KeyModifier.Shift;
        public static readonly int DEFAULT_HK_OPEN_KEY = (int)Keys.C;
        public static readonly int DEFAULT_HK_KEY_EMPTY = 0;

        #endregion

        #region Structs

        public struct TypeFilterSettings
        {
            public bool text;
            public bool files;
            public bool images;
            public bool audio;
            public bool multi;
            public bool unknown;

            public TypeFilterSettings(int val)
            {
                text = (val & 1) == 1;
                val = (int) ((uint) val >> 1);
                files = (val & 1) == 1;
                val = (int)((uint)val >> 1);
                images = (val & 1) == 1;
                val = (int)((uint)val >> 1);
                audio = (val & 1) == 1;
                val = (int)((uint)val >> 1);
                multi = (val & 1) == 1;
                val = (int)((uint)val >> 1);
                unknown = (val & 1) == 1;
            }

            public TypeFilterSettings(bool text, bool files, bool images, bool audio, bool multi, bool unknown)
            {
                this.text = text;
                this.files = files;
                this.images = images;
                this.audio = audio;
                this.multi = multi;
                this.unknown = unknown;
            }

            public int getVal()
            {
                int val = unknown ? 1 : 0;
                val <<= 1;
                val += multi ? 1 : 0;
                val <<= 1;
                val += audio ? 1 : 0;
                val <<= 1;
                val += images ? 1 : 0;
                val <<= 1;
                val += files ? 1 : 0;
                val <<= 1;
                val += text ? 1 : 0;
                return val;
            }
        }

        public struct SizeFilterSettings
        {
            public bool enabled;
            public int value;

            public SizeFilterSettings(int val)
            {
                enabled = (val & 1) == 1;
                
                value = (int) ((uint) val >> 1);
                if (value < 1 || value > 999) value = 1;
            }

            public SizeFilterSettings(bool enabled, int value)
            {
                this.enabled = enabled;
                this.value = value;
            }

            public int getVal()
            {
                int val = enabled ? 1 : 0;
                val += value << 1;
                return val;
            }
        }

        #endregion

        #region Private Fields

        private bool autostart;
        private int numHistory;
        private bool storeAtClear;
        private bool lifeTimeEnabled;
        private int lifeTimeValue;

        private TypeFilterSettings typeFilter;

        private SizeFilterSettings sizeFilterGlobal;
        private SizeFilterSettings sizeFilterText;
        private SizeFilterSettings sizeFilterFiles;
        private SizeFilterSettings sizeFilterImages;
        private SizeFilterSettings sizeFilterAudio;
        private SizeFilterSettings sizeFilterMulti;
        private SizeFilterSettings sizeFilterUnknown;

        private List<HotkeyUtil.HotkeyStruct> hotkeys;

        #endregion

        #region Public Fields

        internal bool Autostart { get { return autostart; } set { setAutostart(value); } }
        internal int NumHistory { get { return numHistory; } set { setNumHistory(value); } }
        internal bool StoreAtClear { get { return storeAtClear; } set { setStoreAtClear(value); } }
        internal bool LifeTimeEnabled { get { return lifeTimeEnabled; } set { setLifeTimeEnabled(value); } }
        internal int LifeTimeValue { get { return lifeTimeValue; } set { setLifeTimeValue(value); } }

        internal TypeFilterSettings TypeFilter { get { return typeFilter; } set { setTypeFilter(value); } }

        internal SizeFilterSettings SizeFilterGlobal { get { return sizeFilterGlobal; } set { setSizeFilterGlobal(value); } }
        internal SizeFilterSettings SizeFilterText { get { return sizeFilterText; } set { setSizeFilterText(value); } }
        internal SizeFilterSettings SizeFilterFiles { get { return sizeFilterFiles; } set { setSizeFilterFiles(value); } }
        internal SizeFilterSettings SizeFilterImages { get { return sizeFilterImages; } set { setSizeFilterImages(value); } }
        internal SizeFilterSettings SizeFilterAudio { get { return sizeFilterAudio; } set { setSizeFilterAudio(value); } }
        internal SizeFilterSettings SizeFilterMulti { get { return sizeFilterMulti; } set { setSizeFilterMulti(value); } }
        internal SizeFilterSettings SizeFilterUnknown { get { return sizeFilterUnknown; } set { setSizeFilterUnknown(value); } }
        
        internal List<HotkeyUtil.HotkeyStruct> Hotkeys { get { return hotkeys; } set { setHotkeys(value); } }

        #endregion

        public Settings()
        {
            cleanUp();
            load();
        }

        public void load()
        {
            loadAutostart();
            loadNumHistory();
            loadStoreAtClear();
            loadLifeTimeEnabled();
            loadLifeTimeValue();
            loadTypeFilter();
            loadSizeFilter();
            loadHotkeys();
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
                        && !REG_KEY_SETTINGS_LIFE_TIME_ENABLED.Equals(value)
                        && !REG_KEY_SETTINGS_LIFE_TIME_VALUE.Equals(value)
                        && !REG_KEY_SETTINGS_TYPEFILTER.Equals(value)
                        && !REG_KEY_SETTINGS_SIZEFILTER_GLOBAL.Equals(value)
                        && !REG_KEY_SETTINGS_SIZEFILTER_TEXT.Equals(value)
                        && !REG_KEY_SETTINGS_SIZEFILTER_FILES.Equals(value)
                        && !REG_KEY_SETTINGS_SIZEFILTER_IMAGES.Equals(value)
                        && !REG_KEY_SETTINGS_SIZEFILTER_AUDIO.Equals(value)
                        && !REG_KEY_SETTINGS_SIZEFILTER_MULTI.Equals(value)
                        && !REG_KEY_SETTINGS_SIZEFILTER_UNKNOWN.Equals(value)
                        && !REG_KEY_SETTINGS_HK_OPEN_MOD.Equals(value)
                        && !REG_KEY_SETTINGS_HK_OPEN_KEY.Equals(value)
                        && !REG_KEY_SETTINGS_HK_PAUSE_MOD.Equals(value)
                        && !REG_KEY_SETTINGS_HK_PAUSE_KEY.Equals(value)
                        && !REG_KEY_SETTINGS_HK_CLEAR_MOD.Equals(value)
                        && !REG_KEY_SETTINGS_HK_CLEAR_KEY.Equals(value))
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
                object value = regKey.GetValue(ClipboardManagerTray.APPLICATION_NAME);
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

        private void loadNumHistory() { numHistory = loadSettingsInt(REG_KEY_SETTINGS_NUM_HISTORY, DEFAULT_NUM_HISTORY, 1, 20); }
        private void loadStoreAtClear() { storeAtClear = loadSettingsBool(REG_KEY_SETTINGS_STORE_AT_CLEAR, DEFAULT_STORE_AT_CLEAR); }
        private void loadLifeTimeEnabled() { lifeTimeEnabled = loadSettingsBool(REG_KEY_SETTINGS_LIFE_TIME_ENABLED, DEFAULT_LIFE_TIME_ENABLED); }
        private void loadLifeTimeValue() { lifeTimeValue = loadSettingsInt(REG_KEY_SETTINGS_LIFE_TIME_VALUE, DEFAULT_LIFE_TIME_VALUE, 1, 60); }


        private void loadTypeFilter()
        {
            typeFilter = new TypeFilterSettings(loadSettingsInt(REG_KEY_SETTINGS_TYPEFILTER, DEFAULT_TYPEFILTER));
        }



        private void loadSizeFilter()
        {
            sizeFilterGlobal = new SizeFilterSettings(loadSettingsInt(REG_KEY_SETTINGS_SIZEFILTER_GLOBAL, DEFAULT_SIZEFILTER));
            sizeFilterText = new SizeFilterSettings(loadSettingsInt(REG_KEY_SETTINGS_SIZEFILTER_TEXT, DEFAULT_SIZEFILTER));
            sizeFilterFiles = new SizeFilterSettings(loadSettingsInt(REG_KEY_SETTINGS_SIZEFILTER_FILES, DEFAULT_SIZEFILTER));
            sizeFilterImages = new SizeFilterSettings(loadSettingsInt(REG_KEY_SETTINGS_SIZEFILTER_IMAGES, DEFAULT_SIZEFILTER));
            sizeFilterAudio = new SizeFilterSettings(loadSettingsInt(REG_KEY_SETTINGS_SIZEFILTER_AUDIO, DEFAULT_SIZEFILTER));
            sizeFilterMulti = new SizeFilterSettings(loadSettingsInt(REG_KEY_SETTINGS_SIZEFILTER_MULTI, DEFAULT_SIZEFILTER));
            sizeFilterUnknown = new SizeFilterSettings(loadSettingsInt(REG_KEY_SETTINGS_SIZEFILTER_UNKNOWN, DEFAULT_SIZEFILTER));
        }

        private void loadHotkeys()
        {
            hotkeys = new List<HotkeyUtil.HotkeyStruct>();
            int mod = loadSettingsInt(REG_KEY_SETTINGS_HK_OPEN_MOD, DEFAULT_HK_OPEN_MOD);
            int key = loadSettingsInt(REG_KEY_SETTINGS_HK_OPEN_KEY, DEFAULT_HK_OPEN_KEY);
            hotkeys.Add(new HotkeyUtil.HotkeyStruct(mod, key));
            mod = loadSettingsInt(REG_KEY_SETTINGS_HK_PAUSE_MOD, DEFAULT_HK_KEY_EMPTY);
            key = loadSettingsInt(REG_KEY_SETTINGS_HK_PAUSE_KEY, DEFAULT_HK_KEY_EMPTY);
            hotkeys.Add(new HotkeyUtil.HotkeyStruct(mod, key));
            mod = loadSettingsInt(REG_KEY_SETTINGS_HK_CLEAR_MOD, DEFAULT_HK_KEY_EMPTY);
            key = loadSettingsInt(REG_KEY_SETTINGS_HK_CLEAR_KEY, DEFAULT_HK_KEY_EMPTY);
            hotkeys.Add(new HotkeyUtil.HotkeyStruct(mod, key));
        }

        #endregion

        #region Save

        private void setAutostart(bool enable)
        {
            if (enable)
            {
                setCurUserRegValue(REG_KEY_WIN_AUTOSTART, ClipboardManagerTray.APPLICATION_NAME, REG_KEY_WIN_AUTOSTART_VALUE);
            }
            else
            {
                deleteCurUserRegValue(REG_KEY_WIN_AUTOSTART, ClipboardManagerTray.APPLICATION_NAME);
            }
            loadAutostart();
        }

        private void setNumHistory(int num) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_NUM_HISTORY, num); }
        private void setStoreAtClear(bool enable) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_STORE_AT_CLEAR, enable ? 1 : 0); }
        private void setLifeTimeEnabled(bool enable) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_LIFE_TIME_ENABLED, enable ? 1 : 0); }
        private void setLifeTimeValue(int num) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_LIFE_TIME_VALUE, num); }

        private void setTypeFilter(TypeFilterSettings filterSettings) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_TYPEFILTER, filterSettings.getVal()); }

        private void setSizeFilterGlobal(SizeFilterSettings filterSettings) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_SIZEFILTER_GLOBAL, filterSettings.getVal()); }
        private void setSizeFilterText(SizeFilterSettings filterSettings) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_SIZEFILTER_TEXT, filterSettings.getVal()); }
        private void setSizeFilterFiles(SizeFilterSettings filterSettings) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_SIZEFILTER_FILES, filterSettings.getVal()); }
        private void setSizeFilterImages(SizeFilterSettings filterSettings) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_SIZEFILTER_IMAGES, filterSettings.getVal()); }
        private void setSizeFilterAudio(SizeFilterSettings filterSettings) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_SIZEFILTER_AUDIO, filterSettings.getVal()); }
        private void setSizeFilterMulti(SizeFilterSettings filterSettings) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_SIZEFILTER_MULTI, filterSettings.getVal()); }
        private void setSizeFilterUnknown(SizeFilterSettings filterSettings) { setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_SIZEFILTER_UNKNOWN, filterSettings.getVal()); }

        private void setHotkeys(List<HotkeyUtil.HotkeyStruct> hks)
        {
            if (hks.Count == 3)
            {
                setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_HK_OPEN_MOD, hks[0].mod);
                setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_HK_OPEN_KEY, hks[0].key);
                setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_HK_PAUSE_MOD, hks[1].mod);
                setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_HK_PAUSE_KEY, hks[1].key);
                setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_HK_CLEAR_MOD, hks[2].mod);
                setCurUserRegValue(REG_KEY_SETTINGS, REG_KEY_SETTINGS_HK_CLEAR_KEY, hks[2].key);
            }
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

        private bool loadSettingsBool(string name, bool def)
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null && name != null)
            {
                object value = regKey.GetValue(name);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    int val = (int)value;
                    return (val != 0);
                }
            }
            return def;
        }

        private int loadSettingsInt(string name, int def)
        {
            RegistryKey regKey = getSettingsRegKey(true);
            if (regKey != null)
            {
                object value = regKey.GetValue(name);
                if (value != null && value.GetType() == typeof(Int32))
                {
                    return (int)value;
                }
            }
            return def;
        }
        
        private int loadSettingsInt(string name, int def, int min, int max)
        {
            int val = loadSettingsInt(name, def);
            if (val >= min && val <= max) return val;
            return def;
        }

        #endregion
    }
}
