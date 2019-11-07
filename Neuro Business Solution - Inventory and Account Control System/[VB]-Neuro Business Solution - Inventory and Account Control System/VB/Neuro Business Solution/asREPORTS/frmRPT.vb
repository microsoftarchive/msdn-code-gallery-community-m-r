Imports SDS = System.Data.SqlClient
Imports CrystalDecisions.Shared
Public Class frmRPT

#Region "Form Control Events"
    Private Sub frmRPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CRV.RefreshReport()
    End Sub
    Private Sub CRV_ReportRefresh(ByVal source As Object, ByVal e As CrystalDecisions.Windows.Forms.ViewerEventArgs) Handles CRV.ReportRefresh
        Me.SetDBLogonForReport(myConnectionInfo)
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "DBLogOn"
    Dim myConnectionInfo As New ConnectionInfo
    Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo)
        myConnectionInfo.IntegratedSecurity = True

        myConnectionInfo.ServerName = "SERVER"
        myConnectionInfo.DatabaseName = "Neuro_BS"

        Dim myTableLogOnInfos As TableLogOnInfos = CRV.LogOnInfo
        For Each myTableLogOnInfo As TableLogOnInfo In myTableLogOnInfos
            myTableLogOnInfo.ConnectionInfo = myConnectionInfo
        Next
    End Sub
#End Region
End Class