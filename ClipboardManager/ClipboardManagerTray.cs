using ClipboardManager.Properties;
using ClipboardManager.Util;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace ClipboardManager
{
    public partial class ClipboardManagerTray : Control
    {
        public static readonly string APPLICATION_NAME = "Clipboard Manager";

        public static ToastForm Toast;

        private NotifyIcon ni;
        private Settings settings;
        private ClipboardContextMenu leftContextMenu;
        private readonly Timer clipboardChangedTimer = new Timer();
        private bool pause = false;
        private ToolStripMenuItem pauseItem;

        #region Init

        public ClipboardManagerTray()
        {
            init();
            initNotifyIcon();
            initContextMenus();
            initClipboardHook();
            HandleUpdate(false);
        }

        private void init()
        {
            this.Name = APPLICATION_NAME;
            this.Visible = false;
            settings = new Settings();

        }

        private void initNotifyIcon()
        {
            ni = new NotifyIcon();
            ni.Text = APPLICATION_NAME;
            ni.Icon = Resources.clipboard_0_32;
            ni.MouseClick += new MouseEventHandler(ni_MouseClick);
            ni.Visible = true;
        }

        private void initContextMenus()
        {
            leftContextMenu = new ClipboardContextMenu(ni, settings);

            ContextMenuStrip rightContextMenu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = "Clear all";
            item.Image = Resources.clipboardEmpty;
            item.Click += clearAll_Click;
            rightContextMenu.Items.Add(item);
            pauseItem = new ToolStripMenuItem();
            pauseItem.Text = "Pause";
            pauseItem.Image = Resources.pause;
            pauseItem.Click += pause_Click;
            rightContextMenu.Items.Add(pauseItem);
            item = new ToolStripMenuItem();
            item.Text = "Settings";
            item.Image = Resources.settings;
            item.Click += settings_Click;
            rightContextMenu.Items.Add(item);
            item = new ToolStripMenuItem();
            item.Text = "Check for updates";
            item.Image = Resources.refresh;
            item.Click += update_Click;
            rightContextMenu.Items.Add(item);
            item = new ToolStripMenuItem();
            item.Text = "About";
            item.Image = Resources.info;
            item.Click += about_Click;
            rightContextMenu.Items.Add(item);
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Image = Resources.exit;
            item.Click += exit_Click;
            rightContextMenu.Items.Add(item);

            ni.ContextMenuStrip = rightContextMenu;
        }

        private void initClipboardHook()
        {
            clipboardChangedTimer.Interval = 100;
            clipboardChangedTimer.Tick += clipboardChangedTimer_Tick;
            this.CreateHandle();
        }

        #endregion

        #region Methods

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            ClipboardUtil.RemoveClipboardFormatListener(this.Handle);
            ClipboardUtil.AddClipboardFormatListener(this.Handle);

            HotkeyUtil.RegisterAllHotkeys(this.Handle, settings.Hotkeys);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ClipboardUtil.RemoveClipboardFormatListener(this.Handle);
            HotkeyUtil.UnregisterAllHotKeys(this.Handle, settings.Hotkeys.Count);
            base.OnHandleDestroyed(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (clipboardChangedTimer != null) clipboardChangedTimer.Stop();
            ni.Dispose();
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_CLIPBOARDUPDATE = 0x031D;
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {

                case WM_CLIPBOARDUPDATE:
                    if (!pause && !clipboardChangedTimer.Enabled) clipboardChangedTimer.Start();
                    break;

                case WM_HOTKEY:
                    switch (m.WParam.ToInt32())
                    {
                        case 0:
                            HandleOpenContextMenu();
                            break;
                        case 1:
                            HandlePause();
                            break;
                        case 2:
                            HandleClear();
                            break;
                    }
                    break;


                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region Handler

        private void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HandleOpenContextMenu();
            }
        }

        private void clipboardChangedTimer_Tick(object sender, EventArgs e)
        {
            clipboardChangedTimer.Stop();
            leftContextMenu.update();
        }

        private void clearAll_Click(object sender, EventArgs e)
        {
            HandleClear();
        }

        private void pause_Click(object sender, EventArgs e)
        {
            HandlePause();
        }

        private void settings_Click(object sender, EventArgs e)
        {
            Form settingsForm = Application.OpenForms["SettingsForm"];
            if (settingsForm == null)
            {
                new SettingsForm(this.Handle, leftContextMenu, settings).Show();
            }
            else settingsForm.Focus();
        }

        private void update_Click(object sender, EventArgs e)
        {
            HandleUpdate(true);
        }

        private void about_Click(object sender, EventArgs e)
        {
            if(Toast != null)
            {
                Toast.Close();
            }

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string title = APPLICATION_NAME + " " + version.Major + "." + version.Minor;
            string text = "Copyright \u00A9 2015 Stefan Käsdorf";

            Toast = new ToastForm(title, text);
            Toast.Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop) Application.Exit();
            else Environment.Exit(1);
        }

        private void HandleOpenContextMenu()
        {
            ContextUtil.openLeftClickContextMenu(ni, leftContextMenu);
        }

        private void HandlePause()
        {
            if (pauseItem != null)
            {
                if (pause)
                {
                    pause = false;
                    pauseItem.Text = "Pause";
                    pauseItem.Image = Resources.pause;
                }
                else
                {
                    pause = true;
                    pauseItem.Text = "Continue";
                    pauseItem.Image = Resources.play;
                }
            }
        }

        private void HandleClear()
        {
            bool p = !pause;
            if (p)  HandlePause();
            Clipboard.Clear();
            if (p) HandlePause();
            leftContextMenu.clearHistory();
        }

        private void HandleUpdate(bool showAll)
        {
            if (Toast != null)
            {
                Toast.Close();
            }
            Updater updater = new Updater();
            updater.checkForUpdate();
            if (showAll || updater.UpdateAvailable)
            {
                Toast = new ToastForm(updater);
                Toast.Show();
            }
        }

        #endregion
    }
}
