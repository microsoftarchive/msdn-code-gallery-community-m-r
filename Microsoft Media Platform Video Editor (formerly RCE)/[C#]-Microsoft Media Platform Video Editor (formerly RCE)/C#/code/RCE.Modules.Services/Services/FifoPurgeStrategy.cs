// <copyright file="FifoPurgeStrategy.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FifoPurgeStrategy.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class FifoPurgeStrategy : IPurgeStrategy
    {
        public event EventHandler<DataEventArgs<IEnumerable<string>>> ItemsPurged;

        public void Purge(ICache cache)
        {
            IList<string> purgedItems = new List<string>();

            List<KeyValuePair<string, CacheItem>> cacheItems = new List<KeyValuePair<string, CacheItem>>(cache.CacheItems);

            cacheItems.Sort((firstPair, secondPair) => firstPair.Value.Date.CompareTo(secondPair.Value.Date));

            int count = cacheItems.Count;

            int maxItems = count >= 2 ? count / 2 : count;

            if (maxItems >= 1)
            {
                int i = 0;
                foreach (KeyValuePair<string, CacheItem> cacheItem in cacheItems)
                {
                    i++;
                    cache.RemoveItem(cacheItem.Key);
                    purgedItems.Add(cacheItem.Key);

                    if (i >= maxItems)
                    {
                        break;
                    }
                }
            }

            if (purgedItems.Count > 0)
            {
                this.OnItemsPurged(purgedItems);
            }
        }

        private void OnItemsPurged(IEnumerable<string> purgedItems)
        {
            EventHandler<DataEventArgs<IEnumerable<string>>> handler = this.ItemsPurged;
            if (handler != null)
            {
                handler(this, new DataEventArgs<IEnumerable<string>>(purgedItems));
            }
        }
    }
}
