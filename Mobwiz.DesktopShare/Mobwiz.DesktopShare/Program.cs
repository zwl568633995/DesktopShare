using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Mobwiz.DesktopShare
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

            // 用于检测单一进程
            var createNew = true;
            _singltonMutex = new Mutex(true, "Palmae-27E85693-5004-4DE4-91F4-2783F17984D2", out createNew);

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
