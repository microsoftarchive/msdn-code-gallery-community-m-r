namespace WindowsFormsApplication1
{
    partial class CustomersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomersForm));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkClearRows = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.CustomersGrid = new System.Windows.Forms.DataGridView();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.ProgressBar1);
            this.Panel1.Controls.Add(this.chkClearRows);
            this.Panel1.Controls.Add(this.cmdCancel);
            this.Panel1.Controls.Add(this.cmdLoad);
            resources.ApplyResources(this.Panel1, "Panel1");
            this.Panel1.Name = "Panel1";
            // 
            // ProgressBar1
            // 
            resources.ApplyResources(this.ProgressBar1, "ProgressBar1");
            this.ProgressBar1.Name = "ProgressBar1";
            // 
            // chkClearRows
            // 
            resources.ApplyResources(this.chkClearRows, "chkClearRows");
            this.chkClearRows.Name = "chkClearRows";
            this.chkClearRows.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdLoad
            // 
            resources.ApplyResources(this.cmdLoad, "cmdLoad");
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // CustomersGrid
            // 
            this.CustomersGrid.AllowUserToAddRows = false;
            this.CustomersGrid.AllowUserToDeleteRows = false;
            this.CustomersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.CustomersGrid, "CustomersGrid");
            this.CustomersGrid.Name = "CustomersGrid";
            this.CustomersGrid.ReadOnly = true;
            this.CustomersGrid.RowTemplate.Height = 24;
            // 
            // CustomersForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CustomersGrid);
            this.Controls.Add(this.Panel1);
            this.Name = "CustomersForm";
            this.Load += new System.EventHandler(this.CustomersForm_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomersGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.Windows.Forms.CheckBox chkClearRows;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdLoad;
        internal System.Windows.Forms.DataGridView CustomersGrid;
    }
}

