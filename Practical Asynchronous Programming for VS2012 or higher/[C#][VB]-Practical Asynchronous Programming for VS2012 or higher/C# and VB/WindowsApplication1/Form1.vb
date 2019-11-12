Imports System.Threading
Imports DataOperations_VB

Public Class Form1
    Private cts As CancellationTokenSource
    Private Sub cmdGetData_Click(sender As Object, e As EventArgs) Handles cmdGetData.Click
        ResetData()

        Dim w As New Work()
        w.Caller = Me
        w.Label = label1

        dataGridView1.DataSource = w.dtPeople

        cts = New CancellationTokenSource()
        If txtCancelTextBox.Value > 0 Then
            cts.CancelAfter(txtCancelTextBox.Value)
        End If

        Task.Run(Function() w.GetDataAsync(cts.Token))
    End Sub

    Private Sub ResetData()
        label1.Text = "0"
        label1.Refresh()

        If cts IsNot Nothing Then
            cts.Cancel()
        End If

        If dataGridView1.DataSource IsNot Nothing Then
            Dim dt = CType(dataGridView1.DataSource, DataTable)
            dt.Rows.Clear()
            dataGridView1.Refresh()

            Thread.Sleep(1000)

        End If
    End Sub
    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

        cts.Cancel()

        If dataGridView1.RowCount > 0 Then
            dataGridView1.CurrentCell = dataGridView1(0, dataGridView1.Rows.Count - 1)
        End If
    End Sub
    Private Sub Form1_Shown(sender1 As Object, e1 As EventArgs) Handles Me.Shown
        ActiveControl = cmdGetData
    End Sub
End Class
