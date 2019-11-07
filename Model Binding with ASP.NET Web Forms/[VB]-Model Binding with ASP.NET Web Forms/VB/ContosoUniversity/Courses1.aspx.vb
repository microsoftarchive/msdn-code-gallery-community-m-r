Imports ContosoUniversity.Models
Imports System.Web.ModelBinding
Imports System.Data.Entity

Public Class Courses1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function coursesGrid_GetData(<QueryString> studentID As Nullable(Of Integer)) As IQueryable(Of Enrollment)
        Dim db As SchoolContext = New SchoolContext()
        Dim query = db.Enrollments.Include(Function(e) e.Course) _
                .Where(Function(e) e.StudentID = studentID)
        Return query
    End Function

End Class