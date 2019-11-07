// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Net;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    /// <summary>
    /// Represents a Conflict base type. 
    /// </summary>
    public class Conflict
    {
        /// <summary>
        /// Represents the current live version that is stored on the server. The version when applied on
        /// the local provider will ensure data convergence between server and client for this particular
        /// entity.
        /// </summary>
        public IOfflineEntity LiveEntity { get; internal set; }
    }
}
