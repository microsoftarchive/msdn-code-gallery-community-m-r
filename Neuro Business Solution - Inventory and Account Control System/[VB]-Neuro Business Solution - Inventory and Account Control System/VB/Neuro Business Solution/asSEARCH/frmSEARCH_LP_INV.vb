Imports SDS = System.Data.SqlClient
Public Class frmSEARCH_LP_INV
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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtLoadPass As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BttnRefresh As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSearch As System.Windows.Forms.Button
    Friend WithEvents daV_MOBILE_ISSUE_MASTER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlCommand7 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsV_MOBILE_ISSUE_MASTER1 As Neruo_Business_Solution.dsV_MOBILE_ISSUE_MASTER
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtRoute As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TxtDateFrom As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSEARCH_LP_INV))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnRefresh = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BttnSearch = New System.Windows.Forms.Button
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtDateTo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDateFrom = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtRoute = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtLoadPass = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daV_MOBILE_ISSUE_MASTER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlCommand7 = New System.Data.SqlClient.SqlCommand
        Me.DsV_MOBILE_ISSUE_MASTER1 = New Neruo_Business_Solution.dsV_MOBILE_ISSUE_MASTER
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DsV_MOBILE_ISSUE_MASTER1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnClose)
        Me.Panel1.Controls.Add(Me.BttnRefresh)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(12, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(768, 384)
        Me.Panel1.TabIndex = 0
        '
        'BttnClose
        '
        Me.BttnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(688, 5)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 35)
        Me.BttnClose.TabIndex = 3
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = True
        '
        'BttnRefresh
        '
        Me.BttnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnRefresh.Location = New System.Drawing.Point(3, 5)
        Me.BttnRefresh.Name = "BttnRefresh"
        Me.BttnRefresh.Size = New System.Drawing.Size(75, 35)
        Me.BttnRefresh.TabIndex = 2
        Me.BttnRefresh.Text = "Refres&h"
        Me.BttnRefresh.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.BttnSearch)
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.TxtDateTo)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.TxtDateFrom)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtRoute)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TxtLoadPass)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 38)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(758, 334)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'BttnSearch
        '
        Me.BttnSearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.BttnSearch.Location = New System.Drawing.Point(680, 18)
        Me.BttnSearch.Name = "BttnSearch"
        Me.BttnSearch.Size = New System.Drawing.Size(69, 21)
        Me.BttnSearch.TabIndex = 9
        Me.BttnSearch.Text = "Sea&rch"
        Me.BttnSearch.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader4, Me.ColumnHeader3, Me.ColumnHeader5, Me.ColumnHeader2})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 45)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(752, 286)
        Me.ListView1.TabIndex = 8
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Load Pass #"
        Me.ColumnHeader1.Width = 120
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Date"
        Me.ColumnHeader8.Width = 110
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Route"
        Me.ColumnHeader9.Width = 180
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Total"
        Me.ColumnHeader4.Width = 90
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Tot. Disc"
        Me.ColumnHeader3.Width = 85
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Net Total"
        Me.ColumnHeader5.Width = 90
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(563, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(21, 21)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "&To"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateTo
        '
        Me.TxtDateTo.BackColor = System.Drawing.Color.White
        Me.TxtDateTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateTo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateTo.Location = New System.Drawing.Point(588, 18)
        Me.TxtDateTo.MaxLength = 50
        Me.TxtDateTo.Name = "TxtDateTo"
        Me.TxtDateTo.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(402, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 21)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Date &From"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDateFrom
        '
        Me.TxtDateFrom.BackColor = System.Drawing.Color.White
        Me.TxtDateFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDateFrom.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrom.Location = New System.Drawing.Point(473, 18)
        Me.TxtDateFrom.MaxLength = 50
        Me.TxtDateFrom.Name = "TxtDateFrom"
        Me.TxtDateFrom.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 21)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "&Route"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtRoute
        '
        Me.TxtRoute.BackColor = System.Drawing.Color.White
        Me.TxtRoute.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtRoute.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRoute.Location = New System.Drawing.Point(67, 18)
        Me.TxtRoute.MaxLength = 50
        Me.TxtRoute.Name = "TxtRoute"
        Me.TxtRoute.Size = New System.Drawing.Size(156, 21)
        Me.TxtRoute.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(229, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 21)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "&Load Pass #"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtLoadPass
        '
        Me.TxtLoadPass.BackColor = System.Drawing.Color.White
        Me.TxtLoadPass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtLoadPass.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLoadPass.Location = New System.Drawing.Point(317, 18)
        Me.TxtLoadPass.MaxLength = 50
        Me.TxtLoadPass.Name = "TxtLoadPass"
        Me.TxtLoadPass.Size = New System.Drawing.Size(79, 21)
        Me.TxtLoadPass.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(766, 35)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "LOAD PASS RECORDS"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
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
        'DsV_MOBILE_ISSUE_MASTER1
        '
        Me.DsV_MOBILE_ISSUE_MASTER1.DataSetName = "dsV_MOBILE_ISSUE_MASTER"
        Me.DsV_MOBILE_ISSUE_MASTER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Posted"
        Me.ColumnHeader2.Width = 70
        '
        'frmSEARCH_LP_INV
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(792, 400)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmSEARCH_LP_INV"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SEARCH LOAD PASS"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DsV_MOBILE_ISSUE_MASTER1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asNum As New AssNumPress
    Dim Str2 As String = Nothing
