<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptSALE_INVOICE
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
        Me.RBttnArea_Date = New System.Windows.Forms.RadioButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.RBttnDate = New System.Windows.Forms.RadioButton
        Me.RBttnInvoice = New System.Windows.Forms.RadioButton
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
        Me.Panel1.Controls.Add(Me.RBttnArea_Date)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.RBttnDate)
        Me.Panel1.Controls.Add(Me.RBttnInvoice)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(268, 139)
        Me.Panel1.TabIndex = 0
        '
        'RBttnArea_Date
        '
        Me.RBttnArea_Date.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnArea_Date.Location = New System.Drawing.Point(3, 101)
        Me.RBttnArea_Date.Name = "RBttnArea_Date"
        Me.RBttnArea_Date.Size = New System.Drawing.Size(214, 24)
        Me.RBttnArea_Date.TabIndex = 3
        Me.RBttnArea_Date.Text = "Area and Date Wise"
        Me.RBttnArea_Date.UseVisualStyleBackColor = True
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
        Me.Label3.Text = "Sales Invoice Options"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RBttnDate
        '
        Me.RBttnDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnDate.Location = New System.Drawing.Point(3, 71)
        Me.RBttnDate.Name = "RBttnDate"
        Me.RBttnDate.Size = New System.Drawing.Size(214, 24)
        Me.RBttnDate.TabIndex = 2
        Me.RBttnDate.Text = "Date Wise"
        Me.RBttnDate.UseVisualStyleBackColor = True
        '
        'RBttnInvoice
        '
        Me.RBttnInvoice.Checked = True
        Me.RBttnInvoice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnInvoice.Location = New System.Drawing.Point(3, 41)
        Me.RBttnInvoice.Name = "RBttnInvoice"
        Me.RBttnInvoice.Size = New System.Drawing.Size(214, 24)
        Me.RBttnInvoice.TabIndex = 1
        Me.RBttnInvoice.TabStop = True
        Me.RBttnInvoice.Text = "Invoice No Wise"
        Me.RBttnInvoice.UseVisualStyleBackColor = True
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
        'frmRptSALE_INVOICE
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
        Me.Name = "frmRptSALE_INVOICE"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SALES INVOICE OPTIONS"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BttnView As System.Windows.Forms.Button
    Friend WithEvents RBttnInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RBttnDate As System.Windows.Forms.RadioButton
    Friend WithEvents RBttnArea_Date As System.Windows.Forms.RadioButton
End Class
