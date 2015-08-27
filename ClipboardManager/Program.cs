using System;
using System.Threading;
using System.Windows.Forms;

namespace ClipboardManager
{
    static class Program
    {
        private static Mutex mutex = new Mutex(true, "3d0c2cac-9a6a-4608-846f-04c94f001260");

        [STAThread]
        private static void Main(string[] args)
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                using (ClipboardManager cm = new ClipboardManager())
                {
                    Application.Run();
                }
                mutex.ReleaseMutex();
            }
        }
    }
}
