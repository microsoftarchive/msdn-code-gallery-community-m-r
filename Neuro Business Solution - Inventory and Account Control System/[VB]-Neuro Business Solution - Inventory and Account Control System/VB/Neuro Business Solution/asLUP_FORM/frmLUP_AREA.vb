Imports SDS = System.Data.SqlClient
Public Class frmLUP_AREA
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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtDESC As System.Windows.Forms.TextBox
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents TxtCode As System.Windows.Forms.TextBox
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents daLUP_ZONE As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLUP_ZONE1 As Neruo_Business_Solution.dsLUP_ZONE
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_AREA As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLUP_AREA1 As Neruo_Business_Solution.dsLUP_AREA
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents CODEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AREADataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ZONEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CmbZONE As MTGCComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.CODEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AREADataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ZONEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DsLUP_AREA1 = New Neruo_Business_Solution.dsLUP_AREA
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CmbZONE = New MTGCComboBox
        Me.TxtDESC = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtCode = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.DsLUP_ZONE1 = New Neruo_Business_Solution.dsLUP_ZONE
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_ZONE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand3 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_AREA = New System.Data.SqlClient.SqlDataAdapter
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_AREA1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsLUP_ZONE1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(594, 384)
        Me.Panel1.TabIndex = 0
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(497, 7)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(89, 45)
        Me.BttnNextForm.TabIndex = 8
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Route(s) Detail"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(8, 7)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(89, 45)
        Me.BttnPrevForm.TabIndex = 7
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Zone(s) Detail"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(592, 48)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "AREA DETAIL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(254, 64)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(332, 312)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.PeachPuff
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeight = 25
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CODEDataGridViewTextBoxColumn, Me.AREADataGridViewTextBoxColumn, Me.ZONEDataGridViewTextBoxColumn})
        Me.DataGridView1.DataMember = "V_LUP_AREA"
        Me.DataGridView1.DataSource = Me.DsLUP_AREA1
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(3, 53)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 30
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowTemplate.Height = 18
        Me.DataGridView1.RowTemplate.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.ShowEditingIcon = False
        Me.DataGridView1.Size = New System.Drawing.Size(326, 256)
        Me.DataGridView1.TabIndex = 2
        '
        'CODEDataGridViewTextBoxColumn
        '
        Me.CODEDataGridViewTextBoxColumn.DataPropertyName = "CODE"
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CODEDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle2
        Me.CODEDataGridViewTextBoxColumn.HeaderText = "CODE"
        Me.CODEDataGridViewTextBoxColumn.Name = "CODEDataGridViewTextBoxColumn"
        Me.CODEDataGridViewTextBoxColumn.ReadOnly = True
        Me.CODEDataGridViewTextBoxColumn.Width = 60
        '
        'AREADataGridViewTextBoxColumn
        '
        Me.AREADataGridViewTextBoxColumn.DataPropertyName = "AREA"
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AREADataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle3
        Me.AREADataGridViewTextBoxColumn.HeaderText = "AREA"
        Me.AREADataGridViewTextBoxColumn.Name = "AREADataGridViewTextBoxColumn"
        Me.AREADataGridViewTextBoxColumn.ReadOnly = True
        Me.AREADataGridViewTextBoxColumn.Width = 160
        '
        'ZONEDataGridViewTextBoxColumn
        '
        Me.ZONEDataGridViewTextBoxColumn.DataPropertyName = "ZONE"
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ZONEDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle4
        Me.ZONEDataGridViewTextBoxColumn.HeaderText = "ZONE"
        Me.ZONEDataGridViewTextBoxColumn.Name = "ZONEDataGridViewTextBoxColumn"
        Me.ZONEDataGridViewTextBoxColumn.ReadOnly = True
        Me.ZONEDataGridViewTextBoxColumn.Width = 160
        '
        'DsLUP_AREA1
        '
        Me.DsLUP_AREA1.DataSetName = "dsLUP_AREA"
        Me.DsLUP_AREA1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_AREA1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        Me.GroupBox1.Location = New System.Drawing.Point(8, 261)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 112)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(12, 24)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(76, 72)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(140, 24)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.CmbZONE)
        Me.GroupBox3.Controls.Add(Me.TxtDESC)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtCode)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 61)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(240, 192)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'CmbZONE
        '
        Me.CmbZONE.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbZONE.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbZONE.ColumnNum = 2
        Me.CmbZONE.ColumnWidth = "140;40"
        Me.CmbZONE.DisplayMember = "Text"
        Me.CmbZONE.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbZONE.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbZONE.DropDownForeColor = System.Drawing.Color.White
        Me.CmbZONE.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbZONE.DropDownWidth = 220
        Me.CmbZONE.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbZONE.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbZONE.GridLineHorizontal = False
        Me.CmbZONE.GridLineVertical = True
        Me.CmbZONE.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbZONE.Location = New System.Drawing.Point(16, 104)
        Me.CmbZONE.ManagingFastMouseMoving = True
        Me.CmbZONE.ManagingFastMouseMovingInterval = 30
        Me.CmbZONE.Name = "CmbZONE"
        Me.CmbZONE.Size = New System.Drawing.Size(208, 24)
        Me.CmbZONE.TabIndex = 3
        '
        'TxtDESC
        '
        Me.TxtDESC.BackColor = System.Drawing.Color.White
        Me.TxtDESC.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDESC.Location = New System.Drawing.Point(16, 160)
        Me.TxtDESC.Name = "TxtDESC"
        Me.TxtDESC.Size = New System.Drawing.Size(208, 23)
        Me.TxtDESC.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(208, 23)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "&Area Name"
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
        Me.Label5.Text = "&Zone Name"
        '
        'DsLUP_ZONE1
        '
        Me.DsLUP_ZONE1.DataSetName = "dsLUP_ZONE"
        Me.DsLUP_ZONE1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_ZONE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT nCODE, sDESC FROM LUP_ZONE"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = "INSERT INTO LUP_ZONE(nCODE, sDESC) VALUES (@nCODE, @sDESC); SELECT nCODE, sDESC F" & _
            "ROM LUP_ZONE WHERE (nCODE = @nCODE)"
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC")})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "UPDATE LUP_ZONE SET nCODE = @nCODE, sDESC = @sDESC WHERE (nCODE = @Original_nCODE" & _
            ") AND (sDESC = @Original_sDESC); SELECT nCODE, sDESC FROM LUP_ZONE WHERE (nCODE " & _
            "= @nCODE)"
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "DELETE FROM LUP_ZONE WHERE (nCODE = @Original_nCODE) AND (sDESC = @Original_sDESC" & _
            ")"
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daLUP_ZONE
        '
        Me.daLUP_ZONE.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_ZONE.InsertCommand = Me.SqlInsertCommand1
        Me.daLUP_ZONE.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_ZONE.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_ZONE", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC")})})
        Me.daLUP_ZONE.UpdateCommand = Me.SqlUpdateCommand1
        '
        'SqlSelectCommand3
        '
        Me.SqlSelectCommand3.CommandText = "SELECT CODE, AREA, ZONE FROM V_LUP_AREA"
        Me.SqlSelectCommand3.Connection = Me.SqlConnection1
        '
        'SqlInsertCommand3
        '
        Me.SqlInsertCommand3.CommandText = "INSERT INTO V_LUP_AREA(CODE, AREA, ZONE) VALUES (@CODE, @AREA, @ZONE); SELECT COD" & _
            "E, AREA, ZONE FROM V_LUP_AREA"
        Me.SqlInsertCommand3.Connection = Me.SqlConnection1
        Me.SqlInsertCommand3.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@CODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "CODE", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 50, "AREA"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 50, "ZONE")})
        '
        'daLUP_AREA
        '
        Me.daLUP_AREA.InsertCommand = Me.SqlInsertCommand3
        Me.daLUP_AREA.SelectCommand = Me.SqlSelectCommand3
        Me.daLUP_AREA.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_LUP_AREA", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("ZONE", "ZONE")})})
        '
        'frmLUP_AREA
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(612, 400)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_AREA"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AREA"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_AREA1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsLUP_ZONE1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_AREA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Me.FillComboBox()
        On Error GoTo Fix
        Me.CmbZONE.SelectedIndex = Me.CmbZONE.FindString(Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex).Value)
