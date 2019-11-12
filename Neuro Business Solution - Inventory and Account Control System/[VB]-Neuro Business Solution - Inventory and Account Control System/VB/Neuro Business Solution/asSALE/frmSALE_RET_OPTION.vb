Public Class frmSALE_RET_OPTION

    Private Sub BttnWholeInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnWholeInvoice.Click
        With My.Forms.frmSALE_RETURN_WHOLE_INV
            .MdiParent = Me.ParentForm
            .Show()
            .Activate()
            .WindowState = FormWindowState.Normal
        End With
    End Sub
    Private Sub BttnPartialInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPartialInvoice.Click
        On Error GoTo Fix
        With My.Forms.frmSEARCH_S_INV
            .TxtClient.Text = Nothing
            .TxtInvoice.Text = Nothing
            .TxtDateFrom.Text = Nothing
            .TxtDateTo.Text = Nothing
            .FrmStr = "SALE_RET_PARTIAL"

            .MdiParent = Me.ParentForm
            .Show()
            .Activate()
            .WindowState = FormWindowState.Normal
        End With

Fix:
    End Sub

    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub


End Class