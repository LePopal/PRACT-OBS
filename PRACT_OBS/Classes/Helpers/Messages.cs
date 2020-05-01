using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PRACT_OBS.Classes.Helpers
{
    public static class Messages
    {
        public static DialogResult ErrorMessage(string Message)
        {
            return MessageBox.Show(Message, string.Format("{0} - Error!", Prefix), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult YesNoMessage(string Message)
        {
            return MessageBox.Show(Message, Prefix, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult WarningMessage(string Message)
        {
            return MessageBox.Show(Message, string.Format("{0} - Warning!", Prefix), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static string Prefix = Assembly.Title;
    }
}
