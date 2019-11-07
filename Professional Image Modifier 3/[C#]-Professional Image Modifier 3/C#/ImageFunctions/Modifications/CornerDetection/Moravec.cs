using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Imaging;
using ImageFunctions.Forms;

namespace ImageFunctions.Modifications.CornerDetection
{
	class Moravec
	{
		private string CurrentImage;
		private FrmProcessing processing;

		public delegate void ImageCompleteHandler(List<IntPoint> Corners);
		public event ImageCompleteHandler ImageComplete;

		public delegate void LogMessageHandler(string Message);
		public event LogMessageHandler LogMessage;

		public Moravec(string CurrentImage)
		{
			this.CurrentImage = CurrentImage;
		}

		public void GetCorners(int threshold, int window)
		{
			MoravecCornersDetector mcd;
			// create corners detector's instance
			if (IsOdd(window))
			{
				mcd = new MoravecCornersDetector(threshold, window);
			}
			else
			{
				mcd = new MoravecCornersDetector(threshold, window - 1); // make it an odd number
				if (LogMessage != null) LogMessage("Changed Window Size to: " + (window - 1).ToString() + " Window Size must be odd!");
			}

			// process image searching for corners
			List<IntPoint> corners = mcd.ProcessImage(AForge.Imaging.Image.FromFile(this.CurrentImage));
			if (ImageComplete != null) ImageComplete(corners);
		}

		// Returns true if the integer supplied is Odd
		private bool IsOdd(int CheckNumber)
		{
			return CheckNumber % 2 != 0;
		}

	}
}
