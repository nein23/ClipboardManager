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

        #endregion

        #region Methods

        internal void update()
        {
            IDataObject iData = Clipboard.GetDataObject();
            Dictionary<string, object> contents = Util.getContents(iData);
            if (contents.Count == 0)
            {
                setClipboardItem(null);
                return;
            }
            Tuple<string, Image, string, Image> itemData = Util.getItemData(iData);
            ClipboardToolStripMenuItem item = new ClipboardToolStripMenuItem(
                itemData.Item1,
                itemData.Item2,
                contents,
                itemData.Item3,
                itemData.Item4);
            item.MouseUp += item_MouseUpRight;
            setClipboardItem(item);
        }

        private void setClipboardItem(ClipboardToolStripMenuItem clipboardItem)
        {
            ToolStripItem cur = Items[0];
            if (clipboardItem == null)
            {
                if (isClipboardEmptyItem(cur)) return;
                Items.RemoveAt(0);
                Items.Insert(0, clipboardEmptyItem);
                if (settings.StoreAtClear && cur is ClipboardToolStripMenuItem) addHistoryItem(cur as ClipboardToolStripMenuItem);
                else if (cur != null) cur.Dispose();
            }
            else
            {
                Items.Remove(cur);
                Items.Insert(0, clipboardItem);
                if (cur is ClipboardToolStripMenuItem) addHistoryItem(cur as ClipboardToolStripMenuItem);
            }
        }

        private void removeItem(ClipboardToolStripMenuItem item)
        {
            if (item != null)
            {
                item.ToolTip.Hide(item.Owner);
                if (isHistoryItem(item)) removeHistoryItem(item);
                else Clipboard.Clear();
            }
        }

        private void addHistoryItem(ClipboardToolStripMenuItem historyItem)
        {
            Items.Remove(historyEmptyItem);
            Items.Insert(2, historyItem);
            historyItem.MouseUp += item_MouseUpLeft;
            historyItem.Date = DateTime.Now;
            historyItem.LifeTimeTimer.Tick += lifeTimeTimer_Tick;
            if (settings.LifeTimeEnabled) historyItem.LifeTimeTimer.Start();
            while (isHistoryOverFilled()) Items.RemoveAt(Items.Count - 1);
            updateNotifyIcon();
        }

        private bool removeHistoryItem(ClipboardToolStripMenuItem historyItem)
        {
            if(isHistoryItem(historyItem))
            {
                historyItem.LifeTimeTimer.Stop();
                Items.Remove(historyItem);
                if (Items.Count == 2) Items.Add(historyEmptyItem);
                updateNotifyIcon();
                return true;
            }
            return false;
        }

        internal void clearHistory(int historySize)
        {
            while (Items.Count > historySize + 2)
            {
                ToolStripItem item = Items[Items.Count - 1];
                if (isHistoryItem(item)) removeHistoryItem(item as ClipboardToolStripMenuItem);
                else break;
            }
            if (Items.Count == 2) Items.Add(historyEmptyItem);
            updateNotifyIcon();
        }

        private bool isHistoryItem(ToolStripItem item)
        {
            return (item != null && item is ClipboardToolStripMenuItem && Items.IndexOf(item) >= 2);
        }

        private bool isClipboardEmptyItem(ToolStripItem item)
        {
            return (item != null && item is ToolStripMenuItem && clipboardEmptyItem.Equals(item));
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

        internal void lifeTimeSettingsChanged()
        {
            foreach (ToolStripItem item in Items)
            {
                if (isHistoryItem(item))
                {
                    DateTime now = DateTime.Now;
                    ClipboardToolStripMenuItem citem = item as ClipboardToolStripMenuItem;
                    citem.LifeTimeTimer.Stop();
                    if (settings.LifeTimeEnabled)
                    {
                        citem.Date = now;
                        citem.LifeTimeTimer.Start();
                    }
                }
            }
        }

        #endregion

        #region Handler

        private void item_MouseUpRight(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && sender is ClipboardToolStripMenuItem)
            {
                removeItem(sender as ClipboardToolStripMenuItem);
            }
        }

        private void item_MouseUpLeft(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender is ClipboardToolStripMenuItem)
            {
                ClipboardToolStripMenuItem item = sender as ClipboardToolStripMenuItem;
                if (item != null)
                {
                    Dictionary<string, object> contents = item.Data;
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

        private void lifeTimeTimer_Tick(object sender, EventArgs e)
        {
            if(sender != null && sender is Timer) {
                Timer timer = sender as Timer;
                if (timer.Tag != null && timer.Tag is ClipboardToolStripMenuItem)
                {
                    ClipboardToolStripMenuItem item = timer.Tag as ClipboardToolStripMenuItem;
                    DateTime end = item.Date.AddMinutes(settings.LifeTimeValue);
                    if (settings.LifeTimeEnabled && DateTime.Now.CompareTo(end) >= 0)
                    {
                        timer.Stop();
                        removeItem(item);
                    }
                }
            }
        }

        #endregion
    }
}
