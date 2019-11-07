// <copyright file="CompositeSmoothStreamingCache.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CompositeSmoothStreamingCache.cs                     
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
    using Microsoft.Practices.Unity;
    using Microsoft.Web.Media.SmoothStreaming;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class CompositeSmoothStreamingCache : ICache
    {
        private readonly ICache primary;

        private readonly ICache secondary;

        private string lastItemUpdated;

        public CompositeSmoothStreamingCache([Dependency("PrimaryCache")] ICache primaryCache/*, [Dependency("SecondaryCache")] ICache secondaryCache*/)
        {
            this.primary = primaryCache;
            //// this.secondary = secondaryCache;

            if (this.primary != null)
            {
                this.primary.CacheMaxSizeReached += this.OnCacheMaxSizeReached;
                this.primary.CacheUpdated += this.OnCacheUpdated;
                this.primary.CacheCleared += this.OnCacheCleared;
            }

            if (this.secondary != null)
            {
                this.secondary.CacheMaxSizeReached += this.OnCacheMaxSizeReached;
                this.secondary.CacheUpdated += this.OnCacheUpdated;
                this.secondary.CacheCleared += this.OnCacheCleared;
            }
        }

        /// <summary>
        /// Occurs when max size of cache is reached.
        /// </summary>
        public event EventHandler CacheMaxSizeReached;

        /// <summary>
        /// Occurs when the cache is updated.
        /// </summary>
        public event EventHandler<DataEventArgs<string>> CacheUpdated;

        /// <summary>
        /// Occurs when the cache is cleared
        /// </summary>
        public event EventHandler CacheCleared;

        public IDictionary<string, CacheItem> CacheItems
        {
            get
            {
                if (this.secondary != null && this.secondary.CacheItems.Count > 0)
                {
                    return this.secondary.CacheItems;
                }

                if (this.primary != null)
                {
                    return this.primary.CacheItems;
                }

                return new Dictionary<string, CacheItem>();
            }
        }

        public void RemoveItem(string key)
        {
            if (this.primary != null)
            {
                this.primary.RemoveItem(key);
            }

            if (this.secondary != null)
            {
                this.secondary.RemoveItem(key);
            }
        }

        public IAsyncResult BeginRetrieve(CacheRequest request, AsyncCallback callback, object state)
        {
            IAsyncResult result = null;

            if (this.primary != null)
            {
                result = this.primary.BeginRetrieve(request, callback, state);
            }
            else if (this.secondary != null)
            {
                result = this.secondary.BeginRetrieve(request, callback, state);
            }

            return result;
        }

        public CacheResponse EndRetrieve(IAsyncResult ar)
        {
            CacheResponse primaryResponse = null;

            if (this.primary != null)
            {
                primaryResponse = this.primary.EndRetrieve(ar);
            }

            if (this.secondary != null)
            {
                CacheResponse secondaryResponse = this.secondary.EndRetrieve(ar);

                if ((secondaryResponse == null || secondaryResponse.ContentLength == 0) && (primaryResponse != null && primaryResponse.ContentLength > 0))
                {
                    CacheAsyncResult cacheAsyncResult = ar as CacheAsyncResult;

                    if (cacheAsyncResult != null)
                    {
                        cacheAsyncResult.Result = primaryResponse;

                        this.secondary.EndPersist(cacheAsyncResult);
                    }
                }

                if ((primaryResponse == null || primaryResponse.ContentLength == 0) && (secondaryResponse != null && secondaryResponse.ContentLength > 0))
                {
                    CacheAsyncResult cacheAsyncResult = ar as CacheAsyncResult;

                    if (cacheAsyncResult != null && this.primary != null)
                    {
                        cacheAsyncResult.Result = secondaryResponse;

                        this.primary.EndPersist(cacheAsyncResult);
                    }

                    primaryResponse = secondaryResponse;
                }
            }

            return primaryResponse;
        }

        public IAsyncResult BeginPersist(CacheRequest request, CacheResponse response, AsyncCallback callback, object state)
        {
            IAsyncResult result = null;

            if (this.primary != null)
            {
                result = this.primary.BeginPersist(request, response, callback, state);
            }
            else if (this.secondary != null)
            {
                result = this.secondary.BeginPersist(request, response, callback, state);
            }

            return result;
        }

        public bool EndPersist(IAsyncResult asyncResult)
        {
            bool primaryResult = false;

            if (this.primary != null)
            {
                primaryResult = this.primary.EndPersist(asyncResult);
            }

            if (this.secondary != null)
            {
                bool secondaryResult = this.secondary.EndPersist(asyncResult);

                primaryResult = primaryResult | secondaryResult;
            }

            return primaryResult;
        }

        public void OpenMedia(Uri manifestUri)
        {
        }

        public void CloseMedia(Uri manifestUri)
        {
        }

        private void OnCacheMaxSizeReached(object sender, EventArgs e)
        {
            this.InvokeCacheMaxSizeReached();
        }

        private void OnCacheUpdated(object sender, DataEventArgs<string> e)
        {
            if (e.Data != this.lastItemUpdated)
            {
                this.lastItemUpdated = e.Data;
                this.InvokeCacheUpdated(e.Data);
            }
        }

        private void InvokeCacheMaxSizeReached()
        {
            EventHandler handler = this.CacheMaxSizeReached;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void InvokeCacheUpdated(string key)
        {
            EventHandler<DataEventArgs<string>> handler = this.CacheUpdated;
            if (handler != null)
            {
                handler(this, new DataEventArgs<string>(key));
            }
        }

        private void OnCacheCleared(object sender, EventArgs e)
        {
            this.InvokeCacheCleared();
        }

        private void InvokeCacheCleared()
        {
            EventHandler handler = this.CacheCleared;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
} 