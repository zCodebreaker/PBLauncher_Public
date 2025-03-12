using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace PBLauncher
{
     
     
    static class Program
    {
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Process aProcess = Process.GetCurrentProcess();
            string aProcName = aProcess.ProcessName;
            if (Process.GetProcessesByName(aProcName).Length > 1)
            {
                MessageBox.Show("Não é permitido abrir dois programas ao mesmo tempo.", "PBLauncher", MessageBoxButtons.OK);
                return;
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Connection());
            }
        }
        public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Log("[#] Não foi possível conectar ao banco de dados [0x0000083]");
            MessageBox.Show("Não foi possível conectar ao banco de dados.", "PBLauncher", MessageBoxButtons.OK);
            Application.Exit();
        }
    }
}
