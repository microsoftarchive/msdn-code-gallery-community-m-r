Namespace ImageFunctions.Forms
	Partial Friend Class FrmAboutBox
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
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
			Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
			Me.logoPictureBox = New System.Windows.Forms.PictureBox()
			Me.labelProductName = New System.Windows.Forms.Label()
			Me.labelVersion = New System.Windows.Forms.Label()
			Me.labelCopyright = New System.Windows.Forms.Label()
			Me.labelCompanyName = New System.Windows.Forms.Label()
			Me.textBoxDescription = New System.Windows.Forms.TextBox()
			Me.okButton = New System.Windows.Forms.Button()
			Me.tableLayoutPanel.SuspendLayout()
			DirectCast(Me.logoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' tableLayoutPanel
			' 
			Me.tableLayoutPanel.ColumnCount = 2
			Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F))
			Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F))
			Me.tableLayoutPanel.Controls.Add(Me.logoPictureBox, 0, 0)
			Me.tableLayoutPanel.Controls.Add(Me.labelProductName, 1, 0)
			Me.tableLayoutPanel.Controls.Add(Me.labelVersion, 1, 1)
			Me.tableLayoutPanel.Controls.Add(Me.labelCopyright, 1, 2)
			Me.tableLayoutPanel.Controls.Add(Me.labelCompanyName, 1, 3)
			Me.tableLayoutPanel.Controls.Add(Me.textBoxDescription, 1, 4)
			Me.tableLayoutPanel.Controls.Add(Me.okButton, 1, 5)
			Me.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tableLayoutPanel.Location = New System.Drawing.Point(9, 9)
			Me.tableLayoutPanel.Name = "tableLayoutPanel"
			Me.tableLayoutPanel.RowCount = 6
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F))
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F))
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F))
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F))
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F))
			Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F))
			Me.tableLayoutPanel.Size = New System.Drawing.Size(417, 265)
			Me.tableLayoutPanel.TabIndex = 0
			' 
			' logoPictureBox
			' 
			Me.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
			Me.logoPictureBox.Image = My.Resources._512_graphics_big_jpg_86
			Me.logoPictureBox.Location = New System.Drawing.Point(3, 3)
			Me.logoPictureBox.Name = "logoPictureBox"
			Me.tableLayoutPanel.SetRowSpan(Me.logoPictureBox, 6)
			Me.logoPictureBox.Size = New System.Drawing.Size(131, 259)
			Me.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
			Me.logoPictureBox.TabIndex = 12
			Me.logoPictureBox.TabStop = False
			' 
			' labelProductName
			' 
			Me.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill
			Me.labelProductName.Location = New System.Drawing.Point(143, 0)
			Me.labelProductName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
			Me.labelProductName.MaximumSize = New System.Drawing.Size(0, 17)
			Me.labelProductName.Name = "labelProductName"
			Me.labelProductName.Size = New System.Drawing.Size(271, 17)
			Me.labelProductName.TabIndex = 19
			Me.labelProductName.Text = "Product Name"
			Me.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' labelVersion
			' 
			Me.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill
			Me.labelVersion.Location = New System.Drawing.Point(143, 26)
			Me.labelVersion.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
			Me.labelVersion.MaximumSize = New System.Drawing.Size(0, 17)
			Me.labelVersion.Name = "labelVersion"
			Me.labelVersion.Size = New System.Drawing.Size(271, 17)
			Me.labelVersion.TabIndex = 0
			Me.labelVersion.Text = "Version"
			Me.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' labelCopyright
			' 
			Me.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill
			Me.labelCopyright.Location = New System.Drawing.Point(143, 52)
			Me.labelCopyright.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
			Me.labelCopyright.MaximumSize = New System.Drawing.Size(0, 17)
			Me.labelCopyright.Name = "labelCopyright"
			Me.labelCopyright.Size = New System.Drawing.Size(271, 17)
			Me.labelCopyright.TabIndex = 21
			Me.labelCopyright.Text = "Copyright"
			Me.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' labelCompanyName
			' 
			Me.labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
			Me.labelCompanyName.Location = New System.Drawing.Point(143, 78)
			Me.labelCompanyName.Margin = New System.Windows.Forms.Padding(6, 0, 3, 0)
			Me.labelCompanyName.MaximumSize = New System.Drawing.Size(0, 17)
			Me.labelCompanyName.Name = "labelCompanyName"
			Me.labelCompanyName.Size = New System.Drawing.Size(271, 17)
			Me.labelCompanyName.TabIndex = 22
			Me.labelCompanyName.Text = "Company Name"
			Me.labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' textBoxDescription
			' 
			Me.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill
			Me.textBoxDescription.Location = New System.Drawing.Point(143, 107)
			Me.textBoxDescription.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
			Me.textBoxDescription.Multiline = True
			Me.textBoxDescription.Name = "textBoxDescription"
			Me.textBoxDescription.ReadOnly = True
			Me.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
			Me.textBoxDescription.Size = New System.Drawing.Size(271, 126)
			Me.textBoxDescription.TabIndex = 23
			Me.textBoxDescription.TabStop = False
			Me.textBoxDescription.Text = "Description"
			' 
			' okButton
			' 
			Me.okButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.okButton.Location = New System.Drawing.Point(339, 239)
			Me.okButton.Name = "okButton"
			Me.okButton.Size = New System.Drawing.Size(75, 23)
			Me.okButton.TabIndex = 24
			Me.okButton.Text = "&OK"
			' 
			' FrmAboutBox
			' 
			Me.AcceptButton = Me.okButton
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(435, 283)
			Me.Controls.Add(Me.tableLayoutPanel)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
			Me.MaximizeBox = False
			Me.MinimizeBox = False
			Me.Name = "FrmAboutBox"
			Me.Padding = New System.Windows.Forms.Padding(9)
			Me.ShowIcon = False
			Me.ShowInTaskbar = False
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "About"
			Me.tableLayoutPanel.ResumeLayout(False)
			Me.tableLayoutPanel.PerformLayout()
			DirectCast(Me.logoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
		Private logoPictureBox As System.Windows.Forms.PictureBox
		Private labelProductName As System.Windows.Forms.Label
		Private labelVersion As System.Windows.Forms.Label
		Private labelCopyright As System.Windows.Forms.Label
		Private labelCompanyName As System.Windows.Forms.Label
		Private textBoxDescription As System.Windows.Forms.TextBox
		Private okButton As System.Windows.Forms.Button
	End Class
End Namespace
