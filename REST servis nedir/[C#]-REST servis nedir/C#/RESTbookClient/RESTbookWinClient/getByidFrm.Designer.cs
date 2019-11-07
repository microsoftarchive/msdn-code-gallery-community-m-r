namespace RESTbookWinClient
{
    partial class getByidFrm
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
            this.TBresult = new System.Windows.Forms.TextBox();
            this.TBid = new System.Windows.Forms.TextBox();
            this.btnGet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TBresult
            // 
            this.TBresult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBresult.Location = new System.Drawing.Point(12, 52);
            this.TBresult.Multiline = true;
            this.TBresult.Name = "TBresult";
            this.TBresult.Size = new System.Drawing.Size(252, 148);
            this.TBresult.TabIndex = 5;
            // 
            // TBid
            // 
            this.TBid.Location = new System.Drawing.Point(164, 12);
            this.TBid.Name = "TBid";
            this.TBid.Size = new System.Drawing.Size(100, 20);
            this.TBid.TabIndex = 4;
            this.TBid.Text = "2";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(11, 9);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(75, 23);
            this.btnGet.TabIndex = 3;
            this.btnGet.Text = "getByid";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "id";
            // 
            // getByidFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 208);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBresult);
            this.Controls.Add(this.TBid);
            this.Controls.Add(this.btnGet);
            this.Name = "getByidFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "getByid";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBresult;
        private System.Windows.Forms.TextBox TBid;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Label label1;
    }
}