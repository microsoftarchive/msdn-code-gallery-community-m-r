Namespace ImageFunctions.Controls
	Partial Public Class SusanCornerProperties
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
			Me.trackDifferenceThreshold = New System.Windows.Forms.TrackBar()
			Me.label2 = New System.Windows.Forms.Label()
			Me.lblDifferenceThreshold = New System.Windows.Forms.Label()
			Me.lblGeometricalThreshold = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.trackGeometricalThreshold = New System.Windows.Forms.TrackBar()
			DirectCast(Me.trackDifferenceThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
			DirectCast(Me.trackGeometricalThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
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
			Me.label1.TabIndex = 0
			Me.label1.Text = "Susan Corner Detection Settings"
			Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' trackDifferenceThreshold
			' 
			Me.trackDifferenceThreshold.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.trackDifferenceThreshold.Location = New System.Drawing.Point(7, 59)
			Me.trackDifferenceThreshold.Maximum = 100
			Me.trackDifferenceThreshold.Name = "trackDifferenceThreshold"
			Me.trackDifferenceThreshold.Size = New System.Drawing.Size(342, 45)
			Me.trackDifferenceThreshold.TabIndex = 3
'			Me.trackDifferenceThreshold.ValueChanged += New System.EventHandler(Me.trackDifferenceThreshold_ValueChanged)
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(7, 43)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(106, 13)
			Me.label2.TabIndex = 4
			Me.label2.Text = "Difference Threshold"
			' 
			' lblDifferenceThreshold
			' 
			Me.lblDifferenceThreshold.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.lblDifferenceThreshold.AutoSize = True
			Me.lblDifferenceThreshold.Location = New System.Drawing.Point(119, 43)
			Me.lblDifferenceThreshold.Name = "lblDifferenceThreshold"
			Me.lblDifferenceThreshold.Size = New System.Drawing.Size(13, 13)
			Me.lblDifferenceThreshold.TabIndex = 5
			Me.lblDifferenceThreshold.Text = "0"
			' 
			' lblGeometricalThreshold
			' 
			Me.lblGeometricalThreshold.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.lblGeometricalThreshold.AutoSize = True
			Me.lblGeometricalThreshold.Location = New System.Drawing.Point(119, 107)
			Me.lblGeometricalThreshold.Name = "lblGeometricalThreshold"
			Me.lblGeometricalThreshold.Size = New System.Drawing.Size(13, 13)
			Me.lblGeometricalThreshold.TabIndex = 8
			Me.lblGeometricalThreshold.Text = "0"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(7, 107)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(113, 13)
			Me.label4.TabIndex = 7
			Me.label4.Text = "Geometrical Threshold"
			' 
			' trackGeometricalThreshold
			' 
			Me.trackGeometricalThreshold.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.trackGeometricalThreshold.Location = New System.Drawing.Point(7, 123)
			Me.trackGeometricalThreshold.Maximum = 100
			Me.trackGeometricalThreshold.Name = "trackGeometricalThreshold"
			Me.trackGeometricalThreshold.Size = New System.Drawing.Size(342, 45)
			Me.trackGeometricalThreshold.TabIndex = 6
'			Me.trackGeometricalThreshold.ValueChanged += New System.EventHandler(Me.trackGeometricalThreshold_ValueChanged)
			' 
			' SusanCornerProperties
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.lblGeometricalThreshold)
			Me.Controls.Add(Me.label4)
			Me.Controls.Add(Me.trackGeometricalThreshold)
			Me.Controls.Add(Me.lblDifferenceThreshold)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.trackDifferenceThreshold)
			Me.Controls.Add(Me.label1)
			Me.Name = "SusanCornerProperties"
			Me.Size = New System.Drawing.Size(352, 166)
			DirectCast(Me.trackDifferenceThreshold, System.ComponentModel.ISupportInitialize).EndInit()
			DirectCast(Me.trackGeometricalThreshold, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private label1 As System.Windows.Forms.Label
		Private WithEvents trackDifferenceThreshold As System.Windows.Forms.TrackBar
		Private label2 As System.Windows.Forms.Label
		Private lblDifferenceThreshold As System.Windows.Forms.Label
		Private lblGeometricalThreshold As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private WithEvents trackGeometricalThreshold As System.Windows.Forms.TrackBar
	End Class
End Namespace
