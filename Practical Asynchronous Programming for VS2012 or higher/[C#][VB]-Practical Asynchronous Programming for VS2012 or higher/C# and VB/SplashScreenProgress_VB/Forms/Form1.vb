Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim da As New DataAccess(100)
        Console.WriteLine(da.RecordCount)

        For Each item In da.Retrieve
            DataGridView1.Rows.Add(item.ItemArray)
        Next

        ' optional
        DataGridView1.ExpandColumns()
    End Sub
End Class
