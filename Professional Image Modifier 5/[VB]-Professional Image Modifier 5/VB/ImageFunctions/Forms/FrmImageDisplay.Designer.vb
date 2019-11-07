Namespace ImageFunctions.Forms
	Partial Public Class FrmImageDisplay
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
			Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
			Me.rToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.resetZoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.label2 = New System.Windows.Forms.Label()
			Me.btnSelectedColour = New System.Windows.Forms.Button()
			Me.lblMouseY = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.lblMouseX = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.pbImage = New Cyotek.Windows.Forms.ImageBox()
			Me.label4 = New System.Windows.Forms.Label()
			Me.lblFileSize = New System.Windows.Forms.Label()
			Me.contextMenuStrip1.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' contextMenuStrip1
			' 
			Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.toolStripMenuItem1, Me.toolStripSeparator1, Me.rToolStripMenuItem, Me.resetZoomToolStripMenuItem})
			Me.contextMenuStrip1.Name = "contextMenuStrip1"
			Me.contextMenuStrip1.Size = New System.Drawing.Size(156, 76)
			' 
			' toolStripMenuItem1
			' 
			Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
			Me.toolStripMenuItem1.Size = New System.Drawing.Size(155, 22)
			Me.toolStripMenuItem1.Text = "Show Pixel Grid"
'			Me.toolStripMenuItem1.Click += New System.EventHandler(Me.toolStripMenuItem1_Click)
			' 
			' toolStripSeparator1
			' 
			Me.toolStripSeparator1.Name = "toolStripSeparator1"
			Me.toolStripSeparator1.Size = New System.Drawing.Size(152, 6)
			' 
			' rToolStripMenuItem
			' 
			Me.rToolStripMenuItem.Name = "rToolStripMenuItem"
			Me.rToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
			Me.rToolStripMenuItem.Text = "Reset Image"
'			Me.rToolStripMenuItem.Click += New System.EventHandler(Me.rToolStripMenuItem_Click)
			' 
			' resetZoomToolStripMenuItem
			' 
			Me.resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem"
			Me.resetZoomToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
			Me.resetZoomToolStripMenuItem.Text = "Reset Zoom"
'			Me.resetZoomToolStripMenuItem.Click += New System.EventHandler(Me.resetZoomToolStripMenuItem_Click)
			' 
			' panel1
			' 
			Me.panel1.Controls.Add(Me.lblFileSize)
			Me.panel1.Controls.Add(Me.label4)
			Me.panel1.Controls.Add(Me.label2)
			Me.panel1.Controls.Add(Me.btnSelectedColour)
			Me.panel1.Controls.Add(Me.lblMouseY)
			Me.panel1.Controls.Add(Me.label3)
			Me.panel1.Controls.Add(Me.lblMouseX)
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.panel1.Location = New System.Drawing.Point(0, 235)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(514, 26)
			Me.panel1.TabIndex = 1
			' 
			' label2
			' 
			Me.label2.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(384, 4)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(40, 13)
			Me.label2.TabIndex = 5
			Me.label2.Text = "Colour:"
			' 
			' btnSelectedColour
			' 
			Me.btnSelectedColour.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnSelectedColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			Me.btnSelectedColour.Location = New System.Drawing.Point(427, 4)
			Me.btnSelectedColour.Name = "btnSelectedColour"
			Me.btnSelectedColour.Size = New System.Drawing.Size(75, 13)
			Me.btnSelectedColour.TabIndex = 4
			Me.btnSelectedColour.UseVisualStyleBackColor = True
			' 
			' lblMouseY
			' 
			Me.lblMouseY.AutoSize = True
			Me.lblMouseY.Location = New System.Drawing.Point(162, 4)
			Me.lblMouseY.Name = "lblMouseY"
			Me.lblMouseY.Size = New System.Drawing.Size(37, 13)
			Me.lblMouseY.TabIndex = 3
			Me.lblMouseY.Text = "00000"
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(104, 4)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(52, 13)
			Me.label3.TabIndex = 2
			Me.label3.Text = "Mouse Y:"
			' 
			' lblMouseX
			' 
			Me.lblMouseX.AutoSize = True
			Me.lblMouseX.Location = New System.Drawing.Point(61, 4)
			Me.lblMouseX.Name = "lblMouseX"
			Me.lblMouseX.Size = New System.Drawing.Size(37, 13)
			Me.lblMouseX.TabIndex = 1
			Me.lblMouseX.Text = "00000"
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(3, 4)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(52, 13)
			Me.label1.TabIndex = 0
			Me.label1.Text = "Mouse X:"
			' 
			' pbImage
			' 
			Me.pbImage.BackColor = System.Drawing.Color.DimGray
			Me.pbImage.ContextMenuStrip = Me.contextMenuStrip1
			Me.pbImage.Cursor = System.Windows.Forms.Cursors.Cross
			Me.pbImage.Dock = System.Windows.Forms.DockStyle.Fill
			Me.pbImage.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Image
			Me.pbImage.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None
			Me.pbImage.Location = New System.Drawing.Point(0, 0)
			Me.pbImage.Name = "pbImage"
			Me.pbImage.Size = New System.Drawing.Size(514, 235)
			Me.pbImage.TabIndex = 2
'			Me.pbImage.MouseMove += New System.Windows.Forms.MouseEventHandler(Me.pbImage_MouseMove_1)
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(205, 4)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(30, 13)
			Me.label4.TabIndex = 6
			Me.label4.Text = "Size:"
			' 
			' lblFileSize
			' 
			Me.lblFileSize.AutoSize = True
			Me.lblFileSize.Location = New System.Drawing.Point(241, 4)
			Me.lblFileSize.Name = "lblFileSize"
			Me.lblFileSize.Size = New System.Drawing.Size(13, 13)
			Me.lblFileSize.TabIndex = 7
			Me.lblFileSize.Text = "0"
			' 
			' FrmImageDisplay
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(514, 261)
			Me.ControlBox = False
			Me.Controls.Add(Me.pbImage)
			Me.Controls.Add(Me.panel1)
			Me.DoubleBuffered = True
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
			Me.Name = "FrmImageDisplay"
			Me.Text = "Image Display"
			Me.contextMenuStrip1.ResumeLayout(False)
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
		Private WithEvents rToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private panel1 As System.Windows.Forms.Panel
		Private lblMouseY As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private lblMouseX As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private WithEvents pbImage As Cyotek.Windows.Forms.ImageBox
		Private WithEvents toolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private btnSelectedColour As System.Windows.Forms.Button
		Private label2 As System.Windows.Forms.Label
		Private WithEvents resetZoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private lblFileSize As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
	End Class
End Namespace