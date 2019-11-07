Imports SDS = System.Data.SqlClient
Imports System
Imports System.IO
Imports System.Text
Imports System.Security
Imports System.Security.Cryptography
Imports Microsoft.Win32

Public Class frmTESTING
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
    Friend WithEvents lsvData As System.Windows.Forms.ListView
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_ZONE1 As Neruo_Business_Solution.dsLUP_ZONE
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_ZONE As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGrid1 As System.Windows.Forms.DataGrid
    Friend WithEvents CmbColor As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TxtRound As System.Windows.Forms.TextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents NCODEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents SDESCDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ComboBox11 As MTGCComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lsvData = New System.Windows.Forms.ListView
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.DsLUP_ZONE1 = New Neruo_Business_Solution.dsLUP_ZONE
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand3 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_ZONE = New System.Data.SqlClient.SqlDataAdapter
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ComboBox11 = New MTGCComboBox
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.DataGrid1 = New System.Windows.Forms.DataGrid
        Me.CmbColor = New System.Windows.Forms.ComboBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.TxtRound = New System.Windows.Forms.TextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.NCODEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column4 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.SDESCDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column5 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Column6 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column2 = New System.Windows.Forms.DataGridViewComboBoxColumn
        CType(Me.DsLUP_ZONE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lsvData
        '
        Me.lsvData.Location = New System.Drawing.Point(0, 0)
        Me.lsvData.Name = "lsvData"
        Me.lsvData.Size = New System.Drawing.Size(121, 97)
        Me.lsvData.TabIndex = 0
        Me.lsvData.UseCompatibleStateImageBehavior = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(112, 48)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(168, 20)
        Me.TextBox1.TabIndex = 0
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(112, 128)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(168, 20)
        Me.TextBox2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Installation Code"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Activation Code"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(120, 88)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Button1"
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(248, 224)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox1.TabIndex = 4
        Me.ComboBox1.Text = "ComboBox1"
        '
        'ListBox1
        '
        Me.ListBox1.DataSource = Me.DsLUP_ZONE1.LUP_ZONE
        Me.ListBox1.Location = New System.Drawing.Point(286, 12)
        Me.ListBox1.MultiColumn = True
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(120, 95)
        Me.ListBox1.TabIndex = 5
        '
        'DsLUP_ZONE1
        '
        Me.DsLUP_ZONE1.DataSetName = "dsLUP_ZONE"
        Me.DsLUP_ZONE1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_ZONE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
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
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT nCODE, sDESC FROM LUP_ZONE"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = "UPDATE LUP_ZONE SET nCODE = @nCODE, sDESC = @sDESC WHERE (nCODE = @Original_nCODE" & _
            ") AND (sDESC = @Original_sDESC); SELECT nCODE, sDESC FROM LUP_ZONE WHERE (nCODE " & _
            "= @nCODE)"
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
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
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(375, 180)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(248, 97)
        Me.ListView1.TabIndex = 6
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 105
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Width = 147
        '
        'ComboBox11
        '
        Me.ComboBox11.BorderStyle = MTGCComboBox.TipiBordi.FlatXP
        Me.ComboBox11.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.ComboBox11.ColumnNum = 2
        Me.ComboBox11.ColumnWidth = "130;60"
        Me.ComboBox11.DisplayMember = "Text"
        Me.ComboBox11.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ComboBox11.DropDownArrowBackColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.ComboBox11.DropDownBackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.ComboBox11.DropDownForeColor = System.Drawing.Color.Black
        Me.ComboBox11.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.ComboBox11.DropDownWidth = 210
        Me.ComboBox11.GridLineColor = System.Drawing.Color.LightGray
        Me.ComboBox11.GridLineHorizontal = False
        Me.ComboBox11.GridLineVertical = False
        Me.ComboBox11.HighlightBorderColor = System.Drawing.Color.Blue
        Me.ComboBox11.HighlightBorderOnMouseEvents = True
        Me.ComboBox11.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.ComboBox11.Location = New System.Drawing.Point(11, 154)
        Me.ComboBox11.ManagingFastMouseMoving = True
        Me.ComboBox11.ManagingFastMouseMovingInterval = 30
        Me.ComboBox11.Name = "ComboBox11"
        Me.ComboBox11.NormalBorderColor = System.Drawing.Color.Black
        Me.ComboBox11.Size = New System.Drawing.Size(296, 21)
        Me.ComboBox11.TabIndex = 7
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"One, Two, Three", "Four"})
        Me.ComboBox2.Location = New System.Drawing.Point(60, 256)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(121, 21)
        Me.ComboBox2.TabIndex = 8
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column5, Me.Column6, Me.Column3, Me.Column2})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 308)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(394, 150)
        Me.DataGridView1.TabIndex = 0
        '
        'DataGridView2
        '
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NCODEDataGridViewTextBoxColumn, Me.Column4, Me.SDESCDataGridViewTextBoxColumn})
        Me.DataGridView2.DataMember = "LUP_ZONE"
        Me.DataGridView2.DataSource = Me.DsLUP_ZONE1
        Me.DataGridView2.Location = New System.Drawing.Point(412, 308)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(394, 150)
        Me.DataGridView2.TabIndex = 0
        '
        'DataGrid1
        '
        Me.DataGrid1.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.DataGrid1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGrid1.BackgroundColor = System.Drawing.Color.LightGray
        Me.DataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGrid1.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.DataGrid1.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.DataMember = ""
        Me.DataGrid1.DataSource = Me.DsLUP_ZONE1
        Me.DataGrid1.FlatMode = True
        Me.DataGrid1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DataGrid1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.GridLineColor = System.Drawing.Color.Gainsboro
        Me.DataGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.DataGrid1.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.DataGrid1.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.DataGrid1.LinkColor = System.Drawing.Color.Teal
        Me.DataGrid1.Location = New System.Drawing.Point(465, 25)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.DataGrid1.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGrid1.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.DataGrid1.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.DataGrid1.Size = New System.Drawing.Size(360, 123)
        Me.DataGrid1.TabIndex = 9
        '
        'CmbColor
        '
        Me.CmbColor.FormattingEnabled = True
        Me.CmbColor.Location = New System.Drawing.Point(665, 180)
        Me.CmbColor.Name = "CmbColor"
        Me.CmbColor.Size = New System.Drawing.Size(121, 21)
        Me.CmbColor.TabIndex = 10
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(665, 207)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 11
        Me.Button2.Text = "Fill Color"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(12, 181)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 39)
        Me.Button3.TabIndex = 12
        Me.Button3.Text = "Round (0.00)"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TxtRound
        '
        Me.TxtRound.Location = New System.Drawing.Point(93, 190)
        Me.TxtRound.Name = "TxtRound"
        Me.TxtRound.Size = New System.Drawing.Size(100, 20)
        Me.TxtRound.TabIndex = 13
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(286, 116)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(64, 32)
        Me.Button4.TabIndex = 14
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(359, 125)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 15
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(216, 88)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(64, 32)
        Me.Button5.TabIndex = 14
        Me.Button5.Text = "Button4"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(313, 151)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(64, 32)
        Me.Button6.TabIndex = 14
        Me.Button6.Text = "Button4"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'NCODEDataGridViewTextBoxColumn
        '
        Me.NCODEDataGridViewTextBoxColumn.DataPropertyName = "nCODE"
        Me.NCODEDataGridViewTextBoxColumn.HeaderText = "nCODE"
        Me.NCODEDataGridViewTextBoxColumn.Name = "NCODEDataGridViewTextBoxColumn"
        '
        'Column4
        '
        Me.Column4.DataPropertyName = "nCODE"
        Me.Column4.HeaderText = "Column4"
        Me.Column4.Name = "Column4"
        '
        'SDESCDataGridViewTextBoxColumn
        '
        Me.SDESCDataGridViewTextBoxColumn.DataPropertyName = "sDESC"
        Me.SDESCDataGridViewTextBoxColumn.HeaderText = "sDESC"
        Me.SDESCDataGridViewTextBoxColumn.Name = "SDESCDataGridViewTextBoxColumn"
        Me.SDESCDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'Column1
        '
        Me.Column1.HeaderText = "Code"
        Me.Column1.Name = "Column1"
        '
        'Column5
        '
        Me.Column5.HeaderText = "Column5"
        Me.Column5.Name = "Column5"
        '
        'Column6
        '
        Me.Column6.HeaderText = "Column6"
        Me.Column6.Name = "Column6"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Roll No"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Name"
        Me.Column2.Items.AddRange(New Object() {"sdfsd", "sdwer", "wergh", "jrtewf"})
        Me.Column2.Name = "Column2"
        '
        'frmTESTING
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(895, 483)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TxtRound)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.CmbColor)
        Me.Controls.Add(Me.DataGrid1)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox11)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Name = "frmTESTING"
        Me.Text = "FfrmTESTING"
        CType(Me.DsLUP_ZONE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim asConn As New AssConn


    Public Shared Function EncryptText(ByVal strText As String) As String
        Return Encrypt(strText, "NEUROSOFT")
    End Function
    Private Shared Function Encrypt(ByVal strText As String, ByVal strEncrKey As String) As String

        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(strEncrKey.ToString().Substring(0, 8))
        Dim des As New DESCryptoServiceProvider
        Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
        Dim ms As New MemoryStream
        Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()
        Return Convert.ToBase64String(ms.ToArray()).ToUpper

    End Function

    Private Sub frmTESTING_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString

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

        Me.ComboBox11.SelectedIndex = -1
        Me.ComboBox11.Items.Clear()
        Me.ComboBox11.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.ComboBox11.SourceDataString = New String(1) {"ZONE", "CODE"}
        Me.ComboBox11.SourceDataTable = dtLoading

        'Dim Str As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0\Target Id 0\Logical Unit Id 0"
        'Dim StrGet As String = "Identifier"
        'Dim Reg As RegistryKey
        'Try
        '    Reg = Registry.LocalMachine.OpenSubKey(Str.ToString)
        '    Me.TextBox1.Text = Reg.GetValue(StrGet.ToString)
        '    Reg.Close()
        'Catch ex As Exception
        'End Try

        'If TextBox1.Text = "" Then
        '    Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0\Target Id 1\Logical Unit Id 0"
        '    Dim StrGet1 As String = "Identifier"
        '    Dim Reg1 As RegistryKey
        '    Try
        '        Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
        '        Me.TextBox1.Text = Reg1.GetValue(StrGet1.ToString)
        '        Reg1.Close()
        '    Catch ex As Exception
        '    End Try

        'Else
        '    Exit Sub

        'End If

        'If TextBox1.Text = "" Then
        '    Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 1\Scsi Bus 0\Target Id 0\Logical Unit Id 0"
        '    Dim StrGet1 As String = "Identifier"
        '    Dim Reg1 As RegistryKey
        '    Try
        '        Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
        '        Me.TextBox1.Text = Reg1.GetValue(StrGet1.ToString)
        '        Reg1.Close()
        '    Catch ex As Exception
        '    End Try

        'Else
        '    Exit Sub

        'End If

        'If TextBox1.Text = "" Then
        '    Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 1\Scsi Bus 0\Target Id 1\Logical Unit Id 0"
        '    Dim StrGet1 As String = "Identifier"
        '    Dim Reg1 As RegistryKey
        '    Try
        '        Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
        '        Me.TextBox1.Text = Reg1.GetValue(StrGet1.ToString)
        '        Reg1.Close()
        '    Catch ex As Exception
        '    End Try

        'Else
        '    Exit Sub

        'End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.TextBox2.Text = EncryptText(Me.TextBox1.Text)
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick, DataGridView2.CellContentClick

    End Sub

    Private Sub DataGridView1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave, DataGridView2.CellLeave
        Select Case e.ColumnIndex
            Case 0


        End Select
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Dim typ As System.Type = GetType(System.Drawing.Color)
        'Dim aPropInfo As System.Reflection.PropertyInfo() = typ.GetProperties()
        'For Each pi As System.Reflection.PropertyInfo In aPropInfo
        '    If pi.PropertyType.Name = "Color" Then ' And pi.Name <> "Transparent" Then
        '        Me.CmbColor.Items.Add(pi.Name)
        '    End If
        'Next

        Dim color As Color

        For Each color In System.ComponentModel.TypeDescriptor.GetConverter(GetType(Color)).GetStandardValues
            CmbColor.Items.Add(color.ToKnownColor)
        Next

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.TxtRound.Text = Decimal.Round(CDec(Me.TxtRound.Text), 2) ', MidpointRounding.AwayFromZero)

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.ComboBox11.Enabled = Not Me.ComboBox11.Enabled
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.ComboBox11.SelectedIndex = Me.ComboBox11.FindString(Me.TextBox3.Text)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        MsgBox(Me.ComboBox11.SelectedIndex)
    End Sub
End Class

