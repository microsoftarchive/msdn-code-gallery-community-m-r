namespace ImageFunctions.Forms
{
	partial class FrmModificationType
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label2 = new System.Windows.Forms.Label();
            this.lbModification = new System.Windows.Forms.DomainUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lbMethods = new System.Windows.Forms.DomainUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lbAdditionalSelections = new System.Windows.Forms.DomainUpDown();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 23);
            this.label2.TabIndex = 41;
            this.label2.Text = "Modify";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbModification
            // 
            this.lbModification.Enabled = false;
            this.lbModification.Items.Add("None");
            this.lbModification.Location = new System.Drawing.Point(58, 10);
            this.lbModification.Name = "lbModification";
            this.lbModification.Size = new System.Drawing.Size(303, 20);
            this.lbModification.TabIndex = 43;
            this.lbModification.SelectedItemChanged += new System.EventHandler(this.lbModification_SelectedItemChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 23);
            this.label3.TabIndex = 42;
            this.label3.Text = "Method";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbMethods
            // 
            this.lbMethods.Enabled = false;
            this.lbMethods.Items.Add("None");
            this.lbMethods.Location = new System.Drawing.Point(58, 36);
            this.lbMethods.Name = "lbMethods";
            this.lbMethods.Size = new System.Drawing.Size(303, 20);
            this.lbMethods.TabIndex = 44;
            this.lbMethods.SelectedItemChanged += new System.EventHandler(this.lbMethods_SelectedItemChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 23);
            this.label1.TabIndex = 46;
            // 
            // lbAdditionalSelections
            // 
            this.lbAdditionalSelections.Enabled = false;
            this.lbAdditionalSelections.Items.Add("None");
            this.lbAdditionalSelections.Location = new System.Drawing.Point(58, 62);
            this.lbAdditionalSelections.Name = "lbAdditionalSelections";
            this.lbAdditionalSelections.Size = new System.Drawing.Size(303, 20);
            this.lbAdditionalSelections.TabIndex = 45;
            this.lbAdditionalSelections.Visible = false;
            // 
            // FrmModificationType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 89);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbModification);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbMethods);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbAdditionalSelections);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmModificationType";
            this.Text = "Modification Types";
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DomainUpDown lbModification;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DomainUpDown lbMethods;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DomainUpDown lbAdditionalSelections;

    }
}