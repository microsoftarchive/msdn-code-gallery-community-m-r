Namespace ImageFunctions.Forms
	Partial Public Class FrmConsole
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
			Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.refreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.timerIdle = New System.Windows.Forms.Timer(Me.components)
			Me.rtbConOut = New System.Windows.Forms.RichTextBox()
			Me.contextMenuStrip1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' contextMenuStrip1
			' 
			Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.refreshToolStripMenuItem})
			Me.contextMenuStrip1.Name = "contextMenuStrip1"
			Me.contextMenuStrip1.Size = New System.Drawing.Size(114, 26)
			' 
			' refreshToolStripMenuItem
			' 
			Me.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem"
			Me.refreshToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
			Me.refreshToolStripMenuItem.Text = "Refresh"
'			Me.refreshToolStripMenuItem.Click += New System.EventHandler(Me.refreshToolStripMenuItem_Click)
			' 
			' timerIdle
			' 
			Me.timerIdle.Enabled = True
			Me.timerIdle.Interval = 1000
'			Me.timerIdle.Tick += New System.EventHandler(Me.timerIdle_Tick)
			' 
			' rtbConOut
			' 
			Me.rtbConOut.BackColor = System.Drawing.Color.Black
			Me.rtbConOut.ContextMenuStrip = Me.contextMenuStrip1
			Me.rtbConOut.DetectUrls = False
			Me.rtbConOut.Dock = System.Windows.Forms.DockStyle.Fill
			Me.rtbConOut.ForeColor = System.Drawing.Color.Gold
			Me.rtbConOut.Location = New System.Drawing.Point(0, 0)
			Me.rtbConOut.Name = "rtbConOut"
			Me.rtbConOut.ReadOnly = True
			Me.rtbConOut.ShowSelectionMargin = True
			Me.rtbConOut.Size = New System.Drawing.Size(450, 177)
			Me.rtbConOut.TabIndex = 2
			Me.rtbConOut.Text = ""
			Me.rtbConOut.WordWrap = False
			' 
			' FrmConsole
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(450, 177)
			Me.ControlBox = False
			Me.Controls.Add(Me.rtbConOut)
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
			Me.Name = "FrmConsole"
			Me.Text = "Console"
			Me.contextMenuStrip1.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
		Private WithEvents refreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents timerIdle As System.Windows.Forms.Timer
		Private rtbConOut As System.Windows.Forms.RichTextBox
	End Class
End Namespace