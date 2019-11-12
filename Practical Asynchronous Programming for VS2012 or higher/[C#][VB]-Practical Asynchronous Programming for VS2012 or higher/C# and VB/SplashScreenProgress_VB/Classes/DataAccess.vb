Imports System.Data.OleDb
''' <summary>
''' Responsible for obtain data from a MS-Access database table,
''' create a customer object, push it back to the caller using
''' an Iterator routine that pushes data back using Yield statement.
''' 
''' In the above process we are popping information to a Splash screen
''' via a delegate. We could do this in the caller but I see no reason
''' why for this simple demonstration.
''' </summary>
''' <remarks>
''' Since there are not many records Threading.Thread.Sleep(25) is used
''' to slow things down to see the results in the splash screen.
''' </remarks>
Public Class DataAccess

    Private Builder As New OleDbConnectionStringBuilder With
        {
            .Provider = "Microsoft.ACE.OLEDB.12.0",
            .DataSource = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database1.accdb")
        }

    Private ConnectionString As String = ""
    Private Delay As Integer = 0

    Public Sub New(ByVal Delay As Integer)
        CurrentIndex = 1
        ConnectionString = Builder.ConnectionString
        Me.Delay = Delay
        GetRecordCount()
    End Sub
    Private mRecordCount As Integer
    Public ReadOnly Property RecordCount As Integer
        Get
            Return mRecordCount
        End Get
    End Property
    ''' <summary>
    ''' Get total records to be used with ProgressBar on SplashScreen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetRecordCount()
        Using cn As New OleDbConnection With {.ConnectionString = ConnectionString}
            Using cmd As New OleDbCommand With {.Connection = cn, .CommandText = "SELECT Count(Identifier) FROM Customer"}
                cn.Open()
                mRecordCount = CInt(cmd.ExecuteScalar)
            End Using
        End Using
    End Sub
    Public Property CurrentIndex As Integer
    ''' <summary>
    ''' used to return customer data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Iterator Function Retrieve() As IEnumerable(Of Customer)
        CurrentIndex = 0
        Dim CustomerIdentifierPosition As Integer = 0
        Dim CustomerNamePosition As Integer = 1
        Dim ContactNamePosition As Integer = 2

        Using cn As New OleDbConnection With {.ConnectionString = ConnectionString}

            Using cmd As New OleDbCommand With
                {
                    .Connection = cn,
                    .CommandText = "SELECT Identifier, CompanyName, ContactName FROM Customer;"
                }

                cn.Open()

                Dim Reader As OleDbDataReader = cmd.ExecuteReader

                If Reader.HasRows Then

                    Dim CompanyName As String = ""

                    While Reader.Read

                        CompanyName = Reader.GetFieldValue(Of String)(CustomerNamePosition)

                        Yield New Customer With
                              {
                                  .Identifier = Reader.GetFieldValue(Of Integer)(CustomerIdentifierPosition),
                                  .Company = CompanyName,
                                  .ContactName = Reader.GetFieldValue(Of String)(ContactNamePosition)
                              }

                        Dim PercentDone As Integer = CInt((CurrentIndex / Me.RecordCount) * 100)
                        SplashScreen1.ShowRecord(PercentDone)
                        Threading.Thread.Sleep(Delay)

                        CurrentIndex += 1

                    End While

                End If

                Reader.Close()

            End Using
        End Using

    End Function
End Class
