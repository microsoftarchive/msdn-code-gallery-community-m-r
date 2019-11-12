Public Class AssDataGrid
    Inherits AssConn
    'Private AsConn As New AssConn

    Public Sub LoadDataGrid(ByVal SelectCmd As String, ByVal Adp As OleDb.OleDbDataAdapter, ByVal DsName As DataSet, ByVal DG As DataGrid, ByVal DataSetName As String, ByVal DatabaseName As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text

            'DG.DataSource = DsName.DataSetName
            DG.DataSource = DsName

            Adp.Fill(DsName, DatabaseName)
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
End Class
