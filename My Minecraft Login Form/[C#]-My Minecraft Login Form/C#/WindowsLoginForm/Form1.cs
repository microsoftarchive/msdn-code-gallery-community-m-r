using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsLoginForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_username.Focus();
            txt_password.Focus();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void login()
        {
            if (txt_username.Text.Length == 0 && txt_password.Text.Length == 0)
                MessageBox.Show("Please enter a valid username and password.");
            else
                if (this.txt_username.Text.Length == 0)
                MessageBox.Show("Please enter a valid username.");
            else
                    if (this.txt_password.Text.Length == 0)
                MessageBox.Show("Please enter a valid password.");
            else
                if (this.txt_username.Text == "Le Quang Dat" && this.txt_password.Text == "Hacker Windows 123")
                MessageBox.Show("Welcome, Dat. Connecting to 25.47.225.476:25565...");
            else
                MessageBox.Show("Please type in the correct administrator's username and password.");


        }
        private void btn_login_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            if (this.txt_username.Text == "Le Quang Dat" && this.txt_password.Text == "Hacker Windows 123")
            {
                fm.Show();
            }
            login();
        }
    }
}
