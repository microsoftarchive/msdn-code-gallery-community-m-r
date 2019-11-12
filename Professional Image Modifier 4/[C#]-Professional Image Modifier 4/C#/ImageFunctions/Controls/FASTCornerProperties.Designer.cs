namespace ImageFunctions.Controls
{
	partial class FASTCornerProperties
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.lblThreshold = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.trackThreshold = new System.Windows.Forms.TrackBar();
			this.cbSupress = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.trackThreshold)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(352, 23);
			this.label1.TabIndex = 11;
			this.label1.Text = "FAST Corner Detection Settings";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblThreshold
			// 
			this.lblThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblThreshold.AutoSize = true;
			this.lblThreshold.Location = new System.Drawing.Point(82, 43);
			this.lblThreshold.Name = "lblThreshold";
			this.lblThreshold.Size = new System.Drawing.Size(13, 13);
			this.lblThreshold.TabIndex = 21;
			this.lblThreshold.Text = "0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 20;
			this.label2.Text = "Threshold";
			// 
			// trackThreshold
			// 
			this.trackThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackThreshold.Location = new System.Drawing.Point(7, 59);
			this.trackThreshold.Maximum = 100;
			this.trackThreshold.Name = "trackThreshold";
			this.trackThreshold.Size = new System.Drawing.Size(346, 45);
			this.trackThreshold.TabIndex = 19;
			this.trackThreshold.ValueChanged += new System.EventHandler(this.trackThreshold_ValueChanged);
			// 
			// cbSupress
			// 
			this.cbSupress.AutoSize = true;
			this.cbSupress.Checked = true;
			this.cbSupress.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbSupress.Location = new System.Drawing.Point(10, 111);
			this.cbSupress.Name = "cbSupress";
			this.cbSupress.Size = new System.Drawing.Size(168, 17);
			this.cbSupress.TabIndex = 22;
			this.cbSupress.Text = "Suppress non-maximum points";
			this.cbSupress.UseVisualStyleBackColor = true;
			this.cbSupress.CheckStateChanged += new System.EventHandler(this.cbSupress_CheckStateChanged);
			// 
			// FASTCornerProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cbSupress);
			this.Controls.Add(this.lblThreshold);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.trackThreshold);
			this.Controls.Add(this.label1);
			this.Name = "FASTCornerProperties";
			this.Size = new System.Drawing.Size(352, 166);
			((System.ComponentModel.ISupportInitialize)(this.trackThreshold)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblThreshold;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar trackThreshold;
		private System.Windows.Forms.CheckBox cbSupress;
	}
}
