using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;

namespace MyShuttle.API.Data.DocumentDb
{
    public class DocumentDbContext : IDisposable
    {
        public const string CollectionTrackedRides = "TrackedRides";
        public const string CollectionVehiclesSummary = "VechiclesSummary";
        public const string CollectionDrivingStyles = "DrivingStyles";

        private readonly string _endpointUri;
        private readonly string _authKey;
        private readonly string _dbid;
        private Database _db;
        private readonly Lazy<DocumentClient> _client;

        private bool disposedValue = false;
        private static string[] _collectionNames = new[] { CollectionTrackedRides, CollectionVehiclesSummary };
        public IEnumerable<string> CollectionNames { get { return _collectionNames; } }

        internal DocumentClient Client
        {
            get
            {
                return _client.Value;
            }
        }


        public DocumentDbContext(string uri, string key, string databaseId)
        {
            _client = new Lazy<DocumentClient>(CreateClient);
            _endpointUri = uri;
            _authKey = key;
            _dbid = databaseId;

        }

        public async Task UpdateDocumentAsync<T>(T document, DocumentCollection collection)
        {
            await Client.UpsertDocumentAsync(collection.DocumentsLink, document);
        }


        public async Task DeleteCollectionAsync(string id)
        {
            var db = await RetrieveOrCreateDatabaseAsync();

            var collection = await GetCollectionAsync(id);

            if (collection == null) return;

            await DeleteCollectionAsync(collection);
        }

        public async Task DeleteCollectionAsync(DocumentCollection collection)
        {
            await Client.DeleteDocumentCollectionAsync(collection.SelfLink);
        }

        public async Task<IEnumerable<string>> GetCollectionsAsync()
        {
            var db = await RetrieveOrCreateDatabaseAsync();
            var collection = Client.CreateDocumentCollectionQuery(db.SelfLink).ToList();
            return collection.Select(d => d.Id).ToList();
        }

        public async Task<IEnumerable<string>> CreateAllNeededCollectionsAsync()
        {
            var collections = await GetCollectionsAsync();
            var missingCollections = _collectionNames.Where(cn => !collections.Contains(cn));
            if (!missingCollections.Any())
            {
                return Enumerable.Empty<string>();
            }

            var db = await RetrieveOrCreateDatabaseAsync();

            foreach (var missingCollection in missingCollections)
            {
                await Client.CreateDocumentCollectionAsync(db.SelfLink, new DocumentCollection() { Id = missingCollection });
            }

            return missingCollections;

        }

        public async Task<DocumentCollection> GetCollectionAsync(string id)
        {
            var db = await RetrieveOrCreateDatabaseAsync();
            var collection = Client.CreateDocumentCollectionQuery(db.SelfLink).ToList().FirstOrDefault(c => c.Id == id);
            return collection;
        }

        internal IOrderedQueryable<T> CreateDocumentQuery<T>(DocumentCollection collection)
        {
            return Client.CreateDocumentQuery<T>(collection.DocumentsLink);
        }

        internal IQueryable<T> CreateDocumentQuery<T>(DocumentCollection collection, string sql)
        {
            return Client.CreateDocumentQuery<T>(collection.DocumentsLink, sql);
        }

        internal IQueryable<dynamic> CreateDocumentQueryFromSQL(DocumentCollection collection, string sql)
        {
            return Client.CreateDocumentQuery(collection.DocumentsLink, sql);
        }


        private DocumentClient CreateClient()
        {
            var client = new DocumentClient(new Uri(_endpointUri), _authKey);
            return client;
        }

        public async Task<Database> RetrieveOrCreateDatabaseAsync()
        {
            if (_db != null)
            {
                return _db;
            }

            _db = Client.CreateDatabaseQuery().Where(db => db.Id == _dbid).AsEnumerable().FirstOrDefault();
            if (_db == null)
            {
                _db = await Client.CreateDatabaseAsync(new Database { Id = _dbid });
            }

            return _db;
        }


        public async Task AddRange<T>(string collectionId, IEnumerable<T> items)
        {
            var collection = await GetCollectionAsync(collectionId);
            foreach (var item in items)
            {
                await AddDocument(collection, item);
                await Task.Delay(1000);
                Console.WriteLine("Doc added to collection");
            }
        }

        public async Task AddDocument<T>(string collectionId, T item)
        {
            var collection = await GetCollectionAsync(collectionId);
            await AddDocument(collection, item);
        }

        private async Task AddDocument<T>(DocumentCollection collection, T item)
        {
            await Client.CreateDocumentAsync(collection.DocumentsLink, item);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Client != null)
                    {
                        Client.Dispose();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
