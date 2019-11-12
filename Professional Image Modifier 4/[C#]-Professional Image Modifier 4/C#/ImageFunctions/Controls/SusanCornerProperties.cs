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
	public partial class SusanCornerProperties : UserControl
	{

		public int GeometricalDefault { get; set; } // As set by Aforge
		public int DifferenceDefault { get; set; }  // As set by Aforge
		public int GeometricalThreshold { get; set; } // Set by User
		public int DifferenceThreshold { get; set; } // Set by User

		public SusanCornerProperties()
		{
			InitializeComponent();
			GeometricalDefault = 18;
			DifferenceDefault = 25;
			trackDifferenceThreshold.Value = DifferenceDefault;
			trackGeometricalThreshold.Value = GeometricalDefault;
		}


		private void trackGeometricalThreshold_ValueChanged(object sender, EventArgs e)
		{
			lblGeometricalThreshold.Text = trackGeometricalThreshold.Value.ToString();
			GeometricalThreshold = trackGeometricalThreshold.Value;
		}

		private void trackDifferenceThreshold_ValueChanged(object sender, EventArgs e)
		{
			lblDifferenceThreshold.Text = trackDifferenceThreshold.Value.ToString();
			DifferenceThreshold = trackDifferenceThreshold.Value;
		}

		public void SetDefaults()
		{
			trackDifferenceThreshold.Value = DifferenceDefault;
			trackGeometricalThreshold.Value = GeometricalDefault;
		}
	}
}
