Namespace ImageFunctions
	Partial Public Class Form1
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
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(Form1))
			Dim dockPanelSkin1 As New WeifenLuo.WinFormsUI.Docking.DockPanelSkin()
			Dim autoHideStripSkin1 As New WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin()
			Dim dockPanelGradient1 As New WeifenLuo.WinFormsUI.Docking.DockPanelGradient()
			Dim tabGradient1 As New WeifenLuo.WinFormsUI.Docking.TabGradient()
			Dim dockPaneStripSkin1 As New WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin()
			Dim dockPaneStripGradient1 As New WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient()
			Dim tabGradient2 As New WeifenLuo.WinFormsUI.Docking.TabGradient()
			Dim dockPanelGradient2 As New WeifenLuo.WinFormsUI.Docking.DockPanelGradient()
			Dim tabGradient3 As New WeifenLuo.WinFormsUI.Docking.TabGradient()
			Dim dockPaneStripToolWindowGradient1 As New WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient()
			Dim tabGradient4 As New WeifenLuo.WinFormsUI.Docking.TabGradient()
			Dim tabGradient5 As New WeifenLuo.WinFormsUI.Docking.TabGradient()
			Dim dockPanelGradient3 As New WeifenLuo.WinFormsUI.Docking.DockPanelGradient()
			Dim tabGradient6 As New WeifenLuo.WinFormsUI.Docking.TabGradient()
			Dim tabGradient7 As New WeifenLuo.WinFormsUI.Docking.TabGradient()
			Me.ilDefault = New System.Windows.Forms.ImageList(Me.components)
			Me.openFile = New System.Windows.Forms.OpenFileDialog()
			Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
			Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
			Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
			Me.printToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.printPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
			Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.editToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.undoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.redoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
			Me.cutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.copyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.pasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
			Me.selectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.customizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.optionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
			Me.WindowsDefaultMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.fileLoaderMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.histogramMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.imageDisplayMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.modificationPropertiesMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.modificationTypesMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.statisticsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.consoleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.commandInputToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.contentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.indexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.searchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
			Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
			Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
			Me.Progress = New System.Windows.Forms.ToolStripProgressBar()
			Me.toolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
			Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
			Me.DockingPanel = New WeifenLuo.WinFormsUI.Docking.DockPanel()
			Me.vS2012LightTheme1 = New WeifenLuo.WinFormsUI.Docking.VS2012LightTheme()
			Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
			Me.openfileToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.openToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.saveToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.printToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
			Me.cutToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.copyToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.pasteToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
			Me.btnSystemCodecInformation = New System.Windows.Forms.ToolStripButton()
			Me.StatisticsContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.rebuildToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.pbContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.resetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.GuiTimer = New System.Windows.Forms.Timer(Me.components)
			Me.menuStrip1.SuspendLayout()
			Me.toolStripContainer1.BottomToolStripPanel.SuspendLayout()
			Me.toolStripContainer1.ContentPanel.SuspendLayout()
			Me.toolStripContainer1.TopToolStripPanel.SuspendLayout()
			Me.toolStripContainer1.SuspendLayout()
			Me.statusStrip1.SuspendLayout()
			Me.toolStrip1.SuspendLayout()
			Me.StatisticsContextMenu.SuspendLayout()
			Me.pbContextMenu.SuspendLayout()
			Me.SuspendLayout()
			' 
			' ilDefault
			' 
			Me.ilDefault.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
			Me.ilDefault.ImageSize = New System.Drawing.Size(16, 16)
			Me.ilDefault.TransparentColor = System.Drawing.Color.Transparent
			' 
			' openFile
			' 
			Me.openFile.FileName = "openFileDialog1"
			Me.openFile.Multiselect = True
			Me.openFile.ReadOnlyChecked = True
			Me.openFile.RestoreDirectory = True
			Me.openFile.SupportMultiDottedExtensions = True
			Me.openFile.Title = "Load More Files"
			' 
			' menuStrip1
			' 
			Me.menuStrip1.Dock = System.Windows.Forms.DockStyle.None
			Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.fileToolStripMenuItem, Me.editToolStripMenuItem, Me.toolsToolStripMenuItem, Me.toolStripMenuItem1, Me.helpToolStripMenuItem})
			Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
			Me.menuStrip1.Name = "menuStrip1"
			Me.menuStrip1.Size = New System.Drawing.Size(935, 24)
			Me.menuStrip1.TabIndex = 0
			Me.menuStrip1.Text = "menuStrip1"
			' 
			' fileToolStripMenuItem
			' 
			Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.newToolStripMenuItem, Me.openToolStripMenuItem, Me.toolStripSeparator, Me.saveToolStripMenuItem, Me.saveAsToolStripMenuItem, Me.toolStripSeparator1, Me.printToolStripMenuItem, Me.printPreviewToolStripMenuItem, Me.toolStripSeparator2, Me.exitToolStripMenuItem})
			Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
			Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
			Me.fileToolStripMenuItem.Text = "&File"
			' 
			' newToolStripMenuItem
			' 
			Me.newToolStripMenuItem.Image = (DirectCast(resources.GetObject("newToolStripMenuItem.Image"), System.Drawing.Image))
			Me.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
			Me.newToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys))
			Me.newToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
			Me.newToolStripMenuItem.Text = "&New"
			' 
			' openToolStripMenuItem
			' 
			Me.openToolStripMenuItem.Image = (DirectCast(resources.GetObject("openToolStripMenuItem.Image"), System.Drawing.Image))
			Me.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
			Me.openToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys))
			Me.openToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
			Me.openToolStripMenuItem.Text = "&Open"
			' 
			' toolStripSeparator
			' 
			Me.toolStripSeparator.Name = "toolStripSeparator"
			Me.toolStripSeparator.Size = New System.Drawing.Size(143, 6)
			' 
			' saveToolStripMenuItem
			' 
			Me.saveToolStripMenuItem.Image = (DirectCast(resources.GetObject("saveToolStripMenuItem.Image"), System.Drawing.Image))
			Me.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
			Me.saveToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys))
			Me.saveToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
			Me.saveToolStripMenuItem.Text = "&Save"
			' 
			' saveAsToolStripMenuItem
			' 
			Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
			Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
			Me.saveAsToolStripMenuItem.Text = "Save &As"
			' 
			' toolStripSeparator1
			' 
			Me.toolStripSeparator1.Name = "toolStripSeparator1"
			Me.toolStripSeparator1.Size = New System.Drawing.Size(143, 6)
			' 
			' printToolStripMenuItem
			' 
			Me.printToolStripMenuItem.Image = (DirectCast(resources.GetObject("printToolStripMenuItem.Image"), System.Drawing.Image))
			Me.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.printToolStripMenuItem.Name = "printToolStripMenuItem"
			Me.printToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
			Me.printToolStripMenuItem.Text = "&Print"
			' 
			' printPreviewToolStripMenuItem
			' 
			Me.printPreviewToolStripMenuItem.Image = (DirectCast(resources.GetObject("printPreviewToolStripMenuItem.Image"), System.Drawing.Image))
			Me.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem"
			Me.printPreviewToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
			Me.printPreviewToolStripMenuItem.Text = "Print Pre&view"
			' 
			' toolStripSeparator2
			' 
			Me.toolStripSeparator2.Name = "toolStripSeparator2"
			Me.toolStripSeparator2.Size = New System.Drawing.Size(143, 6)
			' 
			' exitToolStripMenuItem
			' 
			Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
			Me.exitToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
			Me.exitToolStripMenuItem.Text = "E&xit"
			' 
			' editToolStripMenuItem
			' 
			Me.editToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.undoToolStripMenuItem, Me.redoToolStripMenuItem, Me.toolStripSeparator3, Me.cutToolStripMenuItem, Me.copyToolStripMenuItem, Me.pasteToolStripMenuItem, Me.toolStripSeparator4, Me.selectAllToolStripMenuItem})
			Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
			Me.editToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
			Me.editToolStripMenuItem.Text = "&Edit"
			' 
			' undoToolStripMenuItem
			' 
			Me.undoToolStripMenuItem.Name = "undoToolStripMenuItem"
			Me.undoToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys))
			Me.undoToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
			Me.undoToolStripMenuItem.Text = "&Undo"
			' 
			' redoToolStripMenuItem
			' 
			Me.redoToolStripMenuItem.Name = "redoToolStripMenuItem"
			Me.redoToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys))
			Me.redoToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
			Me.redoToolStripMenuItem.Text = "&Redo"
			' 
			' toolStripSeparator3
			' 
			Me.toolStripSeparator3.Name = "toolStripSeparator3"
			Me.toolStripSeparator3.Size = New System.Drawing.Size(141, 6)
			' 
			' cutToolStripMenuItem
			' 
			Me.cutToolStripMenuItem.Image = (DirectCast(resources.GetObject("cutToolStripMenuItem.Image"), System.Drawing.Image))
			Me.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.cutToolStripMenuItem.Name = "cutToolStripMenuItem"
			Me.cutToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
			Me.cutToolStripMenuItem.Text = "Cu&t"
			' 
			' copyToolStripMenuItem
			' 
			Me.copyToolStripMenuItem.Image = (DirectCast(resources.GetObject("copyToolStripMenuItem.Image"), System.Drawing.Image))
			Me.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.copyToolStripMenuItem.Name = "copyToolStripMenuItem"
			Me.copyToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
			Me.copyToolStripMenuItem.Text = "&Copy"
			' 
			' pasteToolStripMenuItem
			' 
			Me.pasteToolStripMenuItem.Image = (DirectCast(resources.GetObject("pasteToolStripMenuItem.Image"), System.Drawing.Image))
			Me.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem"
			Me.pasteToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
			Me.pasteToolStripMenuItem.Text = "&Paste"
			' 
			' toolStripSeparator4
			' 
			Me.toolStripSeparator4.Name = "toolStripSeparator4"
			Me.toolStripSeparator4.Size = New System.Drawing.Size(141, 6)
			' 
			' selectAllToolStripMenuItem
			' 
			Me.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem"
			Me.selectAllToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
			Me.selectAllToolStripMenuItem.Text = "Select &All"
			' 
			' toolsToolStripMenuItem
			' 
			Me.toolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.customizeToolStripMenuItem, Me.optionsToolStripMenuItem})
			Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
			Me.toolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
			Me.toolsToolStripMenuItem.Text = "&Tools"
			' 
			' customizeToolStripMenuItem
			' 
			Me.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem"
			Me.customizeToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
			Me.customizeToolStripMenuItem.Text = "&Customize"
			' 
			' optionsToolStripMenuItem
			' 
			Me.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem"
			Me.optionsToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
			Me.optionsToolStripMenuItem.Text = "&Options"
			' 
			' toolStripMenuItem1
			' 
			Me.toolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.WindowsDefaultMenuItem})
			Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
			Me.toolStripMenuItem1.Size = New System.Drawing.Size(68, 20)
			Me.toolStripMenuItem1.Text = "Windows"
			' 
			' WindowsDefaultMenuItem
			' 
			Me.WindowsDefaultMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.fileLoaderMenuItem, Me.histogramMenuItem, Me.imageDisplayMenuItem, Me.modificationPropertiesMenuItem, Me.modificationTypesMenuItem, Me.statisticsMenuItem, Me.consoleMenuItem, Me.commandInputToolStripMenuItem})
			Me.WindowsDefaultMenuItem.Name = "WindowsDefaultMenuItem"
			Me.WindowsDefaultMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.WindowsDefaultMenuItem.Text = "Default"
			' 
			' fileLoaderMenuItem
			' 
			Me.fileLoaderMenuItem.Name = "fileLoaderMenuItem"
			Me.fileLoaderMenuItem.Size = New System.Drawing.Size(198, 22)
			Me.fileLoaderMenuItem.Text = "File Loader"
			' 
			' histogramMenuItem
			' 
			Me.histogramMenuItem.Name = "histogramMenuItem"
			Me.histogramMenuItem.Size = New System.Drawing.Size(198, 22)
			Me.histogramMenuItem.Text = "Histogram"