#End Region

#Region "FORM CONTROL"
    Private Sub frmSEARCH_LP_INV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
    End Sub

    Private Sub frmSEARCH_LP_INV_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtLoadPass.GotFocus, TxtDateFrom.GotFocus, TxtDateTo.GotFocus, TxtRoute.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtLoadPass.LostFocus, TxtDateFrom.LostFocus, TxtDateTo.LostFocus, TxtRoute.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtDateFrom"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

                Case "TxtDateTo"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub

#End Region

#Region "ListView Control"
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If Me.ListView1.SelectedItems(0).SubItems(6).Text = "No" Then
                frmLOADPASS.BttnSave.Text = "&Update"
                frmLOADPASS.BttnSave.Enabled = True
                frmLOADPASS.BttnNew.Text = "Ca&ncel"
                frmLOADPASS.BttnClose.Enabled = False
                frmLOADPASS.BttnPrev.Enabled = True
                frmLOADPASS.BttnPrint.Enabled = True
                frmLOADPASS.Search_Inv = True

                frmLOADPASS.Enable_All()

                frmLOADPASS.TxtLoadPass.Text = Me.ListView1.SelectedItems(0).Text

                Me.Close()

            ElseIf Me.ListView1.SelectedItems(0).SubItems(6).Text = "Yes" Then
                MsgBox("Already POSTED, Update/Delete not allowed", MsgBoxStyle.Exclamation, "(NS) - Bound Alert!")

            End If


        Else
            MsgBox("Please Select record to Modify / Update", MsgBoxStyle.Exclamation, "(NS) - Error!")

        End If

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch.Click
        If Not Me.TxtDateFrom.Text = Nothing Then
            If Me.TxtDateTo.Text = Nothing Then
                MsgBox("Please enter both date for 'Date Wise Searching'!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.TxtDateTo.Focus()

                Exit Sub
            End If

        ElseIf Not Me.TxtDateTo.Text = Nothing Then
            If Me.TxtDateFrom.Text = Nothing Then
                MsgBox("Please enter both date for 'Date Wise Searching'!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.TxtDateFrom.Focus()

                Exit Sub
            End If

        ElseIf Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing Then
            If Convert.ToDateTime(Me.TxtDateFrom.Text) > Convert.ToDateTime(Me.TxtDateTo.Text) Then
                MsgBox("Wrong Date Criteria!", MsgBoxStyle.Exclamation, "(NS) - Wrong Entry!")
                Me.TxtDateFrom.Focus()

                Exit Sub
            End If
        End If

        If Not Me.TxtRoute.Text = Nothing And Me.TxtLoadPass.Text = Nothing And Me.TxtDateFrom.Text = Nothing And Me.TxtDateTo.Text = Nothing Then
            Str2 = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER WHERE ROUTE LIKE '%" & Me.TxtRoute.Text & "%' ORDER BY LPINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtLoadPass.Text = Nothing And Me.TxtRoute.Text = Nothing And Me.TxtDateFrom.Text = Nothing And Me.TxtDateTo.Text = Nothing Then
            Str2 = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER WHERE LPINV_NO=" & Val(Me.TxtLoadPass.Text) & " ORDER BY LPINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing And Me.TxtLoadPass.Text = Nothing And Me.TxtRoute.Text = Nothing Then
            Str2 = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER WHERE dDATE>= CONVERT(DATETIME, '" & Me.TxtDateFrom.Text & "') AND dDATE<= CONVERT(DATETIME, '" & Me.TxtDateTo.Text & "') ORDER BY LPINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtRoute.Text = Nothing And Not Me.TxtLoadPass.Text = Nothing And Me.TxtDateFrom.Text = Nothing And Me.TxtDateTo.Text = Nothing Then
            Str2 = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER WHERE ROUTE LIKE '%" & Me.TxtRoute.Text & "%' AND LPINV_NO=" & Val(Me.TxtLoadPass.Text) & " ORDER BY LPINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtRoute.Text = Nothing And Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing And Me.TxtLoadPass.Text = Nothing Then
            Str2 = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER WHERE ROUTE LIKE '%" & Me.TxtRoute.Text & "%' AND dDATE>= CONVERT(DATETIME, '" & Me.TxtDateFrom.Text & "') AND dDATE<= CONVERT(DATETIME, '" & Me.TxtDateTo.Text & "') ORDER BY LPINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtLoadPass.Text = Nothing And Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing And Me.TxtRoute.Text = Nothing Then
            Str2 = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER WHERE LPINV_NO=" & Val(Me.TxtLoadPass.Text) & " AND dDATE>= CONVERT(DATETIME, '" & Me.TxtDateFrom.Text & "') AND dDATE<= CONVERT(DATETIME, '" & Me.TxtDateTo.Text & "') ORDER BY LPINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtLoadPass.Text = Nothing And Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing And Not Me.TxtRoute.Text = Nothing Then
            Str2 = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER WHERE ROUTE LIKE '%" & Me.TxtRoute.Text & "%' AND LPINV_NO=" & Val(Me.TxtLoadPass.Text) & " AND dDATE>= CONVERT(DATETIME, '" & Me.TxtDateFrom.Text & "') AND dDATE<= CONVERT(DATETIME, '" & Me.TxtDateTo.Text & "') ORDER BY LPINV_NO DESC"
            Me.FillListView_Condition()

        Else
            Me.FillListView()
        End If
    End Sub

    Private Sub BttnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnRefresh.Click
        Me.TxtRoute.Text = Nothing
        Me.TxtLoadPass.Text = Nothing
        Me.TxtDateFrom.Text = Nothing
        Me.TxtDateTo.Text = Nothing

        Me.FillListView()

    End Sub

    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER ORDER BY LPINV_NO DESC"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)

            Me.daV_MOBILE_ISSUE_MASTER = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_MOBILE_ISSUE_MASTER1.Clear()
            Me.daV_MOBILE_ISSUE_MASTER.Fill(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    Dim dDATE As String = Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(1).ToString
                    .Add(CDate(dDATE).ToString("dd-MMM-yyyy"), Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(13).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Tot_Bill As Double = Val(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(2).ToString)
                    .Add(Tot_Bill.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Disc_Rs, Disc_Per, Disc_Per_Amt, Disc_Other, Tot_Disc As Double

                    Disc_Rs = Val(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(3).ToString)
                    Disc_Per = Val(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(4).ToString)
                    Disc_Other = Val(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(5).ToString)

                    Disc_Per_Amt = (Tot_Bill * Disc_Per) / 100

                    Tot_Disc = Disc_Rs + Disc_Per_Amt + Disc_Other

                    .Add(Tot_Disc.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(7).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Posted As Boolean = Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(15).ToString
                    If Posted = True Then
                        .Add("Yes", Color.DarkBlue, Color.Red, Me.ListView1.Font)

                    ElseIf Posted = False Then
                        .Add("No", Color.DarkBlue, Color.Green, Me.ListView1.Font)

                    End If


                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
            Me.daV_MOBILE_ISSUE_MASTER = New SDS.SqlDataAdapter(SqlCmd2)

            Me.DsV_MOBILE_ISSUE_MASTER1.Clear()
            Me.daV_MOBILE_ISSUE_MASTER.Fill(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    Dim dDATE As String = Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(1).ToString
                    .Add(CDate(dDATE).ToString("dd-MMM-yyyy"), Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(13).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Tot_Bill As Double = Val(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(2).ToString)
                    .Add(Tot_Bill.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Disc_Rs, Disc_Per, Disc_Per_Amt, Disc_Other, Tot_Disc As Double

                    Disc_Rs = Val(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(3).ToString)
                    Disc_Per = Val(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(4).ToString)
                    Disc_Other = Val(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(5).ToString)

                    Disc_Per_Amt = (Tot_Bill * Disc_Per) / 100

                    Tot_Disc = Disc_Rs + Disc_Per_Amt + Disc_Other

                    .Add(Tot_Disc.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(7).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Posted As Boolean = Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER.Item(Cnt).Item(15).ToString
                    If Posted = True Then
                        .Add("Yes", Color.DarkBlue, Color.Red, Me.ListView1.Font)

                    ElseIf Posted = False Then
                        .Add("No", Color.DarkBlue, Color.Green, Me.ListView1.Font)

                    End If

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

End Class
