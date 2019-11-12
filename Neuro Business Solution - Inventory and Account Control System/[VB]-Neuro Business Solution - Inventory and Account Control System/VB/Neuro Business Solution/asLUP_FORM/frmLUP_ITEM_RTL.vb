Imports SDS = System.Data.SqlClient
Public Class frmLUP_ITEM_RTL
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
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
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
    Friend WithEvents DmnClaim As System.Windows.Forms.DomainUpDown
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents TxtItemID As System.Windows.Forms.TextBox
    Friend WithEvents TxtNick As System.Windows.Forms.TextBox
    Friend WithEvents TxtCost As System.Windows.Forms.TextBox
    Friend WithEvents TxtRate As System.Windows.Forms.TextBox
    Friend WithEvents TxtPcsDesc As System.Windows.Forms.TextBox
    Friend WithEvents TxtPackDesc As System.Windows.Forms.TextBox
    Friend WithEvents TxtRetail As System.Windows.Forms.TextBox
    Friend WithEvents TxtPPP As System.Windows.Forms.TextBox
    Friend WithEvents CmbCompany As MTGCComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TxtB_Qty As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TxtB_Pcs As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TxtMinStock As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtSaleTax As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TxtMaxStock As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtOpenStockValue As System.Windows.Forms.TextBox
    Friend WithEvents TxtOpenStock As System.Windows.Forms.TextBox
    Friend WithEvents DmnStatus As System.Windows.Forms.DomainUpDown
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_VENDOR As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLUP_VENDOR1 As Neruo_Business_Solution.dsLUP_VENDOR
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_ITEM As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents CmbItemCategory As MTGCComboBox
    Friend WithEvents DsV_LUP_ITEM1 As Neruo_Business_Solution.dsV_LUP_ITEM
    Friend WithEvents daLUP_ITEM_CAT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_ITEM_CAT1 As Neruo_Business_Solution.dsLUP_ITEM_CAT
    Friend WithEvents daITEM_LIST As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsITEM_LIST1 As Neruo_Business_Solution.dsITEM_LIST
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents TxtRetAge As System.Windows.Forms.TextBox
    Friend WithEvents TxtPurcAge As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TxtSearchCompany As System.Windows.Forms.TextBox
    Friend WithEvents BttnAuto As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_ITEM_RTL))
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
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtSearchCompany = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TxtRetAge = New System.Windows.Forms.TextBox
        Me.TxtOpenStock = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.TxtPurcAge = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.TxtOpenStockValue = New System.Windows.Forms.TextBox
        Me.CmbItemCategory = New MTGCComboBox
        Me.DsV_LUP_ITEM1 = New Neruo_Business_Solution.dsV_LUP_ITEM
        Me.BttnAuto = New System.Windows.Forms.Button
        Me.Label16 = New System.Windows.Forms.Label
        Me.DmnStatus = New System.Windows.Forms.DomainUpDown
        Me.DmnClaim = New System.Windows.Forms.DomainUpDown
        Me.TxtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtItemID = New System.Windows.Forms.TextBox
        Me.TxtNick = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtCost = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtRate = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtMaxStock = New System.Windows.Forms.TextBox
        Me.TxtMinStock = New System.Windows.Forms.TextBox
        Me.TxtB_Qty = New System.Windows.Forms.TextBox
        Me.TxtPcsDesc = New System.Windows.Forms.TextBox
        Me.TxtPackDesc = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtSaleTax = New System.Windows.Forms.TextBox
        Me.TxtRetail = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtB_Pcs = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtPPP = New System.Windows.Forms.TextBox
        Me.CmbCompany = New MTGCComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_VENDOR = New System.Data.SqlClient.SqlDataAdapter
        Me.DsLUP_VENDOR1 = New Neruo_Business_Solution.dsLUP_VENDOR
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_ITEM = New System.Data.SqlClient.SqlDataAdapter
        Me.daLUP_ITEM_CAT = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_ITEM_CAT1 = New Neruo_Business_Solution.dsLUP_ITEM_CAT
        Me.daITEM_LIST = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.DsITEM_LIST1 = New Neruo_Business_Solution.dsITEM_LIST
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsV_LUP_ITEM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_VENDOR1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_ITEM_CAT1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsITEM_LIST1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(705, 569)
        Me.Panel1.TabIndex = 0
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(14, 3)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(142, 29)
        Me.BttnPrevForm.TabIndex = 1
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Item Categories"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(548, 3)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(142, 29)
        Me.BttnNextForm.TabIndex = 2
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Opening Stock"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.TxtSearchCompany)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(16, 321)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(671, 233)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader3, Me.ColumnHeader5})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 50)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(665, 180)
        Me.ListView1.TabIndex = 0
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
        Me.ColumnHeader2.Text = "Item Name"
        Me.ColumnHeader2.Width = 160
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Company"
        Me.ColumnHeader4.Width = 158
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Stock (Hand)"
        Me.ColumnHeader3.Width = 120
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "In Use (Y/N)"
        Me.ColumnHeader5.Width = 120
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(332, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(75, 23)
        Me.Label24.TabIndex = 3
        Me.Label24.Text = "Company"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(78, 24)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(50, 23)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = "Item"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtSearchCompany
        '
        Me.TxtSearchCompany.BackColor = System.Drawing.Color.White
        Me.TxtSearchCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearchCompany.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearchCompany.Location = New System.Drawing.Point(413, 24)
        Me.TxtSearchCompany.MaxLength = 50
        Me.TxtSearchCompany.Name = "TxtSearchCompany"
        Me.TxtSearchCompany.Size = New System.Drawing.Size(192, 23)
        Me.TxtSearchCompany.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 23)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Sea&rch"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtSearch
        '
        Me.TxtSearch.BackColor = System.Drawing.Color.White
        Me.TxtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(134, 24)
        Me.TxtSearch.MaxLength = 50
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(192, 23)
        Me.TxtSearch.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(567, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 275)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(16, 23)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(16, 229)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(16, 126)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.TxtRetAge)
        Me.GroupBox3.Controls.Add(Me.TxtOpenStock)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.TxtPurcAge)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.TxtOpenStockValue)
        Me.GroupBox3.Controls.Add(Me.CmbItemCategory)
        Me.GroupBox3.Controls.Add(Me.BttnAuto)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.DmnStatus)
        Me.GroupBox3.Controls.Add(Me.DmnClaim)
        Me.GroupBox3.Controls.Add(Me.TxtName)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtItemID)
        Me.GroupBox3.Controls.Add(Me.TxtNick)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.TxtCost)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtRate)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TxtMaxStock)
        Me.GroupBox3.Controls.Add(Me.TxtMinStock)
        Me.GroupBox3.Controls.Add(Me.TxtB_Qty)
        Me.GroupBox3.Controls.Add(Me.TxtPcsDesc)
        Me.GroupBox3.Controls.Add(Me.TxtPackDesc)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.TxtSaleTax)
        Me.GroupBox3.Controls.Add(Me.TxtRetail)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.TxtB_Pcs)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.TxtPPP)
        Me.GroupBox3.Controls.Add(Me.CmbCompany)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(16, 38)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(545, 277)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'TxtRetAge
        '
        Me.TxtRetAge.BackColor = System.Drawing.Color.White
        Me.TxtRetAge.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRetAge.Location = New System.Drawing.Point(166, 169)
        Me.TxtRetAge.Name = "TxtRetAge"
        Me.TxtRetAge.Size = New System.Drawing.Size(34, 23)
        Me.TxtRetAge.TabIndex = 15
        Me.TxtRetAge.Text = "0"
        Me.TxtRetAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtOpenStock
        '
        Me.TxtOpenStock.BackColor = System.Drawing.Color.White
        Me.TxtOpenStock.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOpenStock.Location = New System.Drawing.Point(422, 221)
        Me.TxtOpenStock.Name = "TxtOpenStock"
        Me.TxtOpenStock.Size = New System.Drawing.Size(117, 23)
        Me.TxtOpenStock.TabIndex = 40
        Me.TxtOpenStock.Text = "0"
        Me.TxtOpenStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(301, 246)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(115, 23)
        Me.Label17.TabIndex = 41
        Me.Label17.Text = "Open Stk Value"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPurcAge
        '
        Me.TxtPurcAge.BackColor = System.Drawing.Color.White
        Me.TxtPurcAge.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPurcAge.Location = New System.Drawing.Point(166, 144)
        Me.TxtPurcAge.Name = "TxtPurcAge"
        Me.TxtPurcAge.Size = New System.Drawing.Size(34, 23)
        Me.TxtPurcAge.TabIndex = 12
        Me.TxtPurcAge.Text = "0"
        Me.TxtPurcAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(301, 195)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(115, 23)
        Me.Label21.TabIndex = 37
        Me.Label21.Text = "Item Category*"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtOpenStockValue
        '
        Me.TxtOpenStockValue.BackColor = System.Drawing.Color.White
        Me.TxtOpenStockValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOpenStockValue.Location = New System.Drawing.Point(422, 246)
        Me.TxtOpenStockValue.Name = "TxtOpenStockValue"
        Me.TxtOpenStockValue.ReadOnly = True
        Me.TxtOpenStockValue.Size = New System.Drawing.Size(117, 23)
        Me.TxtOpenStockValue.TabIndex = 42
        Me.TxtOpenStockValue.TabStop = False
        Me.TxtOpenStockValue.Text = "0"
        Me.TxtOpenStockValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmbItemCategory
        '
        Me.CmbItemCategory.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbItemCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbItemCategory.ColumnNum = 2
        Me.CmbItemCategory.ColumnWidth = "140;40"
        Me.CmbItemCategory.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.ITEM_CAT", True))
        Me.CmbItemCategory.DisplayMember = "Text"
        Me.CmbItemCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbItemCategory.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbItemCategory.DropDownForeColor = System.Drawing.Color.White
        Me.CmbItemCategory.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbItemCategory.DropDownWidth = 200
        Me.CmbItemCategory.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbItemCategory.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbItemCategory.GridLineHorizontal = False
        Me.CmbItemCategory.GridLineVertical = True
        Me.CmbItemCategory.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbItemCategory.Location = New System.Drawing.Point(422, 195)
        Me.CmbItemCategory.ManagingFastMouseMoving = True
        Me.CmbItemCategory.ManagingFastMouseMovingInterval = 30
        Me.CmbItemCategory.Name = "CmbItemCategory"
        Me.CmbItemCategory.Size = New System.Drawing.Size(117, 24)
        Me.CmbItemCategory.TabIndex = 38
        '
        'DsV_LUP_ITEM1
        '
        Me.DsV_LUP_ITEM1.DataSetName = "dsV_LUP_ITEM"
        Me.DsV_LUP_ITEM1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BttnAuto
        '
        Me.BttnAuto.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnAuto.Location = New System.Drawing.Point(216, 18)
        Me.BttnAuto.Name = "BttnAuto"
        Me.BttnAuto.Size = New System.Drawing.Size(79, 23)
        Me.BttnAuto.TabIndex = 2
        Me.BttnAuto.TabStop = False
        Me.BttnAuto.Text = "&Auto Code"
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(301, 221)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(115, 23)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "Open Stock"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DmnStatus
        '
        Me.DmnStatus.BackColor = System.Drawing.Color.White
        Me.DmnStatus.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.STATUS", True))
        Me.DmnStatus.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DmnStatus.Items.Add("No")
        Me.DmnStatus.Items.Add("Yes")
        Me.DmnStatus.Location = New System.Drawing.Point(463, 170)
        Me.DmnStatus.Name = "DmnStatus"
        Me.DmnStatus.ReadOnly = True
        Me.DmnStatus.Size = New System.Drawing.Size(76, 23)
        Me.DmnStatus.TabIndex = 36
        '
        'DmnClaim
        '
        Me.DmnClaim.BackColor = System.Drawing.Color.White
        Me.DmnClaim.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.CLAIMABLE", True))
        Me.DmnClaim.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DmnClaim.Items.Add("No")
        Me.DmnClaim.Items.Add("Yes")
        Me.DmnClaim.Location = New System.Drawing.Point(463, 145)
        Me.DmnClaim.Name = "DmnClaim"
        Me.DmnClaim.ReadOnly = True
        Me.DmnClaim.Size = New System.Drawing.Size(76, 23)
        Me.DmnClaim.TabIndex = 34
        '
        'TxtName
        '
        Me.TxtName.BackColor = System.Drawing.Color.White
        Me.TxtName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.sITEM_NAME", True))
        Me.TxtName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.Location = New System.Drawing.Point(96, 69)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(199, 23)
        Me.TxtName.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Name*"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Item Code*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtItemID
        '
        Me.TxtItemID.BackColor = System.Drawing.Color.White
        Me.TxtItemID.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemID.Location = New System.Drawing.Point(96, 18)
        Me.TxtItemID.MaxLength = 50
        Me.TxtItemID.Name = "TxtItemID"
        Me.TxtItemID.Size = New System.Drawing.Size(120, 23)
        Me.TxtItemID.TabIndex = 1
        '
        'TxtNick
        '
        Me.TxtNick.BackColor = System.Drawing.Color.White
        Me.TxtNick.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.sNICK", True))
        Me.TxtNick.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNick.Location = New System.Drawing.Point(96, 94)
        Me.TxtNick.Name = "TxtNick"
        Me.TxtNick.Size = New System.Drawing.Size(199, 23)
        Me.TxtNick.TabIndex = 8
        Me.TxtNick.Text = "*"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 23)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Company*"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 23)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Formula*"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCost
        '
        Me.TxtCost.BackColor = System.Drawing.Color.White
        Me.TxtCost.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.UNIT_COST", True))
        Me.TxtCost.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCost.Location = New System.Drawing.Point(206, 144)
        Me.TxtCost.Name = "TxtCost"
        Me.TxtCost.Size = New System.Drawing.Size(89, 23)
        Me.TxtCost.TabIndex = 13
        Me.TxtCost.Text = "0.00"
        Me.TxtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 144)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(152, 23)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Purchase Price*"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtRate
        '
        Me.TxtRate.BackColor = System.Drawing.Color.White
        Me.TxtRate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.UNIT_RATE", True))
        Me.TxtRate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(206, 119)
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(89, 23)
        Me.TxtRate.TabIndex = 10
        Me.TxtRate.Text = "0.00"
        Me.TxtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 119)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(152, 23)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Sale Price*"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(301, 93)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(115, 23)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Max Stock Limit"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(301, 68)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(115, 23)
        Me.Label23.TabIndex = 27
        Me.Label23.Text = "Min Stock Limit"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(301, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(115, 23)
        Me.Label19.TabIndex = 23
        Me.Label19.Text = "Bonus Qty"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 220)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(152, 24)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Piece Description*"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtMaxStock
        '
        Me.TxtMaxStock.BackColor = System.Drawing.Color.White
        Me.TxtMaxStock.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.nMAX_STOCK", True))
        Me.TxtMaxStock.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMaxStock.Location = New System.Drawing.Point(422, 93)
        Me.TxtMaxStock.Name = "TxtMaxStock"
        Me.TxtMaxStock.Size = New System.Drawing.Size(117, 23)
        Me.TxtMaxStock.TabIndex = 30
        Me.TxtMaxStock.Text = "0"
        Me.TxtMaxStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtMinStock
        '
        Me.TxtMinStock.BackColor = System.Drawing.Color.White
        Me.TxtMinStock.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.nMIN_STOCK", True))
        Me.TxtMinStock.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMinStock.Location = New System.Drawing.Point(422, 68)
        Me.TxtMinStock.Name = "TxtMinStock"
        Me.TxtMinStock.Size = New System.Drawing.Size(117, 23)
        Me.TxtMinStock.TabIndex = 28
        Me.TxtMinStock.Text = "0"
        Me.TxtMinStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtB_Qty
        '
        Me.TxtB_Qty.BackColor = System.Drawing.Color.White
        Me.TxtB_Qty.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.nBONUS_QTY", True))
        Me.TxtB_Qty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtB_Qty.Location = New System.Drawing.Point(422, 18)
        Me.TxtB_Qty.Name = "TxtB_Qty"
        Me.TxtB_Qty.Size = New System.Drawing.Size(117, 23)
        Me.TxtB_Qty.TabIndex = 24
        Me.TxtB_Qty.Text = "0"
        Me.TxtB_Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtPcsDesc
        '
        Me.TxtPcsDesc.BackColor = System.Drawing.Color.White
        Me.TxtPcsDesc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.sPIECE_DESC", True))
        Me.TxtPcsDesc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPcsDesc.Location = New System.Drawing.Point(166, 221)
        Me.TxtPcsDesc.Name = "TxtPcsDesc"
        Me.TxtPcsDesc.Size = New System.Drawing.Size(129, 23)
        Me.TxtPcsDesc.TabIndex = 20
        Me.TxtPcsDesc.Text = "Pcs"
        '
        'TxtPackDesc
        '
        Me.TxtPackDesc.BackColor = System.Drawing.Color.White
        Me.TxtPackDesc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.sPACK_DESC", True))
        Me.TxtPackDesc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPackDesc.Location = New System.Drawing.Point(166, 195)
        Me.TxtPackDesc.Name = "TxtPackDesc"
        Me.TxtPackDesc.Size = New System.Drawing.Size(129, 23)
        Me.TxtPackDesc.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 195)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(152, 23)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Pack Description*"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(301, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(115, 23)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Sale Tax %"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 169)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(152, 23)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Retail Price"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtSaleTax
        '
        Me.TxtSaleTax.BackColor = System.Drawing.Color.White
        Me.TxtSaleTax.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.nSALE_TAX", True))
        Me.TxtSaleTax.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleTax.Location = New System.Drawing.Point(422, 119)
        Me.TxtSaleTax.Name = "TxtSaleTax"
        Me.TxtSaleTax.Size = New System.Drawing.Size(117, 23)
        Me.TxtSaleTax.TabIndex = 32
        Me.TxtSaleTax.Text = "0"
        Me.TxtSaleTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtRetail
        '
        Me.TxtRetail.BackColor = System.Drawing.Color.White
        Me.TxtRetail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.UNIT_RETAIL", True))
        Me.TxtRetail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRetail.Location = New System.Drawing.Point(206, 169)
        Me.TxtRetail.Name = "TxtRetail"
        Me.TxtRetail.Size = New System.Drawing.Size(89, 23)
        Me.TxtRetail.TabIndex = 16
        Me.TxtRetail.Text = "0.00"
        Me.TxtRetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(301, 170)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(156, 23)
        Me.Label20.TabIndex = 35
        Me.Label20.Text = "In Use (Y/N)*"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(301, 43)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(115, 23)
        Me.Label18.TabIndex = 25
        Me.Label18.Text = "Bonus on Pcs."
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(301, 145)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(156, 23)
        Me.Label13.TabIndex = 33
        Me.Label13.Text = "Claimable (Y/N)*"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtB_Pcs
        '
        Me.TxtB_Pcs.BackColor = System.Drawing.Color.White
        Me.TxtB_Pcs.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.nBONUS_ON_PCS", True))
        Me.TxtB_Pcs.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtB_Pcs.Location = New System.Drawing.Point(422, 43)
        Me.TxtB_Pcs.Name = "TxtB_Pcs"
        Me.TxtB_Pcs.Size = New System.Drawing.Size(117, 23)
        Me.TxtB_Pcs.TabIndex = 26
        Me.TxtB_Pcs.Text = "0"
        Me.TxtB_Pcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 246)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(152, 23)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "Pieces Per Pack*"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPPP
        '
        Me.TxtPPP.BackColor = System.Drawing.Color.White
        Me.TxtPPP.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.nPPP", True))
        Me.TxtPPP.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPPP.Location = New System.Drawing.Point(166, 246)
        Me.TxtPPP.Name = "TxtPPP"
        Me.TxtPPP.Size = New System.Drawing.Size(129, 23)
        Me.TxtPPP.TabIndex = 22
        Me.TxtPPP.Text = "1"
        Me.TxtPPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmbCompany
        '
        Me.CmbCompany.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbCompany.ColumnNum = 2
        Me.CmbCompany.ColumnWidth = "140;40"
        Me.CmbCompany.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_LUP_ITEM1, "V_LUP_ITEM.VENDOR", True))
        Me.CmbCompany.DisplayMember = "Text"
        Me.CmbCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbCompany.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbCompany.DropDownForeColor = System.Drawing.Color.White
        Me.CmbCompany.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbCompany.DropDownWidth = 200
        Me.CmbCompany.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbCompany.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbCompany.GridLineHorizontal = False
        Me.CmbCompany.GridLineVertical = True
        Me.CmbCompany.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbCompany.Location = New System.Drawing.Point(96, 43)
        Me.CmbCompany.ManagingFastMouseMoving = True
        Me.CmbCompany.ManagingFastMouseMovingInterval = 30
        Me.CmbCompany.Name = "CmbCompany"
        Me.CmbCompany.Size = New System.Drawing.Size(199, 24)
        Me.CmbCompany.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(703, 35)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "ITEM(s) DETAIL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     nCODE, sDESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         LUP_VENDOR"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "INSERT INTO [LUP_VENDOR] ([sDESC]) VALUES (@sDESC);" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP" & _
            "_VENDOR WHERE (nCODE = SCOPE_IDENTITY())"
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC")})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "UPDATE [LUP_VENDOR] SET [sDESC] = @sDESC WHERE (([nCODE] = @Original_nCODE) AND (" & _
            "[sDESC] = @Original_sDESC));" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP_VENDOR WHERE (nCODE =" & _
            " @nCODE)"
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "DELETE FROM [LUP_VENDOR] WHERE (([nCODE] = @Original_nCODE) AND ([sDESC] = @Origi" & _
            "nal_sDESC))"
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daLUP_VENDOR
        '
        Me.daLUP_VENDOR.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_VENDOR.InsertCommand = Me.SqlInsertCommand1
        Me.daLUP_VENDOR.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_VENDOR.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_VENDOR", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_VENDOR.UpdateCommand = Me.SqlUpdateCommand1
        '
        'DsLUP_VENDOR1
        '
        Me.DsLUP_VENDOR1.DataSetName = "dsLUP_VENDOR"
        Me.DsLUP_VENDOR1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = resources.GetString("SqlSelectCommand2.CommandText")
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        '
        'daLUP_ITEM
        '
        Me.daLUP_ITEM.SelectCommand = Me.SqlSelectCommand2
        Me.daLUP_ITEM.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_ITEM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sITEM_NAME", "sITEM_NAME"), New System.Data.Common.DataColumnMapping("sNICK", "sNICK"), New System.Data.Common.DataColumnMapping("nPPP", "nPPP"), New System.Data.Common.DataColumnMapping("sPACK_DESC", "sPACK_DESC"), New System.Data.Common.DataColumnMapping("sPIECE_DESC", "sPIECE_DESC"), New System.Data.Common.DataColumnMapping("UNIT_COST", "UNIT_COST"), New System.Data.Common.DataColumnMapping("UNIT_RATE", "UNIT_RATE"), New System.Data.Common.DataColumnMapping("UNIT_RETAIL", "UNIT_RETAIL"), New System.Data.Common.DataColumnMapping("nMIN_STOCK", "nMIN_STOCK"), New System.Data.Common.DataColumnMapping("nMAX_STOCK", "nMAX_STOCK"), New System.Data.Common.DataColumnMapping("nSALE_TAX", "nSALE_TAX"), New System.Data.Common.DataColumnMapping("VENDOR", "VENDOR"), New System.Data.Common.DataColumnMapping("nBONUS_QTY", "nBONUS_QTY"), New System.Data.Common.DataColumnMapping("nBONUS_ON_PCS", "nBONUS_ON_PCS"), New System.Data.Common.DataColumnMapping("CLAIMABLE", "CLAIMABLE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("nOPEN_STOCK", "nOPEN_STOCK"), New System.Data.Common.DataColumnMapping("OPEN_UNIT_VALUE", "OPEN_UNIT_VALUE"), New System.Data.Common.DataColumnMapping("ITEM_CAT", "ITEM_CAT")})})
        '
        'daLUP_ITEM_CAT
        '
        Me.daLUP_ITEM_CAT.DeleteCommand = Me.SqlCommand1
        Me.daLUP_ITEM_CAT.InsertCommand = Me.SqlCommand2
        Me.daLUP_ITEM_CAT.SelectCommand = Me.SqlCommand3
        Me.daLUP_ITEM_CAT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_ITEM_CAT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_ITEM_CAT.UpdateCommand = Me.SqlCommand4
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "DELETE FROM [LUP_ITEM_CAT] WHERE (([nCODE] = @Original_nCODE) AND ([sDESC] = @Ori" & _
            "ginal_sDESC))"
        Me.SqlCommand1.Connection = Me.SqlConnection1
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand2
        '
        Me.SqlCommand2.CommandText = "INSERT INTO [LUP_ITEM_CAT] ([sDESC]) VALUES (@sDESC);" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM L" & _
            "UP_ITEM_CAT WHERE (nCODE = SCOPE_IDENTITY())"
        Me.SqlCommand2.Connection = Me.SqlConnection1
        Me.SqlCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC")})
        '
        'SqlCommand3
        '
        Me.SqlCommand3.CommandText = "SELECT     nCODE, sDESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         LUP_ITEM_CAT"
        Me.SqlCommand3.Connection = Me.SqlConnection1
        '
        'SqlCommand4
        '
        Me.SqlCommand4.CommandText = "UPDATE [LUP_ITEM_CAT] SET [sDESC] = @sDESC WHERE (([nCODE] = @Original_nCODE) AND" & _
            " ([sDESC] = @Original_sDESC));" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP_ITEM_CAT WHERE (nCO" & _
            "DE = @nCODE)"
        Me.SqlCommand4.Connection = Me.SqlConnection1
        Me.SqlCommand4.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsLUP_ITEM_CAT1
        '
        Me.DsLUP_ITEM_CAT1.DataSetName = "dsLUP_ITEM_CAT"
        Me.DsLUP_ITEM_CAT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daITEM_LIST
        '
        Me.daITEM_LIST.SelectCommand = Me.SqlCommand5
        Me.daITEM_LIST.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_ITEM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("sITEM_NAME", "sITEM_NAME"), New System.Data.Common.DataColumnMapping("VENDOR", "VENDOR"), New System.Data.Common.DataColumnMapping("ITEM_CAT", "ITEM_CAT"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS")})})
        '
        'SqlCommand5
        '
        Me.SqlCommand5.CommandText = resources.GetString("SqlCommand5.CommandText")
        Me.SqlCommand5.Connection = Me.SqlConnection1
        '
        'DsITEM_LIST1
        '
        Me.DsITEM_LIST1.DataSetName = "dsITEM_LIST"
        Me.DsITEM_LIST1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_ITEM_RTL
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(729, 593)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_ITEM_RTL"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ITEM(s) DETAIL"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsV_LUP_ITEM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_VENDOR1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_ITEM_CAT1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsITEM_LIST1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_ITEM_RTL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Me.FillComboBox_Company()
        Me.FillComboBox_ItemCat()

        Me.BttnNew_Click(sender, e)

        Me.DmnClaim.SelectedIndex = 1
        Me.DmnStatus.SelectedIndex = 1
    End Sub

    Private Sub frmLUP_ITEM_RTL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtName.GotFocus, TxtItemID.GotFocus, TxtNick.GotFocus, TxtCost.GotFocus, TxtRate.GotFocus, TxtPcsDesc.GotFocus, TxtPackDesc.GotFocus, TxtRetail.GotFocus, TxtPPP.GotFocus, TxtB_Pcs.GotFocus, TxtB_Qty.GotFocus, TxtMinStock.GotFocus, TxtSaleTax.GotFocus, TxtMaxStock.GotFocus, TxtOpenStock.GotFocus, TxtOpenStockValue.GotFocus, TxtSearchCompany.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtName.LostFocus, TxtItemID.LostFocus, TxtNick.LostFocus, TxtCost.LostFocus, TxtRate.LostFocus, TxtPcsDesc.LostFocus, TxtPackDesc.LostFocus, TxtRetail.LostFocus, TxtPPP.LostFocus, TxtB_Qty.LostFocus, TxtB_Pcs.LostFocus, TxtMinStock.LostFocus, TxtSaleTax.LostFocus, TxtMaxStock.LostFocus, TxtOpenStock.LostFocus, TxtOpenStockValue.LostFocus, TxtSearchCompany.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        'If sender.Name = "TxtName" Then
        '    If Me.TxtNick.Text = Nothing Then
        '        Me.TxtNick.Text = Me.TxtName.Text
        '    End If
        'End If
    End Sub

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtOpenStock.KeyPress, TxtMaxStock.KeyPress, TxtMinStock.KeyPress, TxtB_Qty.KeyPress, TxtB_Pcs.KeyPress, TxtPPP.KeyPress, TxtPurcAge.KeyPress, TxtRetAge.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtOpenStockValue.KeyPress, TxtCost.KeyPress, TxtRate.KeyPress, TxtRetail.KeyPress, TxtSaleTax.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    'Open Stock Value Calculation
    Private Sub TxtCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCost.TextChanged, TxtOpenStock.TextChanged
        Me.TxtOpenStockValue.Text = Val(Me.TxtOpenStock.Text) * Val(Me.TxtCost.Text)
    End Sub

    Private Sub TxtPurcAge_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPurcAge.TextChanged
        Dim P_Age As Double
        P_Age = (Val(Me.TxtRate.Text) * Val(Me.TxtPurcAge.Text)) / 100
        Me.TxtCost.Text = Val(Me.TxtRate.Text) - P_Age
    End Sub
    Private Sub TxtRetAge_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtRetAge.TextChanged
        Dim Ret_Age As Double
        Ret_Age = (Val(Me.TxtRate.Text) * Val(Me.TxtRetAge.Text)) / 100
        Me.TxtRetail.Text = Val(Me.TxtRate.Text) + Ret_Age
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged, TxtSearchCompany.TextChanged
        If Me.TxtSearch.Text = Nothing And Me.TxtSearchCompany.Text = Nothing Then
            Me.FillListView()

        ElseIf Not Me.TxtSearch.Text = Nothing And Me.TxtSearchCompany.Text = Nothing Then
            Str2 = "SELECT V_STOCK_NET_TOTAL.CODE, V_LUP_ITEM.sITEM_NAME, V_LUP_ITEM.VENDOR, V_LUP_ITEM.ITEM_CAT, SUM(V_STOCK_NET_TOTAL.NET_TOTAL) AS NET_TOTAL, CASE V_LUP_ITEM.sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS FROM V_LUP_ITEM INNER JOIN V_STOCK_NET_TOTAL ON V_LUP_ITEM.nCODE = V_STOCK_NET_TOTAL.CODE GROUP BY V_STOCK_NET_TOTAL.CODE, V_LUP_ITEM.sITEM_NAME, V_LUP_ITEM.VENDOR, V_LUP_ITEM.ITEM_CAT, V_LUP_ITEM.sSTATUS HAVING (V_LUP_ITEM.sITEM_NAME LIKE '%" & Me.TxtSearch.Text & "%') ORDER BY V_LUP_ITEM.sITEM_NAME"
            Me.FillListView_Condition()

        ElseIf Me.TxtSearch.Text = Nothing And Not Me.TxtSearchCompany.Text = Nothing Then
            Str2 = "SELECT V_STOCK_NET_TOTAL.CODE, V_LUP_ITEM.sITEM_NAME, V_LUP_ITEM.VENDOR, V_LUP_ITEM.ITEM_CAT, SUM(V_STOCK_NET_TOTAL.NET_TOTAL) AS NET_TOTAL, CASE V_LUP_ITEM.sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS FROM V_LUP_ITEM INNER JOIN V_STOCK_NET_TOTAL ON V_LUP_ITEM.nCODE = V_STOCK_NET_TOTAL.CODE GROUP BY V_STOCK_NET_TOTAL.CODE, V_LUP_ITEM.sITEM_NAME, V_LUP_ITEM.VENDOR, V_LUP_ITEM.ITEM_CAT, V_LUP_ITEM.sSTATUS HAVING (V_LUP_ITEM.VENDOR LIKE '%" & Me.TxtSearchCompany.Text & "%') ORDER BY V_LUP_ITEM.sITEM_NAME"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtSearch.Text = Nothing And Not Me.TxtSearchCompany.Text = Nothing Then
            Str2 = "SELECT V_STOCK_NET_TOTAL.CODE, V_LUP_ITEM.sITEM_NAME, V_LUP_ITEM.VENDOR, V_LUP_ITEM.ITEM_CAT, SUM(V_STOCK_NET_TOTAL.NET_TOTAL) AS NET_TOTAL, CASE V_LUP_ITEM.sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS FROM V_LUP_ITEM INNER JOIN V_STOCK_NET_TOTAL ON V_LUP_ITEM.nCODE = V_STOCK_NET_TOTAL.CODE GROUP BY V_STOCK_NET_TOTAL.CODE, V_LUP_ITEM.sITEM_NAME, V_LUP_ITEM.VENDOR, V_LUP_ITEM.ITEM_CAT, V_LUP_ITEM.sSTATUS HAVING (V_LUP_ITEM.sITEM_NAME LIKE '%" & Me.TxtSearch.Text & "%' AND V_LUP_ITEM.VENDOR LIKE '%" & Me.TxtSearchCompany.Text & "%') ORDER BY V_LUP_ITEM.sITEM_NAME"
            Me.FillListView_Condition()

        End If
    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompany.GotFocus, CmbItemCategory.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompany.LostFocus, CmbItemCategory.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "DOMAIN_UPDOWN EVENTS"
    'Got and LostFocus
    Private Sub DmnStatus_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnClaim.GotFocus, DmnStatus.GotFocus
        CType(sender, DomainUpDown).BackColor = Color.LightSteelBlue
    End Sub
    Private Sub DmnStatus_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnClaim.LostFocus, DmnStatus.LostFocus
        CType(sender, DomainUpDown).BackColor = Color.White
    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        Me.TxtItemID.Text = Me.ListView1.SelectedItems(0).Text
        If Not Me.TxtItemID.Text = Nothing Then
            Dim Str1 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE, ITEM_CAT FROM V_LUP_ITEM WHERE nCODE=" & Val(Me.TxtItemID.Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_LUP_ITEM1.Clear()
            Me.daLUP_ITEM.Fill(Me.DsV_LUP_ITEM1.V_LUP_ITEM)

            Dim StrCmb As String = Nothing
            StrCmb = Me.CmbCompany.Text
            Me.CmbCompany.SelectedIndex = -1
            Me.CmbCompany.SelectedIndex = Me.CmbCompany.FindString(StrCmb)

            StrCmb = Me.CmbItemCategory.Text
            Me.CmbItemCategory.SelectedIndex = -1
            Me.CmbItemCategory.SelectedIndex = Me.CmbItemCategory.FindString(StrCmb)

            'Me.BttnAuto.Enabled = False
            Me.DmnClaim.SelectedItem = Me.DmnClaim.Text
            Me.DmnStatus.SelectedItem = Me.DmnStatus.Text
        End If

FIX:
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM LUP_ITEMS WHERE nCODE=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

                'Me.FillListView()
                TxtSearch_TextChanged(sender, e)

                Me.BttnNew_Click(sender, New System.EventArgs)
            End If

        Else
            MsgBox("Please Select record to DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            Me.TxtName.Focus()
        End If

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAuto.Click
        Me.TxtItemID.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nCODE) FROM LUP_ITEMS") + 1
        Me.CmbCompany.Focus()
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.DsV_LUP_ITEM1.Clear()

        Me.TxtSearch.Text = Nothing

        Me.TxtPPP.Text = "1"
        Me.TxtOpenStock.Text = "0"
        Me.TxtNick.Text = "*"

        'Me.TxtItemID.Focus()

        Me.BttnAuto_Click(sender, e)
        'Me.BttnAuto.Enabled = True
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM LUP_ITEMS WHERE nCODE=" & Val(Me.TxtItemID.Text) & "")

        If Val(Me.TxtItemID.Text) <= 0 Then
            MsgBox("ITEM CODE can't be 'NULL', Click on 'AUTO CODE' for New ID", MsgBoxStyle.Exclamation, "(NS) - Wrong ID")
            Me.BttnNew.Focus()

        ElseIf Me.CmbCompany.Text = Nothing Or Me.CmbCompany.SelectedIndex = -1 Or Me.TxtName.Text = Nothing Or Me.TxtNick.Text = Nothing Or Val(Me.TxtCost.Text) <= 0 Or Val(Me.TxtRate.Text) <= 0 Or Val(Me.TxtRetail.Text) < 0 Or Me.TxtPackDesc.Text = Nothing Or Me.TxtPcsDesc.Text = Nothing Or Val(Me.TxtPPP.Text) <= 0 Or Val(Me.TxtB_Qty.Text) < 0 Or Val(Me.TxtB_Pcs.Text) < 0 Or Val(Me.TxtMinStock.Text) < 0 Or Val(Me.TxtMaxStock.Text) < 0 Or Val(Me.TxtSaleTax.Text) < 0 Or Me.DmnClaim.SelectedIndex = -1 Or Me.DmnStatus.SelectedIndex = -1 Or Me.CmbItemCategory.SelectedIndex = -1 Or Me.CmbItemCategory.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

            If Me.CmbCompany.Text = Nothing Or Me.CmbCompany.SelectedIndex = -1 Then
                Me.CmbCompany.Focus()

            ElseIf Me.TxtName.Text = Nothing Then
                Me.TxtName.Focus()

            ElseIf Me.TxtNick.Text = Nothing Then
                Me.TxtNick.Focus()

            ElseIf Val(Me.TxtCost.Text) <= 0 Then
                Me.TxtCost.Focus()

            ElseIf Val(Me.TxtRate.Text) <= 0 Then
                Me.TxtRate.Focus()

            ElseIf Val(Me.TxtRetail.Text) <= 0 Then
                Me.TxtRetail.Focus()

            ElseIf Me.TxtPackDesc.Text = Nothing Then
                Me.TxtPackDesc.Focus()

            ElseIf Me.TxtPcsDesc.Text = Nothing Then
                Me.TxtPcsDesc.Focus()

            ElseIf Val(Me.TxtPPP.Text) <= 0 Then
                Me.TxtPPP.Focus()

            ElseIf Val(Me.TxtB_Qty.Text) < 0 Then
                Me.TxtB_Qty.Focus()

            ElseIf Val(Me.TxtB_Pcs.Text) < 0 Then
                Me.TxtB_Pcs.Focus()

            ElseIf Val(Me.TxtMinStock.Text) < 0 Then
                Me.TxtMinStock.Focus()

            ElseIf Val(Me.TxtMaxStock.Text) < 0 Then
                Me.TxtMaxStock.Focus()

            ElseIf Val(Me.TxtSaleTax.Text) < 0 Then
                Me.TxtSaleTax.Focus()

            ElseIf Me.DmnClaim.SelectedIndex = -1 Then
                Me.DmnClaim.Focus()

            ElseIf Me.DmnStatus.SelectedIndex = -1 Then
                Me.DmnStatus.Focus()

            ElseIf Me.CmbItemCategory.SelectedIndex = -1 Or Me.CmbItemCategory.Text = Nothing Then
                Me.CmbItemCategory.Focus()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            If MsgBox("Do you want to save '" & Me.TxtName.Text & "' & '" & Me.TxtNick.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                ''INSERT VALUES
                ''For Whole Saller
                'Me.asInsert.SaveValueIN("INSERT INTO LUP_ITEMS(nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, nUNIT_COST, nUNIT_RATE, nUNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, nITEM_CAT, nVENDOR_CODE, nBONUS_QTY, nBONUS_ON_PCS, sCLAIMABLE, sSTATUS, nOPEN_STOCK, nOPEN_UNIT_VALUE) VALUES(" & Val(Me.TxtItemID.Text) & ",'" & Me.TxtName.Text & "','" & Me.TxtNick.Text & "',1,'" & Me.TxtPackDesc.Text & "'," & Val(Me.TxtCost.Text) & "," & Val(Me.TxtRate.Text) & "," & Val(Me.TxtRetail.Text) & "," & Val(Me.TxtMinStock.Text) & "," & Val(Me.TxtMaxStock.Text) & "," & Val(Me.TxtSaleTax.Text) & "," & Val(Me.CmbItemCategory.SelectedItem.Col2) & "," & Val(Me.CmbCompany.SelectedItem.col2) & "," & Val(Me.TxtB_Qty.Text) & "," & Val(Me.TxtB_Pcs.Text) & ",'" & Me.DmnClaim.SelectedIndex & "','" & Me.DmnStatus.SelectedIndex & "'," & Val(Me.TxtOpenStock.Text) & "," & Val(Me.TxtOpenStockValue.Text) & ") ")

                'INSERT VALUES
                'for retailer
                Me.asInsert.SaveValueIN("INSERT INTO LUP_ITEMS(nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, nUNIT_COST, nUNIT_RATE, nUNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, nITEM_CAT, nVENDOR_CODE, nBONUS_QTY, nBONUS_ON_PCS, sCLAIMABLE, sSTATUS, nOPEN_STOCK, nOPEN_UNIT_VALUE) VALUES(" & Val(Me.TxtItemID.Text) & ", '" & Me.TxtName.Text & "', '" & Me.TxtNick.Text & "', " & Val(Me.TxtPPP.Text) & ", '" & Me.TxtPackDesc.Text & "', '" & Me.TxtPcsDesc.Text & "', " & Val(Me.TxtCost.Text) & ", " & Val(Me.TxtRate.Text) & ", " & Val(Me.TxtRetail.Text) & ", " & Val(Me.TxtMinStock.Text) & ", " & Val(Me.TxtMaxStock.Text) & ", " & Val(Me.TxtSaleTax.Text) & ", " & Val(Me.CmbItemCategory.SelectedItem.Col2) & ", " & Val(Me.CmbCompany.SelectedItem.col2) & ", " & Val(Me.TxtB_Qty.Text) & ", " & Val(Me.TxtB_Pcs.Text) & ", '" & Me.DmnClaim.SelectedIndex & "', '" & Me.DmnStatus.SelectedIndex & "', " & Val(Me.TxtOpenStock.Text) & ", " & Val(Me.TxtOpenStockValue.Text) & ")")

                'FILL THE RECORD IN LISTVIEW
                'Me.FillListView()
                TxtSearch_TextChanged(sender, e)

                Me.TxtName.Focus()
            End If

        ElseIf Me.asSELECT.pFlg1 = True Then
            If MsgBox("This Item Code '" & Me.TxtName.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                ''UPDATE RECORD
                ''For Whole Saller
                'Me.asUpdate.UpdateValueIN("UPDATE LUP_ITEMS SET sITEM_NAME='" & Me.TxtName.Text & "', sNICK='" & Me.TxtNick.Text & "', nPPP=1, sPACK_DESC='" & Me.TxtPackDesc.Text & "', nUNIT_COST=" & Val(Me.TxtCost.Text) & ", nUNIT_RATE=" & Val(Me.TxtRate.Text) & ", nUNIT_RETAIL=" & Val(Me.TxtRetail.Text) & ", nMIN_STOCK=" & Val(Me.TxtMinStock.Text) & ", nMAX_STOCK=" & Val(Me.TxtMaxStock.Text) & ", nSALE_TAX=" & Val(Me.TxtSaleTax.Text) & ", nITEM_CAT=" & Val(Me.CmbItemCategory.SelectedItem.Col2) & ", nVENDOR_CODE=" & Val(Me.CmbCompany.SelectedItem.col2) & ", nBONUS_QTY=" & Val(Me.TxtB_Qty.Text) & ", nBONUS_ON_PCS=" & Val(Me.TxtB_Pcs.Text) & ", sCLAIMABLE='" & Me.DmnClaim.SelectedIndex & "', sSTATUS='" & Me.DmnStatus.SelectedIndex & "', nOPEN_STOCK=" & Val(Me.TxtOpenStock.Text) & ", nOPEN_UNIT_VALUE=" & Val(Me.TxtOpenStockValue.Text) & " WHERE nCODE=" & Val(Me.TxtItemID.Text) & "")

                'Update RECORD
                'for retailer
                Me.asUpdate.UpdateValueIN("UPDATE LUP_ITEMS SET sITEM_NAME='" & Me.TxtName.Text & "', sNICK='" & Me.TxtNick.Text & "', nPPP=" & Val(Me.TxtPPP.Text) & ", sPACK_DESC='" & Me.TxtPackDesc.Text & "', sPIECE_DESC='" & Me.TxtPcsDesc.Text & "', nUNIT_COST=" & Val(Me.TxtCost.Text) & ", nUNIT_RATE=" & Val(Me.TxtRate.Text) & ", nUNIT_RETAIL=" & Val(Me.TxtRetail.Text) & ", nMIN_STOCK=" & Val(Me.TxtMinStock.Text) & ", nMAX_STOCK=" & Val(Me.TxtMaxStock.Text) & ", nSALE_TAX=" & Val(Me.TxtSaleTax.Text) & ", nITEM_CAT=" & Val(Me.CmbItemCategory.SelectedItem.Col2) & ", nVENDOR_CODE=" & Val(Me.CmbCompany.SelectedItem.col2) & ", nBONUS_QTY=" & Val(Me.TxtB_Qty.Text) & ", nBONUS_ON_PCS=" & Val(Me.TxtB_Pcs.Text) & ", sCLAIMABLE='" & Me.DmnClaim.SelectedIndex & "', sSTATUS='" & Me.DmnStatus.SelectedIndex & "', nOPEN_STOCK=" & Val(Me.TxtOpenStock.Text) & ", nOPEN_UNIT_VALUE=" & Val(Me.TxtOpenStockValue.Text) & " WHERE nCODE=" & Val(Me.TxtItemID.Text) & "")

                'FILL THE RECORD IN LISTVIEW
                'Me.FillListView()
                TxtSearch_TextChanged(sender, e)

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
        frmLUP_ITEM_CAT.MdiParent = Me.ParentForm
        frmLUP_ITEM_CAT.Show()
        frmLUP_ITEM_CAT.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_ITEM_OPEN_STK.MdiParent = Me.ParentForm
        frmLUP_ITEM_OPEN_STK.Show()
        frmLUP_ITEM_OPEN_STK.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillComboBox_Company()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_VENDOR ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_VENDOR = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_VENDOR1.Clear()
        Me.daLUP_VENDOR.Fill(Me.DsLUP_VENDOR1.LUP_VENDOR)

        Dim dtLoading As New DataTable("LUP_VENDOR")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_VENDOR1.LUP_VENDOR.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_VENDOR1.LUP_VENDOR.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_VENDOR1.LUP_VENDOR.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbCompany.SelectedIndex = -1
        Me.CmbCompany.Items.Clear()
        Me.CmbCompany.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbCompany.SourceDataString = New String(1) {"sDESC", "nCODE"}
        Me.CmbCompany.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_ItemCat()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_ITEM_CAT ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_ITEM_CAT = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_ITEM_CAT1.Clear()
        Me.daLUP_ITEM_CAT.Fill(Me.DsLUP_ITEM_CAT1.LUP_ITEM_CAT)

        Dim dtLoading As New DataTable("LUP_ITEM_CAT")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_ITEM_CAT1.LUP_ITEM_CAT.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_ITEM_CAT1.LUP_ITEM_CAT.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_ITEM_CAT1.LUP_ITEM_CAT.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbItemCategory.SelectedIndex = -1
        Me.CmbItemCategory.Items.Clear()
        Me.CmbItemCategory.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbItemCategory.SourceDataString = New String(1) {"sDESC", "nCODE"}
        Me.CmbItemCategory.SourceDataTable = dtLoading
    End Sub

    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT V_STOCK_NET_TOTAL.CODE, V_LUP_ITEM.sITEM_NAME, V_LUP_ITEM.VENDOR, V_LUP_ITEM.ITEM_CAT, SUM(V_STOCK_NET_TOTAL.NET_TOTAL) AS NET_TOTAL, CASE V_LUP_ITEM.sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS FROM V_LUP_ITEM INNER JOIN V_STOCK_NET_TOTAL ON V_LUP_ITEM.nCODE = V_STOCK_NET_TOTAL.CODE GROUP BY V_STOCK_NET_TOTAL.CODE, V_LUP_ITEM.sITEM_NAME, V_LUP_ITEM.VENDOR, V_LUP_ITEM.ITEM_CAT, V_LUP_ITEM.sSTATUS ORDER BY V_LUP_ITEM.sITEM_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daITEM_LIST = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsITEM_LIST1.Clear()
            Me.daITEM_LIST.Fill(Me.DsITEM_LIST1.V_LUP_ITEM)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsITEM_LIST1.V_LUP_ITEM.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(4).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(5).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Dim Str2 As String = Nothing

    Private Sub FillListView_Condition()
        Try
            Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
            Me.daITEM_LIST = New SDS.SqlDataAdapter(SqlCmd2)

            Me.DsITEM_LIST1.Clear()
            Me.daITEM_LIST.Fill(Me.DsITEM_LIST1.V_LUP_ITEM)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsITEM_LIST1.V_LUP_ITEM.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(4).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_LIST1.V_LUP_ITEM.Item(Cnt).Item(5).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

End Class
