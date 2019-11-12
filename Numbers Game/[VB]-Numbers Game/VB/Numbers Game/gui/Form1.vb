Public Class Form1

    Private WithEvents solver As Solver
    Dim numbers As Numbers
    Dim chosenNumbers As List(Of Integer)
    Dim displayLabels() As Label
    Dim numberCounter As Integer
    Dim targetNumber As Integer
    Dim r As New Random

    Dim showSolution As Boolean
    Dim solution As String

    ''' <summary>
    ''' Sets up Class level array and initiates a new game
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        displayLabels = New Label() {lblNumbers1, lblNumbers2, lblNumbers3, lblNumbers4, lblNumbers5, lblNumbers6}
        btnNewGame.PerformClick()
    End Sub

    ''' <summary>
    ''' resets the gui for a new game
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnNewGame_Click(sender As Object, e As EventArgs) Handles btnNewGame.Click
        btnLarge.Enabled = True
        btnSmall.Enabled = True
        For Each lbl As Label In displayLabels
            lbl.Text = ""
        Next
        numbers = New Numbers(r)
        chosenNumbers = New List(Of Integer)
        numberCounter = 0
        targetNumber = r.Next(101, 1000)
        lblTargetNumber.Text = ""
        lblSolution.Text = ""
        txtSolution.Text = ""
        Clock1.resetClock()
        showSolution = False
        solution = ""
        btnNewGame.Enabled = False
    End Sub

    ''' <summary>
    ''' Large number clicked handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnLarge_Click(sender As Object, e As EventArgs) Handles btnLarge.Click
        Dim l As LargeDrawn = numbers.drawLarge
        fillLabels(l.x, l.last)
    End Sub

    ''' <summary>
    ''' Smalll  number clicked handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSmall_Click(sender As Object, e As EventArgs) Handles btnSmall.Click
        Dim x As Integer = numbers.drawSmall
        fillLabels(x)
    End Sub

    ''' <summary>
    ''' Puts chosen numbers in display labels
    ''' When 6th number is selected, initiate the solver search and starts the countdown clock
    ''' </summary>
    ''' <param name="x"></param>
    ''' <param name="isLastLarge"></param>
    Private Sub fillLabels(x As Integer, Optional isLastLarge As Boolean = False)
        chosenNumbers.Add(x)
        displayLabels(numberCounter).Text = x.ToString
        numberCounter += 1
        btnSmall.Enabled = Not numberCounter = 6
        btnLarge.Enabled = Not isLastLarge And Not numberCounter = 6
        If numberCounter = 6 Then
            lblTargetNumber.Text = targetNumber.ToString
            Application.DoEvents()
            solver = New Solver(chosenNumbers, targetNumber)
            AddHandler solver.Solution, AddressOf solver_Solution
            solver.search()
            Dim t As New Threading.Thread(AddressOf startClock)
            t.IsBackground = True
            t.Start()
            btnNewGame.Enabled = False
            btnStopClock.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Background thread
    ''' </summary>
    Private Sub startClock()
        Clock1.startClock()
    End Sub

    ''' <summary>
    ''' Handles the event raised in the background thread solver
    ''' </summary>
    ''' <param name="e"></param>
    Private Delegate Sub solver_Solution_Callback(e As NumberEventArgs)
    Private Sub solver_Solution(e As NumberEventArgs) Handles solver.Solution
        If showSolution Then
            If lblSolution.InvokeRequired Then
                lblSolution.Invoke(New solver_Solution_Callback(AddressOf solver_Solution), e)
            Else
                lblSolution.Text = e.nearest.ToString & Environment.NewLine & Environment.NewLine & e.solutionString
            End If
        Else
            solution = e.nearest.ToString & Environment.NewLine & Environment.NewLine & e.solutionString
        End If
        GC.Collect()
    End Sub

    ''' <summary>
    ''' btnStopClock click handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnStopClock_Click(sender As Object, e As EventArgs) Handles btnStopClock.Click
        btnStopClock.Enabled = False
        Clock1.stopClock()
    End Sub

    ''' <summary>
    ''' Handles the event raised in the background clock thread
    ''' </summary>
    Private Delegate Sub Clock1_clockStopped_Callback()
    Private Sub Clock1_clockStopped() Handles Clock1.clockStopped
        showSolution = True
        If solution <> "" Then
            If lblSolution.InvokeRequired Then
                lblSolution.Invoke(New Clock1_clockStopped_Callback(AddressOf Clock1_clockStopped))
            Else
                lblSolution.Text = solution
            End If
        End If
    End Sub

    ''' <summary>
    ''' Enables/disables gui buttons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lblSolution_TextChanged(sender As Object, e As EventArgs) Handles lblSolution.TextChanged
        If lblSolution.Text <> "" Then
            btnNewGame.Enabled = True
            btnStopClock.Enabled = False
        End If
    End Sub

End Class
