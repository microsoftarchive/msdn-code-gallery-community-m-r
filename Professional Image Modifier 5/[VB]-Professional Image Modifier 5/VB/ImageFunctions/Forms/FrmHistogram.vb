Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports AForge.Imaging
Imports WeifenLuo.WinFormsUI.Docking

Namespace ImageFunctions.Forms
	Partial Public Class FrmHistogram
		Inherits DockContent

		Public Delegate Sub HistogramLog(ByVal Message As String)
		Public Event histogramLog_Renamed As HistogramLog
		Public Delegate Sub HistogramCompleted(ByVal Message As String)
		Public Event histogramCompleted_Renamed As HistogramCompleted

		Private swHistogram As New Stopwatch()
		Private Property IsHorizontalIntensity() As Boolean
		Private IsSpline As Boolean = False
		Private IsSplineArea As Boolean = True ' Default
		Private IsSplineRange As Boolean = False
		Private IsArea As Boolean = False
		Private IsBar As Boolean = False

		Private CurrentImage As String = Nothing

		Private chart As New System.Windows.Forms.DataVisualization.Charting.Chart()

		Public Sub New()
			InitializeComponent()
			chart.Dock = DockStyle.Fill
			chart.BackColor = Color.LightYellow
			chart.ChartAreas.Add("Default")
			Me.Controls.Add(chart)
			IsHorizontalIntensity = True ' Default.
		End Sub

		Protected Overrides Function GetPersistString() As String
			Return Me.Text

		End Function

		#Region "Histogram"

		''' <summary>
		''' Build the Histogram for Supplied Image
		''' </summary>
		''' <param name="image">
		''' String: Path to image that histogram is to be created out of
		''' </param>
		Public Sub DoHistogram(ByVal image As String)
			CurrentImage = image ' Used for re-generating the histogram
			Dim IsGrayScale As Boolean = AForge.Imaging.Image.IsGrayscale(New Bitmap(image))
