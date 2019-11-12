Public Class FrmControls

    Private Sub FrmControls_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer = 0
        For i = 0 To My.Computer.FileSystem.Drives.Count - 1
            Me.ListBox1.Items.Add(My.Computer.FileSystem.Drives.Item(i).Name)
        Next
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        'Me.TextBox1.Text = My.Computer.FileSystem.
    End Sub
End Class