Namespace ImageFunctions.Forms
	Partial Public Class FrmStatistics
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Dim dataGridViewCellStyle1 As New System.Windows.Forms.DataGridViewCellStyle()
			Me.dgvStatistics = New System.Windows.Forms.DataGridView()
			Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
			Me.Details = New System.Windows.Forms.DataGridViewTextBoxColumn()
			DirectCast(Me.dgvStatistics, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' dgvStatistics
			' 
			Me.dgvStatistics.AllowUserToAddRows = False
			Me.dgvStatistics.AllowUserToDeleteRows = False
			Me.dgvStatistics.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
			Me.dgvStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
			Me.dgvStatistics.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() { Me.Description, Me.Details})
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkGray
			dataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False
			Me.dgvStatistics.DefaultCellStyle = dataGridViewCellStyle1
			Me.dgvStatistics.Dock = System.Windows.Forms.DockStyle.Fill
			Me.dgvStatistics.Location = New System.Drawing.Point(0, 0)
			Me.dgvStatistics.Name = "dgvStatistics"
			Me.dgvStatistics.ReadOnly = True
			Me.dgvStatistics.RowHeadersVisible = False
			Me.dgvStatistics.ShowCellErrors = False
			Me.dgvStatistics.ShowCellToolTips = False
			Me.dgvStatistics.ShowRowErrors = False
			Me.dgvStatistics.Size = New System.Drawing.Size(654, 345)
			Me.dgvStatistics.TabIndex = 1
			' 
			' Description
			' 
			Me.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
			Me.Description.DividerWidth = 1
			Me.Description.HeaderText = "Description"
			Me.Description.Name = "Description"
			Me.Description.ReadOnly = True
			Me.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
			Me.Description.Width = 67
			' 
			' Details
			' 
			Me.Details.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
			Me.Details.DividerWidth = 1
			Me.Details.HeaderText = "Details"
			Me.Details.Name = "Details"
			Me.Details.ReadOnly = True
			Me.Details.Width = 65
			' 
			' FrmStatistics
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(654, 345)
			Me.Controls.Add(Me.dgvStatistics)
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.Name = "FrmStatistics"
			Me.Text = "Exif Statistics"
			DirectCast(Me.dgvStatistics, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private dgvStatistics As System.Windows.Forms.DataGridView
		Private Description As System.Windows.Forms.DataGridViewTextBoxColumn
		Private Details As System.Windows.Forms.DataGridViewTextBoxColumn
	End Class
End Namespace