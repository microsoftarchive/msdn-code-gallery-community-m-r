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
			Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
			Me.splitContainer2 = New System.Windows.Forms.SplitContainer()
			Me.splitContainer3 = New System.Windows.Forms.SplitContainer()
			Me.btnLoadNewImage = New System.Windows.Forms.Button()
			Me.label1 = New System.Windows.Forms.Label()
			Me.lbImageList = New System.Windows.Forms.ListBox()
			Me.ControlPanel = New System.Windows.Forms.Panel()
			Me.btnProcess = New System.Windows.Forms.Button()
			Me.btnReset = New System.Windows.Forms.Button()
			Me.label3 = New System.Windows.Forms.Label()
			Me.lbMethods = New System.Windows.Forms.ListBox()
			Me.lbModification = New System.Windows.Forms.ListBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.pbImage = New Cyotek.Windows.Forms.ImageBox()
			Me.pbContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.resetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.rtbConOut = New System.Windows.Forms.RichTextBox()
			Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
			Me.newToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.openToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.saveToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.printToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
			Me.cutToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.copyToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.pasteToolStripButton = New System.Windows.Forms.ToolStripButton()
			Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
			Me.btnSystemCodecInformation = New System.Windows.Forms.ToolStripButton()
			Me.GuiTimer = New System.Windows.Forms.Timer(Me.components)
			Me.menuStrip1.SuspendLayout()
			Me.toolStripContainer1.BottomToolStripPanel.SuspendLayout()
			Me.toolStripContainer1.ContentPanel.SuspendLayout()
			Me.toolStripContainer1.TopToolStripPanel.SuspendLayout()
			Me.toolStripContainer1.SuspendLayout()
			Me.statusStrip1.SuspendLayout()
			DirectCast(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.splitContainer1.Panel1.SuspendLayout()
			Me.splitContainer1.Panel2.SuspendLayout()
			Me.splitContainer1.SuspendLayout()
			DirectCast(Me.splitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.splitContainer2.Panel1.SuspendLayout()
			Me.splitContainer2.Panel2.SuspendLayout()
			Me.splitContainer2.SuspendLayout()
			DirectCast(Me.splitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.splitContainer3.Panel1.SuspendLayout()
			Me.splitContainer3.Panel2.SuspendLayout()
			Me.splitContainer3.SuspendLayout()
			Me.pbContextMenu.SuspendLayout()
			Me.toolStrip1.SuspendLayout()
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
			Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.fileToolStripMenuItem, Me.editToolStripMenuItem, Me.toolsToolStripMenuItem, Me.helpToolStripMenuItem})
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
			Me.toolStripContainer1.ContentPanel.Controls.Add(Me.splitContainer1)
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
			' splitContainer1
			' 
			Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer1.Name = "splitContainer1"
			Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
			' 
			' splitContainer1.Panel1
			' 
			Me.splitContainer1.Panel1.Controls.Add(Me.splitContainer2)
			' 
			' splitContainer1.Panel2
			' 
			Me.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.AppWorkspace
			Me.splitContainer1.Panel2.Controls.Add(Me.rtbConOut)
			Me.splitContainer1.Size = New System.Drawing.Size(935, 625)
			Me.splitContainer1.SplitterDistance = 483
			Me.splitContainer1.TabIndex = 0
			' 
			' splitContainer2
			' 
			Me.splitContainer2.BackColor = System.Drawing.SystemColors.ActiveBorder
			Me.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
			Me.splitContainer2.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer2.Name = "splitContainer2"
			' 
			' splitContainer2.Panel1
			' 
			Me.splitContainer2.Panel1.Controls.Add(Me.splitContainer3)
			' 
			' splitContainer2.Panel2
			' 
			Me.splitContainer2.Panel2.AutoScroll = True
			Me.splitContainer2.Panel2.Controls.Add(Me.pbImage)
			Me.splitContainer2.Size = New System.Drawing.Size(935, 483)
			Me.splitContainer2.SplitterDistance = 435
			Me.splitContainer2.TabIndex = 10
			' 
			' splitContainer3
			' 
			Me.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
			Me.splitContainer3.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer3.Name = "splitContainer3"
			Me.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
			' 
			' splitContainer3.Panel1
			' 
			Me.splitContainer3.Panel1.Controls.Add(Me.btnLoadNewImage)
			Me.splitContainer3.Panel1.Controls.Add(Me.label1)
			Me.splitContainer3.Panel1.Controls.Add(Me.lbImageList)
			' 
			' splitContainer3.Panel2
			' 
			Me.splitContainer3.Panel2.Controls.Add(Me.ControlPanel)
			Me.splitContainer3.Panel2.Controls.Add(Me.btnProcess)
			Me.splitContainer3.Panel2.Controls.Add(Me.btnReset)
			Me.splitContainer3.Panel2.Controls.Add(Me.label3)
			Me.splitContainer3.Panel2.Controls.Add(Me.lbMethods)
			Me.splitContainer3.Panel2.Controls.Add(Me.lbModification)
			Me.splitContainer3.Panel2.Controls.Add(Me.label2)
			Me.splitContainer3.Size = New System.Drawing.Size(433, 481)
			Me.splitContainer3.SplitterDistance = 192
			Me.splitContainer3.TabIndex = 0
			' 
			' btnLoadNewImage
			' 
			Me.btnLoadNewImage.Location = New System.Drawing.Point(392, 9)
			Me.btnLoadNewImage.Name = "btnLoadNewImage"
			Me.btnLoadNewImage.Size = New System.Drawing.Size(25, 23)
			Me.btnLoadNewImage.TabIndex = 22
			Me.btnLoadNewImage.Text = "..."
			Me.btnLoadNewImage.UseVisualStyleBackColor = True
'			Me.btnLoadNewImage.Click += New System.EventHandler(Me.btnLoadNewImage_Click_1)
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(15, 14)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(66, 13)
			Me.label1.TabIndex = 21
			Me.label1.Text = "Load Image:"
			' 
			' lbImageList
			' 
			Me.lbImageList.FormattingEnabled = True
			Me.lbImageList.Location = New System.Drawing.Point(83, 12)
			Me.lbImageList.Name = "lbImageList"
			Me.lbImageList.Size = New System.Drawing.Size(303, 17)
			Me.lbImageList.TabIndex = 20
'			Me.lbImageList.SelectedIndexChanged += New System.EventHandler(Me.listBox1_SelectedIndexChanged_1)
			' 
			' ControlPanel
			' 
			Me.ControlPanel.Location = New System.Drawing.Point(3, 58)
			Me.ControlPanel.Name = "ControlPanel"
			Me.ControlPanel.Size = New System.Drawing.Size(427, 185)
			Me.ControlPanel.TabIndex = 31
			' 
			' btnProcess
			' 
			Me.btnProcess.Location = New System.Drawing.Point(342, 249)
			Me.btnProcess.Name = "btnProcess"
			Me.btnProcess.Size = New System.Drawing.Size(75, 23)
			Me.btnProcess.TabIndex = 30
			Me.btnProcess.Text = "Process"
			Me.btnProcess.UseVisualStyleBackColor = True
'			Me.btnProcess.Click += New System.EventHandler(Me.btnProcess_Click)
			' 
			' btnReset
			' 
			Me.btnReset.Location = New System.Drawing.Point(11, 249)
			Me.btnReset.Name = "btnReset"
			Me.btnReset.Size = New System.Drawing.Size(75, 23)
			Me.btnReset.TabIndex = 29
			Me.btnReset.Text = "Reset"
			Me.btnReset.UseVisualStyleBackColor = True
'			Me.btnReset.Click += New System.EventHandler(Me.btnReset_Click)
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(39, 39)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(43, 13)
			Me.label3.TabIndex = 27
			Me.label3.Text = "Method"
			' 
			' lbMethods
			' 
			Me.lbMethods.Enabled = False
			Me.lbMethods.FormattingEnabled = True
			Me.lbMethods.Items.AddRange(New Object() { "None"})
			Me.lbMethods.Location = New System.Drawing.Point(83, 35)
			Me.lbMethods.Name = "lbMethods"
			Me.lbMethods.Size = New System.Drawing.Size(303, 17)
			Me.lbMethods.TabIndex = 26
'			Me.lbMethods.SelectedIndexChanged += New System.EventHandler(Me.lbMethods_SelectedIndexChanged)
			' 
			' lbModification
			' 
			Me.lbModification.Enabled = False
			Me.lbModification.FormattingEnabled = True
			Me.lbModification.Items.AddRange(New Object() { "None", "Corner Detection"})
			Me.lbModification.Location = New System.Drawing.Point(83, 12)
			Me.lbModification.Name = "lbModification"
			Me.lbModification.Size = New System.Drawing.Size(303, 17)
			Me.lbModification.TabIndex = 23
'			Me.lbModification.SelectedIndexChanged += New System.EventHandler(Me.lbModification_SelectedIndexChanged)
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(39, 16)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(38, 13)
			Me.label2.TabIndex = 24
			Me.label2.Text = "Modify"
			' 
			' pbImage
			' 
			Me.pbImage.BackColor = System.Drawing.Color.DimGray
			Me.pbImage.ContextMenuStrip = Me.pbContextMenu
			Me.pbImage.Dock = System.Windows.Forms.DockStyle.Fill
			Me.pbImage.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Image
			Me.pbImage.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None
			Me.pbImage.Location = New System.Drawing.Point(0, 0)
			Me.pbImage.Name = "pbImage"
			Me.pbImage.Size = New System.Drawing.Size(494, 481)
			Me.pbImage.TabIndex = 0
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
'			Me.resetToolStripMenuItem.Click += New System.EventHandler(Me.resetToolStripMenuItem_Click)
			' 
			' rtbConOut
			' 
			Me.rtbConOut.BackColor = System.Drawing.Color.Black
			Me.rtbConOut.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.rtbConOut.Dock = System.Windows.Forms.DockStyle.Fill
			Me.rtbConOut.ForeColor = System.Drawing.SystemColors.Info
			Me.rtbConOut.Location = New System.Drawing.Point(0, 0)
			Me.rtbConOut.Name = "rtbConOut"
			Me.rtbConOut.ShowSelectionMargin = True
			Me.rtbConOut.Size = New System.Drawing.Size(935, 138)
			Me.rtbConOut.TabIndex = 0
			Me.rtbConOut.Text = ""
			' 
			' toolStrip1
			' 
			Me.toolStrip1.Dock = System.Windows.Forms.DockStyle.None
			Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.newToolStripButton, Me.openToolStripButton, Me.saveToolStripButton, Me.printToolStripButton, Me.toolStripSeparator6, Me.cutToolStripButton, Me.copyToolStripButton, Me.pasteToolStripButton, Me.toolStripSeparator7, Me.btnSystemCodecInformation})
			Me.toolStrip1.Location = New System.Drawing.Point(3, 24)
			Me.toolStrip1.Name = "toolStrip1"
			Me.toolStrip1.Size = New System.Drawing.Size(208, 25)
			Me.toolStrip1.TabIndex = 1
			' 
			' newToolStripButton
			' 
			Me.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
			Me.newToolStripButton.Image = (DirectCast(resources.GetObject("newToolStripButton.Image"), System.Drawing.Image))
			Me.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.newToolStripButton.Name = "newToolStripButton"
			Me.newToolStripButton.Size = New System.Drawing.Size(23, 22)
			Me.newToolStripButton.Text = "&New"
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
'			Me.btnSystemCodecInformation.Click += New System.EventHandler(Me.btnSystemCodecInformation_Click)
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
			Me.ClientSize = New System.Drawing.Size(935, 696)
			Me.Controls.Add(Me.toolStripContainer1)
			Me.Icon = (DirectCast(resources.GetObject("$this.Icon"), System.Drawing.Icon))
			Me.MainMenuStrip = Me.menuStrip1
			Me.Name = "Form1"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "Image Functions"
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
			Me.splitContainer1.Panel1.ResumeLayout(False)
			Me.splitContainer1.Panel2.ResumeLayout(False)
			DirectCast(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.splitContainer1.ResumeLayout(False)
			Me.splitContainer2.Panel1.ResumeLayout(False)
			Me.splitContainer2.Panel2.ResumeLayout(False)
			DirectCast(Me.splitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
			Me.splitContainer2.ResumeLayout(False)
			Me.splitContainer3.Panel1.ResumeLayout(False)
			Me.splitContainer3.Panel1.PerformLayout()
			Me.splitContainer3.Panel2.ResumeLayout(False)
			Me.splitContainer3.Panel2.PerformLayout()
			DirectCast(Me.splitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
			Me.splitContainer3.ResumeLayout(False)
			Me.pbContextMenu.ResumeLayout(False)
			Me.toolStrip1.ResumeLayout(False)
			Me.toolStrip1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private ilDefault As System.Windows.Forms.ImageList
		Private openFile As System.Windows.Forms.OpenFileDialog
		Private menuStrip1 As System.Windows.Forms.MenuStrip
		Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
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
		Private aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripContainer1 As System.Windows.Forms.ToolStripContainer
		Private statusStrip1 As System.Windows.Forms.StatusStrip
		Private Progress As System.Windows.Forms.ToolStripProgressBar
		Private toolStrip1 As System.Windows.Forms.ToolStrip
		Private newToolStripButton As System.Windows.Forms.ToolStripButton
		Private openToolStripButton As System.Windows.Forms.ToolStripButton
		Private saveToolStripButton As System.Windows.Forms.ToolStripButton
		Private printToolStripButton As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
		Private cutToolStripButton As System.Windows.Forms.ToolStripButton
		Private copyToolStripButton As System.Windows.Forms.ToolStripButton
		Private pasteToolStripButton As System.Windows.Forms.ToolStripButton
		Private toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents btnSystemCodecInformation As System.Windows.Forms.ToolStripButton
		Private splitContainer1 As System.Windows.Forms.SplitContainer
		Private splitContainer2 As System.Windows.Forms.SplitContainer
		Private splitContainer3 As System.Windows.Forms.SplitContainer
		Private WithEvents btnLoadNewImage As System.Windows.Forms.Button
		Private label1 As System.Windows.Forms.Label
		Private WithEvents lbImageList As System.Windows.Forms.ListBox
		Private WithEvents lbModification As System.Windows.Forms.ListBox
		Private label2 As System.Windows.Forms.Label
		Private pbImage As Cyotek.Windows.Forms.ImageBox
		Private rtbConOut As System.Windows.Forms.RichTextBox
		Private pbContextMenu As System.Windows.Forms.ContextMenuStrip
		Private WithEvents resetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private label3 As System.Windows.Forms.Label
		Private WithEvents lbMethods As System.Windows.Forms.ListBox
		Private toolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
		Private lblStatus As System.Windows.Forms.ToolStripStatusLabel
		Private GuiTimer As System.Windows.Forms.Timer
		Private WithEvents btnProcess As System.Windows.Forms.Button
		Private WithEvents btnReset As System.Windows.Forms.Button
		Private ControlPanel As System.Windows.Forms.Panel
	End Class
End Namespace

