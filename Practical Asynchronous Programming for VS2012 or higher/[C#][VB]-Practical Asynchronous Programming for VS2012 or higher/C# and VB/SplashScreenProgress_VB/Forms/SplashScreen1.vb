Public Class SplashScreen1
    Public Delegate Sub UpdateRecordDelegate(ByVal value As Integer)
    Public Sub ShowRecord(ByVal sender As Integer)
        If Me.InvokeRequired Then
            Me.Invoke(New UpdateRecordDelegate(AddressOf ShowRecord), sender)
        Else
            Me.Label1.Text = sender.ToString & " percent complete"
            Me.ProgressBar1.Value = sender + 1
        End If
    End Sub
    Private Const CP_NOCLOSE_BUTTON As Integer = &H200
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim myCp As CreateParams = MyBase.CreateParams
            myCp.ClassStyle = myCp.ClassStyle Or CP_NOCLOSE_BUTTON
            Return myCp
        End Get
    End Property
End Class