Imports SDS = System.Data.SqlClient
Imports CrystalDecisions.Shared
Public Class frmRPT1

#Region "Variable Declaration Section"
    Dim asConn As New AssConn
    Public STATUS As String
    Dim asNUM As New AssNumPress
#End Region

#Region "Form Events Control"
    Private Sub frmRPT1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        If Me.STATUS = "BANK_LEDGER_ACC_WISE" Then
            Me.FillComboBox_BankAccount()

        ElseIf Me.STATUS = "SUPPLIER_LEDGER_GP_WISE" Then
            Me.FillComboBox_Supplier()
            Me.FillComboBox_Group()

        ElseIf Me.STATUS = "CLIENT_LEDGER_GP_WISE" Then
            Me.FillComboBox_Client()
            'Me.FillComboBox_Group()

        ElseIf Me.STATUS = "SALES_SUMMARY" Then
            Me.FillComboBox_Group()
            Me.FillComboBox_Company()

        ElseIf Me.STATUS = "BANK_DEPOSIT_HISTORY" Or Me.STATUS = "BANK_WITHDRAWAL_HISTORY" Or Me.STATUS = "BANK_ADJUSTMENT_HISTORY" Then
            Me.FillComboBox_BankAccount()

        ElseIf Me.STATUS = "CLIENT_CREDIT_REPORT_AREA" Or Me.STATUS = "CASH_CHALLAN_AREA" Or Me.STATUS = "SALE_INVOICE_AREA_DATE" Or Me.STATUS = "SALES_STOCK_SUMMARY_AREA_DATE" Then
            Me.FillComboBox_AREA()

        ElseIf Me.STATUS = "STOCK_LEDGER" Then
            Me.FillComboBox_ITEM()

        ElseIf Me.STATUS = "STOCK_HAND_COMPANY_BATCH" Or Me.STATUS = "STOCK_HAND_COMPANY" Then
            Me.FillComboBox_Company()

        ElseIf Me.STATUS = "SALE_DETAIL_SALEMAN" Or Me.STATUS = "SALE_DETAIL_SALEMAN_DATE" Or Me.STATUS = "RECOVERY_DETAIL_SALEMAN" Or Me.STATUS = "RECOVERY_DETAIL_SALEMAN_DATE" Then
            Me.FillComboBox_Employee()

        ElseIf Me.STATUS = "SUPPLIER_PAYMENT_HISTORY" Or Me.STATUS = "SUPPLIER_RECEIPTS_HISTORY" Or Me.STATUS = "PURCHASE_SUMMARY_SUPPLIER" Then
            Me.FillComboBox_Supplier()


        End If

    End Sub
    Private Sub frmRPT1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        asNUM.EnterTab(e)
    End Sub

    Private Sub CRV_ReportRefresh(ByVal source As Object, ByVal e As CrystalDecisions.Windows.Forms.ViewerEventArgs) Handles CRV1.ReportRefresh
        Me.SetDBLogonForReport(myConnectionInfo)
    End Sub
#End Region

#Region "TextBox Events Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDateFrom.GotFocus, TxtDateTo.GotFocus, TxtDateFrom1.GotFocus, TxtDateTo1.GotFocus, TxtDateFrom2.GotFocus, TxtDateTo2.GotFocus, TxtDateFrom3.GotFocus, TxtDateTo3.GotFocus, TxtDateFrom4.GotFocus, TxtDateTo4.GotFocus, TxtInvoiceFrom.GotFocus, TxtInvoiceTo.GotFocus, TxtDateFrom5.GotFocus, TxtDateTo5.GotFocus, TxtDateFrom6.GotFocus, TxtDateTo6.GotFocus, TxtDateFrom7.GotFocus, TxtDateTo7.GotFocus, TxtDateFrom8.GotFocus, TxtDateTo8.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDateFrom.LostFocus, TxtDateTo.LostFocus, TxtDateFrom1.LostFocus, TxtDateTo1.LostFocus, TxtDateFrom2.LostFocus, TxtDateTo2.LostFocus, TxtDateFrom3.LostFocus, TxtDateTo3.LostFocus, TxtDateFrom4.LostFocus, TxtDateTo4.LostFocus, TxtInvoiceFrom.LostFocus, TxtInvoiceTo.LostFocus, TxtDateFrom5.LostFocus, TxtDateTo5.LostFocus, TxtDateFrom6.LostFocus, TxtDateTo6.LostFocus, TxtDateFrom7.LostFocus, TxtDateTo7.LostFocus, TxtDateFrom8.LostFocus, TxtDateTo8.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtDateFrom"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateFrom1"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo1"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateFrom2"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo2"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateFrom3"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo3"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateFrom4"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo4"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateFrom5"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo5"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateFrom6"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo6"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateFrom7"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo7"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateFrom8"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select

                Case "TxtDateTo8"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If
                    Exit Select


            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub

    'Num KeyPress
    Private Sub Txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtInvoiceFrom.KeyPress, TxtInvoiceTo.KeyPress
        Me.asNUM.NumPress(True, e)
    End Sub
#End Region

#Region "ComboBox Controls"
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSupplier.GotFocus, CmbGroup.GotFocus, CmbBankAccount.GotFocus, CmbGroup1.GotFocus, CmbGroup2.GotFocus, CmbCompany.GotFocus, CmbClient.GotFocus, CmbBankAccount1.GotFocus, CmbArea.GotFocus, CmbArea1.GotFocus, cmbITEM.GotFocus, CmbCompany1.GotFocus, CmbS_Man.GotFocus, CmbS_Man1.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbSupplier.LostFocus, CmbGroup.LostFocus, CmbBankAccount.LostFocus, CmbGroup1.LostFocus, CmbGroup2.LostFocus, CmbCompany.LostFocus, CmbClient.LostFocus, CmbBankAccount1.LostFocus, CmbArea.LostFocus, CmbArea1.LostFocus, cmbITEM.LostFocus, CmbCompany1.LostFocus, CmbS_Man.LostFocus, CmbS_Man1.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "Buttons Events Control"
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
    Private Sub VIEW_REPORTButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VIEW_REPORTButton.Click
        Me.Cursor = Cursors.WaitCursor

        If Me.STATUS = "BANK_LEDGER_ACC_WISE" Then
            If Me.CmbBankAccount.Text = Nothing Or Me.CmbBankAccount.SelectedIndex = -1 Or Me.TxtDateFrom7.Text = Nothing Or Me.TxtDateTo7.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.CmbBankAccount.Text = Nothing Or Me.CmbBankAccount.SelectedIndex - 1 Then
                    Me.CmbBankAccount.Focus()
                    Exit Sub

                ElseIf Me.TxtDateFrom7.Text = Nothing Then
                    Me.TxtDateFrom7.Focus()
                    Exit Sub

                ElseIf Me.TxtDateTo7.Text = Nothing Then
                    Me.TxtDateTo7.Focus()
                    Exit Sub

                End If


            Else

                Dim TopBAL As Double
                Dim TopDate As Date
                Dim BK_ACC As String
                Dim BK_NAME, BK_ADD, BK_PH, BR_CODE, BR_NAME As String
                Dim GP_ID As Double
                Dim GP_NAME As String
                Dim B_Name As String
                Dim B_ADD As String
                Dim B_PH As String
                Dim B_CELL As String
                Dim B_FAX As String

                Try
                    Dim str22 As String = "SELECT GROUP_ID, GP_NAME, TR_TYPE, TR_DESC, BK_ACC, BK_NAME, BR_NAME, BR_CODE, BK_ADD, BK_PH, dDATE, TR_ID, Dr, Cr, B_NAME, B_ADD, B_PH, B_CELL, B_FAX FROM rptBANK_LEDGER WHERE BK_ACC='" & Me.CmbBankAccount.SelectedItem.Col1 & "' ORDER BY dDATE"
                    Me.daRptBANK_LEDGER = New SqlClient.SqlDataAdapter(str22, Me.SqlConnection1)
                    Me.DsRptBANK_LEDGER1.Clear()
                    Me.daRptBANK_LEDGER.Fill(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER)
                    Dim i As Integer = 0
                    Dim cBAL As Double = 0

                    For i = 0 To Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows.Count - 1
                        If Not i = 0 Then
                            cBAL = Val(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(i - 1).Item("BAL").ToString)
                        Else
                            cBAL = 0
                        End If

                        Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(i).Item("BAL") = cBAL + (Val(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(i).Item("Dr").ToString) - Val(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(i).Item("Cr").ToString))
                        TopBAL = Val(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(i).Item("BAL"))
                    Next

                    If Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows.Count = 0 Then
                        MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Not Found!")
                        Me.Cursor = Cursors.Default
                        Me.CRV1.ReportSource = Nothing
                        Exit Sub

                    ElseIf Not Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows.Count = 1 Then

                        'FILLING 1ST ROW DATA
                        BK_ACC = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("BK_ACC").ToString
                        BK_NAME = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("BK_NAME").ToString
                        BK_ADD = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("BK_ADD").ToString
                        BK_PH = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("BK_PH").ToString
                        BR_CODE = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("BR_CODE").ToString
                        BR_NAME = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("BR_NAME").ToString
                        GP_ID = Val(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("GROUP_ID"))
                        GP_NAME = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("GP_NAME").ToString
                        B_Name = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("B_NAME").ToString
                        B_ADD = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("B_ADD").ToString
                        B_PH = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("B_PH").ToString
                        B_CELL = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("B_CELL").ToString
                        B_FAX = Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("B_FAX").ToString

