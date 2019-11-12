Public Class frmCountriesDemo
    Dim dtCountries As DataTable = LoadAllCountries()
    Private Sub frmCountriesDemo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim TheNameList As New AutoCompleteStringCollection
        Label1.Text = ""

        For Each row As DataRow In dtCountries.Rows
            TheNameList.Add(row.Field(Of String)("Country"))
        Next

        TextBoxSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        TextBoxSearch.AutoCompleteSource = AutoCompleteSource.CustomSource
        TextBoxSearch.AutoCompleteCustomSource = TheNameList

    End Sub
    Private Sub TextBoxSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrWhiteSpace(TextBoxSearch.Text) Then
                Label1.Text = dtCountries.Select("Country='" & TextBoxSearch.Text & "'")(0).Field(Of String)("Capital")
            Else
                Label1.Text = ""
            End If
        End If
    End Sub
End Class