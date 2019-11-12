Namespace ImageFunctions.Forms
	Partial Public Class FrmCommandBox
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
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.lblFlash = New System.Windows.Forms.Label()
			Me.label1 = New System.Windows.Forms.Label()
			Me.tbCommandInput = New System.Windows.Forms.TextBox()
			Me.timerFlash = New System.Windows.Forms.Timer(Me.components)
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' panel1
			' 
			Me.panel1.BackColor = System.Drawing.Color.Black
			Me.panel1.Controls.Add(Me.lblFlash)
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Controls.Add(Me.tbCommandInput)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
			Me.panel1.Location = New System.Drawing.Point(0, 0)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(671, 23)
			Me.panel1.TabIndex = 2
			' 
			' lblFlash
			' 
			Me.lblFlash.BackColor = System.Drawing.Color.Black
			Me.lblFlash.Dock = System.Windows.Forms.DockStyle.Left
			Me.lblFlash.ForeColor = System.Drawing.SystemColors.MenuHighlight
			Me.lblFlash.Location = New System.Drawing.Point(54, 0)
			Me.lblFlash.Name = "lblFlash"
			Me.lblFlash.Size = New System.Drawing.Size(10, 23)
			Me.lblFlash.TabIndex = 2
			Me.lblFlash.Text = ":"
			Me.lblFlash.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' label1
			' 
			Me.label1.BackColor = System.Drawing.Color.Black
			Me.label1.Dock = System.Windows.Forms.DockStyle.Left
			Me.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight
			Me.label1.Location = New System.Drawing.Point(0, 0)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(54, 23)
			Me.label1.TabIndex = 1
			Me.label1.Text = "Command"
			Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			' 
			' tbCommandInput
			' 
			Me.tbCommandInput.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbCommandInput.BackColor = System.Drawing.Color.Black
			Me.tbCommandInput.BorderStyle = System.Windows.Forms.BorderStyle.None
			Me.tbCommandInput.ForeColor = System.Drawing.SystemColors.MenuHighlight
			Me.tbCommandInput.Location = New System.Drawing.Point(62, 5)
			Me.tbCommandInput.Name = "tbCommandInput"
			Me.tbCommandInput.Size = New System.Drawing.Size(599, 13)
			Me.tbCommandInput.TabIndex = 0
			' 
			' timerFlash
			' 
			Me.timerFlash.Enabled = True
			Me.timerFlash.Interval = 500
'			Me.timerFlash.Tick += New System.EventHandler(Me.timerFlash_Tick_1)
			' 
			' FrmCommandBox
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(671, 24)
			Me.ControlBox = False
			Me.Controls.Add(Me.panel1)
			Me.DoubleBuffered = True
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
			Me.Name = "FrmCommandBox"
			Me.Text = "Command Box"
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private panel1 As System.Windows.Forms.Panel
		Private lblFlash As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private tbCommandInput As System.Windows.Forms.TextBox
		Private WithEvents timerFlash As System.Windows.Forms.Timer
	End Class
End Namespace