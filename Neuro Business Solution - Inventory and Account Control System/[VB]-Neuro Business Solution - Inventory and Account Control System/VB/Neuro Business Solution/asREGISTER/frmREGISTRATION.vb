Public Class frmREGISTRATION
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCONTACT As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents BttnEnterCode As System.Windows.Forms.Button
    Friend WithEvents BttnDEMO As System.Windows.Forms.Button
    Friend WithEvents BttnExit As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmREGISTRATION))
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCONTACT = New System.Windows.Forms.TextBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.BttnEnterCode = New System.Windows.Forms.Button
        Me.BttnDEMO = New System.Windows.Forms.Button
        Me.BttnExit = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 112)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Neuro LOGO"
        '
        'txtCONTACT
        '
        Me.txtCONTACT.BackColor = System.Drawing.Color.White
        Me.txtCONTACT.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCONTACT.ForeColor = System.Drawing.Color.Black
        Me.txtCONTACT.Location = New System.Drawing.Point(136, 8)
        Me.txtCONTACT.Multiline = True
        Me.txtCONTACT.Name = "txtCONTACT"
        Me.txtCONTACT.ReadOnly = True
        Me.txtCONTACT.Size = New System.Drawing.Size(296, 112)
        Me.txtCONTACT.TabIndex = 1
        Me.txtCONTACT.TabStop = False
        Me.txtCONTACT.Text = ""
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 128)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(224, 264)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'BttnEnterCode
        '
        Me.BttnEnterCode.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnEnterCode.Location = New System.Drawing.Point(240, 184)
        Me.BttnEnterCode.Name = "BttnEnterCode"
        Me.BttnEnterCode.Size = New System.Drawing.Size(200, 40)
        Me.BttnEnterCode.TabIndex = 3
        Me.BttnEnterCode.Text = "Enter &Code"
        '
        'BttnDEMO
        '
        Me.BttnDEMO.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnDEMO.Location = New System.Drawing.Point(240, 240)
        Me.BttnDEMO.Name = "BttnDEMO"
        Me.BttnDEMO.Size = New System.Drawing.Size(200, 40)
        Me.BttnDEMO.TabIndex = 4
        Me.BttnDEMO.Text = "&Demo"
        '
        'BttnExit
        '
        Me.BttnExit.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnExit.Location = New System.Drawing.Point(240, 296)
        Me.BttnExit.Name = "BttnExit"
        Me.BttnExit.Size = New System.Drawing.Size(200, 40)
        Me.BttnExit.TabIndex = 4
        Me.BttnExit.Text = "E&xit"
        '
        'frmREGISTRATION
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(456, 397)
        Me.ControlBox = False
        Me.Controls.Add(Me.BttnDEMO)
        Me.Controls.Add(Me.BttnEnterCode)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtCONTACT)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BttnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "frmREGISTRATION"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "REGISTERATION OPTIONS!"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmREGISTRATION_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtCONTACT.Text = "Address: P.O Box # 22030" & vbCrLf & "Islamkot, Abbottabad" & vbCrLf & "Contact # " & vbCrLf & "0333-6866685 / 0333-5041044"
    End Sub

   
    Private Sub BttnEnterCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnEnterCode.Click
        Dim Frm As New frmREG_CODE
        Frm.ShowDialog(Me)
    End Sub

    Private Sub BttnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnExit.Click
        Me.Close()
    End Sub
End Class
