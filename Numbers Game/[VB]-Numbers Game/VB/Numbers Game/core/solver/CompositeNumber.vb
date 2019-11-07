''' <summary>
''' Contains the numbers and operators used to make a Composite Number
''' Builds the bracketed tostring used in displaying the solution
''' </summary>
Public Class CompositeNumber

    Private equationString As String
    Private arrayNumbers() As Integer
    Private arrayOperators() As String
    Public Sub New(Numbers() As Integer, Operators() As String, equationStrings() As String, operatorIndex As Integer)
        Me.arrayNumbers = Numbers
        Me.arrayOperators = Operators
        If equationStrings.Length = 0 Then
            Me.equationString = Numbers(0).ToString
        ElseIf equationStrings.Length = 2 Then
            Me.equationString = "(" & equationStrings(0) & " " & Operators(operatorIndex) & " " & equationStrings(1) & ")"
        End If
    End Sub

    Public Function getNumbers() As Integer()
        Return Me.arrayNumbers
    End Function

    Public Function getOperators() As String()
        Return Me.arrayOperators
    End Function

    Public Overrides Function tostring() As String
        Return Me.equationString
    End Function

End Class
