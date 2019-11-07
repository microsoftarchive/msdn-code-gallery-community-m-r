namespace ImageFunctions.Forms
{
    partial class FrmConsole
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
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerIdle = new System.Windows.Forms.Timer(this.components);
            this.rtbConOut = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // timerIdle
            // 
            this.timerIdle.Enabled = true;
            this.timerIdle.Interval = 1000;
            this.timerIdle.Tick += new System.EventHandler(this.timerIdle_Tick);
            // 
            // rtbConOut
            // 
            this.rtbConOut.BackColor = System.Drawing.Color.Black;
            this.rtbConOut.ContextMenuStrip = this.contextMenuStrip1;
            this.rtbConOut.DetectUrls = false;
            this.rtbConOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConOut.ForeColor = System.Drawing.Color.Gold;
            this.rtbConOut.Location = new System.Drawing.Point(0, 0);
            this.rtbConOut.Name = "rtbConOut";
            this.rtbConOut.ReadOnly = true;
            this.rtbConOut.ShowSelectionMargin = true;
            this.rtbConOut.Size = new System.Drawing.Size(450, 177);
            this.rtbConOut.TabIndex = 2;
            this.rtbConOut.Text = "";
            this.rtbConOut.WordWrap = false;
            // 
            // FrmConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 177);
            this.ControlBox = false;
            this.Controls.Add(this.rtbConOut);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmConsole";
            this.Text = "Console";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Timer timerIdle;
        private System.Windows.Forms.RichTextBox rtbConOut;
    }
}