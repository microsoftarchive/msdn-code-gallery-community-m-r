namespace ImageFunctions
{
	partial class Form1
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
			this.ilDefault = new System.Windows.Forms.ImageList(this.components);
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.btnLoadNewImage = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.pbImage = new Cyotek.Windows.Forms.ImageBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.SuspendLayout();
			// 
			// ilDefault
			// 
			this.ilDefault.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilDefault.ImageSize = new System.Drawing.Size(16, 16);
			this.ilDefault.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// openFile
			// 
			this.openFile.FileName = "openFileDialog1";
			this.openFile.Multiselect = true;
			this.openFile.ReadOnlyChecked = true;
			this.openFile.RestoreDirectory = true;
			this.openFile.SupportMultiDottedExtensions = true;
			this.openFile.Title = "Load More Files";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AutoScroll = true;
			this.splitContainer1.Panel2.Controls.Add(this.pbImage);
			
			this.splitContainer1.Size = new System.Drawing.Size(935, 696);
			this.splitContainer1.SplitterDistance = 435;
			this.splitContainer1.TabIndex = 8;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.btnLoadNewImage);
			this.splitContainer3.Panel1.Controls.Add(this.label1);
			this.splitContainer3.Panel1.Controls.Add(this.listBox1);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.button1);
			this.splitContainer3.Panel2.Controls.Add(this.listBox2);
			this.splitContainer3.Panel2.Controls.Add(this.label2);
			this.splitContainer3.Size = new System.Drawing.Size(433, 694);
			this.splitContainer3.SplitterDistance = 329;
			this.splitContainer3.TabIndex = 0;
			// 
			// btnLoadNewImage
			// 
			this.btnLoadNewImage.Location = new System.Drawing.Point(392, 9);
			this.btnLoadNewImage.Name = "btnLoadNewImage";
			this.btnLoadNewImage.Size = new System.Drawing.Size(25, 23);
			this.btnLoadNewImage.TabIndex = 22;
			this.btnLoadNewImage.Text = "...";
			this.btnLoadNewImage.UseVisualStyleBackColor = true;
			this.btnLoadNewImage.Click += new System.EventHandler(this.btnLoadNewImage_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 21;
			this.label1.Text = "Load Image:";
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(83, 12);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(303, 17);
			this.listBox1.TabIndex = 20;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// button1
			// 
		
			this.button1.Location = new System.Drawing.Point(379, 11);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(25, 23);
			this.button1.TabIndex = 25;
			this.button1.UseVisualStyleBackColor = true;
			// 
			// listBox2
			// 
			this.listBox2.FormattingEnabled = true;
			this.listBox2.Location = new System.Drawing.Point(70, 13);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(303, 17);
			this.listBox2.TabIndex = 23;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(26, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 24;
			this.label2.Text = "Modify";
			// 
			// pbImage
			// 
			this.pbImage.BackColor = System.Drawing.Color.DimGray;
			this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbImage.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.Image;
			this.pbImage.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.None;
			this.pbImage.Location = new System.Drawing.Point(0, 0);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(494, 694);
			this.pbImage.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(935, 696);
			this.Controls.Add(this.splitContainer1);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Image Functions";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ImageList ilDefault;
		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.Button btnLoadNewImage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.Label label2;
		private Cyotek.Windows.Forms.ImageBox pbImage;
	}
}

