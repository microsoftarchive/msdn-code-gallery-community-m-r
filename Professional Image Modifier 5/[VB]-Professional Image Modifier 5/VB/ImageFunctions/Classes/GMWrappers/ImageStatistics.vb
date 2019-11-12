Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.IO

Namespace ImageFunctions.Classes
	Friend Class ImageStatistics

		Private p As New Process()

		Private CurrentImage As String
		Private Stats As New ArrayList()

		Public Delegate Sub StatisticsCompleteHandler(ByVal Identity As ArrayList)
		Public Event StatsComplete As StatisticsCompleteHandler
		Public Delegate Sub StatisticsErrorHandler(ByVal Message As String)
		Public Event StatsError As StatisticsErrorHandler

		Public Sub New(ByVal CurrentImage As String)
			Me.CurrentImage = CurrentImage
		End Sub

		' Run Gm.exe and read its results saving them into output.
		Public Sub GetStatistics()
			Dim path As String = System.IO.Path.Combine(Application.StartupPath, "Binn\gm") 'TODO: This should also have an automated search function
			path = System.IO.Path.Combine(path, "gm.exe")

			Dim arguments As String = String.Format("identify -verbose " & """" & Me.CurrentImage & """")
			Dim startInfo = New ProcessStartInfo With {.Arguments = arguments, .FileName = path, .CreateNoWindow = True, .RedirectStandardError = True, .RedirectStandardOutput = True, .UseShellExecute = False}

			p.EnableRaisingEvents = True
			p.StartInfo = startInfo
			p.Start()

			AddHandler p.ErrorDataReceived, AddressOf p_ErrorDataReceived

			'	string error = p.StandardError.ReadToEnd();
			Dim output As String = p.StandardOutput.ReadToEnd()

			p.WaitForExit()

			If output.Length > 0 Then
				OutputDataReceived(output)
			End If
			'	if (error.Length > 0) StatsError(error);

			RaiseEvent StatsComplete(Stats)
		End Sub

		Private Sub p_ErrorDataReceived(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
			RaiseEvent StatsError(e.Data)
			p.Dispose()

		End Sub

		' Process the results of GM.EXE
		Private Sub OutputDataReceived(ByVal output As String)
			Dim lines() As String = output.Split(ControlChars.Cr)
			Dim result As String = ""

			For idx As Integer = 0 To lines.Count() - 1
				Dim parts() As String = lines(idx).Replace("\", "").Split(":"c) ' May not produce desired results if part[1] contains a : But we will know that if there are more that 2 parts.

				Stats.Add(parts(0).ToString())
				If idx = 0 Then
					' This contains the path to the file so needs special treatment *fudging*
					result = lines(idx).Replace(parts(0).ToString(), "").Substring(2).Trim()
				Else
					If parts.Count() > 1 Then
						For idx2 As Integer = 1 To parts.Count() - 1 ' Start loop on 1 as we already added part[0] to the array.
							result &= parts(idx2).ToString().Trim() ' Remove additional spaces
						Next idx2
					Else
						result = ""
					End If
				End If
				Stats.Add(result)
				result = ""
			Next idx
		End Sub


	End Class
End Namespace
