<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainForm
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdSimulation3 = New System.Windows.Forms.Button()
        Me.cmdSimulation2 = New System.Windows.Forms.Button()
        Me.cmdSimulation1 = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdSimulation3)
        Me.Panel1.Controls.Add(Me.cmdSimulation2)
        Me.Panel1.Controls.Add(Me.cmdSimulation1)
        Me.Panel1.Controls.Add(Me.cmdClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 292)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(394, 52)
        Me.Panel1.TabIndex = 1
        '
        'cmdSimulation3
        '
        Me.cmdSimulation3.Location = New System.Drawing.Point(184, 8)
        Me.cmdSimulation3.Name = "cmdSimulation3"
        Me.cmdSimulation3.Size = New System.Drawing.Size(75, 23)
        Me.cmdSimulation3.TabIndex = 2
        Me.cmdSimulation3.Text = "Simulation 3"
        Me.cmdSimulation3.UseVisualStyleBackColor = True
        '
        'cmdSimulation2
        '
        Me.cmdSimulation2.Location = New System.Drawing.Point(98, 8)
        Me.cmdSimulation2.Name = "cmdSimulation2"
        Me.cmdSimulation2.Size = New System.Drawing.Size(75, 23)
        Me.cmdSimulation2.TabIndex = 1
        Me.cmdSimulation2.Text = "Simulation 2"
        Me.cmdSimulation2.UseVisualStyleBackColor = True
        '
        'cmdSimulation1
        '
        Me.cmdSimulation1.Location = New System.Drawing.Point(12, 8)
        Me.cmdSimulation1.Name = "cmdSimulation1"
        Me.cmdSimulation1.Size = New System.Drawing.Size(75, 23)
        Me.cmdSimulation1.TabIndex = 0
        Me.cmdSimulation1.Text = "Simulation 1"
        Me.cmdSimulation1.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Location = New System.Drawing.Point(307, 8)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(394, 292)
        Me.WebBrowser1.TabIndex = 0
        '
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 344)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SQL Parameter peek demonstration "
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdSimulation1 As System.Windows.Forms.Button
    Friend WithEvents cmdSimulation2 As System.Windows.Forms.Button
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents cmdSimulation3 As System.Windows.Forms.Button

End Class
