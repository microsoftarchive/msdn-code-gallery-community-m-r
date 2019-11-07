namespace ImageFunctions.Forms
{
	partial class FrmImageDisplay
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectedColour = new System.Windows.Forms.Button();
            this.lblMouseY = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMouseX = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbImage = new Cyotek.Windows.Forms.ImageBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.rToolStripMenuItem,
            this.resetZoomToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(156, 76);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.toolStripMenuItem1.Text = "Show Pixel Grid";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // rToolStripMenuItem
            // 
            this.rToolStripMenuItem.Name = "rToolStripMenuItem";
            this.rToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.rToolStripMenuItem.Text = "Reset Image";
            this.rToolStripMenuItem.Click += new System.EventHandler(this.rToolStripMenuItem_Click);
            // 
            // resetZoomToolStripMenuItem
            // 
            this.resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            this.resetZoomToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.resetZoomToolStripMenuItem.Text = "Reset Zoom";
            this.resetZoomToolStripMenuItem.Click += new System.EventHandler(this.resetZoomToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblFileSize);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSelectedColour);
            this.panel1.Controls.Add(this.lblMouseY);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblMouseX);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 235);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(514, 26);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(384, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Colour:";
            // 
            // btnSelectedColour
            // 
            this.btnSelectedColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectedColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectedColour.Location = new System.Drawing.Point(427, 4);
            this.btnSelectedColour.Name = "btnSelectedColour";
            this.btnSelectedColour.Size = new System.Drawing.Size(75, 13);
            this.btnSelectedColour.TabIndex = 4;
            this.btnSelectedColour.UseVisualStyleBackColor = true;
            // 
            // lblMouseY
            // 
            this.lblMouseY.AutoSize = true;
            this.lblMouseY.Location = new System.Drawing.Point(162, 4);
            this.lblMouseY.Name = "lblMouseY";
            this.lblMouseY.Size = new System.Drawing.Size(37, 13);
            this.lblMouseY.TabIndex = 3;
            this.lblMouseY.Text = "00000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mouse Y:";
            // 
            // lblMouseX
            // 
            this.lblMouseX.AutoSize = true;
            this.lblMouseX.Location = new System.Drawing.Point(61, 4);
            this.lblMouseX.Name = "lblMouseX";
            this.lblMouseX.Size = new System.Drawing.Size(37, 13);
            this.lblMouseX.TabIndex = 1;
            this.lblMouseX.Text = "00000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mouse X:";
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.DimGray;
            this.pbImage.ContextMenuStrip = this.contextMenuStrip1;
            this.pbImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Image;
            this.pbImage.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(514, 235);
            this.pbImage.TabIndex = 2;
            this.pbImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseMove_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Size:";
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(241, 4);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(13, 13);
            this.lblFileSize.TabIndex = 7;
            this.lblFileSize.Text = "0";
            // 
            // FrmImageDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 261);
            this.ControlBox = false;
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmImageDisplay";
            this.Text = "Image Display";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem rToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblMouseY;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblMouseX;
		private System.Windows.Forms.Label label1;
		private Cyotek.Windows.Forms.ImageBox pbImage;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Button btnSelectedColour;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.Label label4;
	}
}