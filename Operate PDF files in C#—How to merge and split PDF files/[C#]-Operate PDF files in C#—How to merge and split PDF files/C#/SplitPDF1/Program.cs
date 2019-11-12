using System;
using Spire.Pdf;

namespace SplitPDF1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create an object of PdfDocument class and load the sample
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile("TEST_2.pdf");
            //Call the method Split() to split the PDF file one by one
            String pattern = "Split{0}.pdf";
            pdf.Split(pattern);
        }
    }
}
