using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ImageFunctions.Forms
{
    public partial class FrmImageDisplay : DockContent
    {
        

        #region Properties
        public bool IsMediaLoaded { get; set; }
        #endregion

        #region Private Variables
        private string CurrentImage;
        #endregion

        #region Events

        public delegate void ImageDisplayMediaLoadedHandler(string MediaPath);
        public event ImageDisplayMediaLoadedHandler MediaLoaded;
        public delegate void ImageDisplayMediaLoadingFailedHandler(string ErrorMessage);
        public event ImageDisplayMediaLoadingFailedHandler MediaFailedToLoad;
        public delegate void ImageDisplayMediaPixelColourHandler(Color colour);
        public event ImageDisplayMediaPixelColourHandler MediaPixelColour;
        public delegate void ImageDisplayMediaPixelCoordinatesHandler(Point mouseXY);
        public event ImageDisplayMediaPixelCoordinatesHandler MediaPixelCoordinates;
        public delegate void ImageDisplayLogHandler(String Message);
        public event ImageDisplayLogHandler ImageDisplayLog;
        #endregion

        /// <summary>
        /// Instantiation Method
        /// </summary>
        public FrmImageDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Used by the Main Form to Recognise this form when re-applying previous docking settings when loading.
        /// </summary>
        /// <returns></returns>
        protected override string GetPersistString()
        {
            return this.Text;
        }

        //TODO: Workout what media this is and process accordingly - for now assume image.
        public void ShowSingleMedia(string MediaPath)
        {
            CurrentImage = MediaPath;
            try
            {
                FileInfo finfo = new FileInfo(MediaPath);
                lblFileSize.Text = ImageFunctions.Classes.Utilities.BytesToString(finfo.Length);
                pbImage.Image = AForge.Imaging.Image.FromFile(MediaPath);
                if (MediaLoaded != null) MediaLoaded(MediaPath);
                IsMediaLoaded = true;

            }
            catch (Exception ex)
            {
                if (MediaFailedToLoad != null) MediaFailedToLoad(ex.Message);
                IsMediaLoaded = false;
            }

        }

        public void UpdateImage(Image updatedImage)
        {
            pbImage.Image = updatedImage;
            IsMediaLoaded = true;
            ImageDisplayLog("Image Updated");
        }

        /// <summary>
        /// Get's the current Pixel colour from under the mouse position
        /// </summary>
        /// <param name="point">
        /// Point: The X,Y Coordinates of the mouse.
        /// </param>
        /// <returns></returns>
        private Color GetClickedPixel(Point point)
        {
            Bitmap bitmap = (Bitmap)pbImage.Image;
            if (point.X < bitmap.Width && point.Y < bitmap.Height) // Make sure that if loading a new image and it is smaller than the last image that we do not process the PointXY until the 
            // image is adjusted correctly
            {
                return bitmap.GetPixel(point.X, point.Y);
            }
            return Color.Black;
        }

        /// <summary>
        /// Reset the Image display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pbImage.Image = AForge.Imaging.Image.FromFile(CurrentImage);

        }

        /// <summary>
        /// Mouse Move Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">
        /// Point: e.Location used to indicate where the mouse is within the image
        /// </param>
        private void pbImage_MouseMove_1(object sender, MouseEventArgs e)
        {

            pbImage.Cursor = Cursors.Cross; // Force the cursor to remain the cross - it sometimes inexplicably reverts back to Arrow

            if (pbImage.Image != null) // make sure an image is loaded :P
            {
                Point realLocationOnImage = pbImage.PointToImage(e.Location); // This converts the mouse XY coordinates to the real location on the image - as that changes as we pan and zoom.

                if (MediaPixelColour != null) MediaPixelColour(GetClickedPixel(realLocationOnImage));
                if (MediaPixelCoordinates != null) MediaPixelCoordinates(realLocationOnImage);
                lblMouseX.Text = (realLocationOnImage.X).ToString("N0");
                lblMouseY.Text = (realLocationOnImage.Y).ToString("N0");
                btnSelectedColour.BackColor = GetClickedPixel(realLocationOnImage);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripMenuItem1.Checked = !toolStripMenuItem1.Checked; // Toggle the checked box
            pbImage.ShowPixelGrid = toolStripMenuItem1.Checked;
            if (ImageDisplayLog != null) ImageDisplayLog("Display Pixel Grid: " + toolStripMenuItem1.Checked.ToString());
        }

        /// <summary>
        /// Resets the zoom so the image is displayed at 100%
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pbImage.Zoom = 100;
        }
    }
}
