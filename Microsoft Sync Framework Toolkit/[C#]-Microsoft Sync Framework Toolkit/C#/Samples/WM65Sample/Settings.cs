// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

namespace SmartDeviceProject1
{
    /// <summary>
    /// This class contains settings needed by the application.
    /// </summary>
    public static class Settings
    {
        //Note: Change this URL to point to the ListService.
        /// <summary>
        /// Service URL. 
        /// </summary>
        public static string SyncServiceUrl = @"http://localhost/listservice/DefaultScopeSyncService.SVC/";

        //Note: Change this URL to point to the ListService.
        /// <summary>
        /// Login URL
        /// </summary>
        public static string LoginUrl = "http://localhost/listservice/login.ashx/?userName={0}";

        /// <summary>
        /// Scope Name for the sync service
        /// </summary>
        public static string SyncScope = "DefaultScope";

        /// <summary>
        /// Client Id for the user logged in.
        /// </summary>
        public static string ClientId;
    }
}
    