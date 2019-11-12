<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New crossword.GraphicsPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.nudCols = New System.Windows.Forms.NumericUpDown()
        Me.nudRows = New System.Windows.Forms.NumericUpDown()
        Me.nudHeight = New System.Windows.Forms.NumericUpDown()
        Me.nudWidth = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        CType(Me.nudCols, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudRows, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(122, 52)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "New Maze"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Location = New System.Drawing.Point(12, 100)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 140)
        Me.Panel2.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Silver
        Me.Panel1.Location = New System.Drawing.Point(2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(300, 300)
        Me.Panel1.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(166, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Column Count"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(166, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Row Count"
        '
        'nudCols
        '
        Me.nudCols.Location = New System.Drawing.Point(169, 28)
        Me.nudCols.Maximum = New Decimal(New Integer() {140, 0, 0, 0})
        Me.nudCols.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.nudCols.Name = "nudCols"
        Me.nudCols.ReadOnly = True
        Me.nudCols.Size = New System.Drawing.Size(70, 20)
        Me.nudCols.TabIndex = 7
        Me.nudCols.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'nudRows
        '
        Me.nudRows.Location = New System.Drawing.Point(169, 67)
        Me.nudRows.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.nudRows.Name = "nudRows"
        Me.nudRows.ReadOnly = True
        Me.nudRows.Size = New System.Drawing.Size(70, 20)
        Me.nudRows.TabIndex = 8
        Me.nudRows.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'nudHeight
        '
        Me.nudHeight.Location = New System.Drawing.Point(248, 67)
        Me.nudHeight.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.nudHeight.Name = "nudHeight"
        Me.nudHeight.ReadOnly = True
        Me.nudHeight.Size = New System.Drawing.Size(70, 20)
        Me.nudHeight.TabIndex = 13
        Me.nudHeight.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'nudWidth
        '
        Me.nudWidth.Location = New System.Drawing.Point(248, 28)
        Me.nudWidth.Maximum = New Decimal(New Integer() {140, 0, 0, 0})
        Me.nudWidth.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.nudWidth.Name = "nudWidth"
        Me.nudWidth.ReadOnly = True
        Me.nudWidth.Size = New System.Drawing.Size(70, 20)
        Me.nudWidth.TabIndex = 12
        Me.nudWidth.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(245, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Cell Height(Pixels)"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(245, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Cell Width(Pixels)"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(829, 475)
        Me.Controls.Add(Me.nudHeight)
        Me.Controls.Add(Me.nudWidth)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.nudRows)
        Me.Controls.Add(Me.nudCols)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "Form1"
        Me.Text = "Maze Maker"
        Me.Panel2.ResumeLayout(False)
        CType(Me.nudCols, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudRows, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Panel1 As GraphicsPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents nudCols As NumericUpDown
    Friend WithEvents nudRows As NumericUpDown
    Friend WithEvents nudHeight As NumericUpDown
    Friend WithEvents nudWidth As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
End Class
