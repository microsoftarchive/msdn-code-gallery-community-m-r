Imports SDS = System.Data.SqlClient
Public Class frmSALE
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtInvoice As System.Windows.Forms.TextBox
    Friend WithEvents TxtDispDate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtVehicle As System.Windows.Forms.TextBox
    Friend WithEvents CmbGroup As MTGCComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtCashClient As System.Windows.Forms.TextBox
    Friend WithEvents CmbClient As MTGCComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CmbD_Man As MTGCComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtReceivables As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalItems As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TxtFreight As System.Windows.Forms.TextBox
    Friend WithEvents TxtTRno As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TxtUnloading As System.Windows.Forms.TextBox
    Friend WithEvents TxtTRqty As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtTotal As System.Windows.Forms.TextBox
    Friend WithEvents TxtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtOtherDisc As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TxtDiscPercent As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TxtDescription As System.Windows.Forms.TextBox
    Friend WithEvents TxtReceipt As System.Windows.Forms.TextBox
    Friend WithEvents TxtNetTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents BttnReceipt As System.Windows.Forms.Button
    Friend WithEvents TxtInvBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents BttnSearch_Item As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents LblScmItem As System.Windows.Forms.Label
    Friend WithEvents LblB_Pcs As System.Windows.Forms.Label
    Friend WithEvents LblRate As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents LblStock As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents LblRetail As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents LblPPP As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TxtClientBal As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents DsLUP_BUSINESS_GROUP1 As Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
    Friend WithEvents daLUP_BUSINESS_GROUP As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_EMPLOYEE As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_EMPLOYEE1 As Neruo_Business_Solution.dsLUP_EMPLOYEE
    Friend WithEvents DsLUP_ITEM1 As Neruo_Business_Solution.dsLUP_ITEM
    Friend WithEvents daLUP_ITEM As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_CLIENT_BAL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daSV_STOCK_BAL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlConnection2 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblRatePcs As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_SALE_MASTER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_SALE_DETAIL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_CLIENT_RECEIPT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents BttnAdd As System.Windows.Forms.Button
    Friend WithEvents daCLIENT_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsCLIENT_INFO1 As Neruo_Business_Solution.dsCLIENT_INFO
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TxtStandyBy As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents TxtNET_Receivable As System.Windows.Forms.TextBox
    Friend WithEvents daV_CLIENT_STANDBY_CHEQ As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsV_CLIENT_STANDBY_CHEQ1 As Neruo_Business_Solution.dsV_CLIENT_STANDBY_CHEQ
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TxtCashMemo As System.Windows.Forms.TextBox
    Friend WithEvents LblLoadPass As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents DsV_SALE_MASTER1 As Neruo_Business_Solution.dsV_SALE_MASTER
    Friend WithEvents DsV_SALE_DETAIL1 As Neruo_Business_Solution.dsV_SALE_DETAIL
    Friend WithEvents DsV_CLIENT_RECEIPT1 As Neruo_Business_Solution.dsV_CLIENT_RECEIPT
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DsV_CLIENT_BAL1 As Neruo_Business_Solution.dsV_CLIENT_BAL
    Friend WithEvents CmbS_Man As MTGCComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents BttnSearch_Inv As System.Windows.Forms.Button
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnPrev As System.Windows.Forms.Button
    Friend WithEvents BttnPrint As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents ColCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBatch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPiece As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBonus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPercentage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDisc_Rs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSaleTax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColScmItem As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ColScmQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DsNS_DEFAULT1 As Neruo_Business_Solution.dsNS_DEFAULT
    Friend WithEvents daNS_DEFAULT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsSV_STOCK_BAL1 As Neruo_Business_Solution.dsSV_STOCK_BAL
    Friend WithEvents Label32 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle49 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle60 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle50 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle51 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle52 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle53 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle54 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle55 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle56 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle57 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle58 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle59 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSALE))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ColCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColBatch = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColRate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPack = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPiece = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColBonus = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPercentage = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColDisc_Rs = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColSaleTax = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColScmItem = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.ColScmQty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColSR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPPP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LblScmItem = New System.Windows.Forms.Label
        Me.LblB_Pcs = New System.Windows.Forms.Label
        Me.DsLUP_ITEM1 = New Neruo_Business_Solution.dsLUP_ITEM
        Me.LblRatePcs = New System.Windows.Forms.Label
        Me.LblPPP = New System.Windows.Forms.Label
        Me.LblStock = New System.Windows.Forms.Label
        Me.LblRetail = New System.Windows.Forms.Label
        Me.LblRate = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.BttnReceipt = New System.Windows.Forms.Button
        Me.TxtTotal = New System.Windows.Forms.TextBox
        Me.DsV_SALE_MASTER1 = New Neruo_Business_Solution.dsV_SALE_MASTER
        Me.TxtDescription = New System.Windows.Forms.TextBox
        Me.TxtReceipt = New System.Windows.Forms.TextBox
        Me.DsV_CLIENT_RECEIPT1 = New Neruo_Business_Solution.dsV_CLIENT_RECEIPT
        Me.TxtClientBal = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TxtInvBalance = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtNetTotal = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TxtOtherDisc = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.TxtDiscPercent = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.TxtDiscount = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.TxtTRno = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtTRqty = New System.Windows.Forms.TextBox
        Me.TxtTotalItems = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtFreight = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtUnloading = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.TxtRemarks = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnSearch_Inv = New System.Windows.Forms.Button
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnPrev = New System.Windows.Forms.Button
        Me.BttnPrint = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.BttnSearch_Item = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.BttnAdd = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtCashClient = New System.Windows.Forms.TextBox
        Me.TxtInvoice = New System.Windows.Forms.TextBox
        Me.CmbClient = New MTGCComboBox
        Me.CmbS_Man = New MTGCComboBox
        Me.CmbD_Man = New MTGCComboBox
        Me.CmbGroup = New MTGCComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtNET_Receivable = New System.Windows.Forms.TextBox
        Me.DsV_CLIENT_BAL1 = New Neruo_Business_Solution.dsV_CLIENT_BAL
        Me.TxtCashMemo = New System.Windows.Forms.TextBox
        Me.TxtStandyBy = New System.Windows.Forms.TextBox
        Me.DsV_CLIENT_STANDBY_CHEQ1 = New Neruo_Business_Solution.dsV_CLIENT_STANDBY_CHEQ
        Me.TxtReceivables = New System.Windows.Forms.TextBox
        Me.TxtDispDate = New System.Windows.Forms.TextBox
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TxtVehicle = New System.Windows.Forms.TextBox
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_EMPLOYEE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_ITEM = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand
        Me.daV_CLIENT_BAL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection2 = New System.Data.SqlClient.SqlConnection
        Me.daSV_STOCK_BAL = New System.Data.SqlClient.SqlDataAdapter
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlSelectCommand5 = New System.Data.SqlClient.SqlCommand
        Me.daV_SALE_MASTER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand7 = New System.Data.SqlClient.SqlCommand
        Me.daV_SALE_DETAIL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand6 = New System.Data.SqlClient.SqlCommand
        Me.daV_CLIENT_RECEIPT = New System.Data.SqlClient.SqlDataAdapter
        Me.daCLIENT_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.daV_CLIENT_STANDBY_CHEQ = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand6 = New System.Data.SqlClient.SqlCommand
        Me.LblLoadPass = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.daNS_DEFAULT = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_BUSINESS_GROUP1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
        Me.DsLUP_EMPLOYEE1 = New Neruo_Business_Solution.dsLUP_EMPLOYEE
        Me.DsCLIENT_INFO1 = New Neruo_Business_Solution.dsCLIENT_INFO
        Me.DsV_SALE_DETAIL1 = New Neruo_Business_Solution.dsV_SALE_DETAIL
        Me.DsNS_DEFAULT1 = New Neruo_Business_Solution.dsNS_DEFAULT
        Me.DsSV_STOCK_BAL1 = New Neruo_Business_Solution.dsSV_STOCK_BAL
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DsV_SALE_MASTER1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_CLIENT_RECEIPT1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsV_CLIENT_BAL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_CLIENT_STANDBY_CHEQ1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCLIENT_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_SALE_DETAIL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsNS_DEFAULT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSV_STOCK_BAL1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.BackColor = System.Drawing.Color.Aquamarine
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Controls.Add(Me.LblScmItem)
        Me.GroupBox2.Controls.Add(Me.LblB_Pcs)
        Me.GroupBox2.Controls.Add(Me.LblRatePcs)
        Me.GroupBox2.Controls.Add(Me.LblPPP)
        Me.GroupBox2.Controls.Add(Me.LblStock)
        Me.GroupBox2.Controls.Add(Me.LblRetail)
        Me.GroupBox2.Controls.Add(Me.LblRate)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 151)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(810, 203)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.LightSteelBlue
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle49.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle49.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle49.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle49.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle49.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle49.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle49
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCode, Me.ColBatch, Me.ColName, Me.ColCost, Me.ColRate, Me.ColPack, Me.ColPiece, Me.ColBonus, Me.ColPercentage, Me.ColDisc_Rs, Me.ColSaleTax, Me.ColTotal, Me.ColScmItem, Me.ColScmQty, Me.ColSR, Me.ColPPP})
        DataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle60.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle60.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle60.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle60.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle60.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle60.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle60
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.HotTrack
        Me.DataGridView1.Location = New System.Drawing.Point(3, 42)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 15
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowTemplate.Height = 18
        Me.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.Size = New System.Drawing.Size(804, 158)
        Me.DataGridView1.TabIndex = 14
        '
        'ColCode
        '
        DataGridViewCellStyle50.Format = "N0"
        DataGridViewCellStyle50.NullValue = Nothing
        Me.ColCode.DefaultCellStyle = DataGridViewCellStyle50
        Me.ColCode.HeaderText = "Code"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.Width = 80
        '
        'ColBatch
        '
        Me.ColBatch.HeaderText = "Batch #"
        Me.ColBatch.Name = "ColBatch"
        Me.ColBatch.Width = 80
        '
        'ColName
        '
        Me.ColName.HeaderText = "Name"
        Me.ColName.Name = "ColName"
        Me.ColName.ReadOnly = True
        Me.ColName.Width = 160
        '
        'ColCost
        '
        Me.ColCost.HeaderText = "Cost"
        Me.ColCost.Name = "ColCost"
        Me.ColCost.Visible = False
        '
        'ColRate
        '
        DataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle51.Format = "C2"
        DataGridViewCellStyle51.NullValue = "0.00"
        Me.ColRate.DefaultCellStyle = DataGridViewCellStyle51
        Me.ColRate.HeaderText = "Rate"
        Me.ColRate.Name = "ColRate"
        Me.ColRate.Width = 60
        '
        'ColPack
        '
        DataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle52.Format = "N0"
        DataGridViewCellStyle52.NullValue = "0"
        Me.ColPack.DefaultCellStyle = DataGridViewCellStyle52
        Me.ColPack.HeaderText = "Pack(s)"
        Me.ColPack.Name = "ColPack"
        Me.ColPack.Width = 60
        '
        'ColPiece
        '
        DataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle53.Format = "N0"
        DataGridViewCellStyle53.NullValue = "0"
        Me.ColPiece.DefaultCellStyle = DataGridViewCellStyle53
        Me.ColPiece.HeaderText = "Pcs"
        Me.ColPiece.Name = "ColPiece"
        Me.ColPiece.Width = 40
        '
        'ColBonus
        '
        DataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle54.Format = "N0"
        DataGridViewCellStyle54.NullValue = "0"
        Me.ColBonus.DefaultCellStyle = DataGridViewCellStyle54
        Me.ColBonus.HeaderText = "Bonus (Pcs)"
        Me.ColBonus.Name = "ColBonus"
        Me.ColBonus.Width = 50
        '
        'ColPercentage
        '
        DataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle55.Format = "N0"
        DataGridViewCellStyle55.NullValue = "0"
        Me.ColPercentage.DefaultCellStyle = DataGridViewCellStyle55
        Me.ColPercentage.HeaderText = "%age"
        Me.ColPercentage.Name = "ColPercentage"
        Me.ColPercentage.Width = 40
        '
        'ColDisc_Rs
        '
        DataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle56.Format = "C2"
        DataGridViewCellStyle56.NullValue = "0.00"
        Me.ColDisc_Rs.DefaultCellStyle = DataGridViewCellStyle56
        Me.ColDisc_Rs.HeaderText = "Disc Rs"
        Me.ColDisc_Rs.Name = "ColDisc_Rs"
        Me.ColDisc_Rs.Width = 50
        '
        'ColSaleTax
        '
        DataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle57.Format = "N2"
        DataGridViewCellStyle57.NullValue = "0.00"
        Me.ColSaleTax.DefaultCellStyle = DataGridViewCellStyle57
        Me.ColSaleTax.HeaderText = "Sales Tax"
        Me.ColSaleTax.MaxInputLength = 3
        Me.ColSaleTax.Name = "ColSaleTax"
        Me.ColSaleTax.ReadOnly = True
        Me.ColSaleTax.Width = 45
        '
        'ColTotal
        '
        DataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle58.Format = "N2"
        DataGridViewCellStyle58.NullValue = "0.00"
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle58
        Me.ColTotal.HeaderText = "Total"
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.Width = 80
        '
        'ColScmItem
        '
        Me.ColScmItem.HeaderText = "Scm Item"
        Me.ColScmItem.Name = "ColScmItem"
        Me.ColScmItem.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColScmItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColScmItem.Visible = False
        Me.ColScmItem.Width = 80
        '
        'ColScmQty
        '
        DataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle59.Format = "N0"
        DataGridViewCellStyle59.NullValue = "0"
        Me.ColScmQty.DefaultCellStyle = DataGridViewCellStyle59
        Me.ColScmQty.HeaderText = "Scm Qty."
        Me.ColScmQty.Name = "ColScmQty"
        Me.ColScmQty.Visible = False
        Me.ColScmQty.Width = 50
        '
        'ColSR
        '
        Me.ColSR.HeaderText = "Sr. No"
        Me.ColSR.Name = "ColSR"
        Me.ColSR.ReadOnly = True
        Me.ColSR.Visible = False
        '
        'ColPPP
        '
        Me.ColPPP.HeaderText = "PPP"
        Me.ColPPP.Name = "ColPPP"
        Me.ColPPP.Visible = False
        '
        'LblScmItem
        '
        Me.LblScmItem.BackColor = System.Drawing.Color.Aquamarine
        Me.LblScmItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblScmItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblScmItem.ForeColor = System.Drawing.Color.Black
        Me.LblScmItem.Location = New System.Drawing.Point(750, 16)
        Me.LblScmItem.Name = "LblScmItem"
        Me.LblScmItem.Size = New System.Drawing.Size(51, 23)
        Me.LblScmItem.TabIndex = 13
        Me.LblScmItem.Text = "0"
        Me.LblScmItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblB_Pcs
        '
        Me.LblB_Pcs.BackColor = System.Drawing.Color.Aquamarine
        Me.LblB_Pcs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblB_Pcs.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM1, "V_LUP_ITEM.nBONUS_ON_PCS", True))
        Me.LblB_Pcs.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblB_Pcs.ForeColor = System.Drawing.Color.Black
        Me.LblB_Pcs.Location = New System.Drawing.Point(625, 16)
        Me.LblB_Pcs.Name = "LblB_Pcs"
        Me.LblB_Pcs.Size = New System.Drawing.Size(51, 23)
        Me.LblB_Pcs.TabIndex = 11
        Me.LblB_Pcs.Text = "0"
        Me.LblB_Pcs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DsLUP_ITEM1
        '
        Me.DsLUP_ITEM1.DataSetName = "dsLUP_ITEM"
        Me.DsLUP_ITEM1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LblRatePcs
        '
        Me.LblRatePcs.BackColor = System.Drawing.Color.Aquamarine
        Me.LblRatePcs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblRatePcs.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRatePcs.ForeColor = System.Drawing.Color.Black
        Me.LblRatePcs.Location = New System.Drawing.Point(387, 16)
        Me.LblRatePcs.Name = "LblRatePcs"
        Me.LblRatePcs.Size = New System.Drawing.Size(55, 23)
        Me.LblRatePcs.TabIndex = 7
        Me.LblRatePcs.Text = "0"
        Me.LblRatePcs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblPPP
        '
        Me.LblPPP.BackColor = System.Drawing.Color.Aquamarine
        Me.LblPPP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblPPP.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM1, "V_LUP_ITEM.nPPP", True))
        Me.LblPPP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPPP.ForeColor = System.Drawing.Color.Black
        Me.LblPPP.Location = New System.Drawing.Point(276, 16)
        Me.LblPPP.Name = "LblPPP"
        Me.LblPPP.Size = New System.Drawing.Size(46, 23)
        Me.LblPPP.TabIndex = 5
        Me.LblPPP.Text = "0"
        Me.LblPPP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblStock
        '
        Me.LblStock.BackColor = System.Drawing.Color.Aquamarine
        Me.LblStock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblStock.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSV_STOCK_BAL1, "SV_STOCK_BAL.STK_BAL", True))
        Me.LblStock.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStock.ForeColor = System.Drawing.Color.Black
        Me.LblStock.Location = New System.Drawing.Point(500, 16)
        Me.LblStock.Name = "LblStock"
        Me.LblStock.Size = New System.Drawing.Size(51, 23)
        Me.LblStock.TabIndex = 9
        Me.LblStock.Text = "0"
        Me.LblStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblRetail
        '
        Me.LblRetail.BackColor = System.Drawing.Color.Aquamarine
        Me.LblRetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblRetail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM1, "V_LUP_ITEM.UNIT_RETAIL", True))
        Me.LblRetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRetail.ForeColor = System.Drawing.Color.Black
        Me.LblRetail.Location = New System.Drawing.Point(170, 16)
        Me.LblRetail.Name = "LblRetail"
        Me.LblRetail.Size = New System.Drawing.Size(64, 23)
        Me.LblRetail.TabIndex = 3
        Me.LblRetail.Text = "0"
        Me.LblRetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblRate
        '
        Me.LblRate.BackColor = System.Drawing.Color.Aquamarine
        Me.LblRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblRate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM1, "V_LUP_ITEM.UNIT_RATE", True))
        Me.LblRate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRate.ForeColor = System.Drawing.Color.Black
        Me.LblRate.Location = New System.Drawing.Point(49, 16)
        Me.LblRate.Name = "LblRate"
        Me.LblRate.Size = New System.Drawing.Size(64, 23)
        Me.LblRate.TabIndex = 1
        Me.LblRate.Text = "0"
        Me.LblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.LblRate.Visible = False
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Aquamarine
        Me.Label26.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(682, 16)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(62, 23)
        Me.Label26.TabIndex = 12
        Me.Label26.Text = "Scm Stk."
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Aquamarine
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(557, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(62, 23)
        Me.Label24.TabIndex = 10
        Me.Label24.Text = "B. on Pcs"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Aquamarine
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(119, 16)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 23)
        Me.Label30.TabIndex = 2
        Me.Label30.Text = "Retail:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Aquamarine
        Me.Label25.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(328, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(56, 23)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "Rate Pcs"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Aquamarine
        Me.Label32.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(241, 16)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(30, 23)
        Me.Label32.TabIndex = 4
        Me.Label32.Text = "PPP"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Aquamarine
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(445, 16)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(49, 23)
        Me.Label28.TabIndex = 8
        Me.Label28.Text = "Stock:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Aquamarine
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(6, 16)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(39, 23)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Rate:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label21.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox4.BackColor = System.Drawing.Color.Tan
        Me.GroupBox4.Controls.Add(Me.BttnReceipt)
        Me.GroupBox4.Controls.Add(Me.TxtTotal)
        Me.GroupBox4.Controls.Add(Me.TxtDescription)
        Me.GroupBox4.Controls.Add(Me.TxtReceipt)
        Me.GroupBox4.Controls.Add(Me.TxtClientBal)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.TxtInvBalance)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.TxtNetTotal)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.TxtOtherDisc)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.TxtDiscPercent)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.TxtDiscount)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Location = New System.Drawing.Point(625, 359)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(195, 235)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        '
        'BttnReceipt
        '
        Me.BttnReceipt.Location = New System.Drawing.Point(6, 157)
        Me.BttnReceipt.Name = "BttnReceipt"
        Me.BttnReceipt.Size = New System.Drawing.Size(87, 23)
        Me.BttnReceipt.TabIndex = 11
        Me.BttnReceipt.TabStop = False
        Me.BttnReceipt.Text = "Rece&ipt"
        Me.BttnReceipt.UseVisualStyleBackColor = True
        '
        'TxtTotal
        '
        Me.TxtTotal.BackColor = System.Drawing.Color.White
        Me.TxtTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.TOT_BILL", True))
        Me.TxtTotal.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtTotal.Location = New System.Drawing.Point(99, 14)
        Me.TxtTotal.MaxLength = 50
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.ReadOnly = True
        Me.TxtTotal.Size = New System.Drawing.Size(87, 21)
        Me.TxtTotal.TabIndex = 1
        Me.TxtTotal.TabStop = False
        Me.TxtTotal.Text = "0.00"
        Me.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_SALE_MASTER1
        '
        Me.DsV_SALE_MASTER1.DataSetName = "dsV_SALE_MASTER"
        Me.DsV_SALE_MASTER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtDescription
        '
        Me.TxtDescription.BackColor = System.Drawing.Color.White
        Me.TxtDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.OTHER_DESC", True))
        Me.TxtDescription.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDescription.Location = New System.Drawing.Point(9, 110)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(177, 21)
        Me.TxtDescription.TabIndex = 8
        Me.TxtDescription.Text = "Other's Description Here!"
        '
        'TxtReceipt
        '
        Me.TxtReceipt.BackColor = System.Drawing.Color.White
        Me.TxtReceipt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_CLIENT_RECEIPT1, "V_CLIENT_RECEIPT.TOT_RECEIPT", True))
        Me.TxtReceipt.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtReceipt.Location = New System.Drawing.Point(99, 158)
        Me.TxtReceipt.MaxLength = 50
        Me.TxtReceipt.Name = "TxtReceipt"
        Me.TxtReceipt.ReadOnly = True
        Me.TxtReceipt.Size = New System.Drawing.Size(87, 21)
        Me.TxtReceipt.TabIndex = 12
        Me.TxtReceipt.TabStop = False
        Me.TxtReceipt.Text = "0.00"
        Me.TxtReceipt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_CLIENT_RECEIPT1
        '
        Me.DsV_CLIENT_RECEIPT1.DataSetName = "dsV_CLIENT_RECEIPT"
        Me.DsV_CLIENT_RECEIPT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtClientBal
        '
        Me.TxtClientBal.BackColor = System.Drawing.Color.White
        Me.TxtClientBal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtClientBal.ForeColor = System.Drawing.Color.Red
        Me.TxtClientBal.Location = New System.Drawing.Point(99, 206)
        Me.TxtClientBal.MaxLength = 50
        Me.TxtClientBal.Name = "TxtClientBal"
        Me.TxtClientBal.ReadOnly = True
        Me.TxtClientBal.Size = New System.Drawing.Size(87, 21)
        Me.TxtClientBal.TabIndex = 16
        Me.TxtClientBal.TabStop = False
        Me.TxtClientBal.Text = "0.00"
        Me.TxtClientBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Red
        Me.Label23.Location = New System.Drawing.Point(6, 206)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(87, 23)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "Client Bal"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtInvBalance
        '
        Me.TxtInvBalance.BackColor = System.Drawing.Color.White
        Me.TxtInvBalance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtInvBalance.ForeColor = System.Drawing.Color.DarkBlue
        Me.TxtInvBalance.Location = New System.Drawing.Point(99, 182)
        Me.TxtInvBalance.MaxLength = 50
        Me.TxtInvBalance.Name = "TxtInvBalance"
        Me.TxtInvBalance.ReadOnly = True
        Me.TxtInvBalance.Size = New System.Drawing.Size(87, 21)
        Me.TxtInvBalance.TabIndex = 14
        Me.TxtInvBalance.TabStop = False
        Me.TxtInvBalance.Text = "0.00"
        Me.TxtInvBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label22.Location = New System.Drawing.Point(6, 182)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(87, 23)
        Me.Label22.TabIndex = 13
        Me.Label22.Text = "Invoice Bal"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtNetTotal
        '
        Me.TxtNetTotal.BackColor = System.Drawing.Color.White
        Me.TxtNetTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.NET_TOTAL", True))
        Me.TxtNetTotal.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtNetTotal.Location = New System.Drawing.Point(99, 134)
        Me.TxtNetTotal.MaxLength = 50
        Me.TxtNetTotal.Name = "TxtNetTotal"
        Me.TxtNetTotal.ReadOnly = True
        Me.TxtNetTotal.Size = New System.Drawing.Size(87, 21)
        Me.TxtNetTotal.TabIndex = 10
        Me.TxtNetTotal.TabStop = False
        Me.TxtNetTotal.Text = "0.00"
        Me.TxtNetTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(6, 134)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(87, 23)
        Me.Label20.TabIndex = 9
        Me.Label20.Text = "Net Total"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtOtherDisc
        '
        Me.TxtOtherDisc.BackColor = System.Drawing.Color.White
        Me.TxtOtherDisc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.DISC_OTHER", True))
        Me.TxtOtherDisc.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtOtherDisc.Location = New System.Drawing.Point(99, 86)
        Me.TxtOtherDisc.MaxLength = 50
        Me.TxtOtherDisc.Name = "TxtOtherDisc"
        Me.TxtOtherDisc.Size = New System.Drawing.Size(87, 21)
        Me.TxtOtherDisc.TabIndex = 7
        Me.TxtOtherDisc.Text = "0.00"
        Me.TxtOtherDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(6, 86)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(87, 23)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "Other Disc."
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDiscPercent
        '
        Me.TxtDiscPercent.BackColor = System.Drawing.Color.White
        Me.TxtDiscPercent.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.DISC_PER", True))
        Me.TxtDiscPercent.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDiscPercent.Location = New System.Drawing.Point(99, 62)
        Me.TxtDiscPercent.MaxLength = 50
        Me.TxtDiscPercent.Name = "TxtDiscPercent"
        Me.TxtDiscPercent.Size = New System.Drawing.Size(87, 21)
        Me.TxtDiscPercent.TabIndex = 5
        Me.TxtDiscPercent.Text = "0"
        Me.TxtDiscPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 62)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(87, 23)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "Discount %"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDiscount
        '
        Me.TxtDiscount.BackColor = System.Drawing.Color.White
        Me.TxtDiscount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.DISC_RS", True))
        Me.TxtDiscount.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDiscount.Location = New System.Drawing.Point(99, 38)
        Me.TxtDiscount.MaxLength = 50
        Me.TxtDiscount.Name = "TxtDiscount"
        Me.TxtDiscount.Size = New System.Drawing.Size(87, 21)
        Me.TxtDiscount.TabIndex = 3
        Me.TxtDiscount.Text = "0.00"
        Me.TxtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(6, 38)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(87, 23)
        Me.Label17.TabIndex = 2
        Me.Label17.Text = "Discount Rs."
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 13)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(87, 23)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Total"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.GroupBox5.Controls.Add(Me.TxtTRno)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.TxtTRqty)
        Me.GroupBox5.Controls.Add(Me.TxtTotalItems)
        Me.GroupBox5.Controls.Add(Me.Label14)
        Me.GroupBox5.Controls.Add(Me.TxtFreight)
        Me.GroupBox5.Controls.Add(Me.Label13)
        Me.GroupBox5.Controls.Add(Me.TxtUnloading)
        Me.GroupBox5.Controls.Add(Me.Label15)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Location = New System.Drawing.Point(10, 360)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(609, 47)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        '
        'TxtTRno
        '
        Me.TxtTRno.BackColor = System.Drawing.Color.White
        Me.TxtTRno.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.TR_NO", True))
        Me.TxtTRno.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtTRno.Location = New System.Drawing.Point(224, 14)
        Me.TxtTRno.MaxLength = 50
        Me.TxtTRno.Name = "TxtTRno"
        Me.TxtTRno.Size = New System.Drawing.Size(73, 21)
        Me.TxtTRno.TabIndex = 3
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(7, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 21)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Tot. Items"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtTRqty
        '
        Me.TxtTRqty.BackColor = System.Drawing.Color.White
        Me.TxtTRqty.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.TR_QTY", True))
        Me.TxtTRqty.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtTRqty.Location = New System.Drawing.Point(306, 14)
        Me.TxtTRqty.MaxLength = 50
        Me.TxtTRqty.Name = "TxtTRqty"
        Me.TxtTRqty.Size = New System.Drawing.Size(40, 21)
        Me.TxtTRqty.TabIndex = 5
        '
        'TxtTotalItems
        '
        Me.TxtTotalItems.BackColor = System.Drawing.Color.White
        Me.TxtTotalItems.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtTotalItems.Location = New System.Drawing.Point(80, 14)
        Me.TxtTotalItems.MaxLength = 50
        Me.TxtTotalItems.Name = "TxtTotalItems"
        Me.TxtTotalItems.ReadOnly = True
        Me.TxtTotalItems.Size = New System.Drawing.Size(54, 21)
        Me.TxtTotalItems.TabIndex = 1
        Me.TxtTotalItems.TabStop = False
        Me.TxtTotalItems.Text = "0"
        Me.TxtTotalItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(472, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 21)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "Unloading"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtFreight
        '
        Me.TxtFreight.BackColor = System.Drawing.Color.White
        Me.TxtFreight.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.FREIGHT", True))
        Me.TxtFreight.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtFreight.Location = New System.Drawing.Point(407, 14)
        Me.TxtFreight.MaxLength = 50
        Me.TxtFreight.Name = "TxtFreight"
        Me.TxtFreight.Size = New System.Drawing.Size(59, 21)
        Me.TxtFreight.TabIndex = 7
        Me.TxtFreight.Text = "0.00"
        Me.TxtFreight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(352, 14)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 21)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "Freight"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtUnloading
        '
        Me.TxtUnloading.BackColor = System.Drawing.Color.White
        Me.TxtUnloading.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.UNLOADING", True))
        Me.TxtUnloading.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtUnloading.Location = New System.Drawing.Point(543, 14)
        Me.TxtUnloading.MaxLength = 50
        Me.TxtUnloading.Name = "TxtUnloading"
        Me.TxtUnloading.Size = New System.Drawing.Size(59, 21)
        Me.TxtUnloading.TabIndex = 9
        Me.TxtUnloading.Text = "0.00"
        Me.TxtUnloading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(298, 14)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(10, 21)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "/"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(141, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(78, 21)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "TR # / Qty."
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox6.Controls.Add(Me.TxtRemarks)
        Me.GroupBox6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(375, 413)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(244, 181)
        Me.GroupBox6.TabIndex = 7
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.REMARKS", True))
        Me.TxtRemarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtRemarks.Location = New System.Drawing.Point(3, 19)
        Me.TxtRemarks.MaxLength = 100
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(238, 159)
        Me.TxtRemarks.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.BttnSearch_Inv)
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnPrev)
        Me.GroupBox1.Controls.Add(Me.BttnPrint)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Controls.Add(Me.BttnSearch_Item)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 413)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(362, 181)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'BttnSearch_Inv
        '
        Me.BttnSearch_Inv.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BttnSearch_Inv.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch_Inv.Location = New System.Drawing.Point(144, 120)
        Me.BttnSearch_Inv.Name = "BttnSearch_Inv"
        Me.BttnSearch_Inv.Size = New System.Drawing.Size(75, 42)
        Me.BttnSearch_Inv.TabIndex = 19
        Me.BttnSearch_Inv.Text = "Sea&rch Invoice"
        Me.BttnSearch_Inv.UseVisualStyleBackColor = False
        '
        'BttnNew
        '
        Me.BttnNew.BackColor = System.Drawing.Color.LightGreen
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(59, 70)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(75, 31)
        Me.BttnNew.TabIndex = 16
        Me.BttnNew.Text = "&New"
        Me.BttnNew.UseVisualStyleBackColor = False
        '
        'BttnPrev
        '
        Me.BttnPrev.BackColor = System.Drawing.Color.PowderBlue
        Me.BttnPrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrev.Enabled = False
        Me.BttnPrev.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrev.Location = New System.Drawing.Point(196, 19)
        Me.BttnPrev.Name = "BttnPrev"
        Me.BttnPrev.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrev.TabIndex = 17
        Me.BttnPrev.Text = "Pre&view"
        Me.BttnPrev.UseVisualStyleBackColor = False
        '
        'BttnPrint
        '
        Me.BttnPrint.BackColor = System.Drawing.Color.PowderBlue
        Me.BttnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrint.Enabled = False
        Me.BttnPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrint.Location = New System.Drawing.Point(98, 19)
        Me.BttnPrint.Name = "BttnPrint"
        Me.BttnPrint.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrint.TabIndex = 18
        Me.BttnPrint.Text = "&Print"
        Me.BttnPrint.UseVisualStyleBackColor = False
        '
        'BttnClose
        '
        Me.BttnClose.BackColor = System.Drawing.Color.LightGreen
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(230, 70)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 31)
        Me.BttnClose.TabIndex = 20
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = False
        '
        'BttnSave
        '
        Me.BttnSave.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.BttnSave.Enabled = False
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(134, 51)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(95, 68)
        Me.BttnSave.TabIndex = 15
        Me.BttnSave.Text = "&Save"
        Me.BttnSave.UseVisualStyleBackColor = False
        '
        'BttnSearch_Item
        '
        Me.BttnSearch_Item.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BttnSearch_Item.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch_Item.Location = New System.Drawing.Point(10, 125)
        Me.BttnSearch_Item.Name = "BttnSearch_Item"
        Me.BttnSearch_Item.Size = New System.Drawing.Size(75, 42)
        Me.BttnSearch_Item.TabIndex = 4
        Me.BttnSearch_Item.Text = "Sea&rch Item"
        Me.BttnSearch_Item.UseVisualStyleBackColor = False
        Me.BttnSearch_Item.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.BackColor = System.Drawing.Color.Pink
        Me.GroupBox3.Controls.Add(Me.BttnAdd)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.TxtCashClient)
        Me.GroupBox3.Controls.Add(Me.TxtInvoice)
        Me.GroupBox3.Controls.Add(Me.CmbClient)
        Me.GroupBox3.Controls.Add(Me.CmbS_Man)
        Me.GroupBox3.Controls.Add(Me.CmbD_Man)
        Me.GroupBox3.Controls.Add(Me.CmbGroup)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label31)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtNET_Receivable)
        Me.GroupBox3.Controls.Add(Me.TxtCashMemo)
        Me.GroupBox3.Controls.Add(Me.TxtStandyBy)
        Me.GroupBox3.Controls.Add(Me.TxtReceivables)
        Me.GroupBox3.Controls.Add(Me.TxtDispDate)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(10, 44)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(810, 105)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'BttnAdd
        '
        Me.BttnAdd.BackColor = System.Drawing.SystemColors.Control
        Me.BttnAdd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnAdd.Location = New System.Drawing.Point(178, 17)
        Me.BttnAdd.Name = "BttnAdd"
        Me.BttnAdd.Size = New System.Drawing.Size(33, 23)
        Me.BttnAdd.TabIndex = 2
        Me.BttnAdd.TabStop = False
        Me.BttnAdd.Text = "+&1"
        Me.BttnAdd.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(117, 21)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Cash Client Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 21)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Invoice #"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCashClient
        '
        Me.TxtCashClient.BackColor = System.Drawing.Color.White
        Me.TxtCashClient.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.CASH_CLIENT", True))
        Me.TxtCashClient.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtCashClient.Location = New System.Drawing.Point(129, 72)
        Me.TxtCashClient.MaxLength = 50
        Me.TxtCashClient.Name = "TxtCashClient"
        Me.TxtCashClient.Size = New System.Drawing.Size(119, 21)
        Me.TxtCashClient.TabIndex = 18
        '
        'TxtInvoice
        '
        Me.TxtInvoice.BackColor = System.Drawing.Color.White
        Me.TxtInvoice.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtInvoice.Location = New System.Drawing.Point(83, 18)
        Me.TxtInvoice.MaxLength = 50
        Me.TxtInvoice.Name = "TxtInvoice"
        Me.TxtInvoice.ReadOnly = True
        Me.TxtInvoice.Size = New System.Drawing.Size(89, 21)
        Me.TxtInvoice.TabIndex = 1
        Me.TxtInvoice.TabStop = False
        '
        'CmbClient
        '
        Me.CmbClient.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbClient.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbClient.ColumnNum = 3
        Me.CmbClient.ColumnWidth = "140;140;40"
        Me.CmbClient.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.SHOP_NAME", True))
        Me.CmbClient.DisplayMember = "Text"
        Me.CmbClient.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbClient.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbClient.DropDownForeColor = System.Drawing.Color.White
        Me.CmbClient.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbClient.DropDownWidth = 340
        Me.CmbClient.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbClient.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbClient.GridLineHorizontal = False
        Me.CmbClient.GridLineVertical = True
        Me.CmbClient.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbClient.Location = New System.Drawing.Point(83, 44)
        Me.CmbClient.ManagingFastMouseMoving = True
        Me.CmbClient.ManagingFastMouseMovingInterval = 30
        Me.CmbClient.Name = "CmbClient"
        Me.CmbClient.Size = New System.Drawing.Size(165, 22)
        Me.CmbClient.TabIndex = 10
        '
        'CmbS_Man
        '
        Me.CmbS_Man.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbS_Man.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbS_Man.ColumnNum = 3
        Me.CmbS_Man.ColumnWidth = "100;100;30"
        Me.CmbS_Man.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.EMP_NAME", True))
        Me.CmbS_Man.DisplayMember = "Text"
        Me.CmbS_Man.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbS_Man.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbS_Man.DropDownForeColor = System.Drawing.Color.White
        Me.CmbS_Man.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbS_Man.DropDownWidth = 340
        Me.CmbS_Man.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbS_Man.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbS_Man.GridLineHorizontal = False
        Me.CmbS_Man.GridLineVertical = True
        Me.CmbS_Man.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbS_Man.Location = New System.Drawing.Point(687, 17)
        Me.CmbS_Man.ManagingFastMouseMoving = True
        Me.CmbS_Man.ManagingFastMouseMovingInterval = 30
        Me.CmbS_Man.Name = "CmbS_Man"
        Me.CmbS_Man.Size = New System.Drawing.Size(114, 22)
        Me.CmbS_Man.TabIndex = 8
        '
        'CmbD_Man
        '
        Me.CmbD_Man.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbD_Man.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbD_Man.ColumnNum = 3
        Me.CmbD_Man.ColumnWidth = "100;100;30"
        Me.CmbD_Man.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.D_MAN", True))
        Me.CmbD_Man.DisplayMember = "Text"
        Me.CmbD_Man.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbD_Man.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbD_Man.DropDownForeColor = System.Drawing.Color.White
        Me.CmbD_Man.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbD_Man.DropDownWidth = 340
        Me.CmbD_Man.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbD_Man.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbD_Man.GridLineHorizontal = False
        Me.CmbD_Man.GridLineVertical = True
        Me.CmbD_Man.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbD_Man.Location = New System.Drawing.Point(687, 44)
        Me.CmbD_Man.ManagingFastMouseMoving = True
        Me.CmbD_Man.ManagingFastMouseMovingInterval = 30
        Me.CmbD_Man.Name = "CmbD_Man"
        Me.CmbD_Man.Size = New System.Drawing.Size(114, 22)
        Me.CmbD_Man.TabIndex = 16
        '
        'CmbGroup
        '
        Me.CmbGroup.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup.ColumnNum = 3
        Me.CmbGroup.ColumnWidth = "100;100;30"
        Me.CmbGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.GROUP_NAME", True))
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
        Me.CmbGroup.Location = New System.Drawing.Point(496, 17)
        Me.CmbGroup.ManagingFastMouseMoving = True
        Me.CmbGroup.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup.Name = "CmbGroup"
        Me.CmbGroup.Size = New System.Drawing.Size(123, 22)
        Me.CmbGroup.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(426, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 21)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Disp. Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label29.Location = New System.Drawing.Point(619, 72)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(64, 21)
        Me.Label29.TabIndex = 23
        Me.Label29.Text = "Balance"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Black
        Me.Label27.Location = New System.Drawing.Point(426, 72)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(95, 21)
        Me.Label27.TabIndex = 21
        Me.Label27.Text = "Standby Chqs"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(619, 17)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(64, 23)
        Me.Label33.TabIndex = 7
        Me.Label33.Text = "S. Man"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(251, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 21)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Prev. Bal"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(619, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 22)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "D. Man"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 22)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Client"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(227, 17)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(95, 23)
        Me.Label31.TabIndex = 3
        Me.Label31.Text = "Cash Memo #"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(426, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 23)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "B. Group"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(251, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 21)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtNET_Receivable
        '
        Me.TxtNET_Receivable.BackColor = System.Drawing.Color.White
        Me.TxtNET_Receivable.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_CLIENT_BAL1, "SV_CLIENT_BALANCE.CLIENT_BAL", True))
        Me.TxtNET_Receivable.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtNET_Receivable.ForeColor = System.Drawing.Color.Red
        Me.TxtNET_Receivable.Location = New System.Drawing.Point(687, 72)
        Me.TxtNET_Receivable.MaxLength = 50
        Me.TxtNET_Receivable.Name = "TxtNET_Receivable"
        Me.TxtNET_Receivable.ReadOnly = True
        Me.TxtNET_Receivable.Size = New System.Drawing.Size(114, 21)
        Me.TxtNET_Receivable.TabIndex = 24
        Me.TxtNET_Receivable.TabStop = False
        Me.TxtNET_Receivable.Text = "0"
        Me.TxtNET_Receivable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_CLIENT_BAL1
        '
        Me.DsV_CLIENT_BAL1.DataSetName = "dsV_CLIENT_BAL"
        Me.DsV_CLIENT_BAL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtCashMemo
        '
        Me.TxtCashMemo.BackColor = System.Drawing.Color.White
        Me.TxtCashMemo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.CASH_MEMO", True))
        Me.TxtCashMemo.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtCashMemo.Location = New System.Drawing.Point(328, 18)
        Me.TxtCashMemo.MaxLength = 50
        Me.TxtCashMemo.Name = "TxtCashMemo"
        Me.TxtCashMemo.Size = New System.Drawing.Size(92, 21)
        Me.TxtCashMemo.TabIndex = 4
        '
        'TxtStandyBy
        '
        Me.TxtStandyBy.BackColor = System.Drawing.Color.White
        Me.TxtStandyBy.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_CLIENT_STANDBY_CHEQ1, "V_CLIENT_TOT_STANDBY_CHEQ.TOT_STANDBY_CHEQ", True))
        Me.TxtStandyBy.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtStandyBy.ForeColor = System.Drawing.Color.Green
        Me.TxtStandyBy.Location = New System.Drawing.Point(527, 72)
        Me.TxtStandyBy.MaxLength = 50
        Me.TxtStandyBy.Name = "TxtStandyBy"
        Me.TxtStandyBy.ReadOnly = True
        Me.TxtStandyBy.Size = New System.Drawing.Size(92, 21)
        Me.TxtStandyBy.TabIndex = 22
        Me.TxtStandyBy.TabStop = False
        Me.TxtStandyBy.Text = "0"
        Me.TxtStandyBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_CLIENT_STANDBY_CHEQ1
        '
        Me.DsV_CLIENT_STANDBY_CHEQ1.DataSetName = "dsV_CLIENT_STANDBY_CHEQ"
        Me.DsV_CLIENT_STANDBY_CHEQ1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtReceivables
        '
        Me.TxtReceivables.BackColor = System.Drawing.Color.White
        Me.TxtReceivables.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtReceivables.ForeColor = System.Drawing.Color.DarkBlue
        Me.TxtReceivables.Location = New System.Drawing.Point(328, 72)
        Me.TxtReceivables.MaxLength = 50
        Me.TxtReceivables.Name = "TxtReceivables"
        Me.TxtReceivables.ReadOnly = True
        Me.TxtReceivables.Size = New System.Drawing.Size(92, 21)
        Me.TxtReceivables.TabIndex = 20
        Me.TxtReceivables.TabStop = False
        Me.TxtReceivables.Text = "0"
        Me.TxtReceivables.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtDispDate
        '
        Me.TxtDispDate.BackColor = System.Drawing.Color.White
        Me.TxtDispDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.DISP_DATE", True))
        Me.TxtDispDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDispDate.Location = New System.Drawing.Point(527, 45)
        Me.TxtDispDate.MaxLength = 50
        Me.TxtDispDate.Name = "TxtDispDate"
        Me.TxtDispDate.Size = New System.Drawing.Size(92, 21)
        Me.TxtDispDate.TabIndex = 14
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.S_DATE", True))
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDate.Location = New System.Drawing.Point(328, 45)
        Me.TxtDate.MaxLength = 50
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(92, 21)
        Me.TxtDate.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(128, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(93, 22)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Vehicle"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label10.Visible = False
        '
        'TxtVehicle
        '
        Me.TxtVehicle.BackColor = System.Drawing.Color.White
        Me.TxtVehicle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.VEHICLE", True))
        Me.TxtVehicle.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtVehicle.Location = New System.Drawing.Point(222, 10)
        Me.TxtVehicle.MaxLength = 50
        Me.TxtVehicle.Name = "TxtVehicle"
        Me.TxtVehicle.Size = New System.Drawing.Size(81, 21)
        Me.TxtVehicle.TabIndex = 12
        Me.TxtVehicle.Visible = False
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
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
        'daLUP_EMPLOYEE
        '
        Me.daLUP_EMPLOYEE.DeleteCommand = Me.SqlCommand1
        Me.daLUP_EMPLOYEE.SelectCommand = Me.SqlCommand2
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
        'SqlCommand2
        '
        Me.SqlCommand2.CommandText = resources.GetString("SqlCommand2.CommandText")
        Me.SqlCommand2.Connection = Me.SqlConnection1
        '
        'SqlCommand4
        '
        Me.SqlCommand4.CommandText = resources.GetString("SqlCommand4.CommandText")
        Me.SqlCommand4.Connection = Me.SqlConnection1
        Me.SqlCommand4.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'daLUP_ITEM
        '
        Me.daLUP_ITEM.SelectCommand = Me.SqlSelectCommand2
        Me.daLUP_ITEM.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_ITEM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sITEM_NAME", "sITEM_NAME"), New System.Data.Common.DataColumnMapping("sNICK", "sNICK"), New System.Data.Common.DataColumnMapping("nPPP", "nPPP"), New System.Data.Common.DataColumnMapping("sPACK_DESC", "sPACK_DESC"), New System.Data.Common.DataColumnMapping("sPIECE_DESC", "sPIECE_DESC"), New System.Data.Common.DataColumnMapping("UNIT_COST", "UNIT_COST"), New System.Data.Common.DataColumnMapping("UNIT_RATE", "UNIT_RATE"), New System.Data.Common.DataColumnMapping("UNIT_RETAIL", "UNIT_RETAIL"), New System.Data.Common.DataColumnMapping("nMIN_STOCK", "nMIN_STOCK"), New System.Data.Common.DataColumnMapping("nMAX_STOCK", "nMAX_STOCK"), New System.Data.Common.DataColumnMapping("nSALE_TAX", "nSALE_TAX"), New System.Data.Common.DataColumnMapping("VENDOR", "VENDOR"), New System.Data.Common.DataColumnMapping("nBONUS_QTY", "nBONUS_QTY"), New System.Data.Common.DataColumnMapping("nBONUS_ON_PCS", "nBONUS_ON_PCS"), New System.Data.Common.DataColumnMapping("CLAIMABLE", "CLAIMABLE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("nOPEN_STOCK", "nOPEN_STOCK"), New System.Data.Common.DataColumnMapping("OPEN_UNIT_VALUE", "OPEN_UNIT_VALUE")})})
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = resources.GetString("SqlSelectCommand2.CommandText")
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "SELECT     CLIENT_ID, CLIENT_BAL, GROUP_ID" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         SV_CLIENT_BALANCE"
        Me.SqlSelectCommand3.Connection = Me.SqlConnection1
        '
        'daV_CLIENT_BAL
        '
        Me.daV_CLIENT_BAL.SelectCommand = Me.SqlSelectCommand3
        Me.daV_CLIENT_BAL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SV_CLIENT_BALANCE", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CLIENT_ID", "CLIENT_ID"), New System.Data.Common.DataColumnMapping("CLIENT_BAL", "CLIENT_BAL"), New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID")})})
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = "SELECT     nCODE AS CODE, STK_BAL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         SV_STOCK_BAL"
        Me.SqlSelectCommand4.Connection = Me.SqlConnection2
        '
        'SqlConnection2
        '
        Me.SqlConnection2.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection2.FireInfoMessageEventOnUserErrors = False
        '
        'daSV_STOCK_BAL
        '
        Me.daSV_STOCK_BAL.SelectCommand = Me.SqlSelectCommand4
        Me.daSV_STOCK_BAL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SV_STOCK_BAL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("STK_BAL", "STK_BAL")})})
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(831, 43)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Sale Invoice"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlSelectCommand5
        '
        Me.SqlSelectCommand5.CommandText = resources.GetString("SqlSelectCommand5.CommandText")
        Me.SqlSelectCommand5.Connection = Me.SqlConnection1
        '
        'daV_SALE_MASTER
        '
        Me.daV_SALE_MASTER.SelectCommand = Me.SqlSelectCommand5
        Me.daV_SALE_MASTER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_SALE_MASTER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SINV_NO", "SINV_NO"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("CASH_CLIENT", "CASH_CLIENT"), New System.Data.Common.DataColumnMapping("CASH_MEMO", "CASH_MEMO"), New System.Data.Common.DataColumnMapping("LPINV_NO", "LPINV_NO"), New System.Data.Common.DataColumnMapping("S_DATE", "S_DATE"), New System.Data.Common.DataColumnMapping("DISP_DATE", "DISP_DATE"), New System.Data.Common.DataColumnMapping("VEHICLE", "VEHICLE"), New System.Data.Common.DataColumnMapping("FREIGHT", "FREIGHT"), New System.Data.Common.DataColumnMapping("UNLOADING", "UNLOADING"), New System.Data.Common.DataColumnMapping("TR_NO", "TR_NO"), New System.Data.Common.DataColumnMapping("TR_QTY", "TR_QTY"), New System.Data.Common.DataColumnMapping("TOT_BILL", "TOT_BILL"), New System.Data.Common.DataColumnMapping("DISC_RS", "DISC_RS"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("DISC_OTHER", "DISC_OTHER"), New System.Data.Common.DataColumnMapping("OTHER_DESC", "OTHER_DESC"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL"), New System.Data.Common.DataColumnMapping("EMP_NAME", "EMP_NAME"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("REMARKS", "REMARKS"), New System.Data.Common.DataColumnMapping("D_MAN", "D_MAN")})})
        '
        'SqlSelectCommand7
        '
        Me.SqlSelectCommand7.CommandText = resources.GetString("SqlSelectCommand7.CommandText")
        Me.SqlSelectCommand7.Connection = Me.SqlConnection1
        '
        'daV_SALE_DETAIL
        '
        Me.daV_SALE_DETAIL.SelectCommand = Me.SqlSelectCommand7
        Me.daV_SALE_DETAIL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_SALE_DETAIL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("SINV_NO", "SINV_NO"), New System.Data.Common.DataColumnMapping("ITEM_CODE", "ITEM_CODE"), New System.Data.Common.DataColumnMapping("ITEM_NAME", "ITEM_NAME"), New System.Data.Common.DataColumnMapping("BATCH_NO", "BATCH_NO"), New System.Data.Common.DataColumnMapping("UNIT_COST", "UNIT_COST"), New System.Data.Common.DataColumnMapping("UNIT_RATE", "UNIT_RATE"), New System.Data.Common.DataColumnMapping("DISC_RS", "DISC_RS"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("PPP", "PPP"), New System.Data.Common.DataColumnMapping("QTY_PKS", "QTY_PKS"), New System.Data.Common.DataColumnMapping("QTY_PCS", "QTY_PCS"), New System.Data.Common.DataColumnMapping("QTY_BONUS", "QTY_BONUS"), New System.Data.Common.DataColumnMapping("QTY_TOT_PCS", "QTY_TOT_PCS"), New System.Data.Common.DataColumnMapping("TOTAL_VALUE", "TOTAL_VALUE"), New System.Data.Common.DataColumnMapping("SCM_ITEM_CODE", "SCM_ITEM_CODE"), New System.Data.Common.DataColumnMapping("SCM_ITEM", "SCM_ITEM"), New System.Data.Common.DataColumnMapping("SCM_QTY", "SCM_QTY"), New System.Data.Common.DataColumnMapping("SALE_TAX", "SALE_TAX")})})
        '
        'SqlSelectCommand6
        '
        Me.SqlSelectCommand6.CommandText = resources.GetString("SqlSelectCommand6.CommandText")
        Me.SqlSelectCommand6.Connection = Me.SqlConnection1
        '
        'daV_CLIENT_RECEIPT
        '
        Me.daV_CLIENT_RECEIPT.SelectCommand = Me.SqlSelectCommand6
        Me.daV_CLIENT_RECEIPT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_CLIENT_RECEIPT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("CLIENT_ID", "CLIENT_ID"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("R_DATE", "R_DATE"), New System.Data.Common.DataColumnMapping("CASH_AMT", "CASH_AMT"), New System.Data.Common.DataColumnMapping("CHEQ_NO", "CHEQ_NO"), New System.Data.Common.DataColumnMapping("CHEQ_TYPE", "CHEQ_TYPE"), New System.Data.Common.DataColumnMapping("CHEQ_DATE", "CHEQ_DATE"), New System.Data.Common.DataColumnMapping("BANK_AMT", "BANK_AMT"), New System.Data.Common.DataColumnMapping("SINV_NO", "SINV_NO"), New System.Data.Common.DataColumnMapping("CHEQ_STATUS", "CHEQ_STATUS"), New System.Data.Common.DataColumnMapping("STATUS_DATE", "STATUS_DATE"), New System.Data.Common.DataColumnMapping("STATUS_DESC", "STATUS_DESC"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("BANK_NAME", "BANK_NAME"), New System.Data.Common.DataColumnMapping("EMP_NAME", "EMP_NAME"), New System.Data.Common.DataColumnMapping("USER_NAME", "USER_NAME"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION"), New System.Data.Common.DataColumnMapping("TOT_RECEIPT", "TOT_RECEIPT")})})
        '
        'daCLIENT_INFO
        '
        Me.daCLIENT_INFO.SelectCommand = Me.SqlCommand5
        Me.daCLIENT_INFO.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_CLIENT_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("SHOP_ADD", "SHOP_ADD"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("HOME_ADD", "HOME_ADD"), New System.Data.Common.DataColumnMapping("SHOP_PH", "SHOP_PH"), New System.Data.Common.DataColumnMapping("HOME_PH", "HOME_PH"), New System.Data.Common.DataColumnMapping("CELL_NO", "CELL_NO"), New System.Data.Common.DataColumnMapping("FAX_NO", "FAX_NO"), New System.Data.Common.DataColumnMapping("E_MAIL", "E_MAIL"), New System.Data.Common.DataColumnMapping("WEB_SITE", "WEB_SITE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("CLIENT_CAT", "CLIENT_CAT"), New System.Data.Common.DataColumnMapping("CLIENT_GD", "CLIENT_GD"), New System.Data.Common.DataColumnMapping("CLIENT_TYPE", "CLIENT_TYPE"), New System.Data.Common.DataColumnMapping("CREDIT_LIM", "CREDIT_LIM"), New System.Data.Common.DataColumnMapping("GST_NO", "GST_NO"), New System.Data.Common.DataColumnMapping("OPEN_BAL", "OPEN_BAL"), New System.Data.Common.DataColumnMapping("VISIT_TYPE", "VISIT_TYPE"), New System.Data.Common.DataColumnMapping("NO_VISIT", "NO_VISIT"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE")})})
        '
        'SqlCommand5
        '
        Me.SqlCommand5.CommandText = resources.GetString("SqlCommand5.CommandText")
        Me.SqlCommand5.Connection = Me.SqlConnection1
        '
        'daV_CLIENT_STANDBY_CHEQ
        '
        Me.daV_CLIENT_STANDBY_CHEQ.SelectCommand = Me.SqlCommand6
        Me.daV_CLIENT_STANDBY_CHEQ.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_CLIENT_TOT_STANDBY_CHEQ", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CLIENT_ID", "CLIENT_ID"), New System.Data.Common.DataColumnMapping("TOT_STANDBY_CHEQ", "TOT_STANDBY_CHEQ"), New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID")})})
        '
        'SqlCommand6
        '
        Me.SqlCommand6.CommandText = "SELECT     CLIENT_ID, TOT_STANDBY_CHEQ, GROUP_ID" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_CLIENT_TOT_STAND" & _
            "BY_CHEQ"
        Me.SqlCommand6.Connection = Me.SqlConnection1
        '
        'LblLoadPass
        '
        Me.LblLoadPass.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblLoadPass.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.LblLoadPass.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE_MASTER1, "V_SALE_MASTER.LPINV_NO", True))
        Me.LblLoadPass.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLoadPass.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.LblLoadPass.Location = New System.Drawing.Point(715, 15)
        Me.LblLoadPass.Name = "LblLoadPass"
        Me.LblLoadPass.Size = New System.Drawing.Size(93, 22)
        Me.LblLoadPass.TabIndex = 2
        Me.LblLoadPass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label34.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Label34.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label34.Location = New System.Drawing.Point(622, 15)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(87, 22)
        Me.Label34.TabIndex = 1
        Me.Label34.Text = "Load Pass #"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItemsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(831, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'ItemsToolStripMenuItem
        '
        Me.ItemsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectItemToolStripMenuItem})
        Me.ItemsToolStripMenuItem.Name = "ItemsToolStripMenuItem"
        Me.ItemsToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ItemsToolStripMenuItem.Text = "Items"
        Me.ItemsToolStripMenuItem.Visible = False
        '
        'SelectItemToolStripMenuItem
        '
        Me.SelectItemToolStripMenuItem.Name = "SelectItemToolStripMenuItem"
        Me.SelectItemToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.SelectItemToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.SelectItemToolStripMenuItem.Text = "Select Item"
        Me.SelectItemToolStripMenuItem.Visible = False
        '
        'daNS_DEFAULT
        '
        Me.daNS_DEFAULT.DeleteCommand = Me.SqlDeleteCommand1
        Me.daNS_DEFAULT.InsertCommand = Me.SqlInsertCommand1
        Me.daNS_DEFAULT.SelectCommand = Me.SqlSelectCommand1
        Me.daNS_DEFAULT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "NS_DEFAULT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("GROUP", "GROUP"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("S_MAN", "S_MAN"), New System.Data.Common.DataColumnMapping("P_MAN", "P_MAN"), New System.Data.Common.DataColumnMapping("D_MAN", "D_MAN"), New System.Data.Common.DataColumnMapping("R_MAN", "R_MAN"), New System.Data.Common.DataColumnMapping("CLIENT", "CLIENT"), New System.Data.Common.DataColumnMapping("CLIENT_TYPE", "CLIENT_TYPE"), New System.Data.Common.DataColumnMapping("CLIENT_CAT", "CLIENT_CAT"), New System.Data.Common.DataColumnMapping("CLIENT_GD", "CLIENT_GD"), New System.Data.Common.DataColumnMapping("ZONE", "ZONE"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("EXP_SUB_HEAD", "EXP_SUB_HEAD"), New System.Data.Common.DataColumnMapping("PRINTER", "PRINTER"), New System.Data.Common.DataColumnMapping("RPT_TITLE", "RPT_TITLE"), New System.Data.Common.DataColumnMapping("RPT_WARRANTY", "RPT_WARRANTY")})})
        Me.daNS_DEFAULT.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = resources.GetString("SqlDeleteCommand1.CommandText")
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_GROUP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_GROUP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_BANK_ACC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_BANK_ACC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_S_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_S_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_P_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_P_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_D_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_D_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_R_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_R_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_CAT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_GD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_GD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ZONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ZONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ROUTE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ROUTE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_AREA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_AREA", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_EXP_SUB_HEAD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_PRINTER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_PRINTER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_TITLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_TITLE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_WARRANTY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = resources.GetString("SqlInsertCommand1.CommandText")
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@GROUP", System.Data.SqlDbType.VarChar, 0, "GROUP"), New System.Data.SqlClient.SqlParameter("@BANK_ACC", System.Data.SqlDbType.VarChar, 0, "BANK_ACC"), New System.Data.SqlClient.SqlParameter("@S_MAN", System.Data.SqlDbType.VarChar, 0, "S_MAN"), New System.Data.SqlClient.SqlParameter("@P_MAN", System.Data.SqlDbType.VarChar, 0, "P_MAN"), New System.Data.SqlClient.SqlParameter("@D_MAN", System.Data.SqlDbType.VarChar, 0, "D_MAN"), New System.Data.SqlClient.SqlParameter("@R_MAN", System.Data.SqlDbType.VarChar, 0, "R_MAN"), New System.Data.SqlClient.SqlParameter("@CLIENT", System.Data.SqlDbType.VarChar, 0, "CLIENT"), New System.Data.SqlClient.SqlParameter("@CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, "CLIENT_TYPE"), New System.Data.SqlClient.SqlParameter("@CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, "CLIENT_CAT"), New System.Data.SqlClient.SqlParameter("@CLIENT_GD", System.Data.SqlDbType.VarChar, 0, "CLIENT_GD"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 0, "ZONE"), New System.Data.SqlClient.SqlParameter("@ROUTE", System.Data.SqlDbType.VarChar, 0, "ROUTE"), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 0, "AREA"), New System.Data.SqlClient.SqlParameter("@EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, "EXP_SUB_HEAD"), New System.Data.SqlClient.SqlParameter("@PRINTER", System.Data.SqlDbType.VarChar, 0, "PRINTER"), New System.Data.SqlClient.SqlParameter("@RPT_TITLE", System.Data.SqlDbType.VarChar, 0, "RPT_TITLE"), New System.Data.SqlClient.SqlParameter("@RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, "RPT_WARRANTY"), New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing)})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = resources.GetString("SqlSelectCommand1.CommandText")
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@GROUP", System.Data.SqlDbType.VarChar, 0, "GROUP"), New System.Data.SqlClient.SqlParameter("@BANK_ACC", System.Data.SqlDbType.VarChar, 0, "BANK_ACC"), New System.Data.SqlClient.SqlParameter("@S_MAN", System.Data.SqlDbType.VarChar, 0, "S_MAN"), New System.Data.SqlClient.SqlParameter("@P_MAN", System.Data.SqlDbType.VarChar, 0, "P_MAN"), New System.Data.SqlClient.SqlParameter("@D_MAN", System.Data.SqlDbType.VarChar, 0, "D_MAN"), New System.Data.SqlClient.SqlParameter("@R_MAN", System.Data.SqlDbType.VarChar, 0, "R_MAN"), New System.Data.SqlClient.SqlParameter("@CLIENT", System.Data.SqlDbType.VarChar, 0, "CLIENT"), New System.Data.SqlClient.SqlParameter("@CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, "CLIENT_TYPE"), New System.Data.SqlClient.SqlParameter("@CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, "CLIENT_CAT"), New System.Data.SqlClient.SqlParameter("@CLIENT_GD", System.Data.SqlDbType.VarChar, 0, "CLIENT_GD"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 0, "ZONE"), New System.Data.SqlClient.SqlParameter("@ROUTE", System.Data.SqlDbType.VarChar, 0, "ROUTE"), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 0, "AREA"), New System.Data.SqlClient.SqlParameter("@EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, "EXP_SUB_HEAD"), New System.Data.SqlClient.SqlParameter("@PRINTER", System.Data.SqlDbType.VarChar, 0, "PRINTER"), New System.Data.SqlClient.SqlParameter("@RPT_TITLE", System.Data.SqlDbType.VarChar, 0, "RPT_TITLE"), New System.Data.SqlClient.SqlParameter("@RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, "RPT_WARRANTY"), New System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_GROUP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_GROUP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_BANK_ACC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_BANK_ACC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_S_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_S_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_P_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_P_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_D_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_D_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_R_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_R_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_CAT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_GD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_GD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ZONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ZONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ROUTE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ROUTE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_AREA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_AREA", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_EXP_SUB_HEAD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_PRINTER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_PRINTER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_TITLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_TITLE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_WARRANTY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsLUP_BUSINESS_GROUP1
        '
        Me.DsLUP_BUSINESS_GROUP1.DataSetName = "dsLUP_BUSINESS_GROUP"
        Me.DsLUP_BUSINESS_GROUP1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_EMPLOYEE1
        '
        Me.DsLUP_EMPLOYEE1.DataSetName = "dsLUP_EMPLOYEE"
        Me.DsLUP_EMPLOYEE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsCLIENT_INFO1
        '
        Me.DsCLIENT_INFO1.DataSetName = "dsCLIENT_INFO"
        Me.DsCLIENT_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsV_SALE_DETAIL1
        '
        Me.DsV_SALE_DETAIL1.DataSetName = "dsV_SALE_DETAIL"
        Me.DsV_SALE_DETAIL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsNS_DEFAULT1
        '
        Me.DsNS_DEFAULT1.DataSetName = "dsNS_DEFAULT"
        Me.DsNS_DEFAULT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsSV_STOCK_BAL1
        '
        Me.DsSV_STOCK_BAL1.DataSetName = "dsSV_STOCK_BAL"
        Me.DsSV_STOCK_BAL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmSALE
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(831, 605)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtVehicle)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.LblLoadPass)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSALE"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sale Invoice"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.DsV_SALE_MASTER1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_CLIENT_RECEIPT1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsV_CLIENT_BAL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_CLIENT_STANDBY_CHEQ1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCLIENT_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_SALE_DETAIL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsNS_DEFAULT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSV_STOCK_BAL1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Public Search_Inv As Boolean = False

