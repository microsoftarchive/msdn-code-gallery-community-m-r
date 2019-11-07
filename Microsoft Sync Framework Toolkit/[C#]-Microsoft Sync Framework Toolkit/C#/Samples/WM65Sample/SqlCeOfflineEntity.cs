// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using Microsoft.Samples.Synchronization.ClientServices;

namespace SmartDeviceProject1
{
    /// <summary>
    /// This class is the base entity from which all entities used by the application must inherit.
    /// </summary>
    public abstract class SqlCeOfflineEntity : IOfflineEntity
    {
        protected SqlCeOfflineEntity()
        {
            ServiceMetadata = new OfflineEntityMetadata();
        }

        public OfflineEntityMetadata ServiceMetadata { get; set; }
    }
}