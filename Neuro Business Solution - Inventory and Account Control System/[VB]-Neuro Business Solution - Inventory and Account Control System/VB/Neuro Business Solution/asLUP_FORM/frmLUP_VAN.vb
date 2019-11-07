Imports SDS = System.Data.SqlClient
Public Class frmLUP_VAN
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
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DmnStatus As System.Windows.Forms.DomainUpDown
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents TxtPlateNo As System.Windows.Forms.TextBox
    Friend WithEvents TxtModel As System.Windows.Forms.TextBox
    Friend WithEvents TxtYear As System.Windows.Forms.TextBox
    Friend WithEvents TxtEngNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TxtChsNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_VAN As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLUP_VAN11 As Neruo_Business_Solution.dsLUP_VAN1
    Friend WithEvents DsLUP_VAN1 As Neruo_Business_Solution.dsLUP_VAN
    Friend WithEvents CmbColor As System.Windows.Forms.ComboBox
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents LblColor As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_VAN))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CmbColor = New System.Windows.Forms.ComboBox
        Me.DsLUP_VAN1 = New Neruo_Business_Solution.dsLUP_VAN
        Me.DmnStatus = New System.Windows.Forms.DomainUpDown
        Me.TxtName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtPlateNo = New System.Windows.Forms.TextBox
        Me.TxtEngNo = New System.Windows.Forms.TextBox
        Me.TxtModel = New System.Windows.Forms.TextBox
        Me.LblColor = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.TxtChsNo = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtYear = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_VAN = New System.Data.SqlClient.SqlDataAdapter
        Me.DsLUP_VAN11 = New Neruo_Business_Solution.dsLUP_VAN1
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsLUP_VAN1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_VAN11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnNextForm)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.BttnPrevForm)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(12, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(680, 375)
        Me.Panel1.TabIndex = 0
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(570, 3)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(101, 46)
        Me.BttnNextForm.TabIndex = 43
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Business Groups"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 189)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(663, 174)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader3, Me.ColumnHeader5})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 50)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(657, 121)
        Me.ListView1.TabIndex = 2
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Plate #"
        Me.ColumnHeader1.Width = 112
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Name"
        Me.ColumnHeader2.Width = 196
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Model"
        Me.ColumnHeader4.Width = 167
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Year"
        Me.ColumnHeader3.Width = 99
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Colour"
        Me.ColumnHeader5.Width = 76
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
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(8, 3)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(101, 46)
        Me.BttnPrevForm.TabIndex = 42
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Employee Information"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(551, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 135)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(16, 15)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(16, 93)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(16, 54)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.CmbColor)
        Me.GroupBox3.Controls.Add(Me.DmnStatus)
        Me.GroupBox3.Controls.Add(Me.TxtName)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtPlateNo)
        Me.GroupBox3.Controls.Add(Me.TxtEngNo)
        Me.GroupBox3.Controls.Add(Me.TxtModel)
        Me.GroupBox3.Controls.Add(Me.LblColor)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.TxtChsNo)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.TxtYear)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 46)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(537, 137)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'CmbColor
        '
        Me.CmbColor.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_VAN1, "LUP_VANS.sCOLOR", True))
        Me.CmbColor.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.CmbColor.FormattingEnabled = True
        Me.CmbColor.Location = New System.Drawing.Point(116, 99)
        Me.CmbColor.Name = "CmbColor"
        Me.CmbColor.Size = New System.Drawing.Size(112, 24)
        Me.CmbColor.TabIndex = 13
        '
        'DsLUP_VAN1
        '
        Me.DsLUP_VAN1.DataSetName = "dsLUP_VAN"
        Me.DsLUP_VAN1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DmnStatus
        '
        Me.DmnStatus.BackColor = System.Drawing.Color.White
        Me.DmnStatus.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_VAN1, "LUP_VANS.STATUS", True))
        Me.DmnStatus.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DmnStatus.Items.Add("No")
        Me.DmnStatus.Items.Add("Yes")
        Me.DmnStatus.Location = New System.Drawing.Point(379, 99)
        Me.DmnStatus.Name = "DmnStatus"
        Me.DmnStatus.ReadOnly = True
        Me.DmnStatus.Size = New System.Drawing.Size(86, 23)
        Me.DmnStatus.TabIndex = 16
        '
        'TxtName
        '
        Me.TxtName.BackColor = System.Drawing.Color.White
        Me.TxtName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_VAN1, "LUP_VANS.sDESC", True))
        Me.TxtName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.Location = New System.Drawing.Point(379, 24)
        Me.TxtName.MaxLength = 50
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(149, 23)
        Me.TxtName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(271, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "&Name*"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "&Plate #*"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPlateNo
        '
        Me.TxtPlateNo.BackColor = System.Drawing.Color.White
        Me.TxtPlateNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPlateNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlateNo.Location = New System.Drawing.Point(116, 24)
        Me.TxtPlateNo.MaxLength = 50
        Me.TxtPlateNo.Name = "TxtPlateNo"
        Me.TxtPlateNo.Size = New System.Drawing.Size(149, 23)
        Me.TxtPlateNo.TabIndex = 1
        '
        'TxtEngNo
        '
        Me.TxtEngNo.BackColor = System.Drawing.Color.White
        Me.TxtEngNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtEngNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_VAN1, "LUP_VANS.sENG_NO", True))
        Me.TxtEngNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEngNo.Location = New System.Drawing.Point(116, 74)
        Me.TxtEngNo.Name = "TxtEngNo"
        Me.TxtEngNo.Size = New System.Drawing.Size(149, 23)
        Me.TxtEngNo.TabIndex = 9
        '
        'TxtModel
        '
        Me.TxtModel.BackColor = System.Drawing.Color.White
        Me.TxtModel.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_VAN1, "LUP_VANS.sMODEL", True))
        Me.TxtModel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModel.Location = New System.Drawing.Point(116, 49)
        Me.TxtModel.Name = "TxtModel"
        Me.TxtModel.Size = New System.Drawing.Size(149, 23)
        Me.TxtModel.TabIndex = 5
        '
        'LblColor
        '
        Me.LblColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblColor.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblColor.Location = New System.Drawing.Point(234, 100)
        Me.LblColor.Name = "LblColor"
        Me.LblColor.Size = New System.Drawing.Size(32, 23)
        Me.LblColor.TabIndex = 14
        Me.LblColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 99)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 23)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "&Colour"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 74)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(102, 23)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "&Eng. #"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtChsNo
        '
        Me.TxtChsNo.BackColor = System.Drawing.Color.White
        Me.TxtChsNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtChsNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_VAN1, "LUP_VANS.sCH_NO", True))
        Me.TxtChsNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChsNo.Location = New System.Drawing.Point(379, 74)
        Me.TxtChsNo.Name = "TxtChsNo"
        Me.TxtChsNo.Size = New System.Drawing.Size(149, 23)
        Me.TxtChsNo.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 23)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "&Model Name"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(271, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 23)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "&Chs. #"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtYear
        '
        Me.TxtYear.BackColor = System.Drawing.Color.White
        Me.TxtYear.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_VAN1, "LUP_VANS.nMADE_YEAR", True))
        Me.TxtYear.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtYear.Location = New System.Drawing.Point(379, 49)
        Me.TxtYear.Name = "TxtYear"
        Me.TxtYear.Size = New System.Drawing.Size(149, 23)
        Me.TxtYear.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(271, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(102, 23)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "&Year (Made)*"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(274, 100)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(99, 23)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "In Use (Y/N)*"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(678, 45)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "VAN(s) DETAIL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     sPLATE_NO, sDESC, sMODEL, nMADE_YEAR, sENG_NO, sCH_NO, sCOLOR, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     " & _
            "                 CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STAT" & _
            "US" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         LUP_VANS"
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
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sPLATE_NO", System.Data.SqlDbType.VarChar, 0, "sPLATE_NO"), New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@sMODEL", System.Data.SqlDbType.VarChar, 0, "sMODEL"), New System.Data.SqlClient.SqlParameter("@nMADE_YEAR", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMADE_YEAR", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sENG_NO", System.Data.SqlDbType.VarChar, 0, "sENG_NO"), New System.Data.SqlClient.SqlParameter("@sCH_NO", System.Data.SqlDbType.VarChar, 0, "sCH_NO"), New System.Data.SqlClient.SqlParameter("@sCOLOR", System.Data.SqlDbType.VarChar, 0, "sCOLOR"), New System.Data.SqlClient.SqlParameter("@Original_sPLATE_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPLATE_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sMODEL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sMODEL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sMODEL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMODEL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMADE_YEAR", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMADE_YEAR", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sENG_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sENG_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sENG_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sENG_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCH_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCH_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCH_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCH_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCOLOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCOLOR", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCOLOR", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCOLOR", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = resources.GetString("SqlDeleteCommand1.CommandText")
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_sPLATE_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPLATE_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sMODEL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sMODEL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sMODEL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sMODEL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMADE_YEAR", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMADE_YEAR", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sENG_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sENG_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sENG_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sENG_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCH_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCH_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCH_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCH_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCOLOR", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCOLOR", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCOLOR", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCOLOR", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daLUP_VAN
        '
        Me.daLUP_VAN.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_VAN.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_VAN.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_VANS", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("sPLATE_NO", "sPLATE_NO"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC"), New System.Data.Common.DataColumnMapping("sMODEL", "sMODEL"), New System.Data.Common.DataColumnMapping("nMADE_YEAR", "nMADE_YEAR"), New System.Data.Common.DataColumnMapping("sENG_NO", "sENG_NO"), New System.Data.Common.DataColumnMapping("sCH_NO", "sCH_NO"), New System.Data.Common.DataColumnMapping("sCOLOR", "sCOLOR"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS")})})
        Me.daLUP_VAN.UpdateCommand = Me.SqlUpdateCommand1
        '
        'DsLUP_VAN11
        '
        Me.DsLUP_VAN11.DataSetName = "dsLUP_VAN1"
        Me.DsLUP_VAN11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_VAN
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(704, 395)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_VAN"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VAN"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsLUP_VAN1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_VAN11, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_VAN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Dim color As Color

        For Each color In System.ComponentModel.TypeDescriptor.GetConverter(GetType(Color)).GetStandardValues
            CmbColor.Items.Add(color.ToKnownColor)
        Next

        Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmLUP_VAN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtName.GotFocus, TxtPlateNo.GotFocus, TxtModel.GotFocus, TxtYear.GotFocus, TxtChsNo.GotFocus, TxtEngNo.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtName.LostFocus, TxtPlateNo.LostFocus, TxtModel.LostFocus, TxtYear.LostFocus, TxtChsNo.LostFocus, TxtEngNo.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtYear.KeyPress, TxtChsNo.KeyPress
        Me.asNum.NumPress(True, e)
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
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbColor.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbColor.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
        Me.CmbColor.SelectedIndex = Me.CmbColor.FindString(Me.CmbColor.Text)
    End Sub
    Private Sub CmbColor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbColor.SelectedIndexChanged
        Me.LblColor.BackColor = Color.FromName(Me.CmbColor.Text)
    End Sub
#End Region

#Region "DOMAIN_UPDOWN EVENTS"
    Private Sub DmnStatus_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnStatus.GotFocus
        CType(sender, DomainUpDown).BackColor = Color.LightSteelBlue
    End Sub
    Private Sub DmnStatus_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnStatus.LostFocus
        CType(sender, DomainUpDown).BackColor = Color.White
    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        Me.TxtPlateNo.Text = Me.ListView1.SelectedItems(0).Text
        If Not Me.TxtPlateNo.Text = Nothing Then
            Dim Str1 As String = "SELECT sPLATE_NO, sDESC, sMODEL, nMADE_YEAR, sENG_NO, sCH_NO, sCOLOR, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS FROM LUP_VANS WHERE sPLATE_NO='" & Me.TxtPlateNo.Text & "'"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_VAN = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_VAN1.Clear()
            Me.daLUP_VAN.Fill(Me.DsLUP_VAN1.LUP_VANS)

            Me.DmnStatus.SelectedItem = Me.DmnStatus.Text
        End If

FIX:
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(2).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM LUP_VANS WHERE sPLATE_NO='" & Me.ListView1.SelectedItems(0).Text & "'")

                Me.FillListView()

                Me.BttnNew_Click(sender, New System.EventArgs)
            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            Me.TxtName.Focus()
        End If

    End Sub
#End Region

#Region "Button Control"

    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.DsLUP_VAN1.Clear()

        Me.TxtPlateNo.Text = Nothing
        Me.TxtSearch.Text = Nothing
        Me.TxtPlateNo.Focus()
        Me.CmbColor.SelectedIndex = Me.CmbColor.FindString("White")
        Me.DmnStatus.SelectedIndex = 1
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM LUP_VANS WHERE sPLATE_NO='" & Me.TxtPlateNo.Text & "'")

        If Me.TxtPlateNo.Text = Nothing Or Me.TxtName.Text = Nothing Or Val(Me.TxtYear.Text) <= 0 Or Len(Me.TxtYear.Text) < 4 Or Me.DmnStatus.SelectedIndex = -1 Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtPlateNo.Text = Nothing Then
                Me.TxtPlateNo.Focus()

            ElseIf Me.TxtName.Text = Nothing Then
                Me.TxtName.Focus()

            ElseIf Val(Me.TxtYear.Text) <= 0 Or Len(Me.TxtYear.Text) < 4 Then
                Me.TxtYear.Focus()

            ElseIf Me.DmnStatus.SelectedIndex = -1 Then
                Me.DmnStatus.Focus()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            If MsgBox("Do you want to save '" & Me.TxtPlateNo.Text & "' & '" & Me.TxtName.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO LUP_VANS(sPLATE_NO, sDESC, sMODEL, nMADE_YEAR, sENG_NO, sCH_NO, sCOLOR, sSTATUS) VALUES('" & Me.TxtPlateNo.Text & "','" & Me.TxtName.Text & "','" & Me.TxtModel.Text & "'," & Val(Me.TxtYear.Text) & ",'" & Me.TxtEngNo.Text & "','" & Me.TxtChsNo.Text & "','" & Me.CmbColor.Text & "','" & Me.DmnStatus.SelectedIndex & "') ")

                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtPlateNo.Focus()
            End If

        ElseIf Me.asSELECT.pFlg1 = True Then
            If MsgBox("This Account no '" & Me.TxtPlateNo.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE LUP_VANS SET sDESC='" & Me.TxtName.Text & "', sMODEL='" & Me.TxtModel.Text & "', nMADE_YEAR=" & Val(Me.TxtYear.Text) & ", sENG_NO='" & Me.TxtEngNo.Text & "', sCH_NO='" & Me.TxtChsNo.Text & "', sCOLOR='" & Me.CmbColor.Text & "', sSTATUS='" & Me.DmnStatus.SelectedIndex & "' WHERE sPLATE_NO='" & Me.TxtPlateNo.Text & "'")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtPlateNo.Focus()
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        frmLUP_EMPLOYEE.MdiParent = Me.ParentForm
        frmLUP_EMPLOYEE.Show()
        frmLUP_EMPLOYEE.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_BUSINESS_GROUP.MdiParent = Me.ParentForm
        frmLUP_BUSINESS_GROUP.Show()
        frmLUP_BUSINESS_GROUP.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"

    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT sPLATE_NO, sDESC, sMODEL, nMADE_YEAR, sENG_NO, sCH_NO, sCOLOR, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS FROM LUP_VANS ORDER BY sDESC"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_VAN = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_VAN11.Clear()
            Me.daLUP_VAN.Fill(Me.DsLUP_VAN11.LUP_VANS)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_VAN11.LUP_VANS.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(3).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Colour As String = Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(6).ToString
                    If Not Colour = Nothing Then
                        .Add(Colour, Color.DarkBlue, Color.FromName(Colour), Me.ListView1.Font)
                    Else
                        .Add(Colour, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    End If


                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim Str1 As String = "SELECT sPLATE_NO, sDESC, sMODEL, nMADE_YEAR, sENG_NO, sCH_NO, sCOLOR, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS FROM LUP_VANS WHERE sDESC LIKE '%" & Me.TxtSearch.Text & "%' ORDER BY sDESC"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_VAN = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_VAN11.Clear()
            Me.daLUP_VAN.Fill(Me.DsLUP_VAN11.LUP_VANS)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_VAN11.LUP_VANS.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(3).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Colour As String = Me.DsLUP_VAN11.LUP_VANS.Item(Cnt).Item(6).ToString
                    If Not Colour = Nothing Then
                        .Add(Colour, Color.DarkBlue, Color.FromName(Colour), Me.ListView1.Font)
                    Else
                        .Add(Colour, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    End If

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

 
End Class
