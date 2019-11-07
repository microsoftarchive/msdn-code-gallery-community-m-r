Imports SDS = System.Data.SqlClient
Public Class frmSALE_RETURN_PARTIAL
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
    Friend WithEvents TxtInvoice As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalItems As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DsLUP_ITEM1 As Neruo_Business_Solution.dsLUP_ITEM
    Friend WithEvents daLUP_ITEM As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_SALE_MASTER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_SALE_DETAIL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents BttnLoadInvoice As System.Windows.Forms.Button
    Friend WithEvents DsV_SALE_DETAIL1 As Neruo_Business_Solution.dsV_SALE_DETAIL
    Friend WithEvents DsV_SALE1 As Neruo_Business_Solution.dsV_SALE
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnReturn As System.Windows.Forms.Button
    Friend WithEvents TxtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents TxtClientName As System.Windows.Forms.TextBox
    Friend WithEvents TxtGroupID As System.Windows.Forms.TextBox
    Friend WithEvents TxtClientID As System.Windows.Forms.TextBox
    Friend WithEvents TxtDManName As System.Windows.Forms.TextBox
    Friend WithEvents TxtSManName As System.Windows.Forms.TextBox
    Friend WithEvents TxtRDate As System.Windows.Forms.TextBox
    Friend WithEvents TxtDManID As System.Windows.Forms.TextBox
    Friend WithEvents TxtSManID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ColCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBatch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPiece As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBonus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRPack As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColBE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPercentage As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDisc_Rs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSaleTax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColScmItem As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ColScmQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TxtRemarks As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
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
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSALE_RETURN_PARTIAL))
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
        Me.ColRPack = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColRPcs = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColBE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPercentage = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColDisc_Rs = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColSaleTax = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColScmItem = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.ColScmQty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColSR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPPP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TxtTotal = New System.Windows.Forms.TextBox
        Me.DsV_SALE1 = New Neruo_Business_Solution.dsV_SALE
        Me.TxtRemarks = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtTotalItems = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnReturn = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TxtDManName = New System.Windows.Forms.TextBox
        Me.TxtSManName = New System.Windows.Forms.TextBox
        Me.TxtGroupName = New System.Windows.Forms.TextBox
        Me.TxtClientName = New System.Windows.Forms.TextBox
        Me.TxtRDate = New System.Windows.Forms.TextBox
        Me.TxtDManID = New System.Windows.Forms.TextBox
        Me.TxtSManID = New System.Windows.Forms.TextBox
        Me.TxtGroupID = New System.Windows.Forms.TextBox
        Me.TxtClientID = New System.Windows.Forms.TextBox
        Me.BttnLoadInvoice = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtInvoice = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_ITEM = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlSelectCommand5 = New System.Data.SqlClient.SqlCommand
        Me.daV_SALE_MASTER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand7 = New System.Data.SqlClient.SqlCommand
        Me.daV_SALE_DETAIL = New System.Data.SqlClient.SqlDataAdapter
        Me.DsLUP_ITEM1 = New Neruo_Business_Solution.dsLUP_ITEM
        Me.DsV_SALE_DETAIL1 = New Neruo_Business_Solution.dsV_SALE_DETAIL
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DsV_SALE1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_SALE_DETAIL1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.BackColor = System.Drawing.Color.Aquamarine
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 153)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(810, 260)
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
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCode, Me.ColBatch, Me.ColName, Me.ColCost, Me.ColRate, Me.ColPack, Me.ColPiece, Me.ColBonus, Me.ColRPack, Me.ColRPcs, Me.ColBE, Me.ColPercentage, Me.ColDisc_Rs, Me.ColSaleTax, Me.ColTotal, Me.ColScmItem, Me.ColScmQty, Me.ColSR, Me.ColPPP})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle15
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.HotTrack
        Me.DataGridView1.Location = New System.Drawing.Point(3, 16)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 15
        Me.DataGridView1.RowTemplate.Height = 18
        Me.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.Size = New System.Drawing.Size(804, 241)
        Me.DataGridView1.TabIndex = 0
        '
        'ColCode
        '
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.ColCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColCode.HeaderText = "Code"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.ReadOnly = True
        Me.ColCode.Width = 50
        '
        'ColBatch
        '
        Me.ColBatch.HeaderText = "Batch #"
        Me.ColBatch.Name = "ColBatch"
        Me.ColBatch.ReadOnly = True
        Me.ColBatch.Width = 60
        '
        'ColName
        '
        Me.ColName.HeaderText = "Name"
        Me.ColName.Name = "ColName"
        Me.ColName.ReadOnly = True
        Me.ColName.Width = 150
        '
        'ColCost
        '
        Me.ColCost.HeaderText = "Cost"
        Me.ColCost.Name = "ColCost"
        Me.ColCost.ReadOnly = True
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
        Me.ColRate.ReadOnly = True
        Me.ColRate.Width = 50
        '
        'ColPack
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = "0"
        Me.ColPack.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColPack.HeaderText = "Sales Pks"
        Me.ColPack.Name = "ColPack"
        Me.ColPack.ReadOnly = True
        Me.ColPack.Width = 40
        '
        'ColPiece
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = "0"
        Me.ColPiece.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColPiece.HeaderText = "Sales Pcs"
        Me.ColPiece.Name = "ColPiece"
        Me.ColPiece.ReadOnly = True
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
        Me.ColBonus.ReadOnly = True
        Me.ColBonus.Width = 50
        '
        'ColRPack
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.NullValue = "0"
        Me.ColRPack.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColRPack.HeaderText = "Return Pks"
        Me.ColRPack.Name = "ColRPack"
        Me.ColRPack.Width = 50
        '
        'ColRPcs
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.NullValue = "0"
        Me.ColRPcs.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColRPcs.HeaderText = "Retrun Pcs"
        Me.ColRPcs.Name = "ColRPcs"
        Me.ColRPcs.Width = 50
        '
        'ColBE
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColBE.DefaultCellStyle = DataGridViewCellStyle9
        Me.ColBE.HeaderText = "B/E"
        Me.ColBE.MaxInputLength = 1
        Me.ColBE.MinimumWidth = 40
        Me.ColBE.Name = "ColBE"
        Me.ColBE.Width = 40
        '
        'ColPercentage
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Format = "N0"
        DataGridViewCellStyle10.NullValue = "0"
        Me.ColPercentage.DefaultCellStyle = DataGridViewCellStyle10
        Me.ColPercentage.HeaderText = "%age"
        Me.ColPercentage.Name = "ColPercentage"
        Me.ColPercentage.Visible = False
        Me.ColPercentage.Width = 40
        '
        'ColDisc_Rs
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Format = "C2"
        DataGridViewCellStyle11.NullValue = "0.00"
        Me.ColDisc_Rs.DefaultCellStyle = DataGridViewCellStyle11
        Me.ColDisc_Rs.HeaderText = "Disc Rs"
        Me.ColDisc_Rs.Name = "ColDisc_Rs"
        Me.ColDisc_Rs.Visible = False
        Me.ColDisc_Rs.Width = 45
        '
        'ColSaleTax
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle12.Format = "N2"
        DataGridViewCellStyle12.NullValue = "0.00"
        Me.ColSaleTax.DefaultCellStyle = DataGridViewCellStyle12
        Me.ColSaleTax.HeaderText = "S. Tax"
        Me.ColSaleTax.MaxInputLength = 3
        Me.ColSaleTax.Name = "ColSaleTax"
        Me.ColSaleTax.Visible = False
        Me.ColSaleTax.Width = 40
        '
        'ColTotal
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.Format = "N2"
        DataGridViewCellStyle13.NullValue = "0.00"
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle13
        Me.ColTotal.HeaderText = "Total"
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.Width = 70
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
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle14.Format = "N0"
        DataGridViewCellStyle14.NullValue = "0"
        Me.ColScmQty.DefaultCellStyle = DataGridViewCellStyle14
        Me.ColScmQty.HeaderText = "Scm Qty."
        Me.ColScmQty.Name = "ColScmQty"
        Me.ColScmQty.Visible = False
        Me.ColScmQty.Width = 50
        '
        'ColSR
        '
        Me.ColSR.HeaderText = "Sr. No"
        Me.ColSR.Name = "ColSR"
        Me.ColSR.Visible = False
        '
        'ColPPP
        '
        Me.ColPPP.HeaderText = "PPP"
        Me.ColPPP.Name = "ColPPP"
        Me.ColPPP.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox4.BackColor = System.Drawing.Color.Tan
        Me.GroupBox4.Controls.Add(Me.TxtTotal)
        Me.GroupBox4.Controls.Add(Me.TxtRemarks)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Location = New System.Drawing.Point(437, 419)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(382, 131)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        '
        'TxtTotal
        '
        Me.TxtTotal.BackColor = System.Drawing.Color.White
        Me.TxtTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.TOT_BILL", True))
        Me.TxtTotal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtTotal.Location = New System.Drawing.Point(104, 20)
        Me.TxtTotal.MaxLength = 50
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.ReadOnly = True
        Me.TxtTotal.Size = New System.Drawing.Size(87, 21)
        Me.TxtTotal.TabIndex = 1
        Me.TxtTotal.TabStop = False
        Me.TxtTotal.Text = "0.00"
        Me.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_SALE1
        '
        Me.DsV_SALE1.DataSetName = "dsV_SALE"
        Me.DsV_SALE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtRemarks
        '
        Me.TxtRemarks.BackColor = System.Drawing.Color.White
        Me.TxtRemarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtRemarks.Location = New System.Drawing.Point(14, 50)
        Me.TxtRemarks.MaxLength = 50
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.ReadOnly = True
        Me.TxtRemarks.Size = New System.Drawing.Size(355, 75)
        Me.TxtRemarks.TabIndex = 11
        Me.TxtRemarks.Text = "Enter Remarks Here"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(14, 19)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 23)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Total"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(7, 16)
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
        Me.TxtTotalItems.Location = New System.Drawing.Point(7, 40)
        Me.TxtTotalItems.MaxLength = 50
        Me.TxtTotalItems.Name = "TxtTotalItems"
        Me.TxtTotalItems.ReadOnly = True
        Me.TxtTotalItems.Size = New System.Drawing.Size(69, 21)
        Me.TxtTotalItems.TabIndex = 1
        Me.TxtTotalItems.TabStop = False
        Me.TxtTotalItems.Text = "0"
        Me.TxtTotalItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.TxtTotalItems)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnReturn)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 419)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(419, 131)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.BackColor = System.Drawing.Color.LightGreen
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(105, 50)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(75, 31)
        Me.BttnNew.TabIndex = 3
        Me.BttnNew.Text = "&New"
        Me.BttnNew.UseVisualStyleBackColor = False
        '
        'BttnClose
        '
        Me.BttnClose.BackColor = System.Drawing.Color.LightGreen
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(287, 50)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 31)
        Me.BttnClose.TabIndex = 4
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = False
        '
        'BttnReturn
        '
        Me.BttnReturn.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.BttnReturn.Enabled = False
        Me.BttnReturn.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnReturn.Location = New System.Drawing.Point(186, 31)
        Me.BttnReturn.Name = "BttnReturn"
        Me.BttnReturn.Size = New System.Drawing.Size(95, 68)
        Me.BttnReturn.TabIndex = 2
        Me.BttnReturn.Text = "&Return"
        Me.BttnReturn.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.BackColor = System.Drawing.Color.Pink
        Me.GroupBox3.Controls.Add(Me.TxtDManName)
        Me.GroupBox3.Controls.Add(Me.TxtSManName)
        Me.GroupBox3.Controls.Add(Me.TxtGroupName)
        Me.GroupBox3.Controls.Add(Me.TxtClientName)
        Me.GroupBox3.Controls.Add(Me.TxtRDate)
        Me.GroupBox3.Controls.Add(Me.TxtDManID)
        Me.GroupBox3.Controls.Add(Me.TxtSManID)
        Me.GroupBox3.Controls.Add(Me.TxtGroupID)
        Me.GroupBox3.Controls.Add(Me.TxtClientID)
        Me.GroupBox3.Controls.Add(Me.BttnLoadInvoice)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.TxtInvoice)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 46)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(810, 101)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'TxtDManName
        '
        Me.TxtDManName.BackColor = System.Drawing.Color.White
        Me.TxtDManName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.D_MAN", True))
        Me.TxtDManName.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDManName.Location = New System.Drawing.Point(575, 66)
        Me.TxtDManName.MaxLength = 50
        Me.TxtDManName.Name = "TxtDManName"
        Me.TxtDManName.ReadOnly = True
        Me.TxtDManName.Size = New System.Drawing.Size(215, 21)
        Me.TxtDManName.TabIndex = 18
        Me.TxtDManName.TabStop = False
        '
        'TxtSManName
        '
        Me.TxtSManName.BackColor = System.Drawing.Color.White
        Me.TxtSManName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.EMP_NAME", True))
        Me.TxtSManName.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtSManName.Location = New System.Drawing.Point(199, 66)
        Me.TxtSManName.MaxLength = 50
        Me.TxtSManName.Name = "TxtSManName"
        Me.TxtSManName.ReadOnly = True
        Me.TxtSManName.Size = New System.Drawing.Size(180, 21)
        Me.TxtSManName.TabIndex = 15
        Me.TxtSManName.TabStop = False
        '
        'TxtGroupName
        '
        Me.TxtGroupName.BackColor = System.Drawing.Color.White
        Me.TxtGroupName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.GROUP_NAME", True))
        Me.TxtGroupName.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtGroupName.Location = New System.Drawing.Point(575, 42)
        Me.TxtGroupName.MaxLength = 50
        Me.TxtGroupName.Name = "TxtGroupName"
        Me.TxtGroupName.ReadOnly = True
        Me.TxtGroupName.Size = New System.Drawing.Size(215, 21)
        Me.TxtGroupName.TabIndex = 12
        Me.TxtGroupName.TabStop = False
        '
        'TxtClientName
        '
        Me.TxtClientName.BackColor = System.Drawing.Color.White
        Me.TxtClientName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.SHOP_NAME_CC", True))
        Me.TxtClientName.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtClientName.Location = New System.Drawing.Point(199, 42)
        Me.TxtClientName.MaxLength = 50
        Me.TxtClientName.Name = "TxtClientName"
        Me.TxtClientName.ReadOnly = True
        Me.TxtClientName.Size = New System.Drawing.Size(180, 21)
        Me.TxtClientName.TabIndex = 9
        Me.TxtClientName.TabStop = False
        '
        'TxtRDate
        '
        Me.TxtRDate.BackColor = System.Drawing.Color.White
        Me.TxtRDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtRDate.Location = New System.Drawing.Point(523, 18)
        Me.TxtRDate.MaxLength = 50
        Me.TxtRDate.Name = "TxtRDate"
        Me.TxtRDate.Size = New System.Drawing.Size(92, 21)
        Me.TxtRDate.TabIndex = 4
        '
        'TxtDManID
        '
        Me.TxtDManID.BackColor = System.Drawing.Color.White
        Me.TxtDManID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.D_CODE", True))
        Me.TxtDManID.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDManID.Location = New System.Drawing.Point(523, 66)
        Me.TxtDManID.MaxLength = 50
        Me.TxtDManID.Name = "TxtDManID"
        Me.TxtDManID.ReadOnly = True
        Me.TxtDManID.Size = New System.Drawing.Size(46, 21)
        Me.TxtDManID.TabIndex = 17
        Me.TxtDManID.TabStop = False
        '
        'TxtSManID
        '
        Me.TxtSManID.BackColor = System.Drawing.Color.White
        Me.TxtSManID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.EMP_CODE", True))
        Me.TxtSManID.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtSManID.Location = New System.Drawing.Point(129, 66)
        Me.TxtSManID.MaxLength = 50
        Me.TxtSManID.Name = "TxtSManID"
        Me.TxtSManID.ReadOnly = True
        Me.TxtSManID.Size = New System.Drawing.Size(64, 21)
        Me.TxtSManID.TabIndex = 14
        Me.TxtSManID.TabStop = False
        '
        'TxtGroupID
        '
        Me.TxtGroupID.BackColor = System.Drawing.Color.White
        Me.TxtGroupID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.GROUP_ID", True))
        Me.TxtGroupID.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtGroupID.Location = New System.Drawing.Point(523, 42)
        Me.TxtGroupID.MaxLength = 50
        Me.TxtGroupID.Name = "TxtGroupID"
        Me.TxtGroupID.ReadOnly = True
        Me.TxtGroupID.Size = New System.Drawing.Size(46, 21)
        Me.TxtGroupID.TabIndex = 11
        Me.TxtGroupID.TabStop = False
        '
        'TxtClientID
        '
        Me.TxtClientID.BackColor = System.Drawing.Color.White
        Me.TxtClientID.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.CLIENT_ID", True))
        Me.TxtClientID.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtClientID.Location = New System.Drawing.Point(129, 42)
        Me.TxtClientID.MaxLength = 50
        Me.TxtClientID.Name = "TxtClientID"
        Me.TxtClientID.ReadOnly = True
        Me.TxtClientID.Size = New System.Drawing.Size(64, 21)
        Me.TxtClientID.TabIndex = 8
        Me.TxtClientID.TabStop = False
        '
        'BttnLoadInvoice
        '
        Me.BttnLoadInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.BttnLoadInvoice.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnLoadInvoice.Location = New System.Drawing.Point(270, 17)
        Me.BttnLoadInvoice.Name = "BttnLoadInvoice"
        Me.BttnLoadInvoice.Size = New System.Drawing.Size(109, 23)
        Me.BttnLoadInvoice.TabIndex = 2
        Me.BttnLoadInvoice.Text = "&Reload Inv"
        Me.BttnLoadInvoice.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(117, 21)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Enter Invoice #"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtInvoice
        '
        Me.TxtInvoice.BackColor = System.Drawing.Color.White
        Me.TxtInvoice.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtInvoice.Location = New System.Drawing.Point(129, 18)
        Me.TxtInvoice.MaxLength = 50
        Me.TxtInvoice.Name = "TxtInvoice"
        Me.TxtInvoice.Size = New System.Drawing.Size(135, 21)
        Me.TxtInvoice.TabIndex = 1
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(7, 66)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(116, 21)
        Me.Label33.TabIndex = 13
        Me.Label33.Text = "Salesman"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(385, 66)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(132, 21)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Delivery Man"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 42)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(117, 21)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Client ID && Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(385, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 21)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Group ID && Name"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(385, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 21)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Return Date"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(621, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Sale Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_SALE1, "V_SALE_MASTER.S_DATE", True))
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDate.Location = New System.Drawing.Point(698, 18)
        Me.TxtDate.MaxLength = 50
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.ReadOnly = True
        Me.TxtDate.Size = New System.Drawing.Size(92, 21)
        Me.TxtDate.TabIndex = 6
        Me.TxtDate.TabStop = False
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
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
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(831, 43)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Partial Sale Return"
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
        Me.daV_SALE_MASTER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_SALE_MASTER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SINV_NO", "SINV_NO"), New System.Data.Common.DataColumnMapping("CLIENT_ID", "CLIENT_ID"), New System.Data.Common.DataColumnMapping("SHOP_NAME_CC", "SHOP_NAME_CC"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("CASH_CLIENT", "CASH_CLIENT"), New System.Data.Common.DataColumnMapping("CASH_MEMO", "CASH_MEMO"), New System.Data.Common.DataColumnMapping("LPINV_NO", "LPINV_NO"), New System.Data.Common.DataColumnMapping("S_DATE", "S_DATE"), New System.Data.Common.DataColumnMapping("DISP_DATE", "DISP_DATE"), New System.Data.Common.DataColumnMapping("VEHICLE", "VEHICLE"), New System.Data.Common.DataColumnMapping("FREIGHT", "FREIGHT"), New System.Data.Common.DataColumnMapping("UNLOADING", "UNLOADING"), New System.Data.Common.DataColumnMapping("TR_NO", "TR_NO"), New System.Data.Common.DataColumnMapping("TR_QTY", "TR_QTY"), New System.Data.Common.DataColumnMapping("TOT_BILL", "TOT_BILL"), New System.Data.Common.DataColumnMapping("DISC_RS", "DISC_RS"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("DISC_OTHER", "DISC_OTHER"), New System.Data.Common.DataColumnMapping("OTHER_DESC", "OTHER_DESC"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL"), New System.Data.Common.DataColumnMapping("EMP_CODE", "EMP_CODE"), New System.Data.Common.DataColumnMapping("EMP_NAME", "EMP_NAME"), New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("D_CODE", "D_CODE"), New System.Data.Common.DataColumnMapping("D_MAN", "D_MAN"), New System.Data.Common.DataColumnMapping("USER_ID", "USER_ID"), New System.Data.Common.DataColumnMapping("USER_NAME", "USER_NAME"), New System.Data.Common.DataColumnMapping("REMARKS", "REMARKS")})})
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
        'DsLUP_ITEM1
        '
        Me.DsLUP_ITEM1.DataSetName = "dsLUP_ITEM"
        Me.DsLUP_ITEM1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsV_SALE_DETAIL1
        '
        Me.DsV_SALE_DETAIL1.DataSetName = "dsV_SALE_DETAIL"
        Me.DsV_SALE_DETAIL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmSALE_RETURN_PARTIAL
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(831, 565)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmSALE_RETURN_PARTIAL"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Partial Sale Return"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.DsV_SALE1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_SALE_DETAIL1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asInsert As New AssInsert
    Dim AsIsrt As New AssInsert
    Dim asUpdate As New AssUpdate
    Dim asDelete As New AssDelete
    Dim asSELECT As New AssSelect
    Dim asTXT As New AssTextBox
    Dim asNum As New AssNumPress
    Dim asMAX As New AssMaxNo
    Dim Rd As System.Data.SqlClient.SqlDataReader
    Dim Search_Inv As Boolean = False