Fix:
    End Sub

    Private Sub frmLUP_AREA_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub TxtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.GotFocus, TxtDESC.GotFocus, TxtSearch.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub TxtCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.LostFocus, TxtDESC.LostFocus, TxtSearch.LostFocus
        CType(sender, TextBox).BackColor = Color.White
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
    'Private Sub CmbZONE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbZONE.KeyPress
    '    asNum.EnterTab(e)
    'End Sub

    Private Sub CmbZONE_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbZONE.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub CmbZONE_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbZONE.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "ListView Control"
    '    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        On Error GoTo FIX
    '        Me.TxtCode.Text = Me.ListView1.SelectedItems(0).Text
    '        Me.TxtDESC.Text = Me.ListView1.SelectedItems(0).SubItems(1).Text
    '        Me.CmbZONE.Text = Me.ListView1.SelectedItems(0).SubItems(2).Text
    '        'Me.TxtDESC.Focus()

    'FIX:
    '    End Sub
    '    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
    '            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
    '                Me.asDelete.DeleteValueIN("DELETE FROM LUP_AREA WHERE nCODE=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

    '                Me.FillListView()

    '                Me.BttnNew_Click(sender, New System.EventArgs)
    '            End If

    '        Else
    '            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
    '            Me.TxtDESC.Focus()
    '        End If

    '    End Sub
#End Region

#Region "DataGrid Controls"
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        On Error GoTo Fix
        Me.TxtCode.Text = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
        Me.TxtDESC.Text = Me.DataGridView1.Item(1, Me.DataGridView1.CurrentCell.RowIndex).Value
        Me.CmbZONE.SelectedIndex = Me.CmbZONE.FindString(Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex).Value)
