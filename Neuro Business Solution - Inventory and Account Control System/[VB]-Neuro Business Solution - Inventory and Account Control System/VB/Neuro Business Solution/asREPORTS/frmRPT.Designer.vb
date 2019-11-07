<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRPT
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.BttnClose = New System.Windows.Forms.Button
        Me.CRV = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Location = New System.Drawing.Point(512, 12)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 23)
        Me.BttnClose.TabIndex = 0
        Me.BttnClose.TabStop = False
        Me.BttnClose.Text = "Close"
        Me.BttnClose.UseVisualStyleBackColor = True
        Me.BttnClose.Visible = False
        '
        'CRV
        '
        Me.CRV.ActiveViewIndex = -1
        Me.CRV.AutoScroll = True
        Me.CRV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRV.DisplayGroupTree = False
        Me.CRV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRV.EnableToolTips = False
        Me.CRV.Location = New System.Drawing.Point(0, 0)
        Me.CRV.Name = "CRV"
        Me.CRV.SelectionFormula = ""
        Me.CRV.Size = New System.Drawing.Size(776, 519)
        Me.CRV.TabIndex = 1
        Me.CRV.ViewTimeSelectionFormula = ""
        '
        'frmRPT
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(776, 519)
        Me.Controls.Add(Me.CRV)
        Me.Controls.Add(Me.BttnClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmRPT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "REPORT"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents CRV As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
