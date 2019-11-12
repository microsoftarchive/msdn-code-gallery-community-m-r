Imports taapBrowser.Forms

Namespace taapBrowser.Forms
	Partial Public Class FrmBrowser
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(FrmBrowser))
			Me.toolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
			Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
			Me.toolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
			Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
			Me.toolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
			Me.Browser = New Awesomium.Windows.Forms.WebControl(Me.components)
			Me.AddressBar = New System.Windows.Forms.ToolStrip()
			Me.btnBack = New System.Windows.Forms.ToolStripButton()
			Me.btnForward = New System.Windows.Forms.ToolStripButton()
			Me.btnSecure = New System.Windows.Forms.ToolStripButton()
			Me.tbAddressBox = New Awesomium.Windows.Forms.ToolStripAddressBox()
			Me.btnFavourites = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
			Me.btnReload = New System.Windows.Forms.ToolStripButton()
			Me.btnSearchProvider = New System.Windows.Forms.ToolStripSplitButton()
			Me.tbSearchBox = New System.Windows.Forms.ToolStripTextBox()
			Me.btnSearch = New System.Windows.Forms.ToolStripButton()
			Me.btnHome = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
			Me.btnMenu = New System.Windows.Forms.ToolStripSplitButton()
			Me.webSession = New Awesomium.Windows.Forms.WebSessionProvider(Me.components)
			Me.toolStripContainer1.BottomToolStripPanel.SuspendLayout()
			Me.toolStripContainer1.ContentPanel.SuspendLayout()
			Me.toolStripContainer1.TopToolStripPanel.SuspendLayout()
			Me.toolStripContainer1.SuspendLayout()
			Me.statusStrip1.SuspendLayout()
			Me.AddressBar.SuspendLayout()
			Me.SuspendLayout()
			' 
			' toolStripContainer1
			' 
			' 
			' toolStripContainer1.BottomToolStripPanel
			' 
			Me.toolStripContainer1.BottomToolStripPanel.Controls.Add(Me.statusStrip1)
			' 
			' toolStripContainer1.ContentPanel
			' 
			Me.toolStripContainer1.ContentPanel.Controls.Add(Me.Browser)
			Me.toolStripContainer1.ContentPanel.Size = New System.Drawing.Size(1008, 573)
			Me.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.toolStripContainer1.Location = New System.Drawing.Point(0, 0)
			Me.toolStripContainer1.Name = "toolStripContainer1"
			Me.toolStripContainer1.Size = New System.Drawing.Size(1008, 620)
			Me.toolStripContainer1.TabIndex = 0
			Me.toolStripContainer1.Text = "toolStripContainer1"
			' 
			' toolStripContainer1.TopToolStripPanel
			' 
			Me.toolStripContainer1.TopToolStripPanel.Controls.Add(Me.AddressBar)
			' 
			' statusStrip1
			' 
			Me.statusStrip1.Dock = System.Windows.Forms.DockStyle.None
			Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.toolStripProgressBar1, Me.toolStripStatusLabel1, Me.toolStripStatusLabel2})
			Me.statusStrip1.Location = New System.Drawing.Point(0, 0)
			Me.statusStrip1.Name = "statusStrip1"
			Me.statusStrip1.Size = New System.Drawing.Size(1008, 22)
			Me.statusStrip1.TabIndex = 0
			' 
			' toolStripProgressBar1
			' 
			Me.toolStripProgressBar1.Name = "toolStripProgressBar1"
			Me.toolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
			' 
			' toolStripStatusLabel1
			' 
			Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
			Me.toolStripStatusLabel1.Size = New System.Drawing.Size(118, 17)
			Me.toolStripStatusLabel1.Text = "toolStripStatusLabel1"
			' 
			' toolStripStatusLabel2
			' 
			Me.toolStripStatusLabel2.Name = "toolStripStatusLabel2"
			Me.toolStripStatusLabel2.Size = New System.Drawing.Size(118, 17)
			Me.toolStripStatusLabel2.Text = "toolStripStatusLabel2"
			' 
			' Browser
			' 
			Me.Browser.Dock = System.Windows.Forms.DockStyle.Fill
			Me.Browser.Location = New System.Drawing.Point(0, 0)
			Me.Browser.Size = New System.Drawing.Size(1008, 573)
			Me.Browser.Source = New System.Uri("http://ccs-labs.com/", System.UriKind.Absolute)
			Me.Browser.TabIndex = 0
			' 
			' AddressBar
			' 
			Me.AddressBar.Dock = System.Windows.Forms.DockStyle.None
			Me.AddressBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.btnBack, Me.btnForward, Me.btnSecure, Me.tbAddressBox, Me.btnFavourites, Me.toolStripSeparator6, Me.btnReload, Me.btnSearchProvider, Me.tbSearchBox, Me.btnSearch, Me.btnHome, Me.toolStripSeparator7, Me.btnMenu})
			Me.AddressBar.Location = New System.Drawing.Point(3, 0)
			Me.AddressBar.Name = "AddressBar"
			Me.AddressBar.Size = New System.Drawing.Size(661, 25)
			Me.AddressBar.TabIndex = 1
			' 
			' btnBack
			' 
			Me.btnBack.AutoSize = False
			Me.btnBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnBack.Image = My.Resources.Alarm_Arrow_Left_icon
			Me.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnBack.Name = "btnBack"
			Me.btnBack.Size = New System.Drawing.Size(23, 22)
			Me.btnBack.Text = "&New"
			' 
			' btnForward
			' 
			Me.btnForward.AutoSize = False
			Me.btnForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnForward.Image = My.Resources.Arrow_Right
			Me.btnForward.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnForward.Name = "btnForward"
			Me.btnForward.Size = New System.Drawing.Size(23, 22)
			Me.btnForward.Text = "&Open"
			' 
			' btnSecure
			' 
			Me.btnSecure.AutoSize = False
			Me.btnSecure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnSecure.Image = My.Resources.Green_check_box_with_check_mark_289x250
			Me.btnSecure.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnSecure.Name = "btnSecure"
			Me.btnSecure.Size = New System.Drawing.Size(23, 22)
			Me.btnSecure.Text = "&Save"
			' 
			' tbAddressBox
			' 
			Me.tbAddressBox.AutoSize = False
			Me.tbAddressBox.Font = New System.Drawing.Font("Segoe UI", 9F)
			Me.tbAddressBox.Name = "tbAddressBox"
			Me.tbAddressBox.Size = New System.Drawing.Size(308, 25)
			Me.tbAddressBox.URL = Nothing
			Me.tbAddressBox.WebControl = Me.Browser
			' 
			' btnFavourites
			' 
			Me.btnFavourites.AutoSize = False
			Me.btnFavourites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnFavourites.Image = My.Resources.add_to_favourites
			Me.btnFavourites.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnFavourites.Name = "btnFavourites"
			Me.btnFavourites.Size = New System.Drawing.Size(23, 22)
			Me.btnFavourites.Text = "&Print"
			' 
			' toolStripSeparator6
			' 
			Me.toolStripSeparator6.Name = "toolStripSeparator6"
			Me.toolStripSeparator6.Size = New System.Drawing.Size(6, 25)
			' 
			' btnReload
			' 
			Me.btnReload.AutoSize = False
			Me.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnReload.Image = My.Resources.refresh
			Me.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnReload.Name = "btnReload"
			Me.btnReload.Size = New System.Drawing.Size(23, 22)
			Me.btnReload.Text = "C&ut"
			' 
			' btnSearchProvider
			' 
			Me.btnSearchProvider.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnSearchProvider.Image = My.Resources.social_google_box_250x2501
			Me.btnSearchProvider.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnSearchProvider.Name = "btnSearchProvider"
			Me.btnSearchProvider.Size = New System.Drawing.Size(32, 22)
			Me.btnSearchProvider.Text = "toolStripSplitButton1"
			' 
			' tbSearchBox
			' 
			Me.tbSearchBox.Name = "tbSearchBox"
			Me.tbSearchBox.Size = New System.Drawing.Size(100, 25)
			' 
			' btnSearch
			' 
			Me.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnSearch.Image = My.Resources.Search_Search_icon
			Me.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnSearch.Name = "btnSearch"
			Me.btnSearch.Size = New System.Drawing.Size(23, 22)
			Me.btnSearch.Text = "toolStripButton1"
			' 
			' btnHome
			' 
			Me.btnHome.AutoSize = False
			Me.btnHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnHome.Image = My.Resources.homeenergy
			Me.btnHome.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnHome.Name = "btnHome"
			Me.btnHome.Size = New System.Drawing.Size(23, 22)
			Me.btnHome.Text = "&Paste"
			' 
			' toolStripSeparator7
			' 
			Me.toolStripSeparator7.Name = "toolStripSeparator7"
			Me.toolStripSeparator7.Size = New System.Drawing.Size(6, 25)
			' 
			' btnMenu
			' 
			Me.btnMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnMenu.Image = My.Resources.menu
			Me.btnMenu.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnMenu.Name = "btnMenu"
			Me.btnMenu.Size = New System.Drawing.Size(32, 22)
			Me.btnMenu.Text = "toolStripSplitButton1"
			' 
			' webSession
			' 
			Me.webSession.Views.Add(Me.Browser)
			' 
			' FrmBrowser
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(1008, 620)
			Me.CloseButton = False
			Me.CloseButtonVisible = False
			Me.Controls.Add(Me.toolStripContainer1)
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.Icon = (DirectCast(resources.GetObject("$this.Icon"), System.Drawing.Icon))
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "FrmBrowser"
			Me.Text = "FrmBrowser"
			Me.toolStripContainer1.BottomToolStripPanel.ResumeLayout(False)
			Me.toolStripContainer1.BottomToolStripPanel.PerformLayout()
			Me.toolStripContainer1.ContentPanel.ResumeLayout(False)
			Me.toolStripContainer1.TopToolStripPanel.ResumeLayout(False)
			Me.toolStripContainer1.TopToolStripPanel.PerformLayout()
			Me.toolStripContainer1.ResumeLayout(False)
			Me.toolStripContainer1.PerformLayout()
			Me.statusStrip1.ResumeLayout(False)
			Me.statusStrip1.PerformLayout()
			Me.AddressBar.ResumeLayout(False)
			Me.AddressBar.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private toolStripContainer1 As System.Windows.Forms.ToolStripContainer
		Private statusStrip1 As System.Windows.Forms.StatusStrip
		Private toolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
		Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
		Private toolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
		Private Browser As Awesomium.Windows.Forms.WebControl
		Private AddressBar As System.Windows.Forms.ToolStrip
		Private btnBack As System.Windows.Forms.ToolStripButton
		Private btnForward As System.Windows.Forms.ToolStripButton
		Private btnSecure As System.Windows.Forms.ToolStripButton
		Private tbAddressBox As Awesomium.Windows.Forms.ToolStripAddressBox
		Private btnFavourites As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
		Private btnReload As System.Windows.Forms.ToolStripButton
		Private btnHome As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
		Private webSession As Awesomium.Windows.Forms.WebSessionProvider
		Private btnSearchProvider As System.Windows.Forms.ToolStripSplitButton
		Private tbSearchBox As System.Windows.Forms.ToolStripTextBox
		Private btnSearch As System.Windows.Forms.ToolStripButton
		Private btnMenu As System.Windows.Forms.ToolStripSplitButton

	End Class
End Namespace