Imports SDS = System.Data.SqlClient
Public Class frmLUP_SUPPLIER
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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BttnNew As System.Windows.Forms.Button
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnSave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TxtCompany As System.Windows.Forms.TextBox
    Friend WithEvents TxtSupplierID As System.Windows.Forms.TextBox
    Friend WithEvents TxtDesignation As System.Windows.Forms.TextBox
    Friend WithEvents TxtAddress As System.Windows.Forms.TextBox
    Friend WithEvents TxtCoyPh As System.Windows.Forms.TextBox
    Friend WithEvents TxtPrsnPh As System.Windows.Forms.TextBox
    Friend WithEvents TxtEMail As System.Windows.Forms.TextBox
    Friend WithEvents TxtFax As System.Windows.Forms.TextBox
    Friend WithEvents TxtCell As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DmnSTATUS As System.Windows.Forms.DomainUpDown
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TxtWebSite As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtContactPerson As System.Windows.Forms.TextBox
    Friend WithEvents BttnAdd As System.Windows.Forms.Button
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents daSUPPLIER_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsSUPPLIER_INFO11 As Neruo_Business_Solution.dsSUPPLIER_INFO1
    Friend WithEvents DsSUPPLIER_INFO1 As Neruo_Business_Solution.dsSUPPLIER_INFO
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_SUPPLIER))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.BttnAdd = New System.Windows.Forms.Button
        Me.DmnSTATUS = New System.Windows.Forms.DomainUpDown
        Me.DsSUPPLIER_INFO1 = New Neruo_Business_Solution.dsSUPPLIER_INFO
        Me.TxtCompany = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtSupplierID = New System.Windows.Forms.TextBox
        Me.TxtDesignation = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtAddress = New System.Windows.Forms.TextBox
        Me.TxtCoyPh = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtPrsnPh = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtEMail = New System.Windows.Forms.TextBox
        Me.TxtFax = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtCell = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtWebSite = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtContactPerson = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daSUPPLIER_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.DsSUPPLIER_INFO11 = New Neruo_Business_Solution.dsSUPPLIER_INFO1
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsSUPPLIER_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsSUPPLIER_INFO11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnNextForm)
        Me.Panel1.Controls.Add(Me.BttnPrevForm)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(680, 472)
        Me.Panel1.TabIndex = 0
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(578, 3)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(97, 45)
        Me.BttnNextForm.TabIndex = 23
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Supplier Open Bal."
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(8, 3)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(94, 45)
        Me.BttnPrevForm.TabIndex = 22
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Client Open Balance"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 259)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(664, 205)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader3, Me.ColumnHeader5})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 53)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(658, 149)
        Me.ListView1.TabIndex = 2
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "ID"
        Me.ColumnHeader1.Width = 97
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Company"
        Me.ColumnHeader2.Width = 162
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Contact Prsn"
        Me.ColumnHeader4.Width = 131
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Coy Ph."
        Me.ColumnHeader3.Width = 132
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Prsn Ph."
        Me.ColumnHeader5.Width = 132
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 23)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Sea&rch"
        '
        'TxtSearch
        '
        Me.TxtSearch.BackColor = System.Drawing.Color.White
        Me.TxtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearch.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.Location = New System.Drawing.Point(72, 24)
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(136, 23)
        Me.TxtSearch.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox1.Controls.Add(Me.BttnNew)
        Me.GroupBox1.Controls.Add(Me.BttnClose)
        Me.GroupBox1.Controls.Add(Me.BttnSave)
        Me.GroupBox1.Location = New System.Drawing.Point(552, 58)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(120, 195)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(16, 31)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(16, 143)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(16, 87)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.BttnAdd)
        Me.GroupBox3.Controls.Add(Me.DmnSTATUS)
        Me.GroupBox3.Controls.Add(Me.TxtCompany)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtSupplierID)
        Me.GroupBox3.Controls.Add(Me.TxtDesignation)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.TxtAddress)
        Me.GroupBox3.Controls.Add(Me.TxtCoyPh)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtPrsnPh)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.TxtEMail)
        Me.GroupBox3.Controls.Add(Me.TxtFax)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.TxtCell)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.TxtWebSite)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.TxtContactPerson)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 56)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(536, 197)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'BttnAdd
        '
        Me.BttnAdd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnAdd.Location = New System.Drawing.Point(212, 21)
        Me.BttnAdd.Name = "BttnAdd"
        Me.BttnAdd.Size = New System.Drawing.Size(33, 23)
        Me.BttnAdd.TabIndex = 2
        Me.BttnAdd.TabStop = False
        Me.BttnAdd.Text = "+&1"
        '
        'DmnSTATUS
        '
        Me.DmnSTATUS.BackColor = System.Drawing.Color.White
        Me.DmnSTATUS.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.STATUS", True))
        Me.DmnSTATUS.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.DmnSTATUS.Items.Add("No")
        Me.DmnSTATUS.Items.Add("Yes")
        Me.DmnSTATUS.Location = New System.Drawing.Point(112, 141)
        Me.DmnSTATUS.Name = "DmnSTATUS"
        Me.DmnSTATUS.ReadOnly = True
        Me.DmnSTATUS.Size = New System.Drawing.Size(104, 23)
        Me.DmnSTATUS.TabIndex = 14
        '
        'DsSUPPLIER_INFO1
        '
        Me.DsSUPPLIER_INFO1.DataSetName = "dsSUPPLIER_INFO"
        Me.DsSUPPLIER_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TxtCompany
        '
        Me.TxtCompany.BackColor = System.Drawing.Color.White
        Me.TxtCompany.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sSUPPLIER_NAME", True))
        Me.TxtCompany.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCompany.Location = New System.Drawing.Point(333, 21)
        Me.TxtCompany.MaxLength = 50
        Me.TxtCompany.Name = "TxtCompany"
        Me.TxtCompany.Size = New System.Drawing.Size(195, 23)
        Me.TxtCompany.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(250, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 23)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Company*"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Supplier ID"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtSupplierID
        '
        Me.TxtSupplierID.BackColor = System.Drawing.Color.White
        Me.TxtSupplierID.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSupplierID.Location = New System.Drawing.Point(112, 21)
        Me.TxtSupplierID.MaxLength = 50
        Me.TxtSupplierID.Name = "TxtSupplierID"
        Me.TxtSupplierID.ReadOnly = True
        Me.TxtSupplierID.Size = New System.Drawing.Size(96, 23)
        Me.TxtSupplierID.TabIndex = 1
        Me.TxtSupplierID.TabStop = False
        '
        'TxtDesignation
        '
        Me.TxtDesignation.BackColor = System.Drawing.Color.White
        Me.TxtDesignation.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sDESIGNATION", True))
        Me.TxtDesignation.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDesignation.Location = New System.Drawing.Point(112, 69)
        Me.TxtDesignation.Name = "TxtDesignation"
        Me.TxtDesignation.Size = New System.Drawing.Size(192, 23)
        Me.TxtDesignation.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 23)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Contact Prsn*"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtAddress
        '
        Me.TxtAddress.BackColor = System.Drawing.Color.White
        Me.TxtAddress.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sADDRESS", True))
        Me.TxtAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(112, 93)
        Me.TxtAddress.MaxLength = 200
        Me.TxtAddress.Multiline = True
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(192, 47)
        Me.TxtAddress.TabIndex = 10
        '
        'TxtCoyPh
        '
        Me.TxtCoyPh.BackColor = System.Drawing.Color.White
        Me.TxtCoyPh.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sSUPPLIER_PH", True))
        Me.TxtCoyPh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCoyPh.Location = New System.Drawing.Point(376, 45)
        Me.TxtCoyPh.Name = "TxtCoyPh"
        Me.TxtCoyPh.Size = New System.Drawing.Size(152, 23)
        Me.TxtCoyPh.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(304, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 23)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Coy Ph."
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPrsnPh
        '
        Me.TxtPrsnPh.BackColor = System.Drawing.Color.White
        Me.TxtPrsnPh.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sPERSON_PH", True))
        Me.TxtPrsnPh.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrsnPh.Location = New System.Drawing.Point(376, 69)
        Me.TxtPrsnPh.Name = "TxtPrsnPh"
        Me.TxtPrsnPh.Size = New System.Drawing.Size(152, 23)
        Me.TxtPrsnPh.TabIndex = 18
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(304, 69)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 23)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Prsn Ph."
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(304, 141)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 23)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "E-mail"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtEMail
        '
        Me.TxtEMail.BackColor = System.Drawing.Color.White
        Me.TxtEMail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sE_MAIL", True))
        Me.TxtEMail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEMail.Location = New System.Drawing.Point(376, 141)
        Me.TxtEMail.Name = "TxtEMail"
        Me.TxtEMail.Size = New System.Drawing.Size(152, 23)
        Me.TxtEMail.TabIndex = 24
        '
        'TxtFax
        '
        Me.TxtFax.BackColor = System.Drawing.Color.White
        Me.TxtFax.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sFAX_NO", True))
        Me.TxtFax.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFax.Location = New System.Drawing.Point(376, 117)
        Me.TxtFax.Name = "TxtFax"
        Me.TxtFax.Size = New System.Drawing.Size(152, 23)
        Me.TxtFax.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(304, 117)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 23)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Fax #"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(304, 93)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 23)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Cell #"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtCell
        '
        Me.TxtCell.BackColor = System.Drawing.Color.White
        Me.TxtCell.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sCELL_NO", True))
        Me.TxtCell.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCell.Location = New System.Drawing.Point(376, 93)
        Me.TxtCell.Name = "TxtCell"
        Me.TxtCell.Size = New System.Drawing.Size(152, 23)
        Me.TxtCell.TabIndex = 20
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 141)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(104, 23)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Active (Y/N)*"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(304, 165)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 23)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Web Site"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtWebSite
        '
        Me.TxtWebSite.BackColor = System.Drawing.Color.White
        Me.TxtWebSite.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sWEB_ADD", True))
        Me.TxtWebSite.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWebSite.Location = New System.Drawing.Point(376, 165)
        Me.TxtWebSite.Name = "TxtWebSite"
        Me.TxtWebSite.Size = New System.Drawing.Size(152, 23)
        Me.TxtWebSite.TabIndex = 26
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 48)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Company Address"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtContactPerson
        '
        Me.TxtContactPerson.BackColor = System.Drawing.Color.White
        Me.TxtContactPerson.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsSUPPLIER_INFO1, "SUPPLIER_INFO.sCONTACT_PERSON", True))
        Me.TxtContactPerson.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtContactPerson.Location = New System.Drawing.Point(112, 45)
        Me.TxtContactPerson.Name = "TxtContactPerson"
        Me.TxtContactPerson.Size = New System.Drawing.Size(192, 23)
        Me.TxtContactPerson.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(8, 69)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(104, 23)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "Designation"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(678, 48)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "SUPPLIER(s) DETAIL"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = resources.GetString("SqlSelectCommand1.CommandText")
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
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, "sCONTACT_PERSON"), New System.Data.SqlClient.SqlParameter("@sDESIGNATION", System.Data.SqlDbType.VarChar, 0, "sDESIGNATION"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_NAME"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.VarChar, 0, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_PH"), New System.Data.SqlClient.SqlParameter("@sPERSON_PH", System.Data.SqlDbType.VarChar, 0, "sPERSON_PH"), New System.Data.SqlClient.SqlParameter("@sCELL_NO", System.Data.SqlDbType.VarChar, 0, "sCELL_NO"), New System.Data.SqlClient.SqlParameter("@sFAX_NO", System.Data.SqlDbType.VarChar, 0, "sFAX_NO"), New System.Data.SqlClient.SqlParameter("@sE_MAIL", System.Data.SqlDbType.VarChar, 0, "sE_MAIL"), New System.Data.SqlClient.SqlParameter("@sWEB_ADD", System.Data.SqlDbType.VarChar, 0, "sWEB_ADD"), New System.Data.SqlClient.SqlParameter("@nOPEN_BAL", System.Data.SqlDbType.Money, 0, "nOPEN_BAL")})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Current, Nothing), New System.Data.SqlClient.SqlParameter("@sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, "sCONTACT_PERSON"), New System.Data.SqlClient.SqlParameter("@sDESIGNATION", System.Data.SqlDbType.VarChar, 0, "sDESIGNATION"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_NAME"), New System.Data.SqlClient.SqlParameter("@sADDRESS", System.Data.SqlDbType.VarChar, 0, "sADDRESS"), New System.Data.SqlClient.SqlParameter("@sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, "sSUPPLIER_PH"), New System.Data.SqlClient.SqlParameter("@sPERSON_PH", System.Data.SqlDbType.VarChar, 0, "sPERSON_PH"), New System.Data.SqlClient.SqlParameter("@sCELL_NO", System.Data.SqlDbType.VarChar, 0, "sCELL_NO"), New System.Data.SqlClient.SqlParameter("@sFAX_NO", System.Data.SqlDbType.VarChar, 0, "sFAX_NO"), New System.Data.SqlClient.SqlParameter("@sE_MAIL", System.Data.SqlDbType.VarChar, 0, "sE_MAIL"), New System.Data.SqlClient.SqlParameter("@sWEB_ADD", System.Data.SqlDbType.VarChar, 0, "sWEB_ADD"), New System.Data.SqlClient.SqlParameter("@nOPEN_BAL", System.Data.SqlDbType.Money, 0, "nOPEN_BAL"), New System.Data.SqlClient.SqlParameter("@Original_nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT_PERSON", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sDESIGNATION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sDESIGNATION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sSUPPLIER_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sPERSON_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sPERSON_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCELL_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCELL_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sFAX_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sFAX_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sE_MAIL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sE_MAIL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sWEB_ADD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sWEB_ADD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nOPEN_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nOPEN_BAL", System.Data.DataRowVersion.Original, Nothing)})
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = resources.GetString("SqlDeleteCommand1.CommandText")
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nID", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nID", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sCONTACT_PERSON", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCONTACT_PERSON", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sDESIGNATION", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sDESIGNATION", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESIGNATION", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_NAME", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_NAME", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sADDRESS", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sADDRESS", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sADDRESS", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sSUPPLIER_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sSUPPLIER_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sSUPPLIER_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sPERSON_PH", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sPERSON_PH", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sPERSON_PH", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sCELL_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sCELL_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sCELL_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sFAX_NO", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sFAX_NO", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sFAX_NO", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sE_MAIL", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sE_MAIL", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sE_MAIL", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@IsNull_sWEB_ADD", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, True, Nothing, "", "", ""), New System.Data.SqlClient.SqlParameter("@Original_sWEB_ADD", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sWEB_ADD", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_nOPEN_BAL", System.Data.SqlDbType.Money, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "nOPEN_BAL", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daSUPPLIER_INFO
        '
        Me.daSUPPLIER_INFO.DeleteCommand = Me.SqlDeleteCommand1
        Me.daSUPPLIER_INFO.InsertCommand = Me.SqlInsertCommand1
        Me.daSUPPLIER_INFO.SelectCommand = Me.SqlSelectCommand1
        Me.daSUPPLIER_INFO.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SUPPLIER_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nID", "nID"), New System.Data.Common.DataColumnMapping("sCONTACT_PERSON", "sCONTACT_PERSON"), New System.Data.Common.DataColumnMapping("sDESIGNATION", "sDESIGNATION"), New System.Data.Common.DataColumnMapping("sSUPPLIER_NAME", "sSUPPLIER_NAME"), New System.Data.Common.DataColumnMapping("sADDRESS", "sADDRESS"), New System.Data.Common.DataColumnMapping("sSUPPLIER_PH", "sSUPPLIER_PH"), New System.Data.Common.DataColumnMapping("sPERSON_PH", "sPERSON_PH"), New System.Data.Common.DataColumnMapping("sCELL_NO", "sCELL_NO"), New System.Data.Common.DataColumnMapping("sFAX_NO", "sFAX_NO"), New System.Data.Common.DataColumnMapping("sE_MAIL", "sE_MAIL"), New System.Data.Common.DataColumnMapping("sWEB_ADD", "sWEB_ADD"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("nOPEN_BAL", "nOPEN_BAL")})})
        Me.daSUPPLIER_INFO.UpdateCommand = Me.SqlUpdateCommand1
        '
        'DsSUPPLIER_INFO11
        '
        Me.DsSUPPLIER_INFO11.DataSetName = "dsSUPPLIER_INFO1"
        Me.DsSUPPLIER_INFO11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_SUPPLIER
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(698, 488)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_SUPPLIER"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SUPPLIER INFORMATION"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsSUPPLIER_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsSUPPLIER_INFO11, System.ComponentModel.ISupportInitialize).EndInit()
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
    Private Sub frmLUP_SUPPLIER_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()

        Me.BttnNew_Click(sender, e)
    End Sub

    Private Sub frmLUP_SUPPLIER_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub Txt_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.GotFocus, TxtCompany.GotFocus, TxtSupplierID.GotFocus, TxtAddress.GotFocus, TxtDesignation.GotFocus, TxtFax.GotFocus, TxtEMail.GotFocus, TxtCell.GotFocus, TxtCoyPh.GotFocus, TxtPrsnPh.GotFocus, TxtContactPerson.GotFocus, TxtWebSite.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub Txt_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.LostFocus, TxtCompany.LostFocus, TxtSupplierID.LostFocus, TxtAddress.LostFocus, TxtDesignation.LostFocus, TxtFax.LostFocus, TxtEMail.LostFocus, TxtCell.LostFocus, TxtCoyPh.LostFocus, TxtPrsnPh.LostFocus, TxtContactPerson.LostFocus, TxtWebSite.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub

    'KeyPress Numeric With Desh
    Private Sub Txt_Num_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFax.KeyPress, TxtCell.KeyPress, TxtCoyPh.KeyPress, TxtPrsnPh.KeyPress
        Me.asNum.NumPressDash(e)
    End Sub

    'KeyPress Numeric With DOT
    Private Sub Txt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Me.asNum.NumPressDeshDot(e)
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        If Me.TxtSearch.Text = Nothing Then
            Me.FillListView()

        Else
            Me.FillListView_Condition()
        End If
    End Sub
