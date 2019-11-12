// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

namespace SmartDeviceProject1
{
    partial class Lists
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.MenuItemNewList = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelSyncTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ListBoxLists = new System.Windows.Forms.ListBox();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Sync";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.MenuItemNewList);
            this.menuItem2.MenuItems.Add(this.menuItem4);
            this.menuItem2.MenuItems.Add(this.menuItem5);
            this.menuItem2.MenuItems.Add(this.menuItem3);
            this.menuItem2.MenuItems.Add(this.menuItem6);
            this.menuItem2.MenuItems.Add(this.menuItemExit);
            this.menuItem2.Text = "Menu";
            // 
            // MenuItemNewList
            // 
            this.MenuItemNewList.Text = "New List";
            this.MenuItemNewList.Click += new System.EventHandler(this.MenuItemNewList_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Text = "Edit";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "Delete";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // LabelStatus
            // 
            this.LabelStatus.Location = new System.Drawing.Point(109, 217);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(128, 20);
            this.LabelStatus.Text = "Offline";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Last Sync at:";
            // 
            // LabelSyncTime
            // 
            this.LabelSyncTime.Location = new System.Drawing.Point(109, 235);
            this.LabelSyncTime.Name = "LabelSyncTime";
            this.LabelSyncTime.Size = new System.Drawing.Size(128, 20);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Status:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 20);
            this.label1.Text = "Lists:";
            // 
            // ListBoxLists
            // 
            this.ListBoxLists.Location = new System.Drawing.Point(3, 35);
            this.ListBoxLists.Name = "ListBoxLists";
            this.ListBoxLists.Size = new System.Drawing.Size(234, 170);
            this.ListBoxLists.TabIndex = 11;
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "View Items";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Text = "Delete Database";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // Lists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LabelSyncTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBoxLists);
            this.Menu = this.mainMenu1;
            this.Name = "Lists";
            this.Text = "Lists";
            this.Load += new System.EventHandler(this.Lists_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem MenuItemNewList;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LabelSyncTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox ListBoxLists;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItem6;
    }
}