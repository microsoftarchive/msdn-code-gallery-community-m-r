Imports SDS = System.Data.SqlClient
Public Class frmLUP_BUSINESS_INFO
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TxtBusinessName As System.Windows.Forms.TextBox
    Friend WithEvents TxtCity As System.Windows.Forms.TextBox
    Friend WithEvents TxtAddress As System.Windows.Forms.TextBox
    Friend WithEvents TxtPh As System.Windows.Forms.TextBox
    Friend WithEvents TxtEMail As System.Windows.Forms.TextBox
    Friend WithEvents TxtFax As System.Windows.Forms.TextBox
    Friend WithEvents TxtCell As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TxtWebSite As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daLUP_BUSINESS_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLUP_BUSINESS_INFO1 As Neruo_Business_Solution.dsLUP_BUSINESS_INFO
    Friend WithEvents Label15 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_BUSINESS_INFO))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TxtBusinessName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtCity = New System.Windows.Forms.TextBox
        Me.TxtAddress = New System.Windows.Forms.TextBox
        Me.TxtPh = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtEMail = New System.Windows.Forms.TextBox
        Me.TxtFax = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtCell = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtWebSite = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_BUSINESS_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.DsLUP_BUSINESS_INFO1 = New Neruo_Business_Solution.dsLUP_BUSINESS_INFO
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsLUP_BUSINESS_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(640, 304)
        Me.Panel1.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(638, 48)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "BUSINESS INFORMATION"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 210)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(614, 75)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(335, 19)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 37)
        Me.BttnClose.TabIndex = 1
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(190, 19)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 37)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.TxtBusinessName)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.TxtCity)
        Me.GroupBox3.Controls.Add(Me.TxtAddress)
        Me.GroupBox3.Controls.Add(Me.TxtPh)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TxtEMail)
        Me.GroupBox3.Controls.Add(Me.TxtFax)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.TxtCell)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.TxtWebSite)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 51)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(614, 153)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'TxtBusinessName
        '
        Me.TxtBusinessName.BackColor = System.Drawing.Color.White
        Me.TxtBusinessName.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_INFO1, "BUSINESS_INFO.sBUSINESS_NAME", True))
        Me.TxtBusinessName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBusinessName.Location = New System.Drawing.Point(121, 21)
        Me.TxtBusinessName.MaxLength = 50
        Me.TxtBusinessName.Name = "TxtBusinessName"
        Me.TxtBusinessName.Size = New System.Drawing.Size(250, 23)
        Me.TxtBusinessName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Business Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCity
        '
        Me.TxtCity.BackColor = System.Drawing.Color.White
        Me.TxtCity.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_INFO1, "BUSINESS_INFO.sCITY", True))
        Me.TxtCity.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCity.Location = New System.Drawing.Point(121, 45)
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(250, 23)
        Me.TxtCity.TabIndex = 3
        '
        'TxtAddress
        '
        Me.TxtAddress.BackColor = System.Drawing.Color.White
        Me.TxtAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_INFO1, "BUSINESS_INFO.sADDRESS", True))
        Me.TxtAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(121, 69)
        Me.TxtAddress.MaxLength = 150
        Me.TxtAddress.Multiline = True
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(250, 47)
        Me.TxtAddress.TabIndex = 5
        '
        'TxtPh
        '
        Me.TxtPh.BackColor = System.Drawing.Color.White
        Me.TxtPh.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_INFO1, "BUSINESS_INFO.sPHONE", True))
        Me.TxtPh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPh.Location = New System.Drawing.Point(449, 21)
        Me.TxtPh.Name = "TxtPh"
        Me.TxtPh.Size = New System.Drawing.Size(152, 23)
        Me.TxtPh.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(377, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 23)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Ph. #"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(377, 93)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 23)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "E-mail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtEMail
        '
        Me.TxtEMail.BackColor = System.Drawing.Color.White
        Me.TxtEMail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_INFO1, "BUSINESS_INFO.sE_MAIL", True))
        Me.TxtEMail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEMail.Location = New System.Drawing.Point(449, 93)
        Me.TxtEMail.Name = "TxtEMail"
        Me.TxtEMail.Size = New System.Drawing.Size(152, 23)
        Me.TxtEMail.TabIndex = 13
        '
        'TxtFax
        '
        Me.TxtFax.BackColor = System.Drawing.Color.White
        Me.TxtFax.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_INFO1, "BUSINESS_INFO.sFAX", True))
        Me.TxtFax.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(449, 69)
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(152, 23)
        Me.TxtFax.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(377, 69)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 23)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Fax #"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(377, 45)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 23)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "Cell #"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCell
        '
        Me.TxtCell.BackColor = System.Drawing.Color.White
        Me.TxtCell.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_INFO1, "BUSINESS_INFO.sCELL_NO", True))
        Me.TxtCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCell.Location = New System.Drawing.Point(449, 45)
        Me.TxtCell.Name = "TxtCell"
        Me.TxtCell.Size = New System.Drawing.Size(152, 23)
        Me.TxtCell.TabIndex = 9
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(377, 117)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 23)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "Web Site"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtWebSite
        '
        Me.TxtWebSite.BackColor = System.Drawing.Color.White
        Me.TxtWebSite.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLUP_BUSINESS_INFO1, "BUSINESS_INFO.sWEB_ADDRESS", True))
        Me.TxtWebSite.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWebSite.Location = New System.Drawing.Point(449, 117)
        Me.TxtWebSite.Name = "TxtWebSite"
        Me.TxtWebSite.Size = New System.Drawing.Size(152, 23)
        Me.TxtWebSite.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 68)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 48)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Business Address"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 45)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(107, 23)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "City"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     nID, sBUSINESS_NAME, sADDRESS, sCITY, sPHONE, sCELL_NO, sFAX, sWEB_ADD" & _
            "RESS, sE_MAIL" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         BUSINESS_INFO"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=SERVER;Initial Catalog=Neuro_BS;Integrated Security=True;Connect Time" & _
            "out=30"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = resources.GetString("SqlInsertCommand1.CommandText")
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sBUSINESS_NAME", System.Data.SqlDbType.VarChar, 0, "sBUSINESS_NAME"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.NVarChar, 0, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sCITY", System.Data.SqlDbType.VarChar, 0, "sCITY"), New System.Data.SqlClient.SqlParameter("@sPHONE", System.Data.SqlDbType.VarChar, 0, "sPHONE"), New System.Data.SqlClient.SqlParameter("@sCELL_NO", System.Data.SqlDbType.VarChar, 0, "sCELL_NO"), New System.Data.SqlClient.SqlParameter("@sFAX", System.Data.SqlDbType.VarChar, 0, "sFAX"), New System.Data.SqlClient.SqlParameter("@sWEB_ADDRESS", System.Data.SqlDbType.VarChar, 0, "sWEB_ADDRESS"), New System.Data.SqlClient.SqlParameter("@sE_MAIL", System.Data.SqlDbType.VarChar, 0, "sE_MAIL")})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sBUSINESS_NAME", System.Data.SqlDbType.VarChar, 0, "sBUSINESS_NAME"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.NVarChar, 0, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sCITY", System.Data.SqlDbType.VarChar, 0, "sCITY"), New System.Data.SqlClient.SqlParameter("@sPHONE", System.Data.SqlDbType.VarChar, 0, "sPHONE"), New System.Data.SqlClient.SqlParameter("@sCELL_NO", System.Data.SqlDbType.VarChar, 0, "sCELL_NO"), New System.Data.SqlClient.SqlParameter("@sFAX", System.Data.SqlDbType.VarChar, 0, "sFAX"), New System.Data.SqlClient.SqlParameter("@sWEB_ADDRESS", System.Data.SqlDbType.VarChar, 0, "sWEB_ADDRESS"), New System.Data.SqlClient.SqlParameter("@sE_MAIL", System.Data.SqlDbType.VarChar, 0, "sE_MAIL"), New System.Data.SqlClient.SqlParameter("@Original_nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sBUSINESS_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sBUSINESS_NAME", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sBUSINESS_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sBUSINESS_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCITY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCITY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCITY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCITY", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sPHONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sPHONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sPHONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPHONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCELL_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCELL_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sFAX", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sFAX", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sFAX", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sFAX", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sWEB_ADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sWEB_ADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sWEB_ADDRESS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sWEB_ADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sE_MAIL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sE_MAIL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = resources.GetString("SqlDeleteCommand1.CommandText")
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sBUSINESS_NAME", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sBUSINESS_NAME", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sBUSINESS_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sBUSINESS_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCITY", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCITY", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCITY", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCITY", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sPHONE", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sPHONE", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sPHONE", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPHONE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCELL_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCELL_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sFAX", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sFAX", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sFAX", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sFAX", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sWEB_ADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sWEB_ADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sWEB_ADDRESS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sWEB_ADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sE_MAIL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sE_MAIL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daLUP_BUSINESS_INFO
        '
        Me.daLUP_BUSINESS_INFO.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_BUSINESS_INFO.InsertCommand = Me.SqlInsertCommand1
        Me.daLUP_BUSINESS_INFO.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_BUSINESS_INFO.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "BUSINESS_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nID", "nID"), New System.Data.Common.DataColumnMapping("sBUSINESS_NAME", "sBUSINESS_NAME"), New System.Data.Common.DataColumnMapping("sADDRESS", "sADDRESS"), New System.Data.Common.DataColumnMapping("sCITY", "sCITY"), New System.Data.Common.DataColumnMapping("sPHONE", "sPHONE"), New System.Data.Common.DataColumnMapping("sCELL_NO", "sCELL_NO"), New System.Data.Common.DataColumnMapping("sFAX", "sFAX"), New System.Data.Common.DataColumnMapping("sWEB_ADDRESS", "sWEB_ADDRESS"), New System.Data.Common.DataColumnMapping("sE_MAIL", "sE_MAIL")})})
        Me.daLUP_BUSINESS_INFO.UpdateCommand = Me.SqlUpdateCommand1
        '
        'DsLUP_BUSINESS_INFO1
        '
        Me.DsLUP_BUSINESS_INFO1.DataSetName = "dsLUP_BUSINESS_INFO"
        Me.DsLUP_BUSINESS_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_BUSINESS_INFO
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(658, 320)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_BUSINESS_INFO"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BUSINESS INFORMATION"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsLUP_BUSINESS_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asInsert As New AssInsert
    Dim asUpdate As New AssUpdate
    Dim asDelete As New AssDelete
    Dim asSELECT As New AssSelect
    Dim asTXT As New AssTextBox
    Dim asNum As New AssNumPress
    Dim asMAX As New AssMaxNo
    Dim Rd As System.Data.SqlClient.SqlDataReader

