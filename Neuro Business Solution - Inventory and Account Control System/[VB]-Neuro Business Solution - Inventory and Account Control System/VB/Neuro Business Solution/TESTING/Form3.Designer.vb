<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
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
        Me.SqlSelectCommand1 = New System.Data.SqlClient.SqlCommand
        Me.daTEMP_LEDGER = New System.Data.SqlClient.SqlDataAdapter
        Me.SqlConnection1 = New System.Data.SqlClient.SqlConnection
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtGP_ID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCL_ID = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDATE1 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtDATE2 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.DsTEMP_LEDGER1 = New Neruo_Business_Solution.dsTEMP_LEDGER
        Me.GROUPIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TRTYPEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SDESCDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CLIENTIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DDATEDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TRIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DrDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CrDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.BALDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.TEMP_LEDGER1 = New Neruo_Business_Solution.TEMP_LEDGER
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsTEMP_LEDGER1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SqlSelectCommand1
        '
        Me.SqlSelectCommand1.CommandText = "SELECT     GROUP_ID, TR_TYPE, sDESC, CLIENT_ID, dDATE, TR_ID, Dr, Cr" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FROM       " & _
            "  SV_CLIENT_LEDGER"
        Me.SqlSelectCommand1.Connection = Me.SqlConnection1
        '
        'daTEMP_LEDGER
        '
        Me.daTEMP_LEDGER.SelectCommand = Me.SqlSelectCommand1
        Me.daTEMP_LEDGER.TableMappings.AddRange(New System.Data.Common.DataTableMapping() {New System.Data.Common.DataTableMapping("Table", "SV_CLIENT_LEDGER", New System.Data.Common.DataColumnMapping() {New System.Data.Common.DataColumnMapping("GROUP_ID", "GROUP_ID"), New System.Data.Common.DataColumnMapping("TR_TYPE", "TR_TYPE"), New System.Data.Common.DataColumnMapping("sDESC", "sDESC"), New System.Data.Common.DataColumnMapping("CLIENT_ID", "CLIENT_ID"), New System.Data.Common.DataColumnMapping("dDATE", "dDATE"), New System.Data.Common.DataColumnMapping("TR_ID", "TR_ID"), New System.Data.Common.DataColumnMapping("Dr", "Dr"), New System.Data.Common.DataColumnMapping("Cr", "Cr")})})
        '
        'SqlConnection1
        '
        Me.SqlConnection1.ConnectionString = "Data Source=server;Initial Catalog=Neuro_BS;Integrated Security=True"
        Me.SqlConnection1.FireInfoMessageEventOnUserErrors = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 325)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "FILL"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtGP_ID
        '
        Me.txtGP_ID.Location = New System.Drawing.Point(80, 370)
        Me.txtGP_ID.Name = "txtGP_ID"
        Me.txtGP_ID.Size = New System.Drawing.Size(100, 20)
        Me.txtGP_ID.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 370)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "GP ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCL_ID
        '
        Me.txtCL_ID.Location = New System.Drawing.Point(264, 370)
        Me.txtCL_ID.Name = "txtCL_ID"
        Me.txtCL_ID.Size = New System.Drawing.Size(100, 20)
        Me.txtCL_ID.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(186, 370)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "CL ID"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDATE1
        '
        Me.txtDATE1.Location = New System.Drawing.Point(453, 369)
        Me.txtDATE1.Name = "txtDATE1"
        Me.txtDATE1.Size = New System.Drawing.Size(100, 20)
        Me.txtDATE1.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(370, 369)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "DATE 1"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDATE2
        '
        Me.txtDATE2.Location = New System.Drawing.Point(453, 395)
        Me.txtDATE2.Name = "txtDATE2"
        Me.txtDATE2.Size = New System.Drawing.Size(100, 20)
        Me.txtDATE2.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(370, 395)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 20)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "DATE 2"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GROUPIDDataGridViewTextBoxColumn, Me.TRTYPEDataGridViewTextBoxColumn, Me.SDESCDataGridViewTextBoxColumn, Me.CLIENTIDDataGridViewTextBoxColumn, Me.DDATEDataGridViewTextBoxColumn, Me.TRIDDataGridViewTextBoxColumn, Me.DrDataGridViewTextBoxColumn, Me.CrDataGridViewTextBoxColumn, Me.BALDataGridViewTextBoxColumn})
        Me.DataGridView1.DataMember = "SV_CLIENT_LEDGER"
        Me.DataGridView1.DataSource = Me.DsTEMP_LEDGER1
        Me.DataGridView1.Location = New System.Drawing.Point(542, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(390, 307)
        Me.DataGridView1.TabIndex = 0
        '
        'DsTEMP_LEDGER1
        '
        Me.DsTEMP_LEDGER1.DataSetName = "dsTEMP_LEDGER"
        Me.DsTEMP_LEDGER1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'GROUPIDDataGridViewTextBoxColumn
        '
        Me.GROUPIDDataGridViewTextBoxColumn.DataPropertyName = "GROUP_ID"
        Me.GROUPIDDataGridViewTextBoxColumn.HeaderText = "GROUP_ID"
        Me.GROUPIDDataGridViewTextBoxColumn.Name = "GROUPIDDataGridViewTextBoxColumn"
        '
        'TRTYPEDataGridViewTextBoxColumn
        '
        Me.TRTYPEDataGridViewTextBoxColumn.DataPropertyName = "TR_TYPE"
        Me.TRTYPEDataGridViewTextBoxColumn.HeaderText = "TR_TYPE"
        Me.TRTYPEDataGridViewTextBoxColumn.Name = "TRTYPEDataGridViewTextBoxColumn"
        '
        'SDESCDataGridViewTextBoxColumn
        '
        Me.SDESCDataGridViewTextBoxColumn.DataPropertyName = "sDESC"
        Me.SDESCDataGridViewTextBoxColumn.HeaderText = "sDESC"
        Me.SDESCDataGridViewTextBoxColumn.Name = "SDESCDataGridViewTextBoxColumn"
        '
        'CLIENTIDDataGridViewTextBoxColumn
        '
        Me.CLIENTIDDataGridViewTextBoxColumn.DataPropertyName = "CLIENT_ID"
        Me.CLIENTIDDataGridViewTextBoxColumn.HeaderText = "CLIENT_ID"
        Me.CLIENTIDDataGridViewTextBoxColumn.Name = "CLIENTIDDataGridViewTextBoxColumn"
        '
        'DDATEDataGridViewTextBoxColumn
        '
        Me.DDATEDataGridViewTextBoxColumn.DataPropertyName = "dDATE"
        Me.DDATEDataGridViewTextBoxColumn.HeaderText = "dDATE"
        Me.DDATEDataGridViewTextBoxColumn.Name = "DDATEDataGridViewTextBoxColumn"
        '
        'TRIDDataGridViewTextBoxColumn
        '
        Me.TRIDDataGridViewTextBoxColumn.DataPropertyName = "TR_ID"
        Me.TRIDDataGridViewTextBoxColumn.HeaderText = "TR_ID"
        Me.TRIDDataGridViewTextBoxColumn.Name = "TRIDDataGridViewTextBoxColumn"
        '
        'DrDataGridViewTextBoxColumn
        '
        Me.DrDataGridViewTextBoxColumn.DataPropertyName = "Dr"
        Me.DrDataGridViewTextBoxColumn.HeaderText = "Dr"
        Me.DrDataGridViewTextBoxColumn.Name = "DrDataGridViewTextBoxColumn"
        '
        'CrDataGridViewTextBoxColumn
        '
        Me.CrDataGridViewTextBoxColumn.DataPropertyName = "Cr"
        Me.CrDataGridViewTextBoxColumn.HeaderText = "Cr"
        Me.CrDataGridViewTextBoxColumn.Name = "CrDataGridViewTextBoxColumn"
        '
        'BALDataGridViewTextBoxColumn
        '
        Me.BALDataGridViewTextBoxColumn.DataPropertyName = "BAL"
        Me.BALDataGridViewTextBoxColumn.HeaderText = "BAL"
        Me.BALDataGridViewTextBoxColumn.Name = "BALDataGridViewTextBoxColumn"
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = 0
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(12, 12)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.ReportSource = Me.TEMP_LEDGER1
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(524, 307)
        Me.CrystalReportViewer1.TabIndex = 10
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(944, 424)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtDATE2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDATE1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCL_ID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtGP_ID)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Form3"
        Me.Text = "Form3"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsTEMP_LEDGER1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents SqlSelectCommand1 As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection1 As System.Data.SqlClient.SqlConnection
    Friend WithEvents daTEMP_LEDGER As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsTEMP_LEDGER1 As Neruo_Business_Solution.dsTEMP_LEDGER
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtGP_ID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCL_ID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDATE1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDATE2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GROUPIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TRTYPEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SDESCDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CLIENTIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DDATEDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TRIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DrDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CrDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BALDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents TEMP_LEDGER1 As Neruo_Business_Solution.TEMP_LEDGER
End Class
