using System;
using System.IO;
using System.Windows.Forms;

namespace Updater
{
    public class Logger
    {   
     
        public static void Log(string texto)
        {
            string str = string.Concat(Application.StartupPath, "\\PBLauncher.log");
            DateTime now = DateTime.Now;
            StreamWriter streamWriter = new StreamWriter(str, true);
            string[] strArrays = new string[] { "[", now.ToString("yyyy/MM/dd"), "|", now.ToString("HH:mm"), "] ", texto };
            streamWriter.WriteLine(string.Concat(strArrays));
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
