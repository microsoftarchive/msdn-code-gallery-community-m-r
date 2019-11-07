Imports System.Windows.Forms
Public Class frmParamViewer
    Public Property CommandText As String = ""
    Public Property ShowMessage As Boolean = False
    Private Sub frmParamViewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RichTextBox1.Settings.Keywords.AddRange(SyntaxSqlKeywords)
        RichTextBox1.Settings.KeywordColor = Drawing.Color.Blue
        RichTextBox1.CompileKeywords()
        RichTextBox1.Text = Me.CommandText.Trim
        RichTextBox1.ProcessAllLines()
    End Sub
    Private Sub frmParamViewer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If ShowMessage Then
            InformationDialog("It will be easier to see the command text by maximizing this window.")
        End If
    End Sub
    Private Sub cmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy.Click
        If RichTextBox1.Text.Length > 0 Then
            If Question("Copy SQL to the Windows clipboard?") Then
                Windows.Forms.Clipboard.SetText(RichTextBox1.Text)
            End If
        Else
            InformationDialog("No SQL to copy.")
        End If
    End Sub
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        Close()
    End Sub
End Class