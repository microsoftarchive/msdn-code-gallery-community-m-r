Imports System.Data.OleDb

Public Class Form1

    Dim con As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Relational database.accdb;Persist Security Info=False;")
    Dim dt As New DataTable

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim da As New OleDbDataAdapter("SELECT * FROM Student", con)
        da.Fill(dt)

        DomainUpDown1.Items.AddRange(dt.Rows.Cast(Of DataRow).Select(Function(dr) dr.Item("Student_index").ToString).Reverse.ToArray)
        DomainUpDown1.SelectedIndex = DomainUpDown1.Items.Count - 1
    End Sub

    Private Sub DomainUpDown1_SelectedItemChanged(sender As Object, e As EventArgs) Handles DomainUpDown1.SelectedItemChanged
        If DomainUpDown1.SelectedIndex = -1 Then Return
        Dim row As DataRow = dt.Rows.Cast(Of DataRow).First(Function(dr) dr.Item("Student_index").ToString = DomainUpDown1.SelectedItem.ToString)
        Label1.Text = row.Item("Given_name").ToString
        Label2.Text = row.Item("Family_name").ToString
        Label3.Text = row.Item("Address").ToString
        Label4.Text = row.Item("Postcode").ToString

        Dim courses As New OleDbDataAdapter("SELECT Course_code FROM Joining_Table WHERE Student_index = " & CInt(DomainUpDown1.SelectedItem.ToString), con)
        Dim dt2 As New DataTable
        courses.Fill(dt2)

        Dim sqlString As String = String.Join("' OR Course_code = '", dt2.Rows.Cast(Of DataRow).Select(Function(dr) dr.Item("Course_code").ToString).ToArray)

        Dim da As New OleDbDataAdapter("SELECT * FROM Course WHERE Course_code = '" & sqlString & "'", con)
        Dim dt3 As New DataTable
        da.Fill(dt3)

        DataGridView1.DataSource = dt3
        DataGridView1.Columns(3).DefaultCellStyle.Format = "c"

        Label5.Text = String.Format("Total Fees: {0:c}", CInt(dt3.Compute("SUM(Fee)", Nothing).ToString))
        Label5.Left = DataGridView1.Right - Label5.Width
    End Sub

End Class
