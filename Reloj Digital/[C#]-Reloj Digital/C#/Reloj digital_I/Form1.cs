using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reloj_digital_I
	{
	public partial class Form1 : Form
		{
		public Form1()
			{
			InitializeComponent();
			}

		private void Form1_Load(object sender, EventArgs e)
			{

			}

		private void timer1_Tick(object sender, EventArgs e)
			{
			label1.Text = DateTime.Now.ToLongTimeString();
			label2.Text = DateTime.Now.ToShortDateString();
			label3.Text = DateTime.Now.ToString("dddd");
			}
		}
	}
//https://msdn.microsoft.com/es-es/library/zdtaw1bw(v=vs.110).aspx