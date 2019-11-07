using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageFunctions.Controls;
using WeifenLuo.WinFormsUI.Docking;

using ImageFunctions.Classes;
using ImageFunctions.Modifications.CornerDetection;
using System.Diagnostics;
using AForge;

namespace ImageFunctions.Forms
{
    public partial class FrmModificationProperties : DockContent
    {
        public delegate void ModificationPropertiesLogHandler(string Message);
        public event ModificationPropertiesLogHandler ModificationPropertiesLog;
        public delegate void ModificationPropertiesModifiedImageHandler(Image modifiedImage);
        public event ModificationPropertiesModifiedImageHandler UpdateImage;



        #region Stopwatches

        private Stopwatch swSusan = new Stopwatch();
        private Stopwatch swMoravec = new Stopwatch();
        private Stopwatch swHarris = new Stopwatch();
        private Stopwatch swFast = new Stopwatch();
        #endregion

        #region Controls
        private SusanCornerProperties sp;
        private MoravecCornerProperties mp;
        private HarrisCornerProperties hp;
        private FASTCornerProperties fp;
        #endregion

        public string CurrentImage { get; set; }

        public string DetectorType { get; set; }

        public FrmModificationProperties()
        {
            InitializeComponent();
        }

        protected override string GetPersistString()
        {
            return this.Text;
        }

        #region FAST Corner Detection

        public void DoFast()
        {
            DetectorType = "FAST";
            ControlPanel.Controls.Clear();
            fp = new FASTCornerProperties();
            ControlPanel.Controls.Add(fp);

        }

        private void FASTCornerDetection()
        {


            swFast.Reset(); // Used for timing functions. (good for testing changes in optimisation)
            swFast.Start();



            if (ModificationPropertiesLog != null) ModificationPropertiesLog("Conducting FAST Corner Detection");

            FAST f = new FAST(CurrentImage);
            f.ImageComplete += f_ImageComplete;

            Task ht = new Task(() => f.GetCorners(fp.Threshold, fp.Supress));
            ht.Start();
        }

        void f_ImageComplete(List<AForge.IntPoint> Corners)
        {
            ImageCornerDetectionCompleted(Corners, Color.Cornsilk);
        }
        #endregion

        #region Harris Corner Detection

        public void DoHarris()
        {
            DetectorType = "Harris";
            ControlPanel.Controls.Clear();
            hp = new HarrisCornerProperties();
            ControlPanel.Controls.Add(hp);

        }

        private void HarrisCornerDetection()
        {


            swHarris.Reset(); // Used for timing functions. (good for testing changes in optimisation)
            swHarris.Start();


            if (ModificationPropertiesLog != null) ModificationPropertiesLog("Conducting Harris Corner Detection");

            Harris h = new Harris(CurrentImage);
            h.ImageComplete += h_ImageComplete;

            Task ht = new Task(() => h.GetCorners(hp.Threshold, hp.Sigma));
            ht.Start();
        }

        void h_ImageComplete(List<AForge.IntPoint> Corners)
        {
            ImageCornerDetectionCompleted(Corners, Color.Blue);
        }
        #endregion

        #region Moravec Corner Detection

        public void DoMoravec()
        {
            DetectorType = "Moravec";
            ControlPanel.Controls.Clear();
            mp = new MoravecCornerProperties();
            ControlPanel.Controls.Add(mp);

        }

        private void MoravecCornerDetection()
        {


            swMoravec.Reset(); // Used for timing functions. (good for testing changes in optimisation)
            swMoravec.Start();


            if (ModificationPropertiesLog != null) ModificationPropertiesLog("Conducting Moravec Corner Detection");

            Moravec m = new Moravec(CurrentImage);
            m.ImageComplete += m_ImageComplete;
            m.LogMessage += m_LogMessage;
            Task mt = new Task(() => m.GetCorners(mp.Threshold, mp.Window));
            mt.Start();
        }

        void m_LogMessage(string Message)
        {
            ModificationPropertiesLog(Message);
        }

