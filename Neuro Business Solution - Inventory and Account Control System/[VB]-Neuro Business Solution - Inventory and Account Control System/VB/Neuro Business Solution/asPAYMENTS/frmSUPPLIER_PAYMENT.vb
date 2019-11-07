Imports SDS = System.Data.SqlClient
Public Class frmSUPPLIER_PAYMENT

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
    Private Sub frmSUPPLIER_PAYMENT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Employee()
        Me.FillComboBox_Group()
        Me.FillComboBox_Supplier()
        Me.FillComboBox_BankAccount()
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")

        Me.daNS_DEFAULT.Fill(Me.DsNS_DEFAULT1.NS_DEFAULT)

        Me.Default_Setting()
    End Sub

    Private Sub frmSUPPLIER_PAYMENT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
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
            Dim Str1 As String = "SELECT ID, SUPPLIER_ID, SUPPLIER_NAME, PMT_DATE, CONVERT(NUMERIC(18,2),CASH_AMT) AS CASH_AMT, CHQ_NO, CHQ_TYPE, CHQ_DATE, CONVERT(NUMERIC(18,2),BANK_AMT) AS BANK_AMT, BANK_ACC, BANK_NAME, PINV_NO, USER_NAME, GROUP_NAME, TOT_PAYMENT, EMP_NAME, DESCRIPTION, GROUP_ID FROM V_SUPPLIER_PAYMENT WHERE ID=" & Val(Me.LblID.Text) & " ORDER BY ID DESC"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daSUPPLIER_PAYMENT = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsSUPPLIER_PAYMENT1.Clear()
            Me.daSUPPLIER_PAYMENT.Fill(Me.DsSUPPLIER_PAYMENT1.V_SUPPLIER_PAYMENT)
            Dim StrCmb As String

            On Error GoTo Fix

            StrCmb = Me.CmbEmployee.Text
            Me.CmbEmployee.SelectedIndex = -1
            Me.CmbEmployee.SelectedIndex = Me.CmbEmployee.FindString(StrCmb)

            StrCmb = Me.CmbGroup.Text
            Me.CmbGroup.SelectedIndex = -1
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCmb)

            StrCmb = Me.CmbSupplier.Text
            Me.CmbSupplier.SelectedIndex = -1
            Me.CmbSupplier.SelectedIndex = Me.CmbSupplier.FindString(StrCmb)

            StrCmb = Me.CmbBankAccount.Text
            Me.CmbBankAccount.SelectedIndex = -1
            If Not StrCmb = Nothing Then
                Me.CmbBankAccount.SelectedIndex = Me.CmbBankAccount.FindString(StrCmb)
            End If

