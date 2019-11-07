Imports SDS = System.Data.SqlClient
Public Class frmLUP_EMPLOYEE
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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents CmbDesignation As MTGCComboBox
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents TxtCode As System.Windows.Forms.TextBox
    Friend WithEvents TxtFatherName As System.Windows.Forms.TextBox
    Friend WithEvents TxtPerAddress As System.Windows.Forms.TextBox
    Friend WithEvents TxtHomePh As System.Windows.Forms.TextBox
    Friend WithEvents TxtEmail As System.Windows.Forms.TextBox
    Friend WithEvents TxtCell As System.Windows.Forms.TextBox
    Friend WithEvents TxtBankAccount As System.Windows.Forms.TextBox
    Friend WithEvents TxtPreAddress As System.Windows.Forms.TextBox
    Friend WithEvents daLUP_EMPLOYEE As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TxtPay As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents BttnAdd As System.Windows.Forms.Button
    Friend WithEvents TxtNIC As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtBankAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents daLUP_DESIGNATION As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_DESIGNATION1 As Neruo_Business_Solution.dsLUP_DESIGNATION
    Friend WithEvents DsLUP_EMPLOYEE1 As Neruo_Business_Solution.dsLUP_EMPLOYEE
    Friend WithEvents DsLUP_EMPLOYEE11 As Neruo_Business_Solution.dsLUP_EMPLOYEE1
    Friend WithEvents TxtAppDate As System.Windows.Forms.TextBox
    Friend WithEvents TxtLeaveDate As System.Windows.Forms.TextBox
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents ChkWorking As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_EMPLOYEE))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.ChkWorking = New System.Windows.Forms.CheckBox
        Me.DsLUP_EMPLOYEE1 = New Neruo_Business_Solution.dsLUP_EMPLOYEE
        Me.TxtNIC = New System.Windows.Forms.MaskedTextBox
        Me.BttnAdd = New System.Windows.Forms.Button
        Me.TxtPreAddress = New System.Windows.Forms.TextBox
        Me.CmbDesignation = New MTGCComboBox
        Me.TxtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtCode = New System.Windows.Forms.TextBox
        Me.TxtFatherName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtBankAddress = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtPerAddress = New System.Windows.Forms.TextBox
        Me.TxtHomePh = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtEmail = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtCell = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.TxtPay = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtLeaveDate = New System.Windows.Forms.TextBox
        Me.TxtAppDate = New System.Windows.Forms.TextBox
        Me.TxtBankAccount = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_EMPLOYEE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_DESIGNATION = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_DESIGNATION1 = New Neruo_Business_Solution.dsLUP_DESIGNATION
        Me.DsLUP_EMPLOYEE11 = New Neruo_Business_Solution.dsLUP_EMPLOYEE1
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_DESIGNATION1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_EMPLOYEE11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnNextForm)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.BttnPrevForm)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(12, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(694, 524)
        Me.Panel1.TabIndex = 0
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(581, 3)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(101, 46)
        Me.BttnNextForm.TabIndex = 41
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Vans Detail"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 342)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(671, 174)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader3, Me.ColumnHeader5})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 50)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(665, 121)
        Me.ListView1.TabIndex = 2
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Name"
        Me.ColumnHeader2.Width = 197
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Designation"
        Me.ColumnHeader4.Width = 171
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Pay"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Cell #"
        Me.ColumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader5.Width = 133
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 23)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Sea&rch"
        '
        'TxtSearch
        '
        Me.TxtSearch.BackColor = System.Drawing.Color.White
        Me.TxtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(72, 24)
        Me.TxtSearch.MaxLength = 50
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(192, 23)
        Me.TxtSearch.TabIndex = 1
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(11, 3)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(101, 46)
        Me.BttnPrevForm.TabIndex = 40
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Employee Designation"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(562, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 286)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(16, 19)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(16, 241)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(16, 130)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.ChkWorking)
        Me.GroupBox3.Controls.Add(Me.TxtNIC)
        Me.GroupBox3.Controls.Add(Me.BttnAdd)
        Me.GroupBox3.Controls.Add(Me.TxtPreAddress)
        Me.GroupBox3.Controls.Add(Me.CmbDesignation)
        Me.GroupBox3.Controls.Add(Me.TxtName)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtCode)
        Me.GroupBox3.Controls.Add(Me.TxtFatherName)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtBankAddress)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.TxtPerAddress)
        Me.GroupBox3.Controls.Add(Me.TxtHomePh)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TxtEmail)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.TxtCell)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.TxtPay)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.TxtLeaveDate)
        Me.GroupBox3.Controls.Add(Me.TxtAppDate)
        Me.GroupBox3.Controls.Add(Me.TxtBankAccount)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(11, 48)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(545, 288)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'ChkWorking
        '
        Me.ChkWorking.Checked = True
        Me.ChkWorking.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkWorking.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.STATUS", True))
        Me.ChkWorking.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkWorking.Location = New System.Drawing.Point(298, 196)
        Me.ChkWorking.Name = "ChkWorking"
        Me.ChkWorking.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkWorking.Size = New System.Drawing.Size(102, 23)
        Me.ChkWorking.TabIndex = 29
        Me.ChkWorking.Text = "Working"
        Me.ChkWorking.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkWorking.UseVisualStyleBackColor = True
        '
        'DsLUP_EMPLOYEE1
        '
        Me.DsLUP_EMPLOYEE1.DataSetName = "dsLUP_EMPLOYEE"
        Me.DsLUP_EMPLOYEE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtNIC
        '
        Me.TxtNIC.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.NIC", True))
        Me.TxtNIC.Location = New System.Drawing.Point(96, 94)
        Me.TxtNIC.Mask = "00000-0000000-0"
        Me.TxtNIC.Name = "TxtNIC"
        Me.TxtNIC.Size = New System.Drawing.Size(153, 23)
        Me.TxtNIC.TabIndex = 8
        '
        'BttnAdd
        '
        Me.BttnAdd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnAdd.Location = New System.Drawing.Point(216, 18)
        Me.BttnAdd.Name = "BttnAdd"
        Me.BttnAdd.Size = New System.Drawing.Size(33, 23)
        Me.BttnAdd.TabIndex = 2
        Me.BttnAdd.TabStop = False
        Me.BttnAdd.Text = "+&1"
        '
        'TxtPreAddress
        '
        Me.TxtPreAddress.BackColor = System.Drawing.Color.White
        Me.TxtPreAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.PRE_ADD", True))
        Me.TxtPreAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPreAddress.Location = New System.Drawing.Point(96, 171)
        Me.TxtPreAddress.MaxLength = 200
        Me.TxtPreAddress.Multiline = True
        Me.TxtPreAddress.Name = "TxtPreAddress"
        Me.TxtPreAddress.Size = New System.Drawing.Size(196, 48)
        Me.TxtPreAddress.TabIndex = 12
        '
        'CmbDesignation
        '
        Me.CmbDesignation.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbDesignation.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbDesignation.ColumnNum = 2
        Me.CmbDesignation.ColumnWidth = "140;40"
        Me.CmbDesignation.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.DESIGNATION", True))
        Me.CmbDesignation.DisplayMember = "Text"
        Me.CmbDesignation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbDesignation.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbDesignation.DropDownForeColor = System.Drawing.Color.White
        Me.CmbDesignation.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbDesignation.DropDownWidth = 220
        Me.CmbDesignation.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbDesignation.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbDesignation.GridLineHorizontal = False
        Me.CmbDesignation.GridLineVertical = True
        Me.CmbDesignation.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbDesignation.Location = New System.Drawing.Point(96, 222)
        Me.CmbDesignation.ManagingFastMouseMoving = True
        Me.CmbDesignation.ManagingFastMouseMovingInterval = 30
        Me.CmbDesignation.Name = "CmbDesignation"
        Me.CmbDesignation.Size = New System.Drawing.Size(196, 24)
        Me.CmbDesignation.TabIndex = 14
        '
        'TxtName
        '
        Me.TxtName.BackColor = System.Drawing.Color.White
        Me.TxtName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.NAME", True))
        Me.TxtName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.Location = New System.Drawing.Point(96, 44)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(196, 23)
        Me.TxtName.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Name*"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "ID (Auto)*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCode
        '
        Me.TxtCode.BackColor = System.Drawing.Color.White
        Me.TxtCode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.Location = New System.Drawing.Point(96, 18)
        Me.TxtCode.MaxLength = 50
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.ReadOnly = True
        Me.TxtCode.Size = New System.Drawing.Size(120, 23)
        Me.TxtCode.TabIndex = 1
        Me.TxtCode.TabStop = False
        '
        'TxtFatherName
        '
        Me.TxtFatherName.BackColor = System.Drawing.Color.White
        Me.TxtFatherName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.FATHER_NAME", True))
        Me.TxtFatherName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFatherName.Location = New System.Drawing.Point(96, 69)
        Me.TxtFatherName.Name = "TxtFatherName"
        Me.TxtFatherName.Size = New System.Drawing.Size(196, 23)
        Me.TxtFatherName.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 23)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "N.I.C #*"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 69)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 23)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Father Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(8, 172)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(88, 48)
        Me.Label24.TabIndex = 11
        Me.Label24.Text = "Present Address"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(298, 120)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 48)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Bank Address"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtBankAddress
        '
        Me.TxtBankAddress.BackColor = System.Drawing.Color.White
        Me.TxtBankAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.BANK_ADD", True))
        Me.TxtBankAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAddress.Location = New System.Drawing.Point(382, 120)
        Me.TxtBankAddress.MaxLength = 200
        Me.TxtBankAddress.Multiline = True
        Me.TxtBankAddress.Name = "TxtBankAddress"
        Me.TxtBankAddress.Size = New System.Drawing.Size(152, 48)
        Me.TxtBankAddress.TabIndex = 26
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 48)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Permanent Address"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPerAddress
        '
        Me.TxtPerAddress.BackColor = System.Drawing.Color.White
        Me.TxtPerAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.PER_ADD", True))
        Me.TxtPerAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPerAddress.Location = New System.Drawing.Point(96, 120)
        Me.TxtPerAddress.MaxLength = 200
        Me.TxtPerAddress.Multiline = True
        Me.TxtPerAddress.Name = "TxtPerAddress"
        Me.TxtPerAddress.Size = New System.Drawing.Size(196, 48)
        Me.TxtPerAddress.TabIndex = 10
        '
        'TxtHomePh
        '
        Me.TxtHomePh.BackColor = System.Drawing.Color.White
        Me.TxtHomePh.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.HOME_PH", True))
        Me.TxtHomePh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHomePh.Location = New System.Drawing.Point(382, 18)
        Me.TxtHomePh.Name = "TxtHomePh"
        Me.TxtHomePh.Size = New System.Drawing.Size(152, 23)
        Me.TxtHomePh.TabIndex = 18
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(298, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 23)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Home Ph."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(298, 222)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 23)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Leave Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(298, 171)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(84, 23)
        Me.Label19.TabIndex = 27
        Me.Label19.Text = "Joining Date"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(298, 69)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 23)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "E-mail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtEmail
        '
        Me.TxtEmail.BackColor = System.Drawing.Color.White
        Me.TxtEmail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.EMAIL_ADD", True))
        Me.TxtEmail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmail.Location = New System.Drawing.Point(382, 69)
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.Size = New System.Drawing.Size(152, 23)
        Me.TxtEmail.TabIndex = 22
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(298, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(84, 23)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Cell #"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCell
        '
        Me.TxtCell.BackColor = System.Drawing.Color.White
        Me.TxtCell.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.CELL", True))
        Me.TxtCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCell.Location = New System.Drawing.Point(382, 44)
        Me.TxtCell.Name = "TxtCell"
        Me.TxtCell.Size = New System.Drawing.Size(152, 23)
        Me.TxtCell.TabIndex = 20
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(8, 251)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(88, 23)
        Me.Label18.TabIndex = 15
        Me.Label18.Text = "Pay*"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPay
        '
        Me.TxtPay.BackColor = System.Drawing.Color.White
        Me.TxtPay.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.PAY", True))
        Me.TxtPay.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPay.Location = New System.Drawing.Point(96, 252)
        Me.TxtPay.Name = "TxtPay"
        Me.TxtPay.Size = New System.Drawing.Size(120, 23)
        Me.TxtPay.TabIndex = 16
        Me.TxtPay.Text = "0"
        Me.TxtPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(298, 94)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(84, 23)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Bank Acc"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtLeaveDate
        '
        Me.TxtLeaveDate.BackColor = System.Drawing.Color.White
        Me.TxtLeaveDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.LEAVE_DATE", True))
        Me.TxtLeaveDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLeaveDate.Location = New System.Drawing.Point(382, 222)
        Me.TxtLeaveDate.Name = "TxtLeaveDate"
        Me.TxtLeaveDate.Size = New System.Drawing.Size(109, 23)
        Me.TxtLeaveDate.TabIndex = 31
        '
        'TxtAppDate
        '
        Me.TxtAppDate.BackColor = System.Drawing.Color.White
        Me.TxtAppDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.APP_DATE", True))
        Me.TxtAppDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAppDate.Location = New System.Drawing.Point(382, 171)
        Me.TxtAppDate.Name = "TxtAppDate"
        Me.TxtAppDate.Size = New System.Drawing.Size(109, 23)
        Me.TxtAppDate.TabIndex = 28
        '
        'TxtBankAccount
        '
        Me.TxtBankAccount.BackColor = System.Drawing.Color.White
        Me.TxtBankAccount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_EMPLOYEE1, "V_EMPLOYEE_INFO.BANK_ACC", True))
        Me.TxtBankAccount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAccount.Location = New System.Drawing.Point(382, 94)
        Me.TxtBankAccount.Name = "TxtBankAccount"
        Me.TxtBankAccount.Size = New System.Drawing.Size(152, 23)
        Me.TxtBankAccount.TabIndex = 24
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 222)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 23)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "Designation*"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(692, 45)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "EMPLOYEE(s) DETAIL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daLUP_EMPLOYEE
        '
        Me.daLUP_EMPLOYEE.DeleteCommand = Me.SqlCommand1
        Me.daLUP_EMPLOYEE.SelectCommand = Me.SqlCommand3
        Me.daLUP_EMPLOYEE.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_EMPLOYEE_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("FATHER_NAME", "FATHER_NAME"), New System.Data.Common.DataColumnMapping("NIC", "NIC"), New System.Data.Common.DataColumnMapping("HOME_PH", "HOME_PH"), New System.Data.Common.DataColumnMapping("CELL", "CELL"), New System.Data.Common.DataColumnMapping("PRE_ADD", "PRE_ADD"), New System.Data.Common.DataColumnMapping("PER_ADD", "PER_ADD"), New System.Data.Common.DataColumnMapping("DESIGNATION", "DESIGNATION"), New System.Data.Common.DataColumnMapping("APP_DATE", "APP_DATE"), New System.Data.Common.DataColumnMapping("PAY", "PAY"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("LEAVE_DATE", "LEAVE_DATE"), New System.Data.Common.DataColumnMapping("EMAIL_ADD", "EMAIL_ADD"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("BANK_ADD", "BANK_ADD")})})
        Me.daLUP_EMPLOYEE.UpdateCommand = Me.SqlCommand4
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "DELETE FROM LUP_CLIENT_GD WHERE (nCODE = @Original_nCODE) AND (nMAX_LIM = @Origin" & _
            "al_nMAX_LIM) AND (nMIN_LIM = @Original_nMIN_LIM) AND (sDESC = @Original_sDESC)"
        Me.SqlCommand1.Connection = Me.SqlConnection1
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand3
        '
        Me.SqlCommand3.CommandText = resources.GetString("SqlCommand3.CommandText")
        Me.SqlCommand3.Connection = Me.SqlConnection1
        '
        'SqlCommand4
        '
        Me.SqlCommand4.CommandText = resources.GetString("SqlCommand4.CommandText")
        Me.SqlCommand4.Connection = Me.SqlConnection1
        Me.SqlCommand4.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'daLUP_DESIGNATION
        '
        Me.daLUP_DESIGNATION.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_DESIGNATION.InsertCommand = Me.SqlInsertCommand1
        Me.daLUP_DESIGNATION.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_DESIGNATION.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_DESIGNATION", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("DESIGNATION", "DESIGNATION"), New System.Data.Common.DataColumnMapping("PAY_SCALE", "PAY_SCALE")})})
        Me.daLUP_DESIGNATION.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "DELETE FROM [LUP_DESIGNATION] WHERE (([nCODE] = @Original_CODE) AND ([sDESC] = @O" & _
            "riginal_DESIGNATION) AND ([nPAY_SCALE] = @Original_PAY_SCALE))"
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_CODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "CODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_DESIGNATION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "DESIGNATION", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_PAY_SCALE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "PAY_SCALE", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = resources.GetString("SqlInsertCommand1.CommandText")
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@DESIGNATION", System.Data.SqlDbType.VarChar, 0, "DESIGNATION"), New System.Data.SqlClient.SqlParameter("@PAY_SCALE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "PAY_SCALE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     nCODE AS CODE, sDESC AS DESIGNATION, nPAY_SCALE AS PAY_SCALE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM    " & _
            "     LUP_DESIGNATION"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@DESIGNATION", System.Data.SqlDbType.VarChar, 0, "DESIGNATION"), New System.Data.SqlClient.SqlParameter("@PAY_SCALE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "PAY_SCALE", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Original_CODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "CODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_DESIGNATION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "DESIGNATION", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_PAY_SCALE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "PAY_SCALE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "CODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsLUP_DESIGNATION1
        '
        Me.DsLUP_DESIGNATION1.DataSetName = "dsLUP_DESIGNATION"
        Me.DsLUP_DESIGNATION1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_EMPLOYEE11
        '
        Me.DsLUP_EMPLOYEE11.DataSetName = "dsLUP_EMPLOYEE1"
        Me.DsLUP_EMPLOYEE11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_EMPLOYEE
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(718, 540)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_EMPLOYEE"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EMPLOYEE INFORMATION"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_DESIGNATION1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_EMPLOYEE11, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_EMPLOYEE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Me.FillComboBox_DESIGNATION()

        Me.BttnNew_Click(sender, e)
        Me.ChkWorking.Checked = True

    End Sub

    Private Sub frmLUP_EMPLOYEE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtName.GotFocus, TxtCode.GotFocus, TxtFatherName.GotFocus, TxtPerAddress.GotFocus, TxtHomePh.GotFocus, TxtEmail.GotFocus, TxtCell.GotFocus, TxtPreAddress.GotFocus, TxtBankAccount.GotFocus, TxtPay.GotFocus, TxtBankAddress.GotFocus, TxtAppDate.GotFocus, TxtLeaveDate.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtName.LostFocus, TxtCode.LostFocus, TxtFatherName.LostFocus, TxtPerAddress.LostFocus, TxtHomePh.LostFocus, TxtEmail.LostFocus, TxtCell.LostFocus, TxtPreAddress.LostFocus, TxtBankAccount.LostFocus, TxtPay.LostFocus, TxtBankAddress.LostFocus, TxtAppDate.LostFocus, TxtLeaveDate.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtAppDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

                Case "TxtLeaveDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub

    Private Sub Txt_Masked_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNIC.GotFocus
        CType(sender, MaskedTextBox).BackColor = Color.LightSteelBlue
        CType(sender, MaskedTextBox).SelectAll()
    End Sub
    Private Sub Txt_Masked_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNIC.LostFocus
        CType(sender, MaskedTextBox).BackColor = Color.White
    End Sub

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtHomePh.KeyPress, TxtCell.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtPay.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        If Me.TxtSearch.Text = Nothing Then
            Me.FillListView()

        Else
            Me.FillListView_Condition()
        End If
    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDesignation.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbDesignation.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "CheckBox EVENTS"
    Private Sub ChkWorking_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkWorking.CheckedChanged
        Me.TxtLeaveDate.Enabled = Not Me.ChkWorking.Checked

        If Me.ChkWorking.Checked = False Then
            Me.TxtLeaveDate.Focus()
        End If
    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        Me.TxtCode.Text = Me.ListView1.SelectedItems(0).Text
        If Not Me.TxtCode.Text = Nothing Then
            Dim Str1 As String = "SELECT CODE, NAME, FATHER_NAME, NIC, HOME_PH, CELL, PRE_ADD, PER_ADD, DESIGNATION, APP_DATE, PAY, STATUS, LEAVE_DATE, EMAIL_ADD, BANK_ACC, BANK_ADD FROM V_EMPLOYEE_INFO WHERE CODE=" & Val(Me.TxtCode.Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_EMPLOYEE = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_EMPLOYEE1.Clear()
            Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO)

            Me.BttnAdd.Enabled = False
        End If