#End Region

#Region "FORM CONTROL"

    Private Sub frmSALE_RETURN_PARTIAL_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.BttnReturn.Enabled = True Then
            MsgBox("Can't close without Saving OR Cancelling Return Invoice", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        End If
    End Sub
    Private Sub frmSALE_RETURN_PARTIAL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        'Me.Disable_All()
        'Me.BttnReturn.Enabled = False

        Me.TxtRDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.BttnLoadInvoice_Click(sender, e)
        'Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmSALE_RETURN_PARTIAL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.GotFocus, TxtDate.GotFocus, TxtTotal.GotFocus, TxtTotalItems.GotFocus, TxtRemarks.GotFocus, TxtClientID.GotFocus, TxtClientName.GotFocus, TxtDManID.GotFocus, TxtDManName.GotFocus, TxtGroupID.GotFocus, TxtGroupName.GotFocus, TxtRDate.GotFocus, TxtSManID.GotFocus, TxtSManName.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.LostFocus, TxtDate.LostFocus, TxtTotal.LostFocus, TxtTotalItems.LostFocus, TxtRemarks.LostFocus, TxtClientID.LostFocus, TxtClientName.LostFocus, TxtDManID.LostFocus, TxtDManName.LostFocus, TxtGroupID.LostFocus, TxtGroupName.LostFocus, TxtRDate.LostFocus, TxtSManID.LostFocus, TxtSManName.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

                Case "TxtRDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub

#End Region

#Region "DataGridView Control"
    'Dim ItemCode_Old As String

    'Private Sub DataGridView1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
    '    ItemCode_Old = Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value
    'End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If Me.Search_Inv = False Then
            Try
                If Not Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value Is Nothing Then
                    Dim Rate, PPP, Pks, Pcs, Line_Tot As Double
                    Rate = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColRate").Value)
                    PPP = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPPP").Value)
                    Pks = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColRPack").Value)
                    Pcs = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColRPcs").Value)

                    Line_Tot = ((Rate / PPP) * ((Pks * PPP) + Pcs))
                    Line_Tot = Decimal.Round(CDec(Line_Tot), 2)

                    If Pks > Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPack").Value) Then
                        MsgBox("Wrong Return Packs Value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Entry")
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColRPack").Value = 0
                        Exit Sub

                    ElseIf Pks > Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPack").Value) Then
                        MsgBox("Wrong Return Packs Value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Entry")
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value = 0
                        Exit Sub

                    End If

                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColTotal").Value = Line_Tot
                    Dim i As Integer
                    Me.TxtTotal.Text = "0.00"

                    For i = 0 To Me.DataGridView1.Rows.Count - 1
                        Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value)
                    Next

                    'If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value = 1 Then
                    '    If Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCode").Value Is Nothing And Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value Is Nothing Then
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value = 0
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value = 0

                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value = 1
                    '    End If

                    '    If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value > 0 Then
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value = Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value + Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value = 0

                    '    End If

                    'ElseIf Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value > 1 Then
                    '    If Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCode").Value Is Nothing And Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value Is Nothing Then
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value = 0
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value = 0

                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value = 1
                    '    End If

                    '    If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value <= Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value Then
                    '        Dim PPP1, PCS1, PKS1 As Integer
                    '        PPP1 = Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColPPP").Value
                    '        PCS1 = Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value
                    '        PKS1 = PCS1 / PPP1
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value = Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPack").Value + PKS1
                    '        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColRPcs").Value = PCS1 Mod PPP1
                    '    End If
                    'End If

                ElseIf Me.DataGridView1.Rows(e.RowIndex).Cells("ColSR").Value Is Nothing Then
                    MsgBox("New Item is not allow to ADD", MsgBoxStyle.Exclamation, "(NS) - Not Allowed!")
                    Me.DataGridView1.Rows.RemoveAt(e.RowIndex)
                End If

                Me.TxtTotalItems.Text = Me.DataGridView1.Rows.Count - 1

            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        End If


    End Sub

    'Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
    '    ItemCode_Old = Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value
    '    Me.DataGridView1_CellValueChanged(sender, e)
    'End Sub
    'Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
    '    Dim i As Integer
    '    Me.TxtTotal.Text = "0.00"
    '    For i = 0 To Me.DataGridView1.Rows.Count - 1
    '        Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value)
    '    Next

    'End Sub

    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        MsgBox("Record Deletion Not Allowed!", MsgBoxStyle.Critical, "(NS) - Not Allowed!")
        e.Cancel = True
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
    Private Sub BttnLoadInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnLoadInvoice.Click
        If Val(Me.TxtInvoice.Text) <= 0 Then
            MsgBox("Please Enter Correct Invoice No", MsgBoxStyle.Exclamation, "(NS) - Wrong Invoice!")
            Me.TxtInvoice.Focus()
            Exit Sub

        Else
            Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM SALE_MASTER WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")
            Dim Ret_Status As Double = Me.asMAX.LoadValue(Rd, "SELECT nRET_STATUS FROM SALE_MASTER WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

            If Me.asSELECT.pFlg1 = False Then
                MsgBox("Plase enter correct Invoice No!", MsgBoxStyle.Exclamation, "(NS) - Wrong Invoice")
                Me.TxtInvoice.Focus()
                Exit Sub

            ElseIf Ret_Status = 1 Then
                MsgBox("This Invoice # " & Val(Me.TxtInvoice.Text) & " is Already Whole Returned" & vbCrLf & "Can't be Partially Return!", MsgBoxStyle.Exclamation, "(NS) - Wrong Invoice!")
                Me.TxtInvoice.Focus()
                Exit Sub

            Else
                'FILL MASTER RECORD
                Me.Fill_Master_Data()

                Me.Search_Inv = True

                'FILL DETAIL RECORD
                Me.Fill_Detail_Data()

                Me.Search_Inv = False

                Me.TxtRDate.Focus()
            End If

        End If
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        If Me.BttnNew.Text = "&New" Then
            Me.Enable_All()

            Me.BttnReturn.Enabled = True
            Me.BttnClose.Enabled = False

            Me.CancelButton = Me.BttnNew

            Me.Clear_All()
            Me.BttnNew.Text = "Ca&ncel"

        ElseIf Me.BttnNew.Text = "Ca&ncel" Then
            If MsgBox("Are you sure to Cancel this Invoice?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Cancel Invoice?") = MsgBoxResult.Yes Then
                Me.Disable_All()

                Me.BttnReturn.Enabled = False
                Me.BttnClose.Enabled = True
                Me.CancelButton = Me.BttnClose

                Me.Clear_All()
                Me.BttnNew.Text = "&New"
            End If
        End If
    End Sub

    Private Sub BttnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnReturn.Click

TOP:
        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM SALE_MASTER WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")
        Dim Ret_Status As Double = Me.asMAX.LoadValue(Rd, "SELECT nRET_STATUS FROM SALE_MASTER WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

        If Me.asSELECT.pFlg1 = False Then
            MsgBox("Plase enter correct Invoice No!", MsgBoxStyle.Exclamation, "(NS) - Wrong Invoice")
            Me.TxtInvoice.Focus()
            Exit Sub

        ElseIf Ret_Status = 1 Then
            MsgBox("This Invoice # " & Val(Me.TxtInvoice.Text) & " is Already Whole Returned" & vbCrLf & "Can't be Partially Return!", MsgBoxStyle.Exclamation, "(NS) - Wrong Invoice!")
            Me.TxtInvoice.Focus()
            Exit Sub

        End If

        If Me.TxtRemarks.Text = "Enter Remarks Here" Then
            Me.TxtRemarks = Nothing
        End If

        If Me.TxtInvoice.Text = Nothing Or Me.TxtRDate.Text = Nothing Then
            MsgBox("Please enter description OR correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

            Me.Null_Focus()

        ElseIf Me.DataGridView1.Rows.Count = 1 Then
            Me.Fill_Detail_Data()
            GoTo TOP

        ElseIf Me.asSELECT.pFlg1 = True Then
            If Val(Me.DataGridView1.Rows(0).Cells("ColRPack").Value) <= 0 And Val(Me.DataGridView1.Rows(0).Cells("ColRPcs").Value) <= 0 Then
                MsgBox("Please Enter the Return Value First!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                Exit Sub
            End If

            'INSERT VALUES IN SALE RETURN MASTER
            Me.AsIsrt.SaveValue("INSERT INTO SALE_RET_MASTER(nSINV_NO, nCLIENT_ID, dDATE, nTOTAL_BILL, nDISCOUNT, nDISC_PERCENT, nOTHERS, sOTHER_DESC, nNET_TOTAL, nEMPLOYEE_CODE, nLOGIN_ID, nBUSINESS_CODE, sREMARKS) VALUES(" & Val(Me.TxtInvoice.Text) & "," & Val(Me.TxtClientID.Text) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.TxtTotal.Text) & ", 0, 0, 0, '', " & Val(Me.TxtTotal.Text) & ", " & Val(Me.TxtSManID.Text) & ", 10, " & Val(Me.TxtGroupID.Text) & ", '" & Me.TxtRemarks.Text & "')")
            Dim SR_INV As Double = Me.asMAX.LoadValue(Rd, "SELECT MAX(nSRINV_NO) FROM SALE_RET_MASTER")

            Dim i As Integer
            For i = 0 To Me.DataGridView1.Rows.Count - 2
                Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value)
                Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells("ColRPack").Value)
                Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells("ColRPcs").Value)
                Dim Tot_Pcs As Double
                Tot_Pcs = (Pks * PPP) + (Pcs)

                If Tot_Pcs > 0 Then
                    'INSERT VALUES IN SALE RETURN DETAIL
                    If Me.DataGridView1.Rows(i).Cells("ColBE").Value.ToString.ToUpper = "B" Then
                        Me.AsIsrt.SaveValue("INSERT INTO SALE_RETURN_NORMAL (nSRINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, nRET_TYPE, dDATe) VALUES (" & Val(SR_INV) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", 0, 0, 0, " & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRPcs").Value) & ", 0, " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", 3, '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")

                    ElseIf Me.DataGridView1.Rows(i).Cells("ColBE").Value.ToString.ToUpper = "E" Then
                        Me.AsIsrt.SaveValue("INSERT INTO SALE_RETURN_NORMAL (nSRINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, nRET_TYPE, dDATE) VALUES (" & Val(SR_INV) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", 0, 0, 0, " & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRPcs").Value) & ", 0, " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", 2, '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")

                    Else
                        Me.AsIsrt.SaveValue("INSERT INTO SALE_RETURN_NORMAL (nSRINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, nRET_TYPE, dDATE) VALUES (" & Val(SR_INV) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColBatch").Value & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCost").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRate").Value) & ", 0, 0, 0, " & Val(Me.DataGridView1.Rows(i).Cells("ColPPP").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRPack").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColRPcs").Value) & ", 0, " & Tot_Pcs & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColTotal").Value) & ", 1, '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")

                    End If

                Else
                    GoTo Nxt
                End If
