using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MSDN.Sample.XMLToExcelTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            MSDN.Sample.XMLToExcel.OpenXMLOffice objTest = new MSDN.Sample.XMLToExcel.OpenXMLOffice();
            objTest.XMLToExcel(txtFilePath.Text);
            MessageBox.Show("XML converted to Excel successfully.");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML Files|*.xml";
            openFileDialog1.Title = "Select a XML File";

            // Show the Dialog.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = openFileDialog1.FileName;
            }
        }
    }
}
