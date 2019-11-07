Public Class Form3
    Dim asConn As New AssConn
    Private Sub Form3_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim str As String = "SELECT GROUP_ID, TR_TYPE, sDESC, CLIENT_ID, dDATE, TR_ID, Dr, Cr FROM SV_CLIENT_LEDGER WHERE GROUP_ID=" & Val(Me.txtGP_ID.Text) & " AND CLIENT_ID=" & Val(Me.txtCL_ID.Text) & " ORDER BY dDATE"
        Me.daTEMP_LEDGER = New SqlClient.SqlDataAdapter(str, Me.SqlConnection1)
        Me.DsTEMP_LEDGER1.Clear()
        Me.daTEMP_LEDGER.Fill(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER)
        Dim i As Integer = 0
        Dim cBAL As Double

        For i = 0 To Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows.Count - 1
            If Not i = 0 Then
                cBAL = Val(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i - 1).Item("BAL").ToString)
            Else
                cBAL = 0
            End If

            Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i).Item("BAL") = cBAL + (Val(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i).Item("Cr").ToString) - Val(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i).Item("Dr").ToString))

        Next

        'MsgBox(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows.Count)
        If Not Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows.Count = 1 Then
AGAIN:

            For i = 0 To Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows.Count - 1
                If Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i).Item("dDATE").ToString = Nothing Then
                    'MsgBox(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i).Item("dDATE").ToString)
                    Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows.RemoveAt(i)
                    GoTo AGAIN

                ElseIf CDate(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i).Item("dDATE")) < CDate(Me.txtDATE1.Text) Or CDate(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i).Item("dDATE")) > CDate(Me.txtDATE2.Text) Then
                    'MsgBox(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(i).Item("dDATE").ToString)
                    Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows.RemoveAt(i)
                    GoTo AGAIN
                End If
            Next

            Dim TopBAL As Double = Val(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(0).Item("BAL").ToString) - (Val(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(0).Item("Cr").ToString) - Val(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(0).Item("Dr").ToString))
            Dim TopDate As Date = CDate(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows(0).Item("dDATE")).AddDays(-1)
            'Dim Rw As DataRow
            'Rw.Item(0) = Nothing
            'Rw.Item(1) = Nothing
            'Rw.Item(2) = "Opening Balance"
            'Rw.Item(3) = Nothing
            'Rw.Item(4) = Nothing
            'Rw.Item(5) = Nothing
            'Rw.Item(6) = Nothing
            'Rw.Item(7) = Nothing
            'Rw.Item(8) = TopBAL

            'Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.Rows.Add(Rw)
            Try
                Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER.AddSV_CLIENT_LEDGERRow(Nothing, Nothing, "Opening Balance", Nothing, TopDate, Nothing, Nothing, Nothing, TopBAL)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If

        Dim RPT As New TEMP_LEDGER
        RPT.SetDataSource(Me.DsTEMP_LEDGER1)
        Me.CrystalReportViewer1.ReportSource = RPT

        'Dim strDT As String = "SELECT GROUP_ID, TR_TYPE, sDESC, CLIENT_ID, dDATE, TR_ID, Dr, Cr FROM SV_CLIENT_LEDGER WHERE GROUP_ID=" & Val(Me.txtGP_ID.Text) & " AND CLIENT_ID=" & Val(Me.txtCL_ID.Text) & " AND dDATE >= CONVERT(DATETIME, '" & Me.txtDATE1.Text & "') AND dDATE <= CONVERT(DATETIME, '" & Me.txtDATE2.Text & "')"
        'Me.daTEMP_LEDGER = New SqlClient.SqlDataAdapter(strDT, Me.SqlConnection1)
        'Me.daTEMP_LEDGER.Fill(Me.DsTEMP_LEDGER1.SV_CLIENT_LEDGER)
    End Sub


End Class