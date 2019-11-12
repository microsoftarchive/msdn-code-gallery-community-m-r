using NumericalTextBoxes;
namespace NumerictextBoxSample
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
            this.numberFormatcmb = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DecimalNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.MaximumCheckBox = new System.Windows.Forms.CheckBox();
            this.MinimumCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.GroupSeparatorCharacterTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ConditionTextBox = new System.Windows.Forms.TextBox();
            this.DoubleTextBox = new System.Windows.Forms.TextBox();
            this.TextTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.useSeperator = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.MaximumTextBox = new NumericalTextBoxes.NumericalTextBox();
            this.MinimumTextBox = new NumericalTextBoxes.NumericalTextBox();
            this.numericalTextBox = new NumericalTextBoxes.NumericalTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DecimalNumberNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // numberFormatcmb
            // 
            this.numberFormatcmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.numberFormatcmb.FormattingEnabled = true;
            this.numberFormatcmb.Items.AddRange(new object[] {
            "Unsigned Integer",
            "Signed Integer",
            "Decimal Value",
            "Float Value"});
            this.numberFormatcmb.Location = new System.Drawing.Point(159, 31);
            this.numberFormatcmb.Name = "numberFormatcmb";
            this.numberFormatcmb.Size = new System.Drawing.Size(114, 21);
            this.numberFormatcmb.TabIndex = 3;
            this.numberFormatcmb.SelectedIndexChanged += new System.EventHandler(this.numberFormatcmb_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Decimal Digit Count";
            // 
            // DecimalNumberNumericUpDown
            // 
            this.DecimalNumberNumericUpDown.BackColor = System.Drawing.Color.White;
            this.DecimalNumberNumericUpDown.Location = new System.Drawing.Point(159, 5);
            this.DecimalNumberNumericUpDown.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.DecimalNumberNumericUpDown.Name = "DecimalNumberNumericUpDown";
            this.DecimalNumberNumericUpDown.ReadOnly = true;
            this.DecimalNumberNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.DecimalNumberNumericUpDown.TabIndex = 5;
            this.DecimalNumberNumericUpDown.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.DecimalNumberNumericUpDown.ValueChanged += new System.EventHandler(this.DecimalNumberNumericUpDown_ValueChanged);
            // 
            // MaximumCheckBox
            // 
            this.MaximumCheckBox.AutoSize = true;
            this.MaximumCheckBox.Location = new System.Drawing.Point(15, 91);
            this.MaximumCheckBox.Name = "MaximumCheckBox";
            this.MaximumCheckBox.Size = new System.Drawing.Size(100, 17);
            this.MaximumCheckBox.TabIndex = 11;
            this.MaximumCheckBox.Text = "Maximum Value";
            this.MaximumCheckBox.UseVisualStyleBackColor = true;
            this.MaximumCheckBox.CheckedChanged += new System.EventHandler(this.MaximumCheckBox_CheckedChanged);
            // 
            // MinimumCheckBox
            // 
            this.MinimumCheckBox.AutoSize = true;
            this.MinimumCheckBox.Location = new System.Drawing.Point(15, 65);
            this.MinimumCheckBox.Name = "MinimumCheckBox";
            this.MinimumCheckBox.Size = new System.Drawing.Size(97, 17);
            this.MinimumCheckBox.TabIndex = 9;
            this.MinimumCheckBox.Text = "Minimum Value";
            this.MinimumCheckBox.UseVisualStyleBackColor = true;
            this.MinimumCheckBox.CheckedChanged += new System.EventHandler(this.MinimumCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Group Seperator Character:";
            // 
            // GroupSeparatorCharacterTextBox
            // 
            this.GroupSeparatorCharacterTextBox.Location = new System.Drawing.Point(301, 113);
            this.GroupSeparatorCharacterTextBox.Name = "GroupSeparatorCharacterTextBox";
            this.GroupSeparatorCharacterTextBox.Size = new System.Drawing.Size(28, 20);
            this.GroupSeparatorCharacterTextBox.TabIndex = 14;
            this.GroupSeparatorCharacterTextBox.Text = ".";
            this.GroupSeparatorCharacterTextBox.TextChanged += new System.EventHandler(this.GroupSeparatorCharacterTextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Condition";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Double";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Text";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(279, 252);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Less 100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(279, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "+ 3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(279, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "+ \" Reza\"";
            // 
            // ConditionTextBox
            // 
            this.ConditionTextBox.Location = new System.Drawing.Point(64, 249);
            this.ConditionTextBox.Name = "ConditionTextBox";
            this.ConditionTextBox.ReadOnly = true;
            this.ConditionTextBox.Size = new System.Drawing.Size(209, 20);
            this.ConditionTextBox.TabIndex = 20;
            // 
            // DoubleTextBox
            // 
            this.DoubleTextBox.Location = new System.Drawing.Point(64, 223);
            this.DoubleTextBox.Name = "DoubleTextBox";
            this.DoubleTextBox.ReadOnly = true;
            this.DoubleTextBox.Size = new System.Drawing.Size(209, 20);
            this.DoubleTextBox.TabIndex = 19;
            // 
            // TextTextBox
            // 
            this.TextTextBox.Location = new System.Drawing.Point(64, 197);
            this.TextTextBox.Name = "TextTextBox";
            this.TextTextBox.ReadOnly = true;
            this.TextTextBox.Size = new System.Drawing.Size(209, 20);
            this.TextTextBox.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Number Format";
            // 
            // useSeperator
            // 
            this.useSeperator.AutoSize = true;
            this.useSeperator.Checked = true;
            this.useSeperator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useSeperator.Location = new System.Drawing.Point(12, 116);
            this.useSeperator.Name = "useSeperator";
            this.useSeperator.Size = new System.Drawing.Size(126, 17);
            this.useSeperator.TabIndex = 24;
            this.useSeperator.Text = "Use Group Seperator";
            this.useSeperator.UseVisualStyleBackColor = true;
            this.useSeperator.CheckedChanged += new System.EventHandler(this.useSeperator_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(12, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 17);
            this.label10.TabIndex = 25;
            this.label10.Text = "Enter Number";
            // 
            // MaximumTextBox
            // 
            this.MaximumTextBox.DecimalNumber = 15;
            this.MaximumTextBox.Groupsep = '.';
            this.MaximumTextBox.Location = new System.Drawing.Point(158, 89);
            this.MaximumTextBox.MaxCheck = false;
            this.MaximumTextBox.MaxValue = 0D;
            this.MaximumTextBox.MinCheck = false;
            this.MaximumTextBox.MinValue = 0D;
            this.MaximumTextBox.Name = "MaximumTextBox";
            this.MaximumTextBox.NumberFormat = NumberFormat.FloatValue;
            this.MaximumTextBox.Size = new System.Drawing.Size(100, 20);
            this.MaximumTextBox.TabIndex = 12;
            this.MaximumTextBox.Text = "1e5";
            this.MaximumTextBox.Usegroupseparator = true;
            this.MaximumTextBox.TextChanged += new System.EventHandler(this.MaximumTextBox_TextChanged);
            // 
            // MinimumTextBox
            // 
            this.MinimumTextBox.DecimalNumber = 15;
            this.MinimumTextBox.Groupsep = '.';
            this.MinimumTextBox.Location = new System.Drawing.Point(159, 63);
            this.MinimumTextBox.MaxCheck = false;
            this.MinimumTextBox.MaxValue = 0D;
            this.MinimumTextBox.MinCheck = false;
            this.MinimumTextBox.MinValue = 0D;
            this.MinimumTextBox.Name = "MinimumTextBox";
            this.MinimumTextBox.NumberFormat = NumberFormat.FloatValue;
            this.MinimumTextBox.Size = new System.Drawing.Size(100, 20);
            this.MinimumTextBox.TabIndex = 10;
            this.MinimumTextBox.Text = "-1e-10";
            this.MinimumTextBox.Usegroupseparator = true;
            this.MinimumTextBox.TextChanged += new System.EventHandler(this.MinimumTextBox_TextChanged);
            // 
            // numericalTextBox
            // 
            this.numericalTextBox.DecimalNumber = 15;
            this.numericalTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBox.Groupsep = '.';
            this.numericalTextBox.Location = new System.Drawing.Point(12, 160);
            this.numericalTextBox.MaxCheck = false;
            this.numericalTextBox.MaxValue = 100000D;
            this.numericalTextBox.MinCheck = false;
            this.numericalTextBox.MinValue = -1E-10D;
            this.numericalTextBox.Name = "numericalTextBox";
            this.numericalTextBox.NumberFormat = NumberFormat.FloatValue;
            this.numericalTextBox.Size = new System.Drawing.Size(317, 26);
            this.numericalTextBox.TabIndex = 1;
            this.numericalTextBox.Usegroupseparator = true;
            this.numericalTextBox.TextChanged += new System.EventHandler(this.numericalTextBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 278);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.useSeperator);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ConditionTextBox);
            this.Controls.Add(this.DoubleTextBox);
            this.Controls.Add(this.TextTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MaximumTextBox);
            this.Controls.Add(this.MaximumCheckBox);
            this.Controls.Add(this.MinimumTextBox);
            this.Controls.Add(this.MinimumCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GroupSeparatorCharacterTextBox);
            this.Controls.Add(this.DecimalNumberNumericUpDown);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numberFormatcmb);
            this.Controls.Add(this.numericalTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DecimalNumberNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericalTextBox numericalTextBox;
        private System.Windows.Forms.ComboBox numberFormatcmb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown DecimalNumberNumericUpDown;
        private NumericalTextBox MaximumTextBox;
        private System.Windows.Forms.CheckBox MaximumCheckBox;
        private NumericalTextBox MinimumTextBox;
        private System.Windows.Forms.CheckBox MinimumCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox GroupSeparatorCharacterTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ConditionTextBox;
        private System.Windows.Forms.TextBox DoubleTextBox;
        private System.Windows.Forms.TextBox TextTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox useSeperator;
        private System.Windows.Forms.Label label10;
    }
}