'			Me.histogramMenuItem.Click += New System.EventHandler(Me.histogramMenuItem_Click)
			' 
			' imageDisplayMenuItem
			' 
			Me.imageDisplayMenuItem.Name = "imageDisplayMenuItem"
			Me.imageDisplayMenuItem.Size = New System.Drawing.Size(198, 22)
			Me.imageDisplayMenuItem.Text = "Image Display"
'			Me.imageDisplayMenuItem.Click += New System.EventHandler(Me.imageDisplayMenuItem_Click)
			' 
			' modificationPropertiesMenuItem
			' 
			Me.modificationPropertiesMenuItem.Name = "modificationPropertiesMenuItem"
			Me.modificationPropertiesMenuItem.Size = New System.Drawing.Size(198, 22)
			Me.modificationPropertiesMenuItem.Text = "Modification Properties"
'			Me.modificationPropertiesMenuItem.Click += New System.EventHandler(Me.modificationPropertiesMenuItem_Click)
			' 
			' modificationTypesMenuItem
			' 
			Me.modificationTypesMenuItem.Name = "modificationTypesMenuItem"
			Me.modificationTypesMenuItem.Size = New System.Drawing.Size(198, 22)
			Me.modificationTypesMenuItem.Text = "Modification Types"
'			Me.modificationTypesMenuItem.Click += New System.EventHandler(Me.modificationTypesMenuItem_Click)
			' 
			' statisticsMenuItem
			' 
			Me.statisticsMenuItem.Name = "statisticsMenuItem"
			Me.statisticsMenuItem.Size = New System.Drawing.Size(198, 22)
			Me.statisticsMenuItem.Text = "Statistics"
