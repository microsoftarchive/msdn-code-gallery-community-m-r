Imports DataAccess
Imports ParameterView.CommandPeeker
Imports ParameterView.DataTableExtensions
Public Class frmMainForm

    WithEvents bsCustomers As New BindingSource

    Private Sub frmMainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ErrorMessage As String = ""
        Dim Data As New CustomerData

        If Data.LoadContactTypes(ErrorMessage) Then
            cboContactTypes.DisplayMember = "Title"
            cboContactTypes.DataSource = Data.ContactTypes
        Else
            MessageBox.Show(ErrorMessage)
        End If
    End Sub
    Private Sub cmdSelectByContacts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectByContacts.Click
        LoadContactsByType()
        For Each col As DataGridViewColumn In DataGridView1.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next
    End Sub
    Private Sub frmMainForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        LoadContactsByType()
        For Each col As DataGridViewColumn In DataGridView1.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next
    End Sub
    Private Sub LoadContactsByType()
        Dim ErrorMessage As String = ""
        Dim Data As New CustomerData

        If cboContactTypes.Text = DataAccess.ProjectGlobals.SelectAllText Then
            If Data.LoadCustomers(ErrorMessage) Then
                bsCustomers.DataSource = Data.Customers
                DataGridView1.DataSource = bsCustomers
            End If
        Else
            If Data.LoadCustomersByContactType(ErrorMessage, cboContactTypes.Text) Then
                bsCustomers.DataSource = Data.Customers
                DataGridView1.DataSource = bsCustomers
            End If
        End If
    End Sub
    ''' <summary>
    ''' Here the Where clause of a select statement is seeded with a bad value. This can happen say
    ''' when a user enters text into a TextBox that is not valid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdBad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBad.Click
        Dim dt As New DataTable

        Dim Builder As New OleDb.OleDbConnectionStringBuilder With
            {
                .Provider = "Microsoft.ACE.OLEDB.12.0",
                .DataSource = IO.Path.Combine(Application.StartupPath, "Data", "Database1.accdb")
            }
        Using cn As New OleDb.OleDbConnection With {.ConnectionString = Builder.ConnectionString}
            Using cmd As New OleDb.OleDbCommand With {.Connection = cn}
                cmd.CommandText =
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
                        WHERE 
                            (((Customers.ContactTitle)=@P1))
                    </SQL>.Value

                cmd.Parameters.Add(New OleDb.OleDbParameter With {.ParameterName = "@P1", .DbType = DbType.String})
                cmd.Parameters(0).Value = "Whatever"

                cn.Open()

                dt.Load(cmd.ExecuteReader)

                If My.Application.IsRunningUnderDebugger Then
                    Dim FileName As String = IO.Path.Combine(Application.StartupPath, "CommandText.txt")
                    dt.DiagnoseBadSql(cmd, FileName, chkShowSqlStatements.Checked)
                End If

            End Using
        End Using

        bsCustomers.DataSource = dt
        DataGridView1.DataSource = bsCustomers
    End Sub
End Class
