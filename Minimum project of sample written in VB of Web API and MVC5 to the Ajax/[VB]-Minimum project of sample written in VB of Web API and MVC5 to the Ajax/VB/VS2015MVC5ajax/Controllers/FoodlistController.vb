Imports System.Web.Mvc

Namespace Controllers
    Public Class FoodlistController
        Inherits Controller

        ' GET: Foodlist
        Function Index() As ActionResult
            Return View()
        End Function

        Function Food(id As Integer) As ActionResult
            Return View(id)
        End Function

    End Class
End Namespace