Imports System.Windows.Forms
Imports SDS = System.Data.SqlClient
Public Class frmMain

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

#Region "VARIABLES"
    Dim asSELECT As New AssSelect
    Dim asMAX As New AssMaxNo
    Dim Rd As System.Data.SqlClient.SqlDataReader
#End Region

#Region "FORM EVENTS"

    Private Sub frmMain_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If MsgBox("Are you sure to Shutdown the Application?", MsgBoxStyle.Question + vbYesNo, "(NS) - Shutdown?") = MsgBoxResult.Yes Then
            Global.System.Windows.Forms.Application.Exit()
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        With My.Computer.Keyboard
            Select Case e.KeyCode
                Case Keys.NumLock
                    Me.ToolStripNumLock.Enabled = .NumLock

                Case Keys.CapsLock
                    Me.ToolStripCAPS.Enabled = .CapsLock

                Case Keys.Scroll
                    Me.ToolStripSCROLL.Enabled = .ScrollLock

            End Select
        End With
    End Sub

    Private Sub FrmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Activate()
        With My.Computer.Keyboard
            Me.ToolStripNumLock.Enabled = .NumLock
            Me.ToolStripCAPS.Enabled = .CapsLock
            Me.ToolStripSCROLL.Enabled = .ScrollLock
        End With

        'FrmMn.MdiParent = Me
        'FrmMn.Size = New System.Drawing.Size(Me.Size.Width - 25, Me.Size.Height - 120)
        'FrmMn.Show()
    End Sub
    Private Sub FrmMain_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        'Dim Frm As Form
        'For Each Frm In Me.MdiChildren
        '    If Frm.Text = Nothing Then
        '        Frm.Size = New System.Drawing.Size(Me.Size.Width - 25, Me.Size.Height - 120)
        '    End If
        'Next
    End Sub

#End Region

#Region "FILE MENU EVENTS"
    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub
    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub
    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub MnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        On Error GoTo Fix
        If Not Me.ActiveMdiChild.Text = Nothing Then
            Me.ActiveMdiChild.Close()
        End If
Fix:
    End Sub
    Private Sub MnuCloseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseAllToolStripMenuItem1.Click
        For Each ChildForm As Form In Me.MdiChildren
            If Not ChildForm.Text = Nothing Then
                ChildForm.Close()
            End If
        Next
    End Sub
    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        'Global.System.Windows.Forms.Application.Exit()
        Me.Close()
    End Sub
#End Region

#Region "CODE MENU EVENTS"
    Private Sub BusinessInformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BusinessInformationToolStripMenuItem.Click
        frmLUP_BUSINESS_INFO.MdiParent = Me
        frmLUP_BUSINESS_INFO.Show()
        frmLUP_BUSINESS_INFO.Activate()
    End Sub

    Private Sub BusinessGroupsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BusinessGroupsToolStripMenuItem.Click
        frmLUP_BUSINESS_GROUP.MdiParent = Me
        frmLUP_BUSINESS_GROUP.Show()
        frmLUP_BUSINESS_GROUP.Activate()
    End Sub
    Private Sub BankAccountsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankAccountsToolStripMenuItem.Click
        Try
            frmLUP_BANK.MdiParent = Me
            frmLUP_BANK.Show()
            frmLUP_BANK.Activate()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    'ZONE / AREA / ROUTES
    Private Sub ZoneStationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoneStationToolStripMenuItem.Click
        frmLUP_ZONE.MdiParent = Me
        frmLUP_ZONE.Show()
        frmLUP_ZONE.Activate()
    End Sub
    Private Sub AreaLocationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AreaLocationToolStripMenuItem.Click
        frmLUP_AREA.MdiParent = Me
        frmLUP_AREA.Show()
        frmLUP_AREA.Activate()
    End Sub
    Private Sub RoutesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RoutesToolStripMenuItem.Click
        frmLUP_ROUTES.MdiParent = Me
        frmLUP_ROUTES.Show()
        frmLUP_ROUTES.Activate()
    End Sub

    'EXPENSES HEADS
    Private Sub ExpenseHeadsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpenseHeadsToolStripMenuItem.Click
        frmLUP_EXPENSES.MdiParent = Me
        frmLUP_EXPENSES.Show()
        frmLUP_EXPENSES.Activate()
    End Sub
    'SUB HEADS
    Private Sub ExpenseSubHeadsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpenseSubHeadsToolStripMenuItem.Click
        frmLUP_EXPENSES_SUB.MdiParent = Me
        frmLUP_EXPENSES_SUB.Show()
        frmLUP_EXPENSES_SUB.Activate()
    End Sub

    'CLIENT
    Private Sub ClientTypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientTypeToolStripMenuItem.Click
        frmLUP_CLIENT_TYPE.MdiParent = Me
        frmLUP_CLIENT_TYPE.Show()
        frmLUP_CLIENT_TYPE.Activate()
    End Sub
    Private Sub ClientGradesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientGradesToolStripMenuItem.Click
        frmLUP_CLIENT_GD.MdiParent = Me
        frmLUP_CLIENT_GD.Show()
        frmLUP_CLIENT_GD.Activate()
    End Sub
    Private Sub ShopCatagoriesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShopCatagoriesToolStripMenuItem.Click
        frmLUP_SHOP_CAT.MdiParent = Me
        frmLUP_SHOP_CAT.Show()
        frmLUP_SHOP_CAT.Activate()
    End Sub
    Private Sub ClientInformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientInformationToolStripMenuItem.Click
        frmLUP_CLIENT.MdiParent = Me
        frmLUP_CLIENT.Show()
        frmLUP_CLIENT.Activate()
    End Sub
    Private Sub ClientOpeningBalancesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientOpeningBalancesToolStripMenuItem.Click
        frmLUP_CLIENT_OPEN_BAL.MdiParent = Me
        frmLUP_CLIENT_OPEN_BAL.Show()
        frmLUP_CLIENT_OPEN_BAL.Activate()
    End Sub

    'SUPPLIER
    Private Sub SupplierInformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierInformationToolStripMenuItem.Click
        frmLUP_SUPPLIER.MdiParent = Me
        frmLUP_SUPPLIER.Show()
        frmLUP_SUPPLIER.Activate()
    End Sub
    Private Sub SupplierOpeningBalancesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierOpeningBalancesToolStripMenuItem.Click
        frmLUP_SUPPLIER_OPEN_BAL.MdiParent = Me
        frmLUP_SUPPLIER_OPEN_BAL.Show()
        frmLUP_SUPPLIER_OPEN_BAL.Activate()
    End Sub

    'VENDORS
    Private Sub VendorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VendorsToolStripMenuItem.Click
        frmLUP_VENDORS.MdiParent = Me
        frmLUP_VENDORS.Show()
        frmLUP_VENDORS.Activate()
    End Sub

    'ITEM CATEGORIES
    Private Sub ItemProductCategoriesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemProductCategoriesToolStripMenuItem.Click
        frmLUP_ITEM_CAT.MdiParent = Me
        frmLUP_ITEM_CAT.Show()
        frmLUP_ITEM_CAT.Activate()
    End Sub

    'ITEMS
    Private Sub ItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsToolStripMenuItem.Click
        'For Wholesaler
        frmLUP_ITEM.MdiParent = Me
        frmLUP_ITEM.Show()
        frmLUP_ITEM.Activate()
        frmLUP_ITEM.WindowState = FormWindowState.Normal

        ''For Retailer
        'frmLUP_ITEM_RTL.MdiParent = Me
        'frmLUP_ITEM_RTL.Show()
        'frmLUP_ITEM_RTL.Activate()
        'frmLUP_ITEM_RTL.WindowState = FormWindowState.Normal
    End Sub
    Private Sub OpeningStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpeningStockToolStripMenuItem.Click
        frmLUP_ITEM_OPEN_STK.MdiParent = Me
        frmLUP_ITEM_OPEN_STK.Show()
        frmLUP_ITEM_OPEN_STK.Activate()
    End Sub
    Private Sub OpeningStockAdjustmentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpeningStockAdjustmentToolStripMenuItem.Click
        frmLUP_ITEM_OPEN_STK_ADJ.MdiParent = Me
        frmLUP_ITEM_OPEN_STK_ADJ.Show()
        frmLUP_ITEM_OPEN_STK_ADJ.Activate()
    End Sub
    Private Sub SchemeItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SchemeItemsToolStripMenuItem.Click

    End Sub

    'EMPLOYEE
    Private Sub EmployeeDesignationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeDesignationToolStripMenuItem.Click
        frmLUP_DESIGNATION.MdiParent = Me
        frmLUP_DESIGNATION.Show()
        frmLUP_DESIGNATION.Activate()
    End Sub
    Private Sub EmployeeInformationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmployeeInformationToolStripMenuItem.Click
        frmLUP_EMPLOYEE.MdiParent = Me
        frmLUP_EMPLOYEE.Show()
        frmLUP_EMPLOYEE.Activate()
    End Sub

    'VANS
    Private Sub VansToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VansToolStripMenuItem.Click
        My.Forms.frmLUP_VAN.MdiParent = Me
        My.Forms.frmLUP_VAN.Show()
        My.Forms.frmLUP_VAN.Activate()
    End Sub

    'Modify Entries
    Private Sub ItemsProductDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemsProductDetailToolStripMenuItem.Click
        My.Forms.frmUPDATE_ITEM.MdiParent = Me
        My.Forms.frmUPDATE_ITEM.Show()
        My.Forms.frmUPDATE_ITEM.Activate()
    End Sub