'			Me.statisticsMenuItem.Click += New System.EventHandler(Me.statisticsMenuItem_Click)
			' 
			' consoleMenuItem
			' 
			Me.consoleMenuItem.Name = "consoleMenuItem"
			Me.consoleMenuItem.Size = New System.Drawing.Size(198, 22)
			Me.consoleMenuItem.Text = "Console"
'			Me.consoleMenuItem.Click += New System.EventHandler(Me.consoleMenuItem_Click)
			' 
			' commandInputToolStripMenuItem
			' 
			Me.commandInputToolStripMenuItem.Name = "commandInputToolStripMenuItem"
			Me.commandInputToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
			Me.commandInputToolStripMenuItem.Text = "Command Input"
'			Me.commandInputToolStripMenuItem.Click += New System.EventHandler(Me.commandInputToolStripMenuItem_Click)
			' 
			' helpToolStripMenuItem
			' 
			Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.contentsToolStripMenuItem, Me.indexToolStripMenuItem, Me.searchToolStripMenuItem, Me.toolStripSeparator5, Me.aboutToolStripMenuItem})
			Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
			Me.helpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
			Me.helpToolStripMenuItem.Text = "&Help"
			' 
			' contentsToolStripMenuItem
			' 
			Me.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem"
			Me.contentsToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
			Me.contentsToolStripMenuItem.Text = "&Contents"
			' 
			' indexToolStripMenuItem
			' 
			Me.indexToolStripMenuItem.Name = "indexToolStripMenuItem"
			Me.indexToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
			Me.indexToolStripMenuItem.Text = "&Index"
			' 
			' searchToolStripMenuItem
			' 
			Me.searchToolStripMenuItem.Name = "searchToolStripMenuItem"
			Me.searchToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
			Me.searchToolStripMenuItem.Text = "&Search"
			' 
			' toolStripSeparator5
			' 
			Me.toolStripSeparator5.Name = "toolStripSeparator5"
			Me.toolStripSeparator5.Size = New System.Drawing.Size(119, 6)
			' 
			' aboutToolStripMenuItem
			' 
			Me.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem"
			Me.aboutToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
			Me.aboutToolStripMenuItem.Text = "&About..."
