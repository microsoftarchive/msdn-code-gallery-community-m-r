Module Helper1
    ''' <summary>
    ''' Used to create a list for project ParameterView in InternalToProject.vb
    ''' </summary>
    ''' <remarks>
    ''' List taken from http://msdn.microsoft.com/en-us/library/ms189822.aspx
    ''' </remarks>
    Public Sub CreateSlqReservedList()
        Dim TokenList As New List(Of String)
        Dim FileName As String = IO.Path.Combine(Application.StartupPath, "SqlReserved.txt")
        If IO.File.Exists(FileName) Then
            TokenList = (From T In IO.File.ReadAllLines(FileName) Where T.Length > 0 Select T Order By T).ToList
            For x As Integer = 0 To TokenList.Count - 1
                TokenList(x) = TokenList(x).EncloseWithQuotes
            Next
            Console.WriteLine(String.Join("," & Environment.NewLine, TokenList.ToArray))
        Else
            MessageBox.Show(FileName & " not found (you must set Copy If newer for this to work.")
        End If
    End Sub
    <System.Runtime.CompilerServices.Extension()> _
    Public Function EncloseWithQuotes(ByVal sender As String) As String
        Dim Encloser As Char = Chr(34)
        Return String.Concat(Encloser, sender, Encloser)
    End Function
End Module
