Option Strict On
Option Explicit On
Option Infer Off
Public Class Form1
    Sub doLayout()
        Panel2.Top = 100
        Panel2.Left = 0
        Panel2.Height = Me.ClientRectangle.Height - Panel2.Top
        Panel2.Width = Me.ClientRectangle.Width
        Panel2.BorderStyle = BorderStyle.FixedSingle
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        doLayout()
    End Sub
    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        doLayout()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim maze As New Maze(CInt(nudRows.Value), CInt(nudCols.Value), CInt(nudWidth.Value), CInt(nudHeight.Value))
        AddHandler maze.MazeComplete, Sub(m As Image)
                                          Panel1.BackgroundImage = m
                                          Panel1.BackgroundImageLayout = ImageLayout.None
                                          Panel1.Width = m.Width
                                          Panel1.Height = m.Height
                                          'maze.PrintMaze()
                                      End Sub
        maze.Generate()

    End Sub
End Class
Public Class Maze
    Inherits Control
    Dim Rows As Integer
    Dim Columns As Integer
    Dim cellWidth As Integer
    Dim cellHeight As Integer
    Dim cells As New Dictionary(Of String, Cell)
    Dim stack As New Stack(Of Cell)
    Public Maze As Image

    Public Event MazeComplete(Maze As Image)
    Private Event CallComplete(Maze As Image)
    Public Shadows ReadOnly Property Bounds As Rectangle
        Get
            Dim rect As New Rectangle(0, 0, Width, Height)
            Return rect
        End Get
    End Property
    Dim WithEvents printDoc As New Printing.PrintDocument()
    Private Sub PrintImage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        Dim nonprinters As List(Of String) = ({"Send To OneNote 2013", "PDFCreator", "PDF Architect 4",
                               "Microsoft XPS Document Writer", "Microsoft Print to PDF", "Fax", "-"}).ToList
        Dim printerName As String = "none"
        For Each a As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            If nonprinters.IndexOf(a) > -1 Then Continue For
            printerName = a
        Next
        If printerName = "none" Then Exit Sub
        printDoc.PrinterSettings.PrinterName = printerName
        Dim imageLeft As Integer = CInt(e.PageBounds.Width / 2) - CInt(Maze.Width / 2)
        Dim imageTop As Integer = CInt(e.PageBounds.Height / 2) - CInt(Maze.Height / 2)
        e.Graphics.DrawImage(Maze, imageLeft, imageTop)
    End Sub
    Public Sub PrintMaze()
        printDoc.Print()
    End Sub
    Public Sub Generate()
        Dim c As Integer = 0
        Dim r As Integer = 0
        For y As Integer = 0 To Height Step cellHeight
            For x As Integer = 0 To Width Step cellWidth
                Dim cell As New Cell(New Point(x, y), New Size(cellWidth, cellHeight), cells, r, c, (Rows - 1), (Columns - 1))
                c += 1
            Next
            c = 0 : r += 1
        Next
        Dim thread As New Threading.Thread(AddressOf Dig)
        thread.Start()
    End Sub
    Private Sub Dig()
        Dim r As Integer = 0
        Dim c As Integer = 0
        Dim key As String = "c" & 5 & "r" & 5
        Dim startCell As Cell = cells(key)
        stack.Clear()
        startCell.Visited = True
        While (startCell IsNot Nothing)
            startCell = startCell.Dig(stack)
            If startCell IsNot Nothing Then
                startCell.Visited = True
                startCell.Pen = Pens.Black
            End If
        End While
        stack.Clear()
        Dim Maze As New Bitmap(Width, Height)
        Using g As Graphics = Graphics.FromImage(Maze)
            g.Clear(Color.White)
            If cells.Count > 0 Then
                For r = 0 To Me.Rows - 1
                    For c = 0 To Me.Columns - 1
                        Dim cell As Cell = cells("c" & c & "r" & r)
                        cell.draw(g)
                    Next
                Next
            End If
        End Using
        Me.Maze = Maze
        RaiseEvent CallComplete(Maze)
    End Sub
    Delegate Sub dComplete(maze As Image)
    Private Sub Call_Complete(maze As Image) Handles Me.CallComplete
        If Me.InvokeRequired Then
            Me.Invoke(New dComplete(AddressOf Call_Complete), maze)
        Else
            RaiseEvent MazeComplete(maze)
        End If
    End Sub
    Sub New(rows As Integer, columns As Integer, cellWidth As Integer, cellHeight As Integer)
        Me.Rows = rows
        Me.Columns = columns
        Me.cellWidth = cellWidth
        Me.cellHeight = cellHeight
        Me.Width = (Me.Columns * Me.cellWidth) + 1
        Me.Height = (Me.Rows * Me.cellHeight) + 1
        Me.CreateHandle()
    End Sub
