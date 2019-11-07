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
        Me.cmdSelectByContacts = New System.Windows.Forms.Button()
        Me.cboContactTypes = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.cmdBad = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.chkShowSqlStatements = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkShowSqlStatements)
        Me.Panel1.Controls.Add(Me.cmdClose)
        Me.Panel1.Controls.Add(Me.cmdBad)
        Me.Panel1.Controls.Add(Me.cmdSelectByContacts)
        Me.Panel1.Controls.Add(Me.cboContactTypes)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 257)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(842, 66)
        Me.Panel1.TabIndex = 1
        '
        'cmdSelectByContacts
        '
        Me.cmdSelectByContacts.Location = New System.Drawing.Point(223, 15)
        Me.cmdSelectByContacts.Name = "cmdSelectByContacts"
        Me.cmdSelectByContacts.Size = New System.Drawing.Size(75, 23)
        Me.cmdSelectByContacts.TabIndex = 1
        Me.cmdSelectByContacts.Text = "Select"
        Me.cmdSelectByContacts.UseVisualStyleBackColor = True
        '
        'cboContactTypes
        '
        Me.cboContactTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboContactTypes.FormattingEnabled = True
        Me.cboContactTypes.Location = New System.Drawing.Point(12, 17)
        Me.cboContactTypes.Name = "cboContactTypes"
        Me.cboContactTypes.Size = New System.Drawing.Size(205, 21)
        Me.cboContactTypes.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(842, 257)
        Me.DataGridView1.TabIndex = 0
        '
        'cmdBad
        '
        Me.cmdBad.Location = New System.Drawing.Point(319, 15)
        Me.cmdBad.Name = "cmdBad"
        Me.cmdBad.Size = New System.Drawing.Size(75, 23)
        Me.cmdBad.TabIndex = 2
        Me.cmdBad.Text = "Bad"
        Me.cmdBad.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Location = New System.Drawing.Point(755, 15)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Exit"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'chkShowSqlStatements
        '
        Me.chkShowSqlStatements.AutoSize = True
        Me.chkShowSqlStatements.Checked = True
        Me.chkShowSqlStatements.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowSqlStatements.Location = New System.Drawing.Point(400, 19)
        Me.chkShowSqlStatements.Name = "chkShowSqlStatements"
        Me.chkShowSqlStatements.Size = New System.Drawing.Size(77, 17)
        Me.chkShowSqlStatements.TabIndex = 3
        Me.chkShowSqlStatements.Text = "Show SQL"
        Me.chkShowSqlStatements.UseVisualStyleBackColor = True
        '
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 323)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cboContactTypes As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSelectByContacts As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdBad As System.Windows.Forms.Button
    Friend WithEvents chkShowSqlStatements As System.Windows.Forms.CheckBox

End Class
