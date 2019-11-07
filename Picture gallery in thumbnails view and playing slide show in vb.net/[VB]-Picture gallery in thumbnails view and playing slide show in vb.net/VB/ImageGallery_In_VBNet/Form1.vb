Imports System.IO

Public Class Form1
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dlg As New FolderBrowserDialog
        If dlg.ShowDialog = DialogResult.OK Then
            AuthorCodeImageGalleryVB1.Directorypath = dlg.SelectedPath
            TextBox1.Text = dlg.SelectedPath
        End If
    End Sub
End Class
