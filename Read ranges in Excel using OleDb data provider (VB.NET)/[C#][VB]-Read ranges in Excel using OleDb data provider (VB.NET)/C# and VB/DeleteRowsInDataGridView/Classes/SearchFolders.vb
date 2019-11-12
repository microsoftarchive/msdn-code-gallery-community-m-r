Imports System.Security.AccessControl

Public Class SearchFolders
    ''' <summary>
    ''' Populate with one or more file extensions to locate
    ''' </summary>
    ''' <returns></returns>
    Public Property Extensions As String()
    Public Property SkippedFolders As List(Of String)
    ''' <summary>
    ''' Contains a list of files using Extensions array
    ''' to locate items
    ''' </summary>
    ''' <returns></returns>
    Public Property FoundFileList As List(Of FoundFile)

    Public Sub New()
        SkippedFolders = New List(Of String)
        FoundFileList = New List(Of FoundFile)
    End Sub
    Public Sub SearchFolderRecursive(ByVal pStartDir As String)
        Dim DirList As IEnumerable(Of String) = Nothing
        Dim foundFiles() As String

        If CanReadFolder(pStartDir) Then
            DirList = IO.Directory.EnumerateDirectories(pStartDir)
            foundFiles = IO.Directory.EnumerateFiles(pStartDir) _
                .Where(Function(CurrentFile As String)
                           If Extensions.Contains(IO.Path.GetExtension(CurrentFile).ToLower) Then
                               Return True
                           Else
                               Return False
                           End If
                       End Function).ToArray
            If foundFiles.Length > 0 Then
                For Each Item As String In foundFiles
                    FoundFileList.Add(New FoundFile With
                        {
                            .FolderName = pStartDir,
                            .FileName = IO.Path.GetFileName(Item)
                        })
                Next
            End If

        Else
            SkippedFolders.Add(pStartDir)
        End If

        If DirList IsNot Nothing Then
            For Each Dir As String In DirList
                SearchFolderRecursive(Dir)
            Next
        End If
    End Sub
    Public Function CanReadFolder(ByVal pFolderName As String) As Boolean
        Dim Result As Boolean = False

        Dim fi As New IO.FileInfo(pFolderName)
        Dim fs As New Security.AccessControl.FileSecurity
        Dim obTypeToGet As Type

        fs = fi.GetAccessControl()
        obTypeToGet = Type.GetType("System.Security.Principal.NTAccount")

        Dim PaddingValue As Integer = 10

        For Each ace As FileSystemAccessRule In fs.GetAccessRules(True, True, obTypeToGet)
            Dim ACL_Type As String = ""
            If ace.AccessControlType.Equals(Security.AccessControl.AccessControlType.Deny) Then
                Return False
            Else
                Return True
            End If
        Next
        Return Result
    End Function
End Class
Public Class FoundFile
    Public Property FolderName As String
    Public Property FileName As String
End Class
