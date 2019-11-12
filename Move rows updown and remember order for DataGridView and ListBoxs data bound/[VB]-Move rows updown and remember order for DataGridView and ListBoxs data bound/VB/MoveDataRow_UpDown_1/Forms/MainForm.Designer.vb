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
        Me.cmdFromDatabase = New System.Windows.Forms.Button()
        Me.cmdFromTextFile = New System.Windows.Forms.Button()
        Me.cmdListBoxExample = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdFromDatabase
        '
        Me.cmdFromDatabase.Location = New System.Drawing.Point(51, 35)
        Me.cmdFromDatabase.Name = "cmdFromDatabase"
        Me.cmdFromDatabase.Size = New System.Drawing.Size(148, 70)
        Me.cmdFromDatabase.TabIndex = 0
        Me.cmdFromDatabase.Text = "Database/DataGridView"
        Me.cmdFromDatabase.UseVisualStyleBackColor = True
        '
        'cmdFromTextFile
        '
        Me.cmdFromTextFile.Location = New System.Drawing.Point(227, 35)
        Me.cmdFromTextFile.Name = "cmdFromTextFile"
        Me.cmdFromTextFile.Size = New System.Drawing.Size(148, 70)
        Me.cmdFromTextFile.TabIndex = 1
        Me.cmdFromTextFile.Text = "TextFile/DataGridView"
        Me.cmdFromTextFile.UseVisualStyleBackColor = True
        '
        'cmdListBoxExample
        '
        Me.cmdListBoxExample.Location = New System.Drawing.Point(403, 35)
        Me.cmdListBoxExample.Name = "cmdListBoxExample"
        Me.cmdListBoxExample.Size = New System.Drawing.Size(148, 70)
        Me.cmdListBoxExample.TabIndex = 2
        Me.cmdListBoxExample.Text = "Database/ListBox"
        Me.cmdListBoxExample.UseVisualStyleBackColor = True
        '
        'frmMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(598, 144)
        Me.Controls.Add(Me.cmdListBoxExample)
        Me.Controls.Add(Me.cmdFromTextFile)
        Me.Controls.Add(Me.cmdFromDatabase)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmMainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Demo"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdFromDatabase As System.Windows.Forms.Button
    Friend WithEvents cmdFromTextFile As System.Windows.Forms.Button
    Friend WithEvents cmdListBoxExample As System.Windows.Forms.Button
End Class
