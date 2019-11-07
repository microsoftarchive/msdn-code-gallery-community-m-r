// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using DefaultScope;
using Microsoft.Synchronization.ClientServices;
using System.Linq;
using System.ComponentModel;

namespace ListsManagement.ViewModels
{
    static class SyncContextInstance
    {
        static DefaultScopeOfflineContext context;
        static List<CacheRefreshStatistics> statsLog = new List<CacheRefreshStatistics>(10);
        static int index = 0;

        public static string LoginUriFormat = "http://[YourHostName]/ListService/Login.ashx?username={0}";
        public static DefaultScopeOfflineContext Context
        {
            get
            {
                if (context == null)
                {
                    InitContext();
                }
                return context;
            }
        }

        static void InitContext()
        {
            context = new DefaultScopeOfflineContext("ListsManagerCache", new Uri("http://[YourHostName]/listservice/defaultscopesyncservice.svc/"));
            context.CacheController.ControllerBehavior.AddScopeParameters("UserID", SettingsViewModel.Instance.UserId.ToString());
            context.CacheController.ControllerBehavior.SerializationFormat = SerializationFormat.ODataJSON;
        }

        public static void ClearContext()
        {
            context = null;
        }

        public static void AddStats(CacheRefreshStatistics stats, Exception e)
        {
            if (SettingsViewModel.Instance.SyncLogEnabled)
            {
                if (statsLog.Count == 10)
                {
                    statsLog.RemoveAt(index);
                }
                statsLog.Insert(index, stats);
                index = index++ / 10;
            }
        }

        public static IEnumerable<CacheRefreshStatistics> Stats
        {
            get
            {
                return statsLog.OrderByDescending((e) => e.StartTime).Take(10);
            }
        }
    }
}
