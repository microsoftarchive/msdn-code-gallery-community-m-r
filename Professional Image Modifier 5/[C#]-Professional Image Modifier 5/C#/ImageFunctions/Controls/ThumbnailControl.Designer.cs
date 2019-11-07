namespace ImageFunctions.Controls
{
    partial class ThumbnailControl
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
            this.lblSize = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSize
            // 
            this.lblSize.Location = new System.Drawing.Point(4, 132);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(139, 14);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "N/A";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(4, 118);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(139, 14);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "N/A";
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(3, 4);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(140, 107);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 3;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.pbImage_Click);
            this.pbImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbImage_MouseClick);
            this.pbImage.MouseEnter += new System.EventHandler(this.pbImage_MouseEnter);
            this.pbImage.MouseLeave += new System.EventHandler(this.pbImage_MouseLeave);
            // 
            // ThumbnailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.pbImage);
            this.Name = "ThumbnailControl";
            this.Size = new System.Drawing.Size(146, 151);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.PictureBox pbImage;
    }
}
