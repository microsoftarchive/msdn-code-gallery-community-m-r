Public Module ExtensionMethods
    <Runtime.CompilerServices.Extension()>
    Public Function GetRowsChecked(ByVal pTable As DataTable, ByVal pColumnName As String) As List(Of DataRow)
        Return pTable.AsEnumerable.Where(Function(row) row.Field(Of Boolean)(pColumnName)).ToList
    End Function
    <DebuggerStepThrough()>
    <Runtime.CompilerServices.Extension()>
    Public Sub ExpandColumns(ByVal pDataGridView As DataGridView)
        For Each col As DataGridViewColumn In pDataGridView.Columns
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next
    End Sub
End Module