#End Region

#Region "PURCHASE MENU EVENTS"
    Private Sub PurchaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseToolStripMenuItem1.Click
        frmPURCHASE.MdiParent = Me
        frmPURCHASE.Show()
        frmPURCHASE.Activate()
    End Sub
    Private Sub PurchaseReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReturnToolStripMenuItem.Click

    End Sub
#End Region

#Region "SALE MENU EVENTS"
    'Counter Sale
    Private Sub SaleToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleToolStripMenuItem1.Click
        frmSALE.MdiParent = Me
        frmSALE.Show()
        frmSALE.Activate()
    End Sub
    'Mobile Sale (Load Pass)
    Private Sub MobileSaleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MobileSaleToolStripMenuItem.Click
        frmLOADPASS.MdiParent = Me
        frmLOADPASS.Show()
        frmLOADPASS.Activate()
    End Sub
    'Load Pass Billing
    Private Sub LoadPassBillingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadPassBillingToolStripMenuItem.Click
        frmLOADPASS_BILLING.MdiParent = Me
        frmLOADPASS_BILLING.Show()
        frmLOADPASS_BILLING.Activate()
    End Sub

    'Sales Return
    Private Sub SaleReturnToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleReturnToolStripMenuItem.Click
        My.Forms.frmSALE_RET_OPTION.MdiParent = Me
        My.Forms.frmSALE_RET_OPTION.Show()
        My.Forms.frmSALE_RET_OPTION.Activate()
        My.Forms.frmSALE_RET_OPTION.WindowState = FormWindowState.Normal
    End Sub
#End Region

#Region "PAYMENTS MENU EVENTS"
    Private Sub SupplierPaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierPaymentToolStripMenuItem.Click
        frmSUPPLIER_PAYMENT.MdiParent = Me
        frmSUPPLIER_PAYMENT.Show()
        frmSUPPLIER_PAYMENT.Activate()
    End Sub
#End Region

#Region "EXPENSE MENU EVETNS"
    Private Sub ExpensesDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpensesDetailToolStripMenuItem.Click
        frmEXPENSE_DETAIL.MdiParent = Me
        frmEXPENSE_DETAIL.Show()
        frmEXPENSE_DETAIL.Activate()
    End Sub
#End Region

#Region "RECEIPTS MENU EVENTS"
    Private Sub ClientReceiptsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientReceiptsToolStripMenuItem.Click
        frmCLIENT_RECEIPT.MdiParent = Me
        frmCLIENT_RECEIPT.Show()
        frmCLIENT_RECEIPT.Activate()
    End Sub
    Private Sub ClientsRecoveryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientsRecoveryToolStripMenuItem.Click
        frmRECOVERY.MdiParent = Me
        frmRECOVERY.Show()
        frmRECOVERY.Activate()
    End Sub

    Private Sub SupplierReceiptsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierReceiptsToolStripMenuItem.Click
        frmSUPPLIER_RECEIPT.MdiParent = Me
        frmSUPPLIER_RECEIPT.Show()
        frmSUPPLIER_RECEIPT.Activate()
    End Sub
#End Region

#Region "BANK ACCOUNT MENU EVENTS"
    Private Sub BankDepositToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankDepositToolStripMenuItem.Click
        frmBANK_DEPOSIT.MdiParent = Me
        frmBANK_DEPOSIT.Show()
        frmBANK_DEPOSIT.Activate()
    End Sub
    Private Sub BankWithdrawalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankWithdrawalToolStripMenuItem.Click
        frmBANK_WITHDRAWAL.MdiParent = Me
        frmBANK_WITHDRAWAL.Show()
        frmBANK_WITHDRAWAL.Activate()
    End Sub
#End Region

#Region "FRENCHISE MENU EVENTS"
    Private Sub AgreementFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgreementFormToolStripMenuItem.Click
        frmCUST_AGREEMENT.MdiParent = Me
        frmCUST_AGREEMENT.Show()
        frmCUST_AGREEMENT.Activate()
    End Sub
    Private Sub ServiceFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ServiceFormToolStripMenuItem.Click
        frmCUST_SERVICES.MdiParent = Me
        frmCUST_SERVICES.Show()
        frmCUST_SERVICES.Activate()
    End Sub
    Private Sub PortRequestFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PortRequestFormToolStripMenuItem.Click
        frmPORT_REQUEST.MdiParent = Me
        frmPORT_REQUEST.Show()
        frmPORT_REQUEST.Activate()
    End Sub
#End Region

#Region "VIEW MENU EVENTS"
    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub
    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub
#End Region

#Region "TOOL MENU EVENTS"
    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultSettingToolStripMenuItem.Click
        My.Forms.frmDEFAULT_SETTING.MdiParent = Me
        My.Forms.frmDEFAULT_SETTING.Show()
        My.Forms.frmDEFAULT_SETTING.Activate()
    End Sub
