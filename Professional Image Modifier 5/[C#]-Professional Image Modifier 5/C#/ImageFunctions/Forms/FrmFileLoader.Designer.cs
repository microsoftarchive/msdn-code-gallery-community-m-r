namespace ImageFunctions.Forms
{
    partial class FrmFileLoader
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
            this.ThumbnailLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // ThumbnailLayoutPanel
            // 
            this.ThumbnailLayoutPanel.AutoScroll = true;
            this.ThumbnailLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThumbnailLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.ThumbnailLayoutPanel.Name = "ThumbnailLayoutPanel";
            this.ThumbnailLayoutPanel.Size = new System.Drawing.Size(354, 316);
            this.ThumbnailLayoutPanel.TabIndex = 0;
            // 
            // FrmFileLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 316);
            this.ControlBox = false;
            this.Controls.Add(this.ThumbnailLayoutPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmFileLoader";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "File Loader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ThumbnailLayoutPanel;

    }
}