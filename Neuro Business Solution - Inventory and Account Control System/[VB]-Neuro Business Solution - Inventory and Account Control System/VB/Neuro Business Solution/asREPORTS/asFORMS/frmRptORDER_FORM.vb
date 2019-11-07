Public Class frmRptORDER_FORM

    Dim AsNum As New AssNumPress

#Region "Control Events Section"
    Private Sub frmRptORDER_FORM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.AsNum.EnterTab(e)
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnView.Click
        If Me.RBttnCompany.Checked = True Then
            Dim Rpt As New rptORDER_FORM_COMPANY
            Dim Frm As New frmRPT
            Try
                Frm.CRV.ReportSource = Rpt
                Frm.CRV.SelectionFormula = "{V_LUP_ITEM.sSTATUS}=True"
                Frm.Text = "Order Form Company Wise"
                Frm.MdiParent = Me.ParentForm
                Frm.Show()
                Frm.Activate()
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try

        ElseIf Me.RBttnCategory.Checked = True Then
            Dim Rpt As New rptORDER_FORM_CATEGORY
            Dim Frm As New frmRPT
            Try
                Frm.CRV.ReportSource = Rpt
                Frm.CRV.SelectionFormula = "{V_LUP_ITEM.sSTATUS}=True"
                Frm.Text = "Order Form Category Wise"
                Frm.MdiParent = Me.ParentForm
                Frm.Show()
                Frm.Activate()
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try

        End If
    End Sub
    Private Sub BttnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrint.Click
        If Me.RBttnCompany.Checked = True Then
            Dim Rpt As New rptORDER_FORM_COMPANY
            Dim Frm As New frmRPT
            Try
                Frm.CRV.ReportSource = Rpt
                Frm.CRV.PrintReport()
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try

        ElseIf Me.RBttnCategory.Checked = True Then
            Dim Rpt As New rptORDER_FORM_CATEGORY
            Dim Frm As New frmRPT
            Try
                Frm.CRV.ReportSource = Rpt
                Frm.CRV.PrintReport()
            Catch Ex As Exception
                MsgBox(Ex.Message)
            End Try

        End If
    End Sub

    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region
End Class