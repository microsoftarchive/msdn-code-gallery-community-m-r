Imports System.Data.Common
Imports System.Data.OleDb
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms


Public Class Work
    Private Builder As New OleDbConnectionStringBuilder()
    Private cts As New CancellationTokenSource()

    Public Property RecordCount() As Integer

    Public Property Token() As CancellationToken

    Public Property dtPeople() As DataTable

    ''' <summary>
    ''' Form who called us
    ''' </summary>
    Public Property Caller() As Form

    ''' <summary>
    ''' Label to show how many rows are processed
    ''' </summary>
    Public Property Label() As Label

    Public Sub New()
        dtPeople = New DataTable With {.TableName = "PeopleTable"}
        dtPeople.Columns.Add(New DataColumn With {.ColumnName = "Identifier", .DataType = GetType(Int32)})
        dtPeople.Columns.Add(New DataColumn With {.ColumnName = "FirstName", .DataType = GetType(String)})
        dtPeople.Columns.Add(New DataColumn With {.ColumnName = "LastName", .DataType = GetType(String)})

        Builder.Provider = "Microsoft.ACE.OLEDB.12.0"
        Builder.DataSource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PeopleDatabase.accdb")

        GetRecordCount()
    End Sub

    Public Async Function GetDataAsync(ByVal ct As CancellationToken) As Task

        Dim CurrentRecord As Integer = 0

        Using cn As New OleDbConnection(Builder.ConnectionString)
            Using cmd As New OleDbCommand("SELECT Identifier, FirstName, LastName FROM People;", cn)
                Await cn.OpenAsync()
                Using reader As DbDataReader = Await cmd.ExecuteReaderAsync()
                    Do While Await reader.ReadAsync()
                        If ct.IsCancellationRequested Then
                            '
                            ' By hitting the cancel button or via CancelAfter(n)
                            '
                            Exit Do
                        End If

                        ' Best to use instead of Thread.Sleep
                        Await Task.Delay(500)

                        Dim PrimaryKey As Integer = Await reader.GetFieldValueAsync(Of Integer)(0)

                        Dim ItemArray() As Object =
                            {
                                PrimaryKey,
                                Await reader.GetFieldValueAsync(Of String)(1),
                                Await reader.GetFieldValueAsync(Of String)(2)
                            }

                        Caller.Invoke(
                            New EventHandler(
                                Sub(sender As Object, e As EventArgs)
                                    dtPeople.Rows.Add(ItemArray)
                                    If Label IsNot Nothing Then
                                        If Label.InvokeRequired = False Then
                                            Label.Text = String.Format("{0} of {1}", CurrentRecord, Me.RecordCount)
                                        End If
                                    End If
                                End Sub),
                            New Object(1) {ItemArray, Nothing})

                        CurrentRecord += 1

                    Loop

                End Using
            End Using
        End Using
    End Function

    Private Sub GetRecordCount()
        Using cn As New OleDbConnection(Builder.ConnectionString)
            Using cmd As New OleDbCommand("SELECT Count(Identifier) FROM People;", cn)
                cn.Open()
                RecordCount = CInt(Fix(cmd.ExecuteScalar()))
            End Using
        End Using
    End Sub
End Class
