Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports ContosoUniversity.Models

Namespace Migrations

    Friend NotInheritable Class Configuration 
        Inherits DbMigrationsConfiguration(Of ContosoUniversity.Models.SchoolContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
        End Sub

        Protected Overrides Sub Seed(context As ContosoUniversity.Models.SchoolContext)

            context.Students.AddOrUpdate(
                 New Student() With
                 {
                     .FirstName = "Carson",
                     .LastName = "Alexander",
                     .Year = AcademicYear.Freshman,
                     .EnrollmentDate = DateTime.Now
                 },
                 New Student() With
                 {
                     .FirstName = "Meredith",
                     .LastName = "Alonso",
                     .Year = AcademicYear.Freshman,
                     .EnrollmentDate = DateTime.Now
                 },
                 New Student() With
                 {
                     .FirstName = "Arturo",
                     .LastName = "Anand",
                     .Year = AcademicYear.Sophomore,
                     .EnrollmentDate = DateTime.Now
                 },
                 New Student() With
                 {
                     .FirstName = "Gytis",
                     .LastName = "Barzdukas",
                     .Year = AcademicYear.Sophomore,
                     .EnrollmentDate = DateTime.Now
                 },
                 New Student() With
                 {
                     .FirstName = "Yan",
                     .LastName = "Li",
                     .Year = AcademicYear.Junior,
                     .EnrollmentDate = DateTime.Now
                 },
                 New Student() With
                 {
                     .FirstName = "Peggy",
                     .LastName = "Justice",
                     .Year = AcademicYear.Junior,
                     .EnrollmentDate = DateTime.Now
                 },
                 New Student() With
                 {
                     .FirstName = "Laura",
                     .LastName = "Norman",
                     .Year = AcademicYear.Senior,
                     .EnrollmentDate = DateTime.Now
                 },
                 New Student() With
                 {
                     .FirstName = "Nino",
                     .LastName = "Olivetto",
                     .Year = AcademicYear.Senior,
                     .EnrollmentDate = DateTime.Now
                 }
                 )

            context.SaveChanges()

            context.Courses.AddOrUpdate(
                New Course() With {.Title = "Chemistry", .Credits = 3},
                New Course() With {.Title = "Microeconomics", .Credits = 3},
                New Course() With {.Title = "Macroeconomics", .Credits = 3},
                New Course() With {.Title = "Calculus", .Credits = 4},
                New Course() With {.Title = "Trigonometry", .Credits = 4},
                New Course() With {.Title = "Composition", .Credits = 3},
                New Course() With {.Title = "Literature", .Credits = 4}
                )

            context.SaveChanges()

            context.Enrollments.AddOrUpdate(
                New Enrollment() With {.StudentID = 1, .CourseID = 1, .Grade = 1},
                New Enrollment() With {.StudentID = 1, .CourseID = 2, .Grade = 3},
                New Enrollment() With {.StudentID = 1, .CourseID = 3, .Grade = 1},
                New Enrollment() With {.StudentID = 2, .CourseID = 4, .Grade = 2},
                New Enrollment() With {.StudentID = 2, .CourseID = 5, .Grade = 4},
                New Enrollment() With {.StudentID = 2, .CourseID = 6, .Grade = 4},
                New Enrollment() With {.StudentID = 3, .CourseID = 1},
                New Enrollment() With {.StudentID = 4, .CourseID = 1},
                New Enrollment() With {.StudentID = 4, .CourseID = 2, .Grade = 4},
                New Enrollment() With {.StudentID = 5, .CourseID = 3, .Grade = 3},
                New Enrollment() With {.StudentID = 6, .CourseID = 4},
                New Enrollment() With {.StudentID = 7, .CourseID = 5, .Grade = 2}
                )

            context.SaveChanges()
        End Sub

    End Class

End Namespace
