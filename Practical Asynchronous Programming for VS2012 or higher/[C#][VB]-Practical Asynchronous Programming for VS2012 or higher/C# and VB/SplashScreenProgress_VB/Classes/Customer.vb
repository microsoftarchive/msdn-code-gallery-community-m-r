Public Class Customer
    Public Property Identifier As Integer
    Public Property Company As String
    Public Property ContactName As String

    Public Overrides Function ToString() As String
        Return Identifier.ToString & ", " & Company
    End Function
    ''' <summary>
    ''' Represents an object array of one customer
    ''' </summary>
    ''' <value></value>
    ''' <returns>Customer populated data</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ItemArray As Object()
        Get
            Return New Object() _
                {
                    Identifier,
                    Company,
                    ContactName
                }
        End Get
    End Property
End Class
