namespace WindowsFormsApplication1
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
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.nudRows = new System.Windows.Forms.NumericUpDown();
            this.nudCols = new System.Windows.Forms.NumericUpDown();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Panel1 = new WindowsFormsApplication1.GraphicsPanel();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCols)).BeginInit();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(248, 67);
            this.nudHeight.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.ReadOnly = true;
            this.nudHeight.Size = new System.Drawing.Size(70, 20);
            this.nudHeight.TabIndex = 23;
            this.nudHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(248, 28);
            this.nudWidth.Maximum = new decimal(new int[] {
            140,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.ReadOnly = true;
            this.nudWidth.Size = new System.Drawing.Size(70, 20);
            this.nudWidth.TabIndex = 22;
            this.nudWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(245, 51);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(91, 13);
            this.Label5.TabIndex = 21;
            this.Label5.Text = "Cell Height(Pixels)";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(245, 12);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(88, 13);
            this.Label6.TabIndex = 20;
            this.Label6.Text = "Cell Width(Pixels)";
            // 
            // nudRows
            // 
            this.nudRows.Location = new System.Drawing.Point(169, 67);
            this.nudRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudRows.Name = "nudRows";
            this.nudRows.ReadOnly = true;
            this.nudRows.Size = new System.Drawing.Size(70, 20);
            this.nudRows.TabIndex = 19;
            this.nudRows.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // nudCols
            // 
            this.nudCols.Location = new System.Drawing.Point(169, 28);
            this.nudCols.Maximum = new decimal(new int[] {
            140,
            0,
            0,
            0});
            this.nudCols.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudCols.Name = "nudCols";
            this.nudCols.ReadOnly = true;
            this.nudCols.Size = new System.Drawing.Size(70, 20);
            this.nudCols.TabIndex = 18;
            this.nudCols.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(166, 51);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(60, 13);
            this.Label4.TabIndex = 17;
            this.Label4.Text = "Row Count";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(166, 12);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(73, 13);
            this.Label3.TabIndex = 16;
            this.Label3.Text = "Column Count";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(12, 12);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(122, 52);
            this.Button1.TabIndex = 14;
            this.Button1.Text = "New Maze";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Panel2
            // 
            this.Panel2.AutoScroll = true;
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.Panel2.Controls.Add(this.Panel1);
            this.Panel2.Location = new System.Drawing.Point(12, 100);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(200, 140);
            this.Panel2.TabIndex = 15;
            // 
            // Panel1
            // 
            this.Panel1.Location = new System.Drawing.Point(4, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(200, 100);
            this.Panel1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.nudRows);
            this.Controls.Add(this.nudCols);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Panel2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCols)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.NumericUpDown nudHeight;
        internal System.Windows.Forms.NumericUpDown nudWidth;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.NumericUpDown nudRows;
        internal System.Windows.Forms.NumericUpDown nudCols;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Panel Panel2;
        private GraphicsPanel Panel1;
    }
}

