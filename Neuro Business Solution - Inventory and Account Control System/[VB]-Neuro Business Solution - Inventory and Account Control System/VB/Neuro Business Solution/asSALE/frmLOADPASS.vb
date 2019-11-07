Imports SDS = System.Data.SqlClient
Public Class frmLOADPASS
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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtLoadPass As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CmbGroup As MTGCComboBox
    Friend WithEvents CmbD_Man As MTGCComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalItems As System.Windows.Forms.TextBox
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
    Friend WithEvents TxtNetTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
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
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_STOCK_NET_TOT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlConnection2 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblRatePcs As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents BttnAdd As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DsV_STOCK_NET_TOT1 As Neruo_Business_Solution.dsV_STOCK_NET_TOT
    Friend WithEvents CmbS_Man As MTGCComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents CmbVan As MTGCComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbRoute As MTGCComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DsLUP_VAN1 As Neruo_Business_Solution.dsLUP_VAN
    Friend WithEvents daLUP_VAN As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_ROUTES As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_ROUTES1 As Neruo_Business_Solution.dsLUP_ROUTES
    Friend WithEvents daV_MOBILE_ISSUE_MASTER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsV_MOBILE_ISSUE_MASTER1 As Neruo_Business_Solution.dsV_MOBILE_ISSUE_MASTER
    Friend WithEvents daV_MOBILE_ISSUE_DETAIL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand6 As System.Data.SqlClient.SqlCommand
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
    Friend WithEvents ColDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColScmItem As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ColScmQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DsV_MOBILE_ISSUE_DETAIL1 As Neruo_Business_Solution.dsV_MOBILE_ISSUE_DETAIL
    Friend WithEvents BttnSearch_LP As System.Windows.Forms.Button
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnPrev As System.Windows.Forms.Button
    Friend WithEvents BttnPrint As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BttnEdit As System.Windows.Forms.Button
    Friend WithEvents DsV_MOBILE_ISSUE_MASTER11 As Neruo_Business_Solution.dsV_MOBILE_ISSUE_MASTER1
    Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CopyToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PasteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label32 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLOADPASS))
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
        Me.ColDate = New System.Windows.Forms.DataGridViewTextBoxColumn
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
        Me.TxtTotal = New System.Windows.Forms.TextBox
        Me.DsV_MOBILE_ISSUE_MASTER1 = New Neruo_Business_Solution.dsV_MOBILE_ISSUE_MASTER
        Me.TxtDescription = New System.Windows.Forms.TextBox
        Me.TxtNetTotal = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TxtOtherDisc = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.TxtDiscPercent = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.TxtDiscount = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtTotalItems = New System.Windows.Forms.TextBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.TxtRemarks = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnEdit = New System.Windows.Forms.Button
        Me.BttnSearch_LP = New System.Windows.Forms.Button
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnPrev = New System.Windows.Forms.Button
        Me.BttnPrint = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.BttnSearch_Item = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.BttnAdd = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtLoadPass = New System.Windows.Forms.TextBox
        Me.DsV_MOBILE_ISSUE_MASTER11 = New Neruo_Business_Solution.dsV_MOBILE_ISSUE_MASTER1
        Me.CmbS_Man = New MTGCComboBox
        Me.CmbD_Man = New MTGCComboBox
        Me.CmbRoute = New MTGCComboBox
        Me.CmbVan = New MTGCComboBox
        Me.CmbGroup = New MTGCComboBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_EMPLOYEE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_ITEM = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection2 = New System.Data.SqlClient.SqlConnection
        Me.daV_STOCK_NET_TOT = New System.Data.SqlClient.SqlDataAdapter
        Me.Label3 = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.daLUP_VAN = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_ROUTES = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.daV_MOBILE_ISSUE_MASTER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.daV_MOBILE_ISSUE_DETAIL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand6 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_BUSINESS_GROUP1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
        Me.DsLUP_EMPLOYEE1 = New Neruo_Business_Solution.dsLUP_EMPLOYEE
        Me.DsLUP_VAN1 = New Neruo_Business_Solution.dsLUP_VAN
        Me.DsLUP_ROUTES1 = New Neruo_Business_Solution.dsLUP_ROUTES
        Me.DsV_MOBILE_ISSUE_DETAIL1 = New Neruo_Business_Solution.dsV_MOBILE_ISSUE_DETAIL
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_STOCK_NET_TOT1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DsV_MOBILE_ISSUE_MASTER1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsV_MOBILE_ISSUE_MASTER11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_VAN1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_ROUTES1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_MOBILE_ISSUE_DETAIL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
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
        Me.GroupBox2.Location = New System.Drawing.Point(10, 130)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(810, 292)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.LightSteelBlue
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
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCode, Me.ColBatch, Me.ColName, Me.ColCost, Me.ColRate, Me.ColPack, Me.ColPiece, Me.ColBonus, Me.ColPercentage, Me.ColDisc_Rs, Me.ColSaleTax, Me.ColTotal, Me.ColDate, Me.ColScmItem, Me.ColScmQty, Me.ColSR, Me.ColPPP})
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle13
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.HotTrack
        Me.DataGridView1.Location = New System.Drawing.Point(3, 42)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 15
        Me.DataGridView1.Size = New System.Drawing.Size(804, 247)
        Me.DataGridView1.TabIndex = 14
        '
        'ColCode
        '
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.ColCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColCode.HeaderText = "Code"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.Width = 70
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
        Me.ColCost.HeaderText = "Cost"
        Me.ColCost.Name = "ColCost"
        Me.ColCost.Visible = False
        '
        'ColRate
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "C2"
        DataGridViewCellStyle3.NullValue = "0.00"
        Me.ColRate.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColRate.HeaderText = "Rate"
        Me.ColRate.Name = "ColRate"
        Me.ColRate.Width = 50
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
        'ColDisc_Rs
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "C2"
        DataGridViewCellStyle8.NullValue = "0.00"
        Me.ColDisc_Rs.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColDisc_Rs.HeaderText = "Disc Rs"
        Me.ColDisc_Rs.Name = "ColDisc_Rs"
        Me.ColDisc_Rs.Width = 50
        '
        'ColSaleTax
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle9.Format = "N2"
        DataGridViewCellStyle9.NullValue = "0.00"
        Me.ColSaleTax.DefaultCellStyle = DataGridViewCellStyle9
        Me.ColSaleTax.HeaderText = "S.Tax"
        Me.ColSaleTax.MaxInputLength = 3
        Me.ColSaleTax.Name = "ColSaleTax"
        Me.ColSaleTax.ReadOnly = True
        Me.ColSaleTax.Width = 40
        '
        'ColTotal
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N2"
        DataGridViewCellStyle10.NullValue = "0.00"
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle10
        Me.ColTotal.HeaderText = "Total"
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.Width = 80
        '
        'ColDate
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.Format = "d"
        DataGridViewCellStyle11.NullValue = Nothing
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColDate.DefaultCellStyle = DataGridViewCellStyle11
        Me.ColDate.HeaderText = "Date"
        Me.ColDate.Name = "ColDate"
        Me.ColDate.Width = 80
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
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N0"
        DataGridViewCellStyle12.NullValue = "0"
        Me.ColScmQty.DefaultCellStyle = DataGridViewCellStyle12
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
        Me.LblStock.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_STOCK_NET_TOT1, "V_STOCK_NET_TOTAL.NET_TOTAL", True))
        Me.LblStock.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStock.ForeColor = System.Drawing.Color.Black
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
        Me.GroupBox4.Controls.Add(Me.TxtTotal)
        Me.GroupBox4.Controls.Add(Me.TxtDescription)
        Me.GroupBox4.Controls.Add(Me.TxtNetTotal)
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.TxtOtherDisc)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.TxtDiscPercent)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.TxtDiscount)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Location = New System.Drawing.Point(625, 428)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(195, 165)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        '
        'TxtTotal
        '
        Me.TxtTotal.BackColor = System.Drawing.Color.White
        Me.TxtTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.TOTAL_BILL", True))
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
        'DsV_MOBILE_ISSUE_MASTER1
        '
        Me.DsV_MOBILE_ISSUE_MASTER1.DataSetName = "dsV_MOBILE_ISSUE_MASTER"
        Me.DsV_MOBILE_ISSUE_MASTER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtDescription
        '
        Me.TxtDescription.BackColor = System.Drawing.Color.White
        Me.TxtDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.OTHER_DESC", True))
        Me.TxtDescription.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDescription.Location = New System.Drawing.Point(9, 110)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(177, 21)
        Me.TxtDescription.TabIndex = 8
        Me.TxtDescription.Text = "Other's Description Here!"
        '
        'TxtNetTotal
        '
        Me.TxtNetTotal.BackColor = System.Drawing.Color.White
        Me.TxtNetTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.NET_TOTAL", True))
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
        Me.TxtOtherDisc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.OTHER_DISC", True))
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
        Me.TxtDiscPercent.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.DISC_PER", True))
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
        Me.TxtDiscount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.DISC_RS", True))
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
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(6, 7)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 21)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Tot. Items"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtTotalItems
        '
        Me.TxtTotalItems.BackColor = System.Drawing.Color.White
        Me.TxtTotalItems.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtTotalItems.Location = New System.Drawing.Point(6, 31)
        Me.TxtTotalItems.MaxLength = 50
        Me.TxtTotalItems.Name = "TxtTotalItems"
        Me.TxtTotalItems.ReadOnly = True
        Me.TxtTotalItems.Size = New System.Drawing.Size(69, 21)
        Me.TxtTotalItems.TabIndex = 1
        Me.TxtTotalItems.TabStop = False
        Me.TxtTotalItems.Text = "0"
        Me.TxtTotalItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox6.Controls.Add(Me.TxtRemarks)
        Me.GroupBox6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(358, 514)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(261, 79)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.REMARKS", True))
        Me.TxtRemarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtRemarks.Location = New System.Drawing.Point(3, 19)
        Me.TxtRemarks.MaxLength = 100
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(255, 57)
        Me.TxtRemarks.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.BttnEdit)
        Me.GroupBox1.Controls.Add(Me.BttnSearch_LP)
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnPrev)
        Me.GroupBox1.Controls.Add(Me.BttnPrint)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.TxtTotalItems)
        Me.GroupBox1.Controls.Add(Me.BttnSearch_Item)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 429)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(342, 164)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'BttnEdit
        '
        Me.BttnEdit.BackColor = System.Drawing.Color.LightBlue
        Me.BttnEdit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnEdit.Location = New System.Drawing.Point(82, 80)
        Me.BttnEdit.Name = "BttnEdit"
        Me.BttnEdit.Size = New System.Drawing.Size(75, 31)
        Me.BttnEdit.TabIndex = 15
        Me.BttnEdit.Text = "E&dit"
        Me.BttnEdit.UseVisualStyleBackColor = False
        '
        'BttnSearch_LP
        '
        Me.BttnSearch_LP.BackColor = System.Drawing.Color.DarkKhaki
        Me.BttnSearch_LP.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch_LP.Location = New System.Drawing.Point(168, 112)
        Me.BttnSearch_LP.Name = "BttnSearch_LP"
        Me.BttnSearch_LP.Size = New System.Drawing.Size(75, 42)
        Me.BttnSearch_LP.TabIndex = 13
        Me.BttnSearch_LP.Text = "Sea&rch L.Pass"
        Me.BttnSearch_LP.UseVisualStyleBackColor = False
        '
        'BttnNew
        '
        Me.BttnNew.BackColor = System.Drawing.Color.LightBlue
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(82, 43)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(75, 31)
        Me.BttnNew.TabIndex = 10
        Me.BttnNew.Text = "&New"
        Me.BttnNew.UseVisualStyleBackColor = False
        '
        'BttnPrev
        '
        Me.BttnPrev.BackColor = System.Drawing.Color.DarkKhaki
        Me.BttnPrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrev.Enabled = False
        Me.BttnPrev.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrev.Location = New System.Drawing.Point(220, 11)
        Me.BttnPrev.Name = "BttnPrev"
        Me.BttnPrev.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrev.TabIndex = 11
        Me.BttnPrev.Text = "Pre&view"
        Me.BttnPrev.UseVisualStyleBackColor = False
        '
        'BttnPrint
        '
        Me.BttnPrint.BackColor = System.Drawing.Color.DarkKhaki
        Me.BttnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrint.Enabled = False
        Me.BttnPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrint.Location = New System.Drawing.Point(122, 11)
        Me.BttnPrint.Name = "BttnPrint"
        Me.BttnPrint.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrint.TabIndex = 12
        Me.BttnPrint.Text = "&Print"
        Me.BttnPrint.UseVisualStyleBackColor = False
        '
        'BttnClose
        '
        Me.BttnClose.BackColor = System.Drawing.Color.LightBlue
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(254, 62)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 31)
        Me.BttnClose.TabIndex = 14
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = False
        '
        'BttnSave
        '
        Me.BttnSave.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.BttnSave.Enabled = False
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(158, 43)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(95, 68)
        Me.BttnSave.TabIndex = 9
        Me.BttnSave.Text = "&Save"
        Me.BttnSave.UseVisualStyleBackColor = False
        '
        'BttnSearch_Item
        '
        Me.BttnSearch_Item.BackColor = System.Drawing.Color.BurlyWood
        Me.BttnSearch_Item.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch_Item.Location = New System.Drawing.Point(10, 116)
        Me.BttnSearch_Item.Name = "BttnSearch_Item"
        Me.BttnSearch_Item.Size = New System.Drawing.Size(75, 42)
        Me.BttnSearch_Item.TabIndex = 8
        Me.BttnSearch_Item.Text = "Sea&rch Item"
        Me.BttnSearch_Item.UseVisualStyleBackColor = False
        Me.BttnSearch_Item.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.BackColor = System.Drawing.Color.Pink
        Me.GroupBox3.Controls.Add(Me.BttnAdd)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.TxtLoadPass)
        Me.GroupBox3.Controls.Add(Me.CmbS_Man)
        Me.GroupBox3.Controls.Add(Me.CmbD_Man)
        Me.GroupBox3.Controls.Add(Me.CmbRoute)
        Me.GroupBox3.Controls.Add(Me.CmbVan)
        Me.GroupBox3.Controls.Add(Me.CmbGroup)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(10, 44)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(810, 80)
        Me.GroupBox3.TabIndex = 1
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
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 21)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Load Pass #"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtLoadPass
        '
        Me.TxtLoadPass.BackColor = System.Drawing.Color.White
        Me.TxtLoadPass.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER.LPINV_NO", True))
        Me.TxtLoadPass.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtLoadPass.Location = New System.Drawing.Point(90, 18)
        Me.TxtLoadPass.MaxLength = 50
        Me.TxtLoadPass.Name = "TxtLoadPass"
        Me.TxtLoadPass.ReadOnly = True
        Me.TxtLoadPass.Size = New System.Drawing.Size(82, 21)
        Me.TxtLoadPass.TabIndex = 1
        Me.TxtLoadPass.TabStop = False
        '
        'DsV_MOBILE_ISSUE_MASTER11
        '
        Me.DsV_MOBILE_ISSUE_MASTER11.DataSetName = "dsV_MOBILE_ISSUE_MASTER1"
        Me.DsV_MOBILE_ISSUE_MASTER11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CmbS_Man
        '
        Me.CmbS_Man.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbS_Man.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbS_Man.ColumnNum = 3
        Me.CmbS_Man.ColumnWidth = "100;100;30"
        Me.CmbS_Man.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.SALE_MAN", True))
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
        Me.CmbS_Man.Location = New System.Drawing.Point(462, 17)
        Me.CmbS_Man.ManagingFastMouseMoving = True
        Me.CmbS_Man.ManagingFastMouseMovingInterval = 30
        Me.CmbS_Man.Name = "CmbS_Man"
        Me.CmbS_Man.Size = New System.Drawing.Size(133, 22)
        Me.CmbS_Man.TabIndex = 6
        '
        'CmbD_Man
        '
        Me.CmbD_Man.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbD_Man.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbD_Man.ColumnNum = 3
        Me.CmbD_Man.ColumnWidth = "100;100;30"
        Me.CmbD_Man.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.D_MAN", True))
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
        Me.CmbD_Man.Location = New System.Drawing.Point(671, 18)
        Me.CmbD_Man.ManagingFastMouseMoving = True
        Me.CmbD_Man.ManagingFastMouseMovingInterval = 30
        Me.CmbD_Man.Name = "CmbD_Man"
        Me.CmbD_Man.Size = New System.Drawing.Size(133, 22)
        Me.CmbD_Man.TabIndex = 8
        '
        'CmbRoute
        '
        Me.CmbRoute.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbRoute.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbRoute.ColumnNum = 3
        Me.CmbRoute.ColumnWidth = "100;100;30"
        Me.CmbRoute.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.ROUTE", True))
        Me.CmbRoute.DisplayMember = "Text"
        Me.CmbRoute.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbRoute.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbRoute.DropDownForeColor = System.Drawing.Color.White
        Me.CmbRoute.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbRoute.DropDownWidth = 340
        Me.CmbRoute.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbRoute.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbRoute.GridLineHorizontal = False
        Me.CmbRoute.GridLineVertical = True
        Me.CmbRoute.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbRoute.Location = New System.Drawing.Point(462, 45)
        Me.CmbRoute.ManagingFastMouseMoving = True
        Me.CmbRoute.ManagingFastMouseMovingInterval = 30
        Me.CmbRoute.Name = "CmbRoute"
        Me.CmbRoute.Size = New System.Drawing.Size(203, 22)
        Me.CmbRoute.TabIndex = 14
        '
        'CmbVan
        '
        Me.CmbVan.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbVan.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbVan.ColumnNum = 2
        Me.CmbVan.ColumnWidth = "100;50"
        Me.CmbVan.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.VAN_NAME", True))
        Me.CmbVan.DisplayMember = "Text"
        Me.CmbVan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbVan.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbVan.DropDownForeColor = System.Drawing.Color.White
        Me.CmbVan.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbVan.DropDownWidth = 340
        Me.CmbVan.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbVan.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbVan.GridLineHorizontal = False
        Me.CmbVan.GridLineVertical = True
        Me.CmbVan.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbVan.Location = New System.Drawing.Point(267, 45)
        Me.CmbVan.ManagingFastMouseMoving = True
        Me.CmbVan.ManagingFastMouseMovingInterval = 30
        Me.CmbVan.Name = "CmbVan"
        Me.CmbVan.Size = New System.Drawing.Size(119, 22)
        Me.CmbVan.TabIndex = 12
        '
        'CmbGroup
        '
        Me.CmbGroup.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup.ColumnNum = 3
        Me.CmbGroup.ColumnWidth = "100;100;30"
        Me.CmbGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.GROUP_NAME", True))
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
        Me.CmbGroup.Location = New System.Drawing.Point(90, 44)
        Me.CmbGroup.ManagingFastMouseMoving = True
        Me.CmbGroup.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup.Name = "CmbGroup"
        Me.CmbGroup.Size = New System.Drawing.Size(121, 22)
        Me.CmbGroup.TabIndex = 10
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(392, 17)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(64, 23)
        Me.Label33.TabIndex = 5
        Me.Label33.Text = "S. Man"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(601, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 22)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "D. Man"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(392, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 23)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Route"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(217, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 23)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Van"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 23)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "B. Group"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(217, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 21)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.dDATE", True))
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDate.Location = New System.Drawing.Point(267, 19)
        Me.TxtDate.MaxLength = 50
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(119, 21)
        Me.TxtDate.TabIndex = 4
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
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = "SELECT     CODE, BATCH, NET_TOTAL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_STOCK_NET_TOTAL"
        Me.SqlSelectCommand4.Connection = Me.SqlConnection2
        '
        'SqlConnection2
        '
        Me.SqlConnection2.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection2.FireInfoMessageEventOnUserErrors = False
        '
        'daV_STOCK_NET_TOT
        '
        Me.daV_STOCK_NET_TOT.SelectCommand = Me.SqlSelectCommand4
        Me.daV_STOCK_NET_TOT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_STOCK_NET_TOTAL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("BATCH", "BATCH"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL")})})
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
        Me.Label3.Text = "Load Pass (Mobile Sale)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'daLUP_VAN
        '
        Me.daLUP_VAN.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_VAN.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_VAN.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_VANS", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("sPLATE_NO", "sPLATE_NO"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC"), New System.Data.Common.DataColumnMapping("sMODEL", "sMODEL"), New System.Data.Common.DataColumnMapping("nMADE_YEAR", "nMADE_YEAR"), New System.Data.Common.DataColumnMapping("sENG_NO", "sENG_NO"), New System.Data.Common.DataColumnMapping("sCH_NO", "sCH_NO"), New System.Data.Common.DataColumnMapping("sCOLOR", "sCOLOR"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS")})})
        Me.daLUP_VAN.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = resources.GetString("SqlDeleteCommand1.CommandText")
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_sPLATE_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPLATE_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sMODEL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sMODEL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sMODEL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMODEL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMADE_YEAR", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMADE_YEAR", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sENG_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sENG_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sENG_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sENG_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCH_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCH_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCH_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCH_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCOLOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCOLOR", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCOLOR", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCOLOR", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     sPLATE_NO, sDESC, sMODEL, nMADE_YEAR, sENG_NO, sCH_NO, sCOLOR, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     " & _
            "                 CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STAT" & _
            "US" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         LUP_VANS"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sPLATE_NO", System.Data.SqlDbType.VarChar, 0, "sPLATE_NO"), New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@sMODEL", System.Data.SqlDbType.VarChar, 0, "sMODEL"), New System.Data.SqlClient.SqlParameter("@nMADE_YEAR", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMADE_YEAR", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sENG_NO", System.Data.SqlDbType.VarChar, 0, "sENG_NO"), New System.Data.SqlClient.SqlParameter("@sCH_NO", System.Data.SqlDbType.VarChar, 0, "sCH_NO"), New System.Data.SqlClient.SqlParameter("@sCOLOR", System.Data.SqlDbType.VarChar, 0, "sCOLOR"), New System.Data.SqlClient.SqlParameter("@Original_sPLATE_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPLATE_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sMODEL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sMODEL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sMODEL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMODEL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMADE_YEAR", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMADE_YEAR", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sENG_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sENG_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sENG_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sENG_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCH_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCH_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCH_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCH_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCOLOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCOLOR", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCOLOR", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCOLOR", System.Data.DataRowVersion.Original, Nothing)})
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
        'daV_MOBILE_ISSUE_MASTER
        '
        Me.daV_MOBILE_ISSUE_MASTER.SelectCommand = Me.SqlCommand7
        Me.daV_MOBILE_ISSUE_MASTER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_MOBILE_ISSUE_MASTER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("LPINV_NO", "LPINV_NO"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("TOTAL_BILL", "TOTAL_BILL"), New System.Data.Common.DataColumnMapping("DISC_RS", "DISC_RS"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("OTHER_DISC", "OTHER_DISC"), New System.Data.Common.DataColumnMapping("OTHER_DESC", "OTHER_DESC"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL"), New System.Data.Common.DataColumnMapping("VAN_NO", "VAN_NO"), New System.Data.Common.DataColumnMapping("VAN_NAME", "VAN_NAME"), New System.Data.Common.DataColumnMapping("SALE_MAN", "SALE_MAN"), New System.Data.Common.DataColumnMapping("USER_NAME", "USER_NAME"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE"), New System.Data.Common.DataColumnMapping("D_MAN", "D_MAN"), New System.Data.Common.DataColumnMapping("POSTED", "POSTED"), New System.Data.Common.DataColumnMapping("REMARKS", "REMARKS")})})
        '
        'SqlCommand7
        '
        Me.SqlCommand7.CommandText = resources.GetString("SqlCommand7.CommandText")
        Me.SqlCommand7.Connection = Me.SqlConnection1
        '
        'daV_MOBILE_ISSUE_DETAIL
        '
        Me.daV_MOBILE_ISSUE_DETAIL.SelectCommand = Me.SqlCommand6
        Me.daV_MOBILE_ISSUE_DETAIL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_MOBILE_ISSUE_DETAIL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("LPINV_NO", "LPINV_NO"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("ITEM_CODE", "ITEM_CODE"), New System.Data.Common.DataColumnMapping("ITEM_NAME", "ITEM_NAME"), New System.Data.Common.DataColumnMapping("BATCH_NO", "BATCH_NO"), New System.Data.Common.DataColumnMapping("UNIT_COST", "UNIT_COST"), New System.Data.Common.DataColumnMapping("UNIT_RATE", "UNIT_RATE"), New System.Data.Common.DataColumnMapping("DISC_RS", "DISC_RS"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("SALE_TAX", "SALE_TAX"), New System.Data.Common.DataColumnMapping("PPP", "PPP"), New System.Data.Common.DataColumnMapping("QTY_PKS", "QTY_PKS"), New System.Data.Common.DataColumnMapping("QTY_PCS", "QTY_PCS"), New System.Data.Common.DataColumnMapping("QTY_BONUS", "QTY_BONUS"), New System.Data.Common.DataColumnMapping("QTY_TOT_PCS", "QTY_TOT_PCS"), New System.Data.Common.DataColumnMapping("TOTAL_VALUE", "TOTAL_VALUE"), New System.Data.Common.DataColumnMapping("SCM_ITEM_CODE", "SCM_ITEM_CODE"), New System.Data.Common.DataColumnMapping("SCM_ITEM_NAME", "SCM_ITEM_NAME"), New System.Data.Common.DataColumnMapping("SCM_QTY", "SCM_QTY")})})
        '
        'SqlCommand6
        '
        Me.SqlCommand6.CommandText = resources.GetString("SqlCommand6.CommandText")
        Me.SqlCommand6.Connection = Me.SqlConnection1
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
        'DsLUP_VAN1
        '
        Me.DsLUP_VAN1.DataSetName = "dsLUP_VAN"
        Me.DsLUP_VAN1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_ROUTES1
        '
        Me.DsLUP_ROUTES1.DataSetName = "dsLUP_ROUTES"
        Me.DsLUP_ROUTES1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsV_MOBILE_ISSUE_DETAIL1
        '
        Me.DsV_MOBILE_ISSUE_DETAIL1.DataSetName = "dsV_MOBILE_ISSUE_DETAIL"
        Me.DsV_MOBILE_ISSUE_DETAIL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.AutoSize = False
        Me.BindingNavigator1.BindingSource = Me.BindingSource1
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.None
        Me.BindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator3, Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorMovePreviousItem, Me.ToolStripSeparator2, Me.BindingNavigatorMoveNextItem, Me.ToolStripSeparator1, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorPositionItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorCountItem, Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.toolStripSeparator, Me.CutToolStripButton, Me.CopyToolStripButton, Me.PasteToolStripButton, Me.toolStripSeparator4, Me.HelpToolStripButton})
        Me.BindingNavigator1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.BindingNavigator1.Location = New System.Drawing.Point(384, 429)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(214, 74)
        Me.BindingNavigator1.TabIndex = 26
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.AutoSize = False
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(100, 21)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.AutoSize = False
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 40)
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.AutoSize = False
        Me.BindingNavigatorMoveFirstItem.BackColor = System.Drawing.Color.Pink
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(45, 40)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.AutoSize = False
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 40)
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.AutoSize = False
        Me.BindingNavigatorMovePreviousItem.BackColor = System.Drawing.Color.Tan
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(45, 40)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.AutoSize = False
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 40)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.AutoSize = False
        Me.BindingNavigatorMoveNextItem.BackColor = System.Drawing.Color.Tan
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(45, 40)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.AutoSize = False
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 40)
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.AutoSize = False
        Me.BindingNavigatorMoveLastItem.BackColor = System.Drawing.Color.Pink
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(45, 40)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.AutoSize = False
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 40)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.ReadOnly = True
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(100, 21)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.AutoSize = False
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'NewToolStripButton
        '
        Me.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"), System.Drawing.Image)
        Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripButton.Name = "NewToolStripButton"
        Me.NewToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.NewToolStripButton.Text = "&New"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.SaveToolStripButton.Text = "&Save"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 23)
        '
        'CutToolStripButton
        '
        Me.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CutToolStripButton.Image = CType(resources.GetObject("CutToolStripButton.Image"), System.Drawing.Image)
        Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripButton.Name = "CutToolStripButton"
        Me.CutToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.CutToolStripButton.Text = "C&ut"
        '
        'CopyToolStripButton
        '
        Me.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CopyToolStripButton.Image = CType(resources.GetObject("CopyToolStripButton.Image"), System.Drawing.Image)
        Me.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyToolStripButton.Name = "CopyToolStripButton"
        Me.CopyToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.CopyToolStripButton.Text = "&Copy"
        '
        'PasteToolStripButton
        '
        Me.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PasteToolStripButton.Image = CType(resources.GetObject("PasteToolStripButton.Image"), System.Drawing.Image)
        Me.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteToolStripButton.Name = "PasteToolStripButton"
        Me.PasteToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.PasteToolStripButton.Text = "&Paste"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(6, 23)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'frmLOADPASS
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(831, 605)
        Me.Controls.Add(Me.BindingNavigator1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmLOADPASS"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Load Pass (Moblie Sale)"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_STOCK_NET_TOT1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.DsV_MOBILE_ISSUE_MASTER1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsV_MOBILE_ISSUE_MASTER11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_VAN1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_ROUTES1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_MOBILE_ISSUE_DETAIL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
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

#Region "FORM CONTROL"
    Private Sub frmLOADPASS_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.BttnSave.Text = "&Update" And Me.BttnSave.Enabled = True Then
            MsgBox("Can't close without Updating OR Cancelling Invoice", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        ElseIf Me.BttnSave.Text = "&Save" And Me.BttnSave.Enabled = True Then
            MsgBox("Can't close without Saving OR Cancelling Invoice", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        End If
    End Sub
    Private Sub frmLOADPASS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.Search_Inv = False
        Me.FillComboBox_Employee()
        Me.FillComboBox_Group()
        Me.FillComboBox_Van()
        Me.FillComboBox_Route()

        Me.Disable_All()
        Me.BttnPrev.Enabled = False
        Me.BttnPrint.Enabled = False
        Me.BttnSave.Enabled = False

        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.Fill_Navigator()
        Me.BindingNavigatorMoveLastItem_Click(sender, e)
        'Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmLOADPASS_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtLoadPass.GotFocus, TxtDate.GotFocus, TxtDescription.GotFocus, TxtDiscount.GotFocus, TxtDiscPercent.GotFocus, TxtNetTotal.GotFocus, TxtOtherDisc.GotFocus, TxtTotal.GotFocus, TxtTotalItems.GotFocus, TxtRemarks.GotFocus
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
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtLoadPass.LostFocus, TxtDate.LostFocus, TxtDescription.LostFocus, TxtDiscount.LostFocus, TxtDiscPercent.LostFocus, TxtNetTotal.LostFocus, TxtOtherDisc.LostFocus, TxtTotal.LostFocus, TxtTotalItems.LostFocus, TxtRemarks.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub
    Private Sub Txt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDiscount.Leave, TxtDiscPercent.Leave, TxtOtherDisc.Leave
        If sender.Text = Nothing Then
            sender.Text = "0.00"
        End If
    End Sub

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTotalItems.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtDiscount.KeyPress, TxtNetTotal.KeyPress, TxtOtherDisc.KeyPress, TxtTotal.KeyPress, TxtDiscPercent.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    'NET TOTAL CALCULATION
    Private Sub TxtOtherDisc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtOtherDisc.TextChanged, TxtDiscPercent.TextChanged, TxtDiscount.TextChanged, TxtTotal.TextChanged
        If Me.Search_Inv = False Then
            On Error GoTo Fix
            Dim Total, Disc_RS, Disc_Age, Disc_Other As Double
            Total = Val(Me.TxtTotal.Text)
            Disc_RS = Val(Me.TxtDiscount.Text)
            Disc_Age = (Total * Val(Me.TxtDiscPercent.Text)) / 100
            Disc_Other = Val(Me.TxtOtherDisc.Text)

            Me.TxtNetTotal.Text = Total - (Disc_RS + Disc_Age + Disc_Other)
            Me.TxtNetTotal.Text = Decimal.Round(CDec(Me.TxtNetTotal.Text), 2)
Fix:
        End If

    End Sub

    'Fill data for Modification
    Private Sub TxtLoadPass_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtLoadPass.TextChanged
        If Me.Search_Inv = True Then

            'FILL MASTER RECORD
            Me.Fill_Master_Data()

            'FILL DETAIL RECORD
            Me.Fill_Detail_Data()

            Me.Search_Inv = False

        End If

    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbD_Man.GotFocus, CmbGroup.GotFocus, CmbS_Man.GotFocus, CmbVan.GotFocus, CmbRoute.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbD_Man.LostFocus, CmbGroup.LostFocus, CmbS_Man.LostFocus, CmbVan.LostFocus, CmbRoute.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "Select Item Controls"
    Private Sub SelectItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectItemToolStripMenuItem.Click
        On Error GoTo Fix
        frmSELECT_ITEM_BATCH.TxtCompany.Text = Nothing
        frmSELECT_ITEM_BATCH.TxtItem.Text = Nothing
        frmSELECT_ITEM_BATCH.FrmStr = "Mobile_Sale"
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
                Me.LblRatePcs.Text = 0
                Me.LblRate.Text = 0
                Me.LblRetail.Text = 0
                Me.LblStock.Text = 0

                'FILL TOP LABLES OF DATAGRID
                'WORKING ON BATCH WISE STOCK CALCULATION---CHECK BATCH EXIST OR NOT?
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
                        Me.LblRatePcs.Text = 0
                        Me.LblRate.Text = 0
                        Me.LblRetail.Text = 0
                        Me.LblStock.Text = 0

                        Me.SelectItemToolStripMenuItem_Click(sender, e)

                        Exit Sub
                    End If

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

                    Dim Str2 As String = "SELECT CODE, BATCH, NET_TOTAL FROM V_STOCK_NET_TOTAL WHERE CODE=" & Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value) & " AND BATCH='" & Me.DataGridView1.Rows(e.RowIndex).Cells("ColBatch").Value & "'"
                    Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection2)
                    Me.daV_STOCK_NET_TOT = New SDS.SqlDataAdapter(SqlCmd2)

                    Me.DsV_STOCK_NET_TOT1.Clear()
                    Me.daV_STOCK_NET_TOT.Fill(Me.DsV_STOCK_NET_TOT1.V_STOCK_NET_TOTAL)

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
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = 1
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = 0

                        'Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = 1
                    End If

                    If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value > 0 Then
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value) + Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value)
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = 0

                    End If

                ElseIf Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value > 1 Then
                    If Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCode").Value Is Nothing And Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value Is Nothing Then
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = 0
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = 1

                        'Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = 1
                    End If

                    If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value <= Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value Then
                        Dim PPP, PCS, PKS As Integer
                        PPP = Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value)
                        PCS = Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value)
                        PKS = PCS / PPP
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value = Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value) + PKS
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPiece").Value = PCS Mod PPP
                    End If
                End If

                If Not Me.DataGridView1.Rows(e.RowIndex).Cells("ColDate").Value Is Nothing Then
                    Try
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColDate").Value = CDate(Me.DataGridView1.Rows(e.RowIndex).Cells("ColDate").Value).ToString("dd-MMM-yyyy")
                    Catch ex As Exception
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColDate").Value = Date.Now.ToString("dd-MMM-yyyy")

                    End Try

                    'ElseIf Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColDate").Value Is Nothing Then
                    '    Try
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColDate").Value = CDate(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColDate").Value).ToString("dd-MMM-yyyy")
                    '    Catch ex As Exception
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColDate").Value = Date.Now.ToString("dd-MMM-yyyy")

                    '    End Try


                ElseIf Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColDate").Value Is Nothing Or Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColDate").Value) = 0 Then
                    Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColDate").Value = Date.Now.ToString("dd-MMM-yyyy")

                End If

            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        End If

    End Sub

    Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        If Me.Search_Inv = False Then
            ItemCode_Old = Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value
            Me.DataGridView1_CellValueChanged(sender, e)
        End If
    End Sub
    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        Dim i As Integer
        Me.TxtTotal.Text = "0.00"
        For i = 0 To Me.DataGridView1.Rows.Count - 1
            Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value)
        Next

    End Sub
    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        If Me.TxtLoadPass.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbVan.SelectedIndex = -1 Or Me.CmbVan.Text = Nothing Or Me.CmbRoute.SelectedIndex = -1 Or Me.CmbRoute.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

            Me.Null_Focus()

        ElseIf MsgBox("Are you sure to Delete Item?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Deleting Item?") = MsgBoxResult.Yes Then
            'DELETE FROM MOBLIE ISSUE DETAIL
            Me.asDelete.DeleteValue_NoErr("DELETE FROM MOBILE_ISSUE_DETAIL WHERE nID=" & Val(Me.DataGridView1.Rows(e.Row.Index).Cells("ColSR").Value) & "")

            'UPDATE VALUE IN MOBILE ISSUE MASTER
            Me.asUpdate.UpdateValue("UPDATE MOBILE_ISSUE_MASTER SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nTOTAL_BILL=" & Val(Me.TxtTotal.Text) & ", nDISCOUNT=" & Val(Me.TxtDiscount.Text) & ", nDISC_PERCENT=" & Val(Me.TxtDiscPercent.Text) & ", nOTHERS=" & Val(Me.TxtOtherDisc.Text) & ", sOTHER_DESC='" & Me.TxtDescription.Text & "', nNET_TOTAL= " & Val(Me.TxtNetTotal.Text) & ", sVAN_NO='" & Me.CmbVan.SelectedItem.Col2 & "', nEMPLOYEE_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nROUTE_CODE=" & Val(Me.CmbRoute.SelectedItem.Col3) & ", nD_MAN_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nLPINV_NO=" & Val(Me.TxtLoadPass.Text) & "")

            Me.BttnNew.Enabled = False

        Else
            e.Cancel = True
        End If

    End Sub
