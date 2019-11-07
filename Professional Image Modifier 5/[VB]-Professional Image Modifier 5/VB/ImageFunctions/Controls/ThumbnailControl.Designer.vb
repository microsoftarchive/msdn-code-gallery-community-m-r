Namespace ImageFunctions.Controls
	Partial Public Class ThumbnailControl
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

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.lblSize = New System.Windows.Forms.Label()
			Me.lblName = New System.Windows.Forms.Label()
			Me.pbImage = New System.Windows.Forms.PictureBox()
			DirectCast(Me.pbImage, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' lblSize
			' 
			Me.lblSize.Location = New System.Drawing.Point(4, 132)
			Me.lblSize.Name = "lblSize"
			Me.lblSize.Size = New System.Drawing.Size(139, 14)
			Me.lblSize.TabIndex = 5
			Me.lblSize.Text = "N/A"
			' 
			' lblName
			' 
			Me.lblName.Location = New System.Drawing.Point(4, 118)
			Me.lblName.Name = "lblName"
			Me.lblName.Size = New System.Drawing.Size(139, 14)
			Me.lblName.TabIndex = 4
			Me.lblName.Text = "N/A"
			' 
			' pbImage
			' 
			Me.pbImage.Location = New System.Drawing.Point(3, 4)
			Me.pbImage.Name = "pbImage"
			Me.pbImage.Size = New System.Drawing.Size(140, 107)
			Me.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
			Me.pbImage.TabIndex = 3
			Me.pbImage.TabStop = False
'			Me.pbImage.Click += New System.EventHandler(Me.pbImage_Click)
'			Me.pbImage.MouseClick += New System.Windows.Forms.MouseEventHandler(Me.pbImage_MouseClick)
'			Me.pbImage.MouseEnter += New System.EventHandler(Me.pbImage_MouseEnter)
'			Me.pbImage.MouseLeave += New System.EventHandler(Me.pbImage_MouseLeave)
			' 
			' ThumbnailControl
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.BackColor = System.Drawing.Color.Transparent
			Me.Controls.Add(Me.lblSize)
			Me.Controls.Add(Me.lblName)
			Me.Controls.Add(Me.pbImage)
			Me.Name = "ThumbnailControl"
			Me.Size = New System.Drawing.Size(146, 151)
			DirectCast(Me.pbImage, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private lblSize As System.Windows.Forms.Label
		Private lblName As System.Windows.Forms.Label
		Private WithEvents pbImage As System.Windows.Forms.PictureBox
	End Class
End Namespace
