Imports System.Windows.Forms
Public Class CustomerData
    Public Property ContactTypes As New DataTable With {.TableName = "ContactTypes"}
    Public Property Customers As New DataTable With {.TableName = "Customers"}
    Public Sub New()
    End Sub
    Public Function LoadCustomersByContactType(ByRef ErrorMessage As String, ByVal ContactType As String) As Boolean
        Dim Result As Boolean = True
        Dim cn As OleDb.OleDbConnection = Nothing

        Try
            Dim conn As MS_AccessConnection = MS_AccessConnection.GetInstance()
            cn = conn.GetConnection(Builder.ConnectionString)
            Console.WriteLine(cn.ConnectionString)
            Using cmd As New OleDb.OleDbCommand With
                {
                    .Connection = cn,
                    .CommandText =
                    <SQL>
                        SELECT 
                            Identifier, 
                            CompanyName, 
                            ContactName, 
                            Address, 
                            City, 
                            PostalCode, 
                            Country
                        FROM 
                            Customers
                        WHERE (((ContactTitle)=@P1))
                        
                    </SQL>.Value
                }

                cmd.Parameters.Add(New OleDb.OleDbParameter With {.ParameterName = "@P1", .DbType = DbType.String})
                cmd.Parameters(0).Value = ContactType

                Customers.Load(cmd.ExecuteReader)
                Customers.Columns(ProjectGlobals.CustomerIdentifier).ColumnMapping = MappingType.Hidden
            End Using
        Catch ex As Exception
            ErrorMessage = ex.Message
            Result = False
        End Try

        Return Result

    End Function
    Public Function LoadCustomers(ByRef ErrorMessage As String) As Boolean
        Dim Result As Boolean = True
        Dim cn As OleDb.OleDbConnection = Nothing

        Try
            Dim conn As MS_AccessConnection = MS_AccessConnection.GetInstance()
            cn = conn.GetConnection(Builder.ConnectionString)

            Using cmd As New OleDb.OleDbCommand With
                {
                    .Connection = cn,
                    .CommandText =
                    <SQL>
                        SELECT 
                            Identifier, 
                            CompanyName, 
                            ContactName, 
                            Address, 
                            City, 
                            PostalCode, 
                            Country
                        FROM 
                            Customers
                    </SQL>.Value
                }

                Customers.Load(cmd.ExecuteReader)
                Customers.Columns(ProjectGlobals.CustomerIdentifier).ColumnMapping = MappingType.Hidden
            End Using
        Catch ex As Exception
            ErrorMessage = ex.Message
            Result = False
        End Try

        Return Result

    End Function

    Public Function LoadContactTypes(ByRef ErrorMessage As String) As Boolean
        Dim Result As Boolean = True
        Dim cn As OleDb.OleDbConnection = Nothing

        Try
            Dim conn As MS_AccessConnection = MS_AccessConnection.GetInstance()
            cn = conn.GetConnection(Builder.ConnectionString)

            Using cmd As New OleDb.OleDbCommand With
                {
                    .Connection = cn,
                    .CommandText = "SELECT ContactTitle As Title FROM Customers GROUP BY ContactTitle"
                }

                ContactTypes.Load(cmd.ExecuteReader)
                Dim Row As DataRow
                Row = ContactTypes.NewRow
                Row("Title") = ProjectGlobals.SelectAllText
                ContactTypes.Rows.InsertAt(Row, 0)
            End Using
        Catch ex As Exception
            ErrorMessage = ex.Message
            Result = False
        End Try

        Return Result

    End Function
End Class
