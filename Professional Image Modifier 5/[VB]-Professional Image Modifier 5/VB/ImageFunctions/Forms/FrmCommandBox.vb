Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Imports WeifenLuo.WinFormsUI.Docking


Namespace ImageFunctions.Forms
	Partial Public Class FrmCommandBox
		Inherits DockContent

		Public Sub New()
			InitializeComponent()
		End Sub


		Protected Overrides Function GetPersistString() As String
			Return Me.Text
		End Function

		''' <summary>
		''' This produced the flash required to attract attention to this being an active input box
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub timerFlash_Tick_1(ByVal sender As Object, ByVal e As EventArgs) Handles timerFlash.Tick
			If lblFlash.Text = ":" Then
				lblFlash.Text = " "
			Else
				lblFlash.Text = ":"
			End If


		End Sub
	End Class
End Namespace
