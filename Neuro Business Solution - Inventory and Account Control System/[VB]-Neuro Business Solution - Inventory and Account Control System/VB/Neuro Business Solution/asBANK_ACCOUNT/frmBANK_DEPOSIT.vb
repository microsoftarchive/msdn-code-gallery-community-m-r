Imports SDS = System.Data.SqlClient
Public Class frmBANK_DEPOSIT

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
    Private Sub frmBANK_DEPOSIT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Employee()
        Me.FillComboBox_Group()
        Me.FillComboBox_BankAccount()
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")

    End Sub

    Private Sub frmBANK_DEPOSIT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDepAmt.GotFocus, TxtDepositSlipNo.GotFocus, TxtDate.GotFocus, TxtDescription.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDepAmt.LostFocus, TxtDepositSlipNo.LostFocus, TxtDate.LostFocus, TxtDescription.LostFocus
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
    Private Sub TxtDepAmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDepAmt.Leave
        If sender.Text = Nothing Or Val(sender.Text) = 0 Then
            sender.Text = "0.00"
        End If
    End Sub
    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtDepAmt.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub
#End Region

#Region "Lable Control"
    Private Sub LblID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblID.TextChanged
        If Me.LblID.Text.Length > 0 Then
            Me.BttnDelete.Enabled = True
            Dim Str1 As String = "SELECT ID, BANK_ACC, SLIP_NO, CONVERT(NUMERIC(18,2), DEPOSIT_AMT) AS DEPOSIT_AMT, dDATE, sDESC, GROUP_NAME, USER_NAME, GROUP_ID, EMP_NAME FROM V_BANK_DEPOSITS WHERE ID=" & Val(Me.LblID.Text) & " ORDER BY ID DESC"
            ' Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daBANK_DEPOSIT = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsBANK_DEP_NEW1.Clear()
            Me.daBANK_DEPOSIT.Fill(Me.DsBANK_DEP_NEW1.V_BANK_DEPOSITS)
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
            Me.DsBANK_DEP_NEW1.Clear()
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
    Private Sub CmbGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbGroup.SelectedIndexChanged

        Try
            Dim Str1 As String = "SELECT GROUP_ID, CONVERT(NUMERIC(18,2),CASH_BAL) AS CASH_BAL FROM SV_CASH_BALANCE WHERE GROUP_ID=" & Val(Me.CmbGroup.SelectedItem.Col3) & ""
            ' Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
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
        'Me.TxtDepAmt.Text = "0.00"

        'Me.CmbBankAccount.SelectedIndex = -1
        'Me.TxtDepositSlipNo.Text = Nothing

        'Me.TxtDescription.Text = Nothing

        Me.BttnSave.Enabled = True

        Me.LblID.Text = Nothing

        Me.DsBANK_DEP_NEW1.Clear()
        Me.TxtDate.Text = Date.Now.ToString("dd-MMM-yyyy")
        Me.TxtDate.Focus()
    End Sub

    Private Sub BttnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch.Click
        My.Forms.frmSEARCH_BANK_DEPOSIT.ShowDialog(Me)
    End Sub

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click
        If Me.TxtDate.Text = Nothing Or Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Or Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Or Me.CmbBankAccount.SelectedIndex = -1 Or Me.CmbBankAccount.Text = Nothing Or Val(Me.TxtDepAmt.Text) <= 0 Or Me.TxtDepositSlipNo.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtDate.Text = Nothing Then
                Me.TxtDate.Focus()

            ElseIf Me.CmbGroup.SelectedIndex = -1 Or Me.CmbGroup.Text = Nothing Then
                Me.CmbGroup.Focus()

            ElseIf Me.CmbEmployee.SelectedIndex = -1 Or Me.CmbEmployee.Text = Nothing Then
                Me.CmbEmployee.Focus()

            ElseIf Me.CmbBankAccount.SelectedIndex = -1 Or Me.CmbBankAccount.Text = Nothing Then
                Me.CmbBankAccount.Focus()

            ElseIf Val(Me.TxtDepAmt.Text) <= 0 Then
                Me.TxtDepAmt.Focus()

            ElseIf Me.TxtDepositSlipNo.Text = Nothing Then
                Me.TxtDepositSlipNo.Focus()

            End If

        ElseIf Not Val(Me.LblID.Text) > 0 Then
            'FOR INSERT RECORD IN BANK DEPOSIT
            If MsgBox("Do you want to save in BANK DEPOSIT: '" & Me.TxtDepAmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                Me.asInsert.SaveValueIN("INSERT INTO BANK_DEPOSIT(sACCOUNT_CODE, sSLIP_NO, nAMOUNT, dDATE, sDESC,nEMPLOYEE_CODE, nLOGIN_ID, nBUSINESS_CODE) VALUES('" & Me.CmbBankAccount.SelectedItem.Col1 & "', '" & Me.TxtDepositSlipNo.Text & "', " & Val(Me.TxtDepAmt.Text) & ", '" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', '" & Me.TxtDescription.Text & "', " & Val(Me.CmbEmployee.SelectedItem.Col3) & ",10," & Val(Me.CmbGroup.SelectedItem.Col3) & ")")
                Me.BttnSave.Enabled = False
                Me.BttnNew.Focus()

            End If

        ElseIf Val(Me.LblID.Text) > 0 Then
            'FOR UPDATE RECORD IN BANK DEPOSIT
            If MsgBox("Do you want to update in BANK DEPOSIT: '" & Me.TxtDepAmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Update?") = MsgBoxResult.Yes Then
                Me.asUpdate.UpdateValueIN("UPDATE BANK_DEPOSIT SET sACCOUNT_CODE='" & Me.CmbBankAccount.SelectedItem.Col1 & "', sSLIP_NO='" & Me.TxtDepositSlipNo.Text & "', nAMOUNT=" & Val(Me.TxtDepAmt.Text) & ", dDATE='" & CDate(Me.TxtDate.Text).ToString("MM-dd-yyyy") & "', sDESC='" & Me.TxtDescription.Text & "', nEMPLOYEE_CODE=" & Val(Me.CmbEmployee.SelectedItem.Col3) & ", nLOGIN_ID=10, nBUSINESS_CODE=" & Val(Me.CmbGroup.SelectedItem.Col3) & " WHERE nID=" & Val(Me.LblID.Text) & "")
                Me.BttnSave.Enabled = False
                Me.BttnNew.Focus()

            End If

        End If

    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

    Private Sub BttnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnDelete.Click
        If MsgBox("Are you sure to Delete this Deposit Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Deletion!") = MsgBoxResult.Yes Then
            Me.asDelete.DeleteValue("DELETE FROM BANK_DEPOSIT WHERE nID=" & Val(Me.LblID.Text) & "")
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