End Class
Public Class Cell
    Public NorthWall As Boolean = True
    Public SouthWall As Boolean = True
    Public WestWall As Boolean = True
    Public EastWall As Boolean = True
    Public id As String '
    Public Pen As Pen = Pens.Black
    Public Bounds As Rectangle '
    Public Cells As Dictionary(Of String, Cell) '
    Public Column As Integer '
    Public Row As Integer '
    Public NeighborNorthID As String '
    Public NeighborSouthID As String '
    Public NeighborEastID As String '
    Public NeighborWestID As String '
    Public Visited As Boolean = False '
    Public Stack As Stack(Of Cell) '
    Public Sub draw(g As Graphics)
        If NorthWall Then g.DrawLine(Pen, New Point(Bounds.Left, Bounds.Top), New Point(Bounds.Right, Bounds.Top))
        If SouthWall Then g.DrawLine(Pen, New Point(Bounds.Left, Bounds.Bottom), New Point(Bounds.Right, Bounds.Bottom))
        If WestWall Then g.DrawLine(Pen, New Point(Bounds.Left, Bounds.Top), New Point(Bounds.Left, Bounds.Bottom))
        If EastWall Then g.DrawLine(Pen, New Point(Bounds.Right, Bounds.Top), New Point(Bounds.Right, Bounds.Bottom))
    End Sub
    Sub New(location As Point, size As Size, ByRef cellList As Dictionary(Of String, Cell), r As Integer, c As Integer, maxR As Integer, maxC As Integer)
        Me.Bounds = New Rectangle(location, size)
        Me.Column = c
        Me.Row = r
        Me.id = "c" & c & "r" & r
        Dim rowNort As Integer = r - 1
        Dim rowSout As Integer = r + 1
        Dim colEast As Integer = c + 1
        Dim colWest As Integer = c - 1
        NeighborNorthID = "c" & c & "r" & rowNort
        NeighborSouthID = "c" & c & "r" & rowSout
        NeighborEastID = "c" & colEast & "r" & r
        NeighborWestID = "c" & colWest & "r" & r
        If rowNort < 0 Then NeighborNorthID = "none"
        If rowSout > maxR Then NeighborSouthID = "none"
        If colEast > maxC Then NeighborEastID = "none"
        If colWest < 0 Then NeighborWestID = "none"
        Me.Cells = cellList
        Me.Cells.Add(Me.id, Me)
    End Sub
    Function getNeighbor() As Cell
        Dim c As New List(Of Cell)
        If Not NeighborNorthID = "none" AndAlso Cells(NeighborNorthID).Visited = False Then c.Add(Cells(NeighborNorthID))
        If Not NeighborSouthID = "none" AndAlso Cells(NeighborSouthID).Visited = False Then c.Add(Cells(NeighborSouthID))
        If Not NeighborEastID = "none" AndAlso Cells(NeighborEastID).Visited = False Then c.Add(Cells(NeighborEastID))
        If Not NeighborWestID = "none" AndAlso Cells(NeighborWestID).Visited = False Then c.Add(Cells(NeighborWestID))
        Dim max As Integer = c.Count
        Dim currentCell As Cell = Nothing
        If c.Count > 0 Then
            Randomize()
            Dim index As Integer = CInt(Int(c.Count * Rnd()))
            currentCell = c(index)
        End If
        Return currentCell
    End Function
    Function Dig(ByRef stack As Stack(Of Cell)) As Cell
        Me.Stack = stack
        Dim nextCell As Cell = getNeighbor()
        If Not nextCell Is Nothing Then
            stack.Push(nextCell)
            If nextCell.id = Me.NeighborNorthID Then
                Me.NorthWall = False
                nextCell.SouthWall = False
            ElseIf nextCell.id = Me.NeighborSouthID
                Me.SouthWall = False
                nextCell.NorthWall = False
            ElseIf nextCell.id = Me.NeighborEastID
                Me.EastWall = False
                nextCell.WestWall = False
            ElseIf nextCell.id = Me.NeighborWestID
                Me.WestWall = False
                nextCell.EastWall = False
            End If
        ElseIf Not stack.Count = 0
            nextCell = stack.Pop
        Else
            Return Nothing
        End If
        Return nextCell
    End Function
End Class