#End Region

#Region "DOMAIN_UPDOWN EVENTS"
    Private Sub DmnStatus_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnSTATUS.GotFocus
        CType(sender, DomainUpDown).BackColor = Color.LightSteelBlue
    End Sub
    Private Sub DmnStatus_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DmnSTATUS.LostFocus
        CType(sender, DomainUpDown).BackColor = Color.White
    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        Me.TxtSupplierID.Text = Me.ListView1.SelectedItems(0).Text
        If Not Me.TxtSupplierID.Text = Nothing Then
            Dim Str1 As String = "SELECT nID, sCONTACT_PERSON, sDESIGNATION, sSUPPLIER_NAME, sADDRESS, sSUPPLIER_PH, sPERSON_PH, sCELL_NO, sFAX_NO, sE_MAIL, sWEB_ADD, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, nOPEN_BAL FROM SUPPLIER_INFO WHERE nID=" & Val(Me.TxtSupplierID.Text) & ""
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daSUPPLIER_INFO = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsSUPPLIER_INFO1.Clear()
            Me.daSUPPLIER_INFO.Fill(Me.DsSUPPLIER_INFO1.SUPPLIER_INFO)

            Me.BttnAdd.Enabled = False
            Me.DmnSTATUS.SelectedItem = Me.DmnSTATUS.Text
        End If

