Imports SDS = System.Data.SqlClient
Public Class frmEXPENSE_DETAIL

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
    Private Sub frmEXPENSE_DETAIL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Expense_Type()
        Me.FillComboBox_Group()
        Me.FillComboBox_BankAccount()
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")

        Me.daNS_DEFAULT.Fill(Me.DsNS_DEFAULT1.NS_DEFAULT)

        Me.Default_Setting()

    End Sub

    Private Sub frmEXPENSE_DETAIL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtBankPmt.GotFocus, TxtCashPmt.GotFocus, TxtChequeDate.GotFocus, TxtChequeNo.GotFocus, TxtChequeType.GotFocus, TxtDate.GotFocus, TxtDescription.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtBankPmt.LostFocus, TxtCashPmt.LostFocus, TxtChequeDate.LostFocus, TxtChequeNo.LostFocus, TxtChequeType.LostFocus, TxtDate.LostFocus, TxtDescription.LostFocus
        CType(sender, TextBox).BackColor = Color.White

        Try
            If sender.Name = "TxtDate" Then
                If sender.TextLength > 0 Then
                    sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                End If

            ElseIf sender.Name = "TxtChequeDate" Then
                If sender.TextLength > 0 Then
                    sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                End If

                'ElseIf sender.Name = "TxtBankPmt" Then
                '    If sender.TextLength <= 0 Then
                '        sender.Text = "0.00"
                '    End If

                'ElseIf sender.Name = "TxtCashPmt" Then
                '    If sender.TextLength <= 0 Then
                '        sender.Text = "0.00"
                '    End If

            End If
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub
    Private Sub TxtBankPmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtBankPmt.Leave, TxtCashPmt.Leave
        If sender.Text = Nothing Or Val(sender.Text) = 0 Then
            sender.Text = "0.00"
        End If
    End Sub
    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtBankPmt.KeyPress, TxtCashPmt.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub
#End Region

#Region "Lable Control"
    Private Sub LblID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblID.TextChanged
        If Me.LblID.Text.Length > 0 Then
            Me.BttnDelete.Enabled = True
            Dim Str1 As String = "SELECT ID, dDATE, EXP_NAME, EXP_DESC, CONVERT(NUMERIC(18, 2), CASH_AMT) AS CASH_AMT, CHQ_NO, CHQ_TYPE, CHQ_DATE, CONVERT(NUMERIC(18, 2), BANK_AMT) AS BANK_AMT, BANK_ACC, GROUP_NAME, USER_NAME, GROUP_ID FROM V_EXPENSES WHERE ID=" & Val(Me.LblID.Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daEXPENSE_DETAIL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsEXPENSE_DETAIL1.Clear()
            Me.daEXPENSE_DETAIL.Fill(Me.DsEXPENSE_DETAIL1.V_EXPENSES)
            Dim StrCmb As String

            On Error GoTo Fix

            StrCmb = Me.CmbExpenseType.Text
            Me.CmbExpenseType.SelectedIndex = -1
            Me.CmbExpenseType.SelectedIndex = Me.CmbExpenseType.FindString(StrCmb)

            StrCmb = Me.CmbGroup.Text
            Me.CmbGroup.SelectedIndex = -1
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCmb)

            StrCmb = Me.CmbBankAccount.Text
            Me.CmbBankAccount.SelectedIndex = -1
            If Not StrCmb = Nothing Then
                Me.CmbBankAccount.SelectedIndex = Me.CmbBankAccount.FindString(StrCmb)
            End If

