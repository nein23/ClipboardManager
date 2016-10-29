using ClipboardManager.Properties;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ClipboardManager
{
    public partial class SettingsForm : Form
    {
        public static readonly string RegKeyWinAutostart = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        public static readonly string RegKeyWinAutostartValue = "\"" + Assembly.GetExecutingAssembly().Location + "\"";
        
        public SettingsForm()
        {
            InitializeComponent();
            Icon = Resources.clipboard_2_32;
            
            checkBoxAutostart.Checked = Settings.Autostart;
            numericUpDownNumHistory.Value = Settings.MaxEntries;
            hotkeyControl_open.HotkeyModifiers = GetKeys(Settings.HkMod);
            hotkeyControl_open.Hotkey = (Keys)Settings.HkKey;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Settings.Autostart = checkBoxAutostart.Checked;

            Settings.MaxEntries = Convert.ToInt32(numericUpDownNumHistory.Value);
            Settings.HkMod = GetMod( hotkeyControl_open.HotkeyModifiers);
            Settings.HkKey = (int)hotkeyControl_open.Hotkey;
            Settings.Save();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int GetMod(Keys mods)
        {
            int mod = (int) KeyModifier.None;
            if (((int)mods & (int) Keys.Control) == (int) Keys.Control) mod += (int) KeyModifier.Control;
            if (((int)mods & (int) Keys.Shift) == (int) Keys.Shift) mod += (int) KeyModifier.Shift;
            if (((int)mods & (int) Keys.Alt) == (int) Keys.Alt) mod += (int) KeyModifier.Alt;
            return mod;
        }

        private Keys GetKeys(int mods)
        {
            int mod = (int)Keys.None;
            if ((mods & (int) KeyModifier.Control) == (int) KeyModifier.Control) mod += (int) Keys.Control;
            if ((mods & (int) KeyModifier.Shift) == (int) KeyModifier.Shift) mod += (int) Keys.Shift;
            if ((mods & (int) KeyModifier.Alt) == (int) KeyModifier.Alt) mod += (int) Keys.Alt;
            return (Keys)mod;
        }

        public enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
    }
}
