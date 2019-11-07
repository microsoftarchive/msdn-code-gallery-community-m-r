Imports SDS = System.Data.SqlClient
Public Class frmLUP_BUSINESS_GROUP
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
    Friend WithEvents TxtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents TxtCode As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtGroupDealer As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DmnStatus As System.Windows.Forms.DomainUpDown
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents daLUP_BUSINESS_GROUP As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents TxtBusinessName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DsLUP_BUSINESS_GROUP_NEW1 As Neruo_Business_Solution.dsLUP_BUSINESS_GROUP_NEW
    Friend WithEvents DsLUP_BUSINESS_GROUP_NEW11 As Neruo_Business_Solution.dsLUP_BUSINESS_GROUP_NEW1
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents NIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SGROUPNAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SGROUPDEALERDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SBUSINESSNAMEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TxtOpenBal As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DsLUP_BUSINESS_GROUP_NEW11 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP_NEW1
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.DmnStatus = New System.Windows.Forms.DomainUpDown
        Me.DsLUP_BUSINESS_GROUP_NEW1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP_NEW
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtOpenBal = New System.Windows.Forms.TextBox
        Me.TxtGroupDealer = New System.Windows.Forms.TextBox
        Me.TxtGroupName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtBusinessName = New System.Windows.Forms.TextBox
        Me.TxtCode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.NIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SGROUPNAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SGROUPDEALERDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SBUSINESSNAMEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_BUSINESS_GROUP_NEW11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsLUP_BUSINESS_GROUP_NEW1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(600, 434)
        Me.Panel1.TabIndex = 0
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(8, 9)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(89, 45)
        Me.BttnPrevForm.TabIndex = 43
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Vans Detail"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Enabled = False
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(499, 9)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(89, 45)
        Me.BttnNextForm.TabIndex = 4
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Bank Accounts"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(598, 48)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "GROUP(s) INFORMATION"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(254, 60)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(334, 288)
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
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NIDDataGridViewTextBoxColumn, Me.SGROUPNAMEDataGridViewTextBoxColumn, Me.SGROUPDEALERDataGridViewTextBoxColumn, Me.SBUSINESSNAMEDataGridViewTextBoxColumn})
        Me.DataGridView1.DataMember = "V_BUSINESS_GROUP"
        Me.DataGridView1.DataSource = Me.DsLUP_BUSINESS_GROUP_NEW11
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(3, 53)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 30
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowTemplate.Height = 18
        Me.DataGridView1.RowTemplate.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.ShowEditingIcon = False
        Me.DataGridView1.Size = New System.Drawing.Size(328, 232)
        Me.DataGridView1.TabIndex = 3
        '
        'DsLUP_BUSINESS_GROUP_NEW11
        '
        Me.DsLUP_BUSINESS_GROUP_NEW11.DataSetName = "dsLUP_BUSINESS_GROUP_NEW1"
        Me.DsLUP_BUSINESS_GROUP_NEW11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.TxtSearch.Size = New System.Drawing.Size(192, 23)
        Me.TxtSearch.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 354)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(577, 73)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(43, 21)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(445, 21)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(244, 21)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.DmnStatus)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.TxtOpenBal)
        Me.GroupBox3.Controls.Add(Me.TxtGroupDealer)
        Me.GroupBox3.Controls.Add(Me.TxtGroupName)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtBusinessName)
        Me.GroupBox3.Controls.Add(Me.TxtCode)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 60)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(240, 288)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'DmnStatus
        '
        Me.DmnStatus.BackColor = System.Drawing.Color.White
        Me.DmnStatus.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_GROUP_NEW1, "V_BUSINESS_GROUP.STATUS", True))
        Me.DmnStatus.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DmnStatus.Items.Add("No")
        Me.DmnStatus.Items.Add("Yes")
        Me.DmnStatus.Location = New System.Drawing.Point(113, 225)
        Me.DmnStatus.Name = "DmnStatus"
        Me.DmnStatus.ReadOnly = True
        Me.DmnStatus.Size = New System.Drawing.Size(110, 23)
        Me.DmnStatus.TabIndex = 9
        '
        'DsLUP_BUSINESS_GROUP_NEW1
        '
        Me.DsLUP_BUSINESS_GROUP_NEW1.DataSetName = "dsLUP_BUSINESS_GROUP_NEW"
        Me.DsLUP_BUSINESS_GROUP_NEW1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 251)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 23)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Open Bal"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(16, 225)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(91, 23)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "In Use (Y/N)*"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtOpenBal
        '
        Me.TxtOpenBal.BackColor = System.Drawing.Color.White
        Me.TxtOpenBal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_GROUP_NEW1, "V_BUSINESS_GROUP.OPEN_BAL", True))
        Me.TxtOpenBal.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOpenBal.Location = New System.Drawing.Point(113, 251)
        Me.TxtOpenBal.Name = "TxtOpenBal"
        Me.TxtOpenBal.Size = New System.Drawing.Size(110, 23)
        Me.TxtOpenBal.TabIndex = 11
        Me.TxtOpenBal.Text = "0.00"
        Me.TxtOpenBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtGroupDealer
        '
        Me.TxtGroupDealer.BackColor = System.Drawing.Color.White
        Me.TxtGroupDealer.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_GROUP_NEW1, "V_BUSINESS_GROUP.sGROUP_DEALER", True))
        Me.TxtGroupDealer.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroupDealer.Location = New System.Drawing.Point(16, 199)
        Me.TxtGroupDealer.Name = "TxtGroupDealer"
        Me.TxtGroupDealer.Size = New System.Drawing.Size(207, 23)
        Me.TxtGroupDealer.TabIndex = 7
        '
        'TxtGroupName
        '
        Me.TxtGroupName.BackColor = System.Drawing.Color.White
        Me.TxtGroupName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_GROUP_NEW1, "V_BUSINESS_GROUP.sGROUP_NAME", True))
        Me.TxtGroupName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroupName.Location = New System.Drawing.Point(16, 149)
        Me.TxtGroupName.Name = "TxtGroupName"
        Me.TxtGroupName.Size = New System.Drawing.Size(207, 23)
        Me.TxtGroupName.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 175)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(207, 23)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Group &Dealer"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(207, 23)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "&Group Name"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(207, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "ID (Auto Generated)"
        '
        'TxtBusinessName
        '
        Me.TxtBusinessName.BackColor = System.Drawing.Color.White
        Me.TxtBusinessName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_GROUP_NEW1, "V_BUSINESS_GROUP.sBUSINESS_NAME", True))
        Me.TxtBusinessName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBusinessName.Location = New System.Drawing.Point(16, 99)
        Me.TxtBusinessName.Name = "TxtBusinessName"
        Me.TxtBusinessName.ReadOnly = True
        Me.TxtBusinessName.Size = New System.Drawing.Size(207, 23)
        Me.TxtBusinessName.TabIndex = 3
        Me.TxtBusinessName.TabStop = False
        '
        'TxtCode
        '
        Me.TxtCode.BackColor = System.Drawing.Color.White
        Me.TxtCode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.Location = New System.Drawing.Point(16, 48)
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.ReadOnly = True
        Me.TxtCode.Size = New System.Drawing.Size(207, 23)
        Me.TxtCode.TabIndex = 1
        Me.TxtCode.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(207, 23)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "&Business Name"
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
        Me.daLUP_BUSINESS_GROUP.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_BUSINESS_GROUP", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nID", "nID"), New System.Data.Common.DataColumnMapping("sGROUP_NAME", "sGROUP_NAME"), New System.Data.Common.DataColumnMapping("sGROUP_DEALER", "sGROUP_DEALER"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("sBUSINESS_NAME", "sBUSINESS_NAME"), New System.Data.Common.DataColumnMapping("OPEN_BAL", "OPEN_BAL")})})
        '
        'SqlCommand3
        '
        Me.SqlCommand3.CommandText = "SELECT     nID, sGROUP_NAME, sGROUP_DEALER, CASE sSTATUS WHEN '0' THEN 'No' WHEN " & _
            "'1' THEN 'Yes' END AS STATUS, sBUSINESS_NAME, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                      OPEN_BAL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
            "FROM         V_BUSINESS_GROUP"
        Me.SqlCommand3.Connection = Me.SqlConnection1
        '
        'NIDDataGridViewTextBoxColumn
        '
        Me.NIDDataGridViewTextBoxColumn.DataPropertyName = "nID"
        Me.NIDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.NIDDataGridViewTextBoxColumn.Name = "NIDDataGridViewTextBoxColumn"
        Me.NIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.NIDDataGridViewTextBoxColumn.Width = 60
        '
        'SGROUPNAMEDataGridViewTextBoxColumn
        '
        Me.SGROUPNAMEDataGridViewTextBoxColumn.DataPropertyName = "sGROUP_NAME"
        Me.SGROUPNAMEDataGridViewTextBoxColumn.HeaderText = "Group Name"
        Me.SGROUPNAMEDataGridViewTextBoxColumn.Name = "SGROUPNAMEDataGridViewTextBoxColumn"
        Me.SGROUPNAMEDataGridViewTextBoxColumn.ReadOnly = True
        Me.SGROUPNAMEDataGridViewTextBoxColumn.Width = 160
        '
        'SGROUPDEALERDataGridViewTextBoxColumn
        '
        Me.SGROUPDEALERDataGridViewTextBoxColumn.DataPropertyName = "sGROUP_DEALER"
        Me.SGROUPDEALERDataGridViewTextBoxColumn.HeaderText = "Dealer Name"
        Me.SGROUPDEALERDataGridViewTextBoxColumn.Name = "SGROUPDEALERDataGridViewTextBoxColumn"
        Me.SGROUPDEALERDataGridViewTextBoxColumn.ReadOnly = True
        Me.SGROUPDEALERDataGridViewTextBoxColumn.Width = 160
        '
        'SBUSINESSNAMEDataGridViewTextBoxColumn
        '
        Me.SBUSINESSNAMEDataGridViewTextBoxColumn.DataPropertyName = "sBUSINESS_NAME"
        Me.SBUSINESSNAMEDataGridViewTextBoxColumn.HeaderText = "Business Name"
        Me.SBUSINESSNAMEDataGridViewTextBoxColumn.Name = "SBUSINESSNAMEDataGridViewTextBoxColumn"
        Me.SBUSINESSNAMEDataGridViewTextBoxColumn.ReadOnly = True
        Me.SBUSINESSNAMEDataGridViewTextBoxColumn.Width = 160
        '
        'frmLUP_BUSINESS_GROUP
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(618, 450)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_BUSINESS_GROUP"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BUSINESS GROUP(s)"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_BUSINESS_GROUP_NEW11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsLUP_BUSINESS_GROUP_NEW1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_CLIENT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()

        Me.asTXT.LoadTextBox(Rd, "SELECT sBUSINESS_NAME FROM BUSINESS_INFO WHERE nID=1", Me.TxtBusinessName)

        Me.BttnNew_Click(sender, e)

        Me.DmnStatus.SelectedIndex = 1
    End Sub

    Private Sub frmLUP_CLIENT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtBusinessName.GotFocus, TxtCode.GotFocus, TxtGroupDealer.GotFocus, TxtGroupName.GotFocus, TxtOpenBal.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtBusinessName.LostFocus, TxtCode.LostFocus, TxtGroupDealer.LostFocus, TxtGroupName.LostFocus, TxtOpenBal.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtOpenBal.KeyPress
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

