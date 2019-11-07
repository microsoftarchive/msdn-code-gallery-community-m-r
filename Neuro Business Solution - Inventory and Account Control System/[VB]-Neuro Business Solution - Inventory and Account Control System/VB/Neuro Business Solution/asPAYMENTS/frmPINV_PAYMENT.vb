Imports SDS = System.Data.SqlClient
Public Class frmPINV_PAYMENT

#Region "VARIABLES"
    Dim asConn As New AssConn
    Public BnkACC As String
    Dim asNum As New AssNumPress

#End Region

#Region "FORM CONTROL"

    Private Sub frmPINV_PAYMENT_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.TxtCashPmt.Focus()
    End Sub
    Private Sub frmPINV_PAYMENT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_BankAccount()

        Me.daNS_DEFAULT.Fill(Me.DsNS_DEFAULT1.NS_DEFAULT)

        Me.Default_Setting()
    End Sub

    Private Sub frmPINV_PAYMENT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtBankPmt.GotFocus, TxtCashPmt.GotFocus, TxtChequeDate.GotFocus, TxtChequeNo.GotFocus, TxtChequeType.GotFocus, TxtDescription.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtBankPmt.LostFocus, TxtCashPmt.LostFocus, TxtChequeDate.LostFocus, TxtChequeNo.LostFocus, TxtChequeType.LostFocus, TxtDescription.LostFocus
        CType(sender, TextBox).BackColor = Color.White
        Dim Ctrl As Control = sender
        Try
            Select Case Ctrl.Name
                Case "TxtChequeDate"
                    If sender.TextLength > 0 Then
                        sender.Text = CDate(sender.text).ToString("dd-MMM-yyyy")
                    End If

            End Select
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtBankPmt.KeyPress, TxtCashPmt.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    'Private Sub TxtCashPmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCashPmt.TextChanged, TxtBankPmt.TextChanged
    '    frmPURCHASE.TxtPayment.Text = Val(Me.TxtCashPmt.Text) + Val(Me.TxtBankPmt.Text)
    '    '        On Error GoTo Fix
    '    '        Me.TxtCashPmt.Text = Decimal.Round(CDec(Me.TxtCashPmt.Text), 2)
    '    '        Me.TxtBankPmt.Text = Decimal.Round(CDec(Me.TxtBankPmt.Text), 2)
    '    'Fix:
    'End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBankAccount.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBankAccount.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "Button Control"

    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        'PAYMENT MADE VIA BANK ONLY
        If Val(Me.TxtBankPmt.Text) > 0 And Val(Me.TxtCashPmt.Text) <= 0 Then
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

            ElseIf MsgBox("Do you want to save BANK: '" & Me.TxtBankPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                frmPURCHASE.TxtPayment.Text = Val(Me.TxtBankPmt.Text)
                frmPURCHASE.BANK_AMT = Val(Me.TxtBankPmt.Text)

                frmPURCHASE.BANK_ACC = Me.CmbBankAccount.SelectedItem.Col1
                frmPURCHASE.CHEQ_TYPE = Me.TxtChequeType.Text
                frmPURCHASE.CHEQ_NO = Me.TxtChequeNo.Text
                frmPURCHASE.CHEQ_DATE = Me.TxtChequeDate.Text
                frmPURCHASE.DESCRIPTION = Me.TxtDescription.Text

                frmPURCHASE.BttnSave.Focus()

                frmPURCHASE.BANK_PAY = True

                frmPURCHASE.CASH_PAY = False
                frmPURCHASE.BOTH_PAY = False


                Me.Close()

            End If

            'PAYMENT MADE VIA CASH ONLY
        ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) > 0 Then
            If MsgBox("Do you want to save CASH: '" & Me.TxtCashPmt.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                frmPURCHASE.TxtPayment.Text = Val(Me.TxtCashPmt.Text)
                frmPURCHASE.CASH_AMT = Val(Me.TxtCashPmt.Text)
                frmPURCHASE.DESCRIPTION = Me.TxtDescription.Text

                frmPURCHASE.BttnSave.Focus()

                frmPURCHASE.CASH_PAY = True

                frmPURCHASE.BANK_PAY = False
                frmPURCHASE.BOTH_PAY = False
                Me.Close()
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

                frmPURCHASE.TxtPayment.Text = Val(Me.TxtCashPmt.Text) + Val(Me.TxtBankPmt.Text)
                frmPURCHASE.BANK_AMT = Val(Me.TxtBankPmt.Text)
                frmPURCHASE.CASH_AMT = Val(Me.TxtCashPmt.Text)

                frmPURCHASE.BANK_ACC = Me.CmbBankAccount.SelectedItem.Col1
                frmPURCHASE.CHEQ_TYPE = Me.TxtChequeType.Text
                frmPURCHASE.CHEQ_NO = Me.TxtChequeNo.Text
                frmPURCHASE.CHEQ_DATE = Me.TxtChequeDate.Text
                frmPURCHASE.DESCRIPTION = Me.TxtDescription.Text

                frmPURCHASE.BttnSave.Focus()

                frmPURCHASE.BOTH_PAY = True

                frmPURCHASE.CASH_PAY = False
                frmPURCHASE.BANK_PAY = False

                Me.Close()
            End If

        ElseIf Val(Me.TxtBankPmt.Text) <= 0 And Val(Me.TxtCashPmt.Text) <= 0 Then
            MsgBox("Please enter Cash Value OR Bank Value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            Me.TxtCashPmt.Focus()

        End If

    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        If Val(Me.TxtCashPmt.Text) > 0 Or Val(Me.TxtBankPmt.Text) > 0 Then
            Me.BttnSave_Click(sender, New System.EventArgs)
        Else
            Me.Close()
        End If
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
    End Sub

    Private Sub Default_Setting()
        On Error GoTo Fix
        Dim StrCMB As String

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("BANK_ACC").ToString
        Me.CmbBankAccount.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.CmbBankAccount.SelectedIndex = Me.CmbBankAccount.FindString(StrCMB)
        End If
Fix:
    End Sub

#End Region

End Class