using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Login
{
    public partial class Form7 : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=saif-server;Initial Catalog=sabiha;Integrated Security=True");
        SqlCommand cmd;
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                String id = "";
                if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 1)
                {
                    id = comboBox1.SelectedItem.ToString();
                    //MessageBox.Show(id);
                }
                String s = "INSERT INTO tbl_login (name, password, user_type) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + id + "')";
                cmd = new SqlCommand(s, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
                new Form1().Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
