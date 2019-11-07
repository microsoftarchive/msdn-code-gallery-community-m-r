Namespace ImageFunctions.Forms
	Partial Public Class FrmModificationType
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
			Me.label2 = New System.Windows.Forms.Label()
			Me.lbModification = New System.Windows.Forms.DomainUpDown()
			Me.label3 = New System.Windows.Forms.Label()
			Me.lbMethods = New System.Windows.Forms.DomainUpDown()
			Me.label1 = New System.Windows.Forms.Label()
			Me.lbAdditionalSelections = New System.Windows.Forms.DomainUpDown()
			Me.SuspendLayout()
			' 
			' label2
			' 
			Me.label2.Location = New System.Drawing.Point(9, 7)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(43, 23)
			Me.label2.TabIndex = 41
			Me.label2.Text = "Modify"
			Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' lbModification
			' 
			Me.lbModification.Enabled = False
			Me.lbModification.Items.Add("None")
			Me.lbModification.Location = New System.Drawing.Point(58, 10)
			Me.lbModification.Name = "lbModification"
			Me.lbModification.Size = New System.Drawing.Size(303, 20)
			Me.lbModification.TabIndex = 43
'			Me.lbModification.SelectedItemChanged += New System.EventHandler(Me.lbModification_SelectedItemChanged)
			' 
			' label3
			' 
			Me.label3.Location = New System.Drawing.Point(9, 33)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(43, 23)
			Me.label3.TabIndex = 42
			Me.label3.Text = "Method"
			Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' lbMethods
			' 
			Me.lbMethods.Enabled = False
			Me.lbMethods.Items.Add("None")
			Me.lbMethods.Location = New System.Drawing.Point(58, 36)
			Me.lbMethods.Name = "lbMethods"
			Me.lbMethods.Size = New System.Drawing.Size(303, 20)
			Me.lbMethods.TabIndex = 44
'			Me.lbMethods.SelectedItemChanged += New System.EventHandler(Me.lbMethods_SelectedItemChanged)
			' 
			' label1
			' 
			Me.label1.Location = New System.Drawing.Point(9, 59)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(43, 23)
			Me.label1.TabIndex = 46
			' 
			' lbAdditionalSelections
			' 
			Me.lbAdditionalSelections.Enabled = False
			Me.lbAdditionalSelections.Items.Add("None")
			Me.lbAdditionalSelections.Location = New System.Drawing.Point(58, 62)
			Me.lbAdditionalSelections.Name = "lbAdditionalSelections"
			Me.lbAdditionalSelections.Size = New System.Drawing.Size(303, 20)
			Me.lbAdditionalSelections.TabIndex = 45
			Me.lbAdditionalSelections.Visible = False
			' 
			' FrmModificationType
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(370, 89)
			Me.ControlBox = False
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.lbModification)
			Me.Controls.Add(Me.label3)
			Me.Controls.Add(Me.lbMethods)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.lbAdditionalSelections)
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
			Me.Name = "FrmModificationType"
			Me.Text = "Modification Types"
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private label2 As System.Windows.Forms.Label
		Private WithEvents lbModification As System.Windows.Forms.DomainUpDown
		Private label3 As System.Windows.Forms.Label
		Private WithEvents lbMethods As System.Windows.Forms.DomainUpDown
		Private label1 As System.Windows.Forms.Label
		Private lbAdditionalSelections As System.Windows.Forms.DomainUpDown

	End Class
End Namespace