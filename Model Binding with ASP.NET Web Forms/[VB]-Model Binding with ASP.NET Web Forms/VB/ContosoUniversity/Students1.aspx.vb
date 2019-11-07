Imports ContosoUniversity.Models
Imports System.Web.ModelBinding
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity

Public Class Students1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function studentsGrid_GetData(<Control> ByVal displayYear As Nullable(Of AcademicYear)) As IQueryable(Of Student)
        Dim db As SchoolContext = New SchoolContext()
        Dim query = db.Students.Include(Function(s) s.Enrollments.Select(Function(e) e.Course))
        If Not displayYear Is Nothing Then
            query = query.Where(Function(s) s.Year = displayYear)
        End If
        Return query
    End Function

    Public Sub studentsGrid_UpdateItem(ByVal studentID As Integer)
        Using db As SchoolContext = New SchoolContext()
            Dim item As Student = Nothing
            item = db.Students.Find(studentID)
            If item Is Nothing Then
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", studentID))
                Return
            End If

            TryUpdateModel(item)
            If ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Using
    End Sub

    Public Sub studentsGrid_DeleteItem(ByVal studentID As Integer)
        Using db As SchoolContext = New SchoolContext()
            Dim item As Student = New Student() With {.StudentID = studentID}
            db.Entry(item).State = System.Data.EntityState.Deleted
            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                ModelState.AddModelError("", String.Format("Item with id {0} no longer exists in the database.", studentID))
            End Try
        End Using
    End Sub
End Class