Imports System.Threading

Public Class Form1
    Private ct As CancellationTokenSource

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ActiveControl = cmdGetData
    End Sub

    Private Sub cmdGetData_Click(sender As Object, e As EventArgs) Handles cmdGetData.Click
        ResetData()
        Dim w As New DataOperations()
        w.Caller = Me
        w.Label = label1

        dataGridView1.DataSource = w.DataTable

        ct = New CancellationTokenSource()
        Task.Run(Function() w.GetDataAsync(ct.Token))
    End Sub
    Private Sub ResetData()
        label1.Text = "0"
        label1.Refresh()

        If ct IsNot Nothing Then
            ct.Cancel()
        End If

        If dataGridView1.DataSource IsNot Nothing Then
            Dim dt = CType(dataGridView1.DataSource, DataTable)
            dt.Rows.Clear()
            dataGridView1.Refresh()

            Thread.Sleep(1000)
        End If
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        ct.Cancel()
    End Sub
End Class
