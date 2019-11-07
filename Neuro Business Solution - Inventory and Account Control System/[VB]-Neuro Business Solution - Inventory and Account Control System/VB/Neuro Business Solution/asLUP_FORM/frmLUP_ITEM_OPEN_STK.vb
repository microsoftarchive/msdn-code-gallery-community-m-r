Imports SDS = System.Data.SqlClient
Public Class frmLUP_ITEM_OPEN_STK
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
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TxtCost As System.Windows.Forms.TextBox
    Friend WithEvents TxtRate As System.Windows.Forms.TextBox
    Friend WithEvents TxtPcsDesc As System.Windows.Forms.TextBox
    Friend WithEvents TxtPackDesc As System.Windows.Forms.TextBox
    Friend WithEvents TxtRetail As System.Windows.Forms.TextBox
    Friend WithEvents TxtPPP As System.Windows.Forms.TextBox
    Friend WithEvents CmbItem As MTGCComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtOpenStockValue As System.Windows.Forms.TextBox
    Friend WithEvents TxtOpenStock As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_ITEM As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents TxtBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_ITEM_OPEN_STK As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsV_ITEM_OPEN_STK1 As Neruo_Business_Solution.dsV_ITEM_OPEN_STK
    Friend WithEvents DsV_ITEM_OPEN_STK11 As Neruo_Business_Solution.dsV_ITEM_OPEN_STK1
    Friend WithEvents DsLUP_ITEM11 As Neruo_Business_Solution.dsLUP_ITEM1
    Friend WithEvents TxtExpDate As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents DsLUP_ITEM1 As Neruo_Business_Solution.dsLUP_ITEM
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_ITEM_OPEN_STK))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtCost = New System.Windows.Forms.TextBox
        Me.DsV_ITEM_OPEN_STK1 = New Neruo_Business_Solution.dsV_ITEM_OPEN_STK
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtRate = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtOpenStockValue = New System.Windows.Forms.TextBox
        Me.TxtOpenStock = New System.Windows.Forms.TextBox
        Me.TxtPcsDesc = New System.Windows.Forms.TextBox
        Me.DsLUP_ITEM11 = New Neruo_Business_Solution.dsLUP_ITEM1
        Me.TxtBatchNo = New System.Windows.Forms.TextBox
        Me.TxtExpDate = New System.Windows.Forms.TextBox
        Me.TxtPackDesc = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtRetail = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtPPP = New System.Windows.Forms.TextBox
        Me.CmbItem = New MTGCComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_ITEM = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daV_ITEM_OPEN_STK = New System.Data.SqlClient.SqlDataAdapter
        Me.DsLUP_ITEM1 = New Neruo_Business_Solution.dsLUP_ITEM
        Me.DsV_ITEM_OPEN_STK11 = New Neruo_Business_Solution.dsV_ITEM_OPEN_STK1
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsV_ITEM_OPEN_STK1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_ITEM11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_ITEM_OPEN_STK11, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Size = New System.Drawing.Size(694, 490)
        Me.Panel1.TabIndex = 0
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(11, 3)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(142, 29)
        Me.BttnPrevForm.TabIndex = 35
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Product Detail"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(540, 3)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(142, 29)
        Me.BttnNextForm.TabIndex = 36
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Open Stock Adj."
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 215)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(671, 267)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader6, Me.ColumnHeader3, Me.ColumnHeader5})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 53)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(665, 211)
        Me.ListView1.TabIndex = 2
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Item Code"
        Me.ColumnHeader1.Width = 96
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Item Name"
        Me.ColumnHeader2.Width = 230
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Batch #"
        Me.ColumnHeader4.Width = 118
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Expiry"
        Me.ColumnHeader6.Width = 120
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Open Stock"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "ID"
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
        Me.GroupBox1.Size = New System.Drawing.Size(120, 175)
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
        Me.BttnClose.Location = New System.Drawing.Point(16, 113)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(16, 66)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtCost)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtRate)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TxtOpenStockValue)
        Me.GroupBox3.Controls.Add(Me.TxtOpenStock)
        Me.GroupBox3.Controls.Add(Me.TxtPcsDesc)
        Me.GroupBox3.Controls.Add(Me.TxtBatchNo)
        Me.GroupBox3.Controls.Add(Me.TxtExpDate)
        Me.GroupBox3.Controls.Add(Me.TxtPackDesc)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.TxtRetail)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.TxtPPP)
        Me.GroupBox3.Controls.Add(Me.CmbItem)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(11, 32)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(545, 177)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Item Code*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCost
        '
        Me.TxtCost.BackColor = System.Drawing.Color.White
        Me.TxtCost.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_ITEM_OPEN_STK1, "V_ITEM_OPEN_STK.COST", True))
        Me.TxtCost.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCost.Location = New System.Drawing.Point(422, 19)
        Me.TxtCost.Name = "TxtCost"
        Me.TxtCost.Size = New System.Drawing.Size(117, 23)
        Me.TxtCost.TabIndex = 13
        Me.TxtCost.Text = "0.00"
        Me.TxtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_ITEM_OPEN_STK1
        '
        Me.DsV_ITEM_OPEN_STK1.DataSetName = "dsV_ITEM_OPEN_STK"
        Me.DsV_ITEM_OPEN_STK1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(301, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(115, 23)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Purchase Price"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtRate
        '
        Me.TxtRate.BackColor = System.Drawing.Color.White
        Me.TxtRate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_ITEM_OPEN_STK1, "V_ITEM_OPEN_STK.RATE", True))
        Me.TxtRate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(422, 45)
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(117, 23)
        Me.TxtRate.TabIndex = 15
        Me.TxtRate.Text = "0.00"
        Me.TxtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(301, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 23)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "Rate Price"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label17.Location = New System.Drawing.Point(301, 121)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(115, 23)
        Me.Label17.TabIndex = 20
        Me.Label17.Text = "Open Stk Value"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(301, 96)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(115, 23)
        Me.Label16.TabIndex = 18
        Me.Label16.Text = "Open Stock Pcs"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 120)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 24)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Piece Desc"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtOpenStockValue
        '
        Me.TxtOpenStockValue.BackColor = System.Drawing.Color.White
        Me.TxtOpenStockValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_ITEM_OPEN_STK1, "V_ITEM_OPEN_STK.OPEN_STK_VALUE", True))
        Me.TxtOpenStockValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOpenStockValue.ForeColor = System.Drawing.Color.DarkBlue
        Me.TxtOpenStockValue.Location = New System.Drawing.Point(422, 121)
        Me.TxtOpenStockValue.Name = "TxtOpenStockValue"
        Me.TxtOpenStockValue.ReadOnly = True
        Me.TxtOpenStockValue.Size = New System.Drawing.Size(117, 23)
        Me.TxtOpenStockValue.TabIndex = 21
        Me.TxtOpenStockValue.TabStop = False
        Me.TxtOpenStockValue.Text = "0.00"
        Me.TxtOpenStockValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtOpenStock
        '
        Me.TxtOpenStock.BackColor = System.Drawing.Color.White
        Me.TxtOpenStock.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_ITEM_OPEN_STK1, "V_ITEM_OPEN_STK.OPEN_BAL", True))
        Me.TxtOpenStock.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TxtOpenStock.Location = New System.Drawing.Point(422, 96)
        Me.TxtOpenStock.Name = "TxtOpenStock"
        Me.TxtOpenStock.Size = New System.Drawing.Size(117, 22)
        Me.TxtOpenStock.TabIndex = 19
        Me.TxtOpenStock.Text = "0"
        Me.TxtOpenStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtPcsDesc
        '
        Me.TxtPcsDesc.BackColor = System.Drawing.Color.White
        Me.TxtPcsDesc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM11, "V_LUP_ITEM.sPIECE_DESC", True))
        Me.TxtPcsDesc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPcsDesc.Location = New System.Drawing.Point(175, 121)
        Me.TxtPcsDesc.Name = "TxtPcsDesc"
        Me.TxtPcsDesc.ReadOnly = True
        Me.TxtPcsDesc.Size = New System.Drawing.Size(120, 23)
        Me.TxtPcsDesc.TabIndex = 9
        Me.TxtPcsDesc.TabStop = False
        '
        'DsLUP_ITEM11
        '
        Me.DsLUP_ITEM11.DataSetName = "dsLUP_ITEM1"
        Me.DsLUP_ITEM11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtBatchNo
        '
        Me.TxtBatchNo.BackColor = System.Drawing.Color.White
        Me.TxtBatchNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_ITEM_OPEN_STK1, "V_ITEM_OPEN_STK.BATCH", True))
        Me.TxtBatchNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBatchNo.Location = New System.Drawing.Point(113, 45)
        Me.TxtBatchNo.Name = "TxtBatchNo"
        Me.TxtBatchNo.Size = New System.Drawing.Size(182, 23)
        Me.TxtBatchNo.TabIndex = 3
        '
        'TxtExpDate
        '
        Me.TxtExpDate.BackColor = System.Drawing.Color.White
        Me.TxtExpDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_ITEM_OPEN_STK1, "V_ITEM_OPEN_STK.dEXP_DATE", True))
        Me.TxtExpDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExpDate.Location = New System.Drawing.Point(175, 71)
        Me.TxtExpDate.Name = "TxtExpDate"
        Me.TxtExpDate.Size = New System.Drawing.Size(120, 23)
        Me.TxtExpDate.TabIndex = 5
        '
        'TxtPackDesc
        '
        Me.TxtPackDesc.BackColor = System.Drawing.Color.White
        Me.TxtPackDesc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM11, "V_LUP_ITEM.sPACK_DESC", True))
        Me.TxtPackDesc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPackDesc.Location = New System.Drawing.Point(175, 96)
        Me.TxtPackDesc.Name = "TxtPackDesc"
        Me.TxtPackDesc.ReadOnly = True
        Me.TxtPackDesc.Size = New System.Drawing.Size(120, 23)
        Me.TxtPackDesc.TabIndex = 7
        Me.TxtPackDesc.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Expiry Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Batch #"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 96)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 23)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Pack Desc"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(301, 71)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(115, 23)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Retail Price"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtRetail
        '
        Me.TxtRetail.BackColor = System.Drawing.Color.White
        Me.TxtRetail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_ITEM_OPEN_STK1, "V_ITEM_OPEN_STK.RETAIL", True))
        Me.TxtRetail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRetail.Location = New System.Drawing.Point(422, 71)
        Me.TxtRetail.Name = "TxtRetail"
        Me.TxtRetail.Size = New System.Drawing.Size(117, 23)
        Me.TxtRetail.TabIndex = 17
        Me.TxtRetail.Text = "0.00"
        Me.TxtRetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 146)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 23)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "Pcs Per Pack"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPPP
        '
        Me.TxtPPP.BackColor = System.Drawing.Color.White
        Me.TxtPPP.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_ITEM11, "V_LUP_ITEM.nPPP", True))
        Me.TxtPPP.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPPP.Location = New System.Drawing.Point(175, 146)
        Me.TxtPPP.Name = "TxtPPP"
        Me.TxtPPP.ReadOnly = True
        Me.TxtPPP.Size = New System.Drawing.Size(120, 23)
        Me.TxtPPP.TabIndex = 11
        Me.TxtPPP.TabStop = False
        Me.TxtPPP.Text = "0"
        Me.TxtPPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CmbItem
        '
        Me.CmbItem.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbItem.ColumnNum = 3
        Me.CmbItem.ColumnWidth = "140;140;40"
        Me.CmbItem.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_ITEM_OPEN_STK1, "V_ITEM_OPEN_STK.ITEM_NAME", True))
        Me.CmbItem.DisplayMember = "Text"
        Me.CmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbItem.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbItem.DropDownForeColor = System.Drawing.Color.White
        Me.CmbItem.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbItem.DropDownWidth = 340
        Me.CmbItem.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbItem.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbItem.GridLineHorizontal = False
        Me.CmbItem.GridLineVertical = True
        Me.CmbItem.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbItem.Location = New System.Drawing.Point(113, 18)
        Me.CmbItem.ManagingFastMouseMoving = True
        Me.CmbItem.ManagingFastMouseMovingInterval = 30
        Me.CmbItem.Name = "CmbItem"
        Me.CmbItem.Size = New System.Drawing.Size(182, 24)
        Me.CmbItem.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(692, 35)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Batch Wise Opening Stock"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = resources.GetString("SqlSelectCommand2.CommandText")
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        '
        'daLUP_ITEM
        '
        Me.daLUP_ITEM.SelectCommand = Me.SqlSelectCommand2
        Me.daLUP_ITEM.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_ITEM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sITEM_NAME", "sITEM_NAME"), New System.Data.Common.DataColumnMapping("sNICK", "sNICK"), New System.Data.Common.DataColumnMapping("nPPP", "nPPP"), New System.Data.Common.DataColumnMapping("sPACK_DESC", "sPACK_DESC"), New System.Data.Common.DataColumnMapping("sPIECE_DESC", "sPIECE_DESC"), New System.Data.Common.DataColumnMapping("UNIT_COST", "UNIT_COST"), New System.Data.Common.DataColumnMapping("UNIT_RATE", "UNIT_RATE"), New System.Data.Common.DataColumnMapping("UNIT_RETAIL", "UNIT_RETAIL"), New System.Data.Common.DataColumnMapping("nMIN_STOCK", "nMIN_STOCK"), New System.Data.Common.DataColumnMapping("nMAX_STOCK", "nMAX_STOCK"), New System.Data.Common.DataColumnMapping("nSALE_TAX", "nSALE_TAX"), New System.Data.Common.DataColumnMapping("VENDOR", "VENDOR"), New System.Data.Common.DataColumnMapping("nBONUS_QTY", "nBONUS_QTY"), New System.Data.Common.DataColumnMapping("nBONUS_ON_PCS", "nBONUS_ON_PCS"), New System.Data.Common.DataColumnMapping("CLAIMABLE", "CLAIMABLE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("nOPEN_STOCK", "nOPEN_STOCK"), New System.Data.Common.DataColumnMapping("OPEN_UNIT_VALUE", "OPEN_UNIT_VALUE")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     ID, ITEM_CODE, BATCH, COST, RATE, RETAIL, ITEM_NAME, PPP, PKS_DESC, PC" & _
            "S_DESC, OPEN_BAL, OPEN_STK_VALUE, dEXP_DATE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_ITEM_OPEN_STK"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'daV_ITEM_OPEN_STK
        '
        Me.daV_ITEM_OPEN_STK.SelectCommand = Me.SqlSelectCommand1
        Me.daV_ITEM_OPEN_STK.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_ITEM_OPEN_STK", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("ITEM_CODE", "ITEM_CODE"), New System.Data.Common.DataColumnMapping("BATCH", "BATCH"), New System.Data.Common.DataColumnMapping("COST", "COST"), New System.Data.Common.DataColumnMapping("RATE", "RATE"), New System.Data.Common.DataColumnMapping("RETAIL", "RETAIL"), New System.Data.Common.DataColumnMapping("ITEM_NAME", "ITEM_NAME"), New System.Data.Common.DataColumnMapping("PPP", "PPP"), New System.Data.Common.DataColumnMapping("PKS_DESC", "PKS_DESC"), New System.Data.Common.DataColumnMapping("PCS_DESC", "PCS_DESC"), New System.Data.Common.DataColumnMapping("OPEN_BAL", "OPEN_BAL"), New System.Data.Common.DataColumnMapping("OPEN_STK_VALUE", "OPEN_STK_VALUE"), New System.Data.Common.DataColumnMapping("dEXP_DATE", "dEXP_DATE")})})
        '
        'DsLUP_ITEM1
        '
        Me.DsLUP_ITEM1.DataSetName = "dsLUP_ITEM"
        Me.DsLUP_ITEM1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsV_ITEM_OPEN_STK11
        '
        Me.DsV_ITEM_OPEN_STK11.DataSetName = "dsV_ITEM_OPEN_STK1"
        Me.DsV_ITEM_OPEN_STK11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_ITEM_OPEN_STK
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(718, 506)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_ITEM_OPEN_STK"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item's Opening Stock Batch Wise"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsV_ITEM_OPEN_STK1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_ITEM11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_ITEM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_ITEM_OPEN_STK11, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_ITEM_OPEN_STK_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Me.FillComboBox_Item()

        Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmLUP_ITEM_OPEN_STK_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtCost.GotFocus, TxtRate.GotFocus, TxtPcsDesc.GotFocus, TxtPackDesc.GotFocus, TxtRetail.GotFocus, TxtPPP.GotFocus, TxtOpenStock.GotFocus, TxtOpenStockValue.GotFocus, TxtBatchNo.GotFocus, TxtExpDate.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtCost.LostFocus, TxtRate.LostFocus, TxtPcsDesc.LostFocus, TxtPackDesc.LostFocus, TxtRetail.LostFocus, TxtPPP.LostFocus, TxtOpenStock.LostFocus, TxtOpenStockValue.LostFocus, TxtBatchNo.LostFocus, TxtExpDate.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtExpDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtOpenStock.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtOpenStockValue.KeyPress, TxtCost.KeyPress, TxtRate.KeyPress, TxtRetail.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    'Open Stock Value Calculation
    Private Sub TxtCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCost.TextChanged, TxtOpenStock.TextChanged
        Me.TxtOpenStockValue.Text = Val(Me.TxtOpenStock.Text) * (Val(Me.TxtCost.Text) / Val(Me.TxtPPP.Text))
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
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbItem.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbItem.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
    Private Sub CmbItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbItem.SelectedIndexChanged
        On Error GoTo Fix
        Dim Str1 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM WHERE sITEM_NAME='" & Me.CmbItem.Text & "'"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_ITEM11.Clear()
        Me.daLUP_ITEM.Fill(Me.DsLUP_ITEM11.V_LUP_ITEM)
