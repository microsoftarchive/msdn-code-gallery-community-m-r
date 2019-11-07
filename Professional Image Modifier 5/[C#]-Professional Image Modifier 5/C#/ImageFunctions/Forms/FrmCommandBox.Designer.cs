namespace ImageFunctions.Forms
{
    partial class FrmCommandBox
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFlash = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCommandInput = new System.Windows.Forms.TextBox();
            this.timerFlash = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.lblFlash);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbCommandInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 23);
            this.panel1.TabIndex = 2;
            // 
            // lblFlash
            // 
            this.lblFlash.BackColor = System.Drawing.Color.Black;
            this.lblFlash.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFlash.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblFlash.Location = new System.Drawing.Point(54, 0);
            this.lblFlash.Name = "lblFlash";
            this.lblFlash.Size = new System.Drawing.Size(10, 23);
            this.lblFlash.TabIndex = 2;
            this.lblFlash.Text = ":";
            this.lblFlash.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbCommandInput
            // 
            this.tbCommandInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCommandInput.BackColor = System.Drawing.Color.Black;
            this.tbCommandInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCommandInput.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbCommandInput.Location = new System.Drawing.Point(62, 5);
            this.tbCommandInput.Name = "tbCommandInput";
            this.tbCommandInput.Size = new System.Drawing.Size(599, 13);
            this.tbCommandInput.TabIndex = 0;
            // 
            // timerFlash
            // 
            this.timerFlash.Enabled = true;
            this.timerFlash.Interval = 500;
            this.timerFlash.Tick += new System.EventHandler(this.timerFlash_Tick_1);
            // 
            // FrmCommandBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 24);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmCommandBox";
            this.Text = "Command Box";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFlash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCommandInput;
        private System.Windows.Forms.Timer timerFlash;
    }
}