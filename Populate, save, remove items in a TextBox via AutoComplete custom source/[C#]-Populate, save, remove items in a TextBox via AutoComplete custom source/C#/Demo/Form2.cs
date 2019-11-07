using System;
using System.Windows.Forms;

namespace AutoComplete_C_Version
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            ComboBox1.DisplayMember = "FamilyName";
            ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ComboBox1.DataSource = da.FamilyNames();
            ComboBox1.AutoCompleteCustomSource = da.AvailableFonts();

            dataGridView1.DataSource = da.FamilyNames();
            dataGridView1.ExpandColumns();
        }
    }
}