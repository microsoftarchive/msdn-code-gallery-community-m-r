using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System.IO;
using System.Drawing;

using Fonet;
using Fonet.Render.Pdf;


namespace TestPdfConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            xslfoTest();

        }

        static void xslfoTest()
        {
        
            string root = @"C:\Users\Fernando.pires\Documents\Visual Studio 2015\Projects\PdfWriter\Files\Out\";

            if (File.Exists(root + "hello.pdf"))
                File.Delete(root + "hello.pdf");

            FileStream inStm = new FileStream(root + "{6F233C6B-3174-47FC-8A01-80A18DEBAD94}.xml", FileMode.Open);
            FileStream stm = new FileStream(root + "hello.pdf", FileMode.Create);

            FonetDriver driver = FonetDriver.Make();
            //driver.CloseOnExit = false;//important for biztalk to work ... set position = 0

            PdfRendererOptions options = new PdfRendererOptions();
            
            driver.Render(inStm, stm);

            Console.ReadKey();
        }
     
    }
}
