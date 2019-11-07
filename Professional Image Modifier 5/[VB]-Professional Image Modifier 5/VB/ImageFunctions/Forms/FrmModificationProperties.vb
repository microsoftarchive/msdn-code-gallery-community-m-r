Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports ImageFunctions.Controls
Imports WeifenLuo.WinFormsUI.Docking

Imports ImageFunctions.Classes
Imports ImageFunctions.Modifications.CornerDetection
Imports System.Diagnostics
Imports AForge

Namespace ImageFunctions.Forms
	Partial Public Class FrmModificationProperties
		Inherits DockContent

		Public Delegate Sub ModificationPropertiesLogHandler(ByVal Message As String)
		Public Event ModificationPropertiesLog As ModificationPropertiesLogHandler
		Public Delegate Sub ModificationPropertiesModifiedImageHandler(ByVal modifiedImage As Image)
		Public Event UpdateImage As ModificationPropertiesModifiedImageHandler



		#Region "Stopwatches"

		Private swSusan As New Stopwatch()
		Private swMoravec As New Stopwatch()
		Private swHarris As New Stopwatch()
		Private swFast As New Stopwatch()
		#End Region

		#Region "Controls"
		Private sp As SusanCornerProperties
		Private mp As MoravecCornerProperties
		Private hp As HarrisCornerProperties
		Private fp As FASTCornerProperties
		#End Region

		Public Property CurrentImage() As String

		Public Property DetectorType() As String

		Public Sub New()
			InitializeComponent()
		End Sub

		Protected Overrides Function GetPersistString() As String
			Return Me.Text
		End Function

		#Region "FAST Corner Detection"

		Public Sub DoFast()
			DetectorType = "FAST"
			ControlPanel.Controls.Clear()
			fp = New FASTCornerProperties()
			ControlPanel.Controls.Add(fp)

		End Sub

		Private Sub FASTCornerDetection()


			swFast.Reset() ' Used for timing functions. (good for testing changes in optimisation)
			swFast.Start()



			RaiseEvent ModificationPropertiesLog("Conducting FAST Corner Detection")

			Dim f As New FAST(CurrentImage)
			AddHandler f.ImageComplete, AddressOf f_ImageComplete

			Dim ht As New Task(Sub() f.GetCorners(fp.Threshold, fp.Supress))
			ht.Start()
		End Sub

		Private Sub f_ImageComplete(ByVal Corners As List(Of AForge.IntPoint))
			ImageCornerDetectionCompleted(Corners, Color.Cornsilk)
		End Sub
		#End Region

		#Region "Harris Corner Detection"

		Public Sub DoHarris()
			DetectorType = "Harris"
			ControlPanel.Controls.Clear()
			hp = New HarrisCornerProperties()
			ControlPanel.Controls.Add(hp)

		End Sub

		Private Sub HarrisCornerDetection()


			swHarris.Reset() ' Used for timing functions. (good for testing changes in optimisation)
			swHarris.Start()


			RaiseEvent ModificationPropertiesLog("Conducting Harris Corner Detection")

			Dim h As New Harris(CurrentImage)
			AddHandler h.ImageComplete, AddressOf h_ImageComplete

			Dim ht As New Task(Sub() h.GetCorners(hp.Threshold, hp.Sigma))
			ht.Start()
		End Sub

		Private Sub h_ImageComplete(ByVal Corners As List(Of AForge.IntPoint))
			ImageCornerDetectionCompleted(Corners, Color.Blue)
		End Sub
		#End Region

		#Region "Moravec Corner Detection"

		Public Sub DoMoravec()
			DetectorType = "Moravec"
			ControlPanel.Controls.Clear()
			mp = New MoravecCornerProperties()
			ControlPanel.Controls.Add(mp)

		End Sub

		Private Sub MoravecCornerDetection()


			swMoravec.Reset() ' Used for timing functions. (good for testing changes in optimisation)
			swMoravec.Start()


			RaiseEvent ModificationPropertiesLog("Conducting Moravec Corner Detection")

			Dim m As New Moravec(CurrentImage)
			AddHandler m.ImageComplete, AddressOf m_ImageComplete
			AddHandler m.LogMessage, AddressOf m_LogMessage
			Dim mt As New Task(Sub() m.GetCorners(mp.Threshold, mp.Window))
			mt.Start()
		End Sub

		Private Sub m_LogMessage(ByVal Message As String)
			RaiseEvent ModificationPropertiesLog(Message)
		End Sub

		Private Sub m_ImageComplete(ByVal Corners As List(Of AForge.IntPoint))
			ImageCornerDetectionCompleted(Corners, Color.Azure)
		End Sub
		#End Region

		#Region "SUSAN Corner Detection"

		Public Sub DoSusan()
			DetectorType = "SUSAN"
			ControlPanel.Controls.Clear()
			sp = New SusanCornerProperties()
			ControlPanel.Controls.Add(sp)

		End Sub

		Private Sub SusanCornerDetection()


			swSusan.Reset() ' Used for timing functions. (good for testing changes in optimisation)
			swSusan.Start()


			RaiseEvent ModificationPropertiesLog("Conducting Susan Corner Detection")

			Dim s As New Susan(CurrentImage)
			AddHandler s.ImageComplete, AddressOf s_ImageComplete

			Dim st As New Task(Sub() s.GetCorners(sp.DifferenceThreshold, sp.GeometricalThreshold))
			st.Start()
		End Sub

		Private Sub s_ImageComplete(ByVal Corners As List(Of AForge.IntPoint))
			ImageCornerDetectionCompleted(Corners, Color.AliceBlue)
		End Sub
		#End Region

		Private Sub ImageCornerDetectionCompleted(ByVal Corners As List(Of IntPoint), ByVal colour As Color)
			DrawCorners(Corners, colour)

			If swSusan.IsRunning Then
				swSusan.Stop()
				RaiseEvent ModificationPropertiesLog(Corners.Count.ToString("N0") & " SUSAN Corners Detected in " & swSusan.Elapsed)
			ElseIf swHarris.IsRunning Then
				swHarris.Stop()
				RaiseEvent ModificationPropertiesLog(Corners.Count.ToString("N0") & " Harris Corners Detected in " & swHarris.Elapsed)
			ElseIf swMoravec.IsRunning Then
				swMoravec.Stop()
				RaiseEvent ModificationPropertiesLog(Corners.Count.ToString("N0") & " Moravec Corners Detected in " & swMoravec.Elapsed)
			ElseIf swFast.IsRunning Then
				swFast.Stop()
				RaiseEvent ModificationPropertiesLog(Corners.Count.ToString("N0") & " FAST Corners Detected in " & swFast.Elapsed)
			End If
		End Sub

		Private Sub DrawCorners(ByVal Corners As List(Of IntPoint), ByVal colour As Color)
			RaiseEvent ModificationPropertiesLog("Creating corners")
			Try
				' Load image and create everything you need for drawing
				Dim image As New Bitmap(AForge.Imaging.Image.FromFile(CurrentImage))
				Dim graphics As Graphics = System.Drawing.Graphics.FromImage(image)
				Dim brush As New SolidBrush(colour)
				Dim pen As New Pen(brush)


				' Visualization: Draw 3x3 boxes around the corners
				For Each corner As IntPoint In Corners
					graphics.DrawRectangle(pen, corner.X - 1, corner.Y - 1, 3, 3)
				Next corner

				' Display
				RaiseEvent UpdateImage(image)
				graphics.Dispose()
			Catch ex As Exception
				RaiseEvent ModificationPropertiesLog("Error in DrawCorners(): " & ex.Message)
			End Try

		End Sub

		Private Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
			Select Case DetectorType
				Case "SUSAN"
					Dim sp As SusanCornerProperties = TryCast(ControlPanel.Controls(0), SusanCornerProperties)
					sp.SetDefaults()
				Case "Harris"
					Dim hc As HarrisCornerProperties = TryCast(ControlPanel.Controls(0), HarrisCornerProperties)
					hc.SetDefaults()
				Case "Moravec"
					Dim mp As MoravecCornerProperties = TryCast(ControlPanel.Controls(0), MoravecCornerProperties)
					mp.SetDefaults()
				Case "FAST"
					Dim fc As FASTCornerProperties = TryCast(ControlPanel.Controls(0), FASTCornerProperties)
					fc.SetDefaults()
				Case Else
			End Select
		End Sub

		Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProcess.Click
			Select Case DetectorType.ToLowerInvariant()
				Case "susan"
					SusanCornerDetection()
				Case "harris"
					HarrisCornerDetection()
				Case "moravec"
					MoravecCornerDetection()
				Case "fast"
					FASTCornerDetection()
				Case Else
			End Select
		End Sub
	End Class
End Namespace
