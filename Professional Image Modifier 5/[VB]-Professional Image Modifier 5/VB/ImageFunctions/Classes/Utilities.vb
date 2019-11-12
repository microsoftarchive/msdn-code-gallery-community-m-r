Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace ImageFunctions.Classes
	Friend Class Utilities

		Public Shared Function BytesToString(ByVal byteCount As Long) As String
			Dim suf() As String = { "B", "KB", "MB", "GB", "TB", "PB", "EB" } 'Longs run out around EB
			If byteCount = 0 Then
				Return "0" & suf(0)
			End If
			Dim bytes As Long = Math.Abs(byteCount)
			Dim place As Integer = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)))
			Dim num As Double = Math.Round(bytes / Math.Pow(1024, place), 1)
			Return (Math.Sign(byteCount) * num).ToString() & suf(place)
		End Function

		' Convert byte[][] to string
		Public Shared Function ConvertToString(ByVal p()() As Byte) As String
			Dim sb As New StringBuilder()
			For Each b As Byte() In p
				sb.Append(System.Text.Encoding.Default.GetString(b))
			Next b
			Return sb.ToString()
		End Function
	End Class
End Namespace
