using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace com.techphernalia.windows.forms.techieUI
{
    public partial class frmOutput : Form
    {
        public frmOutput()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public TextBox T { get { return txtOutput; } }
    }
}