#End Region

#Region "HELP / ABOUT MENU EVENTS"
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Me.DialogResult = AboutBox_NeuroBS.ShowDialog
    End Sub
#End Region

    Private Sub TestingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestingToolStripMenuItem.Click
        frmTESTING.MdiParent = Me
        frmTESTING.Show()
        frmTESTING.Activate()
    End Sub

#Region "REPORTS MENU EVENTS"
#Region "BANK REPORTS"
    'Ledger
    Private Sub BankLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankLedgerToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "BANK_LEDGER_ACC_WISE"
        frm.pnlBANK_ACC.Visible = True
        frm.TxtDateFrom7.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo7.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "BANK LEDGER / BANK Account Wise"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

    'Deposit History
    Private Sub BankDepositHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankDepositHistoryToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "BANK_DEPOSIT_HISTORY"
        frm.PnlBankAcc_Date.Visible = True
        frm.Text = "BANK DEPOSIT HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub
    'Withdrawal History
    Private Sub WithdrawalHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithdrawalHistoryToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "BANK_WITHDRAWAL_HISTORY"
        frm.PnlBankAcc_Date.Visible = True
        frm.Text = "BANK WITHDRAWAL HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub
    'Adjustment History
    Private Sub AdjustmentHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdjustmentHistoryToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "BANK_ADJUSTMENT_HISTORY"
        frm.PnlBankAcc_Date.Visible = True
        frm.Text = "BANK ADJUSTMENT HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

#End Region

#Region "CASH REPORTS"
    'Ledger
    Private Sub CashLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashLedgerToolStripMenuItem.Click
        'Dim Rpt As New rptCASH_LEDGER
        'Dim Frm As New frmRPT
        'Try
        '    Frm.CRV.ReportSource = Rpt
        '    Frm.Text = "CASH LEDGER"
        '    Frm.MdiParent = Me
        '    Frm.Show()
        '    Frm.Activate()
        'Catch Ex As Exception
        '    MsgBox(Ex.Message)
        'End Try
        My.Forms.frmRptCASH_LEDGER.MdiParent = Me
        My.Forms.frmRptCASH_LEDGER.Show()
        My.Forms.frmRptCASH_LEDGER.Activate()
        My.Forms.frmRptCASH_LEDGER.WindowState = FormWindowState.Normal
    End Sub

    'Cash Payment History
    Private Sub CashPaymentHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashPaymentHistoryToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "CASH_PAYMENT_HISTORY"
        frm.PnlDate.Visible = True
        frm.TxtDateFrom3.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo3.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "CASH PAYMENT HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub
    'Cash Receipt History
    Private Sub CashReceiptHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashReceiptHistoryToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "CASH_RECEIPT_HISTORY"
        frm.PnlDate.Visible = True
        frm.TxtDateFrom3.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo3.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "CASH RECEIPT HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

    'Receipts History
    Private Sub ReceiptHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceiptHistoryToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "RECEIPT_HISTORY"
        frm.PnlDate.Visible = True
        frm.TxtDateFrom3.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo3.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "RECEIPT HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub
    'Recovery (Ugrai) History
    Private Sub RecoveryUgraiHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoveryUgraiHistoryToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "RECOVERY_HISTORY"
        frm.PnlDate.Visible = True
        frm.TxtDateFrom3.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo3.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "RECOVERY HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

    'Cash Challan
    Private Sub CashChallanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashChallanToolStripMenuItem.Click
        My.Forms.frmRptCASH_CHALLAN.MdiParent = Me
        My.Forms.frmRptCASH_CHALLAN.Show()
        My.Forms.frmRptCASH_CHALLAN.Activate()
        My.Forms.frmRptCASH_CHALLAN.WindowState = FormWindowState.Normal
    End Sub

#End Region

#Region "SUPPLIER REPORTS"
    'Ledger
    Private Sub LedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LedgerToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "SUPPLIER_LEDGER_GP_WISE"
        frm.pnlSUPPLIER.Visible = True
        frm.TxtDateFrom.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "SUPPLIER LEDGER / GROUP Wise"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub
    'Credit Report
    Private Sub CreditReportToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreditReportToolStripMenuItem1.Click
        Dim Rpt As New rptSUPPLIER_CREDIT_REPORT
        Dim Frm As New frmRPT
        Try
            Dim Record As Double = Me.asMAX.LoadValue(Rd, "SELECT COUNT(nID) FROM SUPPLIER_INFO")
            If Record > 0 Then
                Frm.CRV.ReportSource = Rpt
                Frm.Text = "SUPPLIER CREDIT REPORT"
                Frm.MdiParent = Me
                Frm.Show()
                Frm.Activate()

            ElseIf Record <= 0 Then
                MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Null!")

            End If

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    'Active Suppliers List
    Private Sub ActiveSuppliersListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActiveSuppliersListToolStripMenuItem.Click
        Dim Rpt As New rptSUPPLIER_LIST_ACTIVE
        Dim Frm As New frmRPT
        Try
            Dim Record As Double = Me.asMAX.LoadValue(Rd, "SELECT COUNT(nID) FROM SUPPLIER_INFO WHERE sSTATUS='1'")
            If Record > 0 Then
                Frm.CRV.ReportSource = Rpt
                Frm.Text = "ACTIVE SUPPLIERS LIST"
                Frm.MdiParent = Me
                Frm.Show()
                Frm.Activate()

            ElseIf Record <= 0 Then
                MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Null!")

            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    'Non-Active Suppliers List
    Private Sub NonActiveSupplierListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NonActiveSuppliersListToolStripMenuItem.Click
        Dim Rpt As New rptSUPPLIER_LIST_NON_ACTIVE
        Dim Frm As New frmRPT
        Try
            Dim Record As Double = Me.asMAX.LoadValue(Rd, "SELECT COUNT(nID) FROM SUPPLIER_INFO WHERE sSTATUS='0'")
            If Record > 0 Then
                Frm.CRV.ReportSource = Rpt
                Frm.Text = "Non-ACTIVE SUPPLIER LIST"
                Frm.MdiParent = Me
                Frm.Show()
                Frm.Activate()

            ElseIf Record <= 0 Then
                MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Null!")

            End If

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    'Payment History
    Private Sub PaymentsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentsToolStripMenuItem1.Click
        Dim frm As New frmRPT1
        frm.STATUS = "SUPPLIER_PAYMENT_HISTORY"
        frm.PnlSupplierDate.Visible = True
        frm.TxtDateFrom8.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo8.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "SUPPLIER PAYMENT HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub
    'Receipt History
    Private Sub ReceiptsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceiptsToolStripMenuItem1.Click
        Dim frm As New frmRPT1
        frm.STATUS = "SUPPLIER_RECEIPTS_HISTORY"
        frm.PnlSupplierDate.Visible = True
        frm.TxtDateFrom8.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo8.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "SUPPLIER RECEIPTS HISTORY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

    'Purchase Summary
    Private Sub PurchaseSummaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseSummaryToolStripMenuItem1.Click
        My.Forms.frmRptPURCHASE_SUMMARY.MdiParent = Me
        My.Forms.frmRptPURCHASE_SUMMARY.Show()
        My.Forms.frmRptPURCHASE_SUMMARY.Activate()
        My.Forms.frmRptPURCHASE_SUMMARY.WindowState = FormWindowState.Normal
    End Sub
