using System;
using Spire.Pdf;


namespace MergePDF
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize an array,and the elements are the PDF documents that you want to merge
            String[] files = new String[] { "sample1.pdf", "sample2.pdf", "sample3.pdf" };

            //Call MergeFiles() method to merge the files
            PdfDocumentBase doc = PdfDocument.MergeFiles(files);

            //Save the file
            doc.Save("Merged.pdf", FileFormat.PDF);
        }
    }
}
