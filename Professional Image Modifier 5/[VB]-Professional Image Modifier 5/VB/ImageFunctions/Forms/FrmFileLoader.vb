Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports ImageFunctions.Controls
Imports WeifenLuo.WinFormsUI.Docking

Namespace ImageFunctions.Forms
	Partial Public Class FrmFileLoader
		Inherits DockContent

		Public Delegate Sub ThumbnailSelectedHandler(ByVal PathToMedia As String)
		Public Event ThumbnailSelected As ThumbnailSelectedHandler


		Public Sub New()
			InitializeComponent()
		End Sub

		Protected Overrides Function GetPersistString() As String
			Return Me.Text
		End Function

		Friend Sub Add(ByVal thumbnail As ThumbnailControl)
			AddHandler thumbnail.ThumbnailSelected, AddressOf thumbnail_ThumbnailSelected
			ThumbnailLayoutPanel.Controls.Add(thumbnail)

		End Sub

		Private Sub thumbnail_ThumbnailSelected(ByVal PathToMedia As String)
			RaiseEvent ThumbnailSelected(PathToMedia)
		End Sub



	End Class
End Namespace
