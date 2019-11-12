Imports SDS = System.Data.SqlClient
Public Class frmRECOVERY
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtRecovery As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CmbGroup As MTGCComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalRecords As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents BttnSearch_Item As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DsLUP_BUSINESS_GROUP1 As Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
    Friend WithEvents daLUP_BUSINESS_GROUP As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand3 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_EMPLOYEE As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand2 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_EMPLOYEE1 As Neruo_Business_Solution.dsLUP_EMPLOYEE
    Friend WithEvents SqlConnection2 As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BttnAdd As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CmbEmployee As MTGCComboBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents BttnSearch_Inv As System.Windows.Forms.Button
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnPrev As System.Windows.Forms.Button
    Friend WithEvents BttnPrint As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents TxtTotCash As System.Windows.Forms.TextBox
    Friend WithEvents TxtTotCheque As System.Windows.Forms.TextBox
    Friend WithEvents TxtNetRecovery As System.Windows.Forms.TextBox
    Friend WithEvents TxtExpense As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents daLUP_BANK As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLUP_BANK1 As Neruo_Business_Solution.dsLUP_BANK
    Friend WithEvents CmbBankAccount As MTGCComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents daV_CLIENT_BAL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsV_CLIENT_BAL1 As Neruo_Business_Solution.dsV_CLIENT_BAL
    Friend WithEvents daV_RECOVERY_MASTER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand6 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsV_RECOVERY1 As Neruo_Business_Solution.dsV_RECOVERY
    Friend WithEvents daV_RECOVERY_DETAIL As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand5 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daNS_DEFAULT As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand8 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand9 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand10 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlCommand11 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsNS_DEFAULT1 As Neruo_Business_Solution.dsNS_DEFAULT
    Friend WithEvents ColCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPrevBal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCash As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCheque As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColChequeNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColChequeDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColTot_Rec As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNetBal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRemarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColSR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRECOVERY))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ColCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColPrevBal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCash = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColCheque = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColChequeNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColChequeDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColTot_Rec = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColNetBal = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColRemarks = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColSR = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TxtTotCheque = New System.Windows.Forms.TextBox
        Me.DsV_RECOVERY1 = New Neruo_Business_Solution.dsV_RECOVERY
        Me.TxtTotal = New System.Windows.Forms.TextBox
        Me.TxtNetRecovery = New System.Windows.Forms.TextBox
        Me.TxtTotCash = New System.Windows.Forms.TextBox
        Me.TxtExpense = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtTotalRecords = New System.Windows.Forms.TextBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.TxtRemarks = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnSearch_Inv = New System.Windows.Forms.Button
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnPrev = New System.Windows.Forms.Button
        Me.BttnPrint = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.BttnSearch_Item = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CmbBankAccount = New MTGCComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.BttnAdd = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtRecovery = New System.Windows.Forms.TextBox
        Me.CmbEmployee = New MTGCComboBox
        Me.CmbGroup = New MTGCComboBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daLUP_BUSINESS_GROUP = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand3 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_EMPLOYEE = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand2 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand4 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection2 = New System.Data.SqlClient.SqlConnection
        Me.Label3 = New System.Windows.Forms.Label
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SelectItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.daLUP_BANK = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daV_CLIENT_BAL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.daV_RECOVERY_MASTER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand6 = New System.Data.SqlClient.SqlCommand
        Me.daV_RECOVERY_DETAIL = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand5 = New System.Data.SqlClient.SqlCommand
        Me.DsLUP_BUSINESS_GROUP1 = New Neruo_Business_Solution.dsLUP_BUSINESS_GROUP
        Me.DsLUP_EMPLOYEE1 = New Neruo_Business_Solution.dsLUP_EMPLOYEE
        Me.DsLUP_BANK1 = New Neruo_Business_Solution.dsLUP_BANK
        Me.DsV_CLIENT_BAL1 = New Neruo_Business_Solution.dsV_CLIENT_BAL
        Me.daNS_DEFAULT = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand8 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand9 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand10 = New System.Data.SqlClient.SqlCommand
        Me.SqlCommand11 = New System.Data.SqlClient.SqlCommand
        Me.DsNS_DEFAULT1 = New Neruo_Business_Solution.dsNS_DEFAULT
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.DsV_RECOVERY1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsLUP_BANK1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_CLIENT_BAL1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsNS_DEFAULT1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.BackColor = System.Drawing.Color.Aquamarine
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Location = New System.Drawing.Point(10, 122)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(810, 232)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.LightSteelBlue
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeight = 22
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCode, Me.ColName, Me.ColPrevBal, Me.ColCash, Me.ColCheque, Me.ColChequeNo, Me.ColChequeDate, Me.ColTot_Rec, Me.ColNetBal, Me.ColRemarks, Me.ColSR})
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.HotTrack
        Me.DataGridView1.Location = New System.Drawing.Point(3, 16)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowTemplate.Height = 18
        Me.DataGridView1.Size = New System.Drawing.Size(804, 213)
        Me.DataGridView1.TabIndex = 0
        '
        'ColCode
        '
        DataGridViewCellStyle2.Format = "N0"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.ColCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColCode.Frozen = True
        Me.ColCode.HeaderText = "ID"
        Me.ColCode.Name = "ColCode"
        Me.ColCode.Width = 55
        '
        'ColName
        '
        Me.ColName.Frozen = True
        Me.ColName.HeaderText = "Customer Name"
        Me.ColName.Name = "ColName"
        Me.ColName.ReadOnly = True
        Me.ColName.Width = 200
        '
        'ColPrevBal
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "0.00"
        Me.ColPrevBal.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColPrevBal.HeaderText = "Prev. Bal"
        Me.ColPrevBal.Name = "ColPrevBal"
        Me.ColPrevBal.ReadOnly = True
        Me.ColPrevBal.Width = 70
        '
        'ColCash
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "C2"
        DataGridViewCellStyle4.NullValue = "0.00"
        Me.ColCash.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColCash.HeaderText = "Cash"
        Me.ColCash.Name = "ColCash"
        Me.ColCash.Width = 65
        '
        'ColCheque
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "C2"
        DataGridViewCellStyle5.NullValue = "0.00"
        Me.ColCheque.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColCheque.HeaderText = "Cheque"
        Me.ColCheque.Name = "ColCheque"
        Me.ColCheque.Width = 65
        '
        'ColChequeNo
        '
        Me.ColChequeNo.HeaderText = "Cheque No"
        Me.ColChequeNo.MaxInputLength = 50
        Me.ColChequeNo.Name = "ColChequeNo"
        Me.ColChequeNo.Width = 80
        '
        'ColChequeDate
        '
        DataGridViewCellStyle6.Format = "d"
        DataGridViewCellStyle6.NullValue = Nothing
        Me.ColChequeDate.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColChequeDate.HeaderText = "Chq Date"
        Me.ColChequeDate.Name = "ColChequeDate"
        Me.ColChequeDate.Width = 80
        '
        'ColTot_Rec
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = "0.00"
        Me.ColTot_Rec.DefaultCellStyle = DataGridViewCellStyle7
        Me.ColTot_Rec.HeaderText = "Total Recv."
        Me.ColTot_Rec.Name = "ColTot_Rec"
        Me.ColTot_Rec.ReadOnly = True
        Me.ColTot_Rec.Width = 80
        '
        'ColNetBal
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = "0.00"
        Me.ColNetBal.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColNetBal.HeaderText = "Net Bal."
        Me.ColNetBal.Name = "ColNetBal"
        Me.ColNetBal.ReadOnly = True
        Me.ColNetBal.Width = 80
        '
        'ColRemarks
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColRemarks.DefaultCellStyle = DataGridViewCellStyle9
        Me.ColRemarks.HeaderText = "Remarks"
        Me.ColRemarks.MaxInputLength = 100
        Me.ColRemarks.Name = "ColRemarks"
        '
        'ColSR
        '
        Me.ColSR.HeaderText = "SR"
        Me.ColSR.Name = "ColSR"
        Me.ColSR.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox4.BackColor = System.Drawing.Color.Tan
        Me.GroupBox4.Controls.Add(Me.TxtTotCheque)
        Me.GroupBox4.Controls.Add(Me.TxtTotal)
        Me.GroupBox4.Controls.Add(Me.TxtNetRecovery)
        Me.GroupBox4.Controls.Add(Me.TxtTotCash)
        Me.GroupBox4.Controls.Add(Me.TxtExpense)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Location = New System.Drawing.Point(603, 360)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(214, 186)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        '
        'TxtTotCheque
        '
        Me.TxtTotCheque.BackColor = System.Drawing.Color.White
        Me.TxtTotCheque.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_RECOVERY1, "V_RECOVERY_MASTER.TOT_CHQ", True))
        Me.TxtTotCheque.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtTotCheque.Location = New System.Drawing.Point(102, 51)
        Me.TxtTotCheque.MaxLength = 50
        Me.TxtTotCheque.Name = "TxtTotCheque"
        Me.TxtTotCheque.ReadOnly = True
        Me.TxtTotCheque.Size = New System.Drawing.Size(100, 21)
        Me.TxtTotCheque.TabIndex = 3
        Me.TxtTotCheque.TabStop = False
        Me.TxtTotCheque.Text = "0.00"
        Me.TxtTotCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DsV_RECOVERY1
        '
        Me.DsV_RECOVERY1.DataSetName = "dsV_RECOVERY"
        Me.DsV_RECOVERY1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtTotal
        '
        Me.TxtTotal.BackColor = System.Drawing.Color.White
        Me.TxtTotal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtTotal.Location = New System.Drawing.Point(102, 82)
        Me.TxtTotal.MaxLength = 50
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.ReadOnly = True
        Me.TxtTotal.Size = New System.Drawing.Size(100, 21)
        Me.TxtTotal.TabIndex = 5
        Me.TxtTotal.TabStop = False
        Me.TxtTotal.Text = "0.00"
        Me.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtNetRecovery
        '
        Me.TxtNetRecovery.BackColor = System.Drawing.Color.White
        Me.TxtNetRecovery.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtNetRecovery.Location = New System.Drawing.Point(102, 144)
        Me.TxtNetRecovery.MaxLength = 50
        Me.TxtNetRecovery.Name = "TxtNetRecovery"
        Me.TxtNetRecovery.ReadOnly = True
        Me.TxtNetRecovery.Size = New System.Drawing.Size(100, 21)
        Me.TxtNetRecovery.TabIndex = 9
        Me.TxtNetRecovery.TabStop = False
        Me.TxtNetRecovery.Text = "0.00"
        Me.TxtNetRecovery.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtTotCash
        '
        Me.TxtTotCash.BackColor = System.Drawing.Color.White
        Me.TxtTotCash.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_RECOVERY1, "V_RECOVERY_MASTER.TOT_CASH", True))
        Me.TxtTotCash.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtTotCash.Location = New System.Drawing.Point(102, 20)
        Me.TxtTotCash.MaxLength = 50
        Me.TxtTotCash.Name = "TxtTotCash"
        Me.TxtTotCash.ReadOnly = True
        Me.TxtTotCash.Size = New System.Drawing.Size(100, 21)
        Me.TxtTotCash.TabIndex = 1
        Me.TxtTotCash.TabStop = False
        Me.TxtTotCash.Text = "0.00"
        Me.TxtTotCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtExpense
        '
        Me.TxtExpense.BackColor = System.Drawing.Color.White
        Me.TxtExpense.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_RECOVERY1, "V_RECOVERY_MASTER.TOT_EXP", True))
        Me.TxtExpense.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtExpense.Location = New System.Drawing.Point(102, 113)
        Me.TxtExpense.MaxLength = 50
        Me.TxtExpense.Name = "TxtExpense"
        Me.TxtExpense.Size = New System.Drawing.Size(100, 21)
        Me.TxtExpense.TabIndex = 7
        Me.TxtExpense.Text = "0.00"
        Me.TxtExpense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 143)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 23)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Net Total"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Expense"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 81)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 23)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Gross Total"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 23)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Total Cheque"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 19)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(90, 23)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Total Cash"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(88, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(98, 21)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "<--Tot. Records"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtTotalRecords
        '
        Me.TxtTotalRecords.BackColor = System.Drawing.Color.White
        Me.TxtTotalRecords.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtTotalRecords.Location = New System.Drawing.Point(21, 4)
        Me.TxtTotalRecords.MaxLength = 50
        Me.TxtTotalRecords.Name = "TxtTotalRecords"
        Me.TxtTotalRecords.ReadOnly = True
        Me.TxtTotalRecords.Size = New System.Drawing.Size(62, 21)
        Me.TxtTotalRecords.TabIndex = 0
        Me.TxtTotalRecords.TabStop = False
        Me.TxtTotalRecords.Text = "0"
        Me.TxtTotalRecords.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox6.Controls.Add(Me.TxtRemarks)
        Me.GroupBox6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(378, 360)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(219, 189)
        Me.GroupBox6.TabIndex = 4
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_RECOVERY1, "V_RECOVERY_MASTER.REMARKS", True))
        Me.TxtRemarks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtRemarks.Location = New System.Drawing.Point(3, 19)
        Me.TxtRemarks.MaxLength = 100
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(213, 167)
        Me.TxtRemarks.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.BttnSearch_Inv)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.TxtTotalRecords)
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnPrev)
        Me.GroupBox1.Controls.Add(Me.BttnPrint)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Controls.Add(Me.BttnSearch_Item)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 360)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(362, 189)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'BttnSearch_Inv
        '
        Me.BttnSearch_Inv.BackColor = System.Drawing.Color.BurlyWood
        Me.BttnSearch_Inv.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch_Inv.Location = New System.Drawing.Point(143, 136)
        Me.BttnSearch_Inv.Name = "BttnSearch_Inv"
        Me.BttnSearch_Inv.Size = New System.Drawing.Size(75, 42)
        Me.BttnSearch_Inv.TabIndex = 6
        Me.BttnSearch_Inv.Text = "Sea&rch Recovery"
        Me.BttnSearch_Inv.UseVisualStyleBackColor = False
        '
        'BttnNew
        '
        Me.BttnNew.BackColor = System.Drawing.Color.LightBlue
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(58, 86)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(75, 31)
        Me.BttnNew.TabIndex = 3
        Me.BttnNew.Text = "&New"
        Me.BttnNew.UseVisualStyleBackColor = False
        '
        'BttnPrev
        '
        Me.BttnPrev.BackColor = System.Drawing.Color.DarkKhaki
        Me.BttnPrev.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrev.Enabled = False
        Me.BttnPrev.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrev.Location = New System.Drawing.Point(195, 35)
        Me.BttnPrev.Name = "BttnPrev"
        Me.BttnPrev.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrev.TabIndex = 4
        Me.BttnPrev.Text = "Pre&view"
        Me.BttnPrev.UseVisualStyleBackColor = False
        '
        'BttnPrint
        '
        Me.BttnPrint.BackColor = System.Drawing.Color.DarkKhaki
        Me.BttnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnPrint.Enabled = False
        Me.BttnPrint.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrint.Location = New System.Drawing.Point(97, 35)
        Me.BttnPrint.Name = "BttnPrint"
        Me.BttnPrint.Size = New System.Drawing.Size(75, 31)
        Me.BttnPrint.TabIndex = 5
        Me.BttnPrint.Text = "&Print"
        Me.BttnPrint.UseVisualStyleBackColor = False
        '
        'BttnClose
        '
        Me.BttnClose.BackColor = System.Drawing.Color.LightBlue
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(229, 86)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 31)
        Me.BttnClose.TabIndex = 8
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = False
        '
        'BttnSave
        '
        Me.BttnSave.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.BttnSave.Enabled = False
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(133, 67)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(95, 68)
        Me.BttnSave.TabIndex = 2
        Me.BttnSave.Text = "&Save"
        Me.BttnSave.UseVisualStyleBackColor = False
        '
        'BttnSearch_Item
        '
        Me.BttnSearch_Item.BackColor = System.Drawing.Color.BurlyWood
        Me.BttnSearch_Item.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch_Item.Location = New System.Drawing.Point(10, 125)
        Me.BttnSearch_Item.Name = "BttnSearch_Item"
        Me.BttnSearch_Item.Size = New System.Drawing.Size(75, 42)
        Me.BttnSearch_Item.TabIndex = 7
        Me.BttnSearch_Item.Text = "Sea&rch Item"
        Me.BttnSearch_Item.UseVisualStyleBackColor = False
        Me.BttnSearch_Item.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.BackColor = System.Drawing.Color.Pink
        Me.GroupBox3.Controls.Add(Me.CmbBankAccount)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.BttnAdd)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.TxtRecovery)
        Me.GroupBox3.Controls.Add(Me.CmbEmployee)
        Me.GroupBox3.Controls.Add(Me.CmbGroup)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(10, 44)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(810, 72)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'CmbBankAccount
        '
        Me.CmbBankAccount.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbBankAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbBankAccount.ColumnNum = 2
        Me.CmbBankAccount.ColumnWidth = "140;40"
        Me.CmbBankAccount.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_RECOVERY1, "V_RECOVERY_MASTER.ACCOUNT_NO", True))
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
        Me.CmbBankAccount.Location = New System.Drawing.Point(326, 41)
        Me.CmbBankAccount.ManagingFastMouseMoving = True
        Me.CmbBankAccount.ManagingFastMouseMovingInterval = 30
        Me.CmbBankAccount.Name = "CmbBankAccount"
        Me.CmbBankAccount.Size = New System.Drawing.Size(166, 22)
        Me.CmbBankAccount.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(217, 41)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 23)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "Bank Account"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BttnAdd
        '
        Me.BttnAdd.BackColor = System.Drawing.SystemColors.Control
        Me.BttnAdd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnAdd.Location = New System.Drawing.Point(178, 17)
        Me.BttnAdd.Name = "BttnAdd"
        Me.BttnAdd.Size = New System.Drawing.Size(33, 22)
        Me.BttnAdd.TabIndex = 2
        Me.BttnAdd.TabStop = False
        Me.BttnAdd.Text = "+&1"
        Me.BttnAdd.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 21)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Recovery #"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtRecovery
        '
        Me.TxtRecovery.BackColor = System.Drawing.Color.White
        Me.TxtRecovery.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtRecovery.Location = New System.Drawing.Point(91, 18)
        Me.TxtRecovery.MaxLength = 50
        Me.TxtRecovery.Name = "TxtRecovery"
        Me.TxtRecovery.ReadOnly = True
        Me.TxtRecovery.Size = New System.Drawing.Size(81, 21)
        Me.TxtRecovery.TabIndex = 1
        Me.TxtRecovery.TabStop = False
        '
        'CmbEmployee
        '
        Me.CmbEmployee.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbEmployee.ColumnNum = 3
        Me.CmbEmployee.ColumnWidth = "100;100;30"
        Me.CmbEmployee.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_RECOVERY1, "V_RECOVERY_MASTER.EMP_NAME", True))
        Me.CmbEmployee.DisplayMember = "Text"
        Me.CmbEmployee.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbEmployee.DropDownBackColor = System.Drawing.Color.Blue
        Me.CmbEmployee.DropDownForeColor = System.Drawing.Color.White
        Me.CmbEmployee.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.CmbEmployee.DropDownWidth = 340
        Me.CmbEmployee.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.CmbEmployee.GridLineColor = System.Drawing.Color.RosyBrown
        Me.CmbEmployee.GridLineHorizontal = False
        Me.CmbEmployee.GridLineVertical = True
        Me.CmbEmployee.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.CmbEmployee.Location = New System.Drawing.Point(91, 41)
        Me.CmbEmployee.ManagingFastMouseMoving = True
        Me.CmbEmployee.ManagingFastMouseMovingInterval = 30
        Me.CmbEmployee.Name = "CmbEmployee"
        Me.CmbEmployee.Size = New System.Drawing.Size(120, 22)
        Me.CmbEmployee.TabIndex = 8
        '
        'CmbGroup
        '
        Me.CmbGroup.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.CmbGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.CmbGroup.ColumnNum = 3
        Me.CmbGroup.ColumnWidth = "100;100;30"
        Me.CmbGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_RECOVERY1, "V_RECOVERY_MASTER.GROUP_NAME", True))
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
        Me.CmbGroup.Location = New System.Drawing.Point(326, 17)
        Me.CmbGroup.ManagingFastMouseMoving = True
        Me.CmbGroup.ManagingFastMouseMovingInterval = 30
        Me.CmbGroup.Name = "CmbGroup"
        Me.CmbGroup.Size = New System.Drawing.Size(166, 22)
        Me.CmbGroup.TabIndex = 4
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(6, 42)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(79, 21)
        Me.Label33.TabIndex = 7
        Me.Label33.Text = "Rec. Person"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(217, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 21)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Business Group"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(498, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Date"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_RECOVERY1, "V_RECOVERY_MASTER.dDATE", True))
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDate.Location = New System.Drawing.Point(546, 17)
        Me.TxtDate.MaxLength = 50
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(92, 21)
        Me.TxtDate.TabIndex = 6
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
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
        'daLUP_EMPLOYEE
        '
        Me.daLUP_EMPLOYEE.DeleteCommand = Me.SqlCommand1
        Me.daLUP_EMPLOYEE.SelectCommand = Me.SqlCommand2
        Me.daLUP_EMPLOYEE.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_EMPLOYEE_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("CODE", "CODE"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("FATHER_NAME", "FATHER_NAME"), New System.Data.Common.DataColumnMapping("NIC", "NIC"), New System.Data.Common.DataColumnMapping("HOME_PH", "HOME_PH"), New System.Data.Common.DataColumnMapping("CELL", "CELL"), New System.Data.Common.DataColumnMapping("PRE_ADD", "PRE_ADD"), New System.Data.Common.DataColumnMapping("PER_ADD", "PER_ADD"), New System.Data.Common.DataColumnMapping("DESIGNATION", "DESIGNATION"), New System.Data.Common.DataColumnMapping("APP_DATE", "APP_DATE"), New System.Data.Common.DataColumnMapping("PAY", "PAY"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("LEAVE_DATE", "LEAVE_DATE"), New System.Data.Common.DataColumnMapping("EMAIL_ADD", "EMAIL_ADD"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("BANK_ADD", "BANK_ADD")})})
        Me.daLUP_EMPLOYEE.UpdateCommand = Me.SqlCommand4
        '
        'SqlCommand1
        '
        Me.SqlCommand1.CommandText = "DELETE FROM LUP_CLIENT_GD WHERE (nCODE = @Original_nCODE) AND (nMAX_LIM = @Origin" & _
            "al_nMAX_LIM) AND (nMIN_LIM = @Original_nMIN_LIM) AND (sDESC = @Original_sDESC)"
        Me.SqlCommand1.Connection = Me.SqlConnection1
        Me.SqlCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand2
        '
        Me.SqlCommand2.CommandText = resources.GetString("SqlCommand2.CommandText")
        Me.SqlCommand2.Connection = Me.SqlConnection1
        '
        'SqlCommand4
        '
        Me.SqlCommand4.CommandText = resources.GetString("SqlCommand4.CommandText")
        Me.SqlCommand4.Connection = Me.SqlConnection1
        Me.SqlCommand4.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 50, "sDESC"), New System.Data.SqlClient.SqlParameter("@nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMAX_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMAX_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nMIN_LIM", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nMIN_LIM", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'SqlConnection2
        '
        Me.SqlConnection2.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection2.FireInfoMessageEventOnUserErrors = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(831, 43)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Recovery (Ugrai)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItemsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(831, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'ItemsToolStripMenuItem
        '
        Me.ItemsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectItemToolStripMenuItem})
        Me.ItemsToolStripMenuItem.Name = "ItemsToolStripMenuItem"
        Me.ItemsToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ItemsToolStripMenuItem.Text = "Items"
        Me.ItemsToolStripMenuItem.Visible = False
        '
        'SelectItemToolStripMenuItem
        '
        Me.SelectItemToolStripMenuItem.Name = "SelectItemToolStripMenuItem"
        Me.SelectItemToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.SelectItemToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.SelectItemToolStripMenuItem.Text = "Select Item"
        Me.SelectItemToolStripMenuItem.Visible = False
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
        'daV_CLIENT_BAL
        '
        Me.daV_CLIENT_BAL.SelectCommand = Me.SqlCommand7
        Me.daV_CLIENT_BAL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_CLIENT_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("CLIENT_BAL", "CLIENT_BAL")})})
        '
        'SqlCommand7
        '
        Me.SqlCommand7.CommandText = resources.GetString("SqlCommand7.CommandText")
        Me.SqlCommand7.Connection = Me.SqlConnection1
        '
        'daV_RECOVERY_MASTER
        '
        Me.daV_RECOVERY_MASTER.SelectCommand = Me.SqlCommand6
        Me.daV_RECOVERY_MASTER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_RECOVERY_MASTER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("EMP_NAME", "EMP_NAME"), New System.Data.Common.DataColumnMapping("TOT_CASH", "TOT_CASH"), New System.Data.Common.DataColumnMapping("TOT_CHQ", "TOT_CHQ"), New System.Data.Common.DataColumnMapping("TOT_EXP", "TOT_EXP"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("ACCOUNT_NO", "ACCOUNT_NO"), New System.Data.Common.DataColumnMapping("REMARKS", "REMARKS")})})
        '
        'SqlCommand6
        '
        Me.SqlCommand6.CommandText = resources.GetString("SqlCommand6.CommandText")
        Me.SqlCommand6.Connection = Me.SqlConnection1
        '
        'daV_RECOVERY_DETAIL
        '
        Me.daV_RECOVERY_DETAIL.SelectCommand = Me.SqlCommand5
        Me.daV_RECOVERY_DETAIL.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SV_CLIENT_BALANCE_TOT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("RECV_NO", "RECV_NO"), New System.Data.Common.DataColumnMapping("SHOP_AREA", "SHOP_AREA"), New System.Data.Common.DataColumnMapping("CASH_AMT", "CASH_AMT"), New System.Data.Common.DataColumnMapping("CHQ_NO", "CHQ_NO"), New System.Data.Common.DataColumnMapping("CHQ_DATE", "CHQ_DATE"), New System.Data.Common.DataColumnMapping("CHQ_AMT", "CHQ_AMT"), New System.Data.Common.DataColumnMapping("REMARKS", "REMARKS"), New System.Data.Common.DataColumnMapping("CLIENT_BAL", "CLIENT_BAL"), New System.Data.Common.DataColumnMapping("CLIENT_CODE", "CLIENT_CODE")})})
        '
        'SqlCommand5
        '
        Me.SqlCommand5.CommandText = resources.GetString("SqlCommand5.CommandText")
        Me.SqlCommand5.Connection = Me.SqlConnection1
        '
        'DsLUP_BUSINESS_GROUP1
        '
        Me.DsLUP_BUSINESS_GROUP1.DataSetName = "dsLUP_BUSINESS_GROUP"
        Me.DsLUP_BUSINESS_GROUP1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_EMPLOYEE1
        '
        Me.DsLUP_EMPLOYEE1.DataSetName = "dsLUP_EMPLOYEE"
        Me.DsLUP_EMPLOYEE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsLUP_BANK1
        '
        Me.DsLUP_BANK1.DataSetName = "dsLUP_BANK"
        Me.DsLUP_BANK1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_BANK1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'DsV_CLIENT_BAL1
        '
        Me.DsV_CLIENT_BAL1.DataSetName = "dsV_CLIENT_BAL"
        Me.DsV_CLIENT_BAL1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'daNS_DEFAULT
        '
        Me.daNS_DEFAULT.DeleteCommand = Me.SqlCommand8
        Me.daNS_DEFAULT.InsertCommand = Me.SqlCommand9
        Me.daNS_DEFAULT.SelectCommand = Me.SqlCommand10
        Me.daNS_DEFAULT.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "NS_DEFAULT", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("GROUP", "GROUP"), New System.Data.Common.DataColumnMapping("BANK_ACC", "BANK_ACC"), New System.Data.Common.DataColumnMapping("S_MAN", "S_MAN"), New System.Data.Common.DataColumnMapping("P_MAN", "P_MAN"), New System.Data.Common.DataColumnMapping("D_MAN", "D_MAN"), New System.Data.Common.DataColumnMapping("R_MAN", "R_MAN"), New System.Data.Common.DataColumnMapping("CLIENT", "CLIENT"), New System.Data.Common.DataColumnMapping("CLIENT_TYPE", "CLIENT_TYPE"), New System.Data.Common.DataColumnMapping("CLIENT_CAT", "CLIENT_CAT"), New System.Data.Common.DataColumnMapping("CLIENT_GD", "CLIENT_GD"), New System.Data.Common.DataColumnMapping("ZONE", "ZONE"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("EXP_SUB_HEAD", "EXP_SUB_HEAD"), New System.Data.Common.DataColumnMapping("PRINTER", "PRINTER"), New System.Data.Common.DataColumnMapping("RPT_TITLE", "RPT_TITLE"), New System.Data.Common.DataColumnMapping("RPT_WARRANTY", "RPT_WARRANTY")})})
        Me.daNS_DEFAULT.UpdateCommand = Me.SqlCommand11
        '
        'SqlCommand8
        '
        Me.SqlCommand8.CommandText = resources.GetString("SqlCommand8.CommandText")
        Me.SqlCommand8.Connection = Me.SqlConnection1
        Me.SqlCommand8.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_GROUP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_GROUP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_BANK_ACC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_BANK_ACC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_S_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_S_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_P_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_P_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_D_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_D_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_R_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_R_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_CAT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_GD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_GD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ZONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ZONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ROUTE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ROUTE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_AREA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_AREA", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_EXP_SUB_HEAD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_PRINTER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_PRINTER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_TITLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_TITLE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_WARRANTY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlCommand9
        '
        Me.SqlCommand9.CommandText = resources.GetString("SqlCommand9.CommandText")
        Me.SqlCommand9.Connection = Me.SqlConnection1
        Me.SqlCommand9.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@GROUP", System.Data.SqlDbType.VarChar, 0, "GROUP"), New System.Data.SqlClient.SqlParameter("@BANK_ACC", System.Data.SqlDbType.VarChar, 0, "BANK_ACC"), New System.Data.SqlClient.SqlParameter("@S_MAN", System.Data.SqlDbType.VarChar, 0, "S_MAN"), New System.Data.SqlClient.SqlParameter("@P_MAN", System.Data.SqlDbType.VarChar, 0, "P_MAN"), New System.Data.SqlClient.SqlParameter("@D_MAN", System.Data.SqlDbType.VarChar, 0, "D_MAN"), New System.Data.SqlClient.SqlParameter("@R_MAN", System.Data.SqlDbType.VarChar, 0, "R_MAN"), New System.Data.SqlClient.SqlParameter("@CLIENT", System.Data.SqlDbType.VarChar, 0, "CLIENT"), New System.Data.SqlClient.SqlParameter("@CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, "CLIENT_TYPE"), New System.Data.SqlClient.SqlParameter("@CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, "CLIENT_CAT"), New System.Data.SqlClient.SqlParameter("@CLIENT_GD", System.Data.SqlDbType.VarChar, 0, "CLIENT_GD"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 0, "ZONE"), New System.Data.SqlClient.SqlParameter("@ROUTE", System.Data.SqlDbType.VarChar, 0, "ROUTE"), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 0, "AREA"), New System.Data.SqlClient.SqlParameter("@EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, "EXP_SUB_HEAD"), New System.Data.SqlClient.SqlParameter("@PRINTER", System.Data.SqlDbType.VarChar, 0, "PRINTER"), New System.Data.SqlClient.SqlParameter("@RPT_TITLE", System.Data.SqlDbType.VarChar, 0, "RPT_TITLE"), New System.Data.SqlClient.SqlParameter("@RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, "RPT_WARRANTY"), New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing)})
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
        Me.SqlCommand11.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@GROUP", System.Data.SqlDbType.VarChar, 0, "GROUP"), New System.Data.SqlClient.SqlParameter("@BANK_ACC", System.Data.SqlDbType.VarChar, 0, "BANK_ACC"), New System.Data.SqlClient.SqlParameter("@S_MAN", System.Data.SqlDbType.VarChar, 0, "S_MAN"), New System.Data.SqlClient.SqlParameter("@P_MAN", System.Data.SqlDbType.VarChar, 0, "P_MAN"), New System.Data.SqlClient.SqlParameter("@D_MAN", System.Data.SqlDbType.VarChar, 0, "D_MAN"), New System.Data.SqlClient.SqlParameter("@R_MAN", System.Data.SqlDbType.VarChar, 0, "R_MAN"), New System.Data.SqlClient.SqlParameter("@CLIENT", System.Data.SqlDbType.VarChar, 0, "CLIENT"), New System.Data.SqlClient.SqlParameter("@CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, "CLIENT_TYPE"), New System.Data.SqlClient.SqlParameter("@CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, "CLIENT_CAT"), New System.Data.SqlClient.SqlParameter("@CLIENT_GD", System.Data.SqlDbType.VarChar, 0, "CLIENT_GD"), New System.Data.SqlClient.SqlParameter("@ZONE", System.Data.SqlDbType.VarChar, 0, "ZONE"), New System.Data.SqlClient.SqlParameter("@ROUTE", System.Data.SqlDbType.VarChar, 0, "ROUTE"), New System.Data.SqlClient.SqlParameter("@AREA", System.Data.SqlDbType.VarChar, 0, "AREA"), New System.Data.SqlClient.SqlParameter("@EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, "EXP_SUB_HEAD"), New System.Data.SqlClient.SqlParameter("@PRINTER", System.Data.SqlDbType.VarChar, 0, "PRINTER"), New System.Data.SqlClient.SqlParameter("@RPT_TITLE", System.Data.SqlDbType.VarChar, 0, "RPT_TITLE"), New System.Data.SqlClient.SqlParameter("@RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, "RPT_WARRANTY"), New System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_GROUP", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_GROUP", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "GROUP", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_BANK_ACC", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_BANK_ACC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "BANK_ACC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_S_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_S_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "S_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_P_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_P_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "P_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_D_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_D_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "D_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_R_MAN", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_R_MAN", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "R_MAN", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_TYPE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_TYPE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_TYPE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_CAT", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_CAT", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_CAT", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_CLIENT_GD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_CLIENT_GD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "CLIENT_GD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ZONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ZONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ZONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_ROUTE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_ROUTE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "ROUTE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_AREA", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_AREA", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "AREA", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_EXP_SUB_HEAD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_EXP_SUB_HEAD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "EXP_SUB_HEAD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_PRINTER", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_PRINTER", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "PRINTER", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_TITLE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_TITLE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_TITLE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_RPT_WARRANTY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_RPT_WARRANTY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "RPT_WARRANTY", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "ID", System.Data.DataRowVersion.Current, Nothing)})
        '
        'DsNS_DEFAULT1
        '
        Me.DsNS_DEFAULT1.DataSetName = "dsNS_DEFAULT"
        Me.DsNS_DEFAULT1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmRECOVERY
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ClientSize = New System.Drawing.Size(831, 568)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmRECOVERY"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RECOVERY (Ugrai)"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.DsV_RECOVERY1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DsLUP_BUSINESS_GROUP1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_EMPLOYEE1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsLUP_BANK1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_CLIENT_BAL1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsNS_DEFAULT1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Public Search_Inv As Boolean = False

