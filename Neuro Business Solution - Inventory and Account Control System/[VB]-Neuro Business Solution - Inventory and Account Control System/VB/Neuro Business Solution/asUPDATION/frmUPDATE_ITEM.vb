Imports SDS = System.Data.SqlClient
Public Class frmUPDATE_ITEM

#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asUpdate As New AssUpdate
    Dim asSELECT As New AssSelect
    Dim asMax As New AssMaxNo
    Dim asNum As New AssNumPress
    Dim Rd As System.Data.SqlClient.SqlDataReader
#End Region

#Region "FORM CONTROL"
    Private Sub frmUPDATE_ITEM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillComboBox_Company()
        Me.daLUP_VENDOR.Fill(Me.DsLUP_VENDOR11.LUP_VENDOR)

        Dim Str2 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE, ITEM_CAT FROM V_LUP_ITEM"
        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
        Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd2)

        Me.DsV_LUP_ITEM_NEW1.Clear()
        Me.daLUP_ITEM.Fill(Me.DsV_LUP_ITEM_NEW1.V_LUP_ITEM)
    End Sub
    Private Sub frmLUP_ITEM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Controls"
    Private Sub txtITEM_NAME_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtITEM_NAME.TextChanged
        Me.Search_Item()
        pFg = True
    End Sub
#End Region

#Region "ComboBox Controls"
    'Got and LostFocus
    Dim pFg As Boolean = False
    Private Sub Cmb_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompany.GotFocus
        CType(sender, ComboBox).BackColor = Color.LightSteelBlue
        CType(sender, ComboBox).SelectAll()
    End Sub
    Private Sub Cmb_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCompany.LostFocus
        CType(sender, ComboBox).BackColor = Color.White
    End Sub

    Private Sub CmbCompany_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCompany.SelectedIndexChanged
        Me.Search_Item()
        pFg = True
    End Sub
#End Region

#Region "DataGridView Controls"
    Dim Str As String
    Dim Item_Code As String
    Dim row As Integer
    Private Sub DataGridView1_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowLeave
        row = e.RowIndex
        'MsgBox("Leave row # : " & row)
        pFg = False
        'MsgBox(Me.DataGridView1.Item(10, e.RowIndex).Value)
        Me.UPDATE_VALUES(sender, e)
    End Sub
    Private Sub UPDATE_VALUES(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If pFg = False Then
            'MsgBox(Me.DataGridView1.Item(10, row).Value)
            Dim Vender_Code As Double = Me.asMax.LoadValue(Rd, "SELECT nCODE FROM LUP_VENDOR WHERE sDESC='" & Me.DataGridView1.Item("ColVendor", row).Value.ToString & "'")
            'MsgBox("Vendor Code: " & Vender_Code)
            Dim StkValue As Double = Val(Me.DataGridView1.Item("ColOpenStk", row).Value) * (Val(Me.DataGridView1.Item("ColPksCost", row).Value) / Val(Me.DataGridView1.Item("ColPPP", row).Value))
            Me.asUpdate.UpdateValue("UPDATE LUP_ITEMS SET sITEM_NAME='" & Me.DataGridView1.Item("ColItemName", row).Value & "', sNICK= '" & Me.DataGridView1.Item("ColNick", row).Value & "', nPPP=" & Val(Me.DataGridView1.Item("ColPPP", row).Value) & ", sPACK_DESC='" & Me.DataGridView1.Item("ColPksDesc", row).Value & "', sPIECE_DESC='" & Me.DataGridView1.Item("ColPcsDesc", row).Value & "', nUNIT_COST=" & Val(Me.DataGridView1.Item("ColPksCost", row).Value) & ", nUNIT_RATE=" & Val(Me.DataGridView1.Item("ColPksRate", row).Value) & ", nUNIT_RETAIL=" & Val(Me.DataGridView1.Item("ColPksRetail", row).Value) & ", nSALE_TAX=" & Val(Me.DataGridView1.Item("ColST", row).Value) & ", nVENDOR_CODE=" & Vender_Code & ", nOPEN_STOCK=" & Val(Me.DataGridView1.Item("ColOpenStk", row).Value) & ", nOPEN_UNIT_VALUE=" & StkValue & " WHERE nCODE=" & Val(Me.DataGridView1.Item("ColCode", row).Value) & "")
        End If
    End Sub
    Private Sub DataGridView1_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError
        'for Handle error 
        'blank error
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillComboBox_Company()
        Dim Str1 As String = "SELECT nCODE, sDESC FROM LUP_VENDOR ORDER BY sDESC"
        Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
        Me.daLUP_VENDOR = New SDS.SqlDataAdapter(SqlCmd1)

        Me.DsLUP_VENDOR1.Clear()
        Me.daLUP_VENDOR.Fill(Me.DsLUP_VENDOR1.LUP_VENDOR)

        Dim dtLoading As New DataTable("LUP_VENDOR")

        dtLoading.Columns.Add("nCODE", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("sDESC", System.Type.GetType("System.String"))

        Dim Cnt As Integer

        For Cnt = 0 To Me.DsLUP_VENDOR1.LUP_VENDOR.Count - 1
            Dim dr As DataRow
            dr = dtLoading.NewRow

            dr("nCODE") = Me.DsLUP_VENDOR1.LUP_VENDOR.Item(Cnt).Item(0).ToString
            dr("sDESC") = Me.DsLUP_VENDOR1.LUP_VENDOR.Item(Cnt).Item(1).ToString

            dtLoading.Rows.Add(dr)
        Next

        Me.CmbCompany.SelectedIndex = -1
        Me.CmbCompany.Items.Clear()
        Me.CmbCompany.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        Me.CmbCompany.SourceDataString = New String(1) {"sDESC", "nCODE"}
        Me.CmbCompany.SourceDataTable = dtLoading
    End Sub

    Private Sub Search_Item()
        Dim Str2 As String
        If Not Me.CmbCompany.SelectedIndex = -1 And Not Me.txtITEM_NAME.Text = Nothing Then
            Str2 = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE, ITEM_CAT FROM V_LUP_ITEM WHERE VENDOR='" & Me.CmbCompany.Text & "' AND sITEM_NAME LIKE '%" & Me.txtITEM_NAME.Text & "%'"
            
        ElseIf Not Me.CmbCompany.SelectedIndex = -1 And Me.txtITEM_NAME.Text = Nothing Then
            Str2 = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE, ITEM_CAT FROM V_LUP_ITEM WHERE VENDOR='" & Me.CmbCompany.Text & "'"

        ElseIf Me.CmbCompany.SelectedIndex = -1 And Not Me.txtITEM_NAME.Text = Nothing Then
            Str2 = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE, ITEM_CAT FROM V_LUP_ITEM WHERE sITEM_NAME LIKE '%" & Me.txtITEM_NAME.Text & "%'"

        Else
            Str2 = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, UNIT_COST, UNIT_RATE, UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE, ITEM_CAT FROM V_LUP_ITEM"
        End If

        Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
        Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd2)

        Me.DsV_LUP_ITEM_NEW1.Clear()
        Me.daLUP_ITEM.Fill(Me.DsV_LUP_ITEM_NEW1.V_LUP_ITEM)

    End Sub
#End Region


   
End Class