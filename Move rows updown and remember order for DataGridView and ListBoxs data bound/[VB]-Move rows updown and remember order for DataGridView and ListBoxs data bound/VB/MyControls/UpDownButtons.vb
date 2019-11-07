Imports System.Windows.Forms
Public Class DownButton
    Inherits Windows.Forms.Button

    Public Sub New()
        MyBase.New()
        Image = My.Resources.DnArrow
        Size = New System.Drawing.Size(100, 48)
    End Sub

    Private Sub DownButton_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Text = ""
    End Sub
End Class
Public Class UpButton
    Inherits Windows.Forms.Button

    Public Sub New()
        MyBase.New()
        Image = My.Resources.UpArrow
        Size = New System.Drawing.Size(100, 48)
    End Sub
    Private Sub DownButton_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Text = ""
    End Sub
End Class
