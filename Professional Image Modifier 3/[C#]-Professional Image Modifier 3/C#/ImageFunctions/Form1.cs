using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using ImageFunctions.Modifications.CornerDetection;
using AForge;
using ImageFunctions.Forms;
using System.Threading.Tasks;
using AForge.Imaging.Filters;
using ImageFunctions.Controls;
using AForge.Imaging;

using System.Diagnostics;
using System.Windows.Forms.DataVisualization;

namespace ImageFunctions
{
	public partial class Form1 : Form
	{
		FrmProcessing processing = null;
		Stopwatch ssw; // Used for timing functions. (good for testing changes in optimisation) Susan StopWatch
		Stopwatch smw; // Used for timing functions. (good for testing changes in optimisation) Moravec StopWatch
		Stopwatch hsw; // Used for timing functions. (good for testing changes in optimisation) Histogram StopWatch


		private System.Windows.Forms.DataVisualization.Charting.Chart chart = new System.Windows.Forms.DataVisualization.Charting.Chart();

		#region CrossThread Delegation
		private delegate void LogDelegate(string message);
		private delegate void StatusDelegate(string status, bool Show);
		private delegate void ImageCompleteDelegate(List<IntPoint> Corners);
		private delegate void HistogramDelegate(AForge.Math.Histogram RedHist, AForge.Math.Histogram GreenHist, AForge.Math.Histogram BlueHist);
		#endregion

		#region Private Variables
		private string CurrentImage = null;
		#endregion

		#region Controls
		private SusanCornerProperties SusanProperties;
		private MoravecCornerProperties MoravecProperties;
		#endregion

		#region Enums
		private enum Modifications : int
		{
			None,
			CornerDetection,
		};

		private enum Methods : int
		{
			none,
			Susan, // Corner Detection
			Moravec, // Corner Detection
		}

		private enum RGB : int
		{
			Red,
			Green,
			Blue,
		};
		#endregion


		public Form1()
		{
			InitializeComponent();
			lbModification.Items.Add("Corner Detection");
			chart.Dock = DockStyle.Fill;
			chart.BackColor = Color.LightYellow;
			chart.ChartAreas.Add("Default");
			tpHistogram.Controls.Add(chart);
		}

		// Load new Image(s) into the listbox and display the first image
		private void btnLoadNewImage_Click_1(object sender, EventArgs e)
		{
			string firstImage = null;
			DialogResult dr = openFile.ShowDialog();
			if (dr == System.Windows.Forms.DialogResult.OK)
			{
				foreach (string file in openFile.FileNames)
				{
					if (firstImage == null) firstImage = file;
					lbImageList.Items.Add(file);

				}
				pbImage.Image = AForge.Imaging.Image.FromFile(firstImage);
				CurrentImage = firstImage;
				lbModification.Enabled = true;
				Task tHistogram = new Task(() => DoAForgeHistogram());
				tHistogram.Start();
			}
		}

		#region Controls
		#region Histogram

		private void DoAForgeHistogram()
		{
			hsw = new Stopwatch();
			hsw.Start();
			Log("Building Histogram");

			AForge.Math.Histogram grayhist;
			AForge.Math.Histogram Redhist;
			AForge.Math.Histogram Greenhist;
			AForge.Math.Histogram Bluehist;
			// collect statistics
			HorizontalIntensityStatistics his = new HorizontalIntensityStatistics(new Bitmap(pbImage.Image));
			// get gray histogram (for grayscale image)
			if (his.IsGrayscale)
			{
				grayhist = his.Gray;
			}
			else
			{
				Redhist = his.Red;
				Greenhist = his.Green;
				Bluehist = his.Blue;
				DoRGBHistogram(Redhist, Greenhist, Bluehist);
			}
			//	AForgeHistogram.Values = hist.Values;

			// output some histogram's information
			//Log("Histogram Mean = " + hist.Mean);
			//Log("Histogram Min = " + hist.Min);
			//Log("Histogram Max = " + hist.Max);

			hsw.Stop();
			Log("Histogram built in " + hsw.Elapsed);
		}

		private void DoRGBHistogram(AForge.Math.Histogram RedHist, AForge.Math.Histogram GreenHist, AForge.Math.Histogram BlueHist)
		{

			if (chart.InvokeRequired)
			{
				HistogramDelegate d = new HistogramDelegate(DoRGBHistogram);
				this.Invoke(d, new object[] { RedHist, GreenHist, BlueHist });
			}
			else
			{
				// Decide which set of values are placed at back, in the middle and to the front of the graph.
				List<double> lis = new List<double>();
				lis.Add(RedHist.Mean);
				lis.Add(GreenHist.Mean);
				lis.Add(BlueHist.Mean);
				lis.Sort();

				try
				{
					chart.Series.Add("Red");
					chart.Series.Add("Green");
					chart.Series.Add("Blue");

					// Set SplineArea chart type
					chart.Series["Red"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
					chart.Series["Green"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
					chart.Series["Blue"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
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
				}
				catch (Exception)
				{
					// Throws an exception if it is already created.
				}
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
			}
		}

		#endregion

		#endregion

		#region Control Toolbar Button Events

		// Display information on the codecs the system is aware of.
		private void btnSystemCodecInformation_Click(object sender, EventArgs e)
		{
			foreach (ImageCodecInfo ici in ImageCodecInfo.GetImageDecoders())
			{
				Log("*************************************************");
				Log("Name: " + ici.CodecName);
				Log("Dll Name: " + ici.DllName);
				Log("Filename Extension(s): " + ici.FilenameExtension);
				//TODO: Work out how to get Flag information.
				//foreach (ImageCodecFlags icf in ici.Flags)
				//{
				//	Log("Codec Flags: " + icf.ToString());
				//}
				Log("Format Description: " + ici.FormatDescription);
				Log("Mime Type: " + ici.MimeType);
				Log("Signature Masks: " + ConvertToString(ici.SignatureMasks));
				Log("Signature Patterns: " + ConvertToString(ici.SignaturePatterns));
				Log("Codec Version: " + ici.Version);
				Log("*************************************************");
			}
		}

		#endregion

		#region Utilities

		// Convert byte[][] to string
		private string ConvertToString(byte[][] p)
		{
			StringBuilder sb = new StringBuilder();
			foreach (byte[] b in p)
			{
				sb.Append(System.Text.Encoding.Default.GetString(b));
			}
			return sb.ToString();
		}
		#endregion

		#region Picturebox Context Menu Events

		// To reset we just reload the image.
		private void resetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			pbImage.Image = AForge.Imaging.Image.FromFile(CurrentImage);
			pbImage.Zoom = 100;
		}
		#endregion

		#region Listbox operations

		// Display the picture that the user has selected
		private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			pbImage.Image = AForge.Imaging.Image.FromFile(lbImageList.SelectedItem.ToString());
			CurrentImage = lbImageList.SelectedItem.ToString();
		}

		// The user has changed the form of modification they want to use.
		private void lbModification_SelectedItemChanged(object sender, EventArgs e)
		{
			lbMethods.Enabled = true;
			switch (lbModification.SelectedIndex)
			{
				case (int)Modifications.None:
					break;
				case (int)Modifications.CornerDetection:
					PopulateMethods(Modifications.CornerDetection);
					break;
				default:
					break;
			}
		}

		private void lbMethods_SelectedItemChanged(object sender, EventArgs e)
		{
			switch (lbMethods.SelectedIndex)
			{
				case (int)Methods.none:
					break;
				case (int)Methods.Susan:
					DoSusan();
					break;
				case (int)Methods.Moravec:
					DoMoravec();
					break;
				default:
					break;
			}
		}

		// Populate the Modification Method list box.
		private void PopulateMethods(Modifications modifications)
		{
			switch (modifications)
			{
				case Modifications.None:
					lbMethods.Enabled = false;
					break;
				case Modifications.CornerDetection:
					lbMethods.Items.Add("Susan Edge Detection"); // index 1
					lbMethods.Items.Add("Moravec Edge Detection"); // index 2
					break;
				default:
					lbMethods.Enabled = false;
					break;
			}

		}
		#endregion

		#region Corners
		#region Moravec Corner Detection

		private void DoMoravec()
		{
			ControlPanel.Controls.Clear(); // Remove any previous controls that were present
			MoravecProperties = new MoravecCornerProperties();
			MoravecProperties.Dock = DockStyle.Fill;
			ControlPanel.Controls.Add(MoravecProperties);
		}

		void m_ImageComplete(List<IntPoint> Corners)
		{
			ImageCornerDetectionCompleted(Corners, Color.Blue);
		}
		#endregion

		#region Susan Corner Detection

		private void DoSusan()
		{
			ControlPanel.Controls.Clear(); // Remove any previous controls that were present
			SusanProperties = new SusanCornerProperties();
			SusanProperties.Dock = DockStyle.Fill;
			ControlPanel.Controls.Add(SusanProperties);
		}

		private void s_ImageComplete(List<IntPoint> Corners)
		{
			ImageCornerDetectionCompleted(Corners, Color.Red);
		}



		#endregion

		private void ImageCornerDetectionCompleted(List<IntPoint> Corners, Color colour)
		{
			if (this.processing != null)
			{
				if (this.processing.InvokeRequired)
				{
					ImageCompleteDelegate d = new ImageCompleteDelegate(s_ImageComplete);
					this.Invoke(d, new object[] { Corners });
				}
				else
				{
					this.processing.Close();
					this.processing = null;
				}

			}
			Log("Detection Finished");
			SetStatus(Corners.Count + " corners detected", true);
			DrawCorners(Corners, colour);
			if (ssw != null && ssw.IsRunning)
			{
				ssw.Stop();
				Log("Process Duration in Seconds: " + ssw.Elapsed);
			}
			if (smw != null && smw.IsRunning)
			{
				smw.Stop();
				Log("Process Duration in Seconds: " + smw.Elapsed);
			}

		}

		private void DrawCorners(List<IntPoint> Corners, Color colour)
		{
			try
			{
				// Load image and create everything you need for drawing
				Bitmap image = new Bitmap(pbImage.Image);
				Graphics graphics = Graphics.FromImage(image);
				SolidBrush brush = new SolidBrush(colour);
				Pen pen = new Pen(brush);


				// Visualization: Draw 3x3 boxes around the corners
				foreach (IntPoint corner in Corners)
				{
					graphics.DrawRectangle(pen, corner.X - 1, corner.Y - 1, 3, 3);
				}

				// Display
				pbImage.Image = image;
				graphics.Dispose();
			}
			catch (Exception ex)
			{
				Log("Error in DrawCorners(): " + ex.Message);
			}

		}
		#endregion

		#region Logging and Status

		// Moravec Message Event
		void m_LogMessage(string Message)
		{
			Log(Message);
		}

		// Write out information to the console window at the bottom of the app
		private void Log(string p)
		{
			if (rtbConOut.InvokeRequired)
			{
				LogDelegate d = new LogDelegate(Log);
				this.Invoke(d, new object[] { p });
			}
			else
			{
				rtbConOut.AppendText(p + Environment.NewLine);
				rtbConOut.Focus();
				Application.DoEvents(); // ensure updates occur instantly
			}
		}

		private void SetStatus(string message, bool Show = true)
		{

			if (Show)
			{
				if (statusStrip1.InvokeRequired)
				{
					StatusDelegate d = new StatusDelegate(SetStatus);
					this.Invoke(d, new object[] { message, Show });
				}
				else
				{
					lblStatus.Text = message;
					Application.DoEvents(); // ensure updates occur instantly
				}

			}
			else
			{
				if (statusStrip1.InvokeRequired)
				{
					StatusDelegate d = new StatusDelegate(SetStatus);
					this.Invoke(d, new object[] { message, Show });
				}
				else
				{
					lblStatus.Text = message;
					Application.DoEvents(); // ensure updates occur instantly
				}
			}
		}

		#endregion

		#region Button Reset and Process

		private void btnReset_Click(object sender, EventArgs e)
		{
			switch (lbMethods.SelectedIndex)
			{
				case (int)Methods.none:
					break;
				case (int)Methods.Susan:
					SusanCornerProperties sp = ControlPanel.Controls[0] as SusanCornerProperties;
					sp.SetDefaults();
					break;
				case (int)Methods.Moravec:
					MoravecCornerProperties mp = ControlPanel.Controls[0] as MoravecCornerProperties;
					mp.SetDefaults();
					break;
				default:
					break;
			}
		}

		private void btnProcess_Click(object sender, EventArgs e)
		{
			switch (lbMethods.SelectedIndex)
			{
				case (int)Methods.none:
					break;
				case (int)Methods.Susan:
					ssw = new Stopwatch(); // Used for timing functions. (good for testing changes in optimisation)
					SusanCornerProperties sp = ControlPanel.Controls[0] as SusanCornerProperties;
					processing = new FrmProcessing("Conducting Susan Corner Detection");
					processing.Show();
					Log("Conducting Susan Corner Detection");
					SetStatus("Please wait for corner detection");
					Susan s = new Susan(CurrentImage);
					s.ImageComplete += s_ImageComplete;
					Task st = new Task(() => s.GetCorners(sp.DifferenceThreshold, sp.GeometricalThreshold));
					ssw.Start();
					st.Start();
					break;
				case (int)Methods.Moravec:
					smw = new Stopwatch(); // Used for timing functions. (good for testing changes in optimisation)
					MoravecCornerProperties mp = ControlPanel.Controls[0] as MoravecCornerProperties;
					processing = new FrmProcessing("Conducting Moravec Corner Detection");
					processing.Show();
					Log("Conducting Moravec Corner Detection");
					SetStatus("Please wait for corner detection");
					Moravec m = new Moravec(CurrentImage);
					m.ImageComplete += m_ImageComplete;
					m.LogMessage += m_LogMessage;
					Task mt = new Task(() => m.GetCorners(mp.Threshold, mp.Window));
					smw.Start();
					mt.Start();
					break;

				default:
					break;
			}
		}



		#endregion

	}
}
