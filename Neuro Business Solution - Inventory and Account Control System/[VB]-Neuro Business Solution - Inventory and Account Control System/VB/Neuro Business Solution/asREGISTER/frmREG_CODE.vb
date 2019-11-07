Imports Microsoft.Win32
Imports System
Imports System.IO
Imports System.Text
Imports System.Security
Imports System.Security.Cryptography
Public Class frmREG_CODE
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtActCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtInsCode As System.Windows.Forms.TextBox
    Friend WithEvents BttnActivate As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtActCode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.BttnActivate = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.TxtInsCode = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(232, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Installation Code"
        '
        'TxtActCode
        '
        Me.TxtActCode.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtActCode.Location = New System.Drawing.Point(8, 128)
        Me.TxtActCode.Name = "TxtActCode"
        Me.TxtActCode.Size = New System.Drawing.Size(232, 27)
        Me.TxtActCode.TabIndex = 0
        Me.TxtActCode.Text = ""
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(232, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Activation Code"
        '
        'BttnActivate
        '
        Me.BttnActivate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnActivate.Location = New System.Drawing.Point(8, 168)
        Me.BttnActivate.Name = "BttnActivate"
        Me.BttnActivate.Size = New System.Drawing.Size(80, 40)
        Me.BttnActivate.TabIndex = 2
        Me.BttnActivate.Text = "&Activate"
        '
        'BttnClose
        '
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(160, 168)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(80, 40)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'TxtInsCode
        '
        Me.TxtInsCode.BackColor = System.Drawing.Color.White
        Me.TxtInsCode.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInsCode.Location = New System.Drawing.Point(8, 72)
        Me.TxtInsCode.Name = "TxtInsCode"
        Me.TxtInsCode.ReadOnly = True
        Me.TxtInsCode.Size = New System.Drawing.Size(232, 27)
        Me.TxtInsCode.TabIndex = 0
        Me.TxtInsCode.Text = ""
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(144, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 40)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "&Activate"
        '
        'frmREG_CODE
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(248, 221)
        Me.ControlBox = False
        Me.Controls.Add(Me.BttnActivate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtInsCode)
        Me.Controls.Add(Me.TxtActCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.BttnClose)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmREG_CODE"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registration Form"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub
    Private Sub frmREG_CODE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Str As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0\Target Id 0\Logical Unit Id 0"
        Dim StrGet As String = "Identifier"
        Dim Reg As RegistryKey
        Try
            Reg = Registry.LocalMachine.OpenSubKey(Str.ToString)
            Me.TxtInsCode.Text = Reg.GetValue(StrGet.ToString)
            Reg.Close()
        Catch ex As Exception
        End Try

        If TxtInsCode.Text = "" Then
            Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0\Target Id 1\Logical Unit Id 0"
            Dim StrGet1 As String = "Identifier"
            Dim Reg1 As RegistryKey
            Try
                Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
                Me.TxtInsCode.Text = Reg1.GetValue(StrGet1.ToString)
                Reg1.Close()
            Catch ex As Exception
            End Try

        Else
            Exit Sub

        End If

        If TxtInsCode.Text = "" Then
            Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 1\Scsi Bus 0\Target Id 0\Logical Unit Id 0"
            Dim StrGet1 As String = "Identifier"
            Dim Reg1 As RegistryKey
            Try
                Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
                Me.TxtInsCode.Text = Reg1.GetValue(StrGet1.ToString)
                Reg1.Close()
            Catch ex As Exception
            End Try

        Else
            Exit Sub

        End If

        If TxtInsCode.Text = "" Then
            Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 1\Scsi Bus 0\Target Id 1\Logical Unit Id 0"
            Dim StrGet1 As String = "Identifier"
            Dim Reg1 As RegistryKey
            Try
                Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
                Me.TxtInsCode.Text = Reg1.GetValue(StrGet1.ToString)
                Reg1.Close()
            Catch ex As Exception
            End Try

        Else
            Exit Sub

        End If
    End Sub
    Private Sub BttnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnActivate.Click
        Dim Str_Reg As String = EncryptText(Me.TxtInsCode.Text)
        Dim i As Integer = 0
        If Str_Reg = Me.TxtActCode.Text Then
            MsgBox("Activated!")
        Else
            MsgBox("Wrong Activation Code!")
            i += 1
            If i = 2 Then
                Me.Close()
            End If
        End If
    End Sub





    Public Shared Function EncryptText(ByVal strText As String) As String
        Return Encrypt(strText, "NEUROSOFT")
    End Function
    Private Shared Function Encrypt(ByVal strText As String, ByVal strEncrKey As String) As String

        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(strEncrKey.ToString().Substring(0, 8))
        Dim des As New DESCryptoServiceProvider
        Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
        Dim ms As New MemoryStream
        Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()
        Return Convert.ToBase64String(ms.ToArray()).ToUpper

    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Frm As New frmROTARENEG_YEK
        Frm.Show()
    End Sub
End Class
