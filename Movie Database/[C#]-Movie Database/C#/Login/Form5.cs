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
    public partial class Form5 : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=saif-server;Initial Catalog=sabiha;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da, da1;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        public Form5()
        {
            InitializeComponent();
        }
        private void dataBind()
        {
            dataGridView1.DataSource = DataAccess.getAllImages();
        }
        private void Form5_Load(object sender, EventArgs e)
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
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        /*private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                nameTextBox.Text = row.Cells["name"].Value.ToString();
                yearTextBox.Text = row.Cells["year"].Value.ToString();
            }
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                String s = "DELETE FROM tbl_movie WHERE name='" + nameTextBox.Text + "' AND year = " + yearTextBox.Text;
                cmd = new SqlCommand(s, cn);
                cmd.ExecuteNonQuery();
                dataBind();
                //String s1 = "SELECT name, year, actor, actress, category, quality, sound, language, myopinion, director FROM tbl_movie";
                //da1 = new SqlDataAdapter(s1, cn);
                //da1.Fill(dt1);
                //dataGridView1.DataSource = dt1;
                //cn.Close();
                MessageBox.Show("Deleted Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {

                    nameTextBox.Text = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
                    yearTextBox.Text = dataGridView1.SelectedRows[0].Cells["year"].Value.ToString();
                }
            }
            catch { }
        }
    }
}