Fix:
        Else
            Me.BttnDelete.Enabled = False
            Me.DsSUPPLIER_PAYMENT1.Clear()
        End If
    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbEmployee.GotFocus, CmbGroup.GotFocus, CmbBankAccount.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbEmployee.LostFocus, CmbGroup.LostFocus, CmbBankAccount.LostFocus
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

        Me.DsSUPPLIER_PAYMENT1.Clear()
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.TxtDate.Focus()

        Me.Default_Setting()
    End Sub

    Private Sub BttnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch.Click
        My.Forms.frmSEARCH_SUP_PAYMENT.ShowDialog(Me)
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click
        If Not Val(Me.LblID.Text) > 0 Then
            'FOR INSERT RECORD IN SUPPLIER PAYMENT
            If Me.TxtDate.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                If Me.TxtDate.Text = Nothing Then
                    Me.TxtDate.Focus()

                ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                    Me.CmbGroup.Focus()

                ElseIf Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
                    Me.CmbEmployee.Focus()

                ElseIf Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Then
                    Me.CmbSupplier.Focus()

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
                    Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, sCHEQUE_NO, sCHEQUE_TYPE, dCHEQUE_DATE, nCHECK_AMOUNT, sACCOUNT_CODE, nEMP_CODE, nLOGIN_ID, nBUSINESS_CODE, sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.TxtChequeNo.Text & "', '" & Me.TxtChequeType.Text & "', '" & CDate(Me.TxtChequeDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.TxtBankPmt.Text) & ",'" & Me.CmbBankAccount.SelectedItem.Col1 & "', " & Val(Me.CmbEmployee.SelectedItem.Col3) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ",'" & Me.TxtDescription.Text & "')")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()

                End If
                'PAYMENT MADE VIA CASH ONLY
            ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) > 0 Then
                If MsgBox("Do you want to save CASH: '" & Me.TxtCashPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                    Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, nCASH_AMOUNT, nEMP_CODE, nLOGIN_ID, nBUSINESS_CODE,sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ",'" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "'," & Val(Me.TxtCashPmt.Text) & ", " & Val(Me.CmbEmployee.SelectedItem.Col3) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ",'" & Me.TxtDescription.Text & "')")
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
                    Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_PAYMENT(nSUPPLIER_ID, dDATE, nCASH_AMOUNT, sCHEQUE_NO,sCHEQUE_TYPE, dCHEQUE_DATE, nCHECK_AMOUNT, sACCOUNT_CODE, nEMP_CODE, nLOGIN_ID, nBUSINESS_CODE,sDESCRIPTON) VALUES(" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.TxtCashPmt.Text) & ", '" & Me.TxtChequeNo.Text & "', '" & Me.TxtChequeType.Text & "', '" & CDate(Me.TxtChequeDate.Text).ToString("MM-dd-yyyy") & "', " & Val(Me.TxtBankPmt.Text) & ",'" & Me.CmbBankAccount.SelectedItem.Col1 & "', " & Val(Me.CmbEmployee.SelectedItem.Col3) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ",'" & Me.TxtDescription.Text & "')")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()
                End If

            ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) <= 0 Then
                MsgBox("Please enter Cash Value OR Bank Value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                Me.TxtCashPmt.Focus()
            End If


        ElseIf Val(Me.LblID.Text) > 0 Then

            'FOR UPDATE RECORD IN SUPPLIER PAYMENT
            If Me.TxtDate.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Then
                MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
                If Me.TxtDate.Text = Nothing Then
                    Me.TxtDate.Focus()

                ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                    Me.CmbGroup.Focus()

                ElseIf Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
                    Me.CmbEmployee.Focus()

                ElseIf Me.CmbSupplier.SelectedIndex = -1 Or Me.CmbSupplier.Text = Nothing Then
                    Me.CmbSupplier.Focus()

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
                    Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_PAYMENT SET nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', sCHEQUE_NO='" & Me.TxtChequeNo.Text & "', sCHEQUE_TYPE='" & Me.TxtChequeType.Text & "', dCHEQUE_DATE='" & CDate(Me.TxtChequeDate.Text).ToString("MM-dd-yyyy") & "', nCHECK_AMOUNT=" & Val(Me.TxtBankPmt.Text) & ", sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', nEMP_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.TxtDescription.Text & "', nCASH_AMOUNT=0 WHERE nID=" & Val(Me.LblID.Text) & "")
                    Me.BttnSave.Enabled = False
                    Me.BttnNew.Focus()

                End If

                'PAYMENT MADE VIA CASH ONLY
            ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) > 0 Then
                If MsgBox("Do you want to update CASH: '" & Me.TxtCashPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Update?") = MsgBoxResult.Yes Then
                    Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_PAYMENT SET nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', sCHEQUE_NO=NULL, sCHEQUE_TYPE=NULL, dCHEQUE_DATE=NULL, nCHECK_AMOUNT=NULL, sACCOUNT_CODE=NULL, nEMP_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.TxtDescription.Text & "', nCASH_AMOUNT=" & Val(Me.TxtCashPmt.Text) & " WHERE nID=" & Val(Me.LblID.Text) & "")
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
                    Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_PAYMENT SET nSUPPLIER_ID=" & Val(Me.CmbSupplier.SelectedItem.Col3) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', sCHEQUE_NO='" & Me.TxtChequeNo.Text & "', sCHEQUE_TYPE='" & Me.TxtChequeType.Text & "', dCHEQUE_DATE='" & CDate(Me.TxtChequeDate.Text).ToString("MM-dd-yyyy") & "', nCHECK_AMOUNT=" & Val(Me.TxtBankPmt.Text) & ", sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', nEMP_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & ", sDESCRIPTON='" & Me.TxtDescription.Text & "', nCASH_AMOUNT=" & Val(Me.TxtCashPmt.Text) & " WHERE nID=" & Val(Me.LblID.Text) & "")
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
            Me.asDelete.DeleteValue("DELETE FROM SUPPLIER_PAYMENT WHERE nID=" & Val(Me.LblID.Text) & "")
            Me.LblID.Text = Nothing
        End If
    End Sub
#End Region

#Region "Sub and Functions"
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

    Private Sub Default_Setting()
        On Error GoTo Fix
        Dim StrCMB As String

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("GROUP").ToString
        Me.CmbGroup.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbGroup.SelectedIndex = Me.CmbGroup.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("P_MAN").ToString
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