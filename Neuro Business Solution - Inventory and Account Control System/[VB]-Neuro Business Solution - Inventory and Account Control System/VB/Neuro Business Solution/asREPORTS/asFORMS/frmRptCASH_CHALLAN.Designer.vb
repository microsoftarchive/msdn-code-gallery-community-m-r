<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptCASH_CHALLAN
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
        Me.ChkCredit = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.RBttnArea = New System.Windows.Forms.RadioButton
        Me.RBttnAllClient = New System.Windows.Forms.RadioButton
        Me.BttnView = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BttnClose
        '
        Me.BttnClose.BackColor = System.Drawing.Color.LightBlue
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(205, 157)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.TabStop = False
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.ChkCredit)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.RBttnArea)
        Me.Panel1.Controls.Add(Me.RBttnAllClient)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(268, 139)
        Me.Panel1.TabIndex = 0
        '
        'ChkCredit
        '
        Me.ChkCredit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ChkCredit.Location = New System.Drawing.Point(3, 92)
        Me.ChkCredit.Name = "ChkCredit"
        Me.ChkCredit.Size = New System.Drawing.Size(214, 24)
        Me.ChkCredit.TabIndex = 3
        Me.ChkCredit.Text = "Credit Between"
        Me.ChkCredit.UseVisualStyleBackColor = True
        Me.ChkCredit.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(266, 29)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Cash Challan Options"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RBttnArea
        '
        Me.RBttnArea.Checked = True
        Me.RBttnArea.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnArea.Location = New System.Drawing.Point(3, 62)
        Me.RBttnArea.Name = "RBttnArea"
        Me.RBttnArea.Size = New System.Drawing.Size(214, 24)
        Me.RBttnArea.TabIndex = 2
        Me.RBttnArea.TabStop = True
        Me.RBttnArea.Text = "Area Wise"
        Me.RBttnArea.UseVisualStyleBackColor = True
        '
        'RBttnAllClient
        '
        Me.RBttnAllClient.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnAllClient.Location = New System.Drawing.Point(3, 32)
        Me.RBttnAllClient.Name = "RBttnAllClient"
        Me.RBttnAllClient.Size = New System.Drawing.Size(214, 24)
        Me.RBttnAllClient.TabIndex = 1
        Me.RBttnAllClient.Text = "All Clients"
        Me.RBttnAllClient.UseVisualStyleBackColor = True
        '
        'BttnView
        '
        Me.BttnView.BackColor = System.Drawing.Color.LightBlue
        Me.BttnView.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnView.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnView.Location = New System.Drawing.Point(12, 157)
        Me.BttnView.Name = "BttnView"
        Me.BttnView.Size = New System.Drawing.Size(75, 31)
        Me.BttnView.TabIndex = 1
        Me.BttnView.Text = "&View"
        Me.BttnView.UseVisualStyleBackColor = False
        '
        'frmRptCASH_CHALLAN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(292, 199)
        Me.Controls.Add(Me.BttnView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BttnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmRptCASH_CHALLAN"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CASH CHALLAN OPTIONS"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BttnView As System.Windows.Forms.Button
    Friend WithEvents RBttnAllClient As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RBttnArea As System.Windows.Forms.RadioButton
    Friend WithEvents ChkCredit As System.Windows.Forms.CheckBox
End Class
