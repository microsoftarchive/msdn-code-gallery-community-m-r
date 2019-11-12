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
	Partial Public Class SusanCornerProperties
		Inherits UserControl

		Public Property GeometricalDefault() As Integer
		Public Property DifferenceDefault() As Integer
		Public Property GeometricalThreshold() As Integer
		Public Property DifferenceThreshold() As Integer

		Public Sub New()
			InitializeComponent()
			GeometricalDefault = 18
			DifferenceDefault = 25
			trackDifferenceThreshold.Value = DifferenceDefault
			trackGeometricalThreshold.Value = GeometricalDefault
		End Sub


		Private Sub trackGeometricalThreshold_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trackGeometricalThreshold.ValueChanged
			lblGeometricalThreshold.Text = trackGeometricalThreshold.Value.ToString()
			GeometricalThreshold = trackGeometricalThreshold.Value
		End Sub

		Private Sub trackDifferenceThreshold_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trackDifferenceThreshold.ValueChanged
			lblDifferenceThreshold.Text = trackDifferenceThreshold.Value.ToString()
			DifferenceThreshold = trackDifferenceThreshold.Value
		End Sub

		Public Sub SetDefaults()
			trackDifferenceThreshold.Value = DifferenceDefault
			trackGeometricalThreshold.Value = GeometricalDefault
		End Sub
	End Class
End Namespace
