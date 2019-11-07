<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.cmdColumnExtension = New System.Windows.Forms.Button()
        Me.cmdGetRange = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdColumnExtension
        '
        Me.cmdColumnExtension.Location = New System.Drawing.Point(12, 32)
        Me.cmdColumnExtension.Name = "cmdColumnExtension"
        Me.cmdColumnExtension.Size = New System.Drawing.Size(260, 23)
        Me.cmdColumnExtension.TabIndex = 0
        Me.cmdColumnExtension.Text = "Column extensions (using C#)"
        Me.cmdColumnExtension.UseVisualStyleBackColor = True
        '
        'cmdGetRange
        '
        Me.cmdGetRange.Location = New System.Drawing.Point(12, 81)
        Me.cmdGetRange.Name = "cmdGetRange"
        Me.cmdGetRange.Size = New System.Drawing.Size(260, 23)
        Me.cmdGetRange.TabIndex = 1
        Me.cmdGetRange.Text = "Get Range"
        Me.cmdGetRange.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 156)
        Me.Controls.Add(Me.cmdGetRange)
        Me.Controls.Add(Me.cmdColumnExtension)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main Form"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmdColumnExtension As Button
    Friend WithEvents cmdGetRange As Button
End Class
