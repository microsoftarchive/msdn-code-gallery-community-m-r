using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WeifenLuo.WinFormsUI.Docking;

namespace ImageFunctions.Forms
{
    public partial class FrmConsole : DockContent
    {
        private static int MessageCounter = 1;
        private static bool IsIdle = false;
        private delegate void LogDelegate(string message);

        public int CountDown { get; set; }

        public FrmConsole()
        {
            InitializeComponent();
            SetCountdown();
        }

        private void SetCountdown()
        {
            CountDown = 5 * 60000; // x Minutes
        }

        protected override string GetPersistString()
        {
            return this.Text;
        }

        /// <summary>
        /// Print out the message
        /// \t = insert Tab spacing
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            if (rtbConOut.InvokeRequired)
            {
                LogDelegate d = new LogDelegate(Log);
                this.Invoke(d, new object[] { message });
            }
            else
            {
                rtbConOut.AppendText("[" + MessageCounter + "]\t " + DateTime.Now.ToString() + "\t" + message + Environment.NewLine);
                rtbConOut.Focus();
               
                MessageCounter++;
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invalidate(); // Should redraw the whole window correcting any display errors
            this.Refresh();
        }

        private void timerIdle_Tick(object sender, EventArgs e)
        {
            if (IsIdle)
            {
                CountDown--;
            }

            if (CountDown == 0)
            {
                rtbConOut.Focus();
                SetCountdown();
            }

            IsIdle = true;
        }

        private void rtbConOut_TextChanged(object sender, EventArgs e)
        {
            IsIdle = false;
         
        }

        private void rtbConOut_MouseMove(object sender, MouseEventArgs e)
        {
            IsIdle = false;
          
        }

        


    }
}
