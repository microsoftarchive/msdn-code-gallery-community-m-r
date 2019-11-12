Public Class frmRptSTOCK_HAND

    Dim AsNum As New AssNumPress

#Region "Control Events Section"
    Private Sub frmRptSTOCK_HAND_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.AsNum.EnterTab(e)
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnView.Click

        If Me.RBttnAllItems.Checked = True Then
            'Batch Wise
            If Me.ChkBatch.Checked = True Then
                'Dim Rpt As New rptCASH_CHALLAN_COMPLETE_DT
                Dim Rpt As New rptSTOCK_HAND_COMPLETE_BATCH_DT
                Dim Frm As New frmRPT
                Try
                    Frm.CRV.ReportSource = Rpt
                    'Frm.CRV.SelectionFormula = "ROUND({@TotBal})<>0"
                    Frm.Text = "Stock Report All Items Batch Wise"
                    Frm.CRV.SelectionFormula = "{SV_STOCK_BALANCE.STK_BAL}<>0"
                    Frm.MdiParent = Me.ParentForm
                    Frm.Show()
                    Frm.Activate()
                Catch Ex As Exception
                    MsgBox(Ex.Message)
                End Try


            ElseIf Me.ChkBatch.Checked = False Then
                Dim Rpt As New rptSTOCK_HAND_COMPLETE_DT
                Dim Frm As New frmRPT
                Try
                    Frm.CRV.ReportSource = Rpt
                    'Frm.CRV.SelectionFormula = "ROUND({@TotBal})<>0"
                    Frm.Text = "Stock Report All Items"
                    Frm.CRV.SelectionFormula = "{SV_STOCK_BAL.STK_BAL}<>0"
                    Frm.MdiParent = Me.ParentForm
                    Frm.Show()
                    Frm.Activate()
                Catch Ex As Exception
                    MsgBox(Ex.Message)
                End Try
            End If

        ElseIf Me.RBttnCompany.Checked = True Then
            'Batch Wise
            If Me.ChkBatch.Checked = True Then
                Dim frm As New frmRPT1
                frm.STATUS = "STOCK_HAND_COMPANY_BATCH"
                frm.pnlCOMPANY.Visible = True
                frm.Text = "Stock in Hand Company/Batch Wise"
                frm.MdiParent = Me.ParentForm
                frm.Show()
                frm.Activate()

            ElseIf Me.ChkBatch.Checked = False Then
                Dim frm As New frmRPT1
                frm.STATUS = "STOCK_HAND_COMPANY"
                frm.pnlCOMPANY.Visible = True
                frm.Text = "Stock in Hand Company Wise"
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