using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using ImageFunctions.Modifications.CornerDetection;
using AForge;
using ImageFunctions.Forms;
using System.Threading.Tasks;
using ImageFunctions.Controls;
using AForge.Imaging;

using System.Diagnostics;

namespace ImageFunctions
{
	public partial class Form1 : Form
	{
		FrmProcessing processing = null;
		Stopwatch swSusan = new Stopwatch(); // Used for timing functions. (good for testing changes in optimisation) Susan StopWatch
		Stopwatch swMoravec = new Stopwatch(); // Used for timing functions. (good for testing changes in optimisation) Moravec StopWatch
		Stopwatch swHarris = new Stopwatch(); // Used for timing functions. (good for testing changes in optimisation) Harris StopWatch
		Stopwatch swHistogram = new Stopwatch(); // Used for timing functions. (good for testing changes in optimisation) Histogram Control StopWatch
		Stopwatch swStatistics = new Stopwatch(); // Used for timing functions. (good for testing changes in optimisation) Statistics Control StopWatch
		Stopwatch swFast = new Stopwatch(); // Fast Corner Detector Timer

		#region CrossThread Delegation
		private delegate void LogDelegate(string message);
		private delegate void StatusDelegate(string status, bool Show);
		private delegate void ImageCompleteDelegate(List<IntPoint> Corners, Color colour);
		private delegate void HistogramDelegate(AForge.Math.Histogram RedHist, AForge.Math.Histogram GreenHist, AForge.Math.Histogram BlueHist);
		private delegate void StatisticsDelegate(DataGridViewRow dgvr);
		private delegate void ResetStatisticsDelegate();
		#endregion

		#region Private Variables
		private string CurrentImage = null;
		
		#endregion

		#region Controls
		private System.Windows.Forms.DataVisualization.Charting.Chart chart = new System.Windows.Forms.DataVisualization.Charting.Chart();

		private SusanCornerProperties SusanProperties;
		private MoravecCornerProperties MoravecProperties;
		private HarrisCornerProperties HarrisProperties;
		private FASTCornerProperties FastProperties;
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
			Harris, // Corner Detection
			Fast,
		}

		private enum RGB : int
		{
			Red,
			Green,
			Blue,
		};
		#endregion

