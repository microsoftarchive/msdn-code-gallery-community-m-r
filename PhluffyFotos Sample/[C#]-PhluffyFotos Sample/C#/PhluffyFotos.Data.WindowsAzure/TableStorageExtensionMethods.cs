namespace Microsoft.WindowsAzure.StorageClient
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    /// <summary>
    /// Development Storage currently requires the schema for an entity stored in a table to have been 
    /// previously defined before you are allowed to query it. To provide the schema, the methods in this 
    /// class create a new table, store a new entity with the required schema and then delete it. 
    /// This workaround is applied only for Development Storage, Azure Storage does not have this restriction.
    ///
    /// NOTE: These methods assume that entities derive from TableServiceEntity.
    /// </summary>
    public static class TableStorageExtensionMethods
    {
        /// <summary>
        /// Creates a new table with the provided schema.
        /// </summary>
        /// <typeparam name="T">A TableServiceEntity derived class with the required schema.</typeparam>
        /// <param name="tableStorage">The table storage client instance.</param>
        /// <param name="entityName">The name of the table.</param>
        /// <returns>true if the table was created, false if the table already existed.</returns>
        /// <remarks>Use this method instead of the non-generic variant in StorageClient.</remarks>
        public static bool CreateTableIfNotExist<T>(this CloudTableClient tableStorage, string entityName)
            where T : TableServiceEntity, new()
        {
            bool result = tableStorage.CreateTableIfNotExist(entityName);

            // Execute conditionally for development storage only
            if (tableStorage.BaseUri.IsLoopback)
            {
                InitializeTableSchemaFromEntity(tableStorage, entityName, new T());
            }

            return result;
        }

        /// <summary>
        /// Creates tables from a DataServiceContext derived class. 
        /// </summary>
        /// <param name="serviceContextType">A DataServiceContext derived class that defines the table schemas.</param>
        /// <param name="baseAddress">The baseAddress of the table storage endpoint.</param>
        /// <param name="credentials">Storage account credentials.</param>
        /// <remarks>
        /// For each table, the class exposes one or more IQueryable&lt;T&gt; properties, where T is a 
        /// TableServiceEntity derived class with the required schema.
        /// </remarks>
        public static void CreateTablesFromModel(Type serviceContextType, string baseAddress, StorageCredentials credentials)
        {
            CloudTableClient.CreateTablesFromModel(serviceContextType, baseAddress, credentials);

            CloudTableClient tableStorage = new CloudTableClient(baseAddress, credentials);

            // Execute conditionally for development storage only
            if (tableStorage.BaseUri.IsLoopback)
            {
                TableServiceContext ctx = tableStorage.GetDataServiceContext();

                var properties = serviceContextType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (var table in properties.Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(IQueryable<>)))
                {
                    TableServiceEntity entity = Activator.CreateInstance(table.PropertyType.GetGenericArguments()[0]) as TableServiceEntity;
                    if (entity != null)
                    {
                        InitializeTableSchemaFromEntity(tableStorage, table.Name, entity);
                    }
                }
            }
        }

        private static void InitializeTableSchemaFromEntity(CloudTableClient tableStorage, string entityName, TableServiceEntity entity)
        {
            TableServiceContext context = tableStorage.GetDataServiceContext();
            DateTime now = DateTime.UtcNow;
            entity.PartitionKey = Guid.NewGuid().ToString();
            entity.RowKey = Guid.NewGuid().ToString();
            Array.ForEach(
                entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance), 
                p =>
                {
                    if ((p.Name != "PartitionKey") && (p.Name != "RowKey") && (p.Name != "Timestamp"))
                    {
                        if (p.PropertyType == typeof(string))
                        {
                            p.SetValue(entity, Guid.NewGuid().ToString(), null);
                        }
                        else if (p.PropertyType == typeof(DateTime))
                        {
                            p.SetValue(entity, now, null);
                        }
                    }
                });

            context.AddObject(entityName, entity);
            context.SaveChangesWithRetries();
            context.DeleteObject(entity);
            context.SaveChangesWithRetries();
        }
    }
}
