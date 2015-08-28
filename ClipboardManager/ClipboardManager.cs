using ClipboardManager.Properties;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClipboardManager
{
    public partial class ClipboardManager : Control
    {
        public static readonly string APPLICATION_NAME = "Clipboard Manager";
        public static readonly string VERSION_URL = "https://raw.githubusercontent.com/nein23/ClipboardManager/master/version";

        private NotifyIcon ni;
        private Settings settings;
        private ClipboardContextMenu leftContextMenu;
        private readonly Timer clipboardChangedTimer = new Timer();
        private bool pause = false;
        private ToastForm toast;

        #region Init

        public ClipboardManager()
        {
            init();
            initNotifyIcon();
            initContextMenus();
            initClipboardHook();
            checkForUpdate();
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
            item.Text = "Clear Clipboard";
            item.Image = Resources.clipboardEmpty;
            item.Click += clearClipboard_Click;
            rightContextMenu.Items.Add(item);
            item = new ToolStripMenuItem();
            item.Text = "Clear History";
            item.Image = Resources.clipboardFull;
            item.Click += clearHistory_Click;
            rightContextMenu.Items.Add(item);
            item = new ToolStripMenuItem();
            item.Text = "Pause";
            item.Image = Resources.pause;
            item.Click += pause_Click;
            rightContextMenu.Items.Add(item);
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

            RemoveClipboardFormatListener(this.Handle);
            Util.UnregisterHotKey(this.Handle, 0);

            AddClipboardFormatListener(this.Handle);
            if (settings != null && (settings.HK_Mod != 0 || settings.HK_Key != 0))
            {
                Util.RegisterHotKey(this.Handle, 0, settings.HK_Mod, settings.HK_Key);
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            RemoveClipboardFormatListener(this.Handle);
            Util.UnregisterHotKey(this.Handle, 0);
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
                    Util.openLeftClickContextMenu(ni, leftContextMenu);
                    break;


                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void checkForUpdate()
        {
            Tuple<string, string, bool> res = Util.checkForUpdate();
            if (res.Item3)
            {
                if (toast != null)
                {
                    toast.Close();
                }
                toast = new ToastForm(res.Item1, res.Item2);
                toast.Show();
            }
        }

        #endregion

        #region Handler

        private void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Util.openLeftClickContextMenu(ni, leftContextMenu);
            }
        }

        private void clipboardChangedTimer_Tick(object sender, EventArgs e)
        {
            clipboardChangedTimer.Stop();
            leftContextMenu.update();
        }

        private void clearClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }

        private void clearHistory_Click(object sender, EventArgs e)
        {
            leftContextMenu.clearHistory(0);
        }

        private void pause_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = (ToolStripMenuItem) sender;
                if (pause)
                {
                    pause = false;
                    item.Text = "Pause";
                    item.Image = Resources.pause;
                }
                else
                {
                    pause = true;
                    item.Text = "Continue";
                    item.Image = Resources.play;
                }
            }
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
            if (toast != null)
            {
                toast.Close();
            }
            Tuple<string, string, bool> res = Util.checkForUpdate();
            toast = new ToastForm(res.Item1, res.Item2);
            toast.Show();
        }

        private void about_Click(object sender, EventArgs e)
        {
            if(toast != null)
            {
                toast.Close();
            }

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string title = APPLICATION_NAME + " " + version.Major + "." + version.Minor;
            string text = "Copyright \u00A9 2015 Stefan Käsdorf";

            toast = new ToastForm(title, text);
            toast.Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop) Application.Exit();
            else Environment.Exit(1);
        }

        #endregion

        #region User32.dll
        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        #endregion
    }
}
