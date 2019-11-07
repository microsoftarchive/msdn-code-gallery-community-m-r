Imports SDS = System.Data.SqlClient
Public Class frmPURCHASE
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
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
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
    Friend WithEvents TxtCashSupplier As System.Windows.Forms.TextBox
    Friend WithEvents CmbSupplier As MTGCComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CmbEmployee As MTGCComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtPayables As System.Windows.Forms.TextBox
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
    Friend WithEvents TxtPayment As System.Windows.Forms.TextBox
    Friend WithEvents TxtNetTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents BttnPayment As System.Windows.Forms.Button
    Friend WithEvents TxtInvBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents BttnSearch_Item As System.Windows.Forms.Button
    Friend WithEvents BttnPrint As System.Windows.Forms.Button
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
    Friend WithEvents TxtSupplierBal As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents DsSUPPLIER_INFO1 As Neruo_Business_Solution.dsSUPPLIER_INFO
    Friend WithEvents daSUPPLIER_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents BttnPrev As System.Windows.Forms.Button
    Friend WithEvents BttnSearch_Inv As System.Windows.Forms.Button
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
    Friend WithEvents daV_SUPPLIER_BAL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlConnection2 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblCostPcs As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daPURCHASE_MASTER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsPURCHASE_MASTER1 As Neruo_Business_Solution.dsPURCHASE_MASTER
    Friend WithEvents SqlSelectCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_PURCHASE_DETAIL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsV_PURCHASE_DETAIL1 As Neruo_Business_Solution.dsV_PURCHASE_DETAIL
    Friend WithEvents SqlSelectCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_SUPPLIER_PAYMENT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsV_SUPPLIER_PAYMENT1 As Neruo_Business_Solution.dsV_SUPPLIER_PAYMENT
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TxtSup_Invoice As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtRecvDisc As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DsV_SUPPLIER_BAL1 As Neruo_Business_Solution.dsV_SUPPLIER_BAL
    Friend WithEvents daV_STOCK_NET_TOT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsV_STOCK_NET_TOT1 As Neruo_Business_Solution.dsV_STOCK_NET_TOT
    Friend WithEvents ColCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBatch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPiece As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBonus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPercentage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSaleTax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColScmItem As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ColScmQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents daNS_DEFAULT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand8 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsNS_DEFAULT1 As Neruo_Business_Solution.dsNS_DEFAULT
    Friend WithEvents Label32 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPURCHASE))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ColCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColBatch = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPack = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPiece = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColBonus = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPercentage = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColSaleTax = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColScmItem = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.ColScmQty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColSR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPPP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LblScmItem = New System.Windows.Forms.Label
        Me.LblB_Pcs = New System.Windows.Forms.Label
        Me.DsLUP_ITEM1 = New Neruo_Business_Solution.dsLUP_ITEM
        Me.LblCostPcs = New System.Windows.Forms.Label
        Me.LblPPP = New System.Windows.Forms.Label
        Me.LblStock = New System.Windows.Forms.Label
        Me.DsV_STOCK_NET_TOT1 = New Neruo_Business_Solution.dsV_STOCK_NET_TOT
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
        Me.TxtDiscPercent = New System.Windows.Forms.TextBox
        Me.DsPURCHASE_MASTER1 = New Neruo_Business_Solution.dsPURCHASE_MASTER
        Me.Label18 = New System.Windows.Forms.Label
        Me.BttnPayment = New System.Windows.Forms.Button
        Me.TxtTotal = New System.Windows.Forms.TextBox
        Me.TxtDescription = New System.Windows.Forms.TextBox
        Me.TxtPayment = New System.Windows.Forms.TextBox
        Me.DsV_SUPPLIER_PAYMENT1 = New Neruo_Business_Solution.dsV_SUPPLIER_PAYMENT
        Me.TxtSupplierBal = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TxtInvBalance = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtNetTotal = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TxtOtherDisc = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
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
        Me.BttnSearch_Item = New System.Windows.Forms.Button
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnPrev = New System.Windows.Forms.Button
        Me.BttnPrint = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSup_Invoice = New System.Windows.Forms.TextBox
        Me.TxtCashSupplier = New System.Windows.Forms.TextBox
        Me.TxtInvoice = New System.Windows.Forms.TextBox
        Me.CmbSupplier = New MTGCComboBox
        Me.CmbEmployee = New MTGCComboBox
        Me.CmbGroup = New MTGCComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtVehicle = New System.Windows.Forms.TextBox
        Me.TxtPayables = New System.Windows.Forms.TextBox
        Me.DsV_SUPPLIER_BAL1 = New Neruo_Business_Solution.dsV_SUPPLIER_BAL
        Me.TxtDispDate = New System.Windows.Forms.TextBox
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.DsSUPPLIER_INFO1 = New Neruo_Business_Solution.dsSUPPLIER_INFO
        Me.daSUPPLIER_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_BUSINESS_GROUP1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_EMPLOYEE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_EMPLOYEE1 = New Neruo_Business_Solution.dsLUP_EMPLOYEE
        Me.daLUP_ITEM = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand
        Me.daV_SUPPLIER_BAL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlConnection2 = New System.Data.SqlClient.SqlConnection
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlSelectCommand5 = New System.Data.SqlClient.SqlCommand
        Me.daPURCHASE_MASTER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand7 = New System.Data.SqlClient.SqlCommand
        Me.daV_PURCHASE_DETAIL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand6 = New System.Data.SqlClient.SqlCommand
        Me.daV_SUPPLIER_PAYMENT = New System.Data.SqlClient.SqlDataAdapter
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.TxtRecvDisc = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.daV_STOCK_NET_TOT = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand
        Me.DsV_PURCHASE_DETAIL1 = New Neruo_Business_Solution.dsV_PURCHASE_DETAIL
        Me.daNS_DEFAULT = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand6 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand8 = New System.Data.SqlClient.SqlCommand
        Me.DsNS_DEFAULT1 = New Neruo_Business_Solution.dsNS_DEFAULT
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_STOCK_NET_TOT1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DsPURCHASE_MASTER1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_SUPPLIER_PAYMENT1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsV_SUPPLIER_BAL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSUPPLIER_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DsV_PURCHASE_DETAIL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsNS_DEFAULT1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Controls.Add(Me.LblScmItem)
        Me.GroupBox2.Controls.Add(Me.LblB_Pcs)
        Me.GroupBox2.Controls.Add(Me.LblCostPcs)
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
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCode, Me.ColBatch, Me.ColName, Me.ColCost, Me.ColPack, Me.ColPiece, Me.ColBonus, Me.ColPercentage, Me.ColSaleTax, Me.ColTotal, Me.ColScmItem, Me.ColScmQty, Me.ColSR, Me.ColPPP})
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.GridColor = System.Drawing.Color.Gray
        Me.DataGridView1.Location = New System.Drawing.Point(3, 42)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 15
        Me.DataGridView1.Size = New System.Drawing.Size(804, 158)
        Me.DataGridView1.TabIndex = 14
        '
        'ColCode
        '
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.ColCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColCode.HeaderText = "Code"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.Width = 80
        '
        'ColBatch
        '
        Me.ColBatch.HeaderText = "Batch #"
        Me.ColBatch.Name = "ColBatch"
        Me.ColBatch.Width = 70
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
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "C2"
        DataGridViewCellStyle3.NullValue = "0.00"
        Me.ColCost.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColCost.HeaderText = "Cost"
        Me.ColCost.Name = "ColCost"
        Me.ColCost.Width = 60
        '
        'ColPack
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = "0"
        Me.ColPack.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColPack.HeaderText = "Pack(s)"
        Me.ColPack.Name = "ColPack"
        Me.ColPack.Width = 60
        '
        'ColPiece
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = "0"
        Me.ColPiece.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColPiece.HeaderText = "Pcs"
        Me.ColPiece.Name = "ColPiece"
        Me.ColPiece.Width = 40
        '
        'ColBonus
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = "0"
        Me.ColBonus.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColBonus.HeaderText = "Bonus (Pcs)"
        Me.ColBonus.Name = "ColBonus"
        Me.ColBonus.Width = 50
        '
        'ColPercentage
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N0"
        DataGridViewCellStyle7.NullValue = "0"
        Me.ColPercentage.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColPercentage.HeaderText = "%age"
        Me.ColPercentage.Name = "ColPercentage"
        Me.ColPercentage.Width = 35
        '
        'ColSaleTax
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = "0.00"
        Me.ColSaleTax.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColSaleTax.HeaderText = "S.Tax"
        Me.ColSaleTax.MaxInputLength = 3
        Me.ColSaleTax.Name = "ColSaleTax"
        Me.ColSaleTax.ReadOnly = True
        Me.ColSaleTax.Width = 40
        '
        'ColTotal
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = "0.00"
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle9
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
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        DataGridViewCellStyle10.NullValue = "0"
        Me.ColScmQty.DefaultCellStyle = DataGridViewCellStyle10
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
        Me.LblScmItem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblScmItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblScmItem.Location = New System.Drawing.Point(750, 16)
        Me.LblScmItem.Name = "LblScmItem"
        Me.LblScmItem.Size = New System.Drawing.Size(51, 23)
        Me.LblScmItem.TabIndex = 13
        Me.LblScmItem.Text = "0"
        Me.LblScmItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblB_Pcs
        '
        Me.LblB_Pcs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblB_Pcs.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM1, "V_LUP_ITEM.nBONUS_ON_PCS", True))
        Me.LblB_Pcs.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'LblCostPcs
        '
        Me.LblCostPcs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCostPcs.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCostPcs.Location = New System.Drawing.Point(387, 16)
        Me.LblCostPcs.Name = "LblCostPcs"
        Me.LblCostPcs.Size = New System.Drawing.Size(55, 23)
        Me.LblCostPcs.TabIndex = 7
        Me.LblCostPcs.Text = "0"
        Me.LblCostPcs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblPPP
        '
        Me.LblPPP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblPPP.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM1, "V_LUP_ITEM.nPPP", True))
        Me.LblPPP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPPP.Location = New System.Drawing.Point(276, 16)
        Me.LblPPP.Name = "LblPPP"
        Me.LblPPP.Size = New System.Drawing.Size(46, 23)
        Me.LblPPP.TabIndex = 5
        Me.LblPPP.Text = "0"
        Me.LblPPP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblStock
        '
        Me.LblStock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblStock.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_STOCK_NET_TOT1, "V_STOCK_NET_TOTAL.NET_TOTAL", True))
        Me.LblStock.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStock.Location = New System.Drawing.Point(500, 16)
        Me.LblStock.Name = "LblStock"
        Me.LblStock.Size = New System.Drawing.Size(51, 23)
        Me.LblStock.TabIndex = 9
        Me.LblStock.Text = "0"
        Me.LblStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DsV_STOCK_NET_TOT1
        '
        Me.DsV_STOCK_NET_TOT1.DataSetName = "dsV_STOCK_NET_TOT"
        Me.DsV_STOCK_NET_TOT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'LblRetail
        '
        Me.LblRetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblRetail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM1, "V_LUP_ITEM.UNIT_RETAIL", True))
        Me.LblRetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRetail.Location = New System.Drawing.Point(170, 16)
        Me.LblRetail.Name = "LblRetail"
        Me.LblRetail.Size = New System.Drawing.Size(64, 23)
        Me.LblRetail.TabIndex = 3
        Me.LblRetail.Text = "0"
        Me.LblRetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblRate
        '
        Me.LblRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblRate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM1, "V_LUP_ITEM.UNIT_RATE", True))
        Me.LblRate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRate.Location = New System.Drawing.Point(49, 16)
        Me.LblRate.Name = "LblRate"
        Me.LblRate.Size = New System.Drawing.Size(64, 23)
        Me.LblRate.TabIndex = 1
        Me.LblRate.Text = "0"
        Me.LblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(682, 16)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(62, 23)
        Me.Label26.TabIndex = 12
        Me.Label26.Text = "Scm Stk."
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(557, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(62, 23)
        Me.Label24.TabIndex = 10
        Me.Label24.Text = "B. on Pcs"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(119, 16)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 23)
        Me.Label30.TabIndex = 2
        Me.Label30.Text = "Retail:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(328, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(56, 23)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "Cost Pcs"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(241, 16)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(30, 23)
        Me.Label32.TabIndex = 4
        Me.Label32.Text = "PPP"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(445, 16)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(49, 23)
        Me.Label28.TabIndex = 8
        Me.Label28.Text = "Stock:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(6, 16)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(39, 23)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "Rate:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox4.Controls.Add(Me.TxtDiscPercent)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.BttnPayment)
        Me.GroupBox4.Controls.Add(Me.TxtTotal)
        Me.GroupBox4.Controls.Add(Me.TxtDescription)
        Me.GroupBox4.Controls.Add(Me.TxtPayment)
        Me.GroupBox4.Controls.Add(Me.TxtSupplierBal)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.TxtInvBalance)
        Me.GroupBox4.Controls.Add(Me.Label22)
        Me.GroupBox4.Controls.Add(Me.TxtNetTotal)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.TxtOtherDisc)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.TxtDiscount)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Location = New System.Drawing.Point(625, 359)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(195, 235)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        '
        'TxtDiscPercent
        '
        Me.TxtDiscPercent.BackColor = System.Drawing.Color.White
        Me.TxtDiscPercent.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.DISC_PER", True))
        Me.TxtDiscPercent.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDiscPercent.Location = New System.Drawing.Point(99, 62)
        Me.TxtDiscPercent.MaxLength = 50
        Me.TxtDiscPercent.Name = "TxtDiscPercent"
        Me.TxtDiscPercent.Size = New System.Drawing.Size(87, 21)
        Me.TxtDiscPercent.TabIndex = 5
        Me.TxtDiscPercent.Text = "0"
        Me.TxtDiscPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsPURCHASE_MASTER1
        '
        Me.DsPURCHASE_MASTER1.DataSetName = "dsPURCHASE_MASTER"
        Me.DsPURCHASE_MASTER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        'BttnPayment
        '
        Me.BttnPayment.Location = New System.Drawing.Point(6, 158)
        Me.BttnPayment.Name = "BttnPayment"
        Me.BttnPayment.Size = New System.Drawing.Size(87, 23)
        Me.BttnPayment.TabIndex = 11
        Me.BttnPayment.Text = "Pay&ment"
        Me.BttnPayment.UseVisualStyleBackColor = True
        '
        'TxtTotal
        '
        Me.TxtTotal.BackColor = System.Drawing.Color.White
        Me.TxtTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.TOTAL_BILL", True))
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
        'TxtDescription
        '
        Me.TxtDescription.BackColor = System.Drawing.Color.White
        Me.TxtDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.OTHER_DESC", True))
        Me.TxtDescription.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDescription.Location = New System.Drawing.Point(6, 111)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(180, 21)
        Me.TxtDescription.TabIndex = 8
        Me.TxtDescription.Text = "Other's Description Here!"
        '
        'TxtPayment
        '
        Me.TxtPayment.BackColor = System.Drawing.Color.White
        Me.TxtPayment.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SUPPLIER_PAYMENT1, "V_SUPPLIER_PAYMENT.TOT_PAYMENT", True))
        Me.TxtPayment.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtPayment.Location = New System.Drawing.Point(99, 159)
        Me.TxtPayment.MaxLength = 50
        Me.TxtPayment.Name = "TxtPayment"
        Me.TxtPayment.ReadOnly = True
        Me.TxtPayment.Size = New System.Drawing.Size(87, 21)
        Me.TxtPayment.TabIndex = 12
        Me.TxtPayment.TabStop = False
        Me.TxtPayment.Text = "0.00"
        Me.TxtPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_SUPPLIER_PAYMENT1
        '
        Me.DsV_SUPPLIER_PAYMENT1.DataSetName = "dsV_SUPPLIER_PAYMENT"
        Me.DsV_SUPPLIER_PAYMENT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtSupplierBal
        '
        Me.TxtSupplierBal.BackColor = System.Drawing.Color.White
        Me.TxtSupplierBal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtSupplierBal.ForeColor = System.Drawing.Color.Red
        Me.TxtSupplierBal.Location = New System.Drawing.Point(99, 207)
        Me.TxtSupplierBal.MaxLength = 50
        Me.TxtSupplierBal.Name = "TxtSupplierBal"
        Me.TxtSupplierBal.ReadOnly = True
        Me.TxtSupplierBal.Size = New System.Drawing.Size(87, 21)
        Me.TxtSupplierBal.TabIndex = 16
        Me.TxtSupplierBal.TabStop = False
        Me.TxtSupplierBal.Text = "0.00"
        Me.TxtSupplierBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Red
        Me.Label23.Location = New System.Drawing.Point(6, 207)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(87, 23)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "Supplier Bal"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtInvBalance
        '
        Me.TxtInvBalance.BackColor = System.Drawing.Color.White
        Me.TxtInvBalance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtInvBalance.ForeColor = System.Drawing.Color.DarkBlue
        Me.TxtInvBalance.Location = New System.Drawing.Point(99, 183)
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
        Me.Label22.Location = New System.Drawing.Point(6, 183)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(87, 23)
        Me.Label22.TabIndex = 13
        Me.Label22.Text = "Invoice Bal"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtNetTotal
        '
        Me.TxtNetTotal.BackColor = System.Drawing.Color.White
        Me.TxtNetTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.NET_TOTAL", True))
        Me.TxtNetTotal.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtNetTotal.Location = New System.Drawing.Point(99, 135)
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
        Me.Label20.Location = New System.Drawing.Point(6, 135)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(87, 23)
        Me.Label20.TabIndex = 9
        Me.Label20.Text = "Net Total"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtOtherDisc
        '
        Me.TxtOtherDisc.BackColor = System.Drawing.Color.White
        Me.TxtOtherDisc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.DISC_OTHER", True))
        Me.TxtOtherDisc.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtOtherDisc.Location = New System.Drawing.Point(99, 87)
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
        'TxtDiscount
        '
        Me.TxtDiscount.BackColor = System.Drawing.Color.White
        Me.TxtDiscount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.DISC_RS", True))
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
        Me.GroupBox5.TabIndex = 3
        Me.GroupBox5.TabStop = False
        '
        'TxtTRno
        '
        Me.TxtTRno.BackColor = System.Drawing.Color.White
        Me.TxtTRno.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.sTR_NO", True))
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
        Me.TxtTRqty.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.nTR_QTY", True))
        Me.TxtTRqty.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtTRqty.Location = New System.Drawing.Point(306, 14)
        Me.TxtTRqty.MaxLength = 50
        Me.TxtTRqty.Name = "TxtTRqty"
        Me.TxtTRqty.Size = New System.Drawing.Size(40, 21)
        Me.TxtTRqty.TabIndex = 4
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
        Me.TxtFreight.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.nFREIGHT", True))
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
        Me.TxtUnloading.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.nUNLOADING", True))
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
        Me.Label15.TabIndex = 5
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
        Me.GroupBox6.Location = New System.Drawing.Point(375, 493)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(244, 101)
        Me.GroupBox6.TabIndex = 6
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.sREMARKS", True))
        Me.TxtRemarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtRemarks.Location = New System.Drawing.Point(3, 19)
        Me.TxtRemarks.MaxLength = 100
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(238, 79)
        Me.TxtRemarks.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnSearch_Inv)
        Me.GroupBox1.Controls.Add(Me.BttnSearch_Item)
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnPrev)
        Me.GroupBox1.Controls.Add(Me.BttnPrint)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 413)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(362, 181)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'BttnSearch_Inv
        '
        Me.BttnSearch_Inv.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch_Inv.Location = New System.Drawing.Point(144, 125)
        Me.BttnSearch_Inv.Name = "BttnSearch_Inv"
        Me.BttnSearch_Inv.Size = New System.Drawing.Size(75, 42)
        Me.BttnSearch_Inv.TabIndex = 5
        Me.BttnSearch_Inv.Text = "Sea&rch Inv"
        '
        'BttnSearch_Item
        '
        Me.BttnSearch_Item.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch_Item.Location = New System.Drawing.Point(10, 125)
        Me.BttnSearch_Item.Name = "BttnSearch_Item"
        Me.BttnSearch_Item.Size = New System.Drawing.Size(75, 42)
        Me.BttnSearch_Item.TabIndex = 4
        Me.BttnSearch_Item.Text = "Sea&rch Item"
        Me.BttnSearch_Item.Visible = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(58, 75)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(75, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnPrev
        '
        Me.BttnPrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrev.Enabled = False
        Me.BttnPrev.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrev.Location = New System.Drawing.Point(197, 24)
        Me.BttnPrev.Name = "BttnPrev"
        Me.BttnPrev.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrev.TabIndex = 2
        Me.BttnPrev.Text = "Pre&view"
        '
        'BttnPrint
        '
        Me.BttnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrint.Enabled = False
        Me.BttnPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrint.Location = New System.Drawing.Point(90, 24)
        Me.BttnPrint.Name = "BttnPrint"
        Me.BttnPrint.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrint.TabIndex = 3
        Me.BttnPrint.Text = "&Print"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(230, 75)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 31)
        Me.BttnClose.TabIndex = 6
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Enabled = False
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(134, 56)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(95, 68)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.TxtSup_Invoice)
        Me.GroupBox3.Controls.Add(Me.TxtCashSupplier)
        Me.GroupBox3.Controls.Add(Me.TxtInvoice)
        Me.GroupBox3.Controls.Add(Me.CmbSupplier)
        Me.GroupBox3.Controls.Add(Me.CmbEmployee)
        Me.GroupBox3.Controls.Add(Me.CmbGroup)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtVehicle)
        Me.GroupBox3.Controls.Add(Me.TxtPayables)
        Me.GroupBox3.Controls.Add(Me.TxtDispDate)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(10, 44)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(810, 105)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(133, 21)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Cash Supplier Name"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(155, 18)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(95, 21)
        Me.Label27.TabIndex = 2
        Me.Label27.Text = "Supplier Inv #"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'TxtSup_Invoice
        '
        Me.TxtSup_Invoice.BackColor = System.Drawing.Color.White
        Me.TxtSup_Invoice.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.sSUP_INV_NO", True))
        Me.TxtSup_Invoice.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtSup_Invoice.Location = New System.Drawing.Point(257, 18)
        Me.TxtSup_Invoice.MaxLength = 50
        Me.TxtSup_Invoice.Name = "TxtSup_Invoice"
        Me.TxtSup_Invoice.Size = New System.Drawing.Size(89, 21)
        Me.TxtSup_Invoice.TabIndex = 3
        '
        'TxtCashSupplier
        '
        Me.TxtCashSupplier.BackColor = System.Drawing.Color.White
        Me.TxtCashSupplier.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.CASH_SUPPLIER", True))
        Me.TxtCashSupplier.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtCashSupplier.Location = New System.Drawing.Point(143, 72)
        Me.TxtCashSupplier.MaxLength = 50
        Me.TxtCashSupplier.Name = "TxtCashSupplier"
        Me.TxtCashSupplier.Size = New System.Drawing.Size(107, 21)
        Me.TxtCashSupplier.TabIndex = 16
        '
        'TxtInvoice
        '
        Me.TxtInvoice.BackColor = System.Drawing.Color.White
        Me.TxtInvoice.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtInvoice.Location = New System.Drawing.Point(83, 18)
        Me.TxtInvoice.MaxLength = 50
        Me.TxtInvoice.Name = "TxtInvoice"
        Me.TxtInvoice.ReadOnly = True
        Me.TxtInvoice.Size = New System.Drawing.Size(66, 21)
        Me.TxtInvoice.TabIndex = 1
        Me.TxtInvoice.TabStop = False
        '
        'CmbSupplier
        '
        Me.CmbSupplier.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbSupplier.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbSupplier.ColumnNum = 3
        Me.CmbSupplier.ColumnWidth = "140;140;40"
        Me.CmbSupplier.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.SUPPLIER_NAME", True))
        Me.CmbSupplier.DisplayMember = "Text"
        Me.CmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbSupplier.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbSupplier.DropDownForeColor = System.Drawing.Color.White
        Me.CmbSupplier.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbSupplier.DropDownWidth = 340
        Me.CmbSupplier.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbSupplier.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbSupplier.GridLineHorizontal = False
        Me.CmbSupplier.GridLineVertical = True
        Me.CmbSupplier.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbSupplier.Location = New System.Drawing.Point(83, 44)
        Me.CmbSupplier.ManagingFastMouseMoving = True
        Me.CmbSupplier.ManagingFastMouseMovingInterval = 30
        Me.CmbSupplier.Name = "CmbSupplier"
        Me.CmbSupplier.Size = New System.Drawing.Size(167, 22)
        Me.CmbSupplier.TabIndex = 10
        '
        'CmbEmployee
        '
        Me.CmbEmployee.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbEmployee.ColumnNum = 3
        Me.CmbEmployee.ColumnWidth = "100;100;30"
        Me.CmbEmployee.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.EMP_NAME", True))
        Me.CmbEmployee.DisplayMember = "Text"
        Me.CmbEmployee.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbEmployee.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbEmployee.DropDownForeColor = System.Drawing.Color.White
        Me.CmbEmployee.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbEmployee.DropDownWidth = 340
        Me.CmbEmployee.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbEmployee.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbEmployee.GridLineHorizontal = False
        Me.CmbEmployee.GridLineVertical = True
        Me.CmbEmployee.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbEmployee.Location = New System.Drawing.Point(525, 45)
        Me.CmbEmployee.ManagingFastMouseMoving = True
        Me.CmbEmployee.ManagingFastMouseMovingInterval = 30
        Me.CmbEmployee.Name = "CmbEmployee"
        Me.CmbEmployee.Size = New System.Drawing.Size(159, 22)
        Me.CmbEmployee.TabIndex = 14
        '
        'CmbGroup
        '
        Me.CmbGroup.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup.ColumnNum = 3
        Me.CmbGroup.ColumnWidth = "100;100;30"
        Me.CmbGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.GROUP_NAME", True))
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
        Me.CmbGroup.Location = New System.Drawing.Point(525, 71)
        Me.CmbGroup.ManagingFastMouseMoving = True
        Me.CmbGroup.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup.Name = "CmbGroup"
        Me.CmbGroup.Size = New System.Drawing.Size(159, 22)
        Me.CmbGroup.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(631, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 21)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Disp. Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(254, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 21)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Prev. Bal"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(434, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 22)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Received By"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 22)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Supplier"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(254, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 22)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Vehicle"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(435, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 21)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "B. Group"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(434, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtVehicle
        '
        Me.TxtVehicle.BackColor = System.Drawing.Color.White
        Me.TxtVehicle.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.VEHICLE", True))
        Me.TxtVehicle.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtVehicle.Location = New System.Drawing.Point(332, 45)
        Me.TxtVehicle.MaxLength = 50
        Me.TxtVehicle.Name = "TxtVehicle"
        Me.TxtVehicle.Size = New System.Drawing.Size(96, 21)
        Me.TxtVehicle.TabIndex = 12
        '
        'TxtPayables
        '
        Me.TxtPayables.BackColor = System.Drawing.Color.White
        Me.TxtPayables.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SUPPLIER_BAL1, "SV_SUPPLIER_BALANCE.SUPPLIER_BAL", True))
        Me.TxtPayables.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtPayables.ForeColor = System.Drawing.Color.Red
        Me.TxtPayables.Location = New System.Drawing.Point(332, 72)
        Me.TxtPayables.MaxLength = 50
        Me.TxtPayables.Name = "TxtPayables"
        Me.TxtPayables.ReadOnly = True
        Me.TxtPayables.Size = New System.Drawing.Size(96, 21)
        Me.TxtPayables.TabIndex = 18
        Me.TxtPayables.TabStop = False
        Me.TxtPayables.Text = "0"
        Me.TxtPayables.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_SUPPLIER_BAL1
        '
        Me.DsV_SUPPLIER_BAL1.DataSetName = "dsV_SUPPLIER_BAL"
        Me.DsV_SUPPLIER_BAL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtDispDate
        '
        Me.TxtDispDate.BackColor = System.Drawing.Color.White
        Me.TxtDispDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.DISP_DATE", True))
        Me.TxtDispDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDispDate.Location = New System.Drawing.Point(706, 18)
        Me.TxtDispDate.MaxLength = 50
        Me.TxtDispDate.Name = "TxtDispDate"
        Me.TxtDispDate.Size = New System.Drawing.Size(94, 21)
        Me.TxtDispDate.TabIndex = 8
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.P_DATE", True))
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDate.Location = New System.Drawing.Point(525, 18)
        Me.TxtDate.MaxLength = 50
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(96, 21)
        Me.TxtDate.TabIndex = 6
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'DsSUPPLIER_INFO1
        '
        Me.DsSUPPLIER_INFO1.DataSetName = "dsSUPPLIER_INFO"
        Me.DsSUPPLIER_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daSUPPLIER_INFO
        '
        Me.daSUPPLIER_INFO.DeleteCommand = Me.SqlDeleteCommand1
        Me.daSUPPLIER_INFO.InsertCommand = Me.SqlInsertCommand1
        Me.daSUPPLIER_INFO.SelectCommand = Me.SqlSelectCommand1
        Me.daSUPPLIER_INFO.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SUPPLIER_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nID", "nID"), New System.Data.Common.DataColumnMapping("sCONTACT_PERSON", "sCONTACT_PERSON"), New System.Data.Common.DataColumnMapping("sDESIGNATION", "sDESIGNATION"), New System.Data.Common.DataColumnMapping("sSUPPLIER_NAME", "sSUPPLIER_NAME"), New System.Data.Common.DataColumnMapping("sADDRESS", "sADDRESS"), New System.Data.Common.DataColumnMapping("sSUPPLIER_PH", "sSUPPLIER_PH"), New System.Data.Common.DataColumnMapping("sPERSON_PH", "sPERSON_PH"), New System.Data.Common.DataColumnMapping("sCELL_NO", "sCELL_NO"), New System.Data.Common.DataColumnMapping("sFAX_NO", "sFAX_NO"), New System.Data.Common.DataColumnMapping("sE_MAIL", "sE_MAIL"), New System.Data.Common.DataColumnMapping("sWEB_ADD", "sWEB_ADD"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("nOPEN_BAL", "nOPEN_BAL")})})
        Me.daSUPPLIER_INFO.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = resources.GetString("SqlDeleteCommand1.CommandText")
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT_PERSON", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sDESIGNATION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sDESIGNATION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sSUPPLIER_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sPERSON_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sPERSON_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCELL_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCELL_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sFAX_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sFAX_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sE_MAIL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sE_MAIL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sWEB_ADD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sWEB_ADD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nOPEN_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nOPEN_BAL", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = resources.GetString("SqlInsertCommand1.CommandText")
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, "sCONTACT_PERSON"), New System.Data.SqlClient.SqlParameter("@sDESIGNATION", System.Data.SqlDbType.VarChar, 0, "sDESIGNATION"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_NAME"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.VarChar, 0, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_PH"), New System.Data.SqlClient.SqlParameter("@sPERSON_PH", System.Data.SqlDbType.VarChar, 0, "sPERSON_PH"), New System.Data.SqlClient.SqlParameter("@sCELL_NO", System.Data.SqlDbType.VarChar, 0, "sCELL_NO"), New System.Data.SqlClient.SqlParameter("@sFAX_NO", System.Data.SqlDbType.VarChar, 0, "sFAX_NO"), New System.Data.SqlClient.SqlParameter("@sE_MAIL", System.Data.SqlDbType.VarChar, 0, "sE_MAIL"), New System.Data.SqlClient.SqlParameter("@sWEB_ADD", System.Data.SqlDbType.VarChar, 0, "sWEB_ADD"), New System.Data.SqlClient.SqlParameter("@nOPEN_BAL", System.Data.SqlDbType.Money, 0, "nOPEN_BAL")})
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
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, "sCONTACT_PERSON"), New System.Data.SqlClient.SqlParameter("@sDESIGNATION", System.Data.SqlDbType.VarChar, 0, "sDESIGNATION"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_NAME"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.VarChar, 0, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_PH"), New System.Data.SqlClient.SqlParameter("@sPERSON_PH", System.Data.SqlDbType.VarChar, 0, "sPERSON_PH"), New System.Data.SqlClient.SqlParameter("@sCELL_NO", System.Data.SqlDbType.VarChar, 0, "sCELL_NO"), New System.Data.SqlClient.SqlParameter("@sFAX_NO", System.Data.SqlDbType.VarChar, 0, "sFAX_NO"), New System.Data.SqlClient.SqlParameter("@sE_MAIL", System.Data.SqlDbType.VarChar, 0, "sE_MAIL"), New System.Data.SqlClient.SqlParameter("@sWEB_ADD", System.Data.SqlDbType.VarChar, 0, "sWEB_ADD"), New System.Data.SqlClient.SqlParameter("@nOPEN_BAL", System.Data.SqlDbType.Money, 0, "nOPEN_BAL"), New System.Data.SqlClient.SqlParameter("@Original_nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT_PERSON", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sDESIGNATION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sDESIGNATION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sSUPPLIER_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sPERSON_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sPERSON_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCELL_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCELL_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sFAX_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sFAX_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sE_MAIL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sE_MAIL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sWEB_ADD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sWEB_ADD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nOPEN_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nOPEN_BAL", System.Data.DataRowVersion.Original, Nothing)})
        '
        'DsLUP_BUSINESS_GROUP1
        '
        Me.DsLUP_BUSINESS_GROUP1.DataSetName = "dsLUP_BUSINESS_GROUP"
        Me.DsLUP_BUSINESS_GROUP1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        'DsLUP_EMPLOYEE1
        '
        Me.DsLUP_EMPLOYEE1.DataSetName = "dsLUP_EMPLOYEE"
        Me.DsLUP_EMPLOYEE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.SqlSelectCommand3.CommandText = "SELECT     GROUP_ID, SUPPLIER_ID, SUPPLIER_BAL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         SV_SUPPLIER_BALANCE"
        Me.SqlSelectCommand3.Connection = Me.SqlConnection1
        '
        'daV_SUPPLIER_BAL
        '
        Me.daV_SUPPLIER_BAL.SelectCommand = Me.SqlSelectCommand3
        Me.daV_SUPPLIER_BAL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SV_SUPPLIER_BALANCE", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID"), New System.Data.Common.DataColumnMapping("SUPPLIER_ID", "SUPPLIER_ID"), New System.Data.Common.DataColumnMapping("SUPPLIER_BAL", "SUPPLIER_BAL")})})
        '
        'SqlConnection2
        '
        Me.SqlConnection2.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection2.FireInfoMessageEventOnUserErrors = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(831, 43)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Purchase Invoice"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlSelectCommand5
        '
        Me.SqlSelectCommand5.CommandText = resources.GetString("SqlSelectCommand5.CommandText")
        Me.SqlSelectCommand5.Connection = Me.SqlConnection1
        '
        'daPURCHASE_MASTER
        '
        Me.daPURCHASE_MASTER.SelectCommand = Me.SqlSelectCommand5
        Me.daPURCHASE_MASTER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_PURCHASE_MASTER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("PINV_NO", "PINV_NO"), New System.Data.Common.DataColumnMapping("SUPPLIER_NAME", "SUPPLIER_NAME"), New System.Data.Common.DataColumnMapping("CASH_SUPPLIER", "CASH_SUPPLIER"), New System.Data.Common.DataColumnMapping("P_DATE", "P_DATE"), New System.Data.Common.DataColumnMapping("DISP_DATE", "DISP_DATE"), New System.Data.Common.DataColumnMapping("VEHICLE", "VEHICLE"), New System.Data.Common.DataColumnMapping("TOTAL_BILL", "TOTAL_BILL"), New System.Data.Common.DataColumnMapping("DISC_RS", "DISC_RS"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("DISC_OTHER", "DISC_OTHER"), New System.Data.Common.DataColumnMapping("OTHER_DESC", "OTHER_DESC"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL"), New System.Data.Common.DataColumnMapping("EMP_NAME", "EMP_NAME"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("nFREIGHT", "nFREIGHT"), New System.Data.Common.DataColumnMapping("nUNLOADING", "nUNLOADING"), New System.Data.Common.DataColumnMapping("sTR_NO", "sTR_NO"), New System.Data.Common.DataColumnMapping("nTR_QTY", "nTR_QTY"), New System.Data.Common.DataColumnMapping("sREMARKS", "sREMARKS"), New System.Data.Common.DataColumnMapping("LOGIN_USER", "LOGIN_USER"), New System.Data.Common.DataColumnMapping("RECV_PER", "RECV_PER"), New System.Data.Common.DataColumnMapping("RECV_RS", "RECV_RS"), New System.Data.Common.DataColumnMapping("sSUP_INV_NO", "sSUP_INV_NO")})})
        '
        'SqlSelectCommand7
        '
        Me.SqlSelectCommand7.CommandText = resources.GetString("SqlSelectCommand7.CommandText")
        Me.SqlSelectCommand7.Connection = Me.SqlConnection1
        '
        'daV_PURCHASE_DETAIL
        '
        Me.daV_PURCHASE_DETAIL.SelectCommand = Me.SqlSelectCommand7
        Me.daV_PURCHASE_DETAIL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_PURCHASE_DETAIL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("PINV_NO", "PINV_NO"), New System.Data.Common.DataColumnMapping("ITEM_CODE", "ITEM_CODE"), New System.Data.Common.DataColumnMapping("ITEM_NAME", "ITEM_NAME"), New System.Data.Common.DataColumnMapping("BATCH_NO", "BATCH_NO"), New System.Data.Common.DataColumnMapping("UNIT_COST", "UNIT_COST"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("PPP", "PPP"), New System.Data.Common.DataColumnMapping("QTY_PKS", "QTY_PKS"), New System.Data.Common.DataColumnMapping("QTY_PCS", "QTY_PCS"), New System.Data.Common.DataColumnMapping("QTY_BONUS", "QTY_BONUS"), New System.Data.Common.DataColumnMapping("QTY_TOT_PCS", "QTY_TOT_PCS"), New System.Data.Common.DataColumnMapping("TOTAL_VALUE", "TOTAL_VALUE"), New System.Data.Common.DataColumnMapping("SCM_ITEM_CODE", "SCM_ITEM_CODE"), New System.Data.Common.DataColumnMapping("SCM_ITEM", "SCM_ITEM"), New System.Data.Common.DataColumnMapping("SCM_QTY", "SCM_QTY"), New System.Data.Common.DataColumnMapping("SALE_TAX", "SALE_TAX")})})
        '
        'SqlSelectCommand6
        '
        Me.SqlSelectCommand6.CommandText = resources.GetString("SqlSelectCommand6.CommandText")
        Me.SqlSelectCommand6.Connection = Me.SqlConnection1
        '
        'daV_SUPPLIER_PAYMENT
        '
        Me.daV_SUPPLIER_PAYMENT.SelectCommand = Me.SqlSelectCommand6
        Me.daV_SUPPLIER_PAYMENT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_SUPPLIER_PAYMENT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("SUPPLIER_ID", "SUPPLIER_ID"), New System.Data.Common.DataColumnMapping("SUPPLIER_NAME", "SUPPLIER_NAME"), New System.Data.Common.DataColumnMapping("PMT_DATE", "PMT_DATE"), New System.Data.Common.DataColumnMapping("CASH_AMT", "CASH_AMT"), New System.Data.Common.DataColumnMapping("CHQ_NO", "CHQ_NO"), New System.Data.Common.DataColumnMapping("CHQ_TYPE", "CHQ_TYPE"), New System.Data.Common.DataColumnMapping("CHQ_DATE", "CHQ_DATE"), New System.Data.Common.DataColumnMapping("BANK_AMT", "BANK_AMT"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("BANK_NAME", "BANK_NAME"), New System.Data.Common.DataColumnMapping("PINV_NO", "PINV_NO"), New System.Data.Common.DataColumnMapping("USER_NAME", "USER_NAME"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("TOT_PAYMENT", "TOT_PAYMENT"), New System.Data.Common.DataColumnMapping("EMP_NAME", "EMP_NAME"), New System.Data.Common.DataColumnMapping("DESCRIPTION", "DESCRIPTION")})})
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox7.Controls.Add(Me.TxtRecvDisc)
        Me.GroupBox7.Controls.Add(Me.Label29)
        Me.GroupBox7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(375, 413)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(244, 77)
        Me.GroupBox7.TabIndex = 5
        Me.GroupBox7.TabStop = False
        '
        'TxtRecvDisc
        '
        Me.TxtRecvDisc.BackColor = System.Drawing.Color.White
        Me.TxtRecvDisc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsPURCHASE_MASTER1, "V_PURCHASE_MASTER.RECV_PER", True))
        Me.TxtRecvDisc.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtRecvDisc.Location = New System.Drawing.Point(135, 19)
        Me.TxtRecvDisc.MaxLength = 50
        Me.TxtRecvDisc.Name = "TxtRecvDisc"
        Me.TxtRecvDisc.Size = New System.Drawing.Size(102, 21)
        Me.TxtRecvDisc.TabIndex = 1
        Me.TxtRecvDisc.Text = "0"
        Me.TxtRecvDisc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(6, 19)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(123, 23)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Receivable Disc %"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItemsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(831, 24)
        Me.MenuStrip1.TabIndex = 8
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
        'daV_STOCK_NET_TOT
        '
        Me.daV_STOCK_NET_TOT.SelectCommand = Me.SqlSelectCommand4
        Me.daV_STOCK_NET_TOT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_STOCK_NET_TOTAL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("BATCH", "BATCH"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL")})})
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = "SELECT     CODE, BATCH, NET_TOTAL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_STOCK_NET_TOTAL"
        Me.SqlSelectCommand4.Connection = Me.SqlConnection2
        '
        'DsV_PURCHASE_DETAIL1
        '
        Me.DsV_PURCHASE_DETAIL1.DataSetName = "dsV_PURCHASE_DETAIL"
        Me.DsV_PURCHASE_DETAIL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daNS_DEFAULT
        '
        Me.daNS_DEFAULT.DeleteCommand = Me.SqlCommand5
        Me.daNS_DEFAULT.InsertCommand = Me.SqlCommand6
        Me.daNS_DEFAULT.SelectCommand = Me.SqlCommand7
        Me.daNS_DEFAULT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "NS_DEFAULT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("GROUP", "GROUP"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("S_MAN", "S_MAN"), New System.Data.Common.DataColumnMapping("P_MAN", "P_MAN"), New System.Data.Common.DataColumnMapping("D_MAN", "D_MAN"), New System.Data.Common.DataColumnMapping("R_MAN", "R_MAN"), New System.Data.Common.DataColumnMapping("CLIENT", "CLIENT"), New System.Data.Common.DataColumnMapping("CLIENT_TYPE", "CLIENT_TYPE"), New System.Data.Common.DataColumnMapping("CLIENT_CAT", "CLIENT_CAT"), New System.Data.Common.DataColumnMapping("CLIENT_GD", "CLIENT_GD"), New System.Data.Common.DataColumnMapping("ZONE", "ZONE"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("EXP_SUB_HEAD", "EXP_SUB_HEAD"), New System.Data.Common.DataColumnMapping("PRINTER", "PRINTER"), New System.Data.Common.DataColumnMapping("RPT_TITLE", "RPT_TITLE"), New System.Data.Common.DataColumnMapping("RPT_WARRANTY", "RPT_WARRANTY")})})
        Me.daNS_DEFAULT.UpdateCommand = Me.SqlCommand8
        '
        'SqlCommand5
        '
        Me.SqlCommand5.CommandText = resources.GetString("SqlCommand5.CommandText")
        Me.SqlCommand5.Connection = Me.SqlConnection1
        Me.SqlCommand5.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_GROUP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_GROUP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_BANK_ACC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_BANK_ACC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_S_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_S_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_P_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_P_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_D_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_D_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_R_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_R_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_CAT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_GD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_GD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ZONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ZONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ROUTE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ROUTE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_AREA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_AREA", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_EXP_SUB_HEAD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_PRINTER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_PRINTER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_TITLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_TITLE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_WARRANTY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand6
        '
        Me.SqlCommand6.CommandText = resources.GetString("SqlCommand6.CommandText")
        Me.SqlCommand6.Connection = Me.SqlConnection1
        Me.SqlCommand6.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@GROUP", System.Data.SqlDbType.VarChar, 0, "GROUP"), New System.Data.SqlClient.SqlParameter("@BANK_ACC", System.Data.SqlDbType.VarChar, 0, "BANK_ACC"), New System.Data.SqlClient.SqlParameter("@S_MAN", System.Data.SqlDbType.VarChar, 0, "S_MAN"), New System.Data.SqlClient.SqlParameter("@P_MAN", System.Data.SqlDbType.VarChar, 0, "P_MAN"), New System.Data.SqlClient.SqlParameter("@D_MAN", System.Data.SqlDbType.VarChar, 0, "D_MAN"), New System.Data.SqlClient.SqlParameter("@R_MAN", System.Data.SqlDbType.VarChar, 0, "R_MAN"), New System.Data.SqlClient.SqlParameter("@CLIENT", System.Data.SqlDbType.VarChar, 0, "CLIENT"), New System.Data.SqlClient.SqlParameter("@CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, "CLIENT_TYPE"), New System.Data.SqlClient.SqlParameter("@CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, "CLIENT_CAT"), New System.Data.SqlClient.SqlParameter("@CLIENT_GD", System.Data.SqlDbType.VarChar, 0, "CLIENT_GD"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 0, "ZONE"), New System.Data.SqlClient.SqlParameter("@ROUTE", System.Data.SqlDbType.VarChar, 0, "ROUTE"), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 0, "AREA"), New System.Data.SqlClient.SqlParameter("@EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, "EXP_SUB_HEAD"), New System.Data.SqlClient.SqlParameter("@PRINTER", System.Data.SqlDbType.VarChar, 0, "PRINTER"), New System.Data.SqlClient.SqlParameter("@RPT_TITLE", System.Data.SqlDbType.VarChar, 0, "RPT_TITLE"), New System.Data.SqlClient.SqlParameter("@RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, "RPT_WARRANTY"), New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing)})
        '
        'SqlCommand7
        '
        Me.SqlCommand7.CommandText = resources.GetString("SqlCommand7.CommandText")
        Me.SqlCommand7.Connection = Me.SqlConnection1
        '
        'SqlCommand8
        '
        Me.SqlCommand8.CommandText = resources.GetString("SqlCommand8.CommandText")
        Me.SqlCommand8.Connection = Me.SqlConnection1
        Me.SqlCommand8.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@GROUP", System.Data.SqlDbType.VarChar, 0, "GROUP"), New System.Data.SqlClient.SqlParameter("@BANK_ACC", System.Data.SqlDbType.VarChar, 0, "BANK_ACC"), New System.Data.SqlClient.SqlParameter("@S_MAN", System.Data.SqlDbType.VarChar, 0, "S_MAN"), New System.Data.SqlClient.SqlParameter("@P_MAN", System.Data.SqlDbType.VarChar, 0, "P_MAN"), New System.Data.SqlClient.SqlParameter("@D_MAN", System.Data.SqlDbType.VarChar, 0, "D_MAN"), New System.Data.SqlClient.SqlParameter("@R_MAN", System.Data.SqlDbType.VarChar, 0, "R_MAN"), New System.Data.SqlClient.SqlParameter("@CLIENT", System.Data.SqlDbType.VarChar, 0, "CLIENT"), New System.Data.SqlClient.SqlParameter("@CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, "CLIENT_TYPE"), New System.Data.SqlClient.SqlParameter("@CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, "CLIENT_CAT"), New System.Data.SqlClient.SqlParameter("@CLIENT_GD", System.Data.SqlDbType.VarChar, 0, "CLIENT_GD"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 0, "ZONE"), New System.Data.SqlClient.SqlParameter("@ROUTE", System.Data.SqlDbType.VarChar, 0, "ROUTE"), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 0, "AREA"), New System.Data.SqlClient.SqlParameter("@EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, "EXP_SUB_HEAD"), New System.Data.SqlClient.SqlParameter("@PRINTER", System.Data.SqlDbType.VarChar, 0, "PRINTER"), New System.Data.SqlClient.SqlParameter("@RPT_TITLE", System.Data.SqlDbType.VarChar, 0, "RPT_TITLE"), New System.Data.SqlClient.SqlParameter("@RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, "RPT_WARRANTY"), New System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_GROUP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_GROUP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_BANK_ACC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_BANK_ACC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_S_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_S_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_P_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_P_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_D_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_D_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_R_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_R_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_CAT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_GD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_GD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ZONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ZONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ROUTE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ROUTE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_AREA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_AREA", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_EXP_SUB_HEAD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_PRINTER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_PRINTER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_TITLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_TITLE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_WARRANTY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsNS_DEFAULT1
        '
        Me.DsNS_DEFAULT1.DataSetName = "dsNS_DEFAULT"
        Me.DsNS_DEFAULT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmPURCHASE
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(831, 605)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmPURCHASE"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Purchase Invoice"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_STOCK_NET_TOT1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.DsPURCHASE_MASTER1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_SUPPLIER_PAYMENT1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsV_SUPPLIER_BAL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSUPPLIER_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DsV_PURCHASE_DETAIL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsNS_DEFAULT1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Public CASH_AMT, BANK_AMT As Double
    Public CASH_PAY As Boolean = False, BANK_PAY As Boolean = False, BOTH_PAY As Boolean = False
    Public CHEQ_NO, CHEQ_DATE, DESCRIPTION, CHEQ_TYPE, BANK_ACC, Pmt_Date, PINV_NO As String
    Public P_Inv As String
#End Region

#Region "FORM CONTROL"

    Private Sub frmPURCHASE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.BttnSave.Text = "&Update" And Me.BttnSave.Enabled = True Then
            MsgBox("Can't close without Updating OR Cancelling Invoice", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        ElseIf Me.BttnSave.Text = "&Save" And Me.BttnSave.Enabled = True Then
            MsgBox("Can't close without Saving OR Cancelling Invoice", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        End If
    End Sub
    Private Sub frmPURCHASE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Supplier()
        Me.FillComboBox_Employee()
        Me.FillComboBox_Group()

        Me.Disable_All()
        Me.BttnPrev.Enabled = False
        Me.BttnPrint.Enabled = False
        Me.BttnSave.Enabled = False

        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.TxtDispDate.Text = Date.Now.ToString("dd-MMM-yyyy")

        Dim StrSql As String = "SELECT nID AS ID, sBUSINESS_GP AS [GROUP], sBANK_ACC AS BANK_ACC, sS_MAN AS S_MAN, sP_MAN AS P_MAN, sD_MAN AS D_MAN, sR_MAN AS R_MAN, sCLIENT AS CLIENT, sCLIENT_TYPE AS CLIENT_TYPE, sCLIENT_CAT AS CLIENT_CAT, sCLIENT_GD AS CLIENT_GD, sZONE AS ZONE, sROUTE AS ROUTE, sAREA AS AREA, sEXP_SUB_HEAD AS EXP_SUB_HEAD, sPRINTER AS PRINTER, sREPORT_TITLE AS RPT_TITLE, sREPORT_WARRANTY AS RPT_WARRANTY FROM NS_DEFAULT"
        Dim CmdSql As New SDS.SqlCommand(StrSql, Me.SqlConnection1)
        Me.daNS_DEFAULT = New SDS.SqlDataAdapter(CmdSql)
        Me.daNS_DEFAULT.Fill(Me.DsNS_DEFAULT1.NS_DEFAULT)
        Me.Default_Setting()
        'Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmPURCHASE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.GotFocus, TxtCashSupplier.GotFocus, TxtDate.GotFocus, TxtDescription.GotFocus, TxtDiscount.GotFocus, TxtDiscPercent.GotFocus, TxtDispDate.GotFocus, TxtFreight.GotFocus, TxtInvBalance.GotFocus, TxtNetTotal.GotFocus, TxtOtherDisc.GotFocus, TxtPayables.GotFocus, TxtPayment.GotFocus, TxtSupplierBal.GotFocus, TxtTotal.GotFocus, TxtTotalItems.GotFocus, TxtTRno.GotFocus, TxtTRqty.GotFocus, TxtUnloading.GotFocus, TxtVehicle.GotFocus, TxtRemarks.GotFocus, TxtSup_Invoice.GotFocus, TxtRecvDisc.GotFocus
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

    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.LostFocus, TxtCashSupplier.LostFocus, TxtDate.LostFocus, TxtDescription.LostFocus, TxtDiscount.LostFocus, TxtDiscPercent.LostFocus, TxtDispDate.LostFocus, TxtFreight.LostFocus, TxtInvBalance.LostFocus, TxtNetTotal.LostFocus, TxtOtherDisc.LostFocus, TxtPayables.LostFocus, TxtPayment.LostFocus, TxtSupplierBal.LostFocus, TxtTotal.LostFocus, TxtTotalItems.LostFocus, TxtTRno.LostFocus, TxtTRqty.LostFocus, TxtUnloading.LostFocus, TxtVehicle.LostFocus, TxtRemarks.LostFocus, TxtSup_Invoice.LostFocus, TxtRecvDisc.LostFocus
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
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtDiscount.KeyPress, TxtFreight.KeyPress, TxtInvBalance.KeyPress, TxtNetTotal.KeyPress, TxtOtherDisc.KeyPress, TxtPayables.KeyPress, TxtPayment.KeyPress, TxtSupplierBal.KeyPress, TxtTotal.KeyPress, TxtUnloading.KeyPress, TxtDiscPercent.KeyPress, TxtRecvDisc.KeyPress
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

    'Invoice & Supplier Balance Calculation
    Private Sub TxtPayment_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPayment.TextChanged, TxtNetTotal.TextChanged, TxtPayables.TextChanged
        'If Me.Search_Inv = False Then
        On Error GoTo Fix
        If Me.TxtPayment.TextLength > 0 Then
            Me.TxtPayment.Text = Decimal.Round(CDec(Me.TxtPayment.Text), 2)
        End If

        Me.TxtInvBalance.Text = Val(Me.TxtNetTotal.Text) - Val(Me.TxtPayment.Text)
        Me.TxtSupplierBal.Text = Val(Me.TxtPayables.Text) + Val(Me.TxtInvBalance.Text)

        Me.TxtInvBalance.Text = Decimal.Round(CDec(Me.TxtInvBalance.Text), 2)
        Me.TxtSupplierBal.Text = Decimal.Round(CDec(Me.TxtSupplierBal.Text), 2)

        If Val(Me.TxtNetTotal.Text) > 0 Then
            Me.BttnPayment.Enabled = True
        Else
            Me.BttnPayment.Enabled = False
        End If

        If bPMT = True Then
            Me.Fill_Payment_Data()
            Exit Sub
        End If

Fix:
        'End If

    End Sub

    'Fill data for Modification
    Private Sub TxtInvoice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.TextChanged
        If Me.Search_Inv = True Then

            'FILL MASTER RECORD
            Me.Fill_Master_Data()

            'FILL DETAIL RECORD
            Me.Fill_Detail_Data()

            Me.Search_Inv = False

            'FILL TOTAL PAYMENT
            Me.Fill_Payment_Data()

            Dim StrSupplier As String = Me.CmbSupplier.Text
            Me.CmbSupplier.SelectedIndex = -1
            Me.CmbSupplier.SelectedIndex = Me.CmbSupplier.FindString(StrSupplier)

            'MsgBox(Me.TxtInvoice.Text)
        End If

    End Sub
#End Region

#Region "ComboBox Controls"
    Private Sub CmbSupplier_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbSupplier.SelectedIndexChanged, CmbGroup.SelectedIndexChanged
        Try
            Dim Str1 As String = "SELECT GROUP_ID, SUPPLIER_ID, CONVERT(NUMERIC(18,2),SUPPLIER_BAL) AS SUPPLIER_BAL FROM SV_SUPPLIER_BALANCE WHERE SUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & " AND GROUP_ID=" & Val(Me.CmbGroup.SelectedItem.Col3) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_SUPPLIER_BAL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_SUPPLIER_BAL1.Clear()
            Me.daV_SUPPLIER_BAL.Fill(Me.DsV_SUPPLIER_BAL1.SV_SUPPLIER_BALANCE)
        Catch ex As Exception

        End Try
    End Sub
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbEmployee.GotFocus, CmbGroup.GotFocus, CmbSupplier.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbEmployee.LostFocus, CmbGroup.LostFocus, CmbSupplier.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "Select Item Controls"
    Private Sub SelectItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectItemToolStripMenuItem.Click
        On Error GoTo Fix
        frmSELECT_ITEM_BATCH.TxtCompany.Text = Nothing
        frmSELECT_ITEM_BATCH.TxtItem.Text = Nothing
        frmSELECT_ITEM_BATCH.FrmStr = "Purchase"
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
                Me.DsV_STOCK_NET_TOT1.Clear()

                Me.LblB_Pcs.Text = 0
                Me.LblPPP.Text = 0
                Me.LblCostPcs.Text = 0
                Me.LblRate.Text = 0
                Me.LblRetail.Text = 0
                Me.LblStock.Text = 0

                'FILL TOP LABLES OF DATAGRID
                If Not Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value Is Nothing Then
                    Dim Str1 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, sCLAIMABLE, sSTATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM WHERE nCODE=" & Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value) & ""
                    Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
                    Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

                    Me.DsLUP_ITEM1.Clear()
                    Me.daLUP_ITEM.Fill(Me.DsLUP_ITEM1.V_LUP_ITEM)

                    If Me.DsLUP_ITEM1.V_LUP_ITEM.Count = 0 Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value = Nothing
                        'SET FOCUS TO ColCode IS PENDING

                        Me.LblB_Pcs.Text = 0
                        Me.LblPPP.Text = 0
                        Me.LblCostPcs.Text = 0
                        Me.LblRate.Text = 0
                        Me.LblRetail.Text = 0
                        Me.LblStock.Text = 0

                        Me.SelectItemToolStripMenuItem_Click(sender, e)
                        Exit Sub
                    End If

                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColName").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(1).ToString()
                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColPPP").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(3).ToString()
                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColSaleTax").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(11).ToString()

                    Dim Itm_Code As String = Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value

                    If Not Itm_Code = ItemCode_Old Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColCost").Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(6).ToString()

                    End If

                    Me.LblCostPcs.Text = Val(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(6).ToString()) / Val(Me.LblPPP.Text)

                    If Val(Me.LblPPP.Text) <= 0 Then
                        MsgBox("Please confirm 'Item Code' or Item Detail", MsgBoxStyle.Exclamation, "(NS) - Error!")

                    ElseIf Not Me.DataGridView1.Rows(e.RowIndex).Cells("ColCost").Value Is Nothing Then
                        Dim Cost, PPP, Pks, Pcs, Line_Tot, P_age, S_Tax As Double
                        Cost = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCost").Value)
                        PPP = Val(Me.LblPPP.Text)
                        Pks = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPack").Value)
                        Pcs = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPiece").Value)
                        P_age = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPercentage").Value)
                        S_Tax = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColSaleTax").Value)

                        Line_Tot = ((Cost / PPP) * ((Pks * PPP) + Pcs))
                        S_Tax = (Line_Tot * S_Tax) / 100
                        Line_Tot = (Line_Tot - ((Line_Tot * P_age) / 100) + S_Tax)
                        Line_Tot = Decimal.Round(CDec(Line_Tot), 2)

                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColTotal").Value = Line_Tot

                        Dim i As Integer
                        Me.TxtTotal.Text = "0.00"

                        For i = 0 To Me.DataGridView1.Rows.Count - 1
                            Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value)
                        Next

                    End If

                    Me.TxtTotalItems.Text = Me.DataGridView1.Rows.Count - 1

                    Dim Str2 As String = "SELECT CODE, BATCH, SUM(NET_TOTAL) AS NET_TOTAL FROM V_STOCK_NET_TOTAL WHERE CODE=" & Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value) & ""
                    Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection2)
                    Me.daV_STOCK_NET_TOT = New SDS.SqlDataAdapter(SqlCmd2)

                    Me.DsV_STOCK_NET_TOT1.Clear()
                    Me.daV_STOCK_NET_TOT.Fill(Me.DsV_STOCK_NET_TOT1.V_STOCK_NET_TOTAL)

                Else
                    Me.LblB_Pcs.Text = 0
                    Me.LblPPP.Text = 0
                    Me.LblCostPcs.Text = 0
                    Me.LblRate.Text = 0
                    Me.LblRetail.Text = 0
                    Me.LblStock.Text = 0
                End If

            Catch ex As Exception

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
        If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

            Me.Null_Focus()

        ElseIf MsgBox("Are you sure to Delete Item?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Deleting Item?") = MsgBoxResult.Yes Then
            Me.asDelete.DeleteValue_NoErr("DELETE FROM PURCHASE_DETAIL WHERE nID=" & Val(Me.DataGridView1.Rows(e.Row.Index).Cells("ColSR").Value) & "")

            Me.asUpdate.UpdateValue_NoErr("UPDATE PURCHASE_MASTER SET sSUP_INV_NO='" & Me.TxtSup_Invoice.Text & "', nSUPPLIER_ID='" & Val(Me.CmbSupplier.SelectedItem.Col3) & "', sCASH_SUPPLIER='" & Me.TxtCashSupplier.Text & "', dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', dDISP_DATE='" & CDate(Me.TxtDispDate.Text).ToString("MM-dd-yyyy") & "', sVEHICLE='" & Me.TxtVehicle.Text & "', nFREIGHT=" & Val(Me.TxtFreight.Text) & ", nUNLOADING=" & Val(Me.TxtUnloading.Text) & ", sTR_NO='" & Me.TxtTRno.Text & "', nTR_QTY=" & Val(Me.TxtTRqty.Text) & ", nTOTAL_BILL=" & Val(Me.TxtTotal.Text) & ", nDISCOUNT=" & Val(Me.TxtDiscount.Text) & ", nDISC_PERCENT=" & Val(Me.TxtDiscPercent.Text) & ", nOTHERS=" & Val(Me.TxtOtherDisc.Text) & ", sOTHER_DESC='" & Me.TxtDescription.Text & "', nNET_TOTAL= " & Val(Me.TxtNetTotal.Text) & ", nRECV_DISC=" & Val(Me.TxtRecvDisc.Text) & ", nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nPINV_NO='" & Me.TxtInvoice.Text & "'")

            Me.BttnNew.Enabled = False

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
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        If Me.BttnNew.Text = "&New" Then
            Me.Enable_All()

            Me.TxtPayment.Text = "0.00"

            Pmt_Date = Nothing
            CASH_AMT = 0
            CHEQ_NO = Nothing
            CHEQ_TYPE = Nothing
            CHEQ_DATE = Nothing
            BANK_AMT = 0
            BANK_ACC = Nothing
            PINV_NO = Nothing
            DESCRIPTION = Nothing

            Me.BOTH_PAY = False
            Me.BANK_PAY = False
            Me.CASH_PAY = False

            Me.BttnSearch_Inv.Enabled = False
            Me.BttnPrev.Enabled = False
            Me.BttnPrint.Enabled = False
            Me.BttnPayment.Enabled = False
            Me.BttnSave.Enabled = True
            Me.BttnSave.Text = "&Save"
            Me.BttnClose.Enabled = False

            Me.CancelButton = Me.BttnNew

            Me.Clear_All()
            Me.TxtInvoice.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nPINV_NO) FROM PURCHASE_MASTER") + 1
            Me.BttnNew.Text = "Ca&ncel"

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

    Private Sub BttnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPayment.Click

        ''FILL TOTAL PAYMENT
        'Me.Fill_Payment_Data()

        'If Not PINV_NO = Nothing Then

        frmPINV_PAYMENT.TxtCashPmt.Text = Me.CASH_AMT
        frmPINV_PAYMENT.TxtBankPmt.Text = Me.BANK_AMT

        frmPINV_PAYMENT.BnkACC = Me.BANK_ACC
        frmPINV_PAYMENT.TxtChequeNo.Text = Me.CHEQ_NO
        frmPINV_PAYMENT.TxtChequeDate.Text = Me.CHEQ_DATE
        frmPINV_PAYMENT.TxtChequeType.Text = Me.CHEQ_TYPE
        frmPINV_PAYMENT.TxtDescription.Text = Me.DESCRIPTION

        'Else
        'frmPINV_PAYMENT.TxtCashPmt.Text = "0.00"
        'frmPINV_PAYMENT.TxtBankPmt.Text = "0.00"

        'frmPINV_PAYMENT.CmbBankAccount.SelectedIndex = -1
        'frmPINV_PAYMENT.TxtChequeNo.Text = Nothing
        'frmPINV_PAYMENT.TxtChequeDate.Text = Nothing
        'frmPINV_PAYMENT.TxtChequeType.Text = Nothing

        'End If

        On Error GoTo Fix

        frmPINV_PAYMENT.ShowDialog(Me)