#End Region

#Region "FORM CONTROL"
    Private Sub frmRECOVERY_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.BttnSave.Text = "&Update" And Me.BttnSave.Enabled = True Then
            MsgBox("Can't close without Updating OR Cancelling Recovery", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        ElseIf Me.BttnSave.Text = "&Save" And Me.BttnSave.Enabled = True Then
            MsgBox("Can't close without Saving OR Cancelling Recovery", MsgBoxStyle.Exclamation, "(NS) - Closing Error!")
            e.Cancel = True

        End If
    End Sub
    Private Sub frmRECOVERY_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Employee()
        Me.FillComboBox_Group()
        Me.FillComboBox_BankAccount()

        Dim StrSql As String = "SELECT nID AS ID, sBUSINESS_GP AS [GROUP], sBANK_ACC AS BANK_ACC, sS_MAN AS S_MAN, sP_MAN AS P_MAN, sD_MAN AS D_MAN, sR_MAN AS R_MAN, sCLIENT AS CLIENT, sCLIENT_TYPE AS CLIENT_TYPE, sCLIENT_CAT AS CLIENT_CAT, sCLIENT_GD AS CLIENT_GD, sZONE AS ZONE, sROUTE AS ROUTE, sAREA AS AREA, sEXP_SUB_HEAD AS EXP_SUB_HEAD, sPRINTER AS PRINTER, sREPORT_TITLE AS RPT_TITLE, sREPORT_WARRANTY AS RPT_WARRANTY FROM NS_DEFAULT"
        Dim CmdSql As New SDS.SqlCommand(StrSql, Me.SqlConnection1)
        Me.daNS_DEFAULT = New SDS.SqlDataAdapter(CmdSql)
        Me.daNS_DEFAULT.Fill(Me.DsNS_DEFAULT1.NS_DEFAULT)
        Me.Default_Setting()

        Me.Disable_All()
        Me.BttnPrev.Enabled = False
        Me.BttnPrint.Enabled = False
        Me.BttnSave.Enabled = False

        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        'Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmRECOVERY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtRecovery.GotFocus, TxtDate.GotFocus, TxtTotal.GotFocus, TxtTotalRecords.GotFocus, TxtRemarks.GotFocus, TxtTotCash.GotFocus, TxtTotCheque.GotFocus, TxtExpense.GotFocus, TxtNetRecovery.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtRecovery.LostFocus, TxtDate.LostFocus, TxtTotal.LostFocus, TxtTotalRecords.LostFocus, TxtRemarks.LostFocus, TxtTotCash.LostFocus, TxtTotCheque.LostFocus, TxtExpense.LostFocus, TxtNetRecovery.LostFocus
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
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTotalRecords.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTotal.KeyPress, TxtExpense.KeyPress, TxtNetRecovery.KeyPress, TxtTotCash.KeyPress, TxtTotCheque.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    Private Sub TxtExpense_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtExpense.TextChanged
        Me.TxtNetRecovery.Text = Val(Me.TxtTotal.Text) - Val(Me.TxtExpense.Text)
    End Sub
    Private Sub TxtTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTotal.TextChanged
        Me.TxtNetRecovery.Text = Val(Me.TxtTotal.Text) - Val(Me.TxtExpense.Text)
    End Sub

    'Fill data for Modification
    Private Sub TxtRecovery_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtRecovery.TextChanged
        If Me.Search_Inv = True Then

            If Not Me.TxtRecovery.Text = Nothing Then
                'FILL MASTER RECORD
                Me.Fill_Master_Data()

                ''FILL DETAIL RECORD
                'Me.Fill_Detail_Data()

                Dim i As Integer
                Me.TxtTotCash.Text = "0.00"
                Me.TxtTotCheque.Text = "0.00"

                For i = 0 To Me.DataGridView1.Rows.Count - 1
                    Me.TxtTotCash.Text = Val(Me.TxtTotCash.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value)
                    Me.TxtTotCheque.Text = Val(Me.TxtTotCheque.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value)
                    'Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTot_Rec").Value)
                Next

                Me.TxtTotal.Text = "0.00"
                Me.TxtTotal.Text = Val(Me.TxtTotCash.Text) + Val(Me.TxtTotCheque.Text)


                Me.TxtTotalRecords.Text = Me.DataGridView1.Rows.Count - 1

                Me.TxtTotal.Text = Val(Me.TxtTotCash.Text) + Val(Me.TxtTotCheque.Text)
                Me.TxtNetRecovery.Text = Val(Me.TxtTotal.Text) - Val(Me.TxtExpense.Text)

                Dim StrCmb As String = Me.CmbEmployee.Text
                Me.CmbEmployee.SelectedIndex = -1
                Me.CmbEmployee.SelectedIndex = Me.CmbEmployee.FindString(StrCmb)

                StrCmb = Me.CmbGroup.Text
                Me.CmbGroup.SelectedIndex = -1
                Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCmb)

                Me.Search_Inv = False
            End If

        End If

    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbGroup.GotFocus, CmbEmployee.GotFocus, CmbBankAccount.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbGroup.LostFocus, CmbEmployee.LostFocus, CmbBankAccount.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "Select Client Controls"
    Private Sub SelectItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectItemToolStripMenuItem.Click
        On Error GoTo Fix
        frmSELECT_CLIENT.TxtArea.Text = Nothing
        frmSELECT_CLIENT.TxtClient.Text = Nothing
        frmSELECT_CLIENT.Row1 = Me.DataGridView1.CurrentRow.Index

        frmSELECT_CLIENT.ShowDialog(Me)
