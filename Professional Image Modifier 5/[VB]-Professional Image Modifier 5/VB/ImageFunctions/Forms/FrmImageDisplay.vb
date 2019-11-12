Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Namespace ImageFunctions.Forms
	Partial Public Class FrmImageDisplay
		Inherits DockContent


		#Region "Properties"
		Public Property IsMediaLoaded() As Boolean
		#End Region

		#Region "Private Variables"
		Private CurrentImage As String
		#End Region

		#Region "Events"

		Public Delegate Sub ImageDisplayMediaLoadedHandler(ByVal MediaPath As String)
		Public Event MediaLoaded As ImageDisplayMediaLoadedHandler
		Public Delegate Sub ImageDisplayMediaLoadingFailedHandler(ByVal ErrorMessage As String)
		Public Event MediaFailedToLoad As ImageDisplayMediaLoadingFailedHandler
		Public Delegate Sub ImageDisplayMediaPixelColourHandler(ByVal colour As Color)
		Public Event MediaPixelColour As ImageDisplayMediaPixelColourHandler
		Public Delegate Sub ImageDisplayMediaPixelCoordinatesHandler(ByVal mouseXY As Point)
		Public Event MediaPixelCoordinates As ImageDisplayMediaPixelCoordinatesHandler
		Public Delegate Sub ImageDisplayLogHandler(ByVal Message As String)
		Public Event ImageDisplayLog As ImageDisplayLogHandler
		#End Region

		''' <summary>
		''' Instantiation Method
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Used by the Main Form to Recognise this form when re-applying previous docking settings when loading.
		''' </summary>
		''' <returns></returns>
		Protected Overrides Function GetPersistString() As String
			Return Me.Text
		End Function

		'TODO: Workout what media this is and process accordingly - for now assume image.
		Public Sub ShowSingleMedia(ByVal MediaPath As String)
			CurrentImage = MediaPath
			Try
				Dim finfo As New FileInfo(MediaPath)
				lblFileSize.Text = ImageFunctions.Classes.Utilities.BytesToString(finfo.Length)
				pbImage.Image = AForge.Imaging.Image.FromFile(MediaPath)
				RaiseEvent MediaLoaded(MediaPath)
				IsMediaLoaded = True

			Catch ex As Exception
				RaiseEvent MediaFailedToLoad(ex.Message)
				IsMediaLoaded = False
			End Try

		End Sub

		Public Sub UpdateImage(ByVal updatedImage As Image)
			pbImage.Image = updatedImage
			IsMediaLoaded = True
			RaiseEvent ImageDisplayLog("Image Updated")
		End Sub

		''' <summary>
		''' Get's the current Pixel colour from under the mouse position
		''' </summary>
		''' <param name="point">
		''' Point: The X,Y Coordinates of the mouse.
		''' </param>
		''' <returns></returns>
		Private Function GetClickedPixel(ByVal point As Point) As Color
			Dim bitmap As Bitmap = CType(pbImage.Image, Bitmap)
			If point.X < bitmap.Width AndAlso point.Y < bitmap.Height Then ' Make sure that if loading a new image and it is smaller than the last image that we do not process the PointXY until the
			' image is adjusted correctly
				Return bitmap.GetPixel(point.X, point.Y)
			End If
			Return Color.Black
		End Function

		''' <summary>
		''' Reset the Image display
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub rToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles rToolStripMenuItem.Click
			pbImage.Image = AForge.Imaging.Image.FromFile(CurrentImage)

		End Sub

		''' <summary>
		''' Mouse Move Event
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e">
		''' Point: e.Location used to indicate where the mouse is within the image
		''' </param>
		Private Sub pbImage_MouseMove_1(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbImage.MouseMove

			pbImage.Cursor = Cursors.Cross ' Force the cursor to remain the cross - it sometimes inexplicably reverts back to Arrow

			If pbImage.Image IsNot Nothing Then ' make sure an image is loaded :P
				Dim realLocationOnImage As Point = pbImage.PointToImage(e.Location) ' This converts the mouse XY coordinates to the real location on the image - as that changes as we pan and zoom.

				RaiseEvent MediaPixelColour(GetClickedPixel(realLocationOnImage))
				RaiseEvent MediaPixelCoordinates(realLocationOnImage)
				lblMouseX.Text = (realLocationOnImage.X).ToString("N0")
				lblMouseY.Text = (realLocationOnImage.Y).ToString("N0")
				btnSelectedColour.BackColor = GetClickedPixel(realLocationOnImage)
			End If
		End Sub

		Private Sub toolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles toolStripMenuItem1.Click
			toolStripMenuItem1.Checked = Not toolStripMenuItem1.Checked ' Toggle the checked box
			pbImage.ShowPixelGrid = toolStripMenuItem1.Checked
			RaiseEvent ImageDisplayLog("Display Pixel Grid: " & toolStripMenuItem1.Checked.ToString())
		End Sub

		''' <summary>
		''' Resets the zoom so the image is displayed at 100%
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub resetZoomToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles resetZoomToolStripMenuItem.Click
			pbImage.Zoom = 100
		End Sub
	End Class
End Namespace
