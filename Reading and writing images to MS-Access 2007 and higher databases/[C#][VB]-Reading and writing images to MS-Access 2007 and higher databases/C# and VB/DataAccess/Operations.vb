Public Class Operations
    Public Property PictureDataTable As DataTable
    Public Property CategoriesDataTable As DataTable
    Public Property ParkingDataTable As DataTable


    Private cn As OleDb.OleDbConnection = Nothing
#If USETHIS1 Then
    Private TempConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Dotnet_Development\Projects_Non_Business\MSDN_Extract_Images\MS_Access_Images\UsingAccess_2007_BinaryField\bin\Debug\Data\Database1.accdb"
    Public Sub DemoMe()
        Demo("KG")
    End Sub
    Private Sub Demo(ByVal UserName As String)
        If Not String.IsNullOrWhiteSpace(UserName) Then
            Using cn As New OleDb.OleDbConnection With {.ConnectionString = TempConnectionString}
                Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                    cmd.CommandText =
                    <SQL>
                        SELECT Identifier, FirstName + ' ' + LastName As FullName, JoinDate, Customers.UserName
                        FROM Customers
                        WHERE (((Customers.UserName)=@UserName));
                    </SQL>.Value

                    cmd.Parameters.AddWithValue("@UserName", UserName.ToUpper)
                    cn.Open()
                    Try
                        Dim Reader As OleDb.OleDbDataReader = cmd.ExecuteReader
                        If Reader.HasRows Then
                            Reader.Read()
                            Dim FullName As String = Reader.GetString(1)
                            Dim JoinDate As Date = Reader.GetDateTime(2)
                            '
                            ' Show data
                            '
                            Console.WriteLine("Name:{0} Joined {1}", FullName, JoinDate.ToShortDateString)
                            Reader.Close()
                            cmd.CommandText = "SELECT Max(Customers.Identifier) As iMax FROM Customers"
                            Dim MaxValue = CLng(cmd.ExecuteScalar)
                            '
                            ' Show Max identifier value, can do a similar SQL for row count too
                            '
                            Console.WriteLine("Max ID: {0}", MaxValue)
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
        End If

    End Sub

#End If

    ''' <summary>
    ''' Add image to database table
    ''' </summary>
    ''' <param name="FileName">Full file name including path</param>
    ''' <param name="Category">Key from Category table</param>
    ''' <param name="Description">User description</param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Only option parameter is Description
    ''' </remarks>
    Public Function AddImage(ByVal FileName As String, ByVal Category As Int32, ByVal Description As String) As Boolean
        Dim conn As MS_AccessConnection = MS_AccessConnection.GetInstance()
        cn = conn.GetConnection(Builder.ConnectionString)
        Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
            cmd.CommandText =
                <SQL>
                    INSERT INTO Pictures 
                    (
                        Category,
                        Picture,
                        Description,
                        BaseName,
                        FileExtension
                    ) 
                    Values
                    (
                        @Category,
                        @Picture,
                        @Description,
                        @BaseName,
                        @FileExtension
                    )
                </SQL>.Value


            cmd.Parameters.AddRange(
                New OleDb.OleDbParameter() _
                {
                    New OleDb.OleDbParameter With
                    {
                        .ParameterName = "@Category",
                        .DbType = DbType.Int32,
                        .Value = Category
                    },
                    New OleDb.OleDbParameter With
                    {
                        .ParameterName = "@Picture",
                        .OleDbType = OleDb.OleDbType.Binary,
                        .Value = FileImageBytes(FileName)
                    },
                    New OleDb.OleDbParameter With
                    {
                        .ParameterName = "@Description",
                        .DbType = DbType.String,
                        .Value = Description
                    },
                    New OleDb.OleDbParameter With
                    {
                        .ParameterName = "@BaseName",
                        .DbType = DbType.String,
                        .Value = IO.Path.GetFileNameWithoutExtension(FileName).ToLower},
                    New OleDb.OleDbParameter With
                    {
                        .ParameterName = "@FileExtension",
                        .DbType = DbType.String,
                        .Value = IO.Path.GetExtension(FileName).Replace(".", "").ToLower
                    }
                }
            )

            Try
                Dim Affected As Int32 = cmd.ExecuteNonQuery
                If Affected = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try

        End Using
    End Function

    Public Sub LoadImages()
        Dim conn As MS_AccessConnection = MS_AccessConnection.GetInstance()
        cn = conn.GetConnection(Builder.ConnectionString)

        Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
            cmd.CommandText =
                <SQL>
                    SELECT 
                        Identifier, 
                        Category
                    FROM 
                        Category
                    ORDER BY 
                        Category
                </SQL>.Value

            CategoriesDataTable = New DataTable
            CategoriesDataTable.Load(cmd.ExecuteReader)

            Dim dr As DataRow
            dr = CategoriesDataTable.NewRow
            dr("Identifier") = 0
            dr("Category") = "ALL"
            CategoriesDataTable.Rows.InsertAt(dr, 0)


            cmd.CommandText =
                <SQL>
                    SELECT 
                        Identifier, 
                        Category, 
                        Picture, 
                        Description, 
                        BaseName,
                        FileExtension,
                        BaseName + '.' + FileExtension As FullFileName
                    FROM 
                        Pictures;
                </SQL>.Value

            PictureDataTable = New DataTable
            PictureDataTable.Load(cmd.ExecuteReader)
            Console.WriteLine()

        End Using

    End Sub
    ''' <summary>
    ''' Quick example to get one row for a social forum post
    ''' </summary>
    ''' <param name="Primarykey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadSingleImage(ByVal Primarykey As Integer) As Tuple(Of String, Byte())

        Dim FileName As String = ""
        Dim ImageBytes As Byte() = {}


        Dim conn As MS_AccessConnection = MS_AccessConnection.GetInstance()
        cn = conn.GetConnection(Builder.ConnectionString)

        Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
            cmd.CommandText =
                <SQL>
                    SELECT 
                        Identifier, 
                        Category, 
                        Picture, 
                        Description, 
                        BaseName,
                        FileExtension,
                        BaseName + '.' + FileExtension As FullFileName
                    FROM 
                        Pictures
                    WHERE Identifier = ?
                </SQL>.Value
            cmd.Parameters.AddWithValue("?", Primarykey)

            Dim dt As New DataTable
            dt.Load(cmd.ExecuteReader)
            ImageBytes = dt.Rows(0).Field(Of Byte())("Picture")
            FileName = dt.Rows(0).Field(Of String)("FullFileName")

            Return New Tuple(Of String, Byte())(FileName, ImageBytes)

        End Using

    End Function
    Public Sub LoadParkingImages()
        Dim conn As MS_AccessConnection = MS_AccessConnection.GetInstance()
        cn = conn.GetConnection(Builder.ConnectionString)

        Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
            cmd.CommandText =
                <SQL>
                    SELECT 
                        Identifier, 
                        Category, 
                        Picture, 
                        Description, 
                        BaseName,
                        FileExtension,
                        BaseName + '.' + FileExtension As FullFileName
                    FROM 
                        Pictures
                    WHERE Category = 3
                </SQL>.Value

            ParkingDataTable = New DataTable
            ParkingDataTable.Load(cmd.ExecuteReader)
            Console.WriteLine()

        End Using

    End Sub
    Public Sub New()
        LoadImages()
        'LoadParkingImages()
    End Sub
End Class
