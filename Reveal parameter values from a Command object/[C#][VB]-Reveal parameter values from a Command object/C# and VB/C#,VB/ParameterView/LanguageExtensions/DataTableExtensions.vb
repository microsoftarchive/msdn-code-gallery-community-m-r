Public Module DataTableExtensions
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub DiagnoseBadSql(ByVal sender As DataTable, ByVal cmd As IDbCommand, ByVal FileName As String, ByVal DisplayFile As Boolean)
        If sender.Rows.Count = 0 Then
            cmd.ActualCommandTextToFile(FileName, True)
            If DisplayFile Then
                Process.Start(FileName)
            End If
        End If
    End Sub
End Module
