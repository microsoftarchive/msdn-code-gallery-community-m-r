Imports ExcelExtensionsCS
''' <summary>
''' Responsible as a container for selected Excel range. Note the
''' read-only properties assist with creating the field list e.g.
''' SELECT F1,F2, F2 
''' 
''' Note the use of ExcelColumnNameToNumber which is a C# language
''' extension method (what, C#). I used C# because VB.NET developers
''' need to get use to mixing it up with C# rather than taking perfectly
''' good code and converting it which in this case serves no purpose
''' other than "hey I have some cool code".
''' 
''' Properties are self-explanatory.
''' </summary>
Public Class ExcelRangeItem
    ''' <summary>
    ''' Start of range column letter
    ''' </summary>
    ''' <returns></returns>
    Public Property StartColumn As String
    Public ReadOnly Property StartColumnIndex As Integer
        Get
            Return StartColumn.ExcelColumnNameToNumber
        End Get
    End Property
    ''' <summary>
    ''' Start row combined with StartColumn e.g. A1 where 1 is the start row
    ''' </summary>
    ''' <returns></returns>
    Public Property StartRow As Integer
    Public Property EndColumn As String
    Public ReadOnly Property EndColumnIndex As Integer
        Get
            Return EndColumn.ExcelColumnNameToNumber
        End Get
    End Property
    Public Property EndRow As Integer
    ''' <summary>
    ''' Show our range so when and if you debug and hit a break-point
    ''' you can see the range selected.
    ''' </summary>
    ''' <returns></returns>
    Public Overrides Function ToString() As String
        Return $"{StartColumn}{StartRow}:{EndColumn}{EndRow}"
    End Function

End Class