#End Region

#Region "Navigator Control"
    Private Sub BindingNavigatorMoveFirstItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMoveFirstItem.Click
        On Error GoTo Fix
        If Me.BttnNew.Text = "Ca&ncel" Then
            Me.BttnNew_Click(sender, e)
        End If
        Me.Search_Inv = True
        Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Position = 0
        Me.BindingNavigatorPositionItem.Text = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Position + 1
        'FILL DETAIL RECORD
        Me.Fill_Detail_Data()
Fix:
    End Sub
    Private Sub BindingNavigatorMovePreviousItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMovePreviousItem.Click
        On Error GoTo Fix
        If Me.BttnNew.Text = "Ca&ncel" Then
            Me.BttnNew_Click(sender, e)
        End If
        Me.Search_Inv = True
        Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Position -= 1
        Me.BindingNavigatorPositionItem.Text = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Position + 1
        'FILL DETAIL RECORD
        Me.Fill_Detail_Data()
Fix:
    End Sub
    Private Sub BindingNavigatorMoveNextItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMoveNextItem.Click
        On Error GoTo Fix
        If Me.BttnNew.Text = "Ca&ncel" Then
            Me.BttnNew_Click(sender, e)
        End If
        Me.Search_Inv = True
        Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Position += 1
        Me.BindingNavigatorPositionItem.Text = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Position + 1
        'FILL DETAIL RECORD
        Me.Fill_Detail_Data()
