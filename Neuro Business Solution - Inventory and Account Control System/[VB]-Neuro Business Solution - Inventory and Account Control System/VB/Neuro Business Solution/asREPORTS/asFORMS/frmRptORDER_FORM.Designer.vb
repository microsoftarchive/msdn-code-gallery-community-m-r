<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptORDER_FORM
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
        Me.RBttnCategory = New System.Windows.Forms.RadioButton
        Me.RBttnCompany = New System.Windows.Forms.RadioButton
        Me.BttnView = New System.Windows.Forms.Button
        Me.BttnPrint = New System.Windows.Forms.Button
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
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.RBttnCategory)
        Me.Panel1.Controls.Add(Me.RBttnCompany)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(268, 139)
        Me.Panel1.TabIndex = 0
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
        Me.Label3.Text = "Order Form Options"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RBttnCategory
        '
        Me.RBttnCategory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnCategory.Location = New System.Drawing.Point(3, 82)
        Me.RBttnCategory.Name = "RBttnCategory"
        Me.RBttnCategory.Size = New System.Drawing.Size(214, 24)
        Me.RBttnCategory.TabIndex = 2
        Me.RBttnCategory.Text = "Item's Category Wise"
        Me.RBttnCategory.UseVisualStyleBackColor = True
        '
        'RBttnCompany
        '
        Me.RBttnCompany.Checked = True
        Me.RBttnCompany.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBttnCompany.Location = New System.Drawing.Point(3, 52)
        Me.RBttnCompany.Name = "RBttnCompany"
        Me.RBttnCompany.Size = New System.Drawing.Size(214, 24)
        Me.RBttnCompany.TabIndex = 1
        Me.RBttnCompany.TabStop = True
        Me.RBttnCompany.Text = "Company Wise"
        Me.RBttnCompany.UseVisualStyleBackColor = True
        '
        'BttnView
        '
        Me.BttnView.BackColor = System.Drawing.Color.LightBlue
        Me.BttnView.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnView.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnView.Location = New System.Drawing.Point(109, 157)
        Me.BttnView.Name = "BttnView"
        Me.BttnView.Size = New System.Drawing.Size(75, 31)
        Me.BttnView.TabIndex = 1
        Me.BttnView.Text = "&View"
        Me.BttnView.UseVisualStyleBackColor = False
        '
        'BttnPrint
        '
        Me.BttnPrint.BackColor = System.Drawing.Color.LightBlue
        Me.BttnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrint.Location = New System.Drawing.Point(12, 157)
        Me.BttnPrint.Name = "BttnPrint"
        Me.BttnPrint.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrint.TabIndex = 1
        Me.BttnPrint.Text = "&Print"
        Me.BttnPrint.UseVisualStyleBackColor = False
        '
        'frmRptORDER_FORM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(292, 199)
        Me.Controls.Add(Me.BttnPrint)
        Me.Controls.Add(Me.BttnView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BttnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmRptORDER_FORM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ORDER FORM OPTIONS"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BttnView As System.Windows.Forms.Button
    Friend WithEvents RBttnCompany As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RBttnCategory As System.Windows.Forms.RadioButton
    Friend WithEvents BttnPrint As System.Windows.Forms.Button
End Class
