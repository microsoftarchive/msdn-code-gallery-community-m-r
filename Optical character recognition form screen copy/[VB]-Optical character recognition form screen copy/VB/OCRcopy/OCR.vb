Public Class OCR
    Dim Point As New System.Drawing.Point()
    Dim X, Y, X1, Y1 As Integer
    Dim pic As Bitmap = New Bitmap(292, 133)
    Dim gfx As Graphics = Graphics.FromImage(pic)
    Public Shared Function OCRImage(ByVal bm As System.Drawing.Image, ByVal language As String, ByVal path As String) As String
        OCRImage = ""
        Dim oOCR As New tessnet2.Tesseract
        oOCR.Init(path, language, False)
        Dim WordList As New List(Of tessnet2.Word)
        WordList = oOCR.DoOCR(bm, Rectangle.Empty)
        Dim LineCount As Integer = tessnet2.Tesseract.LineCount(WordList)
        For i As Integer = 0 To LineCount - 1
            OCRImage &= tessnet2.Tesseract.GetLineText(WordList, i) & vbCrLf
        Next
        oOCR.Dispose()
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        gfx.CopyFromScreen(New Point(Me.Location.X + PictureBox1.Location.X + 4, Me.Location.Y + PictureBox1.Location.Y + 30), New Point(0, 0), pic.Size)
        PictureBox1.Image = pic

    End Sub

    Private Sub RichTextBox1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox1.MouseMove
        If e.Button = MouseButtons.Left Then
            Point = Control.MousePosition
            Point.X = Point.X - (X)
            Point.Y = Point.Y - (Y)
            Me.Location = Point
        End If
    End Sub
    Private Sub RichTextBox1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox1.MouseDown
        X = Control.MousePosition.X - Me.Location.X
        Y = Control.MousePosition.Y - Me.Location.Y
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Timer1.Enabled = True
        RichTextBox1.Text = ""
        RichTextBox1.Text = OCRImage(pic, "eng", "tessdata")


    End Sub
End Class
