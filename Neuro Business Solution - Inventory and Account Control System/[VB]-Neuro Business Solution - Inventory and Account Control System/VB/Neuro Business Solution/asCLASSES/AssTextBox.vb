Imports SDS = System.Data.SqlClient

Public Class AssTextBox
    Inherits AssConn
    Public Sub LoadCmbText(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal CmbName1 As System.Windows.Forms.ComboBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            CmbName1.Text = Nothing

            If Rd.Read() Then
                CmbName1.Text = Rd.GetValue(0)
            Else
                CmbName1.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadCmbText(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal CmbName1 As System.Windows.Forms.ComboBox, ByVal CmbName2 As System.Windows.Forms.ComboBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            CmbName1.Text = Nothing
            CmbName2.Text = Nothing
            If Rd.Read() Then
                CmbName1.Text = Rd.GetValue(0)
                CmbName2.Text = Rd.GetValue(1)

            Else
                CmbName1.Text = Nothing
                CmbName2.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadCmbText(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal CmbName1 As System.Windows.Forms.ComboBox, ByVal CmbName2 As System.Windows.Forms.ComboBox, ByVal CmbName3 As System.Windows.Forms.ComboBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            CmbName1.Text = Nothing
            CmbName2.Text = Nothing
            CmbName3.Text = Nothing

            If Rd.Read() Then
                CmbName1.Text = Rd.GetValue(0)
                CmbName2.Text = Rd.GetValue(1)
                CmbName3.Text = Rd.GetValue(2)
            Else
                CmbName1.Text = Nothing
                CmbName2.Text = Nothing
                CmbName3.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadCmbText(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal CmbName1 As System.Windows.Forms.ComboBox, ByVal CmbName2 As System.Windows.Forms.ComboBox, ByVal CmbName3 As System.Windows.Forms.ComboBox, ByVal CmbName4 As System.Windows.Forms.ComboBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            CmbName1.Text = Nothing
            CmbName2.Text = Nothing
            CmbName3.Text = Nothing
            CmbName4.Text = Nothing

            If Rd.Read() Then
                CmbName1.Text = Rd.GetValue(0)
                CmbName2.Text = Rd.GetValue(1)
                CmbName3.Text = Rd.GetValue(2)
                CmbName4.Text = Rd.GetValue(3)
            Else
                CmbName1.Text = Nothing
                CmbName2.Text = Nothing
                CmbName3.Text = Nothing
                CmbName4.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub


    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
            Else
                TxtName1.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox, ByVal TxtName3 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing
            TxtName3.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
                TxtName3.Text = Rd.GetValue(2)
            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
                TxtName3.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox, ByVal TxtName3 As System.Windows.Forms.TextBox, ByVal TxtName4 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing
            TxtName3.Text = Nothing
            TxtName4.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
                TxtName3.Text = Rd.GetValue(2)
                TxtName4.Text = Rd.GetValue(3)
            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
                TxtName3.Text = Nothing
                TxtName4.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox, ByVal TxtName3 As System.Windows.Forms.TextBox, ByVal TxtName4 As System.Windows.Forms.TextBox, ByVal TxtName5 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing
            TxtName3.Text = Nothing
            TxtName4.Text = Nothing
            TxtName5.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
                TxtName3.Text = Rd.GetValue(2)
                TxtName4.Text = Rd.GetValue(3)
                TxtName5.Text = Rd.GetValue(4)
            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
                TxtName3.Text = Nothing
                TxtName4.Text = Nothing
                TxtName5.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox, ByVal TxtName3 As System.Windows.Forms.TextBox, ByVal TxtName4 As System.Windows.Forms.TextBox, ByVal TxtName5 As System.Windows.Forms.TextBox, ByVal TxtName6 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing
            TxtName3.Text = Nothing
            TxtName4.Text = Nothing
            TxtName5.Text = Nothing
            TxtName6.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
                TxtName3.Text = Rd.GetValue(2)
                TxtName4.Text = Rd.GetValue(3)
                TxtName5.Text = Rd.GetValue(4)
                TxtName6.Text = Rd.GetValue(5)
            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
                TxtName3.Text = Nothing
                TxtName4.Text = Nothing
                TxtName5.Text = Nothing
                TxtName6.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox, ByVal TxtName3 As System.Windows.Forms.TextBox, ByVal TxtName4 As System.Windows.Forms.TextBox, ByVal TxtName5 As System.Windows.Forms.TextBox, ByVal TxtName6 As System.Windows.Forms.TextBox, ByVal TxtName7 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing
            TxtName3.Text = Nothing
            TxtName4.Text = Nothing
            TxtName5.Text = Nothing
            TxtName6.Text = Nothing
            TxtName7.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
                TxtName3.Text = Rd.GetValue(2)
                TxtName4.Text = Rd.GetValue(3)
                TxtName5.Text = Rd.GetValue(4)
                TxtName6.Text = Rd.GetValue(5)
                TxtName7.Text = Rd.GetValue(6)
            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
                TxtName3.Text = Nothing
                TxtName4.Text = Nothing
                TxtName5.Text = Nothing
                TxtName6.Text = Nothing
                TxtName7.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox, ByVal TxtName3 As System.Windows.Forms.TextBox, ByVal TxtName4 As System.Windows.Forms.TextBox, ByVal TxtName5 As System.Windows.Forms.TextBox, ByVal TxtName6 As System.Windows.Forms.TextBox, ByVal TxtName7 As System.Windows.Forms.TextBox, ByVal TxtName8 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing
            TxtName3.Text = Nothing
            TxtName4.Text = Nothing
            TxtName5.Text = Nothing
            TxtName6.Text = Nothing
            TxtName7.Text = Nothing
            TxtName8.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
                TxtName3.Text = Rd.GetValue(2)
                TxtName4.Text = Rd.GetValue(3)
                TxtName5.Text = Rd.GetValue(4)
                TxtName6.Text = Rd.GetValue(5)
                TxtName7.Text = Rd.GetValue(6)
                TxtName8.Text = Rd.GetValue(7)
            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
                TxtName3.Text = Nothing
                TxtName4.Text = Nothing
                TxtName5.Text = Nothing
                TxtName6.Text = Nothing
                TxtName7.Text = Nothing
                TxtName8.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox, ByVal TxtName3 As System.Windows.Forms.TextBox, ByVal TxtName4 As System.Windows.Forms.TextBox, ByVal TxtName5 As System.Windows.Forms.TextBox, ByVal TxtName6 As System.Windows.Forms.TextBox, ByVal TxtName7 As System.Windows.Forms.TextBox, ByVal TxtName8 As System.Windows.Forms.TextBox, ByVal TxtName9 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing
            TxtName3.Text = Nothing
            TxtName4.Text = Nothing
            TxtName5.Text = Nothing
            TxtName6.Text = Nothing
            TxtName7.Text = Nothing
            TxtName8.Text = Nothing
            TxtName9.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
                TxtName3.Text = Rd.GetValue(2)
                TxtName4.Text = Rd.GetValue(3)
                TxtName5.Text = Rd.GetValue(4)
                TxtName6.Text = Rd.GetValue(5)
                TxtName7.Text = Rd.GetValue(6)
                TxtName8.Text = Rd.GetValue(7)
                TxtName9.Text = Rd.GetValue(8)
            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
                TxtName3.Text = Nothing
                TxtName4.Text = Nothing
                TxtName5.Text = Nothing
                TxtName6.Text = Nothing
                TxtName7.Text = Nothing
                TxtName8.Text = Nothing
                TxtName9.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadTextBox(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String, ByVal TxtName1 As System.Windows.Forms.TextBox, ByVal TxtName2 As System.Windows.Forms.TextBox, ByVal TxtName3 As System.Windows.Forms.TextBox, ByVal TxtName4 As System.Windows.Forms.TextBox, ByVal TxtName5 As System.Windows.Forms.TextBox, ByVal TxtName6 As System.Windows.Forms.TextBox, ByVal TxtName7 As System.Windows.Forms.TextBox, ByVal TxtName8 As System.Windows.Forms.TextBox, ByVal TxtName9 As System.Windows.Forms.TextBox, ByVal TxtName10 As System.Windows.Forms.TextBox)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd
            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader

            TxtName1.Text = Nothing
            TxtName2.Text = Nothing
            TxtName3.Text = Nothing
            TxtName4.Text = Nothing
            TxtName5.Text = Nothing
            TxtName6.Text = Nothing
            TxtName7.Text = Nothing
            TxtName8.Text = Nothing
            TxtName9.Text = Nothing
            TxtName10.Text = Nothing

            If Rd.Read() Then
                TxtName1.Text = Rd.GetValue(0)
                TxtName2.Text = Rd.GetValue(1)
                TxtName3.Text = Rd.GetValue(2)
                TxtName4.Text = Rd.GetValue(3)
                TxtName5.Text = Rd.GetValue(4)
                TxtName6.Text = Rd.GetValue(5)
                TxtName7.Text = Rd.GetValue(6)
                TxtName8.Text = Rd.GetValue(7)
                TxtName9.Text = Rd.GetValue(8)
                TxtName10.Text = Rd.GetValue(9)

            Else
                TxtName1.Text = Nothing
                TxtName2.Text = Nothing
                TxtName3.Text = Nothing
                TxtName4.Text = Nothing
                TxtName5.Text = Nothing
                TxtName6.Text = Nothing
                TxtName7.Text = Nothing
                TxtName8.Text = Nothing
                TxtName9.Text = Nothing
                TxtName10.Text = Nothing
            End If
        Catch Ex As Exception
            'MsgBox(Ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub


End Class
