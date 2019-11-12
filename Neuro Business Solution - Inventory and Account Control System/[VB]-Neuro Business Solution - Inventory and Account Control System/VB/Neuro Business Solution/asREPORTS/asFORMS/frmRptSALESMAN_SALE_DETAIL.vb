Public Class frmRptSALESMAN_SALE_DETAIL

    Dim AsNum As New AssNumPress

#Region "Control Events Section"
    Private Sub frmRptSALESMAN_SALE_DETAIL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.AsNum.EnterTab(e)
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnView.Click
        If Me.RBttnOverall.Checked = True Then
            Dim frm As New frmRPT1
            frm.STATUS = "SALE_DETAIL_SALEMAN"
            frm.pnlSALEMAN.Visible = True
            frm.Text = "Sale Detail Saleman Wise"
            frm.MdiParent = Me.ParentForm
            frm.Show()
            frm.Activate()

        ElseIf Me.RBttnDate.Checked = True Then
            Dim frm As New frmRPT1
            frm.STATUS = "SALE_DETAIL_SALEMAN_DATE"
            frm.pnlSALEMAN_DATE.Visible = True
            frm.TxtDateFrom6.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
            frm.TxtDateTo6.Text = Date.Now.ToString("dd-MMM-yyyy")
            frm.Text = "Sale Detail Saleman / Date Wise"
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