Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Imaging

#Region "Image Specific References"
Imports ImageFunctions.Modifications.CornerDetection
Imports AForge
Imports ImageFunctions.Forms
Imports System.Threading.Tasks
Imports ImageFunctions.Controls
Imports AForge.Imaging
Imports System.Diagnostics
Imports System.IO
Imports WeifenLuo.WinFormsUI.Docking
Imports ImageFunctions.Classes
Imports ImageFunctions.Classes.TrIDWrappers

#End Region


Namespace ImageFunctions
	Partial Public Class Form1
		Inherits Form


		#Region "Forms"

		'TODO: To be re-implemented.
		'	private FrmProcessing m_Processing = null; // Not a dockable form

		Private m_bSaveLayout As Boolean = True
		Private m_deserializeDockContent As DeserializeDockContent
		Private ConsoleWindow As New FrmConsole()
		Private FileLoader As New FrmFileLoader()
		Private Histogram As New FrmHistogram()
		Private ModificationProperties As New FrmModificationProperties()
		Private ModificationTypes As New FrmModificationType()
		Private Statistics As New FrmStatistics()
		Private ImageDisplay As New FrmImageDisplay()
		Private CommandBox As New FrmCommandBox()

		#End Region

		#Region "Enums"


		Private Enum RGB As Integer
			Red
			Green
			Blue
		End Enum

		Private Enum FileType
			Unknown
			Image
		End Enum
		#End Region

		#Region "Private Fields"
		Private IsImage As Boolean
		Private dFileLoaderThumbnails As New Dictionary(Of Integer, String)()
		Private ThumbnailControlerCounter As Integer = 0
		Private CurrentImage As String
		#End Region

		'TODO: Prevent Users from running multiple <Hazardous> operations at the same time

		Public Sub New()
			InitializeComponent()
		End Sub

		''' <summary>
		''' Loads the current Docking Layout if exists, or creates the default layout.
		''' Generates all the Events that we need to watch for.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			m_deserializeDockContent = New DeserializeDockContent(AddressOf GetDockContentFromPersistString)
			Dim configFile As String = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config")

			If File.Exists(configFile) Then ' Load DockingPanel Layout.
				Try
					DockingPanel.LoadFromXml(configFile, m_deserializeDockContent)
				Catch ex As Exception
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Error: " & ex.Message)
					End If
				End Try

			Else ' Create default layout.
				ConsoleWindow.Show(DockingPanel, DockState.DockBottom)
				FileLoader.Show(DockingPanel, DockState.DockLeft)
				Histogram.Show(DockingPanel, DockState.DockLeft)
				Statistics.Show(DockingPanel, DockState.DockLeft)
				ModificationTypes.Show(DockingPanel, DockState.DockRight)
				ModificationProperties.Show(DockingPanel, DockState.DockRight)
				ImageDisplay.Show(DockingPanel, DockState.DockRight)
				CommandBox.Show(DockingPanel, DockState.DockBottom)
			End If



			AddHandler ImageDisplay.FormClosing, AddressOf ImageDisplay_FormClosing
			AddHandler ImageDisplay.MediaFailedToLoad, AddressOf ImageDisplay_MediaFailedToLoad
			AddHandler ImageDisplay.MediaLoaded, AddressOf ImageDisplay_MediaLoaded
			AddHandler ImageDisplay.MediaPixelColour, AddressOf ImageDisplay_MediaPixelColour
			AddHandler ImageDisplay.MediaPixelCoordinates, AddressOf ImageDisplay_MediaPixelCoordinates
			AddHandler ImageDisplay.ImageDisplayLog, AddressOf ImageDisplay_ImageDisplayLog
			AddHandler Histogram.histogramLog_Renamed, AddressOf Histogram_histogramStatus
			AddHandler Histogram.histogramCompleted_Renamed, AddressOf Histogram_histogramCompleted
			AddHandler Statistics.StatisticsLog, AddressOf Statistics_StatisticsLog
			AddHandler FileLoader.ThumbnailSelected, AddressOf FileLoader_ThumbnailSelected
			AddHandler ModificationTypes.SusanSelected, AddressOf ModificationTypes_SusanSelected
			AddHandler ModificationTypes.HarrisSelected, AddressOf ModificationTypes_HarrisSelected
			AddHandler ModificationTypes.MoravecSelected, AddressOf ModificationTypes_MoravecSelected
			AddHandler ModificationTypes.FASTSelected, AddressOf ModificationTypes_FASTSelected
			AddHandler ModificationProperties.ModificationPropertiesLog, AddressOf ModificationProperties_ModificationPropertiesLog
			AddHandler ModificationProperties.UpdateImage, AddressOf ModificationProperties_UpdateImage
			ModificationProperties.CurrentImage = CurrentImage
		End Sub

		''' <summary>
		''' Parses the DockingPanel Layout and returns the appropriate Instantiated form as requested by the Persist String.
		''' </summary>
		''' <param name="persistString"></param>
		''' <returns></returns>
		Private Function GetDockContentFromPersistString(ByVal persistString As String) As IDockContent
			Select Case persistString
				Case "Console"
					consoleMenuItem.Checked = True
					Return ConsoleWindow

				Case "File Loader"
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Loading File Loader window")
					End If
					fileLoaderMenuItem.Checked = True
					Return FileLoader

				Case "Histogram"
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Loading Histogram window")
					End If
					histogramMenuItem.Checked = True
					Return Histogram

				Case "Image Display"
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Loading Image Display window")
					End If
					imageDisplayMenuItem.Checked = True
					Return ImageDisplay

				Case "Modification Properties"
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Loading Modification Properties window")
					End If
					modificationPropertiesMenuItem.Checked = True
					Return ModificationProperties

				Case "Modification Types"
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Loading Modification Types window")
					End If
					modificationTypesMenuItem.Checked = True
					Return ModificationTypes

				Case "Exif Statistics"
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Loading Exif Statistics window")
					End If
					statisticsMenuItem.Checked = True
					Return Statistics
				Case "Command Box"
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Loading Command Window")
					End If
					commandInputToolStripMenuItem.Checked = True
					Return CommandBox
				Case Else
					If ConsoleWindow IsNot Nothing Then
						ConsoleWindow.Log("Unknown persistString: " & persistString)
					End If
					consoleMenuItem.Checked = True
					Return ConsoleWindow

			End Select
		End Function

		''' <summary>
		''' Saves the current Docking Layout.
		''' TODO: Save unsaved files and other warnings.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			Dim configFile As String = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config")

			If m_bSaveLayout Then
				DockingPanel.SaveAsXml(configFile)
			ElseIf File.Exists(configFile) Then
				File.Delete(configFile)
			End If
		End Sub

		#Region "Menu"
		#Region "Windows Menu"
		''' <summary>
		''' If the console window is not displayed show it, Create and show if necessary
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub consoleMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles consoleMenuItem.Click
			If ConsoleWindow IsNot Nothing AndAlso consoleMenuItem.Checked = False Then
				consoleMenuItem.Checked = True
				ConsoleWindow.Show(DockingPanel, DockState.DockBottom)
			ElseIf ConsoleWindow IsNot Nothing AndAlso consoleMenuItem.Checked = True Then
				consoleMenuItem.Checked = False
				ConsoleWindow.Close()
			Else
				ConsoleWindow = New FrmConsole()
				consoleMenuItem.Checked = True
				ConsoleWindow.Show(DockingPanel, DockState.DockBottom)
			End If
		End Sub

		''' <summary>
		''' If the Statistics window is not displayed show it, Create and show if necessary
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub statisticsMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles statisticsMenuItem.Click
			If Statistics IsNot Nothing AndAlso statisticsMenuItem.Checked = False Then
				statisticsMenuItem.Checked = True
				Statistics.Show(DockingPanel, DockState.DockBottom)
			ElseIf Statistics IsNot Nothing AndAlso statisticsMenuItem.Checked = True Then
				statisticsMenuItem.Checked = False
				Statistics.Close()
			Else
				Statistics = New FrmStatistics()
				statisticsMenuItem.Checked = True
				Statistics.Show(DockingPanel, DockState.DockBottom)
			End If
		End Sub

		Private Sub commandInputToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles commandInputToolStripMenuItem.Click
			If CommandBox IsNot Nothing AndAlso commandInputToolStripMenuItem.Checked = False Then
				commandInputToolStripMenuItem.Checked = True
				CommandBox.Show(DockingPanel, DockState.DockBottom)
			ElseIf CommandBox IsNot Nothing AndAlso commandInputToolStripMenuItem.Checked = True Then
				commandInputToolStripMenuItem.Checked = False
				CommandBox.Close()
			Else
				CommandBox = New FrmCommandBox()
				commandInputToolStripMenuItem.Checked = True
				CommandBox.Show(DockingPanel, DockState.DockBottom)
			End If
		End Sub

		Private Sub histogramMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles histogramMenuItem.Click
			If Histogram IsNot Nothing AndAlso histogramMenuItem.Checked = False Then
				histogramMenuItem.Checked = True
				Histogram.Show(DockingPanel, DockState.DockBottom)
			ElseIf Histogram IsNot Nothing AndAlso histogramMenuItem.Checked = True Then
				histogramMenuItem.Checked = False
				Histogram.Close()
			Else
				Histogram = New FrmHistogram()
				histogramMenuItem.Checked = True
				Histogram.Show(DockingPanel, DockState.DockBottom)
			End If
		End Sub

		Private Sub imageDisplayMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles imageDisplayMenuItem.Click
			If ImageDisplay IsNot Nothing AndAlso imageDisplayMenuItem.Checked = False Then
				imageDisplayMenuItem.Checked = True
				ImageDisplay.Show(DockingPanel, DockState.DockBottom)
			ElseIf ImageDisplay IsNot Nothing AndAlso imageDisplayMenuItem.Checked = True Then
				imageDisplayMenuItem.Checked = False
				ImageDisplay.Close()
			Else
				ImageDisplay = New FrmImageDisplay()
				imageDisplayMenuItem.Checked = True
				ImageDisplay.Show(DockingPanel, DockState.DockBottom)
			End If
		End Sub

		Private Sub modificationPropertiesMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles modificationPropertiesMenuItem.Click
			If ModificationProperties IsNot Nothing AndAlso modificationPropertiesMenuItem.Checked = False Then
				modificationPropertiesMenuItem.Checked = True
				ModificationProperties.Show(DockingPanel, DockState.DockBottom)
			ElseIf ModificationProperties IsNot Nothing AndAlso modificationPropertiesMenuItem.Checked = True Then
				modificationPropertiesMenuItem.Checked = False
				ModificationProperties.Close()
			Else
				ModificationProperties = New FrmModificationProperties()
				modificationPropertiesMenuItem.Checked = True
				ModificationProperties.Show(DockingPanel, DockState.DockBottom)
			End If
		End Sub

		Private Sub modificationTypesMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles modificationTypesMenuItem.Click
			If ModificationTypes IsNot Nothing AndAlso modificationTypesMenuItem.Checked = False Then
				modificationTypesMenuItem.Checked = True
				ModificationTypes.Show(DockingPanel, DockState.DockBottom)
			ElseIf ModificationTypes IsNot Nothing AndAlso modificationTypesMenuItem.Checked = True Then
				modificationTypesMenuItem.Checked = False
				ModificationTypes.Close()
			Else
				ModificationTypes = New FrmModificationType()
				modificationTypesMenuItem.Checked = True
				ModificationTypes.Show(DockingPanel, DockState.DockBottom)
			End If
		End Sub
		#End Region

		#Region "Help Menu"
		''' <summary>
		''' Shows the About Box
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub aboutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles aboutToolStripMenuItem.Click
			Dim about As New FrmAboutBox()
			about.ShowDialog()
		End Sub
		#End Region
		#End Region

		#Region "Command Strip"
		''' <summary>
		''' Open a Single file for modification
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub openfileToolStripButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openfileToolStripButton.Click
			Dim SingleFileLoad As New FileIO()

			AddHandler SingleFileLoad.SingleMediaSelected, AddressOf SingleFileLoad_SingleMediaSelected
			AddHandler SingleFileLoad.MediaSelectionCancelled, AddressOf SingleFileLoad_MediaSelectionCancelled
			ConsoleWindow.Log("Select Media ")
			SingleFileLoad.OpenFile()
		End Sub

		''' <summary>
		''' Shows all the System Installed Codecs.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub btnSystemCodecInformation_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles btnSystemCodecInformation.Click
			For Each ici As ImageCodecInfo In ImageCodecInfo.GetImageDecoders()
				ConsoleWindow.Log("*************************************************")
				ConsoleWindow.Log("Name: " & ici.CodecName)
				ConsoleWindow.Log("Dll Name: " & ici.DllName)
				ConsoleWindow.Log("Filename Extension(s): " & ici.FilenameExtension)
				'TODO: Work out how to get Flag information.
				'foreach (ImageCodecFlags icf in ici.Flags)
				'{
				'	Log("Codec Flags: " + icf.ToString());
				'}
				ConsoleWindow.Log("Format Description: " & ici.FormatDescription)
				ConsoleWindow.Log("Mime Type: " & ici.MimeType)
				ConsoleWindow.Log("Signature Masks: " & Utilities.ConvertToString(ici.SignatureMasks))
				ConsoleWindow.Log("Signature Patterns: " & Utilities.ConvertToString(ici.SignaturePatterns))
				ConsoleWindow.Log("Codec Version: " & ici.Version)
				ConsoleWindow.Log("*************************************************")
			Next ici
		End Sub
		#End Region


		''' <summary>
		''' The User decided not to open a file.
		''' </summary>
		Private Sub SingleFileLoad_MediaSelectionCancelled()
			ConsoleWindow.Log("User Canceled Media Selection ")
		End Sub

		''' <summary>
		''' Send the PathToMedia value to ImageDisplay
		''' </summary>
		''' <param name="PathToMedia"></param>
		Private Sub SingleFileLoad_SingleMediaSelected(ByVal PathToMedia As String)
			If ImageDisplay IsNot Nothing Then ' If the image display form exists. Should make sure it is showing as well!
				ConsoleWindow.Log("Loading & Processing Media " & PathToMedia)
				Dim fileType As New IdentifyFileType(PathToMedia)
				AddHandler fileType.IdentifyFileTypeComplete, AddressOf fileType_IdentifyFileTypeComplete
				AddHandler fileType.IdentifyFileTypeError, AddressOf fileType_IdentifyFileTypeError
				AddHandler fileType.IdentifyFileTypeLog, AddressOf fileType_IdentifyFileTypeLog
				fileType.GetFileType()
				If IsImage Then
					If Not dFileLoaderThumbnails.ContainsValue(PathToMedia) Then
						If FileLoader IsNot Nothing Then
							Dim thumbnail As New ThumbnailControl(PathToMedia)
							FileLoader.Add(thumbnail)
						End If
						dFileLoaderThumbnails.Add(ThumbnailControlerCounter, PathToMedia)
						ThumbnailControlerCounter += 1
					End If
					If CurrentImage <> PathToMedia Then
						ImageDisplay.ShowSingleMedia(PathToMedia)
						Histogram.DoHistogram(PathToMedia)
						Statistics.DoStatistics(PathToMedia)
						CurrentImage = PathToMedia
					End If
				End If
			End If
		End Sub

		#Region "FileType Identification Events"

		Private Sub fileType_IdentifyFileTypeComplete(ByVal ft As IdentifyFileType.FileType)
			Select Case ft
				Case IdentifyFileType.FileType.Unknown
					IsImage = False
				Case IdentifyFileType.FileType.Image
					IsImage = True
				Case Else
					IsImage = False
			End Select
		End Sub

		Private Sub fileType_IdentifyFileTypeLog(ByVal Message As String)
			ConsoleWindow.Log(Message)
		End Sub

		Private Sub fileType_IdentifyFileTypeError(ByVal Message As String)
			ConsoleWindow.Log("Error Identifying File: " & Message)
		End Sub
		#End Region

		#Region "Histogram Events"

		''' <summary>
		''' Log what the Histogram Builder is reporting to us.
		''' </summary>
		''' <param name="Message">
		''' String: Message to be sent to ConsoleWindow.Log();
		''' </param>
		Private Sub Histogram_histogramStatus(ByVal Message As String)
			ConsoleWindow.Log(Message)
		End Sub

		''' <summary>
		''' Log the Completion of the Histogram being built.
		''' </summary>
		''' <param name="Message">
		''' String: Information about the build process
		''' </param>
		Private Sub Histogram_histogramCompleted(ByVal Message As String)
			ConsoleWindow.Log(Message)
		End Sub



		#End Region

		#Region "ImageDisplay Events"

		''' <summary>
		''' Information from the ImageDisplay form to be shown to the End User
		''' </summary>
		''' <param name="Message"></param>
		Private Sub ImageDisplay_ImageDisplayLog(ByVal Message As String)
			ConsoleWindow.Log(Message)
		End Sub

		''' <summary>
		''' The XY Coordinates of the Mouse on the Image
		''' </summary>
		''' <param name="mouseXY"></param>
		Private Sub ImageDisplay_MediaPixelCoordinates(ByVal mouseXY As System.Drawing.Point)
			If histogramMenuItem.Checked Then
				Histogram.SetMouseCoordinates(mouseXY)
			End If
		End Sub

		''' <summary>
		''' The colour of the Pixel under the Mouse within the image
		''' </summary>
		''' <param name="colour"></param>
		Private Sub ImageDisplay_MediaPixelColour(ByVal colour As Color)
			If histogramMenuItem.Checked Then
				Histogram.SetPixelColour(colour)
			End If
		End Sub

		''' <summary>
		''' Notification that the ImageDisplay form has been closed
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub ImageDisplay_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
			RemoveHandler ImageDisplay.FormClosing, AddressOf ImageDisplay_FormClosing
			RemoveHandler ImageDisplay.MediaFailedToLoad, AddressOf ImageDisplay_MediaFailedToLoad
			RemoveHandler ImageDisplay.MediaLoaded, AddressOf ImageDisplay_MediaLoaded
		End Sub

		''' <summary>
		''' Notification that the ImageDisplay form has loaded an Image
		''' </summary>
		''' <param name="MediaPath"></param>
		Private Sub ImageDisplay_MediaLoaded(ByVal MediaPath As String)
			ConsoleWindow.Log("Successfully loaded: " & MediaPath)
			ModificationTypes.EnableModifications()
			ModificationProperties.CurrentImage = CurrentImage
		End Sub

		''' <summary>
		''' Notification that the ImageDisplay form has failed to load an image
		''' </summary>
		''' <param name="ErrorMessage"></param>
		Private Sub ImageDisplay_MediaFailedToLoad(ByVal ErrorMessage As String)
			ConsoleWindow.Log("Failed to load Media " & ErrorMessage)
			ModificationTypes.DisableModifications()
			ModificationProperties.CurrentImage = CurrentImage
		End Sub
		#End Region

		#Region "Statistics Events"

		''' <summary>
		''' Log messages from the Statistics Application
		''' </summary>
		''' <param name="Message">
		''' String: Message sent from Statistics
		''' </param>
		Private Sub Statistics_StatisticsLog(ByVal Message As String)
			ConsoleWindow.Log(Message)
		End Sub

		#End Region

		#Region "FileLoader Events"

		''' <summary>
		''' Notification that the USer has asked for one of the Thumbnails displayed to be shown in the ImageDisplay form
		''' </summary>
		''' <param name="PathToMedia"></param>
		Private Sub FileLoader_ThumbnailSelected(ByVal PathToMedia As String)
			If CurrentImage <> PathToMedia Then
				ImageDisplay.ShowSingleMedia(PathToMedia)
				Histogram.DoHistogram(PathToMedia)
				Statistics.DoStatistics(PathToMedia)
				CurrentImage = PathToMedia
			End If

		End Sub
		#End Region

		#Region "Modification Type Events"

		''' <summary>
		''' Notification that the ImageDisplay should be updated
		''' </summary>
		''' <param name="modifiedImage"></param>
		Private Sub ModificationProperties_UpdateImage(ByVal modifiedImage As System.Drawing.Image)
			ImageDisplay.UpdateImage(modifiedImage)
		End Sub

		''' <summary>
		''' Notification the FAST Corner detection has been selected
		''' </summary>
		Private Sub ModificationTypes_FASTSelected()
			ModificationProperties.DetectorType = "FAST"
			ModificationProperties.CurrentImage = CurrentImage
			ModificationProperties.DoFast()
		End Sub

		''' <summary>
		''' Notification the Moravec Corner detection has been selected
		''' </summary>
		Private Sub ModificationTypes_MoravecSelected()
			ModificationProperties.DetectorType = "Moravec"
			ModificationProperties.CurrentImage = CurrentImage
			ModificationProperties.DoMoravec()
		End Sub

		''' <summary>
		''' Notification the Harris Corner detection has been selected
		''' </summary>
		Private Sub ModificationTypes_HarrisSelected()
			ModificationProperties.DetectorType = "Harris"
			ModificationProperties.CurrentImage = CurrentImage
			ModificationProperties.DoHarris()
		End Sub

	   ''' <summary>
		''' Notification the SUSAN Corner detection has been selected
	   ''' </summary>
		Private Sub ModificationTypes_SusanSelected()
			ModificationProperties.DetectorType = "SUSAN"
			ModificationProperties.CurrentImage = CurrentImage
			ModificationProperties.DoSusan()
		End Sub

		#End Region

		#Region "Modification Properties Events"

		''' <summary>
		''' Information to be shown to the EU sent for the ModificationProperties form
		''' </summary>
		''' <param name="Message"></param>
		Private Sub ModificationProperties_ModificationPropertiesLog(ByVal Message As String)
			ConsoleWindow.Log(Message)
		End Sub

		#End Region




	End Class
End Namespace
