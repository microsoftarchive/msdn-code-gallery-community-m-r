Namespace Common

    ''' <summary>
    ''' true を false に、および false を true に変換する値コンバーター。
    ''' </summary>
    Public NotInheritable Class BooleanNegationConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object,
                                language As String) As Object Implements IValueConverter.Convert
            Return Not (TypeOf value Is Boolean AndAlso DirectCast(value, Boolean))
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object,
                                    language As String) As Object Implements IValueConverter.ConvertBack
            Return Not (TypeOf value Is Boolean AndAlso DirectCast(value, Boolean))
        End Function

    End Class

End Namespace
