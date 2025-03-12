using System;
using System.IO;
using System.Windows.Forms;

namespace PBLauncher
{
     
     
    public class Logger
    {
        public static void Log(string texto)
        {
            StreamWriter streamWriter = new StreamWriter(string.Concat(Application.StartupPath, "\\PBLauncher.log"), true);
            string[] strArrays = new string[] { "[", DateTime.Now.ToString("yyyy/MM/dd"), "|", DateTime.Now.ToString("HH:mm"), "] ", texto };
            if(texto == null)
            {
                streamWriter.WriteLine("");
            }
            else { streamWriter.WriteLine(string.Concat(strArrays)); }
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
