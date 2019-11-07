Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace ImageFunctions.Controls
	Partial Public Class HarrisCornerProperties
		Inherits UserControl

		Public Property SigmaDefault() As Integer
		Public Property ThresholdDefault() As Integer
		Public Property Sigma() As Integer
		Public Property Threshold() As Integer

		''' <summary>
		''' The Defaults are not real defaults - you should investigate appropriate defaults for Threshold and Sigma
		''' </summary>
		Public Sub New()
			InitializeComponent()
			ThresholdDefault = 5
			SigmaDefault = 3
			trackThreshold.Value = ThresholdDefault
			trackSigma.Value = SigmaDefault
		End Sub

		Public Sub SetDefaults()
			trackThreshold.Value = ThresholdDefault
			trackSigma.Value = SigmaDefault
		End Sub


		Private Sub trackThreshold_ValueChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles trackThreshold.ValueChanged
			lblThreshold.Text = trackThreshold.Value.ToString()
			Threshold = trackThreshold.Value
		End Sub

		Private Sub trackSigma_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trackSigma.ValueChanged
			lblWindow.Text = trackSigma.Value.ToString()
			Sigma = trackSigma.Value
		End Sub
	End Class
End Namespace
