Public Class frmMainForm

    Private Sub cmdFromDatabase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFromDatabase.Click
        Dim f As New frmAccessForm

        Try
            f.ActiveControl = f.DataGridView1
            f.ShowDialog()
        Finally
            f.Dispose()
        End Try
    End Sub
    Private Sub cmdFromTextFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFromTextFile.Click
        Dim f As New frmTextFileForm

        Try
            f.ActiveControl = f.DataGridView1
            f.ShowDialog()
        Finally
            f.Dispose()
        End Try
    End Sub
    Private Sub cmdListBoxExample_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdListBoxExample.Click
        Dim f As New frmListBoxForm

        Try
            f.ActiveControl = f.ListBox1
            f.ShowDialog()
        Finally
            f.Dispose()
        End Try
    End Sub
End Class