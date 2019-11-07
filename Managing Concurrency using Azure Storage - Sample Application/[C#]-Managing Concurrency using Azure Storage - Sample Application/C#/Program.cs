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

namespace ConcurrencySample
{
    /// <summary>
    /// Concurrency samples to accompany the Microsoft Azure Concurrency Blog Post (here: http://inserturlhere) 
    /// Update app.config file to include your storage account information 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BlobSample blobSample = new BlobSample();
            // Demo 1 - Demonstrate optimistic concurrency using a blob 
            blobSample.DemonstrateOptimisticConcurrencyUsingBlob();
            
            // Demo 2 - Demonstrate pessimistic concurrency using a blob 
            blobSample.DemonstratePessimisticConcurrencyUsingBlob();

            TableSample tableSample = new TableSample();
            // Demo 3 - Demonstrate optimistic concurrency using an entity 
            tableSample.DemonstratePessimisticConcurrencyUsingEntity();
            
            // Pause to look at output
            Console.ReadLine(); 

        }
    }
}
