using ClipboardManager.Properties;
using ClipboardManager.Util;
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
        private Timer gcTimer;
        
        public Point MousePos { get; set; }

        #endregion

        #region Init

        public ClipboardContextMenu(NotifyIcon ni, Settings settings)
        {
            this.ni = ni;
            this.settings = settings;
            ShowItemToolTips = false;

            clipboardEmptyItem.Enabled = false;
            historyEmptyItem.Enabled = false;
            clearHistory();

            gcTimer = new Timer();
            gcTimer.Interval = 100;
            gcTimer.Tick += GcTimer_Tick;
            gcTimer.Enabled = false;

            update();
        }

        private void GcTimer_Tick(object sender, EventArgs e)
        {
            GC.Collect();
        }

        #endregion

        #region Methods

        internal void update()
        {
            IDataObject iData = Clipboard.GetDataObject();
            ClipboardContent content = new ClipboardContent(iData);
            if (content.isEmpty())
            {
                setClipboardItem(null);
                return;
            }
            ClipboardToolStripMenuItem item = new ClipboardToolStripMenuItem(content);
            item.MouseUp += item_MouseUpRight;
            setClipboardItem(item);
        }

        private void setClipboardItem(ClipboardToolStripMenuItem clipboardItem)
        {
            ToolStripItem cur = Items[0];
            if (cur != null)
            {
                if (clipboardItem == null)
                {
                    if (isClipboardEmptyItem(cur)) return;
                    Items.RemoveAt(0);
                    Items.Insert(0, clipboardEmptyItem);
                    if (settings.StoreAtClear && cur is ClipboardToolStripMenuItem) addHistoryItem(cur as ClipboardToolStripMenuItem);
                    else if (cur != null) removeItemComplete(cur);
                }
                else
                {
                    if (isFiltered(clipboardItem))
                        clipboardItem.setPlaceholder();
                    else
                    {
                        ClipboardToolStripMenuItem duplicate = ClipboardUtil.findDuplicate(Items, clipboardItem);
                        if (duplicate != null)
                        {
                            Items.Remove(duplicate);
                            if(duplicate.Equals(cur) && cur is ClipboardToolStripMenuItem)
                                (cur as ClipboardToolStripMenuItem).setPlaceholder();
                            else removeItemComplete(duplicate);
                        }

                        Settings.SizeFilterSettings sizeSettings = ClipboardUtil.getMaxSizeForClipboardType(clipboardItem.Formats, settings);
                        long ciSize = clipboardItem.DataSize / 1000;
                        if ((sizeSettings.enabled && (sizeSettings.value * 1000) < ciSize)
                            || (settings.SizeFilterGlobal.enabled && (settings.SizeFilterGlobal.value * 1000) < ciSize))
                        {
                            clipboardItem.setPlaceholder();
                        }
                    }
                    Items.Remove(cur);
                    Items.Insert(0, clipboardItem);
                    if (cur is ClipboardToolStripMenuItem && !((cur as ClipboardToolStripMenuItem).PlaceHolder))
                        addHistoryItem(cur as ClipboardToolStripMenuItem);
                    else removeItemComplete(cur);
                }
            }
        }

        private void removeItem(ClipboardToolStripMenuItem item)
        {
            if (item != null)
            {
                item.ToolTip.Hide(item.Owner);
                if (isHistoryItem(item)) removeHistoryItem(item);
                else
                {
                    removeItemComplete(item);
                    Items.Insert(0, clipboardEmptyItem);
                    Clipboard.Clear();
                }
            }
        }

        private void addHistoryItem(ClipboardToolStripMenuItem historyItem)
        {
            if (historyItem != null && !historyItem.PlaceHolder)
            {
                Items.Remove(historyEmptyItem);
                Items.Insert(2, historyItem);
                historyItem.MouseUp += item_MouseUpLeft;
                historyItem.Date = DateTime.Now;
                historyItem.LifeTimeTimer.Tick += lifeTimeTimer_Tick;
                if (settings.LifeTimeEnabled) historyItem.LifeTimeTimer.Start();
                cutHistory();
                updateNotifyIcon();
            }
        }

        private bool isFiltered(ClipboardToolStripMenuItem historyItem)
        {
            if (!ClipboardUtil.isSingleDataFormat(historyItem.Formats)) return !settings.TypeFilter.multi;
            if (ClipboardUtil.containsClipboardDataFormat(historyItem.Formats, ClipboardUtil.ClipboardDataFormat.Text)) return !settings.TypeFilter.text;
            if (ClipboardUtil.containsClipboardDataFormat(historyItem.Formats, ClipboardUtil.ClipboardDataFormat.Files)) return !settings.TypeFilter.files;
            if (ClipboardUtil.containsClipboardDataFormat(historyItem.Formats, ClipboardUtil.ClipboardDataFormat.Images)) return !settings.TypeFilter.images;
            if (ClipboardUtil.containsClipboardDataFormat(historyItem.Formats, ClipboardUtil.ClipboardDataFormat.Audio)) return !settings.TypeFilter.audio;
            if (ClipboardUtil.containsClipboardDataFormat(historyItem.Formats, ClipboardUtil.ClipboardDataFormat.Unknown)) return !settings.TypeFilter.unknown;
            return false;
        }

        private bool removeHistoryItem(ClipboardToolStripMenuItem historyItem)
        {
            if(isHistoryItem(historyItem))
            {
                historyItem.LifeTimeTimer.Stop();
                removeItemComplete(historyItem);
                if (Items.Count == 2) Items.Add(historyEmptyItem);
                updateNotifyIcon();
                return true;
            }
            return false;
        }

        internal void updateHistoryToMatchSettings()
        {
            List<ClipboardToolStripMenuItem> toDelete = new List<ClipboardToolStripMenuItem>();
            foreach(ToolStripItem item in Items)
            {
                if (item is ClipboardToolStripMenuItem)
                {
                    ClipboardToolStripMenuItem ci = item as ClipboardToolStripMenuItem;
                    int index = Items.IndexOf(ci);
                    if (isFiltered(ci))
                    {
                        if (index == 0) ci.setPlaceholder();
                        else toDelete.Add(ci);
                    }
                    else
                    {
                        Settings.SizeFilterSettings sizeSettings = ClipboardUtil.getMaxSizeForClipboardType(ci.Formats, settings);
                        if (sizeSettings.enabled && (sizeSettings.value * 1000) < (ci.DataSize / 1000))
                        {
                            if (index == 0) ci.setPlaceholder();
                            else toDelete.Add(ci);
                        }
                    }
                }
            }
            for(int i = 0; i < toDelete.Count; i++)
            {
                Items.Remove(toDelete[i]);
            }
            cutHistory();

            ToolStripItem cur = Items[0];
            if (cur is ClipboardToolStripMenuItem)
            {
                ClipboardToolStripMenuItem ci = cur as ClipboardToolStripMenuItem;
                if (settings.SizeFilterGlobal.enabled && (settings.SizeFilterGlobal.value * 1000) < (ci.DataSize / 1000))
                {
                    ci.setPlaceholder();
                }
            }
        }


        internal void cutHistory()
        {
            while (isHistoryOverFilled())
            {
                ToolStripItem item = Items[Items.Count - 1];
                if (isHistoryItem(item)) removeHistoryItem(item as ClipboardToolStripMenuItem);
                else break;
            }
            if (Items.Count == 2) Items.Add(historyEmptyItem);
            updateNotifyIcon();
        }

        internal void clearHistory()
        {
            Items.Clear();
            Items.Add(clipboardEmptyItem);
            Items.Add(sep);
            Items.Add(historyEmptyItem);
            updateNotifyIcon();
        }

        private long getListSizeInKB()
        {
            long size = 0;
            foreach(ToolStripItem item in Items)
            {
                if(item is ClipboardToolStripMenuItem)
                    size += (item as ClipboardToolStripMenuItem).DataSize;
            }
            return size / 1000;
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
            return Items.Count > settings.NumHistory + 2 || (settings.SizeFilterGlobal.enabled && (settings.SizeFilterGlobal.value * 1000) < getListSizeInKB());
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

        internal void removeItemComplete(ToolStripItem item)
        {
            if (item != null && item is ClipboardToolStripMenuItem && Items.Contains(item))
            {
                Items.Remove(item);
            }
            item = null;
            gcTimer.Enabled = false;
            gcTimer.Enabled = true;
        }

        #endregion

        #region Handler

        private void item_MouseUpRight(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && sender is ClipboardToolStripMenuItem)
            {
                removeItem(sender as ClipboardToolStripMenuItem);
                Timer reopenTimer = new Timer();
                reopenTimer.Interval = 1;
                reopenTimer.Tick += ReopenTimer_Tick;
                reopenTimer.Enabled = true;
            }
        }

        private void ReopenTimer_Tick(object sender, EventArgs e)
        {
            ((Timer)sender).Enabled = false;
            Point curPos = Cursor.Position;
            Cursor.Position = MousePos;
            ContextUtil.openLeftClickContextMenu(ni, this);
            Cursor.Position = curPos;
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
                        foreach (KeyValuePair<string, object> pair in contents)
                        {
                            string format = pair.Key;
                            object data = pair.Value;
                            if (format != null && data != null)
                            {
                                iData.SetData(format, data);
                            }
                        }
                        try
                        {
                            removeItem(item);
                            Clipboard.SetDataObject(iData, false);
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
