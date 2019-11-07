<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEXPENSE_DETAIL
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEXPENSE_DETAIL))
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.BttnDelete = New System.Windows.Forms.Button
        Me.BttnSearch = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.TxtDescription = New System.Windows.Forms.TextBox
        Me.DsEXPENSE_DETAIL1 = New Neruo_Business_Solution.dsEXPENSE_DETAIL
        Me.CmbExpenseType = New MTGCComboBox
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.TxtChequeType = New System.Windows.Forms.TextBox
        Me.TxtChequeDate = New System.Windows.Forms.TextBox
        Me.TxtChequeNo = New System.Windows.Forms.TextBox
        Me.TxtBankPmt = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.CmbGroup = New MTGCComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.CmbBankAccount = New MTGCComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtCashPmt = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_BANK = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.Label10 = New System.Windows.Forms.Label
        Me.LblBankBal = New System.Windows.Forms.Label
        Me.DsSV_BANK_BAL1 = New Neruo_Business_Solution.dsSV_BANK_BAL
        Me.Label15 = New System.Windows.Forms.Label
        Me.LblCashBal = New System.Windows.Forms.Label
        Me.DsSV_CASH_BAL1 = New Neruo_Business_Solution.dsSV_CASH_BAL
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.daSV_BANK_BAL = New System.Data.SqlClient.SqlDataAdapter
        Me.daSV_CASH_BAL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand6 = New System.Data.SqlClient.SqlCommand
        Me.LblID = New System.Windows.Forms.Label
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.daV_EXPENSE_SUB = New System.Data.SqlClient.SqlDataAdapter
        Me.daEXPENSE_DETAIL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_BUSINESS_GROUP1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
        Me.DsLUP_BANK1 = New Neruo_Business_Solution.dsLUP_BANK
        Me.DsV_EXPENSE_SUB1 = New Neruo_Business_Solution.dsV_EXPENSE_SUB
        Me.daNS_DEFAULT = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand8 = New System.Data.SqlClient.SqlCommand
        Me.DsNS_DEFAULT1 = New Neruo_Business_Solution.dsNS_DEFAULT
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsEXPENSE_DETAIL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSV_BANK_BAL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSV_CASH_BAL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_BANK1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_EXPENSE_SUB1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsNS_DEFAULT1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(584, 54)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Expense Detail"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.BttnDelete)
        Me.GroupBox3.Controls.Add(Me.BttnSearch)
        Me.GroupBox3.Controls.Add(Me.BttnClose)
        Me.GroupBox3.Controls.Add(Me.BttnNew)
        Me.GroupBox3.Controls.Add(Me.BttnSave)
        Me.GroupBox3.Controls.Add(Me.TxtDescription)
        Me.GroupBox3.Controls.Add(Me.CmbExpenseType)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Controls.Add(Me.TxtChequeType)
        Me.GroupBox3.Controls.Add(Me.TxtChequeDate)
        Me.GroupBox3.Controls.Add(Me.TxtChequeNo)
        Me.GroupBox3.Controls.Add(Me.TxtBankPmt)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.CmbGroup)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.CmbBankAccount)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtCashPmt)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(560, 267)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'BttnDelete
        '
        Me.BttnDelete.Enabled = False
        Me.BttnDelete.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnDelete.ForeColor = System.Drawing.Color.Red
        Me.BttnDelete.Location = New System.Drawing.Point(284, 213)
        Me.BttnDelete.Name = "BttnDelete"
        Me.BttnDelete.Size = New System.Drawing.Size(81, 32)
        Me.BttnDelete.TabIndex = 24
        Me.BttnDelete.Text = "&Delete"
        '
        'BttnSearch
        '
        Me.BttnSearch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch.ForeColor = System.Drawing.Color.DarkBlue
        Me.BttnSearch.Location = New System.Drawing.Point(197, 213)
        Me.BttnSearch.Name = "BttnSearch"
        Me.BttnSearch.Size = New System.Drawing.Size(81, 32)
        Me.BttnSearch.TabIndex = 23
        Me.BttnSearch.Text = "Sea&rch"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(329, 175)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(81, 32)
        Me.BttnClose.TabIndex = 22
        Me.BttnClose.Text = "&Close"
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(151, 175)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(81, 32)
        Me.BttnNew.TabIndex = 21
        Me.BttnNew.Text = "&New"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(240, 175)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(81, 32)
        Me.BttnSave.TabIndex = 20
        Me.BttnSave.Text = "&Save"
        '
        'TxtDescription
        '
        Me.TxtDescription.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.EXP_DESC", True))
        Me.TxtDescription.Location = New System.Drawing.Point(381, 122)
        Me.TxtDescription.Multiline = True
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(166, 49)
        Me.TxtDescription.TabIndex = 19
        '
        'DsEXPENSE_DETAIL1
        '
        Me.DsEXPENSE_DETAIL1.DataSetName = "dsEXPENSE_DETAIL"
        Me.DsEXPENSE_DETAIL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CmbExpenseType
        '
        Me.CmbExpenseType.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbExpenseType.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbExpenseType.ColumnNum = 3
        Me.CmbExpenseType.ColumnWidth = "140;110;20"
        Me.CmbExpenseType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.EXP_NAME", True))
        Me.CmbExpenseType.DisplayMember = "Text"
        Me.CmbExpenseType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbExpenseType.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbExpenseType.DropDownForeColor = System.Drawing.Color.White
        Me.CmbExpenseType.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbExpenseType.DropDownWidth = 340
        Me.CmbExpenseType.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbExpenseType.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbExpenseType.GridLineHorizontal = False
        Me.CmbExpenseType.GridLineVertical = True
        Me.CmbExpenseType.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbExpenseType.Location = New System.Drawing.Point(112, 49)
        Me.CmbExpenseType.ManagingFastMouseMoving = True
        Me.CmbExpenseType.ManagingFastMouseMovingInterval = 30
        Me.CmbExpenseType.Name = "CmbExpenseType"
        Me.CmbExpenseType.Size = New System.Drawing.Size(158, 22)
        Me.CmbExpenseType.TabIndex = 3
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.dDATE", True))
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDate.Location = New System.Drawing.Point(112, 26)
        Me.TxtDate.MaxLength = 50
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(119, 21)
        Me.TxtDate.TabIndex = 1
        '
        'TxtChequeType
        '
        Me.TxtChequeType.BackColor = System.Drawing.Color.White
        Me.TxtChequeType.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.CHQ_TYPE", True))
        Me.TxtChequeType.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtChequeType.Location = New System.Drawing.Point(381, 98)
        Me.TxtChequeType.MaxLength = 50
        Me.TxtChequeType.Name = "TxtChequeType"
        Me.TxtChequeType.Size = New System.Drawing.Size(166, 21)
        Me.TxtChequeType.TabIndex = 17
        '
        'TxtChequeDate
        '
        Me.TxtChequeDate.BackColor = System.Drawing.Color.White
        Me.TxtChequeDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.CHQ_DATE", True))
        Me.TxtChequeDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtChequeDate.Location = New System.Drawing.Point(381, 74)
        Me.TxtChequeDate.MaxLength = 50
        Me.TxtChequeDate.Name = "TxtChequeDate"
        Me.TxtChequeDate.Size = New System.Drawing.Size(108, 21)
        Me.TxtChequeDate.TabIndex = 15
        '
        'TxtChequeNo
        '
        Me.TxtChequeNo.BackColor = System.Drawing.Color.White
        Me.TxtChequeNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.CHQ_NO", True))
        Me.TxtChequeNo.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtChequeNo.Location = New System.Drawing.Point(381, 50)
        Me.TxtChequeNo.MaxLength = 50
        Me.TxtChequeNo.Name = "TxtChequeNo"
        Me.TxtChequeNo.Size = New System.Drawing.Size(166, 21)
        Me.TxtChequeNo.TabIndex = 13
        '
        'TxtBankPmt
        '
        Me.TxtBankPmt.BackColor = System.Drawing.Color.White
        Me.TxtBankPmt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.BANK_AMT", True))
        Me.TxtBankPmt.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtBankPmt.Location = New System.Drawing.Point(171, 148)
        Me.TxtBankPmt.MaxLength = 50
        Me.TxtBankPmt.Name = "TxtBankPmt"
        Me.TxtBankPmt.Size = New System.Drawing.Size(99, 21)
        Me.TxtBankPmt.TabIndex = 9
        Me.TxtBankPmt.Text = "0.00"
        Me.TxtBankPmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(7, 26)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 21)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(276, 73)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 23)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Chq/Online Date"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Expense Type"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(276, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 23)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Chq/DD/TC #"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbGroup
        '
        Me.CmbGroup.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup.ColumnNum = 3
        Me.CmbGroup.ColumnWidth = "100;100;30"
        Me.CmbGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.GROUP_NAME", True))
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
        Me.CmbGroup.Location = New System.Drawing.Point(112, 73)
        Me.CmbGroup.ManagingFastMouseMoving = True
        Me.CmbGroup.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup.Name = "CmbGroup"
        Me.CmbGroup.Size = New System.Drawing.Size(158, 22)
        Me.CmbGroup.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(171, 123)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 23)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "By Bank"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(276, 122)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(103, 34)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Expense Description"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(276, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 23)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Payment Type"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbBankAccount
        '
        Me.CmbBankAccount.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbBankAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbBankAccount.ColumnNum = 2
        Me.CmbBankAccount.ColumnWidth = "140;40"
        Me.CmbBankAccount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.BANK_ACC", True))
        Me.CmbBankAccount.DisplayMember = "Text"
        Me.CmbBankAccount.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbBankAccount.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbBankAccount.DropDownForeColor = System.Drawing.Color.White
        Me.CmbBankAccount.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbBankAccount.DropDownWidth = 340
        Me.CmbBankAccount.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbBankAccount.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbBankAccount.GridLineHorizontal = False
        Me.CmbBankAccount.GridLineVertical = True
        Me.CmbBankAccount.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbBankAccount.Location = New System.Drawing.Point(381, 25)
        Me.CmbBankAccount.ManagingFastMouseMoving = True
        Me.CmbBankAccount.ManagingFastMouseMovingInterval = 30
        Me.CmbBankAccount.Name = "CmbBankAccount"
        Me.CmbBankAccount.Size = New System.Drawing.Size(166, 22)
        Me.CmbBankAccount.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(276, 25)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 23)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Bank Account"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCashPmt
        '
        Me.TxtCashPmt.BackColor = System.Drawing.Color.White
        Me.TxtCashPmt.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEXPENSE_DETAIL1, "V_EXPENSES.CASH_AMT", True))
        Me.TxtCashPmt.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtCashPmt.Location = New System.Drawing.Point(8, 148)
        Me.TxtCashPmt.MaxLength = 50
        Me.TxtCashPmt.Name = "TxtCashPmt"
        Me.TxtCashPmt.Size = New System.Drawing.Size(99, 21)
        Me.TxtCashPmt.TabIndex = 7
        Me.TxtCashPmt.Text = "0.00"
        Me.TxtCashPmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(7, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "B. Group"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 123)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(99, 23)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "By Cash"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daLUP_BANK
        '
        Me.daLUP_BANK.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_BANK.InsertCommand = Me.SqlInsertCommand1
        Me.daLUP_BANK.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_BANK.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_BANK", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("sACCOUNT_NO", "sACCOUNT_NO"), New System.Data.Common.DataColumnMapping("sBANK_NAME", "sBANK_NAME"), New System.Data.Common.DataColumnMapping("sBRANCH_NAME", "sBRANCH_NAME"), New System.Data.Common.DataColumnMapping("sBRANCH_code", "sBRANCH_code"), New System.Data.Common.DataColumnMapping("sADDRESS", "sADDRESS"), New System.Data.Common.DataColumnMapping("sCONTACT1", "sCONTACT1"), New System.Data.Common.DataColumnMapping("sCONTACT2", "sCONTACT2"), New System.Data.Common.DataColumnMapping("sMANAGER_NAME", "sMANAGER_NAME"), New System.Data.Common.DataColumnMapping("sMANAGER_PH", "sMANAGER_PH"), New System.Data.Common.DataColumnMapping("sMANAGER_CELL", "sMANAGER_CELL"), New System.Data.Common.DataColumnMapping("sSTATUS", "sSTATUS")})})
        Me.daLUP_BANK.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = resources.GetString("SqlDeleteCommand1.CommandText")
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_sACCOUNT_NO", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sACCOUNT_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sBANK_NAME", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sBANK_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sBRANCH_NAME", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sBRANCH_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sBRANCH_code", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sBRANCH_code", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT1", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT1", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT2", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT2", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sMANAGER_CELL", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMANAGER_CELL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sMANAGER_NAME", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMANAGER_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sMANAGER_PH", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMANAGER_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sSTATUS", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSTATUS", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = resources.GetString("SqlInsertCommand1.CommandText")
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sACCOUNT_NO", System.Data.SqlDbType.VarChar, 50, "sACCOUNT_NO"), New System.Data.SqlClient.SqlParameter("@sBANK_NAME", System.Data.SqlDbType.VarChar, 50, "sBANK_NAME"), New System.Data.SqlClient.SqlParameter("@sBRANCH_NAME", System.Data.SqlDbType.VarChar, 100, "sBRANCH_NAME"), New System.Data.SqlClient.SqlParameter("@sBRANCH_code", System.Data.SqlDbType.VarChar, 50, "sBRANCH_code"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.VarChar, 200, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sCONTACT1", System.Data.SqlDbType.VarChar, 50, "sCONTACT1"), New System.Data.SqlClient.SqlParameter("@sCONTACT2", System.Data.SqlDbType.VarChar, 50, "sCONTACT2"), New System.Data.SqlClient.SqlParameter("@sMANAGER_NAME", System.Data.SqlDbType.VarChar, 100, "sMANAGER_NAME"), New System.Data.SqlClient.SqlParameter("@sMANAGER_PH", System.Data.SqlDbType.VarChar, 50, "sMANAGER_PH"), New System.Data.SqlClient.SqlParameter("@sMANAGER_CELL", System.Data.SqlDbType.VarChar, 50, "sMANAGER_CELL"), New System.Data.SqlClient.SqlParameter("@sSTATUS", System.Data.SqlDbType.Bit, 1, "sSTATUS")})
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT sACCOUNT_NO, sBANK_NAME, sBRANCH_NAME, sBRANCH_code, sADDRESS, sCONTACT1, " & _
            "sCONTACT2, sMANAGER_NAME, sMANAGER_PH, sMANAGER_CELL, sSTATUS FROM LUP_BANK"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sACCOUNT_NO", System.Data.SqlDbType.VarChar, 50, "sACCOUNT_NO"), New System.Data.SqlClient.SqlParameter("@sBANK_NAME", System.Data.SqlDbType.VarChar, 50, "sBANK_NAME"), New System.Data.SqlClient.SqlParameter("@sBRANCH_NAME", System.Data.SqlDbType.VarChar, 100, "sBRANCH_NAME"), New System.Data.SqlClient.SqlParameter("@sBRANCH_code", System.Data.SqlDbType.VarChar, 50, "sBRANCH_code"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.VarChar, 200, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sCONTACT1", System.Data.SqlDbType.VarChar, 50, "sCONTACT1"), New System.Data.SqlClient.SqlParameter("@sCONTACT2", System.Data.SqlDbType.VarChar, 50, "sCONTACT2"), New System.Data.SqlClient.SqlParameter("@sMANAGER_NAME", System.Data.SqlDbType.VarChar, 100, "sMANAGER_NAME"), New System.Data.SqlClient.SqlParameter("@sMANAGER_PH", System.Data.SqlDbType.VarChar, 50, "sMANAGER_PH"), New System.Data.SqlClient.SqlParameter("@sMANAGER_CELL", System.Data.SqlDbType.VarChar, 50, "sMANAGER_CELL"), New System.Data.SqlClient.SqlParameter("@sSTATUS", System.Data.SqlDbType.Bit, 1, "sSTATUS"), New System.Data.SqlClient.SqlParameter("@Original_sACCOUNT_NO", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sACCOUNT_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sBANK_NAME", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sBANK_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sBRANCH_NAME", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sBRANCH_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sBRANCH_code", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sBRANCH_code", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT1", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT1", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT2", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT2", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sMANAGER_CELL", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMANAGER_CELL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sMANAGER_NAME", System.Data.SqlDbType.VarChar, 100, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMANAGER_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sMANAGER_PH", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMANAGER_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sSTATUS", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSTATUS", System.Data.DataRowVersion.Original, Nothing)})
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 23)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Bank Bal:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblBankBal
        '
        Me.LblBankBal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblBankBal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSV_BANK_BAL1, "SV_BANK_BALANCE.BANK_BAL", True))
        Me.LblBankBal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBankBal.Location = New System.Drawing.Point(86, 32)
        Me.LblBankBal.Name = "LblBankBal"
        Me.LblBankBal.Size = New System.Drawing.Size(100, 23)
        Me.LblBankBal.TabIndex = 6
        Me.LblBankBal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DsSV_BANK_BAL1
        '
        Me.DsSV_BANK_BAL1.DataSetName = "dsSV_BANK_BAL"
        Me.DsSV_BANK_BAL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(12, 9)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 23)
        Me.Label15.TabIndex = 3
        Me.Label15.Text = "Cash Bal:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCashBal
        '
        Me.LblCashBal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCashBal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSV_CASH_BAL1, "SV_CASH_BALANCE.CASH_BAL", True))
        Me.LblCashBal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCashBal.Location = New System.Drawing.Point(86, 9)
        Me.LblCashBal.Name = "LblCashBal"
        Me.LblCashBal.Size = New System.Drawing.Size(100, 23)
        Me.LblCashBal.TabIndex = 4
        Me.LblCashBal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DsSV_CASH_BAL1
        '
        Me.DsSV_CASH_BAL1.DataSetName = "dsSV_CASH_BAL"
        Me.DsSV_CASH_BAL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = "SELECT     BANK_ACC, BANK_BAL, GROUP_ID" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         SV_BANK_BALANCE"
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        '
        'daSV_BANK_BAL
        '
        Me.daSV_BANK_BAL.SelectCommand = Me.SqlSelectCommand2
        Me.daSV_BANK_BAL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SV_BANK_BALANCE", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("BANK_BAL", "BANK_BAL"), New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID")})})
        '
        'daSV_CASH_BAL
        '
        Me.daSV_CASH_BAL.SelectCommand = Me.SqlCommand6
        Me.daSV_CASH_BAL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SV_CASH_BALANCE", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID"), New System.Data.Common.DataColumnMapping("CASH_BAL", "CASH_BAL")})})
        '
        'SqlCommand6
        '
        Me.SqlCommand6.CommandText = "SELECT     GROUP_ID, CASH_BAL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         SV_CASH_BALANCE"
        Me.SqlCommand6.Connection = Me.SqlConnection1
        '
        'LblID
        '
        Me.LblID.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblID.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblID.Location = New System.Drawing.Point(469, 31)
        Me.LblID.Name = "LblID"
        Me.LblID.Size = New System.Drawing.Size(103, 23)
        Me.LblID.TabIndex = 2
        Me.LblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SqlCommand2
        '
        Me.SqlCommand2.CommandText = "SELECT     CODE, SUB_EXP_NAME, MAIN_EXP_NAME" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_LUP_EXPENSE_SUB"
        Me.SqlCommand2.Connection = Me.SqlConnection1
        '
        'daV_EXPENSE_SUB
        '
        Me.daV_EXPENSE_SUB.SelectCommand = Me.SqlCommand2
        Me.daV_EXPENSE_SUB.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_EXPENSE_SUB", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("SUB_EXP_NAME", "SUB_EXP_NAME"), New System.Data.Common.DataColumnMapping("MAIN_EXP_NAME", "MAIN_EXP_NAME")})})
        '
        'daEXPENSE_DETAIL
        '
        Me.daEXPENSE_DETAIL.SelectCommand = Me.SqlCommand1
        Me.daEXPENSE_DETAIL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_EXPENSES", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("EXP_NAME", "EXP_NAME"), New System.Data.Common.DataColumnMapping("EXP_DESC", "EXP_DESC"), New System.Data.Common.DataColumnMapping("CASH_AMT", "CASH_AMT"), New System.Data.Common.DataColumnMapping("CHQ_NO", "CHQ_NO"), New System.Data.Common.DataColumnMapping("CHQ_TYPE", "CHQ_TYPE"), New System.Data.Common.DataColumnMapping("CHQ_DATE", "CHQ_DATE"), New System.Data.Common.DataColumnMapping("BANK_AMT", "BANK_AMT"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("USER_NAME", "USER_NAME"), New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID")})})
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = resources.GetString("SqlCommand1.CommandText")
        Me.SqlCommand1.Connection = Me.SqlConnection1
        '
        'DsLUP_BUSINESS_GROUP1
        '
        Me.DsLUP_BUSINESS_GROUP1.DataSetName = "dsLUP_BUSINESS_GROUP"
        Me.DsLUP_BUSINESS_GROUP1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_BANK1
        '
        Me.DsLUP_BANK1.DataSetName = "dsLUP_BANK"
        Me.DsLUP_BANK1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_BANK1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsV_EXPENSE_SUB1
        '
        Me.DsV_EXPENSE_SUB1.DataSetName = "dsV_EXPENSE_SUB"
        Me.DsV_EXPENSE_SUB1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daNS_DEFAULT
        '
        Me.daNS_DEFAULT.DeleteCommand = Me.SqlCommand4
        Me.daNS_DEFAULT.InsertCommand = Me.SqlCommand5
        Me.daNS_DEFAULT.SelectCommand = Me.SqlCommand7
        Me.daNS_DEFAULT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "NS_DEFAULT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("GROUP", "GROUP"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("S_MAN", "S_MAN"), New System.Data.Common.DataColumnMapping("P_MAN", "P_MAN"), New System.Data.Common.DataColumnMapping("D_MAN", "D_MAN"), New System.Data.Common.DataColumnMapping("R_MAN", "R_MAN"), New System.Data.Common.DataColumnMapping("CLIENT", "CLIENT"), New System.Data.Common.DataColumnMapping("CLIENT_TYPE", "CLIENT_TYPE"), New System.Data.Common.DataColumnMapping("CLIENT_CAT", "CLIENT_CAT"), New System.Data.Common.DataColumnMapping("CLIENT_GD", "CLIENT_GD"), New System.Data.Common.DataColumnMapping("ZONE", "ZONE"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("EXP_SUB_HEAD", "EXP_SUB_HEAD"), New System.Data.Common.DataColumnMapping("PRINTER", "PRINTER"), New System.Data.Common.DataColumnMapping("RPT_TITLE", "RPT_TITLE"), New System.Data.Common.DataColumnMapping("RPT_WARRANTY", "RPT_WARRANTY")})})
        Me.daNS_DEFAULT.UpdateCommand = Me.SqlCommand8
        '
        'SqlCommand4
        '
        Me.SqlCommand4.CommandText = resources.GetString("SqlCommand4.CommandText")
        Me.SqlCommand4.Connection = Me.SqlConnection1
        Me.SqlCommand4.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_GROUP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_GROUP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_BANK_ACC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_BANK_ACC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_S_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_S_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_P_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_P_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_D_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_D_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_R_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_R_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_CAT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_GD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_GD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ZONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ZONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ROUTE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ROUTE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_AREA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_AREA", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_EXP_SUB_HEAD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_PRINTER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_PRINTER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_TITLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_TITLE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_WARRANTY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand5
        '
        Me.SqlCommand5.CommandText = resources.GetString("SqlCommand5.CommandText")
        Me.SqlCommand5.Connection = Me.SqlConnection1
        Me.SqlCommand5.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@GROUP", System.Data.SqlDbType.VarChar, 0, "GROUP"), New System.Data.SqlClient.SqlParameter("@BANK_ACC", System.Data.SqlDbType.VarChar, 0, "BANK_ACC"), New System.Data.SqlClient.SqlParameter("@S_MAN", System.Data.SqlDbType.VarChar, 0, "S_MAN"), New System.Data.SqlClient.SqlParameter("@P_MAN", System.Data.SqlDbType.VarChar, 0, "P_MAN"), New System.Data.SqlClient.SqlParameter("@D_MAN", System.Data.SqlDbType.VarChar, 0, "D_MAN"), New System.Data.SqlClient.SqlParameter("@R_MAN", System.Data.SqlDbType.VarChar, 0, "R_MAN"), New System.Data.SqlClient.SqlParameter("@CLIENT", System.Data.SqlDbType.VarChar, 0, "CLIENT"), New System.Data.SqlClient.SqlParameter("@CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, "CLIENT_TYPE"), New System.Data.SqlClient.SqlParameter("@CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, "CLIENT_CAT"), New System.Data.SqlClient.SqlParameter("@CLIENT_GD", System.Data.SqlDbType.VarChar, 0, "CLIENT_GD"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 0, "ZONE"), New System.Data.SqlClient.SqlParameter("@ROUTE", System.Data.SqlDbType.VarChar, 0, "ROUTE"), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 0, "AREA"), New System.Data.SqlClient.SqlParameter("@EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, "EXP_SUB_HEAD"), New System.Data.SqlClient.SqlParameter("@PRINTER", System.Data.SqlDbType.VarChar, 0, "PRINTER"), New System.Data.SqlClient.SqlParameter("@RPT_TITLE", System.Data.SqlDbType.VarChar, 0, "RPT_TITLE"), New System.Data.SqlClient.SqlParameter("@RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, "RPT_WARRANTY"), New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing)})
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
        'frmEXPENSE_DETAIL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(584, 337)
        Me.Controls.Add(Me.LblID)
        Me.Controls.Add(Me.LblCashBal)
        Me.Controls.Add(Me.LblBankBal)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmEXPENSE_DETAIL"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Expense Detail"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsEXPENSE_DETAIL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSV_BANK_BAL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSV_CASH_BAL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_BANK1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_EXPENSE_SUB1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsNS_DEFAULT1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtCashPmt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtBankPmt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbBankAccount As MTGCComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CmbExpenseType As MTGCComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmbGroup As MTGCComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents TxtDescription As System.Windows.Forms.TextBox
    Friend WithEvents TxtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtChequeDate As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents daLUP_BUSINESS_GROUP As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents DsLUP_BUSINESS_GROUP1 As Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
    Friend WithEvents daLUP_BANK As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_BANK1 As Neruo_Business_Solution.dsLUP_BANK
    Friend WithEvents TxtChequeType As System.Windows.Forms.TextBox
    Friend WithEvents BttnSearch As System.Windows.Forms.Button
    Friend WithEvents BttnDelete As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents LblBankBal As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents LblCashBal As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daSV_BANK_BAL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsSV_BANK_BAL1 As Neruo_Business_Solution.dsSV_BANK_BAL
    Friend WithEvents DsSV_CASH_BAL1 As Neruo_Business_Solution.dsSV_CASH_BAL
    Friend WithEvents daSV_CASH_BAL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents LblID As System.Windows.Forms.Label
    Friend WithEvents SqlCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_EXPENSE_SUB As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsV_EXPENSE_SUB1 As Neruo_Business_Solution.dsV_EXPENSE_SUB
    Friend WithEvents DsEXPENSE_DETAIL1 As Neruo_Business_Solution.dsEXPENSE_DETAIL
    Friend WithEvents daEXPENSE_DETAIL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daNS_DEFAULT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand8 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsNS_DEFAULT1 As Neruo_Business_Solution.dsNS_DEFAULT
End Class
