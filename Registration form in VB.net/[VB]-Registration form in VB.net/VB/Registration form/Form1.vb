Public Class Form1

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox4.Text = My.Settings.Username And TextBox5.Text = My.Settings.Password Then
            Myaccount.show()


        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles signupButton1.Click
        If Not TextBox3.Text = TextBox2.Text Then
            Label7.Text = "password not mached "
        Else
            Label7.Text = "password mached"

        End If
        Try

            My.Settings.Username = TextBox1.Text
            My.Settings.Password = TextBox2.Text
            My.Settings.Re_Password = TextBox3.Text
            My.Settings.Country = ComboBox1.SelectedItem.ToString()
            My.Settings.Avatar = OpenFile.FileName
            My.Settings.Statuas = False
            My.Settings.Save()


            MsgBox("بە سەرکەوتوویی خوت تومار کرد")
            Application.Restart()

        Catch ex As Exception
            MsgBox("تکا بە ووریایی زانیاریەکان پر بکەرەوە")
        End Try

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try

  
        OpenFile.Title = "open picture"
        OpenFile.FileName = ".png"
        OpenFile.Filter = "all failes|*.*"
        OpenFile.ShowDialog()
        PictureBox1.Image = System.Drawing.Image.FromFile(OpenFile.FileName)

        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        End

    End Sub
End Class
