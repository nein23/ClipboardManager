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
            this.hotkeyControl = new HotkeyControl();
            this.groupBox_Filter = new System.Windows.Forms.GroupBox();
            this.checkBox_FilterAudio = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterUnknown = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterMulti = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterImages = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterFiles = new System.Windows.Forms.CheckBox();
            this.checkBox_FilterText = new System.Windows.Forms.CheckBox();
            this.numericUpDownLifeTime = new System.Windows.Forms.NumericUpDown();
            this.checkBoxLifeTime = new System.Windows.Forms.CheckBox();
            this.label_HotKey = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumHistory)).BeginInit();
            this.panel.SuspendLayout();
            this.groupBox_Filter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLifeTime)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxAutostart
            // 
            this.checkBoxAutostart.AutoSize = true;
            this.checkBoxAutostart.Location = new System.Drawing.Point(12, 12);
            this.checkBoxAutostart.Name = "checkBoxAutostart";
            this.checkBoxAutostart.Size = new System.Drawing.Size(117, 17);
            this.checkBoxAutostart.TabIndex = 0;
            this.checkBoxAutostart.Text = "Start with Windows";
            this.checkBoxAutostart.UseVisualStyleBackColor = true;
            // 
            // numericUpDownNumHistory
            // 
            this.numericUpDownNumHistory.Location = new System.Drawing.Point(143, 58);
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
            this.checkBoxStoreAtClear.Location = new System.Drawing.Point(12, 35);
            this.checkBoxStoreAtClear.Name = "checkBoxStoreAtClear";
            this.checkBoxStoreAtClear.Size = new System.Drawing.Size(185, 17);
            this.checkBoxStoreAtClear.TabIndex = 1;
            this.checkBoxStoreAtClear.Text = "Historize cleared Clipboard entries";
            this.checkBoxStoreAtClear.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSave.Image = global::ClipboardManager.Properties.Resources.save;
            this.buttonSave.Location = new System.Drawing.Point(78, 236);
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
            this.buttonCancel.Location = new System.Drawing.Point(142, 236);
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
            this.labelNumHistory.Location = new System.Drawing.Point(12, 60);
            this.labelNumHistory.Name = "labelNumHistory";
            this.labelNumHistory.Size = new System.Drawing.Size(125, 13);
            this.labelNumHistory.TabIndex = 2;
            this.labelNumHistory.Text = "Number of History entries";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.label_HotKey);
            this.panel.Controls.Add(this.hotkeyControl);
            this.panel.Controls.Add(this.groupBox_Filter);
            this.panel.Controls.Add(this.numericUpDownLifeTime);
            this.panel.Controls.Add(this.checkBoxLifeTime);
            this.panel.Controls.Add(this.checkBoxAutostart);
            this.panel.Controls.Add(this.labelNumHistory);
            this.panel.Controls.Add(this.numericUpDownNumHistory);
            this.panel.Controls.Add(this.buttonCancel);
            this.panel.Controls.Add(this.checkBoxStoreAtClear);
            this.panel.Controls.Add(this.buttonSave);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(220, 271);
            this.panel.TabIndex = 0;
            // 
            // hotkeyControl
            // 
            this.hotkeyControl.Hotkey = System.Windows.Forms.Keys.None;
            this.hotkeyControl.HotkeyModifiers = System.Windows.Forms.Keys.None;
            this.hotkeyControl.Location = new System.Drawing.Point(59, 210);
            this.hotkeyControl.Name = "hotkeyControl";
            this.hotkeyControl.Size = new System.Drawing.Size(149, 20);
            this.hotkeyControl.TabIndex = 8;
            this.hotkeyControl.Text = "None";
            // 
            // groupBox_Filter
            // 
            this.groupBox_Filter.Controls.Add(this.checkBox_FilterAudio);
            this.groupBox_Filter.Controls.Add(this.checkBox_FilterUnknown);
            this.groupBox_Filter.Controls.Add(this.checkBox_FilterMulti);
            this.groupBox_Filter.Controls.Add(this.checkBox_FilterImages);
            this.groupBox_Filter.Controls.Add(this.checkBox_FilterFiles);
            this.groupBox_Filter.Controls.Add(this.checkBox_FilterText);
            this.groupBox_Filter.Location = new System.Drawing.Point(13, 110);
            this.groupBox_Filter.Name = "groupBox_Filter";
            this.groupBox_Filter.Size = new System.Drawing.Size(195, 94);
            this.groupBox_Filter.TabIndex = 6;
            this.groupBox_Filter.TabStop = false;
            this.groupBox_Filter.Text = "History Filter";
            // 
            // checkBox_FilterAudio
            // 
            this.checkBox_FilterAudio.AutoSize = true;
            this.checkBox_FilterAudio.Location = new System.Drawing.Point(91, 19);
            this.checkBox_FilterAudio.Name = "checkBox_FilterAudio";
            this.checkBox_FilterAudio.Size = new System.Drawing.Size(79, 17);
            this.checkBox_FilterAudio.TabIndex = 3;
            this.checkBox_FilterAudio.Text = "Audio Data";
            this.checkBox_FilterAudio.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterUnknown
            // 
            this.checkBox_FilterUnknown.AutoSize = true;
            this.checkBox_FilterUnknown.Location = new System.Drawing.Point(91, 65);
            this.checkBox_FilterUnknown.Name = "checkBox_FilterUnknown";
            this.checkBox_FilterUnknown.Size = new System.Drawing.Size(98, 17);
            this.checkBox_FilterUnknown.TabIndex = 5;
            this.checkBox_FilterUnknown.Text = "Unknown Data";
            this.checkBox_FilterUnknown.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterMulti
            // 
            this.checkBox_FilterMulti.AutoSize = true;
            this.checkBox_FilterMulti.Location = new System.Drawing.Point(91, 42);
            this.checkBox_FilterMulti.Name = "checkBox_FilterMulti";
            this.checkBox_FilterMulti.Size = new System.Drawing.Size(88, 17);
            this.checkBox_FilterMulti.TabIndex = 4;
            this.checkBox_FilterMulti.Text = "Multiple Data";
            this.checkBox_FilterMulti.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterImages
            // 
            this.checkBox_FilterImages.AutoSize = true;
            this.checkBox_FilterImages.Location = new System.Drawing.Point(6, 65);
            this.checkBox_FilterImages.Name = "checkBox_FilterImages";
            this.checkBox_FilterImages.Size = new System.Drawing.Size(60, 17);
            this.checkBox_FilterImages.TabIndex = 2;
            this.checkBox_FilterImages.Text = "Images";
            this.checkBox_FilterImages.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterFiles
            // 
            this.checkBox_FilterFiles.AutoSize = true;
            this.checkBox_FilterFiles.Location = new System.Drawing.Point(6, 42);
            this.checkBox_FilterFiles.Name = "checkBox_FilterFiles";
            this.checkBox_FilterFiles.Size = new System.Drawing.Size(86, 17);
            this.checkBox_FilterFiles.TabIndex = 1;
            this.checkBox_FilterFiles.Text = "Files/Folders";
            this.checkBox_FilterFiles.UseVisualStyleBackColor = true;
            // 
            // checkBox_FilterText
            // 
            this.checkBox_FilterText.AutoSize = true;
            this.checkBox_FilterText.Location = new System.Drawing.Point(6, 19);
            this.checkBox_FilterText.Name = "checkBox_FilterText";
            this.checkBox_FilterText.Size = new System.Drawing.Size(47, 17);
            this.checkBox_FilterText.TabIndex = 0;
            this.checkBox_FilterText.Text = "Text";
            this.checkBox_FilterText.UseVisualStyleBackColor = true;
            // 
            // numericUpDownLifeTime
            // 
            this.numericUpDownLifeTime.Location = new System.Drawing.Point(161, 84);
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
            this.checkBoxLifeTime.Location = new System.Drawing.Point(12, 85);
            this.checkBoxLifeTime.Name = "checkBoxLifeTime";
            this.checkBoxLifeTime.Size = new System.Drawing.Size(143, 17);
            this.checkBoxLifeTime.TabIndex = 4;
            this.checkBoxLifeTime.Text = "History lifetime in minutes";
            this.checkBoxLifeTime.UseVisualStyleBackColor = true;
            // 
            // label_HotKey
            // 
            this.label_HotKey.AutoSize = true;
            this.label_HotKey.Location = new System.Drawing.Point(12, 213);
            this.label_HotKey.Name = "label_HotKey";
            this.label_HotKey.Size = new System.Drawing.Size(41, 13);
            this.label_HotKey.TabIndex = 7;
            this.label_HotKey.Text = "Hotkey";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(220, 271);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumHistory)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox_Filter.ResumeLayout(false);
            this.groupBox_Filter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLifeTime)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox_Filter;
        private System.Windows.Forms.CheckBox checkBox_FilterUnknown;
        private System.Windows.Forms.CheckBox checkBox_FilterMulti;
        private System.Windows.Forms.CheckBox checkBox_FilterImages;
        private System.Windows.Forms.CheckBox checkBox_FilterFiles;
        private System.Windows.Forms.CheckBox checkBox_FilterText;
        private System.Windows.Forms.CheckBox checkBox_FilterAudio;
        private HotkeyControl hotkeyControl;
        private System.Windows.Forms.Label label_HotKey;
    }
}