using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Printing;
using System.Drawing;

namespace WebPrint
{
    public class PrinterHandler
    {
        string TextToPrint { get; set; }

        public bool DoPrintDocument(string printerName, string tray, string textToPrint)
        {
            PrintDocument _printDoc = new PrintDocument();
            _printDoc.PrinterSettings.PrinterName = printerName;
            _printDoc.DefaultPageSettings.Margins = new Margins(20, 20, 20, 20);

            this.TextToPrint = textToPrint; 

            //Set paper source
            PaperSource _paperSource = new PaperSource();
            _paperSource.SourceName = tray;
            _paperSource.RawKind = 260;
            _printDoc.DefaultPageSettings.PaperSource = _paperSource;

            try
            {
                _printDoc.PrintPage += new PrintPageEventHandler(PrintPage); 
                _printDoc.Print();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.ToString());
                return false; 
            }
        }
        void PrintPage(object sender, PrintPageEventArgs e)
        {           
            //Collect printing lines for long text 
            List<string> _printingLines = new List<string>();
            while(TextToPrint.Length > 120)
            {
                _printingLines.Add(TextToPrint.Substring(0, 120));
                TextToPrint = TextToPrint.Substring(120, TextToPrint.Length - 120); 
            }
            //Add last line
            _printingLines.Add(TextToPrint); 

            //Setup lines
            for(int i=0;i<_printingLines.Count;i++)
            {
                e.Graphics.DrawString(_printingLines[i], new Font("Arial", 12.0f, FontStyle.Regular), Brushes.Black, 20, (i * 20) + 20);  
            }
            e.Graphics.Dispose(); 
        }



    }
}