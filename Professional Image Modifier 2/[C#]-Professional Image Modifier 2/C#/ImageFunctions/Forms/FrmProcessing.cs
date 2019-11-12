using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageFunctions.Forms
{
	public partial class FrmProcessing : Form
	{
		public string description { get; set; }

		public FrmProcessing(string description)
		{
			InitializeComponent();
			lblDescription.Text = description;

		}


	}
}
