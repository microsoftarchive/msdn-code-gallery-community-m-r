#region license
// Copyright (c) Microsoft Corporation
// All rights reserved. 
// Licensed under the Apache License, Version 2.0 (the License); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 
// THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, 
// INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 
// See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;

namespace ConcurrencySample
{
    public class CustomerEntity : TableEntity
    {
        public CustomerEntity() { }
        public CustomerEntity(string lastName, string firstName)
        {
            this.PartitionKey = lastName;
            this.RowKey = firstName;
        }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    /// <summary>
    /// Sample to demonstrate optimistic locking using Table storage
    /// </summary>
    class TableSample
    {
        const string TABLENAME = "TableSample"; 
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        CloudTableClient tableClient;
        CloudTable customerTable;

        public TableSample()
        {
            tableClient = storageAccount.CreateCloudTableClient();
            customerTable = tableClient.GetTableReference(TABLENAME);
            customerTable.CreateIfNotExists();
        }

        public void DemonstratePessimisticConcurrencyUsingEntity()
        {
            Console.WriteLine("Demo - Demonstrate pessimistic concurrency using entity");
            
            // Add new entity to table
            CustomerEntity orignalCustomer = new CustomerEntity("Schulze", "Sammy")
            {
                Email = "orignalEmail@contoso.org",
                PhoneNumber = "867-5309",
            };

            TableOperation insert = TableOperation.InsertOrReplace(orignalCustomer);
            customerTable.Execute(insert);
            Console.WriteLine("Entity added. Orignal ETag = {0}", orignalCustomer.ETag);

            // Simulate third party updating table
            CustomerEntity updatedCustomer = new CustomerEntity("Schulze", "Sammy")
            {
                Email = "updatedEmail@contoso.org",
                PhoneNumber = "123-4567",
            };
            insert = TableOperation.InsertOrReplace(updatedCustomer);
            customerTable.Execute(insert);
            Console.WriteLine("Entity updated. Updated ETag = {0}", updatedCustomer.ETag);

            // Try updating orignalCustomer. Etag is cached within orignalCustomer and passed by default
            orignalCustomer.Email = "orignalEmailUpdated@contoso.org";
            orignalCustomer.PhoneNumber = "987-6543";
            insert = TableOperation.Merge(orignalCustomer);
            try 
	        {
                Console.WriteLine("Trying to update orignal entity");
                customerTable.Execute(insert);
	        }
	        catch (StorageException ex)
	        {
                if (ex.RequestInformation.HttpStatusCode == (int)HttpStatusCode.PreconditionFailed)
                {
                    Console.WriteLine("Precondition failure as expected. Entities orignal etag does not match");
                    // TODO: client can decide on how it wants to handle the 3rd party updated content.
                }
                else
                {
                    throw;
                }
	        }

            Console.WriteLine();
        }
    }
}
