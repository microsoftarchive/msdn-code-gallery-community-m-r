Imports ContosoUniversity.Models

Public Class AddStudent1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub addStudentForm_InsertItem()
        Dim item = New Student()
        TryUpdateModel(item)
        If ModelState.IsValid Then
            Using db As SchoolContext = New SchoolContext()
                db.Students.Add(item)
                db.SaveChanges()
            End Using
        End If
    End Sub

    Protected Sub cancelButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/Students1.aspx")
    End Sub

    Protected Sub addStudentForm_ItemInserted(ByVal sender As Object, ByVal e As FormViewInsertedEventArgs)
        Response.Redirect("~/Students1.aspx")
    End Sub

End Class