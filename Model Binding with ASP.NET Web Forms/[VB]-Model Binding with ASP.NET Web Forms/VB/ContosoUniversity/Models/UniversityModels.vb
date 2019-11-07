Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations

Namespace Models
    Public Class SchoolContext
        Inherits DbContext
        Public Property Students As DbSet(Of Student)
        Public Property Enrollments As DbSet(Of Enrollment)
        Public Property Courses As DbSet(Of Course)
    End Class

    Public Class Student
        <Key, Display(Name:="ID")>
        <ScaffoldColumn(False)>
        Public Property StudentID As Integer

        <Required, StringLength(40), Display(Name:="Last Name")>
        Public Property LastName As String

        <Required, StringLength(20), Display(Name:="First Name")>
        Public Property FirstName As String

        <EnumDataType(GetType(AcademicYear)), Display(Name:="Academic Year")>
        Public Property Year As AcademicYear

        <Range(GetType(DateTime), "1/1/2013", "1/1/3000", ErrorMessage:="Please provide an enrollment date after 1/1/2013")>
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:d}")>
        Public Property EnrollmentDate As DateTime

        Public Overridable Property Enrollments As ICollection(Of Enrollment)

    End Class

    Public Class Enrollment
        <Key>
        Public Property EnrollmentID As Integer
        Public Property CourseID As Integer
        Public Property StudentID As Integer
        Public Property Grade As Nullable(Of Decimal)
        Public Overridable Property Course As Course
        Public Overridable Property Student As Student
    End Class

    Public Class Course
        <Key>
        Public Property CourseID As Integer
        Public Property Title As String
        Public Property Credits As Integer
        Public Overridable Property Enrollments As ICollection(Of Enrollment)
    End Class

    Public Enum AcademicYear
        Freshman
        Sophomore
        Junior
        Senior
    End Enum
End Namespace
