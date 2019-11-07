using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.techphernalia.windows.forms.techieUI
{
    public partial class frmException : Form
    {
        public frmException(Exception exc,String cause)
        {
            InitializeComponent();
            txtException.Text = "" + exc.Message + Environment.NewLine +Environment.NewLine+ "Stack Trace : " + Environment.NewLine + exc.StackTrace;
            txtCause.Text = cause;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
