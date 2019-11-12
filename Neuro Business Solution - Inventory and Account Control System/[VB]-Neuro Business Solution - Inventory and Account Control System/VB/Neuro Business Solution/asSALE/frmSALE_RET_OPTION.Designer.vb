<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSALE_RET_OPTION
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
        Me.BttnWholeInvoice = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnWithoutReference = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnPartialInvoice = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BttnWholeInvoice
        '
        Me.BttnWholeInvoice.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BttnWholeInvoice.BackColor = System.Drawing.Color.LightBlue
        Me.BttnWholeInvoice.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnWholeInvoice.FlatAppearance.BorderColor = System.Drawing.Color.Teal
        Me.BttnWholeInvoice.FlatAppearance.BorderSize = 2
        Me.BttnWholeInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BttnWholeInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BttnWholeInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BttnWholeInvoice.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnWholeInvoice.Location = New System.Drawing.Point(3, 45)
        Me.BttnWholeInvoice.Name = "BttnWholeInvoice"
        Me.BttnWholeInvoice.Size = New System.Drawing.Size(260, 31)
        Me.BttnWholeInvoice.TabIndex = 1
        Me.BttnWholeInvoice.Text = "&Whole Invoice"
        Me.BttnWholeInvoice.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnWithoutReference)
        Me.Panel1.Controls.Add(Me.BttnClose)
        Me.Panel1.Controls.Add(Me.BttnPartialInvoice)
        Me.Panel1.Controls.Add(Me.BttnWholeInvoice)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(268, 206)
        Me.Panel1.TabIndex = 0
        '
        'BttnWithoutReference
        '
        Me.BttnWithoutReference.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BttnWithoutReference.BackColor = System.Drawing.Color.LightBlue
        Me.BttnWithoutReference.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnWithoutReference.FlatAppearance.BorderColor = System.Drawing.Color.Olive
        Me.BttnWithoutReference.FlatAppearance.BorderSize = 2
        Me.BttnWithoutReference.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BttnWithoutReference.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BttnWithoutReference.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BttnWithoutReference.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnWithoutReference.Location = New System.Drawing.Point(3, 119)
        Me.BttnWithoutReference.Name = "BttnWithoutReference"
        Me.BttnWithoutReference.Size = New System.Drawing.Size(260, 31)
        Me.BttnWithoutReference.TabIndex = 3
        Me.BttnWithoutReference.Text = "Without &Reference"
        Me.BttnWithoutReference.UseVisualStyleBackColor = False
        '
        'BttnClose
        '
        Me.BttnClose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BttnClose.BackColor = System.Drawing.Color.LightPink
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(188, 170)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 31)
        Me.BttnClose.TabIndex = 4
        Me.BttnClose.TabStop = False
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = False
        '
        'BttnPartialInvoice
        '
        Me.BttnPartialInvoice.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BttnPartialInvoice.BackColor = System.Drawing.Color.LightBlue
        Me.BttnPartialInvoice.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPartialInvoice.FlatAppearance.BorderColor = System.Drawing.Color.Teal
        Me.BttnPartialInvoice.FlatAppearance.BorderSize = 2
        Me.BttnPartialInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BttnPartialInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BttnPartialInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BttnPartialInvoice.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPartialInvoice.Location = New System.Drawing.Point(3, 82)
        Me.BttnPartialInvoice.Name = "BttnPartialInvoice"
        Me.BttnPartialInvoice.Size = New System.Drawing.Size(260, 31)
        Me.BttnPartialInvoice.TabIndex = 2
        Me.BttnPartialInvoice.Text = "&Partial Invoice"
        Me.BttnPartialInvoice.UseVisualStyleBackColor = False
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
        Me.Label3.Text = "Sales Return Options"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmSALE_RET_OPTION
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(292, 230)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSALE_RET_OPTION"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SALE RETURN OPTIONS"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BttnWholeInvoice As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnWithoutReference As System.Windows.Forms.Button
    Friend WithEvents BttnPartialInvoice As System.Windows.Forms.Button
End Class
