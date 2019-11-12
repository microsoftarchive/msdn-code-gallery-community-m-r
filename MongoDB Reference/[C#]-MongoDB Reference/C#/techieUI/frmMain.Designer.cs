namespace com.techphernalia.windows.forms.techieUI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitResource = new System.Windows.Forms.SplitContainer();
            this.lbl = new System.Windows.Forms.Label();
            this.treeResources = new System.Windows.Forms.TreeView();
            this.imgResources = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.grpDescription = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitResource)).BeginInit();
            this.splitResource.Panel1.SuspendLayout();
            this.splitResource.Panel2.SuspendLayout();
            this.splitResource.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitResource
            // 
            this.splitResource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitResource.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitResource.Location = new System.Drawing.Point(0, 0);
            this.splitResource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitResource.Name = "splitResource";
            // 
            // splitResource.Panel1
            // 
            this.splitResource.Panel1.Controls.Add(this.lbl);
            this.splitResource.Panel1.Controls.Add(this.treeResources);
            // 
            // splitResource.Panel2
            // 
            this.splitResource.Panel2.Controls.Add(this.label1);
            this.splitResource.Panel2.Controls.Add(this.cmbLanguage);
            this.splitResource.Panel2.Controls.Add(this.btnRun);
            this.splitResource.Panel2.Controls.Add(this.groupBox1);
            this.splitResource.Panel2.Controls.Add(this.grpDescription);
            this.splitResource.Size = new System.Drawing.Size(840, 398);
            this.splitResource.SplitterDistance = 300;
            this.splitResource.SplitterWidth = 6;
            this.splitResource.TabIndex = 0;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(16, 13);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(179, 19);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "Select a node to start with";
            // 
            // treeResources
            // 
            this.treeResources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeResources.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeResources.ImageIndex = 0;
            this.treeResources.ImageList = this.imgResources;
            this.treeResources.Location = new System.Drawing.Point(3, 41);
            this.treeResources.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeResources.Name = "treeResources";
            this.treeResources.SelectedImageIndex = 0;
            this.treeResources.Size = new System.Drawing.Size(294, 353);
            this.treeResources.TabIndex = 1;
            this.treeResources.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeResources_AfterCollapse);
            this.treeResources.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeResources_AfterExpand);
            this.treeResources.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeResources_AfterSelect);
            // 
            // imgResources
            // 
            this.imgResources.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgResources.ImageStream")));
            this.imgResources.TransparentColor = System.Drawing.Color.Transparent;
            this.imgResources.Images.SetKeyName(0, "Closed");
            this.imgResources.Images.SetKeyName(1, "Open");
            this.imgResources.Images.SetKeyName(2, "Topic");
            this.imgResources.Images.SetKeyName(3, "Top");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Language : ";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(93, 166);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(194, 27);
            this.cmbLanguage.TabIndex = 3;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(439, 166);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 27);
            this.btnRun.TabIndex = 1;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Location = new System.Drawing.Point(3, 200);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(518, 194);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Code Block";
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Location = new System.Drawing.Point(3, 24);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(512, 166);
            this.txtCode.TabIndex = 0;
            // 
            // grpDescription
            // 
            this.grpDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDescription.Controls.Add(this.txtDescription);
            this.grpDescription.Location = new System.Drawing.Point(3, 4);
            this.grpDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpDescription.Name = "grpDescription";
            this.grpDescription.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpDescription.Size = new System.Drawing.Size(518, 159);
            this.grpDescription.TabIndex = 0;
            this.grpDescription.TabStop = false;
            this.grpDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(3, 24);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(512, 131);
            this.txtDescription.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnRun;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 398);
            this.Controls.Add(this.splitResource);
            this.Font = new System.Drawing.Font("Calibri", 12F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Learning Mongo the techie way (techPhernalia)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitResource.Panel1.ResumeLayout(false);
            this.splitResource.Panel1.PerformLayout();
            this.splitResource.Panel2.ResumeLayout(false);
            this.splitResource.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitResource)).EndInit();
            this.splitResource.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpDescription.ResumeLayout(false);
            this.grpDescription.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitResource;
        private System.Windows.Forms.TreeView treeResources;
        private System.Windows.Forms.ImageList imgResources;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.GroupBox grpDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label label1;
    }
}

