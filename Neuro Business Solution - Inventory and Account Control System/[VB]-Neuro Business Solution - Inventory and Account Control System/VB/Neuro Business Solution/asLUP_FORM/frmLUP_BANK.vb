Imports SDS = System.Data.SqlClient
Public Class frmLUP_BANK
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
    Friend WithEvents GBoxEntry As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TxtBankName As System.Windows.Forms.TextBox
    Friend WithEvents TxtAccountNo As System.Windows.Forms.TextBox
    Friend WithEvents TxtBrName As System.Windows.Forms.TextBox
    Friend WithEvents TxtBrCode As System.Windows.Forms.TextBox
    Friend WithEvents TxtAddress As System.Windows.Forms.TextBox
    Friend WithEvents TxtPh1 As System.Windows.Forms.TextBox
    Friend WithEvents TxtPh2 As System.Windows.Forms.TextBox
    Friend WithEvents TxtMgrName As System.Windows.Forms.TextBox
    Friend WithEvents TxtMgrCell As System.Windows.Forms.TextBox
    Friend WithEvents TxtMgrPh As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DmnSTATUS As System.Windows.Forms.DomainUpDown
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TxtOpenBal As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents BttnCancel As System.Windows.Forms.Button
    Friend WithEvents daLUP_BUSINESS_GROUP As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents CmbGroup As MTGCComboBox
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_BANK As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLUP_BANK_NEW1 As Neruo_Business_Solution.dsLUP_BANK_NEW
    Friend WithEvents DsLUP_BANK_NEW11 As Neruo_Business_Solution.dsLUP_BANK_NEW1
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents BANKACCDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BANKNAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CONTACT1DataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MgrNAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MgrPHDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MgrCELLDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DsLUP_BUSINESS_GROUP1 As Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_BANK))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.BANKACCDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BANKNAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CONTACT1DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MgrNAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MgrPHDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MgrCELLDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DsLUP_BANK_NEW11 = New Neruo_Business_Solution.dsLUP_BANK_NEW1
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnCancel = New System.Windows.Forms.Button
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GBoxEntry = New System.Windows.Forms.GroupBox
        Me.CmbGroup = New MTGCComboBox
        Me.DsLUP_BANK_NEW1 = New Neruo_Business_Solution.dsLUP_BANK_NEW
        Me.Label14 = New System.Windows.Forms.Label
        Me.DmnSTATUS = New System.Windows.Forms.DomainUpDown
        Me.TxtBankName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtAccountNo = New System.Windows.Forms.TextBox
        Me.TxtBrName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtBrCode = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtAddress = New System.Windows.Forms.TextBox
        Me.TxtPh1 = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtPh2 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtMgrName = New System.Windows.Forms.TextBox
        Me.TxtOpenBal = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.TxtMgrCell = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtMgrPh = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_BUSINESS_GROUP1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_BANK = New System.Data.SqlClient.SqlDataAdapter
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_BANK_NEW11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GBoxEntry.SuspendLayout()
        CType(Me.DsLUP_BANK_NEW1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnPrevForm)
        Me.Panel1.Controls.Add(Me.BttnNextForm)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GBoxEntry)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(680, 480)
        Me.Panel1.TabIndex = 0
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(8, 7)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(89, 45)
        Me.BttnPrevForm.TabIndex = 4
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Business Groups"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(583, 7)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(89, 45)
        Me.BttnNextForm.TabIndex = 5
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Zone(s) Detail"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(678, 48)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "BANK ACCOUNT(s) DETAIL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 267)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(664, 208)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.PeachPuff
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeight = 25
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.BANKACCDataGridViewTextBoxColumn, Me.BANKNAMEDataGridViewTextBoxColumn, Me.CONTACT1DataGridViewTextBoxColumn, Me.MgrNAMEDataGridViewTextBoxColumn, Me.MgrPHDataGridViewTextBoxColumn, Me.MgrCELLDataGridViewTextBoxColumn})
        Me.DataGridView1.DataMember = "V_LUP_BANK"
        Me.DataGridView1.DataSource = Me.DsLUP_BANK_NEW11
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(3, 53)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowTemplate.Height = 18
        Me.DataGridView1.RowTemplate.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.ShowEditingIcon = False
        Me.DataGridView1.Size = New System.Drawing.Size(658, 152)
        Me.DataGridView1.TabIndex = 2
        '
        'BANKACCDataGridViewTextBoxColumn
        '
        Me.BANKACCDataGridViewTextBoxColumn.DataPropertyName = "BANK_ACC"
        Me.BANKACCDataGridViewTextBoxColumn.HeaderText = "Account #"
        Me.BANKACCDataGridViewTextBoxColumn.Name = "BANKACCDataGridViewTextBoxColumn"
        Me.BANKACCDataGridViewTextBoxColumn.ReadOnly = True
        Me.BANKACCDataGridViewTextBoxColumn.Width = 90
        '
        'BANKNAMEDataGridViewTextBoxColumn
        '
        Me.BANKNAMEDataGridViewTextBoxColumn.DataPropertyName = "BANK_NAME"
        Me.BANKNAMEDataGridViewTextBoxColumn.HeaderText = "Bank Name"
        Me.BANKNAMEDataGridViewTextBoxColumn.Name = "BANKNAMEDataGridViewTextBoxColumn"
        Me.BANKNAMEDataGridViewTextBoxColumn.ReadOnly = True
        Me.BANKNAMEDataGridViewTextBoxColumn.Width = 160
        '
        'CONTACT1DataGridViewTextBoxColumn
        '
        Me.CONTACT1DataGridViewTextBoxColumn.DataPropertyName = "CONTACT1"
        Me.CONTACT1DataGridViewTextBoxColumn.HeaderText = "Bank Ph"
        Me.CONTACT1DataGridViewTextBoxColumn.Name = "CONTACT1DataGridViewTextBoxColumn"
        Me.CONTACT1DataGridViewTextBoxColumn.ReadOnly = True
        '
        'MgrNAMEDataGridViewTextBoxColumn
        '
        Me.MgrNAMEDataGridViewTextBoxColumn.DataPropertyName = "Mgr_NAME"
        Me.MgrNAMEDataGridViewTextBoxColumn.HeaderText = "Mrg. Name"
        Me.MgrNAMEDataGridViewTextBoxColumn.Name = "MgrNAMEDataGridViewTextBoxColumn"
        Me.MgrNAMEDataGridViewTextBoxColumn.ReadOnly = True
        Me.MgrNAMEDataGridViewTextBoxColumn.Width = 160
        '
        'MgrPHDataGridViewTextBoxColumn
        '
        Me.MgrPHDataGridViewTextBoxColumn.DataPropertyName = "Mgr_PH"
        Me.MgrPHDataGridViewTextBoxColumn.HeaderText = "Mgr. Ph"
        Me.MgrPHDataGridViewTextBoxColumn.Name = "MgrPHDataGridViewTextBoxColumn"
        Me.MgrPHDataGridViewTextBoxColumn.ReadOnly = True
        '
        'MgrCELLDataGridViewTextBoxColumn
        '
        Me.MgrCELLDataGridViewTextBoxColumn.DataPropertyName = "Mgr_CELL"
        Me.MgrCELLDataGridViewTextBoxColumn.HeaderText = "Mgr. Cell"
        Me.MgrCELLDataGridViewTextBoxColumn.Name = "MgrCELLDataGridViewTextBoxColumn"
        Me.MgrCELLDataGridViewTextBoxColumn.ReadOnly = True
        '
        'DsLUP_BANK_NEW11
        '
        Me.DsLUP_BANK_NEW11.DataSetName = "dsLUP_BANK_NEW1"
        Me.DsLUP_BANK_NEW11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(136, 23)
        Me.TxtSearch.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnCancel)
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(551, 58)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 203)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnCancel
        '
        Me.BttnCancel.Enabled = False
        Me.BttnCancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnCancel.Location = New System.Drawing.Point(14, 62)
        Me.BttnCancel.Name = "BttnCancel"
        Me.BttnCancel.Size = New System.Drawing.Size(92, 31)
        Me.BttnCancel.TabIndex = 2
        Me.BttnCancel.Text = "Ca&ncel"
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(14, 15)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(92, 31)
        Me.BttnNew.TabIndex = 0
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(14, 156)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(92, 31)
        Me.BttnClose.TabIndex = 3
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Enabled = False
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(14, 109)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(92, 31)
        Me.BttnSave.TabIndex = 1
        Me.BttnSave.Text = "&Save"
        '
        'GBoxEntry
        '
        Me.GBoxEntry.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GBoxEntry.Controls.Add(Me.CmbGroup)
        Me.GBoxEntry.Controls.Add(Me.Label14)
        Me.GBoxEntry.Controls.Add(Me.DmnSTATUS)
        Me.GBoxEntry.Controls.Add(Me.TxtBankName)
        Me.GBoxEntry.Controls.Add(Me.Label1)
        Me.GBoxEntry.Controls.Add(Me.Label2)
        Me.GBoxEntry.Controls.Add(Me.TxtAccountNo)
        Me.GBoxEntry.Controls.Add(Me.TxtBrName)
        Me.GBoxEntry.Controls.Add(Me.Label5)
        Me.GBoxEntry.Controls.Add(Me.TxtBrCode)
        Me.GBoxEntry.Controls.Add(Me.Label7)
        Me.GBoxEntry.Controls.Add(Me.Label6)
        Me.GBoxEntry.Controls.Add(Me.TxtAddress)
        Me.GBoxEntry.Controls.Add(Me.TxtPh1)
        Me.GBoxEntry.Controls.Add(Me.Label9)
        Me.GBoxEntry.Controls.Add(Me.TxtPh2)
        Me.GBoxEntry.Controls.Add(Me.Label10)
        Me.GBoxEntry.Controls.Add(Me.Label8)
        Me.GBoxEntry.Controls.Add(Me.TxtMgrName)
        Me.GBoxEntry.Controls.Add(Me.TxtOpenBal)
        Me.GBoxEntry.Controls.Add(Me.Label15)
        Me.GBoxEntry.Controls.Add(Me.TxtMgrCell)
        Me.GBoxEntry.Controls.Add(Me.Label11)
        Me.GBoxEntry.Controls.Add(Me.Label12)
        Me.GBoxEntry.Controls.Add(Me.TxtMgrPh)
        Me.GBoxEntry.Controls.Add(Me.Label13)
        Me.GBoxEntry.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxEntry.Location = New System.Drawing.Point(7, 58)
        Me.GBoxEntry.Name = "GBoxEntry"
        Me.GBoxEntry.Size = New System.Drawing.Size(536, 203)
        Me.GBoxEntry.TabIndex = 1
        Me.GBoxEntry.TabStop = False
        Me.GBoxEntry.Text = "Entry"
        '
        'CmbGroup
        '
        Me.CmbGroup.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup.ColumnNum = 3
        Me.CmbGroup.ColumnWidth = "100;100;30"
        Me.CmbGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.BUSINESS_GROUP", True))
        Me.CmbGroup.DisplayMember = "Text"
        Me.CmbGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbGroup.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbGroup.DropDownForeColor = System.Drawing.Color.White
        Me.CmbGroup.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbGroup.DropDownWidth = 340
        Me.CmbGroup.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbGroup.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbGroup.GridLineHorizontal = False
        Me.CmbGroup.GridLineVertical = True
        Me.CmbGroup.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbGroup.Location = New System.Drawing.Point(104, 145)
        Me.CmbGroup.ManagingFastMouseMoving = True
        Me.CmbGroup.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup.Name = "CmbGroup"
        Me.CmbGroup.Size = New System.Drawing.Size(200, 22)
        Me.CmbGroup.TabIndex = 19
        '
        'DsLUP_BANK_NEW1
        '
        Me.DsLUP_BANK_NEW1.DataSetName = "dsLUP_BANK_NEW"
        Me.DsLUP_BANK_NEW1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 145)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(88, 23)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "B. Group"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DmnSTATUS
        '
        Me.DmnSTATUS.BackColor = System.Drawing.Color.White
        Me.DmnSTATUS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.STATUS", True))
        Me.DmnSTATUS.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DmnSTATUS.Items.Add("No")
        Me.DmnSTATUS.Items.Add("Yes")
        Me.DmnSTATUS.Location = New System.Drawing.Point(232, 169)
        Me.DmnSTATUS.Name = "DmnSTATUS"
        Me.DmnSTATUS.ReadOnly = True
        Me.DmnSTATUS.Size = New System.Drawing.Size(72, 23)
        Me.DmnSTATUS.TabIndex = 23
        '
        'TxtBankName
        '
        Me.TxtBankName.BackColor = System.Drawing.Color.White
        Me.TxtBankName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.BANK_NAME", True))
        Me.TxtBankName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankName.Location = New System.Drawing.Point(304, 24)
        Me.TxtBankName.MaxLength = 50
        Me.TxtBankName.Name = "TxtBankName"
        Me.TxtBankName.Size = New System.Drawing.Size(224, 23)
        Me.TxtBankName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(216, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Bank Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Account #"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtAccountNo
        '
        Me.TxtAccountNo.BackColor = System.Drawing.Color.White
        Me.TxtAccountNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccountNo.Location = New System.Drawing.Point(104, 24)
        Me.TxtAccountNo.MaxLength = 50
        Me.TxtAccountNo.Name = "TxtAccountNo"
        Me.TxtAccountNo.Size = New System.Drawing.Size(112, 23)
        Me.TxtAccountNo.TabIndex = 1
        '
        'TxtBrName
        '
        Me.TxtBrName.BackColor = System.Drawing.Color.White
        Me.TxtBrName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.Br_NAME", True))
        Me.TxtBrName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBrName.Location = New System.Drawing.Point(304, 48)
        Me.TxtBrName.Name = "TxtBrName"
        Me.TxtBrName.Size = New System.Drawing.Size(224, 23)
        Me.TxtBrName.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Br. Code"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtBrCode
        '
        Me.TxtBrCode.BackColor = System.Drawing.Color.White
        Me.TxtBrCode.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.Br_CODE", True))
        Me.TxtBrCode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBrCode.Location = New System.Drawing.Point(104, 48)
        Me.TxtBrCode.Name = "TxtBrCode"
        Me.TxtBrCode.Size = New System.Drawing.Size(112, 23)
        Me.TxtBrCode.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(216, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 23)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Br. Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 23)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Address"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtAddress
        '
        Me.TxtAddress.BackColor = System.Drawing.Color.White
        Me.TxtAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.ADDRESS", True))
        Me.TxtAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(104, 72)
        Me.TxtAddress.MaxLength = 200
        Me.TxtAddress.Multiline = True
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(200, 48)
        Me.TxtAddress.TabIndex = 9
        '
        'TxtPh1
        '
        Me.TxtPh1.BackColor = System.Drawing.Color.White
        Me.TxtPh1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.CONTACT1", True))
        Me.TxtPh1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPh1.Location = New System.Drawing.Point(376, 72)
        Me.TxtPh1.Name = "TxtPh1"
        Me.TxtPh1.Size = New System.Drawing.Size(152, 23)
        Me.TxtPh1.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(304, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 23)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Phone#1"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPh2
        '
        Me.TxtPh2.BackColor = System.Drawing.Color.White
        Me.TxtPh2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.CONTACT2", True))
        Me.TxtPh2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPh2.Location = New System.Drawing.Point(376, 96)
        Me.TxtPh2.Name = "TxtPh2"
        Me.TxtPh2.Size = New System.Drawing.Size(152, 23)
        Me.TxtPh2.TabIndex = 13
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(304, 96)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 23)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "Phone#2"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 121)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 23)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Mgr. Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtMgrName
        '
        Me.TxtMgrName.BackColor = System.Drawing.Color.White
        Me.TxtMgrName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.Mgr_NAME", True))
        Me.TxtMgrName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMgrName.Location = New System.Drawing.Point(104, 121)
        Me.TxtMgrName.Name = "TxtMgrName"
        Me.TxtMgrName.Size = New System.Drawing.Size(200, 23)
        Me.TxtMgrName.TabIndex = 15
        '
        'TxtOpenBal
        '
        Me.TxtOpenBal.BackColor = System.Drawing.Color.White
        Me.TxtOpenBal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.OPEN_BAL", True))
        Me.TxtOpenBal.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOpenBal.Location = New System.Drawing.Point(427, 169)
        Me.TxtOpenBal.Name = "TxtOpenBal"
        Me.TxtOpenBal.Size = New System.Drawing.Size(101, 23)
        Me.TxtOpenBal.TabIndex = 25
        Me.TxtOpenBal.Text = "0.00"
        Me.TxtOpenBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(306, 169)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(115, 23)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Opening Bal."
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtMgrCell
        '
        Me.TxtMgrCell.BackColor = System.Drawing.Color.White
        Me.TxtMgrCell.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.Mgr_CELL", True))
        Me.TxtMgrCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMgrCell.Location = New System.Drawing.Point(376, 145)
        Me.TxtMgrCell.Name = "TxtMgrCell"
        Me.TxtMgrCell.Size = New System.Drawing.Size(152, 23)
        Me.TxtMgrCell.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(304, 145)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 23)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Mgr. Cell"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(304, 121)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 23)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Mgr. Ph #"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtMgrPh
        '
        Me.TxtMgrPh.BackColor = System.Drawing.Color.White
        Me.TxtMgrPh.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BANK_NEW1, "V_LUP_BANK.Mgr_PH", True))
        Me.TxtMgrPh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMgrPh.Location = New System.Drawing.Point(376, 121)
        Me.TxtMgrPh.Name = "TxtMgrPh"
        Me.TxtMgrPh.Size = New System.Drawing.Size(152, 23)
        Me.TxtMgrPh.TabIndex = 17
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 169)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(173, 23)
        Me.Label13.TabIndex = 22
        Me.Label13.Text = "Account in Use (Y/N)"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daLUP_BUSINESS_GROUP
        '
        Me.daLUP_BUSINESS_GROUP.SelectCommand = Me.SqlCommand3
        Me.daLUP_BUSINESS_GROUP.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_BUSINESS_GROUP", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nID", "nID"), New System.Data.Common.DataColumnMapping("sGROUP_NAME", "sGROUP_NAME"), New System.Data.Common.DataColumnMapping("sGROUP_DEALER", "sGROUP_DEALER"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("sBUSINESS_NAME", "sBUSINESS_NAME")})})
        '
        'SqlCommand3
        '
        Me.SqlCommand3.CommandText = "SELECT     nID, sGROUP_NAME, sGROUP_DEALER, CASE sSTATUS WHEN '0' THEN 'No' WHEN " & _
            "'1' THEN 'Yes' END AS STATUS, sBUSINESS_NAME" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_BUSINESS_GROUP"
        Me.SqlCommand3.Connection = Me.SqlConnection1
        '
        'DsLUP_BUSINESS_GROUP1
        '
        Me.DsLUP_BUSINESS_GROUP1.DataSetName = "dsLUP_BUSINESS_GROUP"
        Me.DsLUP_BUSINESS_GROUP1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = resources.GetString("SqlSelectCommand1.CommandText")
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'daLUP_BANK
        '
        Me.daLUP_BANK.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_BANK.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_BANK", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("BANK_NAME", "BANK_NAME"), New System.Data.Common.DataColumnMapping("Br_NAME", "Br_NAME"), New System.Data.Common.DataColumnMapping("Br_CODE", "Br_CODE"), New System.Data.Common.DataColumnMapping("ADDRESS", "ADDRESS"), New System.Data.Common.DataColumnMapping("CONTACT1", "CONTACT1"), New System.Data.Common.DataColumnMapping("CONTACT2", "CONTACT2"), New System.Data.Common.DataColumnMapping("Mgr_NAME", "Mgr_NAME"), New System.Data.Common.DataColumnMapping("Mgr_PH", "Mgr_PH"), New System.Data.Common.DataColumnMapping("Mgr_CELL", "Mgr_CELL"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("OPEN_BAL", "OPEN_BAL"), New System.Data.Common.DataColumnMapping("BUSINESS_GROUP", "BUSINESS_GROUP")})})
        '
        'frmLUP_BANK
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(698, 496)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_BANK"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BANK ACCOUNTS"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_BANK_NEW11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GBoxEntry.ResumeLayout(False)
        Me.GBoxEntry.PerformLayout()
        CType(Me.DsLUP_BANK_NEW1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_BANK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.DsLUP_BANK_NEW1.Clear()
        Me.FillComboBox_Group()
        Me.FillListView()
        Me.DmnSTATUS.SelectedIndex = 1
        Me.GBoxEntry.Enabled = False
    End Sub

    Private Sub frmLUP_BANK_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub TxtCode_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtBankName.GotFocus, TxtAccountNo.GotFocus, TxtAddress.GotFocus, TxtBrCode.GotFocus, TxtBrName.GotFocus, TxtMgrCell.GotFocus, TxtMgrName.GotFocus, TxtMgrPh.GotFocus, TxtPh1.GotFocus, TxtPh2.GotFocus, TxtOpenBal.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub TxtCode_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtBankName.LostFocus, TxtAccountNo.LostFocus, TxtAddress.LostFocus, TxtBrCode.LostFocus, TxtBrName.LostFocus, TxtMgrCell.LostFocus, TxtMgrName.LostFocus, TxtMgrPh.LostFocus, TxtPh1.LostFocus, TxtPh2.LostFocus, TxtOpenBal.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    'KeyPress Account
    Private Sub TxtAccountNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtAccountNo.KeyPress
        Me.asNum.NumPressDash(e)
    End Sub
    Private Sub TxtBrCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtBrCode.KeyPress, TxtMgrCell.KeyPress, TxtMgrPh.KeyPress, TxtPh1.KeyPress, TxtPh2.KeyPress, TxtOpenBal.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        If Me.TxtSearch.Text = Nothing Then
            Me.FillListView()

        Else
            Me.FillListView_Condition()
        End If
    End Sub
#End Region

#Region "DOMAIN_UPDOWN EVENTS"
    Private Sub DmnStatus_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnSTATUS.GotFocus
        CType(sender, DomainUpDown).BackColor = Color.LightSteelBlue
    End Sub
    Private Sub DmnStatus_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnSTATUS.LostFocus
        CType(sender, DomainUpDown).BackColor = Color.White
    End Sub
#End Region

#Region "DataGrid Controls"
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        On Error GoTo Fix
        Me.TxtAccountNo.Text = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
        If Not Me.TxtAccountNo.Text = Nothing Then
            Me.GBoxEntry.Enabled = True
            Dim Str2 As String = "SELECT BANK_ACC, BANK_NAME, Br_NAME, Br_CODE, ADDRESS, CONTACT1, CONTACT2, Mgr_NAME, Mgr_PH, Mgr_CELL, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CONVERT(NUMERIC(18,2),OPEN_BAL) AS OPEN_BAL, BUSINESS_GROUP FROM V_LUP_BANK WHERE BANK_ACC = '" & Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value & "' ORDER BY BANK_NAME"

            Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
            Me.daLUP_BANK = New SDS.SqlDataAdapter(SqlCmd2)

            Me.DsLUP_BANK_NEW1.Clear()
            Me.daLUP_BANK.Fill(Me.DsLUP_BANK_NEW1.V_LUP_BANK)

            Me.BttnNew.Enabled = False
            Me.BttnSave.Enabled = True
            Me.BttnCancel.Enabled = True
            Me.CancelButton = Me.BttnCancel

            Me.TxtAccountNo.Focus()

            Dim StrCmb As String = Me.CmbGroup.Text
            Me.CmbGroup.SelectedIndex = -1
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCmb)
            Me.DmnSTATUS.SelectedItem = Me.DmnSTATUS.Text
        End If
