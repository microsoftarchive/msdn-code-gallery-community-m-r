Imports System.Data.OleDb

Public Class DataOperations
    Inherits BaseExceptionProperties

    Private Builder As New OleDbConnectionStringBuilder With
    {
        .Provider = "Microsoft.ACE.OLEDB.12.0",
        .DataSource = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database1.accdb")
    }


    Public Function LoadCustomers() As DataTable

        Dim CustomersDataTable = New DataTable
        CustomersDataTable.Columns.Add(New DataColumn With {.ColumnName = "Process", .DataType = GetType(Boolean)})

        Try

            Using cn As New OleDbConnection With {.ConnectionString = Builder.ConnectionString}
                Using cmd As New OleDbCommand With {.Connection = cn}
                    cmd.CommandText = "SELECT Identifier, CompanyName, ContactTitle FROM Customers"

                    cn.Open()

                    CustomersDataTable.Load(cmd.ExecuteReader)
                    CustomersDataTable.Columns("Identifier").ColumnMapping = MappingType.Hidden

                    ' values are null by default
                    For Each row As DataRow In CustomersDataTable.Rows
                        row.SetField(Of Boolean)("Process", False)
                    Next

                End Using
            End Using


        Catch ex As Exception
            mLastException = ex
            mHasException = True
        End Try

        Return CustomersDataTable

    End Function
    Public Function DeleteCustomers(ByVal pDataRows As List(Of DataRow)) As Boolean
        Using cn As New OleDbConnection With {.ConnectionString = Builder.ConnectionString}
            Using cmd As New OleDbCommand With {.Connection = cn}
                cmd.Parameters.Add(New OleDbParameter With {.DbType = DbType.Int32, .ParameterName = "@Identifier"})
                cmd.CommandText = "DELETE FROM Customers WHERE Identifier = @Identifier"

                Try

                    cn.Open()

                    For indexer As Integer = 0 To pDataRows.Count - 1
                        cmd.Parameters("@Identifier").Value = pDataRows(indexer).Field(Of Integer)("Identifier")
                        cmd.ExecuteNonQuery()
                    Next

                    Return True

                Catch ex As Exception
                    mLastException = ex
                    mHasException = True
                    Return False
                End Try

            End Using
        End Using
    End Function
    Public Function DeleteSingleCustomer(ByVal pDataRow As DataRow) As Boolean
        Using cn As New OleDbConnection With {.ConnectionString = Builder.ConnectionString}
            Using cmd As New OleDbCommand With {.Connection = cn}

                cmd.CommandText = "DELETE FROM Customers WHERE Identifier = @Identifier"

                Try

                    cn.Open()
                    cmd.Parameters.AddWithValue("@Identifier", pDataRow.Field(Of Integer)("Identifier"))
                    cmd.ExecuteNonQuery()

                    Return True

                Catch ex As Exception
                    mLastException = ex
                    mHasException = True
                    Return False
                End Try

            End Using
        End Using
    End Function

End Class
