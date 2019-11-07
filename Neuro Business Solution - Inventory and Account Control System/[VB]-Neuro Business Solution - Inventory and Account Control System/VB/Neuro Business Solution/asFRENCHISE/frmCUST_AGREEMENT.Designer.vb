<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCUST_AGREEMENT
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.BttnSave = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSearch = New System.Windows.Forms.Button
        Me.BttnDelete = New System.Windows.Forms.Button
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TxtBankPmt = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.TxtChequeNo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(568, 54)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Cellular Services Agreement Form"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(5, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 21)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Mobile #"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 21)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BttnSave
        '
        Me.BttnSave.Enabled = False
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(231, 268)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(81, 32)
        Me.BttnSave.TabIndex = 24
        Me.BttnSave.Text = "&Save"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 21)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Land Line #"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BttnNew
        '
        Me.BttnNew.Enabled = False
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(142, 268)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(81, 32)
        Me.BttnNew.TabIndex = 25
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(320, 268)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(81, 32)
        Me.BttnClose.TabIndex = 26
        Me.BttnClose.Text = "&Close"
        '
        'BttnSearch
        '
        Me.BttnSearch.Enabled = False
        Me.BttnSearch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch.ForeColor = System.Drawing.Color.DarkBlue
        Me.BttnSearch.Location = New System.Drawing.Point(188, 306)
        Me.BttnSearch.Name = "BttnSearch"
        Me.BttnSearch.Size = New System.Drawing.Size(81, 32)
        Me.BttnSearch.TabIndex = 27
        Me.BttnSearch.Text = "Sea&rch"
        '
        'BttnDelete
        '
        Me.BttnDelete.Enabled = False
        Me.BttnDelete.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnDelete.ForeColor = System.Drawing.Color.Red
        Me.BttnDelete.Location = New System.Drawing.Point(275, 306)
        Me.BttnDelete.Name = "BttnDelete"
        Me.BttnDelete.Size = New System.Drawing.Size(81, 32)
        Me.BttnDelete.TabIndex = 28
        Me.BttnDelete.Text = "&Delete"
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDate.Location = New System.Drawing.Point(112, 19)
        Me.TxtDate.MaxLength = 50
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(119, 21)
        Me.TxtDate.TabIndex = 1
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TextBox1.Location = New System.Drawing.Point(112, 44)
        Me.TextBox1.MaxLength = 50
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(216, 21)
        Me.TextBox1.TabIndex = 1
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TextBox3.Location = New System.Drawing.Point(112, 68)
        Me.TextBox3.MaxLength = 50
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(120, 21)
        Me.TextBox3.TabIndex = 1
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Location = New System.Drawing.Point(106, 19)
        Me.MaskedTextBox1.Mask = "0000-0000000"
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(119, 23)
        Me.MaskedTextBox1.TabIndex = 29
        Me.MaskedTextBox1.Text = "03348333850"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TextBox2)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.TxtBankPmt)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.CheckBox1)
        Me.GroupBox4.Controls.Add(Me.MaskedTextBox1)
        Me.GroupBox4.Controls.Add(Me.TextBox4)
        Me.GroupBox4.Controls.Add(Me.TxtChequeNo)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(6, 95)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(524, 158)
        Me.GroupBox4.TabIndex = 32
        Me.GroupBox4.TabStop = False
        '
        'TxtBankPmt
        '
        Me.TxtBankPmt.BackColor = System.Drawing.Color.White
        Me.TxtBankPmt.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtBankPmt.Location = New System.Drawing.Point(336, 21)
        Me.TxtBankPmt.MaxLength = 50
        Me.TxtBankPmt.Name = "TxtBankPmt"
        Me.TxtBankPmt.Size = New System.Drawing.Size(99, 21)
        Me.TxtBankPmt.TabIndex = 32
        Me.TxtBankPmt.Text = "0.00"
        Me.TxtBankPmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(231, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 23)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "By Cash"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CheckBox1
        '
        Me.CheckBox1.Location = New System.Drawing.Point(11, 124)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(215, 21)
        Me.CheckBox1.TabIndex = 30
        Me.CheckBox1.Text = "Frenchise Guaranty"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.Color.White
        Me.TextBox4.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TextBox4.Location = New System.Drawing.Point(106, 97)
        Me.TextBox4.MaxLength = 50
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(119, 21)
        Me.TextBox4.TabIndex = 17
        '
        'TxtChequeNo
        '
        Me.TxtChequeNo.BackColor = System.Drawing.Color.White
        Me.TxtChequeNo.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtChequeNo.Location = New System.Drawing.Point(106, 71)
        Me.TxtChequeNo.MaxLength = 50
        Me.TxtChequeNo.Name = "TxtChequeNo"
        Me.TxtChequeNo.Size = New System.Drawing.Size(119, 21)
        Me.TxtChequeNo.TabIndex = 17
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 23)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Waiver Code:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 45)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(220, 23)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Package && Security Deposit Rs:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Controls.Add(Me.TextBox1)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Controls.Add(Me.BttnDelete)
        Me.GroupBox3.Controls.Add(Me.BttnSearch)
        Me.GroupBox3.Controls.Add(Me.BttnClose)
        Me.GroupBox3.Controls.Add(Me.BttnNew)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.BttnSave)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(13, 53)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(543, 355)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(231, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 23)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "By Cr. Card"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.White
        Me.TextBox2.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TextBox2.Location = New System.Drawing.Point(336, 45)
        Me.TextBox2.MaxLength = 50
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(99, 21)
        Me.TextBox2.TabIndex = 32
        Me.TextBox2.Text = "0.00"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmCUST_AGREEMENT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(568, 420)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label3)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmCUST_AGREEMENT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cellular Services Agreement Form"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSearch As System.Windows.Forms.Button
    Friend WithEvents BttnDelete As System.Windows.Forms.Button
    Friend WithEvents TxtDate As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents TxtBankPmt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
