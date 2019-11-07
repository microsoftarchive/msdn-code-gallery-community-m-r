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


namespace ImageFunctions
{
	public partial class Form1 : Form
	{
		FrmProcessing processing = null;

		#region CrossThread Delegation
		private delegate void LogDelegate(string message);
		private delegate void StatusDelegate(string status, bool Show);
		private delegate void ImageCompleteDelegate(List<IntPoint> Corners);
		#endregion

		#region Private Variables
		private string CurrentImage = null;
		#endregion

		#region Controls
		private SusanCornerProperties SusanProperties;
		#endregion

		#region Enums
		private enum Modifications : int
		{
			None,
			CornerDetection
		};

		private enum Methods : int
		{
			none,
			Susan, // Corner Detection
		}
		#endregion


		public Form1()
		{
			InitializeComponent();

		}


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
				//	Application.DoEvents();
			}
		}

		// To reset we just reload the image.
		private void resetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			pbImage.Image = Image.FromFile(CurrentImage);
			pbImage.Zoom = 100;
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
				pbImage.Image = Image.FromFile(firstImage);
				CurrentImage = firstImage;
				lbModification.Enabled = true;
			}
		}

		// Display the picture that the user has selected
		private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			pbImage.Image = Image.FromFile(lbImageList.SelectedItem.ToString());
			CurrentImage = lbImageList.SelectedItem.ToString();
		}

		// The user has changed the form of modification they want to use.
		private void lbModification_SelectedIndexChanged(object sender, EventArgs e)
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

		// Populate the Modification Method list box.
		private void PopulateMethods(Modifications modifications)
		{
			lbMethods.Items.Add("Susan Edge Detection"); // index 0
		}

		private void lbMethods_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (lbMethods.SelectedIndex)
			{
				case (int)Methods.none:
					break;
				case (int)Methods.Susan:

					DoSusan();
					break;
				default:
					break;
			}
		}

		#region Susan Edge Detection

		private void DoSusan()
		{
			SusanProperties = new SusanCornerProperties();
			SusanProperties.Dock = DockStyle.Fill;
			ControlPanel.Controls.Add(SusanProperties);
		}

		private void s_ImageComplete(List<IntPoint> Corners)
		{
			if (processing != null)
			{
				if (processing.InvokeRequired)
				{
					ImageCompleteDelegate d = new ImageCompleteDelegate(s_ImageComplete);
					this.Invoke(d, new object[] { Corners });
				}
				else
				{
					processing.Close();
					processing = null;
				}

			}
			Log("Detection Finished");
			SetStatus(Corners.Count + " corners detected", true);
			DrawCorners(Corners);
		}

		#endregion

		private void DrawCorners(List<IntPoint> Corners)
		{
			// Load image and create everything you need for drawing
			Bitmap image = new Bitmap(pbImage.Image);
			Graphics graphics = Graphics.FromImage(image);
			SolidBrush brush = new SolidBrush(Color.Red);
			Pen pen = new Pen(brush);


			// Visualization: Draw 3x3 boxes around the corners
			foreach (IntPoint corner in Corners)
			{
				graphics.DrawRectangle(pen, corner.X - 1, corner.Y - 1, 3, 3);
			}

			// Display
			pbImage.Image = image;
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

				}
			}
		}

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
					SusanCornerProperties sp = ControlPanel.Controls[0] as SusanCornerProperties;
					
					processing = new FrmProcessing("Conducting Susan Edge Detection");
					processing.Show();
					Log("Conducting Susan Edge Detection");
					SetStatus("Please wait for edge detection");
					Susan s = new Susan(CurrentImage);
					s.ImageComplete += s_ImageComplete;
					Task t = new Task(() => s.GetCorners(sp.DifferenceThreshold,sp.GeometricalThreshold));
					t.Start();
					
					break;
				default:
					break;
			}
		}



	}
}
