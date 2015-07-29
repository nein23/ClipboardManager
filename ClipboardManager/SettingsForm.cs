using ClipboardManager.Properties;
using System;
using System.Windows.Forms;

namespace ClipboardManager
{
    public partial class SettingsForm : Form
    {
        private ClipboardContextMenu ccm;
        private Settings settings;

        public SettingsForm(ClipboardContextMenu ccm, Settings settings)
        {
            InitializeComponent();
            this.ccm = ccm;
            this.settings = settings;
            Icon = Resources.clipboard_2_32;
            checkBoxAutostart.Checked = settings.Autostart;
            checkBoxStoreAtClear.Checked = settings.StoreAtClear;
            numericUpDownNumHistory.Value = settings.NumHistory;
            checkBoxLifeTime.Checked = settings.LifeTimeEnabled;
            numericUpDownLifeTime.Value = settings.LifeTimeValue;
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
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
