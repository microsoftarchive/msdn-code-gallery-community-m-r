using DataGridViewHelpers;
using QueryHelper;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
        private BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dtUsers = new DataTable();
            DataTable dtMonths = new DataTable();
            Builder.Provider = "Microsoft.ACE.OLEDB.12.0";

            Builder.DataSource = Path.Combine(Application.StartupPath, "Database1.accdb");
            using (OleDbConnection cn = new OleDbConnection { ConnectionString = Builder.ToString() })
            {
                using (OleDbCommand cmd = new OleDbCommand
                {
                    CommandText = "SELECT Identifier, CompanyName, ContactName FROM Customers WHERE Country=@Country",
                    Connection = cn
                })
                {
                    cmd.Parameters.Add(new OleDbParameter { ParameterName = "@Country", DbType = DbType.String, Value = "USA" });

                    textBox1.Text = cmd.CommandText;
                    textBox2.Text = cmd.ActualCommandText();

                    cn.Open();
                    OleDbDataReader dr = cmd.ExecuteReader();

                    dtUsers.Load(dr);
                    dtUsers.Columns["Identifier"].ColumnMapping = MappingType.Hidden;

                    bs.DataSource = dtUsers;
                    dataGridView1.DataSource = bs;

                    dr.Close();
                }
            }

            dataGridView1.ExpandColumns();
            ActiveControl = dataGridView1;
        }
    }
}