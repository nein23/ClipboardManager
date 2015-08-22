using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardManager
{
    internal class ClipboardToolStripMenuItem : ToolStripMenuItem
    {
        public static readonly int TYPE_TEXT = 0;
        public static readonly int TYPE_FILES = 1;
        public static readonly int TYPE_IMAGES = 2;
        public static readonly int TYPE_AUDIO = 3;
        public static readonly int TYPE_MULTI = 4;
        public static readonly int TYPE_UNKNOWN = 5;

        Dictionary<string, object> data;
        private ToolTip toolTip;
        private Image toolTipImage;
        private int toolTipOffset;
        private Timer lifeTimeTimer;
        private int type;

        internal Dictionary<string, object> Data { get { return data; } }
        internal ToolTip ToolTip { get { return toolTip; } }
        internal Timer LifeTimeTimer { get { return lifeTimeTimer; } }
        internal DateTime Date { get; set; }
        internal int Type { get { return type; } }

        internal ClipboardToolStripMenuItem(string text, Image image, Dictionary<string, object> data, string toolTipText, Image toolTipImage, int type)
        {
            this.Text = text;
            this.Image = image;
            this.data = data;
            this.ToolTipText = toolTipText;
            this.toolTipImage = toolTipImage;
            this.type = type;

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

        #endregion
    }
}
