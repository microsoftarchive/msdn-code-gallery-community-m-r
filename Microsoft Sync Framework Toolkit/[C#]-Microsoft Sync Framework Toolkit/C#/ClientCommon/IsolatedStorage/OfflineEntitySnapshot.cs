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

namespace Microsoft.Synchronization.ClientServices.IsolatedStorage
{
    /// <summary>
    /// Class which stores the snapshot of an entity.
    /// </summary>
    internal class OfflineEntitySnapshot
    {
        public OfflineEntitySnapshot()
        {
            this._properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Tick count of the snapshot
        /// </summary>
        public ulong TickCount
        {
            get; set;
        }

        /// <summary>
        /// State of the entity for which this is a snapshot
        /// </summary>
        public OfflineEntityState EntityState
        {
            get; set; 
        }

        /// <summary>
        /// Whether or not the snapshotted entity is a tombstone.
        /// </summary>
        public bool IsTombstone
        {
            get; set;
        }

        /// <summary>
        /// Mapping of property names to values representing the properties of the entity.
        /// </summary>
        public IDictionary<string, object> Properties
        {
            get
            {
                return _properties;
            }
        }

        /// <summary>
        /// Copy of the metadata the entity had before a snapshot was created.  This is important
        /// in the case of rolling back tombstones.
        /// </summary>
        public OfflineEntityMetadata Metadata
        {
            get;
            set;
        }

        /// <summary>
        /// Mapping of property names to values generated when a snapshot is created.
        /// </summary>
        Dictionary<string, object> _properties;
    }
}
