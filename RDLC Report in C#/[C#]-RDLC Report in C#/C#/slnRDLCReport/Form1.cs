using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace slnRDLCReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.tblCityMaster' table. You can move, or remove it, as needed.
            this.tblCityMasterTableAdapter.Fill(this.DataSet1.tblCityMaster);

            this.reportViewer1.RefreshReport();
        }
    }
}
