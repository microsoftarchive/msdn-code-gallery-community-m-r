Public Class ExcelRangeCellData
    Public Property Row As Integer
    Public Property Col As Integer
    Public Overrides Function ToString() As String
        Return $"{Row},{Col}"
    End Function
End Class