Nxt:
            Next

            Me.AsIsrt.SaveValueIN("UPDATE SALE_MASTER SET nRET_STATUS=0 WHERE nSINV_NO=" & Val(Me.TxtInvoice.Text) & "")

            Me.BttnNew.Text = "&New"
            Me.BttnReturn.Enabled = False
            Me.BttnClose.Enabled = True

        End If

    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        If MsgBox("Are you sure to Close?", MsgBoxStyle.Question + vbYesNo, "(NS) - Close?") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Sub and Functions"
    Private Sub Fill_Master_Data()
        Dim Str2 As String = "SELECT SINV_NO, CLIENT_ID, SHOP_NAME + ' : ' + CASH_CLIENT AS SHOP_NAME_CC, SHOP_NAME, CASH_CLIENT, CASH_MEMO, LPINV_NO, S_DATE, DISP_DATE, VEHICLE, CONVERT(NUMERIC(18, 2), FREIGHT) AS FREIGHT, CONVERT(NUMERIC(18, 2), UNLOADING) AS UNLOADING, TR_NO, TR_QTY, CONVERT(NUMERIC(18, 2), TOT_BILL) AS TOT_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), DISC_OTHER) AS DISC_OTHER, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, EMP_CODE, EMP_NAME, GROUP_ID, GROUP_NAME, D_CODE, D_MAN, USER_ID, USER_NAME, REMARKS FROM V_SALE_MASTER WHERE SINV_NO=" & Val(Me.TxtInvoice.Text) & ""
        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)

        Me.daV_SALE_MASTER = New SDS.SqlDataAdapter(SqlCmd2)

        Me.DsV_SALE1.V_SALE_MASTER.Clear()
        Me.daV_SALE_MASTER.Fill(Me.DsV_SALE1.V_SALE_MASTER)

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
            'Me.DataGridView1.Rows(Cnt).Cells("ColTotal").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(14).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColSR").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(0).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPPP").Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(9).ToString

        Next
    End Sub

    Private Sub Null_Focus()
        If Me.TxtInvoice.Text = Nothing Then
            Me.TxtInvoice.Focus()

        ElseIf Me.TxtRDate.Text = Nothing Then
            Me.TxtRDate.Focus()

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
        Me.TxtClientID.Text = Nothing
        Me.TxtClientName.Text = Nothing
        Me.TxtGroupID.Text = Nothing
        Me.TxtGroupName.Text = Nothing
        Me.TxtSManID.Text = Nothing
        Me.TxtSManName.Text = Nothing
        Me.TxtDManID.Text = Nothing
        Me.TxtDManName.Text = Nothing

        Me.TxtTotalItems.Text = 0

        Me.TxtTotal.Text = "0.00"

        Me.TxtRemarks.Text = "Enter Remarks Here!"

        Me.TxtInvoice.Focus()

        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:
    End Sub
#End Region

End Class
