namespace ImageFunctions.Controls
{
	partial class SusanCornerProperties
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
			this.trackDifferenceThreshold = new System.Windows.Forms.TrackBar();
			this.label2 = new System.Windows.Forms.Label();
			this.lblDifferenceThreshold = new System.Windows.Forms.Label();
			this.lblGeometricalThreshold = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.trackGeometricalThreshold = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.trackDifferenceThreshold)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackGeometricalThreshold)).BeginInit();
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
			this.label1.TabIndex = 0;
			this.label1.Text = "Susan Corner Detection Settings";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// trackDifferenceThreshold
			// 
			this.trackDifferenceThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackDifferenceThreshold.Location = new System.Drawing.Point(7, 59);
			this.trackDifferenceThreshold.Maximum = 100;
			this.trackDifferenceThreshold.Name = "trackDifferenceThreshold";
			this.trackDifferenceThreshold.Size = new System.Drawing.Size(342, 45);
			this.trackDifferenceThreshold.TabIndex = 3;
			this.trackDifferenceThreshold.ValueChanged += new System.EventHandler(this.trackDifferenceThreshold_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Difference Threshold";
			// 
			// lblDifferenceThreshold
			// 
			this.lblDifferenceThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDifferenceThreshold.AutoSize = true;
			this.lblDifferenceThreshold.Location = new System.Drawing.Point(119, 43);
			this.lblDifferenceThreshold.Name = "lblDifferenceThreshold";
			this.lblDifferenceThreshold.Size = new System.Drawing.Size(13, 13);
			this.lblDifferenceThreshold.TabIndex = 5;
			this.lblDifferenceThreshold.Text = "0";
			// 
			// lblGeometricalThreshold
			// 
			this.lblGeometricalThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblGeometricalThreshold.AutoSize = true;
			this.lblGeometricalThreshold.Location = new System.Drawing.Point(119, 107);
			this.lblGeometricalThreshold.Name = "lblGeometricalThreshold";
			this.lblGeometricalThreshold.Size = new System.Drawing.Size(13, 13);
			this.lblGeometricalThreshold.TabIndex = 8;
			this.lblGeometricalThreshold.Text = "0";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 107);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(113, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Geometrical Threshold";
			// 
			// trackGeometricalThreshold
			// 
			this.trackGeometricalThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackGeometricalThreshold.Location = new System.Drawing.Point(7, 123);
			this.trackGeometricalThreshold.Maximum = 100;
			this.trackGeometricalThreshold.Name = "trackGeometricalThreshold";
			this.trackGeometricalThreshold.Size = new System.Drawing.Size(342, 45);
			this.trackGeometricalThreshold.TabIndex = 6;
			this.trackGeometricalThreshold.ValueChanged += new System.EventHandler(this.trackGeometricalThreshold_ValueChanged);
			// 
			// SusanCornerProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblGeometricalThreshold);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.trackGeometricalThreshold);
			this.Controls.Add(this.lblDifferenceThreshold);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.trackDifferenceThreshold);
			this.Controls.Add(this.label1);
			this.Name = "SusanCornerProperties";
			this.Size = new System.Drawing.Size(352, 166);
			((System.ComponentModel.ISupportInitialize)(this.trackDifferenceThreshold)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackGeometricalThreshold)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar trackDifferenceThreshold;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblDifferenceThreshold;
		private System.Windows.Forms.Label lblGeometricalThreshold;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TrackBar trackGeometricalThreshold;
	}
}
