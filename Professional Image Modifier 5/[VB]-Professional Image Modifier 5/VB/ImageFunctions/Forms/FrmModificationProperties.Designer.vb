Namespace ImageFunctions.Forms
	Partial Public Class FrmModificationProperties
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
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.btnProcess = New System.Windows.Forms.Button()
			Me.btnReset = New System.Windows.Forms.Button()
			Me.ControlPanel = New System.Windows.Forms.Panel()
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' panel1
			' 
			Me.panel1.BackColor = System.Drawing.Color.LightSteelBlue
			Me.panel1.Controls.Add(Me.btnProcess)
			Me.panel1.Controls.Add(Me.btnReset)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.panel1.Location = New System.Drawing.Point(0, 353)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(704, 37)
			Me.panel1.TabIndex = 0
			' 
			' btnProcess
			' 
			Me.btnProcess.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnProcess.Location = New System.Drawing.Point(617, 8)
			Me.btnProcess.Name = "btnProcess"
			Me.btnProcess.Size = New System.Drawing.Size(75, 23)
			Me.btnProcess.TabIndex = 1
			Me.btnProcess.Text = "Process"
			Me.btnProcess.UseVisualStyleBackColor = True
'			Me.btnProcess.Click += New System.EventHandler(Me.btnProcess_Click)
			' 
			' btnReset
			' 
			Me.btnReset.Location = New System.Drawing.Point(12, 8)
			Me.btnReset.Name = "btnReset"
			Me.btnReset.Size = New System.Drawing.Size(75, 23)
			Me.btnReset.TabIndex = 0
			Me.btnReset.Text = "Reset"
			Me.btnReset.UseVisualStyleBackColor = True
'			Me.btnReset.Click += New System.EventHandler(Me.btnReset_Click)
			' 
			' ControlPanel
			' 
			Me.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill
			Me.ControlPanel.Location = New System.Drawing.Point(0, 0)
			Me.ControlPanel.Name = "ControlPanel"
			Me.ControlPanel.Size = New System.Drawing.Size(704, 353)
			Me.ControlPanel.TabIndex = 1
			' 
			' FrmModificationProperties
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(704, 390)
			Me.ControlBox = False
			Me.Controls.Add(Me.ControlPanel)
			Me.Controls.Add(Me.panel1)
			Me.DoubleBuffered = True
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
			Me.Name = "FrmModificationProperties"
			Me.Text = "Modification Properties"
			Me.panel1.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private panel1 As System.Windows.Forms.Panel
		Private WithEvents btnProcess As System.Windows.Forms.Button
		Private WithEvents btnReset As System.Windows.Forms.Button
		Private ControlPanel As System.Windows.Forms.Panel
	End Class
End Namespace