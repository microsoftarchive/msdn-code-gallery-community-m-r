Module StringExtensions
    <Runtime.CompilerServices.Extension()> _
    Public Function ProperCase(ByVal sender As String) As String
        Dim TI As System.Globalization.TextInfo = New System.Globalization.CultureInfo("en-US", False).TextInfo
        Return TI.ToTitleCase(sender.ToLower)
    End Function
End Module