'			Me.aboutToolStripMenuItem.Click += New System.EventHandler(Me.aboutToolStripMenuItem_Click)
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
			Me.toolStripContainer1.ContentPanel.Controls.Add(Me.DockingPanel)
			Me.toolStripContainer1.ContentPanel.Size = New System.Drawing.Size(935, 625)
			Me.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.toolStripContainer1.Location = New System.Drawing.Point(0, 0)
			Me.toolStripContainer1.Name = "toolStripContainer1"
			Me.toolStripContainer1.Size = New System.Drawing.Size(935, 696)
			Me.toolStripContainer1.TabIndex = 1
			Me.toolStripContainer1.Text = "toolStripContainer1"
			' 
			' toolStripContainer1.TopToolStripPanel
			' 
			Me.toolStripContainer1.TopToolStripPanel.Controls.Add(Me.menuStrip1)
			Me.toolStripContainer1.TopToolStripPanel.Controls.Add(Me.toolStrip1)
			' 
			' statusStrip1
			' 
			Me.statusStrip1.Dock = System.Windows.Forms.DockStyle.None
			Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.Progress, Me.toolStripStatusLabel1, Me.lblStatus})
			Me.statusStrip1.Location = New System.Drawing.Point(0, 0)
			Me.statusStrip1.Name = "statusStrip1"
			Me.statusStrip1.Size = New System.Drawing.Size(935, 22)
			Me.statusStrip1.TabIndex = 0
			' 
			' Progress
			' 
			Me.Progress.Name = "Progress"
			Me.Progress.Size = New System.Drawing.Size(100, 16)
			Me.Progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
			' 
			' toolStripStatusLabel1
			' 
			Me.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control
			Me.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
			Me.toolStripStatusLabel1.Name = "toolStripStatusLabel1"
			Me.toolStripStatusLabel1.Size = New System.Drawing.Size(42, 17)
			Me.toolStripStatusLabel1.Text = "Status:"
			' 
			' lblStatus
			' 
			Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
			Me.lblStatus.Name = "lblStatus"
			Me.lblStatus.Size = New System.Drawing.Size(776, 17)
			Me.lblStatus.Spring = True
			Me.lblStatus.Text = "..."
			Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' DockingPanel
			' 
			Me.DockingPanel.Dock = System.Windows.Forms.DockStyle.Fill
			Me.DockingPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow
			Me.DockingPanel.Location = New System.Drawing.Point(0, 0)
			Me.DockingPanel.Name = "DockingPanel"
			Me.DockingPanel.Size = New System.Drawing.Size(935, 625)
			dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight
			dockPanelGradient1.StartColor = System.Drawing.Color.FromArgb((CInt((CByte(0)))), (CInt((CByte(122)))), (CInt((CByte(204)))))
			autoHideStripSkin1.DockStripGradient = dockPanelGradient1
			tabGradient1.EndColor = System.Drawing.SystemColors.Control
			tabGradient1.StartColor = System.Drawing.SystemColors.Control
			tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark
			autoHideStripSkin1.TabGradient = tabGradient1
			autoHideStripSkin1.TextFont = New System.Drawing.Font("Segoe UI", 9F)
			dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1
			tabGradient2.EndColor = System.Drawing.Color.FromArgb((CInt((CByte(204)))), (CInt((CByte(206)))), (CInt((CByte(219)))))
			tabGradient2.StartColor = System.Drawing.Color.FromArgb((CInt((CByte(0)))), (CInt((CByte(122)))), (CInt((CByte(204)))))
			tabGradient2.TextColor = System.Drawing.Color.White
			dockPaneStripGradient1.ActiveTabGradient = tabGradient2
			dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control
			dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control
			dockPaneStripGradient1.DockStripGradient = dockPanelGradient2
			tabGradient3.EndColor = System.Drawing.Color.FromArgb((CInt((CByte(28)))), (CInt((CByte(151)))), (CInt((CByte(234)))))
			tabGradient3.StartColor = System.Drawing.SystemColors.Control
			tabGradient3.TextColor = System.Drawing.Color.Black
			dockPaneStripGradient1.InactiveTabGradient = tabGradient3
			dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1
			dockPaneStripSkin1.TextFont = New System.Drawing.Font("Segoe UI", 9F)
			tabGradient4.EndColor = System.Drawing.Color.FromArgb((CInt((CByte(80)))), (CInt((CByte(170)))), (CInt((CByte(220)))))
			tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
			tabGradient4.StartColor = System.Drawing.Color.FromArgb((CInt((CByte(0)))), (CInt((CByte(122)))), (CInt((CByte(204)))))
			tabGradient4.TextColor = System.Drawing.Color.White
			dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4
			tabGradient5.EndColor = System.Drawing.SystemColors.ControlLightLight
			tabGradient5.StartColor = System.Drawing.SystemColors.ControlLightLight
			tabGradient5.TextColor = System.Drawing.Color.FromArgb((CInt((CByte(0)))), (CInt((CByte(122)))), (CInt((CByte(204)))))
			dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5
			dockPanelGradient3.EndColor = System.Drawing.SystemColors.Control
			dockPanelGradient3.StartColor = System.Drawing.SystemColors.Control
			dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3
			tabGradient6.EndColor = System.Drawing.SystemColors.ControlDark
			tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
			tabGradient6.StartColor = System.Drawing.SystemColors.Control
			tabGradient6.TextColor = System.Drawing.SystemColors.GrayText
			dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6
			tabGradient7.EndColor = System.Drawing.SystemColors.Control
			tabGradient7.StartColor = System.Drawing.SystemColors.Control
			tabGradient7.TextColor = System.Drawing.SystemColors.GrayText
			dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7
			dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1
			dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1
			Me.DockingPanel.Skin = dockPanelSkin1
			Me.DockingPanel.SupportDeeplyNestedContent = True
			Me.DockingPanel.TabIndex = 0
			Me.DockingPanel.Theme = Me.vS2012LightTheme1
			' 
			' toolStrip1
			' 
			Me.toolStrip1.Dock = System.Windows.Forms.DockStyle.None
			Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.openfileToolStripButton, Me.openToolStripButton, Me.saveToolStripButton, Me.printToolStripButton, Me.toolStripSeparator6, Me.cutToolStripButton, Me.copyToolStripButton, Me.pasteToolStripButton, Me.toolStripSeparator7, Me.btnSystemCodecInformation})
			Me.toolStrip1.Location = New System.Drawing.Point(3, 24)
			Me.toolStrip1.Name = "toolStrip1"
			Me.toolStrip1.Size = New System.Drawing.Size(208, 25)
			Me.toolStrip1.TabIndex = 1
			' 
			' openfileToolStripButton
			' 
			Me.openfileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.openfileToolStripButton.Image = (DirectCast(resources.GetObject("openfileToolStripButton.Image"), System.Drawing.Image))
			Me.openfileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.openfileToolStripButton.Name = "openfileToolStripButton"
			Me.openfileToolStripButton.Size = New System.Drawing.Size(23, 22)
			Me.openfileToolStripButton.Text = "Open &File"