AGAIN_BANK:
                        For i = 0 To Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows.Count - 1
                            If Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(i).Item("dDATE").ToString = Nothing Then
                                Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows.RemoveAt(i)
                                GoTo AGAIN_BANK

                            ElseIf CDate(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(i).Item("dDATE")) < CDate(Me.TxtDateFrom7.Text) Or CDate(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(i).Item("dDATE")) > CDate(Me.TxtDateTo7.Text) Then
                                Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows.RemoveAt(i)
                                GoTo AGAIN_BANK
                            End If
                        Next

                        If Not Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows.Count = 0 Then
                            TopBAL = Val(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("BAL").ToString) - (Val(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("Dr").ToString) - Val(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("Cr").ToString))
                            TopDate = CDate(Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.Rows(0).Item("dDATE")).AddDays(-1)

                        Else
                            TopDate = CDate(Me.TxtDateFrom7.Text).AddDays(-1)

                        End If

                        Me.DsRptBANK_LEDGER1.rptBANK_LEDGER.AddrptBANK_LEDGERRow(GP_ID, GP_NAME, "OPN", "Opening Balance", BK_ACC, BK_NAME, BR_NAME, BR_CODE, BK_ADD, BK_PH, TopDate, Nothing, TopBAL, Nothing, B_Name, B_ADD, B_PH, B_CELL, B_FAX, TopBAL)

                    End If

                    Dim Rpt As New rptBANK_LEDGER_DS
                    Rpt.SetDataSource(Me.DsRptBANK_LEDGER1)
                    Me.CRV1.ReportSource = Rpt
                    Me.Cursor = Cursors.Default

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

                ' ''Dim Rpt As New rptBANK_LEDGER

                ' ''Try
                ' ''    Me.CRV1.ReportSource = Rpt
                ' ''    Me.CRV1.SelectionFormula = "{SV_BANK_LEDGER.BANK_ACC}='" & Me.CmbBankAccount.SelectedItem.Col1 & "'"
                ' ''    Me.Cursor = Cursors.Default

                ' ''    Exit Sub

                ' ''Catch Ex As Exception
                ' ''    Me.Cursor = Cursors.Default
                ' ''    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                ' ''End Try
            End If

        ElseIf Me.STATUS = "CASH_LEDGER_OVERALL" Then
            If Me.TxtDateFrom3.Text = Nothing Or Me.TxtDateTo3.Text = Nothing Then 'Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.TxtDateFrom3.Text = Nothing Then
                    Me.TxtDateFrom3.Focus()

                ElseIf Me.TxtDateTo3.Text = Nothing Then
                    Me.TxtDateTo3.Focus()

                End If

            Else

                Dim TopBAL As Double
                Dim TopDate As Date
                Dim GP_ID As Double
                Dim GP_NAME As String
                Dim B_Name As String
                Dim B_ADD As String
                Dim B_PH As String
                Dim B_CELL As String
                Dim B_FAX As String
               
                Try
                    Dim str22 As String = "SELECT GROUP_ID, GP_NAME, TR_TYPE, TR_DESC, dDATE, TR_ID, Dr, Cr, B_NAME, B_ADD, B_PH, B_CELL, B_FAX FROM rptCASH_LEDGER ORDER BY dDATE"
                    Me.daRptCASH_LEDGER = New SqlClient.SqlDataAdapter(str22, Me.SqlConnection1)
                    Me.DsRptCASH_LEDGER1.Clear()
                    Me.daRptCASH_LEDGER.Fill(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER)
                    Dim i As Integer = 0
                    Dim cBAL As Double = 0

                    For i = 0 To Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows.Count - 1
                        If Not i = 0 Then
                            cBAL = Val(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(i - 1).Item("BAL").ToString)
                        Else
                            cBAL = 0
                        End If

                        Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(i).Item("BAL") = cBAL + (Val(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(i).Item("Dr").ToString) - Val(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(i).Item("Cr").ToString))
                        TopBAL = Val(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(i).Item("BAL"))
                    Next

                    If Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows.Count = 0 Then
                        MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Not Found!")
                        Me.Cursor = Cursors.Default
                        Me.CRV1.ReportSource = Nothing
                        Exit Sub

                    ElseIf Not Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows.Count = 1 Then

                        'FILLING 1ST ROW DATA
                        GP_ID = Val(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("GROUP_ID"))
                        GP_NAME = Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("GP_NAME").ToString
                        B_Name = Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("B_NAME").ToString
                        B_ADD = Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("B_ADD").ToString
                        B_PH = Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("B_PH").ToString
                        B_CELL = Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("B_CELL").ToString
                        B_FAX = Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("B_FAX").ToString