        void m_ImageComplete(List<AForge.IntPoint> Corners)
        {
            ImageCornerDetectionCompleted(Corners, Color.Azure);
        }
        #endregion

        #region SUSAN Corner Detection

        public void DoSusan()
        {
            DetectorType = "SUSAN";
            ControlPanel.Controls.Clear();
            sp = new SusanCornerProperties();
            ControlPanel.Controls.Add(sp);

        }

        private void SusanCornerDetection()
        {


            swSusan.Reset(); // Used for timing functions. (good for testing changes in optimisation)
            swSusan.Start();


            if (ModificationPropertiesLog != null) ModificationPropertiesLog("Conducting Susan Corner Detection");

            Susan s = new Susan(CurrentImage);
            s.ImageComplete += s_ImageComplete;

            Task st = new Task(() => s.GetCorners(sp.DifferenceThreshold, sp.GeometricalThreshold));
            st.Start();
        }

        void s_ImageComplete(List<AForge.IntPoint> Corners)
        {
            ImageCornerDetectionCompleted(Corners, Color.AliceBlue);
        }
        #endregion

        private void ImageCornerDetectionCompleted(List<IntPoint> Corners, Color colour)
        {
            DrawCorners(Corners, colour);

            if (swSusan.IsRunning)
            {
                swSusan.Stop();
                ModificationPropertiesLog(Corners.Count.ToString("N0") + " SUSAN Corners Detected in " + swSusan.Elapsed);
            }
            else if (swHarris.IsRunning)
            {
                swHarris.Stop();
                ModificationPropertiesLog(Corners.Count.ToString("N0") + " Harris Corners Detected in " + swHarris.Elapsed);
            }
            else if (swMoravec.IsRunning)
            {
                swMoravec.Stop();
                ModificationPropertiesLog(Corners.Count.ToString("N0") + " Moravec Corners Detected in " + swMoravec.Elapsed);
            }
            else if (swFast.IsRunning)
            {
                swFast.Stop();
                ModificationPropertiesLog(Corners.Count.ToString("N0") + " FAST Corners Detected in " + swFast.Elapsed);
            }
        }

        private void DrawCorners(List<IntPoint> Corners, Color colour)
        {
            ModificationPropertiesLog("Creating corners");
            try
            {
                // Load image and create everything you need for drawing
                Bitmap image = new Bitmap(AForge.Imaging.Image.FromFile(CurrentImage));
                Graphics graphics = Graphics.FromImage(image);
                SolidBrush brush = new SolidBrush(colour);
                Pen pen = new Pen(brush);


                // Visualization: Draw 3x3 boxes around the corners
                foreach (IntPoint corner in Corners)
                {
                    graphics.DrawRectangle(pen, corner.X - 1, corner.Y - 1, 3, 3);
                }

                // Display
                UpdateImage(image);
                graphics.Dispose();
            }
            catch (Exception ex)
            {
                ModificationPropertiesLog("Error in DrawCorners(): " + ex.Message);
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            switch (DetectorType)
            {
                case "SUSAN":
                    SusanCornerProperties sp = ControlPanel.Controls[0] as SusanCornerProperties;
                    sp.SetDefaults();
                    break;
                case "Harris":
                    HarrisCornerProperties hc = ControlPanel.Controls[0] as HarrisCornerProperties;
                    hc.SetDefaults();
                    break;
                case "Moravec":
                    MoravecCornerProperties mp = ControlPanel.Controls[0] as MoravecCornerProperties;
                    mp.SetDefaults();
                    break;
                case "FAST":
                    FASTCornerProperties fc = ControlPanel.Controls[0] as FASTCornerProperties;
                    fc.SetDefaults();
                    break;
                default:
                    break;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            switch (DetectorType.ToLowerInvariant())
            {
                case "susan":
                    SusanCornerDetection();
                    break;
                case "harris":
                    HarrisCornerDetection();
                    break;
                case "moravec":
                    MoravecCornerDetection();
                    break;
                case "fast":
                    FASTCornerDetection();
                    break;
                default:
                    break;
            }
        }
    }
}
