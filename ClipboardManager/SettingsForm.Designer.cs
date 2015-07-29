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
            this.numericUpDownLifeTime = new System.Windows.Forms.NumericUpDown();
            this.checkBoxLifeTime = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumHistory)).BeginInit();
            this.panel.SuspendLayout();
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
            this.numericUpDownNumHistory.TabIndex = 2;
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
            this.buttonSave.Location = new System.Drawing.Point(71, 110);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(58, 23);
            this.buttonSave.TabIndex = 5;
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
            this.buttonCancel.Location = new System.Drawing.Point(135, 110);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(66, 23);
            this.buttonCancel.TabIndex = 6;
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
            this.labelNumHistory.TabIndex = 5;
            this.labelNumHistory.Text = "Number of History entries";
            // 
            // panel
            // 
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
            this.panel.Size = new System.Drawing.Size(213, 145);
            this.panel.TabIndex = 6;
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
            this.numericUpDownLifeTime.TabIndex = 4;
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
            this.checkBoxLifeTime.TabIndex = 3;
            this.checkBoxLifeTime.Text = "History lifetime in minutes";
            this.checkBoxLifeTime.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(213, 145);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumHistory)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
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
    }
}