Imports SDS = System.Data.SqlClient
Public Class frmSELECT_ITEM_BATCH

#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asNum As New AssNumPress
    Dim Str2 As String = Nothing
    Public FrmStr As String = Nothing
    Public Row As Integer
#End Region

#Region "FORM CONTROL"
    Private Sub frmSELECT_ITEM_BATCH_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.TxtItem.Focus()
    End Sub
    Private Sub frmSELECT_ITEM_BATCH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Me.TxtItem.Focus()
    End Sub

    'Private Sub frmSELECT_ITEM_BATCH_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
    '    Me.asNum.EnterTab(e)
    'End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCompany.GotFocus, TxtItem.GotFocus, TxtFormula.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCompany.LostFocus, TxtItem.LostFocus, TxtFormula.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCompany.TextChanged, TxtItem.TextChanged, TxtFormula.TextChanged
        'Me.FillListView_Condition()
        If Not Me.TxtCompany.Text = Nothing And Me.TxtItem.Text = Nothing And Me.TxtFormula.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, VENDOR, BATCH, CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18,2), UNIT_RETAIL) AS UNIT_RETAIL, nPPP, NET_TOTAL, NICK FROM V_SELECT_ITEM_BATCH WHERE VENDOR LIKE '%" & Me.TxtCompany.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()
            Exit Sub

        ElseIf Me.TxtCompany.Text = Nothing And Not Me.TxtItem.Text = Nothing And Me.TxtFormula.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, VENDOR, BATCH, CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18,2), UNIT_RETAIL) AS UNIT_RETAIL, nPPP, NET_TOTAL, NICK FROM V_SELECT_ITEM_BATCH WHERE sITEM_NAME LIKE '%" & Me.TxtItem.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()
            Exit Sub

        ElseIf Not Me.TxtCompany.Text = Nothing And Not Me.TxtItem.Text = Nothing And Me.TxtFormula.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, VENDOR, BATCH, CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18,2), UNIT_RETAIL) AS UNIT_RETAIL, nPPP, NET_TOTAL, NICK FROM V_SELECT_ITEM_BATCH WHERE sITEM_NAME LIKE '%" & Me.TxtItem.Text & "%' AND VENDOR LIKE '%" & Me.TxtCompany.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()
            Exit Sub

        ElseIf Me.TxtCompany.Text = Nothing And Me.TxtItem.Text = Nothing And Not Me.TxtFormula.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, VENDOR, BATCH, CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18,2), UNIT_RETAIL) AS UNIT_RETAIL, nPPP, NET_TOTAL, NICK FROM V_SELECT_ITEM_BATCH WHERE NICK LIKE '%" & Me.TxtFormula.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()
            Exit Sub

        ElseIf Me.TxtCompany.Text = Nothing And Not Me.TxtItem.Text = Nothing And Not Me.TxtFormula.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, VENDOR, BATCH, CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18,2), UNIT_RETAIL) AS UNIT_RETAIL, nPPP, NET_TOTAL, NICK FROM V_SELECT_ITEM_BATCH WHERE sITEM_NAME LIKE '%" & Me.TxtItem.Text & "%' AND NICK LIKE '%" & Me.TxtFormula.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()
            Exit Sub

        ElseIf Not Me.TxtCompany.Text = Nothing And Not Me.TxtItem.Text = Nothing And Not Me.TxtFormula.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, VENDOR, BATCH, CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18,2), UNIT_RETAIL) AS UNIT_RETAIL, nPPP, NET_TOTAL, NICK FROM V_SELECT_ITEM_BATCH WHERE sITEM_NAME LIKE '%" & Me.TxtItem.Text & "%' AND VENDOR LIKE '%" & Me.TxtCompany.Text & "%' AND NICK LIKE '%" & Me.TxtFormula.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()
            Exit Sub

        ElseIf Not Me.TxtCompany.Text = Nothing And Me.TxtItem.Text = Nothing And Not Me.TxtFormula.Text = Nothing Then
            Me.Str2 = "SELECT nCODE, sITEM_NAME, VENDOR, BATCH, CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18,2), UNIT_RETAIL) AS UNIT_RETAIL, nPPP, NET_TOTAL, NICK FROM V_SELECT_ITEM_BATCH WHERE VENDOR LIKE '%" & Me.TxtCompany.Text & "%' AND NICK LIKE '%" & Me.TxtFormula.Text & "%' ORDER BY sITEM_NAME"
            Me.FillListView_Condition()
            Exit Sub

        ElseIf Me.TxtCompany.Text = Nothing And Me.TxtItem.Text = Nothing Then
            Me.FillListView()
            Exit Sub

        End If

    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If Me.ListView1.SelectedItems(0).Selected = True Then
            If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
                If FrmStr = "Purchase" Then
                    frmPURCHASE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(7).Text
                    frmPURCHASE.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.ListView1.SelectedItems(0).SubItems(3).Text

                ElseIf FrmStr = "Sale" Then
                    frmSALE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(7).Text
                    frmSALE.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.ListView1.SelectedItems(0).SubItems(3).Text

                ElseIf FrmStr = "Mobile_Sale" Then
                    frmLOADPASS.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(7).Text
                    frmLOADPASS.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.ListView1.SelectedItems(0).SubItems(3).Text
                End If

                Me.Close()

            End If
        End If
    End Sub
    Private Sub ListView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListView1.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            If Me.ListView1.SelectedItems(0).Selected = True Then
                If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
                    If FrmStr = "Purchase" Then
                        frmPURCHASE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(7).Text
                        frmPURCHASE.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.ListView1.SelectedItems(0).SubItems(3).Text

                    ElseIf FrmStr = "Sale" Then
                        frmSALE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(7).Text
                        frmSALE.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.ListView1.SelectedItems(0).SubItems(3).Text

                    ElseIf FrmStr = "Mobile_Sale" Then
                        frmLOADPASS.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(7).Text
                        frmLOADPASS.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.ListView1.SelectedItems(0).SubItems(3).Text
                    End If

                    Me.Close()

                End If
            End If
        End If
    End Sub
