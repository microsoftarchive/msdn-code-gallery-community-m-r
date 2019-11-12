Imports System.Threading

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        FileSystemWatcher1.Filter = "*.txt"

        If IO.Directory.Exists(TextBox1.Text) Then
            FileSystemWatcher1.Path = TextBox1.Text
            For Each file As String In IO.Directory.GetFiles(TextBox1.Text, "*.txt")
                ThreadPool.QueueUserWorkItem(AddressOf ProcessItem, file)
            Next
        End If

    End Sub

    Private Sub FileSystemWatcher1_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles FileSystemWatcher1.Created
        If New IO.DirectoryInfo(e.FullPath).Extension = ".txt" Then
            While Not IO.File.Exists(e.FullPath)
                Application.DoEvents()
            End While
            ThreadPool.QueueUserWorkItem(AddressOf ProcessItem, e.FullPath)
        End If
    End Sub

    Private Sub ProcessItem(ByVal item As Object)
        Dim psi As New ProcessStartInfo With { _
        .FileName = item.ToString, _
        .Verb = "Print", _
        .CreateNoWindow = True, _
        .WindowStyle = ProcessWindowStyle.Hidden, _
        .UseShellExecute = True}

        Dim p As New Process With {.StartInfo = psi}
        p.Start()
        p.WaitForInputIdle()

        IO.File.Delete(item.ToString)
        While IO.File.Exists(item.ToString)
            Application.DoEvents()
        End While
        Application.DoEvents()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fbd As New FolderBrowserDialog
        If fbd.ShowDialog = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = fbd.SelectedPath
            FileSystemWatcher1.Path = TextBox1.Text
            For Each file As String In IO.Directory.GetFiles(TextBox1.Text, "*.txt")
                ThreadPool.QueueUserWorkItem(AddressOf ProcessItem, file)
            Next
        End If
    End Sub

End Class
