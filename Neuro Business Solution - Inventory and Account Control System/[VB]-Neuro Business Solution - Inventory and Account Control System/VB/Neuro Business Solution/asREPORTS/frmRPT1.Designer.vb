<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPT1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRPT1))
        Me.BttnClose = New System.Windows.Forms.Button
        Me.VIEW_REPORTButton = New System.Windows.Forms.Button
        Me.pnlBANK_ACC = New System.Windows.Forms.Panel
        Me.Label32 = New System.Windows.Forms.Label
        Me.TxtDateTo7 = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.TxtDateFrom7 = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.CmbBankAccount = New MTGCComboBox
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_BANK = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.pnlSUPPLIER = New System.Windows.Forms.Panel
        Me.CmbGroup = New MTGCComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtDateTo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDateFrom = New System.Windows.Forms.TextBox
        Me.CmbSupplier = New MTGCComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.daSUPPLIER_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand8 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.CRV1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.pnlCLIENT = New System.Windows.Forms.Panel
        Me.CmbGroup1 = New MTGCComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtDateTo1 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtDateFrom1 = New System.Windows.Forms.TextBox
        Me.CmbClient = New MTGCComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.daCLIENT_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand
        Me.PnlCompany_Group = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.CmbCompany = New MTGCComboBox
        Me.CmbGroup2 = New MTGCComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.daLUP_VENDOR = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand6 = New System.Data.SqlClient.SqlCommand
        Me.PnlBankAcc_Date = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtDateTo2 = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtDateFrom2 = New System.Windows.Forms.TextBox
        Me.CmbBankAccount1 = New MTGCComboBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.PnlDate = New System.Windows.Forms.Panel
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtDateTo3 = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TxtDateFrom3 = New System.Windows.Forms.TextBox
        Me.PnlArea = New System.Windows.Forms.Panel
        Me.Label17 = New System.Windows.Forms.Label
        Me.CmbArea = New MTGCComboBox
        Me.daLUP_AREA = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlInsertCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand
        Me.PnlArea_Date = New System.Windows.Forms.Panel
        Me.Label19 = New System.Windows.Forms.Label
        Me.TxtDateTo4 = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TxtDateFrom4 = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.CmbArea1 = New MTGCComboBox
        Me.PnlInvoice = New System.Windows.Forms.Panel
        Me.Label21 = New System.Windows.Forms.Label
        Me.TxtInvoiceTo = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtInvoiceFrom = New System.Windows.Forms.TextBox
        Me.pnlITEM = New System.Windows.Forms.Panel
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtDateTo5 = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.TxtDateFrom5 = New System.Windows.Forms.TextBox
        Me.cmbITEM = New MTGCComboBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.daLUP_ITEM = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.pnlCOMPANY = New System.Windows.Forms.Panel
        Me.CmbCompany1 = New MTGCComboBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.pnlSALEMAN = New System.Windows.Forms.Panel
        Me.CmbS_Man = New MTGCComboBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.daLUP_EMPLOYEE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand9 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand10 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand11 = New System.Data.SqlClient.SqlCommand
        Me.pnlSALEMAN_DATE = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtDateTo6 = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.TxtDateFrom6 = New System.Windows.Forms.TextBox
        Me.CmbS_Man1 = New MTGCComboBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.daRptCLIENT_LEDGER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand13 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_BANK1 = New Neruo_Business_Solution.dsLUP_BANK
        Me.DsSUPPLIER_INFO1 = New Neruo_Business_Solution.dsSUPPLIER_INFO
        Me.DsCLIENT_INFO1 = New Neruo_Business_Solution.dsCLIENT_INFO
        Me.DsLUP_BUSINESS_GROUP1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
        Me.DsLUP_VENDOR1 = New Neruo_Business_Solution.dsLUP_VENDOR
        Me.DsLUP_AREA1 = New Neruo_Business_Solution.dsLUP_AREA
        Me.DsV_LUP_ITEM1 = New Neruo_Business_Solution.dsV_LUP_ITEM
        Me.DsLUP_EMPLOYEE1 = New Neruo_Business_Solution.dsLUP_EMPLOYEE
        Me.DsRptCLIENT_LEDGER1 = New Neruo_Business_Solution.dsRptCLIENT_LEDGER
        Me.daRptSUPPLIER_LEDGER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand12 = New System.Data.SqlClient.SqlCommand
        Me.DsRptSUPPLIER_LEDGER1 = New Neruo_Business_Solution.dsRptSUPPLIER_LEDGER
        Me.daRptCASH_LEDGER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand14 = New System.Data.SqlClient.SqlCommand
        Me.DsRptCASH_LEDGER1 = New Neruo_Business_Solution.dsRptCASH_LEDGER
        Me.daRptBANK_LEDGER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand15 = New System.Data.SqlClient.SqlCommand
        Me.DsRptBANK_LEDGER1 = New Neruo_Business_Solution.dsRptBANK_LEDGER
        Me.PnlSupplierDate = New System.Windows.Forms.Panel
        Me.Label35 = New System.Windows.Forms.Label
        Me.TxtDateTo8 = New System.Windows.Forms.TextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.TxtDateFrom8 = New System.Windows.Forms.TextBox
        Me.CmbSupplier1 = New MTGCComboBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.pnlBANK_ACC.SuspendLayout()
        Me.pnlSUPPLIER.SuspendLayout()
        Me.pnlCLIENT.SuspendLayout()
        Me.PnlCompany_Group.SuspendLayout()
        Me.PnlBankAcc_Date.SuspendLayout()
        Me.PnlDate.SuspendLayout()
        Me.PnlArea.SuspendLayout()
        Me.PnlArea_Date.SuspendLayout()
        Me.PnlInvoice.SuspendLayout()
        Me.pnlITEM.SuspendLayout()
        Me.pnlCOMPANY.SuspendLayout()
        Me.pnlSALEMAN.SuspendLayout()
        Me.pnlSALEMAN_DATE.SuspendLayout()
        CType(Me.DsLUP_BANK1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSUPPLIER_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsCLIENT_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_VENDOR1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_AREA1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_LUP_ITEM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsRptCLIENT_LEDGER1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsRptSUPPLIER_LEDGER1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsRptCASH_LEDGER1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsRptBANK_LEDGER1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlSupplierDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Location = New System.Drawing.Point(689, 50)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 23)
        Me.BttnClose.TabIndex = 3
        Me.BttnClose.TabStop = False
        Me.BttnClose.Text = "Close"
        Me.BttnClose.UseVisualStyleBackColor = True
        Me.BttnClose.Visible = False
        '
        'VIEW_REPORTButton
        '
        Me.VIEW_REPORTButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VIEW_REPORTButton.Location = New System.Drawing.Point(664, 19)
        Me.VIEW_REPORTButton.Name = "VIEW_REPORTButton"
        Me.VIEW_REPORTButton.Size = New System.Drawing.Size(100, 25)
        Me.VIEW_REPORTButton.TabIndex = 2
        Me.VIEW_REPORTButton.Text = "VIEW REPORT"
        Me.VIEW_REPORTButton.UseVisualStyleBackColor = True
        '
        'pnlBANK_ACC
        '
        Me.pnlBANK_ACC.Controls.Add(Me.Label32)
        Me.pnlBANK_ACC.Controls.Add(Me.TxtDateTo7)
        Me.pnlBANK_ACC.Controls.Add(Me.Label33)
        Me.pnlBANK_ACC.Controls.Add(Me.TxtDateFrom7)
        Me.pnlBANK_ACC.Controls.Add(Me.Label9)
        Me.pnlBANK_ACC.Controls.Add(Me.CmbBankAccount)
        Me.pnlBANK_ACC.Location = New System.Drawing.Point(0, 0)
        Me.pnlBANK_ACC.Name = "pnlBANK_ACC"
        Me.pnlBANK_ACC.Size = New System.Drawing.Size(660, 64)
        Me.pnlBANK_ACC.TabIndex = 0
        Me.pnlBANK_ACC.Visible = False
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(445, 22)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(21, 21)
        Me.Label32.TabIndex = 4
        Me.Label32.Text = "&To"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo7
        '
        Me.TxtDateTo7.BackColor = System.Drawing.Color.White
        Me.TxtDateTo7.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo7.Location = New System.Drawing.Point(470, 22)
        Me.TxtDateTo7.MaxLength = 50
        Me.TxtDateTo7.Name = "TxtDateTo7"
        Me.TxtDateTo7.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo7.TabIndex = 5
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(284, 22)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(68, 21)
        Me.Label33.TabIndex = 2
        Me.Label33.Text = "Date &From"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom7
        '
        Me.TxtDateFrom7.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom7.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom7.Location = New System.Drawing.Point(355, 22)
        Me.TxtDateFrom7.MaxLength = 50
        Me.TxtDateFrom7.Name = "TxtDateFrom7"
        Me.TxtDateFrom7.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom7.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 21)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 23)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Bank Account"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbBankAccount
        '
        Me.CmbBankAccount.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbBankAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbBankAccount.ColumnNum = 2
        Me.CmbBankAccount.ColumnWidth = "140;40"
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
        Me.CmbBankAccount.Location = New System.Drawing.Point(112, 21)
        Me.CmbBankAccount.ManagingFastMouseMoving = True
        Me.CmbBankAccount.ManagingFastMouseMovingInterval = 30
        Me.CmbBankAccount.Name = "CmbBankAccount"
        Me.CmbBankAccount.Size = New System.Drawing.Size(166, 22)
        Me.CmbBankAccount.TabIndex = 1
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
        'pnlSUPPLIER
        '
        Me.pnlSUPPLIER.Controls.Add(Me.CmbGroup)
        Me.pnlSUPPLIER.Controls.Add(Me.Label2)
        Me.pnlSUPPLIER.Controls.Add(Me.Label5)
        Me.pnlSUPPLIER.Controls.Add(Me.TxtDateTo)
        Me.pnlSUPPLIER.Controls.Add(Me.Label1)
        Me.pnlSUPPLIER.Controls.Add(Me.TxtDateFrom)
        Me.pnlSUPPLIER.Controls.Add(Me.CmbSupplier)
        Me.pnlSUPPLIER.Controls.Add(Me.Label12)
        Me.pnlSUPPLIER.Location = New System.Drawing.Point(0, 0)
        Me.pnlSUPPLIER.Name = "pnlSUPPLIER"
        Me.pnlSUPPLIER.Size = New System.Drawing.Size(660, 64)
        Me.pnlSUPPLIER.TabIndex = 0
        Me.pnlSUPPLIER.Visible = False
        '
        'CmbGroup
        '
        Me.CmbGroup.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup.ColumnNum = 3
        Me.CmbGroup.ColumnWidth = "100;100;30"
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
        Me.CmbGroup.Location = New System.Drawing.Point(112, 34)
        Me.CmbGroup.ManagingFastMouseMoving = True
        Me.CmbGroup.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup.Name = "CmbGroup"
        Me.CmbGroup.Size = New System.Drawing.Size(158, 22)
        Me.CmbGroup.TabIndex = 7
        Me.CmbGroup.Visible = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "B. Group"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Visible = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(436, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(21, 21)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "&To"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo
        '
        Me.TxtDateTo.BackColor = System.Drawing.Color.White
        Me.TxtDateTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo.Location = New System.Drawing.Point(461, 10)
        Me.TxtDateTo.MaxLength = 50
        Me.TxtDateTo.Name = "TxtDateTo"
        Me.TxtDateTo.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(275, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 21)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Date &From"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom
        '
        Me.TxtDateFrom.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom.Location = New System.Drawing.Point(346, 10)
        Me.TxtDateFrom.MaxLength = 50
        Me.TxtDateFrom.Name = "TxtDateFrom"
        Me.TxtDateFrom.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom.TabIndex = 3
        '
        'CmbSupplier
        '
        Me.CmbSupplier.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbSupplier.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbSupplier.ColumnNum = 3
        Me.CmbSupplier.ColumnWidth = "140;140;40"
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
        Me.CmbSupplier.Location = New System.Drawing.Point(112, 9)
        Me.CmbSupplier.ManagingFastMouseMoving = True
        Me.CmbSupplier.ManagingFastMouseMovingInterval = 30
        Me.CmbSupplier.Name = "CmbSupplier"
        Me.CmbSupplier.Size = New System.Drawing.Size(157, 22)
        Me.CmbSupplier.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(7, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(99, 21)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Supplier"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'daSUPPLIER_INFO
        '
        Me.daSUPPLIER_INFO.DeleteCommand = Me.SqlCommand5
        Me.daSUPPLIER_INFO.SelectCommand = Me.SqlCommand7
        Me.daSUPPLIER_INFO.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SUPPLIER_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nID", "nID"), New System.Data.Common.DataColumnMapping("sCONTACT_PERSON", "sCONTACT_PERSON"), New System.Data.Common.DataColumnMapping("sDESIGNATION", "sDESIGNATION"), New System.Data.Common.DataColumnMapping("sSUPPLIER_NAME", "sSUPPLIER_NAME"), New System.Data.Common.DataColumnMapping("sADDRESS", "sADDRESS"), New System.Data.Common.DataColumnMapping("sSUPPLIER_PH", "sSUPPLIER_PH"), New System.Data.Common.DataColumnMapping("sPERSON_PH", "sPERSON_PH"), New System.Data.Common.DataColumnMapping("sCELL_NO", "sCELL_NO"), New System.Data.Common.DataColumnMapping("sFAX_NO", "sFAX_NO"), New System.Data.Common.DataColumnMapping("sE_MAIL", "sE_MAIL"), New System.Data.Common.DataColumnMapping("sWEB_ADD", "sWEB_ADD"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("nOPEN_BAL", "nOPEN_BAL")})})
        Me.daSUPPLIER_INFO.UpdateCommand = Me.SqlCommand8
        '
        'SqlCommand5
        '
        Me.SqlCommand5.CommandText = resources.GetString("SqlCommand5.CommandText")
        Me.SqlCommand5.Connection = Me.SqlConnection1
        Me.SqlCommand5.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT_PERSON", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sDESIGNATION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sDESIGNATION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sSUPPLIER_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sPERSON_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sPERSON_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCELL_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCELL_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sFAX_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sFAX_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sE_MAIL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sE_MAIL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sWEB_ADD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sWEB_ADD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nOPEN_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nOPEN_BAL", System.Data.DataRowVersion.Original, Nothing)})
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
        Me.SqlCommand8.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, "sCONTACT_PERSON"), New System.Data.SqlClient.SqlParameter("@sDESIGNATION", System.Data.SqlDbType.VarChar, 0, "sDESIGNATION"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_NAME"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.VarChar, 0, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_PH"), New System.Data.SqlClient.SqlParameter("@sPERSON_PH", System.Data.SqlDbType.VarChar, 0, "sPERSON_PH"), New System.Data.SqlClient.SqlParameter("@sCELL_NO", System.Data.SqlDbType.VarChar, 0, "sCELL_NO"), New System.Data.SqlClient.SqlParameter("@sFAX_NO", System.Data.SqlDbType.VarChar, 0, "sFAX_NO"), New System.Data.SqlClient.SqlParameter("@sE_MAIL", System.Data.SqlDbType.VarChar, 0, "sE_MAIL"), New System.Data.SqlClient.SqlParameter("@sWEB_ADD", System.Data.SqlDbType.VarChar, 0, "sWEB_ADD"), New System.Data.SqlClient.SqlParameter("@nOPEN_BAL", System.Data.SqlDbType.Money, 0, "nOPEN_BAL"), New System.Data.SqlClient.SqlParameter("@Original_nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT_PERSON", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sDESIGNATION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sDESIGNATION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sSUPPLIER_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sPERSON_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sPERSON_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCELL_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCELL_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sFAX_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sFAX_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sE_MAIL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sE_MAIL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sWEB_ADD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sWEB_ADD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nOPEN_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nOPEN_BAL", System.Data.DataRowVersion.Original, Nothing)})
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
        'CRV1
        '
        Me.CRV1.ActiveViewIndex = -1
        Me.CRV1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CRV1.AutoScroll = True
        Me.CRV1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CRV1.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.CRV1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRV1.CausesValidation = False
        Me.CRV1.DisplayGroupTree = False
        Me.CRV1.Location = New System.Drawing.Point(0, 70)
        Me.CRV1.Name = "CRV1"
        Me.CRV1.SelectionFormula = ""
        Me.CRV1.Size = New System.Drawing.Size(776, 449)
        Me.CRV1.TabIndex = 1
        Me.CRV1.TabStop = False
        Me.CRV1.ViewTimeSelectionFormula = ""
        '
        'pnlCLIENT
        '
        Me.pnlCLIENT.Controls.Add(Me.CmbGroup1)
        Me.pnlCLIENT.Controls.Add(Me.Label3)
        Me.pnlCLIENT.Controls.Add(Me.Label4)
        Me.pnlCLIENT.Controls.Add(Me.TxtDateTo1)
        Me.pnlCLIENT.Controls.Add(Me.Label6)
        Me.pnlCLIENT.Controls.Add(Me.TxtDateFrom1)
        Me.pnlCLIENT.Controls.Add(Me.CmbClient)
        Me.pnlCLIENT.Controls.Add(Me.Label7)
        Me.pnlCLIENT.Location = New System.Drawing.Point(0, 0)
        Me.pnlCLIENT.Name = "pnlCLIENT"
        Me.pnlCLIENT.Size = New System.Drawing.Size(660, 64)
        Me.pnlCLIENT.TabIndex = 0
        Me.pnlCLIENT.Visible = False
        '
        'CmbGroup1
        '
        Me.CmbGroup1.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup1.ColumnNum = 3
        Me.CmbGroup1.ColumnWidth = "100;100;30"
        Me.CmbGroup1.DisplayMember = "Text"
        Me.CmbGroup1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbGroup1.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbGroup1.DropDownForeColor = System.Drawing.Color.White
        Me.CmbGroup1.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbGroup1.DropDownWidth = 340
        Me.CmbGroup1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbGroup1.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbGroup1.GridLineHorizontal = False
        Me.CmbGroup1.GridLineVertical = True
        Me.CmbGroup1.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbGroup1.Location = New System.Drawing.Point(112, 34)
        Me.CmbGroup1.ManagingFastMouseMoving = True
        Me.CmbGroup1.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup1.Name = "CmbGroup1"
        Me.CmbGroup1.Size = New System.Drawing.Size(158, 22)
        Me.CmbGroup1.TabIndex = 7
        Me.CmbGroup1.Visible = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 23)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "B. Group"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(436, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(21, 21)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "&To"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo1
        '
        Me.TxtDateTo1.BackColor = System.Drawing.Color.White
        Me.TxtDateTo1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo1.Location = New System.Drawing.Point(461, 10)
        Me.TxtDateTo1.MaxLength = 50
        Me.TxtDateTo1.Name = "TxtDateTo1"
        Me.TxtDateTo1.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo1.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(275, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 21)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Date &From"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom1
        '
        Me.TxtDateFrom1.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom1.Location = New System.Drawing.Point(346, 10)
        Me.TxtDateFrom1.MaxLength = 50
        Me.TxtDateFrom1.Name = "TxtDateFrom1"
        Me.TxtDateFrom1.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom1.TabIndex = 3
        '
        'CmbClient
        '
        Me.CmbClient.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbClient.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbClient.ColumnNum = 3
        Me.CmbClient.ColumnWidth = "140;140;40"
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
        Me.CmbClient.Location = New System.Drawing.Point(112, 9)
        Me.CmbClient.ManagingFastMouseMoving = True
        Me.CmbClient.ManagingFastMouseMovingInterval = 30
        Me.CmbClient.Name = "CmbClient"
        Me.CmbClient.Size = New System.Drawing.Size(157, 22)
        Me.CmbClient.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 21)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Client"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'daCLIENT_INFO
        '
        Me.daCLIENT_INFO.SelectCommand = Me.SqlSelectCommand4
        Me.daCLIENT_INFO.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_CLIENT_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("SHOP_ADD", "SHOP_ADD"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("HOME_ADD", "HOME_ADD"), New System.Data.Common.DataColumnMapping("SHOP_PH", "SHOP_PH"), New System.Data.Common.DataColumnMapping("HOME_PH", "HOME_PH"), New System.Data.Common.DataColumnMapping("CELL_NO", "CELL_NO"), New System.Data.Common.DataColumnMapping("FAX_NO", "FAX_NO"), New System.Data.Common.DataColumnMapping("E_MAIL", "E_MAIL"), New System.Data.Common.DataColumnMapping("WEB_SITE", "WEB_SITE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("CLIENT_CAT", "CLIENT_CAT"), New System.Data.Common.DataColumnMapping("CLIENT_GD", "CLIENT_GD"), New System.Data.Common.DataColumnMapping("CLIENT_TYPE", "CLIENT_TYPE"), New System.Data.Common.DataColumnMapping("CREDIT_LIM", "CREDIT_LIM"), New System.Data.Common.DataColumnMapping("GST_NO", "GST_NO"), New System.Data.Common.DataColumnMapping("OPEN_BAL", "OPEN_BAL"), New System.Data.Common.DataColumnMapping("VISIT_TYPE", "VISIT_TYPE"), New System.Data.Common.DataColumnMapping("NO_VISIT", "NO_VISIT"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE")})})
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = resources.GetString("SqlSelectCommand4.CommandText")
        Me.SqlSelectCommand4.Connection = Me.SqlConnection1
        '
        'PnlCompany_Group
        '
        Me.PnlCompany_Group.Controls.Add(Me.Label10)
        Me.PnlCompany_Group.Controls.Add(Me.CmbCompany)
        Me.PnlCompany_Group.Controls.Add(Me.CmbGroup2)
        Me.PnlCompany_Group.Controls.Add(Me.Label8)
        Me.PnlCompany_Group.Location = New System.Drawing.Point(0, 0)
        Me.PnlCompany_Group.Name = "PnlCompany_Group"
        Me.PnlCompany_Group.Size = New System.Drawing.Size(660, 64)
        Me.PnlCompany_Group.TabIndex = 0
        Me.PnlCompany_Group.Visible = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Label10.Location = New System.Drawing.Point(258, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 23)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Company"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbCompany
        '
        Me.CmbCompany.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbCompany.ColumnNum = 2
        Me.CmbCompany.ColumnWidth = "140;40"
        Me.CmbCompany.DisplayMember = "Text"
        Me.CmbCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbCompany.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbCompany.DropDownForeColor = System.Drawing.Color.White
        Me.CmbCompany.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbCompany.DropDownWidth = 200
        Me.CmbCompany.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbCompany.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbCompany.GridLineHorizontal = False
        Me.CmbCompany.GridLineVertical = True
        Me.CmbCompany.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbCompany.Location = New System.Drawing.Point(346, 20)
        Me.CmbCompany.ManagingFastMouseMoving = True
        Me.CmbCompany.ManagingFastMouseMovingInterval = 30
        Me.CmbCompany.Name = "CmbCompany"
        Me.CmbCompany.Size = New System.Drawing.Size(199, 22)
        Me.CmbCompany.TabIndex = 3
        '
        'CmbGroup2
        '
        Me.CmbGroup2.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup2.ColumnNum = 3
        Me.CmbGroup2.ColumnWidth = "100;100;30"
        Me.CmbGroup2.DisplayMember = "Text"
        Me.CmbGroup2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbGroup2.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbGroup2.DropDownForeColor = System.Drawing.Color.White
        Me.CmbGroup2.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbGroup2.DropDownWidth = 340
        Me.CmbGroup2.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbGroup2.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbGroup2.GridLineHorizontal = False
        Me.CmbGroup2.GridLineVertical = True
        Me.CmbGroup2.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbGroup2.Location = New System.Drawing.Point(91, 20)
        Me.CmbGroup2.ManagingFastMouseMoving = True
        Me.CmbGroup2.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup2.Name = "CmbGroup2"
        Me.CmbGroup2.Size = New System.Drawing.Size(158, 22)
        Me.CmbGroup2.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(73, 23)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "B. Group"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'daLUP_VENDOR
        '
        Me.daLUP_VENDOR.DeleteCommand = Me.SqlCommand1
        Me.daLUP_VENDOR.InsertCommand = Me.SqlCommand2
        Me.daLUP_VENDOR.SelectCommand = Me.SqlCommand4
        Me.daLUP_VENDOR.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_VENDOR", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_VENDOR.UpdateCommand = Me.SqlCommand6
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "DELETE FROM [LUP_VENDOR] WHERE (([nCODE] = @Original_nCODE) AND ([sDESC] = @Origi" & _
            "nal_sDESC))"
        Me.SqlCommand1.Connection = Me.SqlConnection1
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand2
        '
        Me.SqlCommand2.CommandText = "INSERT INTO [LUP_VENDOR] ([sDESC]) VALUES (@sDESC);" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP" & _
            "_VENDOR WHERE (nCODE = SCOPE_IDENTITY())"
        Me.SqlCommand2.Connection = Me.SqlConnection1
        Me.SqlCommand2.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC")})
        '
        'SqlCommand4
        '
        Me.SqlCommand4.CommandText = "SELECT     nCODE, sDESC" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         LUP_VENDOR"
        Me.SqlCommand4.Connection = Me.SqlConnection1
        '
        'SqlCommand6
        '
        Me.SqlCommand6.CommandText = "UPDATE [LUP_VENDOR] SET [sDESC] = @sDESC WHERE (([nCODE] = @Original_nCODE) AND (" & _
            "[sDESC] = @Original_sDESC));" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP_VENDOR WHERE (nCODE =" & _
            " @nCODE)"
        Me.SqlCommand6.Connection = Me.SqlConnection1
        Me.SqlCommand6.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'PnlBankAcc_Date
        '
        Me.PnlBankAcc_Date.Controls.Add(Me.Label13)
        Me.PnlBankAcc_Date.Controls.Add(Me.TxtDateTo2)
        Me.PnlBankAcc_Date.Controls.Add(Me.Label14)
        Me.PnlBankAcc_Date.Controls.Add(Me.TxtDateFrom2)
        Me.PnlBankAcc_Date.Controls.Add(Me.CmbBankAccount1)
        Me.PnlBankAcc_Date.Controls.Add(Me.Label15)
        Me.PnlBankAcc_Date.Location = New System.Drawing.Point(0, 0)
        Me.PnlBankAcc_Date.Name = "PnlBankAcc_Date"
        Me.PnlBankAcc_Date.Size = New System.Drawing.Size(660, 64)
        Me.PnlBankAcc_Date.TabIndex = 0
        Me.PnlBankAcc_Date.Visible = False
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(436, 22)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(21, 21)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "&To"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo2
        '
        Me.TxtDateTo2.BackColor = System.Drawing.Color.White
        Me.TxtDateTo2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo2.Location = New System.Drawing.Point(461, 22)
        Me.TxtDateTo2.MaxLength = 50
        Me.TxtDateTo2.Name = "TxtDateTo2"
        Me.TxtDateTo2.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo2.TabIndex = 5
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(275, 22)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(68, 21)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Date &From"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom2
        '
        Me.TxtDateFrom2.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom2.Location = New System.Drawing.Point(346, 22)
        Me.TxtDateFrom2.MaxLength = 50
        Me.TxtDateFrom2.Name = "TxtDateFrom2"
        Me.TxtDateFrom2.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom2.TabIndex = 3
        '
        'CmbBankAccount1
        '
        Me.CmbBankAccount1.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbBankAccount1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbBankAccount1.ColumnNum = 2
        Me.CmbBankAccount1.ColumnWidth = "140;100"
        Me.CmbBankAccount1.DisplayMember = "Text"
        Me.CmbBankAccount1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbBankAccount1.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbBankAccount1.DropDownForeColor = System.Drawing.Color.White
        Me.CmbBankAccount1.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbBankAccount1.DropDownWidth = 340
        Me.CmbBankAccount1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbBankAccount1.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbBankAccount1.GridLineHorizontal = False
        Me.CmbBankAccount1.GridLineVertical = True
        Me.CmbBankAccount1.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbBankAccount1.Location = New System.Drawing.Point(112, 21)
        Me.CmbBankAccount1.ManagingFastMouseMoving = True
        Me.CmbBankAccount1.ManagingFastMouseMovingInterval = 30
        Me.CmbBankAccount1.Name = "CmbBankAccount1"
        Me.CmbBankAccount1.Size = New System.Drawing.Size(157, 22)
        Me.CmbBankAccount1.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(7, 21)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(99, 21)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Account No."
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PnlDate
        '
        Me.PnlDate.Controls.Add(Me.Label11)
        Me.PnlDate.Controls.Add(Me.TxtDateTo3)
        Me.PnlDate.Controls.Add(Me.Label16)
        Me.PnlDate.Controls.Add(Me.TxtDateFrom3)
        Me.PnlDate.Location = New System.Drawing.Point(0, 0)
        Me.PnlDate.Name = "PnlDate"
        Me.PnlDate.Size = New System.Drawing.Size(660, 64)
        Me.PnlDate.TabIndex = 0
        Me.PnlDate.Visible = False
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(178, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(21, 21)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "&To"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo3
        '
        Me.TxtDateTo3.BackColor = System.Drawing.Color.White
        Me.TxtDateTo3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo3.Location = New System.Drawing.Point(203, 23)
        Me.TxtDateTo3.MaxLength = 50
        Me.TxtDateTo3.Name = "TxtDateTo3"
        Me.TxtDateTo3.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo3.TabIndex = 3
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(17, 23)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(68, 21)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Date &From"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom3
        '
        Me.TxtDateFrom3.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom3.Location = New System.Drawing.Point(88, 23)
        Me.TxtDateFrom3.MaxLength = 50
        Me.TxtDateFrom3.Name = "TxtDateFrom3"
        Me.TxtDateFrom3.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom3.TabIndex = 1
        '
        'PnlArea
        '
        Me.PnlArea.Controls.Add(Me.Label17)
        Me.PnlArea.Controls.Add(Me.CmbArea)
        Me.PnlArea.Location = New System.Drawing.Point(0, 0)
        Me.PnlArea.Name = "PnlArea"
        Me.PnlArea.Size = New System.Drawing.Size(660, 64)
        Me.PnlArea.TabIndex = 4
        Me.PnlArea.Visible = False
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(30, 21)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(55, 23)
        Me.Label17.TabIndex = 17
        Me.Label17.Text = "Area"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbArea
        '
        Me.CmbArea.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbArea.ColumnNum = 2
        Me.CmbArea.ColumnWidth = "140;40"
        Me.CmbArea.DisplayMember = "Text"
        Me.CmbArea.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbArea.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbArea.DropDownForeColor = System.Drawing.Color.White
        Me.CmbArea.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbArea.DropDownWidth = 340
        Me.CmbArea.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbArea.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbArea.GridLineHorizontal = False
        Me.CmbArea.GridLineVertical = True
        Me.CmbArea.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbArea.Location = New System.Drawing.Point(91, 21)
        Me.CmbArea.ManagingFastMouseMoving = True
        Me.CmbArea.ManagingFastMouseMovingInterval = 30
        Me.CmbArea.Name = "CmbArea"
        Me.CmbArea.Size = New System.Drawing.Size(187, 22)
        Me.CmbArea.TabIndex = 16
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
        'PnlArea_Date
        '
        Me.PnlArea_Date.Controls.Add(Me.Label19)
        Me.PnlArea_Date.Controls.Add(Me.TxtDateTo4)
        Me.PnlArea_Date.Controls.Add(Me.Label20)
        Me.PnlArea_Date.Controls.Add(Me.TxtDateFrom4)
        Me.PnlArea_Date.Controls.Add(Me.Label18)
        Me.PnlArea_Date.Controls.Add(Me.CmbArea1)
        Me.PnlArea_Date.Location = New System.Drawing.Point(0, 0)
        Me.PnlArea_Date.Name = "PnlArea_Date"
        Me.PnlArea_Date.Size = New System.Drawing.Size(660, 64)
        Me.PnlArea_Date.TabIndex = 0
        Me.PnlArea_Date.Visible = False
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(445, 22)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(21, 21)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "&To"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo4
        '
        Me.TxtDateTo4.BackColor = System.Drawing.Color.White
        Me.TxtDateTo4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo4.Location = New System.Drawing.Point(470, 22)
        Me.TxtDateTo4.MaxLength = 50
        Me.TxtDateTo4.Name = "TxtDateTo4"
        Me.TxtDateTo4.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo4.TabIndex = 5
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(284, 22)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(68, 21)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Date &From"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom4
        '
        Me.TxtDateFrom4.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom4.Location = New System.Drawing.Point(355, 22)
        Me.TxtDateFrom4.MaxLength = 50
        Me.TxtDateFrom4.Name = "TxtDateFrom4"
        Me.TxtDateFrom4.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom4.TabIndex = 3
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(30, 21)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(55, 23)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Area"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbArea1
        '
        Me.CmbArea1.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbArea1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbArea1.ColumnNum = 2
        Me.CmbArea1.ColumnWidth = "140;40"
        Me.CmbArea1.DisplayMember = "Text"
        Me.CmbArea1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbArea1.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbArea1.DropDownForeColor = System.Drawing.Color.White
        Me.CmbArea1.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbArea1.DropDownWidth = 340
        Me.CmbArea1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbArea1.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbArea1.GridLineHorizontal = False
        Me.CmbArea1.GridLineVertical = True
        Me.CmbArea1.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbArea1.Location = New System.Drawing.Point(91, 21)
        Me.CmbArea1.ManagingFastMouseMoving = True
        Me.CmbArea1.ManagingFastMouseMovingInterval = 30
        Me.CmbArea1.Name = "CmbArea1"
        Me.CmbArea1.Size = New System.Drawing.Size(187, 22)
        Me.CmbArea1.TabIndex = 1
        '
        'PnlInvoice
        '
        Me.PnlInvoice.Controls.Add(Me.Label21)
        Me.PnlInvoice.Controls.Add(Me.TxtInvoiceTo)
        Me.PnlInvoice.Controls.Add(Me.Label23)
        Me.PnlInvoice.Controls.Add(Me.Label22)
        Me.PnlInvoice.Controls.Add(Me.TxtInvoiceFrom)
        Me.PnlInvoice.Location = New System.Drawing.Point(0, 0)
        Me.PnlInvoice.Name = "PnlInvoice"
        Me.PnlInvoice.Size = New System.Drawing.Size(660, 64)
        Me.PnlInvoice.TabIndex = 0
        Me.PnlInvoice.Visible = False
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(269, 22)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(21, 21)
        Me.Label21.TabIndex = 4
        Me.Label21.Text = "&To"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtInvoiceTo
        '
        Me.TxtInvoiceTo.BackColor = System.Drawing.Color.White
        Me.TxtInvoiceTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtInvoiceTo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInvoiceTo.Location = New System.Drawing.Point(294, 22)
        Me.TxtInvoiceTo.MaxLength = 50
        Me.TxtInvoiceTo.Name = "TxtInvoiceTo"
        Me.TxtInvoiceTo.Size = New System.Drawing.Size(86, 21)
        Me.TxtInvoiceTo.TabIndex = 5
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(15, 22)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(101, 21)
        Me.Label23.TabIndex = 2
        Me.Label23.Text = "Invoice No"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(123, 22)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(53, 21)
        Me.Label22.TabIndex = 2
        Me.Label22.Text = "&From"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtInvoiceFrom
        '
        Me.TxtInvoiceFrom.BackColor = System.Drawing.Color.White
        Me.TxtInvoiceFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtInvoiceFrom.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInvoiceFrom.Location = New System.Drawing.Point(179, 22)
        Me.TxtInvoiceFrom.MaxLength = 50
        Me.TxtInvoiceFrom.Name = "TxtInvoiceFrom"
        Me.TxtInvoiceFrom.Size = New System.Drawing.Size(86, 21)
        Me.TxtInvoiceFrom.TabIndex = 3
        '
        'pnlITEM
        '
        Me.pnlITEM.Controls.Add(Me.Label25)
        Me.pnlITEM.Controls.Add(Me.TxtDateTo5)
        Me.pnlITEM.Controls.Add(Me.Label26)
        Me.pnlITEM.Controls.Add(Me.TxtDateFrom5)
        Me.pnlITEM.Controls.Add(Me.cmbITEM)
        Me.pnlITEM.Controls.Add(Me.Label27)
        Me.pnlITEM.Location = New System.Drawing.Point(0, 0)
        Me.pnlITEM.Name = "pnlITEM"
        Me.pnlITEM.Size = New System.Drawing.Size(660, 64)
        Me.pnlITEM.TabIndex = 0
        Me.pnlITEM.Visible = False
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(436, 22)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(21, 21)
        Me.Label25.TabIndex = 4
        Me.Label25.Text = "&To"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo5
        '
        Me.TxtDateTo5.BackColor = System.Drawing.Color.White
        Me.TxtDateTo5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo5.Location = New System.Drawing.Point(461, 22)
        Me.TxtDateTo5.MaxLength = 50
        Me.TxtDateTo5.Name = "TxtDateTo5"
        Me.TxtDateTo5.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo5.TabIndex = 5
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(275, 22)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(68, 21)
        Me.Label26.TabIndex = 2
        Me.Label26.Text = "Date &From"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom5
        '
        Me.TxtDateFrom5.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom5.Location = New System.Drawing.Point(346, 22)
        Me.TxtDateFrom5.MaxLength = 50
        Me.TxtDateFrom5.Name = "TxtDateFrom5"
        Me.TxtDateFrom5.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom5.TabIndex = 3
        '
        'cmbITEM
        '
        Me.cmbITEM.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.cmbITEM.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbITEM.ColumnNum = 2
        Me.cmbITEM.ColumnWidth = "140;40"
        Me.cmbITEM.DisplayMember = "Text"
        Me.cmbITEM.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbITEM.DropDownBackColor = System.Drawing.Color.Blue
        Me.cmbITEM.DropDownForeColor = System.Drawing.Color.White
        Me.cmbITEM.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.cmbITEM.DropDownWidth = 340
        Me.cmbITEM.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.cmbITEM.GridLineColor = System.Drawing.Color.RosyBrown
        Me.cmbITEM.GridLineHorizontal = False
        Me.cmbITEM.GridLineVertical = True
        Me.cmbITEM.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.cmbITEM.Location = New System.Drawing.Point(112, 21)
        Me.cmbITEM.ManagingFastMouseMoving = True
        Me.cmbITEM.ManagingFastMouseMovingInterval = 30
        Me.cmbITEM.Name = "cmbITEM"
        Me.cmbITEM.Size = New System.Drawing.Size(157, 22)
        Me.cmbITEM.TabIndex = 1
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(7, 22)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(99, 21)
        Me.Label27.TabIndex = 0
        Me.Label27.Text = "Item Name"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'daLUP_ITEM
        '
        Me.daLUP_ITEM.SelectCommand = Me.SqlSelectCommand2
        Me.daLUP_ITEM.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_ITEM", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sITEM_NAME", "sITEM_NAME"), New System.Data.Common.DataColumnMapping("sNICK", "sNICK"), New System.Data.Common.DataColumnMapping("nPPP", "nPPP"), New System.Data.Common.DataColumnMapping("sPACK_DESC", "sPACK_DESC"), New System.Data.Common.DataColumnMapping("sPIECE_DESC", "sPIECE_DESC"), New System.Data.Common.DataColumnMapping("UNIT_COST", "UNIT_COST"), New System.Data.Common.DataColumnMapping("UNIT_RATE", "UNIT_RATE"), New System.Data.Common.DataColumnMapping("UNIT_RETAIL", "UNIT_RETAIL"), New System.Data.Common.DataColumnMapping("nMIN_STOCK", "nMIN_STOCK"), New System.Data.Common.DataColumnMapping("nMAX_STOCK", "nMAX_STOCK"), New System.Data.Common.DataColumnMapping("nSALE_TAX", "nSALE_TAX"), New System.Data.Common.DataColumnMapping("VENDOR", "VENDOR"), New System.Data.Common.DataColumnMapping("nBONUS_QTY", "nBONUS_QTY"), New System.Data.Common.DataColumnMapping("nBONUS_ON_PCS", "nBONUS_ON_PCS"), New System.Data.Common.DataColumnMapping("CLAIMABLE", "CLAIMABLE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("nOPEN_STOCK", "nOPEN_STOCK"), New System.Data.Common.DataColumnMapping("OPEN_UNIT_VALUE", "OPEN_UNIT_VALUE"), New System.Data.Common.DataColumnMapping("ITEM_CAT", "ITEM_CAT")})})
        '
        'SqlSelectCommand2
        '
        Me.SqlSelectCommand2.CommandText = resources.GetString("SqlSelectCommand2.CommandText")
        Me.SqlSelectCommand2.Connection = Me.SqlConnection1
        '
        'pnlCOMPANY
        '
        Me.pnlCOMPANY.Controls.Add(Me.CmbCompany1)
        Me.pnlCOMPANY.Controls.Add(Me.Label29)
        Me.pnlCOMPANY.Location = New System.Drawing.Point(0, 0)
        Me.pnlCOMPANY.Name = "pnlCOMPANY"
        Me.pnlCOMPANY.Size = New System.Drawing.Size(660, 64)
        Me.pnlCOMPANY.TabIndex = 0
        Me.pnlCOMPANY.Visible = False
        '
        'CmbCompany1
        '
        Me.CmbCompany1.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbCompany1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbCompany1.ColumnNum = 2
        Me.CmbCompany1.ColumnWidth = "140;40"
        Me.CmbCompany1.DisplayMember = "Text"
        Me.CmbCompany1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbCompany1.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbCompany1.DropDownForeColor = System.Drawing.Color.White
        Me.CmbCompany1.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbCompany1.DropDownWidth = 340
        Me.CmbCompany1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbCompany1.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbCompany1.GridLineHorizontal = False
        Me.CmbCompany1.GridLineVertical = True
        Me.CmbCompany1.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbCompany1.Location = New System.Drawing.Point(124, 21)
        Me.CmbCompany1.ManagingFastMouseMoving = True
        Me.CmbCompany1.ManagingFastMouseMovingInterval = 30
        Me.CmbCompany1.Name = "CmbCompany1"
        Me.CmbCompany1.Size = New System.Drawing.Size(232, 22)
        Me.CmbCompany1.TabIndex = 1
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(7, 22)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(111, 21)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Company Name"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlSALEMAN
        '
        Me.pnlSALEMAN.Controls.Add(Me.CmbS_Man)
        Me.pnlSALEMAN.Controls.Add(Me.Label24)
        Me.pnlSALEMAN.Location = New System.Drawing.Point(0, 0)
        Me.pnlSALEMAN.Name = "pnlSALEMAN"
        Me.pnlSALEMAN.Size = New System.Drawing.Size(660, 64)
        Me.pnlSALEMAN.TabIndex = 0
        Me.pnlSALEMAN.Visible = False
        '
        'CmbS_Man
        '
        Me.CmbS_Man.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbS_Man.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbS_Man.ColumnNum = 3
        Me.CmbS_Man.ColumnWidth = "140;100;40"
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
        Me.CmbS_Man.Location = New System.Drawing.Point(124, 21)
        Me.CmbS_Man.ManagingFastMouseMoving = True
        Me.CmbS_Man.ManagingFastMouseMovingInterval = 30
        Me.CmbS_Man.Name = "CmbS_Man"
        Me.CmbS_Man.Size = New System.Drawing.Size(232, 22)
        Me.CmbS_Man.TabIndex = 1
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(7, 22)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(109, 21)
        Me.Label24.TabIndex = 0
        Me.Label24.Text = "Salesman"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'daLUP_EMPLOYEE
        '
        Me.daLUP_EMPLOYEE.DeleteCommand = Me.SqlCommand9
        Me.daLUP_EMPLOYEE.SelectCommand = Me.SqlCommand10
        Me.daLUP_EMPLOYEE.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_EMPLOYEE_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("FATHER_NAME", "FATHER_NAME"), New System.Data.Common.DataColumnMapping("NIC", "NIC"), New System.Data.Common.DataColumnMapping("HOME_PH", "HOME_PH"), New System.Data.Common.DataColumnMapping("CELL", "CELL"), New System.Data.Common.DataColumnMapping("PRE_ADD", "PRE_ADD"), New System.Data.Common.DataColumnMapping("PER_ADD", "PER_ADD"), New System.Data.Common.DataColumnMapping("DESIGNATION", "DESIGNATION"), New System.Data.Common.DataColumnMapping("APP_DATE", "APP_DATE"), New System.Data.Common.DataColumnMapping("PAY", "PAY"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("LEAVE_DATE", "LEAVE_DATE"), New System.Data.Common.DataColumnMapping("EMAIL_ADD", "EMAIL_ADD"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("BANK_ADD", "BANK_ADD")})})
        Me.daLUP_EMPLOYEE.UpdateCommand = Me.SqlCommand11
        '
        'SqlCommand9
        '
        Me.SqlCommand9.CommandText = "DELETE FROM LUP_CLIENT_GD WHERE (nCODE = @Original_nCODE) AND (nMAX_LIM = @Origin" & _
            "al_nMAX_LIM) AND (nMIN_LIM = @Original_nMIN_LIM) AND (sDESC = @Original_sDESC)"
        Me.SqlCommand9.Connection = Me.SqlConnection1
        Me.SqlCommand9.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand10
        '
        Me.SqlCommand10.CommandText = resources.GetString("SqlCommand10.CommandText")
        Me.SqlCommand10.Connection = Me.SqlConnection1
        '
        'SqlCommand11
        '
        Me.SqlCommand11.CommandText = resources.GetString("SqlCommand11.CommandText")
        Me.SqlCommand11.Connection = Me.SqlConnection1
        Me.SqlCommand11.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'pnlSALEMAN_DATE
        '
        Me.pnlSALEMAN_DATE.Controls.Add(Me.Label30)
        Me.pnlSALEMAN_DATE.Controls.Add(Me.TxtDateTo6)
        Me.pnlSALEMAN_DATE.Controls.Add(Me.Label31)
        Me.pnlSALEMAN_DATE.Controls.Add(Me.TxtDateFrom6)
        Me.pnlSALEMAN_DATE.Controls.Add(Me.CmbS_Man1)
        Me.pnlSALEMAN_DATE.Controls.Add(Me.Label28)
        Me.pnlSALEMAN_DATE.Location = New System.Drawing.Point(0, 0)
        Me.pnlSALEMAN_DATE.Name = "pnlSALEMAN_DATE"
        Me.pnlSALEMAN_DATE.Size = New System.Drawing.Size(660, 64)
        Me.pnlSALEMAN_DATE.TabIndex = 0
        Me.pnlSALEMAN_DATE.Visible = False
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(494, 22)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(21, 21)
        Me.Label30.TabIndex = 4
        Me.Label30.Text = "&To"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo6
        '
        Me.TxtDateTo6.BackColor = System.Drawing.Color.White
        Me.TxtDateTo6.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo6.Location = New System.Drawing.Point(519, 22)
        Me.TxtDateTo6.MaxLength = 50
        Me.TxtDateTo6.Name = "TxtDateTo6"
        Me.TxtDateTo6.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo6.TabIndex = 5
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(333, 22)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(68, 21)
        Me.Label31.TabIndex = 2
        Me.Label31.Text = "Date &From"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom6
        '
        Me.TxtDateFrom6.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom6.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom6.Location = New System.Drawing.Point(404, 22)
        Me.TxtDateFrom6.MaxLength = 50
        Me.TxtDateFrom6.Name = "TxtDateFrom6"
        Me.TxtDateFrom6.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom6.TabIndex = 3
        '
        'CmbS_Man1
        '
        Me.CmbS_Man1.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbS_Man1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbS_Man1.ColumnNum = 3
        Me.CmbS_Man1.ColumnWidth = "140;100;40"
        Me.CmbS_Man1.DisplayMember = "Text"
        Me.CmbS_Man1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbS_Man1.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbS_Man1.DropDownForeColor = System.Drawing.Color.White
        Me.CmbS_Man1.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbS_Man1.DropDownWidth = 340
        Me.CmbS_Man1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbS_Man1.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbS_Man1.GridLineHorizontal = False
        Me.CmbS_Man1.GridLineVertical = True
        Me.CmbS_Man1.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbS_Man1.Location = New System.Drawing.Point(95, 21)
        Me.CmbS_Man1.ManagingFastMouseMoving = True
        Me.CmbS_Man1.ManagingFastMouseMovingInterval = 30
        Me.CmbS_Man1.Name = "CmbS_Man1"
        Me.CmbS_Man1.Size = New System.Drawing.Size(232, 22)
        Me.CmbS_Man1.TabIndex = 1
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(7, 22)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(82, 21)
        Me.Label28.TabIndex = 0
        Me.Label28.Text = "Salesman"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'daRptCLIENT_LEDGER
        '
        Me.daRptCLIENT_LEDGER.SelectCommand = Me.SqlCommand13
        Me.daRptCLIENT_LEDGER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "rptCLIENT_LEDGER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID"), New System.Data.Common.DataColumnMapping("GP_NAME", "GP_NAME"), New System.Data.Common.DataColumnMapping("TR_TYPE", "TR_TYPE"), New System.Data.Common.DataColumnMapping("TR_DESC", "TR_DESC"), New System.Data.Common.DataColumnMapping("CLIENT_ID", "CLIENT_ID"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("SHOP_ADD", "SHOP_ADD"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("SHOP_PH", "SHOP_PH"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("TR_ID", "TR_ID"), New System.Data.Common.DataColumnMapping("Dr", "Dr"), New System.Data.Common.DataColumnMapping("Cr", "Cr"), New System.Data.Common.DataColumnMapping("BUSINESS_NAME", "BUSINESS_NAME"), New System.Data.Common.DataColumnMapping("PHONE", "PHONE"), New System.Data.Common.DataColumnMapping("CELL_NO", "CELL_NO"), New System.Data.Common.DataColumnMapping("FAX_NO", "FAX_NO"), New System.Data.Common.DataColumnMapping("ADDRESS", "ADDRESS")})})
        '
        'SqlCommand13
        '
        Me.SqlCommand13.CommandText = resources.GetString("SqlCommand13.CommandText")
        Me.SqlCommand13.Connection = Me.SqlConnection1
        '
        'DsLUP_BANK1
        '
        Me.DsLUP_BANK1.DataSetName = "dsLUP_BANK"
        Me.DsLUP_BANK1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_BANK1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsSUPPLIER_INFO1
        '
        Me.DsSUPPLIER_INFO1.DataSetName = "dsSUPPLIER_INFO"
        Me.DsSUPPLIER_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsCLIENT_INFO1
        '
        Me.DsCLIENT_INFO1.DataSetName = "dsCLIENT_INFO"
        Me.DsCLIENT_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_BUSINESS_GROUP1
        '
        Me.DsLUP_BUSINESS_GROUP1.DataSetName = "dsLUP_BUSINESS_GROUP"
        Me.DsLUP_BUSINESS_GROUP1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_VENDOR1
        '
        Me.DsLUP_VENDOR1.DataSetName = "dsLUP_VENDOR"
        Me.DsLUP_VENDOR1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_AREA1
        '
        Me.DsLUP_AREA1.DataSetName = "dsLUP_AREA"
        Me.DsLUP_AREA1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_AREA1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsV_LUP_ITEM1
        '
        Me.DsV_LUP_ITEM1.DataSetName = "dsV_LUP_ITEM"
        Me.DsV_LUP_ITEM1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_EMPLOYEE1
        '
        Me.DsLUP_EMPLOYEE1.DataSetName = "dsLUP_EMPLOYEE"
        Me.DsLUP_EMPLOYEE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsRptCLIENT_LEDGER1
        '
        Me.DsRptCLIENT_LEDGER1.DataSetName = "dsRptCLIENT_LEDGER"
        Me.DsRptCLIENT_LEDGER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daRptSUPPLIER_LEDGER
        '
        Me.daRptSUPPLIER_LEDGER.SelectCommand = Me.SqlCommand12
        Me.daRptSUPPLIER_LEDGER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "rptSUPPLIER_LEDGER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID"), New System.Data.Common.DataColumnMapping("TR_TYPE", "TR_TYPE"), New System.Data.Common.DataColumnMapping("TR_DESC", "TR_DESC"), New System.Data.Common.DataColumnMapping("SUP_ID", "SUP_ID"), New System.Data.Common.DataColumnMapping("PER_NAME", "PER_NAME"), New System.Data.Common.DataColumnMapping("SUP_NAME", "SUP_NAME"), New System.Data.Common.DataColumnMapping("SUP_ADD", "SUP_ADD"), New System.Data.Common.DataColumnMapping("SUP_PH", "SUP_PH"), New System.Data.Common.DataColumnMapping("PER_PH", "PER_PH"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("TR_ID", "TR_ID"), New System.Data.Common.DataColumnMapping("Dr", "Dr"), New System.Data.Common.DataColumnMapping("Cr", "Cr"), New System.Data.Common.DataColumnMapping("B_NAME", "B_NAME"), New System.Data.Common.DataColumnMapping("B_ADD", "B_ADD"), New System.Data.Common.DataColumnMapping("B_PH", "B_PH"), New System.Data.Common.DataColumnMapping("B_CELL", "B_CELL"), New System.Data.Common.DataColumnMapping("B_FAX", "B_FAX")})})
        '
        'SqlCommand12
        '
        Me.SqlCommand12.CommandText = resources.GetString("SqlCommand12.CommandText")
        Me.SqlCommand12.Connection = Me.SqlConnection1
        '
        'DsRptSUPPLIER_LEDGER1
        '
        Me.DsRptSUPPLIER_LEDGER1.DataSetName = "dsRptSUPPLIER_LEDGER"
        Me.DsRptSUPPLIER_LEDGER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daRptCASH_LEDGER
        '
        Me.daRptCASH_LEDGER.SelectCommand = Me.SqlCommand14
        Me.daRptCASH_LEDGER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "rptCASH_LEDGER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID"), New System.Data.Common.DataColumnMapping("GP_NAME", "GP_NAME"), New System.Data.Common.DataColumnMapping("TR_TYPE", "TR_TYPE"), New System.Data.Common.DataColumnMapping("TR_DESC", "TR_DESC"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("TR_ID", "TR_ID"), New System.Data.Common.DataColumnMapping("Dr", "Dr"), New System.Data.Common.DataColumnMapping("Cr", "Cr"), New System.Data.Common.DataColumnMapping("B_NAME", "B_NAME"), New System.Data.Common.DataColumnMapping("B_ADD", "B_ADD"), New System.Data.Common.DataColumnMapping("B_PH", "B_PH"), New System.Data.Common.DataColumnMapping("B_CELL", "B_CELL"), New System.Data.Common.DataColumnMapping("B_FAX", "B_FAX")})})
        '
        'SqlCommand14
        '
        Me.SqlCommand14.CommandText = "SELECT     GROUP_ID, GP_NAME, TR_TYPE, TR_DESC, dDATE, TR_ID, Dr, Cr, B_NAME, B_A" & _
            "DD, B_PH, B_CELL, B_FAX" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         rptCASH_LEDGER"
        Me.SqlCommand14.Connection = Me.SqlConnection1
        '
        'DsRptCASH_LEDGER1
        '
        Me.DsRptCASH_LEDGER1.DataSetName = "dsRptCASH_LEDGER"
        Me.DsRptCASH_LEDGER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daRptBANK_LEDGER
        '
        Me.daRptBANK_LEDGER.SelectCommand = Me.SqlCommand15
        Me.daRptBANK_LEDGER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "rptBANK_LEDGER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID"), New System.Data.Common.DataColumnMapping("GP_NAME", "GP_NAME"), New System.Data.Common.DataColumnMapping("TR_TYPE", "TR_TYPE"), New System.Data.Common.DataColumnMapping("TR_DESC", "TR_DESC"), New System.Data.Common.DataColumnMapping("BK_ACC", "BK_ACC"), New System.Data.Common.DataColumnMapping("BK_NAME", "BK_NAME"), New System.Data.Common.DataColumnMapping("BR_NAME", "BR_NAME"), New System.Data.Common.DataColumnMapping("BR_CODE", "BR_CODE"), New System.Data.Common.DataColumnMapping("BK_ADD", "BK_ADD"), New System.Data.Common.DataColumnMapping("BK_PH", "BK_PH"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("TR_ID", "TR_ID"), New System.Data.Common.DataColumnMapping("Dr", "Dr"), New System.Data.Common.DataColumnMapping("Cr", "Cr"), New System.Data.Common.DataColumnMapping("B_NAME", "B_NAME"), New System.Data.Common.DataColumnMapping("B_ADD", "B_ADD"), New System.Data.Common.DataColumnMapping("B_PH", "B_PH"), New System.Data.Common.DataColumnMapping("B_CELL", "B_CELL"), New System.Data.Common.DataColumnMapping("B_FAX", "B_FAX")})})
        '
        'SqlCommand15
        '
        Me.SqlCommand15.CommandText = resources.GetString("SqlCommand15.CommandText")
        Me.SqlCommand15.Connection = Me.SqlConnection1
        '
        'DsRptBANK_LEDGER1
        '
        Me.DsRptBANK_LEDGER1.DataSetName = "dsRptBANK_LEDGER"
        Me.DsRptBANK_LEDGER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'PnlSupplierDate
        '
        Me.PnlSupplierDate.Controls.Add(Me.Label35)
        Me.PnlSupplierDate.Controls.Add(Me.TxtDateTo8)
        Me.PnlSupplierDate.Controls.Add(Me.Label36)
        Me.PnlSupplierDate.Controls.Add(Me.TxtDateFrom8)
        Me.PnlSupplierDate.Controls.Add(Me.CmbSupplier1)
        Me.PnlSupplierDate.Controls.Add(Me.Label37)
        Me.PnlSupplierDate.Location = New System.Drawing.Point(0, 0)
        Me.PnlSupplierDate.Name = "PnlSupplierDate"
        Me.PnlSupplierDate.Size = New System.Drawing.Size(660, 64)
        Me.PnlSupplierDate.TabIndex = 0
        Me.PnlSupplierDate.Visible = False
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(436, 22)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(21, 21)
        Me.Label35.TabIndex = 4
        Me.Label35.Text = "&To"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo8
        '
        Me.TxtDateTo8.BackColor = System.Drawing.Color.White
        Me.TxtDateTo8.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo8.Location = New System.Drawing.Point(461, 22)
        Me.TxtDateTo8.MaxLength = 50
        Me.TxtDateTo8.Name = "TxtDateTo8"
        Me.TxtDateTo8.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo8.TabIndex = 5
        '
        'Label36
        '
        Me.Label36.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(275, 22)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(68, 21)
        Me.Label36.TabIndex = 2
        Me.Label36.Text = "Date &From"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom8
        '
        Me.TxtDateFrom8.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom8.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom8.Location = New System.Drawing.Point(346, 22)
        Me.TxtDateFrom8.MaxLength = 50
        Me.TxtDateFrom8.Name = "TxtDateFrom8"
        Me.TxtDateFrom8.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom8.TabIndex = 3
        '
        'CmbSupplier1
        '
        Me.CmbSupplier1.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbSupplier1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbSupplier1.ColumnNum = 3
        Me.CmbSupplier1.ColumnWidth = "140;140;40"
        Me.CmbSupplier1.DisplayMember = "Text"
        Me.CmbSupplier1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbSupplier1.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbSupplier1.DropDownForeColor = System.Drawing.Color.White
        Me.CmbSupplier1.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbSupplier1.DropDownWidth = 340
        Me.CmbSupplier1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbSupplier1.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbSupplier1.GridLineHorizontal = False
        Me.CmbSupplier1.GridLineVertical = True
        Me.CmbSupplier1.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbSupplier1.Location = New System.Drawing.Point(112, 21)
        Me.CmbSupplier1.ManagingFastMouseMoving = True
        Me.CmbSupplier1.ManagingFastMouseMovingInterval = 30
        Me.CmbSupplier1.Name = "CmbSupplier1"
        Me.CmbSupplier1.Size = New System.Drawing.Size(157, 22)
        Me.CmbSupplier1.TabIndex = 1
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(7, 22)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(99, 21)
        Me.Label37.TabIndex = 0
        Me.Label37.Text = "Supplier"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmRPT1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(776, 519)
        Me.Controls.Add(Me.PnlDate)
        Me.Controls.Add(Me.PnlSupplierDate)
        Me.Controls.Add(Me.pnlSUPPLIER)
        Me.Controls.Add(Me.pnlBANK_ACC)
        Me.Controls.Add(Me.pnlCLIENT)
        Me.Controls.Add(Me.PnlBankAcc_Date)
        Me.Controls.Add(Me.pnlSALEMAN_DATE)
        Me.Controls.Add(Me.pnlSALEMAN)
        Me.Controls.Add(Me.pnlCOMPANY)
        Me.Controls.Add(Me.pnlITEM)
        Me.Controls.Add(Me.VIEW_REPORTButton)
        Me.Controls.Add(Me.CRV1)
        Me.Controls.Add(Me.BttnClose)
        Me.Controls.Add(Me.PnlInvoice)
        Me.Controls.Add(Me.PnlArea_Date)
        Me.Controls.Add(Me.PnlArea)
        Me.Controls.Add(Me.PnlCompany_Group)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmRPT1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "REPORT"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlBANK_ACC.ResumeLayout(False)
        Me.pnlBANK_ACC.PerformLayout()
        Me.pnlSUPPLIER.ResumeLayout(False)
        Me.pnlSUPPLIER.PerformLayout()
        Me.pnlCLIENT.ResumeLayout(False)
        Me.pnlCLIENT.PerformLayout()
        Me.PnlCompany_Group.ResumeLayout(False)
        Me.PnlBankAcc_Date.ResumeLayout(False)
        Me.PnlBankAcc_Date.PerformLayout()
        Me.PnlDate.ResumeLayout(False)
        Me.PnlDate.PerformLayout()
        Me.PnlArea.ResumeLayout(False)
        Me.PnlArea_Date.ResumeLayout(False)
        Me.PnlArea_Date.PerformLayout()
        Me.PnlInvoice.ResumeLayout(False)
        Me.PnlInvoice.PerformLayout()
        Me.pnlITEM.ResumeLayout(False)
        Me.pnlITEM.PerformLayout()
        Me.pnlCOMPANY.ResumeLayout(False)
        Me.pnlSALEMAN.ResumeLayout(False)
        Me.pnlSALEMAN_DATE.ResumeLayout(False)
        Me.pnlSALEMAN_DATE.PerformLayout()
        CType(Me.DsLUP_BANK1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSUPPLIER_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsCLIENT_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_VENDOR1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_AREA1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_LUP_ITEM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsRptCLIENT_LEDGER1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsRptSUPPLIER_LEDGER1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsRptCASH_LEDGER1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsRptBANK_LEDGER1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlSupplierDate.ResumeLayout(False)
        Me.PnlSupplierDate.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents VIEW_REPORTButton As System.Windows.Forms.Button
    Friend WithEvents pnlBANK_ACC As System.Windows.Forms.Panel
    Friend WithEvents CmbBankAccount As MTGCComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents DsLUP_BANK1 As Neruo_Business_Solution.dsLUP_BANK
    Friend WithEvents daLUP_BANK As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents pnlSUPPLIER As System.Windows.Forms.Panel
    Friend WithEvents CmbSupplier As MTGCComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom As System.Windows.Forms.TextBox
    Friend WithEvents daSUPPLIER_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand8 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsSUPPLIER_INFO1 As Neruo_Business_Solution.dsSUPPLIER_INFO
    Friend WithEvents CmbGroup As MTGCComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DsLUP_BUSINESS_GROUP1 As Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
    Friend WithEvents daLUP_BUSINESS_GROUP As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Public WithEvents CRV1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents pnlCLIENT As System.Windows.Forms.Panel
    Friend WithEvents CmbGroup1 As MTGCComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom1 As System.Windows.Forms.TextBox
    Friend WithEvents CmbClient As MTGCComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents daCLIENT_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsCLIENT_INFO1 As Neruo_Business_Solution.dsCLIENT_INFO
    Friend WithEvents PnlCompany_Group As System.Windows.Forms.Panel
    Friend WithEvents CmbGroup2 As MTGCComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CmbCompany As MTGCComboBox
    Friend WithEvents daLUP_VENDOR As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_VENDOR1 As Neruo_Business_Solution.dsLUP_VENDOR
    Friend WithEvents PnlBankAcc_Date As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo2 As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom2 As System.Windows.Forms.TextBox
    Friend WithEvents CmbBankAccount1 As MTGCComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents PnlDate As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo3 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom3 As System.Windows.Forms.TextBox
    Friend WithEvents PnlArea As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents CmbArea As MTGCComboBox
    Friend WithEvents daLUP_AREA As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlInsertCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_AREA1 As Neruo_Business_Solution.dsLUP_AREA
    Friend WithEvents PnlArea_Date As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo4 As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom4 As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CmbArea1 As MTGCComboBox
    Friend WithEvents PnlInvoice As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents TxtInvoiceTo As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TxtInvoiceFrom As System.Windows.Forms.TextBox
    Friend WithEvents pnlITEM As System.Windows.Forms.Panel
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo5 As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom5 As System.Windows.Forms.TextBox
    Friend WithEvents cmbITEM As MTGCComboBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents DsV_LUP_ITEM1 As Neruo_Business_Solution.dsV_LUP_ITEM
    Friend WithEvents daLUP_ITEM As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents pnlCOMPANY As System.Windows.Forms.Panel
    Friend WithEvents CmbCompany1 As MTGCComboBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents pnlSALEMAN As System.Windows.Forms.Panel
    Friend WithEvents CmbS_Man As MTGCComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents daLUP_EMPLOYEE As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand9 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand10 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand11 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_EMPLOYEE1 As Neruo_Business_Solution.dsLUP_EMPLOYEE
    Friend WithEvents pnlSALEMAN_DATE As System.Windows.Forms.Panel
    Friend WithEvents CmbS_Man1 As MTGCComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo6 As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom6 As System.Windows.Forms.TextBox
    Friend WithEvents daRptCLIENT_LEDGER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand13 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsRptCLIENT_LEDGER1 As Neruo_Business_Solution.dsRptCLIENT_LEDGER
    Friend WithEvents daRptSUPPLIER_LEDGER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand12 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsRptSUPPLIER_LEDGER1 As Neruo_Business_Solution.dsRptSUPPLIER_LEDGER
    Friend WithEvents daRptCASH_LEDGER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand14 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsRptCASH_LEDGER1 As Neruo_Business_Solution.dsRptCASH_LEDGER
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo7 As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom7 As System.Windows.Forms.TextBox
    Friend WithEvents daRptBANK_LEDGER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand15 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsRptBANK_LEDGER1 As Neruo_Business_Solution.dsRptBANK_LEDGER
    Friend WithEvents PnlSupplierDate As System.Windows.Forms.Panel
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo8 As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents TxtDateFrom8 As System.Windows.Forms.TextBox
    Friend WithEvents CmbSupplier1 As MTGCComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
End Class
