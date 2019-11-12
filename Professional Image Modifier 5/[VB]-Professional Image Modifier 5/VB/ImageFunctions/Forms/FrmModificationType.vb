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
	Partial Public Class FrmModificationType
		Inherits DockContent

		Public Delegate Sub SusanSelectedHandler()
		Public Event SusanSelected As SusanSelectedHandler
		Public Delegate Sub MoravecSelectedHandler()
		Public Event MoravecSelected As MoravecSelectedHandler
		Public Delegate Sub HarrisSelectedHandler()
		Public Event HarrisSelected As HarrisSelectedHandler
		Public Delegate Sub FASTSelectedHandler()
		Public Event FASTSelected As FASTSelectedHandler

		Private Enum Modifications As Integer
			None
			CornerDetection
		End Enum

		Private Enum Methods
			None
			Susan
			Moravec
			Harris
			FAST
		End Enum


		Public Sub EnableModifications()
			lbModification.Enabled = True
		End Sub

		Public Sub DisableModifications()
			lbModification.Enabled = False
		End Sub

		Public Sub New()
			InitializeComponent()
			lbModification.Items.Add("Corner Detection")
		End Sub

		Protected Overrides Function GetPersistString() As String
			Return Me.Text

		End Function

		Private Sub lbModification_SelectedItemChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbModification.SelectedItemChanged
			lbMethods.Enabled = True
			Select Case lbModification.SelectedIndex
				Case CInt(Modifications.None)
				Case CInt(Modifications.CornerDetection)
					PopulateMethods(Modifications.CornerDetection)
				Case Else
			End Select
		End Sub

		'// Populate the Modification Method list box.
		Private Sub PopulateMethods(ByVal modifications As Modifications)
			Select Case modifications
				Case ImageFunctions.Forms.FrmModificationType.Modifications.None
					lbMethods.Enabled = False
				Case ImageFunctions.Forms.FrmModificationType.Modifications.CornerDetection
					lbMethods.Items.Add("Susan Corner Detection") ' index 1
					lbMethods.Items.Add("Moravec Corner Detection") ' index 2
					lbMethods.Items.Add("Harris Corner Detection")
					lbMethods.Items.Add("FAST Corner Detection")
				Case Else
					lbMethods.Enabled = False
			End Select
		End Sub

		Private Sub lbMethods_SelectedItemChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbMethods.SelectedItemChanged
			Select Case lbMethods.SelectedIndex
				Case CInt(Methods.None)
				Case CInt(Methods.Susan)
					RaiseEvent SusanSelected()
				Case CInt(Methods.Moravec)
					RaiseEvent MoravecSelected()
				Case CInt(Methods.Harris)
					RaiseEvent HarrisSelected()
				Case CInt(Methods.FAST)
					RaiseEvent FASTSelected()
				Case Else
			End Select
		End Sub
	End Class
End Namespace
