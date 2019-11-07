Imports System.Data.OleDb
Imports System.Threading

Public Class DataAccess

    Private Builder As New OleDbConnectionStringBuilder With
        {
            .DataSource = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.accdb"),
            .Provider = "Microsoft.ACE.OLEDB.12.0"
        }

    Public Sub New()
    End Sub
    Public Function RowCount() As Integer

        Dim Count As Integer = 0

        Using cn As New OleDbConnection With {.ConnectionString = Builder.ConnectionString}
            Using cmd As New OleDbCommand With {.CommandText = "SELECT COUNT(Identifier) As RowCount FROM People;", .Connection = cn}
                cn.Open()
                Count = CInt(cmd.ExecuteScalar)
            End Using
        End Using

        Return Count

    End Function
    Public Iterator Function LoadCustomers(ByVal ct As CancellationToken) As IEnumerable(Of Object())

        Dim RecordCount As Integer = RowCount()

        Using cn As New OleDbConnection With {.ConnectionString = Builder.ConnectionString}
            Using cmd As New OleDbCommand With {.CommandText = "SELECT Identifier, FirstName, LastName FROM People;", .Connection = cn}

                cn.Open()

                Dim Identifier As Integer = 0
                Dim FirstName As String = ""
                Dim LastName As String = ""
                Dim ItemArray As Object() = {}

                Dim Reader = cmd.ExecuteReader

                If Reader.HasRows Then

                    While Reader.Read
                        Identifier = Reader.GetFieldValue(Of Integer)(0)
                        FirstName = Reader.GetFieldValue(Of String)(1)
                        LastName = Reader.GetFieldValue(Of String)(2)

                        Yield ItemArray

                        ' update label on calling form
                        CustomersForm.UpDateDataTable({Identifier, FirstName, LastName}, String.Format("Loading {0} of {1}", Identifier, RecordCount))
                        ' update progressbar on calling form
                        'CustomersForm.UpDateDataTable({Identifier, FirstName, LastName}, Identifier, RecordCount)


                    End While

                End If

            End Using
        End Using
    End Function
End Class
