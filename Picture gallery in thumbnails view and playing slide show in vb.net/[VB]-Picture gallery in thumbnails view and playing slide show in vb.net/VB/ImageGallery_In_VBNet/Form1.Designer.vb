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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ServiceController1 = New System.ServiceProcess.ServiceController()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AuthorCodeImageGalleryVB1 = New ImageGallery_In_VBNet.AuthorCodeImageGalleryVB()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(464, 46)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(44, 20)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(20, 46)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(433, 20)
        Me.TextBox1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(105, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Enter Directory Path:"
        '
        'AuthorCodeImageGalleryVB1
        '
        Me.AuthorCodeImageGalleryVB1.AutoScroll = True
        Me.AuthorCodeImageGalleryVB1.BackColor = System.Drawing.SystemColors.Control
        Me.AuthorCodeImageGalleryVB1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.AuthorCodeImageGalleryVB1.Directorypath = Nothing
        Me.AuthorCodeImageGalleryVB1.Location = New System.Drawing.Point(25, 72)
        Me.AuthorCodeImageGalleryVB1.Name = "AuthorCodeImageGalleryVB1"
        Me.AuthorCodeImageGalleryVB1.Size = New System.Drawing.Size(483, 318)
        Me.AuthorCodeImageGalleryVB1.TabIndex = 1
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 421)
        Me.Controls.Add(Me.AuthorCodeImageGalleryVB1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents AuthorCodeImageGalleryVB1 As ImageGallery_In_VBNet.AuthorCodeImageGalleryVB
    Friend WithEvents ServiceController1 As System.ServiceProcess.ServiceController
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
