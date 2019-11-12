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
    public partial class Form4 : Form
    {
        String updateName, updateYear;
        SqlConnection cn = new SqlConnection("Data Source=saif-server;Initial Catalog=sabiha;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da, da1;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        public Form4()
        {
            InitializeComponent();
        }

        private void dataBind()
        {
            dataGridView1.DataSource = DataAccess.getAllImages();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            try
            {
                dataBind();
                /*cn.Open();
                String s = "SELECT name, year, actor, actress, category, quality, sound, language, myopinion, director FROM tbl_movie";
                da = new SqlDataAdapter(s, cn);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                cn.Close();
                 */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

       /* private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                
                updateName = row.Cells["name"].Value.ToString();
                updateYear = row.Cells["year"].Value.ToString();

                nameTextBox.Text = row.Cells["name"].Value.ToString();
                yearTextBox.Text = row.Cells["year"].Value.ToString();
                actorTextBox.Text = row.Cells["actor"].Value.ToString();
                actressTextBox.Text = row.Cells["actress"].Value.ToString();
                categoryTextBox.Text = row.Cells["category"].Value.ToString();
                qualityTextBox.Text = row.Cells["quality"].Value.ToString();
                soundTextBox.Text = row.Cells["sound"].Value.ToString();
                languageTextBox.Text = row.Cells["language"].Value.ToString();
                opinionTextBox.Text = row.Cells["myopinion"].Value.ToString();
                directorTextBox.Text = row.Cells["director"].Value.ToString();
            }
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess.updateTableMovie(nameTextBox.Text.ToString(), Convert.ToInt32(yearTextBox.Text), actorTextBox.Text.ToString(), actressTextBox.Text.ToString(), categoryTextBox.Text.ToString(), qualityTextBox.Text.ToString(), soundTextBox.Text.ToString(), languageTextBox.Text.ToString(), opinionTextBox.Text.ToString(), directorTextBox.Text.ToString(), image.Image, linkTextBox.Text.ToString(), updateName, Convert.ToInt32(updateYear));
                dataBind();
                /*cn.Open();
                String s = "UPDATE tbl_movie SET name='" + nameTextBox.Text + "', year=" + yearTextBox.Text + ", actor='" + actorTextBox.Text + "', actress='" + actressTextBox.Text + "', category='" + categoryTextBox.Text + "', quality='" + qualityTextBox.Text + "', sound='" + soundTextBox.Text + "', language='" + languageTextBox.Text + "', myopinion='" + opinionTextBox.Text + "', director='" + directorTextBox.Text + "' WHERE name='" + updateName + "' AND year=" + updateYear;
                cmd = new SqlCommand(s, cn);
                cmd.ExecuteNonQuery();

                String s1 = "SELECT name, year, actor, actress, category, quality, sound, language, myopinion, director FROM tbl_movie";
                da1 = new SqlDataAdapter(s1, cn);
                da1.Fill(dt1);
                dataGridView1.DataSource = dt1;

                cn.Close();
                 */
                MessageBox.Show("Updated Successfully");
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    updateName = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
                    updateYear = dataGridView1.SelectedRows[0].Cells["year"].Value.ToString();

                    nameTextBox.Text = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
                    yearTextBox.Text = dataGridView1.SelectedRows[0].Cells["year"].Value.ToString();
                    actorTextBox.Text = dataGridView1.SelectedRows[0].Cells["actor"].Value.ToString();
                    actressTextBox.Text = dataGridView1.SelectedRows[0].Cells["actress"].Value.ToString();
                    categoryTextBox.Text = dataGridView1.SelectedRows[0].Cells["category"].Value.ToString();
                    qualityTextBox.Text = dataGridView1.SelectedRows[0].Cells["quality"].Value.ToString();
                    soundTextBox.Text = dataGridView1.SelectedRows[0].Cells["sound"].Value.ToString();
                    languageTextBox.Text = dataGridView1.SelectedRows[0].Cells["language"].Value.ToString();
                    opinionTextBox.Text = dataGridView1.SelectedRows[0].Cells["myopinion"].Value.ToString();
                    directorTextBox.Text = dataGridView1.SelectedRows[0].Cells["director"].Value.ToString();
                    linkTextBox.Text = dataGridView1.SelectedRows[0].Cells["link"].Value.ToString();
                    
                    DataTable DT = DataAccess.getImage(dataGridView1.SelectedRows[0].Cells["name"].Value.ToString(), (int)dataGridView1.SelectedRows[0].Cells["year"].Value);
                    image.Image = null;
                    Image img = DataAccess.ByteToImage((Byte[])DT.Rows[0]["image"]);
                    image.Image = img;
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image img;
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
