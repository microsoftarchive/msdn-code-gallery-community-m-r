Public Class AssUpdate
    Inherits AssConn
    'Private AsConn As New AssConn

    Public Sub UpdateValueIN(ByVal UpdateCmd As String)
        Try
            Conn2.Open()
            Cmd2.Connection = Conn2

            Cmd2.CommandText = UpdateCmd
            Cmd2.CommandType = CommandType.Text
            Cmd2.ExecuteNonQuery()
            MsgBox("Record Updated!", MsgBoxStyle.Information, "(NS) - Updating Info!")
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn2.Close()
            Cmd2.Connection = Nothing
        End Try
    End Sub
    Public Sub UpdateValue(ByVal UpdateCmd As String)
        Try
            Conn2.Open()
            Cmd2.Connection = Conn2

            Cmd2.CommandText = UpdateCmd
            Cmd2.CommandType = CommandType.Text
            Cmd2.ExecuteNonQuery()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn2.Close()
            Cmd2.Connection = Nothing
        End Try
    End Sub
    Public Sub UpdateValue_NoErr1(ByVal UpdateCmd As String)
        Try
            Conn3.Open()
            Cmd3.Connection = Conn3

            Cmd3.CommandText = UpdateCmd
            Cmd3.CommandType = CommandType.Text
            Cmd3.ExecuteNonQuery()
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn3.Close()
            Cmd3.Connection = Nothing
        End Try
    End Sub
    Public Sub UpdateValue_NoErr(ByVal UpdateCmd As String)
        Try
            Conn2.Open()
            Cmd2.Connection = Conn2

            Cmd2.CommandText = UpdateCmd
            Cmd2.CommandType = CommandType.Text
            Cmd2.ExecuteNonQuery()
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn2.Close()
            Cmd2.Connection = Nothing
        End Try
    End Sub
    Public Sub UpdatePassword(ByVal UpdateCmd As String)
        Try
            Conn2.Open()
            Cmd2.Connection = Conn2

            Cmd2.CommandText = UpdateCmd
            Cmd2.CommandType = CommandType.Text
            Cmd2.ExecuteNonQuery()
            MsgBox("New password has been set!", MsgBoxStyle.Information, "(NS) - Password Changed!")
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn2.Close()
            Cmd2.Connection = Nothing
        End Try
    End Sub
End Class
