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

        private NotifyIcon ni;
        private Settings settings;
        private ClipboardContextMenu leftContextMenu;
        private ContextMenuStrip rightContextMenu;
        private IntPtr nextClipboardViewer;
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
            item.Checked = false;
            item.CheckOnClick = true;
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
            nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
        }

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            ChangeClipboardChain(this.Handle, nextClipboardViewer);
            if (clipboardChangedTimer != null) clipboardChangedTimer.Stop();
            ni.Dispose();
            base.Dispose(disposing);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    if (!pause && !clipboardChangedTimer.Enabled) clipboardChangedTimer.Start();
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
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
                ni.ContextMenuStrip = leftContextMenu;
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(ni, null);
                ni.ContextMenuStrip = rightContextMenu;
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
            if (sender is ToolStripMenuItem) pause = (sender as ToolStripMenuItem).Checked;
        }

        private void settings_Click(object sender, EventArgs e)
        {
            Form settingsForm = Application.OpenForms["SettingsForm"];
            if (settingsForm == null)
            {
                new SettingsForm(leftContextMenu, settings).Show();
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

        #endregion
    }
}
