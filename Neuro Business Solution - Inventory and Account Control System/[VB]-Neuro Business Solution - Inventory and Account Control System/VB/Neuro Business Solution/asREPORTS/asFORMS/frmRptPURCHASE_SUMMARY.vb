Public Class frmRptPURCHASE_SUMMARY

    Dim AsNum As New AssNumPress

#Region "Control Events Section"
    Private Sub frmRptSALESMAN_SALE_SUMMARY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.AsNum.EnterTab(e)
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnView.Click
        If Me.RBttnOverall.Checked = True Then
            Dim frm As New frmRPT1
            frm.STATUS = "PURCHASE_SUMMARY_OVERALL"
            frm.PnlDate.Visible = True
            frm.TxtDateFrom3.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
            frm.TxtDateTo3.Text = Date.Now.ToString("dd-MMM-yyyy")
            frm.Text = "Purchase Summary Overall"
            frm.MdiParent = Me.ParentForm
            frm.Show()
            frm.Activate()

        ElseIf Me.RBttnSupplier.Checked = True Then
            Dim frm As New frmRPT1
            frm.STATUS = "PURCHASE_SUMMARY_SUPPLIER"
            frm.PnlSupplierDate.Visible = True
            frm.TxtDateFrom8.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
            frm.TxtDateTo8.Text = Date.Now.ToString("dd-MMM-yyyy")
            frm.Text = "Purchase Summary Supplier Wise"
            frm.MdiParent = Me.ParentForm
            frm.Show()
            frm.Activate()
        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

End Class