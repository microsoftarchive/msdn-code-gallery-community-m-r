Namespace ImageFunctions.Forms
	Partial Public Class FrmHistogram
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
			Me.horizontalIntensityMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.verticalIntensityMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
			Me.splineAreaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.AreaStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.splineRangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.splineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.barToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.lblCoordinatesY = New System.Windows.Forms.Label()
			Me.lbCoordinatesX = New System.Windows.Forms.Label()
			Me.btnPixelColour = New System.Windows.Forms.Button()
			Me.label6 = New System.Windows.Forms.Label()
			Me.label5 = New System.Windows.Forms.Label()
			Me.lblBlueMax = New System.Windows.Forms.Label()
			Me.lblBlueMean = New System.Windows.Forms.Label()
			Me.lblBlueMin = New System.Windows.Forms.Label()
			Me.label16 = New System.Windows.Forms.Label()
			Me.lblGreenMax = New System.Windows.Forms.Label()
			Me.lblGreenMean = New System.Windows.Forms.Label()
			Me.lblGreenMin = New System.Windows.Forms.Label()
			Me.label12 = New System.Windows.Forms.Label()
			Me.lblRedMax = New System.Windows.Forms.Label()
			Me.lblRedMean = New System.Windows.Forms.Label()
			Me.lblRedMin = New System.Windows.Forms.Label()
			Me.label8 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.contextMenuStrip1.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' contextMenuStrip1
			' 
			Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.horizontalIntensityMenuItem, Me.verticalIntensityMenuItem, Me.toolStripSeparator1, Me.splineAreaToolStripMenuItem, Me.AreaStripMenuItem, Me.splineRangeToolStripMenuItem, Me.splineToolStripMenuItem, Me.barToolStripMenuItem})
			Me.contextMenuStrip1.Name = "contextMenuStrip1"
			Me.contextMenuStrip1.Size = New System.Drawing.Size(178, 164)
			' 
			' horizontalIntensityMenuItem
			' 
			Me.horizontalIntensityMenuItem.Checked = True
			Me.horizontalIntensityMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
			Me.horizontalIntensityMenuItem.Name = "horizontalIntensityMenuItem"
			Me.horizontalIntensityMenuItem.Size = New System.Drawing.Size(177, 22)
			Me.horizontalIntensityMenuItem.Text = "Horizontal Intensity"
'			Me.horizontalIntensityMenuItem.Click += New System.EventHandler(Me.horizontalIntensityMenuItem_Click)
			' 
			' verticalIntensityMenuItem
			' 
			Me.verticalIntensityMenuItem.Name = "verticalIntensityMenuItem"
			Me.verticalIntensityMenuItem.Size = New System.Drawing.Size(177, 22)
			Me.verticalIntensityMenuItem.Text = "Vertical Intensity"
'			Me.verticalIntensityMenuItem.Click += New System.EventHandler(Me.verticalIntensityMenuItem_Click)
			' 
			' toolStripSeparator1
			' 
			Me.toolStripSeparator1.Name = "toolStripSeparator1"
			Me.toolStripSeparator1.Size = New System.Drawing.Size(174, 6)
			' 
			' splineAreaToolStripMenuItem
			' 
			Me.splineAreaToolStripMenuItem.Checked = True
			Me.splineAreaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
			Me.splineAreaToolStripMenuItem.Name = "splineAreaToolStripMenuItem"
			Me.splineAreaToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
			Me.splineAreaToolStripMenuItem.Text = "Spline Area"
'			Me.splineAreaToolStripMenuItem.Click += New System.EventHandler(Me.splineAreaToolStripMenuItem_Click)
			' 
			' AreaStripMenuItem
			' 
			Me.AreaStripMenuItem.Name = "AreaStripMenuItem"
			Me.AreaStripMenuItem.Size = New System.Drawing.Size(177, 22)
			Me.AreaStripMenuItem.Text = "Area"
