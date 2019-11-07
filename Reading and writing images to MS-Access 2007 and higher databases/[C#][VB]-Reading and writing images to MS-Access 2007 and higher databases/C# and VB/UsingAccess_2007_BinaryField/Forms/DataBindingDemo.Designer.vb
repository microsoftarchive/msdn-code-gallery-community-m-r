<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataBindingDemo
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.CategoryColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DescriptionColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureColumn = New System.Windows.Forms.DataGridViewImageColumn()
        Me.FileNameColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 343)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(761, 100)
        Me.Panel1.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CategoryColumn, Me.DescriptionColumn, Me.PictureColumn, Me.FileNameColumn})
        Me.DataGridView1.Location = New System.Drawing.Point(8, 8)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 25
        Me.DataGridView1.Size = New System.Drawing.Size(386, 329)
        Me.DataGridView1.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(400, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(361, 325)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
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
        Me.PictureColumn.Visible = False
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
        Me.FileNameColumn.Visible = False
        '
        'DataBindingDemo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(761, 443)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "DataBindingDemo"
        Me.Text = "DataBindingDemo"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents CategoryColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DescriptionColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PictureColumn As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents FileNameColumn As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
