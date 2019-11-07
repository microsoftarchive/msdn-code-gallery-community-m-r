Imports System.Net
Imports System.IO

Public Class FormVBBandwidth
    Private TrafficMonitor As NetworkTraffic = Nothing
    Private LastAmountOfBytesReceived As Single

    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TrafficMonitor = New NetworkTraffic(Process.GetCurrentProcess().Id)
        BandwidthCalcTimer.Interval = 1000
        BandwidthCalcTimer.Enabled = True
    End Sub

    Private Sub refreshBrowserButton_Click(sender As Object, e As EventArgs) Handles RefreshBrowserButton.Click
        Dim webRequest__1 As WebRequest = WebRequest.Create("http://www.andrealveslima.com.br")
        webRequest__1.Method = "GET"
        TestWebBrowser.DocumentStream = webRequest__1.GetResponse().GetResponseStream()
    End Sub

    Private Sub downloadSampleFileButton_Click(sender As Object, e As EventArgs) Handles DownloadSampleFileButton.Click
        Dim url As String = "http://www.microsoft.com/downloads/info.aspx?na=41&srcfamilyid=92ced922-d505-457a-8c9c-84036160639f&srcdisplaylang=en&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f2%2f9%2f6%2f296AAFA4-669A-46FE-9509-93753F7B0F46%2fVS-KB-Brochure-CSharp-Letter-HiRez.pdf"
        Dim client As New WebClient()

        client.DownloadFileAsync(New Uri(url), Path.GetTempFileName())
    End Sub

    Private Sub bandwidthCalcTimer_Tick(sender As Object, e As EventArgs) Handles BandwidthCalcTimer.Tick
        Dim currentAmountOfBytesReceived As Single = TrafficMonitor.GetBytesReceived()
        TotalBandwidthConsumptionLabel.Text = String.Format("Total Bandwidth Consumption: {0} kb", currentAmountOfBytesReceived / 1024)
        CurrentBandwidthConsumptionLabel.Text = String.Format("Current Bandwidth Consumption: {0} kb/sec", (currentAmountOfBytesReceived - LastAmountOfBytesReceived) / 1024)
        LastAmountOfBytesReceived = currentAmountOfBytesReceived
    End Sub

End Class