Fix:
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM PURCHASE_MASTER WHERE nPINV_NO=" & Val(Me.TxtInvoice.Text) & "")

        If Me.TxtDescription.Text = "Other's Description Here!" Then
            Me.TxtDescription.Text = Nothing
        End If

        If Me.BttnSave.Text = "&Save" Then

            If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

                Me.Null_Focus()

            ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) <= 0 Then
                MsgBox("Please enter atleast one Item to save Invoice!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.DataGridView1.Focus()

            ElseIf Me.asSELECT.pFlg1 = False Then
                If Val(Me.TxtInvBalance.Text) < 0 Then
                    MsgBox("Can't save!" & vbCrLf & "Payment is more then Invoice Total", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                    Me.BttnPayment.Focus()

                Else
                    'INSERT VALUES IN PURCHASE MASTER
                    Me.asInsert.SaveValue("INSERT INTO PURCHASE_MASTER(nPINV_NO, sSUP_INV_NO, nSUPPLIER_ID, sCASH_SUPPLIER, dDATE, dDISP_DATE, sVEHICLE, nFREIGHT, nUNLOADING, sTR_NO, nTR_QTY, nTOTAL_BILL, nDISCOUNT, nDISC_PERCENT, nOTHERS, sOTHER_DESC, nNET_TOTAL, nRECV_DISC, nEMPLOYEE_CODE, nLOGIN_ID, nBUSINESS_CODE, sREMARKS) VALUES(" & Val(Me.TxtInvoice.Text) & ", '" & Me.TxtSup_Invoice.Text & "'," & Val(Me.CmbSupplier.SelectedItem.Col3) & ",'" & Me.TxtCashSupplier.Text & "','" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "','" & CDate(Me.TxtDispDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.TxtVehicle.Text & "', " & Val(Me.TxtFreight.Text) & ", " & Val(Me.TxtUnloading.Text) & ", '" & Me.TxtTRno.Text & "', " & Val(Me.TxtTRqty.Text) & "," & Val(Me.TxtTotal.Text) & "," & Val(Me.TxtDiscount.Text) & "," & Val(Me.TxtDiscPercent.Text) & "," & Val(Me.TxtOtherDisc.Text) & ",'" & Me.TxtDescription.Text & "', " & Val(Me.TxtNetTotal.Text) & ", " & Val(Me.TxtRecvDisc.Text) & "," & Val(Me.CmbEmployee.SelectedItem.Col3) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ",'" & Me.TxtRemarks.Text & "')")

                    Dim i As Integer
                    For i = 0 To Me.DataGridView1.Rows.Count - 2
                        Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value)
                        Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value)
                        Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value)
                        Dim Bonus As Double = Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value)
                        Dim Tot_Pcs As Double
                        Tot_Pcs = (Pks * PPP) + (Pcs + Bonus)

                        'INSERT VALUES IN PURCHASE DETAIL
                        Me.asInsert.SaveValue("INSERT INTO PURCHASE_DETAIL (nPINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, dDATE)VALUES(" & Val(Me.TxtInvoice.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")

                    Next

                    If Me.CASH_PAY = True Then
                        If Me.CASH_AMT > 0 Then
                            Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, nCASH_AMOUNT, nPINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ",'" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "'," & Me.CASH_AMT & ",'" & Me.TxtInvoice.Text & "',10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbEmployee.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")
                        End If

                    ElseIf Me.BANK_PAY = True Then
                        If Me.BANK_AMT > 0 Then
                            Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, sCHEQUE_NO,sCHEQUE_TYPE, dCHEQUE_DATE, nCHECK_AMOUNT, sACCOUNT_CODE, nPINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', " & Me.BANK_AMT & ",'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbEmployee.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")
                        End If

                    ElseIf Me.BOTH_PAY = True Then
                        If Me.CASH_AMT > 0 And Me.BANK_AMT > 0 Then
                            Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO,sCHEQUE_TYPE, dCHEQUE_DATE, nCHECK_AMOUNT, sACCOUNT_CODE, nPINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Me.CASH_AMT & ", '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', " & Me.BANK_AMT & ",'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbEmployee.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")
                        End If

                    Else
                        MsgBox("Credit Purchase Invoice Saved!", MsgBoxStyle.Information, "(NS) - Credit Invoice!")

                    End If

                    Me.BttnPrev.Enabled = True
                    Me.BttnPrint.Enabled = True
                    Me.BttnSearch_Inv.Enabled = True
                    Me.BttnPayment.Enabled = False
                    Me.BttnNew.Text = "&New"
                    Me.BttnSave.Enabled = False
                    Me.BttnClose.Enabled = True

                End If


            ElseIf Me.asSELECT.pFlg1 = True Then
                MsgBox("This Invoice # " & Val(Me.TxtInvoice.Text) & " is Already Exist. " & vbCrLf & "To modify this invoice please click on 'Search Invoice' Button", MsgBoxStyle.Exclamation, "(NS) - Already Exist!")

            End If

            'UPDATE PURCHASE INVOICE
        ElseIf Me.BttnSave.Text = "&Update" Then
            If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.Null_Focus()

            ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) <= 0 Then
                MsgBox("Please enter atleast one Item to save Invoice!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.DataGridView1.Focus()

            ElseIf Me.asSELECT.pFlg1 = True Then


                If Val(Me.TxtInvBalance.Text) < 0 Then
                    MsgBox("Can't save!" & vbCrLf & "Payment is more then Invoice Total", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                    Me.BttnPayment.Focus()

                Else
                    'UPDATE VALUES IN PURCHASE MASTER
                    Me.asUpdate.UpdateValue("UPDATE PURCHASE_MASTER SET sSUP_INV_NO=" & Me.TxtSup_Invoice.Text & ", nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", sCASH_SUPPLIER='" & Me.TxtCashSupplier.Text & "', dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', dDISP_DATE='" & CDate(Me.TxtDispDate.Text).ToString("MM-dd-yyyy") & "', sVEHICLE='" & Me.TxtVehicle.Text & "', nFREIGHT=" & Val(Me.TxtFreight.Text) & ", nUNLOADING=" & Val(Me.TxtUnloading.Text) & ", sTR_NO='" & Me.TxtTRno.Text & "', nTR_QTY=" & Val(Me.TxtTRqty.Text) & ", nTOTAL_BILL=" & Val(Me.TxtTotal.Text) & ", nDISCOUNT=" & Val(Me.TxtDiscount.Text) & ", nDISC_PERCENT=" & Val(Me.TxtDiscPercent.Text) & ", nOTHERS=" & Val(Me.TxtOtherDisc.Text) & ", sOTHER_DESC='" & Me.TxtDescription.Text & "', nNET_TOTAL= " & Val(Me.TxtNetTotal.Text) & ", nRECV_DISC=" & Val(Me.TxtRecvDisc.Text) & ", nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nPINV_NO='" & Me.TxtInvoice.Text & "'")

                    Dim i As Integer
                    For i = 0 To Me.DataGridView1.Rows.Count - 2
                        Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value)
                        Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value)
                        Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value)
                        Dim Bonus As Double = Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value)
                        Dim Tot_Pcs As Double
                        Tot_Pcs = (Pks * PPP) + (Pcs + Bonus)

                        If Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then
                            'INSERT VALUES IN PURCHASE DETAIL
                            Me.asInsert.SaveValue("INSERT INTO PURCHASE_DETAIL (nPINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, dDATE)VALUES(" & Val(Me.TxtInvoice.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")

                        ElseIf Not Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then

                            'UPDATE VALUES IN PURCHASE DETAIL
                            Me.asUpdate.UpdateValue("UPDATE PURCHASE_DETAIL SET nPINV_NO=" & Val(Me.TxtInvoice.Text) & ", nITEM_CODE=" & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", sBATCH_NO='" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', nUNIT_COST=" & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", nDISC_PER=" & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", nSALE_TAX=" & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & ", nPPP=" & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", nQTY_PKS=" & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", nQTY_PCS=" & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", nQTY_BONUS=" & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", nQTY_Tot_PCS=" & Tot_Pcs & ", nTOTAL_VALUE=" & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "' WHERE nID=" & Val(Me.DataGridView1.Rows(i).Cells("ColSR").Value.ToString) & "")


                        End If


                    Next

                    Me.asSELECT.SavedpFlg2(Rd, "SELECT * FROM SUPPLIER_PAYMENT WHERE nPINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                    If Me.CASH_PAY = True Then
                        If Me.asSELECT.pFlg2 = True Then
                            Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_PAYMENT SET nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nCASH_AMOUNT=" & Me.CASH_AMT & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nEMP_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.DESCRIPTION & "' WHERE nPINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                        Else
                            Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, nCASH_AMOUNT, nPINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ",'" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "'," & Me.CASH_AMT & "," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbEmployee.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")

                        End If


                    ElseIf Me.BANK_PAY = True Then
                        If Me.asSELECT.pFlg2 = True Then
                            Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_PAYMENT SET nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', sCHEQUE_NO='" & Me.CHEQ_NO & "',sCHEQUE_TYPE='" & Me.CHEQ_TYPE & "', dCHEQUE_DATE='" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', nCHECK_AMOUNT=" & Me.BANK_AMT & ", sACCOUNT_CODE='" & Me.BANK_ACC & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nEMP_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.DESCRIPTION & "' WHERE nPINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                        Else
                            Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, sCHEQUE_NO,sCHEQUE_TYPE, dCHEQUE_DATE, nCHECK_AMOUNT, sACCOUNT_CODE, nPINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', " & Me.BANK_AMT & ",'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbEmployee.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")
                        End If


                    ElseIf Me.BOTH_PAY = True Then
                        If Me.asSELECT.pFlg2 = True Then
                            Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_PAYMENT SET nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nCASH_AMOUNT=" & Me.CASH_AMT & ", sCHEQUE_NO='" & Me.CHEQ_NO & "',sCHEQUE_TYPE='" & Me.CHEQ_TYPE & "', dCHEQUE_DATE='" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', nCHECK_AMOUNT=" & Me.BANK_AMT & ", sACCOUNT_CODE='" & Me.BANK_ACC & "', nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nEMP_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.DESCRIPTION & "' WHERE nPINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                        Else
                            Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO,sCHEQUE_TYPE, dCHEQUE_DATE, nCHECK_AMOUNT, sACCOUNT_CODE, nPINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Me.CASH_AMT & ", '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & CDate(Me.CHEQ_DATE).ToString("MM-dd-yyyy") & "', " & Me.BANK_AMT & ",'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbEmployee.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")

                        End If

                    ElseIf Not (Val(Me.CASH_AMT) + Val(Me.BANK_AMT)) = 0 Then
                        If Me.asSELECT.pFlg2 = True Then
                            Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_PAYMENT SET nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", dDATE='" & Me.Pmt_Date & "', nCASH_AMOUNT=" & Me.CASH_AMT & ", sCHEQUE_NO='" & Me.CHEQ_NO & "',sCHEQUE_TYPE='" & Me.CHEQ_TYPE & "', dCHEQUE_DATE='" & Me.CHEQ_DATE & "', nCHECK_AMOUNT=" & Me.BANK_AMT & ", sACCOUNT_CODE='" & Me.BANK_ACC & "', nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nEMP_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.DESCRIPTION & "' WHERE nPINV_NO=" & Val(Me.TxtInvoice.Text) & "")

                        Else
                            Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHECK_AMOUNT, sACCOUNT_CODE, nPINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", '" & Me.Pmt_Date & "', " & Me.CASH_AMT & ", '" & Me.CHEQ_NO & "', '" & Me.CHEQ_TYPE & "', '" & Me.CHEQ_DATE & "', " & Me.BANK_AMT & ",'" & Me.BANK_ACC & "'," & Val(Me.TxtInvoice.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & "," & Val(Me.CmbEmployee.SelectedItem.Col3) & ",'" & Me.DESCRIPTION & "')")

                        End If

                    Else
                        MsgBox("Credit Purchase Invoice Saved!", MsgBoxStyle.Information, "(NS) - Credit Invoice!")

                    End If

                    Me.BttnPrev.Enabled = True
                    Me.BttnPrint.Enabled = True
                    Me.BttnSearch_Inv.Enabled = True
                    Me.BttnPayment.Enabled = False
                    Me.BttnNew.Text = "&New"
                    Me.BttnNew.Enabled = True
                    Me.BttnSave.Enabled = False
                    Me.BttnClose.Enabled = True
                    Me.Disable_All()

                End If

            ElseIf Me.asSELECT.pFlg1 = False Then
                MsgBox("This Invoice # '" & Me.TxtInvoice.Text & "' is not Exist.", MsgBoxStyle.Exclamation, "(NS) - Already Exist!")

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
        frmSEARCH_P_INV.TxtSupplier.Text = Nothing
        frmSEARCH_P_INV.TxtInvoice.Text = Nothing
        frmSEARCH_P_INV.TxtDateFrom.Text = Nothing
        frmSEARCH_P_INV.TxtDateTo.Text = Nothing

        frmSEARCH_P_INV.ShowDialog(Me)
Fix:
    End Sub
#End Region

#Region "Print Button Control"
    Private Sub BttnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrev.Click
        Dim Rpt As New rptPURCHASE_INVOICE
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = "{V_PURCHASE_MASTER.PINV_NO}=" & Val(Me.TxtInvoice.Text) & ""
            Frm.Text = "Purchase Invoice"
            Frm.MdiParent = Me.ParentForm
            Frm.Show()
            Frm.Activate()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub BttnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrint.Click
        Dim Rpt As New rptPURCHASE_INVOICE
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = "{V_PURCHASE_MASTER.PINV_NO}=" & Val(Me.TxtInvoice.Text) & ""
            Frm.CRV.PrintReport()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillComboBox_Supplier()
        Dim Str1 As String = "SELECT nID, sCONTACT_PERSON, sDESIGNATION, sSUPPLIER_NAME, sADDRESS, sSUPPLIER_PH, sPERSON_PH, sCELL_NO, sFAX_NO, sE_MAIL, sWEB_ADD, sSTATUS, nOPEN_BAL FROM SUPPLIER_INFO WHERE sSTATUS='1'"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daSUPPLIER_INFO = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsSUPPLIER_INFO1.Clear()
        Me.daSUPPLIER_INFO.Fill(Me.DsSUPPLIER_INFO1.SUPPLIER_INFO)

        Dim dtLoading As New DataTable("SUPPLIER_INFO")

        dtLoading.Columns.Add("nID", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sCONTACT_PERSON", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sSUPPLIER_NAME", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsSUPPLIER_INFO1.SUPPLIER_INFO.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nID") = Me.DsSUPPLIER_INFO1.SUPPLIER_INFO.Item(Cnt).Item(0).ToString
            dr("sCONTACT_PERSON") = Me.DsSUPPLIER_INFO1.SUPPLIER_INFO.Item(Cnt).Item(1).ToString
            dr("sSUPPLIER_NAME") = Me.DsSUPPLIER_INFO1.SUPPLIER_INFO.Item(Cnt).Item(3).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbSupplier.SelectedIndex = -1
        Me.CmbSupplier.Items.Clear()
        Me.CmbSupplier.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbSupplier.SourceDataString = New String(2) {"sSUPPLIER_NAME", "sCONTACT_PERSON", "nID"}
        Me.CmbSupplier.SourceDataTable = dtLoading
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

        Me.CmbEmployee.SelectedIndex = -1
        Me.CmbEmployee.Items.Clear()
        Me.CmbEmployee.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbEmployee.SourceDataString = New String(2) {"NAME", "DESIGNATION", "CODE"}
        Me.CmbEmployee.SourceDataTable = dtLoading
    End Sub

    Private Sub Fill_Master_Data()
        Dim Str2 As String = "SELECT PINV_NO, SUPPLIER_NAME, CASH_SUPPLIER, P_DATE, DISP_DATE, VEHICLE, CONVERT(NUMERIC(18,2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18,2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18,2), DISC_OTHER) AS DISC_OTHER, OTHER_DESC, CONVERT(NUMERIC(18,2), NET_TOTAL) AS NET_TOTAL, EMP_NAME, GROUP_NAME, CONVERT(NUMERIC(18,2), nFREIGHT) AS nFREIGHT, CONVERT(NUMERIC(18,2), nUNLOADING) AS nUNLOADING, sTR_NO, nTR_QTY, sREMARKS, LOGIN_USER, RECV_PER, RECV_RS, sSUP_INV_NO FROM V_PURCHASE_MASTER WHERE PINV_NO=" & Val(Me.TxtInvoice.Text) & ""
        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
        'DataAdapter Name using without 'V' only in PurchaseMaster
        Me.daPURCHASE_MASTER = New SDS.SqlDataAdapter(SqlCmd2)

        Me.DsPURCHASE_MASTER1.Clear()
        Me.daPURCHASE_MASTER.Fill(Me.DsPURCHASE_MASTER1.V_PURCHASE_MASTER)

    End Sub
    Private Sub Fill_Detail_Data()
        Dim Str3 As String = "SELECT ID, PINV_NO, ITEM_CODE, ITEM_NAME, BATCH_NO, CONVERT(NUMERIC(18,2), UNIT_COST) AS UNIT_COST, DISC_PER, PPP, QTY_PKS, QTY_PCS, QTY_BONUS, QTY_TOT_PCS, CONVERT(NUMERIC(18,2), TOTAL_VALUE) AS TOTAL_VALUE, SCM_ITEM_CODE, SCM_ITEM, SCM_QTY, SALE_TAX FROM V_PURCHASE_DETAIL WHERE PINV_NO=" & Val(Me.TxtInvoice.Text) & ""
        Dim SqlCmd3 As New SDS.SqlCommand(Str3, Me.SqlConnection1)
        Me.daV_PURCHASE_DETAIL = New SDS.SqlDataAdapter(SqlCmd3)

        Me.DsV_PURCHASE_DETAIL1.Clear()
        Me.daV_PURCHASE_DETAIL.Fill(Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL)

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Count - 1
            Me.DataGridView1.Rows.Add()
            Me.DataGridView1.Rows(Cnt).Cells("ColCode").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(2).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColBatch").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(4).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColName").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(3).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColCost").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(5).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPack").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(8).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPiece").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(9).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColBonus").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(10).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPercentage").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(6).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColSaleTax").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(16).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColTotal").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(12).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColSR").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(0).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPPP").Value = Me.DsV_PURCHASE_DETAIL1.V_PURCHASE_DETAIL.Item(Cnt).Item(7).ToString

        Next
    End Sub

    Dim bPMT As Boolean = False
    Private Sub Fill_Payment_Data()
        'Try
        Dim Str1 As String = "SELECT ID, SUPPLIER_ID, SUPPLIER_NAME, PMT_DATE, CONVERT(NUMERIC(18,2), CASH_AMT) AS CASH_AMT, CHQ_NO, CHQ_TYPE, CHQ_DATE, CONVERT(NUMERIC(18,2), BANK_AMT) AS BANK_AMT, BANK_ACC, BANK_NAME, PINV_NO, GROUP_NAME, CONVERT(NUMERIC(18,2), TOT_PAYMENT) AS TOT_PAYMENT, EMP_NAME, DESCRIPTION FROM V_SUPPLIER_PAYMENT WHERE PINV_NO=" & Val(Me.TxtInvoice.Text) & ""
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daV_SUPPLIER_PAYMENT = New SDS.SqlDataAdapter(SqlCmd1)
        bPMT = True
        Me.DsV_SUPPLIER_PAYMENT1.Clear()
        Me.daV_SUPPLIER_PAYMENT.Fill(Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT)

        'FILL VALUE IN PAYMENT's VARIABLES
        If Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Count > 0 Then
            On Error GoTo Fix

            PINV_NO = Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(11).ToString
            Pmt_Date = Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(3).ToString
            CASH_AMT = Val(Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(4).ToString)
            CHEQ_NO = Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(5).ToString
            CHEQ_TYPE = Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(6).ToString
            CHEQ_DATE = Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(7).ToString
            BANK_AMT = Val(Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(8).ToString)
            BANK_ACC = Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(9).ToString
            DESCRIPTION = Me.DsV_SUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT.Item(0).Item(15).ToString

