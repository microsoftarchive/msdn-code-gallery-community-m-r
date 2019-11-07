Public Module CommandPeeker
    ''' <summary>
    ''' Used to display parameter values for named parameters
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns>
    ''' SQL statement with values from parameters
    ''' </returns>
    ''' <remarks>
    ''' Assumes each parameter has a parameter name beginning with @
    ''' and if not throws an exception. Also checks for empty parameter
    ''' names.
    ''' </remarks>
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Function ActualCommandTextByNames(ByVal sender As IDbCommand) As String
        Dim sb As New System.Text.StringBuilder(sender.CommandText)
        Dim EmptyParameterNames = (From T In sender.Parameters.Cast(Of IDataParameter)() Where String.IsNullOrWhiteSpace(T.ParameterName)).FirstOrDefault

        If EmptyParameterNames IsNot Nothing Then
            Return sender.CommandText
        End If

        For Each p As IDataParameter In sender.Parameters

            Select Case p.DbType
                Case DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.Date, DbType.DateTime, DbType.DateTime2, DbType.Guid, DbType.String, DbType.StringFixedLength, DbType.Time, DbType.Xml
                    If p.ParameterName(0) = "@" Then
                        If p.Value Is Nothing Then
                            Throw New Exception("no value given for parameter '" & p.ParameterName & "'")
                        End If
                        sb = sb.Replace(p.ParameterName, String.Format("'{0}'", p.Value.ToString.Replace("'", "''")))
                    Else
                        sb = sb.Replace(String.Concat("@", p.ParameterName), String.Format("'{0}'", p.Value.ToString.Replace("'", "''")))
                    End If
                Case Else
                    sb = sb.Replace(p.ParameterName, p.Value.ToString)
            End Select
        Next
        Return sb.ToString
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="Token">Alternate parameter start token</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Runtime.CompilerServices.Extension()> _
    Public Function ActualCommandTextByNames(ByVal sender As IDbCommand, ByVal Token As String) As String
        Dim sb As New System.Text.StringBuilder(sender.CommandText)
        Dim EmptyParameterNames = (From T In sender.Parameters.Cast(Of IDataParameter)() Where String.IsNullOrWhiteSpace(T.ParameterName)).FirstOrDefault

        If EmptyParameterNames IsNot Nothing Then
            Return sender.CommandText
        End If

        For Each p As IDataParameter In sender.Parameters

            Select Case p.DbType
                Case DbType.AnsiString, DbType.AnsiStringFixedLength, DbType.Date, DbType.DateTime, DbType.DateTime2, DbType.Guid, DbType.String, DbType.StringFixedLength, DbType.Time, DbType.Xml
                    If p.ParameterName(0) = Token Then
                        If p.Value Is Nothing Then
                            Throw New Exception("no value given for parameter '" & p.ParameterName & "'")
                        End If
                        sb = sb.Replace(p.ParameterName, String.Format("'{0}'", p.Value.ToString.Replace("'", "''")))
                    Else
                        sb = sb.Replace(String.Concat(Token, p.ParameterName), String.Format("'{0}'", p.Value.ToString.Replace("'", "''")))
                    End If
                Case Else
                    sb = sb.Replace(p.ParameterName, p.Value.ToString)
            End Select
        Next
        Return sb.ToString
    End Function
    ''' <summary>
    ''' Used to write SQL statement as written and also with passed values
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="FileName"></param>
    ''' <param name="Cheezie"></param>
    ''' <remarks></remarks>
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub ActualCommandTextToFile(ByVal sender As IDbCommand, ByVal FileName As String, ByVal Cheezie As Boolean)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SQL from code w/o values")
        sb.AppendLine(sender.CommandText.TrimEmbeddedQueryText(True))
        sb.AppendLine("SQL from code with values ready for your SQL editor to test")
        sb.AppendLine(sender.ActualCommandTextByNames.TrimEmbeddedQueryText(Cheezie))
        IO.File.WriteAllText(FileName, sb.ToString)
    End Sub
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Public Sub ActualCommandTextToFile(ByVal sender As IDbCommand, ByVal FileName As String)
        Dim SQL As String = sender.ActualCommandTextByNames
        IO.File.WriteAllText(FileName, SQL.TrimEmbeddedQueryText(False))
    End Sub
    ''' <summary>
    ''' Utility function to flatten SQL statements
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="Cheezie"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Not perfect.
    ''' </remarks>
    <System.Diagnostics.DebuggerStepThrough()> _
    <Runtime.CompilerServices.Extension()> _
    Function TrimEmbeddedQueryText(ByVal value As String, ByVal Cheezie As Boolean) As String
        Dim Result As String = System.Text.RegularExpressions.Regex.Replace(value.TabsToSpaces(6).TrimStart, " {2,}", " ")
        Dim sb As New System.Text.StringBuilder

        If Result.LineCount > 0 Then
            Dim Lines As String() = Result.Split(Chr(13))
            For Each Item As String In Lines
                If Item.Length > 0 Then
                    Dim Holder As String = ""
                    If Not Item.EndsWith(" ") Then
                        Holder = Item.Replace(Chr(13), " ").Replace(Chr(10), " ") & " "
                    Else
                        Holder = Item.Replace(Chr(13), " ").Replace(Chr(10), " ")
                    End If
                    sb.AppendLine(Holder)
                End If
            Next
        End If

        If Cheezie Then
            Return sb.ToString.Replace("   ", " ")
        Else
            Return sb.ToString
        End If

    End Function
End Module
