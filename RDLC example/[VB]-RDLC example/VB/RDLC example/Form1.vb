Imports Microsoft.Reporting.WinForms

Public Class Form1

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Dim ds As New DataSet1
        Dim dt As DataTable = ds.Tables(0)

        For x As Integer = 1 To 10
            Dim r As DataRow = dt.NewRow()
            r("ID") = x
            r("Name") = "Item" & x.ToString
            dt.Rows.Add(r)
        Next

        frmViewer.Show()

        frmViewer.ReportViewer1.LocalReport.ReportPath = "Report1.rdlc"
        frmViewer.ReportViewer1.LocalReport.DataSources.Clear()
        frmViewer.ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1_DataTable1", ds.Tables(0)))
        frmViewer.ReportViewer1.RefreshReport()
    End Sub
End Class