Fix:
    End Sub
    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        On Error GoTo Fix
        Me.TxtCode.Text = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
        Me.TxtDESC.Text = Me.DataGridView1.Item(1, Me.DataGridView1.CurrentCell.RowIndex).Value
        Me.CmbZONE.SelectedIndex = Me.CmbZONE.FindString(Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex).Value)
Fix:
    End Sub
    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        If Not Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.DataGridView1.Item(1, Me.DataGridView1.CurrentCell.RowIndex).Value & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM LUP_AREA WHERE nCODE=" & Val(Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value) & "")
                'Me.FillListView()
                Me.BttnNew_Click(sender, New System.EventArgs)

            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            e.Cancel = True
            Me.TxtDESC.Focus()
        End If
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.TxtCode.Text = Nothing
        Me.TxtDESC.Text = Nothing
        Me.TxtSearch.Text = Nothing
        Me.CmbZONE.SelectedIndex = -1
        'Me.TxtCode.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nCODE) FROM LUP_ZONE") + 1
        Me.CmbZONE.Focus()
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        If Me.TxtDESC.Text = Nothing Or Me.CmbZONE.SelectedIndex = -1 Or Me.CmbZONE.Text = Nothing Then
            MsgBox("Please enter description!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.CmbZONE.SelectedIndex = -1 Or Me.CmbZONE.Text = Nothing Then
                Me.CmbZONE.Focus()

            ElseIf Me.TxtDESC.Text = Nothing Then
                Me.TxtDESC.Focus()

            End If


        ElseIf Me.TxtCode.Text = Nothing Then
            If MsgBox("Do you want to save '" & Me.TxtDESC.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO LUP_AREA(sDESC,nZONE_CODE) VALUES('" & Me.TxtDESC.Text & "'," & Val(Me.CmbZONE.SelectedItem.col2) & ") ")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtDESC.Focus()
            End If

        ElseIf Not Me.TxtCode.Text = Nothing Then
            If MsgBox("Do you want to update '" & Me.TxtDESC.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE LUP_AREA SET sDESC='" & Me.TxtDESC.Text & "', nZONE_CODE=" & Val(Me.CmbZONE.SelectedItem.col2) & " WHERE nCODE=" & Val(Me.TxtCode.Text) & "")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtDESC.Focus()
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        frmLUP_ZONE.MdiParent = Me.ParentForm
        frmLUP_ZONE.Show()
        frmLUP_ZONE.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_ROUTES.MdiParent = Me.ParentForm
        frmLUP_ROUTES.Show()
        frmLUP_ROUTES.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillComboBox()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_ZONE ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_ZONE = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_ZONE1.Clear()
        Me.daLUP_ZONE.Fill(Me.DsLUP_ZONE1.LUP_ZONE)

        Dim dtLoading As New DataTable("LUP_ZONE")

        dtLoading.Columns.Add("CODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("ZONE", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_ZONE1.LUP_ZONE.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("CODE") = Me.DsLUP_ZONE1.LUP_ZONE.Item(Cnt).Item(0).ToString
            dr("ZONE") = Me.DsLUP_ZONE1.LUP_ZONE.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbZONE.SelectedIndex = -1
        Me.CmbZONE.Items.Clear()
        Me.CmbZONE.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbZONE.SourceDataString = New String(1) {"ZONE", "CODE"}
        Me.CmbZONE.SourceDataTable = dtLoading
    End Sub
    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT CODE, AREA, ZONE FROM V_LUP_AREA ORDER BY AREA"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_AREA = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_AREA1.Clear()
            Me.daLUP_AREA.Fill(Me.DsLUP_AREA1.V_LUP_AREA)

            'Me.ListView1.Items.Clear()

            'Dim Cnt As Integer
            'Dim LstItem As ListViewItem

            'For Cnt = 0 To Me.DsLUP_AREA1.V_LUP_AREA.Count - 1
            '    LstItem = Me.ListView1.Items.Add(Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(0).ToString)
            '    Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
            '    With LstItem.SubItems

            '        .Add(Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

            '        .Add(Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(2).ToString, Color.DarkGreen, Me.ListView1.BackColor, Me.ListView1.Font)

            '    End With
            'Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim Str1 As String = "SELECT CODE, AREA, ZONE FROM V_LUP_AREA WHERE AREA LIKE '%" & Me.TxtSearch.Text & "%' ORDER BY AREA"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_AREA = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_AREA1.Clear()
            Me.daLUP_AREA.Fill(Me.DsLUP_AREA1.V_LUP_AREA)

            'Me.ListView1.Items.Clear()

            'Dim Cnt As Integer
            'Dim LstItem As ListViewItem

            'For Cnt = 0 To Me.DsLUP_AREA1.V_LUP_AREA.Count - 1
            '    LstItem = Me.ListView1.Items.Add(Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(0).ToString)
            '    Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
            '    With LstItem.SubItems

            '        .Add(Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

            '        .Add(Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(2).ToString, Color.DarkGreen, Me.ListView1.BackColor, Me.ListView1.Font)

            '    End With
            'Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region





End Class
