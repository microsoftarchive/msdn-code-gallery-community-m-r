Imports SDS = System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography
Public Class frmOPTIONS
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents ChkBatchWise As System.Windows.Forms.CheckBox
    Friend WithEvents ChkSerialWise As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtValidUpto As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtRegTo As System.Windows.Forms.TextBox
    Friend WithEvents RBttnRegistered As System.Windows.Forms.RadioButton
    Friend WithEvents RBttnDemo As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtDemoValidity As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents RBttnPackage4 As System.Windows.Forms.RadioButton
    Friend WithEvents RBttnPackage2 As System.Windows.Forms.RadioButton
    Friend WithEvents RBttnPackage5 As System.Windows.Forms.RadioButton
    Friend WithEvents RBttnPackage3 As System.Windows.Forms.RadioButton
    Friend WithEvents RBttnPackage1 As System.Windows.Forms.RadioButton
    Friend WithEvents RBttnPackage6 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkClinic As System.Windows.Forms.CheckBox
    Friend WithEvents ChkFrenchise As System.Windows.Forms.CheckBox
    Friend WithEvents CmbAppType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbPPP As System.Windows.Forms.ComboBox
    Friend WithEvents TxtStartDate As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.RBttnRegistered = New System.Windows.Forms.RadioButton
        Me.RBttnDemo = New System.Windows.Forms.RadioButton
        Me.TxtDemoValidity = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtRegTo = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtValidUpto = New System.Windows.Forms.TextBox
        Me.TxtStartDate = New System.Windows.Forms.TextBox
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.ChkClinic = New System.Windows.Forms.CheckBox
        Me.ChkFrenchise = New System.Windows.Forms.CheckBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.CmbPPP = New System.Windows.Forms.ComboBox
        Me.CmbAppType = New System.Windows.Forms.ComboBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.ChkSerialWise = New System.Windows.Forms.CheckBox
        Me.ChkBatchWise = New System.Windows.Forms.CheckBox
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.RBttnPackage4 = New System.Windows.Forms.RadioButton
        Me.RBttnPackage2 = New System.Windows.Forms.RadioButton
        Me.RBttnPackage6 = New System.Windows.Forms.RadioButton
        Me.RBttnPackage5 = New System.Windows.Forms.RadioButton
        Me.RBttnPackage3 = New System.Windows.Forms.RadioButton
        Me.RBttnPackage1 = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.Panel1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.GroupBox6)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox8)
        Me.Panel1.Controls.Add(Me.GroupBox5)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.GroupBox7)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(12, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(585, 407)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Controls.Add(Me.RBttnRegistered)
        Me.GroupBox6.Controls.Add(Me.RBttnDemo)
        Me.GroupBox6.Controls.Add(Me.TxtDemoValidity)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.TxtRegTo)
        Me.GroupBox6.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox6.Location = New System.Drawing.Point(300, 38)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(271, 151)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Registration Setting"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(13, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 21)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Demo Validity"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RBttnRegistered
        '
        Me.RBttnRegistered.Location = New System.Drawing.Point(122, 52)
        Me.RBttnRegistered.Name = "RBttnRegistered"
        Me.RBttnRegistered.Size = New System.Drawing.Size(135, 20)
        Me.RBttnRegistered.TabIndex = 3
        Me.RBttnRegistered.TabStop = True
        Me.RBttnRegistered.Text = "Registered"
        Me.RBttnRegistered.UseVisualStyleBackColor = True
        '
        'RBttnDemo
        '
        Me.RBttnDemo.Location = New System.Drawing.Point(122, 78)
        Me.RBttnDemo.Name = "RBttnDemo"
        Me.RBttnDemo.Size = New System.Drawing.Size(135, 20)
        Me.RBttnDemo.TabIndex = 3
        Me.RBttnDemo.TabStop = True
        Me.RBttnDemo.Text = "Demo"
        Me.RBttnDemo.UseVisualStyleBackColor = True
        '
        'TxtDemoValidity
        '
        Me.TxtDemoValidity.BackColor = System.Drawing.Color.White
        Me.TxtDemoValidity.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDemoValidity.Location = New System.Drawing.Point(122, 104)
        Me.TxtDemoValidity.MaxLength = 50
        Me.TxtDemoValidity.Name = "TxtDemoValidity"
        Me.TxtDemoValidity.Size = New System.Drawing.Size(96, 21)
        Me.TxtDemoValidity.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(15, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 21)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Registered To"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtRegTo
        '
        Me.TxtRegTo.BackColor = System.Drawing.Color.White
        Me.TxtRegTo.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtRegTo.Location = New System.Drawing.Point(122, 25)
        Me.TxtRegTo.MaxLength = 50
        Me.TxtRegTo.Name = "TxtRegTo"
        Me.TxtRegTo.Size = New System.Drawing.Size(135, 21)
        Me.TxtRegTo.TabIndex = 8
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtValidUpto)
        Me.GroupBox3.Controls.Add(Me.TxtStartDate)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 38)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(282, 151)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "General Setting"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(10, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(160, 21)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Software Valid Upto"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(10, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(160, 21)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Software Starting Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtValidUpto
        '
        Me.TxtValidUpto.BackColor = System.Drawing.Color.White
        Me.TxtValidUpto.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtValidUpto.Location = New System.Drawing.Point(176, 78)
        Me.TxtValidUpto.MaxLength = 50
        Me.TxtValidUpto.Name = "TxtValidUpto"
        Me.TxtValidUpto.Size = New System.Drawing.Size(96, 21)
        Me.TxtValidUpto.TabIndex = 8
        '
        'TxtStartDate
        '
        Me.TxtStartDate.BackColor = System.Drawing.Color.White
        Me.TxtStartDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtStartDate.Location = New System.Drawing.Point(176, 53)
        Me.TxtStartDate.MaxLength = 50
        Me.TxtStartDate.Name = "TxtStartDate"
        Me.TxtStartDate.Size = New System.Drawing.Size(96, 21)
        Me.TxtStartDate.TabIndex = 8
        '
        'GroupBox8
        '
        Me.GroupBox8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox8.Controls.Add(Me.ChkClinic)
        Me.GroupBox8.Controls.Add(Me.ChkFrenchise)
        Me.GroupBox8.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox8.Location = New System.Drawing.Point(12, 195)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(176, 100)
        Me.GroupBox8.TabIndex = 3
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Additional Fetures"
        '
        'ChkClinic
        '
        Me.ChkClinic.Location = New System.Drawing.Point(37, 53)
        Me.ChkClinic.Name = "ChkClinic"
        Me.ChkClinic.Size = New System.Drawing.Size(103, 23)
        Me.ChkClinic.TabIndex = 2
        Me.ChkClinic.Text = "Clinic"
        Me.ChkClinic.UseVisualStyleBackColor = True
        '
        'ChkFrenchise
        '
        Me.ChkFrenchise.Location = New System.Drawing.Point(37, 24)
        Me.ChkFrenchise.Name = "ChkFrenchise"
        Me.ChkFrenchise.Size = New System.Drawing.Size(103, 23)
        Me.ChkFrenchise.TabIndex = 2
        Me.ChkFrenchise.Text = "Frenchise"
        Me.ChkFrenchise.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.CmbPPP)
        Me.GroupBox5.Controls.Add(Me.CmbAppType)
        Me.GroupBox5.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox5.Location = New System.Drawing.Point(395, 195)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(176, 100)
        Me.GroupBox5.TabIndex = 3
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Application Type"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(6, 53)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 21)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "PPP"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(6, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 21)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Type"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbPPP
        '
        Me.CmbPPP.FormattingEnabled = True
        Me.CmbPPP.Items.AddRange(New Object() {"Default", "User Define"})
        Me.CmbPPP.Location = New System.Drawing.Point(53, 53)
        Me.CmbPPP.Name = "CmbPPP"
        Me.CmbPPP.Size = New System.Drawing.Size(117, 21)
        Me.CmbPPP.TabIndex = 0
        '
        'CmbAppType
        '
        Me.CmbAppType.FormattingEnabled = True
        Me.CmbAppType.Items.AddRange(New Object() {"Retailer", "Wholesaler"})
        Me.CmbAppType.Location = New System.Drawing.Point(53, 26)
        Me.CmbAppType.Name = "CmbAppType"
        Me.CmbAppType.Size = New System.Drawing.Size(117, 21)
        Me.CmbAppType.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox4.Controls.Add(Me.ChkSerialWise)
        Me.GroupBox4.Controls.Add(Me.ChkBatchWise)
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox4.Location = New System.Drawing.Point(194, 195)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(195, 100)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Items Behaviour"
        '
        'ChkSerialWise
        '
        Me.ChkSerialWise.Location = New System.Drawing.Point(36, 53)
        Me.ChkSerialWise.Name = "ChkSerialWise"
        Me.ChkSerialWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkSerialWise.Size = New System.Drawing.Size(122, 23)
        Me.ChkSerialWise.TabIndex = 2
        Me.ChkSerialWise.Text = "Serial No wise"
        Me.ChkSerialWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkSerialWise.UseVisualStyleBackColor = True
        '
        'ChkBatchWise
        '
        Me.ChkBatchWise.Location = New System.Drawing.Point(36, 24)
        Me.ChkBatchWise.Name = "ChkBatchWise"
        Me.ChkBatchWise.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkBatchWise.Size = New System.Drawing.Size(122, 23)
        Me.ChkBatchWise.TabIndex = 2
        Me.ChkBatchWise.Text = "Batch No wise"
        Me.ChkBatchWise.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkBatchWise.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox7.Controls.Add(Me.RBttnPackage4)
        Me.GroupBox7.Controls.Add(Me.RBttnPackage2)
        Me.GroupBox7.Controls.Add(Me.RBttnPackage6)
        Me.GroupBox7.Controls.Add(Me.RBttnPackage5)
        Me.GroupBox7.Controls.Add(Me.RBttnPackage3)
        Me.GroupBox7.Controls.Add(Me.RBttnPackage1)
        Me.GroupBox7.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox7.Location = New System.Drawing.Point(10, 301)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(381, 92)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Package Selected"
        '
        'RBttnPackage4
        '
        Me.RBttnPackage4.Location = New System.Drawing.Point(113, 49)
        Me.RBttnPackage4.Name = "RBttnPackage4"
        Me.RBttnPackage4.Size = New System.Drawing.Size(101, 20)
        Me.RBttnPackage4.TabIndex = 3
        Me.RBttnPackage4.TabStop = True
        Me.RBttnPackage4.Text = "Rs. 25,000"
        Me.RBttnPackage4.UseVisualStyleBackColor = True
        '
        'RBttnPackage2
        '
        Me.RBttnPackage2.Location = New System.Drawing.Point(6, 49)
        Me.RBttnPackage2.Name = "RBttnPackage2"
        Me.RBttnPackage2.Size = New System.Drawing.Size(101, 20)
        Me.RBttnPackage2.TabIndex = 3
        Me.RBttnPackage2.TabStop = True
        Me.RBttnPackage2.Text = "Rs. 15,000"
        Me.RBttnPackage2.UseVisualStyleBackColor = True
        '
        'RBttnPackage6
        '
        Me.RBttnPackage6.Location = New System.Drawing.Point(220, 49)
        Me.RBttnPackage6.Name = "RBttnPackage6"
        Me.RBttnPackage6.Size = New System.Drawing.Size(154, 20)
        Me.RBttnPackage6.TabIndex = 3
        Me.RBttnPackage6.TabStop = True
        Me.RBttnPackage6.Text = "Rs. 35,000 Supper"
        Me.RBttnPackage6.UseVisualStyleBackColor = True
        '
        'RBttnPackage5
        '
        Me.RBttnPackage5.Location = New System.Drawing.Point(220, 23)
        Me.RBttnPackage5.Name = "RBttnPackage5"
        Me.RBttnPackage5.Size = New System.Drawing.Size(154, 20)
        Me.RBttnPackage5.TabIndex = 3
        Me.RBttnPackage5.TabStop = True
        Me.RBttnPackage5.Text = "Rs. 30,000"
        Me.RBttnPackage5.UseVisualStyleBackColor = True
        '
        'RBttnPackage3
        '
        Me.RBttnPackage3.Location = New System.Drawing.Point(113, 23)
        Me.RBttnPackage3.Name = "RBttnPackage3"
        Me.RBttnPackage3.Size = New System.Drawing.Size(101, 20)
        Me.RBttnPackage3.TabIndex = 3
        Me.RBttnPackage3.TabStop = True
        Me.RBttnPackage3.Text = "Rs. 20,000"
        Me.RBttnPackage3.UseVisualStyleBackColor = True
        '
        'RBttnPackage1
        '
        Me.RBttnPackage1.Location = New System.Drawing.Point(6, 23)
        Me.RBttnPackage1.Name = "RBttnPackage1"
        Me.RBttnPackage1.Size = New System.Drawing.Size(101, 20)
        Me.RBttnPackage1.TabIndex = 3
        Me.RBttnPackage1.TabStop = True
        Me.RBttnPackage1.Text = "Rs. 10,000"
        Me.RBttnPackage1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(397, 301)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(176, 92)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(91, 25)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(72, 43)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(13, 25)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(72, 43)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(583, 35)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Options / Customization"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'frmOPTIONS
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(609, 427)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmOPTIONS"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Options / Customization"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asInsert As New AssInsert
    Dim asUpdate As New AssUpdate
    Dim asDelete As New AssDelete
    Dim asSELECT As New AssSelect
    Dim asTXT As New AssTextBox
    Dim asNum As New AssNumPress
    Dim asMAX As New AssMaxNo
    Dim Rd As System.Data.SqlClient.SqlDataReader

