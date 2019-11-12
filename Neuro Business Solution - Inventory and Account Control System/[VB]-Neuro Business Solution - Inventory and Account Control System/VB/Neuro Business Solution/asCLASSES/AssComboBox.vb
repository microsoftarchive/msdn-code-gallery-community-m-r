Imports SDS = System.Data.SqlClient
Public Class AssComboBox
    Inherits AssConn
    'Private AsConn As New AssConn

    Public Sub LoadComboBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal CmbName1 As System.Windows.Forms.ComboBox)
        Try
            Conn2.Open()
            Cmd2.Connection = Conn2

            Cmd2.CommandText = SelectCmd
            Cmd2.CommandType = CommandType.Text

            Rd = Cmd2.ExecuteReader

            CmbName1.Items.Clear()
            While Rd.Read()
                CmbName1.Items.Add(Rd.GetValue(0))
            End While
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn2.Close()
            Cmd2.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadCmbNotDuplicate(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal CmbName As System.Windows.Forms.ComboBox)

        Dim StrName As String
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            CmbName.Items.Clear()
            While Rd.Read()
                StrName = Rd.GetValue(0)
                'MsgBox(StrName)
                If CmbName.Items.IndexOf(StrName) = False Then
                ElseIf CmbName.Items.IndexOf(StrName) = True Then
                    CmbName.Items.Add(StrName)
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