#End Region

#Region "CLIENT REPORTS"
    'Ledger
    Private Sub ClientLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientLedgerToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "CLIENT_LEDGER_GP_WISE"
        frm.pnlCLIENT.Visible = True
        frm.TxtDateFrom1.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo1.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "CLIENT LEDGER / GROUP Wise"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

    'Credit Report Complete
    Private Sub CreditReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreditReportToolStripMenuItem.Click
        Dim Rpt As New rptCLIENT_CREDIT_REPORT
        Dim Frm As New frmRPT
        Try
            Dim Record As Double = Me.asMAX.LoadValue(Rd, "SELECT COUNT(nID) FROM CLIENT_INFO")
            If Record > 0 Then
                Frm.CRV.ReportSource = Rpt
                Frm.Text = "CLIENT CREDIT REPORT"
                Frm.MdiParent = Me
                Frm.Show()
                Frm.Activate()

            ElseIf Record <= 0 Then
                MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Null!")

            End If

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub CreditReportAreaWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreditReportAreaWiseToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "CLIENT_CREDIT_REPORT_AREA"
        frm.PnlArea.Visible = True
        frm.Text = "CLIENT CREDIT REPORT AREA WISE"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

    'Active Clients List
    Private Sub ActiveClientListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ActiveClientListToolStripMenuItem.Click
        Dim Rpt As New rptCLIENT_LIST_ACTIVE
        Dim Frm As New frmRPT
        Try
            Dim Record As Double = Me.asMAX.LoadValue(Rd, "SELECT COUNT(nID) FROM CLIENT_INFO WHERE sSTATUS='1'")
            If Record > 0 Then
                Frm.CRV.ReportSource = Rpt
                Frm.Text = "ACTIVE CLIENTS LIST"
                Frm.MdiParent = Me
                Frm.Show()
                Frm.Activate()

            ElseIf Record <= 0 Then
                MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Null!")

            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    'Non-Active Clients List
    Private Sub NonActiveClientsListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NonActiveClientsListToolStripMenuItem.Click
        Dim Rpt As New rptCLIENT_LIST_NON_ACTIVE
        Dim Frm As New frmRPT
        Try
            Dim Record As Double = Me.asMAX.LoadValue(Rd, "SELECT COUNT(nID) FROM CLIENT_INFO WHERE sSTATUS='0'")
            If Record > 0 Then
                Frm.CRV.ReportSource = Rpt
                Frm.Text = "Non-ACTIVE CLIENTS LIST"
                Frm.MdiParent = Me
                Frm.Show()
                Frm.Activate()

            ElseIf Record <= 0 Then
                MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Null!")

            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    'Receipts History
    Private Sub ClientReceiptHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientReceiptHistoryToolStripMenuItem.Click

    End Sub
    
    'Sale Summary
    Private Sub SaleSummaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleSummaryToolStripMenuItem1.Click

    End Sub
#End Region

#Region "SALES MAN REPORTS"
    'Sales Detail
    Private Sub SaleDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleDetailToolStripMenuItem.Click
        My.Forms.frmRptSALESMAN_SALE_DETAIL.MdiParent = Me
        My.Forms.frmRptSALESMAN_SALE_DETAIL.Show()
        My.Forms.frmRptSALESMAN_SALE_DETAIL.Activate()
        My.Forms.frmRptSALESMAN_SALE_DETAIL.WindowState = FormWindowState.Normal
    End Sub

    'Sales Summary
    Private Sub SaleSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleSummaryToolStripMenuItem.Click
        My.Forms.frmRptSALESMAN_SALE_SUMMARY.MdiParent = Me
        My.Forms.frmRptSALESMAN_SALE_SUMMARY.Show()
        My.Forms.frmRptSALESMAN_SALE_SUMMARY.Activate()
        My.Forms.frmRptSALESMAN_SALE_SUMMARY.WindowState = FormWindowState.Normal
    End Sub

    'Recovery Detail
    Private Sub RecoveryDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoveryDetailToolStripMenuItem.Click
        My.Forms.frmRptSALESMAN_RECOVERY_DETAIL.MdiParent = Me
        My.Forms.frmRptSALESMAN_RECOVERY_DETAIL.Show()
        My.Forms.frmRptSALESMAN_RECOVERY_DETAIL.Activate()
        My.Forms.frmRptSALESMAN_RECOVERY_DETAIL.WindowState = FormWindowState.Normal
    End Sub

    'Recovery Summary
    Private Sub RecoverySummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoverySummaryToolStripMenuItem.Click
        My.Forms.frmRptSALESMAN_RECOVERY_SUMMARY.MdiParent = Me
        My.Forms.frmRptSALESMAN_RECOVERY_SUMMARY.Show()
        My.Forms.frmRptSALESMAN_RECOVERY_SUMMARY.Activate()
        My.Forms.frmRptSALESMAN_RECOVERY_SUMMARY.WindowState = FormWindowState.Normal
    End Sub
#End Region

#Region "ITEMS REPORTS"
    'Order Forms
    Private Sub OrderFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrderFormToolStripMenuItem.Click
        frmRptORDER_FORM.MdiParent = Me
        frmRptORDER_FORM.Show()
        frmRptORDER_FORM.Activate()
    End Sub
#End Region

#Region "STOCK REPORTS"
    'Ledger
    Private Sub StockLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockLedgerToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "STOCK_LEDGER"
        frm.pnlITEM.Visible = True
        frm.TxtDateFrom5.Text = CDate("01-06-" & Date.Now.Year).ToString("dd-MMM-yyyy")
        frm.TxtDateTo5.Text = Date.Now.ToString("dd-MMM-yyyy")
        frm.Text = "STOCK LEDGER"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

    Private Sub StockSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockReportToolStripMenuItem.Click

    End Sub
    Private Sub StockSaleSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockSaleSummaryToolStripMenuItem.Click
        Me.SaleStockSummaryToolStripMenuItem_Click(sender, e)
    End Sub

    'Stock in Hand
    Private Sub StockInHandToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockInHandToolStripMenuItem.Click
        My.Forms.frmRptSTOCK_HAND.MdiParent = Me
        My.Forms.frmRptSTOCK_HAND.Show()
        My.Forms.frmRptSTOCK_HAND.Activate()
        My.Forms.frmRptSTOCK_HAND.WindowState = FormWindowState.Normal
    End Sub
#End Region

#Region "PURCHASE REPORTS"

#End Region

#Region "SALES REPORTS"
    'Sales Summary
    Private Sub SalesSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesSummaryToolStripMenuItem.Click
        Dim frm As New frmRPT1
        frm.STATUS = "SALES_SUMMARY"
        frm.PnlCompany_Group.Visible = True
        frm.Text = "SALES SUMMARY"
        frm.MdiParent = Me
        frm.Show()
        frm.Activate()
    End Sub

    'Sale Stock Summary
    Private Sub SaleStockSummaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleStockSummaryToolStripMenuItem.Click
        frmRptSALES_STOCK_SUMMARY.MdiParent = Me
        frmRptSALES_STOCK_SUMMARY.Show()
        frmRptSALES_STOCK_SUMMARY.Activate()
    End Sub

    'Sale Invoice
    Private Sub SalesInvoiceToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesInvoiceToolStripMenuItem1.Click
        frmRptSALE_INVOICE.MdiParent = Me
        frmRptSALE_INVOICE.Show()
        frmRptSALE_INVOICE.Activate()
    End Sub

    'Track Item
    Private Sub TrackItemsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackItemsToolStripMenuItem.Click

    End Sub
