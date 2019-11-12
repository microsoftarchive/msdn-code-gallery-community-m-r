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


Namespace ImageFunctions
	Partial Public Class Form1
		Inherits Form

		Private processing As FrmProcessing = Nothing

		#Region "CrossThread Delegation"
		Private Delegate Sub LogDelegate(ByVal message As String)
		Private Delegate Sub StatusDelegate(ByVal status As String, ByVal Show As Boolean)
		Private Delegate Sub ImageCompleteDelegate(ByVal Corners As List(Of IntPoint))
		#End Region

		#Region "Private Variables"
		Private CurrentImage As String = Nothing
		#End Region

		#Region "Controls"
		Private SusanProperties As SusanCornerProperties
		#End Region

		#Region "Enums"
		Private Enum Modifications As Integer
			None
			CornerDetection
		End Enum

		Private Enum Methods As Integer
			none
			Susan ' Corner Detection
		End Enum
		#End Region


		Public Sub New()
			InitializeComponent()

		End Sub


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

		' Convert byte[][] to string
		Private Function ConvertToString(ByVal p()() As Byte) As String
			Dim sb As New StringBuilder()
			For Each b As Byte() In p
				sb.Append(System.Text.Encoding.Default.GetString(b))
			Next b
			Return sb.ToString()
		End Function

		' Write out information to the console window at the bottom of the app
		Private Sub Log(ByVal p As String)
			If rtbConOut.InvokeRequired Then
				Dim d As New LogDelegate(AddressOf Log)
				Me.Invoke(d, New Object() { p })
			Else
				rtbConOut.AppendText(p & Environment.NewLine)
				rtbConOut.Focus()
				'	Application.DoEvents();
			End If
		End Sub

		' To reset we just reload the image.
		Private Sub resetToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles resetToolStripMenuItem.Click
			pbImage.Image = Image.FromFile(CurrentImage)
			pbImage.Zoom = 100
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
				pbImage.Image = Image.FromFile(firstImage)
				CurrentImage = firstImage
				lbModification.Enabled = True
			End If
		End Sub

		' Display the picture that the user has selected
		Private Sub listBox1_SelectedIndexChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles lbImageList.SelectedIndexChanged
			pbImage.Image = Image.FromFile(lbImageList.SelectedItem.ToString())
			CurrentImage = lbImageList.SelectedItem.ToString()
		End Sub

		' The user has changed the form of modification they want to use.
		Private Sub lbModification_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbModification.SelectedIndexChanged
			lbMethods.Enabled = True
			Select Case lbModification.SelectedIndex
				Case CInt(Modifications.None)
				Case CInt(Modifications.CornerDetection)
					PopulateMethods(Modifications.CornerDetection)
				Case Else
			End Select

		End Sub

		' Populate the Modification Method list box.
		Private Sub PopulateMethods(ByVal modifications As Modifications)
			lbMethods.Items.Add("Susan Edge Detection") ' index 0
		End Sub

		Private Sub lbMethods_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbMethods.SelectedIndexChanged
			Select Case lbMethods.SelectedIndex
				Case CInt(Methods.none)
				Case CInt(Methods.Susan)

					DoSusan()
				Case Else
			End Select
		End Sub

		#Region "Susan Edge Detection"

		Private Sub DoSusan()
			SusanProperties = New SusanCornerProperties()
			SusanProperties.Dock = DockStyle.Fill
			ControlPanel.Controls.Add(SusanProperties)
		End Sub

		Private Sub s_ImageComplete(ByVal Corners As List(Of IntPoint))
			If processing IsNot Nothing Then
				If processing.InvokeRequired Then
					Dim d As New ImageCompleteDelegate(AddressOf s_ImageComplete)
					Me.Invoke(d, New Object() { Corners })
				Else
					processing.Close()
					processing = Nothing
				End If

			End If
			Log("Detection Finished")
			SetStatus(Corners.Count & " corners detected", True)
			DrawCorners(Corners)
		End Sub

		#End Region

		Private Sub DrawCorners(ByVal Corners As List(Of IntPoint))
			' Load image and create everything you need for drawing
			Dim image As New Bitmap(pbImage.Image)
			Dim graphics As Graphics = System.Drawing.Graphics.FromImage(image)
			Dim brush As New SolidBrush(Color.Red)
			Dim pen As New Pen(brush)


			' Visualization: Draw 3x3 boxes around the corners
			For Each corner As IntPoint In Corners
				graphics.DrawRectangle(pen, corner.X - 1, corner.Y - 1, 3, 3)
			Next corner

			' Display
			pbImage.Image = image
		End Sub

		Private Sub SetStatus(ByVal message As String, Optional ByVal Show As Boolean = True)

			If Show Then
				If statusStrip1.InvokeRequired Then
					Dim d As New StatusDelegate(AddressOf SetStatus)
					Me.Invoke(d, New Object() { message, Show })
				Else
					lblStatus.Text = message

				End If

			Else
				If statusStrip1.InvokeRequired Then
					Dim d As New StatusDelegate(AddressOf SetStatus)
					Me.Invoke(d, New Object() { message, Show })
				Else
					lblStatus.Text = message

				End If
			End If
		End Sub

		Private Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
			Select Case lbMethods.SelectedIndex
				Case CInt(Methods.none)
				Case CInt(Methods.Susan)
					Dim sp As SusanCornerProperties = TryCast(ControlPanel.Controls(0), SusanCornerProperties)
					sp.SetDefaults()

				Case Else
			End Select
		End Sub

		Private Sub btnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnProcess.Click
			Select Case lbMethods.SelectedIndex
				Case CInt(Methods.none)
				Case CInt(Methods.Susan)
					Dim sp As SusanCornerProperties = TryCast(ControlPanel.Controls(0), SusanCornerProperties)

					processing = New FrmProcessing("Conducting Susan Edge Detection")
					processing.Show()
					Log("Conducting Susan Edge Detection")
					SetStatus("Please wait for edge detection")
					Dim s As New Susan(CurrentImage)
					AddHandler s.ImageComplete, AddressOf s_ImageComplete
					Dim t As New Task(Sub() s.GetCorners(sp.DifferenceThreshold,sp.GeometricalThreshold))
					t.Start()

				Case Else
			End Select
		End Sub



	End Class
End Namespace
