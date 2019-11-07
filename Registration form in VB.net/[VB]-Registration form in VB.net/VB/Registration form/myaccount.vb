Public Class myaccount

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = My.Settings.username
        Label2.Text = My.Settings.country
        PictureBox1.ImageLocation = My.Settings.avatar()

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
