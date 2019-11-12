using System.Collections.Generic;
using System.Windows.Forms;

using AForge;
using AForge.Imaging;
using ImageFunctions.Forms;

namespace ImageFunctions.Controls
{
	public partial class MoravecCornerProperties : UserControl
	{

		public int WindowDefault { get; set; } // As set by Aforge
		public int ThresholdDefault { get; set; }  // As set by Aforge
		public int Window { get; set; } // Set by User
		public int Threshold { get; set; } // Set by User

		public MoravecCornerProperties()
		{
			InitializeComponent();
			ThresholdDefault = 500;
			WindowDefault = 3;
			trackThreshold.Value = ThresholdDefault;
			trackWindow.Value = WindowDefault;
		}

		public void SetDefaults()
		{
			trackThreshold.Value = ThresholdDefault;
			trackWindow.Value = WindowDefault;
		}

		private void trackThreshold_ValueChanged(object sender, System.EventArgs e)
		{
			lblThreshold.Text = trackThreshold.Value.ToString();
			Threshold = trackThreshold.Value;
		}

		private void trackWindow_ValueChanged(object sender, System.EventArgs e)
		{
			lblWindow.Text = trackWindow.Value.ToString();
			Window = trackWindow.Value;
		}
	}
}
