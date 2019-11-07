namespace RESTbookWinClient
{
    partial class postFrm
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
            this.TBBookName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TBpubYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPost = new System.Windows.Forms.Button();
            this.TBresult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TBBookName
            // 
            this.TBBookName.Location = new System.Drawing.Point(93, 26);
            this.TBBookName.Name = "TBBookName";
            this.TBBookName.Size = new System.Drawing.Size(157, 20);
            this.TBBookName.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Book Name";
            // 
            // TBpubYear
            // 
            this.TBpubYear.Location = new System.Drawing.Point(93, 68);
            this.TBpubYear.Name = "TBpubYear";
            this.TBpubYear.Size = new System.Drawing.Size(157, 20);
            this.TBpubYear.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Pub Year";
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(93, 117);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(98, 23);
            this.btnPost.TabIndex = 7;
            this.btnPost.Text = "post (create)";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // TBresult
            // 
            this.TBresult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBresult.Location = new System.Drawing.Point(20, 158);
            this.TBresult.Multiline = true;
            this.TBresult.Name = "TBresult";
            this.TBresult.Size = new System.Drawing.Size(252, 112);
            this.TBresult.TabIndex = 14;
            // 
            // postFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 301);
            this.Controls.Add(this.TBresult);
            this.Controls.Add(this.TBBookName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TBpubYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPost);
            this.Name = "postFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "postFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBBookName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TBpubYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.TextBox TBresult;
    }
}