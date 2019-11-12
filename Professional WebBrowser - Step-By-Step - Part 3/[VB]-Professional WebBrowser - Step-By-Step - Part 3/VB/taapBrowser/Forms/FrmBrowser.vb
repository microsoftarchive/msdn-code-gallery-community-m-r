Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Awesomium.Core
Imports WeifenLuo.WinFormsUI.Docking

Namespace taapBrowser.Forms
	Partial Public Class FrmBrowser
		Inherits DockContent

		#Region "Threading Delegates"
		Private Delegate Sub UpdateButtonImageDelegate()
		#End Region

		#Region "Settings"
		Private tabIconSize As Integer = 25
		Private home As String = "ccs-labs.com"
		#End Region

		''' <summary>
		''' Initialise the Browser Control
		''' </summary>
		Public Sub New()
			InitializeComponent()

			Me.AddressBar.Height = 30
			Me.LoadStyles() ' Load the AddressBar Styles
			Me.ResizeTheAddressBar()

			AddHandler Browser.DocumentReady, AddressOf Browser_DocumentReady
			AddHandler Browser.ShowPageInfo, AddressOf Browser_ShowPageInfo
		End Sub





		#Region "Styles"

		''' <summary>
		''' Load the AddressBar Styles
		''' </summary>
		Private Sub LoadStyles()
			Try

				LoadBackButton()

				LoadForwardButton()

				LoadStatusButton()

				LoadFavouritesButton()

				LoadRefreshButton()

				LoadSearchProviderButton()

				LoadSearchButton()

				LoadHomeButton()
			Catch e1 As Exception

				Throw
			End Try

		End Sub

		''' <summary>
		''' Load the User's Back button image or if none selected - the Default.
		''' </summary>
		Private Sub LoadBackButton()
			If AddressBar.InvokeRequired Then
				AddressBar.Invoke(New UpdateButtonImageDelegate(AddressOf LoadBackButton), Nothing)
			Else
				Me.btnBack.Image = ScaleImage(My.Resources.Alarm_Arrow_Left_icon, tabIconSize, tabIconSize)
			End If
		End Sub

		''' <summary>
		''' Load the User's Forward button image or if none selected - the Default.
		''' </summary>
		Private Sub LoadForwardButton()
			If AddressBar.InvokeRequired Then
				AddressBar.Invoke(New UpdateButtonImageDelegate(AddressOf LoadForwardButton), Nothing)
			Else
				Me.btnForward.Image = ScaleImage(My.Resources.Arrow_Right, tabIconSize, tabIconSize)
			End If
		End Sub

		''' <summary>
		''' Load the User's Security button image or if none selected - the Default.
		''' </summary>
		Private Sub LoadStatusButton()
			If AddressBar.InvokeRequired Then
				AddressBar.Invoke(New UpdateButtonImageDelegate(AddressOf LoadStatusButton), Nothing)
			Else
				Me.btnSecure.Image = ScaleImage(My.Resources.Green_check_box_with_check_mark_289x250, tabIconSize, tabIconSize)
			End If
		End Sub

		''' <summary>
		''' Load the User's Favourites button image or if none selected - the Default.
		''' </summary>
		Private Sub LoadFavouritesButton()
			If AddressBar.InvokeRequired Then
				AddressBar.Invoke(New UpdateButtonImageDelegate(AddressOf LoadFavouritesButton), Nothing)
			Else
				Me.btnFavourites.Image = ScaleImage(My.Resources.add_to_favourites, tabIconSize, tabIconSize)
			End If
		End Sub

		''' <summary>
		''' Load the User's reload button image or if none selected - the Default.
		''' </summary>
		Private Sub LoadRefreshButton()
			If AddressBar.InvokeRequired Then
				AddressBar.Invoke(New UpdateButtonImageDelegate(AddressOf LoadRefreshButton), Nothing)
			Else
				Me.btnReload.Image = ScaleImage(My.Resources.refresh, tabIconSize, tabIconSize)
			End If
		End Sub

		''' <summary>
		''' Load the User's Search provider button image or if none selected - the Default.
		''' </summary>
		Private Sub LoadSearchProviderButton()
			If AddressBar.InvokeRequired Then
				AddressBar.Invoke(New UpdateButtonImageDelegate(AddressOf LoadSearchProviderButton), Nothing)
			Else
				Me.btnSearchProvider.Image = ScaleImage(My.Resources.social_google_box_250x2501, tabIconSize, tabIconSize)
			End If
		End Sub

		''' <summary>
		''' Load the User's Search button image or if none selected - the Default.
		''' </summary>
		Private Sub LoadSearchButton()
			If AddressBar.InvokeRequired Then
				AddressBar.Invoke(New UpdateButtonImageDelegate(AddressOf LoadSearchButton), Nothing)
			Else
				Me.btnSearch.Image = ScaleImage(My.Resources.Search_Search_icon, tabIconSize, tabIconSize)
			End If
		End Sub

		''' <summary>
		''' Load the User's Home button image or if none selected - the Default.
		''' </summary>
		Private Sub LoadHomeButton()

			If AddressBar.InvokeRequired Then
				AddressBar.Invoke(New UpdateButtonImageDelegate(AddressOf LoadHomeButton), Nothing)
			Else
				Me.btnHome.Image = ScaleImage(My.Resources.homeenergy, tabIconSize, tabIconSize)
			End If
		End Sub

		#End Region

		#Region "Utilities"

		''' <summary>
		''' Allows the application to scale images so that they are suitable for the AddressBar
		''' </summary>
		''' <param name="image">
		''' Image: The Image to scale
		''' </param>
		''' <param name="maxWidth">
		''' Int: The Width to scale this image to.
		''' </param>
		''' <param name="maxHeight">
		''' Int: The Width to Scale this image to
		''' </param>
		''' <returns></returns>
		Private Shared Function ScaleImage(ByVal image As Image, ByVal maxWidth As Integer, ByVal maxHeight As Integer) As Image
			Dim ratioX = CDbl(maxWidth) / image.Width
			Dim ratioY = CDbl(maxHeight) / image.Height
			Dim ratio = Math.Min(ratioX, ratioY)

			Dim newWidth = CInt(image.Width * ratio)
			Dim newHeight = CInt(image.Height * ratio)

			Dim newImage = New Bitmap(newWidth, newHeight)
			Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight)
			Return newImage
		End Function

		''' <summary>
		''' Resizes the address bar so that the URL input area takes up as much room as possible horizontally.
		''' </summary>
		Private Sub ResizeTheAddressBar()
			Dim wid As Integer = Me.GetAddressBarControlsWidth()
			If Me.AddressBar.Size.Width - wid <> Me.tbAddressBox.Size.Width Then
				Me.tbAddressBox.Size = New Size((Me.AddressBar.Size.Width - (wid + 100)), Me.tbAddressBox.Size.Height)
			End If
		End Sub

		''' <summary>
		''' Calculates the current width of the AddressBar
		''' </summary>
		''' <returns>
		''' int: The current width of the AddressBar
		''' </returns>
		Private Function GetAddressBarControlsWidth() As Integer
			Dim width As Integer = 0

			Dim tsic As ToolStripItemCollection = AddressBar.Items

			For Each item As ToolStripItem In tsic
				If item.Name <> "tbAddressBox" Then
					width += item.Width
					width += item.Margin.Horizontal
				End If
			Next item
			Return width
		End Function

		#End Region

		#Region "Browser Events"

		''' <summary>
		''' Actions to run when the Browser Size Changes
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub FrmBrowser_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize

			Me.AddressBar.Height = tabIconSize
			Me.ResizeTheAddressBar()
			Me.AddressBar.Refresh()
		End Sub

		''' <summary>
		''' Actions to run when the Browser window is closing
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub FrmBrowser_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			Try
				Browser.Dispose()
			Catch e1 As Exception

				Throw
			End Try

		End Sub

		''' <summary>
		''' What should we do when a document has loaded?
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub Browser_DocumentReady(ByVal sender As Object, ByVal e As Awesomium.Core.UrlEventArgs)
			btnBack.Enabled = Browser.CanGoBack()
			btnForward.Enabled = Browser.CanGoForward()
		End Sub

		''' <summary>
		''' Show some basic page information.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub Browser_ShowPageInfo(ByVal sender As Object, ByVal e As Awesomium.Core.PageInfoEventArgs)
			Dim res As String = "Certificate Error: " & e.Info.CertError.ToString()
			res &= Environment.NewLine
			res &= e.Info.ContentStatus.ToString()
			res &= Environment.NewLine
			res &= e.Info.SecurityStatus.ToString()
			MessageBox.Show(res)

		End Sub

		#End Region

		#Region "AddressBar Events"

		''' <summary>
		''' Enable or disable the
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub BtnBackClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
			Browser.GoBack()
		End Sub

		''' <summary>
		''' Navigate forward in the browser history.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub BtnForwardClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnForward.Click
			Browser.CanGoForward()
		End Sub

		''' <summary>
		''' Fired when the Security button is clicked.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub BtnSecureClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSecure.Click
			Browser.RequestPageInfo()
		End Sub

		''' <summary>
		''' Redownload and display the page again
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub BtnReloadClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReload.Click
			Browser.Reload(True)
		End Sub

		''' <summary>
		''' Search for the User's string.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub TbSearchBoxKeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles tbSearchBox.KeyUp
			If e.KeyCode = Keys.Return OrElse e.KeyCode = Keys.Enter AndAlso (Not String.IsNullOrWhiteSpace(tbSearchBox.Text)) Then
				Browser.Source = New Uri("https://www.google.co.uk/#q=" & tbSearchBox.Text.Replace(" ", "+"))
			End If
		End Sub

		''' <summary>
		''' Search for the users string
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub BtnSearchClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
			If Not String.IsNullOrWhiteSpace(Me.tbSearchBox.Text) Then
				Browser.Source = New Uri("https://www.google.co.uk/#q=" & tbSearchBox.Text.Replace(" ", "+"))
			End If
		End Sub

		''' <summary>
		''' Reload the home page in this browser.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub BtnHomeClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnHome.Click
		  '  Browser.GoToHome(); not using this just now
			Browser.Source = New Uri(home)
		End Sub

		#End Region

	End Class
End Namespace
