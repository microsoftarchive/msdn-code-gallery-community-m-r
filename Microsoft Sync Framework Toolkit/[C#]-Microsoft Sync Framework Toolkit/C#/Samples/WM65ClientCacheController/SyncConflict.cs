// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    /// <summary>
    /// Represents a Synchronization related Conflict that was raised and handled on the server.
    /// </summary>
    public class SyncConflict : Conflict
    {
        /// <summary>
        /// This represents the version of the Entity that lost in the conflict resolution.
        /// </summary>
        public IOfflineEntity LosingEntity { get; set; }

        public SyncConflictResolution Resolution { get; set;}
    }
}
