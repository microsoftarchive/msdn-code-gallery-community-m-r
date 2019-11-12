''' <summary>
''' Solver Class
''' Encapsates the methods used to match the target through mathematical manipulation of the chosen numbers
''' </summary>
Public Class Solver

    Public Event Solution(e As NumberEventArgs)

    Private closest As Integer = Integer.MaxValue
    Private closestSolutionString As String

    Private numberDictionary As New Dictionary(Of Integer, Integer)
    Private tempNumberDictionary As New Dictionary(Of Integer, Integer)
    Private target As Integer

    ''' <summary>
    ''' Constructor.
    ''' Sets up some class level variables
    ''' </summary>
    ''' <param name="numbers"></param>
    ''' <param name="target"></param>
    Public Sub New(numbers As List(Of Integer), target As Integer)
        numbers = numbers.OrderByDescending(Function(x) x).ToList
        Me.numberDictionary.Add(100, 0)
        Me.numberDictionary.Add(75, 0)
        Me.numberDictionary.Add(50, 0)
        Me.numberDictionary.Add(25, 0)
        Me.numberDictionary.Add(10, 0)
        Me.numberDictionary.Add(9, 0)
        Me.numberDictionary.Add(8, 0)
        Me.numberDictionary.Add(7, 0)
        Me.numberDictionary.Add(6, 0)
        Me.numberDictionary.Add(5, 0)
        Me.numberDictionary.Add(4, 0)
        Me.numberDictionary.Add(3, 0)
        Me.numberDictionary.Add(2, 0)
        Me.numberDictionary.Add(1, 0)

        For Each n As Integer In numbers
            numberDictionary(n) += 1
        Next

        Me.target = target
    End Sub

    ''' <summary>
    ''' Runs the solver code in a Background thread
    ''' </summary>
    Public Sub search()
        Dim t As New Threading.Thread(AddressOf checkEquation)
        t.IsBackground = True
        t.Start()
    End Sub

    ''' <summary>
    ''' Coordinates the solver code and returns the solution by raising the custom event
    ''' </summary>
    Private Sub checkEquation()
        Dim cn As Dictionary(Of Integer, List(Of CompositeNumber)) = getCompositeNumbers()

        If cn.ContainsKey(Me.target) Then
            Me.closest = Me.target
            Me.closestSolutionString = cn(Me.target).First.tostring
        Else
            For Each kvp As KeyValuePair(Of Integer, List(Of CompositeNumber)) In cn
                If Math.Abs(kvp.Key - Me.target) < Math.Abs(Me.closest - Me.target) Then
                    Me.closest = kvp.Key
                    Me.closestSolutionString = kvp.Value.First.tostring
                End If
            Next
        End If

        If Me.closest <> Me.target Then
            If Not cn(Me.closest).All(Function(c) c.getNumbers.Count = 6) Then
                Dim difference As Integer = Math.Abs(Me.closest - Me.target)
                If difference <= 10 Then
                    If cn.ContainsKey(difference) Then
                        Dim cn2 As New Dictionary(Of Integer, List(Of CompositeNumber))
                        cn2.Add(Me.closest, cn(Me.closest))
                        For x As Integer = 1 To difference
                            If cn.ContainsKey(x) Then
                                cn2.Add(x, cn(x))
                            End If
                        Next
                        Dim cn3 As Dictionary(Of Integer, List(Of CompositeNumber)) = accumulateNumbers(cn2, True)
                        If cn3.ContainsKey(Me.target) Then
                            Me.closest = Me.target
                            Me.closestSolutionString = cn3(Me.target).First.tostring
                        Else
                            For Each kvp As KeyValuePair(Of Integer, List(Of CompositeNumber)) In cn3
                                If Math.Abs(kvp.Key - Me.target) < Math.Abs(Me.closest - Me.target) Then
                                    Me.closest = kvp.Key
                                    Me.closestSolutionString = kvp.Value.First.tostring
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        End If


        RaiseEvent Solution(New NumberEventArgs With {.nearest = Me.closest, .solutionString = Me.closestSolutionString})


    End Sub

    ''' <summary>
    ''' Calls accumulateNumbers to build composite numbers
    ''' </summary>
    ''' <returns></returns>
    Private Function getCompositeNumbers() As Dictionary(Of Integer, List(Of CompositeNumber))
        Dim cn As New Dictionary(Of Integer, List(Of CompositeNumber))

        For Each kvp As KeyValuePair(Of Integer, Integer) In Me.numberDictionary
            Dim l As New List(Of CompositeNumber)
            For x As Integer = 0 To kvp.Value - 1
                l.Add(New CompositeNumber(New Integer() {kvp.Key}, New String() {}, New String() {}, -1))
            Next
            If l.Count > 0 Then
                cn.Add(kvp.Key, l)
            End If
        Next

        Dim cn1 As Dictionary(Of Integer, List(Of CompositeNumber)) = accumulateNumbers(cn, False)

        For Each key As Integer In cn.Keys
            If cn1.ContainsKey(key) Then
                cn1(key).AddRange(cn(key))
            Else
                cn1.Add(key, cn(key))
            End If
        Next

        If cn1.ContainsKey(Me.target) Then
            Return cn1
        End If


        Dim cn2 As Dictionary(Of Integer, List(Of CompositeNumber)) = accumulateNumbers(cn1, True)

        For Each key As Integer In cn2.Keys
            If cn1.ContainsKey(key) Then
                cn1(key).AddRange(cn2(key))
            Else
                cn1.Add(key, cn2(key))
            End If
        Next

        If cn1.ContainsKey(Me.target) Then
            Return cn1
        End If


        cn2 = accumulateNumbers(cn1, True)

        For Each key As Integer In cn2.Keys
            If cn1.ContainsKey(key) Then
                cn1(key).AddRange(cn2(key))
            Else
                cn1.Add(key, cn2(key))
            End If
        Next

        Return cn1

    End Function

    ''' <summary>
    ''' Builds composite numbers Dictionary
    ''' </summary>
    ''' <param name="cn"></param>
    ''' <param name="checkNumbers"></param>
    ''' <returns></returns>
    Private Function accumulateNumbers(cn As Dictionary(Of Integer, List(Of CompositeNumber)), checkNumbers As Boolean) As Dictionary(Of Integer, List(Of CompositeNumber))
        Dim cn1 As New Dictionary(Of Integer, List(Of CompositeNumber))
        For x As Integer = 0 To cn.Keys.Count - 2
            For y As Integer = x + 1 To cn.Keys.Count - 1
                If cn1.ContainsKey(Me.target) Then
                    Return cn1
                End If
                Dim i1 As Integer = cn.Keys(x)
                Dim i2 As Integer = cn.Keys(y)
                If i1 > 0 AndAlso i2 > 0 Then
                    For x1 As Integer = 0 To cn(i1).Count - 1
                        For y1 As Integer = 0 To cn(i2).Count - 1
                            Dim a1() As Integer = cn(i1)(x1).getNumbers()
                            Dim a2() As Integer = cn(i2)(y1).getNumbers()
                            Dim ao1() As String = cn(i1)(x1).getOperators()
                            Dim ao2() As String = cn(i2)(y1).getOperators()
                            Dim ts1 As String = cn(i1)(x1).tostring
                            Dim ts2 As String = cn(i2)(y1).tostring
                            Dim used As Integer = a1.Length + a2.Length
                            If Not checkNumbers OrElse numbersAreAvailable(a1, a2) Then
                                If used < 6 OrElse (i1 + i2 < Me.target + 50 And i1 + i2 > Me.target - 50) Then
                                    Dim c As New CompositeNumber(a1.Concat(a2).ToArray,
                                                      ao1.Concat(New String() {"+"}).Concat(ao2).ToArray,
                                                      New String() {ts1, ts2}, ao1.Count)
                                    If cn1.ContainsKey(i1 + i2) Then
                                        cn1(i1 + i2).Add(c)
                                    Else
                                        cn1.Add(i1 + i2, New List(Of CompositeNumber)(New CompositeNumber() {c}))
                                    End If
                                End If
                                If used < 6 OrElse (i1 - i2 < Me.target + 50 And i1 - i2 > Me.target - 50) Then
                                    Dim c As New CompositeNumber(a1.Concat(a2).ToArray,
                                                      ao1.Concat(New String() {"-"}).Concat(ao2).ToArray,
                                                      New String() {ts1, ts2}, ao1.Count)
                                    If cn1.ContainsKey(i1 - i2) Then
                                        cn1(i1 - i2).Add(c)
                                    Else
                                        cn1.Add(i1 - i2, New List(Of CompositeNumber)(New CompositeNumber() {c}))
                                    End If

                                End If
                                If isInteger(CDec(i1 / i2)) AndAlso i2 > 1 Then
                                    If used < 6 OrElse (i1 / i2 < Me.target + 50 And i1 / i2 > Me.target - 50) Then
                                        Dim c As New CompositeNumber(a1.Concat(a2).ToArray,
                                                  ao1.Concat(New String() {"/"}).Concat(ao2).ToArray,
                                                  New String() {ts1, ts2}, ao1.Count)
                                        If cn1.ContainsKey(CInt(i1 / i2)) Then
                                            cn1(CInt(i1 / i2)).Add(c)
                                        Else
                                            cn1.Add(CInt(i1 / i2), New List(Of CompositeNumber)(New CompositeNumber() {c}))
                                        End If
                                    End If
                                End If
                                If i1 <> 1 AndAlso i2 <> 1 Then
                                    If used < 6 OrElse (i1 * i2 < Me.target + 50 And i1 * i2 > Me.target - 50) Then
                                        Dim c As New CompositeNumber(a1.Concat(a2).ToArray,
                                                      ao1.Concat(New String() {"*"}).Concat(ao2).ToArray,
                                                      New String() {ts1, ts2}, ao1.Count)
                                        If cn1.ContainsKey(i1 * i2) Then
                                            cn1(i1 * i2).Add(c)
                                        Else
                                            cn1.Add(i1 * i2, New List(Of CompositeNumber)(New CompositeNumber() {c}))
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Next
                End If
            Next
        Next

        Return cn1
    End Function

    ''' <summary>
    ''' Verifies that any number isn't used twice or more
    ''' </summary>
    ''' <param name="firstComposite"></param>
    ''' <param name="secondComposite"></param>
    ''' <returns></returns>
    Private Function numbersAreAvailable(firstComposite() As Integer,
                                           secondComposite() As Integer) As Boolean

        tempNumberDictionary = Me.numberDictionary.ToDictionary(Function(kvp) kvp.Key, Function(kvp) kvp.Value)

        For Each n As Integer In firstComposite
            tempNumberDictionary(n) -= 1
            If tempNumberDictionary(n) < 0 Then Return False
        Next

        For Each n As Integer In secondComposite
            tempNumberDictionary(n) -= 1
            If tempNumberDictionary(n) < 0 Then Return False
        Next

        Return True

    End Function

    ''' <summary>
    ''' Checks if a decimal has a fractional part
    ''' </summary>
    ''' <param name="d"></param>
    ''' <returns></returns>
    Private Function isInteger(d As Decimal) As Boolean
        Return d Mod 1 = 0
    End Function

End Class