AGAIN_CASH:
                        For i = 0 To Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows.Count - 1
                            If Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(i).Item("dDATE").ToString = Nothing Then
                                Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows.RemoveAt(i)
                                GoTo AGAIN_CASH

                            ElseIf CDate(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(i).Item("dDATE")) < CDate(Me.TxtDateFrom3.Text) Or CDate(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(i).Item("dDATE")) > CDate(Me.TxtDateTo3.Text) Then
                                Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows.RemoveAt(i)
                                GoTo AGAIN_CASH
                            End If
                        Next

                        If Not Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows.Count = 0 Then
                            TopBAL = Val(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("BAL").ToString) - (Val(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("Dr").ToString) - Val(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("Cr").ToString))
                            TopDate = CDate(Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.Rows(0).Item("dDATE")).AddDays(-1)

                        Else
                            TopDate = CDate(Me.TxtDateFrom3.Text).AddDays(-1)

                        End If

                        Me.DsRptCASH_LEDGER1.rptCASH_LEDGER.AddrptCASH_LEDGERRow(GP_ID, GP_NAME, "OPN", "Opening Balance", TopDate, Nothing, TopBAL, Nothing, B_Name, B_ADD, B_PH, B_CELL, B_FAX, TopBAL)

                    End If

                    Dim Rpt As New rptCASH_LEDGER_DS
                    Rpt.SetDataSource(Me.DsRptCASH_LEDGER1)
                    Me.CRV1.ReportSource = Rpt
                    Me.Cursor = Cursors.Default

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "CASH_LEDGER_DAILY" Then
            If Me.TxtDateFrom3.Text = Nothing Or Me.TxtDateTo3.Text = Nothing Then 'Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.TxtDateFrom3.Text = Nothing Then
                    Me.TxtDateFrom3.Focus()

                ElseIf Me.TxtDateTo3.Text = Nothing Then
                    Me.TxtDateTo3.Focus()

                End If

            Else

                Try
                    Dim Rpt As New rptCASH_LEDGER

                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom3.Text).Year
                    Yr2 = CDate(Me.TxtDateTo3.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom3.Text).Month
                    Mn2 = CDate(Me.TxtDateTo3.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom3.Text).Day
                    Dd2 = CDate(Me.TxtDateTo3.Text).Day

                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{SV_CASH_LEDGER.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_CASH_LEDGER.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                    Me.Cursor = Cursors.Default
                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "SUPPLIER_LEDGER_GP_WISE" Then
            If Me.CmbSupplier.Text = Nothing Or Me.CmbSupplier.SelectedIndex = -1 Or Me.TxtDateFrom.Text = Nothing Or Me.TxtDateTo.Text = Nothing Then 'Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.CmbSupplier.Text = Nothing Or Me.CmbSupplier.SelectedIndex = -1 Then
                    Me.CmbSupplier.Focus()

                ElseIf Me.TxtDateFrom.Text = Nothing Then
                    Me.TxtDateFrom.Focus()

                ElseIf Me.TxtDateTo.Text = Nothing Then
                    Me.TxtDateTo.Focus()

                    'ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                    'Me.CmbGroup.Focus()

                End If

            Else
                'Dim Rpt As New rptSUPPLIER_LEDGER

                Dim TopBAL As Double
                Dim TopDate As Date
                Dim B_Name As String
                Dim B_ADD As String
                Dim B_PH As String
                Dim B_CELL As String
                Dim B_FAX As String
                Dim S_ID As Double
                Dim S_NAME As String
                Dim PER_NAME As String
                Dim S_ADD As String
                Dim S_PH As String
                Dim PER_PH As String

                Try
                    Dim str22 As String = "SELECT GROUP_ID, TR_TYPE, TR_DESC, SUP_ID, PER_NAME, SUP_NAME, SUP_ADD, SUP_PH, PER_PH, dDATE, TR_ID, Dr, Cr, B_NAME, B_ADD, B_PH, B_CELL, B_FAX FROM rptSUPPLIER_LEDGER WHERE SUP_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & " ORDER BY dDATE"
                    Me.daRptSUPPLIER_LEDGER = New SqlClient.SqlDataAdapter(str22, Me.SqlConnection1)
                    Me.DsRptSUPPLIER_LEDGER1.Clear()
                    Me.daRptSUPPLIER_LEDGER.Fill(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER)
                    Dim i As Integer = 0
                    Dim cBAL As Double = 0

                    For i = 0 To Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows.Count - 1
                        If Not i = 0 Then
                            cBAL = Val(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(i - 1).Item("BAL").ToString)
                        Else
                            cBAL = 0
                        End If

                        Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(i).Item("BAL") = cBAL + (Val(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(i).Item("Dr").ToString) - Val(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(i).Item("Cr").ToString))
                        TopBAL = Val(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(i).Item("BAL"))
                    Next

                    If Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows.Count = 0 Then
                        MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Not Found!")
                        Me.Cursor = Cursors.Default
                        Me.CRV1.ReportSource = Nothing
                        Exit Sub

                    ElseIf Not Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows.Count = 1 Then

                        'FILLING 1ST ROW DATA
                        B_Name = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("B_NAME").ToString
                        B_ADD = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("B_ADD").ToString
                        B_PH = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("B_PH").ToString
                        B_CELL = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("B_CELL").ToString
                        B_FAX = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("B_FAX").ToString
                        S_ID = Val(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("SUP_ID"))
                        S_NAME = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("SUP_NAME").ToString
                        PER_NAME = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("PER_NAME").ToString
                        S_ADD = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("SUP_ADD").ToString
                        S_PH = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("SUP_PH").ToString
                        PER_PH = Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("PER_PH").ToString

AGAIN_SUP:
                        For i = 0 To Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows.Count - 1
                            If Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(i).Item("dDATE").ToString = Nothing Then
                                Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows.RemoveAt(i)
                                GoTo AGAIN_SUP

                            ElseIf CDate(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(i).Item("dDATE")) < CDate(Me.TxtDateFrom.Text) Or CDate(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(i).Item("dDATE")) > CDate(Me.TxtDateTo.Text) Then
                                Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows.RemoveAt(i)
                                GoTo AGAIN_SUP
                            End If
                        Next

                        If Not Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows.Count = 0 Then
                            TopBAL = Val(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("BAL").ToString) - (Val(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("Dr").ToString) - Val(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("Cr").ToString))
                            TopDate = CDate(Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.Rows(0).Item("dDATE")).AddDays(-1)

                        Else
                            TopDate = CDate(Me.TxtDateFrom.Text).AddDays(-1)

                        End If

                        Me.DsRptSUPPLIER_LEDGER1.rptSUPPLIER_LEDGER.AddrptSUPPLIER_LEDGERRow(Nothing, "OPN", "Opening Balance", S_ID, PER_NAME, S_NAME, S_ADD, S_PH, PER_PH, TopDate, Nothing, TopBAL, Nothing, B_Name, B_ADD, B_PH, B_CELL, B_FAX, TopBAL)

                    End If

                    Dim Rpt As New rptSUPPLIER_LEDGER_DS
                    Rpt.SetDataSource(Me.DsRptSUPPLIER_LEDGER1)
                    Me.CRV1.ReportSource = Rpt
                    Me.Cursor = Cursors.Default

                    '' ''Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    '' ''Yr1 = CDate(Me.TxtDateFrom.Text).Year
                    '' ''Yr2 = CDate(Me.TxtDateTo.Text).Year
                    '' ''Mn1 = CDate(Me.TxtDateFrom.Text).Month
                    '' ''Mn2 = CDate(Me.TxtDateTo.Text).Month
                    '' ''Dd1 = CDate(Me.TxtDateFrom.Text).Day
                    '' ''Dd2 = CDate(Me.TxtDateTo.Text).Day

                    '' ''Me.CRV1.ReportSource = Rpt
                    '' ''Me.CRV1.SelectionFormula = "({SV_SUPPLIER_LEDGER.TR_TYPE}='OPN' AND {V_BUSINESS_GROUP.nID}=" & Me.CmbGroup.SelectedItem.Col3 & " AND {V_SUPPLIER_INFO.ID}=" & Me.CmbSupplier.SelectedItem.Col3 & ") OR ({V_BUSINESS_GROUP.nID}=" & Me.CmbGroup.SelectedItem.Col3 & " AND {V_SUPPLIER_INFO.ID}=" & Me.CmbSupplier.SelectedItem.Col3 & " AND {SV_SUPPLIER_LEDGER.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_SUPPLIER_LEDGER.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & "))"
                    '' ''Me.Cursor = Cursors.Default
                    '' ''Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "CLIENT_LEDGER_GP_WISE" Then
            If Me.CmbClient.Text = Nothing Or Me.CmbClient.SelectedIndex = -1 Or Me.TxtDateFrom1.Text = Nothing Or Me.TxtDateTo1.Text = Nothing Then 'Or Me.CmbGroup1.SelectedIndex = -1 Or Me.CmbGroup1.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.CmbClient.Text = Nothing Or Me.CmbClient.SelectedIndex = -1 Then
                    Me.CmbClient.Focus()

                ElseIf Me.TxtDateFrom1.Text = Nothing Then
                    Me.TxtDateFrom1.Focus()

                ElseIf Me.TxtDateTo1.Text = Nothing Then
                    Me.TxtDateTo1.Focus()

                    'ElseIf Me.CmbGroup1.SelectedIndex = -1 Or Me.CmbGroup1.Text = Nothing Then
                    '    Me.CmbGroup1.Focus()

                End If

            Else

                Dim TopBAL As Double
                Dim TopDate As Date
                Dim B_Name As String
                Dim B_ADD As String
                Dim B_PH As String
                Dim B_CELL As String
                Dim B_FAX As String
                Dim S_ID As Double
                Dim S_NAME As String
                Dim S_ADD As String
                Dim S_AREA As String
                Dim S_PH As String

                Try
                    Dim str22 As String = "SELECT GROUP_ID, GP_NAME, TR_TYPE, TR_DESC, CLIENT_ID, SHOP_NAME, SHOP_ADD, AREA, SHOP_PH, dDATE, TR_ID, Dr, Cr, BUSINESS_NAME, PHONE, CELL_NO, FAX_NO, ADDRESS FROM rptCLIENT_LEDGER WHERE CLIENT_ID=" & Val(Me.CmbClient.SelectedItem.Col3) & " ORDER BY dDATE"
                    'Dim str22 As String = "SELECT GROUP_ID, GP_NAME, TR_TYPE, TR_DESC, CLIENT_ID, SHOP_NAME, SHOP_ADD, AREA, SHOP_PH, dDATE, TR_ID, Dr, Cr, BUSINESS_NAME, PHONE, CELL_NO, FAX_NO, ADDRESS FROM rptCLIENT_LEDGER WHERE GROUP_ID=" & Val(Me.CmbGroup1.SelectedItem.Col3) & " AND CLIENT_ID=" & Val(Me.CmbClient.SelectedItem.Col3) & " ORDER BY dDATE"
                    Me.daRptCLIENT_LEDGER = New SqlClient.SqlDataAdapter(str22, Me.SqlConnection1)
                    Me.DsRptCLIENT_LEDGER1.Clear()
                    Me.daRptCLIENT_LEDGER.Fill(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER)
                    Dim i As Integer = 0
                    Dim cBAL As Double = 0

                    For i = 0 To Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows.Count - 1
                        If Not i = 0 Then
                            cBAL = Val(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(i - 1).Item("BAL").ToString)
                        Else
                            cBAL = 0
                        End If

                        Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(i).Item("BAL") = cBAL + (Val(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(i).Item("Cr").ToString) - Val(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(i).Item("Dr").ToString))
                        TopBAL = Val(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(i).Item("BAL"))
                    Next

                    If Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows.Count = 0 Then
                        MsgBox("No Record Found!", MsgBoxStyle.Exclamation, "(NS) - Not Found!")
                        Me.Cursor = Cursors.Default
                        Me.CRV1.ReportSource = Nothing
                        Exit Sub

                    ElseIf Not Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows.Count = 1 Then

                        'FILLING 1ST ROW DATA
                        B_Name = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("BUSINESS_NAME").ToString
                        B_ADD = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("ADDRESS").ToString
                        B_PH = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("PHONE").ToString
                        B_CELL = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("CELL_NO").ToString
                        B_FAX = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("FAX_NO").ToString
                        S_ID = Val(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("CLIENT_ID"))
                        S_NAME = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("SHOP_NAME").ToString
                        S_ADD = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("SHOP_ADD").ToString
                        S_AREA = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("AREA").ToString
                        S_PH = Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("SHOP_PH").ToString

