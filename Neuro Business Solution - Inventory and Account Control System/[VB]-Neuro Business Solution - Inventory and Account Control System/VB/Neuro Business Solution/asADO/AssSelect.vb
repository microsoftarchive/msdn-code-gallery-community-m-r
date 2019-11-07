Imports SDS = System.Data.SqlClient
Public Class AssSelect
    Inherits AssConn
    'Private AsConn As New AssConn

    Public pFlg1 As Boolean = False
    Public pFlg2 As Boolean = False
    Public pFlg3 As Boolean = False
    Public pFlg4 As Boolean = False
    Public pFlg5 As Boolean = False
    Public pFlg6 As Boolean = False
    Public pFlg7 As Boolean = False
    Public pFlg8 As Boolean = False
    Public pFlg9 As Boolean = False
    Public pFlg10 As Boolean = False

    Public Sub SavedpFlg1(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            If Rd.Read() Then
                pFlg1 = True
            Else
                pFlg1 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg2(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg2 = True
            Else
                pFlg2 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg3(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg3 = True
            Else
                pFlg3 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg4(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg4 = True
            Else
                pFlg4 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg5(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg5 = True
            Else
                pFlg5 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg6(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg6 = True
            Else
                pFlg6 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg7(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg7 = True
            Else
                pFlg7 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg8(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg8 = True
            Else
                pFlg8 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg9(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg9 = True
            Else
                pFlg9 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub SavedpFlg10(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader
            If Rd.Read() Then
                pFlg10 = True
            Else
                pFlg10 = False
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub


End Class
