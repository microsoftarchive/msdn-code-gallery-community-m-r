Imports SDS = System.Data.SqlClient
Public Class frmLUP_SUPPLIER_OPEN_BAL
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
    Friend WithEvents TxtCode As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_SUPPLIER_OPEN_BAL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents TxtOpenBal As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbGroup As MTGCComboBox
    Friend WithEvents DsLUP_BUSINESS_GROUP1 As Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
    Friend WithEvents daLUP_BUSINESS_GROUP As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents DsSUPPLIER_INFO1 As Neruo_Business_Solution.dsSUPPLIER_INFO
    Friend WithEvents daSUPPLIER_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand8 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_SUPPLIER_OPEN_BAL1 As Neruo_Business_Solution.dsLUP_SUPPLIER_OPEN_BAL
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents CmbSupplier As MTGCComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_SUPPLIER_OPEN_BAL))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CmbGroup = New MTGCComboBox
        Me.CmbSupplier = New MTGCComboBox
        Me.TxtOpenBal = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtCode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_SUPPLIER_OPEN_BAL = New System.Data.SqlClient.SqlDataAdapter
        Me.DsLUP_BUSINESS_GROUP1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.DsSUPPLIER_INFO1 = New Neruo_Business_Solution.dsSUPPLIER_INFO
        Me.daSUPPLIER_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand8 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_SUPPLIER_OPEN_BAL1 = New Neruo_Business_Solution.dsLUP_SUPPLIER_OPEN_BAL
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSUPPLIER_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_SUPPLIER_OPEN_BAL1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnNextForm)
        Me.Panel1.Controls.Add(Me.BttnPrevForm)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(643, 384)
        Me.Panel1.TabIndex = 0
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(541, 3)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(97, 45)
        Me.BttnNextForm.TabIndex = 25
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Item Companies"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(5, 3)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(94, 45)
        Me.BttnPrevForm.TabIndex = 24
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Supplier Information"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(251, 51)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(387, 325)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader3, Me.ColumnHeader2, Me.ColumnHeader4})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 53)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(381, 269)
        Me.ListView1.TabIndex = 2
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "CODE"
        Me.ColumnHeader1.Width = 56
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "SUPPLIER(s)"
        Me.ColumnHeader3.Width = 135
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "GROUP(s)"
        Me.ColumnHeader2.Width = 120
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "OPEN BAL."
        Me.ColumnHeader4.Width = 100
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
        Me.TxtSearch.Location = New System.Drawing.Point(72, 24)
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
        Me.GroupBox1.Location = New System.Drawing.Point(5, 283)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 93)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(12, 15)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(76, 52)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(140, 15)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.CmbGroup)
        Me.GroupBox3.Controls.Add(Me.CmbSupplier)
        Me.GroupBox3.Controls.Add(Me.TxtOpenBal)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtCode)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(5, 51)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(240, 226)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'CmbGroup
        '
        Me.CmbGroup.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup.ColumnNum = 3
        Me.CmbGroup.ColumnWidth = "140;100;40"
        Me.CmbGroup.DisplayMember = "Text"
        Me.CmbGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbGroup.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbGroup.DropDownForeColor = System.Drawing.Color.White
        Me.CmbGroup.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbGroup.DropDownWidth = 300
        Me.CmbGroup.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbGroup.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbGroup.GridLineHorizontal = False
        Me.CmbGroup.GridLineVertical = True
        Me.CmbGroup.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbGroup.Location = New System.Drawing.Point(16, 159)
        Me.CmbGroup.ManagingFastMouseMoving = True
        Me.CmbGroup.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup.Name = "CmbGroup"
        Me.CmbGroup.Size = New System.Drawing.Size(208, 24)
        Me.CmbGroup.TabIndex = 5
        '
        'CmbSupplier
        '
        Me.CmbSupplier.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbSupplier.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbSupplier.ColumnNum = 3
        Me.CmbSupplier.ColumnWidth = "140;100;40"
        Me.CmbSupplier.DisplayMember = "Text"
        Me.CmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbSupplier.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbSupplier.DropDownForeColor = System.Drawing.Color.White
        Me.CmbSupplier.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbSupplier.DropDownWidth = 300
        Me.CmbSupplier.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbSupplier.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbSupplier.GridLineHorizontal = False
        Me.CmbSupplier.GridLineVertical = True
        Me.CmbSupplier.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbSupplier.Location = New System.Drawing.Point(16, 104)
        Me.CmbSupplier.ManagingFastMouseMoving = True
        Me.CmbSupplier.ManagingFastMouseMovingInterval = 30
        Me.CmbSupplier.Name = "CmbSupplier"
        Me.CmbSupplier.Size = New System.Drawing.Size(208, 24)
        Me.CmbSupplier.TabIndex = 3
        '
        'TxtOpenBal
        '
        Me.TxtOpenBal.BackColor = System.Drawing.Color.White
        Me.TxtOpenBal.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOpenBal.Location = New System.Drawing.Point(107, 189)
        Me.TxtOpenBal.Name = "TxtOpenBal"
        Me.TxtOpenBal.Size = New System.Drawing.Size(117, 23)
        Me.TxtOpenBal.TabIndex = 7
        Me.TxtOpenBal.Text = "0.00"
        Me.TxtOpenBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 189)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 23)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "&Open Bal."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(208, 23)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "&Group Name"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(208, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Code (Auto Generated)"
        '
        'TxtCode
        '
        Me.TxtCode.BackColor = System.Drawing.Color.White
        Me.TxtCode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.Location = New System.Drawing.Point(16, 48)
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.ReadOnly = True
        Me.TxtCode.Size = New System.Drawing.Size(208, 23)
        Me.TxtCode.TabIndex = 1
        Me.TxtCode.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(208, 23)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "S&upplier Name"
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(641, 48)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "SUPPLIER's OPENING BALANCE(s)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "SELECT     ID, SUPPLIER_NAME, GROUP_NAME, CONVERT(NUMERIC(18, 2), OPEN_BAL) AS OP" & _
            "EN_BAL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_SUPPLIER_OPEN_BAL"
        Me.SqlSelectCommand3.Connection = Me.SqlConnection1
        '
        'daLUP_SUPPLIER_OPEN_BAL
        '
        Me.daLUP_SUPPLIER_OPEN_BAL.SelectCommand = Me.SqlSelectCommand3
        Me.daLUP_SUPPLIER_OPEN_BAL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_SUPPLIER_OPEN_BAL", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("SUPPLIER_NAME", "SUPPLIER_NAME"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("OPEN_BAL", "OPEN_BAL")})})
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
        'DsSUPPLIER_INFO1
        '
        Me.DsSUPPLIER_INFO1.DataSetName = "dsSUPPLIER_INFO"
        Me.DsSUPPLIER_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        'DsLUP_SUPPLIER_OPEN_BAL1
        '
        Me.DsLUP_SUPPLIER_OPEN_BAL1.DataSetName = "dsLUP_SUPPLIER_OPEN_BAL"
        Me.DsLUP_SUPPLIER_OPEN_BAL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_SUPPLIER_OPEN_BAL
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(661, 400)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_SUPPLIER_OPEN_BAL"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SUPPLIER's OPEN BALANCE"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSUPPLIER_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_SUPPLIER_OPEN_BAL1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asInsert As New AssInsert
    Dim asUpdate As New AssUpdate
    Dim asDelete As New AssDelete
    Dim asNum As New AssNumPress
    Dim asMAX As New AssMaxNo
    Dim Rd As System.Data.SqlClient.SqlDataReader

#End Region

#Region "FORM CONTROL"
    Private Sub frmLUP_SUPPLIER_OPEN_BAL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Me.FillComboBox_Supplier()
        Me.FillComboBox_Group()

    End Sub

    Private Sub frmLUP_SUPPLIER_OPEN_BAL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub TxtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.GotFocus, TxtSearch.GotFocus, TxtOpenBal.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub TxtCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.LostFocus, TxtSearch.LostFocus, TxtOpenBal.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    Private Sub Txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtOpenBal.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        If Me.TxtSearch.Text = Nothing Then
            Me.FillListView()

        Else
            Me.FillListView_Condition()
        End If
    End Sub
#End Region

#Region "ComboBox Control"
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSupplier.GotFocus, CmbGroup.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSupplier.LostFocus, CmbGroup.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        Me.TxtCode.Text = Me.ListView1.SelectedItems(0).Text

        Dim StrCmb As String = Nothing

        Me.CmbSupplier.Text = Me.ListView1.SelectedItems(0).SubItems(1).Text
        StrCmb = Me.CmbSupplier.Text
        Me.CmbSupplier.SelectedIndex = -1
        Me.CmbSupplier.SelectedIndex = Me.CmbSupplier.FindString(StrCmb)

        Me.CmbGroup.Text = Me.ListView1.SelectedItems(0).SubItems(2).Text
        StrCmb = Me.CmbGroup.Text
        Me.CmbGroup.SelectedIndex = -1
        Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCmb)

        Me.TxtOpenBal.Text = Me.ListView1.SelectedItems(0).SubItems(3).Text
        'Me.TxtDESC.Focus()

