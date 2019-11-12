namespace RESTbookWinClient
{
    partial class putFrm
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
            this.TBBookName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TBpubYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPut = new System.Windows.Forms.Button();
            this.TBid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TBresult
            // 
            this.TBresult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBresult.Location = new System.Drawing.Point(18, 203);
            this.TBresult.Multiline = true;
            this.TBresult.Name = "TBresult";
            this.TBresult.Size = new System.Drawing.Size(252, 116);
            this.TBresult.TabIndex = 20;
            // 
            // TBBookName
            // 
            this.TBBookName.Location = new System.Drawing.Point(91, 71);
            this.TBBookName.Name = "TBBookName";
            this.TBBookName.Size = new System.Drawing.Size(157, 20);
            this.TBBookName.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Book Name";
            // 
            // TBpubYear
            // 
            this.TBpubYear.Location = new System.Drawing.Point(91, 113);
            this.TBpubYear.Name = "TBpubYear";
            this.TBpubYear.Size = new System.Drawing.Size(157, 20);
            this.TBpubYear.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Pub Year";
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(91, 162);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(75, 23);
            this.btnPut.TabIndex = 15;
            this.btnPut.Text = "put (update)";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // TBid
            // 
            this.TBid.Location = new System.Drawing.Point(91, 12);
            this.TBid.Name = "TBid";
            this.TBid.Size = new System.Drawing.Size(157, 20);
            this.TBid.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "id";
            // 
            // putFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 330);
            this.Controls.Add(this.TBid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBresult);
            this.Controls.Add(this.TBBookName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TBpubYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPut);
            this.Name = "putFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "putFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBresult;
        private System.Windows.Forms.TextBox TBBookName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TBpubYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.TextBox TBid;
        private System.Windows.Forms.Label label1;
    }
}