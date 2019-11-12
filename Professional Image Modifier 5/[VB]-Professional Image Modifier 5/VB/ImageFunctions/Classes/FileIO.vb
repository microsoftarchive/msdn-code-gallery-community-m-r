Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace ImageFunctions.Classes
	Friend Class FileIO
		Public Delegate Sub MediaSelectedHandler(ByVal PathToMedia As String)
		Public Event SingleMediaSelected As MediaSelectedHandler
		Public Delegate Sub MediaSelectionCancelledHandler()
		Public Event MediaSelectionCancelled As MediaSelectionCancelledHandler

		Private Shared LastDirectory As String = Nothing ' Remembers which directory the user last went to.

		''' <summary>
		''' Instantiates the Class
		''' </summary>
		Public Sub New()

		End Sub

		''' <summary>
		''' Open a Single Media File
		''' Raises MediaSelectedHandler Event when a file is chosen
		''' Raises MediaSelectionCancelledHandler Event if the User Cancels
		''' </summary>
		Public Sub OpenFile()
			Dim ofd As New OpenFileDialog()

			' Configure the Dialog
			ofd.AutoUpgradeEnabled = True ' Upgrades appearance on Vista
			ofd.CheckFileExists = True
			ofd.CheckPathExists = True
			ofd.DereferenceLinks = True ' convert .lnk to real paths
			If String.IsNullOrWhiteSpace(LastDirectory) Then
				ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) 'TODO: Allow End user to set this in the application Preferences
			Else
				ofd.InitialDirectory = LastDirectory
			End If

			ofd.Multiselect = False ' One File only!
			ofd.ReadOnlyChecked = True 'TODO: Allow End User to decide in Application Preferences
			ofd.RestoreDirectory = True ' Next time we open this - go to the previous selected folder.
			ofd.ShowReadOnly = False 'TODO: Let EU Decide
			ofd.SupportMultiDottedExtensions = True ' Quite common these days
			ofd.Title = "Select a media file to load" 'TODO: Globalization
			ofd.ValidateNames = False ' Doesn't have to be valid Win32 file name

			' Open the dialog
			Dim dr As DialogResult = ofd.ShowDialog()
			If dr = DialogResult.OK Then
				Dim finfo As New FileInfo(ofd.FileName)
				RaiseEvent SingleMediaSelected(ofd.FileName)
				LastDirectory = finfo.DirectoryName
			Else
				RaiseEvent MediaSelectionCancelled()
			End If

			ofd.Dispose() ' Clean up our resources now.
		End Sub

	End Class
End Namespace
