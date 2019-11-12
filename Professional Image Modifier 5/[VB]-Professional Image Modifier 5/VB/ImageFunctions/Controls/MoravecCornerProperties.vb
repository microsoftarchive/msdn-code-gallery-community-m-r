Imports System.Collections.Generic
Imports System.Windows.Forms

Imports AForge
Imports AForge.Imaging
Imports ImageFunctions.Forms

Namespace ImageFunctions.Controls
	Partial Public Class MoravecCornerProperties
		Inherits UserControl

		Public Property WindowDefault() As Integer
		Public Property ThresholdDefault() As Integer
		Public Property Window() As Integer
		Public Property Threshold() As Integer

		Public Sub New()
			InitializeComponent()
			ThresholdDefault = 500
			WindowDefault = 3
			trackThreshold.Value = ThresholdDefault
			trackWindow.Value = WindowDefault
		End Sub

		Public Sub SetDefaults()
			trackThreshold.Value = ThresholdDefault
			trackWindow.Value = WindowDefault
		End Sub

		Private Sub trackThreshold_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackThreshold.ValueChanged
			lblThreshold.Text = trackThreshold.Value.ToString()
			Threshold = trackThreshold.Value
		End Sub

		Private Sub trackWindow_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trackWindow.ValueChanged
			lblWindow.Text = trackWindow.Value.ToString()
			Window = trackWindow.Value
		End Sub
	End Class
End Namespace
