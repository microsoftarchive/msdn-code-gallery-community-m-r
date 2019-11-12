Imports System.Windows.Forms
''' <summary>
''' Contains two methods for moving DataRows up/down. 
''' You could easily tweak the code to work for say a ListBox.
''' </summary>
''' <remarks></remarks>
Public Module LanguageExtensions
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Function GetChecked(ByVal sender As DataTable, ByVal ColumnName As String) As DataTable
        Dim d = (From T In sender.AsEnumerable Where T.Field(Of Boolean)(ColumnName) = True).ToList
        Dim dt = sender.Clone
        For Each row In d
            dt.Rows.Add(row.ItemArray)
        Next
        dt.Columns(ColumnName).ColumnMapping = MappingType.Hidden
        Return dt
    End Function

    ''' <summary>
    ''' Used to copy columns from another DataGridView to this DataGridView
    ''' </summary>
    ''' <param name="Self"></param>
    ''' <param name="CloneFrom"></param>
    ''' <remarks>
    ''' Only does cloning if Self has no columns
    ''' </remarks>
    <Runtime.CompilerServices.Extension()> _
    Public Sub CloneColumns(ByVal Self As DataGridView, ByVal CloneFrom As DataGridView)
        If Self.ColumnCount = 0 Then
            For Each c As DataGridViewColumn In CloneFrom.Columns
                Self.Columns.Add(CType(c.Clone, DataGridViewColumn))
            Next
        End If
    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub MoveRowUp(ByVal sender As DataGridView, ByVal bs As BindingSource)
        If Not String.IsNullOrWhiteSpace(bs.Sort) Then
            bs.Sort = ""
        End If
        Dim CurrentColumnIndex As Integer = sender.CurrentCell.ColumnIndex
        Dim NewIndex As Int32 = CInt(IIf(bs.Position = 0, 0, bs.Position - 1))
        Dim dt = CType(bs.DataSource, DataTable)
        Dim RowToMove As DataRow = DirectCast(bs.Current, DataRowView).Row
        Dim NewRow As DataRow = dt.NewRow

        NewRow.ItemArray = RowToMove.ItemArray
        dt.Rows.RemoveAt(bs.Position)
        dt.Rows.InsertAt(NewRow, NewIndex)
        dt.AcceptChanges()
        bs.Position = NewIndex
        sender.CurrentCell = sender(CurrentColumnIndex, NewIndex)
    End Sub
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub MoveRowUp(ByVal sender As BindingSource)
        If Not String.IsNullOrWhiteSpace(sender.Sort) Then
            sender.Sort = ""
        End If

        Dim NewIndex As Int32 = CInt(IIf(sender.Position = 0, 0, sender.Position - 1))
        Dim dt = CType(sender.DataSource, DataTable)
        Dim RowToMove As DataRow = DirectCast(sender.Current, DataRowView).Row
        Dim NewRow As DataRow = dt.NewRow

        NewRow.ItemArray = RowToMove.ItemArray
        dt.Rows.RemoveAt(sender.Position)
        dt.Rows.InsertAt(NewRow, NewIndex)
        dt.AcceptChanges()
        sender.Position = NewIndex

    End Sub
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub MoveRowDown(ByVal sender As DataGridView, ByVal bs As BindingSource)
        If Not String.IsNullOrWhiteSpace(bs.Sort) Then
            bs.Sort = ""
        End If
        Dim CurrentColumnIndex As Integer = sender.CurrentCell.ColumnIndex
        Dim UpperLimit As Int32 = bs.Count - 1
        Dim NewIndex As Int32 = CInt(IIf(bs.Position + 1 >= UpperLimit, UpperLimit, bs.Position + 1))
        Dim dt = CType(bs.DataSource, DataTable)
        Dim RowToMove As DataRow = DirectCast(bs.Current, DataRowView).Row
        Dim NewRow As DataRow = dt.NewRow

        NewRow.ItemArray = RowToMove.ItemArray
        dt.Rows.RemoveAt(bs.Position)
        dt.Rows.InsertAt(NewRow, NewIndex)
        dt.AcceptChanges()
        bs.Position = NewIndex
        sender.CurrentCell = sender(CurrentColumnIndex, NewIndex)
    End Sub
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub MoveRowDown(ByVal sender As BindingSource)
        If Not String.IsNullOrWhiteSpace(sender.Sort) Then
            sender.Sort = ""
        End If

        Dim UpperLimit As Int32 = sender.Count - 1
        Dim NewIndex As Int32 = CInt(IIf(sender.Position + 1 >= UpperLimit, UpperLimit, sender.Position + 1))
        Dim dt = CType(sender.DataSource, DataTable)
        Dim RowToMove As DataRow = DirectCast(sender.Current, DataRowView).Row
        Dim NewRow As DataRow = dt.NewRow

        NewRow.ItemArray = RowToMove.ItemArray
        dt.Rows.RemoveAt(sender.Position)
        dt.Rows.InsertAt(NewRow, NewIndex)
        dt.AcceptChanges()
        sender.Position = NewIndex

    End Sub

    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub MoveRowUp(ByVal Sender As ListBox, ByVal bs As BindingSource)
        If Not String.IsNullOrWhiteSpace(bs.Sort) Then
            bs.Sort = ""
        End If

        Dim DisplayText As String = Sender.Text
        Dim SelectedIndex As Int32 = bs.Position
        Dim SelectedItem As String = Sender.SelectedItem.ToString()
        Dim NewIndex As Int32 = CInt(IIf(bs.Position = 0, 0, bs.Position - 1))
        Dim dt = CType(bs.DataSource, DataTable)
        Dim RowToMove As DataRow = DirectCast(bs.Current, DataRowView).Row
        Dim NewRow As DataRow = dt.NewRow
        NewRow.ItemArray = RowToMove.ItemArray
        dt.Rows.RemoveAt(SelectedIndex)
        dt.Rows.InsertAt(NewRow, NewIndex)

        dt.AcceptChanges()
        bs.Position = bs.Find(Sender.DisplayMember, DisplayText)

        For x As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(x).Item(2) = x
        Next
    End Sub
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub MoveRowDown(ByVal Sender As ListBox, ByVal bs As BindingSource)
        If Not String.IsNullOrWhiteSpace(bs.Sort) Then
            bs.Sort = ""
        End If

        Dim DisplayText As String = Sender.Text
        Dim SelectIndex As Int32 = bs.Position
        Dim SelectedItem As String = Sender.SelectedItem.ToString()
        Dim UpperLimit As Int32 = bs.Count - 1
        Dim NewIndex As Int32 = CInt(IIf(bs.Position + 1 >= UpperLimit, UpperLimit, bs.Position + 1))
        Dim dt = CType(bs.DataSource, DataTable)
        Dim RowToMove As DataRow = DirectCast(bs.Current, DataRowView).Row
        Dim NewRow As DataRow = dt.NewRow
        NewRow.ItemArray = RowToMove.ItemArray
        dt.Rows.RemoveAt(SelectIndex)
        dt.Rows.InsertAt(NewRow, NewIndex)

        dt.AcceptChanges()
        bs.Position = bs.Find(Sender.DisplayMember, DisplayText)

        For x As Integer = 0 To dt.Rows.Count - 1
            dt.Rows(x).Item(2) = x
        Next

    End Sub
End Module
