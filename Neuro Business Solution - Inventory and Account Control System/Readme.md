# Neuro Business Solution - Inventory and Account Control System
## Requires
- Visual Studio 2005
## License
- Apache License, Version 2.0
## Technologies
- SQL Server 2000
## Topics
- Invertory and Accounts Control System
## Updated
- 09/15/2011
## Description

<p><span style="font-size:x-large"><strong>Introduction:&nbsp;</strong></span></p>
<p><em>Neuro Business Solution - Complete Inventory and Account System</em></p>
<p><span style="font-size:x-large"><strong>Building the Sample:&nbsp;</strong></span></p>
<p><em>Complete Database Application with Client / Server Connectivity, multi interface.&nbsp;</em></p>
<p><em>Change Connection String as per your SQL Setting.&nbsp;</em></p>
<p><em>Create Database with name of &quot;Neuro_BS&quot; in SQL Server 2000.&nbsp;</em></p>
<p><em>Restore Database file with the name of &quot;Neruo_BS&quot; from Database Folder which include in .zip file.&nbsp;</em></p>
<p><span style="font-size:x-large"><strong>Description:&nbsp;</strong></span></p>
<p><span style="font-size:medium"><em>In this application you will find the following solutions</em></span></p>
<ul>
<li><strong>Complete Inventory Control</strong>
<ul>
<li>Purchase / Sale Items </li><li>Purchase Return / Sale Return with multi choice </li><li>Item Updation </li><li>Item Search Facility </li><li>Etc. </li></ul>
</li><li><strong>Bank</strong>
<ul>
<li>Bank Accounts Information </li><li>Bank Transaction </li><li>Bank Payments </li><li>Bank Receipts </li><li>Checque Deposit and Withdrawal &nbsp;&nbsp; </li><li>Adjustment Entries </li></ul>
</li><li><strong>Group / Partnership</strong>
<ul>
<li>Each Partner Record </li><li>Great help to create multi Partner Application </li></ul>
</li><li><strong>Customization</strong>
<ul>
<li>Complete Customization with LUP Codes </li></ul>
</li><li><strong>Cash / Credit</strong>
<ul>
<li>Cash Record </li><li>Credit Record </li></ul>
</li><li><strong>Ledger System</strong>
<ul>
<li>Client / Customer Ledger </li><li>Dealer / Supplier Ledger </li><li>Bank Account Ledger </li><li>Stock Ledger </li></ul>
</li><li><strong>Search Facilities</strong> </li><li><strong>Printing Facilities</strong>
<ul>
<li>Complete Crystal Reports Solution
<ul>
<li>More than 100 reports to make great decision for business </li></ul>
</li></ul>
<ul>
</ul>
</li></ul>
<h1><strong>Screen Shots</strong></h1>
<p><strong><img src="26250-001.%20splash%20screen.jpg" alt="" width="497" height="323"></strong></p>
<p>&nbsp;</p>
<p><img src="26251-002.%20login%20form.jpg" alt="" width="417" height="232"></p>
<p>&nbsp;</p>
<p><img src="26254-003.%20main%20control%20form.jpg" alt="" width="1280" height="800"></p>
<p>&nbsp;</p>
<p><img src="26257-004.%20sale%20entry%20from.jpg" alt="" width="1280" height="800"></p>
<p>&nbsp;</p>
<p><strong><img src="26258-005.%20sale%20invoice%20-%20report.jpg" alt="" width="1280" height="800"><br>
</strong></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">Imports SDS = System.Data.SqlClient
Public Class frmSALE
    Inherits System.Windows.Forms.Form


#Region &quot;VARIABLES&quot;
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

#Region &quot;PAYMENTS VARIABLES&quot;
    Public CASH_AMT, BANK_AMT, SINV_NO As Double
    Public CASH_PAY As Boolean = False, BANK_PAY As Boolean = False, BOTH_PAY As Boolean = False
    Public CHEQ_NO, CHEQ_DATE, DESCRIPTION, CHEQ_TYPE, BANK_ACC, Rec_Date As String
    Public P_Inv As String
#End Region

