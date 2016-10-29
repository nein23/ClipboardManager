using ClipboardManager.Properties;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ClipboardManager
{
    public sealed class ClipboardManagerTray : Control
    {
        public static readonly string ApplicationName = "Clipboard Manager";
        
        private NotifyIcon _ni;
        private SettingsForm _settingsForm;
        private ClipboardContextMenu _clipboardContextMenu;
        private Point _menuMousePos;

        #region Init

        public ClipboardManagerTray()
        {
            Name = ApplicationName;
            Visible = false;

            Settings.Load();

            InitNotifyIcon();

            InitContextMenus();

            ClipboardNotification.ClipboardUpdate += ClipboardNotification_ClipboardUpdate;
            HotkeyNotification.RegisterHotkey();
            HotkeyNotification.HotkeyPressed += HotkeyNotification_HotkeyPressed;
            Settings.SettingsChanged += Settings_SettingsChanged;
        }

        private void InitNotifyIcon()
        {
            _ni = new NotifyIcon
            {
                Text = ApplicationName,
                Icon = Resources.clipboard_0_32
            };
            _ni.MouseClick += ni_MouseClick;
            _ni.Visible = true;
        }

        private void InitContextMenus()
        {
            _clipboardContextMenu = new ClipboardContextMenu(Settings.MaxEntries);
            _clipboardContextMenu.ContentChanged += _clipboardContextMenu_ContentChanged;
            _clipboardContextMenu.RequestReopen += _clipboardContextMenu_RequestReopen;

            ContextMenuStrip optionsContextMenu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem
            {
                Text = @"Clear",
                Image = Resources.clipboardEmpty
            };
            item.Click += clearAll_Click;
            optionsContextMenu.Items.Add(item);

            item = new ToolStripMenuItem
            {
                Text = @"Settings",
                Image = Resources.settings
            };
            item.Click += settings_Click;
            optionsContextMenu.Items.Add(item);

            item = new ToolStripMenuItem
            {
                Text = @"Exit",
                Image = Resources.exit
            };
            item.Click += exit_Click;
            optionsContextMenu.Items.Add(item);

            _ni.ContextMenuStrip = optionsContextMenu;
            _clipboardContextMenu.AddCurrentClipboard();
        }

        #endregion

        #region Methods

        private void OpenLeftClickContextMenu()
        {
            _menuMousePos = Cursor.Position;
            ContextMenuStrip rightContextMenu = _ni.ContextMenuStrip;
            _ni.ContextMenuStrip = _clipboardContextMenu;
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(_ni, null);
            _ni.ContextMenuStrip = rightContextMenu;
        }

        protected override void Dispose(bool disposing)
        {
            _ni.Dispose();
            base.Dispose(disposing);
        }

        #endregion

        #region Handler

        private void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OpenLeftClickContextMenu();
            }
        }

        private void ClipboardNotification_ClipboardUpdate(object sender, EventArgs e)
        {
            _clipboardContextMenu.AddCurrentClipboard();
        }

        private void HotkeyNotification_HotkeyPressed(object sender, EventArgs e)
        {
            OpenLeftClickContextMenu();
        }

        private void Settings_SettingsChanged(object sender, EventArgs e)
        {
            HotkeyNotification.ResetHotkey();
            _clipboardContextMenu.UpdateLength(Settings.MaxEntries);
        }

        private void _clipboardContextMenu_ContentChanged(object sender, EventArgs e)
        {
            int itemCount = _clipboardContextMenu.Length;
            if (itemCount == 0)
            {
                _ni.Icon = Resources.clipboard_0_32;
            }
            else
            {
                int part = (int)Math.Round(Settings.MaxEntries/3.0);
                if (itemCount  <= part) _ni.Icon = Resources.clipboard_1_32;
                else if (itemCount <= 2*part) _ni.Icon = Resources.clipboard_2_32;
                else _ni.Icon = Resources.clipboard_3_32;
            }
        }

        private void _clipboardContextMenu_RequestReopen(object sender, EventArgs e)
        {
            Point cur = Cursor.Position;
            Cursor.Position = _menuMousePos;
            OpenLeftClickContextMenu();
            Cursor.Position = cur;
        }

        private void clearAll_Click(object sender, EventArgs e)
        {
            _clipboardContextMenu.ClearHistory();
        }

        private void settings_Click(object sender, EventArgs e)
        {
            if (_settingsForm == null || _settingsForm.IsDisposed)
            {
                _settingsForm = new SettingsForm();
                _settingsForm.Show();
            }
            else _settingsForm.BringToFront();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop) Application.Exit();
            else Environment.Exit(1);
        }

        #endregion
    }
}
