Imports SDS = System.Data.SqlClient
Public Class frmSELECT_CLIENT

#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asNum As New AssNumPress
    Dim Str2 As String = Nothing
    Public Row1 As Integer
#End Region

#Region "FORM CONTROL"
    Private Sub frmSELECT_CLIENT_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.TxtClient.Focus()
    End Sub
    Private Sub frmSELECT_CLIENT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
        Me.TxtClient.Focus()
    End Sub
    Private Sub frmSELECT_CLIENT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If Me.ListView1.Focused = False Then
            Me.asNum.EnterTab(e)
        End If

    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtArea.GotFocus, TxtClient.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtArea.LostFocus, TxtClient.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtArea.TextChanged, TxtClient.TextChanged

        If Not Me.TxtArea.Text = Nothing And Me.TxtClient.Text = Nothing Then
            Me.Str2 = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CREDIT_LIM, GST_NO, OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO WHERE AREA LIKE '%" & Me.TxtArea.Text & "%' ORDER BY SHOP_NAME"
            Me.FillListView_Condition()

        ElseIf Me.TxtArea.Text = Nothing And Not Me.TxtClient.Text = Nothing Then
            Me.Str2 = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CREDIT_LIM, GST_NO, OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO WHERE SHOP_NAME LIKE '%" & Me.TxtClient.Text & "%' ORDER BY SHOP_NAME"
            Me.FillListView_Condition()

        ElseIf Not Me.TxtArea.Text = Nothing And Not Me.TxtClient.Text = Nothing Then
            Me.Str2 = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CREDIT_LIM, GST_NO, OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO WHERE SHOP_NAME LIKE '%" & Me.TxtClient.Text & "%' AND AREA LIKE '%" & Me.TxtArea.Text & "%' ORDER BY SHOP_NAME"
            Me.FillListView_Condition()

        ElseIf Me.TxtArea.Text = Nothing And Me.TxtClient.Text = Nothing Then

            Me.FillListView()

        End If

    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If Me.ListView1.SelectedItems(0).Selected = True Then
            If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
                frmRECOVERY.DataGridView1.Rows(Row1).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(2).Text
                Me.Close()

            End If
        End If
    End Sub
    Private Sub ListView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListView1.KeyPress
        If Asc(e.KeyChar) = Keys.Enter Then
            If Me.ListView1.SelectedItems(0).Selected = True Then
                If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
                    frmRECOVERY.DataGridView1.Rows(Row1).Cells("ColCode").Value = Me.ListView1.SelectedItems(0).SubItems(2).Text
                    Me.Close()

                End If
            End If
        End If
    End Sub
   
#End Region

#Region "Button Control"
    Private Sub BttnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnRefresh.Click
        Me.TxtArea.Text = Nothing
        Me.TxtClient.Text = Nothing

        'Me.FillListView()

    End Sub

    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillListView()
        Try
            Dim Str1 As String = "SELECT ID, NAME, SHOP_NAME, SHOP_ADD, AREA, HOME_ADD, SHOP_PH, HOME_PH, CELL_NO, FAX_NO, E_MAIL, WEB_SITE, CASE STATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CLIENT_CAT, CLIENT_GD, CLIENT_TYPE, CREDIT_LIM, GST_NO, OPEN_BAL, VISIT_TYPE, NO_VISIT, ROUTE FROM V_CLIENT_INFO ORDER BY SHOP_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daCLIENT_INFO = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsCLIENT_INFO1.Clear()
            Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO1.V_CLIENT_INFO)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsCLIENT_INFO1.V_CLIENT_INFO.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(2).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False

                With LstItem.SubItems
                    .Add(Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(4).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()
        Try
            Dim SqlCmd1 As New SDS.SqlCommand(Str2, Me.SqlConnection1)
            Me.daCLIENT_INFO = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsCLIENT_INFO1.Clear()
            Me.daCLIENT_INFO.Fill(Me.DsCLIENT_INFO1.V_CLIENT_INFO)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsCLIENT_INFO1.V_CLIENT_INFO.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(2).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False

                With LstItem.SubItems
                    .Add(Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(4).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsCLIENT_INFO1.V_CLIENT_INFO.Item(Cnt).Item(0).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


End Class