Public Class frmCHANGE_PASSWORD

    Dim AsNum As New AssNumPress
    Dim AsConn As New AssConn
    Dim Rd As System.Data.SqlClient.SqlDataReader
    Dim bCorrect As Boolean = False

#Region "Control Events Section"
    Private Sub frmCHANGE_PASSWORD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Activate()
    End Sub
    Private Sub frmCHANGE_PASSWORD_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.AsNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Events Control"
    Private Sub TxtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtUserName.GotFocus, TxtOldPassword.GotFocus, TxtNewPassword.GotFocus, TxtConfirmNew.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub TxtName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtUserName.LostFocus, TxtOldPassword.LostFocus, TxtNewPassword.LostFocus, TxtConfirmNew.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnUpdate.Click
        Me.Check_Password()

        If bCorrect = False Then
            MsgBox("Wrong !User Name! Or !Password!", MsgBoxStyle.Exclamation, "(NS) - Wrong Entry!")
            Me.TxtUserName.Focus()
            Exit Sub

        ElseIf Me.TxtNewPassword.Text = Nothing Or Me.TxtConfirmNew.Text = Nothing Then
            MsgBox("Please Enter Password!", MsgBoxStyle.Exclamation, "(NS) - Wrong Entry!")
            If Me.TxtNewPassword.Text = Nothing Then
                Me.TxtNewPassword.Focus()
                Exit Sub

            ElseIf Me.TxtConfirmNew.Text = Nothing Then
                Me.TxtConfirmNew.Focus()
                Exit Sub

            End If

        ElseIf Not Me.TxtNewPassword.Text = Me.TxtConfirmNew.Text Then
            MsgBox("New Password is mis-matched", MsgBoxStyle.Exclamation, "(NS) - Mis-matched Password")
            Me.TxtNewPassword.Focus()

        ElseIf bCorrect = True Then
            Try
                Me.AsConn.Conn2.Open()
                Me.AsConn.Cmd2.Connection = Me.AsConn.Conn2

                Me.AsConn.Cmd2.CommandText = "UPDATE NIGOL SET sPASSWORD='" & Me.TxtNewPassword.Text & "' WHERE sUSER_NAME='" & Me.TxtUserName.Text & "'"
                Me.AsConn.Cmd2.CommandType = CommandType.Text
                Me.AsConn.Cmd2.ExecuteNonQuery()
                MsgBox("New password has been set!", MsgBoxStyle.Information, "(NS) - Password Changed!")

                Me.TxtUserName.Text = Nothing
                Me.TxtOldPassword.Text = Nothing
                Me.TxtNewPassword.Text = Nothing
                Me.TxtConfirmNew.Text = Nothing

            Catch Ex As Exception
                MsgBox(Ex.Message)
            Finally
                Me.AsConn.Conn2.Close()
                Me.AsConn.Cmd2.Connection = Nothing
            End Try
        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Sub & Function Control"
    Private Sub Check_Password()
        Try
            Me.AsConn.Conn.Open()
            Me.AsConn.Cmd.Connection = Me.AsConn.Conn

            Me.AsConn.Cmd.CommandText = "SELECT * FROM NIGOL WHERE sUSER_NAME='" & Me.TxtUserName.Text & "' AND sPASSWORD='" & Me.TxtOldPassword.Text & "'"
            Me.AsConn.Cmd.CommandType = CommandType.Text
            Rd = Me.AsConn.Cmd.ExecuteReader

            If Rd.Read() Then

                bCorrect = True

            Else
                bCorrect = False


            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Me.AsConn.Conn.Close()
            Me.AsConn.Cmd.Connection = Nothing
        End Try
    End Sub
#End Region

    
End Class