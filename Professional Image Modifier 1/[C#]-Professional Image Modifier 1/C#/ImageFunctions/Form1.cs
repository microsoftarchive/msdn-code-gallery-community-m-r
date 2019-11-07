using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageFunctions
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			
		}




		private void btnLoadNewImage_Click(object sender, EventArgs e)
		{
			DialogResult dr = openFile.ShowDialog();
			if (dr == System.Windows.Forms.DialogResult.OK)
			{
				foreach (string file in openFile.FileNames)
				{
					listBox1.Items.Add(file);
					pbImage.Image = Image.FromFile(file);
				}
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex > ilDefault.Images.Count - 1) // Must be added by the user
			{
				pbImage.Image = Image.FromFile(listBox1.SelectedItem.ToString());

			}
			else // Must be the default images in the ImageList
			{
				pbImage.Image = ilDefault.Images[listBox1.SelectedIndex];

			}
		}

		
	}
}
