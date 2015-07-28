using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ClipboardManager
{
    static class Program
    {
        private static Mutex mutex = new Mutex(true, Assembly.GetExecutingAssembly().GetType().GUID.ToString());

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
