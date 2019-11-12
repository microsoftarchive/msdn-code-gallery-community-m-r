namespace ImageFunctions.Controls
{
	partial class MoravecCornerProperties
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
			this.lblWindow = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.trackWindow = new System.Windows.Forms.TrackBar();
			this.lblThreshold = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.trackThreshold = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackWindow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackThreshold)).BeginInit();
			this.SuspendLayout();
			// 
			// lblWindow
			// 
			this.lblWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblWindow.AutoSize = true;
			this.lblWindow.Location = new System.Drawing.Point(82, 107);
			this.lblWindow.Name = "lblWindow";
			this.lblWindow.Size = new System.Drawing.Size(13, 13);
			this.lblWindow.TabIndex = 15;
			this.lblWindow.Text = "0";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 107);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Window Size";
			// 
			// trackWindow
			// 
			this.trackWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackWindow.Location = new System.Drawing.Point(7, 123);
			this.trackWindow.Maximum = 50;
			this.trackWindow.Name = "trackWindow";
			this.trackWindow.Size = new System.Drawing.Size(346, 45);
			this.trackWindow.TabIndex = 13;
			this.trackWindow.ValueChanged += new System.EventHandler(this.trackWindow_ValueChanged);
			// 
			// lblThreshold
			// 
			this.lblThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblThreshold.AutoSize = true;
			this.lblThreshold.Location = new System.Drawing.Point(82, 43);
			this.lblThreshold.Name = "lblThreshold";
			this.lblThreshold.Size = new System.Drawing.Size(13, 13);
			this.lblThreshold.TabIndex = 12;
			this.lblThreshold.Text = "0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Threshold";
			// 
			// trackThreshold
			// 
			this.trackThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackThreshold.Location = new System.Drawing.Point(7, 59);
			this.trackThreshold.Maximum = 1000;
			this.trackThreshold.Name = "trackThreshold";
			this.trackThreshold.Size = new System.Drawing.Size(346, 45);
			this.trackThreshold.TabIndex = 10;
			this.trackThreshold.TickFrequency = 10;
			this.trackThreshold.ValueChanged += new System.EventHandler(this.trackThreshold_ValueChanged);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(352, 23);
			this.label1.TabIndex = 9;
			this.label1.Text = "Moravec Corner Detection Settings";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MoravecCornerProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblWindow);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.trackWindow);
			this.Controls.Add(this.lblThreshold);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.trackThreshold);
			this.Controls.Add(this.label1);
			this.Name = "MoravecCornerProperties";
			this.Size = new System.Drawing.Size(352, 166);
			((System.ComponentModel.ISupportInitialize)(this.trackWindow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackThreshold)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblWindow;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TrackBar trackWindow;
		private System.Windows.Forms.Label lblThreshold;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar trackThreshold;
		private System.Windows.Forms.Label label1;
	}
}