'			Me.AreaStripMenuItem.Click += New System.EventHandler(Me.AreaStripMenuItem_Click)
			' 
			' splineRangeToolStripMenuItem
			' 
			Me.splineRangeToolStripMenuItem.Name = "splineRangeToolStripMenuItem"
			Me.splineRangeToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
			Me.splineRangeToolStripMenuItem.Text = "Spline Range"
'			Me.splineRangeToolStripMenuItem.Click += New System.EventHandler(Me.splineRangeToolStripMenuItem_Click)
			' 
			' splineToolStripMenuItem
			' 
			Me.splineToolStripMenuItem.Name = "splineToolStripMenuItem"
			Me.splineToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
			Me.splineToolStripMenuItem.Text = "Spline"
'			Me.splineToolStripMenuItem.Click += New System.EventHandler(Me.splineToolStripMenuItem_Click)
			' 
			' barToolStripMenuItem
			' 
			Me.barToolStripMenuItem.Name = "barToolStripMenuItem"
			Me.barToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
			Me.barToolStripMenuItem.Text = "Bar"
'			Me.barToolStripMenuItem.Click += New System.EventHandler(Me.barToolStripMenuItem_Click)
			' 
			' panel1
			' 
			Me.panel1.Controls.Add(Me.lblCoordinatesY)
			Me.panel1.Controls.Add(Me.lbCoordinatesX)
			Me.panel1.Controls.Add(Me.btnPixelColour)
			Me.panel1.Controls.Add(Me.label6)
			Me.panel1.Controls.Add(Me.label5)
			Me.panel1.Controls.Add(Me.lblBlueMax)
			Me.panel1.Controls.Add(Me.lblBlueMean)
			Me.panel1.Controls.Add(Me.lblBlueMin)
			Me.panel1.Controls.Add(Me.label16)
			Me.panel1.Controls.Add(Me.lblGreenMax)
			Me.panel1.Controls.Add(Me.lblGreenMean)
			Me.panel1.Controls.Add(Me.lblGreenMin)
			Me.panel1.Controls.Add(Me.label12)
			Me.panel1.Controls.Add(Me.lblRedMax)
			Me.panel1.Controls.Add(Me.lblRedMean)
			Me.panel1.Controls.Add(Me.lblRedMin)
			Me.panel1.Controls.Add(Me.label8)
			Me.panel1.Controls.Add(Me.label4)
			Me.panel1.Controls.Add(Me.label3)
			Me.panel1.Controls.Add(Me.label2)
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.panel1.Location = New System.Drawing.Point(0, 224)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(298, 74)
			Me.panel1.TabIndex = 1
			' 
			' lblCoordinatesY
			' 
			Me.lblCoordinatesY.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.lblCoordinatesY.AutoSize = True
			Me.lblCoordinatesY.Location = New System.Drawing.Point(98, 57)
			Me.lblCoordinatesY.Name = "lblCoordinatesY"
			Me.lblCoordinatesY.Size = New System.Drawing.Size(31, 13)
			Me.lblCoordinatesY.TabIndex = 20
			Me.lblCoordinatesY.Text = "0000"
			' 
			' lbCoordinatesX
			' 
			Me.lbCoordinatesX.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.lbCoordinatesX.AutoSize = True
			Me.lbCoordinatesX.Location = New System.Drawing.Point(61, 57)
			Me.lbCoordinatesX.Name = "lbCoordinatesX"
			Me.lbCoordinatesX.Size = New System.Drawing.Size(31, 13)
			Me.lbCoordinatesX.TabIndex = 19
			Me.lbCoordinatesX.Text = "0000"
			' 
			' btnPixelColour
			' 
			Me.btnPixelColour.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnPixelColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat
			Me.btnPixelColour.Location = New System.Drawing.Point(210, 57)
			Me.btnPixelColour.Margin = New System.Windows.Forms.Padding(0)
			Me.btnPixelColour.Name = "btnPixelColour"
			Me.btnPixelColour.Size = New System.Drawing.Size(75, 12)
			Me.btnPixelColour.TabIndex = 18
			Me.btnPixelColour.UseVisualStyleBackColor = True
			' 
			' label6
			' 
			Me.label6.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.label6.AutoSize = True
			Me.label6.Location = New System.Drawing.Point(139, 57)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(65, 13)
			Me.label6.TabIndex = 17
			Me.label6.Text = "Pixel Colour:"
			' 
			' label5
			' 
			Me.label5.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(4, 57)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(59, 13)
			Me.label5.TabIndex = 16
			Me.label5.Text = "Mouse XY:"
			' 
			' lblBlueMax
			' 
			Me.lblBlueMax.BackColor = System.Drawing.SystemColors.Control
			Me.lblBlueMax.ForeColor = System.Drawing.Color.Blue
			Me.lblBlueMax.Location = New System.Drawing.Point(245, 43)
			Me.lblBlueMax.Name = "lblBlueMax"
			Me.lblBlueMax.Size = New System.Drawing.Size(37, 13)
			Me.lblBlueMax.TabIndex = 15
			Me.lblBlueMax.Text = "0"
			Me.lblBlueMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' lblBlueMean
			' 
			Me.lblBlueMean.BackColor = System.Drawing.SystemColors.Control
			Me.lblBlueMean.ForeColor = System.Drawing.Color.Blue
			Me.lblBlueMean.Location = New System.Drawing.Point(150, 43)
			Me.lblBlueMean.Name = "lblBlueMean"
			Me.lblBlueMean.Size = New System.Drawing.Size(44, 13)
			Me.lblBlueMean.TabIndex = 14
			Me.lblBlueMean.Text = "0"
			Me.lblBlueMean.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' lblBlueMin
			' 
			Me.lblBlueMin.BackColor = System.Drawing.SystemColors.Control
			Me.lblBlueMin.ForeColor = System.Drawing.Color.Blue
			Me.lblBlueMin.Location = New System.Drawing.Point(68, 43)
			Me.lblBlueMin.Name = "lblBlueMin"
			Me.lblBlueMin.Size = New System.Drawing.Size(34, 13)
			Me.lblBlueMin.TabIndex = 13
			Me.lblBlueMin.Text = "0"
			Me.lblBlueMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' label16
			' 
			Me.label16.AutoSize = True
			Me.label16.Location = New System.Drawing.Point(4, 43)
			Me.label16.Name = "label16"
			Me.label16.Size = New System.Drawing.Size(28, 13)
			Me.label16.TabIndex = 12
			Me.label16.Text = "Blue"
			' 
			' lblGreenMax
			' 
			Me.lblGreenMax.ForeColor = System.Drawing.Color.Green
			Me.lblGreenMax.Location = New System.Drawing.Point(245, 30)
			Me.lblGreenMax.Name = "lblGreenMax"
			Me.lblGreenMax.Size = New System.Drawing.Size(37, 13)
			Me.lblGreenMax.TabIndex = 11
			Me.lblGreenMax.Text = "0"
			Me.lblGreenMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' lblGreenMean
			' 
			Me.lblGreenMean.ForeColor = System.Drawing.Color.Green
			Me.lblGreenMean.Location = New System.Drawing.Point(150, 30)
			Me.lblGreenMean.Name = "lblGreenMean"
			Me.lblGreenMean.Size = New System.Drawing.Size(44, 13)
			Me.lblGreenMean.TabIndex = 10
			Me.lblGreenMean.Text = "0"
			Me.lblGreenMean.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' lblGreenMin
			' 
			Me.lblGreenMin.ForeColor = System.Drawing.Color.Green
			Me.lblGreenMin.Location = New System.Drawing.Point(68, 30)
			Me.lblGreenMin.Name = "lblGreenMin"
			Me.lblGreenMin.Size = New System.Drawing.Size(34, 13)
			Me.lblGreenMin.TabIndex = 9
			Me.lblGreenMin.Text = "0"
			Me.lblGreenMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' label12
			' 
			Me.label12.AutoSize = True
			Me.label12.Location = New System.Drawing.Point(4, 30)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(36, 13)
			Me.label12.TabIndex = 8
			Me.label12.Text = "Green"
			' 
			' lblRedMax
			' 
			Me.lblRedMax.ForeColor = System.Drawing.Color.Red
			Me.lblRedMax.Location = New System.Drawing.Point(245, 17)
			Me.lblRedMax.Name = "lblRedMax"
			Me.lblRedMax.Size = New System.Drawing.Size(37, 13)
			Me.lblRedMax.TabIndex = 7
			Me.lblRedMax.Text = "0"
			Me.lblRedMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' lblRedMean
			' 
			Me.lblRedMean.ForeColor = System.Drawing.Color.Red
			Me.lblRedMean.Location = New System.Drawing.Point(150, 17)
			Me.lblRedMean.Name = "lblRedMean"
			Me.lblRedMean.Size = New System.Drawing.Size(44, 13)
			Me.lblRedMean.TabIndex = 6
			Me.lblRedMean.Text = "0"
			Me.lblRedMean.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' lblRedMin
			' 
			Me.lblRedMin.ForeColor = System.Drawing.Color.Red
			Me.lblRedMin.Location = New System.Drawing.Point(68, 17)
			Me.lblRedMin.Name = "lblRedMin"
			Me.lblRedMin.Size = New System.Drawing.Size(34, 13)
			Me.lblRedMin.TabIndex = 5
			Me.lblRedMin.Text = "0"
			Me.lblRedMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Location = New System.Drawing.Point(4, 17)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(27, 13)
			Me.label8.TabIndex = 4
			Me.label8.Text = "Red"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(245, 4)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(27, 13)
			Me.label4.TabIndex = 3
			Me.label4.Text = "Max"
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(150, 4)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(34, 13)
			Me.label3.TabIndex = 2
			Me.label3.Text = "Mean"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(68, 4)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(24, 13)
			Me.label2.TabIndex = 1
			Me.label2.Text = "Min"
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(4, 4)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(37, 13)
			Me.label1.TabIndex = 0
			Me.label1.Text = "Colour"
			' 
			' FrmHistogram
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(298, 298)
			Me.ContextMenuStrip = Me.contextMenuStrip1
			Me.ControlBox = False
			Me.Controls.Add(Me.panel1)
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
			Me.MinimumSize = New System.Drawing.Size(304, 304)
			Me.Name = "FrmHistogram"
			Me.Text = "Histogram"
			Me.contextMenuStrip1.ResumeLayout(False)
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
		Private WithEvents verticalIntensityMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents horizontalIntensityMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private panel1 As System.Windows.Forms.Panel
		Private lblBlueMax As System.Windows.Forms.Label
		Private lblBlueMean As System.Windows.Forms.Label
		Private lblBlueMin As System.Windows.Forms.Label
		Private label16 As System.Windows.Forms.Label
		Private lblGreenMax As System.Windows.Forms.Label
		Private lblGreenMean As System.Windows.Forms.Label
		Private lblGreenMin As System.Windows.Forms.Label
		Private label12 As System.Windows.Forms.Label
		Private lblRedMax As System.Windows.Forms.Label
		Private lblRedMean As System.Windows.Forms.Label
		Private lblRedMin As System.Windows.Forms.Label
		Private label8 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private label3 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private btnPixelColour As System.Windows.Forms.Button
		Private label6 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private lblCoordinatesY As System.Windows.Forms.Label
		Private lbCoordinatesX As System.Windows.Forms.Label
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents splineAreaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents splineRangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents splineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents AreaStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents barToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	End Class
End Namespace