namespace ImageFunctions.Forms
{
	partial class FrmHistogram
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
            this.horizontalIntensityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalIntensityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.splineAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AreaStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splineRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCoordinatesY = new System.Windows.Forms.Label();
            this.lbCoordinatesX = new System.Windows.Forms.Label();
            this.btnPixelColour = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBlueMax = new System.Windows.Forms.Label();
            this.lblBlueMean = new System.Windows.Forms.Label();
            this.lblBlueMin = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblGreenMax = new System.Windows.Forms.Label();
            this.lblGreenMean = new System.Windows.Forms.Label();
            this.lblGreenMin = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblRedMax = new System.Windows.Forms.Label();
            this.lblRedMean = new System.Windows.Forms.Label();
            this.lblRedMin = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalIntensityMenuItem,
            this.verticalIntensityMenuItem,
            this.toolStripSeparator1,
            this.splineAreaToolStripMenuItem,
            this.AreaStripMenuItem,
            this.splineRangeToolStripMenuItem,
            this.splineToolStripMenuItem,
            this.barToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 164);
            // 
            // horizontalIntensityMenuItem
            // 
            this.horizontalIntensityMenuItem.Checked = true;
            this.horizontalIntensityMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.horizontalIntensityMenuItem.Name = "horizontalIntensityMenuItem";
            this.horizontalIntensityMenuItem.Size = new System.Drawing.Size(177, 22);
            this.horizontalIntensityMenuItem.Text = "Horizontal Intensity";
            this.horizontalIntensityMenuItem.Click += new System.EventHandler(this.horizontalIntensityMenuItem_Click);
            // 
            // verticalIntensityMenuItem
            // 
            this.verticalIntensityMenuItem.Name = "verticalIntensityMenuItem";
            this.verticalIntensityMenuItem.Size = new System.Drawing.Size(177, 22);
            this.verticalIntensityMenuItem.Text = "Vertical Intensity";
            this.verticalIntensityMenuItem.Click += new System.EventHandler(this.verticalIntensityMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
            // 
            // splineAreaToolStripMenuItem
            // 
            this.splineAreaToolStripMenuItem.Checked = true;
            this.splineAreaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.splineAreaToolStripMenuItem.Name = "splineAreaToolStripMenuItem";
            this.splineAreaToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.splineAreaToolStripMenuItem.Text = "Spline Area";
            this.splineAreaToolStripMenuItem.Click += new System.EventHandler(this.splineAreaToolStripMenuItem_Click);
            // 
            // AreaStripMenuItem
            // 
            this.AreaStripMenuItem.Name = "AreaStripMenuItem";
            this.AreaStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.AreaStripMenuItem.Text = "Area";
            this.AreaStripMenuItem.Click += new System.EventHandler(this.AreaStripMenuItem_Click);
            // 
            // splineRangeToolStripMenuItem
            // 
            this.splineRangeToolStripMenuItem.Name = "splineRangeToolStripMenuItem";
            this.splineRangeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.splineRangeToolStripMenuItem.Text = "Spline Range";
            this.splineRangeToolStripMenuItem.Click += new System.EventHandler(this.splineRangeToolStripMenuItem_Click);
            // 
            // splineToolStripMenuItem
            // 
            this.splineToolStripMenuItem.Name = "splineToolStripMenuItem";
            this.splineToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.splineToolStripMenuItem.Text = "Spline";
            this.splineToolStripMenuItem.Click += new System.EventHandler(this.splineToolStripMenuItem_Click);
            // 
            // barToolStripMenuItem
            // 
            this.barToolStripMenuItem.Name = "barToolStripMenuItem";
            this.barToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.barToolStripMenuItem.Text = "Bar";
            this.barToolStripMenuItem.Click += new System.EventHandler(this.barToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCoordinatesY);
            this.panel1.Controls.Add(this.lbCoordinatesX);
            this.panel1.Controls.Add(this.btnPixelColour);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblBlueMax);
            this.panel1.Controls.Add(this.lblBlueMean);
            this.panel1.Controls.Add(this.lblBlueMin);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.lblGreenMax);
            this.panel1.Controls.Add(this.lblGreenMean);
            this.panel1.Controls.Add(this.lblGreenMin);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lblRedMax);
            this.panel1.Controls.Add(this.lblRedMean);
            this.panel1.Controls.Add(this.lblRedMin);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 224);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 74);
            this.panel1.TabIndex = 1;
            // 
            // lblCoordinatesY
            // 
            this.lblCoordinatesY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCoordinatesY.AutoSize = true;
            this.lblCoordinatesY.Location = new System.Drawing.Point(98, 57);
            this.lblCoordinatesY.Name = "lblCoordinatesY";
            this.lblCoordinatesY.Size = new System.Drawing.Size(31, 13);
            this.lblCoordinatesY.TabIndex = 20;
            this.lblCoordinatesY.Text = "0000";
            // 
            // lbCoordinatesX
            // 
            this.lbCoordinatesX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCoordinatesX.AutoSize = true;
            this.lbCoordinatesX.Location = new System.Drawing.Point(61, 57);
            this.lbCoordinatesX.Name = "lbCoordinatesX";
            this.lbCoordinatesX.Size = new System.Drawing.Size(31, 13);
            this.lbCoordinatesX.TabIndex = 19;
            this.lbCoordinatesX.Text = "0000";
            // 
            // btnPixelColour
            // 
            this.btnPixelColour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPixelColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPixelColour.Location = new System.Drawing.Point(210, 57);
            this.btnPixelColour.Margin = new System.Windows.Forms.Padding(0);
            this.btnPixelColour.Name = "btnPixelColour";
            this.btnPixelColour.Size = new System.Drawing.Size(75, 12);
            this.btnPixelColour.TabIndex = 18;
            this.btnPixelColour.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Pixel Colour:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Mouse XY:";
            // 
            // lblBlueMax
            // 
            this.lblBlueMax.BackColor = System.Drawing.SystemColors.Control;
            this.lblBlueMax.ForeColor = System.Drawing.Color.Blue;
            this.lblBlueMax.Location = new System.Drawing.Point(245, 43);
            this.lblBlueMax.Name = "lblBlueMax";
            this.lblBlueMax.Size = new System.Drawing.Size(37, 13);
            this.lblBlueMax.TabIndex = 15;
            this.lblBlueMax.Text = "0";
            this.lblBlueMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBlueMean
            // 
            this.lblBlueMean.BackColor = System.Drawing.SystemColors.Control;
            this.lblBlueMean.ForeColor = System.Drawing.Color.Blue;
            this.lblBlueMean.Location = new System.Drawing.Point(150, 43);
            this.lblBlueMean.Name = "lblBlueMean";
            this.lblBlueMean.Size = new System.Drawing.Size(44, 13);
            this.lblBlueMean.TabIndex = 14;
            this.lblBlueMean.Text = "0";
            this.lblBlueMean.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBlueMin
            // 
            this.lblBlueMin.BackColor = System.Drawing.SystemColors.Control;
            this.lblBlueMin.ForeColor = System.Drawing.Color.Blue;
            this.lblBlueMin.Location = new System.Drawing.Point(68, 43);
            this.lblBlueMin.Name = "lblBlueMin";
            this.lblBlueMin.Size = new System.Drawing.Size(34, 13);
            this.lblBlueMin.TabIndex = 13;
            this.lblBlueMin.Text = "0";
            this.lblBlueMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 43);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "Blue";
            // 
            // lblGreenMax
            // 
            this.lblGreenMax.ForeColor = System.Drawing.Color.Green;
            this.lblGreenMax.Location = new System.Drawing.Point(245, 30);
            this.lblGreenMax.Name = "lblGreenMax";
            this.lblGreenMax.Size = new System.Drawing.Size(37, 13);
            this.lblGreenMax.TabIndex = 11;
            this.lblGreenMax.Text = "0";
            this.lblGreenMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGreenMean
            // 
            this.lblGreenMean.ForeColor = System.Drawing.Color.Green;
            this.lblGreenMean.Location = new System.Drawing.Point(150, 30);
            this.lblGreenMean.Name = "lblGreenMean";
            this.lblGreenMean.Size = new System.Drawing.Size(44, 13);
            this.lblGreenMean.TabIndex = 10;
            this.lblGreenMean.Text = "0";
            this.lblGreenMean.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGreenMin
            // 
            this.lblGreenMin.ForeColor = System.Drawing.Color.Green;
            this.lblGreenMin.Location = new System.Drawing.Point(68, 30);
            this.lblGreenMin.Name = "lblGreenMin";
            this.lblGreenMin.Size = new System.Drawing.Size(34, 13);
            this.lblGreenMin.TabIndex = 9;
            this.lblGreenMin.Text = "0";
            this.lblGreenMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Green";
            // 
            // lblRedMax
            // 
            this.lblRedMax.ForeColor = System.Drawing.Color.Red;
            this.lblRedMax.Location = new System.Drawing.Point(245, 17);
            this.lblRedMax.Name = "lblRedMax";
            this.lblRedMax.Size = new System.Drawing.Size(37, 13);
            this.lblRedMax.TabIndex = 7;
            this.lblRedMax.Text = "0";
            this.lblRedMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRedMean
            // 
            this.lblRedMean.ForeColor = System.Drawing.Color.Red;
            this.lblRedMean.Location = new System.Drawing.Point(150, 17);
            this.lblRedMean.Name = "lblRedMean";
            this.lblRedMean.Size = new System.Drawing.Size(44, 13);
            this.lblRedMean.TabIndex = 6;
            this.lblRedMean.Text = "0";
            this.lblRedMean.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRedMin
            // 
            this.lblRedMin.ForeColor = System.Drawing.Color.Red;
            this.lblRedMin.Location = new System.Drawing.Point(68, 17);
            this.lblRedMin.Name = "lblRedMin";
            this.lblRedMin.Size = new System.Drawing.Size(34, 13);
            this.lblRedMin.TabIndex = 5;
            this.lblRedMin.Text = "0";
            this.lblRedMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Red";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Max";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mean";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Min";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Colour";
            // 
            // FrmHistogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 298);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimumSize = new System.Drawing.Size(304, 304);
            this.Name = "FrmHistogram";
            this.Text = "Histogram";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem verticalIntensityMenuItem;
		private System.Windows.Forms.ToolStripMenuItem horizontalIntensityMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblBlueMax;
		private System.Windows.Forms.Label lblBlueMean;
		private System.Windows.Forms.Label lblBlueMin;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label lblGreenMax;
		private System.Windows.Forms.Label lblGreenMean;
		private System.Windows.Forms.Label lblGreenMin;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label lblRedMax;
		private System.Windows.Forms.Label lblRedMean;
		private System.Windows.Forms.Label lblRedMin;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnPixelColour;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblCoordinatesY;
		private System.Windows.Forms.Label lbCoordinatesX;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem splineAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splineRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AreaStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barToolStripMenuItem;
	}
}