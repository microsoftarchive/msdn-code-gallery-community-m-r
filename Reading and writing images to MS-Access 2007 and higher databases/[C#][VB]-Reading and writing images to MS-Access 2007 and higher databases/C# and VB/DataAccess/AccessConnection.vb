Public Class MS_AccessConnection
    Private Shared _instance As MS_AccessConnection
    Private _Connections As New Hashtable
    Public Sub Reset(connectionString As String)
        Dim connection As OleDb.OleDbConnection = Nothing
        Try
            connection = CType(_Connections(connectionString), OleDb.OleDbConnection)
            connection.Dispose()
            connection = Nothing
        Catch ex As Exception
            'Do Nothing
        End Try
    End Sub
    Public Sub ResetAll()
        For Each Item As Object In _Connections
            Dim connection As OleDb.OleDbConnection = Nothing

            Try
                connection = CType(Item, OleDb.OleDbConnection)
                connection.Dispose()
                connection = Nothing
            Catch ex As Exception
                'Do Nothing
            End Try
        Next

    End Sub
    Public Function GetConnection(connectionString As String) As OleDb.OleDbConnection
        Dim connection As OleDb.OleDbConnection = Nothing
        Dim bNeedAdd As Boolean = False
        Try
            connection = CType(_Connections(connectionString), OleDb.OleDbConnection)
        Catch ex As Exception
            'Do Nothing
        End Try

        If connection Is Nothing Then
            bNeedAdd = True
        End If

        If connection Is Nothing OrElse connection.State = ConnectionState.Broken OrElse connection.State = ConnectionState.Closed Then
            Try
                If connection IsNot Nothing Then
                    connection.Dispose()
                    connection = Nothing
                End If

            Catch ex As Exception
                'Do Nothing
            End Try

            connection = New OleDb.OleDbConnection
        End If

        'Always return an open connection
        If connection.State = ConnectionState.Closed Then
            DataSourceExists(connectionString)
            connection.ConnectionString = connectionString
            connection.Open()
        End If

        If bNeedAdd Then
            _Connections.Add(connectionString, connection)
        End If

        Return connection
    End Function
    Private Sub DataSourceExists(pConnectionString As String)
        Dim builder As New OleDb.OleDbConnectionStringBuilder With {.ConnectionString = pConnectionString}
        If Not IO.File.Exists(builder.DataSource) Then
            Throw New Exception(
                "Failed to locate '" & builder.DataSource & "'." & Environment.NewLine &
                "Please check your configuration file connection string.")
        End If
    End Sub
    Public Shared Function GetInstance() As MS_AccessConnection

        If _Instance Is Nothing Then
            _Instance = New MS_AccessConnection
        End If

        Return _Instance

    End Function
    Protected Sub New()
    End Sub
End Class
