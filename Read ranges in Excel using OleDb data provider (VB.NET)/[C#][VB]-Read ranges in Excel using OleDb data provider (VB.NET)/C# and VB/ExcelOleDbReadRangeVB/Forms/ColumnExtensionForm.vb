Imports ExcelExtensionsCS

Public Class ColumnExtensionForm
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        ' Do a conversion from 1-200 which is A to GR
        '
        For indexer As Integer = 1 To 200
            ListBox1.Items.Add(indexer.ExcelColumnNameFromNumber)
        Next

        '
        ' Do reverse conversion, A-GR to 1-200
        '
        For indexer As Integer = 0 To ListBox1.Items.Count - 1
            ListBox2.Items.Add(ListBox1.Items(indexer).ToString.ExcelColumnNameToNumber)
        Next

        '
        ' Convert back to what we did in ListBox1
        '
        For indexer As Integer = 0 To ListBox2.Items.Count - 1
            ListBox3.Items.Add(CInt(ListBox2.Items(indexer)).ExcelColumnNameFromNumber)
        Next

    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Close()
    End Sub
End Class
