Option Strict On
Imports BackEnd
Imports ExcelExtensionsCS
Imports DataGridViewExtensionsCS

Public Class ReadRangeForm
    Private ops As New ExcelOperations
    Private fileName As String = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Karen1.xlsx")
    Private Sub ReadRangeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim letterArray = Enumerable.
            Range(1, 200).
            Select(Function(item) CStr(item.ExcelColumnNameFromNumber)).
            ToArray

        Dim rowArray = Enumerable.Range(1, 200).
            Select(Function(item) item.ToString).
            ToArray

        cboStartLetter.Items.AddRange(letterArray)
        cboStartLetter.SelectedIndex = 0

        cboStartRow.Items.AddRange(rowArray)
        cboStartRow.SelectedIndex = 0

        cboEndLetter.Items.AddRange(letterArray)
        cboEndLetter.SelectedIndex = 2


        cboEndRow.Items.AddRange(rowArray)
        cboEndRow.SelectedIndex = 10

        cboSheetNames.DataSource = ops.SheetNames(fileName)

        Dim index As Integer = cboSheetNames.FindString("NorthWindData")
        If index > -1 Then
            cboSheetNames.SelectedIndex = index
        End If


        If ops.HasException Then
            MessageBox.Show($"Failed to get sheet names: {ops.LastExceptionMessage}")
        End If

    End Sub
    ''' <summary>
    ''' The idea here is to allow the user to select a range of cells and populate
    ''' a DataTable with the selected range.
    ''' 
    ''' Note as coded I have the connection string setup for WorkSheets w/o headers
    ''' so if in working with other Excel files you have sheets where the first row
    ''' contains field names not data then the Builder object needs HDR set to Yes.
    ''' I have a more flexible method to build connection string with HDR Yes or No
    ''' but didn't want to get anymore complex as we have done so far with code
    ''' presented in this click event. There is a lot going on here!!!
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub cmdReadData_Click(sender As Object, e As EventArgs) Handles cmdReadData.Click
        '
        ' I wanted to show what happens when a sheet is selected where the first row has
        ' column names rather than data. Having column names in headers is ideal yet we are
        ' here in this code sample to examine working with work sheets where the first row
        ' is data.
        '
        If cboSheetNames.Text = "CustomersWithHeadersAndData" Then
            MessageBox.Show("Note this sheet will not work well as we go get data but w/o any errors")
        End If
        '
        ' Using ComboBox selections for creating a range we use two read-only fields
        ' to assist with generating columns e.g. A1 to B2 means we want F1,F2 so not to
        ' use SELECT * but instead SELECT F1, F2
        '
        Dim selectedRange As ExcelRangeItem = ops.CreateRangeString(
            cboStartLetter.Text,
            CInt(cboStartRow.Text),
            cboEndLetter.Text,
            CInt(cboEndRow.Text))

        '
        ' Next two lines builds the field list for the SELECT statement in tangent with the
        ' code in the assertion for result.Count >0
        '
        Dim sequenceList As New List(Of Integer) From
            {selectedRange.StartColumnIndex, selectedRange.EndColumnIndex}

        '
        ' Find the missing numbers between start column and end column
        '
        Dim result = Enumerable.
            Range(selectedRange.StartColumnIndex, selectedRange.EndColumnIndex).
            Except(sequenceList).
            ToList.
            Select(Function(item) $"F{item}").ToArray

        Dim SelectStatement As String = ""

        '
        ' Build the SELECT statement
        '
        If result.Count > 0 Then
            SelectStatement = $"SELECT F{selectedRange.StartColumnIndex}," &
                String.Join(",", result) & $",F{selectedRange.EndColumnIndex} " &
                $"FROM [{cboSheetNames.Text}${selectedRange.ToString}]"
        Else
            '
            ' Caveat: If you were to pick a single cell e.g. A1:A1 then 
            ' we get Expr1000, in other words OleDb makes up another field name
            ' as already have A1:A1 we can't have SELECT F1,F1 as they is not permited
            ' to have duplicate field names.
            '
            SelectStatement = $"SELECT F{selectedRange.StartColumnIndex},F{selectedRange.EndColumnIndex} " &
                $"FROM [{cboSheetNames.Text}${selectedRange.ToString}]"

        End If

        '
        ' Show the SELECT in the event you have problems or are just curious.
        '
        txtSelectStatement.Text = SelectStatement

        '
        ' Attempt to read data from selected work sheet.
        '
        Dim dt As DataTable = ops.ReadData(fileName, SelectStatement)
        DataGridView1.DataSource = dt
        DataGridView1.ExpandColumns
        '
        ' There are many reasons for failure here that would not happen with Open XML, wrapper 
        ' libraries using Open XML plus Excel automation so using OleDb sometimes need more
        ' babying or other methods to get at the data in WorkSheets.
        ' 
        ' In the Excel file as delivered there will be errors and that was done on purpose!
        If ops.HasException Then
            MessageBox.Show($"{ops.LastExceptionMessage}")
        End If

    End Sub

    Private Sub cmdShowSelectedRange_Click(sender As Object, e As EventArgs) Handles cmdShowSelectedRange.Click
        If DataGridView1.DataSource Is Nothing Then
            MessageBox.Show("Please select data via the fetch button first")
            Exit Sub
        End If
        '
        ' Get selected cells via row and column index, order first by row index then by column index
        '
        Dim cellDataList = DataGridView1.
            SelectedCells.
            Cast(Of DataGridViewCell).
            Select(Function(item) New ExcelRangeCellData With {.Row = item.RowIndex, .Col = item.ColumnIndex}).
            OrderBy(Function(item) item.Row).ThenBy(Function(item) item.Col).ToList


        '
        ' Get distinct rows
        '
        Dim distinctRows = cellDataList.Select(Function(item) item.Row).Distinct

        '
        ' Get distinct columns
        '
        Dim distinctCols = cellDataList.Select(Function(item) item.Col).Distinct

        '
        ' Here we create our SELECT statement with the selected range using
        ' using FirstOrDefault and LastOrDefault from distinctRows and
        ' distinctCols
        '
        Dim selectStatement As String = "SELECT * FROM " &
            $"[{cboSheetNames.Text}${(distinctCols.FirstOrDefault + 1).ExcelColumnNameFromNumber}{distinctRows.FirstOrDefault + 1}:" &
            $"{(distinctCols.LastOrDefault + 1).ExcelColumnNameFromNumber}{distinctRows.LastOrDefault + 1}]"


        '
        ' Fetch the selected range 
        '
        Dim dt As DataTable = ops.ReadData(fileName, selectStatement)
        If chkRemoveFirstRow.Checked AndAlso dt.Rows.Count > 1 Then
            dt.Rows.RemoveAt(0)
        End If

        Dim f As New SelectedRangeForm

        Try
            f.DataGridView1.DataSource = dt


            For Each col As DataColumn In dt.Columns
                f.DataGridView2.Rows.Add(New Object() {col.ColumnName, col.DataType.ToString.Replace("System.", "")})
            Next

            '
            ' Rather than show Fx for each column header let's show the Excel column letter instead
            '
            For indexer As Integer = 0 To distinctCols.Count - 1
                f.DataGridView1.Columns(indexer).HeaderText = (distinctCols(indexer) + 1).ExcelColumnNameFromNumber
            Next

            f.ShowDialog()

        Finally
            f.Dispose()
        End Try

    End Sub
End Class
