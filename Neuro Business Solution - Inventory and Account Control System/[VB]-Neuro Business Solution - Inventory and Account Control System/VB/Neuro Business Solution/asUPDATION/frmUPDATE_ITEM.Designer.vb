<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUPDATE_ITEM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUPDATE_ITEM))
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle39 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.CmbCompany = New MTGCComboBox
        Me.daLUP_VENDOR = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.BttnClose = New System.Windows.Forms.Button
        Me.daLUP_ITEM = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand2 = New System.Data.SqlClient.SqlCommand
        Me.Label1 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ColCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColItemName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColNick = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPPP = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPksCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPksRate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPksRetail = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColST = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColVendor = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.DsLUP_VENDOR11 = New Neruo_Business_Solution.dsLUP_VENDOR1
        Me.ColPksDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPcsDesc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColOpenStk = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DsV_LUP_ITEM_NEW1 = New Neruo_Business_Solution.dsV_LUP_ITEM_NEW
        Me.DsLUP_VENDOR1 = New Neruo_Business_Solution.dsLUP_VENDOR
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtITEM_NAME = New System.Windows.Forms.TextBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_VENDOR11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_LUP_ITEM_NEW1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_VENDOR1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(885, 45)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Update Items / Products"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(72, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 23)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "&Company"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CmbCompany
        '
        Me.CmbCompany.Anchor = System.Windows.Forms.AnchorStyles.Top
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
        Me.CmbCompany.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbCompany.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbCompany.GridLineHorizontal = False
        Me.CmbCompany.GridLineVertical = True
        Me.CmbCompany.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbCompany.Location = New System.Drawing.Point(160, 44)
        Me.CmbCompany.ManagingFastMouseMoving = True
        Me.CmbCompany.ManagingFastMouseMovingInterval = 30
        Me.CmbCompany.Name = "CmbCompany"
        Me.CmbCompany.Size = New System.Drawing.Size(279, 24)
        Me.CmbCompany.TabIndex = 2
        '
        'daLUP_VENDOR
        '
        Me.daLUP_VENDOR.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_VENDOR.InsertCommand = Me.SqlInsertCommand1
        Me.daLUP_VENDOR.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_VENDOR.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_VENDOR", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_VENDOR.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "DELETE FROM [LUP_VENDOR] WHERE (([nCODE] = @Original_nCODE) AND ([sDESC] = @Origi" & _
            "nal_sDESC))"
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "INSERT INTO [LUP_VENDOR] ([sDESC]) VALUES (@sDESC);" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP" & _
            "_VENDOR WHERE (nCODE = SCOPE_IDENTITY())"
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC")})
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
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "UPDATE [LUP_VENDOR] SET [sDESC] = @sDESC WHERE (([nCODE] = @Original_nCODE) AND (" & _
            "[sDESC] = @Original_sDESC));" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "SELECT nCODE, sDESC FROM LUP_VENDOR WHERE (nCODE =" & _
            " @nCODE)"
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'BttnClose
        '
        Me.BttnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(784, 408)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 7
        Me.BttnClose.Text = "&Close"
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
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 412)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(555, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Note: Record is being updated on 'ROW' change"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.LightSteelBlue
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle33.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle33
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCode, Me.ColItemName, Me.ColNick, Me.ColPPP, Me.ColPksCost, Me.ColPksRate, Me.ColPksRetail, Me.ColST, Me.ColVendor, Me.ColPksDesc, Me.ColPcsDesc, Me.ColOpenStk})
        Me.DataGridView1.DataMember = "V_LUP_ITEM"
        Me.DataGridView1.DataSource = Me.DsV_LUP_ITEM_NEW1
        DataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle40.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle40.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle40.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle40
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.HotTrack
        Me.DataGridView1.Location = New System.Drawing.Point(12, 71)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 15
        Me.DataGridView1.RowTemplate.Height = 18
        Me.DataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.Size = New System.Drawing.Size(861, 331)
        Me.DataGridView1.TabIndex = 5
        '
        'ColCode
        '
        Me.ColCode.DataPropertyName = "nCODE"
        Me.ColCode.HeaderText = "Code"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.ReadOnly = True
        Me.ColCode.Width = 50
        '
        'ColItemName
        '
        Me.ColItemName.DataPropertyName = "sITEM_NAME"
        Me.ColItemName.HeaderText = "Item Name"
        Me.ColItemName.Name = "ColItemName"
        Me.ColItemName.Width = 160
        '
        'ColNick
        '
        Me.ColNick.DataPropertyName = "sNICK"
        Me.ColNick.HeaderText = "Nick / Formula"
        Me.ColNick.Name = "ColNick"
        Me.ColNick.Width = 120
        '
        'ColPPP
        '
        Me.ColPPP.DataPropertyName = "nPPP"
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle34.Format = "N0"
        DataGridViewCellStyle34.NullValue = Nothing
        Me.ColPPP.DefaultCellStyle = DataGridViewCellStyle34
        Me.ColPPP.HeaderText = "PPP"
        Me.ColPPP.Name = "ColPPP"
        Me.ColPPP.Width = 40
        '
        'ColPksCost
        '
        Me.ColPksCost.DataPropertyName = "UNIT_COST"
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle35.Format = "N2"
        DataGridViewCellStyle35.NullValue = "0.00"
        Me.ColPksCost.DefaultCellStyle = DataGridViewCellStyle35
        Me.ColPksCost.HeaderText = "Pks Cost"
        Me.ColPksCost.Name = "ColPksCost"
        Me.ColPksCost.Width = 60
        '
        'ColPksRate
        '
        Me.ColPksRate.DataPropertyName = "UNIT_RATE"
        DataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle36.Format = "N2"
        DataGridViewCellStyle36.NullValue = "0.00"
        Me.ColPksRate.DefaultCellStyle = DataGridViewCellStyle36
        Me.ColPksRate.HeaderText = "Pks Rate"
        Me.ColPksRate.Name = "ColPksRate"
        Me.ColPksRate.Width = 60
        '
        'ColPksRetail
        '
        Me.ColPksRetail.DataPropertyName = "UNIT_RETAIL"
        DataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle37.Format = "N2"
        DataGridViewCellStyle37.NullValue = "0.00"
        Me.ColPksRetail.DefaultCellStyle = DataGridViewCellStyle37
        Me.ColPksRetail.HeaderText = "Pks Retail"
        Me.ColPksRetail.Name = "ColPksRetail"
        Me.ColPksRetail.Width = 60
        '
        'ColST
        '
        Me.ColST.DataPropertyName = "nSALE_TAX"
        DataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle38.Format = "N2"
        DataGridViewCellStyle38.NullValue = "0.00"
        Me.ColST.DefaultCellStyle = DataGridViewCellStyle38
        Me.ColST.HeaderText = "S.T."
        Me.ColST.Name = "ColST"
        Me.ColST.Width = 40
        '
        'ColVendor
        '
        Me.ColVendor.DataPropertyName = "VENDOR"
        Me.ColVendor.DataSource = Me.DsLUP_VENDOR11
        Me.ColVendor.DisplayMember = "LUP_VENDOR.sDESC"
        Me.ColVendor.HeaderText = "Vendor/Company"
        Me.ColVendor.Name = "ColVendor"
        Me.ColVendor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColVendor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColVendor.ValueMember = "LUP_VENDOR.sDESC"
        Me.ColVendor.Width = 160
        '
        'DsLUP_VENDOR11
        '
        Me.DsLUP_VENDOR11.DataSetName = "dsLUP_VENDOR1"
        Me.DsLUP_VENDOR11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ColPksDesc
        '
        Me.ColPksDesc.DataPropertyName = "sPACK_DESC"
        Me.ColPksDesc.HeaderText = "Pks Desc"
        Me.ColPksDesc.Name = "ColPksDesc"
        Me.ColPksDesc.Width = 50
        '
        'ColPcsDesc
        '
        Me.ColPcsDesc.DataPropertyName = "sPIECE_DESC"
        Me.ColPcsDesc.HeaderText = "Pcs Desc"
        Me.ColPcsDesc.Name = "ColPcsDesc"
        Me.ColPcsDesc.Width = 50
        '
        'ColOpenStk
        '
        Me.ColOpenStk.DataPropertyName = "nOPEN_STOCK"
        DataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle39.Format = "N0"
        DataGridViewCellStyle39.NullValue = "0"
        Me.ColOpenStk.DefaultCellStyle = DataGridViewCellStyle39
        Me.ColOpenStk.HeaderText = "Opn Stk Pcs"
        Me.ColOpenStk.Name = "ColOpenStk"
        Me.ColOpenStk.Width = 40
        '
        'DsV_LUP_ITEM_NEW1
        '
        Me.DsV_LUP_ITEM_NEW1.DataSetName = "dsV_LUP_ITEM_NEW"
        Me.DsV_LUP_ITEM_NEW1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_VENDOR1
        '
        Me.DsLUP_VENDOR1.DataSetName = "dsLUP_VENDOR"
        Me.DsLUP_VENDOR1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(445, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 23)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "&Item Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtITEM_NAME
        '
        Me.txtITEM_NAME.Location = New System.Drawing.Point(534, 46)
        Me.txtITEM_NAME.Name = "txtITEM_NAME"
        Me.txtITEM_NAME.Size = New System.Drawing.Size(278, 20)
        Me.txtITEM_NAME.TabIndex = 4
        '
        'frmUPDATE_ITEM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(885, 451)
        Me.Controls.Add(Me.txtITEM_NAME)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BttnClose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.CmbCompany)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmUPDATE_ITEM"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Items / Products"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_VENDOR11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_LUP_ITEM_NEW1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_VENDOR1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CmbCompany As MTGCComboBox
    Friend WithEvents daLUP_VENDOR As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_VENDOR1 As Neruo_Business_Solution.dsLUP_VENDOR
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents daLUP_ITEM As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_VENDOR11 As Neruo_Business_Solution.dsLUP_VENDOR1
    Friend WithEvents DsV_LUP_ITEM_NEW1 As Neruo_Business_Solution.dsV_LUP_ITEM_NEW
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ColCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNick As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPPP As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPksCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPksRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPksRetail As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColST As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColVendor As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ColPksDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPcsDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColOpenStk As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtITEM_NAME As System.Windows.Forms.TextBox
End Class
