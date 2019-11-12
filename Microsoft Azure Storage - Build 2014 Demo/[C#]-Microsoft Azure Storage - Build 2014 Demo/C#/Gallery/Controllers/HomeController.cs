using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Gallery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var container = GetBlobContainer();
            ViewBag.ContainerPrimaryUri = container.StorageUri.PrimaryUri.AbsoluteUri;
            ViewBag.ContainerSecondaryUri = container.StorageUri.SecondaryUri.AbsoluteUri;
            return View();
        }

        public ActionResult Analytics()
        {
            return View();
        }

        public ActionResult GetRandomBlobUploadUrl(string sasVersion)
        {
            var container = GetBlobContainer();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString("D") + ".jpg");
            var policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Write,
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(15),
            };
            var sas = blob.GetSharedAccessSignature(policy, null, null, sasVersion);
            var credentials = new StorageCredentials(sas);
            var uri = credentials.TransformUri(blob.Uri);
            return Content(uri.AbsoluteUri);
        }

        public ActionResult GetContainerReadToken()
        {
            var container = GetBlobContainer();
            var policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.List,
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(15),
            };
            var sas = container.GetSharedAccessSignature(policy);
            return Content(sas);
        }

        public async Task<ActionResult> GetUserAgentChartAsync()
        {
            var account = GetStorageAccount();
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("$logs");

            var now = DateTime.UtcNow;
            var agents = new SortedDictionary<string, long>();
            var prefix = string.Format("blob/{0:yyyy/MM}/", now);
            BlobContinuationToken token = null;
            do
            {
                var logs = await container.ListBlobsSegmentedAsync(prefix, true, BlobListingDetails.Metadata, null, token, null, null);
                token = logs.ContinuationToken;
                foreach (ICloudBlob log in logs.Results)
                {
                    if (log.Metadata["LogType"].Contains("write"))
                    {
                        await ParseUserAgents(log, agents);
                    }
                }
            }
            while (token != null);

            var chart = new Chart(600, 600);
            chart.AddTitle("User Agents - " + now.ToString("Y"));
            chart.AddSeries(chartType: "Pie", xValue: agents.Keys, yValues: agents.Values);
            var image = chart.GetBytes("jpeg");
            return File(image, "image/jpeg");
        }

        private static async Task ParseUserAgents(ICloudBlob blob, IDictionary<string, long> agents)
        {
            using (var stream = await blob.OpenReadAsync())
            {
                using (var reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        var splitted = SplitLogLine(line);
                        if ((splitted[2] == "PutBlob" || splitted[2] == "PutBlockList") &&
                            splitted[12].Contains(ConfigurationManager.AppSettings["StorageContainerName"] + '/'))
                        {
                            var agent = splitted[27];

                            long value;
                            if (agents.TryGetValue(agent, out value))
                            {
                                value++;
                            }
                            else
                            {
                                value = 1;
                            }

                            agents[agent] = value;
                        }
                    }
                }
            }
        }

        private static string[] SplitLogLine(string line)
        {
            var result = new string[30];
            bool inQuotes = false;
            int current = 0;
            int start = 0;
            for (int i = 0; i < line.Length; i++)
            {
                switch (line[i])
                {
                    case ';':
                        if (!inQuotes)
                        {
                            result[current++] = line.Substring(start, i - start);
                            start = i + 1;
                        }
                        
                        break;

                    case '"':
                        inQuotes = !inQuotes;
                        break;
                }
            }

            if (current != result.Length - 1)
            {
                throw new ArgumentException();
            }

            result[current] = line.Substring(start);
            return result;
        }

        private static CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
        }

        private static CloudBlobContainer GetBlobContainer()
        {
            var account = GetStorageAccount();
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference(ConfigurationManager.AppSettings["StorageContainerName"]);
            return container;
        }
    }
}