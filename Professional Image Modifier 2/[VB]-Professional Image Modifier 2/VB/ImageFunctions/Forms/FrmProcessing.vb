Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace ImageFunctions.Forms
	Partial Public Class FrmProcessing
		Inherits Form

		Public Property description() As String

		Public Sub New(ByVal description As String)
			InitializeComponent()
			lblDescription.Text = description

		End Sub


	End Class
End Namespace
