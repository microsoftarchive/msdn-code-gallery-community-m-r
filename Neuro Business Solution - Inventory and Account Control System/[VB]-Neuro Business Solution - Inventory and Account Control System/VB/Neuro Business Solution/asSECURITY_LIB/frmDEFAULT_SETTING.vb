Public Class frmDEFAULT_SETTING

#Region "VARIABLES"
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
    Private Sub frmDEFAULT_SETTING_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO1.V_CLIENT_INFO)
        Me.daLUP_AREA.Fill(Me.DsLUP_AREA1.V_LUP_AREA)
        Me.daLUP_BANK.Fill(Me.DsLUP_BANK_NEW1.V_LUP_BANK)
        Me.daLUP_BUSINESS_GROUP.Fill(Me.DsLUP_BUSINESS_GROUP_NEW1.V_BUSINESS_GROUP)
        Me.daLUP_CLIENT_GD.Fill(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD)
        Me.daLUP_CLIENT_TYPE.Fill(Me.DsLUP_CLIENT_TYPE1.LUP_CLIENT_TYPE)
        Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE1.V_EMPLOYEE_INFO)
        Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE11.V_EMPLOYEE_INFO)
        Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE2.V_EMPLOYEE_INFO)
        Me.daLUP_EMPLOYEE.Fill(Me.DsLUP_EMPLOYEE12.V_EMPLOYEE_INFO)
        Me.daLUP_EXPENSE_SUB.Fill(Me.DsLUP_EXPENSE_SUB1.V_LUP_EXPENSE_SUB)
        Me.daLUP_ROUTES.Fill(Me.DsLUP_ROUTES1.V_LUP_ROUTES)
        Me.daLUP_SHOP_CAT.Fill(Me.DsLUP_SHOP_CAT1.LUP_SHOP_CAT)
        Me.daLUP_ZONE.Fill(Me.DsLUP_ZONE1.LUP_ZONE)

        Me.daNS_DEFAULT.Fill(Me.DsNS_DEFAULT1.NS_DEFAULT)
        Dim StrCMB As String
        
        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("AREA").ToString
        Me.cmbAREA.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbAREA.SelectedIndex = Me.cmbAREA.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("BANK_ACC").ToString
        Me.cmbBANK_ACC.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbBANK_ACC.SelectedIndex = Me.cmbBANK_ACC.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("CLIENT").ToString
        Me.cmbCLIENT.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbCLIENT.SelectedIndex = Me.cmbCLIENT.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("CLIENT_CAT").ToString
        Me.cmbCLIENT_CAT.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbCLIENT_CAT.SelectedIndex = Me.cmbCLIENT_CAT.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("CLIENT_GD").ToString
        Me.cmbCLIENT_GD.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbCLIENT_GD.SelectedIndex = Me.cmbCLIENT_GD.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("CLIENT_TYPE").ToString
        Me.cmbCLIENT_TYPE.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbCLIENT_TYPE.SelectedIndex = Me.cmbCLIENT_TYPE.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("D_MAN").ToString
        Me.cmbD_MAN.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbD_MAN.SelectedIndex = Me.cmbD_MAN.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("EXP_SUB_HEAD").ToString
        Me.cmbEXP_SUB_HEAD.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbEXP_SUB_HEAD.SelectedIndex = Me.cmbEXP_SUB_HEAD.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("GROUP").ToString
        Me.cmbGROUP.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbGROUP.SelectedIndex = Me.cmbGROUP.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("P_MAN").ToString
        Me.cmbP_PERSON.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbP_PERSON.SelectedIndex = Me.cmbP_PERSON.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("PRINTER").ToString
        Me.cmbPRINTER.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbPRINTER.SelectedIndex = Me.cmbPRINTER.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("R_MAN").ToString
        Me.cmbR_PERSON.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbR_PERSON.SelectedIndex = Me.cmbR_PERSON.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("ROUTE").ToString
        Me.cmbROUTE.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbROUTE.SelectedIndex = Me.cmbROUTE.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("RPT_TITLE").ToString
        Me.cmbTITLE.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbTITLE.SelectedIndex = Me.cmbTITLE.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("RPT_WARRANTY").ToString
        Me.cmbWARRANTY.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbWARRANTY.SelectedIndex = Me.cmbWARRANTY.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("S_MAN").ToString
        Me.cmbS_MAN.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbS_MAN.SelectedIndex = Me.cmbS_MAN.FindString(StrCMB)
        End If

        StrCMB = Me.DsNS_DEFAULT1.NS_DEFAULT.Item(0).Item("ZONE").ToString
        Me.cmbZONE.SelectedIndex = -1
        If Not StrCMB = Nothing Then
            Me.cmbZONE.SelectedIndex = Me.cmbZONE.FindString(StrCMB)
        End If
    End Sub

    Private Sub frmDEFAULT_SETTING_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAREA.GotFocus, cmbBANK_ACC.GotFocus, cmbCLIENT.GotFocus, cmbCLIENT_CAT.GotFocus, cmbCLIENT_GD.GotFocus, cmbCLIENT_TYPE.GotFocus, cmbD_MAN.GotFocus, cmbEXP_SUB_HEAD.GotFocus, cmbGROUP.GotFocus, cmbP_PERSON.GotFocus, cmbPRINTER.GotFocus, cmbR_PERSON.GotFocus, cmbROUTE.GotFocus, cmbS_MAN.GotFocus, cmbZONE.GotFocus, cmbTITLE.GotFocus, cmbWARRANTY.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub

    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAREA.LostFocus, cmbBANK_ACC.LostFocus, cmbCLIENT.LostFocus, cmbCLIENT_CAT.LostFocus, cmbCLIENT_GD.LostFocus, cmbCLIENT_TYPE.LostFocus, cmbD_MAN.LostFocus, cmbEXP_SUB_HEAD.LostFocus, cmbGROUP.LostFocus, cmbP_PERSON.LostFocus, cmbPRINTER.LostFocus, cmbR_PERSON.LostFocus, cmbROUTE.LostFocus, cmbS_MAN.LostFocus, cmbZONE.LostFocus, cmbTITLE.LostFocus, cmbWARRANTY.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click
        If Me.DsNS_DEFAULT1.NS_DEFAULT.Count > 0 Then
            If MsgBox("Do you want to UPDATE 'DEFAULT SETTING'?", MsgBoxStyle.Question + vbYesNo, "(NS) - Update?") = MsgBoxResult.Yes Then
                Me.asUpdate.UpdateValueIN("UPDATE NS_DEFAULT SET sBUSINESS_GP='" & Me.cmbGROUP.Text & "', sBANK_ACC='" & Me.cmbBANK_ACC.Text & "', sS_MAN='" & Me.cmbS_MAN.Text & "', sP_MAN='" & Me.cmbP_PERSON.Text & "', sD_MAN='" & Me.cmbD_MAN.Text & "', sR_MAN='" & Me.cmbR_PERSON.Text & "', sCLIENT='" & Me.cmbCLIENT.Text & "', sCLIENT_TYPE='" & Me.cmbCLIENT_TYPE.Text & "', sCLIENT_CAT='" & Me.cmbCLIENT_CAT.Text & "', sCLIENT_GD'" & Me.cmbCLIENT_GD.Text & "', sZONE='" & Me.cmbZONE.Text & "', sROUTE='" & Me.cmbROUTE.Text & "', sAREA='" & Me.cmbAREA.Text & "', sEXP_SUB_HEAD='" & Me.cmbEXP_SUB_HEAD.Text & "', sPRINTER='" & Me.cmbPRINTER.Text & "', sREPORT_TITLE='" & Me.cmbTITLE.Text & "', sREPORT_WARRANTY='" & Me.cmbWARRANTY.Text & "' WHERE nID=1")
            End If
        Else
            If MsgBox("Do you want to save 'SETTING'?", MsgBoxStyle.Question + vbYesNo, "(NS) - Save?") = MsgBoxResult.Yes Then
                Me.asInsert.SaveValueIN("INSERT INTO NS_DEFAULT (sBUSINESS_GP, sBANK_ACC, sS_MAN, sP_MAN, sD_MAN, sR_MAN, sCLIENT, sCLIENT_TYPE, sCLIENT_CAT, sCLIENT_GD, sZONE, sROUTE, sAREA, sEXP_SUB_HEAD, sPRINTER, sREPORT_TITLE, sREPORT_WARRANTY) VALUES ('" & Me.cmbGROUP.Text & "', '" & Me.cmbBANK_ACC.Text & "', '" & Me.cmbS_MAN.Text & "', '" & Me.cmbP_PERSON.Text & "', '" & Me.cmbD_MAN.Text & "', '" & Me.cmbR_PERSON.Text & "', '" & Me.cmbCLIENT.Text & "', '" & Me.cmbCLIENT_TYPE.Text & "', '" & Me.cmbCLIENT_CAT.Text & "', '" & Me.cmbCLIENT_GD.Text & "', '" & Me.cmbZONE.Text & "', '" & Me.cmbROUTE.Text & "', '" & Me.cmbAREA.Text & "', '" & Me.cmbEXP_SUB_HEAD.Text & "', '" & Me.cmbPRINTER.Text & "', '" & Me.cmbTITLE.Text & "', '" & Me.cmbWARRANTY.Text & "')")
            End If
        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"

#End Region
End Class