using ClipboardManager.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClipboardManager
{
    public sealed class ClipboardContextMenu : ContextMenuStrip
    {
        private readonly ToolStripMenuItem _emptyItem = new ToolStripMenuItem("No entries...");
        
        private int _maxEntries;

        public int Length => Items.Count == 1 && Items[0] == _emptyItem ? 0 : Items.Count;

        #region Init

        public ClipboardContextMenu(int maxEntries)
        {
            _maxEntries = maxEntries;
            ShowItemToolTips = false;
            _emptyItem.Enabled = false;
            Items.Add(_emptyItem);
        }

        #endregion

        #region Events

        public event EventHandler ContentChanged;
        public event EventHandler RequestReopen;

        private void OnContentChanged()
        {
            ContentChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnRequestReopen()
        {
            RequestReopen?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Methods

        public void AddCurrentClipboard()
        {
            ClipboardContent content = ClipboardContent.GetCurrentClipboardContent();
            if (content == null) return;
            Remove(_emptyItem);
            ClipboardToolStripMenuItem item = new ClipboardToolStripMenuItem(content);
            foreach (ToolStripItem i in Items)
            {
                if (i is ClipboardToolStripMenuItem)
                {
                    ClipboardToolStripMenuItem ci = i as ClipboardToolStripMenuItem;
                    if (item.Content.IsDuplicate(ci.Content))
                    {
                        Remove(ci);
                        break;
                    }
                }
            }
            Items.Insert(0, item);
            item.MouseUp += item_MouseUp;
            MatchLength();
            OnContentChanged();
        }

        private void Remove(ToolStripItem item)
        {
            if (item != null)
            {
                Items.Remove(item);
                if (item != _emptyItem)
                {
                    item.Dispose();
                }
            }
        }

        public void UpdateLength(int maxEntries)
        {
            _maxEntries = maxEntries;
            MatchLength();
            OnContentChanged();
        }

        private void MatchLength()
        {
            while (Items.Count > 1 && Items.Count > _maxEntries)
            {
                Remove(Items[Items.Count - 1]);
            }
        }

        internal void ClearHistory()
        {
            if (!Items.Contains(_emptyItem))
            {
                Items.Clear();
                Items.Add(_emptyItem);
                OnContentChanged();
            }
        }

        #endregion

        #region Handler

        private void item_MouseUp(object sender, MouseEventArgs e)
        {
            var menuItem = sender as ClipboardToolStripMenuItem;
            if (menuItem != null)
            {
                ClipboardToolStripMenuItem item = menuItem;
                if (e.Button == MouseButtons.Right)
                {
                    Remove(item);
                    if (Items.Count == 0)
                    {
                        Items.Add(_emptyItem);
                    }
                    OnContentChanged();
                    OnRequestReopen();
                }
                else if (e.Button == MouseButtons.Left)
                {
                    Remove(item);
                    item.Content.Restore();
                }
            }
        }

        #endregion
    }
}
