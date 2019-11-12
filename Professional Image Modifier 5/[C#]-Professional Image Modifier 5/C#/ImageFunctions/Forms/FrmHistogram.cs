using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using WeifenLuo.WinFormsUI.Docking;

namespace ImageFunctions.Forms
{
    public partial class FrmHistogram : DockContent
    {
        public delegate void HistogramLog(string Message);
        public event HistogramLog histogramLog;
        public delegate void HistogramCompleted(string Message);
        public event HistogramCompleted histogramCompleted;

        private Stopwatch swHistogram = new Stopwatch();
        private bool IsHorizontalIntensity { get; set; }
        private bool IsSpline = false;
        private bool IsSplineArea = true; // Default
        private bool IsSplineRange = false;
        private bool IsArea = false;
        private bool IsBar = false;

        private string CurrentImage = null;

        private System.Windows.Forms.DataVisualization.Charting.Chart chart = new System.Windows.Forms.DataVisualization.Charting.Chart();

        public FrmHistogram()
        {
            InitializeComponent();
            chart.Dock = DockStyle.Fill;
            chart.BackColor = Color.LightYellow;
            chart.ChartAreas.Add("Default");
            this.Controls.Add(chart);
            IsHorizontalIntensity = true; // Default.
        }

        protected override string GetPersistString()
        {
            return this.Text;

        }

        #region Histogram

        /// <summary>
        /// Build the Histogram for Supplied Image
        /// </summary>
        /// <param name="image">
        /// String: Path to image that histogram is to be created out of
        /// </param>
        public void DoHistogram(string image)
        {
            CurrentImage = image; // Used for re-generating the histogram
            bool IsGrayScale = AForge.Imaging.Image.IsGrayscale(new Bitmap(image));
            dynamic IntensityStatistics = null; // Use dynamic (a little like var) to assign this variable which maybe of different types.

            swHistogram.Reset();
            swHistogram.Start();
            histogramLog("Creating Histogram");

            AForge.Math.Histogram grayhist;
            AForge.Math.Histogram Redhist;
            AForge.Math.Histogram Greenhist;
            AForge.Math.Histogram Bluehist;
            // collect statistics
            //NOTE: We have to use the braces on these statements see: http://stackoverflow.com/questions/2496589/variable-declarations-following-if-statements
            if (IsHorizontalIntensity)
            {
                histogramLog("Using HorizontalIntensityStatistics");
                IntensityStatistics = new HorizontalIntensityStatistics(new Bitmap(image));
            }
            else
            {
                histogramLog("Using VerticalIntensityStatistics");
                IntensityStatistics = new VerticalIntensityStatistics(new Bitmap(image));
            }

            // get gray histogram (for grayscale image)
            if (IsGrayScale)
            {
                grayhist = IntensityStatistics.Gray;
                //TODO: DoGrayHistogram();
                histogramLog("Grayscale Histogram");
            }
            else
            {

                Redhist = IntensityStatistics.Red;
                Greenhist = IntensityStatistics.Green;
                Bluehist = IntensityStatistics.Blue;
                DoRGBHistogram(Redhist, Greenhist, Bluehist);
                histogramLog("RGB Histogram");
            }

            swHistogram.Stop();
            histogramCompleted("Histogram built in " + swHistogram.Elapsed);

        }

        /// <summary>
        /// Draws the Histogram on the Chart
        /// </summary>
        /// <param name="RedHist"></param>
        /// <param name="GreenHist"></param>
        /// <param name="BlueHist"></param>
        private void DoRGBHistogram(AForge.Math.Histogram RedHist, AForge.Math.Histogram GreenHist, AForge.Math.Histogram BlueHist)
        {

            chart.Series.Clear();
            chart.Series.Add("Red");
            chart.Series.Add("Green");
            chart.Series.Add("Blue");

            // Set SplineArea chart type
            SelectChartType();


            // set line tension
            chart.Series["Red"]["LineTension"] = "0.8";
            chart.Series["Green"]["LineTension"] = "0.8";
            chart.Series["Blue"]["LineTension"] = "0.8";
            // Set colour and transparency
            chart.Series["Red"].Color = Color.FromArgb(50, Color.Red);
            chart.Series["Green"].Color = Color.FromArgb(50, Color.Green);
            chart.Series["Blue"].Color = Color.FromArgb(50, Color.Blue);
            // Disable X & Y axis labels
            chart.ChartAreas["Default"].AxisX.LabelStyle.Enabled = false;
            chart.ChartAreas["Default"].AxisY.LabelStyle.Enabled = false;
            chart.ChartAreas["Default"].AxisX.MinorGrid.Enabled = false;
            chart.ChartAreas["Default"].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas["Default"].AxisY.MinorGrid.Enabled = false;
            chart.ChartAreas["Default"].AxisY.MajorGrid.Enabled = false;

            chart.Series["Red"].Points.Clear();
            chart.Series["Green"].Points.Clear();
            chart.Series["Blue"].Points.Clear();

            foreach (double value in RedHist.Values)
            {
                chart.Series["Red"].Points.AddY(value);
            }
            foreach (double value in GreenHist.Values)
            {
                chart.Series["Green"].Points.AddY(value);
            }
            foreach (double value in BlueHist.Values)
            {
                chart.Series["Blue"].Points.AddY(value);
            }

            lblRedMin.Text = RedHist.Min.ToString("N0");
            lblRedMean.Text = RedHist.Mean.ToString("N0");
            lblRedMax.Text = RedHist.Max.ToString("N0");

            lblGreenMin.Text = GreenHist.Min.ToString("N0");
            lblGreenMean.Text = GreenHist.Mean.ToString("N0");
            lblGreenMax.Text = GreenHist.Max.ToString("N0");

            lblBlueMin.Text = BlueHist.Min.ToString("N0");
            lblBlueMean.Text = BlueHist.Mean.ToString("N0");
            lblBlueMax.Text = BlueHist.Max.ToString("N0");

        }

        #endregion

        #region Horizontal or Vertical Intensity
        /// <summary>
        /// If the Horizontal Intensity is checked then set the IsHorizontalIntensity to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void horizontalIntensityMenuItem_Click(object sender, EventArgs e)
        {
            ToggleMenu();
            if (horizontalIntensityMenuItem.Checked) IsHorizontalIntensity = true;
            DoHistogram(CurrentImage);
        }

        /// <summary>
        /// The two options Horizontal & Vertical are Mutually exclusive this make sure that both options are not checked at the same time
        /// One is checked as default - the other is not, so they will swap around.
        /// </summary>
        private void ToggleMenu()
        {
            horizontalIntensityMenuItem.Checked = !horizontalIntensityMenuItem.Checked; // Toggle this
            verticalIntensityMenuItem.Checked = !verticalIntensityMenuItem.Checked; // Toggle this
        }

        /// <summary>
        /// If the Horizontal Intensity is checked then set the IsHorizontalIntensity to False
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void verticalIntensityMenuItem_Click(object sender, EventArgs e)
        {
            ToggleMenu();
            if (verticalIntensityMenuItem.Checked) IsHorizontalIntensity = false;
            DoHistogram(CurrentImage);
        }
        #endregion

        #region Pixel Colour Under Mouse
        public void SetPixelColour(Color colour)
        {
            btnPixelColour.BackColor = colour;
        }

        public void SetMouseCoordinates(Point mouse)
        {
            lbCoordinatesX.Text = mouse.X.ToString();
            lblCoordinatesY.Text = mouse.Y.ToString();
        }
        #endregion

        #region Display Histogram Types
        /// <summary>
        /// Decide which Graph type to display
        /// </summary>
        private void SelectChartType()
        {
            if (IsSplineArea)
            {
                chart.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                chart.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
                chart.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            }
            else if (IsSpline)
            {
                chart.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chart.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                chart.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            }
            else if (IsSplineRange)
            {
                chart.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineRange;
                chart.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineRange;
                chart.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineRange;
            }
            else if (IsArea)
            {
                chart.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
                chart.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
                chart.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            }
            else if (IsBar) // Bar needs Horizontal Orientation.
            {
                chart.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                chart.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                chart.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;

            }
            else
            {
                histogramLog("Unknown Histogram Type");
            }
        }

        // Because the Chart type has been changed - re-do Histogram
        private void DoChartTypes()
        {
            IsSpline = splineToolStripMenuItem.Checked;
            IsSplineArea = splineAreaToolStripMenuItem.Checked;
            IsSplineRange = splineRangeToolStripMenuItem.Checked;
            IsArea = AreaStripMenuItem.Checked;
            IsBar = barToolStripMenuItem.Checked;
            DoHistogram(CurrentImage);
        }
        #endregion

        #region Chart Types Menu Events
        private void splineAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splineAreaToolStripMenuItem.Checked = true;
            splineToolStripMenuItem.Checked = false;
            splineRangeToolStripMenuItem.Checked = false;
            AreaStripMenuItem.Checked = false;
            barToolStripMenuItem.Checked = false;
            DoChartTypes();
        }

        private void splineRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splineAreaToolStripMenuItem.Checked = false;
            splineToolStripMenuItem.Checked = false;
            splineRangeToolStripMenuItem.Checked = true;
            AreaStripMenuItem.Checked = false;
            barToolStripMenuItem.Checked = false;
            DoChartTypes();
        }

        private void splineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splineAreaToolStripMenuItem.Checked = false;
            splineToolStripMenuItem.Checked = true;
            splineRangeToolStripMenuItem.Checked = false;
            AreaStripMenuItem.Checked = false;
            barToolStripMenuItem.Checked = false;
            DoChartTypes();
        }

        private void AreaStripMenuItem_Click(object sender, EventArgs e)
        {
            splineAreaToolStripMenuItem.Checked = false;
            splineToolStripMenuItem.Checked = false;
            splineRangeToolStripMenuItem.Checked = false;
            AreaStripMenuItem.Checked = true;
            barToolStripMenuItem.Checked = false;
            DoChartTypes();
        }

        private void barToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splineAreaToolStripMenuItem.Checked = false;
            splineToolStripMenuItem.Checked = false;
            splineRangeToolStripMenuItem.Checked = false;
            AreaStripMenuItem.Checked = false;
            barToolStripMenuItem.Checked = true;
            DoChartTypes();
        }
        #endregion





    }
}
