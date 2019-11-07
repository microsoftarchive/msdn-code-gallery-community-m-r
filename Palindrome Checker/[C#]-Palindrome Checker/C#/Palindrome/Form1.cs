using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palindrome
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWord.Text))
            {
                MessageBox.Show("Please enter a word and hit check");
            }
            else
            {
                //Reverse the entered word
                string word = txtWord.Text;
                char[] characters = word.ToCharArray();
                Array.Reverse(characters);                
                word = new string(characters);

                if (word.ToLower() == txtWord.Text.ToLower())
                {
                    MessageBox.Show("The entered word is a palindrome");
                }
                else
                {
                    MessageBox.Show("The entered word is not a palindrome");
                }
            }
        }

        private void btnCheck2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWord.Text))
            {
                MessageBox.Show("Please enter a word and hit check");
            }
            else
            {
                //Reverse the entered word
                string word = txtWord.Text;
                bool flag = false;
                for (int index = 0, jIndex = txtWord.Text.Length - 1; index < word.Length/2; index++, jIndex--)
                {
                    if (txtWord.Text[index] != word[jIndex])
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag)
                {
                    MessageBox.Show("The entered word is not a palindrome");
                }
                else
                {
                    MessageBox.Show("The entered word is a palindrome");
                }
            }
        }
    }
}
