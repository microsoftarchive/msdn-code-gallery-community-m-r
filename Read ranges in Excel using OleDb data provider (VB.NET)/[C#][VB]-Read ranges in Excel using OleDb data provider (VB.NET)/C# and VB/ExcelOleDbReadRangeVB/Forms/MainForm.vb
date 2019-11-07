Public Class MainForm
    Private Sub cmdColumnExtension_Click(sender As Object, e As EventArgs) Handles cmdColumnExtension.Click
        Dim f As New ColumnExtensionForm
        Try
            f.ShowDialog()
        Finally
            f.Dispose()
        End Try
    End Sub

    Private Sub cmdGetRange_Click(sender As Object, e As EventArgs) Handles cmdGetRange.Click
        Dim f As New ReadRangeForm()
        Try
            f.ShowDialog()
        Finally
            f.Dispose()
        End Try
    End Sub
End Class