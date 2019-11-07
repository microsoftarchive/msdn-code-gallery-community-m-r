namespace CSharpNetworkBandwidth
{
    partial class FormCSharpBandwidth
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
            this.testWebBrowser = new System.Windows.Forms.WebBrowser();
            this.totalBandwidthConsumptionLabel = new System.Windows.Forms.Label();
            this.refreshBrowserButton = new System.Windows.Forms.Button();
            this.currentBandwidthConsumptionLabel = new System.Windows.Forms.Label();
            this.downloadSampleFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testWebBrowser
            // 
            this.testWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.testWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.testWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.testWebBrowser.Name = "testWebBrowser";
            this.testWebBrowser.Size = new System.Drawing.Size(963, 479);
            this.testWebBrowser.TabIndex = 0;
            this.testWebBrowser.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // totalBandwidthConsumptionLabel
            // 
            this.totalBandwidthConsumptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.totalBandwidthConsumptionLabel.AutoSize = true;
            this.totalBandwidthConsumptionLabel.Location = new System.Drawing.Point(12, 497);
            this.totalBandwidthConsumptionLabel.Name = "totalBandwidthConsumptionLabel";
            this.totalBandwidthConsumptionLabel.Size = new System.Drawing.Size(151, 13);
            this.totalBandwidthConsumptionLabel.TabIndex = 1;
            this.totalBandwidthConsumptionLabel.Text = "Total Bandwidth Consumption:";
            // 
            // refreshBrowserButton
            // 
            this.refreshBrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBrowserButton.Location = new System.Drawing.Point(693, 487);
            this.refreshBrowserButton.Name = "refreshBrowserButton";
            this.refreshBrowserButton.Size = new System.Drawing.Size(122, 23);
            this.refreshBrowserButton.TabIndex = 2;
            this.refreshBrowserButton.Text = "Refresh Browser";
            this.refreshBrowserButton.UseVisualStyleBackColor = true;
            this.refreshBrowserButton.Click += new System.EventHandler(this.refreshBrowserButton_Click);
            // 
            // currentBandwidthConsumptionLabel
            // 
            this.currentBandwidthConsumptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.currentBandwidthConsumptionLabel.AutoSize = true;
            this.currentBandwidthConsumptionLabel.Location = new System.Drawing.Point(330, 497);
            this.currentBandwidthConsumptionLabel.Name = "currentBandwidthConsumptionLabel";
            this.currentBandwidthConsumptionLabel.Size = new System.Drawing.Size(161, 13);
            this.currentBandwidthConsumptionLabel.TabIndex = 1;
            this.currentBandwidthConsumptionLabel.Text = "Current Bandwidth Consumption:";
            // 
            // downloadSampleFileButton
            // 
            this.downloadSampleFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadSampleFileButton.Location = new System.Drawing.Point(831, 487);
            this.downloadSampleFileButton.Name = "downloadSampleFileButton";
            this.downloadSampleFileButton.Size = new System.Drawing.Size(122, 23);
            this.downloadSampleFileButton.TabIndex = 2;
            this.downloadSampleFileButton.Text = "Download Sample File";
            this.downloadSampleFileButton.UseVisualStyleBackColor = true;
            this.downloadSampleFileButton.Click += new System.EventHandler(this.downloadSampleFileButton_Click);
            // 
            // FormCSharpBandwidth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 519);
            this.Controls.Add(this.downloadSampleFileButton);
            this.Controls.Add(this.refreshBrowserButton);
            this.Controls.Add(this.currentBandwidthConsumptionLabel);
            this.Controls.Add(this.totalBandwidthConsumptionLabel);
            this.Controls.Add(this.testWebBrowser);
            this.Name = "FormCSharpBandwidth";
            this.Text = "C# Bandwidth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser testWebBrowser;
        private System.Windows.Forms.Label totalBandwidthConsumptionLabel;
        private System.Windows.Forms.Button refreshBrowserButton;
        private System.Windows.Forms.Label currentBandwidthConsumptionLabel;
        private System.Windows.Forms.Button downloadSampleFileButton;
    }
}

