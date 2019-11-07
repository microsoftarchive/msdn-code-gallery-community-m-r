''' <summary>
''' Simple demo for auto complete adding items that are not in the list when pressing ENTER in TextBox1.
''' </summary>
''' <remarks>
''' http://code.msdn.microsoft.com/Populate-save-remove-items-be87b5c7
''' </remarks>
Public Class MainForm
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        UpdateFemaleNames(txtFirstName.AutoCompleteCustomSource)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFirstName.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtFirstName.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtFirstName.AutoCompleteCustomSource = LoadFemaleNames()

        ' The next two lines are for demo purposes only to see what is in the list for the TextBox with First names
        DataGridView1.DataSource = AllFemaleNames()
        DataGridView1.Columns("FirstName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        ComboBox1.DisplayMember = "FirstName"
        ComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource
        ComboBox1.AutoCompleteCustomSource = LoadFemaleNames()

    End Sub
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFirstName.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Not String.IsNullOrWhiteSpace(txtFirstName.Text) Then
                If Not txtFirstName.AutoCompleteCustomSource.Contains(txtFirstName.Text.ToLower) Then
                    txtFirstName.AutoCompleteCustomSource.Add(txtFirstName.Text.ProperCase)
                End If

                txtFirstName.Text = txtFirstName.Text.ProperCase
                Console.WriteLine("--> '{0}'", txtFirstName.Text)
                e.SuppressKeyPress = True

            End If
        End If
    End Sub
    Private Sub ComboBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then

            If Not String.IsNullOrWhiteSpace(ComboBox1.Text) Then
                ' --> Use ComboBox1.Text for filtering
                MessageBox.Show("Use this for filtering -> '" & ComboBox1.Text & "'")
                If Not ComboBox1.AutoCompleteCustomSource.Contains(ComboBox1.Text.ToLower) Then
                    ComboBox1.AutoCompleteCustomSource.Add(ComboBox1.Text.ProperCase)

                    ' Next line now allows user to select text enter before
                    ComboBox1.Items.Add(ComboBox1.Text)

                End If
                e.SuppressKeyPress = True
            End If
        End If
    End Sub
    Private Sub cmdRemoveName_Click(sender As Object, e As EventArgs) Handles cmdRemoveName.Click
        If Not String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            Dim CurrentName As String = txtFirstName.Text.Trim.ProperCase
            If My.Dialogs.Question(String.Format("Remove '{0}'", CurrentName)) Then
                RemmoveFemale(CurrentName)
                txtFirstName.AutoCompleteCustomSource.Remove(txtFirstName.Text)
                txtFirstName.Text = ""

            End If
        End If
    End Sub

    Private Sub cmdCountries_Click(sender As Object, e As EventArgs) Handles cmdCountries.Click
        Dim f As New frmCountriesDemo
        Try
            f.ShowDialog()
        Finally
            f.Dispose()
        End Try
    End Sub

    Private Sub cmdStates_Click(sender As Object, e As EventArgs) Handles cmdStates.Click
        Dim f As New frmStatesDemo
        Try
            f.ShowDialog()
        Finally
            f.Dispose()
        End Try
    End Sub
End Class

