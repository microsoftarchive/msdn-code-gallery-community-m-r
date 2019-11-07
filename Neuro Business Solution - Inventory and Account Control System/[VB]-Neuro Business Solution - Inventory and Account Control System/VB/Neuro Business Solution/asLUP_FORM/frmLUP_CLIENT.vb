Imports SDS = System.Data.SqlClient
Public Class frmLUP_CLIENT
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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents CmbClientCAT As MTGCComboBox
    Friend WithEvents DmnStatus As System.Windows.Forms.DomainUpDown
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents TxtClientID As System.Windows.Forms.TextBox
    Friend WithEvents TxtCompany As System.Windows.Forms.TextBox
    Friend WithEvents TxtShopAddress As System.Windows.Forms.TextBox
    Friend WithEvents TxtShopPh As System.Windows.Forms.TextBox
    Friend WithEvents TxtHomePh As System.Windows.Forms.TextBox
    Friend WithEvents TxtEmail As System.Windows.Forms.TextBox
    Friend WithEvents TxtFax As System.Windows.Forms.TextBox
    Friend WithEvents TxtCell As System.Windows.Forms.TextBox
    Friend WithEvents TxtWebSite As System.Windows.Forms.TextBox
    Friend WithEvents TxtHomeAddress As System.Windows.Forms.TextBox
    Friend WithEvents CmbClientGD As MTGCComboBox
    Friend WithEvents CmbClientType As MTGCComboBox
    Friend WithEvents CmbArea As MTGCComboBox
    Friend WithEvents daLUP_AREA As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlInsertCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_AREA1 As Neruo_Business_Solution.dsLUP_AREA
    Friend WithEvents DsLUP_SHOP_CAT1 As Neruo_Business_Solution.dsLUP_SHOP_CAT
    Friend WithEvents daLUP_SHOP_CAT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_CLIENT_GD1 As Neruo_Business_Solution.dsLUP_CLIENT_GD
    Friend WithEvents daLUP_CLIENT_GD As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_CLIENT_TYPE1 As Neruo_Business_Solution.dsLUP_CLIENT_TYPE
    Friend WithEvents daLUP_CLIENT_TYPE As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TxtCreditLimit As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TxtGstNo As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents CmbRoute As MTGCComboBox
    Friend WithEvents TxtNo_Visit As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents daLUP_ROUTES As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_ROUTES1 As Neruo_Business_Solution.dsLUP_ROUTES
    Friend WithEvents BttnAdd As System.Windows.Forms.Button
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daCLIENT_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsCLIENT_INFO1 As Neruo_Business_Solution.dsCLIENT_INFO
    Friend WithEvents DsCLIENT_INFO11 As Neruo_Business_Solution.dsCLIENT_INFO1
    Friend WithEvents CmbVisitType As System.Windows.Forms.ComboBox
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents SqlUpdateCommand2 As System.Data.SqlClient.SqlCommand
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_CLIENT))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnPrevForm = New System.Windows.Forms.Button
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CmbVisitType = New System.Windows.Forms.ComboBox
        Me.DsCLIENT_INFO1 = New Neruo_Business_Solution.dsCLIENT_INFO
        Me.BttnAdd = New System.Windows.Forms.Button
        Me.TxtHomeAddress = New System.Windows.Forms.TextBox
        Me.CmbClientCAT = New MTGCComboBox
        Me.DmnStatus = New System.Windows.Forms.DomainUpDown
        Me.TxtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtClientID = New System.Windows.Forms.TextBox
        Me.TxtCompany = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtShopAddress = New System.Windows.Forms.TextBox
        Me.TxtShopPh = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtHomePh = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtCreditLimit = New System.Windows.Forms.TextBox
        Me.TxtEmail = New System.Windows.Forms.TextBox
        Me.TxtFax = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtCell = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtNo_Visit = New System.Windows.Forms.TextBox
        Me.TxtGstNo = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtWebSite = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.CmbClientGD = New MTGCComboBox
        Me.CmbRoute = New MTGCComboBox
        Me.CmbClientType = New MTGCComboBox
        Me.CmbArea = New MTGCComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_AREA = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlInsertCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_AREA1 = New Neruo_Business_Solution.dsLUP_AREA
        Me.DsLUP_SHOP_CAT1 = New Neruo_Business_Solution.dsLUP_SHOP_CAT
        Me.daLUP_SHOP_CAT = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_CLIENT_GD1 = New Neruo_Business_Solution.dsLUP_CLIENT_GD
        Me.daLUP_CLIENT_GD = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_CLIENT_TYPE1 = New Neruo_Business_Solution.dsLUP_CLIENT_TYPE
        Me.daLUP_CLIENT_TYPE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand2 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_ROUTES = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_ROUTES1 = New Neruo_Business_Solution.dsLUP_ROUTES
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand
        Me.daCLIENT_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.DsCLIENT_INFO11 = New Neruo_Business_Solution.dsCLIENT_INFO1
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsCLIENT_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_AREA1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_SHOP_CAT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_CLIENT_GD1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_CLIENT_TYPE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_ROUTES1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCLIENT_INFO11, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(12, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(694, 555)
        Me.Panel1.TabIndex = 0
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(5, 3)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(137, 25)
        Me.BttnPrevForm.TabIndex = 19
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Client Category"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(545, 3)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(137, 25)
        Me.BttnNextForm.TabIndex = 18
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Client Open Bal."
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 376)
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
        Me.ColumnHeader1.Width = 96
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Name"
        Me.ColumnHeader2.Width = 160
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Company"
        Me.ColumnHeader4.Width = 158
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Shop Ph."
        Me.ColumnHeader3.Width = 120
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Cell #"
        Me.ColumnHeader5.Width = 120
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
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(562, 34)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 336)
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
        Me.BttnClose.Location = New System.Drawing.Point(16, 291)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(16, 155)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.CmbVisitType)
        Me.GroupBox3.Controls.Add(Me.BttnAdd)
        Me.GroupBox3.Controls.Add(Me.TxtHomeAddress)
        Me.GroupBox3.Controls.Add(Me.CmbClientCAT)
        Me.GroupBox3.Controls.Add(Me.DmnStatus)
        Me.GroupBox3.Controls.Add(Me.TxtName)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtClientID)
        Me.GroupBox3.Controls.Add(Me.TxtCompany)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.TxtShopAddress)
        Me.GroupBox3.Controls.Add(Me.TxtShopPh)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtHomePh)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TxtCreditLimit)
        Me.GroupBox3.Controls.Add(Me.TxtEmail)
        Me.GroupBox3.Controls.Add(Me.TxtFax)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.TxtCell)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.TxtNo_Visit)
        Me.GroupBox3.Controls.Add(Me.TxtGstNo)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.TxtWebSite)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.CmbClientGD)
        Me.GroupBox3.Controls.Add(Me.CmbRoute)
        Me.GroupBox3.Controls.Add(Me.CmbClientType)
        Me.GroupBox3.Controls.Add(Me.CmbArea)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(11, 32)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(545, 338)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'CmbVisitType
        '
        Me.CmbVisitType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.VISIT_TYPE", True))
        Me.CmbVisitType.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbVisitType.FormattingEnabled = True
        Me.CmbVisitType.Items.AddRange(New Object() {"Weekly", "Monthly", "Quaterly"})
        Me.CmbVisitType.Location = New System.Drawing.Point(96, 278)
        Me.CmbVisitType.Name = "CmbVisitType"
        Me.CmbVisitType.Size = New System.Drawing.Size(208, 24)
        Me.CmbVisitType.TabIndex = 29
        '
        'DsCLIENT_INFO1
        '
        Me.DsCLIENT_INFO1.DataSetName = "dsCLIENT_INFO"
        Me.DsCLIENT_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BttnAdd
        '
        Me.BttnAdd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnAdd.Location = New System.Drawing.Point(216, 18)
        Me.BttnAdd.Name = "BttnAdd"
        Me.BttnAdd.Size = New System.Drawing.Size(33, 23)
        Me.BttnAdd.TabIndex = 1
        Me.BttnAdd.TabStop = False
        Me.BttnAdd.Text = "+&1"
        '
        'TxtHomeAddress
        '
        Me.TxtHomeAddress.BackColor = System.Drawing.Color.White
        Me.TxtHomeAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.HOME_ADD", True))
        Me.TxtHomeAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHomeAddress.Location = New System.Drawing.Point(96, 121)
        Me.TxtHomeAddress.MaxLength = 200
        Me.TxtHomeAddress.Multiline = True
        Me.TxtHomeAddress.Name = "TxtHomeAddress"
        Me.TxtHomeAddress.Size = New System.Drawing.Size(208, 48)
        Me.TxtHomeAddress.TabIndex = 11
        '
        'CmbClientCAT
        '
        Me.CmbClientCAT.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbClientCAT.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbClientCAT.ColumnNum = 2
        Me.CmbClientCAT.ColumnWidth = "140;40"
        Me.CmbClientCAT.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.CLIENT_CAT", True))
        Me.CmbClientCAT.DisplayMember = "Text"
        Me.CmbClientCAT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbClientCAT.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbClientCAT.DropDownForeColor = System.Drawing.Color.White
        Me.CmbClientCAT.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbClientCAT.DropDownWidth = 220
        Me.CmbClientCAT.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbClientCAT.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbClientCAT.GridLineHorizontal = False
        Me.CmbClientCAT.GridLineVertical = True
        Me.CmbClientCAT.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbClientCAT.Location = New System.Drawing.Point(96, 173)
        Me.CmbClientCAT.ManagingFastMouseMoving = True
        Me.CmbClientCAT.ManagingFastMouseMovingInterval = 30
        Me.CmbClientCAT.Name = "CmbClientCAT"
        Me.CmbClientCAT.Size = New System.Drawing.Size(208, 24)
        Me.CmbClientCAT.TabIndex = 21
        '
        'DmnStatus
        '
        Me.DmnStatus.BackColor = System.Drawing.Color.White
        Me.DmnStatus.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.STATUS", True))
        Me.DmnStatus.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DmnStatus.Items.Add("No")
        Me.DmnStatus.Items.Add("Yes")
        Me.DmnStatus.Location = New System.Drawing.Point(412, 279)
        Me.DmnStatus.Name = "DmnStatus"
        Me.DmnStatus.ReadOnly = True
        Me.DmnStatus.Size = New System.Drawing.Size(73, 23)
        Me.DmnStatus.TabIndex = 43
        '
        'TxtName
        '
        Me.TxtName.BackColor = System.Drawing.Color.White
        Me.TxtName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.NAME", True))
        Me.TxtName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.Location = New System.Drawing.Point(310, 18)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(224, 23)
        Me.TxtName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(251, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 23)
        Me.Label1.TabIndex = 2
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
        Me.Label2.Text = "Client ID*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtClientID
        '
        Me.TxtClientID.BackColor = System.Drawing.Color.White
        Me.TxtClientID.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtClientID.Location = New System.Drawing.Point(96, 18)
        Me.TxtClientID.MaxLength = 50
        Me.TxtClientID.Name = "TxtClientID"
        Me.TxtClientID.ReadOnly = True
        Me.TxtClientID.Size = New System.Drawing.Size(120, 23)
        Me.TxtClientID.TabIndex = 1
        Me.TxtClientID.TabStop = False
        '
        'TxtCompany
        '
        Me.TxtCompany.BackColor = System.Drawing.Color.White
        Me.TxtCompany.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.SHOP_NAME", True))
        Me.TxtCompany.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCompany.Location = New System.Drawing.Point(310, 44)
        Me.TxtCompany.Name = "TxtCompany"
        Me.TxtCompany.Size = New System.Drawing.Size(224, 23)
        Me.TxtCompany.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Area*"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(216, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 23)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Shop Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(8, 122)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(88, 48)
        Me.Label24.TabIndex = 10
        Me.Label24.Text = "Home Address"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 48)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Shop Address"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtShopAddress
        '
        Me.TxtShopAddress.BackColor = System.Drawing.Color.White
        Me.TxtShopAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.SHOP_ADD", True))
        Me.TxtShopAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShopAddress.Location = New System.Drawing.Point(96, 70)
        Me.TxtShopAddress.MaxLength = 200
        Me.TxtShopAddress.Multiline = True
        Me.TxtShopAddress.Name = "TxtShopAddress"
        Me.TxtShopAddress.Size = New System.Drawing.Size(208, 48)
        Me.TxtShopAddress.TabIndex = 9
        '
        'TxtShopPh
        '
        Me.TxtShopPh.BackColor = System.Drawing.Color.White
        Me.TxtShopPh.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.SHOP_PH", True))
        Me.TxtShopPh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShopPh.Location = New System.Drawing.Point(382, 70)
        Me.TxtShopPh.Name = "TxtShopPh"
        Me.TxtShopPh.Size = New System.Drawing.Size(152, 23)
        Me.TxtShopPh.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(310, 70)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 23)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Shop Ph."
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtHomePh
        '
        Me.TxtHomePh.BackColor = System.Drawing.Color.White
        Me.TxtHomePh.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.HOME_PH", True))
        Me.TxtHomePh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHomePh.Location = New System.Drawing.Point(382, 95)
        Me.TxtHomePh.Name = "TxtHomePh"
        Me.TxtHomePh.Size = New System.Drawing.Size(152, 23)
        Me.TxtHomePh.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(310, 95)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 23)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Home Ph."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(310, 226)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(72, 23)
        Me.Label19.TabIndex = 36
        Me.Label19.Text = "Credit Lmt"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(310, 174)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 23)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "E-mail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCreditLimit
        '
        Me.TxtCreditLimit.BackColor = System.Drawing.Color.White
        Me.TxtCreditLimit.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.CREDIT_LIM", True))
        Me.TxtCreditLimit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditLimit.Location = New System.Drawing.Point(382, 226)
        Me.TxtCreditLimit.Name = "TxtCreditLimit"
        Me.TxtCreditLimit.Size = New System.Drawing.Size(103, 23)
        Me.TxtCreditLimit.TabIndex = 37
        Me.TxtCreditLimit.Text = "0"
        Me.TxtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtEmail
        '
        Me.TxtEmail.BackColor = System.Drawing.Color.White
        Me.TxtEmail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.E_MAIL", True))
        Me.TxtEmail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmail.Location = New System.Drawing.Point(382, 174)
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.Size = New System.Drawing.Size(152, 23)
        Me.TxtEmail.TabIndex = 33
        '
        'TxtFax
        '
        Me.TxtFax.BackColor = System.Drawing.Color.White
        Me.TxtFax.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.FAX_NO", True))
        Me.TxtFax.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(382, 146)
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(152, 23)
        Me.TxtFax.TabIndex = 19
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(310, 146)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 23)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Fax #"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(310, 121)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 23)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Cell #"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCell
        '
        Me.TxtCell.BackColor = System.Drawing.Color.White
        Me.TxtCell.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.CELL_NO", True))
        Me.TxtCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCell.Location = New System.Drawing.Point(382, 121)
        Me.TxtCell.Name = "TxtCell"
        Me.TxtCell.Size = New System.Drawing.Size(152, 23)
        Me.TxtCell.TabIndex = 17
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(310, 252)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 23)
        Me.Label18.TabIndex = 38
        Me.Label18.Text = "GST #"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(310, 279)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(96, 23)
        Me.Label13.TabIndex = 42
        Me.Label13.Text = "Active (Y/N)*"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtNo_Visit
        '
        Me.TxtNo_Visit.BackColor = System.Drawing.Color.White
        Me.TxtNo_Visit.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.NO_VISIT", True))
        Me.TxtNo_Visit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNo_Visit.Location = New System.Drawing.Point(96, 304)
        Me.TxtNo_Visit.Name = "TxtNo_Visit"
        Me.TxtNo_Visit.Size = New System.Drawing.Size(120, 23)
        Me.TxtNo_Visit.TabIndex = 31
        Me.TxtNo_Visit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtGstNo
        '
        Me.TxtGstNo.BackColor = System.Drawing.Color.White
        Me.TxtGstNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.GST_NO", True))
        Me.TxtGstNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGstNo.Location = New System.Drawing.Point(382, 252)
        Me.TxtGstNo.Name = "TxtGstNo"
        Me.TxtGstNo.Size = New System.Drawing.Size(152, 23)
        Me.TxtGstNo.TabIndex = 39
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(310, 200)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 23)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "Web Site"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtWebSite
        '
        Me.TxtWebSite.BackColor = System.Drawing.Color.White
        Me.TxtWebSite.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.WEB_SITE", True))
        Me.TxtWebSite.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWebSite.Location = New System.Drawing.Point(382, 200)
        Me.TxtWebSite.Name = "TxtWebSite"
        Me.TxtWebSite.Size = New System.Drawing.Size(152, 23)
        Me.TxtWebSite.TabIndex = 35
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(8, 304)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(88, 23)
        Me.Label22.TabIndex = 30
        Me.Label22.Text = "No of Visits*"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(8, 279)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(88, 23)
        Me.Label21.TabIndex = 28
        Me.Label21.Text = "Visit Type*"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(8, 252)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(88, 23)
        Me.Label20.TabIndex = 26
        Me.Label20.Text = "Route*"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 226)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(88, 23)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Client Type*"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 173)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 23)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "Client Cat.*"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(8, 199)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(88, 23)
        Me.Label17.TabIndex = 22
        Me.Label17.Text = "Client Gd.*"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbClientGD
        '
        Me.CmbClientGD.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbClientGD.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbClientGD.ColumnNum = 2
        Me.CmbClientGD.ColumnWidth = "140;40"
        Me.CmbClientGD.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.CLIENT_GD", True))
        Me.CmbClientGD.DisplayMember = "Text"
        Me.CmbClientGD.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbClientGD.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbClientGD.DropDownForeColor = System.Drawing.Color.White
        Me.CmbClientGD.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbClientGD.DropDownWidth = 220
        Me.CmbClientGD.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbClientGD.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbClientGD.GridLineHorizontal = False
        Me.CmbClientGD.GridLineVertical = True
        Me.CmbClientGD.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbClientGD.Location = New System.Drawing.Point(96, 199)
        Me.CmbClientGD.ManagingFastMouseMoving = True
        Me.CmbClientGD.ManagingFastMouseMovingInterval = 30
        Me.CmbClientGD.Name = "CmbClientGD"
        Me.CmbClientGD.Size = New System.Drawing.Size(208, 24)
        Me.CmbClientGD.TabIndex = 23
        '
        'CmbRoute
        '
        Me.CmbRoute.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbRoute.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbRoute.ColumnNum = 3
        Me.CmbRoute.ColumnWidth = "140;140;40"
        Me.CmbRoute.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.ROUTE", True))
        Me.CmbRoute.DisplayMember = "Text"
        Me.CmbRoute.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbRoute.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbRoute.DropDownForeColor = System.Drawing.Color.White
        Me.CmbRoute.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbRoute.DropDownWidth = 340
        Me.CmbRoute.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbRoute.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbRoute.GridLineHorizontal = False
        Me.CmbRoute.GridLineVertical = True
        Me.CmbRoute.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbRoute.Location = New System.Drawing.Point(96, 251)
        Me.CmbRoute.ManagingFastMouseMoving = True
        Me.CmbRoute.ManagingFastMouseMovingInterval = 30
        Me.CmbRoute.Name = "CmbRoute"
        Me.CmbRoute.Size = New System.Drawing.Size(208, 24)
        Me.CmbRoute.TabIndex = 27
        '
        'CmbClientType
        '
        Me.CmbClientType.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbClientType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbClientType.ColumnNum = 2
        Me.CmbClientType.ColumnWidth = "140;40"
        Me.CmbClientType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.CLIENT_TYPE", True))
        Me.CmbClientType.DisplayMember = "Text"
        Me.CmbClientType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbClientType.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbClientType.DropDownForeColor = System.Drawing.Color.White
        Me.CmbClientType.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbClientType.DropDownWidth = 220
        Me.CmbClientType.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbClientType.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbClientType.GridLineHorizontal = False
        Me.CmbClientType.GridLineVertical = True
        Me.CmbClientType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbClientType.Location = New System.Drawing.Point(96, 225)
        Me.CmbClientType.ManagingFastMouseMoving = True
        Me.CmbClientType.ManagingFastMouseMovingInterval = 30
        Me.CmbClientType.Name = "CmbClientType"
        Me.CmbClientType.Size = New System.Drawing.Size(208, 24)
        Me.CmbClientType.TabIndex = 25
        '
        'CmbArea
        '
        Me.CmbArea.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbArea.ColumnNum = 3
        Me.CmbArea.ColumnWidth = "140;140;40"
        Me.CmbArea.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsCLIENT_INFO1, "V_CLIENT_INFO.AREA", True))
        Me.CmbArea.DisplayMember = "Text"
        Me.CmbArea.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbArea.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbArea.DropDownForeColor = System.Drawing.Color.White
        Me.CmbArea.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbArea.DropDownWidth = 340
        Me.CmbArea.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbArea.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbArea.GridLineHorizontal = False
        Me.CmbArea.GridLineVertical = True
        Me.CmbArea.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbArea.Location = New System.Drawing.Point(96, 44)
        Me.CmbArea.ManagingFastMouseMoving = True
        Me.CmbArea.ManagingFastMouseMovingInterval = 30
        Me.CmbArea.Name = "CmbArea"
        Me.CmbArea.Size = New System.Drawing.Size(120, 24)
        Me.CmbArea.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(692, 35)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "CLIENT(s) DETAIL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daLUP_AREA
        '
        Me.daLUP_AREA.InsertCommand = Me.SqlInsertCommand3
        Me.daLUP_AREA.SelectCommand = Me.SqlSelectCommand3
        Me.daLUP_AREA.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_AREA", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("ZONE", "ZONE")})})
        '
        'SqlInsertCommand3
        '
        Me.SqlInsertCommand3.CommandText = "INSERT INTO V_LUP_AREA(CODE, AREA, ZONE) VALUES (@CODE, @AREA, @ZONE); SELECT COD" & _
            "E, AREA, ZONE FROM V_LUP_AREA"
        Me.SqlInsertCommand3.Connection = Me.SqlConnection1
        Me.SqlInsertCommand3.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@CODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "CODE", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 50, "AREA"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 50, "ZONE")})
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "SELECT CODE, AREA, ZONE FROM V_LUP_AREA"
        Me.SqlSelectCommand3.Connection = Me.SqlConnection1
        '
        'DsLUP_AREA1
        '
        Me.DsLUP_AREA1.DataSetName = "dsLUP_AREA"
        Me.DsLUP_AREA1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_AREA1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_SHOP_CAT1
        '
        Me.DsLUP_SHOP_CAT1.DataSetName = "dsLUP_SHOP_CAT"
        Me.DsLUP_SHOP_CAT1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_SHOP_CAT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daLUP_SHOP_CAT
        '
        Me.daLUP_SHOP_CAT.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_SHOP_CAT.InsertCommand = Me.SqlInsertCommand1
        Me.daLUP_SHOP_CAT.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_SHOP_CAT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_SHOP_CAT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_SHOP_CAT.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "DELETE FROM LUP_SHOP_CAT WHERE (nCODE = @Original_nCODE) AND (sDESC = @Original_s" & _
            "DESC OR @Original_sDESC IS NULL AND sDESC IS NULL)"
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "INSERT INTO LUP_SHOP_CAT(sDESC) VALUES (@sDESC); SELECT nCODE, sDESC FROM LUP_SHO" & _
            "P_CAT WHERE (nCODE = @@IDENTITY)"
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC")})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT nCODE, sDESC FROM LUP_SHOP_CAT"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsLUP_CLIENT_GD1
        '
        Me.DsLUP_CLIENT_GD1.DataSetName = "dsLUP_CLIENT_GD"
        Me.DsLUP_CLIENT_GD1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_CLIENT_GD1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daLUP_CLIENT_GD
        '
        Me.daLUP_CLIENT_GD.DeleteCommand = Me.SqlCommand1
        Me.daLUP_CLIENT_GD.InsertCommand = Me.SqlCommand2
        Me.daLUP_CLIENT_GD.SelectCommand = Me.SqlCommand3
        Me.daLUP_CLIENT_GD.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_CLIENT_GD", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC"), New System.Data.Common.DataColumnMapping("nMIN_LIM", "nMIN_LIM"), New System.Data.Common.DataColumnMapping("nMAX_LIM", "nMAX_LIM")})})
        Me.daLUP_CLIENT_GD.UpdateCommand = Me.SqlCommand4
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "DELETE FROM LUP_CLIENT_GD WHERE (nCODE = @Original_nCODE) AND (nMAX_LIM = @Origin" & _
            "al_nMAX_LIM) AND (nMIN_LIM = @Original_nMIN_LIM) AND (sDESC = @Original_sDESC)"
        Me.SqlCommand1.Connection = Me.SqlConnection1
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand2
        '
        Me.SqlCommand2.CommandText = "INSERT INTO LUP_CLIENT_GD(sDESC, nMIN_LIM, nMAX_LIM) VALUES (@sDESC, @nMIN_LIM, @" & _
            "nMAX_LIM); SELECT nCODE, sDESC, nMIN_LIM, nMAX_LIM FROM LUP_CLIENT_GD WHERE (nCO" & _
            "DE = @@IDENTITY)"
        Me.SqlCommand2.Connection = Me.SqlConnection1
        Me.SqlCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Current, Nothing)})
        '
        'SqlCommand3
        '
        Me.SqlCommand3.CommandText = "SELECT nCODE, sDESC, nMIN_LIM, nMAX_LIM FROM LUP_CLIENT_GD"
        Me.SqlCommand3.Connection = Me.SqlConnection1
        '
        'SqlCommand4
        '
        Me.SqlCommand4.CommandText = resources.GetString("SqlCommand4.CommandText")
        Me.SqlCommand4.Connection = Me.SqlConnection1
        Me.SqlCommand4.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsLUP_CLIENT_TYPE1
        '
        Me.DsLUP_CLIENT_TYPE1.DataSetName = "dsLUP_CLIENT_TYPE"
        Me.DsLUP_CLIENT_TYPE1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_CLIENT_TYPE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daLUP_CLIENT_TYPE
        '
        Me.daLUP_CLIENT_TYPE.DeleteCommand = Me.SqlDeleteCommand2
        Me.daLUP_CLIENT_TYPE.InsertCommand = Me.SqlInsertCommand2
        Me.daLUP_CLIENT_TYPE.SelectCommand = Me.SqlSelectCommand2
        Me.daLUP_CLIENT_TYPE.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_CLIENT_TYPE", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_CLIENT_TYPE.UpdateCommand = Me.SqlUpdateCommand2
        '
        'SqlDeleteCommand2
        '
        Me.SqlDeleteCommand2.CommandText = "DELETE FROM LUP_CLIENT_TYPE WHERE (nCODE = @Original_nCODE) AND (sDESC = @Origina" & _
            "l_sDESC)"
        Me.SqlDeleteCommand2.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand2
        '
        Me.SqlInsertCommand2.CommandText = "INSERT INTO LUP_CLIENT_TYPE(sDESC) VALUES (@sDESC); SELECT nCODE, sDESC FROM LUP_" & _
            "CLIENT_TYPE WHERE (nCODE = @@IDENTITY)"
        Me.SqlInsertCommand2.Connection = Me.SqlConnection1
        Me.SqlInsertCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC")})
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "SELECT nCODE, sDESC FROM LUP_CLIENT_TYPE"
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        '
        'SqlUpdateCommand2
        '
        Me.SqlUpdateCommand2.CommandText = "UPDATE LUP_CLIENT_TYPE SET sDESC = @sDESC WHERE (nCODE = @Original_nCODE) AND (sD" & _
            "ESC = @Original_sDESC); SELECT nCODE, sDESC FROM LUP_CLIENT_TYPE WHERE (nCODE = " & _
            "@nCODE)"
        Me.SqlUpdateCommand2.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'daLUP_ROUTES
        '
        Me.daLUP_ROUTES.SelectCommand = Me.SqlCommand5
        Me.daLUP_ROUTES.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_ROUTES", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("ROUTES", "ROUTES"), New System.Data.Common.DataColumnMapping("AREA", "AREA")})})
        '
        'SqlCommand5
        '
        Me.SqlCommand5.CommandText = "SELECT     CODE, ROUTES, AREA" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_LUP_ROUTES"
        Me.SqlCommand5.Connection = Me.SqlConnection1
        '
        'DsLUP_ROUTES1
        '
        Me.DsLUP_ROUTES1.DataSetName = "dsLUP_ROUTES"
        Me.DsLUP_ROUTES1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = resources.GetString("SqlSelectCommand4.CommandText")
        Me.SqlSelectCommand4.Connection = Me.SqlConnection1
        '
        'daCLIENT_INFO
        '
        Me.daCLIENT_INFO.SelectCommand = Me.SqlSelectCommand4
        Me.daCLIENT_INFO.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_CLIENT_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("SHOP_ADD", "SHOP_ADD"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("HOME_ADD", "HOME_ADD"), New System.Data.Common.DataColumnMapping("SHOP_PH", "SHOP_PH"), New System.Data.Common.DataColumnMapping("HOME_PH", "HOME_PH"), New System.Data.Common.DataColumnMapping("CELL_NO", "CELL_NO"), New System.Data.Common.DataColumnMapping("FAX_NO", "FAX_NO"), New System.Data.Common.DataColumnMapping("E_MAIL", "E_MAIL"), New System.Data.Common.DataColumnMapping("WEB_SITE", "WEB_SITE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("CLIENT_CAT", "CLIENT_CAT"), New System.Data.Common.DataColumnMapping("CLIENT_GD", "CLIENT_GD"), New System.Data.Common.DataColumnMapping("CLIENT_TYPE", "CLIENT_TYPE"), New System.Data.Common.DataColumnMapping("CREDIT_LIM", "CREDIT_LIM"), New System.Data.Common.DataColumnMapping("GST_NO", "GST_NO"), New System.Data.Common.DataColumnMapping("OPEN_BAL", "OPEN_BAL"), New System.Data.Common.DataColumnMapping("VISIT_TYPE", "VISIT_TYPE"), New System.Data.Common.DataColumnMapping("NO_VISIT", "NO_VISIT"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE")})})
        '
        'DsCLIENT_INFO11
        '
        Me.DsCLIENT_INFO11.DataSetName = "dsCLIENT_INFO1"
        Me.DsCLIENT_INFO11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_CLIENT
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(718, 571)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_CLIENT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CLIENT INFOMATION"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsCLIENT_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_AREA1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_SHOP_CAT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_CLIENT_GD1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_CLIENT_TYPE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_ROUTES1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCLIENT_INFO11, System.ComponentModel.ISupportInitialize).EndInit()
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
        Me.FillComboBox_AREA()
        Me.FillComboBox_SHOP_CAT()
        Me.FillComboBox_CLIENT_GD()
        Me.FillComboBox_CLIENT_TYPE()
        Me.FillComboBox_ROUTES()

        Me.BttnNew_Click(sender, e)

        Me.DmnStatus.SelectedIndex = 1
    End Sub

    Private Sub frmLUP_CLIENT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtName.GotFocus, TxtClientID.GotFocus, TxtCompany.GotFocus, TxtShopAddress.GotFocus, TxtShopPh.GotFocus, TxtHomePh.GotFocus, TxtEmail.GotFocus, TxtFax.GotFocus, TxtCell.GotFocus, TxtHomeAddress.GotFocus, TxtEmail.GotFocus, TxtWebSite.GotFocus, TxtGstNo.GotFocus, TxtCreditLimit.GotFocus, TxtNo_Visit.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtName.LostFocus, TxtClientID.LostFocus, TxtCompany.LostFocus, TxtShopAddress.LostFocus, TxtShopPh.LostFocus, TxtHomePh.LostFocus, TxtEmail.LostFocus, TxtFax.LostFocus, TxtCell.LostFocus, TxtHomeAddress.LostFocus, TxtEmail.LostFocus, TxtWebSite.LostFocus, TxtNo_Visit.LostFocus, TxtCreditLimit.LostFocus, TxtGstNo.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNo_Visit.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    'KeyPress Numeric With Desh
    Private Sub Txt_Num_Desh_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtShopPh.KeyPress, TxtHomePh.KeyPress, TxtFax.KeyPress, TxtCell.KeyPress
        Me.asNum.NumPressDash(e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCreditLimit.KeyPress
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
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbArea.GotFocus, CmbClientCAT.GotFocus, CmbClientGD.GotFocus, CmbClientType.GotFocus, CmbRoute.GotFocus, CmbVisitType.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbArea.LostFocus, CmbClientCAT.LostFocus, CmbClientGD.LostFocus, CmbClientType.LostFocus, CmbRoute.LostFocus, CmbVisitType.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Select Case Ctrl.Name
            Case "CmbVisitType"
                sender.SelectedIndex = sender.FindString(sender.Text)
        End Select
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

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        Me.TxtClientID.Text = Me.ListView1.SelectedItems(0).Text
        If Not Me.TxtClientID.Text = Nothing Then
            Dim Str1 As String = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CONVERT(NUMERIC(18,2), CREDIT_LIM) AS CREDIT_LIM, GST_NO, CONVERT(NUMERIC(18,2), OPEN_BAL) AS OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO WHERE ID=" & Val(Me.TxtClientID.Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daCLIENT_INFO = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsCLIENT_INFO1.Clear()
            Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO1.V_CLIENT_INFO)

            Me.BttnAdd.Enabled = False
            Me.DmnStatus.SelectedItem = Me.DmnStatus.Text
        End If

