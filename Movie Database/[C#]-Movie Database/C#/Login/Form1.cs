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
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=saif-server;Initial Catalog=sabiha;Integrated Security=True");
        SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                String id="";
                if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex==1)
                {
                    id = comboBox1.SelectedItem.ToString();
                    //MessageBox.Show(id);
                }
                String s = "SELECT * FROM tbl_login WHERE name='" + textBox1.Text + "' AND password='" + textBox2.Text + "' AND user_type='"+ id +"'";
                cmd = new SqlCommand(s, cn);
                if (cmd.ExecuteScalar() != null)
                {
                    MessageBox.Show("Successful");
                    if (id.Equals("admin"))
                    {
                        new Form2().Show();
                        this.Hide();
                    }
                    else if(id.Equals("normal"))
                    {
                        new Form3().Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("UnSuccessful");
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form7().Show();
            this.Hide();
        }
    }
}
