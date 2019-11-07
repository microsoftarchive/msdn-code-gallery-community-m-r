namespace RESTbookWinClient
{
    partial class mainFrm
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
            this.btnDel = new System.Windows.Forms.Button();
            this.btnPut = new System.Windows.Forms.Button();
            this.btnGetByid = new System.Windows.Forms.Button();
            this.btnGetALL = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(173, 293);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(209, 49);
            this.btnDel.TabIndex = 9;
            this.btnDel.Text = "delete";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnPut
            // 
            this.btnPut.Location = new System.Drawing.Point(173, 227);
            this.btnPut.Name = "btnPut";
            this.btnPut.Size = new System.Drawing.Size(209, 49);
            this.btnPut.TabIndex = 8;
            this.btnPut.Text = "put(upd)";
            this.btnPut.UseVisualStyleBackColor = true;
            this.btnPut.Click += new System.EventHandler(this.btnPut_Click);
            // 
            // btnGetByid
            // 
            this.btnGetByid.Location = new System.Drawing.Point(173, 162);
            this.btnGetByid.Name = "btnGetByid";
            this.btnGetByid.Size = new System.Drawing.Size(209, 49);
            this.btnGetByid.TabIndex = 7;
            this.btnGetByid.Text = "getByid";
            this.btnGetByid.UseVisualStyleBackColor = true;
            this.btnGetByid.Click += new System.EventHandler(this.btnGetByid_Click);
            // 
            // btnGetALL
            // 
            this.btnGetALL.Location = new System.Drawing.Point(173, 96);
            this.btnGetALL.Name = "btnGetALL";
            this.btnGetALL.Size = new System.Drawing.Size(209, 49);
            this.btnGetALL.TabIndex = 6;
            this.btnGetALL.Text = "getALL";
            this.btnGetALL.UseVisualStyleBackColor = true;
            this.btnGetALL.Click += new System.EventHandler(this.btnGetALL_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(173, 31);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(209, 49);
            this.btnPost.TabIndex = 5;
            this.btnPost.Text = "post (create)";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.btnPost_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 391);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnPut);
            this.Controls.Add(this.btnGetByid);
            this.Controls.Add(this.btnGetALL);
            this.Controls.Add(this.btnPost);
            this.Name = "mainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnPut;
        private System.Windows.Forms.Button btnGetByid;
        private System.Windows.Forms.Button btnGetALL;
        private System.Windows.Forms.Button btnPost;
    }
}

