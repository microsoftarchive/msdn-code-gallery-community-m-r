Imports SDS = System.Data.SqlClient
Public Class frmSEARCH_S_INV
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
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtClient As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtInvoice As System.Windows.Forms.TextBox
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtDateTo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daV_SALE_INVOICES As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents BttnRefresh As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSearch As System.Windows.Forms.Button
    Friend WithEvents DsV_SALE_INVOICES1 As Neruo_Business_Solution.dsV_SALE_INVOICES
    Friend WithEvents TxtDateFrom As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnRefresh = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BttnSearch = New System.Windows.Forms.Button
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtDateTo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDateFrom = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtInvoice = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtClient = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daV_SALE_INVOICES = New System.Data.SqlClient.SqlDataAdapter
        Me.DsV_SALE_INVOICES1 = New Neruo_Business_Solution.dsV_SALE_INVOICES
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DsV_SALE_INVOICES1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TxtInvoice)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtClient)
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
        Me.BttnSearch.Location = New System.Drawing.Point(688, 16)
        Me.BttnSearch.Name = "BttnSearch"
        Me.BttnSearch.Size = New System.Drawing.Size(64, 21)
        Me.BttnSearch.TabIndex = 9
        Me.BttnSearch.Text = "Sea&rch"
        Me.BttnSearch.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader3, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7})
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
        Me.ColumnHeader1.Text = "Invoice #"
        Me.ColumnHeader1.Width = 90
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Client Name"
        Me.ColumnHeader2.Width = 200
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
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Receipt"
        Me.ColumnHeader6.Width = 90
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Inv. Bal."
        Me.ColumnHeader7.Width = 90
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(571, 16)
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
        Me.TxtDateTo.Location = New System.Drawing.Point(596, 16)
        Me.TxtDateTo.MaxLength = 50
        Me.TxtDateTo.Name = "TxtDateTo"
        Me.TxtDateTo.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateTo.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(410, 16)
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
        Me.TxtDateFrom.Location = New System.Drawing.Point(481, 16)
        Me.TxtDateFrom.MaxLength = 50
        Me.TxtDateFrom.Name = "TxtDateFrom"
        Me.TxtDateFrom.Size = New System.Drawing.Size(86, 21)
        Me.TxtDateFrom.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(232, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 21)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "&Invoice #"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtInvoice
        '
        Me.TxtInvoice.BackColor = System.Drawing.Color.White
        Me.TxtInvoice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtInvoice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInvoice.Location = New System.Drawing.Point(302, 16)
        Me.TxtInvoice.MaxLength = 50
        Me.TxtInvoice.Name = "TxtInvoice"
        Me.TxtInvoice.Size = New System.Drawing.Size(106, 21)
        Me.TxtInvoice.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 21)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "&Client"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtClient
        '
        Me.TxtClient.BackColor = System.Drawing.Color.White
        Me.TxtClient.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtClient.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtClient.Location = New System.Drawing.Point(66, 16)
        Me.TxtClient.MaxLength = 50
        Me.TxtClient.Name = "TxtClient"
        Me.TxtClient.Size = New System.Drawing.Size(164, 21)
        Me.TxtClient.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(766, 35)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "SALE INVOICE(s)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER," & _
            " NET_TOTAL, CASH_AMT, BANK_AMT" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         V_SALE_INVOICES"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'daV_SALE_INVOICES
        '
        Me.daV_SALE_INVOICES.SelectCommand = Me.SqlSelectCommand1
        Me.daV_SALE_INVOICES.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_SALE_INVOICES", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("SINV_NO", "SINV_NO"), New System.Data.Common.DataColumnMapping("CLIENT_NAME", "CLIENT_NAME"), New System.Data.Common.DataColumnMapping("S_DATE", "S_DATE"), New System.Data.Common.DataColumnMapping("TOT_BILL", "TOT_BILL"), New System.Data.Common.DataColumnMapping("DISC_RS", "DISC_RS"), New System.Data.Common.DataColumnMapping("DISC_PER", "DISC_PER"), New System.Data.Common.DataColumnMapping("DISC_OTHER", "DISC_OTHER"), New System.Data.Common.DataColumnMapping("NET_TOTAL", "NET_TOTAL"), New System.Data.Common.DataColumnMapping("CASH_AMT", "CASH_AMT"), New System.Data.Common.DataColumnMapping("BANK_AMT", "BANK_AMT")})})
        '
        'DsV_SALE_INVOICES1
        '
        Me.DsV_SALE_INVOICES1.DataSetName = "dsV_SALE_INVOICES"
        Me.DsV_SALE_INVOICES1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmSEARCH_S_INV
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(792, 400)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmSEARCH_S_INV"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SEARCH SALE INVOICE"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DsV_SALE_INVOICES1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asNum As New AssNumPress
    Dim Str2 As String = Nothing
    Public FrmStr As String
