Imports SDS = System.Data.SqlClient
Public Class frmBANK_WITHDRAWAL

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
    Private Sub frmBANK_WITHDRAWAL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Employee()
        Me.FillComboBox_Group()
        Me.FillComboBox_BankAccount()
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")

    End Sub

    Private Sub frmBANK_WITHDRAWAL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtWidAmt.GotFocus, TxtChequeNo.GotFocus, TxtDate.GotFocus, TxtDescription.GotFocus, TxtChequeType.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtWidAmt.LostFocus, TxtChequeNo.LostFocus, TxtDate.LostFocus, TxtDescription.LostFocus, TxtChequeType.LostFocus
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
    Private Sub TxtWidAmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtWidAmt.Leave
        If sender.Text = Nothing Or Val(sender.Text) = 0 Then
            sender.Text = "0.00"
        End If
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtWidAmt.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub
#End Region

#Region "Lable Control"
    Private Sub LblID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblID.TextChanged
        If Me.LblID.Text.Length > 0 Then
            Me.BttnDelete.Enabled = True
            Dim Str1 As String = "SELECT ID, CHQ_NO, CHQ_TYPE, CONVERT(NUMERIC(18,2), BANK_AMT) AS BANK_AMT, dDATE, sDESC, BANK_ACC, USER_NAME, GROUP_NAME, GROUP_ID, EMP_NAME FROM V_BANK_WITHDRAWALS WHERE ID=" & Val(Me.LblID.Text) & " ORDER BY ID DESC"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daBANK_WITHDRAWAL = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsBANK_WITHDRAWAL1.Clear()
            Me.daBANK_WITHDRAWAL.Fill(Me.DsBANK_WITHDRAWAL1.V_BANK_WITHDRAWALS)
            Dim StrCmb As String

            On Error GoTo Fix

            StrCmb = Me.CmbEmployee.Text
            Me.CmbEmployee.SelectedIndex = -1
            Me.CmbEmployee.SelectedIndex = Me.CmbEmployee.FindString(StrCmb)

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
            Me.DsBANK_WITHDRAWAL1.Clear()
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

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        'Me.TxtDate.Text = Nothing
        'Me.CmbGroup.SelectedIndex = -1
        'Me.CmbEmployee.SelectedIndex = -1
        'Me.TxtWidAmt.Text = "0.00"

        'Me.CmbBankAccount.SelectedIndex = -1
        'Me.TxtChequeNo.Text = Nothing
        'Me.TxtChequeType.Text = Nothing

        'Me.TxtDescription.Text = Nothing

        Me.BttnSave.Enabled = True

        Me.LblID.Text = Nothing

        Me.DsBANK_WITHDRAWAL1.Clear()
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.TxtDate.Focus()
    End Sub

    Private Sub BttnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch.Click
        My.Forms.frmSEARCH_BANK_WITHDRAWAL.ShowDialog(Me)
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        If Me.TxtDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Or Me.CmbBankAccount.SelectedIndex = -1 Or Me.CmbBankAccount.Text = Nothing Or Val(Me.TxtWidAmt.Text) <= 0 Or Me.TxtChequeNo.Text = Nothing Or Me.TxtChequeType.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtDate.Text = Nothing Then
                Me.TxtDate.Focus()

            ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                Me.CmbGroup.Focus()

            ElseIf Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
                Me.CmbEmployee.Focus()

            ElseIf Me.CmbBankAccount.SelectedIndex = -1 Or Me.CmbBankAccount.Text = Nothing Then
                Me.CmbBankAccount.Focus()

            ElseIf Val(Me.TxtWidAmt.Text) <= 0 Then
                Me.TxtWidAmt.Focus()

            ElseIf Me.TxtChequeNo.Text = Nothing Then
                Me.TxtChequeNo.Focus()

            ElseIf Me.TxtChequeType.Text = Nothing Then
                Me.TxtChequeType.Focus()

            End If

        ElseIf Not Val(Me.LblID.Text) > 0 Then
            'FOR INSERT RECORD IN BANK WITHDRAWAL

            If MsgBox("Do you want to save in BANK WITHDRAWAL: '" & Me.TxtWidAmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                Me.asInsert.SaveValueIN("INSERT INTO BANK_WITHDRAWAL(sACCOUNT_CODE, sCEHQUE_NO, sCHEQUE_TYPE, nAMOUNT, dDATE, sDESC, nEMPLOYEE_CODE, nLOGIN_ID, nBUSINESS_CODE) VALUES('" & Me.CmbBankAccount.SelectedItem.Col1 & "', '" & Me.TxtChequeNo.Text & "', '" & Me.TxtChequeType.Text & "', " & Val(Me.TxtWidAmt.Text) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.TxtDescription.Text & "', " & Val(Me.CmbEmployee.SelectedItem.Col3) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ")")
                Me.BttnSave.Enabled = False
                Me.BttnNew.Focus()

            End If

        ElseIf Val(Me.LblID.Text) > 0 Then
            'FOR UPDATE RECORD IN BANK WITHDRAWAL
            If MsgBox("Do you want to update in BANK WITHDRAWAL: '" & Me.TxtWidAmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Update?") = MsgBoxResult.Yes Then
                Me.asUpdate.UpdateValueIN("UPDATE BANK_WITHDRAWAL SET sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', sCEHQUE_NO='" & Me.TxtChequeNo.Text & "', sCHEQUE_TYPE='" & Me.TxtChequeType.Text & "', nAMOUNT=" & Val(Me.TxtWidAmt.Text) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', sDESC='" & Me.TxtDescription.Text & "', nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & " WHERE nID=" & Val(Me.LblID.Text) & "")
                Me.BttnSave.Enabled = False
                Me.BttnNew.Focus()

            End If

        End If

    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

    Private Sub BttnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnDelete.Click
        If MsgBox("Are you sure to Delete this Withdrawal Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Deletion!") = MsgBoxResult.Yes Then
            Me.asDelete.DeleteValue("DELETE FROM BANK_WITHDRAWAL WHERE nID=" & Val(Me.LblID.Text) & "")
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
        Me.CmbGroup.SourceDataString = New String(2) {"sGROUP_DEALER", "sGROUP_NAME", "nID"}
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
#End Region

End Class