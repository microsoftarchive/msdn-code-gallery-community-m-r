// Copyright 2010 Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License"); 
// You may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, 
// INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
// CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, 
// MERCHANTABLITY OR NON-INFRINGEMENT. 

// See the Apache 2 License for the specific language governing 
// permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Synchronization.ClientServices
{
    /// <summary>
    /// Denotes a list of changes that is either to be uploaded or downloaded.
    /// </summary>
    public class ChangeSet
    {
        /// <summary>
        /// The Server blob (locally stored for an upload and server version for an Download)
        /// </summary>
        public byte[] ServerBlob { get; set; }

        /// <summary>
        /// An collection of IOfflineEntity objects which depicts the actual data being uploaded or downloaded.
        /// </summary>
        public ICollection<IOfflineEntity> Data { get; set; }

        /// <summary>
        /// Flag depicting whether or not this is a last batch or not.
        /// </summary>
        public bool IsLastBatch { get; set; }

        /// <summary>
        /// Public constructor for ChangeSet object. Instantiates with an empty collection for Data and default values of null for 
        /// serverBlob and true for IsLastBatch
        /// </summary>
        public ChangeSet()
        {
            this.ServerBlob = null;
            this.Data = new List<IOfflineEntity>();
            this.IsLastBatch = true;
        }
        internal void AddItem(IOfflineEntity iOfflineEntity)
        {
            if (Data == null)
            {
                Data = new List<IOfflineEntity>();
            }
            Data.Add(iOfflineEntity);
        }
    }
}
