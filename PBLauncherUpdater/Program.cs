using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Updater
{
    static class Program
    {   
     
        [STAThread]
        static void Main(string[] Args)
        {
            if (Args[0] == "0xxt")
            {
                Logger.Log("Closing PBLauncher.");
                Process[] processo = Process.GetProcessesByName("PBLauncher");
                foreach (Process process in processo)
                {
                    process.Kill();
                    process.WaitForExit();
                    Logger.Log("Updater started.");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frmUpdating());
                }
            }
        }
    }
}
