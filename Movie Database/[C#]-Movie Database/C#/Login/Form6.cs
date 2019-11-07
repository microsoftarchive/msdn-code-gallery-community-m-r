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
    public partial class Form6 : Form
    {
        Image img;
        SqlConnection cn = new SqlConnection("Data Source=saif-server;Initial Catalog=sabiha;Integrated Security=True");
        SqlCommand cmd;
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess.addEditImage(textBox1.Text.ToString(), Convert.ToInt32(textBox2.Text), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString(), textBox6.Text.ToString(), textBox7.Text.ToString(), textBox8.Text.ToString(), textBox9.Text.ToString(), textBox10.Text.ToString(), image.Image, textBox11.Text.ToString());
              //  cn.Open();
                //String s = "INSERT INTO tbl_movie (name, year, actor, actress, category, quality, sound, language, myopinion, director, image) VALUES ('" + textBox1.Text + "', " + textBox2.Text + ", '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "', '" + textBox9.Text + "', '" + textBox10.Text + "',)";
                //cmd = new SqlCommand(s, cn);
                //cmd.Parameters.AddWithValue(@IMG, image.Image);
                //cmd.ExecuteNonQuery();
                //cn.Close();
                MessageBox.Show("Inserted Successfully");
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

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.Filter = "Image Files|*.jpg;*.gif;*.bmp;*.png;*.jpeg";
            DialogResult result = fileChooser.ShowDialog();
            string fileName;
            if (result == DialogResult.Cancel)
                return;
            fileName = fileChooser.FileName;
            try
            {
                img = Image.FromFile(fileName);
                image.Image = img;
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }
    }
}