#End Region

#Region "FORM CONTROL"
    Private Sub frmOPTIONS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        'Me.FillListView()
        'Me.FillComboBox_Company()

        'Me.BttnNew_Click(sender, e)

        'Me.DmnClaim.SelectedIndex = 1
        'Me.DmnStatus.SelectedIndex = 1
    End Sub

    Private Sub frmOPTIONS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtStartDate.GotFocus, TxtValidUpto.GotFocus, TxtDemoValidity.GotFocus, TxtRegTo.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtStartDate.LostFocus, TxtValidUpto.LostFocus, TxtDemoValidity.LostFocus, TxtRegTo.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Select Case Ctrl.Name
            Case "TxtStartDate"
                If sender.TextLength > 0 Then
                    sender.Text = CDate(sender.Text).ToString("dd-MMM-yyyy")

                End If
        End Select
    End Sub
#End Region

#Region "ComboBox Controls"
    ''Got and LostFocus
    'Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    CType(sender, ComboBox).BackColor = Color.LightSteelBlue
    '    CType(sender, ComboBox).SelectAll()
    'End Sub
    'Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    CType(sender, ComboBox).BackColor = Color.White
    'End Sub
#End Region

#Region "DOMAIN_UPDOWN EVENTS"
    ''Got and LostFocus
    'Private Sub DmnStatus_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    CType(sender, DomainUpDown).BackColor = Color.LightSteelBlue
    'End Sub
    'Private Sub DmnStatus_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
    '    CType(sender, DomainUpDown).BackColor = Color.White
    'End Sub
