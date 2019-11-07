<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.Status1 = New System.Windows.Forms.StatusStrip
        Me.status2 = New System.Windows.Forms.ToolStripStatusLabel
        Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.TSRX = New System.Windows.Forms.ToolStrip
        Me.RxbtnClear = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnItemWindow = New System.Windows.Forms.ToolStripButton
        Me.btnExit = New System.Windows.Forms.ToolStripButton
        Me.rtbTX = New System.Windows.Forms.RichTextBox
        Me.MenuTxBox = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CopyTx = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteTx = New System.Windows.Forms.ToolStripMenuItem
        Me.CutTx = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearTX = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadTX = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveTx = New System.Windows.Forms.ToolStripMenuItem
        Me.FontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnItemSmall = New System.Windows.Forms.ToolStripMenuItem
        Me.mnItemMedium = New System.Windows.Forms.ToolStripMenuItem
        Me.mnItemLarge = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DataSet1 = New System.Data.DataSet
        Me.Label3 = New System.Windows.Forms.Label
        Me.Status1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TSRX.SuspendLayout()
        Me.MenuTxBox.SuspendLayout()
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Status1
        '
        Me.Status1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.status2})
        Me.Status1.Location = New System.Drawing.Point(0, 374)
        Me.Status1.Name = "Status1"
        Me.Status1.Size = New System.Drawing.Size(510, 22)
        Me.Status1.TabIndex = 1
        Me.Status1.Text = "StatusStrip1"
        '
        'status2
        '
        Me.status2.Name = "status2"
        Me.status2.Size = New System.Drawing.Size(121, 17)
        Me.status2.Text = "ToolStripStatusLabel1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CheckBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TSRX)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rtbTX)
        Me.SplitContainer1.Size = New System.Drawing.Size(510, 374)
        Me.SplitContainer1.SplitterDistance = 229
        Me.SplitContainer1.TabIndex = 3
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(508, 202)
        Me.DataGridView1.TabIndex = 3
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(87, 4)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(89, 17)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "overwrite grid"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TSRX
        '
        Me.TSRX.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RxbtnClear, Me.ToolStripSeparator3, Me.mnItemWindow, Me.btnExit})
        Me.TSRX.Location = New System.Drawing.Point(0, 0)
        Me.TSRX.Name = "TSRX"
        Me.TSRX.Size = New System.Drawing.Size(508, 25)
        Me.TSRX.TabIndex = 0
        Me.TSRX.Text = "ToolStrip2"
        '
        'RxbtnClear
        '
        Me.RxbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RxbtnClear.Image = Global.DbViewer.My.Resources.Resources.Cut
        Me.RxbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RxbtnClear.Name = "RxbtnClear"
        Me.RxbtnClear.Size = New System.Drawing.Size(23, 22)
        Me.RxbtnClear.Text = "clear"
        Me.RxbtnClear.ToolTipText = "clear DataTable"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'mnItemWindow
        '
        Me.mnItemWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.mnItemWindow.Image = Global.DbViewer.My.Resources.Resources.Applications
        Me.mnItemWindow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnItemWindow.Name = "mnItemWindow"
        Me.mnItemWindow.Size = New System.Drawing.Size(23, 22)
        Me.mnItemWindow.Text = "ToolStripButton1"
        Me.mnItemWindow.ToolTipText = "orientation"
        '
        'btnExit
        '
        Me.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnExit.Image = Global.DbViewer.My.Resources.Resources.Standby
        Me.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(23, 22)
        Me.btnExit.Text = "ToolStripButton1"
        Me.btnExit.ToolTipText = "exit"
        '
        'rtbTX
        '
        Me.rtbTX.BackColor = System.Drawing.SystemColors.ControlLight
        Me.rtbTX.ContextMenuStrip = Me.MenuTxBox
        Me.rtbTX.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.DbViewer.My.MySettings.Default, "rtbSendeBox", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.rtbTX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbTX.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbTX.Location = New System.Drawing.Point(0, 0)
        Me.rtbTX.Name = "rtbTX"
        Me.rtbTX.Size = New System.Drawing.Size(508, 139)
        Me.rtbTX.TabIndex = 4
        Me.rtbTX.Text = Global.DbViewer.My.MySettings.Default.rtbSendeBox
        Me.ToolTip1.SetToolTip(Me.rtbTX, "Sende Box" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Menue: rechte Maustaste" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Senden: DoppelKlick auf string")
        '
        'MenuTxBox
        '
        Me.MenuTxBox.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyTx, Me.PasteTx, Me.CutTx, Me.ClearTX, Me.LoadTX, Me.SaveTx, Me.FontToolStripMenuItem})
        Me.MenuTxBox.Name = "MenuTxBox"
        Me.MenuTxBox.Size = New System.Drawing.Size(147, 158)
        '
        'CopyTx
        '
        Me.CopyTx.Image = Global.DbViewer.My.Resources.Resources.Copy
        Me.CopyTx.Name = "CopyTx"
        Me.CopyTx.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyTx.Size = New System.Drawing.Size(146, 22)
        Me.CopyTx.Text = "Copy"
        '
        'PasteTx
        '
        Me.PasteTx.Image = Global.DbViewer.My.Resources.Resources.Paste
        Me.PasteTx.Name = "PasteTx"
        Me.PasteTx.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteTx.Size = New System.Drawing.Size(146, 22)
        Me.PasteTx.Text = "Paste"
        '
        'CutTx
        '
        Me.CutTx.Image = Global.DbViewer.My.Resources.Resources.Cut
        Me.CutTx.Name = "CutTx"
        Me.CutTx.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.CutTx.Size = New System.Drawing.Size(146, 22)
        Me.CutTx.Text = "Cut"
        '
        'ClearTX
        '
        Me.ClearTX.Image = Global.DbViewer.My.Resources.Resources.Doc_Del
        Me.ClearTX.Name = "ClearTX"
        Me.ClearTX.Size = New System.Drawing.Size(146, 22)
        Me.ClearTX.Text = "Clear all"
        '
        'LoadTX
        '
        Me.LoadTX.Image = Global.DbViewer.My.Resources.Resources.Disc
        Me.LoadTX.Name = "LoadTX"
        Me.LoadTX.Size = New System.Drawing.Size(146, 22)
        Me.LoadTX.Text = "Load"
        '
        'SaveTx
        '
        Me.SaveTx.Image = Global.DbViewer.My.Resources.Resources.Disk_download
        Me.SaveTx.Name = "SaveTx"
        Me.SaveTx.Size = New System.Drawing.Size(146, 22)
        Me.SaveTx.Text = "Save"
        '
        'FontToolStripMenuItem
        '
        Me.FontToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnItemSmall, Me.mnItemMedium, Me.mnItemLarge})
        Me.FontToolStripMenuItem.Image = Global.DbViewer.My.Resources.Resources.Fbook
        Me.FontToolStripMenuItem.Name = "FontToolStripMenuItem"
        Me.FontToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.FontToolStripMenuItem.Text = "Font"
        '
        'mnItemSmall
        '
        Me.mnItemSmall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnItemSmall.Name = "mnItemSmall"
        Me.mnItemSmall.Size = New System.Drawing.Size(119, 22)
        Me.mnItemSmall.Text = "Small"
        '
        'mnItemMedium
        '
        Me.mnItemMedium.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnItemMedium.Name = "mnItemMedium"
        Me.mnItemMedium.Size = New System.Drawing.Size(119, 22)
        Me.mnItemMedium.Text = "Medium"
        '
        'mnItemLarge
        '
        Me.mnItemLarge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnItemLarge.Name = "mnItemLarge"
        Me.mnItemLarge.Size = New System.Drawing.Size(119, 22)
        Me.mnItemLarge.Text = "Large"
        '
        'DataSet1
        '
        Me.DataSet1.DataSetName = "NewDataSet"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Image = Global.DbViewer.My.Resources.Resources.ledGray
        Me.Label3.Location = New System.Drawing.Point(377, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 5
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 396)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Status1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(400, 250)
        Me.Name = "frmMain"
        Me.Text = "MySql - ODBC database viewer "
        Me.Status1.ResumeLayout(False)
        Me.Status1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TSRX.ResumeLayout(False)
        Me.TSRX.PerformLayout()
        Me.MenuTxBox.ResumeLayout(False)
        CType(Me.DataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Status1 As System.Windows.Forms.StatusStrip
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TSRX As System.Windows.Forms.ToolStrip
    Friend WithEvents RxbtnClear As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents rtbTX As System.Windows.Forms.RichTextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MenuTxBox As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyTx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteTx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutTx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataSet1 As System.Data.DataSet
    Friend WithEvents ClearTX As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadTX As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveTx As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnItemSmall As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnItemMedium As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnItemLarge As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnExit As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnItemWindow As System.Windows.Forms.ToolStripButton
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents status2 As System.Windows.Forms.ToolStripStatusLabel

End Class
