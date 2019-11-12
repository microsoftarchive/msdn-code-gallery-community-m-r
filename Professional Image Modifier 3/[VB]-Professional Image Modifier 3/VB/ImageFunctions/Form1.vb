Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports ImageFunctions.Modifications.CornerDetection
Imports AForge
Imports ImageFunctions.Forms
Imports System.Threading.Tasks
Imports AForge.Imaging.Filters
Imports ImageFunctions.Controls
Imports AForge.Imaging

Imports System.Diagnostics
Imports System.Windows.Forms.DataVisualization

Namespace ImageFunctions
	Partial Public Class Form1
		Inherits Form

		Private processing As FrmProcessing = Nothing
		Private ssw As Stopwatch ' Used for timing functions. (good for testing changes in optimisation) Susan StopWatch
		Private smw As Stopwatch ' Used for timing functions. (good for testing changes in optimisation) Moravec StopWatch
		Private hsw As Stopwatch ' Used for timing functions. (good for testing changes in optimisation) Histogram StopWatch


		Private chart As New System.Windows.Forms.DataVisualization.Charting.Chart()

		#Region "CrossThread Delegation"
		Private Delegate Sub LogDelegate(ByVal message As String)
		Private Delegate Sub StatusDelegate(ByVal status As String, ByVal Show As Boolean)
		Private Delegate Sub ImageCompleteDelegate(ByVal Corners As List(Of IntPoint))
		Private Delegate Sub HistogramDelegate(ByVal RedHist As AForge.Math.Histogram, ByVal GreenHist As AForge.Math.Histogram, ByVal BlueHist As AForge.Math.Histogram)
		#End Region

		#Region "Private Variables"
		Private CurrentImage As String = Nothing
		#End Region

		#Region "Controls"
		Private SusanProperties As SusanCornerProperties
		Private MoravecProperties As MoravecCornerProperties
		#End Region

		#Region "Enums"
		Private Enum Modifications As Integer
			None
			CornerDetection
		End Enum

		Private Enum Methods As Integer
			none
			Susan ' Corner Detection
			Moravec ' Corner Detection
		End Enum

		Private Enum RGB As Integer
			Red
			Green
			Blue
		End Enum
		#End Region


		Public Sub New()
			InitializeComponent()
			lbModification.Items.Add("Corner Detection")
			chart.Dock = DockStyle.Fill
			chart.BackColor = Color.LightYellow
			chart.ChartAreas.Add("Default")
			tpHistogram.Controls.Add(chart)
		End Sub

		' Load new Image(s) into the listbox and display the first image
		Private Sub btnLoadNewImage_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadNewImage.Click
			Dim firstImage As String = Nothing
			Dim dr As DialogResult = openFile.ShowDialog()
			If dr = System.Windows.Forms.DialogResult.OK Then
				For Each file As String In openFile.FileNames
					If firstImage Is Nothing Then
						firstImage = file
					End If
					lbImageList.Items.Add(file)

				Next file
				pbImage.Image = AForge.Imaging.Image.FromFile(firstImage)
				CurrentImage = firstImage
				lbModification.Enabled = True
				Dim tHistogram As New Task(Sub() DoAForgeHistogram())
				tHistogram.Start()
			End If
		End Sub

		#Region "Controls"
		#Region "Histogram"

		Private Sub DoAForgeHistogram()
			hsw = New Stopwatch()
			hsw.Start()
			Log("Building Histogram")

			Dim grayhist As AForge.Math.Histogram
			Dim Redhist As AForge.Math.Histogram
			Dim Greenhist As AForge.Math.Histogram
			Dim Bluehist As AForge.Math.Histogram
			' collect statistics
			Dim his As New HorizontalIntensityStatistics(New Bitmap(pbImage.Image))
			' get gray histogram (for grayscale image)
			If his.IsGrayscale Then
				grayhist = his.Gray
			Else
				Redhist = his.Red
				Greenhist = his.Green
				Bluehist = his.Blue
				DoRGBHistogram(Redhist, Greenhist, Bluehist)
			End If
			'	AForgeHistogram.Values = hist.Values;

			' output some histogram's information
			'Log("Histogram Mean = " + hist.Mean);
			'Log("Histogram Min = " + hist.Min);
			'Log("Histogram Max = " + hist.Max);

			hsw.Stop()
			Log("Histogram built in " & hsw.Elapsed)
		End Sub

		Private Sub DoRGBHistogram(ByVal RedHist As AForge.Math.Histogram, ByVal GreenHist As AForge.Math.Histogram, ByVal BlueHist As AForge.Math.Histogram)

			If chart.InvokeRequired Then
				Dim d As New HistogramDelegate(AddressOf DoRGBHistogram)
				Me.Invoke(d, New Object() { RedHist, GreenHist, BlueHist })
			Else
				' Decide which set of values are placed at back, in the middle and to the front of the graph.
				Dim lis As New List(Of Double)()
				lis.Add(RedHist.Mean)
				lis.Add(GreenHist.Mean)
				lis.Add(BlueHist.Mean)
				lis.Sort()

				Try
					chart.Series.Add("Red")
					chart.Series.Add("Green")
					chart.Series.Add("Blue")

					' Set SplineArea chart type
					chart.Series("Red").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
					chart.Series("Green").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
					chart.Series("Blue").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
					' set line tension
					chart.Series("Red")("LineTension") = "0.8"
					chart.Series("Green")("LineTension") = "0.8"
					chart.Series("Blue")("LineTension") = "0.8"
					' Set colour and transparency
					chart.Series("Red").Color = Color.FromArgb(50, Color.Red)
					chart.Series("Green").Color = Color.FromArgb(50, Color.Green)
					chart.Series("Blue").Color = Color.FromArgb(50, Color.Blue)
					' Disable X & Y axis labels
					chart.ChartAreas("Default").AxisX.LabelStyle.Enabled = False
					chart.ChartAreas("Default").AxisY.LabelStyle.Enabled = False
					chart.ChartAreas("Default").AxisX.MinorGrid.Enabled = False
					chart.ChartAreas("Default").AxisX.MajorGrid.Enabled = False
					chart.ChartAreas("Default").AxisY.MinorGrid.Enabled = False
					chart.ChartAreas("Default").AxisY.MajorGrid.Enabled = False
				Catch e1 As Exception
					' Throws an exception if it is already created.
				End Try
				chart.Series("Red").Points.Clear()
				chart.Series("Green").Points.Clear()
				chart.Series("Blue").Points.Clear()

				For Each value As Double In RedHist.Values
					chart.Series("Red").Points.AddY(value)
				Next value
				For Each value As Double In GreenHist.Values
					chart.Series("Green").Points.AddY(value)
				Next value
				For Each value As Double In BlueHist.Values
					chart.Series("Blue").Points.AddY(value)
				Next value
			End If
		End Sub

		#End Region

		#End Region

		#Region "Control Toolbar Button Events"

		' Display information on the codecs the system is aware of.
		Private Sub btnSystemCodecInformation_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSystemCodecInformation.Click
			For Each ici As ImageCodecInfo In ImageCodecInfo.GetImageDecoders()
				Log("*************************************************")
				Log("Name: " & ici.CodecName)
				Log("Dll Name: " & ici.DllName)
				Log("Filename Extension(s): " & ici.FilenameExtension)
				'TODO: Work out how to get Flag information.
				'foreach (ImageCodecFlags icf in ici.Flags)
				'{
				'	Log("Codec Flags: " + icf.ToString());
				'}
				Log("Format Description: " & ici.FormatDescription)
				Log("Mime Type: " & ici.MimeType)
				Log("Signature Masks: " & ConvertToString(ici.SignatureMasks))
				Log("Signature Patterns: " & ConvertToString(ici.SignaturePatterns))
				Log("Codec Version: " & ici.Version)
				Log("*************************************************")
			Next ici
		End Sub

		#End Region

		#Region "Utilities"

		' Convert byte[][] to string
		Private Function ConvertToString(ByVal p()() As Byte) As String
			Dim sb As New StringBuilder()
			For Each b As Byte() In p
				sb.Append(System.Text.Encoding.Default.GetString(b))
			Next b
			Return sb.ToString()
		End Function
		#End Region

		#Region "Picturebox Context Menu Events"

		' To reset we just reload the image.
		Private Sub resetToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles resetToolStripMenuItem.Click
			pbImage.Image = AForge.Imaging.Image.FromFile(CurrentImage)
			pbImage.Zoom = 100
		End Sub
		#End Region

		#Region "Listbox operations"

		' Display the picture that the user has selected
		Private Sub listBox1_SelectedIndexChanged_1(ByVal sender As Object, ByVal e As EventArgs)
			pbImage.Image = AForge.Imaging.Image.FromFile(lbImageList.SelectedItem.ToString())
			CurrentImage = lbImageList.SelectedItem.ToString()
		End Sub

		' The user has changed the form of modification they want to use.
		Private Sub lbModification_SelectedItemChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbModification.SelectedItemChanged
			lbMethods.Enabled = True
			Select Case lbModification.SelectedIndex
				Case CInt(Modifications.None)
				Case CInt(Modifications.CornerDetection)
					PopulateMethods(Modifications.CornerDetection)
				Case Else
			End Select
		End Sub

		Private Sub lbMethods_SelectedItemChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbMethods.SelectedItemChanged
			Select Case lbMethods.SelectedIndex
				Case CInt(Methods.none)
				Case CInt(Methods.Susan)
					DoSusan()
				Case CInt(Methods.Moravec)
					DoMoravec()
				Case Else
			End Select
		End Sub

		' Populate the Modification Method list box.
		Private Sub PopulateMethods(ByVal modifications As Modifications)
			Select Case modifications
				Case ImageFunctions.Form1.Modifications.None
					lbMethods.Enabled = False
				Case ImageFunctions.Form1.Modifications.CornerDetection
					lbMethods.Items.Add("Susan Edge Detection") ' index 1
					lbMethods.Items.Add("Moravec Edge Detection") ' index 2
				Case Else
					lbMethods.Enabled = False
			End Select

		End Sub
		#End Region

		#Region "Corners"
		#Region "Moravec Corner Detection"

		Private Sub DoMoravec()
			ControlPanel.Controls.Clear() ' Remove any previous controls that were present
			MoravecProperties = New MoravecCornerProperties()
			MoravecProperties.Dock = DockStyle.Fill
			ControlPanel.Controls.Add(MoravecProperties)
		End Sub

		Private Sub m_ImageComplete(ByVal Corners As List(Of IntPoint))
			ImageCornerDetectionCompleted(Corners, Color.Blue)
		End Sub
		#End Region

		#Region "Susan Corner Detection"

		Private Sub DoSusan()
			ControlPanel.Controls.Clear() ' Remove any previous controls that were present
			SusanProperties = New SusanCornerProperties()
			SusanProperties.Dock = DockStyle.Fill
			ControlPanel.Controls.Add(SusanProperties)
		End Sub

		Private Sub s_ImageComplete(ByVal Corners As List(Of IntPoint))
			ImageCornerDetectionCompleted(Corners, Color.Red)
		End Sub



		#End Region

		Private Sub ImageCornerDetectionCompleted(ByVal Corners As List(Of IntPoint), ByVal colour As Color)
			If Me.processing IsNot Nothing Then
				If Me.processing.InvokeRequired Then
					Dim d As New ImageCompleteDelegate(AddressOf s_ImageComplete)
					Me.Invoke(d, New Object() { Corners })
				Else
					Me.processing.Close()
					Me.processing = Nothing
				End If

			End If
			Log("Detection Finished")
			SetStatus(Corners.Count & " corners detected", True)
			DrawCorners(Corners, colour)
			If ssw IsNot Nothing AndAlso ssw.IsRunning Then
				ssw.Stop()
				Log("Process Duration in Seconds: " & ssw.Elapsed)
			End If
			If smw IsNot Nothing AndAlso smw.IsRunning Then
				smw.Stop()
				Log("Process Duration in Seconds: " & smw.Elapsed)
			End If

		End Sub

		Private Sub DrawCorners(ByVal Corners As List(Of IntPoint), ByVal colour As Color)
			Try
				' Load image and create everything you need for drawing
				Dim image As New Bitmap(pbImage.Image)
				Dim graphics As Graphics = System.Drawing.Graphics.FromImage(image)
				Dim brush As New SolidBrush(colour)
				Dim pen As New Pen(brush)


				' Visualization: Draw 3x3 boxes around the corners
				For Each corner As IntPoint In Corners
					graphics.DrawRectangle(pen, corner.X - 1, corner.Y - 1, 3, 3)
				Next corner

				' Display
				pbImage.Image = image
				graphics.Dispose()
			Catch ex As Exception
				Log("Error in DrawCorners(): " & ex.Message)
			End Try

		End Sub
		#End Region

		#Region "Logging and Status"

		' Moravec Message Event
		Private Sub m_LogMessage(ByVal Message As String)
			Log(Message)
		End Sub

		' Write out information to the console window at the bottom of the app
		Private Sub Log(ByVal p As String)
			If rtbConOut.InvokeRequired Then
				Dim d As New LogDelegate(AddressOf Log)
				Me.Invoke(d, New Object() { p })
			Else
				rtbConOut.AppendText(p & Environment.NewLine)
				rtbConOut.Focus()
				Application.DoEvents() ' ensure updates occur instantly
			End If
		End Sub

		Private Sub SetStatus(ByVal message As String, Optional ByVal Show As Boolean = True)

			If Show Then
				If statusStrip1.InvokeRequired Then
					Dim d As New StatusDelegate(AddressOf SetStatus)
					Me.Invoke(d, New Object() { message, Show })
				Else
					lblStatus.Text = message
					Application.DoEvents() ' ensure updates occur instantly
				End If

			Else
				If statusStrip1.InvokeRequired Then
					Dim d As New StatusDelegate(AddressOf SetStatus)
					Me.Invoke(d, New Object() { message, Show })
				Else
					lblStatus.Text = message
					Application.DoEvents() ' ensure updates occur instantly
				End If
			End If
		End Sub

		#End Region

		#Region "Button Reset and Process"

		Private Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
			Select Case lbMethods.SelectedIndex
				Case CInt(Methods.none)
				Case CInt(Methods.Susan)
					Dim sp As SusanCornerProperties = TryCast(ControlPanel.Controls(0), SusanCornerProperties)
					sp.SetDefaults()
				Case CInt(Methods.Moravec)
					Dim mp As MoravecCornerProperties = TryCast(ControlPanel.Controls(0), MoravecCornerProperties)
					mp.SetDefaults()
				Case Else
			End Select
		End Sub

		Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProcess.Click
			Select Case lbMethods.SelectedIndex
				Case CInt(Methods.none)
				Case CInt(Methods.Susan)
					ssw = New Stopwatch() ' Used for timing functions. (good for testing changes in optimisation)
					Dim sp As SusanCornerProperties = TryCast(ControlPanel.Controls(0), SusanCornerProperties)
					processing = New FrmProcessing("Conducting Susan Corner Detection")
					processing.Show()
					Log("Conducting Susan Corner Detection")
					SetStatus("Please wait for corner detection")
					Dim s As New Susan(CurrentImage)
					AddHandler s.ImageComplete, AddressOf s_ImageComplete
					Dim st As New Task(Sub() s.GetCorners(sp.DifferenceThreshold, sp.GeometricalThreshold))
					ssw.Start()
					st.Start()
				Case CInt(Methods.Moravec)
					smw = New Stopwatch() ' Used for timing functions. (good for testing changes in optimisation)
					Dim mp As MoravecCornerProperties = TryCast(ControlPanel.Controls(0), MoravecCornerProperties)
					processing = New FrmProcessing("Conducting Moravec Corner Detection")
					processing.Show()
					Log("Conducting Moravec Corner Detection")
					SetStatus("Please wait for corner detection")
					Dim m As New Moravec(CurrentImage)
					AddHandler m.ImageComplete, AddressOf m_ImageComplete
					AddHandler m.LogMessage, AddressOf m_LogMessage
					Dim mt As New Task(Sub() m.GetCorners(mp.Threshold, mp.Window))
					smw.Start()
					mt.Start()

				Case Else
			End Select
		End Sub



		#End Region

	End Class
End Namespace
