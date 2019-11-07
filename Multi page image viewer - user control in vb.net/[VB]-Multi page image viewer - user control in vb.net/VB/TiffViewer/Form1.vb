Public Class Form1
    Dim TiffImage As Image
    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        LoadImage()
    End Sub
    ' Load tif image into Viewer
    Private Sub LoadImage()
        Dim dlg As New OpenFileDialog
        dlg.Filter = "tif files (*.tif)|*.tif|All files (*.*)|*.*"
        If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then
            TiffViewer1.Image = Image.FromFile(dlg.FileName)
        End If
    End Sub

    Private Sub TiffViewer1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TiffViewer1.Click

    End Sub
End Class
