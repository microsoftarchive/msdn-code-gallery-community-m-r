Imports System
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Linq

''' <summary>
''' Class that gets the network traffic from the performance counter.
''' Based on: http://pastebin.com/f371375d6
''' </summary>
Public Class NetworkTraffic
    Private bytesSentPerformanceCounter As PerformanceCounter
    Private bytesReceivedPerformanceCounter As PerformanceCounter
    Private pid As Integer
    Private countersInitialized As Boolean

    Public Sub New(processID As Integer)
        pid = processID
        TryToInitializeCounters()
    End Sub

    Private Sub TryToInitializeCounters()
        If Not countersInitialized Then
            Dim category As New PerformanceCounterCategory(".NET CLR Networking 4.0.0.0")

            Dim instanceNames = category.GetInstanceNames().Where(Function(i) i.Contains(String.Format("p{0}", pid)))

            If instanceNames.Any() Then
                bytesSentPerformanceCounter = New PerformanceCounter()
                bytesSentPerformanceCounter.CategoryName = ".NET CLR Networking 4.0.0.0"
                bytesSentPerformanceCounter.CounterName = "Bytes Sent"
                bytesSentPerformanceCounter.InstanceName = instanceNames.First()
                bytesSentPerformanceCounter.[ReadOnly] = True

                bytesReceivedPerformanceCounter = New PerformanceCounter()
                bytesReceivedPerformanceCounter.CategoryName = ".NET CLR Networking 4.0.0.0"
                bytesReceivedPerformanceCounter.CounterName = "Bytes Received"
                bytesReceivedPerformanceCounter.InstanceName = instanceNames.First()
                bytesReceivedPerformanceCounter.[ReadOnly] = True

                countersInitialized = True
            End If
        End If
    End Sub

    Public Function GetBytesSent() As Single
        Dim bytesSent As Single = 0

        Try
            TryToInitializeCounters()
            bytesSent = bytesSentPerformanceCounter.RawValue
        Catch
        End Try

        Return bytesSent
    End Function

    Public Function GetBytesReceived() As Single
        Dim bytesSent As Single = 0

        Try
            TryToInitializeCounters()
            bytesSent = bytesReceivedPerformanceCounter.RawValue
        Catch
        End Try

        Return bytesSent
    End Function
End Class

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik, @toddanglin
'Facebook: facebook.com/telerik
'=======================================================
