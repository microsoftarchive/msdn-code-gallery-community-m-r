Namespace ImageFunctions.Forms
	Partial Public Class FrmProcessing
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
			Me.win8_ProgressRing1 = New Win8ProgressRing.Win8_ProgressRing()
			Me.label1 = New System.Windows.Forms.Label()
			Me.lblDescription = New System.Windows.Forms.Label()
			Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
			Me.closeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.contextMenuStrip1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' win8_ProgressRing1
			' 
			Me.win8_ProgressRing1.Control_Height = 80
			Me.win8_ProgressRing1.Dock = System.Windows.Forms.DockStyle.Left
			Me.win8_ProgressRing1.Indicator_Color = System.Drawing.Color.MediumBlue
			Me.win8_ProgressRing1.Location = New System.Drawing.Point(0, 0)
			Me.win8_ProgressRing1.Name = "win8_ProgressRing1"
			Me.win8_ProgressRing1.Refresh_Rate = 100
			Me.win8_ProgressRing1.Size = New System.Drawing.Size(80, 80)
			Me.win8_ProgressRing1.TabIndex = 0
			Me.win8_ProgressRing1.Text = "win8_ProgressRing1"
			Me.win8_ProgressRing1.UseWaitCursor = True
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label1.Location = New System.Drawing.Point(135, 9)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(113, 24)
			Me.label1.TabIndex = 1
			Me.label1.Text = "Please Wait:"
			Me.label1.UseWaitCursor = True
			' 
			' lblDescription
			' 
			Me.lblDescription.AutoEllipsis = True
			Me.lblDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.lblDescription.Location = New System.Drawing.Point(135, 33)
			Me.lblDescription.Name = "lblDescription"
			Me.lblDescription.Size = New System.Drawing.Size(442, 44)
			Me.lblDescription.TabIndex = 2
			Me.lblDescription.Text = "<message>"
			Me.lblDescription.UseWaitCursor = True
			' 
			' contextMenuStrip1
			' 
			Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.closeToolStripMenuItem})
			Me.contextMenuStrip1.Name = "contextMenuStrip1"
			Me.contextMenuStrip1.Size = New System.Drawing.Size(104, 26)
			' 
			' closeToolStripMenuItem
			' 
			Me.closeToolStripMenuItem.Name = "closeToolStripMenuItem"
			Me.closeToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
			Me.closeToolStripMenuItem.Text = "Close"
'			Me.closeToolStripMenuItem.Click += New System.EventHandler(Me.closeToolStripMenuItem_Click)
			' 
			' FrmProcessing
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.Color.Black
			Me.ClientSize = New System.Drawing.Size(589, 86)
			Me.ControlBox = False
			Me.Controls.Add(Me.lblDescription)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.win8_ProgressRing1)
			Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
			Me.ForeColor = System.Drawing.Color.LightYellow
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "FrmProcessing"
			Me.Opacity = 0.85R
			Me.ShowIcon = False
			Me.ShowInTaskbar = False
			Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "FrmProcessing"
			Me.TopMost = True
			Me.UseWaitCursor = True
			Me.contextMenuStrip1.ResumeLayout(False)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private win8_ProgressRing1 As Win8ProgressRing.Win8_ProgressRing
		Private label1 As System.Windows.Forms.Label
		Private lblDescription As System.Windows.Forms.Label
		Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
		Private WithEvents closeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	End Class
End Namespace