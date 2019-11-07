Imports SDS = System.Data.SqlClient
Public Class frmLUP_ITEM_OPEN_STK_ADJ
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
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtAdjStockValue As System.Windows.Forms.TextBox
    Friend WithEvents TxtStockAdj As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents daITEM_OPEN_STK_ADJ As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents DsITEM_OPEN_STK_ADJ1 As Neruo_Business_Solution.dsITEM_OPEN_STK_ADJ
    Friend WithEvents DsITEM_OPEN_STK_ADJ11 As Neruo_Business_Solution.dsITEM_OPEN_STK_ADJ1
    Friend WithEvents LblID As System.Windows.Forms.Label
    Friend WithEvents daCmbItem As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsCmbItem1 As Neruo_Business_Solution.dsCmbItem
    Friend WithEvents CmbBatch As System.Windows.Forms.ComboBox
    Friend WithEvents CmbItem As MTGCComboBox
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents TxtReason As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_ITEM_OPEN_STK_ADJ))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.LblID = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CmbItem = New MTGCComboBox
        Me.DsITEM_OPEN_STK_ADJ1 = New Neruo_Business_Solution.dsITEM_OPEN_STK_ADJ
        Me.CmbBatch = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtCost = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtRate = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtAdjStockValue = New System.Windows.Forms.TextBox
        Me.TxtStockAdj = New System.Windows.Forms.TextBox
        Me.TxtPcsDesc = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtPackDesc = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtRetail = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtReason = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.TxtPPP = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daITEM_OPEN_STK_ADJ = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.DsITEM_OPEN_STK_ADJ11 = New Neruo_Business_Solution.dsITEM_OPEN_STK_ADJ1
        Me.daCmbItem = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.DsCmbItem1 = New Neruo_Business_Solution.dsCmbItem
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsITEM_OPEN_STK_ADJ1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsITEM_OPEN_STK_ADJ11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCmbItem1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Controls.Add(Me.LblID)
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
        Me.BttnPrevForm.Location = New System.Drawing.Point(3, 2)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(101, 46)
        Me.BttnPrevForm.TabIndex = 37
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Item Open Stock"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(586, 4)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(101, 46)
        Me.BttnNextForm.TabIndex = 38
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Employee Designation"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'LblID
        '
        Me.LblID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblID.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblID.Location = New System.Drawing.Point(583, 8)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(99, 23)
        Me.LblID.TabIndex = 2
        Me.LblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 268)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(671, 214)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader6, Me.ColumnHeader3, Me.ColumnHeader5, Me.ColumnHeader7})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 53)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(665, 158)
        Me.ListView1.TabIndex = 2
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Item Code"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Item Name"
        Me.ColumnHeader4.Width = 200
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Batch #"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Ajustment"
        Me.ColumnHeader3.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Date"
        Me.ColumnHeader5.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "ID"
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
        Me.GroupBox1.Location = New System.Drawing.Point(562, 56)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 206)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(16, 17)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(16, 159)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(16, 88)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.CmbItem)
        Me.GroupBox3.Controls.Add(Me.CmbBatch)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtCost)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtRate)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TxtAdjStockValue)
        Me.GroupBox3.Controls.Add(Me.TxtStockAdj)
        Me.GroupBox3.Controls.Add(Me.TxtPcsDesc)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.TxtPackDesc)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.TxtRetail)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.TxtReason)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Controls.Add(Me.TxtPPP)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(11, 54)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(545, 208)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'CmbItem
        '
        Me.CmbItem.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbItem.ColumnNum = 2
        Me.CmbItem.ColumnWidth = "140;40"
        Me.CmbItem.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.ITEM_NAME", True))
        Me.CmbItem.DisplayMember = "Text"
        Me.CmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbItem.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbItem.DropDownForeColor = System.Drawing.Color.White
        Me.CmbItem.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbItem.DropDownWidth = 220
        Me.CmbItem.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbItem.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbItem.GridLineHorizontal = False
        Me.CmbItem.GridLineVertical = True
        Me.CmbItem.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbItem.Location = New System.Drawing.Point(113, 17)
        Me.CmbItem.ManagingFastMouseMoving = True
        Me.CmbItem.ManagingFastMouseMovingInterval = 30
        Me.CmbItem.Name = "CmbItem"
        Me.CmbItem.Size = New System.Drawing.Size(182, 24)
        Me.CmbItem.TabIndex = 1
        '
        'DsITEM_OPEN_STK_ADJ1
        '
        Me.DsITEM_OPEN_STK_ADJ1.DataSetName = "dsITEM_OPEN_STK_ADJ"
        Me.DsITEM_OPEN_STK_ADJ1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CmbBatch
        '
        Me.CmbBatch.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.BATCH_NO", True))
        Me.CmbBatch.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbBatch.FormattingEnabled = True
        Me.CmbBatch.Location = New System.Drawing.Point(113, 44)
        Me.CmbBatch.Name = "CmbBatch"
        Me.CmbBatch.Size = New System.Drawing.Size(182, 24)
        Me.CmbBatch.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Item Code"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCost
        '
        Me.TxtCost.BackColor = System.Drawing.Color.White
        Me.TxtCost.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.COST", True))
        Me.TxtCost.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCost.Location = New System.Drawing.Point(422, 19)
        Me.TxtCost.Name = "TxtCost"
        Me.TxtCost.ReadOnly = True
        Me.TxtCost.Size = New System.Drawing.Size(117, 23)
        Me.TxtCost.TabIndex = 15
        Me.TxtCost.TabStop = False
        Me.TxtCost.Text = "0.00"
        Me.TxtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(301, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(115, 23)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Purchase Price"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtRate
        '
        Me.TxtRate.BackColor = System.Drawing.Color.White
        Me.TxtRate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.RATE", True))
        Me.TxtRate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(422, 45)
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.ReadOnly = True
        Me.TxtRate.Size = New System.Drawing.Size(117, 23)
        Me.TxtRate.TabIndex = 17
        Me.TxtRate.TabStop = False
        Me.TxtRate.Text = "0.00"
        Me.TxtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(301, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 23)
        Me.Label10.TabIndex = 16
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
        Me.Label17.TabIndex = 22
        Me.Label17.Text = "Adj. Stk Value"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(301, 96)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(115, 23)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "Adj. Stock Pcs"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 95)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 24)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Piece Desc"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtAdjStockValue
        '
        Me.TxtAdjStockValue.BackColor = System.Drawing.Color.White
        Me.TxtAdjStockValue.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.ADJ_VALUE", True))
        Me.TxtAdjStockValue.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdjStockValue.ForeColor = System.Drawing.Color.DarkBlue
        Me.TxtAdjStockValue.Location = New System.Drawing.Point(422, 121)
        Me.TxtAdjStockValue.Name = "TxtAdjStockValue"
        Me.TxtAdjStockValue.ReadOnly = True
        Me.TxtAdjStockValue.Size = New System.Drawing.Size(117, 23)
        Me.TxtAdjStockValue.TabIndex = 23
        Me.TxtAdjStockValue.TabStop = False
        Me.TxtAdjStockValue.Text = "0.00"
        Me.TxtAdjStockValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtStockAdj
        '
        Me.TxtStockAdj.BackColor = System.Drawing.Color.White
        Me.TxtStockAdj.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.ADJ_QTY", True))
        Me.TxtStockAdj.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TxtStockAdj.Location = New System.Drawing.Point(422, 96)
        Me.TxtStockAdj.Name = "TxtStockAdj"
        Me.TxtStockAdj.Size = New System.Drawing.Size(117, 22)
        Me.TxtStockAdj.TabIndex = 21
        Me.TxtStockAdj.Text = "0"
        Me.TxtStockAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtPcsDesc
        '
        Me.TxtPcsDesc.BackColor = System.Drawing.Color.White
        Me.TxtPcsDesc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.PCS_DESC", True))
        Me.TxtPcsDesc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPcsDesc.Location = New System.Drawing.Point(175, 96)
        Me.TxtPcsDesc.Name = "TxtPcsDesc"
        Me.TxtPcsDesc.ReadOnly = True
        Me.TxtPcsDesc.Size = New System.Drawing.Size(120, 23)
        Me.TxtPcsDesc.TabIndex = 7
        Me.TxtPcsDesc.TabStop = False
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
        'TxtPackDesc
        '
        Me.TxtPackDesc.BackColor = System.Drawing.Color.White
        Me.TxtPackDesc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.PKS_DESC", True))
        Me.TxtPackDesc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPackDesc.Location = New System.Drawing.Point(175, 71)
        Me.TxtPackDesc.Name = "TxtPackDesc"
        Me.TxtPackDesc.ReadOnly = True
        Me.TxtPackDesc.Size = New System.Drawing.Size(120, 23)
        Me.TxtPackDesc.TabIndex = 5
        Me.TxtPackDesc.TabStop = False
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 23)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Pack Desc"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(301, 71)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(115, 23)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "Retail Price"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtRetail
        '
        Me.TxtRetail.BackColor = System.Drawing.Color.White
        Me.TxtRetail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.RETAIL", True))
        Me.TxtRetail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRetail.Location = New System.Drawing.Point(422, 71)
        Me.TxtRetail.Name = "TxtRetail"
        Me.TxtRetail.ReadOnly = True
        Me.TxtRetail.Size = New System.Drawing.Size(117, 23)
        Me.TxtRetail.TabIndex = 19
        Me.TxtRetail.TabStop = False
        Me.TxtRetail.Text = "0.00"
        Me.TxtRetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 171)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 23)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Reason"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 146)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 23)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtReason
        '
        Me.TxtReason.BackColor = System.Drawing.Color.White
        Me.TxtReason.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.REASON", True))
        Me.TxtReason.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReason.Location = New System.Drawing.Point(113, 171)
        Me.TxtReason.MaxLength = 100
        Me.TxtReason.Name = "TxtReason"
        Me.TxtReason.Size = New System.Drawing.Size(426, 23)
        Me.TxtReason.TabIndex = 13
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 121)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 23)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "Pcs Per Pack"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.dDATE", True))
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate.Location = New System.Drawing.Point(175, 146)
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(120, 23)
        Me.TxtDate.TabIndex = 11
        '
        'TxtPPP
        '
        Me.TxtPPP.BackColor = System.Drawing.Color.White
        Me.TxtPPP.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsITEM_OPEN_STK_ADJ1, "V_ITEM_OPEN_STK_ADJ.PPP", True))
        Me.TxtPPP.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPPP.Location = New System.Drawing.Point(175, 121)
        Me.TxtPPP.Name = "TxtPPP"
        Me.TxtPPP.ReadOnly = True
        Me.TxtPPP.Size = New System.Drawing.Size(120, 23)
        Me.TxtPPP.TabIndex = 9
        Me.TxtPPP.TabStop = False
        Me.TxtPPP.Text = "0"
        Me.TxtPPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(692, 35)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Batch Wise Opening Stock Adjustment"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daITEM_OPEN_STK_ADJ
        '
        Me.daITEM_OPEN_STK_ADJ.SelectCommand = Me.SqlCommand1
        Me.daITEM_OPEN_STK_ADJ.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_ITEM_OPEN_STK_ADJ", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("ITEM_CODE", "ITEM_CODE"), New System.Data.Common.DataColumnMapping("ITEM_NAME", "ITEM_NAME"), New System.Data.Common.DataColumnMapping("BATCH_NO", "BATCH_NO"), New System.Data.Common.DataColumnMapping("PKS_DESC", "PKS_DESC"), New System.Data.Common.DataColumnMapping("PCS_DESC", "PCS_DESC"), New System.Data.Common.DataColumnMapping("PPP", "PPP"), New System.Data.Common.DataColumnMapping("ADJ_QTY", "ADJ_QTY"), New System.Data.Common.DataColumnMapping("COST", "COST"), New System.Data.Common.DataColumnMapping("RATE", "RATE"), New System.Data.Common.DataColumnMapping("RETAIL", "RETAIL"), New System.Data.Common.DataColumnMapping("ADJ_VALUE", "ADJ_VALUE"), New System.Data.Common.DataColumnMapping("REASON", "REASON"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE")})})
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = resources.GetString("SqlCommand1.CommandText")
        Me.SqlCommand1.Connection = Me.SqlConnection1
        '
        'DsITEM_OPEN_STK_ADJ11
        '
        Me.DsITEM_OPEN_STK_ADJ11.DataSetName = "dsITEM_OPEN_STK_ADJ1"
        Me.DsITEM_OPEN_STK_ADJ11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daCmbItem
        '
        Me.daCmbItem.SelectCommand = Me.SqlSelectCommand1
        Me.daCmbItem.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_ITEM_OPEN_STK", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ITEM_NAME", "ITEM_NAME"), New System.Data.Common.DataColumnMapping("ITEM_CODE", "ITEM_CODE")})})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT DISTINCT ITEM_NAME, ITEM_CODE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_ITEM_OPEN_STK"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'DsCmbItem1
        '
        Me.DsCmbItem1.DataSetName = "dsCmbItem"
        Me.DsCmbItem1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_ITEM_OPEN_STK_ADJ
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(718, 506)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_ITEM_OPEN_STK_ADJ"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Item's Opening Stock Batch Wise Adjustment"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsITEM_OPEN_STK_ADJ1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsITEM_OPEN_STK_ADJ11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCmbItem1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Dim asCmb As New AssComboBox
    Dim asMAX As New AssMaxNo
    Dim Rd As System.Data.SqlClient.SqlDataReader

