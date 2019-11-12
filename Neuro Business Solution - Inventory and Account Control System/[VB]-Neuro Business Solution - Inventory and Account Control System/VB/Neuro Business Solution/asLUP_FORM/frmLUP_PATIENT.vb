Imports SDS = System.Data.SqlClient
Public Class frmLUP_PATIENT
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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents CmbPanel As MTGCComboBox
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents TxtCode As System.Windows.Forms.TextBox
    Friend WithEvents TxtFatherName As System.Windows.Forms.TextBox
    Friend WithEvents TxtPerAddress As System.Windows.Forms.TextBox
    Friend WithEvents TxtHomePh As System.Windows.Forms.TextBox
    Friend WithEvents TxtEmail As System.Windows.Forms.TextBox
    Friend WithEvents TxtCell As System.Windows.Forms.TextBox
    Friend WithEvents TxtDiseaseDesc As System.Windows.Forms.TextBox
    Friend WithEvents daV_PATIENT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents BttnAdd As System.Windows.Forms.Button
    Friend WithEvents TxtNIC As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtEnrolmentDate As System.Windows.Forms.TextBox
    Friend WithEvents TxtDischargeDate As System.Windows.Forms.TextBox
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents CmbDisease As MTGCComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DsV_PATIENT1 As Neruo_Business_Solution.dsV_PATIENT
    Friend WithEvents DsV_PATIENT11 As Neruo_Business_Solution.dsV_PATIENT1
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CELLDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DISEASEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DISEASEDESCDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PANELDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ENROLDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents daLUP_DISEASE As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_DISEASE1 As Neruo_Business_Solution.dsLUP_DISEASE
    Friend WithEvents daLUP_PANEL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand8 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_PANEL1 As Neruo_Business_Solution.dsLUP_PANEL
    Friend WithEvents ChkRegular As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CmbDisease = New MTGCComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.ChkRegular = New System.Windows.Forms.CheckBox
        Me.TxtNIC = New System.Windows.Forms.MaskedTextBox
        Me.BttnAdd = New System.Windows.Forms.Button
        Me.TxtDiseaseDesc = New System.Windows.Forms.TextBox
        Me.CmbPanel = New MTGCComboBox
        Me.TxtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtCode = New System.Windows.Forms.TextBox
        Me.TxtFatherName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
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
        Me.TxtDischargeDate = New System.Windows.Forms.TextBox
        Me.TxtEnrolmentDate = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daV_PATIENT = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.DsV_PATIENT1 = New Neruo_Business_Solution.dsV_PATIENT
        Me.DsV_PATIENT11 = New Neruo_Business_Solution.dsV_PATIENT1
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.NAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CELLDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DISEASEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DISEASEDESCDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PANELDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ENROLDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.daLUP_DISEASE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_DISEASE1 = New Neruo_Business_Solution.dsLUP_DISEASE
        Me.daLUP_PANEL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand6 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand8 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_PANEL1 = New Neruo_Business_Solution.dsLUP_PANEL
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsV_PATIENT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_PATIENT11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_DISEASE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_PANEL1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.BttnNextForm.TabIndex = 5
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Vans Detail"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 311)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(671, 205)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.AutoGenerateColumns = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.NAMEDataGridViewTextBoxColumn, Me.CELLDataGridViewTextBoxColumn, Me.DISEASEDataGridViewTextBoxColumn, Me.DISEASEDESCDataGridViewTextBoxColumn, Me.PANELDataGridViewTextBoxColumn, Me.ENROLDATEDataGridViewTextBoxColumn})
        Me.DataGridView1.DataMember = "V_PATIENT_INFO"
        Me.DataGridView1.DataSource = Me.DsV_PATIENT11
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(3, 46)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.Size = New System.Drawing.Size(665, 156)
        Me.DataGridView1.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(199, 17)
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
        Me.TxtSearch.Location = New System.Drawing.Point(263, 17)
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
        Me.BttnPrevForm.TabIndex = 4
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
        Me.GroupBox1.Size = New System.Drawing.Size(120, 255)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BttnNew.Location = New System.Drawing.Point(16, 41)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 0
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BttnClose.Location = New System.Drawing.Point(16, 183)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.BttnSave.Location = New System.Drawing.Point(16, 112)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 1
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.CmbDisease)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.ChkRegular)
        Me.GroupBox3.Controls.Add(Me.TxtNIC)
        Me.GroupBox3.Controls.Add(Me.BttnAdd)
        Me.GroupBox3.Controls.Add(Me.TxtDiseaseDesc)
        Me.GroupBox3.Controls.Add(Me.CmbPanel)
        Me.GroupBox3.Controls.Add(Me.TxtName)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtCode)
        Me.GroupBox3.Controls.Add(Me.TxtFatherName)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label24)
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
        Me.GroupBox3.Controls.Add(Me.TxtDischargeDate)
        Me.GroupBox3.Controls.Add(Me.TxtEnrolmentDate)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(11, 48)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(545, 257)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'CmbDisease
        '
        Me.CmbDisease.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbDisease.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbDisease.ColumnNum = 2
        Me.CmbDisease.ColumnWidth = "140;40"
        Me.CmbDisease.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.DISEASE", True))
        Me.CmbDisease.DisplayMember = "Text"
        Me.CmbDisease.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbDisease.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbDisease.DropDownForeColor = System.Drawing.Color.White
        Me.CmbDisease.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbDisease.DropDownWidth = 220
        Me.CmbDisease.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbDisease.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbDisease.GridLineHorizontal = False
        Me.CmbDisease.GridLineVertical = True
        Me.CmbDisease.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbDisease.Location = New System.Drawing.Point(398, 42)
        Me.CmbDisease.ManagingFastMouseMoving = True
        Me.CmbDisease.ManagingFastMouseMovingInterval = 30
        Me.CmbDisease.Name = "CmbDisease"
        Me.CmbDisease.Size = New System.Drawing.Size(141, 24)
        Me.CmbDisease.TabIndex = 20
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label13.Location = New System.Drawing.Point(288, 42)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(107, 23)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "Disease"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ChkRegular
        '
        Me.ChkRegular.Checked = True
        Me.ChkRegular.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkRegular.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Me.DsV_PATIENT1, "V_PATIENT_INFO.STATUS", True))
        Me.ChkRegular.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkRegular.Location = New System.Drawing.Point(288, 146)
        Me.ChkRegular.Name = "ChkRegular"
        Me.ChkRegular.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ChkRegular.Size = New System.Drawing.Size(125, 23)
        Me.ChkRegular.TabIndex = 25
        Me.ChkRegular.Text = "Being Treated"
        Me.ChkRegular.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ChkRegular.UseVisualStyleBackColor = True
        '
        'TxtNIC
        '
        Me.TxtNIC.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.NIC", True))
        Me.TxtNIC.Location = New System.Drawing.Point(96, 94)
        Me.TxtNIC.Mask = "00000-0000000-0"
        Me.TxtNIC.Name = "TxtNIC"
        Me.TxtNIC.Size = New System.Drawing.Size(181, 23)
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
        'TxtDiseaseDesc
        '
        Me.TxtDiseaseDesc.BackColor = System.Drawing.Color.White
        Me.TxtDiseaseDesc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.DISEASE_DESC", True))
        Me.TxtDiseaseDesc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDiseaseDesc.Location = New System.Drawing.Point(398, 69)
        Me.TxtDiseaseDesc.MaxLength = 200
        Me.TxtDiseaseDesc.Multiline = True
        Me.TxtDiseaseDesc.Name = "TxtDiseaseDesc"
        Me.TxtDiseaseDesc.Size = New System.Drawing.Size(141, 48)
        Me.TxtDiseaseDesc.TabIndex = 22
        '
        'CmbPanel
        '
        Me.CmbPanel.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbPanel.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbPanel.ColumnNum = 2
        Me.CmbPanel.ColumnWidth = "140;40"
        Me.CmbPanel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.PANEL", True))
        Me.CmbPanel.DisplayMember = "Text"
        Me.CmbPanel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbPanel.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbPanel.DropDownForeColor = System.Drawing.Color.White
        Me.CmbPanel.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbPanel.DropDownWidth = 220
        Me.CmbPanel.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbPanel.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbPanel.GridLineHorizontal = False
        Me.CmbPanel.GridLineVertical = True
        Me.CmbPanel.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbPanel.Location = New System.Drawing.Point(398, 17)
        Me.CmbPanel.ManagingFastMouseMoving = True
        Me.CmbPanel.ManagingFastMouseMovingInterval = 30
        Me.CmbPanel.Name = "CmbPanel"
        Me.CmbPanel.Size = New System.Drawing.Size(141, 24)
        Me.CmbPanel.TabIndex = 18
        '
        'TxtName
        '
        Me.TxtName.BackColor = System.Drawing.Color.White
        Me.TxtName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.NAME", True))
        Me.TxtName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.Location = New System.Drawing.Point(96, 44)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(181, 23)
        Me.TxtName.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Name*"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "ID (Auto)*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCode
        '
        Me.TxtCode.BackColor = System.Drawing.Color.White
        Me.TxtCode.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.ID", True))
        Me.TxtCode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.Location = New System.Drawing.Point(96, 18)
        Me.TxtCode.MaxLength = 50
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.ReadOnly = True
        Me.TxtCode.Size = New System.Drawing.Size(105, 23)
        Me.TxtCode.TabIndex = 1
        Me.TxtCode.TabStop = False
        '
        'TxtFatherName
        '
        Me.TxtFatherName.BackColor = System.Drawing.Color.White
        Me.TxtFatherName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.FNAME", True))
        Me.TxtFatherName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFatherName.Location = New System.Drawing.Point(96, 69)
        Me.TxtFatherName.Name = "TxtFatherName"
        Me.TxtFatherName.Size = New System.Drawing.Size(181, 23)
        Me.TxtFatherName.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 23)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "N.I.C #"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 69)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 23)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Father Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(288, 69)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(107, 48)
        Me.Label24.TabIndex = 21
        Me.Label24.Text = "Disease Desc."
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 48)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Permanent Address"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPerAddress
        '
        Me.TxtPerAddress.BackColor = System.Drawing.Color.White
        Me.TxtPerAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.PER_ADD", True))
        Me.TxtPerAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPerAddress.Location = New System.Drawing.Point(96, 120)
        Me.TxtPerAddress.MaxLength = 200
        Me.TxtPerAddress.Multiline = True
        Me.TxtPerAddress.Name = "TxtPerAddress"
        Me.TxtPerAddress.Size = New System.Drawing.Size(181, 48)
        Me.TxtPerAddress.TabIndex = 10
        '
        'TxtHomePh
        '
        Me.TxtHomePh.BackColor = System.Drawing.Color.White
        Me.TxtHomePh.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.PHONE", True))
        Me.TxtHomePh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHomePh.Location = New System.Drawing.Point(96, 171)
        Me.TxtHomePh.Name = "TxtHomePh"
        Me.TxtHomePh.Size = New System.Drawing.Size(181, 23)
        Me.TxtHomePh.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 171)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 23)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Home Ph."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(288, 171)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(107, 23)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Discharge Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(288, 120)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(107, 23)
        Me.Label19.TabIndex = 23
        Me.Label19.Text = "Enroll. Date"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 222)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 23)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "E-mail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtEmail
        '
        Me.TxtEmail.BackColor = System.Drawing.Color.White
        Me.TxtEmail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.EMAIL", True))
        Me.TxtEmail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmail.Location = New System.Drawing.Point(96, 222)
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.Size = New System.Drawing.Size(181, 23)
        Me.TxtEmail.TabIndex = 16
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 197)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(88, 23)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Cell #"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCell
        '
        Me.TxtCell.BackColor = System.Drawing.Color.White
        Me.TxtCell.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.CELL", True))
        Me.TxtCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCell.Location = New System.Drawing.Point(96, 197)
        Me.TxtCell.Name = "TxtCell"
        Me.TxtCell.Size = New System.Drawing.Size(181, 23)
        Me.TxtCell.TabIndex = 14
        '
        'TxtDischargeDate
        '
        Me.TxtDischargeDate.BackColor = System.Drawing.Color.White
        Me.TxtDischargeDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.DISCH_DATE", True))
        Me.TxtDischargeDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDischargeDate.Location = New System.Drawing.Point(398, 171)
        Me.TxtDischargeDate.Name = "TxtDischargeDate"
        Me.TxtDischargeDate.Size = New System.Drawing.Size(98, 23)
        Me.TxtDischargeDate.TabIndex = 27
        '
        'TxtEnrolmentDate
        '
        Me.TxtEnrolmentDate.BackColor = System.Drawing.Color.White
        Me.TxtEnrolmentDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_PATIENT1, "V_PATIENT_INFO.ENROL_DATE", True))
        Me.TxtEnrolmentDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEnrolmentDate.Location = New System.Drawing.Point(398, 120)
        Me.TxtEnrolmentDate.Name = "TxtEnrolmentDate"
        Me.TxtEnrolmentDate.Size = New System.Drawing.Size(98, 23)
        Me.TxtEnrolmentDate.TabIndex = 24
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.Label16.Location = New System.Drawing.Point(288, 17)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(107, 23)
        Me.Label16.TabIndex = 17
        Me.Label16.Text = "Panel"
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
        Me.Label3.Text = "PATIENT(s) DETAIL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daV_PATIENT
        '
        Me.daV_PATIENT.DeleteCommand = Me.SqlCommand1
        Me.daV_PATIENT.SelectCommand = Me.SqlCommand3
        Me.daV_PATIENT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_PATIENT_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("FNAME", "FNAME"), New System.Data.Common.DataColumnMapping("NIC", "NIC"), New System.Data.Common.DataColumnMapping("PER_ADD", "PER_ADD"), New System.Data.Common.DataColumnMapping("PHONE", "PHONE"), New System.Data.Common.DataColumnMapping("CELL", "CELL"), New System.Data.Common.DataColumnMapping("EMAIL", "EMAIL"), New System.Data.Common.DataColumnMapping("DISEASE", "DISEASE"), New System.Data.Common.DataColumnMapping("DISEASE_DESC", "DISEASE_DESC"), New System.Data.Common.DataColumnMapping("PANEL", "PANEL"), New System.Data.Common.DataColumnMapping("ENROL_DATE", "ENROL_DATE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("DISCH_DATE", "DISCH_DATE")})})
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
        Me.SqlCommand3.CommandText = "SELECT     V_PATIENT_INFO.*" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_PATIENT_INFO"
        Me.SqlCommand3.Connection = Me.SqlConnection1
        '
        'DsV_PATIENT1
        '
        Me.DsV_PATIENT1.DataSetName = "dsV_PATIENT"
        Me.DsV_PATIENT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsV_PATIENT11
        '
        Me.DsV_PATIENT11.DataSetName = "dsV_PATIENT1"
        Me.DsV_PATIENT11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.Width = 60
        '
        'NAMEDataGridViewTextBoxColumn
        '
        Me.NAMEDataGridViewTextBoxColumn.DataPropertyName = "NAME"
        Me.NAMEDataGridViewTextBoxColumn.HeaderText = "NAME"
        Me.NAMEDataGridViewTextBoxColumn.Name = "NAMEDataGridViewTextBoxColumn"
        Me.NAMEDataGridViewTextBoxColumn.Width = 160
        '
        'CELLDataGridViewTextBoxColumn
        '
        Me.CELLDataGridViewTextBoxColumn.DataPropertyName = "CELL"
        Me.CELLDataGridViewTextBoxColumn.HeaderText = "CELL"
        Me.CELLDataGridViewTextBoxColumn.Name = "CELLDataGridViewTextBoxColumn"
        Me.CELLDataGridViewTextBoxColumn.Width = 80
        '
        'DISEASEDataGridViewTextBoxColumn
        '
        Me.DISEASEDataGridViewTextBoxColumn.DataPropertyName = "DISEASE"
        Me.DISEASEDataGridViewTextBoxColumn.HeaderText = "DISEASE"
        Me.DISEASEDataGridViewTextBoxColumn.Name = "DISEASEDataGridViewTextBoxColumn"
        Me.DISEASEDataGridViewTextBoxColumn.Width = 120
        '
        'DISEASEDESCDataGridViewTextBoxColumn
        '
        Me.DISEASEDESCDataGridViewTextBoxColumn.DataPropertyName = "DISEASE_DESC"
        Me.DISEASEDESCDataGridViewTextBoxColumn.HeaderText = "DISEASE_DESC"
        Me.DISEASEDESCDataGridViewTextBoxColumn.Name = "DISEASEDESCDataGridViewTextBoxColumn"
        Me.DISEASEDESCDataGridViewTextBoxColumn.Width = 120
        '
        'PANELDataGridViewTextBoxColumn
        '
        Me.PANELDataGridViewTextBoxColumn.DataPropertyName = "PANEL"
        Me.PANELDataGridViewTextBoxColumn.HeaderText = "PANEL"
        Me.PANELDataGridViewTextBoxColumn.Name = "PANELDataGridViewTextBoxColumn"
        Me.PANELDataGridViewTextBoxColumn.Width = 80
        '
        'ENROLDATEDataGridViewTextBoxColumn
        '
        Me.ENROLDATEDataGridViewTextBoxColumn.DataPropertyName = "ENROL_DATE"
        Me.ENROLDATEDataGridViewTextBoxColumn.HeaderText = "ENROL_DATE"
        Me.ENROLDATEDataGridViewTextBoxColumn.Name = "ENROLDATEDataGridViewTextBoxColumn"
        Me.ENROLDATEDataGridViewTextBoxColumn.Width = 80
        '
        'daLUP_DISEASE
        '
        Me.daLUP_DISEASE.DeleteCommand = Me.SqlCommand2
        Me.daLUP_DISEASE.InsertCommand = Me.SqlInsertCommand
        Me.daLUP_DISEASE.SelectCommand = Me.SqlCommand4
        Me.daLUP_DISEASE.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_DISEASE", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_DISEASE.UpdateCommand = Me.SqlUpdateCommand
        '
        'SqlCommand2
        '
        Me.SqlCommand2.CommandText = "DELETE FROM [LUP_DISEASE] WHERE (([nCODE] = @Original_nCODE) AND ([sDESC] = @Orig" & _
            "inal_sDESC))"
        Me.SqlCommand2.Connection = Me.SqlConnection1
        Me.SqlCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand4
        '
        Me.SqlCommand4.CommandText = "SELECT     LUP_DISEASE.*" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         LUP_DISEASE"
        Me.SqlCommand4.Connection = Me.SqlConnection1
        '
        'SqlInsertCommand
        '
        Me.SqlInsertCommand.CommandText = "INSERT INTO [LUP_DISEASE] ([sDESC]) VALUES (@sDESC);" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LU" & _
            "P_DISEASE WHERE (nCODE = SCOPE_IDENTITY())"
        Me.SqlInsertCommand.Connection = Me.SqlConnection1
        Me.SqlInsertCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC")})
        '
        'SqlUpdateCommand
        '
        Me.SqlUpdateCommand.CommandText = "UPDATE [LUP_DISEASE] SET [sDESC] = @sDESC WHERE (([nCODE] = @Original_nCODE) AND " & _
            "([sDESC] = @Original_sDESC));" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP_DISEASE WHERE (nCODE" & _
            " = @nCODE)"
        Me.SqlUpdateCommand.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsLUP_DISEASE1
        '
        Me.DsLUP_DISEASE1.DataSetName = "dsLUP_DISEASE"
        Me.DsLUP_DISEASE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daLUP_PANEL
        '
        Me.daLUP_PANEL.DeleteCommand = Me.SqlCommand5
        Me.daLUP_PANEL.InsertCommand = Me.SqlCommand6
        Me.daLUP_PANEL.SelectCommand = Me.SqlCommand7
        Me.daLUP_PANEL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_PANEL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_PANEL.UpdateCommand = Me.SqlCommand8
        '
        'SqlCommand5
        '
        Me.SqlCommand5.CommandText = "DELETE FROM [LUP_PANEL] WHERE (([nCODE] = @Original_nCODE) AND ([sDESC] = @Origin" & _
            "al_sDESC))"
        Me.SqlCommand5.Connection = Me.SqlConnection1
        Me.SqlCommand5.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand6
        '
        Me.SqlCommand6.CommandText = "INSERT INTO [LUP_PANEL] ([sDESC]) VALUES (@sDESC);" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP_" & _
            "PANEL WHERE (nCODE = SCOPE_IDENTITY())"
        Me.SqlCommand6.Connection = Me.SqlConnection1
        Me.SqlCommand6.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC")})
        '
        'SqlCommand7
        '
        Me.SqlCommand7.CommandText = "SELECT     LUP_PANEL.*" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         LUP_PANEL"
        Me.SqlCommand7.Connection = Me.SqlConnection1
        '
        'SqlCommand8
        '
        Me.SqlCommand8.CommandText = "UPDATE [LUP_PANEL] SET [sDESC] = @sDESC WHERE (([nCODE] = @Original_nCODE) AND ([" & _
            "sDESC] = @Original_sDESC));" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP_PANEL WHERE (nCODE = @" & _
            "nCODE)"
        Me.SqlCommand8.Connection = Me.SqlConnection1
        Me.SqlCommand8.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsLUP_PANEL1
        '
        Me.DsLUP_PANEL1.DataSetName = "dsLUP_PANEL"
        Me.DsLUP_PANEL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_PATIENT
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(718, 540)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_PATIENT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PATIENT DETAIL"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsV_PATIENT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_PATIENT11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_DISEASE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_PANEL1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_PATIENT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Me.FillComboBox_PANEL()
        Me.FillComboBox_DISEASE()

        Me.BttnNew_Click(sender, e)
        Me.ChkRegular.Checked = True

    End Sub

    Private Sub frmLUP_PATIENT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtName.GotFocus, TxtCode.GotFocus, TxtFatherName.GotFocus, TxtPerAddress.GotFocus, TxtHomePh.GotFocus, TxtEmail.GotFocus, TxtCell.GotFocus, TxtDiseaseDesc.GotFocus, TxtEnrolmentDate.GotFocus, TxtDischargeDate.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtName.LostFocus, TxtCode.LostFocus, TxtFatherName.LostFocus, TxtPerAddress.LostFocus, TxtHomePh.LostFocus, TxtEmail.LostFocus, TxtCell.LostFocus, TxtDiseaseDesc.LostFocus, TxtEnrolmentDate.LostFocus, TxtDischargeDate.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtEnrolmentDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

                Case "TxtDischargeDate"
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
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbPanel.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbPanel.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "CheckBox EVENTS"
    Private Sub ChkWorking_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkRegular.CheckedChanged
        Me.TxtDischargeDate.Enabled = Not Me.ChkRegular.Checked

        If Me.ChkRegular.Checked = False Then
            Me.TxtDischargeDate.Focus()
        End If
    End Sub
#End Region

#Region "ListView Control"
    '    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        On Error GoTo FIX
    '        Me.TxtCode.Text = Me.ListView1.SelectedItems(0).Text
    '        If Not Me.TxtCode.Text = Nothing Then
    '            Dim Str1 As String = "SELECT CODE, NAME, FATHER_NAME, NIC, HOME_PH, CELL, PRE_ADD, PER_ADD, DESIGNATION, APP_DATE, PAY, STATUS, LEAVE_DATE, EMAIL_ADD, BANK_ACC, BANK_ADD FROM V_EMPLOYEE_INFO WHERE CODE=" & Val(Me.TxtCode.Text) & ""
    '            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
    '            Me.daV_PATIENT = New SDS.SqlDataAdapter(SqlCmd1)

    '            Me.DsLUP_EMPLOYEE1.Clear()
    '            Me.daLUP_PATIENT.Fill(Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO)

    '            Me.BttnAdd.Enabled = False
    '        End If

    'FIX:
    '    End Sub
    '    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
    '            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(2).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
    '                Me.asDelete.DeleteValueIN("DELETE FROM LUP_EMPLOYEE WHERE nCODE=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

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
    Private Sub BttnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAdd.Click
        Me.TxtCode.Text = Val(Me.TxtCode.Text) + 1
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.DsV_PATIENT1.Clear()

        Me.TxtSearch.Text = Nothing
        Me.TxtCode.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nID) FROM LUP_PATIENT") + 1
        Me.TxtName.Focus()
        Me.BttnAdd.Enabled = True
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        'Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM LUP_EMPLOYEE WHERE nCODE=" & Val(Me.TxtCode.Text) & "")

        'If Val(Me.TxtCode.Text) <= 0 Then
        '    MsgBox("Employee ID can't be 'NULL', Click on 'NEW' for New ID", MsgBoxStyle.Exclamation, "(NS) - Wrong ID")
        '    Me.BttnNew.Focus()

        'ElseIf Me.TxtName.Text = Nothing Or Me.TxtFatherName.Text = Nothing Or Me.TxtNIC.Text = Nothing Or Me.CmbDesignation.SelectedIndex = -1 Or Me.CmbDesignation.Text = Nothing Or Val(Me.TxtPay.Text) <= 0 Or Me.TxtEnrolmentDate.Text = Nothing Then
        '    MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
        '    If Me.TxtName.Text = Nothing Then
        '        Me.TxtName.Focus()

        '    ElseIf Me.TxtFatherName.Text = Nothing Then
        '        Me.TxtFatherName.Focus()

        '    ElseIf Me.TxtNIC.Text = Nothing Then
        '        Me.TxtNIC.Focus()

        '    ElseIf Me.CmbDesignation.Text = Nothing Or Me.CmbDesignation.SelectedIndex = -1 Then
        '        Me.CmbDesignation.Focus()

        '    ElseIf Val(Me.TxtPay.Text) <= 0 Then
        '        Me.TxtPay.Focus()

        '    ElseIf Me.TxtEnrolmentDate.Text = Nothing Then
        '        Me.TxtEnrolmentDate.Focus()

        '    End If

        'ElseIf Me.asSELECT.pFlg1 = False Then
        '    If MsgBox("Do you want to save '" & Me.TxtName.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
        '        'INSERT VALUES
        '        If Me.TxtDischargeDate.Enabled = True Then
        '            If Me.TxtDischargeDate.Text = Nothing Then
        '                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
        '                Me.TxtDischargeDate.Focus()

        '                Exit Sub
        '            End If

        '            Me.asInsert.SaveValueIN("INSERT INTO LUP_EMPLOYEE(sNAME, sFATHER_NAME, sNIC, sHOME_PH, sCELL, sPRE_ADDRESS, sPER_ADDRESS, nDESIG_CODE, dAPP_DATE, nPAY, sSTATUS, dLEAVE_DATE, sEMAIL_ADD, sBANK_ACCOUNT, sBRANCH_ADD) VALUES('" & Me.TxtName.Text & "','" & Me.TxtFatherName.Text & "','" & Me.TxtNIC.Text & "','" & Me.TxtHomePh.Text & "','" & Me.TxtCell.Text & "','" & Me.TxtPreAddress.Text & "','" & Me.TxtPerAddress.Text & "'," & Val(Me.CmbDesignation.SelectedItem.col2) & ",'" & CDate(Me.TxtEnrolmentDate.Text).ToString("MM-dd-yyyy") & "'," & Val(Me.TxtPay.Text) & ",'" & Me.ChkRegular.CheckState & "','" & CDate(Me.TxtDischargeDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.TxtEmail.Text & "','" & Me.TxtBankAccount.Text & "','" & Me.TxtBankAddress.Text & "') ")

        '        Else
        '            Me.asInsert.SaveValueIN("INSERT INTO LUP_EMPLOYEE(sNAME, sFATHER_NAME, sNIC, sHOME_PH, sCELL, sPRE_ADDRESS, sPER_ADDRESS, nDESIG_CODE, dAPP_DATE, nPAY, sSTATUS, sEMAIL_ADD, sBANK_ACCOUNT, sBRANCH_ADD) VALUES('" & Me.TxtName.Text & "','" & Me.TxtFatherName.Text & "','" & Me.TxtNIC.Text & "','" & Me.TxtHomePh.Text & "','" & Me.TxtCell.Text & "','" & Me.TxtPreAddress.Text & "','" & Me.TxtPerAddress.Text & "'," & Val(Me.CmbDesignation.SelectedItem.col2) & ",'" & CDate(Me.TxtEnrolmentDate.Text).ToString("MM-dd-yyyy") & "'," & Val(Me.TxtPay.Text) & ",'" & Me.ChkRegular.CheckState & "', '" & Me.TxtEmail.Text & "','" & Me.TxtBankAccount.Text & "','" & Me.TxtBankAddress.Text & "') ")

        '        End If


        '        'FILL THE RECORD IN LISTVIEW
        '        Me.FillListView()
        '        Me.TxtName.Focus()
        '    End If

        'ElseIf Me.asSELECT.pFlg1 = True Then
        '    If MsgBox("This Employee ID '" & Me.TxtName.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
        '        'UPDATE RECORD
        '        If Me.TxtDischargeDate.Enabled = True Or Me.TxtEnrolmentDate.Text = Nothing Then
        '            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
        '            If Me.TxtEnrolmentDate.Text = Nothing Then
        '                Me.TxtEnrolmentDate.Focus()
        '                Exit Sub

        '            ElseIf Me.TxtDischargeDate.Text = Nothing Then
        '                Me.TxtDischargeDate.Focus()
        '                Exit Sub

        '            End If

        '            Me.asUpdate.UpdateValueIN("UPDATE LUP_EMPLOYEE SET sNAME='" & Me.TxtName.Text & "', sFATHER_NAME='" & Me.TxtFatherName.Text & "', sNIC='" & Me.TxtNIC.Text & "', sHOME_PH='" & Me.TxtHomePh.Text & "', sCELL='" & Me.TxtCell.Text & "', sPRE_ADDRESS='" & Me.TxtPreAddress.Text & "', sPER_ADDRESS='" & Me.TxtPerAddress.Text & "', nDESIG_CODE=" & Val(Me.CmbDesignation.SelectedItem.col2) & ", dAPP_DATE='" & CDate(Me.TxtEnrolmentDate.Text).ToString("MM-dd-yyyy") & "', nPAY=" & Val(Me.TxtPay.Text) & ", sSTATUS='" & Me.ChkRegular.CheckState & "', sEMAIL_ADD='" & Me.TxtEmail.Text & "', sBANK_ACCOUNT='" & Me.TxtBankAccount.Text & "', sBRANCH_ADD='" & Me.TxtBankAddress.Text & "' WHERE nCODE=" & Val(Me.TxtCode.Text) & "")

        '        Else
        '            If Me.TxtEnrolmentDate.Text = Nothing Then
        '                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
        '                Me.TxtEnrolmentDate.Focus()
        '                Exit Sub

        '            End If

        '            Me.asUpdate.UpdateValueIN("UPDATE LUP_EMPLOYEE SET sNAME='" & Me.TxtName.Text & "', sFATHER_NAME='" & Me.TxtFatherName.Text & "', sNIC='" & Me.TxtNIC.Text & "', sHOME_PH='" & Me.TxtHomePh.Text & "', sCELL='" & Me.TxtCell.Text & "', sPRE_ADDRESS='" & Me.TxtPreAddress.Text & "', sPER_ADDRESS='" & Me.TxtPerAddress.Text & "', nDESIG_CODE=" & Val(Me.CmbDesignation.SelectedItem.col2) & ", dAPP_DATE='" & CDate(Me.TxtEnrolmentDate.Text).ToString("MM-dd-yyyy") & "', nPAY=" & Val(Me.TxtPay.Text) & ", sSTATUS='" & Me.ChkRegular.CheckState & "', dLEAVE_DATE=NULL, sEMAIL_ADD='" & Me.TxtEmail.Text & "', sBANK_ACCOUNT='" & Me.TxtBankAccount.Text & "', sBRANCH_ADD='" & Me.TxtBankAddress.Text & "' WHERE nCODE=" & Val(Me.TxtCode.Text) & "")

        '        End If

        '        'FILL THE RECORD IN LISTVIEW
        '        Me.FillListView()
        '        Me.TxtName.Focus()
        '    End If

        'End If
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

    Private Sub FillComboBox_PANEL()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_PANEL ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_PANEL = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_PANEL1.Clear()
        Me.daLUP_PANEL.Fill(Me.DsLUP_PANEL1.LUP_PANEL)

        Dim dtLoading As New DataTable("LUP_PANEL")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_PANEL1.LUP_PANEL.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_PANEL1.LUP_PANEL.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_PANEL1.LUP_PANEL.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbPanel.SelectedIndex = -1
        Me.CmbPanel.Items.Clear()
        Me.CmbPanel.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbPanel.SourceDataString = New String(1) {"sDESC", "CODE"}
        Me.CmbPanel.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_DISEASE()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_DISEASE ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_DISEASE = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_DISEASE1.Clear()
        Me.daLUP_DISEASE.Fill(Me.DsLUP_DISEASE1.LUP_DISEASE)

        Dim dtLoading As New DataTable("LUP_DISEASE")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_DISEASE1.LUP_DISEASE.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_DISEASE1.LUP_DISEASE.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_DISEASE1.LUP_DISEASE.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbDisease.SelectedIndex = -1
        Me.CmbDisease.Items.Clear()
        Me.CmbDisease.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbDisease.SourceDataString = New String(1) {"sDESC", "CODE"}
        Me.CmbDisease.SourceDataTable = dtLoading
    End Sub


    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT ID, NAME, FNAME, NIC, PER_ADD, PHONE, CELL, EMAIL, DISEASE, DISEASE_DESC, PANEL, ENROL_DATE, STATUS, DISCH_DATE FROM V_PATIENT_INFO"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_PATIENT = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_PATIENT11.Clear()
            Me.daV_PATIENT.Fill(Me.DsV_PATIENT11.V_PATIENT_INFO)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim Str1 As String = "SELECT ID, NAME, FNAME, NIC, PER_ADD, PHONE, CELL, EMAIL, DISEASE, DISEASE_DESC, PANEL, ENROL_DATE, STATUS, DISCH_DATE FROM V_PATIENT_INFO WHERE NAME LIKE '%" & Me.TxtSearch.Text & "%'"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_PATIENT = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_PATIENT11.Clear()
            Me.daV_PATIENT.Fill(Me.DsV_PATIENT11.V_PATIENT_INFO)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


End Class
