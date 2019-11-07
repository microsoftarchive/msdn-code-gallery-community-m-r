using System;
using System.Windows.Forms;

namespace NumerictextBoxSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numberFormatcmb.SelectedIndex = 3;
        }


        private void numberFormatcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericalTextBox.NumberFormat = (NumberFormat)numberFormatcmb.SelectedIndex;
        }

        private void MinimumCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            numericalTextBox.MinCheck = MinimumCheckBox.Checked;
            MinimumTextBox.Enabled = MinimumCheckBox.Checked;
        }

        private void MaximumCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            numericalTextBox.MaxCheck = MaximumCheckBox.Checked;
            MaximumTextBox.Enabled = MaximumCheckBox.Checked;

        }

        private void GroupSeparatorCharacterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (GroupSeparatorCharacterTextBox.Text != "")
            {
                numericalTextBox.Groupsep = System.Convert.ToChar(GroupSeparatorCharacterTextBox.Text[0]);
                GroupSeparatorCharacterTextBox.Text = numericalTextBox.Groupsep.ToString();
            }
        }

        private void MinimumTextBox_TextChanged(object sender, EventArgs e)
        {
            numericalTextBox.MinValue = MinimumTextBox.NumericValue;
        }

        private void MaximumTextBox_TextChanged(object sender, EventArgs e)
        {
            numericalTextBox.MaxValue = MaximumTextBox.NumericValue;
        }

        private void DecimalNumberNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            numericalTextBox.DecimalNumber = (int)DecimalNumberNumericUpDown.Value;
        }

        private void useSeperator_CheckedChanged(object sender, EventArgs e)
        {
            numericalTextBox.Usegroupseparator = useSeperator.Checked;
        }

        private void numericalTextBox_TextChanged(object sender, EventArgs e)
        {
            string s = numericalTextBox.NumericValue.ToString();
            TextTextBox.Text = s + " Text";
            DoubleTextBox.Text = (numericalTextBox.NumericValue + 3).ToString();
            ConditionTextBox.Text = (numericalTextBox.NumericValue < 100) ? numericalTextBox.NumericValue.ToString() : "Over 100";
        }
    }
}