Fix:
    End Sub
    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        On Error GoTo Fix
        Me.TxtAccountNo.Text = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
        If Not Me.TxtAccountNo.Text = Nothing Then
            Me.GBoxEntry.Enabled = True
            Dim Str2 As String = "SELECT BANK_ACC, BANK_NAME, Br_NAME, Br_CODE, ADDRESS, CONTACT1, CONTACT2, Mgr_NAME, Mgr_PH, Mgr_CELL, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CONVERT(NUMERIC(18,2),OPEN_BAL) AS OPEN_BAL, BUSINESS_GROUP FROM V_LUP_BANK WHERE BANK_ACC = '" & Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value & "' ORDER BY BANK_NAME"

            Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
            Me.daLUP_BANK = New SDS.SqlDataAdapter(SqlCmd2)

            Me.DsLUP_BANK_NEW1.Clear()
            Me.daLUP_BANK.Fill(Me.DsLUP_BANK_NEW1.V_LUP_BANK)

            Me.BttnNew.Enabled = False
            Me.BttnSave.Enabled = True
            Me.BttnCancel.Enabled = True
            Me.CancelButton = Me.BttnCancel

            Me.TxtAccountNo.Focus()

            Dim StrCmb As String = Me.CmbGroup.Text
            Me.CmbGroup.SelectedIndex = -1
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCmb)
            Me.DmnSTATUS.SelectedItem = Me.DmnSTATUS.Text
        End If
