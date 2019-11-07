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
    /// Class that represents the stats for a sync session.
    /// </summary>
    public class CacheRefreshStatistics
    {
        /// <summary>
        /// Start Time of Sync Session
        /// </summary>
        public DateTime StartTime { get; internal set; }

        /// <summary>
        /// End Time of Sync Session
        /// </summary>
        public DateTime EndTime { get; internal set; }

        /// <summary>
        /// Total number of change sets downloaded
        /// </summary>
        public uint TotalChangeSetsDownloaded { get; internal set; }

        /// <summary>
        /// Total number of change sets uploaded
        /// </summary>
        public uint TotalChangeSetsUploaded { get; internal set; }

        /// <summary>
        /// Total number of Uploded Items
        /// </summary>
        public uint TotalUploads { get; internal set; }

        /// <summary>
        /// Total number of downloaded items
        /// </summary>
        public uint TotalDownloads { get; internal set; }

        /// <summary>
        /// Total number of Sync Conflicts
        /// </summary>
        public uint TotalSyncConflicts { get; internal set; }

        /// <summary>
        /// Total number of Sync Conflicts
        /// </summary>
        public uint TotalSyncErrors { get; internal set; }
    }
}
