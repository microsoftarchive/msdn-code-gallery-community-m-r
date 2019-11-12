Imports System.Data.OleDb
Imports KarensBaseClasses
Imports ExcelConnectionLibraryCS

Public Class ExcelOperations
    Inherits BaseExceptionProperties

    Private pExcelConnection = New Utils()
    ''' <summary>
    ''' Get used rows from a work sheet. N0 guarantees
    ''' that this is completly dependable as no rows
    ''' can show up as one row. Yet another issue with OleDb.
    ''' </summary>
    ''' <param name="pFileName"></param>
    ''' <param name="pSheetName"></param>
    ''' <returns></returns>
    Public Function UsedRows(ByVal pFileName As String, ByVal pSheetName As String) As Integer

        mHasException = False

        Dim rowCount As Integer = 0

        Try

            Using cn As New OleDbConnection(pExcelConnection.ConnectionString(pFileName))
                Using cmd As New OleDbCommand With {.Connection = cn}
                    cmd.CommandText = $"SELECT * FROM [{pSheetName}$]"
                    cn.Open()
                    Dim dt As New DataTable
                    dt.Load(cmd.ExecuteReader)
                    rowCount = dt.Rows.Count
                End Using
            End Using

        Catch ex As Exception
            mHasException = True
            mLastException = ex
        End Try

        Return rowCount

    End Function
    Public Function CreateRangeString(
        ByVal pStartColumn As String,
        ByVal pStartRow As Integer,
        ByVal pEndColumn As String,
        ByVal pEndRow As Integer) As ExcelRangeItem

        Return New ExcelRangeItem With
            {
                .StartColumn = pStartColumn,
                .StartRow = pStartRow,
                .EndColumn = pEndColumn,
                .EndRow = pEndRow
            }

    End Function
    ''' <summary>
    ''' Get available sheet names
    ''' </summary>
    ''' <param name="pFileName"></param>
    ''' <returns></returns>
    Public Function SheetNames(ByVal pFileName As String) As List(Of String)
        mHasException = False

        Dim Names As New List(Of String)

        Try

            Using cn As New OleDbConnection(pExcelConnection.ConnectionString(pFileName))

                cn.Open()

                Dim dt As DataTable = cn.GetSchema("Tables", New String() _
                    {Nothing, Nothing, Nothing, "Table"})

                For Each row As DataRow In dt.Rows
                    Names.Add(row.Field(Of String)("Table_Name").Replace("$", ""))
                Next

            End Using

        Catch ex As Exception
            mHasException = True
            mLastException = ex
        End Try

        Return Names

    End Function
    ''' <summary>
    ''' Read sheet data based on range selected.
    ''' </summary>
    ''' <param name="pFileName"></param>
    ''' <param name="pQuery"></param>
    ''' <returns></returns>
    Public Function ReadData(ByVal pFileName As String, ByVal pQuery As String) As DataTable

        mHasException = False

        Dim dt As New DataTable


        Try
            Using cn As New OleDbConnection
                cn.ConnectionString = pExcelConnection.ConnectionString(pFileName)
                Using cmd As New OleDbCommand With {.Connection = cn, .CommandText = pQuery}
                    cn.Open()
                    dt.Load(cmd.ExecuteReader)
                End Using

            End Using
        Catch ex As Exception
            mHasException = True
            mLastException = ex
        End Try

        Return dt

    End Function

End Class
