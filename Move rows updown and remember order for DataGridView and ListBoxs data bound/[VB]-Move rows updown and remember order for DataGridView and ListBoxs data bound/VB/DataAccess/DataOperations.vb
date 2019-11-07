Public Module DataOperations
    ''' <summary>
    ''' Two forms use this connection string
    ''' </summary>
    ''' <remarks></remarks>
    Public BuilderAccdb As New OleDb.OleDbConnectionStringBuilder With
        {
            .Provider = "Microsoft.ACE.OLEDB.12.0",
            .DataSource = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database1.accdb")
        }
    Public Function LoadCustomersAccessForm() As DataTable
        Using cn As New OleDb.OleDbConnection With
            {
                .ConnectionString = BuilderAccdb.ConnectionString
            }
            Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                cmd.CommandText =
                    <SQL>
                        SELECT TOP 10 
                            Identifier, 
                            CompanyName, 
                            ContactName, 
                            ContactTitle,
                            RowPosition
                        FROM 
                            Customer 
                        Order By RowPosition
                    </SQL>.Value

                Dim dt As New DataTable
                cn.Open()
                dt.Load(cmd.ExecuteReader)
                dt.Columns("Identifier").ColumnMapping = MappingType.Hidden
                dt.Columns("RowPosition").ColumnMapping = MappingType.Hidden

                '
                ' Process column added 12/5/2012 to demo copying data from
                ' DataGridView1 to DataGridView2
                '
                dt.Columns.Add(New DataColumn With
                               {
                                   .ColumnName = "Process",
                                   .DataType = GetType(Boolean)
                               }
                           )

                For Each row As DataRow In dt.Rows
                    row.SetField(Of Boolean)("Process", False)
                Next
                '
                ' End 12/5 changes
                '
                dt.AcceptChanges()

                Return dt

            End Using
        End Using
    End Function
    Public Sub UpdatePosition(ByVal dt As DataTable)
        Using cn As New OleDb.OleDbConnection With
            {
                .ConnectionString = BuilderAccdb.ConnectionString
            }
            Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                cmd.CommandText =
                    <SQL>
                        UPDATE Customer 
                        SET Customer.RowPosition = @P1
                        WHERE (((Customer.Identifier)=@P2));
                    </SQL>.Value

                cmd.Parameters.Add(New OleDb.OleDbParameter With {.ParameterName = "@P1", .OleDbType = OleDb.OleDbType.Integer})
                cmd.Parameters.Add(New OleDb.OleDbParameter With {.ParameterName = "@P2", .OleDbType = OleDb.OleDbType.Integer})

                cn.Open()

                Dim Position As Integer = 0

                For x As Integer = 0 To dt.Rows.Count - 1
                    Position = x + 1
                    cmd.Parameters("@P1").Value = Position
                    cmd.Parameters("@P2").Value = dt.Rows(x).Field(Of Integer)("Identifier")
                    cmd.ExecuteNonQuery()
                Next

            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Used to update DisplayIndex for all rows
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateListBoxData(ByVal dt As DataTable)
        Using cn As New OleDb.OleDbConnection With {.ConnectionString = BuilderAccdb.ConnectionString}
            cn.Open()
            Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                cmd.CommandText =
                    <SQL>
                        Update Table1
                        SET DisplayIndex=P1
                        WHERE Identifier=P2
                    </SQL>.Value

                Dim P1 As New OleDb.OleDbParameter With {.DbType = DbType.Int32}
                Dim P2 As New OleDb.OleDbParameter With {.DbType = DbType.Int32}

                cmd.Parameters.AddRange(New OleDb.OleDbParameter() {P1, P2})

                For x As Integer = 0 To dt.Rows.Count - 1
                    P1.Value = dt.Rows(x).Item("DisplayIndex")
                    P2.Value = dt.Rows(x).Item("Identifier")
                    cmd.ExecuteNonQuery()
                Next

            End Using
        End Using
    End Sub
    Private TextFileName As String = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.txt")
    Public Function LoadCustomersTextFileForm() As DataTable

        Dim dt As New DataTable

        dt.Columns.Add(New DataColumn With {.ColumnName = "Identifier", .DataType = GetType(System.String),
                                            .ColumnMapping = MappingType.Hidden})
        dt.Columns.Add(New DataColumn With {.ColumnName = "CompanyName", .DataType = GetType(System.String)})
        dt.Columns.Add(New DataColumn With {.ColumnName = "ContactName", .DataType = GetType(System.String)})
        dt.Columns.Add(New DataColumn With {.ColumnName = "ContactTitle", .DataType = GetType(System.String)})

        Dim Lines = IO.File.ReadAllLines(TextFileName)
        For Each line In Lines
            dt.Rows.Add(line.Split(",".ToCharArray))
        Next

        Return dt

    End Function
    Public Sub SaveCustomerTextFile(ByVal dt As DataTable)
        Dim sb As New System.Text.StringBuilder

        For Each row As DataRow In dt.Rows
            sb.AppendLine(String.Join(",", row.ItemArray))
        Next
        IO.File.WriteAllText(TextFileName, sb.ToString)
    End Sub
End Module
