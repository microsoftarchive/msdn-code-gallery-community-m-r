<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnLarge = New System.Windows.Forms.Button()
        Me.btnSmall = New System.Windows.Forms.Button()
        Me.lblNumbers1 = New System.Windows.Forms.Label()
        Me.lblNumbers2 = New System.Windows.Forms.Label()
        Me.lblNumbers3 = New System.Windows.Forms.Label()
        Me.lblNumbers4 = New System.Windows.Forms.Label()
        Me.lblNumbers5 = New System.Windows.Forms.Label()
        Me.lblNumbers6 = New System.Windows.Forms.Label()
        Me.btnNewGame = New System.Windows.Forms.Button()
        Me.lblTargetNumber = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSolution = New System.Windows.Forms.Label()
        Me.Clock1 = New Numbers_Game.CountdownClock()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSolution = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnStopClock = New System.Windows.Forms.Button()
        CType(Me.Clock1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLarge
        '
        Me.btnLarge.Location = New System.Drawing.Point(127, 108)
        Me.btnLarge.Name = "btnLarge"
        Me.btnLarge.Size = New System.Drawing.Size(102, 23)
        Me.btnLarge.TabIndex = 0
        Me.btnLarge.Text = "Large"
        Me.btnLarge.UseVisualStyleBackColor = True
        '
        'btnSmall
        '
        Me.btnSmall.Location = New System.Drawing.Point(235, 108)
        Me.btnSmall.Name = "btnSmall"
        Me.btnSmall.Size = New System.Drawing.Size(102, 23)
        Me.btnSmall.TabIndex = 1
        Me.btnSmall.Text = "Small"
        Me.btnSmall.UseVisualStyleBackColor = True
        '
        'lblNumbers1
        '
        Me.lblNumbers1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumbers1.Location = New System.Drawing.Point(127, 57)
        Me.lblNumbers1.Name = "lblNumbers1"
        Me.lblNumbers1.Size = New System.Drawing.Size(30, 25)
        Me.lblNumbers1.TabIndex = 2
        Me.lblNumbers1.Text = "100"
        Me.lblNumbers1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumbers2
        '
        Me.lblNumbers2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumbers2.Location = New System.Drawing.Point(163, 57)
        Me.lblNumbers2.Name = "lblNumbers2"
        Me.lblNumbers2.Size = New System.Drawing.Size(30, 25)
        Me.lblNumbers2.TabIndex = 3
        Me.lblNumbers2.Text = "100"
        Me.lblNumbers2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumbers3
        '
        Me.lblNumbers3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumbers3.Location = New System.Drawing.Point(199, 57)
        Me.lblNumbers3.Name = "lblNumbers3"
        Me.lblNumbers3.Size = New System.Drawing.Size(30, 25)
        Me.lblNumbers3.TabIndex = 4
        Me.lblNumbers3.Text = "100"
        Me.lblNumbers3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumbers4
        '
        Me.lblNumbers4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumbers4.Location = New System.Drawing.Point(235, 57)
        Me.lblNumbers4.Name = "lblNumbers4"
        Me.lblNumbers4.Size = New System.Drawing.Size(30, 25)
        Me.lblNumbers4.TabIndex = 5
        Me.lblNumbers4.Text = "100"
        Me.lblNumbers4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumbers5
        '
        Me.lblNumbers5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumbers5.Location = New System.Drawing.Point(271, 57)
        Me.lblNumbers5.Name = "lblNumbers5"
        Me.lblNumbers5.Size = New System.Drawing.Size(30, 25)
        Me.lblNumbers5.TabIndex = 6
        Me.lblNumbers5.Text = "100"
        Me.lblNumbers5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNumbers6
        '
        Me.lblNumbers6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumbers6.Location = New System.Drawing.Point(307, 57)
        Me.lblNumbers6.Name = "lblNumbers6"
        Me.lblNumbers6.Size = New System.Drawing.Size(30, 25)
        Me.lblNumbers6.TabIndex = 7
        Me.lblNumbers6.Text = "100"
        Me.lblNumbers6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNewGame
        '
        Me.btnNewGame.Location = New System.Drawing.Point(236, 341)
        Me.btnNewGame.Name = "btnNewGame"
        Me.btnNewGame.Size = New System.Drawing.Size(102, 23)
        Me.btnNewGame.TabIndex = 8
        Me.btnNewGame.Text = "New Game"
        Me.btnNewGame.UseVisualStyleBackColor = True
        '
        'lblTargetNumber
        '
        Me.lblTargetNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTargetNumber.Location = New System.Drawing.Point(163, 11)
        Me.lblTargetNumber.Name = "lblTargetNumber"
        Me.lblTargetNumber.Size = New System.Drawing.Size(30, 25)
        Me.lblTargetNumber.TabIndex = 9
        Me.lblTargetNumber.Text = "100"
        Me.lblTargetNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(124, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Target:"
        '
        'lblSolution
        '
        Me.lblSolution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSolution.Location = New System.Drawing.Point(12, 148)
        Me.lblSolution.Name = "lblSolution"
        Me.lblSolution.Size = New System.Drawing.Size(325, 77)
        Me.lblSolution.TabIndex = 11
        Me.lblSolution.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Clock1
        '
        Me.Clock1.Image = CType(resources.GetObject("Clock1.Image"), System.Drawing.Image)
        Me.Clock1.Location = New System.Drawing.Point(12, 11)
        Me.Clock1.Name = "Clock1"
        Me.Clock1.Size = New System.Drawing.Size(100, 100)
        Me.Clock1.TabIndex = 12
        Me.Clock1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(9, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Solution:"
        '
        'txtSolution
        '
        Me.txtSolution.Location = New System.Drawing.Point(12, 258)
        Me.txtSolution.Multiline = True
        Me.txtSolution.Name = "txtSolution"
        Me.txtSolution.Size = New System.Drawing.Size(325, 77)
        Me.txtSolution.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(9, 242)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Your solution:"
        '
        'btnStopClock
        '
        Me.btnStopClock.Enabled = False
        Me.btnStopClock.Location = New System.Drawing.Point(127, 341)
        Me.btnStopClock.Name = "btnStopClock"
        Me.btnStopClock.Size = New System.Drawing.Size(102, 23)
        Me.btnStopClock.TabIndex = 16
        Me.btnStopClock.Text = "Stop the clock"
        Me.btnStopClock.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 376)
        Me.Controls.Add(Me.btnStopClock)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtSolution)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Clock1)
        Me.Controls.Add(Me.lblSolution)
        Me.Controls.Add(Me.lblTargetNumber)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnNewGame)
        Me.Controls.Add(Me.lblNumbers6)
        Me.Controls.Add(Me.lblNumbers5)
        Me.Controls.Add(Me.lblNumbers4)
        Me.Controls.Add(Me.lblNumbers3)
        Me.Controls.Add(Me.lblNumbers2)
        Me.Controls.Add(Me.lblNumbers1)
        Me.Controls.Add(Me.btnSmall)
        Me.Controls.Add(Me.btnLarge)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Numbers Game"
        CType(Me.Clock1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnLarge As Button
    Friend WithEvents btnSmall As Button
    Friend WithEvents lblNumbers1 As Label
    Friend WithEvents lblNumbers2 As Label
    Friend WithEvents lblNumbers3 As Label
    Friend WithEvents lblNumbers4 As Label
    Friend WithEvents lblNumbers5 As Label
    Friend WithEvents lblNumbers6 As Label
    Friend WithEvents btnNewGame As Button
    Friend WithEvents lblTargetNumber As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblSolution As Label
    Friend WithEvents Clock1 As CountdownClock
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSolution As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnStopClock As Button
End Class
