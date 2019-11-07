<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddPictureForm
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
        Me.cmdCancelAddingPicture = New System.Windows.Forms.Button()
        Me.cmdContinueAddingPicture = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFullFileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCategories = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.cmdSelectPicture = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdCancelAddingPicture)
        Me.Panel1.Controls.Add(Me.cmdContinueAddingPicture)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 328)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(649, 72)
        Me.Panel1.TabIndex = 7
        '
        'cmdCancelAddingPicture
        '
        Me.cmdCancelAddingPicture.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancelAddingPicture.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancelAddingPicture.Location = New System.Drawing.Point(548, 17)
        Me.cmdCancelAddingPicture.Name = "cmdCancelAddingPicture"
        Me.cmdCancelAddingPicture.Size = New System.Drawing.Size(89, 34)
        Me.cmdCancelAddingPicture.TabIndex = 1
        Me.cmdCancelAddingPicture.Text = "Cancel"
        Me.cmdCancelAddingPicture.UseVisualStyleBackColor = True
        '
        'cmdContinueAddingPicture
        '
        Me.cmdContinueAddingPicture.Location = New System.Drawing.Point(23, 17)
        Me.cmdContinueAddingPicture.Name = "cmdContinueAddingPicture"
        Me.cmdContinueAddingPicture.Size = New System.Drawing.Size(89, 34)
        Me.cmdContinueAddingPicture.TabIndex = 0
        Me.cmdContinueAddingPicture.Text = "Accept"
        Me.cmdContinueAddingPicture.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.AddExtension = False
        Me.OpenFileDialog1.Filter = "Bitmap|*.bmp|Gif|*.gif|Jpg|*.jpg|All|*.*"
        Me.OpenFileDialog1.RestoreDirectory = True
        Me.OpenFileDialog1.Title = "Select picture to add"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(90, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "File name"
        '
        'txtFullFileName
        '
        Me.txtFullFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFullFileName.BackColor = System.Drawing.Color.White
        Me.txtFullFileName.Location = New System.Drawing.Point(165, 21)
        Me.txtFullFileName.Name = "txtFullFileName"
        Me.txtFullFileName.ReadOnly = True
        Me.txtFullFileName.Size = New System.Drawing.Size(472, 22)
        Me.txtFullFileName.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(94, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Category"
        '
        'cboCategories
        '
        Me.cboCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategories.FormattingEnabled = True
        Me.cboCategories.Location = New System.Drawing.Point(165, 61)
        Me.cboCategories.Name = "cboCategories"
        Me.cboCategories.Size = New System.Drawing.Size(181, 24)
        Me.cboCategories.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(80, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Description"
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.BackColor = System.Drawing.Color.White
        Me.txtDescription.Location = New System.Drawing.Point(165, 106)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(472, 22)
        Me.txtDescription.TabIndex = 5
        '
        'cmdSelectPicture
        '
        Me.cmdSelectPicture.Location = New System.Drawing.Point(49, 143)
        Me.cmdSelectPicture.Name = "cmdSelectPicture"
        Me.cmdSelectPicture.Size = New System.Drawing.Size(110, 48)
        Me.cmdSelectPicture.TabIndex = 6
        Me.cmdSelectPicture.Text = "Select picture"
        Me.cmdSelectPicture.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(165, 143)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(472, 169)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'frmAddPictureForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 400)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdSelectPicture)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboCategories)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtFullFileName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmAddPictureForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add new picture"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmdCancelAddingPicture As System.Windows.Forms.Button
    Friend WithEvents cmdContinueAddingPicture As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFullFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCategories As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents cmdSelectPicture As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
