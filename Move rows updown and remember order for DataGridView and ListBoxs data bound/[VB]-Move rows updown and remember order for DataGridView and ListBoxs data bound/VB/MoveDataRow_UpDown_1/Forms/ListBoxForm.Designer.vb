<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListBoxForm
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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.cmdMoveDown = New System.Windows.Forms.Button()
        Me.cmdMoveUp = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8"})
        Me.ListBox1.Location = New System.Drawing.Point(12, 12)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(270, 108)
        Me.ListBox1.TabIndex = 0
        '
        'cmdMoveDown
        '
        Me.cmdMoveDown.Image = Global.MoveDataRow_UpDown.My.Resources.Resources.DnArrow
        Me.cmdMoveDown.Location = New System.Drawing.Point(300, 68)
        Me.cmdMoveDown.Name = "cmdMoveDown"
        Me.cmdMoveDown.Size = New System.Drawing.Size(47, 32)
        Me.cmdMoveDown.TabIndex = 2
        Me.cmdMoveDown.UseVisualStyleBackColor = True
        '
        'cmdMoveUp
        '
        Me.cmdMoveUp.Image = Global.MoveDataRow_UpDown.My.Resources.Resources.UpArrow
        Me.cmdMoveUp.Location = New System.Drawing.Point(300, 30)
        Me.cmdMoveUp.Name = "cmdMoveUp"
        Me.cmdMoveUp.Size = New System.Drawing.Size(47, 32)
        Me.cmdMoveUp.TabIndex = 1
        Me.cmdMoveUp.UseVisualStyleBackColor = True
        '
        'frmListBoxForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 151)
        Me.Controls.Add(Me.cmdMoveDown)
        Me.Controls.Add(Me.cmdMoveUp)
        Me.Controls.Add(Me.ListBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmListBoxForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Move Row Up/Down Demo - MS-Access/ListBox"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents cmdMoveUp As System.Windows.Forms.Button
    Friend WithEvents cmdMoveDown As System.Windows.Forms.Button

End Class
