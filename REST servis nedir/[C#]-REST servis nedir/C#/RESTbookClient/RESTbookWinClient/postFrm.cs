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
    public partial class postFrm : Form
    {
        public postFrm()
        {
            InitializeComponent();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            
            string bookName = TBBookName.Text;
            string pubYear = TBpubYear.Text;

            TBresult.Text = Library.RESTpost(bookName, pubYear);
        }
    }
}
