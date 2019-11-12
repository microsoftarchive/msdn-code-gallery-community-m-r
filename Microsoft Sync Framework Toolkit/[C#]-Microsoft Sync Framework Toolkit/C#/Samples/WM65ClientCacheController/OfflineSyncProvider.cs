// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Samples.Synchronization.ClientServices
{
    public abstract class OfflineSyncProvider
    {
        public abstract void BeginSession();

        public abstract ChangeSet GetChangeSet(Guid state);

        public abstract void OnChangeSetUploaded(Guid state, ChangeSetResponse response);

        public abstract byte[] GetServerBlob();

        public abstract void SaveChangeSet(ChangeSet changeSet);

        public abstract void EndSession();
    }
}
