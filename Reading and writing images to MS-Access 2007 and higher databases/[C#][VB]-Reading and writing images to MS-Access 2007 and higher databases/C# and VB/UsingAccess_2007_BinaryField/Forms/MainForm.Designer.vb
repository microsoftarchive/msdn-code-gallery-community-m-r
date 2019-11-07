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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdPictureBoxDemo = New System.Windows.Forms.Button()
        Me.cmdExportCurrentImage = New System.Windows.Forms.Button()
        Me.cmdFilterByCategory = New System.Windows.Forms.Button()
        Me.cboCategories = New System.Windows.Forms.ComboBox()
        Me.cmdAddPicture = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CategoryColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureColumn = New System.Windows.Forms.DataGridViewImageColumn()
        Me.FileNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdFlowLayout = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cmdFlowLayout)
        Me.Panel1.Controls.Add(Me.cmdPictureBoxDemo)
        Me.Panel1.Controls.Add(Me.cmdExportCurrentImage)
        Me.Panel1.Controls.Add(Me.cmdFilterByCategory)
        Me.Panel1.Controls.Add(Me.cboCategories)
        Me.Panel1.Controls.Add(Me.cmdAddPicture)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 287)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(648, 81)
        Me.Panel1.TabIndex = 1
        '
        'cmdPictureBoxDemo
        '
        Me.cmdPictureBoxDemo.Location = New System.Drawing.Point(427, 14)
        Me.cmdPictureBoxDemo.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmdPictureBoxDemo.Name = "cmdPictureBoxDemo"
        Me.cmdPictureBoxDemo.Size = New System.Drawing.Size(89, 35)
        Me.cmdPictureBoxDemo.TabIndex = 4
        Me.cmdPictureBoxDemo.Text = "PictureBox"
        Me.cmdPictureBoxDemo.UseVisualStyleBackColor = True
        '
        'cmdExportCurrentImage
        '
        Me.cmdExportCurrentImage.Location = New System.Drawing.Point(334, 14)
        Me.cmdExportCurrentImage.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmdExportCurrentImage.Name = "cmdExportCurrentImage"
        Me.cmdExportCurrentImage.Size = New System.Drawing.Size(89, 35)
        Me.cmdExportCurrentImage.TabIndex = 3
        Me.cmdExportCurrentImage.Text = "Export current"
        Me.cmdExportCurrentImage.UseVisualStyleBackColor = True
        '
        'cmdFilterByCategory
        '
        Me.cmdFilterByCategory.Location = New System.Drawing.Point(149, 13)
        Me.cmdFilterByCategory.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmdFilterByCategory.Name = "cmdFilterByCategory"
        Me.cmdFilterByCategory.Size = New System.Drawing.Size(89, 35)
        Me.cmdFilterByCategory.TabIndex = 1
        Me.cmdFilterByCategory.Text = "Filter"
        Me.cmdFilterByCategory.UseVisualStyleBackColor = True
        '
        'cboCategories
        '
        Me.cboCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategories.FormattingEnabled = True
        Me.cboCategories.Location = New System.Drawing.Point(9, 22)
        Me.cboCategories.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cboCategories.Name = "cboCategories"
        Me.cboCategories.Size = New System.Drawing.Size(137, 21)
        Me.cboCategories.TabIndex = 0
        '
        'cmdAddPicture
        '
        Me.cmdAddPicture.Location = New System.Drawing.Point(241, 14)
        Me.cmdAddPicture.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.cmdAddPicture.Name = "cmdAddPicture"
        Me.cmdAddPicture.Size = New System.Drawing.Size(89, 35)
        Me.cmdAddPicture.TabIndex = 2
        Me.cmdAddPicture.Text = "Add Picture"
        Me.cmdAddPicture.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CategoryColumn, Me.DescriptionColumn, Me.PictureColumn, Me.FileNameColumn})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 377
        Me.DataGridView1.Size = New System.Drawing.Size(648, 287)
        Me.DataGridView1.TabIndex = 0
        '
        'CategoryColumn
        '
        Me.CategoryColumn.DataPropertyName = "Category"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.CategoryColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.CategoryColumn.HeaderText = "Category"
        Me.CategoryColumn.Name = "CategoryColumn"
        Me.CategoryColumn.ReadOnly = True
        '
        'DescriptionColumn
        '
        Me.DescriptionColumn.DataPropertyName = "Description"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.DescriptionColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.DescriptionColumn.HeaderText = "Description"
        Me.DescriptionColumn.Name = "DescriptionColumn"
        Me.DescriptionColumn.ReadOnly = True
        '
        'PictureColumn
        '
        Me.PictureColumn.DataPropertyName = "Picture"
        Me.PictureColumn.HeaderText = "Picture"
        Me.PictureColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch
        Me.PictureColumn.Name = "PictureColumn"
        Me.PictureColumn.ReadOnly = True
        Me.PictureColumn.Width = 345
        '
        'FileNameColumn
        '
        Me.FileNameColumn.DataPropertyName = "FullFileName"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft
        Me.FileNameColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.FileNameColumn.HeaderText = "File name"
        Me.FileNameColumn.Name = "FileNameColumn"
        Me.FileNameColumn.ReadOnly = True
        '
        'cmdFlowLayout
        '
        Me.cmdFlowLayout.Location = New System.Drawing.Point(520, 14)
        Me.cmdFlowLayout.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdFlowLayout.Name = "cmdFlowLayout"
        Me.cmdFlowLayout.Size = New System.Drawing.Size(89, 35)
        Me.cmdFlowLayout.TabIndex = 6
        Me.cmdFlowLayout.Text = "FlowLayout"
        Me.cmdFlowLayout.UseVisualStyleBackColor = True
        '
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 368)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MS-Access images"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmdAddPicture As System.Windows.Forms.Button
    Friend WithEvents cboCategories As System.Windows.Forms.ComboBox
    Friend WithEvents cmdFilterByCategory As System.Windows.Forms.Button
    Friend WithEvents CategoryColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureColumn As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents FileNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdExportCurrentImage As System.Windows.Forms.Button
    Friend WithEvents cmdPictureBoxDemo As System.Windows.Forms.Button
    Friend WithEvents cmdFlowLayout As Button
End Class
