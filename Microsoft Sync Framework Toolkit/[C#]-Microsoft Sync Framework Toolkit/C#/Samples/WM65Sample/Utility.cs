// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using DefaultScope;
using Microsoft.Samples.Synchronization.ClientServices;

namespace SmartDeviceProject1
{
    class Utility
    {
        public static void Sync()
        {
            var localProvider = new SqlCeOfflineSyncProvider();
            var controller =
                new CacheController(new Uri(Settings.SyncServiceUrl), Settings.SyncScope, localProvider);

            controller.ControllerBehavior.AddScopeParameters("userid", Settings.ClientId);
            controller.ControllerBehavior.AddType<Tag>();
            controller.ControllerBehavior.AddType<TagItemMapping>();
            controller.ControllerBehavior.AddType<Status>();
            controller.ControllerBehavior.AddType<Priority>();
            controller.ControllerBehavior.AddType<List>();
            controller.ControllerBehavior.AddType<Item>();
            controller.ControllerBehavior.AddType<User>();

            controller.Refresh();
        }
    }
}