#End Region

#Region "ListView Control"
    '    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        On Error GoTo FIX
    '        Me.TxtItemID.Text = Me.ListView1.SelectedItems(0).Text
    '        If Not Me.TxtItemID.Text = Nothing Then
    '            Dim Str1 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM WHERE nCODE=" & Val(Me.TxtItemID.Text) & ""
    '            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
    '            Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

    '            Me.DsLUP_ITEM1.Clear()
    '            Me.daLUP_ITEM.Fill(Me.DsLUP_ITEM1.V_LUP_ITEM)

    '            'Me.BttnAuto.Enabled = False
    '            Me.DmnClaim.SelectedItem = Me.DmnClaim.Text
    '            Me.DmnStatus.SelectedItem = Me.DmnStatus.Text
    '        End If

    'FIX:
    '    End Sub
    '    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
    '            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
    '                Me.asDelete.DeleteValueIN("DELETE FROM LUP_ITEMS WHERE nCODE=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

    '                Me.FillListView()

    '                Me.BttnNew_Click(sender, New System.EventArgs)
    '            End If

    '        Else
    '            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
    '            Me.TxtName.Focus()
    '        End If

    '    End Sub
#End Region

#Region "Button Control"
    'Private Sub BttnAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.TxtItemID.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nCODE) FROM LUP_ITEMS") + 1
    '    Me.CmbCompany.Focus()
    'End Sub
    'Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
    '    Me.DsLUP_ITEM1.Clear()

    '    Me.TxtSearch.Text = Nothing

    '    Me.TxtItemID.Focus()
    '    'Me.BttnAuto.Enabled = True
    'End Sub
    'Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

    '    Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM LUP_ITEMS WHERE nCODE=" & Val(Me.TxtItemID.Text) & "")

    '    If Val(Me.TxtItemID.Text) <= 0 Then
    '        MsgBox("ITEM CODE can't be 'NULL', Click on 'AUTO CODE' for New ID", MsgBoxStyle.Exclamation, "(NS) - Wrong ID")
    '        Me.BttnNew.Focus()

    '    ElseIf Me.CmbCompany.Text = Nothing Or Me.CmbCompany.SelectedIndex = -1 Or Me.TxtName.Text = Nothing Or Me.TxtNick.Text = Nothing Or Val(Me.TxtCost.Text) <= 0 Or Val(Me.TxtRate.Text) <= 0 Or Val(Me.TxtRetail.Text) < 0 Or Me.TxtPackDesc.Text = Nothing Or Me.TxtPcsDesc.Text = Nothing Or Val(Me.TxtPPP.Text) <= 0 Or Val(Me.TxtB_Qty.Text) < 0 Or Val(Me.TxtB_Pcs.Text) < 0 Or Val(Me.TxtMinStock.Text) < 0 Or Val(Me.TxtMaxStock.Text) < 0 Or Val(Me.TxtOpenStock.Text) < 0 Or Val(Me.TxtOpenStockValue.Text) < 0 Or Val(Me.TxtSaleTax.Text) < 0 Or Me.DmnClaim.SelectedIndex = -1 Or Me.DmnStatus.SelectedIndex = -1 Then
    '        MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

    '        If Me.CmbCompany.Text = Nothing Or Me.CmbCompany.SelectedIndex = -1 Then
    '            Me.CmbCompany.Focus()

    '        ElseIf Me.TxtName.Text = Nothing Then
    '            Me.TxtName.Focus()

    '        ElseIf Me.TxtNick.Text = Nothing Then
    '            Me.TxtNick.Focus()

    '        ElseIf Val(Me.TxtCost.Text) <= 0 Then
    '            Me.TxtCost.Focus()

    '        ElseIf Val(Me.TxtRate.Text) <= 0 Then
    '            Me.TxtRate.Focus()

    '        ElseIf Val(Me.TxtRetail.Text) <= 0 Then
    '            Me.TxtRetail.Focus()

    '        ElseIf Me.TxtPackDesc.Text = Nothing Then
    '            Me.TxtPackDesc.Focus()

    '        ElseIf Me.TxtPcsDesc.Text = Nothing Then
    '            Me.TxtPcsDesc.Focus()

    '        ElseIf Val(Me.TxtPPP.Text) <= 0 Then
    '            Me.TxtPPP.Focus()

    '        ElseIf Val(Me.TxtB_Qty.Text) < 0 Then
    '            Me.TxtB_Qty.Focus()

    '        ElseIf Val(Me.TxtB_Pcs.Text) < 0 Then
    '            Me.TxtB_Pcs.Focus()

    '        ElseIf Val(Me.TxtMinStock.Text) < 0 Then
    '            Me.TxtMinStock.Focus()

    '        ElseIf Val(Me.TxtMaxStock.Text) < 0 Then
    '            Me.TxtMaxStock.Focus()

    '        ElseIf Val(Me.TxtOpenStock.Text) < 0 Then
    '            Me.TxtOpenStock.Focus()

    '        ElseIf Val(Me.TxtOpenStockValue.Text) < 0 Then
    '            Me.TxtOpenStockValue.Focus()

    '        ElseIf Val(Me.TxtSaleTax.Text) < 0 Then
    '            Me.TxtSaleTax.Focus()

    '        ElseIf Me.DmnClaim.SelectedIndex = -1 Then
    '            Me.DmnClaim.Focus()

    '        ElseIf Me.DmnStatus.SelectedIndex = -1 Then
    '            Me.DmnStatus.Focus()

    '        End If

    '    ElseIf Me.asSELECT.pFlg1 = False Then
    '        If MsgBox("Do you want to save '" & Me.TxtName.Text & "' & '" & Me.TxtNick.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
    '            'INSERT VALUES
    '            Me.asInsert.SaveValueIN("INSERT INTO LUP_ITEMS(nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, nUNIT_COST, nUNIT_RATE, nUNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, nVENDOR_CODE, nBONUS_QTY, nBONUS_ON_PCS, sCLAIMABLE, sSTATUS, nOPEN_STOCK, nOPEN_UNIT_VALUE) VALUES(" & Val(Me.TxtItemID.Text) & ",'" & Me.TxtName.Text & "','" & Me.TxtNick.Text & "'," & Val(Me.TxtPPP.Text) & ",'" & Me.TxtPackDesc.Text & "','" & Me.TxtPcsDesc.Text & "'," & Val(Me.TxtCost.Text) & "," & Val(Me.TxtRate.Text) & "," & Val(Me.TxtRetail.Text) & "," & Val(Me.TxtMinStock.Text) & "," & Val(Me.TxtMaxStock.Text) & "," & Val(Me.TxtSaleTax.Text) & "," & Val(Me.CmbCompany.SelectedItem.col2) & "," & Val(Me.TxtB_Qty.Text) & "," & Val(Me.TxtB_Pcs.Text) & ",'" & Me.DmnClaim.SelectedIndex & "','" & Me.DmnStatus.SelectedIndex & "'," & Val(Me.TxtOpenStock.Text) & "," & Val(Me.TxtOpenStockValue.Text) & ") ")

    '            'FILL THE RECORD IN LISTVIEW
    '            Me.FillListView()
    '            Me.TxtName.Focus()
    '        End If

    '    ElseIf Me.asSELECT.pFlg1 = True Then
    '        If MsgBox("This Item Code '" & Me.TxtName.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
    '            'UPDATE RECORD
    '            Me.asUpdate.UpdateValueIN("UPDATE LUP_ITEMS SET sITEM_NAME='" & Me.TxtName.Text & "', sNICK='" & Me.TxtNick.Text & "', nPPP=" & Val(Me.TxtPPP.Text) & ", sPACK_DESC='" & Me.TxtPackDesc.Text & "', sPIECE_DESC='" & Me.TxtPcsDesc.Text & "', nUNIT_COST=" & Val(Me.TxtCost.Text) & ", nUNIT_RATE=" & Val(Me.TxtRate.Text) & ", nUNIT_RETAIL=" & Val(Me.TxtRetail.Text) & ", nMIN_STOCK=" & Val(Me.TxtMinStock.Text) & ", nMAX_STOCK=" & Val(Me.TxtMaxStock.Text) & ", nSALE_TAX=" & Val(Me.TxtSaleTax.Text) & ", nVENDOR_CODE=" & Val(Me.CmbCompany.SelectedItem.col2) & ", nBONUS_QTY=" & Val(Me.TxtB_Qty.Text) & ", nBONUS_ON_PCS=" & Val(Me.TxtB_Pcs.Text) & ", sCLAIMABLE='" & Me.DmnClaim.SelectedIndex & "', sSTATUS='" & Me.DmnStatus.SelectedIndex & "', nOPEN_STOCK=" & Val(Me.TxtOpenStock.Text) & ", nOPEN_UNIT_VALUE=" & Val(Me.TxtOpenStockValue.Text) & " WHERE nCODE=" & Val(Me.TxtItemID.Text) & "")
    '            'FILL THE RECORD IN LISTVIEW
    '            Me.FillListView()
    '            Me.TxtName.Focus()
    '        End If

    '    End If


    'End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Sub and Functions"
    Public Shared Function EncryptText(ByVal strText As String) As String
        Return Encrypt(strText, "Neuro4742")
    End Function
    Private Shared Function Encrypt(ByVal strText As String, ByVal strEncrKey As String) As String
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(strEncrKey.ToString().Substring(0, 8))
        Dim des As New DESCryptoServiceProvider
        Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
        Dim ms As New MemoryStream
        Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()
        Return Convert.ToBase64String(ms.ToArray()).ToUpper
    End Function
#End Region

End Class
