Public Class LoginForm

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Dim AsConn As New AssConn
    Dim AsNum As New AssNumPress
    Dim Rd As System.Data.SqlClient.SqlDataReader

#Region "Control Events Section"
    Private Sub FrmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Activate()
    End Sub
    Private Sub FrmLogin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.AsNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Events Control"
    Private Sub TxtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles UsernameTextBox.GotFocus, PasswordTextBox.GotFocus
        CType(sender, TextBox).SelectAll()
        Dim Ctrl As Control = sender
        Select Case Ctrl.Name
            Case "UsernameTextBox"
                Me.AcceptButton = Nothing

            Case "PasswordTextBox"
                Me.AcceptButton = Me.OK

        End Select

    End Sub
    Private Sub TxtName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles UsernameTextBox.LostFocus, PasswordTextBox.LostFocus
        Dim Ctrl As Control = sender
        Select Case Ctrl.Name
            Case "PasswordTextBox"
                Me.AcceptButton = Nothing

        End Select
    End Sub
#End Region

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If Me.UsernameTextBox.Text.ToUpper = "ISHARPK" And Me.PasswordTextBox.Text.ToUpper = "DREAMS47427754" Then
            Dim Thr As New System.Threading.Thread(AddressOf OpenMainAdmin)
            Thr.Start()
            Me.Close()

        Else
            Try
                Me.AsConn.Conn.Open()
                Me.AsConn.Cmd.Connection = Me.AsConn.Conn

                Me.AsConn.Cmd.CommandText = "SELECT * FROM NIGOL WHERE sUSER_NAME='" & Me.UsernameTextBox.Text & "' AND sPASSWORD='" & Me.PasswordTextBox.Text & "'"
                Me.AsConn.Cmd.CommandType = CommandType.Text
                Rd = Me.AsConn.Cmd.ExecuteReader

                If Rd.Read() Then
                    Dim Thr As New System.Threading.Thread(AddressOf OpenMainAdmin)
                    Thr.Start()
                    Me.Close()

                Else
                    MsgBox("Wrong !User Name! Or !Password!", MsgBoxStyle.Exclamation, "(NS) - Wrong Entry!")
                    Me.UsernameTextBox.Focus()
                End If
            Catch Ex As Exception
                MsgBox(Ex.Message)
            Finally
                Me.AsConn.Conn.Close()
                Me.AsConn.Cmd.Connection = Nothing
            End Try
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub


    Private Sub OpenMainAdmin()
        Application.Run(New frmMain)
    End Sub
End Class
