using ClipboardManager.Properties;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClipboardManager
{
    public partial class ClipboardManager : Form
    {
        public static readonly string APPLICATION_NAME = "Clipboard Manager";

        private NotifyIcon ni;
        private Settings settings;
        private ClipboardContextMenu leftContextMenu;
        private ContextMenuStrip rightContextMenu;
        private readonly Timer clipboardChangedTimer = new Timer();
        private bool pause = false;

        #region Init

        public ClipboardManager()
        {
            init();
            initNotifyIcon();
            initContextMenus();
            initClipboardHook();
        }

        private void init()
        {
            this.Name = "ClipboardManager";
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

            rightContextMenu = new ContextMenuStrip();
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

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_CLIPBOARDUPDATE = 0x031D;
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {

                case WM_CLIPBOARDUPDATE:
                    if (!pause && !clipboardChangedTimer.Enabled) clipboardChangedTimer.Start();
                    break;

                case WM_HOTKEY:

                    openLeftClickContextMenu();
                    break;


                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void openLeftClickContextMenu()
        {
            ni.ContextMenuStrip = leftContextMenu;
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(ni, null);
            ni.ContextMenuStrip = rightContextMenu;
        }

        #endregion

        #region Handler

        private void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                openLeftClickContextMenu();
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

        private void about_Click(object sender, EventArgs e)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string title = APPLICATION_NAME + " " + version.Major + "." + version.Minor;
            string text = "Copyright \u00A9 2015 Stefan Käsdorf";
            ni.ShowBalloonTip(5, title, text, ToolTipIcon.Info);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop) System.Windows.Forms.Application.Exit();
            else System.Environment.Exit(1);
        }

        #endregion

        #region User32.dll

        [DllImport("User32.dll")]
        private static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        #endregion
    }
}