FIX:
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM SUPPLIER_OPEN_BAL WHERE nID=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

                Me.FillListView()

                Me.BttnNew_Click(sender, New System.EventArgs)
            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            Me.CmbSupplier.Focus()
        End If

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.TxtCode.Text = Nothing
        Me.TxtSearch.Text = Nothing
        Me.CmbSupplier.SelectedIndex = -1
        Me.CmbGroup.SelectedIndex = -1
        Me.TxtOpenBal.Text = "0.00"
        'Me.TxtCode.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nCODE) FROM LUP_ZONE") + 1
        Me.CmbSupplier.Focus()
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        If Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Val(Me.TxtOpenBal.Text) <= 0 Then
            MsgBox("Please enter description!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Then
                Me.CmbSupplier.Focus()

            ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                Me.CmbGroup.Focus()

            ElseIf Val(Me.TxtOpenBal.Text) <= 0 Then
                Me.TxtOpenBal.Focus()

            End If


        ElseIf Me.TxtCode.Text = Nothing Then
            If MsgBox("Do you want to save '" & Me.CmbSupplier.Text & "' : & '" & Me.TxtOpenBal.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_OPEN_BAL(nSUPPLIER_ID,nOPEN_BAL,nBUSINESS_CODE) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & "," & Val(Me.TxtOpenBal.Text) & "," & Val(Me.CmbGroup.SelectedItem.Col3) & ") ")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.CmbSupplier.Focus()
            End If

        ElseIf Not Me.TxtCode.Text = Nothing Then
            If MsgBox("Do you want to update '" & Me.CmbSupplier.Text & "' : & '" & Me.TxtOpenBal.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_OPEN_BAL SET nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", nOPEN_BAL=" & Val(Me.TxtOpenBal.Text) & ", nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & " WHERE nID=" & Val(Me.TxtCode.Text) & "")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.CmbSupplier.Focus()
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        frmLUP_SUPPLIER.MdiParent = Me.ParentForm
        frmLUP_SUPPLIER.Show()
        frmLUP_SUPPLIER.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_VENDORS.MdiParent = Me.ParentForm
        frmLUP_VENDORS.Show()
        frmLUP_VENDORS.Activate()
        Me.Close()
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

    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT ID, SUPPLIER_NAME, GROUP_NAME, CONVERT(NUMERIC(18, 2), OPEN_BAL) AS OPEN_BAL FROM V_SUPPLIER_OPEN_BAL ORDER BY SUPPLIER_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_SUPPLIER_OPEN_BAL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_SUPPLIER_OPEN_BAL1.Clear()
            Me.daLUP_SUPPLIER_OPEN_BAL.Fill(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Item(Cnt).Item(2).ToString, Color.DarkGreen, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Item(Cnt).Item(3).ToString, Color.DarkGreen, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()

        Try
            Dim Str1 As String = "SELECT ID, SUPPLIER_NAME, GROUP_NAME, CONVERT(NUMERIC(18, 2), OPEN_BAL) AS OPEN_BAL FROM V_SUPPLIER_OPEN_BAL WHERE SUPPLIER_NAME LIKE '%" & Me.TxtSearch.Text & "%' ORDER BY SUPPLIER_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_SUPPLIER_OPEN_BAL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_SUPPLIER_OPEN_BAL1.Clear()
            Me.daLUP_SUPPLIER_OPEN_BAL.Fill(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Item(Cnt).Item(2).ToString, Color.DarkGreen, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add(Me.DsLUP_SUPPLIER_OPEN_BAL1.V_SUPPLIER_OPEN_BAL.Item(Cnt).Item(3).ToString, Color.DarkGreen, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


End Class
