Namespace ImageFunctions.Controls
	Partial Public Class HarrisCornerProperties
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
			Me.label1 = New System.Windows.Forms.Label()
			Me.lblWindow = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.trackSigma = New System.Windows.Forms.TrackBar()
			Me.lblThreshold = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.trackThreshold = New System.Windows.Forms.TrackBar()
			DirectCast(Me.trackSigma, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.trackThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' label1
			' 
			Me.label1.BackColor = System.Drawing.SystemColors.ActiveCaption
			Me.label1.Dock = System.Windows.Forms.DockStyle.Top
			Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label1.Location = New System.Drawing.Point(0, 0)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(352, 23)
			Me.label1.TabIndex = 10
			Me.label1.Text = "Harris Corner Detection Settings"
			Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' lblWindow
			' 
			Me.lblWindow.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.lblWindow.AutoSize = True
			Me.lblWindow.Location = New System.Drawing.Point(82, 107)
			Me.lblWindow.Name = "lblWindow"
			Me.lblWindow.Size = New System.Drawing.Size(13, 13)
			Me.lblWindow.TabIndex = 21
			Me.lblWindow.Text = "0"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(7, 107)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(36, 13)
			Me.label4.TabIndex = 20
			Me.label4.Text = "Sigma"
			' 
			' trackSigma
			' 
			Me.trackSigma.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.trackSigma.LargeChange = 1
			Me.trackSigma.Location = New System.Drawing.Point(7, 123)
			Me.trackSigma.Name = "trackSigma"
			Me.trackSigma.Size = New System.Drawing.Size(346, 45)
			Me.trackSigma.TabIndex = 19
'			Me.trackSigma.ValueChanged += New System.EventHandler(Me.trackSigma_ValueChanged)
			' 
			' lblThreshold
			' 
			Me.lblThreshold.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.lblThreshold.AutoSize = True
			Me.lblThreshold.Location = New System.Drawing.Point(82, 43)
			Me.lblThreshold.Name = "lblThreshold"
			Me.lblThreshold.Size = New System.Drawing.Size(13, 13)
			Me.lblThreshold.TabIndex = 18
			Me.lblThreshold.Text = "0"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(7, 43)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(54, 13)
			Me.label2.TabIndex = 17
			Me.label2.Text = "Threshold"
			' 
			' trackThreshold
			' 
			Me.trackThreshold.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.trackThreshold.LargeChange = 1
			Me.trackThreshold.Location = New System.Drawing.Point(7, 59)
			Me.trackThreshold.Maximum = 20
			Me.trackThreshold.Name = "trackThreshold"
			Me.trackThreshold.Size = New System.Drawing.Size(346, 45)
			Me.trackThreshold.TabIndex = 16
'			Me.trackThreshold.ValueChanged += New System.EventHandler(Me.trackThreshold_ValueChanged_1)
			' 
			' HarrisCornerProperties
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.lblWindow)
			Me.Controls.Add(Me.label4)
			Me.Controls.Add(Me.trackSigma)
			Me.Controls.Add(Me.lblThreshold)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.trackThreshold)
			Me.Controls.Add(Me.label1)
			Me.Name = "HarrisCornerProperties"
			Me.Size = New System.Drawing.Size(352, 166)
			DirectCast(Me.trackSigma, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.trackThreshold, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private label1 As System.Windows.Forms.Label
		Private lblWindow As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private WithEvents trackSigma As System.Windows.Forms.TrackBar
		Private lblThreshold As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private WithEvents trackThreshold As System.Windows.Forms.TrackBar
	End Class
End Namespace