AGAIN_CL:
                        For i = 0 To Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows.Count - 1
                            If Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(i).Item("dDATE").ToString = Nothing Then
                                Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows.RemoveAt(i)
                                GoTo AGAIN_CL

                            ElseIf CDate(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(i).Item("dDATE")) < CDate(Me.TxtDateFrom1.Text) Or CDate(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(i).Item("dDATE")) > CDate(Me.TxtDateTo1.Text) Then
                                Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows.RemoveAt(i)
                                GoTo AGAIN_CL
                            End If
                        Next

                        If Not Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows.Count = 0 Then
                            TopBAL = Val(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("BAL").ToString) - (Val(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("Cr").ToString) - Val(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("Dr").ToString))
                            TopDate = CDate(Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.Rows(0).Item("dDATE")).AddDays(-1)

                        Else
                            TopDate = CDate(Me.TxtDateFrom1.Text).AddDays(-1)

                        End If

                        Me.DsRptCLIENT_LEDGER1.rptCLIENT_LEDGER.AddrptCLIENT_LEDGERRow(Nothing, Nothing, "OPN", "Opening Balance", S_ID, S_NAME, S_ADD, S_AREA, S_PH, TopDate, Nothing, Nothing, TopBAL, B_Name, B_PH, B_CELL, B_FAX, TopBAL, B_ADD)

                    End If

                    Dim Rpt As New rptCLIENT_LEDGER_DS
                    Rpt.SetDataSource(Me.DsRptCLIENT_LEDGER1)
                    Me.CRV1.ReportSource = Rpt
                    Me.Cursor = Cursors.Default


                    ' ''    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    ' ''    Yr1 = CDate(Me.TxtDateFrom1.Text).Year
                    ' ''    Yr2 = CDate(Me.TxtDateTo1.Text).Year
                    ' ''    Mn1 = CDate(Me.TxtDateFrom1.Text).Month
                    ' ''    Mn2 = CDate(Me.TxtDateTo1.Text).Month
                    ' ''    Dd1 = CDate(Me.TxtDateFrom1.Text).Day
                    ' ''    Dd2 = CDate(Me.TxtDateTo1.Text).Day

                    ' ''    Me.CRV1.ReportSource = Rpt
                    ' ''    ''Rpt.RecordSelectionFormula = "{V_BUSINESS_GROUP.nID}=" & Me.CmbGroup1.SelectedItem.Col3 & " AND {V_CLIENT_INFO.ID}=" & Me.CmbClient.SelectedItem.Col3 & ""
                    ' ''    ' ''Me.CRV1.ReportSource = Rpt
                    ' ''    ' ''Rpt.Refresh()

                    ' ''    ''Rpt.RecordSelectionFormula = "{V_BUSINESS_GROUP.nID}=" & Me.CmbGroup1.SelectedItem.Col3 & " AND {V_CLIENT_INFO.ID}=" & Me.CmbClient.SelectedItem.Col3 & " AND {SV_CLIENT_LEDGER.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_CLIENT_LEDGER.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                    ' ''    ' ''Me.CRV1.ReportSource = Rpt

                    ' ''    Me.CRV1.SelectionFormula = "({SV_CLIENT_LEDGER.TR_TYPE}='OPN' AND {V_BUSINESS_GROUP.nID}=" & Me.CmbGroup1.SelectedItem.Col3 & " AND {V_CLIENT_INFO.ID}=" & Me.CmbClient.SelectedItem.Col3 & ") OR ({V_BUSINESS_GROUP.nID}=" & Me.CmbGroup1.SelectedItem.Col3 & " AND {V_CLIENT_INFO.ID}=" & Me.CmbClient.SelectedItem.Col3 & " AND {SV_CLIENT_LEDGER.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_CLIENT_LEDGER.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & "))"

                    ' ''    ' ''Me.CRV1.SelectionFormula = "{V_BUSINESS_GROUP.nID}=" & Me.CmbGroup1.SelectedItem.Col3 & " AND {V_CLIENT_INFO.ID}=" & Me.CmbClient.SelectedItem.Col3 & ""
                    ' ''    ' ''Rpt.RecordSelectionFormula = ""

                    ' ''    ' ''Me.CRV1.SelectionFormula = 

                    ' ''    Me.Cursor = Cursors.Default
                    ' ''    Exit Sub
                    ' ''    '{SV_CLIENT_LEDGER.TR_ID}=0  OR (

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

            End If

        ElseIf Me.STATUS = "SALES_SUMMARY" Then
            If Me.CmbGroup2.Text = Nothing And Me.CmbCompany.Text = Nothing Then
                Try
                    Dim Rpt As New rptSALES_SUMMARY_ITEM_GpW
                    'Rpt.GroupHeaderSection1.SectionFormat.EnableSuppress = True
                    'Rpt.GroupFooterSection1.SectionFormat.EnableSuppress = True

                    Me.CRV1.ReportSource = Rpt
                    Me.Cursor = Cursors.Default

                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

            ElseIf Not Me.CmbGroup2.Text = Nothing And Me.CmbCompany.Text = Nothing Then
                Try
                    Dim Rpt As New rptSALES_SUMMARY_ITEM_GpW
                    'Rpt.GroupHeaderSection1.SectionFormat.EnableSuppress = True
                    'Rpt.GroupFooterSection1.SectionFormat.EnableSuppress = True

                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{V_BUSINESS_GROUP.nID}=" & Me.CmbGroup2.SelectedItem.Col3 & ""
                    Me.Cursor = Cursors.Default

                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

            ElseIf Me.CmbGroup2.Text = Nothing And Not Me.CmbCompany.Text = Nothing Then
                Try
                    Dim Rpt As New rptSALES_SUMMARY_ITEM_GpW
                    'Rpt.GroupHeaderSection1.SectionFormat.EnableSuppress = True
                    'Rpt.GroupFooterSection1.SectionFormat.EnableSuppress = True

                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{V_LUP_ITEM.VENDOR}='" & Me.CmbCompany.SelectedItem.Col1 & "'"
                    Me.Cursor = Cursors.Default

                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

            ElseIf Not Me.CmbGroup2.Text = Nothing And Not Me.CmbCompany.Text = Nothing Then
                Try
                    Dim Rpt As New rptSALES_SUMMARY_ITEM_GpW
                    'Rpt.GroupHeaderSection1.SectionFormat.EnableSuppress = True
                    'Rpt.GroupFooterSection1.SectionFormat.EnableSuppress = True

                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{V_BUSINESS_GROUP.nID}=" & Me.CmbGroup2.SelectedItem.Col3 & " AND {V_LUP_ITEM.VENDOR}='" & Me.CmbCompany.SelectedItem.Col1 & "'"
                    Me.Cursor = Cursors.Default

                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

            End If

        ElseIf Me.STATUS = "BANK_DEPOSIT_HISTORY" Or Me.STATUS = "BANK_WITHDRAWAL_HISTORY" Or Me.STATUS = "BANK_ADJUSTMENT_HISTORY" Then
            If Me.CmbBankAccount1.Text = Nothing Or Me.CmbBankAccount1.SelectedIndex = -1 Or Me.TxtDateFrom2.Text = Nothing Or Me.TxtDateTo2.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.CmbBankAccount1.Text = Nothing Or Me.CmbBankAccount1.SelectedIndex = -1 Then
                    Me.CmbBankAccount1.Focus()

                ElseIf Me.TxtDateFrom2.Text = Nothing Then
                    Me.TxtDateFrom2.Focus()

                ElseIf Me.TxtDateTo2.Text = Nothing Then
                    Me.TxtDateTo2.Focus()

                End If

            Else
                Try

                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom2.Text).Year
                    Yr2 = CDate(Me.TxtDateTo2.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom2.Text).Month
                    Mn2 = CDate(Me.TxtDateTo2.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom2.Text).Day
                    Dd2 = CDate(Me.TxtDateTo2.Text).Day

                    If Me.STATUS = "BANK_DEPOSIT_HISTORY" Then
                        Dim Rpt As New rptBANK_DEPOSIT_HISTORY
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{V_BANK_DEPOSITS.BANK_ACC}='" & Me.CmbBankAccount1.SelectedItem.Col1 & "' AND {V_BANK_DEPOSITS.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_BANK_DEPOSITS.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "BANK_WITHDRAWAL_HISTORY" Then
                        Dim Rpt As New rptBANK_WITHDRAWAL_HISTORY
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{V_BANK_WITHDRAWALS.BANK_ACC}='" & Me.CmbBankAccount1.SelectedItem.Col1 & "' AND {V_BANK_WITHDRAWALS.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_BANK_WITHDRAWALS.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "BANK_ADJUSTMENT_HISTORY" Then
                        Dim Rpt As New rptBANK_ADJ_HISTORY
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{V_BANK_ADJUSTMENT.BANK_ACC}='" & Me.CmbBankAccount1.SelectedItem.Col1 & "' AND {V_BANK_ADJUSTMENT.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_BANK_ADJUSTMENT.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    End If

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "CASH_PAYMENT_HISTORY" Or Me.STATUS = "CASH_RECEIPT_HISTORY" Or Me.STATUS = "SALE_INVOICE_DATE" Or Me.STATUS = "SALES_STOCK_SUMMARY_DATE" Or Me.STATUS = "RECEIPT_HISTORY" Or Me.STATUS = "RECOVERY_HISTORY" Then
            If Me.TxtDateFrom3.Text = Nothing Or Me.TxtDateTo3.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.TxtDateFrom3.Text = Nothing Then
                    Me.TxtDateFrom3.Focus()

                ElseIf Me.TxtDateTo3.Text = Nothing Then
                    Me.TxtDateTo3.Focus()

                End If

            Else
                Try

                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom3.Text).Year
                    Yr2 = CDate(Me.TxtDateTo3.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom3.Text).Month
                    Mn2 = CDate(Me.TxtDateTo3.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom3.Text).Day
                    Dd2 = CDate(Me.TxtDateTo3.Text).Day

                    If Me.STATUS = "CASH_PAYMENT_HISTORY" Then
                        Dim Rpt As New rptCASH_PAYMENT_HISTORY
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{SV_CASH_PAYMENT_HISTORY.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_CASH_PAYMENT_HISTORY.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "CASH_RECEIPT_HISTORY" Then
                        Dim Rpt As New rptCASH_RECEIPT_HISTORY
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{SV_CASH_RECEIPT_HISTORY.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_CASH_RECEIPT_HISTORY.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "SALE_INVOICE_DATE" Then
                        Dim Rpt As New rptSALES_INVOICE_NO_WS
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{V_SALE_MASTER.S_DATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_SALE_MASTER.S_DATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "SALES_STOCK_SUMMARY_DATE" Then
                        Dim Rpt As New rptSALES_STOCK_SUMMARY_DATE
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{rptSALES_STOCK_SUMMARY.S_DATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {rptSALES_STOCK_SUMMARY.S_DATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "RECEIPT_HISTORY" Then
                        Dim Rpt As New rptRECEIPT_HISTORY
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{V_RECEIPT_HISTORY.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_RECEIPT_HISTORY.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "RECOVERY_HISTORY" Then
                        Dim Rpt As New rptRECOVERY_HISTORY
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{V_RECOVERY_HISTORY.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_RECOVERY_HISTORY.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub



                    End If

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
                Exit Sub
            End If

        ElseIf Me.STATUS = "CLIENT_CREDIT_REPORT_AREA" Or Me.STATUS = "CASH_CHALLAN_AREA" Then
            If Me.CmbArea.Text = Nothing Or Me.CmbArea.SelectedIndex = -1 Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select Account!", MsgBoxStyle.Exclamation, "(NS) - Select Record!")
                Me.CmbArea.Focus()

            ElseIf Me.STATUS = "CLIENT_CREDIT_REPORT_AREA" Then

                Try
                    Dim Rpt As New rptCLIENT_CREDIT_REPORT_AREA
                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{V_CLIENT_INFO.AREA}='" & Me.CmbArea.SelectedItem.Col1 & "'"
                    Me.Cursor = Cursors.Default

                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

            ElseIf Me.STATUS = "CASH_CHALLAN_AREA" Then

                Try
                    'Dim Rpt As New rptCASH_CHALLAN_AREA_DT
                    Dim Rpt As New rptCASH_CHALLAN_AREA_PREV_DT
                    Me.CRV1.ReportSource = Rpt
                    'Me.CRV1.SelectionFormula = "{V_CLIENT_INFO.AREA}='" & Me.CmbArea.SelectedItem.Col1 & "' AND {SV_CLIENT_BALANCE_TOT.CLIENT_BAL}<>0" ' AND ({rptCASH_CHALLAN.S_DATE}= DateTime (" & Date.Now.Year & "," & Date.Now.Month & " ," & Date.Now.Day & ") OR CSTR({rptCASH_CHALLAN.S_DATE})='')"
                    Me.CRV1.SelectionFormula = "{LUP_AREA.sDESC}='" & Me.CmbArea.SelectedItem.Col1 & "' AND ROUND({@TotBal})<>0"
                    Me.Cursor = Cursors.Default
                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

            End If

        ElseIf Me.STATUS = "SALE_INVOICE_AREA_DATE" Or Me.STATUS = "SALES_STOCK_SUMMARY_AREA_DATE" Then
            If Me.CmbArea1.Text = Nothing Or Me.CmbArea1.SelectedIndex = -1 Or Me.TxtDateFrom4.Text = Nothing Or Me.TxtDateTo4.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.CmbArea1.Text = Nothing Or Me.CmbArea1.SelectedIndex = -1 Then
                    Me.CmbArea1.Focus()

                ElseIf Me.TxtDateFrom4.Text = Nothing Then
                    Me.TxtDateFrom4.Focus()

                ElseIf Me.TxtDateTo4.Text = Nothing Then
                    Me.TxtDateTo4.Focus()

                End If

            Else
                Try
                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom4.Text).Year
                    Yr2 = CDate(Me.TxtDateTo4.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom4.Text).Month
                    Mn2 = CDate(Me.TxtDateTo4.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom4.Text).Day
                    Dd2 = CDate(Me.TxtDateTo4.Text).Day

                    If Me.STATUS = "SALE_INVOICE_AREA_DATE" Then
                        Dim Rpt As New rptSALES_INVOICE_NO_WS
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{V_CLIENT_INFO.AREA}='" & Me.CmbArea1.SelectedItem.Col1 & "' AND {V_SALE_MASTER.S_DATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_SALE_MASTER.S_DATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "SALES_STOCK_SUMMARY_AREA_DATE" Then
                        Dim Rpt As New rptSALES_STOCK_SUMMARY_AREA_DATE
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{rptSALES_STOCK_SUMMARY.AREA}='" & Me.CmbArea1.SelectedItem.Col1 & "' AND {rptSALES_STOCK_SUMMARY.S_DATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {rptSALES_STOCK_SUMMARY.S_DATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    End If

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
                Exit Sub
            End If

        ElseIf Me.STATUS = "SALE_INVOICE_NO" Or Me.STATUS = "SALES_STOCK_SUMMARY_INV" Then
            If Val(Me.TxtInvoiceFrom.Text) = 0 Or Val(Me.TxtInvoiceTo.Text) = 0 Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Val(Me.TxtInvoiceFrom.Text) = 0 Then
                    Me.TxtInvoiceFrom.Focus()

                ElseIf Val(Me.TxtInvoiceTo.Text) = 0 Then
                    Me.TxtInvoiceTo.Focus()

                End If

            ElseIf Me.STATUS = "SALE_INVOICE_NO" Then
                Dim Rpt As New rptSALES_INVOICE_NO_WS

                Try
                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{V_SALE_MASTER.SINV_NO} >= " & Val(Me.TxtInvoiceFrom.Text) & " AND {V_SALE_MASTER.SINV_NO} <= " & Val(Me.TxtInvoiceTo.Text) & ""
                    Me.Cursor = Cursors.Default
                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
                Exit Sub

            ElseIf Me.STATUS = "SALES_STOCK_SUMMARY_INV" Then
                Dim Rpt As New rptSALES_STOCK_SUMMARY_INV

                Try
                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{rptSALES_STOCK_SUMMARY.SINV_NO} >= " & Val(Me.TxtInvoiceFrom.Text) & " AND {rptSALES_STOCK_SUMMARY.SINV_NO} <= " & Val(Me.TxtInvoiceTo.Text) & ""
                    Me.Cursor = Cursors.Default
                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
                Exit Sub

            End If

        ElseIf Me.STATUS = "STOCK_LEDGER" Then
            If Me.cmbITEM.Text = Nothing Or Me.cmbITEM.SelectedIndex = -1 Or Me.TxtDateFrom5.Text = Nothing Or Me.TxtDateTo5.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.cmbITEM.Text = Nothing Or Me.cmbITEM.SelectedIndex = -1 Then
                    Me.cmbITEM.Focus()

                ElseIf Me.TxtDateFrom5.Text = Nothing Then
                    Me.TxtDateFrom5.Focus()

                ElseIf Me.TxtDateTo5.Text = Nothing Then
                    Me.TxtDateTo5.Focus()

                End If

            Else
                Dim Rpt As New rptSTOCK_LEDGER

                Try
                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom5.Text).Year
                    Yr2 = CDate(Me.TxtDateTo5.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom5.Text).Month
                    Mn2 = CDate(Me.TxtDateTo5.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom5.Text).Day
                    Dd2 = CDate(Me.TxtDateTo5.Text).Day

                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "({SV_STOCK_LEDGER.TR_TYPE}='OPN' AND {SV_STOCK_LEDGER.nCODE}=" & Me.cmbITEM.SelectedItem.Col2 & ") OR ({SV_STOCK_LEDGER.nCODE}=" & Me.cmbITEM.SelectedItem.Col2 & " AND {SV_STOCK_LEDGER.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_STOCK_LEDGER.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & "))"
                    Me.Cursor = Cursors.Default
                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "STOCK_HAND_COMPANY_BATCH" Or Me.STATUS = "STOCK_HAND_COMPANY" Then
            If Me.CmbCompany1.Text = Nothing Or Me.CmbCompany1.SelectedIndex = -1 Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select Account!", MsgBoxStyle.Exclamation, "(NS) - Select Record!")
                Me.CmbCompany1.Focus()

            ElseIf Me.STATUS = "STOCK_HAND_COMPANY_BATCH" Then
                Dim Rpt As New rptSTOCK_HAND_COMPANY_BATCH_DT

                Try
                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{LUP_VENDOR.sDESC}='" & Me.CmbCompany1.SelectedItem.Col1 & "' AND {SV_STOCK_BALANCE.STK_BAL}<>0"
                    Me.Cursor = Cursors.Default

                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try

            ElseIf Me.STATUS = "STOCK_HAND_COMPANY" Then
                Dim Rpt As New rptSTOCK_HAND_COMPANY_DT

                Try
                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{LUP_VENDOR.sDESC}='" & Me.CmbCompany1.SelectedItem.Col1 & "' AND {SV_STOCK_BAL.STK_BAL}<>0"
                    Me.Cursor = Cursors.Default

                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "SALE_DETAIL_SALEMAN" Then
            If Me.CmbS_Man.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select Account!", MsgBoxStyle.Exclamation, "(NS) - Select Record!")
                Me.CmbS_Man.Focus()

            Else
                Dim Rpt As New rptSALES_DETAIL_SALEMAN_WISE

                Try
                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{LUP_EMPLOYEE.sNAME}='" & Me.CmbS_Man.SelectedItem.Col1 & "'"
                    Me.Cursor = Cursors.Default

                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "SALE_DETAIL_SALEMAN_DATE" Then
            If Me.CmbS_Man1.Text = Nothing Or Me.CmbS_Man1.SelectedIndex = -1 Or Me.TxtDateFrom6.Text = Nothing Or Me.TxtDateTo6.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.CmbS_Man1.Text = Nothing Or Me.CmbS_Man1.SelectedIndex = -1 Then
                    Me.CmbS_Man1.Focus()

                ElseIf Me.TxtDateFrom6.Text = Nothing Then
                    Me.TxtDateFrom6.Focus()

                ElseIf Me.TxtDateTo6.Text = Nothing Then
                    Me.TxtDateTo6.Focus()

                End If

            Else
                Dim Rpt As New rptSALES_DETAIL_SALEMAN_DATE_WISE

                Try
                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom6.Text).Year
                    Yr2 = CDate(Me.TxtDateTo6.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom6.Text).Month
                    Mn2 = CDate(Me.TxtDateTo6.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom6.Text).Day
                    Dd2 = CDate(Me.TxtDateTo6.Text).Day

                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{LUP_EMPLOYEE.sNAME}='" & Me.CmbS_Man1.SelectedItem.Col1 & "' AND {SV_SALESMAN_SALES.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_SALESMAN_SALES.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                    Me.Cursor = Cursors.Default
                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "RECOVERY_DETAIL_SALEMAN" Then
            If Me.CmbS_Man.Text = Nothing Or Me.CmbS_Man.SelectedIndex = -1 Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select Account!", MsgBoxStyle.Exclamation, "(NS) - Select Record!")
                Me.CmbS_Man.Focus()

            Else
                Dim Rpt As New rptRECOVERY_DETAIL_SALEMAN_WISE

                Try
                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{LUP_EMPLOYEE.sNAME}='" & Me.CmbS_Man.SelectedItem.Col1 & "'"
                    Me.Cursor = Cursors.Default

                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "RECOVERY_DETAIL_SALEMAN_DATE" Then
            If Me.CmbS_Man1.Text = Nothing Or Me.CmbS_Man1.SelectedIndex = -1 Or Me.TxtDateFrom6.Text = Nothing Or Me.TxtDateTo6.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.CmbS_Man1.Text = Nothing Or Me.CmbS_Man1.SelectedIndex = -1 Then
                    Me.CmbS_Man1.Focus()

                ElseIf Me.TxtDateFrom6.Text = Nothing Then
                    Me.TxtDateFrom6.Focus()

                ElseIf Me.TxtDateTo6.Text = Nothing Then
                    Me.TxtDateTo6.Focus()

                End If

            Else
                Dim Rpt As New rptRECOVERY_DETAIL_SALEMAN_DATE_WISE

                Try
                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom6.Text).Year
                    Yr2 = CDate(Me.TxtDateTo6.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom6.Text).Month
                    Mn2 = CDate(Me.TxtDateTo6.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom6.Text).Day
                    Dd2 = CDate(Me.TxtDateTo6.Text).Day

                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{LUP_EMPLOYEE.sNAME}='" & Me.CmbS_Man1.SelectedItem.Col1 & "' AND {SV_SALESMAN_RECOVERY.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_SALESMAN_RECOVERY.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                    Me.Cursor = Cursors.Default
                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "RECOVERY_SUMMARY_DATE" Then
            If Me.TxtDateFrom3.Text = Nothing Or Me.TxtDateTo3.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.TxtDateFrom3.Text = Nothing Then
                    Me.TxtDateFrom3.Focus()

                ElseIf Me.TxtDateTo3.Text = Nothing Then
                    Me.TxtDateTo3.Focus()

                End If

            Else
                Dim Rpt As New rptRECOVERY_SUMMARY_SALEMAN_DATE_WISE

                Try
                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom3.Text).Year
                    Yr2 = CDate(Me.TxtDateTo3.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom3.Text).Month
                    Mn2 = CDate(Me.TxtDateTo3.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom3.Text).Day
                    Dd2 = CDate(Me.TxtDateTo3.Text).Day

                    Me.CRV1.ReportSource = Rpt
                    Me.CRV1.SelectionFormula = "{SV_SALESMAN_RECOVERY.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_SALESMAN_RECOVERY.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                    Me.Cursor = Cursors.Default
                    Exit Sub

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "SALES_SUMMARY_DATE" Or Me.STATUS = "PURCHASE_SUMMARY_OVERALL" Then
            If Me.TxtDateFrom3.Text = Nothing Or Me.TxtDateTo3.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.TxtDateFrom3.Text = Nothing Then
                    Me.TxtDateFrom3.Focus()

                ElseIf Me.TxtDateTo3.Text = Nothing Then
                    Me.TxtDateTo3.Focus()

                End If

            Else

                Try
                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom3.Text).Year
                    Yr2 = CDate(Me.TxtDateTo3.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom3.Text).Month
                    Mn2 = CDate(Me.TxtDateTo3.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom3.Text).Day
                    Dd2 = CDate(Me.TxtDateTo3.Text).Day

                    If Me.STATUS = "SALES_SUMMARY_DATE" Then
                        Dim Rpt As New rptSALES_SUMMARY_SALEMAN_DATE_WISE
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{SV_SALESMAN_SALES.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {SV_SALESMAN_SALES.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "PURCHASE_SUMMARY_OVERALL" Then
                        Dim Rpt As New rptPURCHASE_SUMMARY_OVERALL
                        Me.CRV1.ReportSource = Rpt
                        Me.CRV1.SelectionFormula = "{V_PURCHASE_MASTER.P_DATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_PURCHASE_MASTER.P_DATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    End If

                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If

        ElseIf Me.STATUS = "SUPPLIER_PAYMENT_HISTORY" Or Me.STATUS = "SUPPLIER_RECEIPTS_HISTORY" Or Me.STATUS = "PURCHASE_SUMMARY_SUPPLIER" Then
            If Me.CmbSupplier1.Text = Nothing Or Me.CmbSupplier1.SelectedIndex = -1 Or Me.TxtDateFrom8.Text = Nothing Or Me.TxtDateTo8.Text = Nothing Then
                Me.Cursor = Cursors.Default
                MsgBox("Please Select or Enter correct value!", MsgBoxStyle.Exclamation, "(NS) - Wrong Value!")
                If Me.CmbSupplier1.SelectedIndex = -1 Or Me.CmbSupplier1.Text = Nothing Then
                    Me.CmbSupplier1.Focus()

                ElseIf Me.TxtDateFrom8.Text = Nothing Then
                    Me.TxtDateFrom8.Focus()

                ElseIf Me.TxtDateTo8.Text = Nothing Then
                    Me.TxtDateTo8.Focus()

                End If

            Else

                Try
                    Dim Yr1, Yr2, Mn1, Mn2, Dd1, Dd2 As Integer
                    Yr1 = CDate(Me.TxtDateFrom8.Text).Year
                    Yr2 = CDate(Me.TxtDateTo8.Text).Year
                    Mn1 = CDate(Me.TxtDateFrom8.Text).Month
                    Mn2 = CDate(Me.TxtDateTo8.Text).Month
                    Dd1 = CDate(Me.TxtDateFrom8.Text).Day
                    Dd2 = CDate(Me.TxtDateTo8.Text).Day

                    If Me.STATUS = "SUPPLIER_PAYMENT_HISTORY" Then
                        Dim RPT As New rptSUPPLIER_PAYMENT_HISTORY
                        Me.CRV1.ReportSource = RPT
                        Me.CRV1.SelectionFormula = "{V_SUPPLIER_INFO.ID} = " & Me.CmbSupplier1.SelectedItem.Col3 & " AND {V_SUPPLIER_PAYMENT.PMT_DATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_SUPPLIER_PAYMENT.PMT_DATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "SUPPLIER_RECEIPTS_HISTORY" Then
                        Dim RPT As New rptSUPPLIER_RECEIPTS_HISTORY
                        Me.CRV1.ReportSource = RPT
                        Me.CRV1.SelectionFormula = "{V_SUPPLIER_INFO.ID} = " & Me.CmbSupplier1.SelectedItem.Col3 & " AND {V_SUPPLIER_RECEIPTS.dDATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_SUPPLIER_RECEIPTS.dDATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub

                    ElseIf Me.STATUS = "PURCHASE_SUMMARY_SUPPLIER" Then
                        Dim RPT As New rptPURCHASE_SUMMARY_SUPPLIER
                        Me.CRV1.ReportSource = RPT
                        Me.CRV1.SelectionFormula = "{V_SUPPLIER_INFO.ID} = " & Me.CmbSupplier1.SelectedItem.Col3 & " AND {V_PURCHASE_MASTER.P_DATE} >= DateTime (" & Yr1 & "," & Mn1 & " ," & Dd1 & ") AND {V_PURCHASE_MASTER.P_DATE} <= DateTime (" & Yr2 & "," & Mn2 & " ," & Dd2 & ")"
                        Me.Cursor = Cursors.Default
                        Exit Sub


                    End If
                Catch Ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(Ex.Message, MsgBoxStyle.Exclamation, "(NS) - Error!")
                End Try
            End If



        ElseIf Me.STATUS = "" Then

        End If


        Me.Cursor = Cursors.Default
    End Sub

#End Region

#Region "Sub and Functions"
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

        Me.CmbBankAccount1.SelectedIndex = -1
        Me.CmbBankAccount1.Items.Clear()
        Me.CmbBankAccount1.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbBankAccount1.SourceDataString = New String(1) {"sACCOUNT_NO", "sBANK_NAME"}
        Me.CmbBankAccount1.SourceDataTable = dtLoading
    End Sub
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

        Me.CmbSupplier1.SelectedIndex = -1
        Me.CmbSupplier1.Items.Clear()
        Me.CmbSupplier1.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbSupplier1.SourceDataString = New String(2) {"sSUPPLIER_NAME", "sCONTACT_PERSON", "nID"}
        Me.CmbSupplier1.SourceDataTable = dtLoading

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
        Me.CmbGroup.SourceDataString = New String(2) {"sGROUP_DEALER", "sGROUP_NAME", "nID"}
        Me.CmbGroup.SourceDataTable = dtLoading

        Me.CmbGroup1.SelectedIndex = -1
        Me.CmbGroup1.Items.Clear()
        Me.CmbGroup1.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbGroup1.SourceDataString = New String(2) {"sGROUP_DEALER", "sGROUP_NAME", "nID"}
        Me.CmbGroup1.SourceDataTable = dtLoading

        Me.CmbGroup2.SelectedIndex = -1
        Me.CmbGroup2.Items.Clear()
        Me.CmbGroup2.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbGroup2.SourceDataString = New String(2) {"sGROUP_DEALER", "sGROUP_NAME", "nID"}
        Me.CmbGroup2.SourceDataTable = dtLoading

    End Sub
    Private Sub FillComboBox_Company()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_VENDOR ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_VENDOR = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_VENDOR1.Clear()
        Me.daLUP_VENDOR.Fill(Me.DsLUP_VENDOR1.LUP_VENDOR)

        Dim dtLoading As New DataTable("LUP_VENDOR")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_VENDOR1.LUP_VENDOR.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_VENDOR1.LUP_VENDOR.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_VENDOR1.LUP_VENDOR.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbCompany.SelectedIndex = -1
        Me.CmbCompany.Items.Clear()
        Me.CmbCompany.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbCompany.SourceDataString = New String(1) {"sDESC", "nCODE"}
        Me.CmbCompany.SourceDataTable = dtLoading

        Me.CmbCompany1.SelectedIndex = -1
        Me.CmbCompany1.Items.Clear()
        Me.CmbCompany1.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbCompany1.SourceDataString = New String(1) {"sDESC", "nCODE"}
        Me.CmbCompany1.SourceDataTable = dtLoading

    End Sub
    Private Sub FillComboBox_Client()
        Dim Str1 As String = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CONVERT(NUMERIC(18,2), CREDIT_LIM) AS CREDIT_LIM, GST_NO, CONVERT(NUMERIC(18,2), OPEN_BAL) AS OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO WHERE STATUS='1' ORDER BY SHOP_NAME"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daCLIENT_INFO = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsCLIENT_INFO1.Clear()
        Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO1.V_CLIENT_INFO)

        Dim dtLoading As New DataTable("V_CLIENT_INFO")

        dtLoading.Columns.Add("ID", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("NAME", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("SHOP_NAME", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsCLIENT_INFO1.V_CLIENT_INFO.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("ID") = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(0).ToString
            dr("NAME") = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(1).ToString
            dr("SHOP_NAME") = Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(2).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbClient.SelectedIndex = -1
        Me.CmbClient.Items.Clear()
        Me.CmbClient.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbClient.SourceDataString = New String(2) {"SHOP_NAME", "NAME", "ID"}
        Me.CmbClient.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_AREA()
        Dim Str1 As String = "SELECT CODE, AREA, ZONE FROM V_LUP_AREA ORDER BY AREA"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_AREA = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_AREA1.Clear()
        Me.daLUP_AREA.Fill(Me.DsLUP_AREA1.V_LUP_AREA)

        Dim dtLoading As New DataTable("V_LUP_AREA")

        dtLoading.Columns.Add("CODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("AREA", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_AREA1.V_LUP_AREA.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("CODE") = Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(0).ToString
            dr("AREA") = Me.DsLUP_AREA1.V_LUP_AREA.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbArea.SelectedIndex = -1
        Me.CmbArea.Items.Clear()
        Me.CmbArea.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbArea.SourceDataString = New String(1) {"AREA", "CODE"}
        Me.CmbArea.SourceDataTable = dtLoading

        Me.CmbArea1.SelectedIndex = -1
        Me.CmbArea1.Items.Clear()
        Me.CmbArea1.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbArea1.SourceDataString = New String(1) {"AREA", "CODE"}
        Me.CmbArea1.SourceDataTable = dtLoading
    End Sub
    Private Sub FillComboBox_ITEM()
        Dim Str1 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE, ITEM_CAT FROM V_LUP_ITEM ORDER BY sITEM_NAME"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsV_LUP_ITEM1.Clear()
        Me.daLUP_ITEM.Fill(Me.DsV_LUP_ITEM1.V_LUP_ITEM)

        Dim dtLoading As New DataTable("V_LUP_ITEM")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sITEM_NAME", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsV_LUP_ITEM1.V_LUP_ITEM.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsV_LUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(0).ToString
            dr("sITEM_NAME") = Me.DsV_LUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.cmbITEM.SelectedIndex = -1
        Me.cmbITEM.Items.Clear()
        Me.cmbITEM.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.cmbITEM.SourceDataString = New String(1) {"sITEM_NAME", "nCODE"}
        Me.cmbITEM.SourceDataTable = dtLoading

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

        Me.CmbS_Man.SelectedIndex = -1
        Me.CmbS_Man.Items.Clear()
        Me.CmbS_Man.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbS_Man.SourceDataString = New String(2) {"NAME", "DESIGNATION", "CODE"}
        Me.CmbS_Man.SourceDataTable = dtLoading

        Me.CmbS_Man1.SelectedIndex = -1
        Me.CmbS_Man1.Items.Clear()
        Me.CmbS_Man1.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbS_Man1.SourceDataString = New String(2) {"NAME", "DESIGNATION", "CODE"}
        Me.CmbS_Man1.SourceDataTable = dtLoading

    End Sub
#End Region

#Region "DBLogOn"
    Dim myConnectionInfo As New ConnectionInfo
    Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo)
        myConnectionInfo.IntegratedSecurity = True

        myConnectionInfo.ServerName = "SERVER"
        myConnectionInfo.DatabaseName = "Neuro_BS"

        Dim myTableLogOnInfos As TableLogOnInfos = CRV1.LogOnInfo
        For Each myTableLogOnInfo As TableLogOnInfo In myTableLogOnInfos
            myTableLogOnInfo.ConnectionInfo = myConnectionInfo
        Next
    End Sub
#End Region

End Class