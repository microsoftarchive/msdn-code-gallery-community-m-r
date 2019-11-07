<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdGetData = New System.Windows.Forms.Button()
        Me.dataGridView1 = New System.Windows.Forms.DataGridView()
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panel1.SuspendLayout()
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.label1)
        Me.panel1.Controls.Add(Me.cmdCancel)
        Me.panel1.Controls.Add(Me.cmdGetData)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panel1.Location = New System.Drawing.Point(0, 320)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(653, 100)
        Me.panel1.TabIndex = 1
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(277, 38)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(16, 17)
        Me.label1.TabIndex = 2
        Me.label1.Text = "0"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(140, 22)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(109, 49)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "Cancel"
        Me.toolTip1.SetToolTip(Me.cmdCancel, "Cancel getting data")
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdGetData
        '
        Me.cmdGetData.Location = New System.Drawing.Point(25, 22)
        Me.cmdGetData.Name = "cmdGetData"
        Me.cmdGetData.Size = New System.Drawing.Size(109, 49)
        Me.cmdGetData.TabIndex = 0
        Me.cmdGetData.Text = "Get data"
        Me.toolTip1.SetToolTip(Me.cmdGetData, "Start getting data")
        Me.cmdGetData.UseVisualStyleBackColor = True
        '
        'dataGridView1
        '
        Me.dataGridView1.AllowUserToAddRows = False
        Me.dataGridView1.AllowUserToDeleteRows = False
        Me.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.dataGridView1.Name = "dataGridView1"
        Me.dataGridView1.RowTemplate.Height = 24
        Me.dataGridView1.Size = New System.Drawing.Size(653, 320)
        Me.dataGridView1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(653, 420)
        Me.Controls.Add(Me.dataGridView1)
        Me.Controls.Add(Me.panel1)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents cmdCancel As System.Windows.Forms.Button
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents cmdGetData As System.Windows.Forms.Button
    Private WithEvents dataGridView1 As System.Windows.Forms.DataGridView

End Class
