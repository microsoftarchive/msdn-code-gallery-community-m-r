using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuadraticEquationCsharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            	

				 double a,b,c,D, x1,x2;
				label5.Text ="";
				label6.Text ="";
				label9.Text ="";
	 
				a = System.Convert.ToDouble(textBox1.Text);
				b = System.Convert.ToDouble(textBox2.Text);
				c = System.Convert.ToDouble(textBox3.Text);

                if (textBox1.Text == "0.0") a = 0;
                if (textBox2.Text == "0.0") b = 0;
                if (textBox3.Text == "0.0") c = 0;

            if (a==0) { 
               MessageBox.Show("This is not a Quadratic Equation, enter Factor A at least");
				
                goto next; 
                } 
                 
              
				D=b*b-4*a*c;
				label9.Text =System.Convert.ToString(D);
 
 
                
				
				if(D>=0 ||a!=0 && b!=0 && c!=0)
                {
                    x1 = (-b + Math.Sqrt(D)) / 2 / a;
                    x2 = (-b - Math.Sqrt(D)) / 2 / a;
				label5.Text =System.Convert.ToString(x1);
				label6.Text =System.Convert.ToString(x2);
				}
				else MessageBox.Show("equation has no solution");
				next:;

			 
        }

       
    }
}