#Region "DOMAIN_UPDOWN EVENTS"
    Private Sub DmnStatus_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnStatus.GotFocus
        CType(sender, DomainUpDown).BackColor = Color.LightSteelBlue
    End Sub
    Private Sub DmnStatus_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnStatus.LostFocus
        CType(sender, DomainUpDown).BackColor = Color.White
    End Sub
#End Region

#Region "DataGrid Controls"
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        On Error GoTo FIX
        Me.TxtCode.Text = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
        If Not Me.TxtCode.Text = Nothing Then
            Dim Str1 As String = "SELECT nID, sGROUP_NAME, sGROUP_DEALER, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, sBUSINESS_NAME, CONVERT(NUMERIC(18,0), OPEN_BAL) AS OPEN_BAL FROM V_BUSINESS_GROUP WHERE nID=" & Val(Me.TxtCode.Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_BUSINESS_GROUP = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_BUSINESS_GROUP_NEW1.Clear()
            Me.daLUP_BUSINESS_GROUP.Fill(Me.DsLUP_BUSINESS_GROUP_NEW1.V_BUSINESS_GROUP)

            Me.DmnStatus.SelectedItem = Me.DmnStatus.Text
        End If

FIX:
    End Sub
    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        On Error GoTo FIX
        Me.TxtCode.Text = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
        If Not Me.TxtCode.Text = Nothing Then
            Dim Str1 As String = "SELECT nID, sGROUP_NAME, sGROUP_DEALER, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, sBUSINESS_NAME, CONVERT(NUMERIC(18,0), OPEN_BAL) AS OPEN_BAL FROM V_BUSINESS_GROUP WHERE nID=" & Val(Me.TxtCode.Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_BUSINESS_GROUP = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_BUSINESS_GROUP_NEW1.Clear()
            Me.daLUP_BUSINESS_GROUP.Fill(Me.DsLUP_BUSINESS_GROUP_NEW1.V_BUSINESS_GROUP)

            Me.DmnStatus.SelectedItem = Me.DmnStatus.Text
        End If

FIX:
    End Sub
    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        If Not Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.DataGridView1.Item(1, Me.DataGridView1.CurrentCell.RowIndex).Value & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM BUSINESS_GROUP WHERE nID=" & Val(Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value) & "")
                'Me.FillListView()
                Me.BttnNew_Click(sender, New System.EventArgs)

            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            e.Cancel = True
            Me.TxtGroupName.Focus()
        End If
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.TxtSearch.Text = Nothing

        Me.TxtCode.Text = Nothing
        Me.TxtGroupDealer.Text = Nothing
        Me.TxtGroupName.Text = Nothing
        Me.TxtOpenBal.Text = "0.00"

        Me.TxtGroupName.Focus()
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        If Me.TxtBusinessName.Text = Nothing Then
            MsgBox("Please Register your software application!", MsgBoxStyle.Critical, "(NS) - Illegal Copy!")
            Exit Sub

        ElseIf Me.TxtGroupName.Text = Nothing Or Me.TxtGroupDealer.Text = Nothing Or Me.DmnStatus.SelectedIndex = -1 Or Me.DmnStatus.Text = Nothing Or Val(Me.TxtOpenBal.Text) < 0 Then

            MsgBox("Please enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtGroupName.Text = Nothing Then
                Me.TxtGroupName.Focus()

            ElseIf Me.TxtGroupDealer.Text = Nothing Then
                Me.TxtGroupDealer.Focus()

            ElseIf Me.DmnStatus.SelectedIndex = -1 Or Me.DmnStatus.Text = Nothing Then
                Me.DmnStatus.Focus()

            ElseIf Val(Me.TxtOpenBal.Text) < 0 Then
                Me.TxtOpenBal.Focus()

            End If


        ElseIf Me.TxtCode.Text = Nothing Then
            If MsgBox("Do you want to save '" & Me.TxtGroupName.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO BUSINESS_GROUP(sGROUP_NAME,sGROUP_DEALER,sSTATUS,nBUSINESS_CODE,nOPEN_BAL) VALUES('" & Me.TxtGroupName.Text & "','" & Me.TxtGroupDealer.Text & "','" & Me.DmnStatus.SelectedIndex & "',1," & Val(Me.TxtOpenBal.Text) & ") ")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtGroupName.Focus()
            End If

        ElseIf Not Me.TxtCode.Text = Nothing Then
            If MsgBox("Do you want to update '" & Me.TxtGroupName.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE BUSINESS_GROUP SET sGROUP_NAME='" & Me.TxtGroupName.Text & "',sGROUP_DEALER='" & Me.TxtGroupDealer.Text & "', sSTATUS='" & Me.DmnStatus.SelectedIndex & "',nBUSINESS_CODE=1,nOPEN_BAL=" & Val(Me.TxtOpenBal.Text) & " WHERE nID=" & Val(Me.TxtCode.Text) & "")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtGroupName.Focus()
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        frmLUP_VAN.MdiParent = Me.ParentForm
        frmLUP_VAN.Show()
        frmLUP_VAN.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_BANK.MdiParent = Me.ParentForm
        frmLUP_BANK.Show()
        frmLUP_BANK.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT nID, sGROUP_NAME, sGROUP_DEALER, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, sBUSINESS_NAME, CONVERT(NUMERIC(18,0), OPEN_BAL) AS OPEN_BAL FROM V_BUSINESS_GROUP ORDER BY sGROUP_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_BUSINESS_GROUP = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_BUSINESS_GROUP_NEW11.Clear()
            Me.daLUP_BUSINESS_GROUP.Fill(Me.DsLUP_BUSINESS_GROUP_NEW11.V_BUSINESS_GROUP)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim Str1 As String = "SELECT nID, sGROUP_NAME, sGROUP_DEALER, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, sBUSINESS_NAME, CONVERT(NUMERIC(18,0), OPEN_BAL) AS OPEN_BAL FROM V_BUSINESS_GROUP WHERE sGROUP_NAME LIKE '%" & Me.TxtSearch.Text & "%' ORDER BY sGROUP_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_BUSINESS_GROUP = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_BUSINESS_GROUP_NEW11.Clear()
            Me.daLUP_BUSINESS_GROUP.Fill(Me.DsLUP_BUSINESS_GROUP_NEW11.V_BUSINESS_GROUP)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

End Class
