using ClipboardManager.Properties;
using ClipboardManager.Util;
using System;
using System.Collections.Generic;
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

            //General
            checkBoxAutostart.Checked = settings.Autostart;
            checkBoxStoreAtClear.Checked = settings.StoreAtClear;
            numericUpDownNumHistory.Value = settings.NumHistory;
            checkBoxLifeTime.Checked = settings.LifeTimeEnabled;
            numericUpDownLifeTime.Value = settings.LifeTimeValue;

            //Type Filter
            checkBox_FilterText.Checked = settings.TypeFilter.text;
            checkBox_FilterFiles.Checked = settings.TypeFilter.files;
            checkBox_FilterImages.Checked = settings.TypeFilter.images;
            checkBox_FilterAudio.Checked = settings.TypeFilter.audio;
            checkBox_FilterMulti.Checked = settings.TypeFilter.multi;
            checkBox_FilterUnknown.Checked = settings.TypeFilter.unknown;

            //Size Filter
            checkBox_sizeFilter_global.Checked = settings.SizeFilterGlobal.enabled;
            checkBox_sizeFilter_text.Checked = settings.SizeFilterText.enabled;
            checkBox_sizeFilter_files.Checked = settings.SizeFilterFiles.enabled;
            checkBox_sizeFilter_images.Checked = settings.SizeFilterImages.enabled;
            checkBox_sizeFilter_audio.Checked = settings.SizeFilterAudio.enabled;
            checkBox_sizeFilter_multi.Checked = settings.SizeFilterMulti.enabled;
            checkBox_sizeFilter_unknown.Checked = settings.SizeFilterUnknown.enabled;

            numericUpDown_sizeFilter_global.Value = settings.SizeFilterGlobal.value;
            numericUpDown_sizeFilter_text.Value = settings.SizeFilterText.value;
            numericUpDown_sizeFilter_files.Value = settings.SizeFilterFiles.value;
            numericUpDown_sizeFilter_images.Value = settings.SizeFilterImages.value;
            numericUpDown_sizeFilter_audio.Value = settings.SizeFilterAudio.value;
            numericUpDown_sizeFilter_multi.Value = settings.SizeFilterMulti.value;
            numericUpDown_sizeFilter_unknown.Value = settings.SizeFilterUnknown.value;

            numericUpDown_sizeFilter_global.ValueChanged += NumericUpDown_sizeFilter_global_ValueChanged;

            numericUpDown_sizeFilter_text.ValueChanged += NumericUpDown_sizeFilter_ValueChanged;
            numericUpDown_sizeFilter_files.ValueChanged += NumericUpDown_sizeFilter_ValueChanged;
            numericUpDown_sizeFilter_images.ValueChanged += NumericUpDown_sizeFilter_ValueChanged;
            numericUpDown_sizeFilter_audio.ValueChanged += NumericUpDown_sizeFilter_ValueChanged;
            numericUpDown_sizeFilter_multi.ValueChanged += NumericUpDown_sizeFilter_ValueChanged;
            numericUpDown_sizeFilter_unknown.ValueChanged += NumericUpDown_sizeFilter_ValueChanged;

            //Hotkeys
            Keys mods = getModForKeys(settings.Hotkeys[0].mod);
            hotkeyControl_open.HotkeyModifiers = mods;
            hotkeyControl_open.Hotkey = (Keys)settings.Hotkeys[0].key;

            mods = getModForKeys(settings.Hotkeys[1].mod);
            hotkeyControl_pause.HotkeyModifiers = mods;
            hotkeyControl_pause.Hotkey = (Keys)settings.Hotkeys[1].key;

            mods = getModForKeys(settings.Hotkeys[2].mod);
            hotkeyControl_clear.HotkeyModifiers = mods;
            hotkeyControl_clear.Hotkey = (Keys)settings.Hotkeys[2].key;
        }

        private void NumericUpDown_sizeFilter_global_ValueChanged(object sender, EventArgs e)
        {
            if (sender != null && sender is NumericUpDown)
            {
                NumericUpDown global = sender as NumericUpDown;
                if (global.Value < numericUpDown_sizeFilter_text.Value)
                    numericUpDown_sizeFilter_text.Value = global.Value;
                if (global.Value < numericUpDown_sizeFilter_files.Value)
                    numericUpDown_sizeFilter_files.Value = global.Value;
                if (global.Value < numericUpDown_sizeFilter_images.Value)
                    numericUpDown_sizeFilter_images.Value = global.Value;
                if (global.Value < numericUpDown_sizeFilter_audio.Value)
                    numericUpDown_sizeFilter_audio.Value = global.Value;
                if (global.Value < numericUpDown_sizeFilter_multi.Value)
                    numericUpDown_sizeFilter_multi.Value = global.Value;
                if (global.Value < numericUpDown_sizeFilter_unknown.Value)
                    numericUpDown_sizeFilter_unknown.Value = global.Value;
            }
        }

        private void NumericUpDown_sizeFilter_ValueChanged(object sender, EventArgs e)
        {
            if(sender != null && sender is NumericUpDown)
            {
                NumericUpDown nud = sender as NumericUpDown;
                if (nud.Value > numericUpDown_sizeFilter_global.Value)
                    numericUpDown_sizeFilter_global.Value = nud.Value;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            settings.Autostart = checkBoxAutostart.Checked;

            bool lifeTimeSettingsChanged = settings.LifeTimeEnabled != checkBoxLifeTime.Checked
                || settings.LifeTimeValue != Convert.ToInt32(numericUpDownLifeTime.Value);

            settings.StoreAtClear = checkBoxStoreAtClear.Checked;
            settings.NumHistory = Convert.ToInt32(numericUpDownNumHistory.Value);
            settings.LifeTimeEnabled = checkBoxLifeTime.Checked;
            settings.LifeTimeValue = Convert.ToInt32(numericUpDownLifeTime.Value);

            settings.TypeFilter = new Settings.TypeFilterSettings(
                checkBox_FilterText.Checked, 
                checkBox_FilterFiles.Checked, 
                checkBox_FilterImages.Checked, 
                checkBox_FilterAudio.Checked, 
                checkBox_FilterMulti.Checked, 
                checkBox_FilterUnknown.Checked);

            settings.SizeFilterGlobal = new Settings.SizeFilterSettings(checkBox_sizeFilter_global.Checked, Convert.ToInt32(numericUpDown_sizeFilter_global.Value));
            settings.SizeFilterText = new Settings.SizeFilterSettings(checkBox_sizeFilter_text.Checked, Convert.ToInt32(numericUpDown_sizeFilter_text.Value));
            settings.SizeFilterFiles = new Settings.SizeFilterSettings(checkBox_sizeFilter_files.Checked, Convert.ToInt32(numericUpDown_sizeFilter_files.Value));
            settings.SizeFilterImages = new Settings.SizeFilterSettings(checkBox_sizeFilter_images.Checked, Convert.ToInt32(numericUpDown_sizeFilter_images.Value));
            settings.SizeFilterAudio = new Settings.SizeFilterSettings(checkBox_sizeFilter_audio.Checked, Convert.ToInt32(numericUpDown_sizeFilter_audio.Value));
            settings.SizeFilterMulti = new Settings.SizeFilterSettings(checkBox_sizeFilter_multi.Checked, Convert.ToInt32(numericUpDown_sizeFilter_multi.Value));
            settings.SizeFilterUnknown = new Settings.SizeFilterSettings(checkBox_sizeFilter_unknown.Checked, Convert.ToInt32(numericUpDown_sizeFilter_unknown.Value));

            List<HotkeyUtil.HotkeyStruct> hotkeys = new List<HotkeyUtil.HotkeyStruct>();
            int mod = getModFromKeys(hotkeyControl_open.HotkeyModifiers);
            int key = (int)hotkeyControl_open.Hotkey;
            hotkeys.Add(new HotkeyUtil.HotkeyStruct(mod, key));
            mod = getModFromKeys(hotkeyControl_pause.HotkeyModifiers);
            key = (int)hotkeyControl_pause.Hotkey;
            hotkeys.Add(new HotkeyUtil.HotkeyStruct(mod, key));
            mod = getModFromKeys(hotkeyControl_clear.HotkeyModifiers);
            key = (int)hotkeyControl_clear.Hotkey;
            hotkeys.Add(new HotkeyUtil.HotkeyStruct(mod, key));
            settings.Hotkeys = hotkeys;

            settings.load();

            HotkeyUtil.RegisterAllHotkeys(cmHandle, settings.Hotkeys);

            ccm.updateHistoryToMatchSettings();
            if (lifeTimeSettingsChanged) ccm.lifeTimeSettingsChanged();

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int getModFromKeys(Keys mods)
        {
            int mod = (int)HotkeyUtil.KeyModifier.None;
            if (((int)mods & (int)Keys.Control) == (int)Keys.Control) mod += (int)HotkeyUtil.KeyModifier.Control;
            if (((int)mods & (int)Keys.Shift) == (int)Keys.Shift) mod += (int)HotkeyUtil.KeyModifier.Shift;
            if (((int)mods & (int)Keys.Alt) == (int)Keys.Alt) mod += (int)HotkeyUtil.KeyModifier.Alt;
            return mod;
        }

        private Keys getModForKeys(int mods)
        {
            int mod = (int)Keys.None;
            if ((mods & (int)HotkeyUtil.KeyModifier.Control) == (int)HotkeyUtil.KeyModifier.Control) mod += (int)Keys.Control;
            if ((mods & (int)HotkeyUtil.KeyModifier.Shift) == (int)HotkeyUtil.KeyModifier.Shift) mod += (int)Keys.Shift;
            if ((mods & (int)HotkeyUtil.KeyModifier.Alt) == (int)HotkeyUtil.KeyModifier.Alt) mod += (int)Keys.Alt;

            return (Keys)mod;
        }
    }
}
