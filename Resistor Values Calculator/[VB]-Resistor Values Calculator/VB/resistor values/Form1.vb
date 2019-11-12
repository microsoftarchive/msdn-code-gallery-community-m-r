Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        dt.Columns.Add("color")
        dt.Columns.Add("value", GetType(Long))
        dt.Rows.Add(New Object() {"BLACK", 1})
        dt.Rows.Add(New Object() {"BROWN", 10})
        dt.Rows.Add(New Object() {"RED", 100})
        dt.Rows.Add(New Object() {"ORANGE", 1000})
        dt.Rows.Add(New Object() {"YELLOW", 10000})
        dt.Rows.Add(New Object() {"GREEN", 100000})
        dt.Rows.Add(New Object() {"BLUE", 1000000})
        dt.Rows.Add(New Object() {"PURPLE", 10000000})
        dt.Rows.Add(New Object() {"GRAY", 100000000})
        dt.Rows.Add(New Object() {"WHITE", 1000000000})

        Dim colors() As String = {"BLACK", "BROWN", "RED", "ORANGE", "YELLOW", "GREEN", "BLUE", "PURPLE", "GRAY", "WHITE"}

        ComboBox1.DataSource = colors.Clone
        ComboBox2.DataSource = colors.Clone

        ComboBox3.DisplayMember = "color"
        ComboBox3.ValueMember = "value"
        ComboBox3.DataSource = dt.Copy

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim totalVal As Long = (10 * ComboBox1.SelectedIndex + ComboBox2.SelectedIndex) * CLng(ComboBox3.SelectedValue)
        ToolTip1.SetToolTip(Label1, totalVal.ToString("n0"))
        Dim values As New List(Of Object())
        values.Add(New Object() {1000, "K"})
        values.Add(New Object() {1000000, "M"})
        values.Add(New Object() {1000000000, "G"})
        Dim output = (From v In values Where CLng(v(0)) <= totalVal).LastOrDefault
        Label1.Text = "Your resistor's value is " & If(output IsNot Nothing, String.Format("{0}{1}", totalVal / CLng(output(0)), output(1)), totalVal.ToString) & " OHMS"
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        PictureBox1.BackColor = Color.FromName(ComboBox1.Text)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        PictureBox2.BackColor = Color.FromName(ComboBox2.Text)
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        PictureBox3.BackColor = Color.FromName(ComboBox3.Text)
    End Sub

End Class
