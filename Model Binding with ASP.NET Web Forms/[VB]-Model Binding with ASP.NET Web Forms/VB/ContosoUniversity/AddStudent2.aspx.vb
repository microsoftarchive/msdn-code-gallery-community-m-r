Public Class AddStudent2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub addStudentForm_CallingDataMethods(ByVal sender As Object, ByVal e As CallingDataMethodsEventArgs)
        e.DataMethodsObject = New ContosoUniversity.BLL.SchoolBL()
    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/Students2.aspx")
    End Sub

    Protected Sub addStudentForm_ItemInserted(ByVal sender As Object, ByVal e As FormViewInsertedEventArgs)
        Response.Redirect("~/Students2.aspx")
    End Sub

End Class