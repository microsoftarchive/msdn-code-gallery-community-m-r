<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVBBandwidth
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.CurrentBandwidthConsumptionLabel = New System.Windows.Forms.Label()
        Me.BandwidthCalcTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TotalBandwidthConsumptionLabel = New System.Windows.Forms.Label()
        Me.DownloadSampleFileButton = New System.Windows.Forms.Button()
        Me.RefreshBrowserButton = New System.Windows.Forms.Button()
        Me.TestWebBrowser = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'CurrentBandwidthConsumptionLabel
        '
        Me.CurrentBandwidthConsumptionLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CurrentBandwidthConsumptionLabel.AutoSize = True
        Me.CurrentBandwidthConsumptionLabel.Location = New System.Drawing.Point(417, 515)
        Me.CurrentBandwidthConsumptionLabel.Name = "CurrentBandwidthConsumptionLabel"
        Me.CurrentBandwidthConsumptionLabel.Size = New System.Drawing.Size(161, 13)
        Me.CurrentBandwidthConsumptionLabel.TabIndex = 9
        Me.CurrentBandwidthConsumptionLabel.Text = "Current Bandwidth Consumption:"
        '
        'BandwidthCalcTimer
        '
        '
        'TotalBandwidthConsumptionLabel
        '
        Me.TotalBandwidthConsumptionLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TotalBandwidthConsumptionLabel.AutoSize = True
        Me.TotalBandwidthConsumptionLabel.Location = New System.Drawing.Point(13, 515)
        Me.TotalBandwidthConsumptionLabel.Name = "TotalBandwidthConsumptionLabel"
        Me.TotalBandwidthConsumptionLabel.Size = New System.Drawing.Size(151, 13)
        Me.TotalBandwidthConsumptionLabel.TabIndex = 8
        Me.TotalBandwidthConsumptionLabel.Text = "Total Bandwidth Consumption:"
        '
        'DownloadSampleFileButton
        '
        Me.DownloadSampleFileButton.Location = New System.Drawing.Point(828, 510)
        Me.DownloadSampleFileButton.Name = "DownloadSampleFileButton"
        Me.DownloadSampleFileButton.Size = New System.Drawing.Size(129, 23)
        Me.DownloadSampleFileButton.TabIndex = 7
        Me.DownloadSampleFileButton.Text = "Download Sample File"
        Me.DownloadSampleFileButton.UseVisualStyleBackColor = True
        '
        'RefreshBrowserButton
        '
        Me.RefreshBrowserButton.Location = New System.Drawing.Point(708, 510)
        Me.RefreshBrowserButton.Name = "RefreshBrowserButton"
        Me.RefreshBrowserButton.Size = New System.Drawing.Size(114, 23)
        Me.RefreshBrowserButton.TabIndex = 6
        Me.RefreshBrowserButton.Text = "Refresh Browser"
        Me.RefreshBrowserButton.UseVisualStyleBackColor = True
        '
        'TestWebBrowser
        '
        Me.TestWebBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TestWebBrowser.Location = New System.Drawing.Point(1, 6)
        Me.TestWebBrowser.MinimumSize = New System.Drawing.Size(20, 20)
        Me.TestWebBrowser.Name = "TestWebBrowser"
        Me.TestWebBrowser.Size = New System.Drawing.Size(967, 482)
        Me.TestWebBrowser.TabIndex = 5
        '
        'FormVBBandwidth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(968, 539)
        Me.Controls.Add(Me.CurrentBandwidthConsumptionLabel)
        Me.Controls.Add(Me.TotalBandwidthConsumptionLabel)
        Me.Controls.Add(Me.DownloadSampleFileButton)
        Me.Controls.Add(Me.RefreshBrowserButton)
        Me.Controls.Add(Me.TestWebBrowser)
        Me.Name = "FormVBBandwidth"
        Me.Text = "VB Bandwidth"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents CurrentBandwidthConsumptionLabel As System.Windows.Forms.Label
    Friend WithEvents BandwidthCalcTimer As System.Windows.Forms.Timer
    Private WithEvents TotalBandwidthConsumptionLabel As System.Windows.Forms.Label
    Friend WithEvents DownloadSampleFileButton As System.Windows.Forms.Button
    Friend WithEvents RefreshBrowserButton As System.Windows.Forms.Button
    Friend WithEvents TestWebBrowser As System.Windows.Forms.WebBrowser
End Class
