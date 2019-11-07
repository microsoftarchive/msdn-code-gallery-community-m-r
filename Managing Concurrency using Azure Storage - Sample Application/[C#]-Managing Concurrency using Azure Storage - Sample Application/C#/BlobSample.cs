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
using System.IO; 
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Net;

namespace ConcurrencySample
{
    /// <summary>
    /// Sample to demonstrate optimistic and pessimistic locking using Blob storage
    /// </summary>
    public class BlobSample
    {
        const string BLOBNAME = "helloworld.txt";
        const string CONTAINERNAME = "blobsample"; 
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        CloudBlobClient blobClient;
        CloudBlobContainer container; 
        public BlobSample()
        {
            blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(CONTAINERNAME);
            container.CreateIfNotExists();
        }

        public void DemonstrateOptimisticConcurrencyUsingBlob()
        {
            Console.WriteLine("Demo - Demonstrate optimistic concurrency using a blob");

            // Create test blob - default strategy is last writer wins - so UploadText will overwrite existing blob if present
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(BLOBNAME);
            blockBlob.UploadText("Hello World!");

            // Retrieve the ETag from the newly created blob
            // Etag is already populated as UploadText should cause a PUT Blob call to storage blob service which returns the etag in response.
            string orignalETag = blockBlob.Properties.ETag;
            Console.WriteLine("Blob added. Orignal ETag = {0}", orignalETag);

            /// This code simulates an update by a third party.
            string helloText = "Blob updated by a third party.";
            // No etag, provided so orignal blob is overwritten (thus generating a new etag)
            blockBlob.UploadText(helloText);
            Console.WriteLine("Blob updated. Updated ETag = {0}", blockBlob.Properties.ETag);

            // npw try to update the blob using the orignal ETag provided when the blob was created
            try
            {
                Console.WriteLine("Trying to update blob using orignal etag to generate if-match access condition");
                blockBlob.UploadText(helloText, accessCondition: AccessCondition.GenerateIfMatchCondition(orignalETag));
            }
            catch (StorageException ex)
            {
                if (ex.RequestInformation.HttpStatusCode == (int)HttpStatusCode.PreconditionFailed)
                {
                    Console.WriteLine("Precondition failure as expected. Blob's orignal etag no longer matches");
                    // TODO: client can decide on how it wants to handle the 3rd party updated content.
                }
                else
                {
                    throw;
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// This code demonstrates how to take a lease on a blob and simulate a third party attempting to update the blob
        /// </summary>
        public void DemonstratePessimisticConcurrencyUsingBlob()
        {
            Console.WriteLine("Demo - Demonstrate pessimistic concurrency using a blob");
            CloudBlockBlob blockBlob= container.GetBlockBlobReference(BLOBNAME);
            blockBlob.UploadText("Hello World!");
            Console.WriteLine("Blob added.");
            
            // Acquire lease for 15 seconds
            string lease = blockBlob.AcquireLease(TimeSpan.FromSeconds(15), null);
            Console.WriteLine("Blob lease acquired. Lease = {0}", lease);
    
            // Update blob using lease. This operation will succeed
            const string helloText = "Blob updated";
            var accessCondition = AccessCondition.GenerateLeaseCondition(lease);
            blockBlob.UploadText(helloText, accessCondition: accessCondition);
            Console.WriteLine("Blob updated using an exclusive lease");

            //Simulate third party update to blob without lease
            try
            {
                // Below operation will fail as no valid lease provided
                Console.WriteLine("Trying to update blob without valid lease");
                blockBlob.UploadText("Update without lease, will fail");
            }
            catch (StorageException ex)
            {
                if (ex.RequestInformation.HttpStatusCode == (int)HttpStatusCode.PreconditionFailed)
                {
                    Console.WriteLine("Precondition failure as expected. Blob's lease does not match");
                }
                else
                {
                    throw;
                }
            }

            // Releasee lease proatively. Lease expires anyways after 15 seconds
            blockBlob.ReleaseLease(accessCondition);
            Console.WriteLine();
        }
    }
}
