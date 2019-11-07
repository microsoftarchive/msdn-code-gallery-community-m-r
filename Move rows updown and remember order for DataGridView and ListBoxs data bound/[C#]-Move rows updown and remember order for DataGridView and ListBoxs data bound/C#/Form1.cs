using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SimpleMsAccess
{
    public partial class Form1 : Form
    {
        private BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.KeyDown +=textBox1_KeyDown;
            DataGridView1.KeyDown +=DataGridView1_KeyDown;
            DataTable dt = new DataTable();
            
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
            Builder.DataSource = Path.Combine(Application.StartupPath, "Database1.accdb");

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = Builder.ToString() })
            {
                using (OleDbCommand cmd = new OleDbCommand { CommandText = "SELECT Identifier, UserName, UserRole FROM Users", Connection = cn })
                {
                    cn.Open();
                    OleDbDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
            }

            bs.DataSource = dt;
            DataGridView1.DataSource = bs;
            ActiveControl = textBox1;
        }
        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            ActiveControl = textBox1;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                bs.MovePrevious();
            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                bs.MoveNext();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}