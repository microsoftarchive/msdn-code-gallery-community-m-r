Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports taapBrowser.Forms

Namespace taapBrowser
	Partial Public Class Form1
		Inherits Form

		Private browser As FrmBrowser

		Public Sub New()
			InitializeComponent()
			browser = New FrmBrowser()
			browser.Show(dockPanel)

		End Sub
	End Class
End Namespace
