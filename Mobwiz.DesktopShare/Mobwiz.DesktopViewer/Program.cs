using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Mobwiz.DesktopViewer
{
    static class Program
    {
        private static Mutex _singltonMutex;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var createNew = true;
            _singltonMutex = new Mutex(true, "Palmae-5FBA3B3E-A79C-444A-8F2F-D80AF4038DCA", out createNew);

            if (createNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("程序只能运行一个实例！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }
    }
}
