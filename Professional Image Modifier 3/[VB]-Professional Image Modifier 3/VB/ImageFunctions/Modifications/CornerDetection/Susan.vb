'Remarks
'The class implements Susan corners detector, which is described by S.M. Smith in: S.M. Smith, "SUSAN - a new approach to low level image processing", 
'Internal Technical Report TR95SMS1, Defense Research Agency, Chobham Lane, Chertsey, Surrey, UK, 1995.
'[Note] Note:Some implementation notes:
'	Analyzing each pixel and searching for its USAN area, the 7x7 mask is used, which is comprised of 37 pixels. The mask has circle shape:
'	Copy 
'	  xxx
'	 xxxxx
'	xxxxxxx
'	xxxxxxx
'	xxxxxxx
'	 xxxxx
'	  xxx
'	In the case if USAN's center of mass has the same coordinates as nucleus (central point), the pixel is not a corner.
'	For noise suppression the 5x5 square window is used.
'The class processes only grayscale 8 bpp and color 24/32 bpp images. In the case of color image, it is converted to grayscale internally using 
'GrayscaleBT709 filter.
' http://www.aforgenet.com/framework/docs/

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports AForge
Imports AForge.Imaging
Imports ImageFunctions.Forms


Namespace ImageFunctions.Modifications.CornerDetection
	Friend Class Susan
		Private CurrentImage As String
		Private processing As FrmProcessing

		Public Delegate Sub ImageCompleteHandler(ByVal Corners As List(Of IntPoint))
		Public Event ImageComplete As ImageCompleteHandler

		Public Sub New(ByVal CurrentImage As String)

			Me.CurrentImage = CurrentImage

		End Sub

		Public Sub GetCorners(ByVal diff As Integer, ByVal geo As Integer)
			' create corners detector's instance
			Dim scd As New SusanCornersDetector(diff, geo)
			' process image searching for corners
			Dim corners As List(Of IntPoint) = scd.ProcessImage(AForge.Imaging.Image.FromFile(Me.CurrentImage))
			RaiseEvent ImageComplete(corners)
		End Sub
	End Class
End Namespace
