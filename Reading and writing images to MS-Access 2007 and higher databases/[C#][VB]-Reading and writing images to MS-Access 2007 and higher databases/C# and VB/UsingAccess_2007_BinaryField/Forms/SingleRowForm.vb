''' <summary>
''' Done for Social forum post
''' </summary>
''' <remarks></remarks>
Public Class SingleRowForm
    Private Sub SingleRowForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim da As New DataAccess.Operations
        Dim result = da.LoadSingleImage(10)
        Label1.Text = result.Item1
        PictureBox1.Image = Image.FromStream(New IO.MemoryStream(result.Item2))
    End Sub
End Class