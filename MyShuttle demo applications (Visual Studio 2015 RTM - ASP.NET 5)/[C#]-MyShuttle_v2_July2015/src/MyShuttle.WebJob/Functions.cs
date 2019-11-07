

namespace MyShuttle.WebJob
{
    using System.IO;
    using Microsoft.Azure.WebJobs;
    using Services;
    using System;
    using Services.SharePoint;

    public class Functions
    {
        public static async void ProcessQueueMessage([QueueTrigger("%invoicequeuename%")] Invoice invoice, TextWriter log)
        {
            log.WriteLine(String.Format("Generate new invoice for {0}", invoice.EmployeeName));

            var invoiceService = new InvoiceService();
            var file = await invoiceService.Create(invoice);
            if (file != null)
            {
                SharePointUploader.UploadFile(file, String.Format("invoice_{0}.pdf", invoice.RideId));
            }

        }

    }
}
