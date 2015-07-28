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
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            settings.Autostart = checkBoxAutostart.Checked;
            settings.StoreAtClear = checkBoxStoreAtClear.Checked;
            settings.NumHistory = Convert.ToInt32(numericUpDownNumHistory.Value);
            ccm.machtsHistorySettings();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
