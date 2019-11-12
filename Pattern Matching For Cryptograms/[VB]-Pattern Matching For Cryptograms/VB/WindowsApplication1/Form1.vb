Option Strict On
Imports CH = System.Windows.Forms.ColumnHeader
Imports CHrsz = System.Windows.Forms.ColumnHeaderAutoResizeStyle
Public Class Form1
    Dim DeepDictionary As New List(Of String)
    Dim ShallowDictionary As New List(Of String)
    Const col1 As String = "Original"
    Const col2 As String = "Match"
    Const col3 As String = "Pattern"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        With ListView1
            .View = View.Details
            .Columns.AddRange({New CH With {.Text = col1}, New CH With {.Text = col2}, New CH With {.Text = col3}})
            .AutoResizeColumns(CHrsz.HeaderSize)
            .FullRowSelect = True
        End With
        DeepDictionary = My.Resources.wordsEn.Replace(" ", "").Replace(vbLf, vbCr).Replace(vbCr, " ").Replace("  ", " ").Split(" "c).ToList
        ShallowDictionary = My.Resources.commonenglist.Replace(" ", "").Replace(vbLf, vbCr).Replace(vbCr, " ").Replace("  ", " ").Split(" "c).ToList
        DoLayout()
    End Sub
    Private Sub btnGetMatches_Click(sender As Object, e As EventArgs) Handles btnGetMatches.Click
        ListView1.Items.Clear()
        Dim Result As ListViewItem() = {}
        Dim Sw As Stopwatch = Stopwatch.StartNew
        Dim Dictionary As List(Of String)
        Select Case True
            Case RadioButton1.Checked
                Dictionary = ShallowDictionary
            Case Else
                Dictionary = DeepDictionary
        End Select
        Select Case CheckBox1.Checked
            Case True : Result = GetWordPatternMatches(LCase(TextBox1.Text), Dictionary, LCase(TextBox2.Text), Nothing, Label4)
            Case Else : Result = GetWordPatternMatches(LCase(TextBox1.Text), Dictionary, LCase(TextBox2.Text), ProgressBar1, Label4)
        End Select
        Sw.Stop()
        ListView1.Items.AddRange(Result)
        ListView1.AutoResizeColumns(CHrsz.ColumnContent)
        Label4.Text = ListView1.Items.Count & " matches found in " & Math.Round(Sw.Elapsed.TotalSeconds, 2).ToString & " seconds."
    End Sub
    Sub DoLayout()
        Me.SuspendLayout()
        ProgressBar1.Top = Me.ClientRectangle.Height - ProgressBar1.Height
        Dim Padding As Integer = 15
        TextBox1.Width = (Me.ClientRectangle.Width \ 2) - (Padding \ 2) - (Padding * 2)
        ListView1.Width = (Me.ClientRectangle.Width \ 2) - (Padding \ 2) - (Padding * 2)
        Label1.Top = Padding
        Label1.Left = Padding
        Label3.Left = Padding
        TextBox1.Top = Padding + Label1.Height
        Label3.Top = TextBox1.Top + TextBox1.Height
        ListView1.Left = TextBox1.Left + TextBox1.Width + (Padding * 3)
        ListView1.Top = TextBox1.Top
        Label2.Top = Label1.Top
        Label2.Left = ListView1.Left
        TextBox2.Top = Label3.Top + Label3.Height
        TextBox2.Width = TextBox1.Width
        btnGetMatches.Top = TextBox2.Top + Padding + TextBox2.Height
        btnGetMatches.Left = TextBox1.Left
        btnGetMatches.Width = TextBox1.Width
        GroupBox1.Left = btnGetMatches.Left
        GroupBox1.Top = btnGetMatches.Top + Padding + btnGetMatches.Height
        GroupBox1.Width = btnGetMatches.Width
        CheckBox1.Left = Label1.Left
        CheckBox1.Top = GroupBox1.Top + GroupBox1.Height + Padding
        Label4.Top = ProgressBar1.Top - Label4.Height
        Label4.Left = Label1.Left
        ListView1.Height = Label4.Top - Padding - Label2.Height
        Me.ResumeLayout()
    End Sub

    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        DoLayout()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Select Case CheckBox1.Checked
            Case True
                ProgressBar1.Visible = False
            Case Else
                ProgressBar1.Visible = True
        End Select
    End Sub
End Class
