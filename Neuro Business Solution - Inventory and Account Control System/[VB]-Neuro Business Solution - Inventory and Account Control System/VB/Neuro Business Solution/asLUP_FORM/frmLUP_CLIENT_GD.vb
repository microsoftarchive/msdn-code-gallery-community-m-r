Imports SDS = System.Data.SqlClient
Public Class frmLUP_CLIENT_GD
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
    Friend WithEvents TxtDESC As System.Windows.Forms.TextBox
    Friend WithEvents TxtSearch As System.Windows.Forms.TextBox
    Friend WithEvents TxtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlInsertCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlUpdateCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlDeleteCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents daLUP_CLIENT_GD As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLUP_CLIENT_GD1 As Neruo_Business_Solution.dsLUP_CLIENT_GD
    Friend WithEvents TxtMin As System.Windows.Forms.TextBox
    Friend WithEvents BttnPrevForm As System.Windows.Forms.Button
    Friend WithEvents BttnNextForm As System.Windows.Forms.Button
    Friend WithEvents TxtMax As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLUP_CLIENT_GD))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnPrevForm = New System.Windows.Forms.Button
        Me.BttnNextForm = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSearch = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BttnNew = New System.Windows.Forms.Button
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnSave = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.TxtDESC = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtCode = New System.Windows.Forms.TextBox
        Me.TxtMin = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtMax = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.SqlInsertCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlUpdateCommand1 = New System.Data.SqlClient.SqlCommand
        Me.SqlDeleteCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daLUP_CLIENT_GD = New System.Data.SqlClient.SqlDataAdapter
        Me.DsLUP_CLIENT_GD1 = New Neruo_Business_Solution.dsLUP_CLIENT_GD
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DsLUP_CLIENT_GD1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnPrevForm)
        Me.Panel1.Controls.Add(Me.BttnNextForm)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Location = New System.Drawing.Point(8, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(568, 416)
        Me.Panel1.TabIndex = 0
        '
        'BttnPrevForm
        '
        Me.BttnPrevForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnPrevForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnPrevForm.Location = New System.Drawing.Point(8, 3)
        Me.BttnPrevForm.Name = "BttnPrevForm"
        Me.BttnPrevForm.Size = New System.Drawing.Size(89, 45)
        Me.BttnPrevForm.TabIndex = 16
        Me.BttnPrevForm.TabStop = False
        Me.BttnPrevForm.Text = "Client Type"
        Me.BttnPrevForm.UseVisualStyleBackColor = False
        '
        'BttnNextForm
        '
        Me.BttnNextForm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnNextForm.BackColor = System.Drawing.Color.CornflowerBlue
        Me.BttnNextForm.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNextForm.Location = New System.Drawing.Point(468, 3)
        Me.BttnNextForm.Name = "BttnNextForm"
        Me.BttnNextForm.Size = New System.Drawing.Size(89, 45)
        Me.BttnNextForm.TabIndex = 15
        Me.BttnNextForm.TabStop = False
        Me.BttnNextForm.Text = "Client Category"
        Me.BttnNextForm.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(566, 48)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "CLIENT GRADE SETTING"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(256, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(304, 352)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Saved"
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 53)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(298, 296)
        Me.ListView1.TabIndex = 2
        Me.ListView1.TabStop = False
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "CODE"
        Me.ColumnHeader1.Width = 55
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Grade"
        Me.ColumnHeader2.Width = 65
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Min."
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader3.Width = 84
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Max."
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader4.Width = 84
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
        Me.GroupBox1.Location = New System.Drawing.Point(8, 293)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 112)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'BttnNew
        '
        Me.BttnNew.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnNew.Location = New System.Drawing.Point(12, 24)
        Me.BttnNew.Name = "BttnNew"
        Me.BttnNew.Size = New System.Drawing.Size(89, 31)
        Me.BttnNew.TabIndex = 1
        Me.BttnNew.Text = "&New"
        '
        'BttnClose
        '
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(76, 72)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(89, 31)
        Me.BttnClose.TabIndex = 2
        Me.BttnClose.Text = "&Close"
        '
        'BttnSave
        '
        Me.BttnSave.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnSave.Location = New System.Drawing.Point(140, 24)
        Me.BttnSave.Name = "BttnSave"
        Me.BttnSave.Size = New System.Drawing.Size(89, 31)
        Me.BttnSave.TabIndex = 0
        Me.BttnSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox3.Controls.Add(Me.TxtDESC)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.TxtCode)
        Me.GroupBox3.Controls.Add(Me.TxtMin)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.TxtMax)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 56)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(240, 232)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Entry"
        '
        'TxtDESC
        '
        Me.TxtDESC.BackColor = System.Drawing.Color.White
        Me.TxtDESC.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDESC.Location = New System.Drawing.Point(16, 112)
        Me.TxtDESC.MaxLength = 50
        Me.TxtDESC.Name = "TxtDESC"
        Me.TxtDESC.Size = New System.Drawing.Size(208, 23)
        Me.TxtDESC.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(208, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "&Grade (A/B/C) etc."
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(208, 23)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Code (Auto Generated)"
        '
        'TxtCode
        '
        Me.TxtCode.BackColor = System.Drawing.Color.White
        Me.TxtCode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.Location = New System.Drawing.Point(16, 56)
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.ReadOnly = True
        Me.TxtCode.Size = New System.Drawing.Size(208, 23)
        Me.TxtCode.TabIndex = 1
        Me.TxtCode.TabStop = False
        '
        'TxtMin
        '
        Me.TxtMin.BackColor = System.Drawing.Color.White
        Me.TxtMin.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMin.Location = New System.Drawing.Point(16, 200)
        Me.TxtMin.MaxLength = 50
        Me.TxtMin.Name = "TxtMin"
        Me.TxtMin.Size = New System.Drawing.Size(96, 23)
        Me.TxtMin.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 176)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 23)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "M&in. Value"
        '
        'TxtMax
        '
        Me.TxtMax.BackColor = System.Drawing.Color.White
        Me.TxtMax.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMax.Location = New System.Drawing.Point(120, 200)
        Me.TxtMax.MaxLength = 50
        Me.TxtMax.Name = "TxtMax"
        Me.TxtMax.Size = New System.Drawing.Size(104, 23)
        Me.TxtMax.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(120, 176)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 23)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Ma&x. Value"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 144)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(208, 23)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Purchase Values"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     nCODE, sDESC, CONVERT(NUMERIC(18, 2), nMIN_LIM) AS nMIN_LIM, CONVERT(N" & _
            "UMERIC(18, 2), nMAX_LIM) AS nMAX_LIM" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM         LUP_CLIENT_GD"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'SqlInsertCommand1
        '
        Me.SqlInsertCommand1.CommandText = resources.GetString("SqlInsertCommand1.CommandText")
        Me.SqlInsertCommand1.Connection = Me.SqlConnection1
        Me.SqlInsertCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC")})
        '
        'SqlUpdateCommand1
        '
        Me.SqlUpdateCommand1.CommandText = resources.GetString("SqlUpdateCommand1.CommandText")
        Me.SqlUpdateCommand1.Connection = Me.SqlConnection1
        Me.SqlUpdateCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@sDESC", System.Data.SqlDbType.VarChar, 0, "sDESC"), New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@nCODE", System.Data.SqlDbType.[Decimal], 9, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Current, Nothing)})
        '
        'SqlDeleteCommand1
        '
        Me.SqlDeleteCommand1.CommandText = "DELETE FROM [LUP_CLIENT_GD] WHERE (([nCODE] = @Original_nCODE) AND ([sDESC] = @Or" & _
            "iginal_sDESC))"
        Me.SqlDeleteCommand1.Connection = Me.SqlConnection1
        Me.SqlDeleteCommand1.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Original_nCODE", System.Data.SqlDbType.[Decimal], 0, System.Data.ParameterDirection.Input, False, CType(18, Byte), CType(0, Byte), "nCODE", System.Data.DataRowVersion.Original, Nothing), New System.Data.SqlClient.SqlParameter("@Original_sDESC", System.Data.SqlDbType.VarChar, 0, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "sDESC", System.Data.DataRowVersion.Original, Nothing)})
        '
        'daLUP_CLIENT_GD
        '
        Me.daLUP_CLIENT_GD.DeleteCommand = Me.SqlDeleteCommand1
        Me.daLUP_CLIENT_GD.InsertCommand = Me.SqlInsertCommand1
        Me.daLUP_CLIENT_GD.SelectCommand = Me.SqlSelectCommand1
        Me.daLUP_CLIENT_GD.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "LUP_CLIENT_GD", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("nCODE", "nCODE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC"), New System.Data.Common.DataColumnMapping("nMIN_LIM", "nMIN_LIM"), New System.Data.Common.DataColumnMapping("nMAX_LIM", "nMAX_LIM")})})
        Me.daLUP_CLIENT_GD.UpdateCommand = Me.SqlUpdateCommand1
        '
        'DsLUP_CLIENT_GD1
        '
        Me.DsLUP_CLIENT_GD1.DataSetName = "dsLUP_CLIENT_GD"
        Me.DsLUP_CLIENT_GD1.Locale = New System.Globalization.CultureInfo("en-US")
        Me.DsLUP_CLIENT_GD1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmLUP_CLIENT_GD
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(586, 432)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Name = "frmLUP_CLIENT_GD"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GRADE SETTING"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DsLUP_CLIENT_GD1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


