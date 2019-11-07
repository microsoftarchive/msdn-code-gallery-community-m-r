<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptSALESMAN_SALE_DETAIL
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BttnClose = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.RBttnDate = New System.Windows.Forms.RadioButton
        Me.RBttnOverall = New System.Windows.Forms.RadioButton
        Me.BttnView = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BttnClose
        '
        Me.BttnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnClose.BackColor = System.Drawing.Color.LightBlue
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(177, 117)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.TabStop = False
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.RBttnDate)
        Me.Panel1.Controls.Add(Me.RBttnOverall)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(240, 99)
        Me.Panel1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.YellowGreen
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(238, 29)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "SALE DETAIL (SALES MAN)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RBttnDate
        '
        Me.RBttnDate.Checked = True
        Me.RBttnDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnDate.Location = New System.Drawing.Point(3, 62)
        Me.RBttnDate.Name = "RBttnDate"
        Me.RBttnDate.Size = New System.Drawing.Size(214, 24)
        Me.RBttnDate.TabIndex = 2
        Me.RBttnDate.TabStop = True
        Me.RBttnDate.Text = "Date Wise"
        Me.RBttnDate.UseVisualStyleBackColor = True
        '
        'RBttnOverall
        '
        Me.RBttnOverall.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnOverall.Location = New System.Drawing.Point(3, 32)
        Me.RBttnOverall.Name = "RBttnOverall"
        Me.RBttnOverall.Size = New System.Drawing.Size(214, 24)
        Me.RBttnOverall.TabIndex = 1
        Me.RBttnOverall.Text = "Overall"
        Me.RBttnOverall.UseVisualStyleBackColor = True
        '
        'BttnView
        '
        Me.BttnView.BackColor = System.Drawing.Color.LightBlue
        Me.BttnView.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnView.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnView.Location = New System.Drawing.Point(12, 117)
        Me.BttnView.Name = "BttnView"
        Me.BttnView.Size = New System.Drawing.Size(75, 31)
        Me.BttnView.TabIndex = 1
        Me.BttnView.Text = "&View"
        Me.BttnView.UseVisualStyleBackColor = False
        '
        'frmRptSALESMAN_SALE_DETAIL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(264, 158)
        Me.Controls.Add(Me.BttnView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BttnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmRptSALESMAN_SALE_DETAIL"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SALE DETAIL (SALES MAN)"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BttnView As System.Windows.Forms.Button
    Friend WithEvents RBttnOverall As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RBttnDate As System.Windows.Forms.RadioButton
End Class