Fix:
    End Sub
    Private Sub BindingNavigatorMoveLastItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMoveLastItem.Click
        On Error GoTo Fix
        If Me.BttnNew.Text = "Ca&ncel" Then
            Me.BttnNew_Click(sender, e)
        End If
        Me.Search_Inv = True
        Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Position = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Count - 1
        Me.BindingNavigatorPositionItem.Text = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER11, "V_MOBILE_ISSUE_MASTER").Position + 1
        'FILL DETAIL RECORD
        Me.Fill_Detail_Data()
Fix:
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAdd.Click
        Me.TxtLoadPass.Text = Val(Me.TxtLoadPass.Text) + 1
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        If Me.BttnNew.Text = "&New" Then
            Me.Enable_All()

            Me.BttnSearch_LP.Enabled = False
            Me.BttnPrev.Enabled = False
            Me.BttnPrint.Enabled = False
            Me.BttnSave.Enabled = True
            Me.BttnSave.Text = "&Save"
            Me.BttnClose.Enabled = False

            Me.Search_Inv = False

            Me.CancelButton = Me.BttnNew

            Me.BttnNew.Text = "Ca&ncel"
            Me.Clear_All()
            Me.TxtDate.Focus()

            Me.TxtLoadPass.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nLPINV_NO) FROM MOBILE_ISSUE_MASTER") + 1

        ElseIf Me.BttnNew.Text = "Ca&ncel" Then
            If MsgBox("Are you sure to Cancel this Invoice?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Cancel Invoice?") = MsgBoxResult.Yes Then
                Me.Disable_All()

                Me.BttnPrev.Enabled = False
                Me.BttnPrint.Enabled = False
                Me.BttnSave.Enabled = False
                Me.BttnClose.Enabled = True
                Me.BttnSearch_LP.Enabled = True
                Me.BttnSave.Text = "&Save"
                Me.CancelButton = Me.BttnClose

                Me.Search_Inv = True

                Me.BttnNew.Text = "&New"

                Me.Fill_Navigator()
                Me.BindingNavigatorMoveLastItem_Click(sender, e)

            End If
        End If
    End Sub
    Private Sub BttnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnEdit.Click
        Me.asSELECT.SavedpFlg10(Rd, "SELECT * FROM V_MOBILE_ISSUE_MASTER WHERE LPINV_NO=" & Val(Me.TxtLoadPass.Text) & "")

        If Me.asSELECT.pFlg10 = True Then

            Me.BttnSave.Text = "&Update"
            Me.BttnSave.Enabled = True
            Me.BttnNew.Text = "Ca&ncel"
            Me.BttnClose.Enabled = False
            Me.BttnPrev.Enabled = False
            Me.BttnPrint.Enabled = False
            Me.Search_Inv = True

            Me.Enable_All()

        Else
            MsgBox("This LoadPass cannot Modify, Please save it first!", MsgBoxStyle.Exclamation, "(NS) - Edit!")
        End If
        
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM MOBILE_ISSUE_MASTER WHERE nLPINV_NO=" & Val(Me.TxtLoadPass.Text) & "")

        If Me.TxtDescription.Text = "Other's Description Here!" Then
            Me.TxtDescription.Text = Nothing
        End If

        If Me.BttnSave.Text = "&Save" Then

            If Me.TxtLoadPass.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbVan.SelectedIndex = -1 Or Me.CmbVan.Text = Nothing Or Me.CmbRoute.SelectedIndex = -1 Or Me.CmbRoute.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

                Me.Null_Focus()

            ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) <= 0 Then
                MsgBox("Please enter atleast one Item to save Invoice!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.DataGridView1.Focus()

            ElseIf Me.asSELECT.pFlg1 = False Then
                If Val(Me.TxtNetTotal.Text) <= 0 Then
                    MsgBox("Can't save!" & vbCrLf & "Net Total can't be '" & Val(Me.TxtNetTotal.Text) & "', Please maintain your Discounts!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                    Me.TxtDiscount.Focus()

                Else
                    'INSERT VALUES IN MOBILE ISSUE MASTER
                    Me.asInsert.SaveValueIN("INSERT INTO MOBILE_ISSUE_MASTER(nLPINV_NO, dDATE, nTOTAL_BILL, nDISCOUNT, nDISC_PERCENT, nOTHERS, sOTHER_DESC, nNET_TOTAL, sVAN_NO, nEMPLOYEE_CODE, nLOGIN_ID, nBUSINESS_CODE, nROUTE_CODE, nD_MAN_CODE, sREMARKS) VALUES(" & Val(Me.TxtLoadPass.Text) & ",'" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.TxtTotal.Text) & ", " & Val(Me.TxtDiscount.Text) & ", " & Val(Me.TxtDiscPercent.Text) & "," & Val(Me.TxtOtherDisc.Text) & ",'" & Me.TxtDescription.Text & "', " & Val(Me.TxtNetTotal.Text) & ", '" & Me.CmbVan.SelectedItem.Col2 & "'," & Val(Me.CmbS_Man.SelectedItem.Col3) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ", " & Val(Me.CmbRoute.SelectedItem.Col3) & ", " & Val(Me.CmbD_Man.SelectedItem.Col3) & ",'" & Me.TxtRemarks.Text & "')")

                    Dim i As Integer
                    For i = 0 To Me.DataGridView1.Rows.Count - 2
                        Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value)
                        Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value)
                        Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value)
                        Dim Bonus As Double = Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value)
                        Dim Tot_Pcs As Double
                        Tot_Pcs = (Pks * PPP) + (Pcs + Bonus)

                        'INSERT VALUES IN MOBILE ISSUE DETAIL
                        Me.asInsert.SaveValue("INSERT INTO MOBILE_ISSUE_DETAIL (nLPINV_NO, dDATE, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE)VALUES(" & Val(Me.TxtLoadPass.Text) & ",'" & CDate(Me.DataGridView1.Rows(i).Cells("ColDate").Value).ToString("MM-dd-yyyy") & "'," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColDisc_Rs").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & " )")

                    Next

                    Me.BttnPrev.Enabled = True
                    Me.BttnPrint.Enabled = True
                    Me.BttnSearch_LP.Enabled = True
                    Me.BttnNew.Text = "&New"
                    Me.BttnSave.Enabled = False
                    Me.BttnClose.Enabled = True

                End If

            ElseIf Me.asSELECT.pFlg1 = True Then
                MsgBox("This Load Pass # '" & Me.TxtLoadPass.Text & "' is Already Exist. " & vbCrLf & "To modify this load pass please click on 'Search Load Pass' Button", MsgBoxStyle.Exclamation, "(NS) - Already Exist!")

            End If

            'UPDATE LOAD PASS
        ElseIf Me.BttnSave.Text = "&Update" Then
            If Me.TxtLoadPass.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbVan.SelectedIndex = -1 Or Me.CmbVan.Text = Nothing Or Me.CmbRoute.SelectedIndex = -1 Or Me.CmbRoute.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.Null_Focus()

            ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) <= 0 Then
                MsgBox("Please enter atleast one Item to save Invoice!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.DataGridView1.Focus()

            ElseIf Me.asSELECT.pFlg1 = True Then

                If Val(Me.TxtNetTotal.Text) <= 0 Then
                    MsgBox("Can't save!" & vbCrLf & "Net Total can't be '" & Val(Me.TxtNetTotal.Text) & "', Please maintain your Discounts!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                    Me.TxtDiscount.Focus()

                Else
                    'UPDATE VALUES IN MOBLIE ISSUE MASTER
                    Me.asUpdate.UpdateValueIN("UPDATE MOBILE_ISSUE_MASTER SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nTOTAL_BILL=" & Val(Me.TxtTotal.Text) & ", nDISCOUNT=" & Val(Me.TxtDiscount.Text) & ", nDISC_PERCENT=" & Val(Me.TxtDiscPercent.Text) & ", nOTHERS=" & Val(Me.TxtOtherDisc.Text) & ", sOTHER_DESC='" & Me.TxtDescription.Text & "', nNET_TOTAL= " & Val(Me.TxtNetTotal.Text) & ", sVAN_NO='" & Me.CmbVan.SelectedItem.Col2 & "', nEMPLOYEE_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nROUTE_CODE=" & Val(Me.CmbRoute.SelectedItem.Col3) & ", nD_MAN_CODE=" & Val(Me.CmbD_Man.SelectedItem.Col3) & ", sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nLPINV_NO=" & Val(Me.TxtLoadPass.Text) & "")

                    Dim i As Integer
                    For i = 0 To Me.DataGridView1.Rows.Count - 2
                        Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value)
                        Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value)
                        Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value)
                        Dim Bonus As Double = Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value)
                        Dim Tot_Pcs As Double
                        Tot_Pcs = (Pks * PPP) + (Pcs + Bonus)

                        If Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then
                            'INSERT VALUES IN MOBILE ISSUE DETAIL
                            Me.asInsert.SaveValue("INSERT INTO MOBILE_ISSUE_DETAIL (nLPINV_NO, dDATE, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE)VALUES(" & Val(Me.TxtLoadPass.Text) & ",'" & CDate(Me.DataGridView1.Rows(i).Cells("ColDate").Value).ToString("MM-dd-yyyy") & "'," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColDisc_Rs").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & " )")

                        ElseIf Not Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then

                            'UPDATE VALUES IN MOBILE ISSUE DETAIL
                            Me.asUpdate.UpdateValue("UPDATE MOBILE_ISSUE_DETAIL SET nLPINV_NO=" & Val(Me.TxtLoadPass.Text) & ", dDATE='" & CDate(Me.DataGridView1.Rows(i).Cells("ColDate").Value).ToString("MM-dd-yyyy") & "', nITEM_CODE=" & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", sBATCH_NO='" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', nUNIT_COST=" & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", nUNIT_RATE=" & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", nDISC_RS=" & Val(Me.DataGridView1.Rows(i).Cells("ColDisc_Rs").Value) & ", nDISC_PER=" & Val(Me.DataGridView1.Rows(i).Cells("ColPercentage").Value) & ", nSALE_TAX=" & Val(Me.DataGridView1.Rows(i).Cells("ColSaleTax").Value) & ", nPPP=" & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", nQTY_PKS=" & Val(Me.DataGridView1.Rows(i).Cells("ColPack").Value) & ", nQTY_PCS=" & Val(Me.DataGridView1.Rows(i).Cells("ColPiece").Value) & ", nQTY_BONUS=" & Val(Me.DataGridView1.Rows(i).Cells("ColBonus").Value) & ", nQTY_Tot_PCS=" & Tot_Pcs & ", nTOTAL_VALUE=" & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & "  WHERE nID=" & Val(Me.DataGridView1.Rows(i).Cells("ColSR").Value.ToString) & "")

                        End If

                    Next

                    Me.BttnPrev.Enabled = True
                    Me.BttnPrint.Enabled = True
                    Me.BttnSearch_LP.Enabled = True
                    Me.BttnNew.Text = "&New"
                    Me.BttnNew.Enabled = True
                    Me.BttnSave.Enabled = False
                    Me.BttnClose.Enabled = True
                    Me.Disable_All()

                End If

            ElseIf Me.asSELECT.pFlg1 = False Then
                MsgBox("This Load Pass # " & Val(Me.TxtLoadPass.Text) & " is not Exist.", MsgBoxStyle.Exclamation, "(NS) - Not Exist!")

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
    Private Sub BttnSearch_LP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch_LP.Click
        On Error GoTo Fix
        frmSEARCH_LP_INV.TxtRoute.Text = Nothing
        frmSEARCH_LP_INV.TxtLoadPass.Text = Nothing
        frmSEARCH_LP_INV.TxtDateFrom.Text = Nothing
        frmSEARCH_LP_INV.TxtDateTo.Text = Nothing

        frmSEARCH_LP_INV.ShowDialog(Me)
Fix:
    End Sub
#End Region

#Region "Print Button Control"
    Private Sub BttnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrev.Click
        Dim Rpt As New rptLOADPASS_INVOICE
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = "{V_MOBILE_ISSUE_MASTER.LPINV_NO}=" & Val(Me.TxtLoadPass.Text) & ""
            Frm.Text = "Load Pass"
            Frm.MdiParent = Me.ParentForm
            Frm.Show()
            Frm.Activate()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub BttnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrint.Click
        Dim Rpt As New rptLOADPASS_INVOICE
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = "{V_MOBILE_ISSUE_MASTER.LPINV_NO}=" & Val(Me.TxtLoadPass.Text) & ""
            Frm.CRV.PrintReport()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
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
    Private Sub FillComboBox_Van()
        Dim Str1 As String = "SELECT sPLATE_NO, sDESC, sMODEL, nMADE_YEAR, sENG_NO, sCH_NO, sCOLOR, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS FROM LUP_VANS ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_VAN = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_VAN1.Clear()
        Me.daLUP_VAN.Fill(Me.DsLUP_VAN1.LUP_VANS)

        Dim dtLoading As New DataTable("LUP_VANS")

        dtLoading.Columns.Add("sPLATE_NO", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_VAN1.LUP_VANS.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("sPLATE_NO") = Me.DsLUP_VAN1.LUP_VANS.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_VAN1.LUP_VANS.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbVan.SelectedIndex = -1
        Me.CmbVan.Items.Clear()
        Me.CmbVan.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbVan.SourceDataString = New String(1) {"sDESC", "sPLATE_NO"}
        Me.CmbVan.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_Route()
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

    Private Sub Fill_Navigator()
        Dim Str5 As String = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER"
        Dim SqlCmd5 As New SDS.SqlCommand(Str5, Me.SqlConnection1)

        Me.daV_MOBILE_ISSUE_MASTER = New SDS.SqlDataAdapter(SqlCmd5)

        Me.BindingSource1.DataSource = Me.DsV_MOBILE_ISSUE_MASTER11.V_MOBILE_ISSUE_MASTER

        Me.DsV_MOBILE_ISSUE_MASTER11.Clear()
        Me.daV_MOBILE_ISSUE_MASTER.Fill(Me.DsV_MOBILE_ISSUE_MASTER11.V_MOBILE_ISSUE_MASTER)
    End Sub

    Private Sub Fill_Master_Data()
        Dim Str2 As String = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER WHERE LPINV_NO=" & Val(Me.TxtLoadPass.Text) & ""
        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)

        Me.daV_MOBILE_ISSUE_MASTER = New SDS.SqlDataAdapter(SqlCmd2)

        Me.DsV_MOBILE_ISSUE_MASTER1.Clear()
        Me.daV_MOBILE_ISSUE_MASTER.Fill(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER)

    End Sub
    Private Sub Fill_Detail_Data()
        Dim Str3 As String = "SELECT ID, LPINV_NO, dDATE, ITEM_CODE, ITEM_NAME, BATCH_NO, CONVERT(NUMERIC(18, 2), UNIT_COST) AS UNIT_COST, CONVERT(NUMERIC(18, 2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, SALE_TAX, PPP, QTY_PKS, QTY_PCS, QTY_BONUS, QTY_TOT_PCS, CONVERT(NUMERIC(18, 2), TOTAL_VALUE) AS TOTAL_VALUE, SCM_ITEM_CODE, SCM_ITEM_NAME, SCM_QTY FROM V_MOBILE_ISSUE_DETAIL WHERE LPINV_NO=" & Val(Me.TxtLoadPass.Text) & ""
        Dim SqlCmd3 As New SDS.SqlCommand(Str3, Me.SqlConnection1)
        Me.daV_MOBILE_ISSUE_DETAIL = New SDS.SqlDataAdapter(SqlCmd3)

        Me.DsV_MOBILE_ISSUE_DETAIL1.Clear()
        Me.daV_MOBILE_ISSUE_DETAIL.Fill(Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL)

        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Count - 1
            Me.DataGridView1.Rows.Add()
            Me.DataGridView1.Rows(Cnt).Cells("ColCode").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(3).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColBatch").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(5).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColName").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(4).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColCost").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(6).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColRate").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(7).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPack").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(12).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPiece").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(13).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColBonus").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(14).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPercentage").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(9).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColDisc_Rs").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(8).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColSaleTax").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(10).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColTotal").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(16).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColDate").Value = CDate(Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(2).ToString).ToString("dd-MMM-yyyy")
            Me.DataGridView1.Rows(Cnt).Cells("ColSR").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(0).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPPP").Value = Me.DsV_MOBILE_ISSUE_DETAIL1.V_MOBILE_ISSUE_DETAIL.Item(Cnt).Item(11).ToString

        Next

        Me.Search_Inv = False
    End Sub

    Private Sub Null_Focus()
        If Me.TxtLoadPass.Text = Nothing Then
            Me.TxtLoadPass.Focus()

        ElseIf Me.TxtDate.Text = Nothing Then
            Me.TxtDate.Focus()

        ElseIf Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Then
            Me.CmbS_Man.Focus()

        ElseIf Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
            Me.CmbD_Man.Focus()

        ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
            Me.CmbGroup.Focus()

        ElseIf Me.CmbVan.SelectedIndex = -1 Or Me.CmbVan.Text = Nothing Then
            Me.CmbVan.Focus()

        ElseIf Me.CmbRoute.SelectedIndex = -1 Or Me.CmbRoute.Text = Nothing Then
            Me.CmbRoute.Focus()

        End If
    End Sub

    Public Sub Disable_All()
        Dim Ctrl As Control
        For Each Ctrl In Me.Controls
            If Not Ctrl.Name = "GroupBox1" And Not Ctrl.Name = "Label3" And Not Ctrl.Name = "BindingNavigator1" Then
                Ctrl.Enabled = False
            End If
        Next
    End Sub
    Public Sub Enable_All()
        Dim Ctrl As Control
        For Each Ctrl In Me.Controls
            If Not Ctrl.Name = "GroupBox1" And Not Ctrl.Name = "Label3" And Not Ctrl.Name = "BindingNavigator1" Then
                Ctrl.Enabled = True
            End If
        Next
    End Sub

    Public Sub Clear_All()
        'Me.DsV_MOBILE_ISSUE_MASTER1.Clear()
        'Me.DsV_MOBILE_ISSUE_DETAIL1.Clear()

        Me.TxtLoadPass.Text = Nothing
        Me.CmbS_Man.SelectedIndex = -1
        Me.CmbD_Man.SelectedIndex = -1
        Me.CmbGroup.SelectedIndex = -1
        Me.CmbVan.SelectedIndex = -1
        Me.CmbRoute.SelectedIndex = -1

        Me.LblB_Pcs.Text = 0
        Me.LblPPP.Text = 0
        Me.LblRatePcs.Text = 0
        Me.LblRate.Text = 0
        Me.LblRetail.Text = 0
        Me.LblStock.Text = 0

        Me.TxtTotalItems.Text = 0

        Me.TxtTotal.Text = "0.00"
        Me.TxtDiscount.Text = "0.00"
        Me.TxtDiscPercent.Text = "0.00"

        Me.TxtRemarks.Text = Nothing

        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:
    End Sub
#End Region

End Class
