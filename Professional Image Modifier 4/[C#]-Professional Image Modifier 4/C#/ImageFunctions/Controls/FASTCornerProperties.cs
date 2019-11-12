using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageFunctions.Controls
{
	public partial class FASTCornerProperties : UserControl
	{
		public bool SupressDefault { get; set; } // As set by Aforge
		public int ThresholdDefault { get; set; }  // As set by Aforge
		public bool Supress { get; set; } // Set by User
		public int Threshold { get; set; } // Set by User

		/// <summary>
		/// The Defaults are not real defaults - you should investigate appropriate defaults for Threshold and Sigma
		/// </summary>
		public FASTCornerProperties()
		{
			InitializeComponent();
			ThresholdDefault = 40;
			SupressDefault = true;
			trackThreshold.Value = ThresholdDefault;
			cbSupress.Checked = SupressDefault;
		}

		public void SetDefaults()
		{
			trackThreshold.Value = ThresholdDefault;
			cbSupress.Checked = SupressDefault;
		}


		private void trackThreshold_ValueChanged(object sender, EventArgs e)
		{
			lblThreshold.Text = trackThreshold.Value.ToString();
			Threshold = trackThreshold.Value;
		}

		private void cbSupress_CheckStateChanged(object sender, EventArgs e)
		{
			Supress = cbSupress.Checked;
		}
	}
}
