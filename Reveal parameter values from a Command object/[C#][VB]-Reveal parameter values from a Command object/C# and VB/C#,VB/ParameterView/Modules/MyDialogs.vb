Module MyDialogs
    ''' <summary>
    ''' Display a dialog asking a question, returns true or false
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Question(ByVal Text As String) As Boolean
        Return (Windows.Forms.MessageBox.Show(Text, "Question", Windows.Forms.MessageBoxButtons.YesNo, Windows.Forms.MessageBoxIcon.Question, Windows.Forms.MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes)
    End Function
    ''' <summary>
    ''' Presents a dialog with information to the user
    ''' </summary>
    ''' <param name="Text"></param>
    ''' <remarks></remarks>
    Public Sub InformationDialog(ByVal Text As String)
        Windows.Forms.MessageBox.Show(Text, "FYI", Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
    End Sub
End Module
