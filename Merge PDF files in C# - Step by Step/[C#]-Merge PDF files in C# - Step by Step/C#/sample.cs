using System;
using System.IO;
using System.Collections;


namespace Sample
{
    class Test
    {
        static void Main(string[] args)
        {
            // Merge PDF files		
            SautinSoft.PdfVision v = new SautinSoft.PdfVision();
           
            //merge 3 PDF files
            string[] files = new string[3];
            files[0] = @"d:\Tempos\image1.pdf";
            files[1] = @"d:\Tempos\image2.pdf";
            files[2] = @"d:\Tempos\image3.pdf";

            // You may download the latest version of SDK here:   
            // www.sautinsoft.com/products/convert-html-pdf-and-tiff-pdf-asp.net/download.php 
            int ret = v.MergePDFFileArrayToPDFFile(files, @"D:\Tempos\hardcopy.pdf");

            //0 - merged successfully
            //1 - error, can't merge PDF documents
            //2 - error, can't create output file, probably it used by another application
            //3 - merging failed
            //4 - merged successfully, but some files were not merged
            
        }
    }
}
