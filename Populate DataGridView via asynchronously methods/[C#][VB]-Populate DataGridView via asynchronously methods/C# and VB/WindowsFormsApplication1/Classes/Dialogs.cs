using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public static class Dialogs
    {
        private static string _LineBreakChar = "~";
        public static string LineBreakChar
        {
            get
            {
                return _LineBreakChar;
            }
            set
            {
                _LineBreakChar = value;
            }
        }
        private static string ApplicationTitle()
        {
            string title = "";
            object[] titleAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
            if (titleAttributes.Length > 0 && titleAttributes[0] is AssemblyTitleAttribute)
            {
                title = (titleAttributes[0] as AssemblyTitleAttribute).Title;
                
            }

            return title;
        }
        /// <summary>
        /// Get line break char
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private static string CreateLineBreaks(string Text)
        {
            if (string.IsNullOrWhiteSpace(LineBreakChar))
            {
                return Text;
            }
            else
            {
                return Text.Replace(LineBreakChar, Environment.NewLine);
            }
        }
        /// <summary>
        /// Present a dialog asking a question with the default button No.
        /// </summary>
        /// <param name="Text">Question to ask</param>
        /// <returns></returns>
        /// <remarks>
        /// A tilde character will split the text into multiple lines
        /// </remarks>
        public static bool Question(string Text)
        {
            return (MessageBox.Show(CreateLineBreaks(Text), ApplicationTitle(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }

    }
}
