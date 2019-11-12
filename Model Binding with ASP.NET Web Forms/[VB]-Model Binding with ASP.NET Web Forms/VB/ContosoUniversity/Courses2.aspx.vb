Public Class Courses2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub coursesGrid_CallingDataMethods(ByVal sender As Object, ByVal e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New ContosoUniversity.BLL.SchoolBL()
    End Sub

End Class