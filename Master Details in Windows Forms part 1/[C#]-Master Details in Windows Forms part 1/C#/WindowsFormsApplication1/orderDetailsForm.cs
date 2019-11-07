using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class orderDetailsForm : Form
    {
        public orderDetailsForm()
        {
            InitializeComponent();
        }
        private DataTable currentOrderDetailsTable;
        public orderDetailsForm(DataTable dt)
        {
            InitializeComponent();
            currentOrderDetailsTable = dt;
            
        }

        private void orderDetailsForm_Load(object sender, EventArgs e)
        {
            OrderDetailsDataGridView.DataSource = currentOrderDetailsTable;
        }
    }
}