Fix:
        Else
            Me.BttnDelete.Enabled = False
            Me.DsEXPENSE_DETAIL1.Clear()
        End If
    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbExpenseType.GotFocus, CmbGroup.GotFocus, CmbBankAccount.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbExpenseType.LostFocus, CmbGroup.LostFocus, CmbBankAccount.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
    Private Sub CmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbGroup.SelectedIndexChanged, CmbBankAccount.SelectedIndexChanged
        Try
            Dim Str1 As String = "SELECT BANK_ACC, CONVERT(NUMERIC(18,2),BANK_BAL) AS BANK_BAL, GROUP_ID FROM SV_BANK_BALANCE WHERE BANK_ACC='" & Me.CmbBankAccount.SelectedItem.Col1 & "' AND GROUP_ID=" & Val(Me.CmbGroup.SelectedItem.Col3) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daSV_BANK_BAL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsSV_BANK_BAL1.Clear()
            Me.daSV_BANK_BAL.Fill(Me.DsSV_BANK_BAL1.SV_BANK_BALANCE)
        Catch ex As Exception

        End Try

        Try
            Dim Str1 As String = "SELECT GROUP_ID, CONVERT(NUMERIC(18,2),CASH_BAL) AS CASH_BAL FROM SV_CASH_BALANCE WHERE GROUP_ID=" & Val(Me.CmbGroup.SelectedItem.Col3) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daSV_CASH_BAL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsSV_CASH_BAL1.Clear()
            Me.daSV_CASH_BAL.Fill(Me.DsSV_CASH_BAL1.SV_CASH_BALANCE)
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        'Me.TxtDate.Text = Nothing
        'Me.CmbGroup.SelectedIndex = -1
        'Me.CmbEmployee.SelectedIndex = -1
        'Me.CmbSupplier.SelectedIndex = -1
        'Me.TxtCashPmt.Text = "0.00"
        'Me.TxtBankPmt.Text = "0.00"

        'Me.CmbBankAccount.SelectedIndex = -1
        'Me.TxtChequeDate.Text = Nothing
        'Me.TxtChequeNo.Text = Nothing
        'Me.TxtChequeType.Text = Nothing

        'Me.TxtDescription.Text = Nothing

        Me.BttnSave.Enabled = True

        Me.LblID.Text = Nothing

        Me.DsEXPENSE_DETAIL1.Clear()
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.TxtDate.Focus()

        Me.Default_Setting()

    End Sub

    Private Sub BttnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch.Click
        My.Forms.frmSEARCH_EXPENSE_DETAIL.ShowDialog(Me)
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click
        If Not Val(Me.LblID.Text) > 0 Then
            'FOR INSERT RECORD IN EXPENSES
            If Me.TxtDate.Text = Nothing Or Me.CmbExpenseType.SelectedIndex = -1 Or Me.CmbExpenseType.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                If Me.TxtDate.Text = Nothing Then
                    Me.TxtDate.Focus()

                ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                    Me.CmbGroup.Focus()

                ElseIf Me.CmbExpenseType.SelectedIndex = -1 Or Me.CmbExpenseType.Text = Nothing Then
                    Me.CmbExpenseType.Focus()

                End If

                'PAYMENT MADE VIA BANK ONLY
            ElseIf Val(Me.TxtBankPmt.Text) > 0 And Val(Me.TxtCashPmt.Text) <= 0 Then
                If Me.CmbBankAccount.SelectedIndex = -1 Or Me.TxtChequeNo.Text = Nothing Or Me.TxtChequeType.Text = Nothing Or Me.TxtChequeDate.Text = Nothing Then
                    If Me.CmbBankAccount.SelectedIndex = -1 Then
                        Me.CmbBankAccount.Focus()

                    ElseIf Me.TxtChequeNo.Text = Nothing Then
                        Me.TxtChequeNo.Focus()

                    ElseIf Me.TxtChequeType.Text = Nothing Then
                        Me.TxtChequeType.Focus()

                    ElseIf Me.TxtChequeDate.Text = Nothing Then
                        Me.TxtChequeDate.Focus()

                    End If


                ElseIf MsgBox("Do you want to save in BANK: '" & Me.TxtBankPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                    Me.asInsert.SaveValueIN("INSERT INTO EXPENSES(dDATE, nEXPENSE_CODE, sEXP_DESC, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, sACCOUNT_CODE, nLOGIN_ID, nBUSINESS_CODE) VALUES('" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.CmbExpenseType.SelectedItem.Col3) & ", '" & Me.TxtDescription.Text & "', '" & Me.TxtChequeNo.Text & "', '" & Me.TxtChequeType.Text & "', '" & CDate(Me.TxtChequeDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.TxtBankPmt.Text) & ",'" & Me.CmbBankAccount.SelectedItem.Col1 & "',10," & Val(Me.CmbGroup.SelectedItem.Col3) & ")")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()

                End If
                'PAYMENT MADE VIA CASH ONLY
            ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) > 0 Then
                If MsgBox("Do you want to save CASH: '" & Me.TxtCashPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                    Me.asInsert.SaveValueIN("INSERT INTO EXPENSES(dDATE, nEXPENSE_CODE, sEXP_DESC, nCASH_AMOUNT, nLOGIN_ID, nBUSINESS_CODE) VALUES('" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "'," & Val(Me.CmbExpenseType.SelectedItem.Col3) & ",'" & Me.TxtDescription.Text & "'," & Val(Me.TxtCashPmt.Text) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ")")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()

                End If

                'PAYMENT DATE VIA BOTH (CASH & BANK)
            ElseIf Val(Me.TxtBankPmt.Text) > 0 And Val(Me.TxtCashPmt.Text) > 0 Then

                If Me.CmbBankAccount.SelectedIndex = -1 Or Me.TxtChequeNo.Text = Nothing Or Me.TxtChequeType.Text = Nothing Or Me.TxtChequeDate.Text = Nothing Then
                    MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                    If Me.CmbBankAccount.SelectedIndex = -1 Then
                        Me.CmbBankAccount.Focus()

                    ElseIf Me.TxtChequeNo.Text = Nothing Then
                        Me.TxtChequeNo.Focus()

                    ElseIf Me.TxtChequeType.Text = Nothing Then
                        Me.TxtChequeType.Focus()

                    ElseIf Me.TxtChequeDate.Text = Nothing Then
                        Me.TxtChequeDate.Focus()

                    End If

                ElseIf MsgBox("Do you want to save CASH: '" & Me.TxtCashPmt.Text & "' & BANK: '" & Me.TxtBankPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                    Me.asInsert.SaveValueIN("INSERT INTO EXPENSES(dDATE, nEXPENSE_CODE, sEXP_DESC, nCASH_AMOUNT, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHEQUE_AMOUNT, sACCOUNT_CODE, nLOGIN_ID, nBUSINESS_CODE) VALUES('" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "'," & Val(Me.CmbExpenseType.SelectedItem.Col3) & ",'" & Me.TxtDescription.Text & "', " & Val(Me.TxtCashPmt.Text) & ", '" & Me.TxtChequeNo.Text & "', '" & Me.TxtChequeType.Text & "', '" & CDate(Me.TxtChequeDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.TxtBankPmt.Text) & ",'" & Me.CmbBankAccount.SelectedItem.Col1 & "',10," & Val(Me.CmbGroup.SelectedItem.Col3) & ")")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()
                End If

            ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) <= 0 Then
                MsgBox("Please enter Cash Value OR Bank Value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.TxtCashPmt.Focus()
            End If


        ElseIf Val(Me.LblID.Text) > 0 Then

            'FOR UPDATE RECORD IN SUPPLIER PAYMENT
            If Me.TxtDate.Text = Nothing Or Me.CmbExpenseType.SelectedIndex = -1 Or Me.CmbExpenseType.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                If Me.TxtDate.Text = Nothing Then
                    Me.TxtDate.Focus()

                ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                    Me.CmbGroup.Focus()

                ElseIf Me.CmbExpenseType.SelectedIndex = -1 Or Me.CmbExpenseType.Text = Nothing Then
                    Me.CmbExpenseType.Focus()

                End If

                'PAYMENT MADE VIA BANK ONLY
            ElseIf Val(Me.TxtBankPmt.Text) > 0 And Val(Me.TxtCashPmt.Text) <= 0 Then
                If Me.CmbBankAccount.SelectedIndex = -1 Or Me.TxtChequeNo.Text = Nothing Or Me.TxtChequeType.Text = Nothing Or Me.TxtChequeDate.Text = Nothing Then
                    If Me.CmbBankAccount.SelectedIndex = -1 Then
                        Me.CmbBankAccount.Focus()

                    ElseIf Me.TxtChequeNo.Text = Nothing Then
                        Me.TxtChequeNo.Focus()

                    ElseIf Me.TxtChequeType.Text = Nothing Then
                        Me.TxtChequeType.Focus()

                    ElseIf Me.TxtChequeDate.Text = Nothing Then
                        Me.TxtChequeDate.Focus()

                    End If


                ElseIf MsgBox("Do you want to update to BANK: '" & Me.TxtBankPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Update?") = MsgBoxResult.Yes Then
                    Me.asUpdate.UpdateValueIN("UPDATE EXPENSES SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nEXPENSE_CODE=" & Val(Me.CmbExpenseType.SelectedItem.Col3) & ", sEXP_DESC='" & Me.TxtDescription.Text & "', sCHEQUE_NO='" & Me.TxtChequeNo.Text & "', sCHEQUE_TYPE='" & Me.TxtChequeType.Text & "', dCHEQUE_DATE='" & CDate(Me.TxtChequeDate.Text).ToString("MM-dd-yyyy") & "', nCHEQUE_AMOUNT=" & Val(Me.TxtBankPmt.Text) & ", sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nCASH_AMOUNT=0 WHERE nID=" & Val(Me.LblID.Text) & "")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()

                End If

                'PAYMENT MADE VIA CASH ONLY
            ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) > 0 Then
                If MsgBox("Do you want to update CASH: '" & Me.TxtCashPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Update?") = MsgBoxResult.Yes Then
                    Me.asUpdate.UpdateValueIN("UPDATE EXPENSES SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nEXPENSE_CODE=" & Val(Me.CmbExpenseType.SelectedItem.Col3) & ", sEXP_DESC='" & Me.TxtDescription.Text & "', sCHEQUE_NO=NULL, sCHEQUE_TYPE=NULL, dCHEQUE_DATE=NULL, nCHEQUE_AMOUNT=NULL, sACCOUNT_CODE=NULL, nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nCASH_AMOUNT=" & Val(Me.TxtCashPmt.Text) & " WHERE nID=" & Val(Me.LblID.Text) & "")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()

                End If

                'PAYMENT DATE VIA BOTH (CASH & BANK)
            ElseIf Val(Me.TxtBankPmt.Text) > 0 And Val(Me.TxtCashPmt.Text) > 0 Then

                If Me.CmbBankAccount.SelectedIndex = -1 Or Me.TxtChequeNo.Text = Nothing Or Me.TxtChequeType.Text = Nothing Or Me.TxtChequeDate.Text = Nothing Then
                    MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                    If Me.CmbBankAccount.SelectedIndex = -1 Then
                        Me.CmbBankAccount.Focus()

                    ElseIf Me.TxtChequeNo.Text = Nothing Then
                        Me.TxtChequeNo.Focus()

                    ElseIf Me.TxtChequeType.Text = Nothing Then
                        Me.TxtChequeType.Focus()

                    ElseIf Me.TxtChequeDate.Text = Nothing Then
                        Me.TxtChequeDate.Focus()

                    End If

                ElseIf MsgBox("Do you want to update CASH: '" & Me.TxtCashPmt.Text & "' & BANK: '" & Me.TxtBankPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Update?") = MsgBoxResult.Yes Then
                    Me.asUpdate.UpdateValueIN("UPDATE EXPENSES SET dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', nEXPENSE_CODE=" & Val(Me.CmbExpenseType.SelectedItem.Col3) & ", sEXP_DESC='" & Me.TxtDescription.Text & "', sCHEQUE_NO='" & Me.TxtChequeNo.Text & "', sCHEQUE_TYPE='" & Me.TxtChequeType.Text & "', dCHEQUE_DATE='" & CDate(Me.TxtChequeDate.Text).ToString("MM-dd-yyyy") & "', nCHEQUE_AMOUNT=" & Val(Me.TxtBankPmt.Text) & ", sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", nCASH_AMOUNT=" & Val(Me.TxtCashPmt.Text) & " WHERE nID=" & Val(Me.LblID.Text) & "")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()
                End If

            ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) <= 0 Then
                MsgBox("Please enter Cash Value OR Bank Value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.TxtCashPmt.Focus()
            End If


        End If

    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

    Private Sub BttnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnDelete.Click
        If MsgBox("Are you sure to Delete this Payment Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Deletion!") = MsgBoxResult.Yes Then
            Me.asDelete.DeleteValue("DELETE FROM EXPENSES WHERE nID=" & Val(Me.LblID.Text) & "")
            Me.LblID.Text = Nothing
        End If
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
    Private Sub FillComboBox_Expense_Type()
        Dim Str1 As String = "SELECT CODE, SUB_EXP_NAME, MAIN_EXP_NAME FROM V_LUP_EXPENSE_SUB"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daV_EXPENSE_SUB = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsV_EXPENSE_SUB1.Clear()
        Me.daV_EXPENSE_SUB.Fill(Me.DsV_EXPENSE_SUB1.V_LUP_EXPENSE_SUB)

        Dim dtLoading As New DataTable("V_LUP_EXPENSE_SUB")

        dtLoading.Columns.Add("CODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("SUB_EXP_NAME", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("MAIN_EXP_NAME", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsV_EXPENSE_SUB1.V_LUP_EXPENSE_SUB.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("CODE") = Me.DsV_EXPENSE_SUB1.V_LUP_EXPENSE_SUB.Item(Cnt).Item(0).ToString
            dr("SUB_EXP_NAME") = Me.DsV_EXPENSE_SUB1.V_LUP_EXPENSE_SUB.Item(Cnt).Item(1).ToString
            dr("MAIN_EXP_NAME") = Me.DsV_EXPENSE_SUB1.V_LUP_EXPENSE_SUB.Item(Cnt).Item(2).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbExpenseType.SelectedIndex = -1
        Me.CmbExpenseType.Items.Clear()
        Me.CmbExpenseType.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbExpenseType.SourceDataString = New String(2) {"SUB_EXP_NAME", "MAIN_EXP_NAME", "CODE"}
        Me.CmbExpenseType.SourceDataTable = dtLoading
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

    Private Sub Default_Setting()
        On Error GoTo Fix
        Dim StrCMB As String

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("EXP_SUB_HEAD").ToString
        Me.CmbExpenseType.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbExpenseType.SelectedIndex = Me.CmbExpenseType.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("GROUP").ToString
        Me.CmbGroup.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCMB)
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