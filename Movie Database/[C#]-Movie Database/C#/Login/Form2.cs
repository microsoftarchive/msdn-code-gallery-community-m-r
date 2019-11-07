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
    public partial class Form2 : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=saif-server;Initial Catalog=sabiha;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Form6().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form5().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form4().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void dataBind()
        {
            dataGridView1.DataSource = DataAccess.getAllImages();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                dataBind();
                //cn.Open();
                //String s = "SELECT name, year, actor, actress, category, quality, sound, language, myopinion, director FROM tbl_movie";
                //da = new SqlDataAdapter(s, cn);
                //da.Fill(dt);
                //dataGridView1.DataSource = dt;
                //cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if(cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

    }
}
