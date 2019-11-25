Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class FoodsController
        Inherits ApiController

        Private foods As String() = {"bread", "rice", "noodles", "sushi", "spaghetti", "pizza"}

        ' GET: api/Foods
        Public Function GetValues() As IEnumerable(Of String)
            Return foods
        End Function

        ' GET: api/Foods/5
        Public Function GetValue(ByVal id As Integer) As String
            If id > 0 And id <= foods.Length Then
                Return foods(id - 1)
            Else
                Return "I don't know."
            End If
        End Function

    End Class
End Namespace