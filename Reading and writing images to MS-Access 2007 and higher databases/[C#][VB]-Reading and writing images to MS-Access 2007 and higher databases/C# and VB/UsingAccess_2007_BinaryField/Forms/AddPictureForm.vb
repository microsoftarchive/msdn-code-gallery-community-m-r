Public Class frmAddPictureForm
    Public Property FileName As String = ""
    Private Sub AddPictureForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OpenFileDialog1.InitialDirectory = IO.Path.Combine(Application.StartupPath, "Images")

        If My.User.Name.Contains("Karen") Then
            OpenFileDialog1.InitialDirectory = IO.Path.Combine(Application.StartupPath, "Images\Flowers")
            OpenFileDialog1.FilterIndex = 4
        End If

    End Sub
    Private Sub cmdSelectPicture_Click(sender As Object, e As EventArgs) Handles cmdSelectPicture.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.FileName = OpenFileDialog1.FileName
            PictureBox1.Load(OpenFileDialog1.FileName)
            txtFullFileName.Text = Me.FileName
            If My.User.Name.Contains("Karen") Then
                txtDescription.Text = IO.Path.GetFileNameWithoutExtension(Me.FileName)
            End If
        End If
    End Sub
    Private Sub cmdContinueAddingPicture_Click(sender As Object, e As EventArgs) Handles cmdContinueAddingPicture.Click

        If cboCategories.SelectedIndex = 0 Then
            MessageBox.Show("Please select a category")
            Exit Sub
        End If
        If PictureBox1.Image Is Nothing Then
            MessageBox.Show("Please select a image")
            Exit Sub
        End If

        DialogResult = Windows.Forms.DialogResult.OK

    End Sub
End Class