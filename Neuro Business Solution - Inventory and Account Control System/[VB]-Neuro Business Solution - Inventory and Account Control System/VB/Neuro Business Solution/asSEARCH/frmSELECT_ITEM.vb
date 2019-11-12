Imports SDS = System.Data.SqlClient
Public Class frmSELECT_ITEM

#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asNum As New AssNumPress
    Dim Str2 As String = Nothing
    Public FrmStr As String = Nothing
    Public Row As Integer
#End Region

#Region "FORM CONTROL"
    Private Sub frmSEARCH_P_INV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
    End Sub

    Private Sub frmSEARCH_P_INV_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Me.asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCompany.GotFocus, TxtItem.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCompany.LostFocus, TxtItem.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCompany.TextChanged, TxtItem.TextChanged

        If Not Me.TxtCompany.Text = Nothing And Me.TxtItem.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, CONVERT(NUMERIC(18,2),UNIT_COST) AS UNIT_COST, UNIT_RATE, CONVERT(NUMERIC(18,2),UNIT_RETAIL) AS UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM WHERE VENDOR LIKE '%" & Me.TxtCompany.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()

        ElseIf Me.TxtCompany.Text = Nothing And Not Me.TxtItem.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, CONVERT(NUMERIC(18,2),UNIT_COST) AS UNIT_COST, UNIT_RATE, CONVERT(NUMERIC(18,2),UNIT_RETAIL) AS UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM WHERE sITEM_NAME LIKE '%" & Me.TxtItem.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtCompany.Text = Nothing And Not Me.TxtItem.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, CONVERT(NUMERIC(18,2),UNIT_COST) AS UNIT_COST, UNIT_RATE, CONVERT(NUMERIC(18,2),UNIT_RETAIL) AS UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM WHERE sITEM_NAME LIKE '%" & Me.TxtItem.Text & "%' AND VENDOR LIKE '%" & Me.TxtCompany.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()

        ElseIf Me.TxtCompany.Text = Nothing And Me.TxtItem.Text = Nothing Then

            Me.FillListView()

        End If

    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If Me.ListView1.SelectedItems(0).Selected = True Then
            If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
                If FrmStr = "Purchase" Then
                    frmPURCHASE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(5).Text

                ElseIf FrmStr = "Sale" Then
                    frmSALE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(5).Text
                End If

                Me.Close()

            End If
        End If
    End Sub
    Private Sub ListView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListView1.KeyPress
        'If Asc(e.KeyChar) = Keys.Enter Then

        '    Me.ListView1_DoubleClick(sender, New System.EventArgs)
        'End If
    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnRefresh.Click
        Me.TxtCompany.Text = Nothing
        Me.TxtItem.Text = Nothing

        'Me.FillListView()

    End Sub

    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT nCODE, sITEM_NAME, sNICK, nPPP, sPACK_DESC, sPIECE_DESC, CONVERT(NUMERIC(18,2),UNIT_COST) AS UNIT_COST, UNIT_RATE, CONVERT(NUMERIC(18,2),UNIT_RETAIL) AS UNIT_RETAIL, nMIN_STOCK, nMAX_STOCK, nSALE_TAX, VENDOR, nBONUS_QTY, nBONUS_ON_PCS, CASE sCLAIMABLE WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS CLAIMABLE, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_STOCK, OPEN_UNIT_VALUE FROM V_LUP_ITEM ORDER BY sITEM_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_ITEM1.Clear()
            Me.daLUP_ITEM.Fill(Me.DsLUP_ITEM1.V_LUP_ITEM)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_ITEM1.V_LUP_ITEM.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(1).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False

                With LstItem.SubItems
                    .Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(12).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(8).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(3).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim SqlCmd2 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
            Me.daLUP_ITEM = New SDS.SqlDataAdapter(SqlCmd2)

            Me.DsLUP_ITEM1.Clear()
            Me.daLUP_ITEM.Fill(Me.DsLUP_ITEM1.V_LUP_ITEM)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_ITEM1.V_LUP_ITEM.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(1).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False

                With LstItem.SubItems
                    .Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(12).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_ITEM1.V_LUP_ITEM.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

   
End Class