#End Region

#Region "FORM CONTROL"
    Private Sub frmSEARCH_S_INV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
    End Sub

    Private Sub frmSEARCH_S_INV_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtClient.GotFocus, TxtInvoice.GotFocus, TxtDateFrom.GotFocus, TxtDateTo.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtClient.LostFocus, TxtInvoice.LostFocus, TxtDateFrom.LostFocus, TxtDateTo.LostFocus
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

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtClient.TextChanged, TxtInvoice.TextChanged, TxtDateFrom.TextChanged, TxtDateTo.TextChanged

    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If FrmStr = "SALE" Then
                With My.Forms.frmSALE
                    .BttnSave.Text = "&Update"
                    .BttnSave.Enabled = True
                    .BttnNew.Text = "Ca&ncel"
                    .BttnClose.Enabled = False
                    .BttnSearch_Inv.Enabled = False
                    .BttnPrev.Enabled = True
                    .BttnPrint.Enabled = True
                    .Search_Inv = True
                    .BttnReceipt.Enabled = True

                    .Enable_All()

                    On Error GoTo Fix
                    .DataGridView1.Rows.Clear()
Fix:
                    .TxtInvoice.Text = Nothing
                    .TxtInvoice.Text = Me.ListView1.SelectedItems(0).Text
                    Me.Close()

                End With


            ElseIf Me.FrmStr = "SALE_RET_PARTIAL" Then
                With My.Forms.frmSALE_RETURN_PARTIAL
                    .BttnReturn.Enabled = True
                    .BttnNew.Text = "Ca&ncel"
                    .BttnClose.Enabled = False

                    .Enable_All()

                    On Error GoTo Fix1
                    .DataGridView1.Rows.Clear()
