Public Class Rootobject
    Public Property d As D
End Class

Public Class D
    Public Property __copyright As String
    Public Property __count As Integer
    Public Property results() As List(Of Result)
End Class

Public Class Result
    Public Property __metadata As __Metadata
    Public Property EntityID As String
    Public Property AddressLine As String
    Public Property PostalCode As String
    Public Property Latitude As Single
    Public Property Longitude As Single
    Public Property Geom As String
End Class

Public Class __Metadata
    Public Property uri As String
End Class