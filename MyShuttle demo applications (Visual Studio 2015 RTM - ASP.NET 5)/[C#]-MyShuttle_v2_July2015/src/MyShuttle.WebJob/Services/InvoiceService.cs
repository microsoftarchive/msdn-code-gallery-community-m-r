


namespace MyShuttle.WebJob.Services
{
    using iTextSharp.text.pdf;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class InvoiceService
    {
        string _InvoiceFormUrl = ConfigurationManager.AppSettings["pdf::invoiceform"];

        public InvoiceService()
        {
            _InvoiceFormUrl = ConfigurationManager.AppSettings["pdf::invoiceform"];
        }

        public async Task<byte[]> Create(Invoice invoice)
        {
            var formBytes = await GetInvoiceFormAsync();
            byte[] output = null;

            using (var rdr = new PdfReader(formBytes))
            {
                var outputStream = new MemoryStream();
                using (var stamper = new PdfStamper(rdr, outputStream))
                {
                    stamper.FormFlattening = true;
                    stamper.AcroFields.SetField("StartDate", invoice.StartDateTime.ToString("yyyy-MM-dd"));
                    stamper.AcroFields.SetField("EndAddress", invoice.EndAddress);
                    stamper.AcroFields.SetField("StartAddress", invoice.StartAddress);
                    stamper.AcroFields.SetField("Cost", invoice.Cost.ToString());
                    stamper.AcroFields.SetField("Distance", String.Format("{0} miles", invoice.Distance.ToString()));
                    stamper.AcroFields.SetField("Duration", String.Format("{0}''", invoice.Duration.ToString()));
                    stamper.AcroFields.SetField("EmployeeName", invoice.EmployeeName);
                    stamper.AcroFields.SetField("SignatureName", invoice.EmployeeName);
                    stamper.AcroFields.SetField("StartTime", invoice.StartDateTime.ToString("HH:mm:ss tt"));
                    stamper.AcroFields.SetField("Signature", Convert.ToBase64String(invoice.Signature));
                }
                output = (Byte[])outputStream.ToArray();
            }

            return output;
        }

       
        async Task<byte[]> GetInvoiceFormAsync()
        {
            var httpClient = new HttpClient();
            var form = await httpClient.GetAsync(_InvoiceFormUrl);
            return await form.Content.ReadAsByteArrayAsync();
        }

    }
}
