Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.IO
Imports System.Drawing.Imaging

Namespace ImageFunctions.Controls
	Partial Public Class ThumbnailControl
		Inherits UserControl

		Public Delegate Sub ThumbnailSelectedHandler(ByVal PathToMedia As String)
		Public Event ThumbnailSelected As ThumbnailSelectedHandler

		Private CurrentImage As String

		Public Sub New(ByVal ImagePath As String)
			InitializeComponent()

			CurrentImage = ImagePath
			Dim finfo As New FileInfo(ImagePath)
			Dim img As Image = Image.FromFile(ImagePath)
			pbImage.Image = FixedSize(img, 100, 100)
			lblName.Text = Path.GetFileName(ImagePath)
			lblSize.Text = finfo.Length.ToString("N0") & " bytes"
		End Sub

		Private Shared Function FixedSize(ByVal imgPhoto As Image, ByVal Width As Integer, ByVal Height As Integer) As Image
			Dim sourceWidth As Integer = imgPhoto.Width
			Dim sourceHeight As Integer = imgPhoto.Height
			Dim sourceX As Integer = 0
			Dim sourceY As Integer = 0
			Dim destX As Integer = 0
			Dim destY As Integer = 0

			Dim nPercent As Single = 0
			Dim nPercentW As Single = 0
			Dim nPercentH As Single = 0

			nPercentW = (CSng(Width) / CSng(sourceWidth))
			nPercentH = (CSng(Height) / CSng(sourceHeight))
			If nPercentH < nPercentW Then
				nPercent = nPercentH
				destX = System.Convert.ToInt16((Width - (sourceWidth * nPercent)) / 2)
			Else
				nPercent = nPercentW
				destY = System.Convert.ToInt16((Height - (sourceHeight * nPercent)) / 2)
			End If

			Dim destWidth As Integer = CInt(Math.Truncate(sourceWidth * nPercent))
			Dim destHeight As Integer = CInt(Math.Truncate(sourceHeight * nPercent))

			Dim bmPhoto As New Bitmap(Width, Height, PixelFormat.Format24bppRgb)
			bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)

			Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
			grPhoto.Clear(Color.Transparent)
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic

			grPhoto.DrawImage(imgPhoto, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel)

			grPhoto.Dispose()
			Return bmPhoto
		End Function

		Private Sub pbImage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbImage.Click
			RaiseEvent ThumbnailSelected(Me.CurrentImage)
		End Sub

		Private Sub pbImage_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles pbImage.MouseEnter
			Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		End Sub

		Private Sub pbImage_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles pbImage.MouseLeave
			Me.BorderStyle = System.Windows.Forms.BorderStyle.None
		End Sub

		Private Sub pbImage_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbImage.MouseClick
			RaiseEvent ThumbnailSelected(Me.CurrentImage)
		End Sub
	End Class
End Namespace
