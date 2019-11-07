Imports SDS = System.Data.SqlClient
Public Class frmLOADPASS_BILLING

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
    Private Sub frmLOADPASS_BILLING_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Dim Str2 As String = "SELECT LPINV_NO, dDATE, CONVERT(NUMERIC(18, 2), TOTAL_BILL) AS TOTAL_BILL, CONVERT(NUMERIC(18, 2), DISC_RS) AS DISC_RS, DISC_PER, CONVERT(NUMERIC(18, 2), OTHER_DISC) AS OTHER_DISC, OTHER_DESC, CONVERT(NUMERIC(18, 2), NET_TOTAL) AS NET_TOTAL, VAN_NO, VAN_NAME, SALE_MAN, USER_NAME, GROUP_NAME, ROUTE, D_MAN, POSTED, REMARKS FROM V_MOBILE_ISSUE_MASTER"
        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)

        Me.daV_MOBILE_ISSUE_MASTER = New SDS.SqlDataAdapter(SqlCmd2)

        Me.BindingSource1.DataSource = Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER

        Me.DsV_MOBILE_ISSUE_MASTER1.Clear()
        Me.daV_MOBILE_ISSUE_MASTER.Fill(Me.DsV_MOBILE_ISSUE_MASTER1.V_MOBILE_ISSUE_MASTER)

        Me.BindingNavigatorMoveLastItem_Click(sender, e)
    End Sub

    Private Sub frmLOADPASS_BILLING_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "Lable Control"
    Private Sub LblPosted_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblPosted.TextChanged
        'If Me.LblPosted.Text = 0 Then
        '    Me.LblPosted.Text = "Not Posted"
        '    Me.LblPosted.BackColor = Color.Green

        'ElseIf Me.LblPosted.Text = 1 Then
        '    Me.LblPosted.Text = "Posted"
        '    Me.LblPosted.BackColor = Color.Red
        'End If
    End Sub
#End Region

#Region "TextBox Control"
    'Got and LostFocus
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDate.GotFocus, TxtGroup.GotFocus, TxtLoadPass.GotFocus, TxtRoute.GotFocus, TxtDMan.GotFocus, TxtSalesMan.GotFocus, TxtDiscRs.GotFocus, TxtTotalBill.GotFocus, TxtDiscPER.GotFocus, TxtOtherDisc.GotFocus, TxtOtherDesc.GotFocus, TxtNetTotal.GotFocus, TxtVanNo.GotFocus, TxtVanName.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtDate.LostFocus, TxtGroup.LostFocus, TxtLoadPass.LostFocus, TxtRoute.LostFocus, TxtDMan.LostFocus, TxtSalesMan.LostFocus, TxtDiscRs.LostFocus, TxtTotalBill.LostFocus, TxtDiscPER.LostFocus, TxtOtherDisc.LostFocus, TxtOtherDesc.LostFocus, TxtNetTotal.LostFocus, TxtVanNo.LostFocus, TxtVanName.LostFocus
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

            End If
        Catch ex As Exception
            sender.Text = Nothing
            sender.Focus()
        End Try
    End Sub
  
#End Region

#Region "Navigator"
    Private Sub BindingNavigatorMoveFirstItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMoveFirstItem.Click
        On Error GoTo Fix
        Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Position = 0
        Me.BindingNavigatorPositionItem.Text = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Position + 1
Fix:
    End Sub
    Private Sub BindingNavigatorMovePreviousItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMovePreviousItem.Click
        On Error GoTo Fix
        Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Position -= 1
        Me.BindingNavigatorPositionItem.Text = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Position + 1
Fix:
    End Sub
    Private Sub BindingNavigatorMoveNextItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMoveNextItem.Click
        On Error GoTo Fix
        Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Position += 1
        Me.BindingNavigatorPositionItem.Text = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Position + 1
Fix:
    End Sub
    Private Sub BindingNavigatorMoveLastItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BindingNavigatorMoveLastItem.Click
        On Error GoTo Fix
        Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Position = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Count - 1
        Me.BindingNavigatorPositionItem.Text = Me.BindingContext(Me.DsV_MOBILE_ISSUE_MASTER1, "V_MOBILE_ISSUE_MASTER").Position + 1
Fix:
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnBilling.Click
        My.Forms.frmSEARCH_EXPENSE_DETAIL.ShowDialog(Me)
    End Sub

    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

    Private Sub BttnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPostIT.Click
        If MsgBox("Are you sure to Delete this Payment Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Deletion!") = MsgBoxResult.Yes Then

        End If
    End Sub
#End Region

#Region "Sub and Functions"

#End Region

End Class