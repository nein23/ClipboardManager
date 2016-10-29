using System;
using System.Windows.Forms;

namespace ClipboardManager
{
    internal class ClipboardNotification
    {
        public static event EventHandler ClipboardUpdate;

        private static readonly NotificationForm Form = new NotificationForm();


        private static void OnClipboardUpdate()
        {
            ClipboardUpdate?.Invoke(null, EventArgs.Empty);
        }

        private class NotificationForm : Form
        {
            private readonly Timer _clipboardChangedTimer;

            public NotificationForm()
            {
                _clipboardChangedTimer = new Timer {Interval = 100};
                _clipboardChangedTimer.Tick += _clipboardChangedTimer_Tick;
                NativeMethods.SetParent(Handle, NativeMethods.HwndMessage);
                NativeMethods.AddClipboardFormatListener(Handle);
            }

            private void _clipboardChangedTimer_Tick(object sender, EventArgs e)
            {
                _clipboardChangedTimer.Stop();
                OnClipboardUpdate();
            }

            protected override void Dispose(bool disposing)
            {
                _clipboardChangedTimer?.Stop();
                NativeMethods.RemoveClipboardFormatListener(Handle);
                base.Dispose(disposing);
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == NativeMethods.WmClipboardupdate && !_clipboardChangedTimer.Enabled)
                {
                    _clipboardChangedTimer.Start();
                }
                base.WndProc(ref m);
            }
        }
    }
}
