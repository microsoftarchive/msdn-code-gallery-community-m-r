Module DataAccess
    Private BuilderForFemales As New OleDb.OleDbConnectionStringBuilder With
        {
            .Provider = "Microsoft.ACE.OLEDB.12.0",
            .DataSource = IO.Path.Combine(Application.StartupPath, "Database1.accdb")
        }
    Private BuilderForCounties As New OleDb.OleDbConnectionStringBuilder With
        {
            .Provider = "Microsoft.ACE.OLEDB.12.0",
            .DataSource = IO.Path.Combine(Application.StartupPath, "Capitals4.accdb")
        }
    ''' <summary>
    ''' Used to remove the current item selected in the txtFirstName text box.
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <remarks></remarks>
    Public Sub RemmoveFemale(ByVal Name As String)
        Using cn As New OleDb.OleDbConnection With
                {
                    .ConnectionString = BuilderForFemales.ConnectionString
                }
            Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                cmd.CommandText =
                    <SQL>
                        DELETE FROM FirstNames WHERE FirstName = @FirstName
                    </SQL>.Value

                cmd.Parameters.Add(New OleDb.OleDbParameter With
                                   {
                                       .DbType = DbType.String,
                                       .ParameterName = "@FirstName",
                                       .Value = Name
                                   }
                               )

                cn.Open()
                Dim Affected As Int32 = cmd.ExecuteNonQuery

            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Called in Form1 on FormClosing event to update the database table if
    ''' needed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Public Sub UpdateFemaleNames(ByVal sender As AutoCompleteStringCollection)
        Dim NewNames As New List(Of String)

        Using cn As New OleDb.OleDbConnection With
                {
                    .ConnectionString = BuilderForFemales.ConnectionString
                }
            Dim cmd As New OleDb.OleDbCommand With {.Connection = cn}
            cmd.CommandText =
                <SQL>
                        SELECT FirstName
                        FROM FirstNames
                        WHERE FirstName = @FirstName
                    </SQL>.Value

            cmd.Parameters.Add(
                New OleDb.OleDbParameter With
                {
                    .DbType = DbType.String,
                    .ParameterName = "@FirstName"
                }
            )

            cn.Open()

            For x As Int32 = 0 To sender.Count - 1
                cmd.Parameters("@FirstName").Value = sender.Item(x)
                Dim Result As String = CStr(cmd.ExecuteScalar)
                If String.IsNullOrWhiteSpace(Result) Then
                    NewNames.Add(sender.Item(x))
                End If
            Next

            If NewNames.Count > 0 Then

                cmd = New OleDb.OleDbCommand With {.Connection = cn}

                cmd.CommandText =
                    <SQL>
                                INSERT INTO FirstNames 
                                (FirstName,Gender) 
                                VALUES (@FirstName,@Gender)
                            </SQL>.Value

                cmd.Parameters.Add(
                New OleDb.OleDbParameter With
                {
                    .DbType = DbType.String,
                    .ParameterName = "@FirstName"
                })

                cmd.Parameters.Add(
                    New OleDb.OleDbParameter With
                    {
                        .DbType = DbType.String,
                        .ParameterName = "@Gender",
                        .Value = "Female"
                    }
                )

                For Each Item In NewNames
                    cmd.Parameters("@FirstName").Value = Item
                    cmd.ExecuteReader()
                Next

            End If

        End Using

    End Sub
    ''' <summary>
    ''' Used in Form1 DataGridView1 for learning purposes only
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AllFemaleNames() As DataTable
        Dim dt As New DataTable
        Using cn As New OleDb.OleDbConnection With
                {
                    .ConnectionString = BuilderForFemales.ConnectionString
                }
            Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                cmd.CommandText =
                    <SQL>
                        SELECT Identifier, FirstName
                        FROM FirstNames
                        WHERE Gender = 'Female'
                        ORDER BY FirstName;
                    </SQL>.Value

                cn.Open()

                dt.Load(cmd.ExecuteReader)

            End Using
        End Using

        Return dt
    End Function
    Public Function LoadAllCountries() As DataTable
        Dim dt As New DataTable

        Using cn As New OleDb.OleDbConnection With
                {
                    .ConnectionString = BuilderForCounties.ConnectionString
                }
            Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                cmd.CommandText =
                    <SQL>
                        SELECT 
                            Identifier, 
                            Country, 
                            Capital
                        FROM countrylist;
                    </SQL>.Value

                cn.Open()

                dt.Load(cmd.ExecuteReader)

            End Using
        End Using

        Return dt
    End Function
    ''' <summary>
    ''' Load only female first names into the auto complete source
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadFemaleNames() As AutoCompleteStringCollection
        Dim TheNameList As New AutoCompleteStringCollection

        Using cn As New OleDb.OleDbConnection With
                {
                    .ConnectionString = BuilderForFemales.ConnectionString
                }
            Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                cmd.CommandText =
                    <SQL>
                        SELECT FirstName
                        FROM FirstNames
                        WHERE Gender = 'Female'
                        ORDER BY FirstName;
                    </SQL>.Value

                cn.Open()
                Dim Reader As OleDb.OleDbDataReader = cmd.ExecuteReader

                If Reader.HasRows Then
                    While Reader.Read
                        TheNameList.Add(Reader.GetString(0))
                    End While

                    Reader.Close()

                End If

            End Using
        End Using

        Return TheNameList

    End Function

End Module
