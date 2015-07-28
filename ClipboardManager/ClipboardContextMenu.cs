using ClipboardManager.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ClipboardManager
{
    public class ClipboardContextMenu : ContextMenuStrip
    {
        #region Constants

        private readonly ToolStripMenuItem clipboardEmptyItem = new ToolStripMenuItem("Clipboard empty...");
        private readonly ToolStripMenuItem historyEmptyItem = new ToolStripMenuItem("History empty...");
        private readonly ToolStripSeparator sep = new ToolStripSeparator();

        #endregion

        #region Fields

        private NotifyIcon ni;
        private Settings settings;
        private ToolTip toolTip = new ToolTip();
        private Image toolTipImage = null;

        #endregion

        #region Init

        public ClipboardContextMenu(NotifyIcon ni, Settings settings)
        {
            this.ni = ni;
            this.settings = settings;
            ShowItemToolTips = false;
            clipboardEmptyItem.Enabled = false;
            historyEmptyItem.Enabled = false;
            Items.Add(clipboardEmptyItem);
            Items.Add(sep);
            Items.Add(historyEmptyItem);
        }

        private void toolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Draw the background and border.
            e.DrawBackground();
            e.DrawBorder();

            if (toolTipImage != null)
            {
                // Draw the image.
                e.Graphics.DrawImage(toolTipImage, 1, 1);
            }
            else
            {
                // Draw the text.
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;

                    Rectangle rect = new Rectangle(5, 5, e.Bounds.Width, e.Bounds.Height);
                    e.Graphics.DrawString( e.ToolTipText, e.Font, Brushes.Black, rect, sf);
                }
            }
        }

        private void toolTip_Popup(object sender, PopupEventArgs e)
        {
            int wid = e.ToolTipSize.Width + 10;
            int hgt = e.ToolTipSize.Height + 10;
            if (toolTipImage != null)
            {
                wid = toolTipImage.Width + 2;
                hgt = toolTipImage.Height + 2;
            }
            e.ToolTipSize = new Size(wid, hgt);
        }

        #endregion

        #region Methods

        public void update()
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            IDataObject iData = Clipboard.GetDataObject();
            Dictionary<string, object> contents = Util.getContents(iData);
            if (contents.Count == 0)
            {
                setClipboardItem(null);
                return;
            }
            Tuple<string, string, Image> itemData = Util.getItemData(iData);
            item.Text = itemData.Item1;
            item.ToolTipText = itemData.Item2;
            item.Image = itemData.Item3;
            item.Tag = contents;
            item.AutoToolTip = false;
            item.MouseUp += item_MouseUpRight;
            item.MouseEnter += item_MouseEnter;
            item.MouseLeave += item_MouseLeave;
            setClipboardItem(item);
        }

        public void setClipboardItem(ToolStripMenuItem clipboardItem)
        {
            ToolStripItem cur = Items[0];
            if (clipboardItem == null)
            {
                if (isClipboardEmptyItem(cur)) return;
                Items.RemoveAt(0);
                Items.Insert(0, clipboardEmptyItem);
                if (settings.StoreAtClear) addHistoryItem(cur);
                else if (cur != null) cur.Dispose();
            }
            else
            {
                Items.Remove(cur);
                Items.Insert(0, clipboardItem);
                if (!isClipboardEmptyItem(cur)) addHistoryItem(cur);
            }
        }

        public void removeItem(ToolStripItem item)
        {
            if (item != null && !isEmptyItem(item))
            {
                toolTip.Hide(item.Owner);
                if (isHistoryItem(item)) removeHistoryItem(item);
                else Clipboard.Clear();
            }
        }

        public void addHistoryItem(ToolStripItem historyItem)
        {
            Items.Remove(historyEmptyItem);
            Items.Insert(2, historyItem);
            historyItem.MouseUp += item_MouseUpLeft;
            while (isHistoryOverFilled()) Items.RemoveAt(Items.Count - 1);
            updateNotifyIcon();
        }

        public bool removeHistoryItem(ToolStripItem historyItem)
        {
            if(isHistoryItem(historyItem))
            {
                Items.Remove(historyItem);
                if (Items.Count == 2) Items.Add(historyEmptyItem);
                updateNotifyIcon();
                return true;
            }
            return false;
        }

        public void clearHistory()
        {
            while (Items.Count > 2)
            {
                ToolStripItem item = Items[Items.Count - 1];
                if (!removeHistoryItem(item)) break;
            }
            if (Items.Count == 2) Items.Add(historyEmptyItem);
            updateNotifyIcon();
        }

        public void machtsHistorySettings()
        {
            while (Items.Count > settings.NumHistory + 2)
            {
                ToolStripItem item = Items[Items.Count - 1];
                if (!removeHistoryItem(item)) break;
            }
        }

        public bool isHistoryItem(ToolStripItem item)
        {
            return (item != null && item is ToolStripMenuItem && !isEmptyItem(item) && Items.IndexOf(item) >= 2);
        }

        private bool isEmptyItem(ToolStripItem item) 
        {
            return (isClipboardEmptyItem(item) || isHistoryEmptyItem(item));
        }

        private bool isClipboardEmptyItem(ToolStripItem item)
        {
            return (item != null && item is ToolStripMenuItem && clipboardEmptyItem.Equals(item));
        }

        private bool isHistoryEmptyItem(ToolStripItem item)
        {
            return (item != null && item is ToolStripMenuItem && historyEmptyItem.Equals(item));
        }

        private bool isHistoryOverFilled()
        {
            return Items.Count > settings.NumHistory + 2;
        }

        private void updateNotifyIcon()
        {
            if (Items.Count == 3 && historyEmptyItem.Equals(Items[2]))
            {
                ni.Icon = Resources.clipboard_0_32;
            }
            else {
                int part = settings.NumHistory / 3;
                if (Items.Count <= part + 2) ni.Icon = Resources.clipboard_1_32;
                else if (Items.Count <= 2 * part + 2) ni.Icon = Resources.clipboard_2_32;
                else ni.Icon = Resources.clipboard_3_32;
            }
        }

        #endregion

        #region Handler

        private void item_MouseUpRight(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item.Tag != null)
                {
                    removeItem(item);
                }
            }
        }

        private void item_MouseUpLeft(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item.Tag != null && isHistoryItem(item) && item.Tag is Dictionary<string, object>)
                {
                    Dictionary<string, object> contents = (Dictionary<string, object>)item.Tag;
                    if (contents != null)
                    {
                        IDataObject iData = new DataObject();
                        foreach (string format in contents.Keys)
                        {
                            iData.SetData(format, contents[format]);
                        }
                        try
                        {
                            removeItem(item);
                            Clipboard.SetDataObject(iData, true);
                        }
                        catch { }
                    }
                }
            }
        }

        private void item_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Dispose();
            toolTip = new ToolTip();
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string toolTipStr = item.ToolTipText;
            int itemHeight = item.Bounds.Height;
            int yOffSet = 0;
            int index = Items.IndexOf(item);
            if (toolTipStr != null)
            {
                string[] split = toolTipStr.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                int numLines = split.Length;
                yOffSet = -1 * (5+(numLines * 15));
            }
            else
            {
                toolTip.OwnerDraw = true;
                toolTip.Popup += toolTip_Popup;
                toolTip.Draw += toolTip_Draw;
                toolTipImage = item.Image;
                toolTipStr = "empty";
                int imgheight = toolTipImage.Height;
                yOffSet = -1 * (imgheight+2);
            }
            toolTip.Show(toolTipStr, item.Owner, item.Bounds.X, yOffSet);
        }

        private void item_MouseLeave(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            toolTip.Hide(item.Owner);
            toolTipImage = null;
        }

        #endregion
    }
}
