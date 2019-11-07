using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PictureBox.Image.Testes
{
    public partial class FormSize : Form
    {
        public int Size { get; set; }

        public FormSize()
        {
            InitializeComponent();
        }

        private void FormSize_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 150;i++ )
                comboBox1.Items.Add(i);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Size=Convert.ToInt32(comboBox1.SelectedItem);
            this.Close();
        }
    }
}
