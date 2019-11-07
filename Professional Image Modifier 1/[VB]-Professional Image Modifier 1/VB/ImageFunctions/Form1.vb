Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Namespace ImageFunctions
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()

		End Sub




		Private Sub btnLoadNewImage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadNewImage.Click
			Dim dr As DialogResult = openFile.ShowDialog()
			If dr = System.Windows.Forms.DialogResult.OK Then
				For Each file As String In openFile.FileNames
					listBox1.Items.Add(file)
					pbImage.Image = Image.FromFile(file)
				Next file
			End If
		End Sub

		Private Sub listBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles listBox1.SelectedIndexChanged
			If listBox1.SelectedIndex > ilDefault.Images.Count - 1 Then ' Must be added by the user
				pbImage.Image = Image.FromFile(listBox1.SelectedItem.ToString())

			Else ' Must be the default images in the ImageList
				pbImage.Image = ilDefault.Images(listBox1.SelectedIndex)

			End If
		End Sub


	End Class
End Namespace
