Imports SDS = System.Data.SqlClient
Public Class frmMOBILE_SUMMARY

#Region "VARIABLES"
    Dim asUpdate As New AssUpdate
    Dim asSELECT As New AssSelect
    Dim asNum As New AssNumPress
    Dim Rd As System.Data.SqlClient.SqlDataReader
#End Region

#Region "FORM CONTROL"
    Private Sub frmMOBILE_DETAIL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub frmMOBILE_DETAIL_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        CType(sender, ComboBox).BackColor = Color.White
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
  
#End Region

  
End Class