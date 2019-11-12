Imports System
Imports System.IO
Imports System.Text
Imports System.Security
Imports System.Security.Cryptography
Imports Microsoft.Win32

Public Class frmROTARENEG_YEK
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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(136, 192)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "GENERATE"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(32, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 23)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Installation Code"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(136, 159)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(288, 20)
        Me.TextBox2.TabIndex = 5
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(136, 72)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(288, 20)
        Me.TextBox1.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(32, 159)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 23)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Activation Code"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(352, 192)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "CLOSE"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(136, 98)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(288, 20)
        Me.TextBox3.TabIndex = 4
        '
        'frmROTARENEG_YEK
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(496, 261)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button2)
        Me.Name = "frmROTARENEG_YEK"
        Me.Text = "KEY GENERATOR"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Shared Function EncryptText(ByVal strText As String) As String
        Return Encrypt(strText, "Neuro4742")
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

    Private Sub frmTESTING_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Str As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0\Target Id 0\Logical Unit Id 0"
        Dim StrGet As String = "Identifier"
        Dim Reg As RegistryKey
        Try
            Reg = Registry.LocalMachine.OpenSubKey(Str.ToString)
            Me.TextBox1.Text = Reg.GetValue(StrGet.ToString)
            Reg.Close()
        Catch ex As Exception
        End Try

        If TextBox1.Text = "" Then
            Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 0\Scsi Bus 0\Target Id 1\Logical Unit Id 0"
            Dim StrGet1 As String = "Identifier"
            Dim Reg1 As RegistryKey
            Try
                Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
                Me.TextBox1.Text = Reg1.GetValue(StrGet1.ToString)
                Reg1.Close()
            Catch ex As Exception
            End Try

        Else
            Exit Sub

        End If

        If TextBox1.Text = "" Then
            Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 1\Scsi Bus 0\Target Id 0\Logical Unit Id 0"
            Dim StrGet1 As String = "Identifier"
            Dim Reg1 As RegistryKey
            Try
                Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
                Me.TextBox1.Text = Reg1.GetValue(StrGet1.ToString)
                Reg1.Close()
            Catch ex As Exception
            End Try

        Else
            Exit Sub

        End If

        If TextBox1.Text = "" Then
            Dim Str1 As String = "Hardware\DEVICEMAP\Scsi\Scsi Port 1\Scsi Bus 0\Target Id 1\Logical Unit Id 0"
            Dim StrGet1 As String = "Identifier"
            Dim Reg1 As RegistryKey
            Try
                Reg1 = Registry.LocalMachine.OpenSubKey(Str1.ToString)
                Me.TextBox1.Text = Reg1.GetValue(StrGet1.ToString)
                Reg1.Close()
            Catch ex As Exception
            End Try

        Else
            Exit Sub

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.TextBox2.Text = EncryptText(Me.TextBox1.Text)
    End Sub

End Class
