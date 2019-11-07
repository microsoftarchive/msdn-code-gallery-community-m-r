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
	Partial Public Class FASTCornerProperties
		Inherits UserControl

		Public Property SupressDefault() As Boolean
		Public Property ThresholdDefault() As Integer
		Public Property Supress() As Boolean
		Public Property Threshold() As Integer

		''' <summary>
		''' The Defaults are not real defaults - you should investigate appropriate defaults for Threshold and Sigma
		''' </summary>
		Public Sub New()
			InitializeComponent()
			ThresholdDefault = 40
			SupressDefault = True
			trackThreshold.Value = ThresholdDefault
			cbSupress.Checked = SupressDefault
		End Sub

		Public Sub SetDefaults()
			trackThreshold.Value = ThresholdDefault
			cbSupress.Checked = SupressDefault
		End Sub


		Private Sub trackThreshold_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trackThreshold.ValueChanged
			lblThreshold.Text = trackThreshold.Value.ToString()
			Threshold = trackThreshold.Value
		End Sub

		Private Sub cbSupress_CheckStateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbSupress.CheckStateChanged
			Supress = cbSupress.Checked
		End Sub
	End Class
End Namespace
