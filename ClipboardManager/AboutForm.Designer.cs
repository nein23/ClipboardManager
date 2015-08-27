namespace ClipboardManager
{
    partial class AboutForm
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
            this.label_title = new System.Windows.Forms.Label();
            this.pictureBox_icon = new System.Windows.Forms.PictureBox();
            this.label_text = new System.Windows.Forms.Label();
            this.label_URL = new System.Windows.Forms.Label();
            this.pictureBox_exit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_exit)).BeginInit();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(50, 8);
            this.label_title.Margin = new System.Windows.Forms.Padding(0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(142, 20);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "Clipboard Manager";
            // 
            // pictureBox1_icon
            // 
            this.pictureBox_icon.Image = global::ClipboardManager.Properties.Resources.info_blue;
            this.pictureBox_icon.Location = new System.Drawing.Point(15, 15);
            this.pictureBox_icon.Name = "pictureBox1_icon";
            this.pictureBox_icon.Size = new System.Drawing.Size(26, 26);
            this.pictureBox_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_icon.TabIndex = 1;
            this.pictureBox_icon.TabStop = false;
            // 
            // label_text
            // 
            this.label_text.AutoSize = true;
            this.label_text.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_text.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_text.Location = new System.Drawing.Point(50, 28);
            this.label_text.Name = "label_text";
            this.label_text.Size = new System.Drawing.Size(74, 20);
            this.label_text.TabIndex = 2;
            this.label_text.Text = "Copyright";
            // 
            // label_URL
            // 
            this.label_URL.AutoSize = true;
            this.label_URL.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_URL.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_URL.Location = new System.Drawing.Point(50, 48);
            this.label_URL.Name = "label_URL";
            this.label_URL.Size = new System.Drawing.Size(88, 20);
            this.label_URL.TabIndex = 3;
            this.label_URL.Text = "Visit GitHub";
            this.label_URL.Click += new System.EventHandler(this.label_URL_Click);
            // 
            // pictureBox_exit
            // 
            this.pictureBox_exit.Location = new System.Drawing.Point(335, 14);
            this.pictureBox_exit.Name = "pictureBox_exit";
            this.pictureBox_exit.Size = new System.Drawing.Size(10, 10);
            this.pictureBox_exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_exit.TabIndex = 4;
            this.pictureBox_exit.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(360, 80);
            this.Controls.Add(this.pictureBox_exit);
            this.Controls.Add(this.label_URL);
            this.Controls.Add(this.label_text);
            this.Controls.Add(this.pictureBox_icon);
            this.Controls.Add(this.label_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AboutForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_exit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox pictureBox_icon;
        private System.Windows.Forms.Label label_text;
        private System.Windows.Forms.Label label_URL;
        private System.Windows.Forms.PictureBox pictureBox_exit;
    }
}