Fix:
    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        If Not Me.ListView1.SelectedItems(0).SubItems(5).Text = Nothing Then
            Dim Str1 As String = "SELECT ID, ITEM_CODE, BATCH, CONVERT(NUMERIC(18,2),COST) AS COST, CONVERT(NUMERIC(18,2), RATE) AS RATE, CONVERT(NUMERIC(18,2), RETAIL) AS RETAIL, ITEM_NAME, PPP, PKS_DESC, PCS_DESC, CONVERT(NUMERIC(18,2), OPEN_BAL) AS OPEN_BAL, CONVERT(NUMERIC(18,2), OPEN_STK_VALUE) AS OPEN_STK_VALUE, dEXP_DATE FROM V_ITEM_OPEN_STK WHERE ID=" & Val(Me.ListView1.SelectedItems(0).SubItems(5).Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_ITEM_OPEN_STK = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_ITEM_OPEN_STK1.Clear()
            Me.daV_ITEM_OPEN_STK.Fill(Me.DsV_ITEM_OPEN_STK1.V_ITEM_OPEN_STK)

            Dim StrCmb As String = Me.CmbItem.Text
            Me.CmbItem.SelectedIndex = -1
            Me.CmbItem.SelectedIndex = Me.CmbItem.FindString(StrCmb)
        End If

FIX:
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).SubItems(5).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM ITEM_OPEN_STK WHERE ID=" & Val(Me.ListView1.SelectedItems(0).SubItems(4).Text) & "")

                Me.FillListView()

                Me.BttnNew_Click(sender, New System.EventArgs)
            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            Me.CmbItem.Focus()
        End If

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.DsV_ITEM_OPEN_STK1.Clear()

        Me.TxtSearch.Text = Nothing

        Me.CmbItem.Focus()

    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM ITEM_OPEN_STK WHERE nITEM_CODE=" & Val(Me.CmbItem.SelectedItem.Col3) & " AND sBATCH='" & Me.TxtBatchNo.Text & "'")

        If Me.CmbItem.Text = Nothing Or Me.CmbItem.SelectedIndex = -1 Or Me.TxtExpDate.Text = Nothing Or Val(Me.TxtCost.Text) <= 0 Or Val(Me.TxtRate.Text) <= 0 Or Val(Me.TxtRetail.Text) < 0 Or Val(Me.TxtOpenStock.Text) <= 0 Or Val(Me.TxtOpenStockValue.Text) <= 0 Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

            If Me.CmbItem.Text = Nothing Or Me.CmbItem.SelectedIndex = -1 Then
                Me.CmbItem.Focus()

            ElseIf Me.TxtExpDate.Text = Nothing Then
                Me.TxtExpDate.Focus()

            ElseIf Val(Me.TxtCost.Text) <= 0 Then
                Me.TxtCost.Focus()

            ElseIf Val(Me.TxtRate.Text) <= 0 Then
                Me.TxtRate.Focus()

            ElseIf Val(Me.TxtRetail.Text) <= 0 Then
                Me.TxtRetail.Focus()

            ElseIf Val(Me.TxtOpenStock.Text) <= 0 Then
                Me.TxtOpenStock.Focus()

            ElseIf Val(Me.TxtOpenStockValue.Text) <= 0 Then
                Me.TxtOpenStockValue.Focus()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            If MsgBox("Do you want to save '" & Me.CmbItem.Text & "' & '" & Me.TxtBatchNo.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO ITEM_OPEN_STK(nITEM_CODE, sBATCH, dEXP_DATE, nUNIT_COST, nUNIT_RATE, nUNIT_RETAIL, nOPEN_BAL, nOPEN_STK_VALUE) VALUES(" & Val(Me.CmbItem.SelectedItem.Col3) & ",'" & Me.TxtBatchNo.Text & "','" & CDate(Me.TxtExpDate.Text).ToString("MM-dd-yyyy") & "'," & Val(Me.TxtCost.Text) & "," & Val(Me.TxtRate.Text) & "," & Val(Me.TxtRetail.Text) & "," & Val(Me.TxtOpenStock.Text) & "," & Val(Me.TxtOpenStockValue.Text) & ") ")

                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.CmbItem.Focus()
            End If

        ElseIf Me.asSELECT.pFlg1 = True Then
            If MsgBox("This Item '" & Me.CmbItem.Text & "' & BATCH # '" & Me.TxtBatchNo.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE ITEM_OPEN_STK SET dEXP_DATE='" & CDate(Me.TxtExpDate.Text).ToString("MM-dd-yyyy") & "', nUNIT_COST=" & Val(Me.TxtCost.Text) & ", nUNIT_RATE=" & Val(Me.TxtRate.Text) & ", nUNIT_RETAIL=" & Val(Me.TxtRetail.Text) & ", nOPEN_BAL=" & Val(Me.TxtOpenStock.Text) & ", nOPEN_STK_VALUE=" & Val(Me.TxtOpenStockValue.Text) & " WHERE nITEM_CODE=" & Val(Me.CmbItem.SelectedItem.Col3) & " AND sBATCH='" & Me.TxtBatchNo.Text & "'")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.CmbItem.Focus()
            End If

        End If


    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        'For Wholesaler
        frmLUP_ITEM.MdiParent = Me.ParentForm
        frmLUP_ITEM.Show()
        frmLUP_ITEM.Activate()
        frmLUP_ITEM.WindowState = FormWindowState.Normal
        Me.Close()

        ''For Retailer
        'frmLUP_ITEM_RTL.MdiParent = Me.ParentForm
        'frmLUP_ITEM_RTL.Show()
        'frmLUP_ITEM_RTL.Activate()
        'frmLUP_ITEM_RTL.WindowState = FormWindowState.Normal
        'Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_ITEM_OPEN_STK_ADJ.MdiParent = Me.ParentForm
        frmLUP_ITEM_OPEN_STK_ADJ.Show()
        frmLUP_ITEM_OPEN_STK_ADJ.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillComboBox_Item()
        Dim Str1 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM ORDER BY sITEM_NAME"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_ITEM1.Clear()
        Me.daLUP_ITEM.Fill(Me.DsLUP_ITEM1.V_LUP_ITEM)

        Dim dtLoading As New DataTable("LUP_VENDOR")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sITEM_NAME", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("VENDOR", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_ITEM1.V_LUP_ITEM.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(0).ToString
            dr("sITEM_NAME") = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(1).ToString
            dr("VENDOR") = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(12).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbItem.SelectedIndex = -1
        Me.CmbItem.Items.Clear()
        Me.CmbItem.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbItem.SourceDataString = New String(2) {"sITEM_NAME", "VENDOR", "nCODE"}
        Me.CmbItem.SourceDataTable = dtLoading
    End Sub

    Private Sub FillListView()
        Try
            
            Dim Str1 As String = "SELECT ID, ITEM_CODE, BATCH, COST, RATE, RETAIL, ITEM_NAME, PPP, PKS_DESC, PCS_DESC, OPEN_BAL, OPEN_STK_VALUE, dEXP_DATE FROM V_ITEM_OPEN_STK ORDER BY ITEM_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_ITEM_OPEN_STK = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_ITEM_OPEN_STK11.Clear()
            Me.daV_ITEM_OPEN_STK.Fill(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(1).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim StrDate As String = Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(12).ToString
                    If Not StrDate = Nothing Then
                        .Add(CDate(StrDate).ToString("dd-MMM-yyyy"), Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    Else
                        .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(12).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    End If

                    .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(10).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim Str1 As String = "SELECT ID, ITEM_CODE, BATCH, COST, RATE, RETAIL, ITEM_NAME, PPP, PKS_DESC, PCS_DESC, OPEN_BAL, OPEN_STK_VALUE, dEXP_DATE FROM V_ITEM_OPEN_STK WHERE ITEM_NAME LIKE '%" & Me.TxtSearch.Text & "%' ORDER BY ITEM_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_ITEM_OPEN_STK = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_ITEM_OPEN_STK11.Clear()
            Me.daV_ITEM_OPEN_STK.Fill(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(1).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim StrDate As String = Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(12).ToString
                    If Not StrDate = Nothing Then
                        .Add(CDate(StrDate).ToString("dd-MMM-yyyy"), Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    Else
                        .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(12).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    End If

                    .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(10).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsV_ITEM_OPEN_STK11.V_ITEM_OPEN_STK.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

  
End Class