FIX:
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then
                Me.asDelete.DeleteValueIN("DELETE FROM SUPPLIER_INFO WHERE nID=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

                Me.FillListView()

                Me.BttnNew_Click(sender, New System.EventArgs)
            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            Me.TxtCompany.Focus()
        End If

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnAdd.Click
        Me.TxtSupplierID.Text = Val(Me.TxtSupplierID.Text) + 1
    End Sub
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.DsSUPPLIER_INFO1.Clear()

        Me.TxtSearch.Text = Nothing
        Me.TxtSupplierID.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nID) FROM SUPPLIER_INFO") + 1
        Me.TxtCompany.Focus()
        Me.BttnAdd.Enabled = True
        Me.DmnSTATUS.SelectedIndex = 1

    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        Me.asSELECT.SavedpFlg1(Rd, "SELECT * FROM SUPPLIER_INFO WHERE nID=" & Val(Me.TxtSupplierID.Text) & "")

        If Val(Me.TxtSupplierID.Text) <= 0 Then
            MsgBox("Supplier ID can't be 'NULL', Click on 'NEW' for New ID", MsgBoxStyle.Exclamation, "(NS) - Wrong ID")
            Me.BttnNew.Focus()

        ElseIf Me.TxtCompany.Text = Nothing Or Me.TxtContactPerson.Text = Nothing Or Me.DmnSTATUS.SelectedIndex = -1 Then
            MsgBox("Please enter description OR select correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtCompany.Text = Nothing Then
                Me.TxtCompany.Focus()

            ElseIf Me.TxtContactPerson.Text = Nothing Then
                Me.TxtContactPerson.Focus()

            ElseIf Me.DmnSTATUS.SelectedIndex = -1 Then
                Me.DmnSTATUS.Focus()

            End If

        ElseIf Me.asSELECT.pFlg1 = False Then
            If MsgBox("Do you want to save '" & Me.TxtCompany.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO SUPPLIER_INFO(nID, sCONTACT_PERSON, sDESIGNATION, sSUPPLIER_NAME, sADDRESS, sSUPPLIER_PH, sPERSON_PH, sCELL_NO, sFAX_NO, sE_MAIL, sWEB_ADD, sSTATUS) VALUES(" & Val(Me.TxtSupplierID.Text) & ",'" & Me.TxtContactPerson.Text & "','" & Me.TxtDesignation.Text & "','" & Me.TxtCompany.Text & "','" & Me.TxtAddress.Text & "','" & Me.TxtCoyPh.Text & "','" & Me.TxtPrsnPh.Text & "','" & Me.TxtCell.Text & "','" & Me.TxtFax.Text & "','" & Me.TxtEMail.Text & "','" & Me.TxtWebSite.Text & "','" & Me.DmnSTATUS.SelectedIndex & "') ")

                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtCompany.Focus()
            End If

        ElseIf Me.asSELECT.pFlg1 = True Then
            If MsgBox("This Supplier ID '" & Me.TxtCompany.Text & "' is Already Save. " & vbCrLf & " Do you want to update?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE SUPPLIER_INFO SET sCONTACT_PERSON='" & Me.TxtContactPerson.Text & "', sDESIGNATION='" & Me.TxtDesignation.Text & "', sSUPPLIER_NAME='" & Me.TxtCompany.Text & "', sADDRESS='" & Me.TxtAddress.Text & "', sSUPPLIER_PH='" & Me.TxtCoyPh.Text & "', sPERSON_PH='" & Me.TxtPrsnPh.Text & "', sCELL_NO='" & Me.TxtCell.Text & "', sFAX_NO='" & Me.TxtFax.Text & "', sE_MAIL='" & Me.TxtEMail.Text & "', sWEB_ADD='" & Me.TxtWebSite.Text & "', sSTATUS='" & Me.DmnSTATUS.SelectedIndex & "' WHERE nID=" & Val(Me.TxtSupplierID.Text) & "")
                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtCompany.Focus()
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        frmLUP_CLIENT_OPEN_BAL.MdiParent = Me.ParentForm
        frmLUP_CLIENT_OPEN_BAL.Show()
        frmLUP_CLIENT_OPEN_BAL.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_SUPPLIER_OPEN_BAL.MdiParent = Me.ParentForm
        frmLUP_SUPPLIER_OPEN_BAL.Show()
        frmLUP_SUPPLIER_OPEN_BAL.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillListView()

        Try
            Dim Str1 As String = "SELECT nID, sCONTACT_PERSON, sDESIGNATION, sSUPPLIER_NAME, sADDRESS, sSUPPLIER_PH, sPERSON_PH, sCELL_NO, sFAX_NO, sE_MAIL, sWEB_ADD, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CONVERT(NUMERIC(18,2),nOPEN_BAL) AS nOPEN_BAL FROM SUPPLIER_INFO ORDER BY sSUPPLIER_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daSUPPLIER_INFO = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsSUPPLIER_INFO11.Clear()
            Me.daSUPPLIER_INFO.Fill(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(3).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(5).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()

        Try
            Dim Str1 As String = "SELECT nID, sCONTACT_PERSON, sDESIGNATION, sSUPPLIER_NAME, sADDRESS, sSUPPLIER_PH, sPERSON_PH, sCELL_NO, sFAX_NO, sE_MAIL, sWEB_ADD, CASE sSTATUS WHEN '0' THEN 'No' WHEN '1' THEN 'Yes' END AS STATUS, CONVERT(NUMERIC(18,2), nOPEN_BAL) AS nOPEN_BAL FROM SUPPLIER_INFO WHERE sSUPPLIER_NAME LIKE '%" & Me.TxtSearch.Text & "%'ORDER BY sSUPPLIER_NAME"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daSUPPLIER_INFO = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsSUPPLIER_INFO11.Clear()
            Me.daSUPPLIER_INFO.Fill(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(3).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(5).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsSUPPLIER_INFO11.SUPPLIER_INFO.Item(Cnt).Item(6).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

End Class
