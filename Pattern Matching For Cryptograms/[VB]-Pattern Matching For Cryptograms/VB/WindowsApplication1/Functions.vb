Option Strict On
Module Functions
    Function GetWordPatternMatches(Word As String, _
                               Dictionary As List(Of String), _
                               Optional Filter As String = "*", _
                               Optional PB As ProgressBar = Nothing, _
                               Optional UpdateLabel As Label = Nothing) _
                        As ListViewItem()
        If Not PB Is Nothing Then PB.Value = 0
        If Not PB Is Nothing Then PB.Maximum = 0
        Const Legend As String = "01234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        If Word.Length = 0 Then Return {}
        Dim map As New List(Of pt), I = 0, WordPattern As String = ""
        If Not PB Is Nothing Then PB.Maximum += Word.Count
        For Each S As String In Word
            If Not PB Is Nothing Then PB.Increment(1)
            Dim Q1 = From P In map Where P.Letter = S Select P
            If Not Q1.ToArray.Count = 0 Then map.Add(New pt(Q1.ToArray(0).ID, S)) : Continue For
            map.Add(New pt((Legend)(I), S))
            I += 1
        Next
        If Not PB Is Nothing Then PB.Maximum += map.Count
        For Each P As pt In map
            If Not PB Is Nothing Then PB.Increment(1)
            WordPattern = WordPattern & P.ID : Next
        Dim Q2 = From W In Dictionary Where (W.Length = Word.Length) And (W Like Filter) Select W
        Dim results As New List(Of String)
        If Not PB Is Nothing Then PB.Maximum += Q2.ToArray.Count
        For Each W In Q2.ToArray
            If Not PB Is Nothing Then PB.Increment(1)
            Dim map2 As New List(Of pt), I2 = 0, DictPattern As String = ""
            For Each S As String In W
                Dim Q3 = From P In map2 Where P.Letter = S Select P
                If Not Q3.ToArray.Count = 0 Then map2.Add(New pt(Q3.ToArray(0).ID, S)) : Continue For
                map2.Add(New pt((Legend)(I2), S))
                I2 += 1 : Next
            For Each P As pt In map2
                DictPattern = DictPattern & P.ID
            Next
            If DictPattern = WordPattern Then results.Add(W)
            If Not UpdateLabel Is Nothing Then
                UpdateLabel.Text = results.Count & " matches found so far..."
                Application.DoEvents()
            End If
        Next
        Dim Items As New List(Of ListViewItem)
        If Not PB Is Nothing Then PB.Maximum += results.Count
        For Each S As String In results
            If Not PB Is Nothing Then PB.Increment(1)
            Dim Item As New ListViewItem(Word)
            Item.SubItems.AddRange({S, WordPattern})
            Items.Add(Item)
        Next
        Return Items.ToArray
    End Function
    Sub SaveToLower(List As List(Of String), Filename As String)
        Dim Result As String = String.Empty
        For Each S As String In List
            Result = Result & LCase(S) & vbCrLf
        Next
        Result = Result.Substring(0, Result.Length - 1)
        My.Computer.FileSystem.WriteAllText(Filename, Result, False)
    End Sub
    Sub ExportNewWordList(origDict As List(Of String), WordListToCombine As String, FileName As String)
        Dim Dict2 As String() = WordListToCombine.Replace(" ", "").Replace(vbLf, vbCr).Replace(vbCr, " ").Replace("  ", " ").Split(" "c)
        Dim Q As IEnumerable(Of String) = From w As String In Dict2 Where origDict.IndexOf(w) = -1 Select w
        origDict.AddRange(Q.ToArray)
        MsgBox(FileName)
        Dim Resultlist As New List(Of String)
        Dim Result As String = String.Empty
        If My.Computer.FileSystem.FileExists(FileName) Then My.Computer.FileSystem.DeleteFile(FileName)
        For Each W As String In origDict
            Dim Valid As Boolean = True
            For Each C As Char In W
                If Not Char.IsLetter(C) Then Valid = False
            Next
            If Valid = False Then Continue For
            If Resultlist.IndexOf(W) = -1 Then Resultlist.Add(W)
        Next
        Resultlist.Sort()
        For Each W As String In Resultlist
            Result = Result & W & vbCrLf
        Next
        My.Computer.FileSystem.WriteAllText(FileName, Result, False)
    End Sub
    Private Class pt ' Pattern Table
        Public ID, Letter As String
        Sub New(ID As String, Letter As String)
            Me.ID = ID : Me.Letter = Letter
        End Sub
    End Class
End Module
