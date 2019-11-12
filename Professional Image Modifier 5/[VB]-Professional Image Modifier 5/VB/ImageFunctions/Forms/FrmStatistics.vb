Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports WeifenLuo.WinFormsUI.Docking

Namespace ImageFunctions.Forms
	Partial Public Class FrmStatistics
		Inherits DockContent

		Public Delegate Sub StatisticsLogHandler(ByVal Message As String)
		Public Event StatisticsLog As StatisticsLogHandler

		Private swStatistics As New Stopwatch()
		Private thisImage As String
		Public Sub New()
			InitializeComponent()

		End Sub


		Protected Overrides Function GetPersistString() As String
			Return Me.Text

		End Function


		Public Sub DoStatistics(ByVal CurrentImage As String)
			thisImage = CurrentImage
			swStatistics.Reset()
			swStatistics.Start()
			RaiseEvent StatisticsLog("Building Statistics")
			Dim imgStats As New Classes.ImageStatistics(CurrentImage)
			AddHandler imgStats.StatsComplete, AddressOf imgStats_StatsComplete
			AddHandler imgStats.StatsError, AddressOf imgStats_StatsError
			imgStats.GetStatistics()
			swStatistics.Stop()
			RaiseEvent StatisticsLog("Statistics completed in " & swStatistics.Elapsed)
		End Sub

		Private Sub imgStats_StatsError(ByVal Message As String)
			RaiseEvent StatisticsLog("Error Collecting Statistics: " & Message)
		End Sub

		' Compile the statistics into the Datagrid view that we have generated
		Private Sub imgStats_StatsComplete(ByVal Statistics As System.Collections.ArrayList)
			Dim tmp As Array = Statistics.ToArray()

			ResetStatistics()

			For idx As Integer = 0 To tmp.Length - 1
				Dim dgvr As New DataGridViewRow()
				dgvr.CreateCells(dgvStatistics)
'INSTANT VB NOTE: The variable description was renamed since Visual Basic does not handle local variables named the same as class members well:
				Dim description_Renamed As String = tmp.GetValue(idx).ToString()

				Dim value As String = tmp.GetValue(idx + 1).ToString()
				dgvr.Cells(0).Value = description_Renamed
				dgvr.Cells(1).Value = value
				AddStatisticsRow(dgvr)
				idx += 1
			Next idx
		End Sub

		Private Sub AddStatisticsRow(ByVal dgvr As DataGridViewRow)

			dgvStatistics.Rows.Add(dgvr)

		End Sub


		Private Sub ResetStatistics()

			dgvStatistics.Rows.Clear()

		End Sub

		Private Sub rebuildToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

			DoStatistics(thisImage)

		End Sub
	End Class
End Namespace
