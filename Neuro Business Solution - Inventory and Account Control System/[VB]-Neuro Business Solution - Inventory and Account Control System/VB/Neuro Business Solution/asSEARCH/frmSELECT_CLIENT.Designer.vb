<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSELECT_CLIENT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSELECT_CLIENT))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.BttnClose = New System.Windows.Forms.Button
        Me.BttnRefresh = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtClient = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtArea = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.daCLIENT_INFO = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlSelectCommand4 = New System.Data.SqlClient.SqlCommand
        Me.DsCLIENT_INFO1 = New Neruo_Business_Solution.dsCLIENT_INFO
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DsCLIENT_INFO1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.BttnClose)
        Me.Panel1.Controls.Add(Me.BttnRefresh)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(705, 282)
        Me.Panel1.TabIndex = 0
        '
        'BttnClose
        '
        Me.BttnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BttnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BttnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnClose.Location = New System.Drawing.Point(622, 4)
        Me.BttnClose.Name = "BttnClose"
        Me.BttnClose.Size = New System.Drawing.Size(75, 26)
        Me.BttnClose.TabIndex = 3
        Me.BttnClose.TabStop = False
        Me.BttnClose.Text = "&Close"
        Me.BttnClose.UseVisualStyleBackColor = True
        '
        'BttnRefresh
        '
        Me.BttnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BttnRefresh.Location = New System.Drawing.Point(3, 4)
        Me.BttnRefresh.Name = "BttnRefresh"
        Me.BttnRefresh.Size = New System.Drawing.Size(75, 26)
        Me.BttnRefresh.TabIndex = 2
        Me.BttnRefresh.TabStop = False
        Me.BttnRefresh.Text = "Refres&h"
        Me.BttnRefresh.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.ListView1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TxtClient)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtArea)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 29)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(695, 249)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'ListView1
        '
        Me.ListView1.AllowColumnReorder = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ListView1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(3, 38)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(689, 208)
        Me.ListView1.TabIndex = 4
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Client Name"
        Me.ColumnHeader1.Width = 324
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Area"
        Me.ColumnHeader2.Width = 280
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Client ID"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ColumnHeader3.Width = 80
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 21)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Clien&t"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtClient
        '
        Me.TxtClient.BackColor = System.Drawing.Color.White
        Me.TxtClient.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtClient.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtClient.Location = New System.Drawing.Point(81, 14)
        Me.TxtClient.MaxLength = 50
        Me.TxtClient.Name = "TxtClient"
        Me.TxtClient.Size = New System.Drawing.Size(229, 21)
        Me.TxtClient.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(316, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 21)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "&Area"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtArea
        '
        Me.TxtArea.BackColor = System.Drawing.Color.White
        Me.TxtArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtArea.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtArea.Location = New System.Drawing.Point(370, 14)
        Me.TxtArea.MaxLength = 50
        Me.TxtArea.Name = "TxtArea"
        Me.TxtArea.Size = New System.Drawing.Size(229, 21)
        Me.TxtArea.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(703, 35)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "SELECT CLIENT"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "workstation id=SERVER;packet size=8192;integrated security=SSPI;data source=SERVE" & _
            "R;persist security info=False;initial catalog=Neuro_BS"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'daCLIENT_INFO
        '
        Me.daCLIENT_INFO.SelectCommand = Me.SqlSelectCommand4
        Me.daCLIENT_INFO.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "V_CLIENT_INFO", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("ID", "ID"), New System.Data.Common.DataColumnMapping("NAME", "NAME"), New System.Data.Common.DataColumnMapping("SHOP_NAME", "SHOP_NAME"), New System.Data.Common.DataColumnMapping("SHOP_ADD", "SHOP_ADD"), New System.Data.Common.DataColumnMapping("AREA", "AREA"), New System.Data.Common.DataColumnMapping("HOME_ADD", "HOME_ADD"), New System.Data.Common.DataColumnMapping("SHOP_PH", "SHOP_PH"), New System.Data.Common.DataColumnMapping("HOME_PH", "HOME_PH"), New System.Data.Common.DataColumnMapping("CELL_NO", "CELL_NO"), New System.Data.Common.DataColumnMapping("FAX_NO", "FAX_NO"), New System.Data.Common.DataColumnMapping("E_MAIL", "E_MAIL"), New System.Data.Common.DataColumnMapping("WEB_SITE", "WEB_SITE"), New System.Data.Common.DataColumnMapping("STATUS", "STATUS"), New System.Data.Common.DataColumnMapping("CLIENT_CAT", "CLIENT_CAT"), New System.Data.Common.DataColumnMapping("CLIENT_GD", "CLIENT_GD"), New System.Data.Common.DataColumnMapping("CLIENT_TYPE", "CLIENT_TYPE"), New System.Data.Common.DataColumnMapping("CREDIT_LIM", "CREDIT_LIM"), New System.Data.Common.DataColumnMapping("GST_NO", "GST_NO"), New System.Data.Common.DataColumnMapping("OPEN_BAL", "OPEN_BAL"), New System.Data.Common.DataColumnMapping("VISIT_TYPE", "VISIT_TYPE"), New System.Data.Common.DataColumnMapping("NO_VISIT", "NO_VISIT"), New System.Data.Common.DataColumnMapping("ROUTE", "ROUTE")})})
        '
        'SqlSelectCommand4
        '
        Me.SqlSelectCommand4.CommandText = resources.GetString("SqlSelectCommand4.CommandText")
        Me.SqlSelectCommand4.Connection = Me.SqlConnection1
        '
        'DsCLIENT_INFO1
        '
        Me.DsCLIENT_INFO1.DataSetName = "dsCLIENT_INFO"
        Me.DsCLIENT_INFO1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmSELECT_CLIENT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BttnClose
        Me.ClientSize = New System.Drawing.Size(705, 282)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSELECT_CLIENT"
        Me.Opacity = 0.9
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DsCLIENT_INFO1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BttnClose As System.Windows.Forms.Button
    Friend WithEvents BttnRefresh As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtClient As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtArea As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents daCLIENT_INFO As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents SqlSelectCommand4 As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsCLIENT_INFO1 As Neruo_Business_Solution.dsCLIENT_INFO
End Class
