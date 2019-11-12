'Imports UD.General.DM
Imports SDS = System.Data.SqlClient
'Imports CrystalDecisions.Shared
Public Class FrmMn
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BttnSearch As gsxpbut10.MyXPButton
    Friend WithEvents BttnPosting As gsxpbut10.MyXPButton
    Friend WithEvents TxtArmyNo As System.Windows.Forms.TextBox
    Friend WithEvents BttnManPowerState As gsxpbut10.MyXPButton
    Friend WithEvents BttnAdvanceSearch As gsxpbut10.MyXPButton
    Friend WithEvents BttnPROFILECARD As gsxpbut10.MyXPButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.BttnSearch = New gsxpbut10.MyXPButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnManPowerState = New gsxpbut10.MyXPButton
        Me.BttnPosting = New gsxpbut10.MyXPButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.BttnPROFILECARD = New gsxpbut10.MyXPButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TxtArmyNo = New System.Windows.Forms.TextBox
        Me.BttnAdvanceSearch = New gsxpbut10.MyXPButton
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'BttnSearch
        '
        Me.BttnSearch.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.BttnSearch.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BttnSearch.BackColor = System.Drawing.Color.Black
        Me.BttnSearch.BtnShape = gsxpbut10.BtnShape.Ellipse
        Me.BttnSearch.BtnStyle = gsxpbut10.XPStyle.Silver
        Me.BttnSearch.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSearch.ForeColor = System.Drawing.Color.Black
        Me.BttnSearch.Location = New System.Drawing.Point(184, 25)
        Me.BttnSearch.Name = "BttnSearch"
        Me.BttnSearch.Size = New System.Drawing.Size(121, 40)
        Me.BttnSearch.TabIndex = 1
        Me.BttnSearch.Text = "SEARCH"
        Me.BttnSearch.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Black
        Me.GroupBox1.Controls.Add(Me.BttnManPowerState)
        Me.GroupBox1.Controls.Add(Me.BttnPosting)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(160, 486)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reports Section"
        '
        'BttnManPowerState
        '
        Me.BttnManPowerState.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.BttnManPowerState.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BttnManPowerState.BtnShape = gsxpbut10.BtnShape.Rectangle
        Me.BttnManPowerState.BtnStyle = gsxpbut10.XPStyle.Silver
        Me.BttnManPowerState.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnManPowerState.ForeColor = System.Drawing.Color.Black
        Me.BttnManPowerState.Location = New System.Drawing.Point(8, 32)
        Me.BttnManPowerState.Name = "BttnManPowerState"
        Me.BttnManPowerState.Size = New System.Drawing.Size(144, 40)
        Me.BttnManPowerState.TabIndex = 1
        Me.BttnManPowerState.TabStop = False
        Me.BttnManPowerState.Text = "Button1"
        '
        'BttnPosting
        '
        Me.BttnPosting.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.BttnPosting.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.BttnPosting.BtnShape = gsxpbut10.BtnShape.Ellipse
        Me.BttnPosting.BtnStyle = gsxpbut10.XPStyle.Silver
        Me.BttnPosting.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPosting.ForeColor = System.Drawing.Color.Black
        Me.BttnPosting.Location = New System.Drawing.Point(8, 80)
        Me.BttnPosting.Name = "BttnPosting"
        Me.BttnPosting.Size = New System.Drawing.Size(144, 40)
        Me.BttnPosting.TabIndex = 2
        Me.BttnPosting.TabStop = False
        Me.BttnPosting.Text = "Button2"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Black
        Me.GroupBox2.Controls.Add(Me.GroupBox4)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Right
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(614, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(160, 486)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox4.Location = New System.Drawing.Point(3, 339)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(154, 144)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Other Reports"
        '
        'BttnPROFILECARD
        '
        Me.BttnPROFILECARD.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.BttnPROFILECARD.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BttnPROFILECARD.BtnShape = gsxpbut10.BtnShape.Rectangle
        Me.BttnPROFILECARD.BtnStyle = gsxpbut10.XPStyle.Silver
        Me.BttnPROFILECARD.Enabled = False
        Me.BttnPROFILECARD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPROFILECARD.ForeColor = System.Drawing.Color.Black
        Me.BttnPROFILECARD.Location = New System.Drawing.Point(328, 24)
        Me.BttnPROFILECARD.Name = "BttnPROFILECARD"
        Me.BttnPROFILECARD.Size = New System.Drawing.Size(104, 40)
        Me.BttnPROFILECARD.TabIndex = 3
        Me.BttnPROFILECARD.TabStop = False
        Me.BttnPROFILECARD.Text = "PROFILE CARD"
        Me.BttnPROFILECARD.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Black
        Me.GroupBox3.Controls.Add(Me.TxtArmyNo)
        Me.GroupBox3.Controls.Add(Me.BttnSearch)
        Me.GroupBox3.Controls.Add(Me.BttnAdvanceSearch)
        Me.GroupBox3.Controls.Add(Me.BttnPROFILECARD)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(160, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(454, 80)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Search"
        '
        'TxtArmyNo
        '
        Me.TxtArmyNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TxtArmyNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TxtArmyNo.Font = New System.Drawing.Font("Verdana", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtArmyNo.ForeColor = System.Drawing.Color.Black
        Me.TxtArmyNo.Location = New System.Drawing.Point(8, 25)
        Me.TxtArmyNo.MaxLength = 7
        Me.TxtArmyNo.Name = "TxtArmyNo"
        Me.TxtArmyNo.Size = New System.Drawing.Size(169, 40)
        Me.TxtArmyNo.TabIndex = 0
        Me.TxtArmyNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BttnAdvanceSearch
        '
        Me.BttnAdvanceSearch.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.BttnAdvanceSearch.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BttnAdvanceSearch.BackColor = System.Drawing.Color.Black
        Me.BttnAdvanceSearch.BtnShape = gsxpbut10.BtnShape.Ellipse
        Me.BttnAdvanceSearch.BtnStyle = gsxpbut10.XPStyle.Silver
        Me.BttnAdvanceSearch.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnAdvanceSearch.ForeColor = System.Drawing.Color.Black
        Me.BttnAdvanceSearch.Location = New System.Drawing.Point(320, 25)
        Me.BttnAdvanceSearch.Name = "BttnAdvanceSearch"
        Me.BttnAdvanceSearch.Size = New System.Drawing.Size(120, 40)
        Me.BttnAdvanceSearch.TabIndex = 2
        Me.BttnAdvanceSearch.TabStop = False
        Me.BttnAdvanceSearch.Text = "A&dv Search"
        Me.BttnAdvanceSearch.UseVisualStyleBackColor = False
        '
        'FrmMn
        '
        Me.AcceptButton = Me.BttnSearch
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.Firebrick
        Me.ClientSize = New System.Drawing.Size(774, 486)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMn"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Variable Sections"
    Dim AsSelect As New AssSelect
    Dim Rd As SDS.SqlDataReader
    Dim AsConn As New AssConn
    Dim FrmM As FrmMain
    Dim AsNum As New AssNumPress
    'Public Const LogInUser As String = "COMDT"
    'Dim cLogOnIf As New CrystalDecisions.Shared.TableLogOnInfo
    'Dim myConnectionInfo As ConnectionInfo = New ConnectionInfo

#End Region

#Region "Form Control Events"
    Private Sub FrmMn_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Me.TxtArmyNo.Focus()
    End Sub

    
#End Region

#Region "Form SEARCH Control Events"
    Private Sub BttnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSearch.Click
      
    End Sub
    Private Sub BttnAdvanceSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAdvanceSearch.Click
        
    End Sub
    Private Sub BttnPROFILECARD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPROFILECARD.Click

    End Sub
#End Region

#Region "Main Menu Events Control"
    
#End Region

#Region "TextBox Events Controls"
    Private Sub TxtArmyNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtArmyNo.GotFocus
        CType(sender, TextBox).SelectAll()
    End Sub
#End Region

#Region "Entry Form Section"

#End Region

#Region "Reports Section"

#End Region


#Region "Sub & Function Declaration"
    'Private Sub SetDBLogonForReport(ByVal myConnectionInfo As ConnectionInfo)
    '    myConnectionInfo.ServerName = "server"
    '    myConnectionInfo.DatabaseName = "UD-AMC"
    '    myConnectionInfo.UserID = "isharpk"
    '    myConnectionInfo.Password = "ishardreams"

    '    Dim myTableLogOnInfos As TableLogOnInfos = FrmR.CRV.LogOnInfo
    '    For Each myTableLogOnInfo As TableLogOnInfo In myTableLogOnInfos
    '        myTableLogOnInfo.ConnectionInfo = myConnectionInfo
    '    Next
    'End Sub
#End Region


End Class

