Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ops As New Operations
        ' if My.Settings.DatabaseConnectionString is not set the database has not been created
        ' so let's do it
        If String.IsNullOrWhiteSpace(My.Settings.DatabaseConnectionString) Then
            Dim dataSource = ""
            Dim ServerName As String = ""

            If ops.GetConnection(dataSource) Then
                If ops.CreateDatabase() Then
                    If ops.CreateTablesAndPopulate Then
                        My.Settings.DatabaseConnectionString = ops.NewConnectionString
                        MessageBox.Show("Done")
                    Else
                        If ops.LastException IsNot Nothing Then
                            ' could be failure on create or populate
                            MessageBox.Show($"Failed populating table : {ops.LastException}")
                        End If
                    End If
                Else
                    If ops.LastException IsNot Nothing Then
                        MessageBox.Show($"Failed to create database: {ops.LastException}")
                    End If
                End If
            Else
                ' failed to get connection
            End If

        Else
            ' database was created and populated
            DataGridView1.DataSource = ops.GetData(My.Settings.DatabaseConnectionString)
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not String.IsNullOrWhiteSpace(My.Settings.DatabaseConnectionString) Then
            Button1.Visible = False
            Dim ops As New Operations
            DataGridView1.DataSource = ops.GetData(My.Settings.DatabaseConnectionString)
        End If
    End Sub
End Class