#End Region

#Region "PAYMENTS VARIABLES"
    Public CASH_AMT, BANK_AMT, SINV_NO As Double
    Public CASH_PAY As Boolean = False, BANK_PAY As Boolean = False, BOTH_PAY As Boolean = False
    Public CHEQ_NO, CHEQ_DATE, DESCRIPTION, CHEQ_TYPE, BANK_ACC, Rec_Date As String
    Public P_Inv As String
#End Region

#Region "FORM CONTROL"

    Private Sub frmSALE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.BttnSave.Text = "&Update" And Me.BttnSave.Enabled = True Then
            MsgBox("Can't close without Updating OR Cancelling Invoice", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        ElseIf Me.BttnSave.Text = "&Save" And Me.BttnSave.Enabled = True Then
            MsgBox("Can't close without Saving OR Cancelling Invoice", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        End If
    End Sub
    Private Sub frmSALE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Client()
        Me.FillComboBox_Employee()
        Me.FillComboBox_Group()

        Dim StrSql As String = "SELECT nID AS ID, sBUSINESS_GP AS [GROUP], sBANK_ACC AS BANK_ACC, sS_MAN AS S_MAN, sP_MAN AS P_MAN, sD_MAN AS D_MAN, sR_MAN AS R_MAN, sCLIENT AS CLIENT, sCLIENT_TYPE AS CLIENT_TYPE, sCLIENT_CAT AS CLIENT_CAT, sCLIENT_GD AS CLIENT_GD, sZONE AS ZONE, sROUTE AS ROUTE, sAREA AS AREA, sEXP_SUB_HEAD AS EXP_SUB_HEAD, sPRINTER AS PRINTER, sREPORT_TITLE AS RPT_TITLE, sREPORT_WARRANTY AS RPT_WARRANTY FROM NS_DEFAULT"
        Dim CmdSql As New SDS.SqlCommand(StrSql, Me.SqlConnection1)
        Me.daNS_DEFAULT = New SDS.SqlDataAdapter(CmdSql)
        Me.daNS_DEFAULT.Fill(Me.DsNS_DEFAULT1.NS_DEFAULT)
        Me.Default_Setting()

        Me.Disable_All()
        Me.BttnPrev.Enabled = False
        Me.BttnPrint.Enabled = False
        Me.BttnSave.Enabled = False

        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.TxtDispDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        'Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmSALE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.GotFocus, TxtCashClient.GotFocus, TxtDate.GotFocus, TxtDescription.GotFocus, TxtDiscount.GotFocus, TxtDiscPercent.GotFocus, TxtDispDate.GotFocus, TxtFreight.GotFocus, TxtNetTotal.GotFocus, TxtOtherDisc.GotFocus, TxtReceipt.GotFocus, TxtTotal.GotFocus, TxtTotalItems.GotFocus, TxtTRno.GotFocus, TxtTRqty.GotFocus, TxtUnloading.GotFocus, TxtVehicle.GotFocus, TxtRemarks.GotFocus, TxtCashMemo.GotFocus, TxtReceivables.GotFocus, TxtClientBal.GotFocus, TxtStandyBy.GotFocus, TxtNET_Receivable.GotFocus, TxtInvBalance.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
        Dim Ctrl As Control = sender
        Select Case Ctrl.Name
            Case "TxtDescription"
                If sender.Text = "Other's Description Here!" Then
                    sender.Text = Nothing
                End If
        End Select
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.LostFocus, TxtCashClient.LostFocus, TxtDate.LostFocus, TxtDescription.LostFocus, TxtDiscount.LostFocus, TxtDiscPercent.LostFocus, TxtDispDate.LostFocus, TxtFreight.LostFocus, TxtInvBalance.LostFocus, TxtNetTotal.LostFocus, TxtOtherDisc.LostFocus, TxtReceivables.LostFocus, TxtReceipt.LostFocus, TxtClientBal.LostFocus, TxtTotal.LostFocus, TxtTotalItems.LostFocus, TxtTRno.LostFocus, TxtTRqty.LostFocus, TxtUnloading.LostFocus, TxtVehicle.LostFocus, TxtRemarks.LostFocus, TxtStandyBy.LostFocus, TxtNET_Receivable.LostFocus, TxtCashMemo.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

                Case "TxtDispDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub

    'Leave
    Private Sub TxtTRqty_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtTRqty.Leave
        If Me.TxtTRqty.Text = Nothing Then
            Me.TxtTRqty.Text = 0
        End If
    End Sub

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTotalItems.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtDiscount.KeyPress, TxtFreight.KeyPress, TxtInvBalance.KeyPress, TxtNetTotal.KeyPress, TxtOtherDisc.KeyPress, TxtReceivables.KeyPress, TxtReceipt.KeyPress, TxtClientBal.KeyPress, TxtTotal.KeyPress, TxtUnloading.KeyPress, TxtDiscPercent.KeyPress, TxtStandyBy.KeyPress, TxtNET_Receivable.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    'NET TOTAL CALCULATION
    Private Sub TxtUnloading_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUnloading.TextChanged, TxtFreight.TextChanged, TxtOtherDisc.TextChanged, TxtDiscPercent.TextChanged, TxtDiscount.TextChanged, TxtTotal.TextChanged
        If Me.Search_Inv = False Then
            On Error GoTo Fix
            Dim Freight, Unloading, Total, Disc_RS, Disc_Age, Disc_Other As Double
            Freight = Val(Me.TxtFreight.Text)
            Unloading = Val(Me.TxtUnloading.Text)
            Total = Val(Me.TxtTotal.Text)
            Disc_RS = Val(Me.TxtDiscount.Text)
            Disc_Age = (Total * Val(Me.TxtDiscPercent.Text)) / 100
            Disc_Other = Val(Me.TxtOtherDisc.Text)

            Me.TxtNetTotal.Text = (Total + Freight + Unloading) - (Disc_RS + Disc_Age + Disc_Other)
            Me.TxtNetTotal.Text = Decimal.Round(CDec(Me.TxtNetTotal.Text), 2)
Fix:
        End If

    End Sub

    'Net Receivable Calculation
    Private Sub TxtReceivables_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtStandyBy.TextChanged
        Me.TxtReceivables.Text = Val(Me.TxtNET_Receivable.Text) + Val(Me.TxtStandyBy.Text)
    End Sub

    'Invoice & Supplier Balance Calculation
    Private Sub TxtReceipt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtReceipt.TextChanged, TxtNetTotal.TextChanged, TxtNET_Receivable.TextChanged

        Me.TxtReceivables.Text = Val(Me.TxtNET_Receivable.Text) + Val(Me.TxtStandyBy.Text)

        On Error GoTo Fix

        If Me.TxtReceipt.TextLength > 0 Then
            Me.TxtReceipt.Text = Decimal.Round(CDec(Me.TxtReceipt.Text), 2)
        End If

        Me.TxtInvBalance.Text = Val(Me.TxtNetTotal.Text) - Val(Me.TxtReceipt.Text)
        Me.TxtClientBal.Text = Val(Me.TxtNET_Receivable.Text) + Val(Me.TxtInvBalance.Text)

        Me.TxtInvBalance.Text = Decimal.Round(CDec(Me.TxtInvBalance.Text), 2)
        Me.TxtClientBal.Text = Decimal.Round(CDec(Me.TxtClientBal.Text), 2)

        If Val(Me.TxtNetTotal.Text) > 0 Then
            Me.BttnReceipt.Enabled = True
        Else
            Me.BttnReceipt.Enabled = False
        End If

Fix:
    End Sub

    'Fill data for Modification
    Private Sub TxtInvoice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.TextChanged
        If Me.Search_Inv = True Then

            'FILL MASTER RECORD
            Me.Fill_Master_Date()

            'FILL DETAIL RECORD
            Me.Fill_Detail_Data()

            Me.Search_Inv = False

            'FILL TOTAL PAYMENT
            Me.Fill_Receipt_Data()

            Dim StrClient As String = Me.CmbClient.Text
            Me.CmbClient.SelectedIndex = -1
            Me.CmbClient.SelectedIndex = Me.CmbClient.FindString(StrClient)

            'MsgBox(Me.TxtInvoice.Text)
        End If

    End Sub
#End Region

#Region "ComboBox Controls"
    Private Sub CmbClient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbClient.SelectedIndexChanged, CmbGroup.SelectedIndexChanged
        Try
            Dim Str1 As String = "SELECT CLIENT_ID, CONVERT(NUMERIC(18,2),CLIENT_BAL) AS CLIENT_BAL, GROUP_ID FROM SV_CLIENT_BALANCE WHERE CLIENT_ID=" & Val(Me.CmbClient.SelectedItem.Col3) & " AND GROUP_ID=" & Val(Me.CmbGroup.SelectedItem.Col3) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_CLIENT_BAL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_CLIENT_BAL1.Clear()
            Me.daV_CLIENT_BAL.Fill(Me.DsV_CLIENT_BAL1.SV_CLIENT_BALANCE)

        Catch ex As Exception

        End Try

        Try
            Dim Str1 As String = "SELECT CLIENT_ID, CONVERT(NUMERIC(18,2),TOT_STANDBY_CHEQ) AS TOT_STANDBY_CHEQ, GROUP_ID FROM V_CLIENT_TOT_STANDBY_CHEQ WHERE CLIENT_ID=" & Val(Me.CmbClient.SelectedItem.Col3) & " AND GROUP_ID=" & Val(Me.CmbGroup.SelectedItem.Col3) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_CLIENT_STANDBY_CHEQ = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_CLIENT_STANDBY_CHEQ1.Clear()
            Me.daV_CLIENT_STANDBY_CHEQ.Fill(Me.DsV_CLIENT_STANDBY_CHEQ1.V_CLIENT_TOT_STANDBY_CHEQ)
        Catch ex As Exception

        End Try

    End Sub
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbD_Man.GotFocus, CmbGroup.GotFocus, CmbClient.GotFocus, CmbS_Man.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbD_Man.LostFocus, CmbGroup.LostFocus, CmbClient.LostFocus, CmbS_Man.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "Select Item Controls"
    Private Sub SelectItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectItemToolStripMenuItem.Click
        On Error GoTo Fix
        frmSELECT_ITEM_BATCH.TxtCompany.Text = Nothing
        frmSELECT_ITEM_BATCH.TxtItem.Text = Nothing
        frmSELECT_ITEM_BATCH.FrmStr = "Sale"
        frmSELECT_ITEM_BATCH.Row = Me.DataGridView1.CurrentRow.Index

        frmSELECT_ITEM_BATCH.ShowDialog(Me)
Fix:
    End Sub
#End Region

#Region "DataGridView Control"
    Dim ItemCode_Old As String

    Private Sub DataGridView1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
        ItemCode_Old = Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If Me.Search_Inv = False Then

            Try
                Me.DsLUP_ITEM1.Clear()
                Me.DsSV_STOCK_BAL1.Clear()

                Me.LblB_Pcs.Text = 0
                Me.LblPPP.Text = 0
                Me.LblRatePcs.Text = 0
                Me.LblRate.Text = 0
                Me.LblRetail.Text = 0
                Me.LblStock.Text = 0

                'FILL TOP LABLES OF DATAGRID
                'WORKING ON BATCH WISE STOCK CALCULATION---CHECK BATCH EXIST OR NOT?
                If Not Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value Is Nothing Then
                    Dim SalePrice As Double = 0, Bonus_Q As Double = 0
                    Dim Str1 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, sCLAIMABLE, sSTATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM WHERE nCODE=" & Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value) & ""
                    Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
                    Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

                    Me.DsLUP_ITEM1.Clear()
                    Me.daLUP_ITEM.Fill(Me.DsLUP_ITEM1.V_LUP_ITEM)

                    If Me.DsLUP_ITEM1.V_LUP_ITEM.Count = 0 Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColBatch").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColName").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColCost").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColRate").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColPack").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColPiece").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColBonus").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColPercentage").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColDisc_Rs").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColSaleTax").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColTotal").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColScmQty").Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColPPP").Value = Nothing

                        'SET FOCUS TO ColCode IS PENDING

                        Me.SelectItemToolStripMenuItem_Click(sender, e)

                        Me.LblB_Pcs.Text = 0
                        Me.LblPPP.Text = 0
                        Me.LblRatePcs.Text = 0
                        Me.LblRate.Text = 0
                        Me.LblRetail.Text = 0
                        Me.LblStock.Text = 0

                        Exit Sub
                    End If

                    Dim Str10 As String = "SELECT nCODE AS CODE, STK_BAL FROM SV_STOCK_BAL WHERE nCODE=" & Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value) & ""
                    Dim SqlCmd10 As New SDS.SqlCommand(Str10, Me.SqlConnection2)
                    Me.daSV_STOCK_BAL = New SDS.SqlDataAdapter(SqlCmd10)

                    Me.DsSV_STOCK_BAL1.Clear()
                    Me.daSV_STOCK_BAL.Fill(Me.DsSV_STOCK_BAL1.SV_STOCK_BAL)

                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColName").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(1).ToString()
                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColPPP").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(3).ToString()
                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColCost").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(6).ToString()
                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColSaleTax").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(11).ToString()

                    Dim Itm_Code As String = Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value

                    If Not Itm_Code = ItemCode_Old Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColRate").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(7).ToString()

                    End If

                    Me.LblRatePcs.Text = Val(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(7).ToString()) / Val(Me.LblPPP.Text)

                    If Val(Me.LblPPP.Text) <= 0 Then
                        MsgBox("Please confirm 'Item Code' or Item Detail", MsgBoxStyle.Exclamation, "(NS) - Error!")

                    ElseIf Not Me.DataGridView1.Rows(e.RowIndex).Cells("ColRate").Value Is Nothing Then

                        ''Sale Price Notification/Alert
                        'If Me.DataGridView1.Item("ColRate", Me.DataGridView1.CurrentCell.RowIndex).Selected = True Then
                        '    If Not SalePrice = Me.DataGridView1.Rows(e.RowIndex).Cells("ColRate").Value Then
                        '        MsgBox("Rate is not Equal to Actual Rate", MsgBoxStyle.Information, "(NS) - Rate")
                        '    End If

                        'ElseIf Me.DataGridView1.Item("ColBonus", Me.DataGridView1.CurrentCell.RowIndex).Selected = True Then
                        '    'Bonus Qty Notification/Alert
                        '    If Not Bonus_Q = Me.DataGridView1.Rows(e.RowIndex).Cells("ColBonus").Value Then
                        '        If MsgBox("Are you sure to Give him Bonus on this Item?", MsgBoxStyle.Question + vbYesNo, "(NS) - Rate") = MsgBoxResult.No Then
                        '            Me.DataGridView1.Rows(e.RowIndex).Cells("ColBonus").Value = 0
                        '        End If
                        '    End If
                        'End If

                        Dim Rate, PPP, Pks, Pcs, Line_Tot, P_age, Disc_Rs, S_Tax As Double
                        Rate = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColRate").Value)
                        PPP = Val(Me.LblPPP.Text)
                        Pks = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPack").Value)
                        Pcs = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPiece").Value)
                        P_age = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPercentage").Value)
                        Disc_Rs = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColDisc_Rs").Value)
                        S_Tax = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColSaleTax").Value)

                        Line_Tot = ((Rate / PPP) * ((Pks * PPP) + Pcs))
                        S_Tax = (Line_Tot * S_Tax) / 100
                        Line_Tot = (Line_Tot - (((Line_Tot * P_age) / 100) + Disc_Rs) + S_Tax)
                        Line_Tot = Decimal.Round(CDec(Line_Tot), 2)

                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColTotal").Value = Line_Tot

                        Dim i As Integer
                        Me.TxtTotal.Text = "0.00"

                        For i = 0 To Me.DataGridView1.Rows.Count - 1
                            Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value)
                        Next

                    End If

                    Me.TxtTotalItems.Text = Me.DataGridView1.Rows.Count - 1

                    SalePrice = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item("UNIT_RATE")
                    Bonus_Q = Val(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item("nBONUS_ON_PCS"))

                Else
                    Me.LblB_Pcs.Text = 0
                    Me.LblPPP.Text = 0
                    Me.LblRatePcs.Text = 0
                    Me.LblRate.Text = 0
                    Me.LblRetail.Text = 0
                    Me.LblStock.Text = 0
                End If

                If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value = 1 Then
                    If Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCode").Value Is Nothing And Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value Is Nothing Then
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = 0
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = 0

                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = 1
                    End If

                    If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value > 0 Then
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value) + Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value)
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = 0

                    End If

                ElseIf Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value > 1 Then
                    If Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCode").Value Is Nothing And Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value Is Nothing Then
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = 0
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = 0

                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = 1
                    End If

                    If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value <= Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value Then
                        Dim PPP, PCS, PKS As Integer
                        PPP = Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value
                        PCS = Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value
                        PKS = PCS / PPP
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value) + PKS
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = PCS Mod PPP
                    End If
                End If

            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        End If


    End Sub

    Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        ItemCode_Old = Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value
        Me.DataGridView1_CellValueChanged(sender, e)
    End Sub
    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        Dim i As Integer
        Me.TxtTotal.Text = "0.00"
        For i = 0 To Me.DataGridView1.Rows.Count - 1
            Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value)
        Next

    End Sub
    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbClient.SelectedIndex = -1 Or Me.CmbClient.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

            Me.Null_Focus()


        ElseIf MsgBox("Are you sure to Delete Item?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Deleting Item?") = MsgBoxResult.Yes Then
            'DELETE FROM SALE DETAIL
            Me.asDelete.DeleteValue_NoErr("DELETE FROM SALE_DETAIL WHERE nID=" & Val(Me.DataGridView1.Rows(e.Row.Index).Cells("ColSR").Value) & "")

            'UPDATE VALUE IN SALE MASTER
            Me.asUpdate.UpdateValue_NoErr("UPDATE SALE_MASTER SET nCLIENT_ID='" & Val(Me.CmbClient.SelectedItem.Col3) & "', sCASH_CLIENT='" & Me.TxtCashClient.Text & "', sCASH_MEMO_NO='" & Me.TxtCashMemo.Text & "', nLPINV_NO=" & Val(Me.LblLoadPass.Text) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', dDISP_DATE='" & CDate(Me.TxtDispDate.Text).ToString("MM-dd-yyyy") & "', sVEHICLE='" & Me.TxtVehicle.Text & "', nFREIGHT=" & Val(Me.TxtFreight.Text) & ", nUNLOADING=" & Val(Me.TxtUnloading.Text) & ", sTR_NO='" & Me.TxtTRno.Text & "', nTR_QTY=" & Val(Me.TxtTRqty.Text) & ", nTOTAL_BILL=" & Val(Me.TxtTotal.Text) & ", nDISCOUNT=" & Val(Me.TxtDiscount.Text) & ", nDISC_PERCENT=" & Val(Me.TxtDiscPercent.Text) & ", nOTHERS=" & Val(Me.TxtOtherDisc.Text) & ", sOTHER_DESC='" & Me.TxtDescription.Text & "', nNET_TOTAL= " & Val(Me.TxtNetTotal.Text) & ", nEMPLOYEE_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

            Me.BttnNew.Enabled = False
            Me.BttnAdd.Enabled = False
        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        If Asc(e.KeyChar) = Keys.Escape Then
            If Me.BttnNew.Text = "Ca&ncel" Then
                Me.BttnNew_Click(sender, e)
            Else
                Me.BttnClose_Click(sender, e)
            End If
        End If
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAdd.Click
        Me.TxtInvoice.Text = Val(Me.TxtInvoice.Text) + 1
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        If Me.BttnNew.Text = "&New" Then
            Me.Enable_All()

            Me.TxtReceipt.Text = "0.00"

            Rec_Date = Nothing
            CASH_AMT = 0
            CHEQ_NO = Nothing
            CHEQ_TYPE = Nothing
            CHEQ_DATE = Nothing
            BANK_AMT = 0
            BANK_ACC = Nothing
            SINV_NO = 0
            DESCRIPTION = Nothing

            Me.BOTH_PAY = False
            Me.BANK_PAY = False
            Me.CASH_PAY = False

            Me.BttnSearch_Inv.Enabled = False
            Me.BttnPrev.Enabled = False
            Me.BttnPrint.Enabled = False
            Me.BttnReceipt.Enabled = False
            Me.BttnSave.Enabled = True
            Me.BttnSave.Text = "&Save"
            Me.BttnClose.Enabled = False

            Me.CancelButton = Me.BttnNew

            Me.Clear_All()
            Me.BttnNew.Text = "Ca&ncel"

            Me.TxtInvoice.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nSINV_NO) FROM SALE_MASTER") + 1

        ElseIf Me.BttnNew.Text = "Ca&ncel" Then
            If MsgBox("Are you sure to Cancel this Invoice?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Cancel Invoice?") = MsgBoxResult.Yes Then
                Me.Disable_All()

                Me.BttnPrev.Enabled = False
                Me.BttnPrint.Enabled = False
                Me.BttnSave.Enabled = False
                Me.BttnClose.Enabled = True
                Me.BttnSearch_Inv.Enabled = True
                Me.BttnSave.Text = "&Save"
                Me.CancelButton = Me.BttnClose

                Me.Search_Inv = False

                Me.Clear_All()
                Me.BttnNew.Text = "&New"
            End If
        End If
    End Sub

    Private Sub BttnReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnReceipt.Click
        ''FILL TOTAL PAYMENT
        'Me.Fill_Receipt_Data()

        'If Not SINV_NO <= 0 Then

        frmSINV_RECEIPT.TxtCashPmt.Text = Me.CASH_AMT
        frmSINV_RECEIPT.TxtBankPmt.Text = Me.BANK_AMT

        frmSINV_RECEIPT.BnkACC = Me.BANK_ACC
        frmSINV_RECEIPT.TxtChequeNo.Text = Me.CHEQ_NO
        frmSINV_RECEIPT.TxtChequeDate.Text = Me.CHEQ_DATE
        frmSINV_RECEIPT.TxtChequeType.Text = Me.CHEQ_TYPE

        'Else
        'frmSINV_RECEIPT.TxtCashPmt.Text = "0.00"
        'frmSINV_RECEIPT.TxtBankPmt.Text = "0.00"

        'frmSINV_RECEIPT.CmbBankAccount.SelectedIndex = -1
        'frmSINV_RECEIPT.TxtChequeNo.Text = Nothing
        'frmSINV_RECEIPT.TxtChequeDate.Text = Nothing
        'frmSINV_RECEIPT.TxtChequeType.Text = Nothing

        'End If

        On Error GoTo Fix

        frmSINV_RECEIPT.ShowDialog(Me)