#End Region

#Region "FORM CONTROL"
    Private Sub frmLUP_ITEM_OPEN_STK_ADJ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Item()
        Me.FillListView()
        Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmLUP_ITEM_OPEN_STK_ADJ_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtCost.GotFocus, TxtRate.GotFocus, TxtPcsDesc.GotFocus, TxtPackDesc.GotFocus, TxtRetail.GotFocus, TxtPPP.GotFocus, TxtStockAdj.GotFocus, TxtAdjStockValue.GotFocus, TxtReason.GotFocus, TxtDate.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtCost.LostFocus, TxtRate.LostFocus, TxtPcsDesc.LostFocus, TxtPackDesc.LostFocus, TxtRetail.LostFocus, TxtPPP.LostFocus, TxtStockAdj.LostFocus, TxtAdjStockValue.LostFocus, TxtDate.LostFocus, TxtReason.LostFocus
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

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtStockAdj.KeyPress
        Me.asNum.NumPressDash(e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtAdjStockValue.KeyPress, TxtCost.KeyPress, TxtRate.KeyPress, TxtRetail.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    'Open Stock Value Calculation
    Private Sub TxtCost_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCost.TextChanged, TxtStockAdj.TextChanged
        Me.TxtAdjStockValue.Text = Val(Me.TxtStockAdj.Text) * (Val(Me.TxtCost.Text) / Val(Me.TxtPPP.Text))
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
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBatch.GotFocus, CmbItem.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBatch.LostFocus, CmbItem.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
        If sender.Name = "CmbBatch" Then
            Me.CmbBatch.SelectedIndex = Me.CmbBatch.FindString(Me.CmbBatch.Text)
        End If
    End Sub
    Private Sub CmbItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbItem.SelectedIndexChanged
        On Error GoTo Fix
        Me.asCmb.LoadComboBox(Rd, "SELECT BATCH FROM V_ITEM_OPEN_STK WHERE ITEM_CODE=" & Val(Me.CmbItem.SelectedItem.Col2) & "", Me.CmbBatch)
        If Me.CmbBatch.Items.Count > 0 Then
            Me.CmbBatch.SelectedIndex = 0
        End If
        Me.asTXT.LoadTextBox(Rd, "SELECT sPACK_DESC, sPIECE_DESC, nPPP FROM LUP_ITEMS WHERE nCODE = " & Val(Me.CmbItem.SelectedItem.Col2) & "", Me.TxtPackDesc, Me.TxtPcsDesc, Me.TxtPPP)
        If Not Me.CmbBatch.SelectedIndex = -1 Then
            Me.asTXT.LoadTextBox(Rd, "SELECT nUNIT_COST, nUNIT_RATE, nUNIT_RETAIL FROM ITEM_OPEN_STK WHERE nITEM_CODE=" & Val(Me.CmbItem.SelectedItem.Col2) & " AND sBATCH='" & Me.CmbBatch.Text & "'", Me.TxtCost, Me.TxtRate, Me.TxtRetail)
        End If

Fix:
    End Sub
    Private Sub CmbBatch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbBatch.SelectedIndexChanged
        On Error GoTo Fix
        Me.asTXT.LoadTextBox(Rd, "SELECT nUNIT_COST, nUNIT_RATE, nUNIT_RETAIL FROM ITEM_OPEN_STK WHERE nITEM_CODE=" & Val(Me.CmbItem.SelectedItem.Col2) & " AND sBATCH='" & Me.CmbBatch.Text & "'", Me.TxtCost, Me.TxtRate, Me.TxtRetail)
Fix:
    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        If Not Me.ListView1.SelectedItems(0).SubItems(5).Text = Nothing Then
            Dim Str1 As String = "SELECT ID, ITEM_CODE, ITEM_NAME, BATCH_NO, PKS_DESC, PCS_DESC, PPP, ADJ_QTY, CONVERT(NUMERIC(18,2), COST) AS COST, CONVERT(NUMERIC(18,2), RATE) AS RATE, CONVERT(NUMERIC(18,2), RETAIL) AS RETAIL, CONVERT(NUMERIC(18,2), ADJ_VALUE) AS ADJ_VALUE, REASON, dDATE FROM V_ITEM_OPEN_STK_ADJ WHERE ID=" & Val(Me.ListView1.SelectedItems(0).SubItems(5).Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daITEM_OPEN_STK_ADJ = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsITEM_OPEN_STK_ADJ1.Clear()
            Me.daITEM_OPEN_STK_ADJ.Fill(Me.DsITEM_OPEN_STK_ADJ1.V_ITEM_OPEN_STK_ADJ)

            Dim StrCmb As String = Me.CmbItem.Text
            Me.CmbItem.SelectedIndex = -1
            Me.CmbItem.SelectedIndex = Me.CmbItem.FindString(StrCmb)
        End If

FIX:
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If Not Me.ListView1.SelectedItems(0).SubItems(5).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM ADJUSTED_OPEN_STOCK WHERE nCODE=" & Val(Me.ListView1.SelectedItems(0).SubItems(5).Text) & "")

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
        Me.DsITEM_OPEN_STK_ADJ1.Clear()

        Me.LblID.Text = Nothing
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.TxtSearch.Text = Nothing

        Me.CmbItem.Focus()

    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        If Me.CmbItem.Text = Nothing Or Me.CmbItem.SelectedIndex = -1 Or Me.CmbBatch.SelectedIndex = -1 Or Me.TxtDate.Text = Nothing Or Me.TxtReason.Text = Nothing Or Val(Me.TxtStockAdj.Text) = 0 Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

            If Me.CmbItem.Text = Nothing Or Me.CmbItem.SelectedIndex = -1 Then
                Me.CmbItem.Focus()

            ElseIf Me.CmbBatch.SelectedIndex = -1 Then
                Me.CmbBatch.Focus()

            ElseIf Me.TxtDate.Text = Nothing Then
                Me.TxtDate.Focus()

            ElseIf Me.TxtReason.Text = Nothing Then
                Me.TxtReason.Focus()

            ElseIf Val(Me.TxtStockAdj.Text) <= 0 Then
                Me.TxtStockAdj.Focus()

            End If

        ElseIf Val(Me.LblID.Text) <= 0 Then
            If MsgBox("Do you want to save '" & Me.CmbItem.Text & "' & '" & Me.CmbBatch.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO ADJUSTED_OPEN_STOCK(nITEM_CODE, sBATCH_NO, nADJUSTED_QTY, nUNIT_COST, nUNIT_RATE, nUNIT_RETAIL, nADJ_STK_VALUE, sREASON, dDATE) VALUES(" & Val(Me.CmbItem.SelectedItem.Col2) & ",'" & Me.CmbBatch.Text & "'," & Val(Me.TxtStockAdj.Text) & "," & Val(Me.TxtCost.Text) & "," & Val(Me.TxtRate.Text) & "," & Val(Me.TxtRetail.Text) & "," & Val(Me.TxtAdjStockValue.Text) & ",'" & Me.TxtReason.Text & "','" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")

                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.CmbItem.Focus()
            End If

        ElseIf Not Val(Me.LblID.Text) <= 0 Then
            If MsgBox("This Item '" & Me.CmbItem.Text & "' & BATCH # '" & Me.CmbBatch.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE ADJUSTED_OPEN_STOCK SET dEXP_DATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nUNIT_COST=" & Val(Me.TxtCost.Text) & ", nUNIT_RATE=" & Val(Me.TxtRate.Text) & ", nUNIT_RETAIL=" & Val(Me.TxtRetail.Text) & ", nADJUSTED_QTY=" & Val(Me.TxtStockAdj.Text) & ", nADJ_STK_VALUE=" & Val(Me.TxtAdjStockValue.Text) & ", sREASON='" & Me.TxtReason.Text & "' WHERE nITEM_CODE=" & Val(Me.CmbItem.SelectedItem.Col2) & " AND sBATCH_NO='" & Me.CmbBatch.Text & "'")
                'FILL THE RECORD IN LISTVIEW
                Me.LblID.Text = Nothing
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
        frmLUP_ITEM_OPEN_STK.MdiParent = Me.ParentForm
        frmLUP_ITEM_OPEN_STK.Show()
        frmLUP_ITEM_OPEN_STK.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_DESIGNATION.MdiParent = Me.ParentForm
        frmLUP_DESIGNATION.Show()
        frmLUP_DESIGNATION.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillComboBox_Item()
        Dim Str1 As String = "SELECT DISTINCT ITEM_NAME, ITEM_CODE FROM V_ITEM_OPEN_STK ORDER BY ITEM_NAME"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daCmbItem = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsCmbItem1.Clear()
        Me.daCmbItem.Fill(Me.DsCmbItem1.V_ITEM_OPEN_STK)

        Dim dtLoading As New DataTable("V_ITEM_OPEN_STK")

        dtLoading.Columns.Add("ITEM_CODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("ITEM_NAME", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsCmbItem1.V_ITEM_OPEN_STK.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("ITEM_CODE") = Me.DsCmbItem1.V_ITEM_OPEN_STK.Item(Cnt).Item(1).ToString
            dr("ITEM_NAME") = Me.DsCmbItem1.V_ITEM_OPEN_STK.Item(Cnt).Item(0).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbItem.SelectedIndex = -1
        Me.CmbItem.Items.Clear()
        Me.CmbItem.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbItem.SourceDataString = New String(1) {"ITEM_NAME", "ITEM_CODE"}
        Me.CmbItem.SourceDataTable = dtLoading
    End Sub

    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT ID, ITEM_CODE, BATCH_NO, PKS_DESC, PCS_DESC, PPP, ADJ_QTY, CONVERT(NUMERIC(18,2), COST) AS COST, CONVERT(NUMERIC(18,2), RATE) AS RATE, CONVERT(NUMERIC(18,2), RETAIL) AS RETAIL, CONVERT(NUMERIC(18,2), ADJ_VALUE) AS ADJ_VALUE, REASON, dDATE, ITEM_NAME FROM V_ITEM_OPEN_STK_ADJ ORDER BY ITEM_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daITEM_OPEN_STK_ADJ = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsITEM_OPEN_STK_ADJ11.Clear()
            Me.daITEM_OPEN_STK_ADJ.Fill(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(1).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(13).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim StrDate As String = Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(12).ToString
                    If Not StrDate = Nothing Then
                        .Add(CDate(StrDate).ToString("dd-MMM-yyyy"), Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    Else
                        .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(12).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    End If

                    .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FillListView_Condition()
        Try
            Dim Str1 As String = "SELECT ID, ITEM_CODE, BATCH_NO, PKS_DESC, PCS_DESC, PPP, ADJ_QTY, CONVERT(NUMERIC(18,2), COST) AS COST, CONVERT(NUMERIC(18,2), RATE) AS RATE, CONVERT(NUMERIC(18,2), RETAIL) AS RETAIL, CONVERT(NUMERIC(18,2), ADJ_VALUE) AS ADJ_VALUE, REASON, dDATE, ITEM_NAME FROM V_ITEM_OPEN_STK_ADJ ORDER BY ITEM_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daITEM_OPEN_STK_ADJ = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsITEM_OPEN_STK_ADJ11.Clear()
            Me.daITEM_OPEN_STK_ADJ.Fill(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(1).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(13).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim StrDate As String = Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(12).ToString
                    If Not StrDate = Nothing Then
                        .Add(CDate(StrDate).ToString("dd-MMM-yyyy"), Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    Else
                        .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(12).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    End If

                    .Add(Me.DsITEM_OPEN_STK_ADJ11.V_ITEM_OPEN_STK_ADJ.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    End Class