'			Me.openfileToolStripButton.Click += New System.EventHandler(Me.openfileToolStripButton_Click)
			' 
			' openToolStripButton
			' 
			Me.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.openToolStripButton.Image = (DirectCast(resources.GetObject("openToolStripButton.Image"), System.Drawing.Image))
			Me.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.openToolStripButton.Name = "openToolStripButton"
			Me.openToolStripButton.Size = New System.Drawing.Size(23, 22)
			Me.openToolStripButton.Text = "&Open"
			' 
			' saveToolStripButton
			' 
			Me.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.saveToolStripButton.Image = (DirectCast(resources.GetObject("saveToolStripButton.Image"), System.Drawing.Image))
			Me.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.saveToolStripButton.Name = "saveToolStripButton"
			Me.saveToolStripButton.Size = New System.Drawing.Size(23, 22)
			Me.saveToolStripButton.Text = "&Save"
			' 
			' printToolStripButton
			' 
			Me.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.printToolStripButton.Image = (DirectCast(resources.GetObject("printToolStripButton.Image"), System.Drawing.Image))
			Me.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.printToolStripButton.Name = "printToolStripButton"
			Me.printToolStripButton.Size = New System.Drawing.Size(23, 22)
			Me.printToolStripButton.Text = "&Print"
			' 
			' toolStripSeparator6
			' 
			Me.toolStripSeparator6.Name = "toolStripSeparator6"
			Me.toolStripSeparator6.Size = New System.Drawing.Size(6, 25)
			' 
			' cutToolStripButton
			' 
			Me.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.cutToolStripButton.Image = (DirectCast(resources.GetObject("cutToolStripButton.Image"), System.Drawing.Image))
			Me.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.cutToolStripButton.Name = "cutToolStripButton"
			Me.cutToolStripButton.Size = New System.Drawing.Size(23, 22)
			Me.cutToolStripButton.Text = "C&ut"
			' 
			' copyToolStripButton
			' 
			Me.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.copyToolStripButton.Image = (DirectCast(resources.GetObject("copyToolStripButton.Image"), System.Drawing.Image))
			Me.copyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.copyToolStripButton.Name = "copyToolStripButton"
			Me.copyToolStripButton.Size = New System.Drawing.Size(23, 22)
			Me.copyToolStripButton.Text = "&Copy"
			' 
			' pasteToolStripButton
			' 
			Me.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.pasteToolStripButton.Image = (DirectCast(resources.GetObject("pasteToolStripButton.Image"), System.Drawing.Image))
			Me.pasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.pasteToolStripButton.Name = "pasteToolStripButton"
			Me.pasteToolStripButton.Size = New System.Drawing.Size(23, 22)
			Me.pasteToolStripButton.Text = "&Paste"
			' 
			' toolStripSeparator7
			' 
			Me.toolStripSeparator7.Name = "toolStripSeparator7"
			Me.toolStripSeparator7.Size = New System.Drawing.Size(6, 25)
			' 
			' btnSystemCodecInformation
			' 
			Me.btnSystemCodecInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.btnSystemCodecInformation.Image = My.Resources.codec
			Me.btnSystemCodecInformation.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.btnSystemCodecInformation.Name = "btnSystemCodecInformation"
			Me.btnSystemCodecInformation.Size = New System.Drawing.Size(23, 22)
			Me.btnSystemCodecInformation.ToolTipText = "Codecs Information"
