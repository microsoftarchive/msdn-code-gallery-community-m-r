Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Imports WeifenLuo.WinFormsUI.Docking

Namespace ImageFunctions.Forms
	Partial Public Class FrmConsole
		Inherits DockContent

		Private Shared MessageCounter As Integer = 1
		Private Shared IsIdle As Boolean = False
		Private Delegate Sub LogDelegate(ByVal message As String)

		Public Property CountDown() As Integer

		Public Sub New()
			InitializeComponent()
			SetCountdown()
		End Sub

		Private Sub SetCountdown()
			CountDown = 5 * 60000 ' x Minutes
		End Sub

		Protected Overrides Function GetPersistString() As String
			Return Me.Text
		End Function

		''' <summary>
		''' Print out the message
		''' \t = insert Tab spacing
		''' </summary>
		''' <param name="message"></param>
		Public Sub Log(ByVal message As String)
			If rtbConOut.InvokeRequired Then
				Dim d As New LogDelegate(AddressOf Log)
				Me.Invoke(d, New Object() { message })
			Else
				rtbConOut.AppendText("[" & MessageCounter & "]" & vbTab & " " & Date.Now.ToString() & vbTab & message & Environment.NewLine)
				rtbConOut.Focus()

				MessageCounter += 1
			End If
		End Sub

		Private Sub refreshToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles refreshToolStripMenuItem.Click
			Me.Invalidate() ' Should redraw the whole window correcting any display errors
			Me.Refresh()
		End Sub

		Private Sub timerIdle_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timerIdle.Tick
			If IsIdle Then
				CountDown -= 1
			End If

			If CountDown = 0 Then
				rtbConOut.Focus()
				SetCountdown()
			End If

			IsIdle = True
		End Sub

		Private Sub rtbConOut_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
			IsIdle = False

		End Sub

		Private Sub rtbConOut_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
			IsIdle = False

		End Sub




	End Class
End Namespace