#Region "VARIABLES"
    Dim asConn As New AssConn
    Dim asInsert As New AssInsert
    Dim asUpdate As New AssUpdate
    Dim asDelete As New AssDelete
    Dim asNum As New AssNumPress
    Dim asMAX As New AssMaxNo
    Dim Rd As System.Data.SqlClient.SqlDataReader

#End Region

#Region "FORM CONTROL"
    Private Sub frmLUP_CLIENT_GD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SqlConnection1.ConnectionString = Me.asConn.Conn.ConnectionString
        Me.FillListView()
    End Sub

    Private Sub frmLUP_CLIENT_GD_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        asNum.EnterTab(e)
    End Sub
#End Region

#Region "TextBox Control"
    Private Sub TxtCode_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.GotFocus, TxtDESC.GotFocus, TxtSearch.GotFocus, TxtMax.GotFocus, TxtMin.GotFocus
        CType(sender, TextBox).BackColor = Color.LightSteelBlue
        CType(sender, TextBox).SelectAll()
    End Sub
    Private Sub TxtCode_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCode.LostFocus, TxtDESC.LostFocus, TxtSearch.LostFocus, TxtMax.LostFocus, TxtMin.LostFocus
        CType(sender, TextBox).BackColor = Color.White
    End Sub
    Private Sub TxtMin_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtMin.KeyPress, TxtMax.KeyPress
        Me.asNum.NumPressDot(e)
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtSearch.TextChanged
        If Me.TxtSearch.Text = Nothing Then
            Me.FillListView()

        Else
            Me.FillListView_Condition()
        End If
    End Sub
