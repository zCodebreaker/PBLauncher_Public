using System;
using System.Windows.Forms;

namespace PBLauncher
{
     
     
    public class Messages
    {
        public static void InfoMessage(string Message, string title)
        {
            MessageBox.Show(Message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ErrorMessage(string Message, string title)
        {
            MessageBox.Show(Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void WarningMessage(string Message, string title)
        {
            MessageBox.Show(Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void QuestionMessage(string Message, string title)
        {
            MessageBox.Show(Message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static void ExceptionMessage(Exception Message)
        {
            MessageBox.Show(Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
