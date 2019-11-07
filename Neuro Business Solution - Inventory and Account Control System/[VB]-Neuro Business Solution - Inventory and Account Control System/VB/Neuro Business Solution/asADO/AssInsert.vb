Public Class AssInsert
    Inherits AssConn
    'Private AsConn As New AssConn

    Public Sub SaveValueIN(ByVal InsertCmd As String) ', Optional ByVal MsgString As String = Nothing)
        Try
            Conn2.Open()
            Cmd2.Connection = Conn2

            Cmd2.CommandText = InsertCmd
            Cmd2.CommandType = CommandType.Text
            Cmd2.ExecuteNonQuery()

            'If Not MsgString = Nothing Then
            '    MsgBox(MsgString, MsgBoxStyle.Information, "(NS) - Saving Info!")
            'End If

            MsgBox("Record Save!", MsgBoxStyle.Information, "(NS) - Saving Info!")
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn2.Close()
            Cmd2.Connection = Nothing
        End Try
    End Sub
    Public Sub SaveValue(ByVal InsertCmd As String)
        Try
            Conn2.Open()
            Cmd2.Connection = Conn2

            Cmd2.CommandText = InsertCmd
            Cmd2.CommandType = CommandType.Text
            Cmd2.ExecuteNonQuery()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn2.Close()
            Cmd2.Connection = Nothing
        End Try
    End Sub
End Class
