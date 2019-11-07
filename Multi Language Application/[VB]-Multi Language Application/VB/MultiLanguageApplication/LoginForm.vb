Imports System.Globalization
Imports System.Resources
Imports System.Reflection
Public Class LoginForm

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.
#Region "Varibales"
    Dim culture As CultureInfo
    Dim errActive, errBlock, errDelete, errInvalid, errNormal As String
    Dim errActive_Heading, errBlock_Heading, errDelete_Heading, errInvalid_Heading, errNormal_Heading As String
    Dim Lang As String
    'Dim RM As ResourceManager
#End Region
#Region "Form"
    Private Sub LoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CmbLanguage.SelectedIndex = 1
        Me.Activate()
    End Sub
#End Region

#Region "ComboBox Control"
    Private Sub CmbLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLanguage.SelectedIndexChanged
        If Me.CmbLanguage.SelectedIndex = 1 Then 'When Urdu Language Selected
            Lang = "ur"
        Else
            Lang = ""
        End If
        setCulture()
    End Sub
#End Region

#Region "Button Control"
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If Me.UsernameTextBox.Text = Nothing Or Me.PasswordTextBox.Text = Nothing Then Me.UsernameTextBox.Focus() : Exit Sub

        If Me.rBttnActive.Checked = True Then
            MsgBox(errActive, MsgBoxStyle.Exclamation, errActive_Heading)
            Me.UsernameTextBox.Focus()
            Exit Sub
        ElseIf Me.rBttnBlocked.Checked = True Then
            MsgBox(errBlock, MsgBoxStyle.Exclamation, errBlock_Heading)
            Me.UsernameTextBox.Focus()
            Exit Sub
        ElseIf Me.rBttnDeleted.Checked = True Then
            MsgBox(errDelete, MsgBoxStyle.Exclamation, errDelete_Heading)
            Me.UsernameTextBox.Focus()
            Exit Sub
        ElseIf Me.rBttnInvalid.Checked = True Then
            MsgBox(errInvalid, MsgBoxStyle.Exclamation, errInvalid_Heading)
            Me.UsernameTextBox.Focus()
            Exit Sub
        ElseIf Me.rBttnNormal.Checked = True Then
            MsgBox(errNormal, MsgBoxStyle.Exclamation, errNormal_Heading)
            Me.UsernameTextBox.Focus()
            Exit Sub
        Else

        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
#End Region
    
#Region "Sub & Functions"
    Private Sub setCulture()

        culture = CultureInfo.CreateSpecificCulture(Lang)
        Dim rm As New ResourceManager("MultiLanguageApplication.Login", GetType(LoginForm).Assembly)

        Me.Text = rm.GetString("FormTitle", culture)
        Me.UsernameLabel.Text = rm.GetString("LblUserName", culture)
        Me.PasswordLabel.Text = rm.GetString("LblPassword", culture)
        Me.OK.Text = rm.GetString("BttnOK", culture)
        Me.Cancel.Text = rm.GetString("BttnCancel", culture)
        Me.errActive = rm.GetString("ErrActive", culture)
        Me.errBlock = rm.GetString("ErrBlock", culture)
        Me.errDelete = rm.GetString("ErrDelete", culture)
        Me.errInvalid = rm.GetString("ErrInvalid", culture)
        Me.errNormal = rm.GetString("ErrNormal", culture)
        Me.errActive_Heading = rm.GetString("ErrActive_Heading", culture)
        Me.errBlock_Heading = rm.GetString("ErrBlock_Heading", culture)
        Me.errDelete_Heading = rm.GetString("ErrDelete_Heading", culture)
        Me.errInvalid_Heading = rm.GetString("ErrInvalid_Heading", culture)
        Me.errNormal_Heading = rm.GetString("ErrNormal_Heading", culture)
        Me.LblLanguage.Text = rm.GetString("LblLanguage", culture)
        Me.gbErrMessage.Text = rm.GetString("gBoxError", culture)
        Me.rBttnActive.Text = rm.GetString("rBttnActive", culture)
        Me.rBttnBlocked.Text = rm.GetString("rBttnBlocked", culture)
        Me.rBttnDeleted.Text = rm.GetString("rBttnDeleted", culture)
        Me.rBttnNormal.Text = rm.GetString("rBttnNormal", culture)
        Me.rBttnInvalid.Text = rm.GetString("ErrInvalid_Heading", culture)
    End Sub
#End Region



End Class
