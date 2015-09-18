namespace ClipboardManager
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxAutostart = new System.Windows.Forms.CheckBox();
            this.numericUpDownNumHistory = new System.Windows.Forms.NumericUpDown();
            this.checkBoxStoreAtClear = new System.Windows.Forms.CheckBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelNumHistory = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_general = new System.Windows.Forms.TabPage();
            this.groupBox_hotkey = new System.Windows.Forms.GroupBox();
            this.label_hotkey_clear = new System.Windows.Forms.Label();
            this.hotkeyControl_clear = new ClipboardManager.HotkeyControl();
            this.label_hotkey_pause = new System.Windows.Forms.Label();
            this.hotkeyControl_pause = new ClipboardManager.HotkeyControl();
            this.label_hotkey_openHistory = new System.Windows.Forms.Label();
            this.hotkeyControl_open = new ClipboardManager.HotkeyControl();
            this.numericUpDownLifeTime = new System.Windows.Forms.NumericUpDown();
            this.checkBoxLifeTime = new System.Windows.Forms.CheckBox();
            this.tabPage_filter = new System.Windows.Forms.TabPage();
            this.groupBox_sizeFilter = new System.Windows.Forms.GroupBox();
            this.label_sizeFilter_desc = new System.Windows.Forms.Label();
            this.numericUpDown_sizeFilter_unknown = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_sizeFilter_multi = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_sizeFilter_audio = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_sizeFilter_images = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_sizeFilter_files = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_sizeFilter_text = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_sizeFilter_global = new System.Windows.Forms.NumericUpDown();
            this.checkBox_sizeFilter_unknown = new System.Windows.Forms.CheckBox();
            this.checkBox_sizeFilter_multi = new System.Windows.Forms.CheckBox();
            this.checkBox_sizeFilter_audio = new System.Windows.Forms.CheckBox();
            this.checkBox_sizeFilter_images = new System.Windows.Forms.CheckBox();
            this.checkBox_sizeFilter_files = new System.Windows.Forms.CheckBox();
            this.checkBox_sizeFilter_text = new System.Windows.Forms.CheckBox();
            this.checkBox_sizeFilter_global = new System.Windows.Forms.CheckBox();
            this.groupBox_typeFilter = new System.Windows.Forms.GroupBox();
            this.label_typeFilter_desc = new System.Windows.Forms.Label();
            this.checkBox_FilterAudio = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterUnknown = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterMulti = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterImages = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterFiles = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterText = new System.Windows.Forms.CheckBox();
            this.label_sizeFilter_unit_global = new System.Windows.Forms.Label();
            this.label_sizeFilter_unit_text = new System.Windows.Forms.Label();
            this.label_sizeFilter_unit_files = new System.Windows.Forms.Label();
            this.label_sizeFilter_unit_audio = new System.Windows.Forms.Label();
            this.label_sizeFilter_unit_images = new System.Windows.Forms.Label();
            this.label_sizeFilter_unit_multi = new System.Windows.Forms.Label();
            this.label_sizeFilter_unit_unknown = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumHistory)).BeginInit();
            this.panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_general.SuspendLayout();
            this.groupBox_hotkey.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLifeTime)).BeginInit();
            this.tabPage_filter.SuspendLayout();
            this.groupBox_sizeFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_unknown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_multi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_audio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_images)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_files)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_text)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_global)).BeginInit();
            this.groupBox_typeFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxAutostart
            // 
            this.checkBoxAutostart.AutoSize = true;
            this.checkBoxAutostart.Location = new System.Drawing.Point(6, 6);
            this.checkBoxAutostart.Name = "checkBoxAutostart";
            this.checkBoxAutostart.Size = new System.Drawing.Size(117, 17);
            this.checkBoxAutostart.TabIndex = 0;
            this.checkBoxAutostart.Text = "Start with Windows";
            this.checkBoxAutostart.UseVisualStyleBackColor = true;
            // 
            // numericUpDownNumHistory
            // 
            this.numericUpDownNumHistory.Location = new System.Drawing.Point(219, 28);
            this.numericUpDownNumHistory.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownNumHistory.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumHistory.Name = "numericUpDownNumHistory";
            this.numericUpDownNumHistory.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownNumHistory.TabIndex = 3;
            this.numericUpDownNumHistory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownNumHistory.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // checkBoxStoreAtClear
            // 
            this.checkBoxStoreAtClear.AutoSize = true;
            this.checkBoxStoreAtClear.Location = new System.Drawing.Point(6, 77);
            this.checkBoxStoreAtClear.Name = "checkBoxStoreAtClear";
            this.checkBoxStoreAtClear.Size = new System.Drawing.Size(184, 17);
            this.checkBoxStoreAtClear.TabIndex = 1;
            this.checkBoxStoreAtClear.Text = "Historize cleared clipboard entries";
            this.checkBoxStoreAtClear.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Image = global::ClipboardManager.Properties.Resources.save;
            this.buttonSave.Location = new System.Drawing.Point(150, 416);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(58, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Save";
            this.buttonSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::ClipboardManager.Properties.Resources.cancel;
            this.buttonCancel.Location = new System.Drawing.Point(214, 416);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(66, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelNumHistory
            // 
            this.labelNumHistory.AutoSize = true;
            this.labelNumHistory.Location = new System.Drawing.Point(6, 30);
            this.labelNumHistory.Name = "labelNumHistory";
            this.labelNumHistory.Size = new System.Drawing.Size(168, 13);
            this.labelNumHistory.TabIndex = 2;
            this.labelNumHistory.Text = "Maximum number of history entries";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.tabControl1);
            this.panel.Controls.Add(this.buttonCancel);
            this.panel.Controls.Add(this.buttonSave);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(285, 444);
            this.panel.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_general);
            this.tabControl1.Controls.Add(this.tabPage_filter);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(275, 406);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage_general
            // 
            this.tabPage_general.Controls.Add(this.groupBox_hotkey);
            this.tabPage_general.Controls.Add(this.checkBoxAutostart);
            this.tabPage_general.Controls.Add(this.checkBoxStoreAtClear);
            this.tabPage_general.Controls.Add(this.numericUpDownNumHistory);
            this.tabPage_general.Controls.Add(this.numericUpDownLifeTime);
            this.tabPage_general.Controls.Add(this.labelNumHistory);
            this.tabPage_general.Controls.Add(this.checkBoxLifeTime);
            this.tabPage_general.Location = new System.Drawing.Point(4, 22);
            this.tabPage_general.Name = "tabPage_general";
            this.tabPage_general.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_general.Size = new System.Drawing.Size(267, 380);
            this.tabPage_general.TabIndex = 0;
            this.tabPage_general.Text = "General";
            this.tabPage_general.UseVisualStyleBackColor = true;
            // 
            // groupBox_hotkey
            // 
            this.groupBox_hotkey.Controls.Add(this.label_hotkey_clear);
            this.groupBox_hotkey.Controls.Add(this.hotkeyControl_clear);
            this.groupBox_hotkey.Controls.Add(this.label_hotkey_pause);
            this.groupBox_hotkey.Controls.Add(this.hotkeyControl_pause);
            this.groupBox_hotkey.Controls.Add(this.label_hotkey_openHistory);
            this.groupBox_hotkey.Controls.Add(this.hotkeyControl_open);
            this.groupBox_hotkey.Location = new System.Drawing.Point(6, 100);
            this.groupBox_hotkey.Name = "groupBox_hotkey";
            this.groupBox_hotkey.Size = new System.Drawing.Size(253, 97);
            this.groupBox_hotkey.TabIndex = 6;
            this.groupBox_hotkey.TabStop = false;
            this.groupBox_hotkey.Text = "Hotkeys";
            // 
            // label_hotkey_clear
            // 
            this.label_hotkey_clear.AutoSize = true;
            this.label_hotkey_clear.Location = new System.Drawing.Point(6, 74);
            this.label_hotkey_clear.Name = "label_hotkey_clear";
            this.label_hotkey_clear.Size = new System.Drawing.Size(44, 13);
            this.label_hotkey_clear.TabIndex = 17;
            this.label_hotkey_clear.Text = "Clear all";
            // 
            // hotkeyControl_clear
            // 
            this.hotkeyControl_clear.Hotkey = System.Windows.Forms.Keys.None;
            this.hotkeyControl_clear.HotkeyModifiers = System.Windows.Forms.Keys.None;
            this.hotkeyControl_clear.Location = new System.Drawing.Point(97, 71);
            this.hotkeyControl_clear.Name = "hotkeyControl_clear";
            this.hotkeyControl_clear.Size = new System.Drawing.Size(149, 20);
            this.hotkeyControl_clear.TabIndex = 18;
            this.hotkeyControl_clear.Text = "None";
            // 
            // label_hotkey_pause
            // 
            this.label_hotkey_pause.AutoSize = true;
            this.label_hotkey_pause.Location = new System.Drawing.Point(6, 48);
            this.label_hotkey_pause.Name = "label_hotkey_pause";
            this.label_hotkey_pause.Size = new System.Drawing.Size(84, 13);
            this.label_hotkey_pause.TabIndex = 15;
            this.label_hotkey_pause.Text = "Pause/Continue";
            // 
            // hotkeyControl_pause
            // 
            this.hotkeyControl_pause.Hotkey = System.Windows.Forms.Keys.None;
            this.hotkeyControl_pause.HotkeyModifiers = System.Windows.Forms.Keys.None;
            this.hotkeyControl_pause.Location = new System.Drawing.Point(97, 45);
            this.hotkeyControl_pause.Name = "hotkeyControl_pause";
            this.hotkeyControl_pause.Size = new System.Drawing.Size(149, 20);
            this.hotkeyControl_pause.TabIndex = 16;
            this.hotkeyControl_pause.Text = "None";
            // 
            // label_hotkey_openHistory
            // 
            this.label_hotkey_openHistory.AutoSize = true;
            this.label_hotkey_openHistory.Location = new System.Drawing.Point(6, 22);
            this.label_hotkey_openHistory.Name = "label_hotkey_openHistory";
            this.label_hotkey_openHistory.Size = new System.Drawing.Size(68, 13);
            this.label_hotkey_openHistory.TabIndex = 13;
            this.label_hotkey_openHistory.Text = "Open History";
            // 
            // hotkeyControl_open
            // 
            this.hotkeyControl_open.Hotkey = System.Windows.Forms.Keys.None;
            this.hotkeyControl_open.HotkeyModifiers = System.Windows.Forms.Keys.None;
            this.hotkeyControl_open.Location = new System.Drawing.Point(97, 19);
            this.hotkeyControl_open.Name = "hotkeyControl_open";
            this.hotkeyControl_open.Size = new System.Drawing.Size(149, 20);
            this.hotkeyControl_open.TabIndex = 14;
            this.hotkeyControl_open.Text = "None";
            // 
            // numericUpDownLifeTime
            // 
            this.numericUpDownLifeTime.Location = new System.Drawing.Point(219, 51);
            this.numericUpDownLifeTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownLifeTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLifeTime.Name = "numericUpDownLifeTime";
            this.numericUpDownLifeTime.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownLifeTime.TabIndex = 5;
            this.numericUpDownLifeTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownLifeTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxLifeTime
            // 
            this.checkBoxLifeTime.AutoSize = true;
            this.checkBoxLifeTime.Location = new System.Drawing.Point(6, 52);
            this.checkBoxLifeTime.Name = "checkBoxLifeTime";
            this.checkBoxLifeTime.Size = new System.Drawing.Size(169, 17);
            this.checkBoxLifeTime.TabIndex = 4;
            this.checkBoxLifeTime.Text = "History entry lifetime in minutes";
            this.checkBoxLifeTime.UseVisualStyleBackColor = true;
            // 
            // tabPage_filter
            // 
            this.tabPage_filter.Controls.Add(this.groupBox_sizeFilter);
            this.tabPage_filter.Controls.Add(this.groupBox_typeFilter);
            this.tabPage_filter.Location = new System.Drawing.Point(4, 22);
            this.tabPage_filter.Name = "tabPage_filter";
            this.tabPage_filter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_filter.Size = new System.Drawing.Size(267, 380);
            this.tabPage_filter.TabIndex = 1;
            this.tabPage_filter.Text = "Filter";
            this.tabPage_filter.UseVisualStyleBackColor = true;
            // 
            // groupBox_sizeFilter
            // 
            this.groupBox_sizeFilter.Controls.Add(this.label_sizeFilter_unit_unknown);
            this.groupBox_sizeFilter.Controls.Add(this.label_sizeFilter_unit_multi);
            this.groupBox_sizeFilter.Controls.Add(this.label_sizeFilter_unit_images);
            this.groupBox_sizeFilter.Controls.Add(this.label_sizeFilter_unit_audio);
            this.groupBox_sizeFilter.Controls.Add(this.label_sizeFilter_unit_files);
            this.groupBox_sizeFilter.Controls.Add(this.label_sizeFilter_unit_text);
            this.groupBox_sizeFilter.Controls.Add(this.label_sizeFilter_unit_global);
            this.groupBox_sizeFilter.Controls.Add(this.label_sizeFilter_desc);
            this.groupBox_sizeFilter.Controls.Add(this.numericUpDown_sizeFilter_unknown);
            this.groupBox_sizeFilter.Controls.Add(this.numericUpDown_sizeFilter_multi);
            this.groupBox_sizeFilter.Controls.Add(this.numericUpDown_sizeFilter_audio);
            this.groupBox_sizeFilter.Controls.Add(this.numericUpDown_sizeFilter_images);
            this.groupBox_sizeFilter.Controls.Add(this.numericUpDown_sizeFilter_files);
            this.groupBox_sizeFilter.Controls.Add(this.numericUpDown_sizeFilter_text);
            this.groupBox_sizeFilter.Controls.Add(this.numericUpDown_sizeFilter_global);
            this.groupBox_sizeFilter.Controls.Add(this.checkBox_sizeFilter_unknown);
            this.groupBox_sizeFilter.Controls.Add(this.checkBox_sizeFilter_multi);
            this.groupBox_sizeFilter.Controls.Add(this.checkBox_sizeFilter_audio);
            this.groupBox_sizeFilter.Controls.Add(this.checkBox_sizeFilter_images);
            this.groupBox_sizeFilter.Controls.Add(this.checkBox_sizeFilter_files);
            this.groupBox_sizeFilter.Controls.Add(this.checkBox_sizeFilter_text);
            this.groupBox_sizeFilter.Controls.Add(this.checkBox_sizeFilter_global);
            this.groupBox_sizeFilter.Location = new System.Drawing.Point(6, 133);
            this.groupBox_sizeFilter.Name = "groupBox_sizeFilter";
            this.groupBox_sizeFilter.Size = new System.Drawing.Size(253, 241);
            this.groupBox_sizeFilter.TabIndex = 7;
            this.groupBox_sizeFilter.TabStop = false;
            this.groupBox_sizeFilter.Text = "Size Filter";
            // 
            // label_sizeFilter_desc
            // 
            this.label_sizeFilter_desc.AutoSize = true;
            this.label_sizeFilter_desc.Location = new System.Drawing.Point(3, 20);
            this.label_sizeFilter_desc.Name = "label_sizeFilter_desc";
            this.label_sizeFilter_desc.Size = new System.Drawing.Size(222, 52);
            this.label_sizeFilter_desc.TabIndex = 21;
            this.label_sizeFilter_desc.Text = "Specify the maximum size for the whole list or\r\neach data type. Keep in mind that" +
    " clipboard\r\nentries are larger than the original copied data\r\nbecause different " +
    "format types are stored.";
            // 
            // numericUpDown_sizeFilter_unknown
            // 
            this.numericUpDown_sizeFilter_unknown.Location = new System.Drawing.Point(136, 215);
            this.numericUpDown_sizeFilter_unknown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_unknown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_unknown.Name = "numericUpDown_sizeFilter_unknown";
            this.numericUpDown_sizeFilter_unknown.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_sizeFilter_unknown.TabIndex = 13;
            this.numericUpDown_sizeFilter_unknown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_sizeFilter_multi
            // 
            this.numericUpDown_sizeFilter_multi.Location = new System.Drawing.Point(136, 192);
            this.numericUpDown_sizeFilter_multi.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_multi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_multi.Name = "numericUpDown_sizeFilter_multi";
            this.numericUpDown_sizeFilter_multi.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_sizeFilter_multi.TabIndex = 12;
            this.numericUpDown_sizeFilter_multi.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_sizeFilter_audio
            // 
            this.numericUpDown_sizeFilter_audio.Location = new System.Drawing.Point(136, 169);
            this.numericUpDown_sizeFilter_audio.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_audio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_audio.Name = "numericUpDown_sizeFilter_audio";
            this.numericUpDown_sizeFilter_audio.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_sizeFilter_audio.TabIndex = 11;
            this.numericUpDown_sizeFilter_audio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_sizeFilter_images
            // 
            this.numericUpDown_sizeFilter_images.Location = new System.Drawing.Point(136, 146);
            this.numericUpDown_sizeFilter_images.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_images.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_images.Name = "numericUpDown_sizeFilter_images";
            this.numericUpDown_sizeFilter_images.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_sizeFilter_images.TabIndex = 10;
            this.numericUpDown_sizeFilter_images.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_sizeFilter_files
            // 
            this.numericUpDown_sizeFilter_files.Location = new System.Drawing.Point(136, 123);
            this.numericUpDown_sizeFilter_files.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_files.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_files.Name = "numericUpDown_sizeFilter_files";
            this.numericUpDown_sizeFilter_files.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_sizeFilter_files.TabIndex = 9;
            this.numericUpDown_sizeFilter_files.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_sizeFilter_text
            // 
            this.numericUpDown_sizeFilter_text.Location = new System.Drawing.Point(136, 100);
            this.numericUpDown_sizeFilter_text.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_text.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_text.Name = "numericUpDown_sizeFilter_text";
            this.numericUpDown_sizeFilter_text.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_sizeFilter_text.TabIndex = 8;
            this.numericUpDown_sizeFilter_text.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown_sizeFilter_global
            // 
            this.numericUpDown_sizeFilter_global.Location = new System.Drawing.Point(136, 77);
            this.numericUpDown_sizeFilter_global.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_global.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_sizeFilter_global.Name = "numericUpDown_sizeFilter_global";
            this.numericUpDown_sizeFilter_global.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_sizeFilter_global.TabIndex = 7;
            this.numericUpDown_sizeFilter_global.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBox_sizeFilter_unknown
            // 
            this.checkBox_sizeFilter_unknown.AutoSize = true;
            this.checkBox_sizeFilter_unknown.Location = new System.Drawing.Point(6, 216);
            this.checkBox_sizeFilter_unknown.Name = "checkBox_sizeFilter_unknown";
            this.checkBox_sizeFilter_unknown.Size = new System.Drawing.Size(98, 17);
            this.checkBox_sizeFilter_unknown.TabIndex = 6;
            this.checkBox_sizeFilter_unknown.Text = "Unknown Data";
            this.checkBox_sizeFilter_unknown.UseVisualStyleBackColor = true;
            // 
            // checkBox_sizeFilter_multi
            // 
            this.checkBox_sizeFilter_multi.AutoSize = true;
            this.checkBox_sizeFilter_multi.Location = new System.Drawing.Point(6, 193);
            this.checkBox_sizeFilter_multi.Name = "checkBox_sizeFilter_multi";
            this.checkBox_sizeFilter_multi.Size = new System.Drawing.Size(88, 17);
            this.checkBox_sizeFilter_multi.TabIndex = 5;
            this.checkBox_sizeFilter_multi.Text = "Multiple Data";
            this.checkBox_sizeFilter_multi.UseVisualStyleBackColor = true;
            // 
            // checkBox_sizeFilter_audio
            // 
            this.checkBox_sizeFilter_audio.AutoSize = true;
            this.checkBox_sizeFilter_audio.Location = new System.Drawing.Point(6, 170);
            this.checkBox_sizeFilter_audio.Name = "checkBox_sizeFilter_audio";
            this.checkBox_sizeFilter_audio.Size = new System.Drawing.Size(79, 17);
            this.checkBox_sizeFilter_audio.TabIndex = 4;
            this.checkBox_sizeFilter_audio.Text = "Audio Data";
            this.checkBox_sizeFilter_audio.UseVisualStyleBackColor = true;
            // 
            // checkBox_sizeFilter_images
            // 
            this.checkBox_sizeFilter_images.AutoSize = true;
            this.checkBox_sizeFilter_images.Location = new System.Drawing.Point(6, 147);
            this.checkBox_sizeFilter_images.Name = "checkBox_sizeFilter_images";
            this.checkBox_sizeFilter_images.Size = new System.Drawing.Size(60, 17);
            this.checkBox_sizeFilter_images.TabIndex = 3;
            this.checkBox_sizeFilter_images.Text = "Images";
            this.checkBox_sizeFilter_images.UseVisualStyleBackColor = true;
            // 
            // checkBox_sizeFilter_files
            // 
            this.checkBox_sizeFilter_files.AutoSize = true;
            this.checkBox_sizeFilter_files.Location = new System.Drawing.Point(6, 124);
            this.checkBox_sizeFilter_files.Name = "checkBox_sizeFilter_files";
            this.checkBox_sizeFilter_files.Size = new System.Drawing.Size(86, 17);
            this.checkBox_sizeFilter_files.TabIndex = 2;
            this.checkBox_sizeFilter_files.Text = "Files/Folders";
            this.checkBox_sizeFilter_files.UseVisualStyleBackColor = true;
            // 
            // checkBox_sizeFilter_text
            // 
            this.checkBox_sizeFilter_text.AutoSize = true;
            this.checkBox_sizeFilter_text.Location = new System.Drawing.Point(6, 101);
            this.checkBox_sizeFilter_text.Name = "checkBox_sizeFilter_text";
            this.checkBox_sizeFilter_text.Size = new System.Drawing.Size(47, 17);
            this.checkBox_sizeFilter_text.TabIndex = 1;
            this.checkBox_sizeFilter_text.Text = "Text";
            this.checkBox_sizeFilter_text.UseVisualStyleBackColor = true;
            // 
            // checkBox_sizeFilter_global
            // 
            this.checkBox_sizeFilter_global.AutoSize = true;
            this.checkBox_sizeFilter_global.Location = new System.Drawing.Point(6, 78);
            this.checkBox_sizeFilter_global.Name = "checkBox_sizeFilter_global";
            this.checkBox_sizeFilter_global.Size = new System.Drawing.Size(124, 17);
            this.checkBox_sizeFilter_global.TabIndex = 0;
            this.checkBox_sizeFilter_global.Text = "Maximum history size";
            this.checkBox_sizeFilter_global.UseVisualStyleBackColor = true;
            // 
            // groupBox_typeFilter
            // 
            this.groupBox_typeFilter.Controls.Add(this.label_typeFilter_desc);
            this.groupBox_typeFilter.Controls.Add(this.checkBox_FilterAudio);
            this.groupBox_typeFilter.Controls.Add(this.checkBox_FilterUnknown);
            this.groupBox_typeFilter.Controls.Add(this.checkBox_FilterMulti);
            this.groupBox_typeFilter.Controls.Add(this.checkBox_FilterImages);
            this.groupBox_typeFilter.Controls.Add(this.checkBox_FilterFiles);
            this.groupBox_typeFilter.Controls.Add(this.checkBox_FilterText);
            this.groupBox_typeFilter.Location = new System.Drawing.Point(6, 6);
            this.groupBox_typeFilter.Name = "groupBox_typeFilter";
            this.groupBox_typeFilter.Size = new System.Drawing.Size(253, 121);
            this.groupBox_typeFilter.TabIndex = 6;
            this.groupBox_typeFilter.TabStop = false;
            this.groupBox_typeFilter.Text = "Type Filter";
            // 
            // label_typeFilter_desc
            // 
            this.label_typeFilter_desc.AutoSize = true;
            this.label_typeFilter_desc.Location = new System.Drawing.Point(3, 20);
            this.label_typeFilter_desc.Name = "label_typeFilter_desc";
            this.label_typeFilter_desc.Size = new System.Drawing.Size(184, 26);
            this.label_typeFilter_desc.TabIndex = 7;
            this.label_typeFilter_desc.Text = "Historize only checked content types.\r\nUnchecked types will be dismissed.";
            // 
            // checkBox_FilterAudio
            // 
            this.checkBox_FilterAudio.AutoSize = true;
            this.checkBox_FilterAudio.Location = new System.Drawing.Point(91, 52);
            this.checkBox_FilterAudio.Name = "checkBox_FilterAudio";
            this.checkBox_FilterAudio.Size = new System.Drawing.Size(79, 17);
            this.checkBox_FilterAudio.TabIndex = 3;
            this.checkBox_FilterAudio.Text = "Audio Data";
            this.checkBox_FilterAudio.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterUnknown
            // 
            this.checkBox_FilterUnknown.AutoSize = true;
            this.checkBox_FilterUnknown.Location = new System.Drawing.Point(91, 98);
            this.checkBox_FilterUnknown.Name = "checkBox_FilterUnknown";
            this.checkBox_FilterUnknown.Size = new System.Drawing.Size(98, 17);
            this.checkBox_FilterUnknown.TabIndex = 5;
            this.checkBox_FilterUnknown.Text = "Unknown Data";
            this.checkBox_FilterUnknown.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterMulti
            // 
            this.checkBox_FilterMulti.AutoSize = true;
            this.checkBox_FilterMulti.Location = new System.Drawing.Point(91, 75);
            this.checkBox_FilterMulti.Name = "checkBox_FilterMulti";
            this.checkBox_FilterMulti.Size = new System.Drawing.Size(88, 17);
            this.checkBox_FilterMulti.TabIndex = 4;
            this.checkBox_FilterMulti.Text = "Multiple Data";
            this.checkBox_FilterMulti.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterImages
            // 
            this.checkBox_FilterImages.AutoSize = true;
            this.checkBox_FilterImages.Location = new System.Drawing.Point(6, 98);
            this.checkBox_FilterImages.Name = "checkBox_FilterImages";
            this.checkBox_FilterImages.Size = new System.Drawing.Size(60, 17);
            this.checkBox_FilterImages.TabIndex = 2;
            this.checkBox_FilterImages.Text = "Images";
            this.checkBox_FilterImages.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterFiles
            // 
            this.checkBox_FilterFiles.AutoSize = true;
            this.checkBox_FilterFiles.Location = new System.Drawing.Point(6, 75);
            this.checkBox_FilterFiles.Name = "checkBox_FilterFiles";
            this.checkBox_FilterFiles.Size = new System.Drawing.Size(86, 17);
            this.checkBox_FilterFiles.TabIndex = 1;
            this.checkBox_FilterFiles.Text = "Files/Folders";
            this.checkBox_FilterFiles.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterText
            // 
            this.checkBox_FilterText.AutoSize = true;
            this.checkBox_FilterText.Location = new System.Drawing.Point(6, 52);
            this.checkBox_FilterText.Name = "checkBox_FilterText";
            this.checkBox_FilterText.Size = new System.Drawing.Size(47, 17);
            this.checkBox_FilterText.TabIndex = 0;
            this.checkBox_FilterText.Text = "Text";
            this.checkBox_FilterText.UseVisualStyleBackColor = true;
            // 
            // label_sizeFilter_unit_global
            // 
            this.label_sizeFilter_unit_global.AutoSize = true;
            this.label_sizeFilter_unit_global.Location = new System.Drawing.Point(182, 79);
            this.label_sizeFilter_unit_global.Name = "label_sizeFilter_unit_global";
            this.label_sizeFilter_unit_global.Size = new System.Drawing.Size(23, 13);
            this.label_sizeFilter_unit_global.TabIndex = 22;
            this.label_sizeFilter_unit_global.Text = "MB";
            // 
            // label_sizeFilter_unit_text
            // 
            this.label_sizeFilter_unit_text.AutoSize = true;
            this.label_sizeFilter_unit_text.Location = new System.Drawing.Point(182, 102);
            this.label_sizeFilter_unit_text.Name = "label_sizeFilter_unit_text";
            this.label_sizeFilter_unit_text.Size = new System.Drawing.Size(23, 13);
            this.label_sizeFilter_unit_text.TabIndex = 23;
            this.label_sizeFilter_unit_text.Text = "MB";
            // 
            // label_sizeFilter_unit_files
            // 
            this.label_sizeFilter_unit_files.AutoSize = true;
            this.label_sizeFilter_unit_files.Location = new System.Drawing.Point(182, 125);
            this.label_sizeFilter_unit_files.Name = "label_sizeFilter_unit_files";
            this.label_sizeFilter_unit_files.Size = new System.Drawing.Size(23, 13);
            this.label_sizeFilter_unit_files.TabIndex = 24;
            this.label_sizeFilter_unit_files.Text = "MB";
            // 
            // label_sizeFilter_unit_audio
            // 
            this.label_sizeFilter_unit_audio.AutoSize = true;
            this.label_sizeFilter_unit_audio.Location = new System.Drawing.Point(182, 171);
            this.label_sizeFilter_unit_audio.Name = "label_sizeFilter_unit_audio";
            this.label_sizeFilter_unit_audio.Size = new System.Drawing.Size(23, 13);
            this.label_sizeFilter_unit_audio.TabIndex = 25;
            this.label_sizeFilter_unit_audio.Text = "MB";
            // 
            // label_sizeFilter_unit_images
            // 
            this.label_sizeFilter_unit_images.AutoSize = true;
            this.label_sizeFilter_unit_images.Location = new System.Drawing.Point(182, 146);
            this.label_sizeFilter_unit_images.Name = "label_sizeFilter_unit_images";
            this.label_sizeFilter_unit_images.Size = new System.Drawing.Size(23, 13);
            this.label_sizeFilter_unit_images.TabIndex = 26;
            this.label_sizeFilter_unit_images.Text = "MB";
            // 
            // label_sizeFilter_unit_multi
            // 
            this.label_sizeFilter_unit_multi.AutoSize = true;
            this.label_sizeFilter_unit_multi.Location = new System.Drawing.Point(182, 194);
            this.label_sizeFilter_unit_multi.Name = "label_sizeFilter_unit_multi";
            this.label_sizeFilter_unit_multi.Size = new System.Drawing.Size(23, 13);
            this.label_sizeFilter_unit_multi.TabIndex = 27;
            this.label_sizeFilter_unit_multi.Text = "MB";
            // 
            // label_sizeFilter_unit_unknown
            // 
            this.label_sizeFilter_unit_unknown.AutoSize = true;
            this.label_sizeFilter_unit_unknown.Location = new System.Drawing.Point(182, 217);
            this.label_sizeFilter_unit_unknown.Name = "label_sizeFilter_unit_unknown";
            this.label_sizeFilter_unit_unknown.Size = new System.Drawing.Size(23, 13);
            this.label_sizeFilter_unit_unknown.TabIndex = 28;
            this.label_sizeFilter_unit_unknown.Text = "MB";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(285, 444);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumHistory)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_general.ResumeLayout(false);
            this.tabPage_general.PerformLayout();
            this.groupBox_hotkey.ResumeLayout(false);
            this.groupBox_hotkey.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLifeTime)).EndInit();
            this.tabPage_filter.ResumeLayout(false);
            this.groupBox_sizeFilter.ResumeLayout(false);
            this.groupBox_sizeFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_unknown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_multi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_audio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_images)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_files)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_text)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_sizeFilter_global)).EndInit();
            this.groupBox_typeFilter.ResumeLayout(false);
            this.groupBox_typeFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAutostart;
        private System.Windows.Forms.NumericUpDown numericUpDownNumHistory;
        private System.Windows.Forms.CheckBox checkBoxStoreAtClear;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelNumHistory;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.NumericUpDown numericUpDownLifeTime;
        private System.Windows.Forms.CheckBox checkBoxLifeTime;
        private System.Windows.Forms.GroupBox groupBox_typeFilter;
        private System.Windows.Forms.CheckBox checkBox_FilterUnknown;
        private System.Windows.Forms.CheckBox checkBox_FilterMulti;
        private System.Windows.Forms.CheckBox checkBox_FilterImages;
        private System.Windows.Forms.CheckBox checkBox_FilterFiles;
        private System.Windows.Forms.CheckBox checkBox_FilterText;
        private System.Windows.Forms.CheckBox checkBox_FilterAudio;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_general;
        private System.Windows.Forms.TabPage tabPage_filter;
        private System.Windows.Forms.GroupBox groupBox_sizeFilter;
        private System.Windows.Forms.Label label_typeFilter_desc;
        private System.Windows.Forms.NumericUpDown numericUpDown_sizeFilter_unknown;
        private System.Windows.Forms.NumericUpDown numericUpDown_sizeFilter_audio;
        private System.Windows.Forms.NumericUpDown numericUpDown_sizeFilter_images;
        private System.Windows.Forms.NumericUpDown numericUpDown_sizeFilter_files;
        private System.Windows.Forms.NumericUpDown numericUpDown_sizeFilter_text;
        private System.Windows.Forms.NumericUpDown numericUpDown_sizeFilter_global;
        private System.Windows.Forms.CheckBox checkBox_sizeFilter_unknown;
        private System.Windows.Forms.CheckBox checkBox_sizeFilter_multi;
        private System.Windows.Forms.CheckBox checkBox_sizeFilter_audio;
        private System.Windows.Forms.CheckBox checkBox_sizeFilter_images;
        private System.Windows.Forms.CheckBox checkBox_sizeFilter_files;
        private System.Windows.Forms.CheckBox checkBox_sizeFilter_text;
        private System.Windows.Forms.CheckBox checkBox_sizeFilter_global;
        private System.Windows.Forms.Label label_sizeFilter_desc;
        private System.Windows.Forms.GroupBox groupBox_hotkey;
        private System.Windows.Forms.Label label_hotkey_clear;
        private HotkeyControl hotkeyControl_clear;
        private System.Windows.Forms.Label label_hotkey_pause;
        private HotkeyControl hotkeyControl_pause;
        private System.Windows.Forms.Label label_hotkey_openHistory;
        private HotkeyControl hotkeyControl_open;
        private System.Windows.Forms.NumericUpDown numericUpDown_sizeFilter_multi;
        private System.Windows.Forms.Label label_sizeFilter_unit_unknown;
        private System.Windows.Forms.Label label_sizeFilter_unit_multi;
        private System.Windows.Forms.Label label_sizeFilter_unit_images;
        private System.Windows.Forms.Label label_sizeFilter_unit_audio;
        private System.Windows.Forms.Label label_sizeFilter_unit_files;
        private System.Windows.Forms.Label label_sizeFilter_unit_text;
        private System.Windows.Forms.Label label_sizeFilter_unit_global;
    }
}