'			Me.btnSystemCodecInformation.Click += New System.EventHandler(Me.btnSystemCodecInformation_Click_1)
			' 
			' StatisticsContextMenu
			' 
			Me.StatisticsContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.rebuildToolStripMenuItem})
			Me.StatisticsContextMenu.Name = "StatisticsContextMenu"
			Me.StatisticsContextMenu.Size = New System.Drawing.Size(115, 26)
			' 
			' rebuildToolStripMenuItem
			' 
			Me.rebuildToolStripMenuItem.Name = "rebuildToolStripMenuItem"
			Me.rebuildToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
			Me.rebuildToolStripMenuItem.Text = "Rebuild"
			' 
			' pbContextMenu
			' 
			Me.pbContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.resetToolStripMenuItem})
			Me.pbContextMenu.Name = "pbContextMenu"
			Me.pbContextMenu.Size = New System.Drawing.Size(103, 26)
			' 
			' resetToolStripMenuItem
			' 
			Me.resetToolStripMenuItem.Name = "resetToolStripMenuItem"
			Me.resetToolStripMenuItem.Size = New System.Drawing.Size(102, 22)
			Me.resetToolStripMenuItem.Text = "Reset"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
			Me.ClientSize = New System.Drawing.Size(935, 696)
			Me.Controls.Add(Me.toolStripContainer1)
			Me.Icon = (DirectCast(resources.GetObject("$this.Icon"), System.Drawing.Icon))
			Me.IsMdiContainer = True
			Me.MainMenuStrip = Me.menuStrip1
			Me.Name = "Form1"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "Image Functions"
			Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.Form1_FormClosing)
