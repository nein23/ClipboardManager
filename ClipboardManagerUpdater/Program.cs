using System;
using System.Windows.Forms;

namespace ClipboardManagerUpdater
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Updater(args[0], args[1], args[2]));
            }
        }
    }
}
