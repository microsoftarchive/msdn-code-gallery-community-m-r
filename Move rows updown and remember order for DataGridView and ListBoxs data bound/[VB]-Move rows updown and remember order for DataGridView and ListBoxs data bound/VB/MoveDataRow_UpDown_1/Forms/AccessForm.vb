Imports DataAccess
Imports UpDownExtensionMethods
''' <summary>
''' </summary>
''' <remarks>
''' * As noted in the article sorting must be turned off during the repositioning
''' * Any sort can cause issues and this form allows sorting which complicates matters
'''   while the other two forms have no sort options thus no issues. In the end you
'''   may get what looks like duplicate rows which is untrue if sorting after load.
''' </remarks>
Public Class frmAccessForm
    WithEvents bsData As New BindingSource
    Private HasChanges As Boolean = False
    ''' <summary>
    ''' Save changes back to database table which updates
    ''' the column RowPosition which when loading data
    ''' sorts on RowPosition.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If HasChanges Then
            Dim dt = CType(bsData.DataSource, DataTable)
            UpdatePosition(CType(bsData.DataSource, DataTable))
        End If
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader
        DataGridView1.EnableHeadersVisualStyles = False

        Dim dt = LoadCustomersAccessForm()

        bsData.DataSource = dt
        Label1.DataBindings.Add("Text", bsData, "Identifier")
        DataGridView1.DataSource = bsData
        DataGridView1.Columns("Process").DisplayIndex = 0
        DataGridView1.CurrentCell = DataGridView1(1, 1)

        For Each Column As DataGridViewColumn In DataGridView1.Columns
            Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next

        DataGridView1.Columns("CompanyName").HeaderText = "Company"
        DataGridView1.Columns("ContactName").HeaderText = "Contact"
        DataGridView1.Columns("ContactTitle").HeaderText = "Title"
        cboFilter.SelectedIndex = 2

        '
        ' 12/5 changes to demo copying Data from DataGridView1 to DataGridView2
        '
        Dim TheClone = dt.Clone
        TheClone.Columns("Process").ColumnMapping = MappingType.Hidden
        DataGridView2.DataSource = TheClone

    End Sub
    Private Sub cmdMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUp.Click
        HasChanges = True
        DataGridView1.MoveRowUp(bsData)
    End Sub
    Private Sub cmdMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDown.Click
        HasChanges = True
        DataGridView1.MoveRowDown(bsData)
    End Sub
    ''' <summary>
    ''' Allows person running code to sort by one of three columns
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSort.Click
        Dim CompanyID As String = CType(bsData.Current, DataRowView).Item("Identifier").ToString

        If cboFilter.SelectedIndex = 0 Then
            bsData.Sort = "CompanyName ASC"
        ElseIf cboFilter.SelectedIndex = 1 Then
            bsData.Sort = "Identifier ASC"
        ElseIf cboFilter.SelectedIndex = 2 Then
            bsData.Sort = "RowPosition ASC"
        End If

        bsData.Position = bsData.Find("Identifier", CompanyID)

    End Sub
    ''' <summary>
    ''' Shows row number in RowHeader which allows the person running this
    ''' code to track what is going on.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If Not DataGridView1.Rows(e.RowIndex).IsNewRow Then
            DataGridView1.Rows(e.RowIndex).HeaderCell.Value = CStr(e.RowIndex + 1)
        End If
    End Sub

    ''' <summary>
    ''' Copies data rows from top DataGridView where Proccess is True to
    ''' bottom DataGridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub cmdCopyFromTopToBottom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyFromTopToBottom.Click

        Dim dtFromTop = CType(bsData.DataSource, DataTable).GetChecked("Process")
        Dim dtBottom = CType(DataGridView2.DataSource, DataTable)

        For Each row As DataRow In dtFromTop.Rows
            If dtBottom.Select("CompanyName ='" & row.Field(Of String)("CompanyName") & "'").Count = 0 Then
                dtBottom.ImportRow(row)
                CType(bsData.Item(bsData.Find("CompanyName", row.Field(Of String)("CompanyName"))), DataRowView).Row.SetField(Of Boolean)("Process", False)
            Else
                '
                ' Deny duplication row
                '
            End If
        Next
    End Sub
    Private Sub cmdClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Close()
    End Sub
End Class
