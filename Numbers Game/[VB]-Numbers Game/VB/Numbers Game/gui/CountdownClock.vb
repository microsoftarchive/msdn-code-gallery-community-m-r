''' <summary>
''' The graphical countdown clock
''' </summary>
Public Class CountdownClock
    Inherits PictureBox

    Public Event clockStopped()

    Private seconds As Integer = 0
    Private stopped As Boolean = False

    ''' <summary>
    ''' Default constructor
    ''' </summary>
    Public Sub New()
        resetClock()
    End Sub

    ''' <summary>
    ''' Resets the clock to the zero seconds position
    ''' </summary>
    Public Sub resetClock()
        MyBase.Image = drawClock(0)
    End Sub

    ''' <summary>
    ''' Starts the countdown clock
    ''' </summary>
    Public Sub startClock()
        stopped = False
        While seconds < 30 AndAlso stopped = False
            Threading.Thread.Sleep(1000)
            seconds += 1
            MyBase.Image = drawClock(seconds)
        End While
        seconds = 0
        RaiseEvent clockStopped()
    End Sub

    ''' <summary>
    ''' Stops the countdown clock
    ''' </summary>
    Public Sub stopClock()
        stopped = True
    End Sub

    ''' <summary>
    ''' Draws the clock face and second hand
    ''' </summary>
    ''' <param name="seconds"></param>
    ''' <returns></returns>
    Private Function drawClock(seconds As Integer) As Bitmap
        Dim face As Bitmap = My.Resources.clockFace
        Dim gr As Graphics = Graphics.FromImage(face)

        gr.DrawEllipse(New Pen(Color.Black, 2), New Rectangle(1, 1, 98, 98))

        gr.TranslateTransform(50, 50)
        gr.RotateTransform(seconds * 6)

        gr.DrawLine(New Pen(Color.Red, 3), 0, 0, 0, -46)
        gr.ResetTransform()

        Return face
    End Function

End Class
