namespace ImageFunctions.Forms
{
	partial class FrmProcessing
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
			this.win8_ProgressRing1 = new Win8ProgressRing.Win8_ProgressRing();
			this.label1 = new System.Windows.Forms.Label();
			this.lblDescription = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// win8_ProgressRing1
			// 
			this.win8_ProgressRing1.Control_Height = 80;
			this.win8_ProgressRing1.Dock = System.Windows.Forms.DockStyle.Left;
			this.win8_ProgressRing1.Indicator_Color = System.Drawing.Color.MediumBlue;
			this.win8_ProgressRing1.Location = new System.Drawing.Point(0, 0);
			this.win8_ProgressRing1.Name = "win8_ProgressRing1";
			this.win8_ProgressRing1.Refresh_Rate = 100;
			this.win8_ProgressRing1.Size = new System.Drawing.Size(80, 80);
			this.win8_ProgressRing1.TabIndex = 0;
			this.win8_ProgressRing1.Text = "win8_ProgressRing1";
			this.win8_ProgressRing1.UseWaitCursor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(135, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Please Wait:";
			this.label1.UseWaitCursor = true;
			// 
			// lblDescription
			// 
			this.lblDescription.AutoEllipsis = true;
			this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDescription.Location = new System.Drawing.Point(135, 33);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(442, 44);
			this.lblDescription.TabIndex = 2;
			this.lblDescription.Text = "label2";
			this.lblDescription.UseWaitCursor = true;
			// 
			// FrmProcessing
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(589, 86);
			this.ControlBox = false;
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.win8_ProgressRing1);
			this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			this.ForeColor = System.Drawing.Color.LightYellow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmProcessing";
			this.Opacity = 0.85D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmProcessing";
			this.TopMost = true;
			this.UseWaitCursor = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Win8ProgressRing.Win8_ProgressRing win8_ProgressRing1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblDescription;
	}
}