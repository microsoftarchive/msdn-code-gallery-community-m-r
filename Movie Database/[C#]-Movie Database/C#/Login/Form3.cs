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
using System.IO;

namespace Login
{
    public partial class Form3 : Form
    {
        SqlConnection cn = new SqlConnection("Data Source=saif-server;Initial Catalog=sabiha;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da, da1, da2;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        String year;

        public Form3()
        {
            InitializeComponent();
        }
        private void dataBind()
        {
            dataGridView1.DataSource = DataAccess.getImage(textBox1.Text.ToString(), Convert.ToInt32(textBox2.Text.ToString()));
        }
        private void dataBind1()
        {
            dataGridView1.DataSource = DataAccess.getImage1(Convert.ToInt32(year));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //cn.Open();
                //String s = "SELECT name, year, actor, actress, category, quality, sound, language, myopinion, director FROM tbl_movie WHERE name='" + textBox1.Text + "' AND year=" + textBox2.Text;
                //cmd = new SqlCommand(s, cn);
                //da = new SqlDataAdapter(s,cn);
                //dt.Clear();
                //da.Fill(dt);
                dataBind();
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

        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                String s = "SELECT * FROM tbl_movie";
                cmd = new SqlCommand(s, cn);
                da2 = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da2.Fill(ds);
                DataTable table = ds.Tables[0];
                for (int i = 0; i < table.Rows.Count; ++i)
                {
                    comboBox1.Items.Add(table.Rows[i][1].ToString());
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                year = "";
                if (comboBox1.SelectedIndex >= 0)
                {
                    year = comboBox1.SelectedItem.ToString();
                    //MessageBox.Show(id);
                }
                dataBind1();
                //String s = "SELECT name, year, actor, actress, category, quality, sound, language, myopinion, director FROM tbl_movie WHERE year=" + year;
                //da1 = new SqlDataAdapter(s, cn);
                //dt1.Clear();
                //da1.Fill(dt1);
                //dataGridView1.DataSource = dt1;
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    movietextbox.Text = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
                    yeartextbox.Text = dataGridView1.SelectedRows[0].Cells["year"].Value.ToString();
                    actortextbox.Text = dataGridView1.SelectedRows[0].Cells["actor"].Value.ToString();
                    actresstextbox.Text = dataGridView1.SelectedRows[0].Cells["actress"].Value.ToString();
                    categorytextbox.Text = dataGridView1.SelectedRows[0].Cells["category"].Value.ToString();
                    qualitytextbox.Text = dataGridView1.SelectedRows[0].Cells["quality"].Value.ToString();
                    soundtextbox.Text = dataGridView1.SelectedRows[0].Cells["sound"].Value.ToString();
                    languagetextbox.Text = dataGridView1.SelectedRows[0].Cells["language"].Value.ToString();
                    opiniontextbox.Text = dataGridView1.SelectedRows[0].Cells["myopinion"].Value.ToString();
                    directortextbox.Text = dataGridView1.SelectedRows[0].Cells["director"].Value.ToString();
                    linktextbox.Text = dataGridView1.SelectedRows[0].Cells["link"].Value.ToString(); 
                    
                    DataTable DT = DataAccess.getImage(dataGridView1.SelectedRows[0].Cells["name"].Value.ToString(), (int)dataGridView1.SelectedRows[0].Cells["year"].Value);
                    pictureBox1.Image = null;
                    Image img = DataAccess.ByteToImage((Byte[])DT.Rows[0]["image"]);
                    pictureBox1.Image = img;
                }
            }
            catch { }
        }
    }
}
