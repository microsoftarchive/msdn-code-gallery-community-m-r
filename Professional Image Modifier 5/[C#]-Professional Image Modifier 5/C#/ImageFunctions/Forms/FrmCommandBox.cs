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
    public partial class FrmCommandBox : DockContent
    {
        public FrmCommandBox()
        {
            InitializeComponent();
        }


        protected override string GetPersistString()
        {
            return this.Text;
        }

        /// <summary>
        /// This produced the flash required to attract attention to this being an active input box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFlash_Tick_1(object sender, EventArgs e)
        {
            if (lblFlash.Text == ":")
                lblFlash.Text = " ";
            else
                lblFlash.Text = ":";

           
        }
    }
}
