Namespace ImageFunctions.Forms
	Partial Public Class FrmFileLoader
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
			Me.ThumbnailLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
			Me.SuspendLayout()
			' 
			' ThumbnailLayoutPanel
			' 
			Me.ThumbnailLayoutPanel.AutoScroll = True
			Me.ThumbnailLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
			Me.ThumbnailLayoutPanel.Location = New System.Drawing.Point(0, 0)
			Me.ThumbnailLayoutPanel.Name = "ThumbnailLayoutPanel"
			Me.ThumbnailLayoutPanel.Size = New System.Drawing.Size(354, 316)
			Me.ThumbnailLayoutPanel.TabIndex = 0
			' 
			' FrmFileLoader
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(354, 316)
			Me.ControlBox = False
			Me.Controls.Add(Me.ThumbnailLayoutPanel)
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
			Me.MaximizeBox = False
			Me.Name = "FrmFileLoader"
			Me.ShowIcon = False
			Me.ShowInTaskbar = False
			Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
			Me.Text = "File Loader"
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private ThumbnailLayoutPanel As System.Windows.Forms.FlowLayoutPanel

	End Class
End Namespace