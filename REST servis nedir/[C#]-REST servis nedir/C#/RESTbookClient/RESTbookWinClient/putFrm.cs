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
    public partial class putFrm : Form
    {
        public putFrm()
        {
            InitializeComponent();
        }

        private void btnPut_Click(object sender, EventArgs e)
        {
            string id = TBid.Text.Trim();
            string bookName = TBBookName.Text;
            string pubYear = TBpubYear.Text;

            TBresult.Text = Library.RESTput(id, bookName, pubYear);
        }
    }
}
