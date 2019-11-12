Public Class Form1

    Dim ops As New DataOperations
    Dim bsCustomers As New BindingSource
    ''' <summary>
    ''' Load our data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bsCustomers.DataSource = ops.LoadCustomers
        DataGridView1.DataSource = bsCustomers
        DataGridView1.Columns("Process").Width = 30
        DataGridView1.Columns("Process").HeaderText = ""
        DataGridView1.Columns("CompanyName").HeaderText = "Name"
        DataGridView1.Columns("ContactTitle").HeaderText = "Title"
        DataGridView1.ExpandColumns
    End Sub
    ''' <summary>
    ''' Get selected rows via the DataGridViewCheckBoxColumn
    ''' pass the rows to the data class, if the rows are removed
    ''' then remove the rows from the DataTable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdDeleteSelected_Click(sender As Object, e As EventArgs) Handles cmdDeleteSelected.Click

        Dim dt As DataTable = CType(bsCustomers.DataSource, DataTable)
        Dim rows As List(Of DataRow) = dt.GetRowsChecked("Process")

        If rows.Count > 0 Then
            If ops.DeleteCustomers(rows) Then
                Dim row As DataRow = Nothing
                Dim xInd As Integer = 0
                For indexer As Integer = 0 To rows.Count - 1
                    xInd = indexer
                    dt.Rows.Remove(dt.AsEnumerable.FirstOrDefault(
                        Function(xrow) xrow.Field(Of Integer)("Identifier") = rows(xInd).Field(Of Integer)("Identifier")))
                Next
            Else
                MessageBox.Show($"Encountered errors: {ops.LastExceptionMessage}")
            End If
        Else
            MessageBox.Show("No rows selected")
        End If
    End Sub
    ''' <summary>
    ''' Delete the selected row from the database table, if successful delete the row
    ''' from the DataGridView via the BindingSource cast to a DataRow
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdDeleteCurrent_Click(sender As Object, e As EventArgs) Handles cmdDeleteCurrent.Click
        Dim currentRow As DataRow = CType(bsCustomers.Current, DataRowView).Row
        If ops.DeleteSingleCustomer(currentRow) Then
            bsCustomers.RemoveCurrent()
        Else
            MessageBox.Show($"Encountered errors: {ops.LastExceptionMessage}")
        End If
    End Sub

End Class