Fix:
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
            Pmt_Date = Nothing
            CASH_AMT = 0
            CHEQ_NO = Nothing
            CHEQ_TYPE = Nothing
            CHEQ_DATE = Nothing
            BANK_AMT = 0
            BANK_ACC = Nothing
            PINV_NO = Nothing
            DESCRIPTION = Nothing

            Me.BOTH_PAY = False
            Me.BANK_PAY = False
            Me.CASH_PAY = False

        End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

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

        ElseIf Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Then
            Me.CmbSupplier.Focus()

        ElseIf Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
            Me.CmbEmployee.Focus()

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
        'Me.CmbGroup.SelectedIndex = -1
        Me.CmbSupplier.SelectedIndex = -1
        Me.TxtVehicle.Text = Nothing
        'Me.CmbEmployee.SelectedIndex = -1
        Me.TxtCashSupplier.Text = Nothing
        Me.TxtPayables.Text = 0

        Me.LblB_Pcs.Text = 0
        Me.LblPPP.Text = 0
        Me.LblCostPcs.Text = 0
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
        'Me.TxtPayment.Text = "0.00"

        Me.TxtRemarks.Text = Nothing

        Me.TxtSup_Invoice.Focus()

        'Me.CASH_AMT = 0.0
        'Me.BANK_AMT = 0.0

        'Me.BANK_ACC = Nothing
        'Me.CHEQ_NO = Nothing
        'Me.CHEQ_DATE = Nothing
        'Me.CHEQ_TYPE = Nothing


        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:
        Me.Default_Setting()
    End Sub

    Private Sub Default_Setting()
        On Error GoTo Fix
        Dim StrCMB As String

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("R_MAN").ToString
        Me.CmbEmployee.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbEmployee.SelectedIndex = Me.CmbEmployee.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("GROUP").ToString
        Me.CmbGroup.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCMB)
        End If

Fix:
    End Sub
#End Region

End Class