Fix1:
                    .TxtInvoice.Text = Nothing
                    .TxtInvoice.Text = Me.ListView1.SelectedItems(0).Text

                    .MdiParent = Me.ParentForm
                    .Show()
                    .Activate()
                    Me.Close()
                End With
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

        If Not Me.TxtClient.Text = Nothing And Me.TxtInvoice.Text = Nothing And Me.TxtDateFrom.Text = Nothing And Me.TxtDateTo.Text = Nothing Then
            Str2 = "SELECT SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER, NET_TOTAL, CASH_AMT, BANK_AMT FROM V_SALE_INVOICES WHERE CLIENT_NAME LIKE '%" & Me.TxtClient.Text & "%' ORDER BY SINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtInvoice.Text = Nothing And Me.TxtClient.Text = Nothing And Me.TxtDateFrom.Text = Nothing And Me.TxtDateTo.Text = Nothing Then
            Str2 = "SELECT SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER, NET_TOTAL, CASH_AMT, BANK_AMT FROM V_SALE_INVOICES WHERE SINV_NO = " & Val(Me.TxtInvoice.Text) & " ORDER BY SINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing And Me.TxtInvoice.Text = Nothing And Me.TxtClient.Text = Nothing Then
            Str2 = "SELECT SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER, NET_TOTAL, CASH_AMT, BANK_AMT FROM V_SALE_INVOICES WHERE S_DATE >= CONVERT(DATETIME, '" & Me.TxtDateFrom.Text & "') AND S_DATE <= CONVERT(DATETIME, '" & Me.TxtDateTo.Text & "') ORDER BY SINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtClient.Text = Nothing And Not Me.TxtInvoice.Text = Nothing And Me.TxtDateFrom.Text = Nothing And Me.TxtDateTo.Text = Nothing Then
            Str2 = "SELECT SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER, NET_TOTAL, CASH_AMT, BANK_AMT FROM V_SALE_INVOICES WHERE CLIENT_NAME LIKE '%" & Me.TxtClient.Text & "%' AND SINV_NO = " & Val(Me.TxtInvoice.Text) & " ORDER BY SINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtClient.Text = Nothing And Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing And Me.TxtInvoice.Text = Nothing Then
            Str2 = "SELECT SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER, NET_TOTAL, CASH_AMT, BANK_AMT FROM V_SALE_INVOICES WHERE CLIENT_NAME LIKE '%" & Me.TxtClient.Text & "%' AND S_DATE >= CONVERT(DATETIME, '" & Me.TxtDateFrom.Text & "') AND S_DATE <= CONVERT(DATETIME, '" & Me.TxtDateTo.Text & "') ORDER BY SINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtInvoice.Text = Nothing And Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing And Me.TxtClient.Text = Nothing Then
            Str2 = "SELECT SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER, NET_TOTAL, CASH_AMT, BANK_AMT FROM V_SALE_INVOICES WHERE SINV_NO = " & Val(Me.TxtInvoice.Text) & " AND S_DATE >= CONVERT(DATETIME, '" & Me.TxtDateFrom.Text & "') AND S_DATE <= CONVERT(DATETIME, '" & Me.TxtDateTo.Text & "') ORDER BY SINV_NO DESC"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtInvoice.Text = Nothing And Not Me.TxtDateFrom.Text = Nothing And Not Me.TxtDateTo.Text = Nothing And Not Me.TxtClient.Text = Nothing Then
            Str2 = "SELECT SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER, NET_TOTAL, CASH_AMT, BANK_AMT FROM V_SALE_INVOICES WHERE CLIENT_NAME LIKE '%" & Me.TxtClient.Text & "%' AND SINV_NO = " & Val(Me.TxtInvoice.Text) & " AND S_DATE >= CONVERT(DATETIME, '" & Me.TxtDateFrom.Text & "') AND S_DATE <= CONVERT(DATETIME, '" & Me.TxtDateTo.Text & "') ORDER BY SINV_NO DESC"
            Me.FillListView_Condition()

        Else
            Me.FillListView()
        End If
    End Sub

    Private Sub BttnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnRefresh.Click
        Me.TxtClient.Text = Nothing
        Me.TxtInvoice.Text = Nothing
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
            Dim Str1 As String = "SELECT SINV_NO, CLIENT_NAME, S_DATE, TOT_BILL, DISC_RS, DISC_PER, DISC_OTHER, NET_TOTAL, CASH_AMT, BANK_AMT FROM V_SALE_INVOICES ORDER BY SINV_NO DESC"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_SALE_INVOICES = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_SALE_INVOICES1.Clear()
            Me.daV_SALE_INVOICES.Fill(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Tot_Bill As Double = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(3).ToString)

                    .Add(Tot_Bill.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Disc_Rs, Disc_Per, Disc_Per_Amt, Disc_Other, Tot_Disc As Double

                    Disc_Rs = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(4).ToString)
                    Disc_Per = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(5).ToString)
                    Disc_Other = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(6).ToString)

                    Disc_Per_Amt = (Tot_Bill * Disc_Per) / 100

                    Tot_Disc = Disc_Rs + Disc_Per_Amt + Disc_Other

                    .Add(Tot_Disc.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Net_Total As Double = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(7).ToString)
                    .Add(Net_Total.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Tot_Receipt As Double = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(8).ToString) + Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(9).ToString)

                    .Add(Tot_Receipt.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add((Net_Total - Tot_Receipt).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
            Me.daV_SALE_INVOICES = New SDS.SqlDataAdapter(SqlCmd2)

            Me.DsV_SALE_INVOICES1.Clear()
            Me.daV_SALE_INVOICES.Fill(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Tot_Bill As Double = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(3).ToString)

                    .Add(Tot_Bill.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Disc_Rs, Disc_Per, Disc_Per_Amt, Disc_Other, Tot_Disc As Double

                    Disc_Rs = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(4).ToString)
                    Disc_Per = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(5).ToString)
                    Disc_Other = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(6).ToString)

                    Disc_Per_Amt = (Tot_Bill * Disc_Per) / 100

                    Tot_Disc = Disc_Rs + Disc_Per_Amt + Disc_Other

                    .Add(Tot_Disc.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Net_Total As Double = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(7).ToString)
                    .Add(Net_Total.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    Dim Tot_Receipt As Double = Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(8).ToString) + Val(Me.DsV_SALE_INVOICES1.V_SALE_INVOICES.Item(Cnt).Item(9).ToString)

                    .Add(Tot_Receipt.ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                    .Add((Net_Total - Tot_Receipt).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

End Class
