Public Class AssNumPress

    Dim NumStr As String = "0123456789"

    Public Sub NumPress(ByVal Flg As Boolean, ByVal s As System.Windows.Forms.KeyPressEventArgs)
        'MsgBox(Asc(s.KeyChar))
        If Flg = True Then
            If Not Asc(s.KeyChar) = 8 And NumStr.IndexOf(s.KeyChar) < 0 Then
                s.Handled = True
            End If
        ElseIf Flg = False Then
            If NumStr.IndexOf(s.KeyChar) >= 0 Then
                s.Handled = True
            End If
        End If
    End Sub
    Public Sub NumPressDash(ByVal s As System.Windows.Forms.KeyPressEventArgs)
        Dim StrDesh As String = "0123456789-"
        If Not Asc(s.KeyChar) = 8 And StrDesh.IndexOf(s.KeyChar) < 0 Then
            s.Handled = True
        End If
    End Sub
    Public Sub NumPressDot(ByVal s As System.Windows.Forms.KeyPressEventArgs)
        Dim StrDot As String = "0123456789. "
        If Not Asc(s.KeyChar) = 8 And StrDot.IndexOf(s.KeyChar) < 0 Then
            s.Handled = True
        End If
    End Sub
    Public Sub NumPressDeshDot(ByVal s As System.Windows.Forms.KeyPressEventArgs)
        Dim StrDot As String = "0123456789. -"
        If Not Asc(s.KeyChar) = 8 And StrDot.IndexOf(s.KeyChar) < 0 Then
            s.Handled = True
        End If
    End Sub
    Public Sub EnterTab(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then SendKeys.Send("{TAB}")
    End Sub
End Class
