<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReadRangeForm
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
        Me.cmdShowSelectedRange = New System.Windows.Forms.Button()
        Me.txtSelectStatement = New System.Windows.Forms.TextBox()
        Me.cboSheetNames = New System.Windows.Forms.ComboBox()
        Me.cmdReadData = New System.Windows.Forms.Button()
        Me.cboEndRow = New System.Windows.Forms.ComboBox()
        Me.cboEndLetter = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboStartRow = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboStartLetter = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.chkRemoveFirstRow = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkRemoveFirstRow)
        Me.Panel1.Controls.Add(Me.cmdShowSelectedRange)
        Me.Panel1.Controls.Add(Me.txtSelectStatement)
        Me.Panel1.Controls.Add(Me.cboSheetNames)
        Me.Panel1.Controls.Add(Me.cmdReadData)
        Me.Panel1.Controls.Add(Me.cboEndRow)
        Me.Panel1.Controls.Add(Me.cboEndLetter)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cboStartRow)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cboStartLetter)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 303)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(559, 190)
        Me.Panel1.TabIndex = 0
        '
        'cmdShowSelectedRange
        '
        Me.cmdShowSelectedRange.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdShowSelectedRange.Location = New System.Drawing.Point(455, 129)
        Me.cmdShowSelectedRange.Name = "cmdShowSelectedRange"
        Me.cmdShowSelectedRange.Size = New System.Drawing.Size(99, 58)
        Me.cmdShowSelectedRange.TabIndex = 9
        Me.cmdShowSelectedRange.Text = "Show selected range"
        Me.cmdShowSelectedRange.UseVisualStyleBackColor = True
        '
        'txtSelectStatement
        '
        Me.txtSelectStatement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelectStatement.Location = New System.Drawing.Point(12, 104)
        Me.txtSelectStatement.Name = "txtSelectStatement"
        Me.txtSelectStatement.ReadOnly = True
        Me.txtSelectStatement.Size = New System.Drawing.Size(544, 20)
        Me.txtSelectStatement.TabIndex = 8
        '
        'cboSheetNames
        '
        Me.cboSheetNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSheetNames.FormattingEnabled = True
        Me.cboSheetNames.Location = New System.Drawing.Point(89, 3)
        Me.cboSheetNames.Name = "cboSheetNames"
        Me.cboSheetNames.Size = New System.Drawing.Size(248, 21)
        Me.cboSheetNames.TabIndex = 7
        '
        'cmdReadData
        '
        Me.cmdReadData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdReadData.Location = New System.Drawing.Point(455, 31)
        Me.cmdReadData.Name = "cmdReadData"
        Me.cmdReadData.Size = New System.Drawing.Size(99, 58)
        Me.cmdReadData.TabIndex = 6
        Me.cmdReadData.Text = "Fetch data"
        Me.cmdReadData.UseVisualStyleBackColor = True
        '
        'cboEndRow
        '
        Me.cboEndRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEndRow.FormattingEnabled = True
        Me.cboEndRow.Location = New System.Drawing.Point(216, 68)
        Me.cboEndRow.Name = "cboEndRow"
        Me.cboEndRow.Size = New System.Drawing.Size(121, 21)
        Me.cboEndRow.TabIndex = 5
        '
        'cboEndLetter
        '
        Me.cboEndLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEndLetter.FormattingEnabled = True
        Me.cboEndLetter.Location = New System.Drawing.Point(89, 68)
        Me.cboEndLetter.Name = "cboEndLetter"
        Me.cboEndLetter.Size = New System.Drawing.Size(121, 21)
        Me.cboEndLetter.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "End of range"
        '
        'cboStartRow
        '
        Me.cboStartRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStartRow.FormattingEnabled = True
        Me.cboStartRow.Location = New System.Drawing.Point(216, 31)
        Me.cboStartRow.Name = "cboStartRow"
        Me.cboStartRow.Size = New System.Drawing.Size(121, 21)
        Me.cboStartRow.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Start of range"
        '
        'cboStartLetter
        '
        Me.cboStartLetter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStartLetter.FormattingEnabled = True
        Me.cboStartLetter.Location = New System.Drawing.Point(89, 31)
        Me.cboStartLetter.Name = "cboStartLetter"
        Me.cboStartLetter.Size = New System.Drawing.Size(121, 21)
        Me.cboStartLetter.TabIndex = 0
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
        Me.DataGridView1.Size = New System.Drawing.Size(559, 303)
        Me.DataGridView1.TabIndex = 1
        '
        'chkRemoveFirstRow
        '
        Me.chkRemoveFirstRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkRemoveFirstRow.AutoSize = True
        Me.chkRemoveFirstRow.Location = New System.Drawing.Point(344, 151)
        Me.chkRemoveFirstRow.Name = "chkRemoveFirstRow"
        Me.chkRemoveFirstRow.Size = New System.Drawing.Size(105, 17)
        Me.chkRemoveFirstRow.TabIndex = 10
        Me.chkRemoveFirstRow.Text = "Remove first row"
        Me.chkRemoveFirstRow.UseVisualStyleBackColor = True
        '
        'ReadRangeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(559, 493)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ReadRangeForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Read Range"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents cboStartLetter As ComboBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents cboStartRow As ComboBox
    Friend WithEvents cboEndLetter As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboEndRow As ComboBox
    Friend WithEvents cmdReadData As Button
    Friend WithEvents cboSheetNames As ComboBox
    Friend WithEvents txtSelectStatement As TextBox
    Friend WithEvents cmdShowSelectedRange As Button
    Friend WithEvents chkRemoveFirstRow As CheckBox
End Class
