Public Class Numbers

    Private large As New List(Of Integer) From {25, 50, 75, 100}
    Private small As New List(Of Integer) From {1, 2, 3, 4, 5,
                                                6, 7, 8, 9, 10,
                                                1, 2, 3, 4, 5,
                                                6, 7, 8, 9, 10}

    Public Sub New(r As Random)
        large = large.OrderBy(Function(x) r.NextDouble).ToList
        small = small.OrderBy(Function(x) r.NextDouble).ToList
    End Sub

    Public Function drawSmall() As Integer
        Dim x As Integer = small(0)
        small.RemoveAt(0)
        Return x
    End Function

    Public Function drawLarge() As LargeDrawn
        Dim x As Integer = large(0)
        large.RemoveAt(0)
        Dim isLastNumber As Boolean = large.Count = 0
        Return New LargeDrawn With {.x = x, .last = isLastNumber}
    End Function

End Class