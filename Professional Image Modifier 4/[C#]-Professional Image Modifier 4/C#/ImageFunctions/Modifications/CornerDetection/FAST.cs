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
	class FAST
	{

		private string CurrentImage;

		public delegate void ImageCompleteHandler(List<IntPoint> Corners);
		public event ImageCompleteHandler ImageComplete;

		public FAST(string CurrentImage)
		{
			this.CurrentImage = CurrentImage;
		}

		public void GetCorners(int threshold, bool supress)
		{
			// create corners detector's instance
			FastCornersDetector fcd = new FastCornersDetector()
			{
				Suppress = supress,
				Threshold = threshold
			};
				
			// Apply the filter and return the points
			List<IntPoint> corners = fcd.ProcessImage(AForge.Imaging.Image.FromFile(this.CurrentImage));
			if (ImageComplete != null) ImageComplete(corners);
		}

	}
}
