Imports System.Threading

Public Class CustomersForm
    Private Const WM_CLOSE As Integer = &H10

    Private tokenSource As New CancellationTokenSource()
    Private token As CancellationToken = tokenSource.Token
    Private CurrentlyRunning As Boolean = False

    Private dt As New DataTable With {.TableName = "MyTable"}

    Public Delegate Sub UpdateDataTableDelegate(ByVal value As Object(), ByVal Message As String)
    Public Delegate Sub UpdateDataTableDelegateProgress(ByVal value As Object(), ByVal CurrentPosition As Integer, ByVal Total As Integer)

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_CLOSE Then
            'MessageBox.Show("Closing")
        End If
        MyBase.WndProc(m)
    End Sub

    ''' <summary>
    ''' Version 1: updates a label showing current position and total rows
    ''' value contains an array of values representing field values from
    ''' the backend database pushed here from DataAccess.LoadCustomers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' I check for a column count which is three unless the operation was 
    ''' inadvertently cancelled i.e. closed form while populating rows.
    ''' </remarks>
    Public Sub UpDateDataTable(ByVal sender As Object(), ByVal Message As String)
        If InvokeRequired Then
            Invoke(New UpdateDataTableDelegate(AddressOf UpDateDataTable), sender, Message)
        Else
            If dt.Columns.Count > 0 Then
                dt.Rows.Add(sender)
                Label1.Text = Message
            End If
        End If
    End Sub
    ''' <summary>
    ''' Version 2: updates a progressbar showing current position
    ''' value contains an array of values representing field values from
    ''' the backend database pushed here from DataAccess.LoadCustomers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks>
    ''' I check for a column count which is three unless the operation was 
    ''' inadvertently cancelled i.e. closed form while populating rows.
    ''' </remarks>
    Public Sub UpDateDataTable(ByVal sender As Object(), ByVal CurrentPosition As Integer, ByVal Total As Integer)
        If InvokeRequired Then
            Invoke(New UpdateDataTableDelegateProgress(AddressOf UpDateDataTable), sender, CurrentPosition, Total)
        Else
            If dt.Columns.Count > 0 Then
                dt.Rows.Add(sender)
                ProgressBar1.Value = CurrentPosition
                If CurrentPosition = Total Then
                    MessageBox.Show("Done loading")
                End If
            End If
        End If
    End Sub
    Private Async Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Label1.Text = ""
        CurrentlyRunning = True
        dt.Rows.Clear()
        CustomersGrid.DataSource = dt

        Dim da As New DataAccess

        ProgressBar1.Maximum = da.RowCount

        For Each item As Object() In da.LoadCustomers(token)

            If token.IsCancellationRequested Then
                Exit For
            End If

            Await Task.Factory.StartNew(Sub() Thread.Sleep(1), token)

        Next

        If token.IsCancellationRequested Then
            tokenSource = New CancellationTokenSource()
            token = tokenSource.Token

            If chkClearRows.Checked Then
                dt.Rows.Clear()
            End If

        End If
    End Sub
    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click



        If My.Dialogs.Question("Cancel loading of data?") Then
            CurrentlyRunning = False
            tokenSource.Cancel()
        End If
    End Sub
    Private Sub CustomersForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If CurrentlyRunning Then
            If Not token.IsCancellationRequested Then
                tokenSource.Cancel()
            End If
        End If
    End Sub
    Private Sub CustomersForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dt.Columns.Add(New DataColumn With {.ColumnName = "Identifier", .DataType = GetType(Int32)})
        dt.Columns.Add(New DataColumn With {.ColumnName = "FirstName", .DataType = GetType(String)})
        dt.Columns.Add(New DataColumn With {.ColumnName = "LastName", .DataType = GetType(String)})
        Label1.Text = ""
    End Sub
End Class
