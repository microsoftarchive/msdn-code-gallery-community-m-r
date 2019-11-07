// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Net;
using System.Collections.Generic;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    /// <summary>
    /// Wrapper Class representing all the related information about an Sync request
    /// </summary>
    class CacheRequest
    {
        public Guid RequestId;
        public ICollection<IOfflineEntity> Changes;
        public CacheRequestType RequestType;
        public byte[] KnowledgeBlob;
        public bool IsLastBatch;
    }
}