		//TODO: Prevent Users from running multiple operations at the same time

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
				Task tHistogram = new Task(() => DoHistogram());
				tHistogram.Start();
				Task tStatistics = new Task(() => DoStatistics());
				tStatistics.Start();
			}
		}

		#region Controls
		#region Histogram

		private void DoHistogram()
		{
			swHistogram.Reset();
			swHistogram.Start();
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

			// output some histogram's information
			//Log("Histogram Mean = " + hist.Mean);
			//Log("Histogram Min = " + hist.Min);
			//Log("Histogram Max = " + hist.Max);

			swHistogram.Stop();
			Log("Histogram built in " + swHistogram.Elapsed);

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

		#region Statistics
		private void DoStatistics()
		{
			swStatistics.Reset();
			swStatistics.Start();
			Log("Building Statistics");
			Classes.ImageStatistics imgStats = new Classes.ImageStatistics(this.CurrentImage);
			imgStats.StatsComplete += imgStats_StatsComplete;
			imgStats.StatsError += imgStats_StatsError;
			imgStats.GetStatistics();
			swStatistics.Stop();
			Log("Statistics completed in " + swStatistics.Elapsed);
		}

		void imgStats_StatsError(string Message)
		{
			Log("Error Collecting Statistics: " + Message);
		}

		// Compile the statistics into the Datagrid view that we have generated
		void imgStats_StatsComplete(System.Collections.ArrayList Statistics)
		{
			Array tmp = Statistics.ToArray();

			ResetStatistics();

			for (int idx = 0; idx < tmp.Length; idx++)
			{
				DataGridViewRow dgvr = new DataGridViewRow();
				dgvr.CreateCells(dgvStatistics);
				string description = tmp.GetValue(idx).ToString();

				string value = tmp.GetValue(idx + 1).ToString();
				dgvr.Cells[0].Value = description;
				dgvr.Cells[1].Value = value;
				AddStatisticsRow(dgvr);
				idx++;
			}
		}

		private void AddStatisticsRow(DataGridViewRow dgvr)
		{
			if (dgvStatistics.InvokeRequired)
			{
				StatisticsDelegate d = new StatisticsDelegate(AddStatisticsRow);
				this.Invoke(d, new object[] { dgvr });
			}
			else
			{
				dgvStatistics.Rows.Add(dgvr);
			}
		}


		private void ResetStatistics()
		{
			if (dgvStatistics.InvokeRequired)
			{
				ResetStatisticsDelegate d = new ResetStatisticsDelegate(ResetStatistics);
				this.Invoke(d, new object[] { });
			}
			else
			{
				dgvStatistics.Rows.Clear();
			}
		}

		private void rebuildToolStripMenuItem_Click(object sender, EventArgs e)
		{

			DoStatistics();

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

		// Toggle the Process and Reset buttons
		private void ToggleButtons()
		{
			btnProcess.Enabled = !btnProcess.Enabled;
			btnReset.Enabled = !btnReset.Enabled;
		}

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
				case (int)Methods.Harris:
					DoHarris();
					break;
				case (int)Methods.Fast:
					DoFast();
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
					lbMethods.Items.Add("Susan Corner Detection"); // index 1
					lbMethods.Items.Add("Moravec Corner Detection"); // index 2
					lbMethods.Items.Add("Harris Corner Detection");
					lbMethods.Items.Add("FAST Corner Detection");
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

		#region Harris

		private void DoHarris()
		{
			ControlPanel.Controls.Clear(); // Remove any previous controls that were present
			HarrisProperties = new HarrisCornerProperties();
			HarrisProperties.Dock = DockStyle.Fill;
			ControlPanel.Controls.Add(HarrisProperties);
		}

		void h_ImageComplete(List<IntPoint> Corners)
		{
			ImageCornerDetectionCompleted(Corners, Color.Chartreuse);
		}

		#endregion

		#region FAST Corner Detection
		private void DoFast()
		{
			ControlPanel.Controls.Clear(); // Remove any previous controls that were present
			FastProperties = new FASTCornerProperties();
			FastProperties.Dock = DockStyle.Fill;
			ControlPanel.Controls.Add(FastProperties);
		}

		void f_ImageComplete(List<IntPoint> Corners)
		{
			ImageCornerDetectionCompleted(Corners, Color.LightCyan);
		}

		#endregion

		private void ImageCornerDetectionCompleted(List<IntPoint> Corners, Color colour)
		{
			if (this.processing != null)
			{
				if (this.processing.InvokeRequired)
				{
					ImageCompleteDelegate d = new ImageCompleteDelegate(ImageCornerDetectionCompleted);
					this.Invoke(d, new object[] { Corners, colour });
				}
				else
				{
					this.processing.Close();
					this.processing = null;
					DrawCorners(Corners, colour);

					if (swSusan.IsRunning)
					{
						swSusan.Stop();
						Log(Corners.Count.ToString("N0") + " SUSAN Corners Detected in " + swSusan.Elapsed);
					}
					else if (swHarris.IsRunning)
					{
						swHarris.Stop();
						Log(Corners.Count.ToString("N0") + " Harris Corners Detected in " + swHarris.Elapsed);
					}
					else if (swMoravec.IsRunning)
					{
						swMoravec.Stop();
						Log(Corners.Count.ToString("N0") + " Moravec Corners Detected in " + swMoravec.Elapsed);
					}
					else if (swFast.IsRunning)
					{
						swFast.Stop();
						Log(Corners.Count.ToString("N0") + " FAST Corners Detected in " + swFast.Elapsed);
					}


				}

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
				case (int)Methods.Harris:
					HarrisCornerProperties hc = ControlPanel.Controls[0] as HarrisCornerProperties;
					hc.SetDefaults();
					break;
				case (int)Methods.Fast:
					FASTCornerProperties fc = ControlPanel.Controls[0] as FASTCornerProperties;
					fc.SetDefaults();
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
					SusanCornerDetection();
					break;
				case (int)Methods.Moravec:
					MoravecCornerDetection();
					break;
				case (int)Methods.Harris:
					HarrisCornerDetection();
					break;
				case (int)Methods.Fast:
					FASTCornerDetection();
					break;
				default:
					break;
			}
		}

		private void FASTCornerDetection()
		{
			swFast.Reset(); // Used for timing functions. (good for testing changes in optimisation)
			swFast.Start();
			FASTCornerProperties fp = ControlPanel.Controls[0] as FASTCornerProperties;
			processing = new FrmProcessing("Conducting FAST Corner Detection");
			processing.Show();
			Log("Conducting FAST Corner Detection");
			SetStatus("Please wait for corner detection");
			FAST f = new FAST(CurrentImage);
			f.ImageComplete += f_ImageComplete;

			Task ht = new Task(() => f.GetCorners(fp.Threshold, fp.Supress));
			ht.Start();
		}



		private void HarrisCornerDetection()
		{
			swHarris.Reset(); // Used for timing functions. (good for testing changes in optimisation)
			swHarris.Start();
			HarrisCornerProperties hp = ControlPanel.Controls[0] as HarrisCornerProperties;
			processing = new FrmProcessing("Conducting Harris Corner Detection");
			processing.Show();
			Log("Conducting Harris Corner Detection");
			SetStatus("Please wait for corner detection");
			Harris h = new Harris(CurrentImage);
			h.ImageComplete += h_ImageComplete;

			Task ht = new Task(() => h.GetCorners(hp.Threshold, hp.Sigma));
			ht.Start();
		}

		private void MoravecCornerDetection()
		{
			swMoravec.Reset(); // Used for timing functions. (good for testing changes in optimisation)
			swMoravec.Start();
			MoravecCornerProperties mp = ControlPanel.Controls[0] as MoravecCornerProperties;
			processing = new FrmProcessing("Conducting Moravec Corner Detection");
			processing.Show();
			Log("Conducting Moravec Corner Detection");
			SetStatus("Please wait for corner detection");
			Moravec m = new Moravec(CurrentImage);
			m.ImageComplete += m_ImageComplete;
			m.LogMessage += m_LogMessage;
			Task mt = new Task(() => m.GetCorners(mp.Threshold, mp.Window));
			mt.Start();
		}

		private void SusanCornerDetection()
		{
			swSusan.Reset(); // Used for timing functions. (good for testing changes in optimisation)
			swSusan.Start();
			SusanCornerProperties sp = ControlPanel.Controls[0] as SusanCornerProperties;
			processing = new FrmProcessing("Conducting Susan Corner Detection");
			processing.Show();
			Log("Conducting Susan Corner Detection");
			SetStatus("Please wait for corner detection");
			Susan s = new Susan(CurrentImage);
			s.ImageComplete += s_ImageComplete;

			Task st = new Task(() => s.GetCorners(sp.DifferenceThreshold, sp.GeometricalThreshold));
			st.Start();
		}





		#endregion



	}
}
