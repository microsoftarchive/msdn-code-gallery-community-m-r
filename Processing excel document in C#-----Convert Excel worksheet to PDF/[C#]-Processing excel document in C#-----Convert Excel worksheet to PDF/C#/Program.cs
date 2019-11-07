using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Xls;

namespace ConvertExcelToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a workbook
            Workbook workbook = new Workbook();

            // load Excel file
            workbook.LoadFromFile(@"..\..\sample2.xlsx");

            //convert to PDF file format
            workbook.ConverterSetting.SheetFitToPage = true;

            //save the file
            workbook.SaveToFile(@"..\..\sample.pdf", FileFormat.PDF);

            System.Diagnostics.Process.Start(@"..\..\sample.pdf");
        }
    }
}