Fix:
    End Sub
    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        If Not Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM LUP_BANK WHERE sACCOUNT_NO=" & Val(Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value) & "")
                'Me.FillListView()
                Me.BttnNew_Click(sender, New System.EventArgs)

            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            e.Cancel = True
            Me.TxtAccountNo.Focus()
        End If
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.GBoxEntry.Enabled = True
        Me.ClearAll()

        Me.BttnNew.Enabled = False
        Me.BttnSave.Enabled = True
        Me.BttnCancel.Enabled = True
        Me.CancelButton = Me.BttnCancel

        Me.TxtAccountNo.Focus()
    End Sub
    Private Sub BttnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnCancel.Click
        If MsgBox("Are you sure to Cancel?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Cancel?") = MsgBoxResult.Yes Then
            Me.GBoxEntry.Enabled = False
            Me.ClearAll()

            Me.BttnNew.Enabled = True
            Me.BttnSave.Enabled = False
            Me.BttnCancel.Enabled = False
            Me.CancelButton = Me.BttnClose
        End If
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM LUP_BANK WHERE sACCOUNT_NO='" & Me.TxtAccountNo.Text & "'")

        If Me.TxtAccountNo.Text = Nothing Or Me.TxtBankName.Text = Nothing Or Val(Me.TxtBrCode.Text) <= 0 Or Me.TxtBrName.Text = Nothing Or Me.TxtAddress.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.DmnSTATUS.SelectedIndex = -1 Or Val(Me.TxtOpenBal.Text) < 0 Then
            MsgBox("Please enter description OR correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtAccountNo.Text = Nothing Then
                Me.TxtAccountNo.Focus()

            ElseIf Me.TxtBankName.Text = Nothing Then
                Me.TxtBankName.Focus()

            ElseIf Val(Me.TxtBrCode.Text) <= 0 Then
                Me.TxtBrCode.Focus()

            ElseIf Me.TxtBrName.Text = Nothing Then
                Me.TxtBrName.Focus()

            ElseIf Me.TxtAddress.Text = Nothing Then
                Me.TxtAddress.Focus()

            ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                Me.CmbGroup.Focus()

            ElseIf Me.DmnSTATUS.SelectedIndex = -1 Then
                Me.DmnSTATUS.Focus()

            ElseIf Val(Me.TxtOpenBal.Text) < 0 Then
                Me.TxtOpenBal.Focus()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            If MsgBox("Do you want to save '" & Me.TxtAccountNo.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO LUP_BANK(sACCOUNT_NO, sBANK_NAME, sBRANCH_code, sBRANCH_NAME, sADDRESS, sCONTACT1, sCONTACT2, sMANAGER_NAME, sMANAGER_PH, sMANAGER_CELL,sSTATUS,nBUSINESS_CODE,nOPEN_BAL) VALUES('" & Me.TxtAccountNo.Text & "','" & Me.TxtBankName.Text & "','" & Me.TxtBrCode.Text & "','" & Me.TxtBrName.Text & "','" & Me.TxtAddress.Text & "','" & Me.TxtPh1.Text & "','" & Me.TxtPh2.Text & "','" & Me.TxtMgrName.Text & "','" & Me.TxtMgrPh.Text & "','" & Me.TxtMgrCell.Text & "','" & Me.DmnSTATUS.SelectedIndex & "'," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.TxtOpenBal.Text) & ") ")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtAccountNo.Focus()
                Me.GBoxEntry.Enabled = False
                Me.BttnNew.Enabled = True
                Me.BttnSave.Enabled = False
                Me.BttnCancel.Enabled = False
                Me.CancelButton = Me.BttnClose
            End If

        ElseIf Me.asSELECT.pFlg1 = True Then
            If MsgBox("This Account no '" & Me.TxtAccountNo.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE LUP_BANK SET sBANK_NAME='" & Me.TxtBankName.Text & "',sBRANCH_code='" & Me.TxtBrCode.Text & "',sBRANCH_NAME='" & Me.TxtBrName.Text & "',sADDRESS='" & Me.TxtAddress.Text & "',sCONTACT1='" & Me.TxtPh1.Text & "',sCONTACT2='" & Me.TxtPh2.Text & "',sMANAGER_NAME='" & Me.TxtMgrName.Text & "',sMANAGER_PH='" & Me.TxtMgrPh.Text & "',sMANAGER_CELL='" & Me.TxtMgrCell.Text & "',sSTATUS='" & Me.DmnSTATUS.SelectedIndex & "', nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ",nOPEN_BAL=" & Val(Me.TxtOpenBal.Text) & " WHERE sACCOUNT_NO='" & Me.TxtAccountNo.Text & "'")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtAccountNo.Focus()
                Me.GBoxEntry.Enabled = False
                Me.BttnNew.Enabled = True
                Me.BttnSave.Enabled = False
                Me.BttnCancel.Enabled = False
                Me.CancelButton = Me.BttnClose
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_ZONE.MdiParent = Me.ParentForm
        frmLUP_ZONE.Show()
        frmLUP_ZONE.Activate()
        Me.Close()
    End Sub
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        frmLUP_BUSINESS_GROUP.MdiParent = Me.ParentForm
        frmLUP_BUSINESS_GROUP.Show()
        frmLUP_BUSINESS_GROUP.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillComboBox_Group()
        Dim Str1 As String = "SELECT nID, sGROUP_NAME, sGROUP_DEALER, sSTATUS sBUSINESS_NAME FROM V_BUSINESS_GROUP WHERE sSTATUS='1'"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_BUSINESS_GROUP = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_BUSINESS_GROUP1.Clear()
        Me.daLUP_BUSINESS_GROUP.Fill(Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP)

        Dim dtLoading As New DataTable("V_BUSINESS_GROUP")

        dtLoading.Columns.Add("nID", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sGROUP_NAME", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sGROUP_DEALER", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nID") = Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(0).ToString
            dr("sGROUP_NAME") = Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(1).ToString
            dr("sGROUP_DEALER") = Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(2).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbGroup.SelectedIndex = -1
        Me.CmbGroup.Items.Clear()
        Me.CmbGroup.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbGroup.SourceDataString = New String(2) {"sGROUP_NAME", "sGROUP_DEALER", "nID"}
        Me.CmbGroup.SourceDataTable = dtLoading
    End Sub
    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT BANK_ACC, BANK_NAME, Br_NAME, Br_CODE, ADDRESS, CONTACT1, CONTACT2, Mgr_NAME, Mgr_PH, Mgr_CELL, STATUS, OPEN_BAL, BUSINESS_GROUP FROM V_LUP_BANK ORDER BY BANK_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_BANK = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_BANK_NEW11.Clear()
            Me.daLUP_BANK.Fill(Me.DsLUP_BANK_NEW11.V_LUP_BANK)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim Str1 As String = "SELECT BANK_ACC, BANK_NAME, Br_NAME, Br_CODE, ADDRESS, CONTACT1, CONTACT2, Mgr_NAME, Mgr_PH, Mgr_CELL, STATUS, OPEN_BAL, BUSINESS_GROUP FROM V_LUP_BANK WHERE BANK_NAME LIKE '%" & Me.TxtSearch.Text & "%' ORDER BY BANK_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_BANK = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_BANK_NEW11.Clear()
            Me.daLUP_BANK.Fill(Me.DsLUP_BANK_NEW11.V_LUP_BANK)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ClearAll()
        Me.DsLUP_BANK_NEW1.Clear()
        Me.TxtAccountNo.Text = Nothing
        Me.TxtBankName.Text = Nothing
        Me.TxtBrCode.Text = Nothing
        Me.TxtBrName.Text = Nothing
        Me.TxtAddress.Text = Nothing
        Me.TxtPh1.Text = Nothing
        Me.TxtPh2.Text = Nothing
        Me.TxtMgrName.Text = Nothing
        Me.TxtMgrPh.Text = Nothing
        Me.TxtMgrCell.Text = Nothing

        Me.TxtSearch.Text = Nothing
    End Sub
#End Region

End Class
