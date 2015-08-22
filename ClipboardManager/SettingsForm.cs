using ClipboardManager.Properties;
using System;
using System.Windows.Forms;

namespace ClipboardManager
{
    public partial class SettingsForm : Form
    {
        IntPtr cmHandle;
        private ClipboardContextMenu ccm;
        private Settings settings;

        public SettingsForm(IntPtr cmHandle, ClipboardContextMenu ccm, Settings settings)
        {
            InitializeComponent();
            this.cmHandle = cmHandle;
            this.ccm = ccm;
            this.settings = settings;
            Icon = Resources.clipboard_2_32;
            checkBoxAutostart.Checked = settings.Autostart;
            checkBoxStoreAtClear.Checked = settings.StoreAtClear;
            numericUpDownNumHistory.Value = settings.NumHistory;
            checkBoxLifeTime.Checked = settings.LifeTimeEnabled;
            numericUpDownLifeTime.Value = settings.LifeTimeValue;
            checkBox_FilterText.Checked = settings.FilterText;
            checkBox_FilterFiles.Checked = settings.FilterFiles;
            checkBox_FilterImages.Checked = settings.FilterImages;
            checkBox_FilterAudio.Checked = settings.FilterAudio;
            checkBox_FilterMulti.Checked = settings.FilterMulti;
            checkBox_FilterUnknown.Checked = settings.FilterUnknown;

            Keys mods = getModForKeys(settings.HK_Mod);
            hotkeyControl.HotkeyModifiers = mods;
            hotkeyControl.Hotkey = (Keys)settings.HK_Key;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            bool lifeTimeSettingsChanged = settings.LifeTimeEnabled != checkBoxLifeTime.Checked 
                || settings.LifeTimeValue != Convert.ToInt32(numericUpDownLifeTime.Value);
            settings.Autostart = checkBoxAutostart.Checked;
            settings.StoreAtClear = checkBoxStoreAtClear.Checked;
            settings.NumHistory = Convert.ToInt32(numericUpDownNumHistory.Value);
            settings.LifeTimeEnabled = checkBoxLifeTime.Checked;
            settings.LifeTimeValue = Convert.ToInt32(numericUpDownLifeTime.Value);
            ccm.clearHistory(settings.NumHistory);
            if (lifeTimeSettingsChanged) ccm.lifeTimeSettingsChanged();
            settings.FilterText = checkBox_FilterText.Checked;
            settings.FilterFiles = checkBox_FilterFiles.Checked;
            settings.FilterImages = checkBox_FilterImages.Checked;
            settings.FilterAudio = checkBox_FilterAudio.Checked;
            settings.FilterMulti = checkBox_FilterMulti.Checked;
            settings.FilterUnknown = checkBox_FilterUnknown.Checked;

            int mod = getModFromKeys(hotkeyControl.HotkeyModifiers);
            settings.HK_Mod = mod;
            settings.HK_Key = (int)hotkeyControl.Hotkey;
            Util.UnregisterHotKey(cmHandle, 0);
            if (settings.HK_Mod != 0 || settings.HK_Key != 0)
            {
                Util.RegisterHotKey(cmHandle, 0, settings.HK_Mod, settings.HK_Key);
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int getModFromKeys(Keys mods)
        {
            int mod = (int)Util.KeyModifier.None;
            if (((int)mods & (int)Keys.Control) == (int)Keys.Control) mod += (int)Util.KeyModifier.Control;
            if (((int)mods & (int)Keys.Shift) == (int)Keys.Shift) mod += (int)Util.KeyModifier.Shift;
            if (((int)mods & (int)Keys.Alt) == (int)Keys.Alt) mod += (int)Util.KeyModifier.Alt;
            return mod;
        }

        private Keys getModForKeys(int mods)
        {
            int mod = (int)Keys.None;
            if ((mods & (int)Util.KeyModifier.Control) == (int)Util.KeyModifier.Control) mod += (int)Keys.Control;
            if ((mods & (int)Util.KeyModifier.Shift) == (int)Util.KeyModifier.Shift) mod += (int)Keys.Shift;
            if ((mods & (int)Util.KeyModifier.Alt) == (int)Util.KeyModifier.Alt) mod += (int)Keys.Alt;

            return (Keys)mod;
        }
    }
}
