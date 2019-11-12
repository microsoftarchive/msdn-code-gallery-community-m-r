namespace FileSharing_FTP_Server
{
    partial class Server
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
            this.StartServer = new System.Windows.Forms.Button();
            this.ServerIP = new System.Windows.Forms.Label();
            this.ServerIPValue = new System.Windows.Forms.Label();
            this.Separate = new System.Windows.Forms.Label();
            this.ServerPort = new System.Windows.Forms.Label();
            this.ServerPortValue = new System.Windows.Forms.TextBox();
            this.ServerStatus = new System.Windows.Forms.StatusStrip();
            this.ServerStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.Logger = new System.Windows.Forms.TextBox();
            this.ServerStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartServer
            // 
            this.StartServer.Location = new System.Drawing.Point(408, 11);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(75, 23);
            this.StartServer.TabIndex = 0;
            this.StartServer.Text = "Start Server";
            this.StartServer.UseVisualStyleBackColor = true;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // ServerIP
            // 
            this.ServerIP.AutoSize = true;
            this.ServerIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerIP.Location = new System.Drawing.Point(12, 15);
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Size = new System.Drawing.Size(72, 18);
            this.ServerIP.TabIndex = 3;
            this.ServerIP.Text = "Server IP:";
            // 
            // ServerIPValue
            // 
            this.ServerIPValue.AutoSize = true;
            this.ServerIPValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerIPValue.Location = new System.Drawing.Point(90, 15);
            this.ServerIPValue.Name = "ServerIPValue";
            this.ServerIPValue.Size = new System.Drawing.Size(76, 18);
            this.ServerIPValue.TabIndex = 4;
            this.ServerIPValue.Text = "[Server IP]";
            // 
            // Separate
            // 
            this.Separate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Separate.Location = new System.Drawing.Point(12, 40);
            this.Separate.Name = "Separate";
            this.Separate.Size = new System.Drawing.Size(471, 2);
            this.Separate.TabIndex = 5;
            // 
            // ServerPort
            // 
            this.ServerPort.AutoSize = true;
            this.ServerPort.Location = new System.Drawing.Point(249, 19);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(63, 13);
            this.ServerPort.TabIndex = 6;
            this.ServerPort.Text = "Server Port:";
            // 
            // ServerPortValue
            // 
            this.ServerPortValue.Location = new System.Drawing.Point(318, 14);
            this.ServerPortValue.MaxLength = 5;
            this.ServerPortValue.Name = "ServerPortValue";
            this.ServerPortValue.Size = new System.Drawing.Size(74, 20);
            this.ServerPortValue.TabIndex = 7;
            this.ServerPortValue.Text = "8081";
            this.ServerPortValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ServerPortValue_KeyPress);
            // 
            // ServerStatus
            // 
            this.ServerStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServerStatusMessage});
            this.ServerStatus.Location = new System.Drawing.Point(0, 199);
            this.ServerStatus.Name = "ServerStatus";
            this.ServerStatus.Size = new System.Drawing.Size(491, 22);
            this.ServerStatus.SizingGrip = false;
            this.ServerStatus.TabIndex = 9;
            // 
            // ServerStatusMessage
            // 
            this.ServerStatusMessage.Name = "ServerStatusMessage";
            this.ServerStatusMessage.Size = new System.Drawing.Size(43, 17);
            this.ServerStatusMessage.Text = "[Status]";
            // 
            // Logger
            // 
            this.Logger.BackColor = System.Drawing.Color.White;
            this.Logger.Location = new System.Drawing.Point(12, 45);
            this.Logger.Multiline = true;
            this.Logger.Name = "Logger";
            this.Logger.ReadOnly = true;
            this.Logger.Size = new System.Drawing.Size(467, 151);
            this.Logger.TabIndex = 10;
            // 
            // Server
            // 
            this.AcceptButton = this.StartServer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 221);
            this.Controls.Add(this.Logger);
            this.Controls.Add(this.ServerStatus);
            this.Controls.Add(this.ServerPortValue);
            this.Controls.Add(this.ServerPort);
            this.Controls.Add(this.Separate);
            this.Controls.Add(this.ServerIPValue);
            this.Controls.Add(this.ServerIP);
            this.Controls.Add(this.StartServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Server_FormClosing);
            this.ServerStatus.ResumeLayout(false);
            this.ServerStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartServer;
        private System.Windows.Forms.Label ServerIP;
        private System.Windows.Forms.Label ServerIPValue;
        private System.Windows.Forms.Label Separate;
        private System.Windows.Forms.Label ServerPort;
        private System.Windows.Forms.TextBox ServerPortValue;
        private System.Windows.Forms.StatusStrip ServerStatus;
        private System.Windows.Forms.ToolStripStatusLabel ServerStatusMessage;
        private System.Windows.Forms.TextBox Logger;
    }
}