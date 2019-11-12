using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MongoStart
{
    public partial class frmMongoSetup : Form
    {
        public frmMongoSetup()
        {
            InitializeComponent();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            if (txtMongoHost.Text.Trim().Length == 0 || txtPort.Text.Trim().Length == 0 || txtDatabase.Text.Trim().Length == 0) return;
            Program.MongoData = new MongoData { Host = txtMongoHost.Text.Trim(), Port = Convert.ToInt32( txtPort.Text.Trim()), Database = txtDatabase.Text.Trim() };
            this.Close();
        }
    }
}