'			Me.Load += New System.EventHandler(Me.Form1_Load)
			Me.menuStrip1.ResumeLayout(False)
			Me.menuStrip1.PerformLayout()
			Me.toolStripContainer1.BottomToolStripPanel.ResumeLayout(False)
			Me.toolStripContainer1.BottomToolStripPanel.PerformLayout()
			Me.toolStripContainer1.ContentPanel.ResumeLayout(False)
			Me.toolStripContainer1.TopToolStripPanel.ResumeLayout(False)
			Me.toolStripContainer1.TopToolStripPanel.PerformLayout()
			Me.toolStripContainer1.ResumeLayout(False)
			Me.toolStripContainer1.PerformLayout()
			Me.statusStrip1.ResumeLayout(False)
			Me.statusStrip1.PerformLayout()
			Me.toolStrip1.ResumeLayout(False)
			Me.toolStrip1.PerformLayout()
			Me.StatisticsContextMenu.ResumeLayout(False)
			Me.pbContextMenu.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private ilDefault As System.Windows.Forms.ImageList
		Private openFile As System.Windows.Forms.OpenFileDialog
		Private menuStrip1 As System.Windows.Forms.MenuStrip
		Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator As System.Windows.Forms.ToolStripSeparator
		Private saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private printToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private printPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
		Private exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private editToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private undoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private redoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
		Private cutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private copyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private pasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
		Private selectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private customizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private optionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private contentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private indexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private searchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripContainer1 As System.Windows.Forms.ToolStripContainer
		Private statusStrip1 As System.Windows.Forms.StatusStrip
		Private Progress As System.Windows.Forms.ToolStripProgressBar
		Private toolStrip1 As System.Windows.Forms.ToolStrip
		Private openToolStripButton As System.Windows.Forms.ToolStripButton
		Private saveToolStripButton As System.Windows.Forms.ToolStripButton
		Private printToolStripButton As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
		Private cutToolStripButton As System.Windows.Forms.ToolStripButton
		Private copyToolStripButton As System.Windows.Forms.ToolStripButton
		Private pasteToolStripButton As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents btnSystemCodecInformation As System.Windows.Forms.ToolStripButton
		Private pbContextMenu As System.Windows.Forms.ContextMenuStrip
		Private resetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
		Private lblStatus As System.Windows.Forms.ToolStripStatusLabel
		Private GuiTimer As System.Windows.Forms.Timer
		Private StatisticsContextMenu As System.Windows.Forms.ContextMenuStrip
		Private rebuildToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private DockingPanel As WeifenLuo.WinFormsUI.Docking.DockPanel
		Private vS2012LightTheme1 As WeifenLuo.WinFormsUI.Docking.VS2012LightTheme
		Private toolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private WindowsDefaultMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private fileLoaderMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents histogramMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents imageDisplayMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents modificationPropertiesMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents modificationTypesMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents statisticsMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents openfileToolStripButton As System.Windows.Forms.ToolStripButton
		Private WithEvents consoleMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents commandInputToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	End Class
End Namespace

