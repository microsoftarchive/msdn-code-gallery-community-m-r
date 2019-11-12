using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Imaging;
using AForge;

namespace ImageFunctions.Modifications.CornerDetection
{
	class Harris
	{

		private string CurrentImage;

		public delegate void ImageCompleteHandler(List<IntPoint> Corners);
		public event ImageCompleteHandler ImageComplete;

		public Harris(string CurrentImage)
		{
			this.CurrentImage = CurrentImage;
		}

		public void GetCorners(int threshold, int sigma)
		{
			// create corners detector's instance
			HarrisCornersDetector hcd = new HarrisCornersDetector(HarrisCornerMeasure.Noble,threshold,sigma);
				
			// Apply the filter and return the points
			List<IntPoint> corners = hcd.ProcessImage(AForge.Imaging.Image.FromFile(this.CurrentImage));
			if (ImageComplete != null) ImageComplete(corners);
		}

	}
}
