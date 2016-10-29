using ClipboardManager.Properties;
using System;
using System.Windows.Forms;

namespace ClipboardManager
{
    internal class HotkeyNotification
    {
        public static event EventHandler HotkeyPressed;

        private static readonly NotificationForm Form = new NotificationForm();


        private static void OnHotkeyPressed()
        {
            HotkeyPressed?.Invoke(null, EventArgs.Empty);
        }

        public static void RegisterHotkey()
        {
            if (Settings.HkMod != 0 && Settings.HkKey != 0)
            {
                NativeMethods.RegisterHotKey(Form.Handle, 0, Settings.HkMod, Settings.HkKey);
            }
        }

        public static void ResetHotkey()
        {
            NativeMethods.UnregisterHotKey(Form.Handle, 0);
            RegisterHotkey();
        }

        private class NotificationForm : Form
        {

            protected override void Dispose(bool disposing)
            {
                NativeMethods.UnregisterHotKey(Handle, 0);
                base.Dispose(disposing);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == NativeMethods.WmHotkey)
                {
                    OnHotkeyPressed();
                }
                base.WndProc(ref m);
            }
        }
    }
}
