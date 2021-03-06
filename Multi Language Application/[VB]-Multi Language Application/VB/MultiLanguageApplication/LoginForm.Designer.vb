<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoginForm
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
    Friend WithEvents LogoPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents Cancel As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoginForm))
        Me.LogoPictureBox = New System.Windows.Forms.PictureBox
        Me.UsernameLabel = New System.Windows.Forms.Label
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UsernameTextBox = New System.Windows.Forms.TextBox
        Me.PasswordTextBox = New System.Windows.Forms.TextBox
        Me.OK = New System.Windows.Forms.Button
        Me.ImageList_Button = New System.Windows.Forms.ImageList(Me.components)
        Me.Cancel = New System.Windows.Forms.Button
        Me.CmbLanguage = New System.Windows.Forms.ComboBox
        Me.LblLanguage = New System.Windows.Forms.Label
        Me.gbErrMessage = New System.Windows.Forms.GroupBox
        Me.rBttnActive = New System.Windows.Forms.RadioButton
        Me.rBttnBlocked = New System.Windows.Forms.RadioButton
        Me.rBttnDeleted = New System.Windows.Forms.RadioButton
        Me.rBttnNormal = New System.Windows.Forms.RadioButton
        Me.rBttnInvalid = New System.Windows.Forms.RadioButton
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbErrMessage.SuspendLayout()
        Me.SuspendLayout()
        '
        'LogoPictureBox
        '
        Me.LogoPictureBox.Image = CType(resources.GetObject("LogoPictureBox.Image"), System.Drawing.Image)
        Me.LogoPictureBox.Location = New System.Drawing.Point(0, 0)
        Me.LogoPictureBox.Name = "LogoPictureBox"
        Me.LogoPictureBox.Size = New System.Drawing.Size(165, 193)
        Me.LogoPictureBox.TabIndex = 0
        Me.LogoPictureBox.TabStop = False
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Location = New System.Drawing.Point(171, 40)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(220, 23)
        Me.UsernameLabel.TabIndex = 0
        Me.UsernameLabel.Text = "&User name"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Location = New System.Drawing.Point(171, 97)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(220, 23)
        Me.PasswordLabel.TabIndex = 2
        Me.PasswordLabel.Text = "&Password"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsernameTextBox
        '
        Me.UsernameTextBox.Location = New System.Drawing.Point(173, 60)
        Me.UsernameTextBox.Name = "UsernameTextBox"
        Me.UsernameTextBox.Size = New System.Drawing.Size(220, 20)
        Me.UsernameTextBox.TabIndex = 1
        Me.UsernameTextBox.Text = "123"
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Location = New System.Drawing.Point(173, 117)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.Size = New System.Drawing.Size(220, 20)
        Me.PasswordTextBox.TabIndex = 3
        Me.PasswordTextBox.Text = "123"
        Me.PasswordTextBox.UseSystemPasswordChar = True
        '
        'OK
        '
        Me.OK.ImageKey = "TASKL.ICO"
        Me.OK.ImageList = Me.ImageList_Button
        Me.OK.Location = New System.Drawing.Point(192, 155)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(94, 25)
        Me.OK.TabIndex = 4
        Me.OK.Text = "&OK"
        Me.OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        '
        'ImageList_Button
        '
        Me.ImageList_Button.ImageStream = CType(resources.GetObject("ImageList_Button.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList_Button.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList_Button.Images.SetKeyName(0, "DISK14.ICO")
        Me.ImageList_Button.Images.SetKeyName(1, "exit.ICO")
        Me.ImageList_Button.Images.SetKeyName(2, "TASKL.ICO")
        Me.ImageList_Button.Images.SetKeyName(3, "W95MBX01.ICO")
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.ImageKey = "W95MBX01.ICO"
        Me.Cancel.ImageList = Me.ImageList_Button
        Me.Cancel.Location = New System.Drawing.Point(295, 155)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(94, 25)
        Me.Cancel.TabIndex = 5
        Me.Cancel.Text = "&Cancel"
        Me.Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        '
        'CmbLanguage
        '
        Me.CmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLanguage.FormattingEnabled = True
        Me.CmbLanguage.Items.AddRange(New Object() {"English", "اردو"})
        Me.CmbLanguage.Location = New System.Drawing.Point(272, 12)
        Me.CmbLanguage.Name = "CmbLanguage"
        Me.CmbLanguage.Size = New System.Drawing.Size(121, 21)
        Me.CmbLanguage.TabIndex = 6
        '
        'LblLanguage
        '
        Me.LblLanguage.Location = New System.Drawing.Point(171, 12)
        Me.LblLanguage.Name = "LblLanguage"
        Me.LblLanguage.Size = New System.Drawing.Size(95, 23)
        Me.LblLanguage.TabIndex = 0
        Me.LblLanguage.Text = "&Language"
        Me.LblLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbErrMessage
        '
        Me.gbErrMessage.Controls.Add(Me.rBttnInvalid)
        Me.gbErrMessage.Controls.Add(Me.rBttnNormal)
        Me.gbErrMessage.Controls.Add(Me.rBttnDeleted)
        Me.gbErrMessage.Controls.Add(Me.rBttnBlocked)
        Me.gbErrMessage.Controls.Add(Me.rBttnActive)
        Me.gbErrMessage.Location = New System.Drawing.Point(399, 40)
        Me.gbErrMessage.Name = "gbErrMessage"
        Me.gbErrMessage.Size = New System.Drawing.Size(132, 140)
        Me.gbErrMessage.TabIndex = 7
        Me.gbErrMessage.TabStop = False
        Me.gbErrMessage.Text = "Error Message"
        '
        'rBttnActive
        '
        Me.rBttnActive.AutoSize = True
        Me.rBttnActive.Location = New System.Drawing.Point(6, 46)
        Me.rBttnActive.Name = "rBttnActive"
        Me.rBttnActive.Size = New System.Drawing.Size(75, 17)
        Me.rBttnActive.TabIndex = 0
        Me.rBttnActive.Text = "Not Active"
        Me.rBttnActive.UseVisualStyleBackColor = True
        '
        'rBttnBlocked
        '
        Me.rBttnBlocked.AutoSize = True
        Me.rBttnBlocked.Location = New System.Drawing.Point(6, 69)
        Me.rBttnBlocked.Name = "rBttnBlocked"
        Me.rBttnBlocked.Size = New System.Drawing.Size(64, 17)
        Me.rBttnBlocked.TabIndex = 0
        Me.rBttnBlocked.Text = "Blocked"
        Me.rBttnBlocked.UseVisualStyleBackColor = True
        '
        'rBttnDeleted
        '
        Me.rBttnDeleted.AutoSize = True
        Me.rBttnDeleted.Location = New System.Drawing.Point(6, 92)
        Me.rBttnDeleted.Name = "rBttnDeleted"
        Me.rBttnDeleted.Size = New System.Drawing.Size(62, 17)
        Me.rBttnDeleted.TabIndex = 0
        Me.rBttnDeleted.Text = "Deleted"
        Me.rBttnDeleted.UseVisualStyleBackColor = True
        '
        'rBttnNormal
        '
        Me.rBttnNormal.AutoSize = True
        Me.rBttnNormal.Checked = True
        Me.rBttnNormal.Location = New System.Drawing.Point(6, 23)
        Me.rBttnNormal.Name = "rBttnNormal"
        Me.rBttnNormal.Size = New System.Drawing.Size(58, 17)
        Me.rBttnNormal.TabIndex = 0
        Me.rBttnNormal.Text = "Normal"
        Me.rBttnNormal.UseVisualStyleBackColor = True
        '
        'rBttnInvalid
        '
        Me.rBttnInvalid.AutoSize = True
        Me.rBttnInvalid.Location = New System.Drawing.Point(6, 115)
        Me.rBttnInvalid.Name = "rBttnInvalid"
        Me.rBttnInvalid.Size = New System.Drawing.Size(111, 17)
        Me.rBttnInvalid.TabIndex = 0
        Me.rBttnInvalid.Text = "Invalid Information"
        Me.rBttnInvalid.UseVisualStyleBackColor = True
        '
        'LoginForm
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel
        Me.ClientSize = New System.Drawing.Size(544, 192)
        Me.Controls.Add(Me.gbErrMessage)
        Me.Controls.Add(Me.CmbLanguage)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.UsernameTextBox)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.LblLanguage)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.LogoPictureBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LoginForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login Form"
        CType(Me.LogoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbErrMessage.ResumeLayout(False)
        Me.gbErrMessage.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageList_Button As System.Windows.Forms.ImageList
    Friend WithEvents CmbLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents LblLanguage As System.Windows.Forms.Label
    Friend WithEvents gbErrMessage As System.Windows.Forms.GroupBox
    Friend WithEvents rBttnDeleted As System.Windows.Forms.RadioButton
    Friend WithEvents rBttnBlocked As System.Windows.Forms.RadioButton
    Friend WithEvents rBttnActive As System.Windows.Forms.RadioButton
    Friend WithEvents rBttnNormal As System.Windows.Forms.RadioButton
    Friend WithEvents rBttnInvalid As System.Windows.Forms.RadioButton

End Class
