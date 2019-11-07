Public Class Students2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub studentsGrid_CallingDataMethods(ByVal sender As Object, ByVal e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New ContosoUniversity.BLL.SchoolBL()
    End Sub

End Class