FIX:
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(2).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM LUP_EMPLOYEE WHERE nCODE=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

                Me.FillListView()

                Me.BttnNew_Click(sender, New System.EventArgs)
            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            Me.TxtName.Focus()
        End If

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAdd.Click
        Me.TxtCode.Text = Val(Me.TxtCode.Text) + 1
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.DsLUP_EMPLOYEE1.Clear()

        Me.TxtSearch.Text = Nothing
        Me.TxtCode.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nCODE) FROM LUP_EMPLOYEE") + 1
        Me.TxtName.Focus()
        Me.BttnAdd.Enabled = True
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click



        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM LUP_EMPLOYEE WHERE nCODE=" & Val(Me.TxtCode.Text) & "")

        If Val(Me.TxtCode.Text) <= 0 Then
            MsgBox("Employee ID can't be 'NULL', Click on 'NEW' for New ID", MsgBoxStyle.Exclamation, "(NS) - Wrong ID")
            Me.BttnNew.Focus()

        ElseIf Me.TxtName.Text = Nothing Or Me.TxtFatherName.Text = Nothing Or Me.TxtNIC.Text = Nothing Or Me.CmbDesignation.SelectedIndex = -1 Or Me.CmbDesignation.Text = Nothing Or Val(Me.TxtPay.Text) <= 0 Or Me.TxtAppDate.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtName.Text = Nothing Then
                Me.TxtName.Focus()

            ElseIf Me.TxtFatherName.Text = Nothing Then
                Me.TxtFatherName.Focus()

            ElseIf Me.TxtNIC.Text = Nothing Then
                Me.TxtNIC.Focus()

            ElseIf Me.CmbDesignation.Text = Nothing Or Me.CmbDesignation.SelectedIndex = -1 Then
                Me.CmbDesignation.Focus()

            ElseIf Val(Me.TxtPay.Text) <= 0 Then
                Me.TxtPay.Focus()

            ElseIf Me.TxtAppDate.Text = Nothing Then
                Me.TxtAppDate.Focus()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            If MsgBox("Do you want to save '" & Me.TxtName.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                If Me.TxtLeaveDate.Enabled = True Then
                    If Me.TxtLeaveDate.Text = Nothing Then
                        MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                        Me.TxtLeaveDate.Focus()

                        Exit Sub
                    End If

                    Me.asInsert.SaveValueIN("INSERT INTO LUP_EMPLOYEE(sNAME, sFATHER_NAME, sNIC, sHOME_PH, sCELL, sPRE_ADDRESS, sPER_ADDRESS, nDESIG_CODE, dAPP_DATE, nPAY, sSTATUS, dLEAVE_DATE, sEMAIL_ADD, sBANK_ACCOUNT, sBRANCH_ADD) VALUES('" & Me.TxtName.Text & "','" & Me.TxtFatherName.Text & "','" & Me.TxtNIC.Text & "','" & Me.TxtHomePh.Text & "','" & Me.TxtCell.Text & "','" & Me.TxtPreAddress.Text & "','" & Me.TxtPerAddress.Text & "'," & Val(Me.CmbDesignation.SelectedItem.col2) & ",'" & CDate(Me.TxtAppDate.Text).ToString("MM-dd-yyyy") & "'," & Val(Me.TxtPay.Text) & ",'" & Me.ChkWorking.CheckState & "','" & CDate(Me.TxtLeaveDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.TxtEmail.Text & "','" & Me.TxtBankAccount.Text & "','" & Me.TxtBankAddress.Text & "') ")

                Else
                    Me.asInsert.SaveValueIN("INSERT INTO LUP_EMPLOYEE(sNAME, sFATHER_NAME, sNIC, sHOME_PH, sCELL, sPRE_ADDRESS, sPER_ADDRESS, nDESIG_CODE, dAPP_DATE, nPAY, sSTATUS, sEMAIL_ADD, sBANK_ACCOUNT, sBRANCH_ADD) VALUES('" & Me.TxtName.Text & "','" & Me.TxtFatherName.Text & "','" & Me.TxtNIC.Text & "','" & Me.TxtHomePh.Text & "','" & Me.TxtCell.Text & "','" & Me.TxtPreAddress.Text & "','" & Me.TxtPerAddress.Text & "'," & Val(Me.CmbDesignation.SelectedItem.col2) & ",'" & CDate(Me.TxtAppDate.Text).ToString("MM-dd-yyyy") & "'," & Val(Me.TxtPay.Text) & ",'" & Me.ChkWorking.CheckState & "', '" & Me.TxtEmail.Text & "','" & Me.TxtBankAccount.Text & "','" & Me.TxtBankAddress.Text & "') ")

                End If


                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtName.Focus()
            End If

        ElseIf Me.asSELECT.pFlg1 = True Then
            If MsgBox("This Employee ID '" & Me.TxtName.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                If Me.TxtLeaveDate.Enabled = True Or Me.TxtAppDate.Text = Nothing Then
                    MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                    If Me.TxtAppDate.Text = Nothing Then
                        Me.TxtAppDate.Focus()
                        Exit Sub

                    ElseIf Me.TxtLeaveDate.Text = Nothing Then
                        Me.TxtLeaveDate.Focus()
                        Exit Sub

                    End If

                    Me.asUpdate.UpdateValueIN("UPDATE LUP_EMPLOYEE SET sNAME='" & Me.TxtName.Text & "', sFATHER_NAME='" & Me.TxtFatherName.Text & "', sNIC='" & Me.TxtNIC.Text & "', sHOME_PH='" & Me.TxtHomePh.Text & "', sCELL='" & Me.TxtCell.Text & "', sPRE_ADDRESS='" & Me.TxtPreAddress.Text & "', sPER_ADDRESS='" & Me.TxtPerAddress.Text & "', nDESIG_CODE=" & Val(Me.CmbDesignation.SelectedItem.col2) & ", dAPP_DATE='" & CDate(Me.TxtAppDate.Text).ToString("MM-dd-yyyy") & "', nPAY=" & Val(Me.TxtPay.Text) & ", sSTATUS='" & Me.ChkWorking.CheckState & "', sEMAIL_ADD='" & Me.TxtEmail.Text & "', sBANK_ACCOUNT='" & Me.TxtBankAccount.Text & "', sBRANCH_ADD='" & Me.TxtBankAddress.Text & "' WHERE nCODE=" & Val(Me.TxtCode.Text) & "")

                Else
                    If Me.TxtAppDate.Text = Nothing Then
                        MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                        Me.TxtAppDate.Focus()
                        Exit Sub

                    End If

                    Me.asUpdate.UpdateValueIN("UPDATE LUP_EMPLOYEE SET sNAME='" & Me.TxtName.Text & "', sFATHER_NAME='" & Me.TxtFatherName.Text & "', sNIC='" & Me.TxtNIC.Text & "', sHOME_PH='" & Me.TxtHomePh.Text & "', sCELL='" & Me.TxtCell.Text & "', sPRE_ADDRESS='" & Me.TxtPreAddress.Text & "', sPER_ADDRESS='" & Me.TxtPerAddress.Text & "', nDESIG_CODE=" & Val(Me.CmbDesignation.SelectedItem.col2) & ", dAPP_DATE='" & CDate(Me.TxtAppDate.Text).ToString("MM-dd-yyyy") & "', nPAY=" & Val(Me.TxtPay.Text) & ", sSTATUS='" & Me.ChkWorking.CheckState & "', dLEAVE_DATE=NULL, sEMAIL_ADD='" & Me.TxtEmail.Text & "', sBANK_ACCOUNT='" & Me.TxtBankAccount.Text & "', sBRANCH_ADD='" & Me.TxtBankAddress.Text & "' WHERE nCODE=" & Val(Me.TxtCode.Text) & "")

                End If

                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtName.Focus()
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        frmLUP_DESIGNATION.MdiParent = Me.ParentForm
        frmLUP_DESIGNATION.Show()
        frmLUP_DESIGNATION.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_VAN.MdiParent = Me.ParentForm
        frmLUP_VAN.Show()
        frmLUP_VAN.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"

    Private Sub FillComboBox_DESIGNATION()
        Dim Str1 As String = "SELECT nCODE AS CODE, sDESC AS DESIGNATION, nPAY_SCALE AS PAY_SCALE FROM LUP_DESIGNATION ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_DESIGNATION = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_DESIGNATION1.Clear()
        Me.daLUP_DESIGNATION.Fill(Me.DsLUP_DESIGNATION1.LUP_DESIGNATION)

        Dim dtLoading As New DataTable("LUP_DESIGNATION")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_DESIGNATION1.LUP_DESIGNATION.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_DESIGNATION1.LUP_DESIGNATION.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_DESIGNATION1.LUP_DESIGNATION.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbDesignation.SelectedIndex = -1
        Me.CmbDesignation.Items.Clear()
        Me.CmbDesignation.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbDesignation.SourceDataString = New String(1) {"sDESC", "CODE"}
        Me.CmbDesignation.SourceDataTable = dtLoading
    End Sub

    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT CODE, NAME, FATHER_NAME, NIC, HOME_PH, CELL, PRE_ADD, PER_ADD, DESIGNATION, APP_DATE, PAY, STATUS, LEAVE_DATE, EMAIL_ADD, BANK_ACC, BANK_ADD FROM V_EMPLOYEE_INFO"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_EMPLOYEE = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_EMPLOYEE11.Clear()
            Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(8).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(10).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(5).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try

            Dim Str1 As String = "SELECT CODE, NAME, FATHER_NAME, NIC, HOME_PH, CELL, PRE_ADD, PER_ADD, DESIGNATION, APP_DATE, PAY, STATUS, LEAVE_DATE, EMAIL_ADD, BANK_ACC, BANK_ADD FROM V_EMPLOYEE_INFO WHERE NAME LIKE '%" & Me.TxtSearch.Text & "%' ORDER BY NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_EMPLOYEE = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_EMPLOYEE11.Clear()
            Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(8).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(10).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO.Item(Cnt).Item(5).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


End Class
