Imports System.Data.SqlClient
Imports ParameterView

''' <summary>
''' For demoing ActualCommandTextByNames language extension.
''' 
''' IMPORTANT NOTE
''' Each parameter must have a unique name. It makes sense
''' to have meaningful names when you are within a complex
''' section of data so nothing is left to chance, you know
''' exactly what each part of the code does even with 
''' MS-Access SQL statements even if MS-Access does not
''' make use of them but instead works ordinally thru the
''' parameters.
''' 
''' </summary>
''' <remarks></remarks>
Public Class frmMainForm
    Private HasBeenShown As Boolean = False
    ''' <summary>
    ''' Used to show giving command parameters meaningful names
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSimulation1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimulation1.Click
        Dim SomeValue As Integer = 100

        Dim InsertStatement As String = _
         <SQL>
            INSERT INTO CustomerMaster (AccountNumber,UDNumeric1,CreatedDate,Comments) VALUES (@AccountNumber,@UDNumeric1, @CreatedDate, @Comments)
		</SQL>.Value

        Using cn As New OleDb.OleDbConnection
            Using cmd As New OleDb.OleDbCommand
                cmd.CommandText = InsertStatement

                cmd.Parameters.AddRange(New OleDb.OleDbParameter() _
                    {
                        New OleDb.OleDbParameter With {.ParameterName = "@AccountNumber", .DbType = DbType.Int32},
                        New OleDb.OleDbParameter With {.ParameterName = "@UDNumeric1", .DbType = DbType.Int32},
                        New OleDb.OleDbParameter With {.ParameterName = "@CreatedDate", .DbType = DbType.Date},
                        New OleDb.OleDbParameter With {.ParameterName = "@Comments", .DbType = DbType.String}
                    }
                )

                cmd.Parameters(0).Value = SomeValue
                cmd.Parameters(1).Value = 12
                cmd.Parameters(2).Value = Now
                cmd.Parameters(3).Value = SomeValue

                Dim f As New frmParamViewer
                Try
                    f.Text = "Using names"
                    f.ShowMessage = Not HasBeenShown
                    f.CommandText = cmd.ActualCommandTextByNames
                    f.ShowDialog()
                Finally
                    HasBeenShown = True
                    f.Dispose()
                End Try
            End Using
        End Using
    End Sub
    ''' <summary>
    ''' Used to show giving command parameters unique names but have no meaning
    ''' to the underlying data which allows the language extension
    ''' ActualCommandTextByNames to properly display values for parameters.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSimulation2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimulation2.Click
        Dim SomeValue As Integer = 100

        Dim InsertStatement As String = _
         <SQL>
            INSERT INTO CustomerMaster (AccountNumber,UDNumeric1,CreatedDate,Comments) VALUES (@P1,@P2, @P3, @P4)
		</SQL>.Value

        Using cn As New OleDb.OleDbConnection
            Using cmd As New OleDb.OleDbCommand
                cmd.CommandText = InsertStatement

                cmd.Parameters.AddRange(New OleDb.OleDbParameter() _
                    {
                        New OleDb.OleDbParameter With {.ParameterName = "@P1", .DbType = DbType.Int32},
                        New OleDb.OleDbParameter With {.ParameterName = "@P2", .DbType = DbType.Int32},
                        New OleDb.OleDbParameter With {.ParameterName = "@P3", .DbType = DbType.Date},
                        New OleDb.OleDbParameter With {.ParameterName = "@P4", .DbType = DbType.String}
                    }
                )

                cmd.Parameters(0).Value = SomeValue
                cmd.Parameters(1).Value = 12
                cmd.Parameters(2).Value = Now
                cmd.Parameters(3).Value = SomeValue

                Dim f As New frmParamViewer
                Try
                    f.Text = "Using Pn"
                    f.ShowMessage = Not HasBeenShown
                    f.CommandText = cmd.ActualCommandTextByNames
                    f.ShowDialog()
                Finally
                    HasBeenShown = True
                    f.Dispose()
                End Try
            End Using
        End Using
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSimulation3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSimulation3.Click
        Dim SomeValue As Integer = 100

        Dim InsertStatement As String = _
         <SQL>
            INSERT INTO CustomerMaster (AccountNumber,UDNumeric1,CreatedDate,Comments) VALUES (@AccountNumber,@UDNumeric1, @CreatedDate, @Comments)
		</SQL>.Value

        Using cn As New SqlConnection
            Using cmd As New SqlCommand
                cmd.CommandText = InsertStatement

                cmd.Parameters.AddRange(New SqlParameter() _
                    {
                        New SqlParameter With {.ParameterName = "@AccountNumber", .DbType = DbType.Int32},
                        New SqlParameter With {.ParameterName = "@UDNumeric1", .DbType = DbType.Int32},
                        New SqlParameter With {.ParameterName = "@CreatedDate", .DbType = DbType.Date},
                        New SqlParameter With {.ParameterName = "@Comments", .DbType = DbType.String}
                    }
                )

                cmd.Parameters(0).Value = SomeValue
                cmd.Parameters(1).Value = 12
                cmd.Parameters(2).Value = Now
                cmd.Parameters(3).Value = SomeValue

                Dim f As New frmParamViewer
                Try
                    f.Text = "Using names"
                    f.ShowMessage = Not HasBeenShown
                    f.CommandText = cmd.ActualCommandTextByNames
                    f.ShowDialog()
                Finally
                    HasBeenShown = True
                    f.Dispose()
                End Try
            End Using
        End Using
    End Sub
    Private Sub frmMainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        WebBrowser1.DocumentText = My.Resources.IntroductionText
        ActiveControl = cmdSimulation1
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Close()
    End Sub
End Class
