using RESTbookProvider;
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
    public partial class getByidFrm : Form
    {
        public getByidFrm()
        {
            InitializeComponent();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            TBresult.Text = Library.RESTgetByid(TBid.Text.Trim());
        }
    }
}
