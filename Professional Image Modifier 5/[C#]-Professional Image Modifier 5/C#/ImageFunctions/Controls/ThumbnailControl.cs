using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageFunctions.Controls
{
    public partial class ThumbnailControl : UserControl
    {
        public delegate void ThumbnailSelectedHandler(string PathToMedia);
        public event ThumbnailSelectedHandler ThumbnailSelected;

        private string CurrentImage;

        public ThumbnailControl(string ImagePath)
        {
            InitializeComponent();

            CurrentImage = ImagePath;
            FileInfo finfo = new FileInfo(ImagePath);
            Image img = Image.FromFile(ImagePath);
            pbImage.Image = FixedSize(img, 100, 100);
            lblName.Text = Path.GetFileName(ImagePath);
            lblSize.Text = finfo.Length.ToString("N0") + " bytes";
        }

        static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Transparent);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            ThumbnailSelected(this.CurrentImage);
        }

        private void pbImage_MouseEnter(object sender, EventArgs e)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void pbImage_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void pbImage_MouseClick(object sender, MouseEventArgs e)
        {
            ThumbnailSelected(this.CurrentImage);
        }
    }
}
