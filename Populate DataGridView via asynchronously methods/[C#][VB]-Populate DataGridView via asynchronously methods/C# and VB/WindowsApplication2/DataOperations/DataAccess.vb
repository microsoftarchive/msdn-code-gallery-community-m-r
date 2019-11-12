Imports LanguageExtensions
Imports System.Threading

Public Class DataAccess
    Public Property UseProgressBar As Boolean = False
    Public ReadOnly Property RowCount As Integer
        Get
            Return _RowCount()
        End Get
    End Property

    Private Function _RowCount() As Integer

        Dim Count As Integer = 0

        Using cn As New SqlClient.SqlConnection With {.ConnectionString = My.Settings.ConnectionString}
            Using cmd As New SqlClient.SqlCommand With {.CommandText = "SELECT COUNT(Identifier) As Row_Count FROM People", .Connection = cn}
                cn.Open()
                Count = CInt(cmd.ExecuteScalar)
            End Using
        End Using

        Return Count

    End Function
    Public Iterator Function LoadCustomers(ByVal ct As CancellationToken) As IEnumerable(Of Object())
        Dim RecordCount As Integer = RowCount

        Using cn As New SqlClient.SqlConnection With {.ConnectionString = My.Settings.ConnectionString}
            '
            ' If an ORDER BY is used in tangent with UseProgressBar = False the label display will be 
            ' funky yet all data is loaded
            '
            Using cmd As New SqlClient.SqlCommand With {.CommandText = "SELECT Identifier, FirstName, LastName FROM People;", .Connection = cn}

                cn.Open()

                Dim Identifier As Integer = 0
                Dim FirstName As String = ""
                Dim LastName As String = ""
                Dim ItemArray As Object() = {}

                Dim Reader = cmd.ExecuteReader

                If Reader.HasRows Then
                    '
                    ' NOTE: I have FirstName and LastName as required fields, if 
                    ' you implement this with data where the fields can be null then
                    ' without proper assertion you will get a System.Reflection.TargetInvokationException
                    ' I have included a language extension method in LanguageExtensions project 
                    ' e.g.
                    ' LastName = Reader.SafeGetString(2)
                    '
                    While Reader.Read
                        Identifier = Reader.GetFieldValue(Of Integer)(0)
                        FirstName = Reader.GetFieldValue(Of String)(1)
                        LastName = Reader.GetFieldValue(Of String)(2)

                        Yield ItemArray

                        If UseProgressBar Then
                            CustomersForm.UpDateDataTable({Identifier, FirstName, LastName}, Identifier, RecordCount)
                        Else
                            CustomersForm.UpDateDataTable({Identifier, FirstName, LastName}, String.Format("Loading {0} of {1}", Identifier, RecordCount))
                        End If
                    End While

                End If

            End Using
        End Using
    End Function
End Class
