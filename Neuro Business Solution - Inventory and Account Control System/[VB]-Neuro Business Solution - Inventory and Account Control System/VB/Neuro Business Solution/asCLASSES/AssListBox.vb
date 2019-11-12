Imports SDS = System.Data.SqlClient
Public Class AssListBox
    Inherits AssConn

    Public Sub LoadListBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal LstName As System.Windows.Forms.ListBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            LstName.Items.Clear()
            While Rd.Read()
                LstName.Items.Add(Rd.GetValue(0))
            End While
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadLstNotDuplicate(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal LstName As System.Windows.Forms.ListBox)
        Dim StrName As String
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            LstName.Items.Clear()
            While Rd.Read()
                StrName = Rd.GetValue(0)
                'MsgBox(StrName)
                If LstName.Items.IndexOf(StrName) = False Then
                ElseIf LstName.Items.IndexOf(StrName) = True Then
                    LstName.Items.Add(StrName)
                End If
            End While
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
End Class
