Imports Microsoft.Data.ConnectionUI
Imports System.Data.Common
Imports System.Data.SqlClient
''' <summary>
''' This is a sample to show how to create a database, tables, populate the tables
''' using Microsoft's database connection dialog.
''' 
''' In the bin\Debug folder there is DataConnection.xml which is setup to use
''' SqlClient provider as the default, remove it and there will be no default which
''' may be what some developers want.
''' </summary>
''' <remarks>
''' Source code with C# examples 
''' https://code.msdn.microsoft.com/Using-Visual-Studio-a7e740f8
''' 
''' Improvements:
''' * Several public properties could be private
''' * SQL scripts could be moved into their own classes or into resource file.
''' * TableNames serves no purpose for this task but left here as it does not harm.
''' </remarks>
Public Class Operations
    Private mException As Exception
    Public ReadOnly Property LastException As Exception
        Get
            Return mException
        End Get
    End Property
    Private mBuilder As SqlConnectionStringBuilder = New SqlConnectionStringBuilder()
    Public ReadOnly Property ConnectionString As String
        Get
            Return mBuilder.ConnectionString
        End Get
    End Property
    Private mNewConnectionString As String
    Public ReadOnly Property NewConnectionString As String
        Get
            Return mNewConnectionString
        End Get
    End Property
    ''' <summary>
    ''' SQL-Server name returned in GetConnection method
    ''' </summary>
    Public Property ServerName() As String
    Public Property DatabaseName As String

    ''' <summary>
    ''' SQL-Server database returned in GetConnection method
    ''' </summary>
    Public Property InitialCatalog() As String
    ''' <summary>
    ''' Table names in ServerName.InitialCatalog 
    ''' </summary>
    Public Property TableNames() As List(Of String)
    Public Sub New()
        InitialCatalog = "SampleDatabase"
        DatabaseName = "SampleDatabase"
    End Sub
    ''' <summary>
    ''' Create connection string using Microsoft's ConnectionUI class
    ''' </summary>
    ''' <param name="SaveConfiguration"></param>
    ''' <returns></returns>
    Public Function GetConnection(ByRef DataSource As String, Optional ByVal SaveConfiguration As Boolean = False) As Boolean
        Dim success = False

        Dim dcd As New DataConnectionDialog()

        Dim dcs As New DataConnectionConfiguration(Nothing)

        dcs.LoadConfiguration(dcd)

        If DataConnectionDialog.Show(dcd) = DialogResult.OK Then
            Dim factory As DbProviderFactory = DbProviderFactories.GetFactory(dcd.SelectedDataProvider.Name)
            Using connection = factory.CreateConnection()
                connection.ConnectionString = dcd.ConnectionString

                DataSource = connection.DataSource
                connection.Open()
                Dim cmd = connection.CreateCommand()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "SELECT * FROM INFORMATION_SCHEMA.TABLES"

                Dim dt = New DataTable()
                dt.Load(cmd.ExecuteReader())

                TableNames = dt.AsEnumerable().Select(Function(row) row.Field(Of String)("table_name")).OrderBy(Function(field) field).ToList()
            End Using

            mBuilder = New SqlConnectionStringBuilder() With {.ConnectionString = dcd.ConnectionString}

            ServerName = mBuilder.DataSource

            If SaveConfiguration Then
                dcs.SaveConfiguration(dcd)
            End If

            If TableNames.Count > 0 Then
                success = True
            End If
        End If

        Return success

    End Function
    Public Function GetData(ByVal pConnectionString As String) As DataTable
        Dim dt As New DataTable
        Using cn As New SqlConnection With {.ConnectionString = pConnectionString}
            Using cmd As New SqlCommand With {.Connection = cn}
                cmd.CommandText = PersonGenderSelect()
                cn.Open()
                dt.Load(cmd.ExecuteReader)
            End Using
        End Using

        Return dt

    End Function
    ''' <summary>
    ''' SELECT to get back our data
    ''' </summary>
    ''' <returns></returns>
    Private Function PersonGenderSelect() As String
        Return <SQL>
SELECT 
	P.id, 
	P.FirstName, 
	P.LastName, 
	G.Gender
FROM            
	Persons1 AS P INNER JOIN
    GenderTypes AS G ON P.GenderIdentifier = G.GenderIdentifier
ORDER BY P.LastName
               </SQL>.Value
    End Function
    ''' <summary>
    ''' Create our database
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateDatabase() As Boolean
        Dim success As Boolean = True
        Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
            Using cmd As New SqlCommand With {.Connection = cn}
                cmd.CommandText = $"CREATE DATABASE {DatabaseName}"
                cn.Open()
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    mException = ex
                    success = False
                End Try
            End Using
        End Using
        Return success
    End Function
    ''' <summary>
    ''' Create tables and populate
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateTablesAndPopulate() As Boolean
        Dim success As Boolean = True

        mBuilder.InitialCatalog = InitialCatalog
        mBuilder.DataSource = ServerName

        Using cn As New SqlConnection With {.ConnectionString = mBuilder.ConnectionString}
            Using cmd As New SqlCommand With {.Connection = cn}
                cmd.CommandText = GenderTableScript()
                cn.Open()
                Try
                    ' create child table 
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = PeopleTableScript()
                    ' create main table
                    cmd.ExecuteNonQuery()

                    'insert gender records
                    cmd.CommandText = InsertGenderScript()
                    cmd.ExecuteNonQuery()

                    ' insert people records
                    cmd.CommandText = InsertPeopleScript()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    mException = ex
                    success = False
                End Try
            End Using
        End Using

        If success Then
            mNewConnectionString = mBuilder.ConnectionString
        End If
        Return success
    End Function
    ''' <summary>
    ''' SQL Script to generate gender table
    ''' </summary>
    ''' <returns></returns>
    Private Function GenderTableScript() As String
        Return <SQL>
CREATE TABLE [dbo].[GenderTypes](
	[GenderIdentifier] [INT] IDENTITY(1,1) NOT NULL,
	[Gender] [NVARCHAR](MAX) NULL,
 CONSTRAINT [PK_GenderTypes] PRIMARY KEY CLUSTERED 
(
	[GenderIdentifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

               </SQL>.Value
    End Function
    ''' <summary>
    ''' SQl script to populate gender table
    ''' </summary>
    ''' <returns></returns>
    Private Function InsertGenderScript() As String
        Return <SQL>
INSERT INTO [GenderTypes]
VALUES
( N'Female' ), 
( N'Male' ), 
( N'Non binary' )
               </SQL>.Value
    End Function
    ''' <summary>
    ''' Sql script to create person table
    ''' </summary>
    ''' <returns></returns>
    Private Function PeopleTableScript() As String
        Return <SQL>
CREATE TABLE [dbo].[Persons1](
	[id] [INT] IDENTITY(1,1) NOT NULL,
	[FirstName] [NVARCHAR](MAX) NULL,
	[LastName] [NVARCHAR](MAX) NULL,
	[GenderIdentifier] [INT] NULL,
	[IsDeleted] [BIT] NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

               </SQL>.Value
    End Function
    ''' <summary>
    ''' Sql script to populate person table
    ''' </summary>
    ''' <returns></returns>
    Private Function InsertPeopleScript() As String
        Return <SQL>
INSERT INTO [Persons1]
VALUES
( N'Mary', N'Buckley', 1, 1 ), 
( N'Karen', N'Payne', 1, 1 ), 
( N'Lee', N'Warren', 1, 1 ), 
( N'Regina', N'Forbes', 1, 0 ), 
( N'Daniel', N'Kim', 2, 0 ), 
( N'Dennis', N'Nunez', 2, 0 ), 
( N'Myra', N'Zuniga', 1, 0 ), 
( N'Teddy', N'Ingram', 2, 0 ), 
( N'Annie', N'Larson', 1, 0 ), 
( N'Karen', N'Anderson', 1, 0 ), 
( N'Jenifer', N'Livingston', 1, 0 ), 
( N'Stefanie', N'Perez', 1, 0 ), 
( N'Chastity', N'Garcia', 1, 0 ), 
( N'Evelyn', N'Stokes', 1, 0 ), 
( N'Jeannie', N'Daniel', 1, 0 ), 
( N'Rickey', N'Santos', 2, 0 ), 
( N'Bobbie', N'Hurst', 2, 0 ), 
( N'Lesley', N'Lawson', 1, 0 ), 
( N'Shawna', N'Browning', 1, 0 ), 
( N'Theresa', N'Ross', 1, 0 ), 
( N'Tasha', N'Hughes', 3, 0 ), 
( N'Karla', N'Hale', 1, 0 ), 
( N'Otis', N'Holt', 2, 0 ), 
( N'Alisa', N'Browning', 3, 0 ), 
( N'Peggy', N'Donaldson', 1, 0 ), 
( N'Lisa', N'Bentley', 1, 0 ), 
( N'Vicky', N'Wiley', 1, 0 ), 
( N'Nicolas', N'Spence', 2, 0 ), 
( N'Miranda', N'Barnes', 1, 0 ), 
( N'Karen', N'Barry', 1, 1 ), 
( N'Rosemary', N'Levine', 3, 0 ), 
( N'Ernest', N'Gamble', 2, 0 ), 
( N'Lindsay', N'Henderson', 1, 0 ), 
( N'Lorenzo', N'Adams', 2, 0 ), 
( N'Tammie', N'Graves', 1, 0 ), 
( N'Kareem', N'Benton', 3, 0 ), 
( N'Cesar', N'Vance', 3, 0 ), 
( N'Charlene', N'Rocha', 1, 0 ), 
( N'Sonja', N'Mac Donald', 1, 0 ), 
( N'Gwendolyn', N'Russell', 1, 0 ), 
( N'Stephan', N'Hill', 2, 0 ), 
( N'Maggie', N'Day', 1, 0 ), 
( N'Earnest', N'Walters', 1, 0 ), 
( N'Zachary', N'Pratt', 1, 0 ), 
( N'Erin', N'Hinton', 3, 0 ), 
( N'Rodolfo', N'Collier', 2, 0 ), 
( N'Carla', N'Jackson', 1, 0 ), 
( N'Norma', N'Robles', 1, 0 ), 
( N'Jean', N'Haynes', 1, 0 ), 
( N'Tara', N'Pope', 3, 0 ), 
( N'Ann', N'Patterson', 1, 0 ), 
( N'Nancy', N'Lebow', 3, 0 ), 
( N'Joe', N'Hansen', 2, 0 ), 
( N'Joe', N'Hansen', 2, 0 ), 
( N'Jill', N'Gallagher', 1, 0 ), 
( N'Sunshine', N'Miller', 1, 0 ), 
( N'Annabelle', N'Huff', 1, 0 ), 
( N'Pam', N'Gallagher', 3, 0 )
               </SQL>.Value
    End Function
End Class
