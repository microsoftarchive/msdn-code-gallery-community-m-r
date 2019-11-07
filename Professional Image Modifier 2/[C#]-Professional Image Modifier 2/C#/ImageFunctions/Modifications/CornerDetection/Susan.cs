//Remarks
//The class implements Susan corners detector, which is described by S.M. Smith in: S.M. Smith, "SUSAN - a new approach to low level image processing", 
//Internal Technical Report TR95SMS1, Defense Research Agency, Chobham Lane, Chertsey, Surrey, UK, 1995.
//[Note] Note:Some implementation notes:
//	Analyzing each pixel and searching for its USAN area, the 7x7 mask is used, which is comprised of 37 pixels. The mask has circle shape:
//	Copy 
//	  xxx
//	 xxxxx
//	xxxxxxx
//	xxxxxxx
//	xxxxxxx
//	 xxxxx
//	  xxx
//	In the case if USAN's center of mass has the same coordinates as nucleus (central point), the pixel is not a corner.
//	For noise suppression the 5x5 square window is used.
//The class processes only grayscale 8 bpp and color 24/32 bpp images. In the case of color image, it is converted to grayscale internally using 
//GrayscaleBT709 filter.
// http://www.aforgenet.com/framework/docs/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge;
using AForge.Imaging;
using ImageFunctions.Forms;


namespace ImageFunctions.Modifications.CornerDetection
{
	class Susan
	{
		private string CurrentImage;
		private FrmProcessing processing;
		public delegate void ImageCompleteHandler(List<IntPoint> Corners);
		public event ImageCompleteHandler ImageComplete;

		public Susan(string CurrentImage)
		{

			this.CurrentImage = CurrentImage;
			
		}

		public void GetCorners(int diff, int geo)
		{
			// create corners detector's instance
			SusanCornersDetector scd = new SusanCornersDetector(diff,geo);
			// process image searching for corners
			List<IntPoint> corners = scd.ProcessImage(AForge.Imaging.Image.FromFile(this.CurrentImage));
			if (ImageComplete != null) ImageComplete(corners);
		}


	}
}
