Option Strict On
Option Infer On
Imports System.Data
Imports System.Text
Imports System.Data.Odbc
Imports System.IO
'#Const debug = True

''' <summary>
''' MySql - ODBC database viewer 
''' </summary>
''' <remarks>update 2012-05-11</remarks>
Public Class frmMain
#Region "object declarations"

    ''' <summary>
    ''' string zum Adodb Aufruf
    ''' </summary>
    Private strSQL As String

    Private fullpath As String
    Private stopwatch As New System.Diagnostics.Stopwatch

#End Region
#Region "init"
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text &= String.Format(" Version {0}.{1:00} Ellen Ramcke 2011", _
                                 My.Application.Info.Version.Major, _
                                 My.Application.Info.Version.Minor)
      
        Me.fullpath = My.Application.Info.DirectoryPath

    End Sub

#End Region
#Region "form TX Box events"
    '
    '  contextmenue tx box
    '
    Private Sub CopyTx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyTx.Click
        rtbTX.Copy()
    End Sub
    Private Sub PasteTx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteTx.Click
        rtbTX.Paste()
    End Sub
    Private Sub CutTx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutTx.Click
        rtbTX.Cut()
    End Sub
    '
    '   get a query string from tx box now
    '
    Private Sub rtbTX_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtbTX.MouseDoubleClick
        'first char in current line
        Dim selectionStart As Integer = rtbTX.GetFirstCharIndexOfCurrentLine

        If rtbTX.Text.Substring(selectionStart).StartsWith("//") Then
            MsgBox("You clicked on a comment line")
            rtbTX.SelectionLength = 0
            Exit Sub
        End If
        'line starts with  "SELECT" ?
        If Not rtbTX.Text.Substring(selectionStart).ToUpper.StartsWith("SELECT") Then
            rtbTX.SelectionLength = 0
            MsgBox("please click in first line")
            Exit Sub
        End If
        rtbTX.SelectionStart = selectionStart
        Dim selectionEnd As Integer = rtbTX.Text.IndexOf(";", selectionStart)
        If selectionEnd = -1 Then
            MsgBox("';' not found!")
            Exit Sub
        End If

        rtbTX.SelectionLength = selectionEnd - selectionStart + 1
        ' now get query string from the richtextbox
        Dim line As String = rtbTX.Text.Substring(selectionStart, rtbTX.SelectionLength)
        'ev. LF weg
        line = line.Replace(ControlChars.Lf, "")

#If Debug = True Then
        Debug.Print(line)
        Dim array() As Byte = System.Text.Encoding.ASCII.GetBytes(line)
        Dim s As String = "1: "
        For Each b As Byte In array
            s &= String.Format("{0:X2} ", b)
        Next
        Debug.Print(s)
#End If

        ' database
        dbAccess(line)

    End Sub
    '
    '  save tx box to file
    '
    Private Sub Save_Log(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveTx.Click

        Dim dateNow As String = String.Format("{0:yyyy-MM-dd-hh-mm-ss}", DateTime.Now)
        Dim newFile As String = "sqlData_" & dateNow & ".TXT"
        'Path erzeugen für abspeichern der daten

        With My.Computer.FileSystem

            Dim fileName As String = Path.Combine(fullpath, newFile)
            rtbTX.SaveFile(fileName, RichTextBoxStreamType.PlainText)

            If .FileExists(fileName) Then
                MsgBox(newFile & " written")
            End If
        End With
    End Sub
    '
    ' load file into tx box
    '
    Private Sub TxbtnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadTX.Click

        OpenFileDialog1.DefaultExt = "*.TXT"
        OpenFileDialog1.Filter = "txt Files | *.TXT"
        OpenFileDialog1.InitialDirectory = Me.fullpath
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            Dim file As String = OpenFileDialog1.FileName()

            rtbTX.Clear()
            rtbTX.LoadFile(file, RichTextBoxStreamType.PlainText)

        Else
            MsgBox("TX box: no data")
        End If
    End Sub
    '
    'clear tx box now
    '
    Private Sub TxbtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearTX.Click
        rtbTX.Clear()
    End Sub
#End Region
#Region "form events"
    '
    '  clear dataset
    '
    Private Sub RxbtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RxbtnClear.Click
        For Each tbl As DataTable In Me.DataSet1.Tables
            tbl.Rows.Clear()
            tbl.Columns.Clear()
            tbl.Clear()
        Next
    End Sub
    '
    '  mainForm close
    '
    Private Sub QuitTool_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        My.Settings.Save()
        Me.Close()
    End Sub
    '
    ' window orientation
    '
    Private Sub mnItemWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnItemWindow.Click
        If SplitContainer1.Orientation = Orientation.Horizontal Then
            SplitContainer1.Orientation = Orientation.Vertical
        Else
            SplitContainer1.Orientation = Orientation.Horizontal
        End If
    End Sub
#End Region
#Region "utilites"
    ' 
    '   select font
    '
    Private Sub fontMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
                                   Handles mnItemSmall.Click, mnItemMedium.Click, mnItemLarge.Click
        Dim s As String = CType(sender, ToolStripMenuItem).Text
        Dim fnt As Font = Nothing

        If s = "Large" Then
            fnt = New Font("Lucida Console", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf s = "Medium" Then
            fnt = New Font("Lucida Console", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ElseIf s = "Small" Then
            fnt = New Font("Lucida Console", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If

        rtbTX.Font = fnt

    End Sub
    '
    ' stop time measuring
    '
    Private Sub stopTime()
        stopwatch.Stop()
        Dim deltaT As Long = stopwatch.ElapsedMilliseconds
        Me.status2.Text = "** time: " & deltaT.ToString & "ms"
        stopwatch.Reset()
    End Sub

#End Region
#Region "db methods"
    ''' <summary>
    ''' use odbcdataadapter 
    ''' </summary>
    ''' <param name="strSQL">query</param>
    ''' <returns>successfull</returns>
    ''' <remarks>read database and bring data to form</remarks>
    Private Function dbAccess(ByVal strSQL As String) As Boolean

        Dim tab As String = "Table0"
        Dim adapter As OdbcDataAdapter
        'Connector/ODBC 3.51 connection string
        Dim MyConString As String = "DRIVER={MySQL ODBC 3.51 Driver};" & _
        "SERVER=14.177.224.53;" & _
        "DATABASE=result;" & _
        "USER=username;" & _
        "PASSWORD=password;" & _
        "OPTION=2;"

        Try
            adapter = New OdbcDataAdapter(strSQL, MyConString)
            adapter.MissingMappingAction = MissingMappingAction.Passthrough
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
            stopwatch.Start()
        Catch ex As Exception
            MsgBox("connection: " & ex.Message)
            Return False
        End Try

        'checkbox overwrite mode
        If Me.CheckBox1.Checked Then
            For Each tbl As DataTable In DataSet1.Tables
                tbl.Rows.Clear()
                tbl.Columns.Clear()
                tbl.Clear()
            Next
        End If

        'fill table now
        Try
            adapter.Fill(DataSet1, tab)
        Catch ex As Exception
            MsgBox("fill: " & ex.Message)
            Return False
        End Try

        'binding now
        Me.DataGridView1.DataSource = DataSet1
        Me.DataGridView1.DataMember = tab

        stopTime()

        Return True
    End Function
#End Region
End Class
