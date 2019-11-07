Public Class frmRptCASH_CHALLAN

    Dim AsNum As New AssNumPress

#Region "Control Events Section"
    Private Sub frmRptCASH_CHALLAN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.AsNum.EnterTab(e)
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnView.Click
        If Me.RBttnAllClient.Checked = True Then
            If Me.ChkCredit.Checked = True Then


                'Without Credit Between
            ElseIf Me.ChkCredit.Checked = False Then
                'Dim Rpt As New rptCASH_CHALLAN_COMPLETE_DT
                Dim Rpt As New rptCASH_CHALLAN_COMPLETE_PREV_DT
                Dim Frm As New frmRPT
                Try
                    Frm.CRV.ReportSource = Rpt
                    'Frm.CRV.SelectionFormula = "{SV_CLIENT_BALANCE_TOT.CLIENT_BAL}<>0"
                    Frm.CRV.SelectionFormula = "ROUND({@TotBal})<>0"
                    Frm.Text = "Cash Challan All Client"
                    Frm.MdiParent = Me.ParentForm
                    Frm.Show()
                    Frm.Activate()
                Catch Ex As Exception
                    MsgBox(Ex.Message)
                End Try
            End If

        ElseIf Me.RBttnArea.Checked = True Then
            If Me.ChkCredit.Checked = True Then


                'Without Credit Between
            ElseIf Me.ChkCredit.Checked = False Then
                Dim frm As New frmRPT1
                frm.STATUS = "CASH_CHALLAN_AREA"
                frm.PnlArea.Visible = True
                frm.Text = "Cash Challan Area Wise"
                frm.MdiParent = Me.ParentForm
                frm.Show()
                frm.Activate()
            End If
        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

End Class