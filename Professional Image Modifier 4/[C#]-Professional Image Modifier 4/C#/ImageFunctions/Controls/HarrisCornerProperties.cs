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
	public partial class HarrisCornerProperties : UserControl
	{

		public int SigmaDefault { get; set; } // As set by Aforge
		public int ThresholdDefault { get; set; }  // As set by Aforge
		public int Sigma { get; set; } // Set by User
		public int Threshold { get; set; } // Set by User

		/// <summary>
		/// The Defaults are not real defaults - you should investigate appropriate defaults for Threshold and Sigma
		/// </summary>
		public HarrisCornerProperties()
		{
			InitializeComponent();
			ThresholdDefault = 5;
			SigmaDefault = 3;
			trackThreshold.Value = ThresholdDefault;
			trackSigma.Value = SigmaDefault;
		}

		public void SetDefaults()
		{
			trackThreshold.Value = ThresholdDefault;
			trackSigma.Value = SigmaDefault;
		}


		private void trackThreshold_ValueChanged_1(object sender, EventArgs e)
		{
			lblThreshold.Text = trackThreshold.Value.ToString();
			Threshold = trackThreshold.Value;
		}

		private void trackSigma_ValueChanged(object sender, EventArgs e)
		{
			lblWindow.Text = trackSigma.Value.ToString();
			Sigma = trackSigma.Value;
		}
	}
}