#End Region

#Region "FORM CONTROL"
    Private Sub frmLUP_BUSINESS_INFO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.daLUP_BUSINESS_INFO.Fill(Me.DsLUP_BUSINESS_INFO1.BUSINESS_INFO)
    End Sub

    Private Sub frmLUP_BUSINESS_INFO_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtBusinessName.GotFocus, TxtAddress.GotFocus, TxtCity.GotFocus, TxtFax.GotFocus, TxtEMail.GotFocus, TxtCell.GotFocus, TxtPh.GotFocus, TxtWebSite.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtBusinessName.LostFocus, TxtAddress.LostFocus, TxtCity.LostFocus, TxtFax.LostFocus, TxtEMail.LostFocus, TxtCell.LostFocus, TxtPh.LostFocus, TxtWebSite.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    'KeyPress Numeric With Desh
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFax.KeyPress, TxtCell.KeyPress, TxtPh.KeyPress
        Me.asNum.NumPressDash(e)
    End Sub
#End Region

#Region "ListView Control"

#End Region

#Region "Button Control"
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM BUSINESS_INFO")

        If Me.TxtBusinessName.Text = Nothing Or Me.TxtCity.Text = Nothing Or Me.TxtAddress.Text = Nothing Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtBusinessName.Text = Nothing Then
                Me.TxtBusinessName.Focus()

            ElseIf Me.TxtCity.Text = Nothing Then
                Me.TxtCity.Focus()

            ElseIf Me.TxtAddress.Text = Nothing Then
                Me.TxtAddress.Focus()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            If MsgBox("Do you want to save '" & Me.TxtBusinessName.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO BUSINESS_INFO(nID, sBUSINESS_NAME, sADDRESS, sCITY, sPHONE, sCELL_NO, sFAX, sWEB_ADDRESS, sE_MAIL) VALUES(1,'" & Me.TxtBusinessName.Text & "','" & Me.TxtAddress.Text & "','" & Me.TxtCity.Text & "','" & Me.TxtPh.Text & "','" & Me.TxtCell.Text & "','" & Me.TxtFax.Text & "','" & Me.TxtWebSite.Text & "','" & Me.TxtEMail.Text & "') ")

                Me.TxtBusinessName.Focus()
            End If

        ElseIf Me.asSELECT.pFlg1 = True Then
            If MsgBox("This Business Name: '" & Me.TxtBusinessName.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE BUSINESS_INFO SET sBUSINESS_NAME='" & Me.TxtBusinessName.Text & "', sADDRESS='" & Me.TxtAddress.Text & "', sCITY='" & Me.TxtCity.Text & "', sPHONE='" & Me.TxtPh.Text & "', sCELL_NO='" & Me.TxtCell.Text & "', sFAX='" & Me.TxtFax.Text & "', sWEB_ADDRESS='" & Me.TxtWebSite.Text & "', sE_MAIL='" & Me.TxtEMail.Text & "' WHERE nID=1")
                Me.TxtBusinessName.Focus()
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Sub and Functions"
  
#End Region

End Class
