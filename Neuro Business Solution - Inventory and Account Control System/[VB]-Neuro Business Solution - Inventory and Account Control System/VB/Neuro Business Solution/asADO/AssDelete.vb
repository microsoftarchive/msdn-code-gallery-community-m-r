Public Class AssDelete
    Inherits AssConn
    'Private AsConn As New AssConn

    Public Sub DeleteValueIN(ByVal DeleteCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = DeleteCmd
            Cmd.CommandType = CommandType.Text
            Cmd.ExecuteNonQuery()
            MsgBox("Record Deleted!", MsgBoxStyle.Information, "(NS) - Deleting Info!")
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try

    End Sub
    Public Sub DeleteValue(ByVal DeleteCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = DeleteCmd
            Cmd.CommandType = CommandType.Text
            Cmd.ExecuteNonQuery()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub DeleteValue_NoErr(ByVal DeleteCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = DeleteCmd
            Cmd.CommandType = CommandType.Text
            Cmd.ExecuteNonQuery()
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
End Class
