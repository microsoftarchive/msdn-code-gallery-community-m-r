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
			Me.ilDefault = New System.Windows.Forms.ImageList(Me.components)
			Me.openFile = New System.Windows.Forms.OpenFileDialog()
			Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
			Me.splitContainer3 = New System.Windows.Forms.SplitContainer()
			Me.btnLoadNewImage = New System.Windows.Forms.Button()
			Me.label1 = New System.Windows.Forms.Label()
			Me.listBox1 = New System.Windows.Forms.ListBox()
			Me.button1 = New System.Windows.Forms.Button()
			Me.listBox2 = New System.Windows.Forms.ListBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.pbImage = New Cyotek.Windows.Forms.ImageBox()
			DirectCast(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.splitContainer1.Panel1.SuspendLayout()
			Me.splitContainer1.Panel2.SuspendLayout()
			Me.splitContainer1.SuspendLayout()
			DirectCast(Me.splitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.splitContainer3.Panel1.SuspendLayout()
			Me.splitContainer3.Panel2.SuspendLayout()
			Me.splitContainer3.SuspendLayout()
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
			' splitContainer1
			' 
			Me.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveBorder
			Me.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
			Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer1.Name = "splitContainer1"
			' 
			' splitContainer1.Panel1
			' 
			Me.splitContainer1.Panel1.Controls.Add(Me.splitContainer3)
			' 
			' splitContainer1.Panel2
			' 
			Me.splitContainer1.Panel2.AutoScroll = True
			Me.splitContainer1.Panel2.Controls.Add(Me.pbImage)

			Me.splitContainer1.Size = New System.Drawing.Size(935, 696)
			Me.splitContainer1.SplitterDistance = 435
			Me.splitContainer1.TabIndex = 8
			' 
			' splitContainer3
			' 
			Me.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer3.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer3.Name = "splitContainer3"
			Me.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
			' 
			' splitContainer3.Panel1
			' 
			Me.splitContainer3.Panel1.Controls.Add(Me.btnLoadNewImage)
			Me.splitContainer3.Panel1.Controls.Add(Me.label1)
			Me.splitContainer3.Panel1.Controls.Add(Me.listBox1)
			' 
			' splitContainer3.Panel2
			' 
			Me.splitContainer3.Panel2.Controls.Add(Me.button1)
			Me.splitContainer3.Panel2.Controls.Add(Me.listBox2)
			Me.splitContainer3.Panel2.Controls.Add(Me.label2)
			Me.splitContainer3.Size = New System.Drawing.Size(433, 694)
			Me.splitContainer3.SplitterDistance = 329
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
'			Me.btnLoadNewImage.Click += New System.EventHandler(Me.btnLoadNewImage_Click)
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
			' listBox1
			' 
			Me.listBox1.FormattingEnabled = True
			Me.listBox1.Location = New System.Drawing.Point(83, 12)
			Me.listBox1.Name = "listBox1"
			Me.listBox1.Size = New System.Drawing.Size(303, 17)
			Me.listBox1.TabIndex = 20
'			Me.listBox1.SelectedIndexChanged += New System.EventHandler(Me.listBox1_SelectedIndexChanged)
			' 
			' button1
			' 

			Me.button1.Location = New System.Drawing.Point(379, 11)
			Me.button1.Name = "button1"
			Me.button1.Size = New System.Drawing.Size(25, 23)
			Me.button1.TabIndex = 25
			Me.button1.UseVisualStyleBackColor = True
			' 
			' listBox2
			' 
			Me.listBox2.FormattingEnabled = True
			Me.listBox2.Location = New System.Drawing.Point(70, 13)
			Me.listBox2.Name = "listBox2"
			Me.listBox2.Size = New System.Drawing.Size(303, 17)
			Me.listBox2.TabIndex = 23
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(26, 16)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(38, 13)
			Me.label2.TabIndex = 24
			Me.label2.Text = "Modify"
			' 
			' pbImage
			' 
			Me.pbImage.BackColor = System.Drawing.Color.DimGray
			Me.pbImage.Dock = System.Windows.Forms.DockStyle.Fill
			Me.pbImage.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Image
			Me.pbImage.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None
			Me.pbImage.Location = New System.Drawing.Point(0, 0)
			Me.pbImage.Name = "pbImage"
			Me.pbImage.Size = New System.Drawing.Size(494, 694)
			Me.pbImage.TabIndex = 0
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.SystemColors.ActiveCaption
			Me.ClientSize = New System.Drawing.Size(935, 696)
			Me.Controls.Add(Me.splitContainer1)
			Me.Name = "Form1"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
			Me.Text = "Image Functions"
			Me.splitContainer1.Panel1.ResumeLayout(False)
			Me.splitContainer1.Panel2.ResumeLayout(False)
			DirectCast(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.splitContainer1.ResumeLayout(False)
			Me.splitContainer3.Panel1.ResumeLayout(False)
			Me.splitContainer3.Panel1.PerformLayout()
			Me.splitContainer3.Panel2.ResumeLayout(False)
			Me.splitContainer3.Panel2.PerformLayout()
			DirectCast(Me.splitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
			Me.splitContainer3.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private ilDefault As System.Windows.Forms.ImageList
		Private openFile As System.Windows.Forms.OpenFileDialog
		Private splitContainer1 As System.Windows.Forms.SplitContainer
		Private splitContainer3 As System.Windows.Forms.SplitContainer
		Private WithEvents btnLoadNewImage As System.Windows.Forms.Button
		Private label1 As System.Windows.Forms.Label
		Private WithEvents listBox1 As System.Windows.Forms.ListBox
		Private button1 As System.Windows.Forms.Button
		Private listBox2 As System.Windows.Forms.ListBox
		Private label2 As System.Windows.Forms.Label
		Private pbImage As Cyotek.Windows.Forms.ImageBox
	End Class
End Namespace