Fix:
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click
        If Not CDate(Me.TxtDate.Text) >= CDate("01-03-2010") Then

            Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM SALE_MASTER WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

            If Me.TxtDescription.Text = "Other's Description Here!" Then
                Me.TxtDescription.Text = Nothing
            End If

            If Me.BttnSave.Text = "&Save" Then

                If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbClient.SelectedIndex = -1 Or Me.CmbClient.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
                    MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

                    Me.Null_Focus()

                ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) <= 0 Then
                    MsgBox("Please enter atleast one Item to save Invoice!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                    Me.DataGridView1.Focus()

                ElseIf Me.asSELECT.pFlg1 = False Then
                    If Val(Me.TxtInvBalance.Text) < 0 Then
                        MsgBox("Can't save!" & vbCrLf & "Payment is more then Invoice Total", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                        Me.BttnReceipt.Focus()

                    Else
                        'INSERT VALUES IN SALE MASTER
                        Me.asInsert.SaveValue("INSERT INTO SALE_MASTER(nSINV_NO, nCLIENT_ID, sCASH_CLIENT, sCASH_MEMO_NO, nLPINV_NO, dDATE, dDISP_DATE, sVEHICLE, nFREIGHT, nUNLOADING, sTR_NO, nTR_QTY, nTOTAL_BILL, nDISCOUNT, nDISC_PERCENT, nOTHERS, sOTHER_DESC, nNET_TOTAL, nEMPLOYEE_CODE, nLOGIN_ID, nBUSINESS_CODE, sREMARKS, nD_MAN_CODE) VALUES(" & Val(Me.TxtInvoice.Text) & "," & Val(Me.CmbClient.SelectedItem.Col3) & ",'" & Me.TxtCashClient.Text & "', '" & Me.TxtCashMemo.Text & "', " & Val(Me.LblLoadPass.Text) & ",'" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "','" & CDate(Me.TxtDispDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.TxtVehicle.Text & "', " & Val(Me.TxtFreight.Text) & ", " & Val(Me.TxtUnloading.Text) & ", '" & Me.TxtTRno.Text & "', " & Val(Me.TxtTRqty.Text) & "," & Val(Me.TxtTotal.Text) & "," & Val(Me.TxtDiscount.Text) & "," & Val(Me.TxtDiscPercent.Text) & "," & Val(Me.TxtOtherDisc.Text) & ",'" & Me.TxtDescription.Text & "', " & Val(Me.TxtNetTotal.Text) & "," & Val(Me.CmbS_Man.SelectedItem.Col3) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ",'" & Me.TxtRemarks.Text & "'," & Val(Me.CmbD_Man.SelectedItem.Col3) & ")")

                        Dim i As Integer
                        For i = 0 To Me.DataGridView1.Rows.Count - 2
                            Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value)
                            Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value)
                            Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value)
                            Dim Bonus As Double = Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value)
                            Dim Tot_Pcs As Double
                            Tot_Pcs = (Pks * PPP) + (Pcs + Bonus)

                            'INSERT VALUES IN SALE DETAIL
                            Me.asInsert.SaveValue("INSERT INTO SALE_DETAIL (nSINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, dDATE)VALUES(" & Val(Me.TxtInvoice.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColDisc_Rs").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")

                        Next

                        If Me.CASH_PAY = True Then
                            If Me.CASH_AMT > 0 Then
                                Me.asInsert.SaveValueIN("INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbClient.SelectedItem.Col3) & ",'" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "'," & Me.CASH_AMT & "," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbD_Man.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")
                            End If

                        ElseIf Me.BANK_PAY = True Then
                            If Me.BANK_AMT > 0 Then
                                Me.asInsert.SaveValueIN("INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbClient.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', " & Me.BANK_AMT & ",1,'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbD_Man.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")
                            End If

                        ElseIf Me.BOTH_PAY = True Then
                            If Me.CASH_AMT > 0 And Me.BANK_AMT > 0 Then
                                Me.asInsert.SaveValueIN("INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbClient.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Me.CASH_AMT & ", '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', " & Me.BANK_AMT & ",1,'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbD_Man.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")
                            End If

                        Else
                            MsgBox("Credit Sale Invoice Saved!", MsgBoxStyle.Information, "(NS) - Credit Invoice!")

                        End If

                        Me.BttnPrev.Enabled = True
                        Me.BttnPrint.Enabled = True
                        Me.BttnSearch_Inv.Enabled = True
                        Me.BttnReceipt.Enabled = False
                        Me.BttnNew.Text = "&New"
                        Me.BttnSave.Enabled = False
                        Me.BttnClose.Enabled = True

                    End If


                ElseIf Me.asSELECT.pFlg1 = True Then
                    MsgBox("This Invoice # '" & Me.TxtInvoice.Text & "' is Already Exist. " & vbCrLf & "To modify this invoice please click on 'Search Invoice' Button", MsgBoxStyle.Exclamation, "(NS) - Already Exist!")

                End If

                'UPDATE SALE INVOICE
            ElseIf Me.BttnSave.Text = "&Update" Then
                If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbClient.SelectedIndex = -1 Or Me.CmbClient.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
                    MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                    Me.Null_Focus()

                ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) <= 0 Then
                    MsgBox("Please enter atleast one Item to save Invoice!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                    Me.DataGridView1.Focus()

                ElseIf Me.asSELECT.pFlg1 = True Then


                    If Val(Me.TxtInvBalance.Text) < 0 Then
                        MsgBox("Can't save!" & vbCrLf & "Payment is more then Invoice Total", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                        Me.BttnReceipt.Focus()

                    Else
                        'UPDATE VALUES IN SALE MASTER
                        Me.asUpdate.UpdateValue("UPDATE SALE_MASTER SET nCLIENT_ID='" & Val(Me.CmbClient.SelectedItem.Col3) & "', sCASH_CLIENT='" & Me.TxtCashClient.Text & "', sCASH_MEMO_NO='" & Me.TxtCashMemo.Text & "', nLPINV_NO=" & Val(Me.LblLoadPass.Text) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', dDISP_DATE='" & CDate(Me.TxtDispDate.Text).ToString("MM-dd-yyyy") & "', sVEHICLE='" & Me.TxtVehicle.Text & "', nFREIGHT=" & Val(Me.TxtFreight.Text) & ", nUNLOADING=" & Val(Me.TxtUnloading.Text) & ", sTR_NO='" & Me.TxtTRno.Text & "', nTR_QTY=" & Val(Me.TxtTRqty.Text) & ", nTOTAL_BILL=" & Val(Me.TxtTotal.Text) & ", nDISCOUNT=" & Val(Me.TxtDiscount.Text) & ", nDISC_PERCENT=" & Val(Me.TxtDiscPercent.Text) & ", nOTHERS=" & Val(Me.TxtOtherDisc.Text) & ", sOTHER_DESC='" & Me.TxtDescription.Text & "', nNET_TOTAL= " & Val(Me.TxtNetTotal.Text) & ", nEMPLOYEE_CODE=" & Val(Me.CmbS_Man.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sREMARKS='" & Me.TxtRemarks.Text & "', nD_MAN_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & " WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                        Dim i As Integer
                        For i = 0 To Me.DataGridView1.Rows.Count - 2
                            Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value)
                            Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value)
                            Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value)
                            Dim Bonus As Double = Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value)
                            Dim Tot_Pcs As Double
                            Tot_Pcs = (Pks * PPP) + (Pcs + Bonus)

                            If Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then
                                'INSERT VALUES IN SALE DETAIL
                                Me.asInsert.SaveValue("INSERT INTO SALE_DETAIL (nSINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, dDATE)VALUES(" & Val(Me.TxtInvoice.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColDisc_Rs").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")

                            ElseIf Not Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then

                                'UPDATE VALUES IN SALE DETAIL
                                Me.asUpdate.UpdateValue("UPDATE SALE_DETAIL SET nSINV_NO=" & Val(Me.TxtInvoice.Text) & ", nITEM_CODE=" & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", sBATCH_NO='" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', nUNIT_COST=" & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", nUNIT_RATE=" & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", nDISC_RS=" & Val(Me.DataGridView1.Rows(i).Cells("ColDisc_Rs").Value) & ", nDISC_PER=" & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", nSALE_TAX=" & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & ", nPPP=" & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", nQTY_PKS=" & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", nQTY_PCS=" & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", nQTY_BONUS=" & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", nQTY_Tot_PCS=" & Tot_Pcs & ", nTOTAL_VALUE=" & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "' WHERE nID=" & Val(Me.DataGridView1.Rows(i).Cells("ColSR").Value.ToString) & "")

                            End If

                        Next

                        Me.asSELECT.SavedpFlg2(Rd, "SELECT * FROM CLIENT_RECEIPT WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                        If Me.CASH_PAY = True Then
                            If Me.asSELECT.pFlg2 = True Then
                                Me.asUpdate.UpdateValueIN("UPDATE CLIENT_RECEIPT SET nCLIENT_ID=" & Val(Me.CmbClient.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nCASH_AMOUNT=" & Me.CASH_AMT & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nEMP_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.DESCRIPTION & "' WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                            Else
                                Me.asInsert.SaveValueIN("INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbClient.SelectedItem.Col3) & ",'" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "'," & Me.CASH_AMT & "," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbD_Man.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")

                            End If


                        ElseIf Me.BANK_PAY = True Then
                            If Me.asSELECT.pFlg2 = True Then
                                Me.asUpdate.UpdateValueIN("UPDATE CLIENT_RECEIPT SET nCLIENT_ID=" & Val(Me.CmbClient.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', sCHEQUE_NO='" & Me.CHEQ_NO & "',sCHEQUE_TYPE='" & Me.CHEQ_TYPE & "', dCHEQUE_DATE='" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', nCHEQUE_AMOUNT=" & Me.BANK_AMT & ", sACCOUNT_CODE='" & Me.BANK_ACC & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nEMP_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.DESCRIPTION & "' WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                            Else
                                Me.asInsert.SaveValueIN("INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbClient.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', " & Me.BANK_AMT & ",1,'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbD_Man.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")
                            End If


                        ElseIf Me.BOTH_PAY = True Then
                            If Me.asSELECT.pFlg2 = True Then
                                Me.asUpdate.UpdateValueIN("UPDATE CLIENT_RECEIPT SET nCLIENT_ID=" & Val(Me.CmbClient.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nCASH_AMOUNT=" & Me.CASH_AMT & ", sCHEQUE_NO='" & Me.CHEQ_NO & "',sCHEQUE_TYPE='" & Me.CHEQ_TYPE & "', dCHEQUE_DATE='" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', nCHEQUE_AMOUNT=" & Me.BANK_AMT & ", sACCOUNT_CODE='" & Me.BANK_ACC & "', nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nEMP_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.DESCRIPTION & "' WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                            Else
                                Me.asInsert.SaveValueIN("INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO,sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbClient.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Me.CASH_AMT & ", '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', " & Me.BANK_AMT & ",1,'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbD_Man.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")

                            End If

                        ElseIf Not (Val(Me.CASH_AMT) + Val(Me.BANK_AMT)) = 0 Then
                            If Me.asSELECT.pFlg2 = True Then
                                Me.asUpdate.UpdateValueIN("UPDATE CLIENT_RECEIPT SET nCLIENT_ID=" & Val(Me.CmbClient.SelectedItem.Col3) & ", dDATE='" & Me.Rec_Date & "', nCASH_AMOUNT=" & Me.CASH_AMT & ", sCHEQUE_NO='" & Me.CHEQ_NO & "',sCHEQUE_TYPE='" & Me.CHEQ_TYPE & "', dCHEQUE_DATE='" & Me.CHEQ_DATE & "', nCHEQUE_AMOUNT=" & Me.BANK_AMT & ", sACCOUNT_CODE='" & Me.BANK_ACC & "', nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nEMP_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.DESCRIPTION & "' WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                            Else
                                Me.asInsert.SaveValueIN("INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbClient.SelectedItem.Col3) & ", '" & Me.Rec_Date & "', " & Me.CASH_AMT & ", '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & Me.CHEQ_DATE & "', " & Me.BANK_AMT & ",1,'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbD_Man.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")

                            End If

                        Else
                            MsgBox("Credit Sale Invoice Saved!", MsgBoxStyle.Information, "(NS) - Credit Invoice!")

                        End If

                        Me.BttnPrev.Enabled = True
                        Me.BttnPrint.Enabled = True
                        Me.BttnSearch_Inv.Enabled = True
                        Me.BttnReceipt.Enabled = False
                        Me.BttnNew.Text = "&New"
                        Me.BttnNew.Enabled = True
                        Me.BttnAdd.Enabled = True
                        Me.BttnSave.Text = "&Save"
                        Me.BttnSave.Enabled = False
                        Me.BttnClose.Enabled = True
                        Me.Disable_All()

                    End If

                ElseIf Me.asSELECT.pFlg1 = False Then
                    MsgBox("This Invoice # " & Val(Me.TxtInvoice.Text) & " is not Exist.", MsgBoxStyle.Exclamation, "(NS) - Not Exist!")

                End If
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        If MsgBox("Are you sure to Close?", MsgBoxStyle.Question + vbYesNo, "(NS) - Close?") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Search Button Control"
    Private Sub BttnSearch_Inv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch_Inv.Click
        On Error GoTo Fix
        frmSEARCH_S_INV.TxtClient.Text = Nothing
        frmSEARCH_S_INV.TxtInvoice.Text = Nothing
        frmSEARCH_S_INV.TxtDateFrom.Text = Nothing
        frmSEARCH_S_INV.TxtDateTo.Text = Nothing
        frmSEARCH_S_INV.FrmStr = "SALE"

        frmSEARCH_S_INV.ShowDialog(Me)
Fix:
    End Sub
#End Region

#Region "Print Button Control"
    Private Sub BttnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrev.Click
        Dim Rpt As New rptSALES_INVOICE_WS
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = "{V_SALE_MASTER.SINV_NO}=" & Val(Me.TxtInvoice.Text) & ""
            Frm.Text = "Sale Invoice"
            Frm.MdiParent = Me.ParentForm
            Frm.Show()
            Frm.Activate()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub BttnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrint.Click
        Dim Rpt As New rptSALES_INVOICE_WS
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = "{V_SALE_MASTER.SINV_NO}=" & Val(Me.TxtInvoice.Text) & ""
            Frm.CRV.PrintReport()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillComboBox_Client()
        Dim Str1 As String = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CONVERT(NUMERIC(18,2), CREDIT_LIM) AS CREDIT_LIM, GST_NO, CONVERT(NUMERIC(18,2), OPEN_BAL) AS OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO WHERE STATUS='1' ORDER BY SHOP_NAME"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daCLIENT_INFO = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsCLIENT_INFO1.Clear()
        Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO1.V_CLIENT_INFO)

        Dim dtLoading As New DataTable("V_CLIENT_INFO")

        dtLoading.Columns.Add("ID", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("NAME", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("SHOP_NAME", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsCLIENT_INFO1.V_CLIENT_INFO.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("ID") = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(0).ToString
            dr("NAME") = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(1).ToString
            dr("SHOP_NAME") = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(2).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbClient.SelectedIndex = -1
        Me.CmbClient.Items.Clear()
        Me.CmbClient.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbClient.SourceDataString = New String(2) {"SHOP_NAME", "NAME", "ID"}
        Me.CmbClient.SourceDataTable = dtLoading
    End Sub
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
    Private Sub FillComboBox_Employee()
        Dim Str1 As String = "SELECT CODE, NAME, FATHER_NAME, NIC, HOME_PH, CELL, PRE_ADD, PER_ADD, DESIGNATION, APP_DATE, PAY, STATUS, LEAVE_DATE, EMAIL_ADD, BANK_ACC, BANK_ADD FROM V_EMPLOYEE_INFO WHERE STATUS='1'"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_EMPLOYEE = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_EMPLOYEE1.Clear()
        Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO)

        Dim dtLoading As New DataTable("V_EMPLOYEE_INFO")

        dtLoading.Columns.Add("CODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("NAME", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("DESIGNATION", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("CODE") = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(0).ToString
            dr("NAME") = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(1).ToString
            dr("DESIGNATION") = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(8).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbS_Man.SelectedIndex = -1
        Me.CmbS_Man.Items.Clear()
        Me.CmbS_Man.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbS_Man.SourceDataString = New String(2) {"NAME", "DESIGNATION", "CODE"}
        Me.CmbS_Man.SourceDataTable = dtLoading

        Me.CmbD_Man.SelectedIndex = -1
        Me.CmbD_Man.Items.Clear()
        Me.CmbD_Man.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbD_Man.SourceDataString = New String(2) {"NAME", "DESIGNATION", "CODE"}
        Me.CmbD_Man.SourceDataTable = dtLoading
    End Sub

    Private Sub Fill_Master_Date()
        Dim Str2 As String = "SELECT SINV_NO, SHOP_NAME, CASH_CLIENT, CASH_MEMO, LPINV_NO, S_DATE, DISP_DATE, VEHICLE, CONVERT(NUMERIC(18,2), FREIGHT) AS FREIGHT, CONVERT(NUMERIC(18,2), UNLOADING) AS UNLOADING, TR_NO, TR_QTY, CONVERT(NUMERIC(18,2), TOT_BILL) AS TOT_BILL, CONVERT(NUMERIC(18,2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18,2), DISC_OTHER) AS DISC_OTHER, OTHER_DESC, CONVERT(NUMERIC(18,2), NET_TOTAL) AS NET_TOTAL, EMP_NAME, GROUP_NAME, REMARKS, D_MAN FROM V_SALE_MASTER WHERE SINV_NO=" & Val(Me.TxtInvoice.Text) & ""
        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)

        Me.daV_SALE_MASTER = New SDS.SqlDataAdapter(SqlCmd2)

        Me.DsV_SALE_MASTER1.Clear()
        Me.daV_SALE_MASTER.Fill(Me.DsV_SALE_MASTER1.V_SALE_MASTER)

    End Sub
    Private Sub Fill_Detail_Data()
        Dim Str3 As String = "SELECT ID, SINV_NO, ITEM_CODE, ITEM_NAME, BATCH_NO, CONVERT(NUMERIC(18,2), UNIT_COST) AS UNIT_COST , CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE , CONVERT(NUMERIC(18,2), DISC_RS) AS DISC_RS, DISC_PER, PPP, QTY_PKS, QTY_PCS, QTY_BONUS, QTY_TOT_PCS, CONVERT(NUMERIC(18,2), TOTAL_VALUE) AS TOTAL_VALUE, SCM_ITEM_CODE, SCM_ITEM, SCM_QTY, SALE_TAX FROM V_SALE_DETAIL WHERE SINV_NO=" & Val(Me.TxtInvoice.Text) & ""
        Dim SqlCmd3 As New SDS.SqlCommand(Str3, Me.SqlConnection1)
        Me.daV_SALE_DETAIL = New SDS.SqlDataAdapter(SqlCmd3)

        Me.DsV_SALE_DETAIL1.Clear()
        Me.daV_SALE_DETAIL.Fill(Me.DsV_SALE_DETAIL1.V_SALE_DETAIL)

        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Count - 1
            Me.DataGridView1.Rows.Add()
            Me.DataGridView1.Rows(Cnt).Cells("ColCode").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(2).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColBatch").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(4).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColName").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(3).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColCost").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(5).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColRate").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(6).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPack").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(10).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPiece").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(11).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColBonus").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(12).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPercentage").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(8).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColDisc_Rs").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(7).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColSaleTax").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(18).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColTotal").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(14).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColSR").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(0).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPPP").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(9).ToString

        Next
    End Sub
    Private Sub Fill_Receipt_Data()
        Try
            Dim Str1 As String = "SELECT ID, CLIENT_ID, SHOP_NAME, R_DATE, CONVERT(NUMERIC(18,2), CASH_AMT) AS CASH_AMT, CHEQ_NO, CHEQ_TYPE, CHEQ_DATE, CONVERT(NUMERIC(18,2), BANK_AMT) AS BANK_AMT, SINV_NO, CHEQ_STATUS, STATUS_DATE, STATUS_DESC, BANK_ACC, BANK_NAME, EMP_NAME, USER_NAME, GROUP_NAME, DESCRIPTION, CONVERT(NUMERIC(18,2), TOT_RECEIPT) AS TOT_RECEIPT FROM V_CLIENT_RECEIPT WHERE SINV_NO=" & Val(Me.TxtInvoice.Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_CLIENT_RECEIPT = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_CLIENT_RECEIPT1.Clear()
            Me.daV_CLIENT_RECEIPT.Fill(Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT)

            'FILL VALUE IN PAYMENT's VARIABLES
            If Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Count > 0 Then
                SINV_NO = Val(Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(9).ToString)
                Rec_Date = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(3).ToString
                CASH_AMT = Val(Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(4).ToString)
                CHEQ_NO = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(5).ToString
                CHEQ_TYPE = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(6).ToString
                CHEQ_DATE = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(7).ToString

                BANK_AMT = Val(Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(8).ToString)
                BANK_ACC = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(13).ToString
                DESCRIPTION = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(17).ToString

                If Me.CASH_AMT > 0 And Me.BANK_AMT <= 0 Then
                    Me.CASH_PAY = True
                    Me.BANK_PAY = False
                    Me.BOTH_PAY = False

                ElseIf Me.CASH_AMT <= 0 And Me.BANK_AMT > 0 Then
                    Me.BANK_PAY = True
                    Me.CASH_PAY = False
                    Me.BOTH_PAY = False

                ElseIf Me.CASH_AMT > 0 And Me.BANK_AMT > 0 Then
                    Me.BOTH_PAY = True
                    Me.BANK_PAY = False
                    Me.CASH_PAY = False

                End If

                If CHEQ_DATE.Length > 0 Then
                    Me.CHEQ_DATE = CDate(Me.CHEQ_DATE).ToString("dd-MMM-yyyy")
                End If

            Else
                Rec_Date = Nothing
                CASH_AMT = 0
                CHEQ_NO = Nothing
                CHEQ_TYPE = Nothing
                CHEQ_DATE = Nothing
                BANK_AMT = 0
                BANK_ACC = Nothing
                SINV_NO = 0
                DESCRIPTION = Nothing

                Me.BOTH_PAY = False
                Me.BANK_PAY = False
                Me.CASH_PAY = False
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Null_Focus()
        If Me.TxtInvoice.Text = Nothing Then
            Me.TxtInvoice.Focus()

        ElseIf Me.TxtDate.Text = Nothing Then
            Me.TxtDate.Focus()

        ElseIf Me.TxtDispDate.Text = Nothing Then
            Me.TxtDispDate.Focus()

        ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
            Me.CmbGroup.Focus()

        ElseIf Me.CmbClient.SelectedIndex = -1 Or Me.CmbClient.Text = Nothing Then
            Me.CmbClient.Focus()

        ElseIf Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Then
            Me.CmbS_Man.Focus()

        ElseIf Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
            Me.CmbD_Man.Focus()


        End If
    End Sub

    Public Sub Disable_All()
        Dim Ctrl As Control
        For Each Ctrl In Me.Controls
            If Not Ctrl.Name = "GroupBox1" And Not Ctrl.Name = "Label3" Then
                Ctrl.Enabled = False
            End If
        Next
    End Sub
    Public Sub Enable_All()
        Dim Ctrl As Control
        For Each Ctrl In Me.Controls
            If Not Ctrl.Name = "GroupBox1" And Not Ctrl.Name = "Label3" Then
                Ctrl.Enabled = True
            End If
        Next
    End Sub

    Public Sub Clear_All()
        Me.TxtInvoice.Text = Nothing
        Me.CmbGroup.SelectedIndex = -1
        Me.CmbClient.SelectedIndex = -1
        Me.TxtVehicle.Text = Nothing
        Me.CmbS_Man.SelectedIndex = -1
        Me.CmbD_Man.SelectedIndex = -1
        Me.TxtCashClient.Text = Nothing
        Me.TxtReceivables.Text = 0
        Me.TxtStandyBy.Text = 0
        Me.TxtNET_Receivable.Text = 0

        Me.LblB_Pcs.Text = 0
        Me.LblPPP.Text = 0
        Me.LblRatePcs.Text = 0
        Me.LblRate.Text = 0
        Me.LblRetail.Text = 0
        Me.LblStock.Text = 0

        Me.TxtTotalItems.Text = 0
        Me.TxtTRno.Text = Nothing
        Me.TxtTRqty.Text = Nothing

        Me.TxtFreight.Text = "0.00"
        Me.TxtUnloading.Text = "0.00"

        Me.TxtTotal.Text = "0.00"
        Me.TxtDiscount.Text = "0.00"
        Me.TxtDiscPercent.Text = "0.00"
        'Me.TxtReceipt.Text = "0.00"

        Me.TxtRemarks.Text = Nothing

        Me.TxtCashMemo.Focus()

        'Me.CASH_AMT = 0.0
        'Me.BANK_AMT = 0.0

        'Me.BANK_ACC = Nothing
        'Me.CHEQ_NO = Nothing
        'Me.CHEQ_DATE = Nothing
        'Me.CHEQ_TYPE = Nothing

        Me.Default_Setting()

        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:
    End Sub

    Private Sub Default_Setting()
        On Error GoTo Fix
        Dim StrCMB As String

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("GROUP").ToString
        Me.CmbGroup.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("S_MAN").ToString
        Me.CmbS_Man.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbS_Man.SelectedIndex = Me.CmbS_Man.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("D_MAN").ToString
        Me.CmbD_Man.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbD_Man.SelectedIndex = Me.CmbD_Man.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("CLIENT").ToString
        Me.CmbClient.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbClient.SelectedIndex = Me.CmbClient.FindString(StrCMB)
        End If
Fix:
    End Sub
#End Region

End Class
