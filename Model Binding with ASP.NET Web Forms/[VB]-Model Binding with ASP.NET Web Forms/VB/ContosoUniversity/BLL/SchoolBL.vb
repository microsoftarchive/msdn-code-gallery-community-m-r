Imports ContosoUniversity.Models
Imports System.Web.ModelBinding
Imports System.Web.UI
Imports System.Web
Imports System.Web.UI.WebControls
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Namespace BLL
    Public Class SchoolBL
        Implements IDisposable

        Private db As SchoolContext = New SchoolContext()

        Public Function GetStudents(<Control> ByVal displayYear As Nullable(Of AcademicYear)) As IQueryable(Of Student)
            Dim query = db.Students.Include(Function(s) s.Enrollments.Select(Function(e) e.Course))
            If Not displayYear Is Nothing Then
                query = query.Where(Function(s) s.Year = displayYear)
            End If
            Return query
        End Function


        Public Sub InsertStudent(context As ModelMethodContext)
            Dim item = New Student()
            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.Students.Add(item)
                db.SaveChanges()
            End If
        End Sub

        Public Sub DeleteStudent(ByVal studentID As Integer, context As ModelMethodContext)
            Dim item As Student = New Student() With {.StudentID = studentID}
            db.Entry(item).State = System.Data.EntityState.Deleted
            Try
                db.SaveChanges()
            Catch ex As DbUpdateConcurrencyException
                context.ModelState.AddModelError("", String.Format("Item with id {0} no longer exists in the database.", studentID))
            End Try
        End Sub


        Public Sub UpdateStudent(ByVal studentID As Integer, context As ModelMethodContext)
            Dim item As Student = Nothing
            item = db.Students.Find(studentID)
            If item Is Nothing Then
                context.ModelState.AddModelError("", String.Format("Item with id {0} was not found", studentID))
                Return
            End If

            context.TryUpdateModel(item)
            If context.ModelState.IsValid Then
                db.SaveChanges()
            End If
        End Sub

        Public Function GetCourses(<QueryString> studentID As Nullable(Of Integer)) As IQueryable(Of Enrollment)
            Dim query = db.Enrollments.Include(Function(e) e.Course) _
                    .Where(Function(e) e.StudentID = studentID)
            Return query
        End Function

        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    db.Dispose()
                End If
            End If
            Me.disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

    End Class
End Namespace