#Region &quot;FORM CONTROL&quot;

    Private Sub frmSALE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.BttnSave.Text = &quot;&amp;Update&quot; And Me.BttnSave.Enabled = True Then
            MsgBox(&quot;Can't close without Updating OR Cancelling Invoice&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Closing Error!&quot;)
            e.Cancel = True

        ElseIf Me.BttnSave.Text = &quot;&amp;Save&quot; And Me.BttnSave.Enabled = True Then
            MsgBox(&quot;Can't close without Saving OR Cancelling Invoice&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Closing Error!&quot;)
            e.Cancel = True

        End If
    End Sub
    Private Sub frmSALE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Client()
        Me.FillComboBox_Employee()
        Me.FillComboBox_Group()

        Dim StrSql As String = &quot;SELECT nID AS ID, sBUSINESS_GP AS [GROUP], sBANK_ACC AS BANK_ACC, sS_MAN AS S_MAN, sP_MAN AS P_MAN, sD_MAN AS D_MAN, sR_MAN AS R_MAN, sCLIENT AS CLIENT, sCLIENT_TYPE AS CLIENT_TYPE, sCLIENT_CAT AS CLIENT_CAT, sCLIENT_GD AS CLIENT_GD, sZONE AS ZONE, sROUTE AS ROUTE, sAREA AS AREA, sEXP_SUB_HEAD AS EXP_SUB_HEAD, sPRINTER AS PRINTER, sREPORT_TITLE AS RPT_TITLE, sREPORT_WARRANTY AS RPT_WARRANTY FROM NS_DEFAULT&quot;
        Dim CmdSql As New SDS.SqlCommand(StrSql, Me.SqlConnection1)
        Me.daNS_DEFAULT = New SDS.SqlDataAdapter(CmdSql)
        Me.daNS_DEFAULT.Fill(Me.DsNS_DEFAULT1.NS_DEFAULT)
        Me.Default_Setting()

        Me.Disable_All()
        Me.BttnPrev.Enabled = False
        Me.BttnPrint.Enabled = False
        Me.BttnSave.Enabled = False

        Me.TxtDate.Text = Date.Now.ToString(&quot;dd-MMM-yyyy&quot;)
        Me.TxtDispDate.Text = Date.Now.ToString(&quot;dd-MMM-yyyy&quot;)
        'Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmSALE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region &quot;TextBox Control&quot;
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.GotFocus, TxtCashClient.GotFocus, TxtDate.GotFocus, TxtDescription.GotFocus, TxtDiscount.GotFocus, TxtDiscPercent.GotFocus, TxtDispDate.GotFocus, TxtFreight.GotFocus, TxtNetTotal.GotFocus, TxtOtherDisc.GotFocus, TxtReceipt.GotFocus, TxtTotal.GotFocus, TxtTotalItems.GotFocus, TxtTRno.GotFocus, TxtTRqty.GotFocus, TxtUnloading.GotFocus, TxtVehicle.GotFocus, TxtRemarks.GotFocus, TxtCashMemo.GotFocus, TxtReceivables.GotFocus, TxtClientBal.GotFocus, TxtStandyBy.GotFocus, TxtNET_Receivable.GotFocus, TxtInvBalance.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
        Dim Ctrl As Control = sender
        Select Case Ctrl.Name
            Case &quot;TxtDescription&quot;
                If sender.Text = &quot;Other's Description Here!&quot; Then
                    sender.Text = Nothing
                End If
        End Select
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.LostFocus, TxtCashClient.LostFocus, TxtDate.LostFocus, TxtDescription.LostFocus, TxtDiscount.LostFocus, TxtDiscPercent.LostFocus, TxtDispDate.LostFocus, TxtFreight.LostFocus, TxtInvBalance.LostFocus, TxtNetTotal.LostFocus, TxtOtherDisc.LostFocus, TxtReceivables.LostFocus, TxtReceipt.LostFocus, TxtClientBal.LostFocus, TxtTotal.LostFocus, TxtTotalItems.LostFocus, TxtTRno.LostFocus, TxtTRqty.LostFocus, TxtUnloading.LostFocus, TxtVehicle.LostFocus, TxtRemarks.LostFocus, TxtStandyBy.LostFocus, TxtNET_Receivable.LostFocus, TxtCashMemo.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case &quot;TxtDate&quot;
                    If sender.TextLength &gt; 0 Then
                        sender.Text = CDate(sender.text).ToString(&quot;dd-MMM-yyyy&quot;)
                    End If

                Case &quot;TxtDispDate&quot;
                    If sender.TextLength &gt; 0 Then
                        sender.Text = CDate(sender.text).ToString(&quot;dd-MMM-yyyy&quot;)
                    End If

            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub

    'Leave
    Private Sub TxtTRqty_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtTRqty.Leave
        If Me.TxtTRqty.Text = Nothing Then
            Me.TxtTRqty.Text = 0
        End If
    End Sub

    'KeyPress Numeric
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTotalItems.KeyPress
        Me.asNum.NumPress(True, e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtDiscount.KeyPress, TxtFreight.KeyPress, TxtInvBalance.KeyPress, TxtNetTotal.KeyPress, TxtOtherDisc.KeyPress, TxtReceivables.KeyPress, TxtReceipt.KeyPress, TxtClientBal.KeyPress, TxtTotal.KeyPress, TxtUnloading.KeyPress, TxtDiscPercent.KeyPress, TxtStandyBy.KeyPress, TxtNET_Receivable.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    'NET TOTAL CALCULATION
    Private Sub TxtUnloading_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtUnloading.TextChanged, TxtFreight.TextChanged, TxtOtherDisc.TextChanged, TxtDiscPercent.TextChanged, TxtDiscount.TextChanged, TxtTotal.TextChanged
        If Me.Search_Inv = False Then
            On Error GoTo Fix
            Dim Freight, Unloading, Total, Disc_RS, Disc_Age, Disc_Other As Double
            Freight = Val(Me.TxtFreight.Text)
            Unloading = Val(Me.TxtUnloading.Text)
            Total = Val(Me.TxtTotal.Text)
            Disc_RS = Val(Me.TxtDiscount.Text)
            Disc_Age = (Total * Val(Me.TxtDiscPercent.Text)) / 100
            Disc_Other = Val(Me.TxtOtherDisc.Text)

            Me.TxtNetTotal.Text = (Total &#43; Freight &#43; Unloading) - (Disc_RS &#43; Disc_Age &#43; Disc_Other)
            Me.TxtNetTotal.Text = Decimal.Round(CDec(Me.TxtNetTotal.Text), 2)
Fix:
        End If

    End Sub

    'Net Receivable Calculation
    Private Sub TxtReceivables_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtStandyBy.TextChanged
        Me.TxtReceivables.Text = Val(Me.TxtNET_Receivable.Text) &#43; Val(Me.TxtStandyBy.Text)
    End Sub

    'Invoice &amp; Supplier Balance Calculation
    Private Sub TxtReceipt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtReceipt.TextChanged, TxtNetTotal.TextChanged, TxtNET_Receivable.TextChanged

        Me.TxtReceivables.Text = Val(Me.TxtNET_Receivable.Text) &#43; Val(Me.TxtStandyBy.Text)

        On Error GoTo Fix

        If Me.TxtReceipt.TextLength &gt; 0 Then
            Me.TxtReceipt.Text = Decimal.Round(CDec(Me.TxtReceipt.Text), 2)
        End If

        Me.TxtInvBalance.Text = Val(Me.TxtNetTotal.Text) - Val(Me.TxtReceipt.Text)
        Me.TxtClientBal.Text = Val(Me.TxtNET_Receivable.Text) &#43; Val(Me.TxtInvBalance.Text)

        Me.TxtInvBalance.Text = Decimal.Round(CDec(Me.TxtInvBalance.Text), 2)
        Me.TxtClientBal.Text = Decimal.Round(CDec(Me.TxtClientBal.Text), 2)

        If Val(Me.TxtNetTotal.Text) &gt; 0 Then
            Me.BttnReceipt.Enabled = True
        Else
            Me.BttnReceipt.Enabled = False
        End If

Fix:
    End Sub

    'Fill data for Modification
    Private Sub TxtInvoice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInvoice.TextChanged
        If Me.Search_Inv = True Then

            'FILL MASTER RECORD
            Me.Fill_Master_Date()

            'FILL DETAIL RECORD
            Me.Fill_Detail_Data()

            Me.Search_Inv = False

            'FILL TOTAL PAYMENT
            Me.Fill_Receipt_Data()

            Dim StrClient As String = Me.CmbClient.Text
            Me.CmbClient.SelectedIndex = -1
            Me.CmbClient.SelectedIndex = Me.CmbClient.FindString(StrClient)

            'MsgBox(Me.TxtInvoice.Text)
        End If

    End Sub
#End Region

#Region &quot;ComboBox Controls&quot;
    Private Sub CmbClient_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbClient.SelectedIndexChanged, CmbGroup.SelectedIndexChanged
        Try
            Dim Str1 As String = &quot;SELECT CLIENT_ID, CONVERT(NUMERIC(18,2),CLIENT_BAL) AS CLIENT_BAL, GROUP_ID FROM SV_CLIENT_BALANCE WHERE CLIENT_ID=&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot; AND GROUP_ID=&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;&quot;
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_CLIENT_BAL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_CLIENT_BAL1.Clear()
            Me.daV_CLIENT_BAL.Fill(Me.DsV_CLIENT_BAL1.SV_CLIENT_BALANCE)

        Catch ex As Exception

        End Try

        Try
            Dim Str1 As String = &quot;SELECT CLIENT_ID, CONVERT(NUMERIC(18,2),TOT_STANDBY_CHEQ) AS TOT_STANDBY_CHEQ, GROUP_ID FROM V_CLIENT_TOT_STANDBY_CHEQ WHERE CLIENT_ID=&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot; AND GROUP_ID=&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;&quot;
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_CLIENT_STANDBY_CHEQ = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_CLIENT_STANDBY_CHEQ1.Clear()
            Me.daV_CLIENT_STANDBY_CHEQ.Fill(Me.DsV_CLIENT_STANDBY_CHEQ1.V_CLIENT_TOT_STANDBY_CHEQ)
        Catch ex As Exception

        End Try

    End Sub
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbD_Man.GotFocus, CmbGroup.GotFocus, CmbClient.GotFocus, CmbS_Man.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbD_Man.LostFocus, CmbGroup.LostFocus, CmbClient.LostFocus, CmbS_Man.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region &quot;Select Item Controls&quot;
    Private Sub SelectItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectItemToolStripMenuItem.Click
        On Error GoTo Fix
        frmSELECT_ITEM_BATCH.TxtCompany.Text = Nothing
        frmSELECT_ITEM_BATCH.TxtItem.Text = Nothing
        frmSELECT_ITEM_BATCH.FrmStr = &quot;Sale&quot;
        frmSELECT_ITEM_BATCH.Row = Me.DataGridView1.CurrentRow.Index

        frmSELECT_ITEM_BATCH.ShowDialog(Me)
Fix:
    End Sub
#End Region

#Region &quot;DataGridView Control&quot;
    Dim ItemCode_Old As String

    Private Sub DataGridView1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
        ItemCode_Old = Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCode&quot;).Value
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If Me.Search_Inv = False Then

            Try
                Me.DsLUP_ITEM1.Clear()
                Me.DsSV_STOCK_BAL1.Clear()

                Me.LblB_Pcs.Text = 0
                Me.LblPPP.Text = 0
                Me.LblRatePcs.Text = 0
                Me.LblRate.Text = 0
                Me.LblRetail.Text = 0
                Me.LblStock.Text = 0

                'FILL TOP LABLES OF DATAGRID
                'WORKING ON BATCH WISE STOCK CALCULATION---CHECK BATCH EXIST OR NOT?
                If Not Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCode&quot;).Value Is Nothing Then
                    Dim SalePrice As Double = 0, Bonus_Q As Double = 0
                    Dim Str1 As String = &quot;SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, sCLAIMABLE, sSTATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM WHERE nCODE=&quot; &amp; Val(Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCode&quot;).Value) &amp; &quot;&quot;
                    Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
                    Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

                    Me.DsLUP_ITEM1.Clear()
                    Me.daLUP_ITEM.Fill(Me.DsLUP_ITEM1.V_LUP_ITEM)

                    If Me.DsLUP_ITEM1.V_LUP_ITEM.Count = 0 Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCode&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColBatch&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColName&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCost&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColRate&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColPack&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColPiece&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColBonus&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColPercentage&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColDisc_Rs&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColSaleTax&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColTotal&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColScmQty&quot;).Value = Nothing
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColPPP&quot;).Value = Nothing

                        'SET FOCUS TO ColCode IS PENDING

                        Me.SelectItemToolStripMenuItem_Click(sender, e)

                        Me.LblB_Pcs.Text = 0
                        Me.LblPPP.Text = 0
                        Me.LblRatePcs.Text = 0
                        Me.LblRate.Text = 0
                        Me.LblRetail.Text = 0
                        Me.LblStock.Text = 0

                        Exit Sub
                    End If

                    Dim Str10 As String = &quot;SELECT nCODE AS CODE, STK_BAL FROM SV_STOCK_BAL WHERE nCODE=&quot; &amp; Val(Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCode&quot;).Value) &amp; &quot;&quot;
                    Dim SqlCmd10 As New SDS.SqlCommand(Str10, Me.SqlConnection2)
                    Me.daSV_STOCK_BAL = New SDS.SqlDataAdapter(SqlCmd10)

                    Me.DsSV_STOCK_BAL1.Clear()
                    Me.daSV_STOCK_BAL.Fill(Me.DsSV_STOCK_BAL1.SV_STOCK_BAL)

                    Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColName&quot;).Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(1).ToString()
                    Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColPPP&quot;).Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(3).ToString()
                    Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCost&quot;).Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(6).ToString()
                    Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColSaleTax&quot;).Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(11).ToString()

                    Dim Itm_Code As String = Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCode&quot;).Value

                    If Not Itm_Code = ItemCode_Old Then
                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColRate&quot;).Value = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(7).ToString()

                    End If

                    Me.LblRatePcs.Text = Val(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(7).ToString()) / Val(Me.LblPPP.Text)

                    If Val(Me.LblPPP.Text) &lt;= 0 Then
                        MsgBox(&quot;Please confirm 'Item Code' or Item Detail&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Error!&quot;)

                    ElseIf Not Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColRate&quot;).Value Is Nothing Then

                        ''Sale Price Notification/Alert
                        'If Me.DataGridView1.Item(&quot;ColRate&quot;, Me.DataGridView1.CurrentCell.RowIndex).Selected = True Then
                        '    If Not SalePrice = Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColRate&quot;).Value Then
                        '        MsgBox(&quot;Rate is not Equal to Actual Rate&quot;, MsgBoxStyle.Information, &quot;(NS) - Rate&quot;)
                        '    End If

                        'ElseIf Me.DataGridView1.Item(&quot;ColBonus&quot;, Me.DataGridView1.CurrentCell.RowIndex).Selected = True Then
                        '    'Bonus Qty Notification/Alert
                        '    If Not Bonus_Q = Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColBonus&quot;).Value Then
                        '        If MsgBox(&quot;Are you sure to Give him Bonus on this Item?&quot;, MsgBoxStyle.Question &#43; vbYesNo, &quot;(NS) - Rate&quot;) = MsgBoxResult.No Then
                        '            Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColBonus&quot;).Value = 0
                        '        End If
                        '    End If
                        'End If

                        Dim Rate, PPP, Pks, Pcs, Line_Tot, P_age, Disc_Rs, S_Tax As Double
                        Rate = Val(Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColRate&quot;).Value)
                        PPP = Val(Me.LblPPP.Text)
                        Pks = Val(Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColPack&quot;).Value)
                        Pcs = Val(Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColPiece&quot;).Value)
                        P_age = Val(Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColPercentage&quot;).Value)
                        Disc_Rs = Val(Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColDisc_Rs&quot;).Value)
                        S_Tax = Val(Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColSaleTax&quot;).Value)

                        Line_Tot = ((Rate / PPP) * ((Pks * PPP) &#43; Pcs))
                        S_Tax = (Line_Tot * S_Tax) / 100
                        Line_Tot = (Line_Tot - (((Line_Tot * P_age) / 100) &#43; Disc_Rs) &#43; S_Tax)
                        Line_Tot = Decimal.Round(CDec(Line_Tot), 2)

                        Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColTotal&quot;).Value = Line_Tot

                        Dim i As Integer
                        Me.TxtTotal.Text = &quot;0.00&quot;

                        For i = 0 To Me.DataGridView1.Rows.Count - 1
                            Me.TxtTotal.Text = Val(Me.TxtTotal.Text) &#43; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColTotal&quot;).Value)
                        Next

                    End If

                    Me.TxtTotalItems.Text = Me.DataGridView1.Rows.Count - 1

                    SalePrice = Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(&quot;UNIT_RATE&quot;)
                    Bonus_Q = Val(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(0).Item(&quot;nBONUS_ON_PCS&quot;))

                Else
                    Me.LblB_Pcs.Text = 0
                    Me.LblPPP.Text = 0
                    Me.LblRatePcs.Text = 0
                    Me.LblRate.Text = 0
                    Me.LblRetail.Text = 0
                    Me.LblStock.Text = 0
                End If

                If Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPPP&quot;).Value = 1 Then
                    If Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColCode&quot;).Value Is Nothing And Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPack&quot;).Value Is Nothing Then
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPack&quot;).Value = 0
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value = 0

                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPack&quot;).Value = 1
                    End If

                    If Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value &gt; 0 Then
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPack&quot;).Value = Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPack&quot;).Value) &#43; Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value)
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value = 0

                    End If

                ElseIf Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPPP&quot;).Value &gt; 1 Then
                    If Not Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColCode&quot;).Value Is Nothing And Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value Is Nothing Then
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPack&quot;).Value = 0
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value = 0

                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value = 1
                    End If

                    If Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPPP&quot;).Value &lt;= Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value Then
                        Dim PPP, PCS, PKS As Integer
                        PPP = Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPPP&quot;).Value
                        PCS = Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value
                        PKS = PCS / PPP
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPack&quot;).Value = Val(Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPack&quot;).Value) &#43; PKS
                        Me.DataGridView1.Rows(e.RowIndex - 1).Cells(&quot;ColPiece&quot;).Value = PCS Mod PPP
                    End If
                End If

            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try
        End If


    End Sub

    Private Sub DataGridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        ItemCode_Old = Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColCode&quot;).Value
        Me.DataGridView1_CellValueChanged(sender, e)
    End Sub
    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        Dim i As Integer
        Me.TxtTotal.Text = &quot;0.00&quot;
        For i = 0 To Me.DataGridView1.Rows.Count - 1
            Me.TxtTotal.Text = Val(Me.TxtTotal.Text) &#43; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColTotal&quot;).Value)
        Next

    End Sub
    Private Sub DataGridView1_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles DataGridView1.UserDeletingRow
        If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbClient.SelectedIndex = -1 Or Me.CmbClient.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
            MsgBox(&quot;Please enter description OR select correct value!&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Entry Required!&quot;)

            Me.Null_Focus()


        ElseIf MsgBox(&quot;Are you sure to Delete Item?&quot;, MsgBoxStyle.Critical &#43; vbYesNo, &quot;(NS) - Deleting Item?&quot;) = MsgBoxResult.Yes Then
            'DELETE FROM SALE DETAIL
            Me.asDelete.DeleteValue_NoErr(&quot;DELETE FROM SALE_DETAIL WHERE nID=&quot; &amp; Val(Me.DataGridView1.Rows(e.Row.Index).Cells(&quot;ColSR&quot;).Value) &amp; &quot;&quot;)

            'UPDATE VALUE IN SALE MASTER
            Me.asUpdate.UpdateValue_NoErr(&quot;UPDATE SALE_MASTER SET nCLIENT_ID='&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;', sCASH_CLIENT='&quot; &amp; Me.TxtCashClient.Text &amp; &quot;', sCASH_MEMO_NO='&quot; &amp; Me.TxtCashMemo.Text &amp; &quot;', nLPINV_NO=&quot; &amp; Val(Me.LblLoadPass.Text) &amp; &quot;, dDATE='&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', dDISP_DATE='&quot; &amp; CDate(Me.TxtDispDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', sVEHICLE='&quot; &amp; Me.TxtVehicle.Text &amp; &quot;', nFREIGHT=&quot; &amp; Val(Me.TxtFreight.Text) &amp; &quot;, nUNLOADING=&quot; &amp; Val(Me.TxtUnloading.Text) &amp; &quot;, sTR_NO='&quot; &amp; Me.TxtTRno.Text &amp; &quot;', nTR_QTY=&quot; &amp; Val(Me.TxtTRqty.Text) &amp; &quot;, nTOTAL_BILL=&quot; &amp; Val(Me.TxtTotal.Text) &amp; &quot;, nDISCOUNT=&quot; &amp; Val(Me.TxtDiscount.Text) &amp; &quot;, nDISC_PERCENT=&quot; &amp; Val(Me.TxtDiscPercent.Text) &amp; &quot;, nOTHERS=&quot; &amp; Val(Me.TxtOtherDisc.Text) &amp; &quot;, sOTHER_DESC='&quot; &amp; Me.TxtDescription.Text &amp; &quot;', nNET_TOTAL= &quot; &amp; Val(Me.TxtNetTotal.Text) &amp; &quot;, nEMPLOYEE_CODE=&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;, nLOGIN_ID=10, nBUSINESS_CODE=&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;, sREMARKS='&quot; &amp; Me.TxtRemarks.Text &amp; &quot;' WHERE nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;)

            Me.BttnNew.Enabled = False
            Me.BttnAdd.Enabled = False
        Else
            e.Cancel = True
        End If

    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        If Asc(e.KeyChar) = Keys.Escape Then
            If Me.BttnNew.Text = &quot;Ca&amp;ncel&quot; Then
                Me.BttnNew_Click(sender, e)
            Else
                Me.BttnClose_Click(sender, e)
            End If
        End If
    End Sub
#End Region

#Region &quot;Button Control&quot;
    Private Sub BttnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAdd.Click
        Me.TxtInvoice.Text = Val(Me.TxtInvoice.Text) &#43; 1
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        If Me.BttnNew.Text = &quot;&amp;New&quot; Then
            Me.Enable_All()

            Me.TxtReceipt.Text = &quot;0.00&quot;

            Rec_Date = Nothing
            CASH_AMT = 0
            CHEQ_NO = Nothing
            CHEQ_TYPE = Nothing
            CHEQ_DATE = Nothing
            BANK_AMT = 0
            BANK_ACC = Nothing
            SINV_NO = 0
            DESCRIPTION = Nothing

            Me.BOTH_PAY = False
            Me.BANK_PAY = False
            Me.CASH_PAY = False

            Me.BttnSearch_Inv.Enabled = False
            Me.BttnPrev.Enabled = False
            Me.BttnPrint.Enabled = False
            Me.BttnReceipt.Enabled = False
            Me.BttnSave.Enabled = True
            Me.BttnSave.Text = &quot;&amp;Save&quot;
            Me.BttnClose.Enabled = False

            Me.CancelButton = Me.BttnNew

            Me.Clear_All()
            Me.BttnNew.Text = &quot;Ca&amp;ncel&quot;

            Me.TxtInvoice.Text = Me.asMAX.LoadValue(Rd, &quot;SELECT MAX(nSINV_NO) FROM SALE_MASTER&quot;) &#43; 1

        ElseIf Me.BttnNew.Text = &quot;Ca&amp;ncel&quot; Then
            If MsgBox(&quot;Are you sure to Cancel this Invoice?&quot;, MsgBoxStyle.Critical &#43; vbYesNo, &quot;(NS) - Cancel Invoice?&quot;) = MsgBoxResult.Yes Then
                Me.Disable_All()

                Me.BttnPrev.Enabled = False
                Me.BttnPrint.Enabled = False
                Me.BttnSave.Enabled = False
                Me.BttnClose.Enabled = True
                Me.BttnSearch_Inv.Enabled = True
                Me.BttnSave.Text = &quot;&amp;Save&quot;
                Me.CancelButton = Me.BttnClose

                Me.Search_Inv = False

                Me.Clear_All()
                Me.BttnNew.Text = &quot;&amp;New&quot;
            End If
        End If
    End Sub

    Private Sub BttnReceipt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnReceipt.Click
        ''FILL TOTAL PAYMENT
        'Me.Fill_Receipt_Data()

        'If Not SINV_NO &lt;= 0 Then

        frmSINV_RECEIPT.TxtCashPmt.Text = Me.CASH_AMT
        frmSINV_RECEIPT.TxtBankPmt.Text = Me.BANK_AMT

        frmSINV_RECEIPT.BnkACC = Me.BANK_ACC
        frmSINV_RECEIPT.TxtChequeNo.Text = Me.CHEQ_NO
        frmSINV_RECEIPT.TxtChequeDate.Text = Me.CHEQ_DATE
        frmSINV_RECEIPT.TxtChequeType.Text = Me.CHEQ_TYPE

        'Else
        'frmSINV_RECEIPT.TxtCashPmt.Text = &quot;0.00&quot;
        'frmSINV_RECEIPT.TxtBankPmt.Text = &quot;0.00&quot;

        'frmSINV_RECEIPT.CmbBankAccount.SelectedIndex = -1
        'frmSINV_RECEIPT.TxtChequeNo.Text = Nothing
        'frmSINV_RECEIPT.TxtChequeDate.Text = Nothing
        'frmSINV_RECEIPT.TxtChequeType.Text = Nothing

        'End If

        On Error GoTo Fix

        frmSINV_RECEIPT.ShowDialog(Me)

Fix:
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click
        If Not CDate(Me.TxtDate.Text) &gt;= CDate(&quot;01-03-2010&quot;) Then

            Me.asSELECT.SavedpFlg1(Rd, &quot;SELECT * FROM SALE_MASTER WHERE nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;)

            If Me.TxtDescription.Text = &quot;Other's Description Here!&quot; Then
                Me.TxtDescription.Text = Nothing
            End If

            If Me.BttnSave.Text = &quot;&amp;Save&quot; Then

                If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbClient.SelectedIndex = -1 Or Me.CmbClient.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
                    MsgBox(&quot;Please enter description OR select correct value!&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Entry Required!&quot;)

                    Me.Null_Focus()

                ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) &lt;= 0 Then
                    MsgBox(&quot;Please enter atleast one Item to save Invoice!&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Entry Required!&quot;)
                    Me.DataGridView1.Focus()

                ElseIf Me.asSELECT.pFlg1 = False Then
                    If Val(Me.TxtInvBalance.Text) &lt; 0 Then
                        MsgBox(&quot;Can't save!&quot; &amp; vbCrLf &amp; &quot;Payment is more then Invoice Total&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Wrong Value!&quot;)
                        Me.BttnReceipt.Focus()

                    Else
                        'INSERT VALUES IN SALE MASTER
                        Me.asInsert.SaveValue(&quot;INSERT INTO SALE_MASTER(nSINV_NO, nCLIENT_ID, sCASH_CLIENT, sCASH_MEMO_NO, nLPINV_NO, dDATE, dDISP_DATE, sVEHICLE, nFREIGHT, nUNLOADING, sTR_NO, nTR_QTY, nTOTAL_BILL, nDISCOUNT, nDISC_PERCENT, nOTHERS, sOTHER_DESC, nNET_TOTAL, nEMPLOYEE_CODE, nLOGIN_ID, nBUSINESS_CODE, sREMARKS, nD_MAN_CODE) VALUES(&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.TxtCashClient.Text &amp; &quot;', '&quot; &amp; Me.TxtCashMemo.Text &amp; &quot;', &quot; &amp; Val(Me.LblLoadPass.Text) &amp; &quot;,'&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;','&quot; &amp; CDate(Me.TxtDispDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', '&quot; &amp; Me.TxtVehicle.Text &amp; &quot;', &quot; &amp; Val(Me.TxtFreight.Text) &amp; &quot;, &quot; &amp; Val(Me.TxtUnloading.Text) &amp; &quot;, '&quot; &amp; Me.TxtTRno.Text &amp; &quot;', &quot; &amp; Val(Me.TxtTRqty.Text) &amp; &quot;,&quot; &amp; Val(Me.TxtTotal.Text) &amp; &quot;,&quot; &amp; Val(Me.TxtDiscount.Text) &amp; &quot;,&quot; &amp; Val(Me.TxtDiscPercent.Text) &amp; &quot;,&quot; &amp; Val(Me.TxtOtherDisc.Text) &amp; &quot;,'&quot; &amp; Me.TxtDescription.Text &amp; &quot;', &quot; &amp; Val(Me.TxtNetTotal.Text) &amp; &quot;,&quot; &amp; Val(Me.CmbS_Man.SelectedItem.Col3) &amp; &quot;,10,&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.TxtRemarks.Text &amp; &quot;',&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;)&quot;)

                        Dim i As Integer
                        For i = 0 To Me.DataGridView1.Rows.Count - 2
                            Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPPP&quot;).Value)
                            Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPack&quot;).Value)
                            Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPiece&quot;).Value)
                            Dim Bonus As Double = Val(Me.DataGridView1.Rows(i).Cells(&quot;ColBonus&quot;).Value)
                            Dim Tot_Pcs As Double
                            Tot_Pcs = (Pks * PPP) &#43; (Pcs &#43; Bonus)

                            'INSERT VALUES IN SALE DETAIL
                            Me.asInsert.SaveValue(&quot;INSERT INTO SALE_DETAIL (nSINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, dDATE)VALUES(&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColCode&quot;).Value) &amp; &quot;, '&quot; &amp; Me.DataGridView1.Rows(i).Cells(&quot;ColBatch&quot;).Value &amp; &quot;', &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColCost&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColRate&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColDisc_Rs&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPercentage&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColSaleTax&quot;).Value) &amp; &quot;,&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPPP&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPack&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPiece&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColBonus&quot;).Value) &amp; &quot;, &quot; &amp; Tot_Pcs &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColTotal&quot;).Value) &amp; &quot;, '&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;')&quot;)

                        Next

                        If Me.CASH_PAY = True Then
                            If Me.CASH_AMT &gt; 0 Then
                                Me.asInsert.SaveValueIN(&quot;INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;',&quot; &amp; Me.CASH_AMT &amp; &quot;,&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,10,&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;,&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.DESCRIPTION &amp; &quot;')&quot;)
                            End If

                        ElseIf Me.BANK_PAY = True Then
                            If Me.BANK_AMT &gt; 0 Then
                                Me.asInsert.SaveValueIN(&quot;INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, '&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', '&quot; &amp; Me.CHEQ_NO &amp; &quot;', '&quot; &amp; Me.CHEQ_TYPE &amp; &quot;', '&quot; &amp; CDate(Me.CHEQ_DATE).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', &quot; &amp; Me.BANK_AMT &amp; &quot;,1,'&quot; &amp; Me.BANK_ACC &amp; &quot;',&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,10,&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;,&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.DESCRIPTION &amp; &quot;')&quot;)
                            End If

                        ElseIf Me.BOTH_PAY = True Then
                            If Me.CASH_AMT &gt; 0 And Me.BANK_AMT &gt; 0 Then
                                Me.asInsert.SaveValueIN(&quot;INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, '&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', &quot; &amp; Me.CASH_AMT &amp; &quot;, '&quot; &amp; Me.CHEQ_NO &amp; &quot;', '&quot; &amp; Me.CHEQ_TYPE &amp; &quot;', '&quot; &amp; CDate(Me.CHEQ_DATE).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', &quot; &amp; Me.BANK_AMT &amp; &quot;,1,'&quot; &amp; Me.BANK_ACC &amp; &quot;',&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,10,&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;,&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.DESCRIPTION &amp; &quot;')&quot;)
                            End If

                        Else
                            MsgBox(&quot;Credit Sale Invoice Saved!&quot;, MsgBoxStyle.Information, &quot;(NS) - Credit Invoice!&quot;)

                        End If

                        Me.BttnPrev.Enabled = True
                        Me.BttnPrint.Enabled = True
                        Me.BttnSearch_Inv.Enabled = True
                        Me.BttnReceipt.Enabled = False
                        Me.BttnNew.Text = &quot;&amp;New&quot;
                        Me.BttnSave.Enabled = False
                        Me.BttnClose.Enabled = True

                    End If


                ElseIf Me.asSELECT.pFlg1 = True Then
                    MsgBox(&quot;This Invoice # '&quot; &amp; Me.TxtInvoice.Text &amp; &quot;' is Already Exist. &quot; &amp; vbCrLf &amp; &quot;To modify this invoice please click on 'Search Invoice' Button&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Already Exist!&quot;)

                End If

                'UPDATE SALE INVOICE
            ElseIf Me.BttnSave.Text = &quot;&amp;Update&quot; Then
                If Me.TxtInvoice.Text = Nothing Or Me.TxtDate.Text = Nothing Or Me.TxtDispDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbClient.SelectedIndex = -1 Or Me.CmbClient.Text = Nothing Or Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
                    MsgBox(&quot;Please enter description OR select correct value!&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Entry Required!&quot;)
                    Me.Null_Focus()

                ElseIf Me.DataGridView1.Rows.Count = 1 Or Val(Me.TxtTotal.Text) &lt;= 0 Then
                    MsgBox(&quot;Please enter atleast one Item to save Invoice!&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Entry Required!&quot;)
                    Me.DataGridView1.Focus()

                ElseIf Me.asSELECT.pFlg1 = True Then


                    If Val(Me.TxtInvBalance.Text) &lt; 0 Then
                        MsgBox(&quot;Can't save!&quot; &amp; vbCrLf &amp; &quot;Payment is more then Invoice Total&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Wrong Value!&quot;)
                        Me.BttnReceipt.Focus()

                    Else
                        'UPDATE VALUES IN SALE MASTER
                        Me.asUpdate.UpdateValue(&quot;UPDATE SALE_MASTER SET nCLIENT_ID='&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;', sCASH_CLIENT='&quot; &amp; Me.TxtCashClient.Text &amp; &quot;', sCASH_MEMO_NO='&quot; &amp; Me.TxtCashMemo.Text &amp; &quot;', nLPINV_NO=&quot; &amp; Val(Me.LblLoadPass.Text) &amp; &quot;, dDATE='&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', dDISP_DATE='&quot; &amp; CDate(Me.TxtDispDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', sVEHICLE='&quot; &amp; Me.TxtVehicle.Text &amp; &quot;', nFREIGHT=&quot; &amp; Val(Me.TxtFreight.Text) &amp; &quot;, nUNLOADING=&quot; &amp; Val(Me.TxtUnloading.Text) &amp; &quot;, sTR_NO='&quot; &amp; Me.TxtTRno.Text &amp; &quot;', nTR_QTY=&quot; &amp; Val(Me.TxtTRqty.Text) &amp; &quot;, nTOTAL_BILL=&quot; &amp; Val(Me.TxtTotal.Text) &amp; &quot;, nDISCOUNT=&quot; &amp; Val(Me.TxtDiscount.Text) &amp; &quot;, nDISC_PERCENT=&quot; &amp; Val(Me.TxtDiscPercent.Text) &amp; &quot;, nOTHERS=&quot; &amp; Val(Me.TxtOtherDisc.Text) &amp; &quot;, sOTHER_DESC='&quot; &amp; Me.TxtDescription.Text &amp; &quot;', nNET_TOTAL= &quot; &amp; Val(Me.TxtNetTotal.Text) &amp; &quot;, nEMPLOYEE_CODE=&quot; &amp; Val(Me.CmbS_Man.SelectedItem.Col3) &amp; &quot;, nLOGIN_ID=10, nBUSINESS_CODE=&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;, sREMARKS='&quot; &amp; Me.TxtRemarks.Text &amp; &quot;', nD_MAN_CODE=&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot; WHERE nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;)

                        Dim i As Integer
                        For i = 0 To Me.DataGridView1.Rows.Count - 2
                            Dim PPP As Double = Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPPP&quot;).Value)
                            Dim Pks As Double = Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPack&quot;).Value)
                            Dim Pcs As Double = Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPiece&quot;).Value)
                            Dim Bonus As Double = Val(Me.DataGridView1.Rows(i).Cells(&quot;ColBonus&quot;).Value)
                            Dim Tot_Pcs As Double
                            Tot_Pcs = (Pks * PPP) &#43; (Pcs &#43; Bonus)

                            If Me.DataGridView1.Rows(i).Cells(&quot;ColSR&quot;).Value = Nothing Then
                                'INSERT VALUES IN SALE DETAIL
                                Me.asInsert.SaveValue(&quot;INSERT INTO SALE_DETAIL (nSINV_NO, nITEM_CODE, sBATCH_NO, nUNIT_COST, nUNIT_RATE, nDISC_RS, nDISC_PER, nSALE_TAX, nPPP, nQTY_PKS, nQTY_PCS, nQTY_BONUS, nQTY_Tot_PCS, nTOTAL_VALUE, dDATE)VALUES(&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColCode&quot;).Value) &amp; &quot;, '&quot; &amp; Me.DataGridView1.Rows(i).Cells(&quot;ColBatch&quot;).Value &amp; &quot;', &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColCost&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColRate&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColDisc_Rs&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPercentage&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColSaleTax&quot;).Value) &amp; &quot;,&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPPP&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPack&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPiece&quot;).Value) &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColBonus&quot;).Value) &amp; &quot;, &quot; &amp; Tot_Pcs &amp; &quot;, &quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColTotal&quot;).Value) &amp; &quot;, '&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;')&quot;)

                            ElseIf Not Me.DataGridView1.Rows(i).Cells(&quot;ColSR&quot;).Value = Nothing Then

                                'UPDATE VALUES IN SALE DETAIL
                                Me.asUpdate.UpdateValue(&quot;UPDATE SALE_DETAIL SET nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;, nITEM_CODE=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColCode&quot;).Value) &amp; &quot;, sBATCH_NO='&quot; &amp; Me.DataGridView1.Rows(i).Cells(&quot;ColBatch&quot;).Value &amp; &quot;', nUNIT_COST=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColCost&quot;).Value) &amp; &quot;, nUNIT_RATE=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColRate&quot;).Value) &amp; &quot;, nDISC_RS=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColDisc_Rs&quot;).Value) &amp; &quot;, nDISC_PER=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPercentage&quot;).Value) &amp; &quot;, nSALE_TAX=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColSaleTax&quot;).Value) &amp; &quot;, nPPP=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPPP&quot;).Value) &amp; &quot;, nQTY_PKS=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPack&quot;).Value) &amp; &quot;, nQTY_PCS=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColPiece&quot;).Value) &amp; &quot;, nQTY_BONUS=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColBonus&quot;).Value) &amp; &quot;, nQTY_Tot_PCS=&quot; &amp; Tot_Pcs &amp; &quot;, nTOTAL_VALUE=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColTotal&quot;).Value) &amp; &quot;, dDATE='&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;' WHERE nID=&quot; &amp; Val(Me.DataGridView1.Rows(i).Cells(&quot;ColSR&quot;).Value.ToString) &amp; &quot;&quot;)

                            End If

                        Next

                        Me.asSELECT.SavedpFlg2(Rd, &quot;SELECT * FROM CLIENT_RECEIPT WHERE nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;)

                        If Me.CASH_PAY = True Then
                            If Me.asSELECT.pFlg2 = True Then
                                Me.asUpdate.UpdateValueIN(&quot;UPDATE CLIENT_RECEIPT SET nCLIENT_ID=&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, dDATE='&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', nCASH_AMOUNT=&quot; &amp; Me.CASH_AMT &amp; &quot;, nLOGIN_ID=10, nBUSINESS_CODE=&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;, nEMP_CODE=&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;, sDESCRIPTON='&quot; &amp; Me.DESCRIPTION &amp; &quot;' WHERE nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;)

                            Else
                                Me.asInsert.SaveValueIN(&quot;INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;',&quot; &amp; Me.CASH_AMT &amp; &quot;,&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,10,&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;,&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.DESCRIPTION &amp; &quot;')&quot;)

                            End If


                        ElseIf Me.BANK_PAY = True Then
                            If Me.asSELECT.pFlg2 = True Then
                                Me.asUpdate.UpdateValueIN(&quot;UPDATE CLIENT_RECEIPT SET nCLIENT_ID=&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, dDATE='&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', sCHEQUE_NO='&quot; &amp; Me.CHEQ_NO &amp; &quot;',sCHEQUE_TYPE='&quot; &amp; Me.CHEQ_TYPE &amp; &quot;', dCHEQUE_DATE='&quot; &amp; CDate(Me.CHEQ_DATE).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', nCHEQUE_AMOUNT=&quot; &amp; Me.BANK_AMT &amp; &quot;, sACCOUNT_CODE='&quot; &amp; Me.BANK_ACC &amp; &quot;, nLOGIN_ID=10, nBUSINESS_CODE=&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;, nEMP_CODE=&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;, sDESCRIPTON='&quot; &amp; Me.DESCRIPTION &amp; &quot;' WHERE nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;)

                            Else
                                Me.asInsert.SaveValueIN(&quot;INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, '&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', '&quot; &amp; Me.CHEQ_NO &amp; &quot;', '&quot; &amp; Me.CHEQ_TYPE &amp; &quot;', '&quot; &amp; CDate(Me.CHEQ_DATE).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', &quot; &amp; Me.BANK_AMT &amp; &quot;,1,'&quot; &amp; Me.BANK_ACC &amp; &quot;',&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,10,&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;,&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.DESCRIPTION &amp; &quot;')&quot;)
                            End If


                        ElseIf Me.BOTH_PAY = True Then
                            If Me.asSELECT.pFlg2 = True Then
                                Me.asUpdate.UpdateValueIN(&quot;UPDATE CLIENT_RECEIPT SET nCLIENT_ID=&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, dDATE='&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', nCASH_AMOUNT=&quot; &amp; Me.CASH_AMT &amp; &quot;, sCHEQUE_NO='&quot; &amp; Me.CHEQ_NO &amp; &quot;',sCHEQUE_TYPE='&quot; &amp; Me.CHEQ_TYPE &amp; &quot;', dCHEQUE_DATE='&quot; &amp; CDate(Me.CHEQ_DATE).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', nCHEQUE_AMOUNT=&quot; &amp; Me.BANK_AMT &amp; &quot;, sACCOUNT_CODE='&quot; &amp; Me.BANK_ACC &amp; &quot;', nLOGIN_ID=10, nBUSINESS_CODE=&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;, nEMP_CODE=&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;, sDESCRIPTON='&quot; &amp; Me.DESCRIPTION &amp; &quot;' WHERE nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;)

                            Else
                                Me.asInsert.SaveValueIN(&quot;INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO,sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, '&quot; &amp; CDate(Me.TxtDate.Text).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', &quot; &amp; Me.CASH_AMT &amp; &quot;, '&quot; &amp; Me.CHEQ_NO &amp; &quot;', '&quot; &amp; Me.CHEQ_TYPE &amp; &quot;', '&quot; &amp; CDate(Me.CHEQ_DATE).ToString(&quot;MM-dd-yyyy&quot;) &amp; &quot;', &quot; &amp; Me.BANK_AMT &amp; &quot;,1,'&quot; &amp; Me.BANK_ACC &amp; &quot;',&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,10,&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;,&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.DESCRIPTION &amp; &quot;')&quot;)

                            End If

                        ElseIf Not (Val(Me.CASH_AMT) &#43; Val(Me.BANK_AMT)) = 0 Then
                            If Me.asSELECT.pFlg2 = True Then
                                Me.asUpdate.UpdateValueIN(&quot;UPDATE CLIENT_RECEIPT SET nCLIENT_ID=&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, dDATE='&quot; &amp; Me.Rec_Date &amp; &quot;', nCASH_AMOUNT=&quot; &amp; Me.CASH_AMT &amp; &quot;, sCHEQUE_NO='&quot; &amp; Me.CHEQ_NO &amp; &quot;',sCHEQUE_TYPE='&quot; &amp; Me.CHEQ_TYPE &amp; &quot;', dCHEQUE_DATE='&quot; &amp; Me.CHEQ_DATE &amp; &quot;', nCHEQUE_AMOUNT=&quot; &amp; Me.BANK_AMT &amp; &quot;, sACCOUNT_CODE='&quot; &amp; Me.BANK_ACC &amp; &quot;', nLOGIN_ID=10, nBUSINESS_CODE=&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;, nEMP_CODE=&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;, sDESCRIPTON='&quot; &amp; Me.DESCRIPTION &amp; &quot;' WHERE nSINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;)

                            Else
                                Me.asInsert.SaveValueIN(&quot;INSERT INTO CLIENT_RECEIPT(nCLIENT_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, nCHEQUE_STATUS, sACCOUNT_CODE, nSINV_NO, nLOGIN_ID, nBUSINESS_CODE, nEMP_CODE, sDESCRIPTON) VALUES(&quot; &amp; Val(Me.CmbClient.SelectedItem.Col3) &amp; &quot;, '&quot; &amp; Me.Rec_Date &amp; &quot;', &quot; &amp; Me.CASH_AMT &amp; &quot;, '&quot; &amp; Me.CHEQ_NO &amp; &quot;', '&quot; &amp; Me.CHEQ_TYPE &amp; &quot;', '&quot; &amp; Me.CHEQ_DATE &amp; &quot;', &quot; &amp; Me.BANK_AMT &amp; &quot;,1,'&quot; &amp; Me.BANK_ACC &amp; &quot;',&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;,10,&quot; &amp; Val(Me.CmbGroup.SelectedItem.Col3) &amp; &quot;,&quot; &amp; Val(Me.CmbD_Man.SelectedItem.Col3) &amp; &quot;,'&quot; &amp; Me.DESCRIPTION &amp; &quot;')&quot;)

                            End If

                        Else
                            MsgBox(&quot;Credit Sale Invoice Saved!&quot;, MsgBoxStyle.Information, &quot;(NS) - Credit Invoice!&quot;)

                        End If

                        Me.BttnPrev.Enabled = True
                        Me.BttnPrint.Enabled = True
                        Me.BttnSearch_Inv.Enabled = True
                        Me.BttnReceipt.Enabled = False
                        Me.BttnNew.Text = &quot;&amp;New&quot;
                        Me.BttnNew.Enabled = True
                        Me.BttnAdd.Enabled = True
                        Me.BttnSave.Text = &quot;&amp;Save&quot;
                        Me.BttnSave.Enabled = False
                        Me.BttnClose.Enabled = True
                        Me.Disable_All()

                    End If

                ElseIf Me.asSELECT.pFlg1 = False Then
                    MsgBox(&quot;This Invoice # &quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot; is not Exist.&quot;, MsgBoxStyle.Exclamation, &quot;(NS) - Not Exist!&quot;)

                End If
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        If MsgBox(&quot;Are you sure to Close?&quot;, MsgBoxStyle.Question &#43; vbYesNo, &quot;(NS) - Close?&quot;) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

#End Region

#Region &quot;Search Button Control&quot;
    Private Sub BttnSearch_Inv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch_Inv.Click
        On Error GoTo Fix
        frmSEARCH_S_INV.TxtClient.Text = Nothing
        frmSEARCH_S_INV.TxtInvoice.Text = Nothing
        frmSEARCH_S_INV.TxtDateFrom.Text = Nothing
        frmSEARCH_S_INV.TxtDateTo.Text = Nothing
        frmSEARCH_S_INV.FrmStr = &quot;SALE&quot;

        frmSEARCH_S_INV.ShowDialog(Me)
Fix:
    End Sub
#End Region

#Region &quot;Print Button Control&quot;
    Private Sub BttnPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrev.Click
        Dim Rpt As New rptSALES_INVOICE_WS
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = &quot;{V_SALE_MASTER.SINV_NO}=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;
            Frm.Text = &quot;Sale Invoice&quot;
            Frm.MdiParent = Me.ParentForm
            Frm.Show()
            Frm.Activate()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub BttnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrint.Click
        Dim Rpt As New rptSALES_INVOICE_WS
        Dim Frm As New frmRPT
        Try
            Frm.CRV.ReportSource = Rpt
            Frm.CRV.SelectionFormula = &quot;{V_SALE_MASTER.SINV_NO}=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;
            Frm.CRV.PrintReport()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
#End Region

#Region &quot;Sub and Functions&quot;
    Private Sub FillComboBox_Client()
        Dim Str1 As String = &quot;SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CONVERT(NUMERIC(18,2), CREDIT_LIM) AS CREDIT_LIM, GST_NO, CONVERT(NUMERIC(18,2), OPEN_BAL) AS OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO WHERE STATUS='1' ORDER BY SHOP_NAME&quot;
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daCLIENT_INFO = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsCLIENT_INFO1.Clear()
        Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO1.V_CLIENT_INFO)

        Dim dtLoading As New DataTable(&quot;V_CLIENT_INFO&quot;)

        dtLoading.Columns.Add(&quot;ID&quot;, System.Type.GetType(&quot;System.String&quot;))
        dtLoading.Columns.Add(&quot;NAME&quot;, System.Type.GetType(&quot;System.String&quot;))
        dtLoading.Columns.Add(&quot;SHOP_NAME&quot;, System.Type.GetType(&quot;System.String&quot;))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsCLIENT_INFO1.V_CLIENT_INFO.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr(&quot;ID&quot;) = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(0).ToString
            dr(&quot;NAME&quot;) = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(1).ToString
            dr(&quot;SHOP_NAME&quot;) = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(2).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbClient.SelectedIndex = -1
        Me.CmbClient.Items.Clear()
        Me.CmbClient.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbClient.SourceDataString = New String(2) {&quot;SHOP_NAME&quot;, &quot;NAME&quot;, &quot;ID&quot;}
        Me.CmbClient.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_Group()
        Dim Str1 As String = &quot;SELECT nID, sGROUP_NAME, sGROUP_DEALER, sSTATUS sBUSINESS_NAME FROM V_BUSINESS_GROUP WHERE sSTATUS='1'&quot;
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_BUSINESS_GROUP = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_BUSINESS_GROUP1.Clear()
        Me.daLUP_BUSINESS_GROUP.Fill(Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP)

        Dim dtLoading As New DataTable(&quot;V_BUSINESS_GROUP&quot;)

        dtLoading.Columns.Add(&quot;nID&quot;, System.Type.GetType(&quot;System.String&quot;))
        dtLoading.Columns.Add(&quot;sGROUP_NAME&quot;, System.Type.GetType(&quot;System.String&quot;))
        dtLoading.Columns.Add(&quot;sGROUP_DEALER&quot;, System.Type.GetType(&quot;System.String&quot;))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr(&quot;nID&quot;) = Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(0).ToString
            dr(&quot;sGROUP_NAME&quot;) = Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(1).ToString
            dr(&quot;sGROUP_DEALER&quot;) = Me.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(2).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbGroup.SelectedIndex = -1
        Me.CmbGroup.Items.Clear()
        Me.CmbGroup.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbGroup.SourceDataString = New String(2) {&quot;sGROUP_NAME&quot;, &quot;sGROUP_DEALER&quot;, &quot;nID&quot;}
        Me.CmbGroup.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_Employee()
        Dim Str1 As String = &quot;SELECT CODE, NAME, FATHER_NAME, NIC, HOME_PH, CELL, PRE_ADD, PER_ADD, DESIGNATION, APP_DATE, PAY, STATUS, LEAVE_DATE, EMAIL_ADD, BANK_ACC, BANK_ADD FROM V_EMPLOYEE_INFO WHERE STATUS='1'&quot;
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_EMPLOYEE = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_EMPLOYEE1.Clear()
        Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO)

        Dim dtLoading As New DataTable(&quot;V_EMPLOYEE_INFO&quot;)

        dtLoading.Columns.Add(&quot;CODE&quot;, System.Type.GetType(&quot;System.String&quot;))
        dtLoading.Columns.Add(&quot;NAME&quot;, System.Type.GetType(&quot;System.String&quot;))
        dtLoading.Columns.Add(&quot;DESIGNATION&quot;, System.Type.GetType(&quot;System.String&quot;))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr(&quot;CODE&quot;) = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(0).ToString
            dr(&quot;NAME&quot;) = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(1).ToString
            dr(&quot;DESIGNATION&quot;) = Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(8).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbS_Man.SelectedIndex = -1
        Me.CmbS_Man.Items.Clear()
        Me.CmbS_Man.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbS_Man.SourceDataString = New String(2) {&quot;NAME&quot;, &quot;DESIGNATION&quot;, &quot;CODE&quot;}
        Me.CmbS_Man.SourceDataTable = dtLoading

        Me.CmbD_Man.SelectedIndex = -1
        Me.CmbD_Man.Items.Clear()
        Me.CmbD_Man.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbD_Man.SourceDataString = New String(2) {&quot;NAME&quot;, &quot;DESIGNATION&quot;, &quot;CODE&quot;}
        Me.CmbD_Man.SourceDataTable = dtLoading
    End Sub

    Private Sub Fill_Master_Date()
        Dim Str2 As String = &quot;SELECT SINV_NO, SHOP_NAME, CASH_CLIENT, CASH_MEMO, LPINV_NO, S_DATE, DISP_DATE, VEHICLE, CONVERT(NUMERIC(18,2), FREIGHT) AS FREIGHT, CONVERT(NUMERIC(18,2), UNLOADING) AS UNLOADING, TR_NO, TR_QTY, CONVERT(NUMERIC(18,2), TOT_BILL) AS TOT_BILL, CONVERT(NUMERIC(18,2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18,2), DISC_OTHER) AS DISC_OTHER, OTHER_DESC, CONVERT(NUMERIC(18,2), NET_TOTAL) AS NET_TOTAL, EMP_NAME, GROUP_NAME, REMARKS, D_MAN FROM V_SALE_MASTER WHERE SINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;
        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)

        Me.daV_SALE_MASTER = New SDS.SqlDataAdapter(SqlCmd2)

        Me.DsV_SALE_MASTER1.Clear()
        Me.daV_SALE_MASTER.Fill(Me.DsV_SALE_MASTER1.V_SALE_MASTER)

    End Sub
    Private Sub Fill_Detail_Data()
        Dim Str3 As String = &quot;SELECT ID, SINV_NO, ITEM_CODE, ITEM_NAME, BATCH_NO, CONVERT(NUMERIC(18,2), UNIT_COST) AS UNIT_COST , CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE , CONVERT(NUMERIC(18,2), DISC_RS) AS DISC_RS, DISC_PER, PPP, QTY_PKS, QTY_PCS, QTY_BONUS, QTY_TOT_PCS, CONVERT(NUMERIC(18,2), TOTAL_VALUE) AS TOTAL_VALUE, SCM_ITEM_CODE, SCM_ITEM, SCM_QTY, SALE_TAX FROM V_SALE_DETAIL WHERE SINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;
        Dim SqlCmd3 As New SDS.SqlCommand(Str3, Me.SqlConnection1)
        Me.daV_SALE_DETAIL = New SDS.SqlDataAdapter(SqlCmd3)

        Me.DsV_SALE_DETAIL1.Clear()
        Me.daV_SALE_DETAIL.Fill(Me.DsV_SALE_DETAIL1.V_SALE_DETAIL)

        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Count - 1
            Me.DataGridView1.Rows.Add()
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColCode&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(2).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColBatch&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(4).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColName&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(3).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColCost&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(5).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColRate&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(6).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColPack&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(10).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColPiece&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(11).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColBonus&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(12).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColPercentage&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(8).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColDisc_Rs&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(7).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColSaleTax&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(18).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColTotal&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(14).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColSR&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(0).ToString
            Me.DataGridView1.Rows(Cnt).Cells(&quot;ColPPP&quot;).Value = Me.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(9).ToString

        Next
    End Sub
    Private Sub Fill_Receipt_Data()
        Try
            Dim Str1 As String = &quot;SELECT ID, CLIENT_ID, SHOP_NAME, R_DATE, CONVERT(NUMERIC(18,2), CASH_AMT) AS CASH_AMT, CHEQ_NO, CHEQ_TYPE, CHEQ_DATE, CONVERT(NUMERIC(18,2), BANK_AMT) AS BANK_AMT, SINV_NO, CHEQ_STATUS, STATUS_DATE, STATUS_DESC, BANK_ACC, BANK_NAME, EMP_NAME, USER_NAME, GROUP_NAME, DESCRIPTION, CONVERT(NUMERIC(18,2), TOT_RECEIPT) AS TOT_RECEIPT FROM V_CLIENT_RECEIPT WHERE SINV_NO=&quot; &amp; Val(Me.TxtInvoice.Text) &amp; &quot;&quot;
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_CLIENT_RECEIPT = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_CLIENT_RECEIPT1.Clear()
            Me.daV_CLIENT_RECEIPT.Fill(Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT)

            'FILL VALUE IN PAYMENT's VARIABLES
            If Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Count &gt; 0 Then
                SINV_NO = Val(Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(9).ToString)
                Rec_Date = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(3).ToString
                CASH_AMT = Val(Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(4).ToString)
                CHEQ_NO = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(5).ToString
                CHEQ_TYPE = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(6).ToString
                CHEQ_DATE = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(7).ToString

                BANK_AMT = Val(Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(8).ToString)
                BANK_ACC = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(13).ToString
                DESCRIPTION = Me.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(0).Item(17).ToString

                If Me.CASH_AMT &gt; 0 And Me.BANK_AMT &lt;= 0 Then
                    Me.CASH_PAY = True
                    Me.BANK_PAY = False
                    Me.BOTH_PAY = False

                ElseIf Me.CASH_AMT &lt;= 0 And Me.BANK_AMT &gt; 0 Then
                    Me.BANK_PAY = True
                    Me.CASH_PAY = False
                    Me.BOTH_PAY = False

                ElseIf Me.CASH_AMT &gt; 0 And Me.BANK_AMT &gt; 0 Then
                    Me.BOTH_PAY = True
                    Me.BANK_PAY = False
                    Me.CASH_PAY = False

                End If

                If CHEQ_DATE.Length &gt; 0 Then
                    Me.CHEQ_DATE = CDate(Me.CHEQ_DATE).ToString(&quot;dd-MMM-yyyy&quot;)
                End If

            Else
                Rec_Date = Nothing
                CASH_AMT = 0
                CHEQ_NO = Nothing
                CHEQ_TYPE = Nothing
                CHEQ_DATE = Nothing
                BANK_AMT = 0
                BANK_ACC = Nothing
                SINV_NO = 0
                DESCRIPTION = Nothing

                Me.BOTH_PAY = False
                Me.BANK_PAY = False
                Me.CASH_PAY = False
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Null_Focus()
        If Me.TxtInvoice.Text = Nothing Then
            Me.TxtInvoice.Focus()

        ElseIf Me.TxtDate.Text = Nothing Then
            Me.TxtDate.Focus()

        ElseIf Me.TxtDispDate.Text = Nothing Then
            Me.TxtDispDate.Focus()

        ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
            Me.CmbGroup.Focus()

        ElseIf Me.CmbClient.SelectedIndex = -1 Or Me.CmbClient.Text = Nothing Then
            Me.CmbClient.Focus()

        ElseIf Me.CmbS_Man.SelectedIndex = -1 Or Me.CmbS_Man.Text = Nothing Then
            Me.CmbS_Man.Focus()

        ElseIf Me.CmbD_Man.SelectedIndex = -1 Or Me.CmbD_Man.Text = Nothing Then
            Me.CmbD_Man.Focus()


        End If
    End Sub

    Public Sub Disable_All()
        Dim Ctrl As Control
        For Each Ctrl In Me.Controls
            If Not Ctrl.Name = &quot;GroupBox1&quot; And Not Ctrl.Name = &quot;Label3&quot; Then
                Ctrl.Enabled = False
            End If
        Next
    End Sub
    Public Sub Enable_All()
        Dim Ctrl As Control
        For Each Ctrl In Me.Controls
            If Not Ctrl.Name = &quot;GroupBox1&quot; And Not Ctrl.Name = &quot;Label3&quot; Then
                Ctrl.Enabled = True
            End If
        Next
    End Sub

    Public Sub Clear_All()
        Me.TxtInvoice.Text = Nothing
        Me.CmbGroup.SelectedIndex = -1
        Me.CmbClient.SelectedIndex = -1
        Me.TxtVehicle.Text = Nothing
        Me.CmbS_Man.SelectedIndex = -1
        Me.CmbD_Man.SelectedIndex = -1
        Me.TxtCashClient.Text = Nothing
        Me.TxtReceivables.Text = 0
        Me.TxtStandyBy.Text = 0
        Me.TxtNET_Receivable.Text = 0

        Me.LblB_Pcs.Text = 0
        Me.LblPPP.Text = 0
        Me.LblRatePcs.Text = 0
        Me.LblRate.Text = 0
        Me.LblRetail.Text = 0
        Me.LblStock.Text = 0

        Me.TxtTotalItems.Text = 0
        Me.TxtTRno.Text = Nothing
        Me.TxtTRqty.Text = Nothing

        Me.TxtFreight.Text = &quot;0.00&quot;
        Me.TxtUnloading.Text = &quot;0.00&quot;

        Me.TxtTotal.Text = &quot;0.00&quot;
        Me.TxtDiscount.Text = &quot;0.00&quot;
        Me.TxtDiscPercent.Text = &quot;0.00&quot;
        'Me.TxtReceipt.Text = &quot;0.00&quot;

        Me.TxtRemarks.Text = Nothing

        Me.TxtCashMemo.Focus()

        'Me.CASH_AMT = 0.0
        'Me.BANK_AMT = 0.0

        'Me.BANK_ACC = Nothing
        'Me.CHEQ_NO = Nothing
        'Me.CHEQ_DATE = Nothing
        'Me.CHEQ_TYPE = Nothing

        Me.Default_Setting()

        On Error GoTo Fix
        Me.DataGridView1.Rows.Clear()
Fix:
    End Sub

    Private Sub Default_Setting()
        On Error GoTo Fix
        Dim StrCMB As String

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item(&quot;GROUP&quot;).ToString
        Me.CmbGroup.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item(&quot;S_MAN&quot;).ToString
        Me.CmbS_Man.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbS_Man.SelectedIndex = Me.CmbS_Man.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item(&quot;D_MAN&quot;).ToString
        Me.CmbD_Man.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbD_Man.SelectedIndex = Me.CmbD_Man.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item(&quot;CLIENT&quot;).ToString
        Me.CmbClient.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbClient.SelectedIndex = Me.CmbClient.FindString(StrCMB)
        End If
Fix:
    End Sub
#End Region

==================================


Imports SDS = System.Data.SqlClient
Public Class AssConn


    Public Conn, Conn2, Conn3 As New SDS.SqlConnection
    Public Cmd, Cmd2, Cmd3 As New SDS.SqlCommand

   Sub New()
             Conn.ConnectionString = &quot;Data Source=server;Initial Catalog=Neuro_BS;User ID=sa&quot;
        Conn2.ConnectionString = &quot;workstation id=SERVER;packet size=32767;integrated security=SSPI;data source=SERVER;persist security info=False;Connect Timeout=30;initial catalog=Neuro_BS&quot;
        Conn3.ConnectionString = &quot;Data Source=server;Initial Catalog=Neuro_BS;Integrated Security=True&quot;
      
    End Sub

End Class
</pre>
<div class="preview">
<pre class="vb"><span class="visualBasic__keyword">Imports</span>&nbsp;SDS&nbsp;=&nbsp;System.Data.SqlClient&nbsp;
<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Class</span>&nbsp;frmSALE&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Inherits</span>&nbsp;System.Windows.Forms.Form<span class="visualBasic__preproc">&nbsp;
&nbsp;
&nbsp;
#Region&nbsp;&quot;VARIABLES</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;asConn&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;AssConn&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;asInsert&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;AssInsert&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;asUpdate&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;AssUpdate&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;asDelete&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;AssDelete&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;asSELECT&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;AssSelect&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;asTXT&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;AssTextBox&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;asNum&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;AssNumPress&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;asMAX&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;AssMaxNo&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Rd&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Data.SqlClient.SqlDataReader&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;Search_Inv&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;PAYMENTS&nbsp;VARIABLES</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;CASH_AMT,&nbsp;BANK_AMT,&nbsp;SINV_NO&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;CASH_PAY&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>,&nbsp;BANK_PAY&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>,&nbsp;BOTH_PAY&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;CHEQ_NO,&nbsp;CHEQ_DATE,&nbsp;DESCRIPTION,&nbsp;CHEQ_TYPE,&nbsp;BANK_ACC,&nbsp;Rec_Date&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;P_Inv&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;FORM&nbsp;CONTROL</span>&quot;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;frmSALE_FormClosing(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.FormClosingEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;<span class="visualBasic__keyword">Me</span>.FormClosing&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;Update&quot;</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Can't&nbsp;close&nbsp;without&nbsp;Updating&nbsp;OR&nbsp;Cancelling&nbsp;Invoice&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Closing&nbsp;Error!&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.Cancel&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;Save&quot;</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Can't&nbsp;close&nbsp;without&nbsp;Saving&nbsp;OR&nbsp;Cancelling&nbsp;Invoice&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Closing&nbsp;Error!&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.Cancel&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;frmSALE_Load(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;<span class="visualBasic__keyword">MyBase</span>.Load&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1.ConnectionString&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.asConn.Conn.ConnectionString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.FillComboBox_Client()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.FillComboBox_Employee()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.FillComboBox_Group()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;StrSql&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;nID&nbsp;AS&nbsp;ID,&nbsp;sBUSINESS_GP&nbsp;AS&nbsp;[GROUP],&nbsp;sBANK_ACC&nbsp;AS&nbsp;BANK_ACC,&nbsp;sS_MAN&nbsp;AS&nbsp;S_MAN,&nbsp;sP_MAN&nbsp;AS&nbsp;P_MAN,&nbsp;sD_MAN&nbsp;AS&nbsp;D_MAN,&nbsp;sR_MAN&nbsp;AS&nbsp;R_MAN,&nbsp;sCLIENT&nbsp;AS&nbsp;CLIENT,&nbsp;sCLIENT_TYPE&nbsp;AS&nbsp;CLIENT_TYPE,&nbsp;sCLIENT_CAT&nbsp;AS&nbsp;CLIENT_CAT,&nbsp;sCLIENT_GD&nbsp;AS&nbsp;CLIENT_GD,&nbsp;sZONE&nbsp;AS&nbsp;ZONE,&nbsp;sROUTE&nbsp;AS&nbsp;ROUTE,&nbsp;sAREA&nbsp;AS&nbsp;AREA,&nbsp;sEXP_SUB_HEAD&nbsp;AS&nbsp;EXP_SUB_HEAD,&nbsp;sPRINTER&nbsp;AS&nbsp;PRINTER,&nbsp;sREPORT_TITLE&nbsp;AS&nbsp;RPT_TITLE,&nbsp;sREPORT_WARRANTY&nbsp;AS&nbsp;RPT_WARRANTY&nbsp;FROM&nbsp;NS_DEFAULT&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;CmdSql&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(StrSql,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daNS_DEFAULT&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(CmdSql)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daNS_DEFAULT.Fill(<span class="visualBasic__keyword">Me</span>.DsNS_DEFAULT1.NS_DEFAULT)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Default_Setting()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Disable_All()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrev.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrint.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Date</span>.Now.ToString(<span class="visualBasic__string">&quot;dd-MMM-yyyy&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDispDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Date</span>.Now.ToString(<span class="visualBasic__string">&quot;dd-MMM-yyyy&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.BttnNew_Click(sender,&nbsp;e)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;frmSALE_KeyPress(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.KeyPressEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;<span class="visualBasic__keyword">MyBase</span>.KeyPress&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asNum.EnterTab(e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;TextBox&nbsp;Control</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Got&nbsp;and&nbsp;LostFocus</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Txt_GotFocus(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtInvoice.GotFocus,&nbsp;TxtCashClient.GotFocus,&nbsp;TxtDate.GotFocus,&nbsp;TxtDescription.GotFocus,&nbsp;TxtDiscount.GotFocus,&nbsp;TxtDiscPercent.GotFocus,&nbsp;TxtDispDate.GotFocus,&nbsp;TxtFreight.GotFocus,&nbsp;TxtNetTotal.GotFocus,&nbsp;TxtOtherDisc.GotFocus,&nbsp;TxtReceipt.GotFocus,&nbsp;TxtTotal.GotFocus,&nbsp;TxtTotalItems.GotFocus,&nbsp;TxtTRno.GotFocus,&nbsp;TxtTRqty.GotFocus,&nbsp;TxtUnloading.GotFocus,&nbsp;TxtVehicle.GotFocus,&nbsp;TxtRemarks.GotFocus,&nbsp;TxtCashMemo.GotFocus,&nbsp;TxtReceivables.GotFocus,&nbsp;TxtClientBal.GotFocus,&nbsp;TxtStandyBy.GotFocus,&nbsp;TxtNET_Receivable.GotFocus,&nbsp;TxtInvBalance.GotFocus&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">CType</span>(sender,&nbsp;TextBox).BackColor&nbsp;=&nbsp;Color.LightSteelBlue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">CType</span>(sender,&nbsp;TextBox).SelectAll()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Ctrl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Control&nbsp;=&nbsp;sender&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;Ctrl.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__string">&quot;TxtDescription&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;sender.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Other's&nbsp;Description&nbsp;Here!&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sender.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Txt_LostFocus(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtInvoice.LostFocus,&nbsp;TxtCashClient.LostFocus,&nbsp;TxtDate.LostFocus,&nbsp;TxtDescription.LostFocus,&nbsp;TxtDiscount.LostFocus,&nbsp;TxtDiscPercent.LostFocus,&nbsp;TxtDispDate.LostFocus,&nbsp;TxtFreight.LostFocus,&nbsp;TxtInvBalance.LostFocus,&nbsp;TxtNetTotal.LostFocus,&nbsp;TxtOtherDisc.LostFocus,&nbsp;TxtReceivables.LostFocus,&nbsp;TxtReceipt.LostFocus,&nbsp;TxtClientBal.LostFocus,&nbsp;TxtTotal.LostFocus,&nbsp;TxtTotalItems.LostFocus,&nbsp;TxtTRno.LostFocus,&nbsp;TxtTRqty.LostFocus,&nbsp;TxtUnloading.LostFocus,&nbsp;TxtVehicle.LostFocus,&nbsp;TxtRemarks.LostFocus,&nbsp;TxtStandyBy.LostFocus,&nbsp;TxtNET_Receivable.LostFocus,&nbsp;TxtCashMemo.LostFocus&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">CType</span>(sender,&nbsp;TextBox).BackColor&nbsp;=&nbsp;Color.White&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Ctrl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Control&nbsp;=&nbsp;sender&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;Ctrl.Name&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__string">&quot;TxtDate&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;sender.TextLength&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sender.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">CDate</span>(sender.text).ToString(<span class="visualBasic__string">&quot;dd-MMM-yyyy&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__string">&quot;TxtDispDate&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;sender.TextLength&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sender.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">CDate</span>(sender.text).ToString(<span class="visualBasic__string">&quot;dd-MMM-yyyy&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sender.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sender.Focus()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Leave</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;TxtTRqty_Leave(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtTRqty.Leave&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTRqty.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTRqty.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'KeyPress&nbsp;Numeric</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Txt_Num_KeyPress(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.KeyPressEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtTotalItems.KeyPress&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asNum.NumPress(<span class="visualBasic__keyword">True</span>,&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'KeyPress&nbsp;Numeric&nbsp;With&nbsp;DOT</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Txt_KeyPress(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.KeyPressEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtDiscount.KeyPress,&nbsp;TxtFreight.KeyPress,&nbsp;TxtInvBalance.KeyPress,&nbsp;TxtNetTotal.KeyPress,&nbsp;TxtOtherDisc.KeyPress,&nbsp;TxtReceivables.KeyPress,&nbsp;TxtReceipt.KeyPress,&nbsp;TxtClientBal.KeyPress,&nbsp;TxtTotal.KeyPress,&nbsp;TxtUnloading.KeyPress,&nbsp;TxtDiscPercent.KeyPress,&nbsp;TxtStandyBy.KeyPress,&nbsp;TxtNET_Receivable.KeyPress&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asNum.NumPressDot(e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'NET&nbsp;TOTAL&nbsp;CALCULATION</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;TxtUnloading_TextChanged(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtUnloading.TextChanged,&nbsp;TxtFreight.TextChanged,&nbsp;TxtOtherDisc.TextChanged,&nbsp;TxtDiscPercent.TextChanged,&nbsp;TxtDiscount.TextChanged,&nbsp;TxtTotal.TextChanged&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.Search_Inv&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;Fix&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Freight,&nbsp;Unloading,&nbsp;Total,&nbsp;Disc_RS,&nbsp;Disc_Age,&nbsp;Disc_Other&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Freight&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtFreight.Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Unloading&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtUnloading.Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Total&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTotal.Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Disc_RS&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtDiscount.Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Disc_Age&nbsp;=&nbsp;(Total&nbsp;*&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtDiscPercent.Text))&nbsp;/&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Disc_Other&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtOtherDisc.Text)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtNetTotal.Text&nbsp;=&nbsp;(Total&nbsp;&#43;&nbsp;Freight&nbsp;&#43;&nbsp;Unloading)&nbsp;-&nbsp;(Disc_RS&nbsp;&#43;&nbsp;Disc_Age&nbsp;&#43;&nbsp;Disc_Other)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtNetTotal.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Decimal</span>.Round(<span class="visualBasic__keyword">CDec</span>(<span class="visualBasic__keyword">Me</span>.TxtNetTotal.Text),&nbsp;<span class="visualBasic__number">2</span>)&nbsp;
Fix:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Net&nbsp;Receivable&nbsp;Calculation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;TxtReceivables_TextChanged(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtStandyBy.TextChanged&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtReceivables.Text&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtNET_Receivable.Text)&nbsp;&#43;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtStandyBy.Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Invoice&nbsp;&amp;&nbsp;Supplier&nbsp;Balance&nbsp;Calculation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;TxtReceipt_TextChanged(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtReceipt.TextChanged,&nbsp;TxtNetTotal.TextChanged,&nbsp;TxtNET_Receivable.TextChanged&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtReceivables.Text&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtNET_Receivable.Text)&nbsp;&#43;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtStandyBy.Text)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;Fix&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtReceipt.TextLength&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtReceipt.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Decimal</span>.Round(<span class="visualBasic__keyword">CDec</span>(<span class="visualBasic__keyword">Me</span>.TxtReceipt.Text),&nbsp;<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvBalance.Text&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtNetTotal.Text)&nbsp;-&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtReceipt.Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtClientBal.Text&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtNET_Receivable.Text)&nbsp;&#43;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvBalance.Text)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvBalance.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Decimal</span>.Round(<span class="visualBasic__keyword">CDec</span>(<span class="visualBasic__keyword">Me</span>.TxtInvBalance.Text),&nbsp;<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtClientBal.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Decimal</span>.Round(<span class="visualBasic__keyword">CDec</span>(<span class="visualBasic__keyword">Me</span>.TxtClientBal.Text),&nbsp;<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtNetTotal.Text)&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnReceipt.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnReceipt.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
Fix:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Fill&nbsp;data&nbsp;for&nbsp;Modification</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;TxtInvoice_TextChanged(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;TxtInvoice.TextChanged&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.Search_Inv&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'FILL&nbsp;MASTER&nbsp;RECORD</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Fill_Master_Date()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'FILL&nbsp;DETAIL&nbsp;RECORD</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Fill_Detail_Data()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Search_Inv&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'FILL&nbsp;TOTAL&nbsp;PAYMENT</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Fill_Receipt_Data()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;StrClient&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.Text&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.FindString(StrClient)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'MsgBox(Me.TxtInvoice.Text)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;ComboBox&nbsp;Controls</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;CmbClient_SelectedIndexChanged(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;CmbClient.SelectedIndexChanged,&nbsp;CmbGroup.SelectedIndexChanged&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;CLIENT_ID,&nbsp;CONVERT(NUMERIC(18,2),CLIENT_BAL)&nbsp;AS&nbsp;CLIENT_BAL,&nbsp;GROUP_ID&nbsp;FROM&nbsp;SV_CLIENT_BALANCE&nbsp;WHERE&nbsp;CLIENT_ID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;AND&nbsp;GROUP_ID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str1,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_CLIENT_BAL&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd1)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_BAL1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_CLIENT_BAL.Fill(<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_BAL1.SV_CLIENT_BALANCE)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;CLIENT_ID,&nbsp;CONVERT(NUMERIC(18,2),TOT_STANDBY_CHEQ)&nbsp;AS&nbsp;TOT_STANDBY_CHEQ,&nbsp;GROUP_ID&nbsp;FROM&nbsp;V_CLIENT_TOT_STANDBY_CHEQ&nbsp;WHERE&nbsp;CLIENT_ID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;AND&nbsp;GROUP_ID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str1,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_CLIENT_STANDBY_CHEQ&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd1)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_STANDBY_CHEQ1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_CLIENT_STANDBY_CHEQ.Fill(<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_STANDBY_CHEQ1.V_CLIENT_TOT_STANDBY_CHEQ)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Got&nbsp;and&nbsp;LostFocus</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Cmb_GotFocus(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;CmbD_Man.GotFocus,&nbsp;CmbGroup.GotFocus,&nbsp;CmbClient.GotFocus,&nbsp;CmbS_Man.GotFocus&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">CType</span>(sender,&nbsp;ComboBox).BackColor&nbsp;=&nbsp;Color.LightSteelBlue&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">CType</span>(sender,&nbsp;ComboBox).SelectAll()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Cmb_LostFocus(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;CmbD_Man.LostFocus,&nbsp;CmbGroup.LostFocus,&nbsp;CmbClient.LostFocus,&nbsp;CmbS_Man.LostFocus&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">CType</span>(sender,&nbsp;ComboBox).BackColor&nbsp;=&nbsp;Color.White&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;Select&nbsp;Item&nbsp;Controls</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;SelectItemToolStripMenuItem_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;SelectItemToolStripMenuItem.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;Fix&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSELECT_ITEM_BATCH.TxtCompany.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSELECT_ITEM_BATCH.TxtItem.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSELECT_ITEM_BATCH.FrmStr&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Sale&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSELECT_ITEM_BATCH.Row&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.CurrentRow.Index&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSELECT_ITEM_BATCH.ShowDialog(<span class="visualBasic__keyword">Me</span>)&nbsp;
Fix:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;DataGridView&nbsp;Control</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;ItemCode_Old&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DataGridView1_CellLeave(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.DataGridViewCellEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;DataGridView1.CellLeave&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ItemCode_Old&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DataGridView1_CellValueChanged(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.DataGridViewCellEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;DataGridView1.CellValueChanged&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.Search_Inv&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsSV_STOCK_BAL1.Clear()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblB_Pcs.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblPPP.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRatePcs.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRate.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRetail.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblStock.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'FILL&nbsp;TOP&nbsp;LABLES&nbsp;OF&nbsp;DATAGRID</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'WORKING&nbsp;ON&nbsp;BATCH&nbsp;WISE&nbsp;STOCK&nbsp;CALCULATION---CHECK&nbsp;BATCH&nbsp;EXIST&nbsp;OR&nbsp;NOT?</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SalePrice&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;<span class="visualBasic__number">0</span>,&nbsp;Bonus_Q&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;nCODE,&nbsp;sITEM_NAME,&nbsp;sNICK,&nbsp;nPPP,&nbsp;sPACK_DESC,&nbsp;sPIECE_DESC,&nbsp;UNIT_COST,&nbsp;UNIT_RATE,&nbsp;UNIT_RETAIL,&nbsp;nMIN_STOCK,&nbsp;nMAX_STOCK,&nbsp;nSALE_TAX,&nbsp;VENDOR,&nbsp;nBONUS_QTY,&nbsp;nBONUS_ON_PCS,&nbsp;sCLAIMABLE,&nbsp;sSTATUS,&nbsp;nOPEN_STOCK,&nbsp;OPEN_UNIT_VALUE&nbsp;FROM&nbsp;V_LUP_ITEM&nbsp;WHERE&nbsp;nCODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str1,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daLUP_ITEM&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd1)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daLUP_ITEM.Fill(<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Count&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColBatch&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColName&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCost&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColRate&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColBonus&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColPercentage&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColDisc_Rs&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColSaleTax&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColTotal&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColScmQty&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'SET&nbsp;FOCUS&nbsp;TO&nbsp;ColCode&nbsp;IS&nbsp;PENDING</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.SelectItemToolStripMenuItem_Click(sender,&nbsp;e)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblB_Pcs.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblPPP.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRatePcs.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRate.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRetail.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblStock.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Exit</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str10&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;nCODE&nbsp;AS&nbsp;CODE,&nbsp;STK_BAL&nbsp;FROM&nbsp;SV_STOCK_BAL&nbsp;WHERE&nbsp;nCODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd10&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str10,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection2)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daSV_STOCK_BAL&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd10)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsSV_STOCK_BAL1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daSV_STOCK_BAL.Fill(<span class="visualBasic__keyword">Me</span>.DsSV_STOCK_BAL1.SV_STOCK_BAL)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColName&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">1</span>).ToString()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">3</span>).ToString()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCost&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">6</span>).ToString()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColSaleTax&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">11</span>).ToString()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Itm_Code&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;Itm_Code&nbsp;=&nbsp;ItemCode_Old&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColRate&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">7</span>).ToString()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRatePcs.Text&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">7</span>).ToString())&nbsp;/&nbsp;Val(<span class="visualBasic__keyword">Me</span>.LblPPP.Text)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;Val(<span class="visualBasic__keyword">Me</span>.LblPPP.Text)&nbsp;&lt;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Please&nbsp;confirm&nbsp;'Item&nbsp;Code'&nbsp;or&nbsp;Item&nbsp;Detail&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Error!&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColRate&quot;</span>).Value&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">''Sale&nbsp;Price&nbsp;Notification/Alert</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;Me.DataGridView1.Item(&quot;ColRate&quot;,&nbsp;Me.DataGridView1.CurrentCell.RowIndex).Selected&nbsp;=&nbsp;True&nbsp;Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;If&nbsp;Not&nbsp;SalePrice&nbsp;=&nbsp;Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColRate&quot;).Value&nbsp;Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(&quot;Rate&nbsp;is&nbsp;not&nbsp;Equal&nbsp;to&nbsp;Actual&nbsp;Rate&quot;,&nbsp;MsgBoxStyle.Information,&nbsp;&quot;(NS)&nbsp;-&nbsp;Rate&quot;)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'ElseIf&nbsp;Me.DataGridView1.Item(&quot;ColBonus&quot;,&nbsp;Me.DataGridView1.CurrentCell.RowIndex).Selected&nbsp;=&nbsp;True&nbsp;Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;'Bonus&nbsp;Qty&nbsp;Notification/Alert</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;If&nbsp;Not&nbsp;Bonus_Q&nbsp;=&nbsp;Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColBonus&quot;).Value&nbsp;Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If&nbsp;MsgBox(&quot;Are&nbsp;you&nbsp;sure&nbsp;to&nbsp;Give&nbsp;him&nbsp;Bonus&nbsp;on&nbsp;this&nbsp;Item?&quot;,&nbsp;MsgBoxStyle.Question&nbsp;&#43;&nbsp;vbYesNo,&nbsp;&quot;(NS)&nbsp;-&nbsp;Rate&quot;)&nbsp;=&nbsp;MsgBoxResult.No&nbsp;Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Me.DataGridView1.Rows(e.RowIndex).Cells(&quot;ColBonus&quot;).Value&nbsp;=&nbsp;0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'End&nbsp;If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Rate,&nbsp;PPP,&nbsp;Pks,&nbsp;Pcs,&nbsp;Line_Tot,&nbsp;P_age,&nbsp;Disc_Rs,&nbsp;S_Tax&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rate&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColRate&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PPP&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.LblPPP.Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pks&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pcs&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;P_age&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColPercentage&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Disc_Rs&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColDisc_Rs&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;S_Tax&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColSaleTax&quot;</span>).Value)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Line_Tot&nbsp;=&nbsp;((Rate&nbsp;/&nbsp;PPP)&nbsp;*&nbsp;((Pks&nbsp;*&nbsp;PPP)&nbsp;&#43;&nbsp;Pcs))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;S_Tax&nbsp;=&nbsp;(Line_Tot&nbsp;*&nbsp;S_Tax)&nbsp;/&nbsp;<span class="visualBasic__number">100</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Line_Tot&nbsp;=&nbsp;(Line_Tot&nbsp;-&nbsp;(((Line_Tot&nbsp;*&nbsp;P_age)&nbsp;/&nbsp;<span class="visualBasic__number">100</span>)&nbsp;&#43;&nbsp;Disc_Rs)&nbsp;&#43;&nbsp;S_Tax)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Line_Tot&nbsp;=&nbsp;<span class="visualBasic__keyword">Decimal</span>.Round(<span class="visualBasic__keyword">CDec</span>(Line_Tot),&nbsp;<span class="visualBasic__number">2</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColTotal&quot;</span>).Value&nbsp;=&nbsp;Line_Tot&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTotal.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;0.00&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Count&nbsp;-&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTotal.Text&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTotal.Text)&nbsp;&#43;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColTotal&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTotalItems.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Count&nbsp;-&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SalePrice&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__string">&quot;UNIT_RATE&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bonus_Q&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DsLUP_ITEM1.V_LUP_ITEM.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__string">&quot;nBONUS_ON_PCS&quot;</span>))&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblB_Pcs.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblPPP.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRatePcs.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRate.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRetail.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblStock.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value)&nbsp;&#43;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value&nbsp;&gt;&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value&nbsp;&lt;=&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;PPP,&nbsp;PCS,&nbsp;PKS&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PPP&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PCS&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PKS&nbsp;=&nbsp;PCS&nbsp;/&nbsp;PPP&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value)&nbsp;&#43;&nbsp;PKS&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex&nbsp;-&nbsp;<span class="visualBasic__number">1</span>).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;=&nbsp;PCS&nbsp;<span class="visualBasic__keyword">Mod</span>&nbsp;PPP&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'MsgBox(ex.Message)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DataGridView1_RowEnter(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.DataGridViewCellEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;DataGridView1.RowEnter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ItemCode_Old&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.RowIndex).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1_CellValueChanged(sender,&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DataGridView1_RowsRemoved(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.DataGridViewRowsRemovedEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;DataGridView1.RowsRemoved&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTotal.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;0.00&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Count&nbsp;-&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTotal.Text&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTotal.Text)&nbsp;&#43;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColTotal&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DataGridView1_UserDeletingRow(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.DataGridViewRowCancelEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;DataGridView1.UserDeletingRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDispDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Please&nbsp;enter&nbsp;description&nbsp;OR&nbsp;select&nbsp;correct&nbsp;value!&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Entry&nbsp;Required!&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Null_Focus()&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;MsgBox(<span class="visualBasic__string">&quot;Are&nbsp;you&nbsp;sure&nbsp;to&nbsp;Delete&nbsp;Item?&quot;</span>,&nbsp;MsgBoxStyle.Critical&nbsp;&#43;&nbsp;vbYesNo,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Deleting&nbsp;Item?&quot;</span>)&nbsp;=&nbsp;MsgBoxResult.Yes&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'DELETE&nbsp;FROM&nbsp;SALE&nbsp;DETAIL</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asDelete.DeleteValue_NoErr(<span class="visualBasic__string">&quot;DELETE&nbsp;FROM&nbsp;SALE_DETAIL&nbsp;WHERE&nbsp;nID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(e.Row.Index).Cells(<span class="visualBasic__string">&quot;ColSR&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'UPDATE&nbsp;VALUE&nbsp;IN&nbsp;SALE&nbsp;MASTER</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asUpdate.UpdateValue_NoErr(<span class="visualBasic__string">&quot;UPDATE&nbsp;SALE_MASTER&nbsp;SET&nbsp;nCLIENT_ID='&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;sCASH_CLIENT='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtCashClient.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;sCASH_MEMO_NO='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtCashMemo.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nLPINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.LblLoadPass.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;dDATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;dDISP_DATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDispDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;sVEHICLE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtVehicle.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nFREIGHT=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtFreight.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nUNLOADING=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtUnloading.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sTR_NO='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTRno.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nTR_QTY=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTRqty.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nTOTAL_BILL=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTotal.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nDISCOUNT=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtDiscount.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nDISC_PERCENT=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtDiscPercent.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nOTHERS=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtOtherDisc.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sOTHER_DESC='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDescription.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nNET_TOTAL=&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtNetTotal.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nEMPLOYEE_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nLOGIN_ID=10,&nbsp;nBUSINESS_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sREMARKS='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtRemarks.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;'&nbsp;WHERE&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnAdd.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.Cancel&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;DataGridView1_KeyPress(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.Windows.Forms.KeyPressEventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;DataGridView1.KeyPress&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;Asc(e.KeyChar)&nbsp;=&nbsp;Keys.Escape&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Ca&amp;ncel&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew_Click(sender,&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnClose_Click(sender,&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;Button&nbsp;Control</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;BttnAdd_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;BttnAdd.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;BttnNew_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;BttnNew.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;New&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Enable_All()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtReceipt.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;0.00&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rec_Date&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CASH_AMT&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_NO&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_TYPE&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_DATE&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BANK_AMT&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BANK_ACC&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SINV_NO&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DESCRIPTION&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BOTH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSearch_Inv.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrev.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrint.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnReceipt.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;Save&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnClose.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CancelButton&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Clear_All()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Ca&amp;ncel&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.asMAX.LoadValue(Rd,&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;MAX(nSINV_NO)&nbsp;FROM&nbsp;SALE_MASTER&quot;</span>)&nbsp;&#43;&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Ca&amp;ncel&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;MsgBox(<span class="visualBasic__string">&quot;Are&nbsp;you&nbsp;sure&nbsp;to&nbsp;Cancel&nbsp;this&nbsp;Invoice?&quot;</span>,&nbsp;MsgBoxStyle.Critical&nbsp;&#43;&nbsp;vbYesNo,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Cancel&nbsp;Invoice?&quot;</span>)&nbsp;=&nbsp;MsgBoxResult.Yes&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Disable_All()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrev.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrint.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnClose.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSearch_Inv.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;Save&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CancelButton&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.BttnClose&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Search_Inv&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Clear_All()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;New&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;BttnReceipt_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;BttnReceipt.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">''FILL&nbsp;TOTAL&nbsp;PAYMENT</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.Fill_Receipt_Data()</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'If&nbsp;Not&nbsp;SINV_NO&nbsp;&lt;=&nbsp;0&nbsp;Then</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSINV_RECEIPT.TxtCashPmt.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSINV_RECEIPT.TxtBankPmt.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSINV_RECEIPT.BnkACC&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSINV_RECEIPT.TxtChequeNo.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSINV_RECEIPT.TxtChequeDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_DATE&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSINV_RECEIPT.TxtChequeType.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'frmSINV_RECEIPT.TxtCashPmt.Text&nbsp;=&nbsp;&quot;0.00&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'frmSINV_RECEIPT.TxtBankPmt.Text&nbsp;=&nbsp;&quot;0.00&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'frmSINV_RECEIPT.CmbBankAccount.SelectedIndex&nbsp;=&nbsp;-1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'frmSINV_RECEIPT.TxtChequeNo.Text&nbsp;=&nbsp;Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'frmSINV_RECEIPT.TxtChequeDate.Text&nbsp;=&nbsp;Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'frmSINV_RECEIPT.TxtChequeType.Text&nbsp;=&nbsp;Nothing</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'End&nbsp;If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;Fix&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSINV_RECEIPT.ShowDialog(<span class="visualBasic__keyword">Me</span>)&nbsp;
&nbsp;
Fix:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;BttnSave_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;BttnSave.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text)&nbsp;&gt;=&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__string">&quot;01-03-2010&quot;</span>)&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.SavedpFlg1(Rd,&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;*&nbsp;FROM&nbsp;SALE_MASTER&nbsp;WHERE&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDescription.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Other's&nbsp;Description&nbsp;Here!&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDescription.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;Save&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDispDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Please&nbsp;enter&nbsp;description&nbsp;OR&nbsp;select&nbsp;correct&nbsp;value!&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Entry&nbsp;Required!&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Null_Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Count&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTotal.Text)&nbsp;&lt;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Please&nbsp;enter&nbsp;atleast&nbsp;one&nbsp;Item&nbsp;to&nbsp;save&nbsp;Invoice!&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Entry&nbsp;Required!&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.pFlg1&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvBalance.Text)&nbsp;&lt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Can't&nbsp;save!&quot;</span>&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Payment&nbsp;is&nbsp;more&nbsp;then&nbsp;Invoice&nbsp;Total&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Wrong&nbsp;Value!&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnReceipt.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'INSERT&nbsp;VALUES&nbsp;IN&nbsp;SALE&nbsp;MASTER</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValue(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;SALE_MASTER(nSINV_NO,&nbsp;nCLIENT_ID,&nbsp;sCASH_CLIENT,&nbsp;sCASH_MEMO_NO,&nbsp;nLPINV_NO,&nbsp;dDATE,&nbsp;dDISP_DATE,&nbsp;sVEHICLE,&nbsp;nFREIGHT,&nbsp;nUNLOADING,&nbsp;sTR_NO,&nbsp;nTR_QTY,&nbsp;nTOTAL_BILL,&nbsp;nDISCOUNT,&nbsp;nDISC_PERCENT,&nbsp;nOTHERS,&nbsp;sOTHER_DESC,&nbsp;nNET_TOTAL,&nbsp;nEMPLOYEE_CODE,&nbsp;nLOGIN_ID,&nbsp;nBUSINESS_CODE,&nbsp;sREMARKS,&nbsp;nD_MAN_CODE)&nbsp;VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtCashClient.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtCashMemo.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.LblLoadPass.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;','&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDispDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtVehicle.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtFreight.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtUnloading.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTRno.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTRqty.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTotal.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtDiscount.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtDiscPercent.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtOtherDisc.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDescription.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtNetTotal.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,10,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtRemarks.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;)&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Count&nbsp;-&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;PPP&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Pks&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Pcs&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Bonus&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColBonus&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Tot_Pcs&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tot_Pcs&nbsp;=&nbsp;(Pks&nbsp;*&nbsp;PPP)&nbsp;&#43;&nbsp;(Pcs&nbsp;&#43;&nbsp;Bonus)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'INSERT&nbsp;VALUES&nbsp;IN&nbsp;SALE&nbsp;DETAIL</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValue(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;SALE_DETAIL&nbsp;(nSINV_NO,&nbsp;nITEM_CODE,&nbsp;sBATCH_NO,&nbsp;nUNIT_COST,&nbsp;nUNIT_RATE,&nbsp;nDISC_RS,&nbsp;nDISC_PER,&nbsp;nSALE_TAX,&nbsp;nPPP,&nbsp;nQTY_PKS,&nbsp;nQTY_PCS,&nbsp;nQTY_BONUS,&nbsp;nQTY_Tot_PCS,&nbsp;nTOTAL_VALUE,&nbsp;dDATE)VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColBatch&quot;</span>).Value&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColCost&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColRate&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColDisc_Rs&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPercentage&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColSaleTax&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColBonus&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Tot_Pcs&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColTotal&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValueIN(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;CLIENT_RECEIPT(nCLIENT_ID,&nbsp;dDATE,&nbsp;nCASH_AMOUNT,&nbsp;nSINV_NO,&nbsp;nLOGIN_ID,&nbsp;nBUSINESS_CODE,&nbsp;nEMP_CODE,&nbsp;sDESCRIPTON)&nbsp;VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,10,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValueIN(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;CLIENT_RECEIPT(nCLIENT_ID,&nbsp;dDATE,&nbsp;sCHEQUE_NO,&nbsp;sCHEQUE_TYPE,&nbsp;dCHEQUE_DATE,&nbsp;nCHEQUE_AMOUNT,&nbsp;nCHEQUE_STATUS,&nbsp;sACCOUNT_CODE,&nbsp;nSINV_NO,&nbsp;nLOGIN_ID,&nbsp;nBUSINESS_CODE,&nbsp;nEMP_CODE,&nbsp;sDESCRIPTON)&nbsp;VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.CHEQ_DATE).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,1,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,10,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BOTH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValueIN(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;CLIENT_RECEIPT(nCLIENT_ID,&nbsp;dDATE,&nbsp;nCASH_AMOUNT,&nbsp;sCHEQUE_NO,&nbsp;sCHEQUE_TYPE,&nbsp;dCHEQUE_DATE,&nbsp;nCHEQUE_AMOUNT,&nbsp;nCHEQUE_STATUS,&nbsp;sACCOUNT_CODE,&nbsp;nSINV_NO,&nbsp;nLOGIN_ID,&nbsp;nBUSINESS_CODE,&nbsp;nEMP_CODE,&nbsp;sDESCRIPTON)&nbsp;VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.CHEQ_DATE).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,1,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,10,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Credit&nbsp;Sale&nbsp;Invoice&nbsp;Saved!&quot;</span>,&nbsp;MsgBoxStyle.Information,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Credit&nbsp;Invoice!&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrev.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrint.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSearch_Inv.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnReceipt.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;New&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnClose.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.pFlg1&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;This&nbsp;Invoice&nbsp;#&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;'&nbsp;is&nbsp;Already&nbsp;Exist.&nbsp;&quot;</span>&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;To&nbsp;modify&nbsp;this&nbsp;invoice&nbsp;please&nbsp;click&nbsp;on&nbsp;'Search&nbsp;Invoice'&nbsp;Button&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Already&nbsp;Exist!&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'UPDATE&nbsp;SALE&nbsp;INVOICE</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;Update&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDispDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Please&nbsp;enter&nbsp;description&nbsp;OR&nbsp;select&nbsp;correct&nbsp;value!&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Entry&nbsp;Required!&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Null_Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Count&nbsp;=&nbsp;<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTotal.Text)&nbsp;&lt;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Please&nbsp;enter&nbsp;atleast&nbsp;one&nbsp;Item&nbsp;to&nbsp;save&nbsp;Invoice!&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Entry&nbsp;Required!&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.pFlg1&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvBalance.Text)&nbsp;&lt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Can't&nbsp;save!&quot;</span>&nbsp;&amp;&nbsp;vbCrLf&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;Payment&nbsp;is&nbsp;more&nbsp;then&nbsp;Invoice&nbsp;Total&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Wrong&nbsp;Value!&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnReceipt.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'UPDATE&nbsp;VALUES&nbsp;IN&nbsp;SALE&nbsp;MASTER</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asUpdate.UpdateValue(<span class="visualBasic__string">&quot;UPDATE&nbsp;SALE_MASTER&nbsp;SET&nbsp;nCLIENT_ID='&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;sCASH_CLIENT='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtCashClient.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;sCASH_MEMO_NO='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtCashMemo.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nLPINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.LblLoadPass.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;dDATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;dDISP_DATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDispDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;sVEHICLE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtVehicle.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nFREIGHT=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtFreight.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nUNLOADING=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtUnloading.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sTR_NO='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTRno.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nTR_QTY=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTRqty.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nTOTAL_BILL=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtTotal.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nDISCOUNT=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtDiscount.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nDISC_PERCENT=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtDiscPercent.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nOTHERS=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtOtherDisc.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sOTHER_DESC='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDescription.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nNET_TOTAL=&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtNetTotal.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nEMPLOYEE_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nLOGIN_ID=10,&nbsp;nBUSINESS_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sREMARKS='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtRemarks.Text&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nD_MAN_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;WHERE&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;i&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;i&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Count&nbsp;-&nbsp;<span class="visualBasic__number">2</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;PPP&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Pks&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Pcs&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Bonus&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColBonus&quot;</span>).Value)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Tot_Pcs&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Double</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tot_Pcs&nbsp;=&nbsp;(Pks&nbsp;*&nbsp;PPP)&nbsp;&#43;&nbsp;(Pcs&nbsp;&#43;&nbsp;Bonus)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColSR&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'INSERT&nbsp;VALUES&nbsp;IN&nbsp;SALE&nbsp;DETAIL</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValue(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;SALE_DETAIL&nbsp;(nSINV_NO,&nbsp;nITEM_CODE,&nbsp;sBATCH_NO,&nbsp;nUNIT_COST,&nbsp;nUNIT_RATE,&nbsp;nDISC_RS,&nbsp;nDISC_PER,&nbsp;nSALE_TAX,&nbsp;nPPP,&nbsp;nQTY_PKS,&nbsp;nQTY_PCS,&nbsp;nQTY_BONUS,&nbsp;nQTY_Tot_PCS,&nbsp;nTOTAL_VALUE,&nbsp;dDATE)VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColBatch&quot;</span>).Value&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColCost&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColRate&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColDisc_Rs&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPercentage&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColSaleTax&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColBonus&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Tot_Pcs&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColTotal&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColSR&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'UPDATE&nbsp;VALUES&nbsp;IN&nbsp;SALE&nbsp;DETAIL</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asUpdate.UpdateValue(<span class="visualBasic__string">&quot;UPDATE&nbsp;SALE_DETAIL&nbsp;SET&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nITEM_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sBATCH_NO='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColBatch&quot;</span>).Value&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nUNIT_COST=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColCost&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nUNIT_RATE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColRate&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nDISC_RS=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColDisc_Rs&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nDISC_PER=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPercentage&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nSALE_TAX=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColSaleTax&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nPPP=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nQTY_PKS=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nQTY_PCS=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nQTY_BONUS=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColBonus&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nQTY_Tot_PCS=&quot;</span>&nbsp;&amp;&nbsp;Tot_Pcs&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nTOTAL_VALUE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColTotal&quot;</span>).Value)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;dDATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;'&nbsp;WHERE&nbsp;nID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(i).Cells(<span class="visualBasic__string">&quot;ColSR&quot;</span>).Value.ToString)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.SavedpFlg2(Rd,&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;*&nbsp;FROM&nbsp;CLIENT_RECEIPT&nbsp;WHERE&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.pFlg2&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asUpdate.UpdateValueIN(<span class="visualBasic__string">&quot;UPDATE&nbsp;CLIENT_RECEIPT&nbsp;SET&nbsp;nCLIENT_ID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;dDATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nCASH_AMOUNT=&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nLOGIN_ID=10,&nbsp;nBUSINESS_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nEMP_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sDESCRIPTON='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;'&nbsp;WHERE&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValueIN(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;CLIENT_RECEIPT(nCLIENT_ID,&nbsp;dDATE,&nbsp;nCASH_AMOUNT,&nbsp;nSINV_NO,&nbsp;nLOGIN_ID,&nbsp;nBUSINESS_CODE,&nbsp;nEMP_CODE,&nbsp;sDESCRIPTON)&nbsp;VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,10,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.pFlg2&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asUpdate.UpdateValueIN(<span class="visualBasic__string">&quot;UPDATE&nbsp;CLIENT_RECEIPT&nbsp;SET&nbsp;nCLIENT_ID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;dDATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;sCHEQUE_NO='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',sCHEQUE_TYPE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;dCHEQUE_DATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.CHEQ_DATE).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nCHEQUE_AMOUNT=&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sACCOUNT_CODE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nLOGIN_ID=10,&nbsp;nBUSINESS_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nEMP_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sDESCRIPTON='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;'&nbsp;WHERE&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValueIN(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;CLIENT_RECEIPT(nCLIENT_ID,&nbsp;dDATE,&nbsp;sCHEQUE_NO,&nbsp;sCHEQUE_TYPE,&nbsp;dCHEQUE_DATE,&nbsp;nCHEQUE_AMOUNT,&nbsp;nCHEQUE_STATUS,&nbsp;sACCOUNT_CODE,&nbsp;nSINV_NO,&nbsp;nLOGIN_ID,&nbsp;nBUSINESS_CODE,&nbsp;nEMP_CODE,&nbsp;sDESCRIPTON)&nbsp;VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.CHEQ_DATE).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,1,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,10,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BOTH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.pFlg2&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asUpdate.UpdateValueIN(<span class="visualBasic__string">&quot;UPDATE&nbsp;CLIENT_RECEIPT&nbsp;SET&nbsp;nCLIENT_ID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;dDATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nCASH_AMOUNT=&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sCHEQUE_NO='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',sCHEQUE_TYPE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;dCHEQUE_DATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.CHEQ_DATE).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nCHEQUE_AMOUNT=&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sACCOUNT_CODE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nLOGIN_ID=10,&nbsp;nBUSINESS_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nEMP_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sDESCRIPTON='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;'&nbsp;WHERE&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValueIN(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;CLIENT_RECEIPT(nCLIENT_ID,&nbsp;dDATE,&nbsp;nCASH_AMOUNT,&nbsp;sCHEQUE_NO,sCHEQUE_TYPE,&nbsp;dCHEQUE_DATE,&nbsp;nCHEQUE_AMOUNT,&nbsp;nCHEQUE_STATUS,&nbsp;sACCOUNT_CODE,&nbsp;nSINV_NO,&nbsp;nLOGIN_ID,&nbsp;nBUSINESS_CODE,&nbsp;nEMP_CODE,&nbsp;sDESCRIPTON)&nbsp;VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.TxtDate.Text).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.CHEQ_DATE).ToString(<span class="visualBasic__string">&quot;MM-dd-yyyy&quot;</span>)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,1,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,10,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;(Val(<span class="visualBasic__keyword">Me</span>.CASH_AMT)&nbsp;&#43;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.BANK_AMT))&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.pFlg2&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asUpdate.UpdateValueIN(<span class="visualBasic__string">&quot;UPDATE&nbsp;CLIENT_RECEIPT&nbsp;SET&nbsp;nCLIENT_ID=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;dDATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.Rec_Date&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nCASH_AMOUNT=&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sCHEQUE_NO='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',sCHEQUE_TYPE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;dCHEQUE_DATE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_DATE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nCHEQUE_AMOUNT=&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sACCOUNT_CODE='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;nLOGIN_ID=10,&nbsp;nBUSINESS_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;nEMP_CODE=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;sDESCRIPTON='&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;'&nbsp;WHERE&nbsp;nSINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.asInsert.SaveValueIN(<span class="visualBasic__string">&quot;INSERT&nbsp;INTO&nbsp;CLIENT_RECEIPT(nCLIENT_ID,&nbsp;dDATE,&nbsp;nCASH_AMOUNT,&nbsp;sCHEQUE_NO,&nbsp;sCHEQUE_TYPE,&nbsp;dCHEQUE_DATE,&nbsp;nCHEQUE_AMOUNT,&nbsp;nCHEQUE_STATUS,&nbsp;sACCOUNT_CODE,&nbsp;nSINV_NO,&nbsp;nLOGIN_ID,&nbsp;nBUSINESS_CODE,&nbsp;nEMP_CODE,&nbsp;sDESCRIPTON)&nbsp;VALUES(&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.Rec_Date&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_NO&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_TYPE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_DATE&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&nbsp;&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,1,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_ACC&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;',&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,10,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedItem.Col3)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;,'&quot;</span>&nbsp;&amp;&nbsp;<span class="visualBasic__keyword">Me</span>.DESCRIPTION&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;')&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;Credit&nbsp;Sale&nbsp;Invoice&nbsp;Saved!&quot;</span>,&nbsp;MsgBoxStyle.Information,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Credit&nbsp;Invoice!&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrev.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnPrint.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSearch_Inv.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnReceipt.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;New&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnNew.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnAdd.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;&amp;Save&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnSave.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BttnClose.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Disable_All()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.asSELECT.pFlg1&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;This&nbsp;Invoice&nbsp;#&nbsp;&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&nbsp;is&nbsp;not&nbsp;Exist.&quot;</span>,&nbsp;MsgBoxStyle.Exclamation,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Not&nbsp;Exist!&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;BttnClose_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;BttnClose.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;MsgBox(<span class="visualBasic__string">&quot;Are&nbsp;you&nbsp;sure&nbsp;to&nbsp;Close?&quot;</span>,&nbsp;MsgBoxStyle.Question&nbsp;&#43;&nbsp;vbYesNo,&nbsp;<span class="visualBasic__string">&quot;(NS)&nbsp;-&nbsp;Close?&quot;</span>)&nbsp;=&nbsp;MsgBoxResult.Yes&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Close()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;Search&nbsp;Button&nbsp;Control</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;BttnSearch_Inv_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;BttnSearch_Inv.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;Fix&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSEARCH_S_INV.TxtClient.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSEARCH_S_INV.TxtInvoice.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSEARCH_S_INV.TxtDateFrom.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSEARCH_S_INV.TxtDateTo.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSEARCH_S_INV.FrmStr&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SALE&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frmSEARCH_S_INV.ShowDialog(<span class="visualBasic__keyword">Me</span>)&nbsp;
Fix:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;Print&nbsp;Button&nbsp;Control</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;BttnPrev_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;BttnPrev.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Rpt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;rptSALES_INVOICE_WS&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Frm&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;frmRPT&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.CRV.ReportSource&nbsp;=&nbsp;Rpt&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.CRV.SelectionFormula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;{V_SALE_MASTER.SINV_NO}=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Sale&nbsp;Invoice&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.MdiParent&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.ParentForm&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.Show()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.Activate()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;Ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(Ex.Message)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;BttnPrint_Click(<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.<span class="visualBasic__keyword">Object</span>,&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;e&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;System.EventArgs)&nbsp;<span class="visualBasic__keyword">Handles</span>&nbsp;BttnPrint.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Rpt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;rptSALES_INVOICE_WS&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Frm&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;frmRPT&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.CRV.ReportSource&nbsp;=&nbsp;Rpt&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.CRV.SelectionFormula&nbsp;=&nbsp;<span class="visualBasic__string">&quot;{V_SALE_MASTER.SINV_NO}=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Frm.CRV.PrintReport()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;Ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(Ex.Message)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span><span class="visualBasic__preproc">&nbsp;
&nbsp;
#Region&nbsp;&quot;Sub&nbsp;and&nbsp;Functions</span>&quot;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;FillComboBox_Client()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;ID,&nbsp;NAME,&nbsp;SHOP_NAME,&nbsp;SHOP_ADD,&nbsp;AREA,&nbsp;HOME_ADD,&nbsp;SHOP_PH,&nbsp;HOME_PH,&nbsp;CELL_NO,&nbsp;FAX_NO,&nbsp;E_MAIL,&nbsp;WEB_SITE,&nbsp;CASE&nbsp;STATUS&nbsp;WHEN&nbsp;'0'&nbsp;THEN&nbsp;'No'&nbsp;WHEN&nbsp;'1'&nbsp;THEN&nbsp;'Yes'&nbsp;END&nbsp;AS&nbsp;STATUS,&nbsp;CLIENT_CAT,&nbsp;CLIENT_GD,&nbsp;CLIENT_TYPE,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;CREDIT_LIM)&nbsp;AS&nbsp;CREDIT_LIM,&nbsp;GST_NO,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;OPEN_BAL)&nbsp;AS&nbsp;OPEN_BAL,&nbsp;VISIT_TYPE,&nbsp;NO_VISIT,&nbsp;ROUTE&nbsp;FROM&nbsp;V_CLIENT_INFO&nbsp;WHERE&nbsp;STATUS='1'&nbsp;ORDER&nbsp;BY&nbsp;SHOP_NAME&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str1,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daCLIENT_INFO&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd1)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsCLIENT_INFO1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daCLIENT_INFO.Fill(<span class="visualBasic__keyword">Me</span>.DsCLIENT_INFO1.V_CLIENT_INFO)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;dtLoading&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;DataTable(<span class="visualBasic__string">&quot;V_CLIENT_INFO&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;ID&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;NAME&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;SHOP_NAME&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Cnt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;Cnt&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DsCLIENT_INFO1.V_CLIENT_INFO.Count&nbsp;-&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;dr&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;DataRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr&nbsp;=&nbsp;dtLoading.NewRow&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;ID&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(<span class="visualBasic__number">0</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;NAME&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(<span class="visualBasic__number">1</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;SHOP_NAME&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(<span class="visualBasic__number">2</span>).ToString&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Rows.Add(dr)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.Items.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.LoadingType&nbsp;=&nbsp;MTGCComboBox.CaricamentoCombo.DataTable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SourceDataString&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;<span class="visualBasic__keyword">String</span>(<span class="visualBasic__number">2</span>)&nbsp;{<span class="visualBasic__string">&quot;SHOP_NAME&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;NAME&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;ID&quot;</span>}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SourceDataTable&nbsp;=&nbsp;dtLoading&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;FillComboBox_Group()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;nID,&nbsp;sGROUP_NAME,&nbsp;sGROUP_DEALER,&nbsp;sSTATUS&nbsp;sBUSINESS_NAME&nbsp;FROM&nbsp;V_BUSINESS_GROUP&nbsp;WHERE&nbsp;sSTATUS='1'&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str1,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daLUP_BUSINESS_GROUP&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd1)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_BUSINESS_GROUP1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daLUP_BUSINESS_GROUP.Fill(<span class="visualBasic__keyword">Me</span>.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;dtLoading&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;DataTable(<span class="visualBasic__string">&quot;V_BUSINESS_GROUP&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;nID&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;sGROUP_NAME&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;sGROUP_DEALER&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Cnt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;Cnt&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Count&nbsp;-&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;dr&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;DataRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr&nbsp;=&nbsp;dtLoading.NewRow&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;nID&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(<span class="visualBasic__number">0</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;sGROUP_NAME&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(<span class="visualBasic__number">1</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;sGROUP_DEALER&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_BUSINESS_GROUP1.V_BUSINESS_GROUP.Item(Cnt).Item(<span class="visualBasic__number">2</span>).ToString&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Rows.Add(dr)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.Items.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.LoadingType&nbsp;=&nbsp;MTGCComboBox.CaricamentoCombo.DataTable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SourceDataString&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;<span class="visualBasic__keyword">String</span>(<span class="visualBasic__number">2</span>)&nbsp;{<span class="visualBasic__string">&quot;sGROUP_NAME&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;sGROUP_DEALER&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;nID&quot;</span>}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SourceDataTable&nbsp;=&nbsp;dtLoading&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;FillComboBox_Employee()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;CODE,&nbsp;NAME,&nbsp;FATHER_NAME,&nbsp;NIC,&nbsp;HOME_PH,&nbsp;CELL,&nbsp;PRE_ADD,&nbsp;PER_ADD,&nbsp;DESIGNATION,&nbsp;APP_DATE,&nbsp;PAY,&nbsp;STATUS,&nbsp;LEAVE_DATE,&nbsp;EMAIL_ADD,&nbsp;BANK_ACC,&nbsp;BANK_ADD&nbsp;FROM&nbsp;V_EMPLOYEE_INFO&nbsp;WHERE&nbsp;STATUS='1'&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str1,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daLUP_EMPLOYEE&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd1)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_EMPLOYEE1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daLUP_EMPLOYEE.Fill(<span class="visualBasic__keyword">Me</span>.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;dtLoading&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;DataTable(<span class="visualBasic__string">&quot;V_EMPLOYEE_INFO&quot;</span>)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;CODE&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;NAME&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Columns.Add(<span class="visualBasic__string">&quot;DESIGNATION&quot;</span>,&nbsp;System.Type.<span class="visualBasic__keyword">GetType</span>(<span class="visualBasic__string">&quot;System.String&quot;</span>))&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Cnt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;Cnt&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Count&nbsp;-&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;dr&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;DataRow&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr&nbsp;=&nbsp;dtLoading.NewRow&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;CODE&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(<span class="visualBasic__number">0</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;NAME&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(<span class="visualBasic__number">1</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dr(<span class="visualBasic__string">&quot;DESIGNATION&quot;</span>)&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO.Item(Cnt).Item(<span class="visualBasic__number">8</span>).ToString&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dtLoading.Rows.Add(dr)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.Items.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.LoadingType&nbsp;=&nbsp;MTGCComboBox.CaricamentoCombo.DataTable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SourceDataString&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;<span class="visualBasic__keyword">String</span>(<span class="visualBasic__number">2</span>)&nbsp;{<span class="visualBasic__string">&quot;NAME&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;DESIGNATION&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;CODE&quot;</span>}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SourceDataTable&nbsp;=&nbsp;dtLoading&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.Items.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.LoadingType&nbsp;=&nbsp;MTGCComboBox.CaricamentoCombo.DataTable&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SourceDataString&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;<span class="visualBasic__keyword">String</span>(<span class="visualBasic__number">2</span>)&nbsp;{<span class="visualBasic__string">&quot;NAME&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;DESIGNATION&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;CODE&quot;</span>}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SourceDataTable&nbsp;=&nbsp;dtLoading&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Fill_Master_Date()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;SINV_NO,&nbsp;SHOP_NAME,&nbsp;CASH_CLIENT,&nbsp;CASH_MEMO,&nbsp;LPINV_NO,&nbsp;S_DATE,&nbsp;DISP_DATE,&nbsp;VEHICLE,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;FREIGHT)&nbsp;AS&nbsp;FREIGHT,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;UNLOADING)&nbsp;AS&nbsp;UNLOADING,&nbsp;TR_NO,&nbsp;TR_QTY,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;TOT_BILL)&nbsp;AS&nbsp;TOT_BILL,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;DISC_RS)&nbsp;AS&nbsp;DISC_RS,&nbsp;DISC_PER,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;DISC_OTHER)&nbsp;AS&nbsp;DISC_OTHER,&nbsp;OTHER_DESC,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;NET_TOTAL)&nbsp;AS&nbsp;NET_TOTAL,&nbsp;EMP_NAME,&nbsp;GROUP_NAME,&nbsp;REMARKS,&nbsp;D_MAN&nbsp;FROM&nbsp;V_SALE_MASTER&nbsp;WHERE&nbsp;SINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd2&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str2,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_SALE_MASTER&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd2)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_MASTER1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_SALE_MASTER.Fill(<span class="visualBasic__keyword">Me</span>.DsV_SALE_MASTER1.V_SALE_MASTER)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Fill_Detail_Data()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str3&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;ID,&nbsp;SINV_NO,&nbsp;ITEM_CODE,&nbsp;ITEM_NAME,&nbsp;BATCH_NO,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;UNIT_COST)&nbsp;AS&nbsp;UNIT_COST&nbsp;,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;UNIT_RATE)&nbsp;AS&nbsp;UNIT_RATE&nbsp;,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;DISC_RS)&nbsp;AS&nbsp;DISC_RS,&nbsp;DISC_PER,&nbsp;PPP,&nbsp;QTY_PKS,&nbsp;QTY_PCS,&nbsp;QTY_BONUS,&nbsp;QTY_TOT_PCS,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;TOTAL_VALUE)&nbsp;AS&nbsp;TOTAL_VALUE,&nbsp;SCM_ITEM_CODE,&nbsp;SCM_ITEM,&nbsp;SCM_QTY,&nbsp;SALE_TAX&nbsp;FROM&nbsp;V_SALE_DETAIL&nbsp;WHERE&nbsp;SINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd3&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str3,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_SALE_DETAIL&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd3)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_SALE_DETAIL.Fill(<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;Fix&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Clear()&nbsp;
Fix:&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Cnt&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Integer</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;Cnt&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">To</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Count&nbsp;-&nbsp;<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Add()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColCode&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">2</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColBatch&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">4</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColName&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">3</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColCost&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">5</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColRate&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">6</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColPack&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">10</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColPiece&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">11</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColBonus&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">12</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColPercentage&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">8</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColDisc_Rs&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">7</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColSaleTax&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">18</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColTotal&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">14</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColSR&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">0</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows(Cnt).Cells(<span class="visualBasic__string">&quot;ColPPP&quot;</span>).Value&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_SALE_DETAIL1.V_SALE_DETAIL.Item(Cnt).Item(<span class="visualBasic__number">9</span>).ToString&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Fill_Receipt_Data()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Str1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;SELECT&nbsp;ID,&nbsp;CLIENT_ID,&nbsp;SHOP_NAME,&nbsp;R_DATE,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;CASH_AMT)&nbsp;AS&nbsp;CASH_AMT,&nbsp;CHEQ_NO,&nbsp;CHEQ_TYPE,&nbsp;CHEQ_DATE,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;BANK_AMT)&nbsp;AS&nbsp;BANK_AMT,&nbsp;SINV_NO,&nbsp;CHEQ_STATUS,&nbsp;STATUS_DATE,&nbsp;STATUS_DESC,&nbsp;BANK_ACC,&nbsp;BANK_NAME,&nbsp;EMP_NAME,&nbsp;USER_NAME,&nbsp;GROUP_NAME,&nbsp;DESCRIPTION,&nbsp;CONVERT(NUMERIC(18,2),&nbsp;TOT_RECEIPT)&nbsp;AS&nbsp;TOT_RECEIPT&nbsp;FROM&nbsp;V_CLIENT_RECEIPT&nbsp;WHERE&nbsp;SINV_NO=&quot;</span>&nbsp;&amp;&nbsp;Val(<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text)&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;SqlCmd1&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand(Str1,&nbsp;<span class="visualBasic__keyword">Me</span>.SqlConnection1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_CLIENT_RECEIPT&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlDataAdapter(SqlCmd1)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.daV_CLIENT_RECEIPT.Fill(<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT)&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'FILL&nbsp;VALUE&nbsp;IN&nbsp;PAYMENT's&nbsp;VARIABLES</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Count&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SINV_NO&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">9</span>).ToString)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rec_Date&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">3</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CASH_AMT&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">4</span>).ToString)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_NO&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">5</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_TYPE&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">6</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_DATE&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">7</span>).ToString&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BANK_AMT&nbsp;=&nbsp;Val(<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">8</span>).ToString)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BANK_ACC&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">13</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DESCRIPTION&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsV_CLIENT_RECEIPT1.V_CLIENT_RECEIPT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__number">17</span>).ToString&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&lt;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BOTH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&lt;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BOTH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_AMT&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_AMT&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BOTH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;CHEQ_DATE.Length&nbsp;&gt;&nbsp;<span class="visualBasic__number">0</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CHEQ_DATE&nbsp;=&nbsp;<span class="visualBasic__keyword">CDate</span>(<span class="visualBasic__keyword">Me</span>.CHEQ_DATE).ToString(<span class="visualBasic__string">&quot;dd-MMM-yyyy&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rec_Date&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CASH_AMT&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_NO&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_TYPE&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CHEQ_DATE&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BANK_AMT&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;BANK_ACC&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SINV_NO&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DESCRIPTION&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BOTH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.BANK_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CASH_PAY&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'MsgBox(ex.Message)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Null_Focus()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDate.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDispDate.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDispDate.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ElseIf</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;<span class="visualBasic__keyword">Or</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.Focus()&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Disable_All()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Ctrl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Control&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;Ctrl&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;<span class="visualBasic__keyword">Me</span>.Controls&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;Ctrl.Name&nbsp;=&nbsp;<span class="visualBasic__string">&quot;GroupBox1&quot;</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;Ctrl.Name&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Label3&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ctrl.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Enable_All()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;Ctrl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Control&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;Ctrl&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;<span class="visualBasic__keyword">Me</span>.Controls&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;Ctrl.Name&nbsp;=&nbsp;<span class="visualBasic__string">&quot;GroupBox1&quot;</span>&nbsp;<span class="visualBasic__keyword">And</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;Ctrl.Name&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Label3&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ctrl.Enabled&nbsp;=&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Clear_All()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtInvoice.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtVehicle.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtCashClient.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtReceivables.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtStandyBy.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtNET_Receivable.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblB_Pcs.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblPPP.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRatePcs.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRate.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblRetail.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.LblStock.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTotalItems.Text&nbsp;=&nbsp;<span class="visualBasic__number">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTRno.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTRqty.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtFreight.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;0.00&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtUnloading.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;0.00&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtTotal.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;0.00&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDiscount.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;0.00&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtDiscPercent.Text&nbsp;=&nbsp;<span class="visualBasic__string">&quot;0.00&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.TxtReceipt.Text&nbsp;=&nbsp;&quot;0.00&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtRemarks.Text&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.TxtCashMemo.Focus()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.CASH_AMT&nbsp;=&nbsp;0.0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.BANK_AMT&nbsp;=&nbsp;0.0</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.BANK_ACC&nbsp;=&nbsp;Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.CHEQ_NO&nbsp;=&nbsp;Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.CHEQ_DATE&nbsp;=&nbsp;Nothing</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Me.CHEQ_TYPE&nbsp;=&nbsp;Nothing</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.Default_Setting()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;Fix&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.Rows.Clear()&nbsp;
Fix:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;Default_Setting()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">On</span>&nbsp;<span class="visualBasic__keyword">Error</span>&nbsp;<span class="visualBasic__keyword">GoTo</span>&nbsp;Fix&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;StrCMB&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StrCMB&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsNS_DEFAULT1.NS_DEFAULT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__string">&quot;GROUP&quot;</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;StrCMB&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.SelectedIndex&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CmbGroup.FindString(StrCMB)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StrCMB&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsNS_DEFAULT1.NS_DEFAULT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__string">&quot;S_MAN&quot;</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;StrCMB&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.SelectedIndex&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CmbS_Man.FindString(StrCMB)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StrCMB&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsNS_DEFAULT1.NS_DEFAULT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__string">&quot;D_MAN&quot;</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;StrCMB&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.SelectedIndex&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CmbD_Man.FindString(StrCMB)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;StrCMB&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.DsNS_DEFAULT1.NS_DEFAULT.Item(<span class="visualBasic__number">0</span>).Item(<span class="visualBasic__string">&quot;CLIENT&quot;</span>).ToString&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;-<span class="visualBasic__number">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Not</span>&nbsp;StrCMB&nbsp;=&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.SelectedIndex&nbsp;=&nbsp;<span class="visualBasic__keyword">Me</span>.CmbClient.FindString(StrCMB)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
Fix:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span><span class="visualBasic__preproc">&nbsp;
#End&nbsp;Region</span>&nbsp;
&nbsp;
==================================&nbsp;
&nbsp;
&nbsp;
<span class="visualBasic__keyword">Imports</span>&nbsp;SDS&nbsp;=&nbsp;System.Data.SqlClient&nbsp;
<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Class</span>&nbsp;AssConn&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;Conn,&nbsp;Conn2,&nbsp;Conn3&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlConnection&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Public</span>&nbsp;Cmd,&nbsp;Cmd2,&nbsp;Cmd3&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;SDS.SqlCommand&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;<span class="visualBasic__keyword">New</span>()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Conn.ConnectionString&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Data&nbsp;Source=server;Initial&nbsp;Catalog=Neuro_BS;User&nbsp;ID=sa&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Conn2.ConnectionString&nbsp;=&nbsp;<span class="visualBasic__string">&quot;workstation&nbsp;id=SERVER;packet&nbsp;size=32767;integrated&nbsp;security=SSPI;data&nbsp;source=SERVER;persist&nbsp;security&nbsp;info=False;Connect&nbsp;Timeout=30;initial&nbsp;catalog=Neuro_BS&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Conn3.ConnectionString&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Data&nbsp;Source=server;Initial&nbsp;Catalog=Neuro_BS;Integrated&nbsp;Security=True&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Sub</span>&nbsp;
&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Class</span>&nbsp;</pre>
</div>
</div>
</div>
<h1>More Information</h1>
<p><em>For more information, see ...</em></p>
<p><em><a href="http://www.a1vbcode.com/author.asp?name=Ishtiaq&#43;Ahmed">http://www.a1vbcode.com/author.asp?name=Ishtiaq&#43;Ahmed</a><br>
</em></p>
