Imports System.Threading
Imports System.Data.Common

Public Class DataOperations
    Private Builder As New OleDb.OleDbConnectionStringBuilder
    Private cts As New CancellationTokenSource

    ''' <summary>
    ''' Record count for Customers table
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RecordCount As Integer
    ''' <summary>
    ''' Used for cancel operations
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Token As CancellationToken
    ''' <summary>
    ''' DataTable containing customer data
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataTable As DataTable
    ''' <summary>
    ''' Calling for used for invoke
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Caller As Form
    ''' <summary>
    ''' Label to show status while running
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Label As Label

    Public Sub New()
        DataTable = New DataTable With {.TableName = "Customers"}
        DataTable.Columns.Add(
            New DataColumn With {.ColumnName = "Identifier", .DataType = GetType(Int32)})

        DataTable.Columns.Add(
            New DataColumn With {.ColumnName = "CompanyName", .DataType = GetType(String)})
        DataTable.Columns.Add(
            New DataColumn With {.ColumnName = "ContactName", .DataType = GetType(String)})
        DataTable.Columns.Add(
            New DataColumn With {.ColumnName = "ContactTitle", .DataType = GetType(String)})
        DataTable.Columns.Add(
            New DataColumn With {.ColumnName = "Country", .DataType = GetType(String)})

        Builder.Provider = "Microsoft.ACE.OLEDB.12.0"
        Builder.DataSource = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Customers.accdb")

        GetRecordCount()
    End Sub
    ''' <summary>
    ''' Code to get data
    ''' </summary>
    ''' <param name="ct"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDataAsync(ByVal ct As CancellationToken) As Task
        Dim CurrentRecord As Integer = 1

        Using cn As New OleDbConnection(Builder.ConnectionString)
            Using cmd As New OleDbCommand("SELECT Identifier, CompanyName, ContactName, ContactTitle, Country FROM Customers", cn)

                Await cn.OpenAsync()

                Using reader As DbDataReader = Await cmd.ExecuteReaderAsync()
                    Do While Await reader.ReadAsync()
                        If ct.IsCancellationRequested Then
                            Exit Do
                        End If

                        Dim PrimaryKey As Integer = Await reader.GetFieldValueAsync(Of Integer)(0)

                        Dim ItemArray() As Object =
                            {
                                PrimaryKey,
                                Await reader.GetFieldValueAsync(Of String)(1),
                                Await reader.GetFieldValueAsync(Of String)(2),
                                Await reader.GetFieldValueAsync(Of String)(3),
                                Await reader.GetFieldValueAsync(Of String)(4)
                            }

                        Caller.Invoke(
                            New EventHandler(
                                Sub(sender As Object, e As EventArgs)
                                    DataTable.Rows.Add(ItemArray)
                                    If Label IsNot Nothing Then
                                        If Not Label.InvokeRequired Then
                                            Label.Text = String.Format("{0} of {1}", CurrentRecord, Me.RecordCount)
                                        End If
                                    End If
                                End Sub), New Object(1) {ItemArray, Nothing})

                        CurrentRecord += 1

                        '
                        ' Beings we only have a small set of records we will do a delay
                        '
                        Thread.Sleep(1000)

                    Loop
                End Using
            End Using
        End Using
    End Function
    ''' <summary>
    ''' Get record count in Customers table
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRecordCount()
        Using cn As New OleDbConnection(Builder.ConnectionString)
            Using cmd As New OleDbCommand("SELECT Count(Identifier) FROM Customers;", cn)
                cn.Open()
                RecordCount = CInt(Fix(cmd.ExecuteScalar()))
            End Using
        End Using
    End Sub
End Class