FIX:
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(2).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM CLIENT_INFO WHERE nID=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

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
        Me.TxtClientID.Text = Val(Me.TxtClientID.Text) + 1
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.DsCLIENT_INFO1.Clear()

        Me.TxtSearch.Text = Nothing
        Me.TxtClientID.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nID) FROM CLIENT_INFO") + 1
        Me.TxtName.Focus()
        Me.BttnAdd.Enabled = True
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM CLIENT_INFO WHERE nID=" & Val(Me.TxtClientID.Text) & "")

        If Val(Me.TxtClientID.Text) <= 0 Then
            MsgBox("Client ID can't be 'NULL', Click on 'NEW' for New ID", MsgBoxStyle.Exclamation, "(NS) - Wrong ID")
            Me.BttnNew.Focus()

        ElseIf Me.TxtName.Text = Nothing Or Me.CmbArea.Text = Nothing Or Me.CmbArea.SelectedIndex = -1 Or Me.CmbClientCAT.Text = Nothing Or Me.CmbClientCAT.SelectedIndex = -1 Or Me.CmbClientGD.Text = Nothing Or Me.CmbClientGD.SelectedIndex = -1 Or Me.CmbClientType.Text = Nothing Or Me.CmbClientType.SelectedIndex = -1 Or Me.CmbRoute.Text = Nothing Or Me.CmbRoute.SelectedIndex = -1 Or Me.CmbVisitType.Text = Nothing Or Me.CmbVisitType.SelectedIndex = -1 Or Me.DmnStatus.SelectedIndex = -1 Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtName.Text = Nothing Then
                Me.TxtName.Focus()

            ElseIf Me.CmbArea.Text = Nothing Or Me.CmbArea.SelectedIndex = -1 Then
                Me.CmbArea.Focus()

            ElseIf Me.CmbClientCAT.Text = Nothing Or Me.CmbClientCAT.SelectedIndex = -1 Then
                Me.CmbClientCAT.Focus()

            ElseIf Me.CmbClientGD.Text = Nothing Or Me.CmbClientGD.SelectedIndex = -1 Then
                Me.CmbClientGD.Focus()

            ElseIf Me.CmbClientType.Text = Nothing Or Me.CmbClientType.SelectedIndex = -1 Then
                Me.CmbClientType.Focus()

            ElseIf Me.CmbRoute.Text = Nothing Or Me.CmbRoute.SelectedIndex = -1 Then
                Me.CmbRoute.Focus()

            ElseIf Me.CmbVisitType.Text = Nothing Or Me.CmbVisitType.SelectedIndex = -1 Then
                Me.CmbVisitType.Focus()

            ElseIf Me.DmnStatus.SelectedIndex = -1 Then
                Me.DmnStatus.Focus()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            If MsgBox("Do you want to save '" & Me.TxtName.Text & "' & '" & Me.TxtCompany.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO CLIENT_INFO(nID, sNAME, sCOMPANY_NAME, sSHOP_ADD, nAREA_CODE, sHOME_ADD, sSHOP_PH, sHOME_PH, sCELL_NO, sFAX_NO, sE_MAIL, sWEB_ADD, sSTATUS, nSHOP_CAT_CODE, nCLIENT_GD_CODE, nCLIENT_TYPE_CODE, nCREDIT_LIM, sGST_NO, sVISIT_TYPE, nNO_VISIT, nROUTES_CODE) VALUES(" & Val(Me.TxtClientID.Text) & ",'" & Me.TxtName.Text & "','" & Me.TxtCompany.Text & "','" & Me.TxtShopAddress.Text & "'," & Val(Me.CmbArea.SelectedItem.col3) & ",'" & Me.TxtHomeAddress.Text & "','" & Me.TxtShopPh.Text & "','" & Me.TxtHomePh.Text & "','" & Me.TxtCell.Text & "','" & Me.TxtFax.Text & "','" & Me.TxtEmail.Text & "','" & Me.TxtWebSite.Text & "','" & Me.DmnStatus.SelectedIndex & "'," & Val(Me.CmbClientCAT.SelectedItem.col2) & "," & Val(Me.CmbClientGD.SelectedItem.col2) & "," & Val(Me.CmbClientType.SelectedItem.col2) & "," & Val(Me.TxtCreditLimit.Text) & ",'" & Me.TxtGstNo.Text & "','" & Me.CmbVisitType.Text & "'," & Val(Me.TxtNo_Visit.Text) & "," & Val(Me.CmbRoute.SelectedItem.col3) & ") ")

                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtName.Focus()
            End If

        ElseIf Me.asSELECT.pFlg1 = True Then
            If MsgBox("This Client ID '" & Me.TxtName.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE CLIENT_INFO SET sNAME='" & Me.TxtName.Text & "', sCOMPANY_NAME='" & Me.TxtCompany.Text & "', sSHOP_ADD='" & Me.TxtShopAddress.Text & "', nAREA_CODE=" & Val(Me.CmbArea.SelectedItem.col3) & ", sHOME_ADD='" & Me.TxtHomeAddress.Text & "', sSHOP_PH='" & Me.TxtShopPh.Text & "', sHOME_PH='" & Me.TxtHomePh.Text & "', sCELL_NO='" & Me.TxtCell.Text & "', sFAX_NO='" & Me.TxtFax.Text & "', sE_MAIL='" & Me.TxtEmail.Text & "', sWEB_ADD='" & Me.TxtWebSite.Text & "', sSTATUS='" & Me.DmnStatus.SelectedIndex & "', nSHOP_CAT_CODE=" & Val(Me.CmbClientCAT.SelectedItem.col2) & ", nCLIENT_GD_CODE=" & Val(Me.CmbClientGD.SelectedItem.col2) & ", nCLIENT_TYPE_CODE=" & Val(Me.CmbClientType.SelectedItem.col2) & ", nCREDIT_LIM=" & Val(Me.TxtCreditLimit.Text) & ", sGST_NO='" & Me.TxtGstNo.Text & "', sVISIT_TYPE='" & Me.CmbVisitType.Text & "', nNO_VISIT=" & Val(Me.TxtNo_Visit.Text) & ", nROUTES_CODE=" & Val(Me.CmbRoute.SelectedItem.col3) & " WHERE nID=" & Val(Me.TxtClientID.Text) & "")
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
        frmLUP_SHOP_CAT.MdiParent = Me.ParentForm
        frmLUP_SHOP_CAT.Show()
        frmLUP_SHOP_CAT.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_CLIENT_OPEN_BAL.MdiParent = Me.ParentForm
        frmLUP_CLIENT_OPEN_BAL.Show()
        frmLUP_CLIENT_OPEN_BAL.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"

    Private Sub FillComboBox_AREA()
        Dim Str1 As String = "SELECT CODE, AREA, ZONE FROM V_LUP_AREA ORDER BY AREA"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_AREA = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_AREA1.Clear()
        Me.daLUP_AREA.Fill(Me.DsLUP_AREA1.V_LUP_AREA)

        Dim dtLoading As New DataTable("V_LUP_AREA")

        dtLoading.Columns.Add("CODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("AREA", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("ZONE", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_AREA1.V_LUP_AREA.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("CODE") = Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(0).ToString
            dr("AREA") = Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(1).ToString
            dr("ZONE") = Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(2).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbArea.SelectedIndex = -1
        Me.CmbArea.Items.Clear()
        Me.CmbArea.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbArea.SourceDataString = New String(2) {"AREA", "ZONE", "CODE"}
        Me.CmbArea.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_SHOP_CAT()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_SHOP_CAT ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_SHOP_CAT = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_SHOP_CAT1.Clear()
        Me.daLUP_SHOP_CAT.Fill(Me.DsLUP_SHOP_CAT1.LUP_SHOP_CAT)

        Dim dtLoading As New DataTable("LUP_SHOP_CAT")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_SHOP_CAT1.LUP_SHOP_CAT.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_SHOP_CAT1.LUP_SHOP_CAT.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_SHOP_CAT1.LUP_SHOP_CAT.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbClientCAT.SelectedIndex = -1
        Me.CmbClientCAT.Items.Clear()
        Me.CmbClientCAT.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbClientCAT.SourceDataString = New String(1) {"sDESC", "nCODE"}
        Me.CmbClientCAT.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_CLIENT_GD()
        Dim Str1 As String = "SELECT nCODE, sDESC, nMIN_LIM, nMAX_LIM FROM LUP_CLIENT_GD ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_CLIENT_GD = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_CLIENT_GD1.Clear()
        Me.daLUP_CLIENT_GD.Fill(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD)

        Dim dtLoading As New DataTable("LUP_CLIENT_GD")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbClientGD.SelectedIndex = -1
        Me.CmbClientGD.Items.Clear()
        Me.CmbClientGD.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbClientGD.SourceDataString = New String(1) {"sDESC", "nCODE"}
        Me.CmbClientGD.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_CLIENT_TYPE()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_CLIENT_TYPE ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_CLIENT_TYPE = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_CLIENT_TYPE1.Clear()
        Me.daLUP_CLIENT_TYPE.Fill(Me.DsLUP_CLIENT_TYPE1.LUP_CLIENT_TYPE)

        Dim dtLoading As New DataTable("LUP_CLIENT_TYPE")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_CLIENT_TYPE1.LUP_CLIENT_TYPE.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_CLIENT_TYPE1.LUP_CLIENT_TYPE.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_CLIENT_TYPE1.LUP_CLIENT_TYPE.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbClientType.SelectedIndex = -1
        Me.CmbClientType.Items.Clear()
        Me.CmbClientType.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbClientType.SourceDataString = New String(1) {"sDESC", "nCODE"}
        Me.CmbClientType.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_ROUTES()
        Dim Str1 As String = "SELECT CODE, ROUTES, AREA FROM V_LUP_ROUTES ORDER BY ROUTES"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_ROUTES = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_ROUTES1.Clear()
        Me.daLUP_ROUTES.Fill(Me.DsLUP_ROUTES1.V_LUP_ROUTES)

        Dim dtLoading As New DataTable("V_LUP_ROUTES")

        dtLoading.Columns.Add("CODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("ROUTES", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("AREA", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_ROUTES1.V_LUP_ROUTES.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("CODE") = Me.DsLUP_ROUTES1.V_LUP_ROUTES.Item(Cnt).Item(0).ToString
            dr("ROUTES") = Me.DsLUP_ROUTES1.V_LUP_ROUTES.Item(Cnt).Item(1).ToString
            dr("AREA") = Me.DsLUP_ROUTES1.V_LUP_ROUTES.Item(Cnt).Item(2).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbRoute.SelectedIndex = -1
        Me.CmbRoute.Items.Clear()
        Me.CmbRoute.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbRoute.SourceDataString = New String(2) {"ROUTES", "AREA", "CODE"}
        Me.CmbRoute.SourceDataTable = dtLoading
    End Sub

    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CONVERT(NUMERIC(18,2), CREDIT_LIM) AS CREDIT_LIM, GST_NO, CONVERT(NUMERIC(18,2), OPEN_BAL) AS OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO ORDER BY SHOP_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daCLIENT_INFO = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsCLIENT_INFO11.Clear()
            Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO11.V_CLIENT_INFO)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsCLIENT_INFO11.V_CLIENT_INFO.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(8).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim Str1 As String = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CONVERT(NUMERIC(18,2), CREDIT_LIM) AS CREDIT_LIM, GST_NO, CONVERT(NUMERIC(18, 2), OPEN_BAL) AS OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO WHERE SHOP_NAME LIKE '%" & Me.TxtSearch.Text & "%'ORDER BY SHOP_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daCLIENT_INFO = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsCLIENT_INFO11.Clear()
            Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO11.V_CLIENT_INFO)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsCLIENT_INFO11.V_CLIENT_INFO.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsCLIENT_INFO11.V_CLIENT_INFO.Item(Cnt).Item(8).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

End Class
