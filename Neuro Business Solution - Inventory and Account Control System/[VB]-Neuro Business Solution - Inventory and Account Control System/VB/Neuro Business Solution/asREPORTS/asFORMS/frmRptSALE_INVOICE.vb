Public Class frmRptSALE_INVOICE

    Dim AsNum As New AssNumPress

#Region "Control Events Section"
    Private Sub frmRptSALE_INVOICE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.AsNum.EnterTab(e)
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnView.Click
        
        Dim Frm As New frmRPT1
        Try
            If Me.RBttnInvoice.Checked = True Then
                Frm.STATUS = "SALE_INVOICE_NO"
                Frm.Text = "Sales Invoice No-to-No"
                Frm.PnlInvoice.Visible = True

            ElseIf Me.RBttnDate.Checked = True Then
                Frm.STATUS = "SALE_INVOICE_DATE"
                Frm.Text = "Sales Invoice Date Wise"
                Frm.PnlDate.Visible = True
                Frm.TxtDateFrom3.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
                Frm.TxtDateTo3.Text = Date.Now.ToString("dd-MMM-yyyy")

            ElseIf Me.RBttnArea_Date.Checked = True Then
                Frm.STATUS = "SALE_INVOICE_AREA_DATE"
                Frm.Text = "Sales Invoice Area and Date Wise"
                Frm.PnlArea_Date.Visible = True
                Frm.TxtDateFrom4.Text = CDate("01-" & Date.Now.Month & "-" & Date.Now.Year).ToString("dd-MMM-yyyy")
                Frm.TxtDateTo4.Text = Date.Now.ToString("dd-MMM-yyyy")


            Else
                Exit Sub
            End If

            Frm.MdiParent = Me.ParentForm
            Frm.Show()
            Frm.Activate()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try

    End Sub

    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

End Class