Fix:
    End Sub
#End Region

#Region "DataGridView Control"

    Private Sub DataGridView1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave

        Try
            'MsgBox(e.ColumnIndex & "  " & e.RowIndex)
            If e.ColumnIndex = 6 Then
                If Not Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCheque").Value) <= 0 And Not Me.DataGridView1.Rows(e.RowIndex).Cells("ColChequeDate").Value Is Nothing Then
                    Try
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColChequeDate").Value = CDate(Me.DataGridView1.Rows(e.RowIndex).Cells("ColChequeDate").Value).ToString("dd-MMM-yyyy")
                    Catch ex As Exception
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColChequeDate").Value = Date.Now.ToString("dd-MMM-yyyy")
                        Exit Sub
                    End Try
                End If

                If Not Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCheque").Value) <= 0 And Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColChequeDate").Value Is Nothing Then
                    Try
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColChequeDate").Value = CDate(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColChequeDate").Value).ToString("dd-MMM-yyyy")
                    Catch ex As Exception
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColChequeDate").Value = Date.Now.ToString("dd-MMM-yyyy")
                        Exit Sub
                    End Try
                End If

                If Not Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCheque").Value) = 0 And Me.DataGridView1.Rows(e.RowIndex).Cells("ColChequeDate").Value Is Nothing Then
                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColChequeDate").Value = Date.Now.ToString("dd-MMM-yyyy")
                    Exit Sub
                End If

                If Not Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCheque").Value) = 0 And Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColChequeDate").Value Is Nothing Then
                    Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColChequeDate").Value = Date.Now.ToString("dd-MMM-yyyy")
                    Exit Sub
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If Me.Search_Inv = False Then

            Try
                'Me.DsV_CLIENT_BAL1.Clear()

                If Not Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value Is Nothing Then
                    Dim Str1 As String = "SELECT V_CLIENT_INFO.ID, V_CLIENT_INFO.SHOP_NAME + ' (' + V_CLIENT_INFO.AREA + ')' AS SHOP_NAME, CONVERT(NUMERIC(18, 0), SV_CLIENT_BALANCE_TOT.CLIENT_BAL) AS CLIENT_BAL FROM SV_CLIENT_BALANCE_TOT INNER JOIN V_CLIENT_INFO ON SV_CLIENT_BALANCE_TOT.CLIENT_ID = V_CLIENT_INFO.ID WHERE V_CLIENT_INFO.ID=" & Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value) & ""
                    Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
                    Me.daV_CLIENT_BAL = New SDS.SqlDataAdapter(SqlCmd1)

                    Me.DsV_CLIENT_BAL1.Clear()
                    Me.daV_CLIENT_BAL.Fill(Me.DsV_CLIENT_BAL1.SV_CLIENT_BALANCE_TOT)

                    If Me.DsV_CLIENT_BAL1.SV_CLIENT_BALANCE_TOT.Count = 0 Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColCode").Value = Nothing
                        'SET FOCUS TO ColCode IS PENDING

                        Me.SelectItemToolStripMenuItem_Click(sender, e)

                        Exit Sub
                    Else
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColName").Value = Me.DsV_CLIENT_BAL1.SV_CLIENT_BALANCE_TOT.Item(0).Item(1).ToString()
                        Me.DataGridView1.Rows(e.RowIndex).Cells("ColPrevBal").Value = Me.DsV_CLIENT_BAL1.SV_CLIENT_BALANCE_TOT.Item(0).Item(2).ToString()

                    End If

                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColTot_Rec").Value = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCash").Value) + Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColCheque").Value)
                    Me.DataGridView1.Rows(e.RowIndex).Cells("ColNetBal").Value = Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColPrevBal").Value) - Val(Me.DataGridView1.Rows(e.RowIndex).Cells("ColTot_Rec").Value)

                    Dim i As Integer
                    Me.TxtTotCash.Text = "0.00"
                    Me.TxtTotCheque.Text = "0.00"

                    For i = 0 To Me.DataGridView1.Rows.Count - 1
                        Me.TxtTotCash.Text = Val(Me.TxtTotCash.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value)
                        Me.TxtTotCheque.Text = Val(Me.TxtTotCheque.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value)
                        'Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTot_Rec").Value)
                    Next

                    'If Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCode").Value Is Nothing Or (Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCash").Value Is Nothing And Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCheque").Value Is Nothing) Then
                    '    Me.DataGridView1.Rows(e.RowIndex - 1).Cells("ColCode").Selected = True
                    '    Exit Sub
                    'End If

                End If

            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try

            Me.TxtTotal.Text = "0.00"
            Me.TxtTotal.Text = Val(Me.TxtTotCash.Text) + Val(Me.TxtTotCheque.Text)

            Me.TxtTotalRecords.Text = Me.DataGridView1.Rows.Count - 1

        End If



    End Sub

    Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        Me.DataGridView1_CellValueChanged(sender, e)
    End Sub
    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        Dim i As Integer
        Me.TxtTotCash.Text = "0.00"
        Me.TxtTotCheque.Text = "0.00"

        For i = 0 To Me.DataGridView1.Rows.Count - 1
            Me.TxtTotCash.Text = Val(Me.TxtTotCash.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value)
            Me.TxtTotCheque.Text = Val(Me.TxtTotCheque.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value)
            'Me.TxtTotal.Text = Val(Me.TxtTotal.Text) + Val(Me.DataGridView1.Rows(i).Cells("ColTot_Rec").Value)
        Next

        Me.TxtTotal.Text = "0.00"
        Me.TxtTotal.Text = Val(Me.TxtTotCash.Text) + Val(Me.TxtTotCheque.Text)

    End Sub
    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        If Me.TxtRecovery.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

            Me.Null_Focus()

        ElseIf MsgBox("Are you sure to Delete Item?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Deleting Record?") = MsgBoxResult.Yes Then
            If Not Val(Me.DataGridView1.Rows(e.Row.Index).Cells("ColSR").Value) = 0 Then
                'Try
                'DELETE FROM RECOVERY DETAIL
                Me.asDelete.DeleteValue_NoErr("DELETE FROM RECOVERY_DETAIL WHERE nID=" & Val(Me.DataGridView1.Rows(e.Row.Index).Cells("ColSR").Value) & "")

                'UPDATE VALUE IN RECOVERY MASTER
                If Me.CmbBankAccount.SelectedIndex = -1 Or Me.CmbBankAccount.Text = Nothing Then
                    Me.asUpdate.UpdateValue_NoErr("UPDATE RECOVERY_MASTER SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nTOTAL_CASH=" & Val(Me.TxtTotCash.Text) & ", nTOTAL_CHEQUE=" & Val(Me.TxtTotCheque.Text) & ", nTOTAL_EXPENSE=" & Val(Me.TxtExpense.Text) & ", nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sACCOUNT_CODE=NULL, sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nID=" & Val(Me.TxtRecovery.Text) & "")
                Else
                    Me.asUpdate.UpdateValue_NoErr("UPDATE RECOVERY_MASTER SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nTOTAL_CASH=" & Val(Me.TxtTotCash.Text) & ", nTOTAL_CHEQUE=" & Val(Me.TxtTotCheque.Text) & ", nTOTAL_EXPENSE=" & Val(Me.TxtExpense.Text) & ", nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nID=" & Val(Me.TxtRecovery.Text) & "")
                End If


                'Me.AsConn.Conn.Open()
                ''Me.AsConn.Cmd.Connection = Me.AsConn.Conn

                ''Me.AsConn.Cmd.CommandText = "UPDATE RECOVERY_MASTER SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nTOTAL_CASH=" & Val(Me.TxtTotCash.Text) & ", nTOTAL_CHEQUE=" & Val(Me.TxtTotCheque.Text) & ", nTOTAL_EXPENSE=" & Val(Me.TxtExpense.Text) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nID=" & Val(Me.TxtRecovery.Text) & ""

                ''Me.AsConn.Cmd.CommandType = CommandType.Text

                ''Me.AsConn.Cmd.ExecuteNonQuery()

                'Dim SqlCmd As String
                'If Me.CmbBankAccount.Text = Nothing Or Me.CmbBankAccount.SelectedIndex = -1 Then
                '    SqlCmd = "UPDATE RECOVERY_MASTER SET dDATE=@dDATE, nEMPLOYEE_CODE=@nEMPLOYEE_CODE, nTOTAL_CASH=@nTOTAL_CASH, nTOTAL_CHEQUE=@nTOTAL_CHEQUE, nTOTAL_EXPENSE=@nTOTAL_EXPENSE, nLOGIN_ID=@nLOGIN_ID, nBUSINESS_CODE=@nBUSINESS_CODE, sACCOUNT_CODE=null, sREMARKS=@sREMARKS WHERE nID=" & Val(Me.TxtRecovery.Text) & ""
                'Else
                '    SqlCmd = "UPDATE RECOVERY_MASTER SET dDATE=@dDATE, nEMPLOYEE_CODE=@nEMPLOYEE_CODE, nTOTAL_CASH=@nTOTAL_CASH, nTOTAL_CHEQUE=@nTOTAL_CHEQUE, nTOTAL_EXPENSE=@nTOTAL_EXPENSE, nLOGIN_ID=@nLOGIN_ID, nBUSINESS_CODE=@nBUSINESS_CODE, sACCOUNT_CODE=@sACCOUNT_CODE, sREMARKS=@sREMARKS WHERE nID=" & Val(Me.TxtRecovery.Text) & ""
                'End If

                'Dim CmdRecovery As New System.Data.SqlClient.SqlCommand(SqlCmd, Me.AsConn.Conn)

                'With CmdRecovery.Parameters
                '    .Add("@dDATE", SqlDbType.DateTime).Value = CDate(Me.TxtDate.Text)
                '    .Add("@nEMPLOYEE_CODE", SqlDbType.VarChar).Value = Me.CmbEmployee.SelectedItem.Col3
                '    .Add("@nTOTAL_CASH", SqlDbType.Money).Value = Val(Me.TxtTotCash.Text)
                '    .Add("@nTOTAL_CHEQUE", SqlDbType.Money).Value = Val(Me.TxtTotCheque.Text)
                '    .Add("@nTOTAL_EXPENSE", SqlDbType.Money).Value = Val(Me.TxtExpense.Text)
                '    .Add("@nLOGIN_ID", SqlDbType.VarChar).Value = 10
                '    .Add("@nBUSINESS_CODE", SqlDbType.VarChar).Value = Me.CmbGroup.SelectedItem.Col3
                '    If Me.CmbBankAccount.Text = Nothing Or Me.CmbBankAccount.SelectedIndex = -1 Then
                '        '.Add("@sACCOUNT_CODE", SqlDbType.VarChar).Value = Nothing
                '    Else
                '        .Add("@sACCOUNT_CODE", SqlDbType.VarChar).Value = Me.CmbBankAccount.SelectedItem.Col1
                '    End If
                '    .Add("@sREMARKS", SqlDbType.VarChar).Value = Me.TxtRemarks.Text
                'End With

                'CmdRecovery.ExecuteNonQuery()



                'Dim AsUpdate1 As AssUpdate = New AssUpdate
                'AsUpdate1.UpdateValue_NoErr("UPDATE RECOVERY_MASTER SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nTOTAL_CASH=" & Val(Me.TxtTotCash.Text) & ", nTOTAL_CHEQUE=" & Val(Me.TxtTotCheque.Text) & ", nTOTAL_EXPENSE=" & Val(Me.TxtExpense.Text) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nID=" & Val(Me.TxtRecovery.Text) & "")

                Me.BttnNew.Enabled = False
                'Catch ex As Exception
                '    MsgBox(ex.Message)

                'Finally
                '    Me.AsConn.Conn.Close()

                'End Try
            End If


        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        If Asc(e.KeyChar) = Keys.Escape Then
            If Me.BttnNew.Text = "Ca&ncel" Then
                Me.BttnNew_Click(sender, e)
            Else
                Me.BttnClose_Click(sender, e)
            End If

            'ElseIf Asc(e.KeyChar) = Keys.Return Then
            '    '    'MsgBox(Me.DataGridView1.CurrentCell.ColumnIndex)
            '    '    Me.DataGridView1.CurrentCell = Me.DataGridView1(Me.DataGridView1.CurrentCell.ColumnIndex + 1, Me.DataGridView1.CurrentCell.RowIndex - 1)
            '    Dim x, y As Integer
            '    x = Me.DataGridView1.CurrentCell.ColumnIndex
            '    y = Me.DataGridView1.CurrentCell.RowIndex
            '    If Me.DataGridView1.CurrentCell.ColumnIndex = 1 Then
            '        Me.DataGridView1.CurrentCell = Me.DataGridView1(2, y - 1)
            '    Else
            '        Me.DataGridView1.CurrentCell = Me.DataGridView1(x, y - 1)
            '    End If

        End If

    End Sub
    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        'If e.KeyValue = Keys.Return Then
        '    Dim x, y As Integer
        '    x = Me.DataGridView1.CurrentCell.ColumnIndex
        '    y = Me.DataGridView1.CurrentCell.RowIndex

        '    Me.DataGridView1.CurrentCell = Me.DataGridView1(x, y - 1)


        'End If
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAdd.Click
        Me.TxtRecovery.Text = Val(Me.TxtRecovery.Text) + 1
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        If Me.BttnNew.Text = "&New" Then
            Me.Enable_All()

            Me.BttnSearch_Inv.Enabled = False
            Me.BttnPrev.Enabled = False
            Me.BttnPrint.Enabled = False
            Me.BttnSave.Enabled = True
            Me.BttnSave.Text = "&Save"
            Me.BttnClose.Enabled = False

            Me.CancelButton = Me.BttnNew

            Me.Clear_All()
            Me.BttnNew.Text = "Ca&ncel"

            Me.TxtRecovery.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nID) FROM RECOVERY_MASTER") + 1

        ElseIf Me.BttnNew.Text = "Ca&ncel" Then
            If MsgBox("Are you sure to Cancel this Recovery?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Cancel Recovery?") = MsgBoxResult.Yes Then
                Me.Disable_All()

                Me.BttnPrev.Enabled = False
                Me.BttnPrint.Enabled = False
                Me.BttnSave.Enabled = False
                Me.BttnClose.Enabled = True
                Me.BttnSearch_Inv.Enabled = True
                Me.BttnSave.Text = "&Save"

                Me.CancelButton = Me.BttnClose

                Me.Search_Inv = False

                Me.Clear_All()
                Me.BttnNew.Text = "&New"
            End If
        End If
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM RECOVERY_MASTER WHERE nID=" & Val(Me.TxtRecovery.Text) & "")

        If Me.BttnSave.Text = "&Save" Then

            If Me.TxtRecovery.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")

                Me.Null_Focus()

            ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) <= 0 Then
                MsgBox("Please enter atleast one Item to save Recovery!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.DataGridView1.Focus()

            ElseIf Me.asSELECT.pFlg1 = False Then
                If Val(Me.TxtNetRecovery.Text) < 0 Then
                    MsgBox("Can't save!" & vbCrLf & "Net Total less than Zero(0)", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                    Me.TxtExpense.Focus()

                Else
                    'INSERT VALUES IN RECOVERY MASTER
                    Me.asInsert.SaveValue("INSERT INTO RECOVERY_MASTER(nID, dDATE, nEMPLOYEE_CODE, nTOTAL_CASH, nTOTAL_CHEQUE, nTOTAL_EXPENSE, nLOGIN_ID, nBUSINESS_CODE, sACCOUNT_CODE, sREMARKS) VALUES(" & Val(Me.TxtRecovery.Text) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.CmbEmployee.SelectedItem.Col3) & ", " & Val(Me.TxtTotCash.Text) & ", " & Val(Me.TxtTotCheque.Text) & ", " & Val(Me.TxtExpense.Text) & ", 10, " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & Me.CmbBankAccount.Text & "', '" & Me.TxtRemarks.Text & "')")

                    Dim i As Integer
                    For i = 0 To Me.DataGridView1.Rows.Count - 2

                        'INSERT VALUES WITH MSGBOX
                        If i = Me.DataGridView1.Rows.Count - 2 Then
                            'INSERT VALUES IN RECOVERY DETAIL
                            If Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value Is Nothing Then
                                Me.asInsert.SaveValueIN("INSERT INTO RECOVERY_DETAIL(nRECOVERY_ID, nCLIENT_CODE, nCASH_AMOUNT, sREMARKS, nBUSINESS_CODE, dDATE)VALUES(" & Val(Me.TxtRecovery.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")
                            Else
                                Me.asInsert.SaveValueIN("INSERT INTO RECOVERY_DETAIL(nRECOVERY_ID, nCLIENT_CODE, nCASH_AMOUNT, sCHEQUE_NO, dCHEQUE_DATE, nCHEQUE_AMOUNT, sREMARKS, nBUSINESS_CODE, dDATE)VALUES(" & Val(Me.TxtRecovery.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColChequeNo").Value & "', '" & CDate(Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value).ToString("MM-dd-yyyy") & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")
                            End If

                        Else 'INSERT VALUES WITHOUT MSGBOX
                            'INSERT VALUES IN RECOVERY DETAIL
                            If Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value Is Nothing Then
                                Me.asInsert.SaveValue("INSERT INTO RECOVERY_DETAIL(nRECOVERY_ID, nCLIENT_CODE, nCASH_AMOUNT, sREMARKS, nBUSINESS_CODE, dDATE)VALUES(" & Val(Me.TxtRecovery.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")
                            Else
                                Me.asInsert.SaveValue("INSERT INTO RECOVERY_DETAIL(nRECOVERY_ID, nCLIENT_CODE, nCASH_AMOUNT, sCHEQUE_NO, dCHEQUE_DATE, nCHEQUE_AMOUNT, sREMARKS, nBUSINESS_CODE, dDATE)VALUES(" & Val(Me.TxtRecovery.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColChequeNo").Value & "', '" & CDate(Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value).ToString("MM-dd-yyyy") & "', " & Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")
                            End If
                        End If


                    Next

                    Me.BttnPrev.Enabled = True
                    Me.BttnPrint.Enabled = True
                    Me.BttnSearch_Inv.Enabled = True
                    Me.BttnNew.Text = "&New"
                    Me.BttnSave.Enabled = False
                    Me.BttnClose.Enabled = True

                End If

            ElseIf Me.asSELECT.pFlg1 = True Then
                MsgBox("This Recovery # '" & Me.TxtRecovery.Text & "' is Already Exist. " & vbCrLf & "To modify this recovery please click on 'Search Recovery' Button", MsgBoxStyle.Exclamation, "(NS) - Already Exist!")

            End If

            'UPDATE SALE INVOICE
        ElseIf Me.BttnSave.Text = "&Update" Then
            If Me.TxtRecovery.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.Null_Focus()

            ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) <= 0 Then
                MsgBox("Please enter atleast one Item to save Invoice!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.DataGridView1.Focus()

            ElseIf Me.asSELECT.pFlg1 = True Then

                If Val(Me.TxtNetRecovery.Text) < 0 Then
                    MsgBox("Can't save!" & vbCrLf & "Net Total less than Zero(0)", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                    Me.TxtExpense.Focus()

                Else
                    'UPDATE VALUES IN RECOVERY MASTER
                    If Me.CmbBankAccount.Text = Nothing Or Me.CmbBankAccount.SelectedIndex = -1 Then
                        Me.asUpdate.UpdateValue("UPDATE RECOVERY_MASTER SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nTOTAL_CASH=" & Val(Me.TxtTotCash.Text) & ", nTOTAL_CHEQUE=" & Val(Me.TxtTotCheque.Text) & ", nTOTAL_EXPENSE=" & Val(Me.TxtExpense.Text) & ", nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nID=" & Val(Me.TxtRecovery.Text) & "")

                    Else
                        Me.asUpdate.UpdateValue("UPDATE RECOVERY_MASTER SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nTOTAL_CASH=" & Val(Me.TxtTotCash.Text) & ", nTOTAL_CHEQUE=" & Val(Me.TxtTotCheque.Text) & ", nTOTAL_EXPENSE=" & Val(Me.TxtExpense.Text) & ", nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', sREMARKS='" & Me.TxtRemarks.Text & "' WHERE nID=" & Val(Me.TxtRecovery.Text) & "")

                    End If


                    Dim i As Integer
                    For i = 0 To Me.DataGridView1.Rows.Count - 2
                        'UPDATE WITH MSGBOX
                        If i = Me.DataGridView1.Rows.Count - 2 Then
                            If Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then
                                'INSERT VALUES IN RECOVERY DETAIL
                                If Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value = Nothing Then
                                    Me.asUpdate.UpdateValueIN("INSERT INTO RECOVERY_DETAIL(nRECOVERY_ID, nCLIENT_CODE, nCASH_AMOUNT, sREMARKS, nBUSINESS_CODE, dDATE)VALUES(" & Val(Me.TxtRecovery.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")
                                Else
                                    Me.asUpdate.UpdateValueIN("INSERT INTO RECOVERY_DETAIL(nRECOVERY_ID, nCLIENT_CODE, nCASH_AMOUNT, sCHEQUE_NO, dCHEQUE_DATE, nCHEQUE_AMOUNT, sREMARKS, nBUSINESS_CODE, dDATE)VALUES(" & Val(Me.TxtRecovery.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColChequeNo").Value & "', '" & CDate(Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value).ToString("MM-dd-yyyy") & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")
                                End If

                            ElseIf Not Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then
                                'UPDATE VALUES IN RECOVERY DETAIL
                                If Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value = Nothing Then
                                    Me.asUpdate.UpdateValueIN("UPDATE RECOVERY_DETAIL SET nRECOVERY_ID=" & Val(Me.TxtRecovery.Text) & ", nCLIENT_CODE=" & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", nCASH_AMOUNT=" & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", sCHEQUE_NO=NULL, dCHEQUE_DATE=NULL, nCHEQUE_AMOUNT=0, sREMARKS='" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "' WHERE nID=" & Val(Me.DataGridView1.Rows(i).Cells("ColSR").Value) & "")
                                Else
                                    Me.asUpdate.UpdateValueIN("UPDATE RECOVERY_DETAIL SET nRECOVERY_ID=" & Val(Me.TxtRecovery.Text) & ", nCLIENT_CODE=" & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", nCASH_AMOUNT=" & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", sCHEQUE_NO='" & Me.DataGridView1.Rows(i).Cells("ColChequeNo").Value & "', dCHEQUE_DATE='" & CDate(Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value).ToString("MM-dd-yyyy") & "', nCHEQUE_AMOUNT=" & Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value) & ", sREMARKS='" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "' WHERE nID=" & Val(Me.DataGridView1.Rows(i).Cells("ColSR").Value) & "")
                                End If

                            End If

                        Else 'UPDATE WITHOUT MSGBOX
                            If Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then
                                'INSERT VALUES IN RECOVERY DETAIL
                                If Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value = Nothing Then
                                    Me.asInsert.SaveValue("INSERT INTO RECOVERY_DETAIL(nRECOVERY_ID, nCLIENT_CODE, nCASH_AMOUNT, sREMARKS, nBUSINESS_CODE, dDATE)VALUES(" & Val(Me.TxtRecovery.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")
                                Else
                                    Me.asInsert.SaveValue("INSERT INTO RECOVERY_DETAIL(nRECOVERY_ID, nCLIENT_CODE, nCASH_AMOUNT, sCHEQUE_NO, dCHEQUE_DATE, nCHEQUE_AMOUNT, sREMARKS, nBUSINESS_CODE, dDATE)VALUES(" & Val(Me.TxtRecovery.Text) & "," & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColChequeNo").Value & "', '" & CDate(Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value).ToString("MM-dd-yyyy") & ", " & Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value) & ", '" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', " & Val(Me.CmbGroup.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "')")
                                End If

                            ElseIf Not Me.DataGridView1.Rows(i).Cells("ColSR").Value = Nothing Then
                                'UPDATE VALUES IN RECOVERY DETAIL
                                If Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value = Nothing Then
                                    Me.asUpdate.UpdateValue("UPDATE RECOVERY_DETAIL SET nRECOVERY_ID=" & Val(Me.TxtRecovery.Text) & ", nCLIENT_CODE=" & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", nCASH_AMOUNT=" & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", sCHEQUE_NO=NULL, dCHEQUE_DATE=NULL, nCHEQUE_AMOUNT=0, sREMARKS='" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "' WHERE nID=" & Val(Me.DataGridView1.Rows(i).Cells("ColSR").Value) & "")
                                Else
                                    Me.asUpdate.UpdateValue("UPDATE RECOVERY_DETAIL SET nRECOVERY_ID=" & Val(Me.TxtRecovery.Text) & ", nCLIENT_CODE=" & Val(Me.DataGridView1.Rows(i).Cells("ColCode").Value) & ", nCASH_AMOUNT=" & Val(Me.DataGridView1.Rows(i).Cells("ColCash").Value) & ", sCHEQUE_NO='" & Me.DataGridView1.Rows(i).Cells("ColChequeNo").Value & "', dCHEQUE_DATE='" & CDate(Me.DataGridView1.Rows(i).Cells("ColChequeDate").Value).ToString("MM-dd-yyyy") & "', nCHEQUE_AMOUNT=" & Val(Me.DataGridView1.Rows(i).Cells("ColCheque").Value) & ", sREMARKS='" & Me.DataGridView1.Rows(i).Cells("ColRemarks").Value & "', nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "' WHERE nID=" & Val(Me.DataGridView1.Rows(i).Cells("ColSR").Value) & "")
                                End If

                            End If
                        End If

                    Next
                End If


                Me.BttnPrev.Enabled = True
                Me.BttnPrint.Enabled = True
                Me.BttnSearch_Inv.Enabled = True
                Me.BttnNew.Text = "&New"
                Me.BttnNew.Enabled = True
                Me.BttnSave.Text = "&Save"
                Me.BttnSave.Enabled = False
                Me.BttnClose.Enabled = True
                Me.Disable_All()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            MsgBox("This Recovery # " & Val(Me.TxtRecovery.Text) & " is not Exist.", MsgBoxStyle.Exclamation, "(NS) - Not Exist!")

        End If

    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        If MsgBox("Are you sure to Close?", MsgBoxStyle.Question + vbYesNo, "(NS) - Close?") = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Search Button Control"
    Private Sub BttnSearch_Inv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch_Inv.Click
        On Error GoTo Fix
        frmSEARCH_RECV_INV.TxtRecPerson.Text = Nothing
        frmSEARCH_RECV_INV.TxtRecovery.Text = Nothing
        frmSEARCH_RECV_INV.TxtDateFrom.Text = Nothing
        frmSEARCH_RECV_INV.TxtDateTo.Text = Nothing

        frmSEARCH_RECV_INV.ShowDialog(Me)
Fix:
    End Sub
#End Region

#Region "Print Button Control"
    Private Sub BttnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrev.Click
        Dim Rpt As New rptRECOVERY
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = "{V_RECOVERY_MASTER.ID}=" & Val(Me.TxtRecovery.Text) & ""
            Frm.Text = "Recovery (Ugrai)"
            Frm.MdiParent = Me.ParentForm
            Frm.Show()
            Frm.Activate()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub BttnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrint.Click
        Dim Rpt As New rptRECOVERY
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = "{V_RECOVERY_MASTER.ID}=" & Val(Me.TxtRecovery.Text) & ""
            Frm.CRV.PrintReport()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
#End Region

#Region "Sub and Functions"
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
    Private Sub FillComboBox_Employee()
        Dim Str1 As String = "SELECT CODE, NAME, FATHER_NAME, NIC, HOME_PH, CELL, PRE_ADD, PER_ADD, DESIGNATION, APP_DATE, PAY, STATUS, LEAVE_DATE, EMAIL_ADD, BANK_ACC, BANK_ADD FROM V_EMPLOYEE_INFO WHERE STATUS='1'"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_EMPLOYEE = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_EMPLOYEE1.Clear()
        Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO)

        Dim dtLoading As New DataTable("V_EMPLOYEE_INFO")

        dtLoading.Columns.Add("CODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("NAME", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("DESIGNATION", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("CODE") = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(0).ToString
            dr("NAME") = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(1).ToString
            dr("DESIGNATION") = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(8).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbEmployee.SelectedIndex = -1
        Me.CmbEmployee.Items.Clear()
        Me.CmbEmployee.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbEmployee.SourceDataString = New String(2) {"NAME", "DESIGNATION", "CODE"}
        Me.CmbEmployee.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_BankAccount()
        Dim Str1 As String = "SELECT sACCOUNT_NO, sBANK_NAME, sBRANCH_NAME, sBRANCH_code, sADDRESS, sCONTACT1, sCONTACT2, sMANAGER_NAME, sMANAGER_PH, sMANAGER_CELL, sSTATUS FROM LUP_BANK WHERE sSTATUS='1'"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_BANK = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_BANK1.Clear()
        Me.daLUP_BANK.Fill(Me.DsLUP_BANK1.LUP_BANK)

        Dim dtLoading As New DataTable("LUP_BANK")

        dtLoading.Columns.Add("sACCOUNT_NO", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sBANK_NAME", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_BANK1.LUP_BANK.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("sACCOUNT_NO") = Me.DsLUP_BANK1.LUP_BANK.Item(Cnt).Item(0).ToString
            dr("sBANK_NAME") = Me.DsLUP_BANK1.LUP_BANK.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbBankAccount.SelectedIndex = -1
        Me.CmbBankAccount.Items.Clear()
        Me.CmbBankAccount.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbBankAccount.SourceDataString = New String(1) {"sACCOUNT_NO", "sBANK_NAME"}
        Me.CmbBankAccount.SourceDataTable = dtLoading
    End Sub

    Private Sub Fill_Master_Data()
        Dim Str2 As String = "SELECT ID, dDATE, EMP_NAME, CONVERT(NUMERIC(18, 2), TOT_CASH) AS TOT_CASH, CONVERT(NUMERIC(18, 2), TOT_CHQ) AS TOT_CHQ, CONVERT(NUMERIC(18, 2), TOT_EXP) AS TOT_EXP, GROUP_NAME, ACCOUNT_NO, REMARKS FROM V_RECOVERY_MASTER WHERE ID=" & Val(Me.TxtRecovery.Text) & ""
        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)

        Me.daV_RECOVERY_MASTER = New SDS.SqlDataAdapter(SqlCmd2)

        Me.DsV_RECOVERY1.V_RECOVERY_MASTER.Clear()
        Me.daV_RECOVERY_MASTER.Fill(Me.DsV_RECOVERY1.V_RECOVERY_MASTER)

        Dim Str3 As String = "SELECT V_RECOVERY_DETAIL.ID, V_RECOVERY_DETAIL.RECV_NO, V_RECOVERY_DETAIL.SHOP_AREA, CONVERT(NUMERIC(18, 2), V_RECOVERY_DETAIL.CASH_AMT) AS CASH_AMT, V_RECOVERY_DETAIL.CHQ_NO, V_RECOVERY_DETAIL.CHQ_DATE, CONVERT(NUMERIC(18, 2), V_RECOVERY_DETAIL.CHQ_AMT) AS CHQ_AMT, V_RECOVERY_DETAIL.REMARKS, SV_CLIENT_BALANCE_TOT.CLIENT_BAL, V_RECOVERY_DETAIL.CLIENT_CODE FROM V_RECOVERY_DETAIL LEFT OUTER JOIN SV_CLIENT_BALANCE_TOT ON V_RECOVERY_DETAIL.CLIENT_CODE = SV_CLIENT_BALANCE_TOT.CLIENT_ID WHERE V_RECOVERY_DETAIL.RECV_NO=" & Val(Me.TxtRecovery.Text) & ""
        Dim SqlCmd3 As New SDS.SqlCommand(Str3, Me.SqlConnection1)
        Me.daV_RECOVERY_DETAIL = New SDS.SqlDataAdapter(SqlCmd3)

        Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Clear()
        Me.daV_RECOVERY_DETAIL.Fill(Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT)

        '        On Error GoTo Fix
        '        Me.DataGridView1.Rows.Clear()
        'Fix:
        Dim Cnt As Integer

        For Cnt = 0 To Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Count - 1
            Me.DataGridView1.Rows.Add()
            Me.DataGridView1.Rows(Cnt).Cells("ColCode").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(9).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColName").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(2).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColCash").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(3).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColCheque").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(6).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColChequeNo").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(4).ToString

            Dim ChqDate As String = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(5).ToString
            If Not ChqDate = Nothing Then
                Me.DataGridView1.Rows(Cnt).Cells("ColChequeDate").Value = CDate(ChqDate).ToString("dd-MMM-yyyy")
            Else
                Me.DataGridView1.Rows(Cnt).Cells("ColChequeDate").Value = ChqDate
            End If

            Me.DataGridView1.Rows(Cnt).Cells("ColTot_Rec").Value = Val(Me.DataGridView1.Rows(Cnt).Cells("ColCash").Value) + Val(Me.DataGridView1.Rows(Cnt).Cells("ColCheque").Value)
            Me.DataGridView1.Rows(Cnt).Cells("ColNetBal").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(8).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColPrevBal").Value = Val(Me.DataGridView1.Rows(Cnt).Cells("ColNetBal").Value) - Val(Me.DataGridView1.Rows(Cnt).Cells("ColTot_Rec").Value)
            Me.DataGridView1.Rows(Cnt).Cells("ColRemarks").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(7).ToString
            Me.DataGridView1.Rows(Cnt).Cells("ColSR").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(0).ToString

        Next

        'MsgBox(Me.DataGridView1.Rows.Count)
    End Sub
    Private Sub Fill_Detail_Data()
        'Dim Str3 As String = "SELECT V_RECOVERY_DETAIL.ID, V_RECOVERY_DETAIL.RECV_NO, V_RECOVERY_DETAIL.SHOP_AREA, CONVERT(NUMERIC(18, 2), V_RECOVERY_DETAIL.CASH_AMT) AS CASH_AMT, V_RECOVERY_DETAIL.CHQ_NO, V_RECOVERY_DETAIL.CHQ_DATE, CONVERT(NUMERIC(18, 2), V_RECOVERY_DETAIL.CHQ_AMT) AS CHQ_AMT, V_RECOVERY_DETAIL.REMARKS, SV_CLIENT_BALANCE_TOT.CLIENT_BAL, V_RECOVERY_DETAIL.CLIENT_CODE FROM V_RECOVERY_DETAIL LEFT OUTER JOIN SV_CLIENT_BALANCE_TOT ON V_RECOVERY_DETAIL.CLIENT_CODE = SV_CLIENT_BALANCE_TOT.CLIENT_ID WHERE V_RECOVERY_DETAIL.RECV_NO=" & Val(Me.TxtRecovery.Text) & ""
        'Dim SqlCmd3 As New SDS.SqlCommand(Str3, Me.SqlConnection1)
        'Me.daV_RECOVERY_DETAIL = New SDS.SqlDataAdapter(SqlCmd3)

        'Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Clear()
        'Me.daV_RECOVERY_DETAIL.Fill(Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT)

        ''        On Error GoTo Fix
        ''        Me.DataGridView1.Rows.Clear()
        ''Fix:
        'Dim Cnt As Integer

        'For Cnt = 0 To Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Count - 1
        '    Me.DataGridView1.Rows.Add()
        '    Me.DataGridView1.Rows(Cnt).Cells("ColCode").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(9).ToString
        '    Me.DataGridView1.Rows(Cnt).Cells("ColName").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(2).ToString
        '    Me.DataGridView1.Rows(Cnt).Cells("ColCash").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(3).ToString
        '    Me.DataGridView1.Rows(Cnt).Cells("ColCheque").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(6).ToString
        '    Me.DataGridView1.Rows(Cnt).Cells("ColChequeNo").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(4).ToString

        '    Dim ChqDate As String = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(5).ToString
        '    If Not ChqDate = Nothing Then
        '        Me.DataGridView1.Rows(Cnt).Cells("ColChequeDate").Value = CDate(ChqDate).ToString("dd-MMM-yyyy")
        '    Else
        '        Me.DataGridView1.Rows(Cnt).Cells("ColChequeDate").Value = ChqDate
        '    End If

        '    Me.DataGridView1.Rows(Cnt).Cells("ColTot_Rec").Value = Val(Me.DataGridView1.Rows(Cnt).Cells("ColCash").Value) + Val(Me.DataGridView1.Rows(Cnt).Cells("ColCheque").Value)
        '    Me.DataGridView1.Rows(Cnt).Cells("ColNetBal").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(8).ToString
        '    Me.DataGridView1.Rows(Cnt).Cells("ColPrevBal").Value = Val(Me.DataGridView1.Rows(Cnt).Cells("ColNetBal").Value) - Val(Me.DataGridView1.Rows(Cnt).Cells("ColTot_Rec").Value)
        '    Me.DataGridView1.Rows(Cnt).Cells("ColRemarks").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(7).ToString
        '    Me.DataGridView1.Rows(Cnt).Cells("ColSR").Value = Me.DsV_RECOVERY1.SV_CLIENT_BALANCE_TOT.Item(Cnt).Item(0).ToString

        'Next
    End Sub

    Private Sub Null_Focus()
        If Me.TxtRecovery.Text = Nothing Then
            Me.TxtRecovery.Focus()

        ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
            Me.CmbGroup.Focus()

        ElseIf Me.TxtDate.Text = Nothing Then
            Me.TxtDate.Focus()

        ElseIf Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
            Me.CmbEmployee.Focus()

        End If
    End Sub

    Public Sub Disable_All()
        Dim Ctrl As Control
        For Each Ctrl In Me.Controls
            If Not Ctrl.Name = "GroupBox1" And Not Ctrl.Name = "Label3" Then
                Ctrl.Enabled = False
            End If
        Next
    End Sub
    Public Sub Enable_All()
        Dim Ctrl As Control
        For Each Ctrl In Me.Controls
            If Not Ctrl.Name = "GroupBox1" And Not Ctrl.Name = "Label3" Then
                Ctrl.Enabled = True
            End If
        Next
    End Sub

    Public Sub Clear_All()
        Me.TxtRecovery.Text = Nothing
        Me.CmbGroup.SelectedIndex = -1
        Me.CmbEmployee.SelectedIndex = -1
        Me.CmbBankAccount.SelectedIndex = -1

        Me.TxtTotalRecords.Text = 0

        Me.TxtTotCash.Text = "0.00"
        Me.TxtTotCheque.Text = "0.00"
        Me.TxtTotal.Text = "0.00"

        Me.TxtExpense.Text = "0.00"
        Me.TxtNetRecovery.Text = "0.00"

        Me.TxtRemarks.Text = Nothing

        Me.Default_Setting()

        Me.CmbGroup.Focus()



        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:
    End Sub

    Private Sub Default_Setting()
        On Error GoTo Fix
        Dim StrCMB As String

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("GROUP").ToString
        Me.CmbGroup.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("R_MAN").ToString
        Me.CmbEmployee.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbEmployee.SelectedIndex = Me.CmbEmployee.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("BANK_ACC").ToString
        Me.CmbBankAccount.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbBankAccount.SelectedIndex = Me.CmbBankAccount.FindString(StrCMB)
        End If
Fix:
    End Sub
#End Region

End Class
