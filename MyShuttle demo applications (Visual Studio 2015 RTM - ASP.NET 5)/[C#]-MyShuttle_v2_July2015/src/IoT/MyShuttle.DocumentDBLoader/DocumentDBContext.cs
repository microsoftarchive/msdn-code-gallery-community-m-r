using System.IO;
using System.Runtime.InteropServices;
using MyShuttle.DocumentDBLoader.Model;

namespace MyShuttle.DocumentDBLoader
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;
    using System.Configuration;
    using System.Collections.Generic;

    public class DocumentDBContext
    {
        private const int MaxClassificationsPerDriver = 100;
        private DocumentClient client;
        private readonly string databaseId = ConfigurationManager.AppSettings["DocumentDb:DatabaseId"];
        private readonly string collectionId = "DrivingStyles";
        private readonly string endpointUrl = ConfigurationManager.AppSettings["DocumentDb:EndpointUrl"];
        private readonly string authorizationKey = ConfigurationManager.AppSettings["DocumentDb:AccessKey"];
        private string documentCollectionDocumentsLink;

        public DocumentDBContext()
        {
            try
            {
                client = new DocumentClient(new Uri(endpointUrl), authorizationKey);
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("Status code {0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
        }

        public async Task<Database> RetrieveOrCreateDatabaseAsync()
        {
            // Try to retrieve the database           
            var database = client.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();

            // If the previous call didn't return a Database, it is necessary to create it
            if (database == null)
            {
                database = await client.CreateDatabaseAsync(new Database { Id = databaseId });
                Console.WriteLine("Created Database: id - {0} and selfLink - {1}", database.Id, database.SelfLink);
            }

            return database;
        }

        public async Task<DocumentCollection> RetrieveOrCreateCollectionAsync(string databaseSelfLink)
        {
            // Try to retrieve the collection
            var collection = client.CreateDocumentCollectionQuery(databaseSelfLink).Where(c => c.Id == collectionId).ToArray().FirstOrDefault();

            // If the previous call didn't return a Collection, it is necessary to create it
            if (collection == null)
            {
                collection = await client.CreateDocumentCollectionAsync(databaseSelfLink, new DocumentCollection { Id = collectionId });
            }

            documentCollectionDocumentsLink = collection.DocumentsLink;

            return collection;
        }

        public void AddOrUpdateRange(IEnumerable<DriverClassification> collection)
        {
            foreach (var item in collection)
            {
                AddOrUpdateDocument(item);
            }
        }

        public async void AddOrUpdateDocument(DriverClassification driverClassification)
        {
            var driverStyle = Get(driverClassification.DriverId);

            if (driverStyle == null)
            {
                var newDriverStyle = DriverStyle.CreateFromDriverClassification(driverClassification);
                await client.CreateDocumentAsync(documentCollectionDocumentsLink, newDriverStyle);
            }
            else
            {
                DriverStyle driverStyleToUpdate = driverStyle;

                var classifications = driverStyleToUpdate.Classifications
                    .OrderByDescending(x => x.Timestamp)
                    .Take(MaxClassificationsPerDriver - 1)
                    .ToList();

                classifications.Insert(0, Classification.CreateFromDriverClassification(driverClassification));

                driverStyleToUpdate.ClassificationAvg = Convert.ToInt32(classifications.Average(x => x.Value));
                driverStyleToUpdate.Classifications = classifications.ToArray();

                await client.ReplaceDocumentAsync(driverStyle.SelfLink, driverStyleToUpdate);
            }
        }

        private dynamic Get(int driverId)
        {
            return client.CreateDocumentQuery<Document>(documentCollectionDocumentsLink,
                new SqlQuerySpec
                {
                    QueryText = "SELECT * FROM d WHERE (d.DriverId = @id)",
                    Parameters = new SqlParameterCollection()
                    {
                        new SqlParameter("@id", driverId)
                    }
                })
                .AsEnumerable()
                .FirstOrDefault();
        }
    }
}
