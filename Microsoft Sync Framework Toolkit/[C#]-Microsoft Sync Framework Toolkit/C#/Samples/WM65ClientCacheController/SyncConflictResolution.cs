// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    /// <summary>
    /// Represents the resolution that the server employed to resolve a conflict.
    /// </summary>
    public enum SyncConflictResolution
    {
        /// <summary>
        /// Client version was ignored.
        /// </summary>
        ServerWins,

        /// <summary>
        /// Server version was ignored.
        /// </summary>
        ClientWins,

        /// <summary>
        /// Changes from both server and client version were merged
        /// </summary>
        Merge
    }
}
