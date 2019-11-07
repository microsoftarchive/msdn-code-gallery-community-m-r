// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Net;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    public class SyncError : Conflict
    {
        /// <summary>
        /// Represents a copy of the Client Entity that raised the error on the server.
        /// </summary>
        public IOfflineEntity ErrorEntity { get; set; }

        /// <summary>
        /// The description as sent by the sync service explaining the reason for the error.
        /// </summary>
        public string Description { get; internal set; }
    }
}
