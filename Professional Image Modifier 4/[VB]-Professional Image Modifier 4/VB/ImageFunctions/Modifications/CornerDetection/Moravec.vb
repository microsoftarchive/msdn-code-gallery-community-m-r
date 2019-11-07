Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports AForge
Imports AForge.Imaging
Imports ImageFunctions.Forms

Namespace ImageFunctions.Modifications.CornerDetection
	Friend Class Moravec
		Private CurrentImage As String

		Public Delegate Sub ImageCompleteHandler(ByVal Corners As List(Of IntPoint))
		Public Event ImageComplete As ImageCompleteHandler

		Public Delegate Sub LogMessageHandler(ByVal Message As String)
		Public Event LogMessage As LogMessageHandler

		Public Sub New(ByVal CurrentImage As String)
			Me.CurrentImage = CurrentImage
		End Sub

		Public Sub GetCorners(ByVal threshold As Integer, ByVal window As Integer)
			Dim mcd As MoravecCornersDetector
			' create corners detector's instance
			If IsOdd(window) Then
				mcd = New MoravecCornersDetector(threshold, window)
			Else
				mcd = New MoravecCornersDetector(threshold, window - 1) ' make it an odd number
				RaiseEvent LogMessage("Changed Window Size to: " & (window - 1).ToString() & " Window Size must be odd!")
			End If

			' process image searching for corners
			Dim corners As List(Of IntPoint) = mcd.ProcessImage(AForge.Imaging.Image.FromFile(Me.CurrentImage))
			RaiseEvent ImageComplete(corners)
		End Sub

		' Returns true if the integer supplied is Odd
		Private Function IsOdd(ByVal CheckNumber As Integer) As Boolean
			Return CheckNumber Mod 2 <> 0
		End Function

	End Class
End Namespace