#End Region

#Region "EMPLOYEE REPORTS"

#End Region

#Region "EXPENSE REPORTS"

#End Region

#End Region

#Region "SECURITY MENU EVENTS"

    'Change Password
    Private Sub ChangePasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        frmCHANGE_PASSWORD.MdiParent = Me
        frmCHANGE_PASSWORD.Show()
        frmCHANGE_PASSWORD.Activate()
    End Sub
#End Region

#Region "TOOLBAR"
    'Sale
    Private Sub BttnSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSales.Click
        Me.SaleToolStripMenuItem1_Click(sender, e)
    End Sub

    'Cash Challan
    Private Sub BttnCashChallan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnCashChallan.Click
        Me.CashChallanToolStripMenuItem_Click(sender, e)
    End Sub

    'Client Ledger
    Private Sub BttnClientLedger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClientLedger.Click
        Me.ClientLedgerToolStripMenuItem_Click(sender, e)
    End Sub

    'Client Recovery
    Private Sub BttnRecovery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnRecovery.Click
        Me.ClientsRecoveryToolStripMenuItem_Click(sender, e)
    End Sub

    'Client Credit List
    Private Sub BttnCreditList_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnCreditList.ButtonClick
        Me.CreditReportAreaWiseToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub CompleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompleteToolStripMenuItem.Click
        Me.CreditReportToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub AreaWiseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AreaWiseToolStripMenuItem.Click
        Me.CreditReportAreaWiseToolStripMenuItem_Click(sender, e)
    End Sub

    'Pruchase
    Private Sub BttnPurchase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPurchase.Click
        Me.PurchaseToolStripMenuItem_Click(sender, e)
    End Sub

    'Salesman
    Private Sub BttnSalesman_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSalesman.ButtonClick
        Me.RecoveryDetailToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub RecoveryDetailToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoveryDetailToolStripMenuItem1.Click
        Me.RecoveryDetailToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub RecoverySummaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoverySummaryToolStripMenuItem1.Click
        Me.RecoverySummaryToolStripMenuItem_Click(sender, e)
    End Sub
    
    Private Sub SalesDetailToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesDetailToolStripMenuItem.Click
        Me.SaleDetailToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub SalesSummaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesSummaryToolStripMenuItem1.Click
        Me.SaleSummaryToolStripMenuItem_Click(sender, e)
    End Sub

#End Region

