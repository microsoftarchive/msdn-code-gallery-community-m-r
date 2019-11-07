Imports SDS = System.Data.SqlClient
Public Class AssMaxNo
    Inherits AssConn

    Public MaxNo, Value1, ValueD, ValueDb, ValueDb2 As Double
    Public ValueT1a, ValueT1b, ValueTH1, ValueTH2, ValueTH3 As Double
    Public ValueZ1 As Double
    Public wValue1 As Double
    Public Chr As Char
    Public Str1, StrDt As String
    Public SvcRetYear, SvcLibYear As Integer
    Public MedicalCAT, nUnit, nUnit1, nCode, nMonth As Double
    Public dDATE, dDATE1, dDATE2 As String
    Public nRANK, nPayGROUP As Double
    Public dDOS1, dPRank As Date
    Public nTrade, nRankGroup, nUNIT2 As Double
    Public nINN, nOUT As Integer
    Public nTRADE1, nPAYGP1 As Double
    Public bFlg As Boolean = False

    Public Sub LoadSvcRetYear(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                SvcRetYear = Rd.GetValue(0)
                SvcLibYear = Rd.GetValue(1)
            Else
                SvcRetYear = 0
                SvcLibYear = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub

    Public Sub MaxAutoNo(ByVal Rd As SDS.SqlDataReader, ByVal MAXcmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = MAXcmd
            Cmd.CommandType = CommandType.Text

            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                MaxNo = Rd.Item(0)
            Else
                MaxNo = 0
            End If
        Catch ex As Exception
            MaxNo = 0
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub

    Public Function LoadValue(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String) As Double
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                Value1 = Rd.Item(0)
            Else
                Value1 = 0
            End If
        Catch ex As Exception
            Value1 = 0
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try

        Return Value1
    End Function

    Public Function LoadwValue(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String) As Double
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()

            wValue1 = 0
            While Rd.Read()

                wValue1 = wValue1 + Rd.Item(0)

            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try

        Return wValue1
    End Function

    Public Function LoadString(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String) As String
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                Str1 = Rd.Item(0)
            Else
                Str1 = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
        Return Str1
    End Function

    Public Function LoadValueZ(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String) As Double
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                ValueZ1 = Rd.Item(0)
            Else
                ValueZ1 = -1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
        Return ValueZ1
    End Function

    Public Function LoadValueT(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String) As Double
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                ValueT1a = Rd.Item(0)
                ValueT1b = Rd.Item(1)
            Else
                ValueT1a = 0
                ValueT1b = 0
            End If
        Catch ex As Exception
            ValueT1a = 0
            ValueT1b = 0
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try

        Return ValueT1a And ValueT1b
    End Function

    Public Sub LoadValueT_CD(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                Chr = Rd.Item(0)
                ValueD = Rd.Item(1)
            Else
                Chr = "N"
                ValueD = 0
            End If
        Catch ex As Exception
            Chr = "N"
            ValueD = 0
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub

    Public Sub LoadValueT_Dt_Db(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                ValueDb = Rd.Item(0)
                StrDt = Rd.Item(1)
            Else
                ValueDb = 0
                StrDt = ""
            End If
        Catch ex As Exception
            ValueD = 0
            StrDt = ""
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub
    Public Sub LoadValueT_Dt_Db2(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String)
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                ValueDb = Rd.Item(0)
                ValueDb2 = Rd.Item(0)
                StrDt = Rd.Item(1)
            Else
                ValueDb = 0
                ValueDb2 = 0
                StrDt = ""
            End If
        Catch ex As Exception
            ValueD = 0
            StrDt = ""
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
    End Sub

    Public Function LoadValueTH(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String) As Double
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                ValueTH1 = Rd.Item(0)
                ValueTH2 = Rd.Item(1)
                ValueTH3 = Rd.Item(2)
            Else
                ValueTH1 = 0
                ValueTH2 = 0
                ValueTH3 = 0
            End If
        Catch ex As Exception
            ValueTH1 = 0
            ValueTH2 = 0
            ValueTH3 = 0
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try

        Return ValueTH1 And ValueTH2 And ValueTH3
    End Function

    Public Function LoadDATE(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String) As Date
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                dDATE = Rd.Item(0)
            Else
                dDATE = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
        Return dDATE
    End Function

    Public Function bFlag(ByVal Rd As SDS.SqlDataReader, ByVal SelectCmd As String) As Boolean
        Try
            Conn.Open()
            Cmd.Connection = Conn

            Cmd.CommandText = SelectCmd

            Cmd.CommandType = CommandType.Text
            Rd = Cmd.ExecuteReader()
            If Rd.Read() Then
                bFlg = Rd.Item(0)
            Else
                bFlg = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            Cmd.Connection = Nothing
        End Try
        Return bFlg
    End Function
End Class

