<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLOADPASS_BILLING
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLOADPASS_BILLING))
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.BindingNavigator1 = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.BttnPostIT = New System.Windows.Forms.Button
        Me.BttnBilling = New System.Windows.Forms.Button
        Me.TxtLoadPass = New System.Windows.Forms.TextBox
        Me.DsV_MOBILE_ISSUE_MASTER1 = New Neruo_Business_Solution.dsV_MOBILE_ISSUE_MASTER
        Me.TxtVanName = New System.Windows.Forms.TextBox
        Me.TxtRoute = New System.Windows.Forms.TextBox
        Me.TxtDMan = New System.Windows.Forms.TextBox
        Me.TxtSalesMan = New System.Windows.Forms.TextBox
        Me.TxtOtherDisc = New System.Windows.Forms.TextBox
        Me.TxtDiscPER = New System.Windows.Forms.TextBox
        Me.TxtOtherDesc = New System.Windows.Forms.TextBox
        Me.TxtDiscRs = New System.Windows.Forms.TextBox
        Me.TxtNetTotal = New System.Windows.Forms.TextBox
        Me.TxtVanNo = New System.Windows.Forms.TextBox
        Me.TxtTotalBill = New System.Windows.Forms.TextBox
        Me.TxtGroup = New System.Windows.Forms.TextBox
        Me.TxtDate = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblPosted = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.BttnClose = New System.Windows.Forms.Button
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daV_MOBILE_ISSUE_MASTER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.GroupBox3.SuspendLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1.SuspendLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsV_MOBILE_ISSUE_MASTER1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Label3.Text = "LOAD PASS BILLING"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.BindingNavigator1)
        Me.GroupBox3.Controls.Add(Me.BttnPostIT)
        Me.GroupBox3.Controls.Add(Me.BttnBilling)
        Me.GroupBox3.Controls.Add(Me.TxtLoadPass)
        Me.GroupBox3.Controls.Add(Me.TxtVanName)
        Me.GroupBox3.Controls.Add(Me.TxtRoute)
        Me.GroupBox3.Controls.Add(Me.TxtDMan)
        Me.GroupBox3.Controls.Add(Me.TxtSalesMan)
        Me.GroupBox3.Controls.Add(Me.TxtOtherDisc)
        Me.GroupBox3.Controls.Add(Me.TxtDiscPER)
        Me.GroupBox3.Controls.Add(Me.TxtOtherDesc)
        Me.GroupBox3.Controls.Add(Me.TxtDiscRs)
        Me.GroupBox3.Controls.Add(Me.TxtNetTotal)
        Me.GroupBox3.Controls.Add(Me.TxtVanNo)
        Me.GroupBox3.Controls.Add(Me.TxtTotalBill)
        Me.GroupBox3.Controls.Add(Me.TxtGroup)
        Me.GroupBox3.Controls.Add(Me.TxtDate)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.LblPosted)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(560, 303)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'BindingNavigator1
        '
        Me.BindingNavigator1.AddNewItem = Nothing
        Me.BindingNavigator1.BindingSource = Me.BindingSource1
        Me.BindingNavigator1.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1.DeleteItem = Nothing
        Me.BindingNavigator1.Dock = System.Windows.Forms.DockStyle.None
        Me.BindingNavigator1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BindingNavigator1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator3, Me.BindingNavigatorMoveFirstItem, Me.ToolStripSeparator2, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.ToolStripSeparator1, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2})
        Me.BindingNavigator1.Location = New System.Drawing.Point(9, 253)
        Me.BindingNavigator1.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1.Name = "BindingNavigator1"
        Me.BindingNavigator1.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1.Size = New System.Drawing.Size(357, 33)
        Me.BindingNavigator1.TabIndex = 25
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(36, 30)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 33)
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.AutoSize = False
        Me.BindingNavigatorMoveFirstItem.BackColor = System.Drawing.Color.Pink
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(45, 30)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 33)
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.AutoSize = False
        Me.BindingNavigatorMovePreviousItem.BackColor = System.Drawing.Color.Tan
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(45, 30)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 33)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(100, 33)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 33)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.AutoSize = False
        Me.BindingNavigatorMoveNextItem.BackColor = System.Drawing.Color.Tan
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(45, 30)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 33)
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.AutoSize = False
        Me.BindingNavigatorMoveLastItem.BackColor = System.Drawing.Color.Pink
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(45, 30)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 33)
        '
        'BttnPostIT
        '
        Me.BttnPostIT.Enabled = False
        Me.BttnPostIT.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPostIT.ForeColor = System.Drawing.Color.Red
        Me.BttnPostIT.Location = New System.Drawing.Point(471, 253)
        Me.BttnPostIT.Name = "BttnPostIT"
        Me.BttnPostIT.Size = New System.Drawing.Size(81, 32)
        Me.BttnPostIT.TabIndex = 24
        Me.BttnPostIT.Text = "Post It!"
        Me.BttnPostIT.Visible = False
        '
        'BttnBilling
        '
        Me.BttnBilling.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnBilling.ForeColor = System.Drawing.Color.DarkBlue
        Me.BttnBilling.Location = New System.Drawing.Point(384, 253)
        Me.BttnBilling.Name = "BttnBilling"
        Me.BttnBilling.Size = New System.Drawing.Size(81, 32)
        Me.BttnBilling.TabIndex = 23
        Me.BttnBilling.Text = "&Billing"
        '
        'TxtLoadPass
        '
        Me.TxtLoadPass.BackColor = System.Drawing.Color.White
        Me.TxtLoadPass.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.LPINV_NO", True))
        Me.TxtLoadPass.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtLoadPass.Location = New System.Drawing.Point(98, 22)
        Me.TxtLoadPass.MaxLength = 50
        Me.TxtLoadPass.Name = "TxtLoadPass"
        Me.TxtLoadPass.ReadOnly = True
        Me.TxtLoadPass.Size = New System.Drawing.Size(119, 21)
        Me.TxtLoadPass.TabIndex = 1
        '
        'DsV_MOBILE_ISSUE_MASTER1
        '
        Me.DsV_MOBILE_ISSUE_MASTER1.DataSetName = "dsV_MOBILE_ISSUE_MASTER"
        Me.DsV_MOBILE_ISSUE_MASTER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtVanName
        '
        Me.TxtVanName.BackColor = System.Drawing.Color.White
        Me.TxtVanName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.VAN_NAME", True))
        Me.TxtVanName.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtVanName.Location = New System.Drawing.Point(299, 94)
        Me.TxtVanName.MaxLength = 50
        Me.TxtVanName.Name = "TxtVanName"
        Me.TxtVanName.ReadOnly = True
        Me.TxtVanName.Size = New System.Drawing.Size(119, 21)
        Me.TxtVanName.TabIndex = 1
        '
        'TxtRoute
        '
        Me.TxtRoute.BackColor = System.Drawing.Color.White
        Me.TxtRoute.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.ROUTE", True))
        Me.TxtRoute.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtRoute.Location = New System.Drawing.Point(299, 70)
        Me.TxtRoute.MaxLength = 50
        Me.TxtRoute.Name = "TxtRoute"
        Me.TxtRoute.ReadOnly = True
        Me.TxtRoute.Size = New System.Drawing.Size(255, 21)
        Me.TxtRoute.TabIndex = 1
        '
        'TxtDMan
        '
        Me.TxtDMan.BackColor = System.Drawing.Color.White
        Me.TxtDMan.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.D_MAN", True))
        Me.TxtDMan.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDMan.Location = New System.Drawing.Point(299, 46)
        Me.TxtDMan.MaxLength = 50
        Me.TxtDMan.Name = "TxtDMan"
        Me.TxtDMan.ReadOnly = True
        Me.TxtDMan.Size = New System.Drawing.Size(119, 21)
        Me.TxtDMan.TabIndex = 1
        '
        'TxtSalesMan
        '
        Me.TxtSalesMan.BackColor = System.Drawing.Color.White
        Me.TxtSalesMan.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.SALE_MAN", True))
        Me.TxtSalesMan.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtSalesMan.Location = New System.Drawing.Point(299, 22)
        Me.TxtSalesMan.MaxLength = 50
        Me.TxtSalesMan.Name = "TxtSalesMan"
        Me.TxtSalesMan.ReadOnly = True
        Me.TxtSalesMan.Size = New System.Drawing.Size(119, 21)
        Me.TxtSalesMan.TabIndex = 1
        '
        'TxtOtherDisc
        '
        Me.TxtOtherDisc.BackColor = System.Drawing.Color.White
        Me.TxtOtherDisc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.OTHER_DISC", True))
        Me.TxtOtherDisc.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtOtherDisc.Location = New System.Drawing.Point(440, 162)
        Me.TxtOtherDisc.MaxLength = 50
        Me.TxtOtherDisc.Name = "TxtOtherDisc"
        Me.TxtOtherDisc.ReadOnly = True
        Me.TxtOtherDisc.Size = New System.Drawing.Size(79, 21)
        Me.TxtOtherDisc.TabIndex = 1
        '
        'TxtDiscPER
        '
        Me.TxtDiscPER.BackColor = System.Drawing.Color.White
        Me.TxtDiscPER.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.DISC_PER", True))
        Me.TxtDiscPER.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDiscPER.Location = New System.Drawing.Point(279, 162)
        Me.TxtDiscPER.MaxLength = 50
        Me.TxtDiscPER.Name = "TxtDiscPER"
        Me.TxtDiscPER.ReadOnly = True
        Me.TxtDiscPER.Size = New System.Drawing.Size(79, 21)
        Me.TxtDiscPER.TabIndex = 1
        '
        'TxtOtherDesc
        '
        Me.TxtOtherDesc.BackColor = System.Drawing.Color.White
        Me.TxtOtherDesc.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.OTHER_DESC", True))
        Me.TxtOtherDesc.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtOtherDesc.Location = New System.Drawing.Point(118, 186)
        Me.TxtOtherDesc.MaxLength = 50
        Me.TxtOtherDesc.Name = "TxtOtherDesc"
        Me.TxtOtherDesc.ReadOnly = True
        Me.TxtOtherDesc.Size = New System.Drawing.Size(401, 21)
        Me.TxtOtherDesc.TabIndex = 1
        '
        'TxtDiscRs
        '
        Me.TxtDiscRs.BackColor = System.Drawing.Color.White
        Me.TxtDiscRs.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.DISC_RS", True))
        Me.TxtDiscRs.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDiscRs.Location = New System.Drawing.Point(118, 162)
        Me.TxtDiscRs.MaxLength = 50
        Me.TxtDiscRs.Name = "TxtDiscRs"
        Me.TxtDiscRs.ReadOnly = True
        Me.TxtDiscRs.Size = New System.Drawing.Size(79, 21)
        Me.TxtDiscRs.TabIndex = 1
        '
        'TxtNetTotal
        '
        Me.TxtNetTotal.BackColor = System.Drawing.Color.White
        Me.TxtNetTotal.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.NET_TOTAL", True))
        Me.TxtNetTotal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtNetTotal.Location = New System.Drawing.Point(253, 210)
        Me.TxtNetTotal.MaxLength = 50
        Me.TxtNetTotal.Name = "TxtNetTotal"
        Me.TxtNetTotal.ReadOnly = True
        Me.TxtNetTotal.Size = New System.Drawing.Size(119, 21)
        Me.TxtNetTotal.TabIndex = 1
        '
        'TxtVanNo
        '
        Me.TxtVanNo.BackColor = System.Drawing.Color.White
        Me.TxtVanNo.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.VAN_NO", True))
        Me.TxtVanNo.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtVanNo.Location = New System.Drawing.Point(98, 94)
        Me.TxtVanNo.MaxLength = 50
        Me.TxtVanNo.Name = "TxtVanNo"
        Me.TxtVanNo.ReadOnly = True
        Me.TxtVanNo.Size = New System.Drawing.Size(119, 21)
        Me.TxtVanNo.TabIndex = 1
        '
        'TxtTotalBill
        '
        Me.TxtTotalBill.BackColor = System.Drawing.Color.White
        Me.TxtTotalBill.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.TOTAL_BILL", True))
        Me.TxtTotalBill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.TxtTotalBill.Location = New System.Drawing.Point(253, 138)
        Me.TxtTotalBill.MaxLength = 50
        Me.TxtTotalBill.Name = "TxtTotalBill"
        Me.TxtTotalBill.ReadOnly = True
        Me.TxtTotalBill.Size = New System.Drawing.Size(119, 21)
        Me.TxtTotalBill.TabIndex = 1
        '
        'TxtGroup
        '
        Me.TxtGroup.BackColor = System.Drawing.Color.White
        Me.TxtGroup.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.GROUP_NAME", True))
        Me.TxtGroup.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtGroup.Location = New System.Drawing.Point(98, 70)
        Me.TxtGroup.MaxLength = 50
        Me.TxtGroup.Name = "TxtGroup"
        Me.TxtGroup.ReadOnly = True
        Me.TxtGroup.Size = New System.Drawing.Size(119, 21)
        Me.TxtGroup.TabIndex = 1
        '
        'TxtDate
        '
        Me.TxtDate.BackColor = System.Drawing.Color.White
        Me.TxtDate.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER.dDATE", True))
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.TxtDate.Location = New System.Drawing.Point(98, 46)
        Me.TxtDate.MaxLength = 50
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.ReadOnly = True
        Me.TxtDate.Size = New System.Drawing.Size(119, 21)
        Me.TxtDate.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(223, 94)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(70, 21)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Van Name"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 46)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 21)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(223, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 21)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Route"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(364, 162)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(70, 21)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Other Disc"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(203, 162)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 21)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Disc %age"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(42, 186)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 21)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Description"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 21)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Load Pass #"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(42, 162)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 21)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Disc Rs"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(178, 210)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(69, 21)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Net Total"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(223, 22)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 21)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "Sales Man"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(223, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 21)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "D. Man"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(178, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 21)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Total Bill"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 21)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Van No."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblPosted
        '
        Me.LblPosted.BackColor = System.Drawing.Color.Red
        Me.LblPosted.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPosted.ForeColor = System.Drawing.Color.Black
        Me.LblPosted.Location = New System.Drawing.Point(424, 21)
        Me.LblPosted.Name = "LblPosted"
        Me.LblPosted.Size = New System.Drawing.Size(130, 46)
        Me.LblPosted.TabIndex = 4
        Me.LblPosted.Text = "POSTED"
        Me.LblPosted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LblPosted.Visible = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 21)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "B. Group"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(491, 12)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(81, 32)
        Me.BttnClose.TabIndex = 25
        Me.BttnClose.Text = "&Close"
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daV_MOBILE_ISSUE_MASTER
        '
        Me.daV_MOBILE_ISSUE_MASTER.SelectCommand = Me.SqlCommand7
        Me.daV_MOBILE_ISSUE_MASTER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_MOBILE_ISSUE_MASTER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("LPINV_NO", "LPINV_NO"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("TOTAL_BILL", "TOTAL_BILL"), New System.Data.Common.DataColumnMapping("DISC_RS", "DISC_RS"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("OTHER_DISC", "OTHER_DISC"), New System.Data.Common.DataColumnMapping("OTHER_DESC", "OTHER_DESC"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL"), New System.Data.Common.DataColumnMapping("VAN_NO", "VAN_NO"), New System.Data.Common.DataColumnMapping("VAN_NAME", "VAN_NAME"), New System.Data.Common.DataColumnMapping("SALE_MAN", "SALE_MAN"), New System.Data.Common.DataColumnMapping("USER_NAME", "USER_NAME"), New System.Data.Common.DataColumnMapping("GROUP_NAME", "GROUP_NAME"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE"), New System.Data.Common.DataColumnMapping("D_MAN", "D_MAN"), New System.Data.Common.DataColumnMapping("POSTED", "POSTED"), New System.Data.Common.DataColumnMapping("REMARKS", "REMARKS")})})
        '
        'SqlCommand7
        '
        Me.SqlCommand7.CommandText = resources.GetString("SqlCommand7.CommandText")
        Me.SqlCommand7.Connection = Me.SqlConnection1
        '
        'frmLOADPASS_BILLING
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 372)
        Me.Controls.Add(Me.BttnClose)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmLOADPASS_BILLING"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LOAD PASS"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.BindingNavigator1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1.ResumeLayout(False)
        Me.BindingNavigator1.PerformLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsV_MOBILE_ISSUE_MASTER1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtDate As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents BttnBilling As System.Windows.Forms.Button
    Friend WithEvents BttnPostIT As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents TxtLoadPass As System.Windows.Forms.TextBox
    Friend WithEvents TxtGroup As System.Windows.Forms.TextBox
    Friend WithEvents TxtRoute As System.Windows.Forms.TextBox
    Friend WithEvents TxtDMan As System.Windows.Forms.TextBox
    Friend WithEvents TxtSalesMan As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LblPosted As System.Windows.Forms.Label
    Friend WithEvents TxtDiscRs As System.Windows.Forms.TextBox
    Friend WithEvents TxtTotalBill As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtDiscPER As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtOtherDisc As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtOtherDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TxtNetTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents BindingNavigator1 As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DsV_MOBILE_ISSUE_MASTER1 As Neruo_Business_Solution.dsV_MOBILE_ISSUE_MASTER
    Friend WithEvents daV_MOBILE_ISSUE_MASTER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents TxtVanName As System.Windows.Forms.TextBox
    Friend WithEvents TxtVanNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
End Class
