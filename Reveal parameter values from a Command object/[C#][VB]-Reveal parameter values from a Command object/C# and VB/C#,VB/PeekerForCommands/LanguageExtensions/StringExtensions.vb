Public Module StringExtensions
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Function RemoveWhitespace(ByVal sender As String) As String
        Dim reg As New System.Text.RegularExpressions.Regex("\s*")
        Return reg.Replace(sender, "")
    End Function
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Function LineCount(ByVal value As String) As Integer
        Return System.Text.RegularExpressions.Regex.Matches(value, ".+\n*").Count
    End Function
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Function TabStop(ByVal value As String) As String
        Return value & System.Text.RegularExpressions.Regex.Unescape("\t")
    End Function
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Function TabsToSpaces(ByVal value As String, ByVal tabSize As Integer) As String
        Dim Result As New System.Text.StringBuilder

        For counter As Integer = 0 To value.Length - 1
            If (value.Chars(counter) = vbTab) Then
                Do
                    Result.Append(Space(1))
                Loop Until ((Result.Length Mod tabSize) = 0)
            Else
                Result.Append(value.Chars(counter))
            End If
        Next counter
        Return Result.ToString()
    End Function

End Module