'INSTANT VB NOTE: In the following line, Instant VB substituted 'Object' for 'dynamic' - this will work in VB with Option Strict Off:
			Dim IntensityStatistics As Object = Nothing ' Use dynamic (a little like var) to assign this variable which maybe of different types.

			swHistogram.Reset()
			swHistogram.Start()
			RaiseEvent histogramLog_Renamed("Creating Histogram")

			Dim grayhist As AForge.Math.Histogram
			Dim Redhist As AForge.Math.Histogram
			Dim Greenhist As AForge.Math.Histogram
			Dim Bluehist As AForge.Math.Histogram
			' collect statistics
			'NOTE: We have to use the braces on these statements see: http://stackoverflow.com/questions/2496589/variable-declarations-following-if-statements
			If IsHorizontalIntensity Then
				RaiseEvent histogramLog_Renamed("Using HorizontalIntensityStatistics")
				IntensityStatistics = New HorizontalIntensityStatistics(New Bitmap(image))
			Else
				RaiseEvent histogramLog_Renamed("Using VerticalIntensityStatistics")
				IntensityStatistics = New VerticalIntensityStatistics(New Bitmap(image))
			End If

			' get gray histogram (for grayscale image)
			If IsGrayScale Then
				grayhist = IntensityStatistics.Gray
				'TODO: DoGrayHistogram();
				RaiseEvent histogramLog_Renamed("Grayscale Histogram")
			Else

				Redhist = IntensityStatistics.Red
				Greenhist = IntensityStatistics.Green
				Bluehist = IntensityStatistics.Blue
				DoRGBHistogram(Redhist, Greenhist, Bluehist)
				RaiseEvent histogramLog_Renamed("RGB Histogram")
			End If

			swHistogram.Stop()
			RaiseEvent histogramCompleted_Renamed("Histogram built in " & swHistogram.Elapsed)

		End Sub

		''' <summary>
		''' Draws the Histogram on the Chart
		''' </summary>
		''' <param name="RedHist"></param>
		''' <param name="GreenHist"></param>
		''' <param name="BlueHist"></param>
		Private Sub DoRGBHistogram(ByVal RedHist As AForge.Math.Histogram, ByVal GreenHist As AForge.Math.Histogram, ByVal BlueHist As AForge.Math.Histogram)

			chart.Series.Clear()
			chart.Series.Add("Red")
			chart.Series.Add("Green")
			chart.Series.Add("Blue")

			' Set SplineArea chart type
			SelectChartType()


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

			lblRedMin.Text = RedHist.Min.ToString("N0")
			lblRedMean.Text = RedHist.Mean.ToString("N0")
			lblRedMax.Text = RedHist.Max.ToString("N0")

			lblGreenMin.Text = GreenHist.Min.ToString("N0")
			lblGreenMean.Text = GreenHist.Mean.ToString("N0")
			lblGreenMax.Text = GreenHist.Max.ToString("N0")

			lblBlueMin.Text = BlueHist.Min.ToString("N0")
			lblBlueMean.Text = BlueHist.Mean.ToString("N0")
			lblBlueMax.Text = BlueHist.Max.ToString("N0")

		End Sub

		#End Region

		#Region "Horizontal or Vertical Intensity"
		''' <summary>
		''' If the Horizontal Intensity is checked then set the IsHorizontalIntensity to true
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub horizontalIntensityMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles horizontalIntensityMenuItem.Click
			ToggleMenu()
			If horizontalIntensityMenuItem.Checked Then
				IsHorizontalIntensity = True
			End If
			DoHistogram(CurrentImage)
		End Sub

		''' <summary>
		''' The two options Horizontal & Vertical are Mutually exclusive this make sure that both options are not checked at the same time
		''' One is checked as default - the other is not, so they will swap around.
		''' </summary>
		Private Sub ToggleMenu()
			horizontalIntensityMenuItem.Checked = Not horizontalIntensityMenuItem.Checked ' Toggle this
			verticalIntensityMenuItem.Checked = Not verticalIntensityMenuItem.Checked ' Toggle this
		End Sub

		''' <summary>
		''' If the Horizontal Intensity is checked then set the IsHorizontalIntensity to False
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub verticalIntensityMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles verticalIntensityMenuItem.Click
			ToggleMenu()
			If verticalIntensityMenuItem.Checked Then
				IsHorizontalIntensity = False
			End If
			DoHistogram(CurrentImage)
		End Sub
		#End Region

		#Region "Pixel Colour Under Mouse"
		Public Sub SetPixelColour(ByVal colour As Color)
			btnPixelColour.BackColor = colour
		End Sub

		Public Sub SetMouseCoordinates(ByVal mouse As Point)
			lbCoordinatesX.Text = mouse.X.ToString()
			lblCoordinatesY.Text = mouse.Y.ToString()
		End Sub
		#End Region

		#Region "Display Histogram Types"
		''' <summary>
		''' Decide which Graph type to display
		''' </summary>
		Private Sub SelectChartType()
			If IsSplineArea Then
				chart.Series("Red").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
				chart.Series("Green").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
				chart.Series("Blue").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea
			ElseIf IsSpline Then
				chart.Series("Red").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
				chart.Series("Green").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
				chart.Series("Blue").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline
			ElseIf IsSplineRange Then
				chart.Series("Red").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineRange
				chart.Series("Green").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineRange
				chart.Series("Blue").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineRange
			ElseIf IsArea Then
				chart.Series("Red").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area
				chart.Series("Green").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area
				chart.Series("Blue").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area
			ElseIf IsBar Then ' Bar needs Horizontal Orientation.
				chart.Series("Red").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
				chart.Series("Green").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
				chart.Series("Blue").ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar

			Else
				RaiseEvent histogramLog_Renamed("Unknown Histogram Type")
			End If
		End Sub

		' Because the Chart type has been changed - re-do Histogram
		Private Sub DoChartTypes()
			IsSpline = splineToolStripMenuItem.Checked
			IsSplineArea = splineAreaToolStripMenuItem.Checked
			IsSplineRange = splineRangeToolStripMenuItem.Checked
			IsArea = AreaStripMenuItem.Checked
			IsBar = barToolStripMenuItem.Checked
			DoHistogram(CurrentImage)
		End Sub
		#End Region

		#Region "Chart Types Menu Events"
		Private Sub splineAreaToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles splineAreaToolStripMenuItem.Click
			splineAreaToolStripMenuItem.Checked = True
			splineToolStripMenuItem.Checked = False
			splineRangeToolStripMenuItem.Checked = False
			AreaStripMenuItem.Checked = False
			barToolStripMenuItem.Checked = False
			DoChartTypes()
		End Sub

		Private Sub splineRangeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles splineRangeToolStripMenuItem.Click
			splineAreaToolStripMenuItem.Checked = False
			splineToolStripMenuItem.Checked = False
			splineRangeToolStripMenuItem.Checked = True
			AreaStripMenuItem.Checked = False
			barToolStripMenuItem.Checked = False
			DoChartTypes()
		End Sub

		Private Sub splineToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles splineToolStripMenuItem.Click
			splineAreaToolStripMenuItem.Checked = False
			splineToolStripMenuItem.Checked = True
			splineRangeToolStripMenuItem.Checked = False
			AreaStripMenuItem.Checked = False
			barToolStripMenuItem.Checked = False
			DoChartTypes()
		End Sub

		Private Sub AreaStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AreaStripMenuItem.Click
			splineAreaToolStripMenuItem.Checked = False
			splineToolStripMenuItem.Checked = False
			splineRangeToolStripMenuItem.Checked = False
			AreaStripMenuItem.Checked = True
			barToolStripMenuItem.Checked = False
			DoChartTypes()
		End Sub

		Private Sub barToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles barToolStripMenuItem.Click
			splineAreaToolStripMenuItem.Checked = False
			splineToolStripMenuItem.Checked = False
			splineRangeToolStripMenuItem.Checked = False
			AreaStripMenuItem.Checked = False
			barToolStripMenuItem.Checked = True
			DoChartTypes()
		End Sub
		#End Region





	End Class
End Namespace
