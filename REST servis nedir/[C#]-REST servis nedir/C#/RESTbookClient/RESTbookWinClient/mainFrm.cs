using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RESTbookWinClient
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
        }

        private void btnGetALL_Click(object sender, EventArgs e)
        {
            getALLfrm frm = new getALLfrm();
            frm.ShowDialog();
        }

        private void btnGetByid_Click(object sender, EventArgs e)
        {
            getByidFrm frm = new getByidFrm();
            frm.ShowDialog();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            postFrm frm = new postFrm();
            frm.ShowDialog();
        }

        private void btnPut_Click(object sender, EventArgs e)
        {
            putFrm frm = new putFrm();
            frm.ShowDialog();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            delFrm frm = new delFrm();
            frm.ShowDialog();
        }

       
    }
}
