using System;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-22

namespace DUPALPayroll.General
{
    public class TcMessageBox
    {
        public static void ShowAndLogUnexpectedError(Exception ex)
        {
            TcMessageBox.ShowAndLogUnexpectedError(null, ex);
        }

        public static void ShowAndLogUnexpectedError(string error, Exception ex)
        {
            string message = ex.Message;

            if (!string.IsNullOrEmpty(error))
            {
                message = string.Format("{0}\n\n{1}", error, ex.Message);
            }
            
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            string logMessage = string.Format("{0}\n\n{1}", message, ex.StackTrace);
            TcLog.LogUnexpectedError(logMessage);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarning(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowYesNoQuestion(string message)
        {
            return MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult ShowYesNoWarning(string message)
        {
            return MessageBox.Show(message, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        public static void ShowInformation(string message)
        {
            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
