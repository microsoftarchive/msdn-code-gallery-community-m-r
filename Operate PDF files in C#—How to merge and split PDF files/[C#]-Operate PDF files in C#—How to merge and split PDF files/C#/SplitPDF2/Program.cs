using System;
using System.Collections.Generic;
using System.Linq;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;

namespace SplitPDF2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create an object of PdfDocument class and load the sample from file
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile("TEST_2.pdf");

            //Create a new document PDF1 and initialize a new instance of PdfPageBase class
            PdfDocument pdf1 = new PdfDocument();
            PdfPageBase page;

            /*Add new page to pdf1 based on the original page size and the specified margins,
            draw the original page element to the new page using Draw() method. 
            Use for loop to select pages(from 1 to 2) that you want to be divided.*/
            for (int i = 0; i < 2; i++)
            {                
                page = pdf1.Pages.Add(pdf.Pages[i].Size, new PdfMargins(0));
                pdf.Pages[i].CreateTemplate().Draw(page, new PointF(0, 0));
            }
            //Save the file 
            pdf1.SaveToFile("1-2.pdf");


            //Repeat the last step, split the document from page 3 to 5 and save   
            PdfDocument pdf2 = new PdfDocument();
            for (int i = 2; i < 5; i++)
            {
                page = pdf2.Pages.Add(pdf.Pages[i].Size, new PdfMargins(0));
                pdf.Pages[i].CreateTemplate().Draw(page, new PointF(0, 0));
            }
            pdf2.SaveToFile("3-5.pdf");


            //Split the docuemnt (from page 6 to 7) and save  
            PdfDocument pdf3 = new PdfDocument();
            for (int i = 5; i < 7; i++)
            {
                page = pdf3.Pages.Add(pdf.Pages[i].Size, new PdfMargins(0));
                pdf.Pages[i].CreateTemplate().Draw(page, new PointF(0, 0));
            }
            pdf3.SaveToFile("6-7.pdf");
        }
    }
}
