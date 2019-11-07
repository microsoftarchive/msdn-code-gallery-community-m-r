using System;
using System.Windows.Forms;

namespace AutoComplete_C_Version
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            txtFirstName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtFirstName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtFirstName.AutoCompleteCustomSource = da.LoadFemaleNames();
            ComboBox1.DisplayMember = "FirstName";
            ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ComboBox1.DataSource = da.LoadFemaleNames();
            ComboBox1.AutoCompleteCustomSource = da.LoadFemaleNames();
            dataGridView1.DataSource = da.AllFemaleNames();
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}