#End Region

#Region "DataGrid Controls"
    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If Me.DataGridView1.CurrentCell.RowIndex > -1 Then
            If FrmStr = "Purchase" Then
                'frmPURCHASE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.DataGrid1.SelectedItems(0).SubItems(7).Text
                frmPURCHASE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
                frmPURCHASE.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex).Value

            ElseIf FrmStr = "Sale" Then
                'MsgBox(Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value)

                frmSALE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
                frmSALE.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex).Value

            ElseIf FrmStr = "Mobile_Sale" Then
                frmLOADPASS.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex).Value
                frmLOADPASS.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex).Value
            End If

            Me.Close()
        End If

    End Sub
    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            If FrmStr = "Purchase" Then
                'frmPURCHASE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.DataGrid1.SelectedItems(0).SubItems(7).Text
                frmPURCHASE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex - 1).Value
                frmPURCHASE.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex - 1).Value

            ElseIf FrmStr = "Sale" Then
                'MsgBox(Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex - 1).Value)

                frmSALE.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex - 1).Value
                frmSALE.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex - 1).Value

            ElseIf FrmStr = "Mobile_Sale" Then
                frmLOADPASS.DataGridView1.Rows(Row).Cells("ColCode").Value = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentCell.RowIndex - 1).Value
                frmLOADPASS.DataGridView1.Rows(Row).Cells("ColBatch").Value = Me.DataGridView1.Item(2, Me.DataGridView1.CurrentCell.RowIndex - 1).Value
            End If

            Me.Close()
        End If
    End Sub

    'Private Sub DataGrid1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGrid1.MouseDown
    '    'If Me.ListView1.SelectedItems(0).Selected = True Then
    '    '    If Not Me.ListView1.SelectedItems(0).Text = Nothing Then

    '    Dim DataGrid1 As DataGrid = CType(sender, DataGrid)
    '    Dim hti As System.Windows.Forms.DataGrid.HitTestInfo
    '    hti = DataGrid1.HitTest(e.X, e.Y)

    '    Dim message As String = "You clicked "

    '    Select Case hti.Type
    '        Case System.Windows.Forms.DataGrid.HitTestType.None
    '            message &= "the background."
    '        Case System.Windows.Forms.DataGrid.HitTestType.Cell
    '            'message &= "cell at row " & hti.Row & ", col " & hti.Column & " value " & Me.DataGrid1.CurrentCell.ToString
    '            message &= "value " & Me.DataGrid1.Item(hti.Row, hti.Column)
    '        Case System.Windows.Forms.DataGrid.HitTestType.ColumnHeader
    '            message &= "the column header for column " & hti.Column
    '        Case System.Windows.Forms.DataGrid.HitTestType.RowHeader
    '            message &= "the row header for row " & hti.Row
    '        Case System.Windows.Forms.DataGrid.HitTestType.ColumnResize
    '            message &= "the column resizer for column " & hti.Column
    '        Case System.Windows.Forms.DataGrid.HitTestType.RowResize
    '            message &= "the row resizer for row " & hti.Row
    '        Case System.Windows.Forms.DataGrid.HitTestType.Caption
    '            message &= "the caption"
    '        Case System.Windows.Forms.DataGrid.HitTestType.ParentRows
    '            message &= "the parent row"
    '    End Select

    '    MsgBox(message)
    'End Sub
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
            Dim Str1 As String = "SELECT nCODE, sITEM_NAME, VENDOR, BATCH, CONVERT(NUMERIC(18,2), UNIT_RATE) AS UNIT_RATE, CONVERT(NUMERIC(18,2), UNIT_RETAIL) AS UNIT_RETAIL, nPPP, NET_TOTAL, NICK FROM V_SELECT_ITEM_BATCH ORDER BY sITEM_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daV_SELECT_ITEM_BATCH = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_SELECT_ITEM_BATCH1.Clear()
            Me.daV_SELECT_ITEM_BATCH.Fill(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH)

            'Me.ListView1.Items.Clear()

            'Dim Cnt As Integer
            'Dim LstItem As ListViewItem

            'For Cnt = 0 To Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Count - 1
            '    LstItem = Me.ListView1.Items.Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(1).ToString)
            '    Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False

            '    With LstItem.SubItems
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(7).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(3).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(4).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(5).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '    End With
            'Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim SqlCmd1 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
            Me.daV_SELECT_ITEM_BATCH = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsV_SELECT_ITEM_BATCH1.Clear()
            Me.daV_SELECT_ITEM_BATCH.Fill(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH)

            'Me.ListView1.Items.Clear()

            ''Me.ListView1.BindingContext(Me.DsV_SELECT_ITEM_BATCH1, "V_SELECT_ITEM_BATCH")

            'Dim Cnt As Integer
            'Dim LstItem As ListViewItem

            'For Cnt = 0 To Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Count - 1
            '    LstItem = Me.ListView1.Items.Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(1).ToString)
            '    Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False

            '    With LstItem.SubItems
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(7).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(2).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(3).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(4).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(5).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '        .Add(Me.DsV_SELECT_ITEM_BATCH1.V_SELECT_ITEM_BATCH.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
            '    End With
            'Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

End Class