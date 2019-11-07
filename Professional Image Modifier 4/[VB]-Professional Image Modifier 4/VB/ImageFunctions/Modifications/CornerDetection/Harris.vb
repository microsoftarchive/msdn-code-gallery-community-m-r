Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Accord.Imaging
Imports AForge

Namespace ImageFunctions.Modifications.CornerDetection
	Friend Class Harris

		Private CurrentImage As String

		Public Delegate Sub ImageCompleteHandler(ByVal Corners As List(Of IntPoint))
		Public Event ImageComplete As ImageCompleteHandler

		Public Sub New(ByVal CurrentImage As String)
			Me.CurrentImage = CurrentImage
		End Sub

		Public Sub GetCorners(ByVal threshold As Integer, ByVal sigma As Integer)
			' create corners detector's instance
			Dim hcd As New HarrisCornersDetector(HarrisCornerMeasure.Noble,threshold,sigma)

			' Apply the filter and return the points
			Dim corners As List(Of IntPoint) = hcd.ProcessImage(AForge.Imaging.Image.FromFile(Me.CurrentImage))
			RaiseEvent ImageComplete(corners)
		End Sub

	End Class
End Namespace
