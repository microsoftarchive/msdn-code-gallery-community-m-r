Public Class frmStatesDemo
    Private dictStateData As New Dictionary(Of String, String)

    Private Sub StatesDemoFormForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim Parts As String() = {}
        Dim Line As String = ""

        Using Reader As New System.IO.StreamReader(IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "States.txt"))
            Line = Reader.ReadLine
            Do While (Not Line Is Nothing)
                Parts = Line.Split(","c)
                dictStateData.Add(Parts(1), Parts(0))
                Line = Reader.ReadLine
            Loop
        End Using

        Dim AbbreviationList As New AutoCompleteStringCollection
        AbbreviationList.AddRange((From T In dictStateData Select T.Key).ToArray)
        txtSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtSearch.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtSearch.AutoCompleteCustomSource = AbbreviationList

    End Sub
    Private Sub TextBoxSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrWhiteSpace(txtSearch.Text) Then
                If dictStateData.ContainsKey(txtSearch.Text) Then
                    txtState.Text = dictStateData(txtSearch.Text)
                Else
                    txtState.Text = "No state for your entry."
                End If
            Else
                txtState.Text = "Need valid input."
            End If
        End If
    End Sub
End Class