#Region "STATUS TOOLBAR"
    'STATUS LABEL
    Private Sub MenuItems_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.MouseHover, AccountsSummaryToolStripMenuItem.MouseHover, ActiveClientListToolStripMenuItem.MouseHover, ActiveItemsListToolStripMenuItem.MouseHover, ActiveSuppliersListToolStripMenuItem.MouseHover, AdjustmentHistoryToolStripMenuItem.MouseHover, AdjustmentHistoryToolStripMenuItem1.MouseHover, AgreementFormToolStripMenuItem.MouseHover, AreaLocationToolStripMenuItem.MouseHover, AreaWiseToolStripMenuItem.MouseHover, ArrangeIconsToolStripMenuItem.MouseHover, BankAccountsToolStripMenuItem.MouseHover, BankAccountToolStripMenuItem.MouseHover, BankDepositHistoryToolStripMenuItem.MouseHover, BankDepositToolStripMenuItem.MouseHover, BankDepositToolStripMenuItem1.MouseHover, BankDetailToolStripMenuItem.MouseHover, BankLedgerToolStripMenuItem.MouseHover, BankLedgerToolStripMenuItem1.MouseHover, BankToolStripMenuItem.MouseHover, BankWithdrawalToolStripMenuItem.MouseHover, BankWithdrawalToolStripMenuItem1.MouseHover, BttnCashChallan.MouseHover, BttnClientLedger.MouseHover, BttnCreditList.MouseHover, BttnPurchase.MouseHover, BttnPurchase.MouseHover, BttnRecovery.MouseHover, BttnSales.MouseHover, BusinessGroupsToolStripMenuItem.MouseHover, BusinessInformationToolStripMenuItem.MouseHover, CascadeToolStripMenuItem.MouseHover, CashChallanToolStripMenuItem.MouseHover, CashChallanToolStripMenuItem1.MouseHover, CashHistoryToolStripMenuItem.MouseHover, CashLedgerToolStripMenuItem.MouseHover, CashLedgerToolStripMenuItem1.MouseHover, CashPaymentHistoryToolStripMenuItem.MouseHover, CashReceiptHistoryToolStripMenuItem.MouseHover, CashToolStripMenuItem.MouseHover, ChangePasswordToolStripMenuItem.MouseHover, ChangePermissionsToolStripMenuItem.MouseHover, ChequesBouncedDetailToolStripMenuItem.MouseHover, ClientCreditReportToolStripMenuItem.MouseHover, ClientGradesToolStripMenuItem.MouseHover, ClientInformationToolStripMenuItem.MouseHover, ClientLedgerToolStripMenuItem.MouseHover, ClientLedgerToolStripMenuItem1.MouseHover, ClientOpeningBalancesToolStripMenuItem.MouseHover, ClientReceiptHistoryToolStripMenuItem.MouseHover, ClientReceiptsToolStripMenuItem.MouseHover, ClientsRecoveryToolStripMenuItem.MouseHover, ClientToolStripMenuItem.MouseHover, ClientToolStripMenuItem1.MouseHover, ClientTypeToolStripMenuItem.MouseHover, CloseAllToolStripMenuItem1.MouseHover, CloseToolStripMenuItem.MouseHover, CodeToolStripMenuItem.MouseHover, CompaniesListToolStripMenuItem.MouseHover, CompleteToolStripMenuItem.MouseHover, ContentsToolStripMenuItem.MouseHover, CreditReportAreaWiseToolStripMenuItem.MouseHover, CreditReportToolStripMenuItem.MouseHover, CreditReportToolStripMenuItem1.MouseHover, DailyExpenseToolStripMenuItem.MouseHover, DefaultSettingToolStripMenuItem.MouseHover, DeleteUserToolStripMenuItem.MouseHover, DepositHistoryToolStripMenuItem.MouseHover, EmployeeDesignationToolStripMenuItem.MouseHover, EmployeeInformationToolStripMenuItem.MouseHover, EmployeesListToolStripMenuItem.MouseHover, EmployeeToolStripMenuItem.MouseHover, EmployeeToolStripMenuItem1.MouseHover, ExitToolStripMenuItem.MouseHover, ExpenseDetailsToolStripMenuItem.MouseHover, ExpenseHeadsToolStripMenuItem.MouseHover, ExpenseHistoryToolStripMenuItem.MouseHover, ExpensesDetailToolStripMenuItem.MouseHover, ExpensesToolStripMenuItem.MouseHover, ExpensesToolStripMenuItem1.MouseHover, ExpenseSubHeadsToolStripMenuItem.MouseHover, ExpenseSummaryToolStripMenuItem.MouseHover, ExpenseToolStripMenuItem.MouseHover, FileMenu.MouseHover, FrenchiseToolStripMenuItem.MouseHover, HelpMenu.MouseHover, IndexToolStripMenuItem.MouseHover, InvoiceReportToolStripMenuItem.MouseHover, InvoiceReturnsToolStripMenuItem.MouseHover, PurchaseSummaryToolStripMenuItem1.MouseHover, ItemProductCategoriesToolStripMenuItem.MouseHover, ItemsToolStripMenuItem.MouseHover, ItemsToolStripMenuItem1.MouseHover, ItemToolStripMenuItem.MouseHover, LedgersToolStripMenuItem.MouseHover, LedgerToolStripMenuItem.MouseHover, LoadPassBillingToolStripMenuItem.MouseHover, MobileSaleToolStripMenuItem.MouseHover, NewBankAccountToolStripMenuItem.MouseHover, NewUserToolStripMenuItem.MouseHover, NonActiveClientsListToolStripMenuItem.MouseHover, NonActiveItemsListToolStripMenuItem.MouseHover, NonActiveSuppliersListToolStripMenuItem.MouseHover, OpeningStockAdjustmentToolStripMenuItem.MouseHover, OpeningStockToolStripMenuItem.MouseHover, OrderFormToolStripMenuItem.MouseHover, PaymentHistoryToolStripMenuItem.MouseHover, PaymentsToolStripMenuItem.MouseHover, PaymentsToolStripMenuItem1.MouseHover, PLGroupWiseToolStripMenuItem.MouseHover, PLInvoiceWiseToolStripMenuItem.MouseHover, PLOverallToolStripMenuItem.MouseHover, PLToolStripMenuItem.MouseHover, PortRequestFormToolStripMenuItem.MouseHover, PruchaseSummaryToolStripMenuItem.MouseHover, PurchaseInvoiceToolStripMenuItem.MouseHover, PurchaseReturnToolStripMenuItem.MouseHover, PurchaseReturnToolStripMenuItem1.MouseHover, PurchaseToolStripMenuItem.MouseHover, PurchaseToolStripMenuItem1.MouseHover, PurchaseToolStripMenuItem2.MouseHover, ReceiptsHistoryToolStripMenuItem.MouseHover, ReceiptsToolStripMenuItem.MouseHover, ReceiptsToolStripMenuItem1.MouseHover, RecoveryUgraiHistoryToolStripMenuItem.MouseHover, RecoveryUgraiToolStripMenuItem.MouseHover, ReportsToolStripMenuItem.MouseHover, RoutesToolStripMenuItem.MouseHover, SaleInvoiceToolStripMenuItem.MouseHover, SaleReturnToolStripMenuItem.MouseHover, SaleReturnToolStripMenuItem1.MouseHover, SalesInvoiceToolStripMenuItem1.MouseHover, SalesSummaryToolStripMenuItem.MouseHover, SaleStockSummaryToolStripMenuItem.MouseHover, SalesToolStripMenuItem.MouseHover, SaleToolStripMenuItem.MouseHover, SaleToolStripMenuItem1.MouseHover, SchemeItemsToolStripMenuItem.MouseHover, SchemeItemsToolStripMenuItem1.MouseHover, SearchToolStripMenuItem.MouseHover, SecurityToolStripMenuItem.MouseHover, ServiceFormToolStripMenuItem.MouseHover, ShopCatagoriesToolStripMenuItem.MouseHover, StockLedgerToolStripMenuItem.MouseHover, StockLedgerToolStripMenuItem1.MouseHover, StockReportToolStripMenuItem.MouseHover, StockSaleSummaryToolStripMenuItem.MouseHover, StockToolStripMenuItem.MouseHover, SupplierCreditReportToolStripMenuItem.MouseHover, SupplierInformationToolStripMenuItem.MouseHover, SupplierLedgerToolStripMenuItem.MouseHover, SupplierOpeningBalancesToolStripMenuItem.MouseHover, SupplierPaymentToolStripMenuItem.MouseHover, SupplierPaymentToolStripMenuItem1.MouseHover, SupplierReceiptsToolStripMenuItem.MouseHover, SupplierReciptToolStripMenuItem.MouseHover, SupplierToolStripMenuItem.MouseHover, SupplierToolStripMenuItem1.MouseHover, TileHorizontalToolStripMenuItem.MouseHover, TileVerticalToolStripMenuItem.MouseHover, TrackItemsToolStripMenuItem.MouseHover, TrackItemToolStripMenuItem.MouseHover, VansToolStripMenuItem.MouseHover, VendorsToolStripMenuItem.MouseHover, ViewMenu.MouseHover, WithdrawalHistoryToolStripMenuItem.MouseHover, WithdrawalHistoryToolStripMenuItem1.MouseHover, ZoneStationToolStripMenuItem.MouseHover, ToolsMenu.MouseHover, PurchaseHistoryToolStripMenuItem.MouseHover, ClientReceiptsToolStripMenuItem1.MouseHover, SalesManToolStripMenuItem.MouseHover, SalesmanToolStripMenuItem1.MouseHover, SaleDetailToolStripMenuItem.MouseHover, SaleSummaryToolStripMenuItem.MouseHover, RecoveryDetailToolStripMenuItem.MouseHover, RecoveryDetailToolStripMenuItem1.MouseHover, RecoveryDetailToolStripMenuItem2.MouseHover, RecoverySummaryToolStripMenuItem.MouseHover, SalesDetailToolStripMenuItem.MouseHover, SalesDetailToolStripMenuItem1.MouseHover, BttnSalesman.MouseHover, SalesSummaryToolStripMenuItem2.MouseHover, RecoverySummaryToolStripMenuItem2.MouseHover, RecoverySummaryToolStripMenuItem1.MouseHover, SalesSummaryToolStripMenuItem1.MouseHover, ReceiptHistoryToolStripMenuItem.MouseHover, SaleSummaryToolStripMenuItem1.MouseHover
        'Dim Ctrl As Control = sender
        Me.LblStatus.Text = "Status: " & sender.Text
    End Sub
    Private Sub MenuItems_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.MouseLeave, AccountsSummaryToolStripMenuItem.MouseLeave, ActiveClientListToolStripMenuItem.MouseLeave, ActiveItemsListToolStripMenuItem.MouseLeave, ActiveSuppliersListToolStripMenuItem.MouseLeave, AdjustmentHistoryToolStripMenuItem.MouseLeave, AdjustmentHistoryToolStripMenuItem1.MouseLeave, AgreementFormToolStripMenuItem.MouseLeave, AreaLocationToolStripMenuItem.MouseLeave, AreaWiseToolStripMenuItem.MouseLeave, ArrangeIconsToolStripMenuItem.MouseLeave, BankAccountsToolStripMenuItem.MouseLeave, BankAccountToolStripMenuItem.MouseLeave, BankDepositHistoryToolStripMenuItem.MouseLeave, BankDepositToolStripMenuItem.MouseLeave, BankDepositToolStripMenuItem1.MouseLeave, BankDetailToolStripMenuItem.MouseLeave, BankLedgerToolStripMenuItem.MouseLeave, BankLedgerToolStripMenuItem1.MouseLeave, BankToolStripMenuItem.MouseLeave, BankWithdrawalToolStripMenuItem.MouseLeave, BankWithdrawalToolStripMenuItem1.MouseLeave, BttnCashChallan.MouseLeave, BttnClientLedger.MouseLeave, BttnCreditList.MouseLeave, BttnPurchase.MouseLeave, BttnPurchase.MouseLeave, BttnRecovery.MouseLeave, BttnSales.MouseLeave, BusinessGroupsToolStripMenuItem.MouseLeave, BusinessInformationToolStripMenuItem.MouseLeave, CascadeToolStripMenuItem.MouseLeave, CashChallanToolStripMenuItem.MouseLeave, CashChallanToolStripMenuItem1.MouseLeave, CashHistoryToolStripMenuItem.MouseLeave, CashLedgerToolStripMenuItem.MouseLeave, CashLedgerToolStripMenuItem1.MouseLeave, CashPaymentHistoryToolStripMenuItem.MouseLeave, CashReceiptHistoryToolStripMenuItem.MouseLeave, CashToolStripMenuItem.MouseLeave, ChangePasswordToolStripMenuItem.MouseLeave, ChangePermissionsToolStripMenuItem.MouseLeave, ChequesBouncedDetailToolStripMenuItem.MouseLeave, ClientCreditReportToolStripMenuItem.MouseLeave, ClientGradesToolStripMenuItem.MouseLeave, ClientInformationToolStripMenuItem.MouseLeave, ClientLedgerToolStripMenuItem.MouseLeave, ClientLedgerToolStripMenuItem1.MouseLeave, ClientOpeningBalancesToolStripMenuItem.MouseLeave, ClientReceiptHistoryToolStripMenuItem.MouseLeave, ClientReceiptsToolStripMenuItem.MouseLeave, ClientsRecoveryToolStripMenuItem.MouseLeave, ClientToolStripMenuItem.MouseLeave, ClientToolStripMenuItem1.MouseLeave, ClientTypeToolStripMenuItem.MouseLeave, CloseAllToolStripMenuItem1.MouseLeave, CloseToolStripMenuItem.MouseLeave, CodeToolStripMenuItem.MouseLeave, CompaniesListToolStripMenuItem.MouseLeave, CompleteToolStripMenuItem.MouseLeave, ContentsToolStripMenuItem.MouseLeave, CreditReportAreaWiseToolStripMenuItem.MouseLeave, CreditReportToolStripMenuItem.MouseLeave, CreditReportToolStripMenuItem1.MouseLeave, DailyExpenseToolStripMenuItem.MouseLeave, DefaultSettingToolStripMenuItem.MouseLeave, DeleteUserToolStripMenuItem.MouseLeave, DepositHistoryToolStripMenuItem.MouseLeave, EmployeeDesignationToolStripMenuItem.MouseLeave, EmployeeInformationToolStripMenuItem.MouseLeave, EmployeesListToolStripMenuItem.MouseLeave, EmployeeToolStripMenuItem.MouseLeave, EmployeeToolStripMenuItem1.MouseLeave, ExitToolStripMenuItem.MouseLeave, ExpenseDetailsToolStripMenuItem.MouseLeave, ExpenseHeadsToolStripMenuItem.MouseLeave, ExpenseHistoryToolStripMenuItem.MouseLeave, ExpensesDetailToolStripMenuItem.MouseLeave, ExpensesToolStripMenuItem.MouseLeave, ExpensesToolStripMenuItem1.MouseLeave, ExpenseSubHeadsToolStripMenuItem.MouseLeave, ExpenseSummaryToolStripMenuItem.MouseLeave, ExpenseToolStripMenuItem.MouseLeave, FileMenu.MouseLeave, FrenchiseToolStripMenuItem.MouseLeave, HelpMenu.MouseLeave, IndexToolStripMenuItem.MouseLeave, InvoiceReportToolStripMenuItem.MouseLeave, InvoiceReturnsToolStripMenuItem.MouseLeave, PurchaseSummaryToolStripMenuItem1.MouseLeave, ItemProductCategoriesToolStripMenuItem.MouseLeave, ItemsToolStripMenuItem.MouseLeave, ItemsToolStripMenuItem1.MouseLeave, ItemToolStripMenuItem.MouseLeave, LedgersToolStripMenuItem.MouseLeave, LedgerToolStripMenuItem.MouseLeave, LoadPassBillingToolStripMenuItem.MouseLeave, MobileSaleToolStripMenuItem.MouseLeave, NewBankAccountToolStripMenuItem.MouseLeave, NewUserToolStripMenuItem.MouseLeave, NonActiveClientsListToolStripMenuItem.MouseLeave, NonActiveItemsListToolStripMenuItem.MouseLeave, NonActiveSuppliersListToolStripMenuItem.MouseLeave, OpeningStockAdjustmentToolStripMenuItem.MouseLeave, OpeningStockToolStripMenuItem.MouseLeave, OrderFormToolStripMenuItem.MouseLeave, PaymentHistoryToolStripMenuItem.MouseLeave, PaymentsToolStripMenuItem.MouseLeave, PaymentsToolStripMenuItem1.MouseLeave, PLGroupWiseToolStripMenuItem.MouseLeave, PLInvoiceWiseToolStripMenuItem.MouseLeave, PLOverallToolStripMenuItem.MouseLeave, PLToolStripMenuItem.MouseLeave, PortRequestFormToolStripMenuItem.MouseLeave, PruchaseSummaryToolStripMenuItem.MouseLeave, PurchaseInvoiceToolStripMenuItem.MouseLeave, PurchaseReturnToolStripMenuItem.MouseLeave, PurchaseReturnToolStripMenuItem1.MouseLeave, PurchaseToolStripMenuItem.MouseLeave, PurchaseToolStripMenuItem1.MouseLeave, PurchaseToolStripMenuItem2.MouseLeave, ReceiptsHistoryToolStripMenuItem.MouseLeave, ReceiptsToolStripMenuItem.MouseLeave, ReceiptsToolStripMenuItem1.MouseLeave, RecoveryUgraiHistoryToolStripMenuItem.MouseLeave, RecoveryUgraiToolStripMenuItem.MouseLeave, ReportsToolStripMenuItem.MouseLeave, RoutesToolStripMenuItem.MouseLeave, SaleInvoiceToolStripMenuItem.MouseLeave, SaleReturnToolStripMenuItem.MouseLeave, SaleReturnToolStripMenuItem1.MouseLeave, SalesInvoiceToolStripMenuItem1.MouseLeave, SalesSummaryToolStripMenuItem.MouseLeave, SaleStockSummaryToolStripMenuItem.MouseLeave, SalesToolStripMenuItem.MouseLeave, SaleToolStripMenuItem.MouseLeave, SaleToolStripMenuItem1.MouseLeave, SchemeItemsToolStripMenuItem.MouseLeave, SchemeItemsToolStripMenuItem1.MouseLeave, SearchToolStripMenuItem.MouseLeave, SecurityToolStripMenuItem.MouseLeave, ServiceFormToolStripMenuItem.MouseLeave, ShopCatagoriesToolStripMenuItem.MouseLeave, StockLedgerToolStripMenuItem.MouseLeave, StockLedgerToolStripMenuItem1.MouseLeave, StockReportToolStripMenuItem.MouseLeave, StockSaleSummaryToolStripMenuItem.MouseLeave, StockToolStripMenuItem.MouseLeave, SupplierCreditReportToolStripMenuItem.MouseLeave, SupplierInformationToolStripMenuItem.MouseLeave, SupplierLedgerToolStripMenuItem.MouseLeave, SupplierOpeningBalancesToolStripMenuItem.MouseLeave, SupplierPaymentToolStripMenuItem.MouseLeave, SupplierPaymentToolStripMenuItem1.MouseLeave, SupplierReceiptsToolStripMenuItem.MouseLeave, SupplierReciptToolStripMenuItem.MouseLeave, SupplierToolStripMenuItem.MouseLeave, SupplierToolStripMenuItem1.MouseLeave, TileHorizontalToolStripMenuItem.MouseLeave, TileVerticalToolStripMenuItem.MouseLeave, TrackItemsToolStripMenuItem.MouseLeave, TrackItemToolStripMenuItem.MouseLeave, VansToolStripMenuItem.MouseLeave, VendorsToolStripMenuItem.MouseLeave, ViewMenu.MouseLeave, WithdrawalHistoryToolStripMenuItem.MouseLeave, WithdrawalHistoryToolStripMenuItem1.MouseLeave, ZoneStationToolStripMenuItem.MouseLeave, ToolsMenu.MouseLeave, PurchaseHistoryToolStripMenuItem.MouseLeave, ClientReceiptsToolStripMenuItem1.MouseLeave, SalesManToolStripMenuItem.MouseLeave, SalesmanToolStripMenuItem1.MouseLeave, SaleDetailToolStripMenuItem.MouseLeave, SaleSummaryToolStripMenuItem.MouseLeave, RecoveryDetailToolStripMenuItem.MouseLeave, RecoveryDetailToolStripMenuItem1.MouseLeave, RecoveryDetailToolStripMenuItem2.MouseLeave, RecoverySummaryToolStripMenuItem.MouseLeave, SalesDetailToolStripMenuItem.MouseLeave, SalesDetailToolStripMenuItem1.MouseLeave, BttnSalesman.MouseLeave, SalesSummaryToolStripMenuItem2.MouseLeave, RecoverySummaryToolStripMenuItem2.MouseLeave, RecoverySummaryToolStripMenuItem1.MouseLeave, SalesSummaryToolStripMenuItem1.MouseLeave, ReceiptHistoryToolStripMenuItem.MouseLeave, SaleSummaryToolStripMenuItem1.MouseLeave
        Me.LblStatus.Text = "Status: "
    End Sub

    'Sale & Purchase Invoice
    Private Sub SaleInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleInvoiceToolStripMenuItem.Click
        Me.SaleToolStripMenuItem1_Click(sender, e)
    End Sub
    Private Sub PurchaseInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseInvoiceToolStripMenuItem.Click
        Me.PurchaseToolStripMenuItem_Click(sender, e)
    End Sub

    'Sale Return & Purchase Return Invoice
    Private Sub SaleReturnToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaleReturnToolStripMenuItem1.Click
        Me.SaleReturnToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub PurchaseReturnToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseReturnToolStripMenuItem1.Click
        Me.PurchaseReturnToolStripMenuItem_Click(sender, e)
    End Sub

    'Cash Challan
    Private Sub CashChallanToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashChallanToolStripMenuItem1.Click
        Me.CashChallanToolStripMenuItem_Click(sender, e)
    End Sub

    'Cash History
    Private Sub PaymentHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentHistoryToolStripMenuItem.Click
        Me.CashPaymentHistoryToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub ReceiptsHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceiptsHistoryToolStripMenuItem.Click
        Me.CashReceiptHistoryToolStripMenuItem_Click(sender, e)
    End Sub

    'Ledgers
    Private Sub BankLedgerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankLedgerToolStripMenuItem1.Click
        Me.BankLedgerToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub CashLedgerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashLedgerToolStripMenuItem1.Click
        Me.CashLedgerToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub StockLedgerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockLedgerToolStripMenuItem1.Click
        Me.StockLedgerToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub SupplierLedgerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierLedgerToolStripMenuItem.Click
        Me.LedgerToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub ClientLedgerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientLedgerToolStripMenuItem1.Click
        Me.ClientLedgerToolStripMenuItem_Click(sender, e)
    End Sub

    'Payments
    Private Sub SupplierCreditReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierCreditReportToolStripMenuItem.Click
        Me.CreditReportToolStripMenuItem1_Click(sender, e)
    End Sub
    Private Sub SupplierPaymentToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierPaymentToolStripMenuItem1.Click
        SupplierPaymentToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub ExpenseDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpenseDetailsToolStripMenuItem.Click
        Me.ExpensesDetailToolStripMenuItem_Click(sender, e)
    End Sub

    'Receipts
    Private Sub ClientCreditReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientCreditReportToolStripMenuItem.Click
        Me.CreditReportAreaWiseToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub ClientReceiptsToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClientReceiptsToolStripMenuItem1.Click
        Me.ClientReceiptsToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub RecoveryUgraiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoveryUgraiToolStripMenuItem.Click
        Me.ClientsRecoveryToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub SupplierReciptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierReciptToolStripMenuItem.Click
        Me.SupplierReceiptsToolStripMenuItem_Click(sender, e)
    End Sub

    'Bank Details
    Private Sub NewBankAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewBankAccountToolStripMenuItem.Click
        Me.BankAccountsToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub BankDepositToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankDepositToolStripMenuItem1.Click
        Me.BankDepositToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub BankWithdrawalToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankWithdrawalToolStripMenuItem1.Click
        Me.BankWithdrawalToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub DepositHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DepositHistoryToolStripMenuItem.Click
        Me.BankDepositHistoryToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub WithdrawalHistoryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithdrawalHistoryToolStripMenuItem1.Click
        Me.WithdrawalHistoryToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub AdjustmentHistoryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdjustmentHistoryToolStripMenuItem1.Click
        Me.AdjustmentHistoryToolStripMenuItem_Click(sender, e)
    End Sub

    'Salesman
    Private Sub SalesDetailToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesDetailToolStripMenuItem1.Click
        Me.SaleDetailToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub SalesSummaryToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesSummaryToolStripMenuItem2.Click
        Me.SaleSummaryToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub RecoveryDetailToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoveryDetailToolStripMenuItem2.Click
        Me.RecoveryDetailToolStripMenuItem_Click(sender, e)
    End Sub
    Private Sub RecoverySummaryToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoverySummaryToolStripMenuItem2.Click
        Me.RecoverySummaryToolStripMenuItem_Click(sender, e)
    End Sub

#End Region





End Class
