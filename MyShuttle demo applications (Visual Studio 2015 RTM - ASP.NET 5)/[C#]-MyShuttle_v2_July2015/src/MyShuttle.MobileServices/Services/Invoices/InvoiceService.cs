
namespace MyShuttle.MobileServices.Services.Invoices
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Microsoft.WindowsAzure.Storage.RetryPolicies;
    using Newtonsoft.Json;
    using System;
    using System.Configuration;
    using System.Threading.Tasks;

    public static class InvoiceService
    {
        public async static Task CreateInvoice(Invoice invoice)
        {
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["AzureWebJobsStorage"].ToString());
            string queueName = ConfigurationManager.AppSettings["InvoiceQueueName"].ToString();

            var queueClient = storageAccount.CreateCloudQueueClient();
            queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);
            var invoiceQueue = queueClient.GetQueueReference(queueName);
            invoiceQueue.CreateIfNotExists();

            // Create Queue Message and Submit to Azure Queue
            await invoiceQueue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(invoice)));
        }
    }
}