#End Region

#Region "ListView Control"
    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        On Error GoTo FIX
        Me.TxtCode.Text = Me.ListView1.SelectedItems(0).Text
        Me.TxtDESC.Text = Me.ListView1.SelectedItems(0).SubItems(1).Text
        Me.TxtMin.Text = Me.ListView1.SelectedItems(0).SubItems(2).Text
        Me.TxtMax.Text = Me.ListView1.SelectedItems(0).SubItems(3).Text
        'Me.TxtDESC.Focus()

FIX:

    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        If Not Me.ListView1.SelectedItems(0).Text = Nothing Then
            If MsgBox("Do you want to DELETE '" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "' From Record?", MsgBoxStyle.Critical + vbYesNo, "(NS) - Confirm Delete!") = MsgBoxResult.Yes Then

                Me.asDelete.DeleteValue("DELETE FROM LUP_CLIENT_GDs WHERE nCLIENT_GD_CODE=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

                Me.asDelete.DeleteValueIN("DELETE FROM LUP_CLIENT_GD WHERE nCODE=" & Val(Me.ListView1.SelectedItems(0).Text) & "")

                Me.FillListView()

                Me.BttnNew_Click(sender, New System.EventArgs)
            End If

        Else
            MsgBox("Please Select record for DELETE", MsgBoxStyle.Exclamation, "(NS) - Error!")
            Me.TxtDESC.Focus()
        End If

    End Sub
#End Region

#Region "Button Control"
    Private Sub BttnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNew.Click
        Me.TxtCode.Text = Nothing
        Me.TxtDESC.Text = Nothing
        Me.TxtMin.Text = Nothing
        Me.TxtMax.Text = Nothing
        Me.TxtSearch.Text = Nothing
        'Me.TxtCode.Text = Me.asMAX.LoadValue(Rd, "SELECT MAX(nCODE) FROM LUP_ZONE") + 1
        Me.TxtDESC.Focus()
    End Sub
    Private Sub BttnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnSave.Click

        If Me.TxtDESC.Text = Nothing Or Val(Me.TxtMin.Text) <= 0 Or Val(Me.TxtMax.Text) <= 0 Or Val(Me.TxtMin.Text) > Val(Me.TxtMax.Text) Then
            MsgBox("Please enter description OR correct value!", MsgBoxStyle.Exclamation, "(NS) - Entry Required!")
            If Me.TxtDESC.Text = Nothing Then
                Me.TxtDESC.Focus()

            ElseIf Val(Me.TxtMin.Text) <= 0 Or Val(Me.TxtMin.Text) > Val(Me.TxtMax.Text) Then
                Me.TxtMin.Focus()

            ElseIf Val(Me.TxtMax.Text) <= 0 Then
                Me.TxtMax.Focus()

            End If

        ElseIf Me.TxtCode.Text = Nothing Then
            If MsgBox("Do you want to save '" & Me.TxtDESC.Text & "'" & vbCrLf & "Min. Value '" & Val(Me.TxtMin.Text) & "' and Max. Value '" & Val(Me.TxtMax.Text) & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Save?") = MsgBoxResult.Yes Then
                'INSERT VALUES
                Me.asInsert.SaveValueIN("INSERT INTO LUP_CLIENT_GD(sDESC,nMIN_LIM,nMAX_LIM) VALUES('" & Me.TxtDESC.Text & "'," & Val(Me.TxtMin.Text) & "," & Val(Me.TxtMax.Text) & ") ")

                'LOCATE THE CLIENT_GD_CODE..... Which is recently save
                Dim GD_CODE As Double = Me.asMAX.LoadValue(Rd, "SELECT nCODE FROM LUP_CLIENT_GD WHERE sDESC='" & Me.TxtDESC.Text & "'")

                ''''Delete Previous Saved Record according to located CLIENT_GD_CODE
                ''Me.asDelete.DeleteValue("DELETE FROM LUP_CLIENT_GDs WHERE nCLIENT_GD_CODE=" & GD_CODE & "")

                ''''INSERT VALUES IN LUP_CLIENT_GDs....  Using Loop
                ''Dim i As Double
                ''For i = Val(Me.TxtMin.Text) To Val(Me.TxtMax.Text)
                ''    If Not i = Val(Me.TxtMax.Text) Then
                ''        Me.asInsert.SaveValue("INSERT INTO LUP_CLIENT_GDs(nCLIENT_GD_CODE,nVALUE) VALUES(" & GD_CODE & "," & i & ")")

                ''    ElseIf i = Val(Me.TxtMax.Text) Then
                ''        Me.asInsert.SaveValueIN("INSERT INTO LUP_CLIENT_GDs(nCLIENT_GD_CODE,nVALUE) VALUES(" & GD_CODE & "," & i & ")")

                ''    End If

                ''Next

                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtDESC.Focus()
            End If

        ElseIf Not Me.TxtCode.Text = Nothing Then
            If MsgBox("Do you want to update '" & Me.TxtDESC.Text & "'", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Update?") = MsgBoxResult.Yes Then
                'UPDATE RECORD
                Me.asUpdate.UpdateValueIN("UPDATE LUP_CLIENT_GD SET sDESC='" & Me.TxtDESC.Text & "',nMIN_LIM=" & Val(Me.TxtMin.Text) & ", nMAX_LIM=" & Val(Me.TxtMax.Text) & " WHERE nCODE=" & Val(Me.TxtCode.Text) & "")

                ''''Delete Previous Saved Record according to selected CLIENT_GD_CODE
                ''Me.asDelete.DeleteValue("DELETE FROM LUP_CLIENT_GDs WHERE nCLIENT_GD_CODE=" & Val(Me.TxtCode.Text) & "")

                ''''INSERT VALUES IN LUP_CLIENT_GDs....  Using Loop
                ''Dim i As Double
                ''For i = Val(Me.TxtMin.Text) To Val(Me.TxtMax.Text)
                ''    If Not i = Val(Me.TxtMax.Text) Then
                ''        Me.asInsert.SaveValue("INSERT INTO LUP_CLIENT_GDs(nCLIENT_GD_CODE,nVALUE) VALUES(" & Val(Me.TxtCode.Text) & "," & i & ")")

                ''    ElseIf i = Val(Me.TxtMax.Text) Then
                ''        Me.asUpdate.UpdateValueIN("INSERT INTO LUP_CLIENT_GDs(nCLIENT_GD_CODE,nVALUE) VALUES(" & Val(Me.TxtCode.Text) & "," & i & ")")

                ''    End If

                ''Next

                'FILL THE RECORD IN LISTVIEW
                Me.FillListView()
                Me.TxtDESC.Focus()
            End If

        End If
    End Sub
    Private Sub BttnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Form Navigation Button Control"
    Private Sub BttnPrevForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnPrevForm.Click
        frmLUP_CLIENT_TYPE.MdiParent = Me.ParentForm
        frmLUP_CLIENT_TYPE.Show()
        frmLUP_CLIENT_TYPE.Activate()
        Me.Close()
    End Sub
    Private Sub BttnNextForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnNextForm.Click
        frmLUP_SHOP_CAT.MdiParent = Me.ParentForm
        frmLUP_SHOP_CAT.Show()
        frmLUP_SHOP_CAT.Activate()
        Me.Close()
    End Sub
#End Region

#Region "Sub and Functions"
    Private Sub FillListView()

        Try
            Dim Str1 As String = "SELECT nCODE, sDESC, CONVERT(NUMERIC(18, 2), nMIN_LIM) AS nMIN_LIM, CONVERT(NUMERIC(18, 2), nMAX_LIM) AS nMAX_LIM FROM LUP_CLIENT_GD ORDER BY sDESC"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_CLIENT_GD = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_CLIENT_GD1.Clear()
            Me.daLUP_CLIENT_GD.Fill(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(2).ToString, Color.DarkCyan, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(3).ToString, Color.DarkGoldenrod, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillListView_Condition()

        Try
            Dim Str1 As String = "SELECT nCODE, sDESC, CONVERT(NUMERIC(18, 2), nMIN_LIM) AS nMIN_LIM, CONVERT(NUMERIC(18, 2), nMAX_LIM) AS nMAX_LIM FROM LUP_CLIENT_GD WHERE sDESC LIKE '%" & Me.TxtSearch.Text & "%' ORDER BY sDESC"
            Dim SqlCmd1 As New SDS.SqlCommand(Str1, Me.SqlConnection1)
            Me.daLUP_CLIENT_GD = New SDS.SqlDataAdapter(SqlCmd1)

            Me.DsLUP_CLIENT_GD1.Clear()
            Me.daLUP_CLIENT_GD.Fill(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD)

            Me.ListView1.Items.Clear()

            Dim Cnt As Integer
            Dim LstItem As ListViewItem

            For Cnt = 0 To Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Count - 1
                LstItem = Me.ListView1.Items.Add(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(0).ToString)
                Me.ListView1.Items(Cnt).UseItemStyleForSubItems = False
                With LstItem.SubItems

                    .Add(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(1).ToString, Color.DarkBlue, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(2).ToString, Color.DarkCyan, Me.ListView1.BackColor, Me.ListView1.Font)
                    .Add(Me.DsLUP_CLIENT_GD1.LUP_CLIENT_GD.Item(Cnt).Item(3).ToString, Color.DarkGoldenrod, Me.ListView1.BackColor, Me.ListView1.Font)

                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


End Class
