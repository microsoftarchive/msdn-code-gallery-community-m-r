Imports System
Imports System.Data.Entity.Migrations

Namespace Migrations
    Public Partial Class initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Students",
                Function(c) New With
                    {
                        .StudentID = c.Int(nullable := False, identity := True),
                        .LastName = c.String(nullable := False, maxLength := 40),
                        .FirstName = c.String(nullable := False, maxLength := 20),
                        .Year = c.Int(nullable := False),
                        .EnrollmentDate = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.StudentID)
            
            CreateTable(
                "dbo.Enrollments",
                Function(c) New With
                    {
                        .EnrollmentID = c.Int(nullable := False, identity := True),
                        .CourseID = c.Int(nullable := False),
                        .StudentID = c.Int(nullable := False),
                        .Grade = c.Decimal(precision := 18, scale := 2)
                    }) _
                .PrimaryKey(Function(t) t.EnrollmentID) _
                .ForeignKey("dbo.Courses", Function(t) t.CourseID, cascadeDelete := True) _
                .ForeignKey("dbo.Students", Function(t) t.StudentID, cascadeDelete := True) _
                .Index(Function(t) t.CourseID) _
                .Index(Function(t) t.StudentID)
            
            CreateTable(
                "dbo.Courses",
                Function(c) New With
                    {
                        .CourseID = c.Int(nullable := False, identity := True),
                        .Title = c.String(),
                        .Credits = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.CourseID)
            
        End Sub
        
        Public Overrides Sub Down()
            DropIndex("dbo.Enrollments", New String() { "StudentID" })
            DropIndex("dbo.Enrollments", New String() { "CourseID" })
            DropForeignKey("dbo.Enrollments", "StudentID", "dbo.Students")
            DropForeignKey("dbo.Enrollments", "CourseID", "dbo.Courses")
            DropTable("dbo.Courses")
            DropTable("dbo.Enrollments")
            DropTable("dbo.Students")
        End Sub
    End Class
End Namespace
