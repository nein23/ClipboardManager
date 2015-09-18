using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ClipboardManager
{
    internal class ClipboardToolStripMenuItem : ToolStripMenuItem
    {
        private int formats;
        private Dictionary<string, object> data;
        private Dictionary<string, string> hashs;
        private long dataSize;
        private ToolTip toolTip;
        private Image toolTipImage;
        private int toolTipOffset;
        private Timer lifeTimeTimer;
        private bool placeHolder = false;

        internal int Formats { get { return formats; } }
        internal Dictionary<string, object> Data { get { return data; } }
        internal Dictionary<string, string> Hashs { get { return hashs; } }
        internal long DataSize { get { return dataSize; } }
        internal ToolTip ToolTip { get { return toolTip; } }
        internal Timer LifeTimeTimer { get { return lifeTimeTimer; } }
        internal DateTime Date { get; set; }
        internal bool PlaceHolder { get { return placeHolder; } }

        internal ClipboardToolStripMenuItem(ClipboardContent content)
        {
            this.Text = content.Text;
            this.Image = content.Image;
            this.data = content.Data;
            this.hashs = content.Hashs;
            this.dataSize = content.Size;
            this.ToolTipText = content.ToolTip;
            this.toolTipImage = content.ToolTipImage;
            this.formats = content.Foramts;

            initToolTip();

            lifeTimeTimer = new Timer();
            lifeTimeTimer.Interval = 60000;
            lifeTimeTimer.Tag = this;

            AutoToolTip = false;

            MouseEnter += ClipboardToolStripMenuItem_MouseEnter;
            MouseLeave += ClipboardToolStripMenuItem_MouseLeave;
        }

        private void initToolTip()
        {
            toolTip = new ToolTip();
            if (toolTipImage != null)
            {
                toolTip.OwnerDraw = true;
                toolTip.Popup += toolTip_Popup;
                toolTip.Draw += toolTip_Draw;
                ToolTipText = "Image";
                toolTipOffset = -1 * (toolTipImage.Height + 2);
            }
            else if (ToolTipText != null)
            {
                string[] split = ToolTipText.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                int numLines = split.Length;
                toolTipOffset = -1 * (5 + (numLines * 15));
            }
            else toolTipOffset = 0;
        }

        #region Handler

        private void ClipboardToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show(ToolTipText, Owner, Bounds.X, toolTipOffset);
        }

        private void ClipboardToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(Owner);
        }

        private void toolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            if (toolTipImage != null)
            {
                e.DrawBackground();
                e.DrawBorder();
                e.Graphics.DrawImage(toolTipImage, 1, 1);
            }
        }

        private void toolTip_Popup(object sender, PopupEventArgs e)
        {
            if (toolTipImage != null)
            {
                int wid = toolTipImage.Width + 2;
                int hgt = toolTipImage.Height + 2;
                e.ToolTipSize = new Size(wid, hgt);
            }
        }

        internal void setPlaceholder()
        {
            data = null;
            hashs = null;
            dataSize = 0;
            this.ForeColor = SystemColors.GrayText;
            placeHolder = true;
        }

        #endregion
    }
}
