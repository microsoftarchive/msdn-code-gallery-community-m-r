namespace rotate
{
    partial class Form1
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
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnRotateDown = new System.Windows.Forms.Button();
            this.btnRotateRight = new System.Windows.Forms.Button();
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDefault
            // 
            this.btnDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefault.Location = new System.Drawing.Point(133, 12);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(98, 63);
            this.btnDefault.TabIndex = 0;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnRotateDown
            // 
            this.btnRotateDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.btnRotateDown.Location = new System.Drawing.Point(133, 143);
            this.btnRotateDown.Name = "btnRotateDown";
            this.btnRotateDown.Size = new System.Drawing.Size(98, 63);
            this.btnRotateDown.TabIndex = 1;
            this.btnRotateDown.Text = "Rotate Down";
            this.btnRotateDown.UseVisualStyleBackColor = true;
            this.btnRotateDown.Click += new System.EventHandler(this.btnRotateDown_Click);
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.btnRotateRight.Location = new System.Drawing.Point(234, 76);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(98, 63);
            this.btnRotateRight.TabIndex = 2;
            this.btnRotateRight.Text = "Rotate Right";
            this.btnRotateRight.UseVisualStyleBackColor = true;
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.btnRotateLeft.Location = new System.Drawing.Point(31, 74);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(98, 63);
            this.btnRotateLeft.TabIndex = 3;
            this.btnRotateLeft.Text = "Rotate Left";
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "you can also press\r\nCTRL + ALT + AEROKEYS [UP, DOWN, RIGHT, LEFT]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 312);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRotateLeft);
            this.Controls.Add(this.btnRotateRight);
            this.Controls.Add(this.btnRotateDown);
            this.Controls.Add(this.btnDefault);
            this.Name = "Form1";
            this.Text = "Windows Screen Rotate / Tilt";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnRotateDown;
        private System.Windows.Forms.Button btnRotateRight;
        private System.Windows.Forms.Button btnRotateLeft;
        private System.Windows.Forms.Label label1;
    }
}

