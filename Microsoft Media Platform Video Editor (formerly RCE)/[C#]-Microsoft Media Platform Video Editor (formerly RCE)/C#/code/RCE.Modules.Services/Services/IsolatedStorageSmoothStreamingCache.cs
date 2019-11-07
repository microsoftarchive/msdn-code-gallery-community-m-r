// <copyright file="IsolatedStorageSmoothStreamingCache.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IsolatedStorageSmoothStreamingCache.cs                     
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
    using System.IO;
    using System.Net;

    using Microsoft.Web.Media.SmoothStreaming;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class IsolatedStorageSmoothStreamingCache : ICache
    {
        private readonly Dictionary<string, CacheItem> cache;

        private readonly IPersistenceService persitenceService;

        public IsolatedStorageSmoothStreamingCache(IPersistenceService persistenceService)
        {
            this.persitenceService = persistenceService;
            this.persitenceService.StorageRemoved += this.OnStorageRemoved;
            this.persitenceService.StorageMaxSpaceReached += this.OnStorageMaxSpaceReached;
            this.cache = new Dictionary<string, CacheItem>();

            Dictionary<string, object> temporalCache = this.persitenceService.GetApplicationSettings();

            foreach (KeyValuePair<string, object> item in temporalCache)
            {
                CacheItem cacheItem = item.Value as CacheItem;

                if (cacheItem != null)
                {
                    this.cache.Add(item.Key, cacheItem);
                }
            }
        }

        public event EventHandler CacheMaxSizeReached;

        public event EventHandler<DataEventArgs<string>> CacheUpdated;

        public event EventHandler CacheCleared;

        public IDictionary<string, CacheItem> CacheItems
        {
            get
            {
                return this.cache;
            }
        }

        public void RemoveItem(string key)
        {
            if (this.cache.ContainsKey(key))
            {
                string fileName = (string)this.cache[key].CachedValue;

                this.persitenceService.RemoveItem(fileName);
                this.cache.Remove(key);
            }
        }

        public IAsyncResult BeginRetrieve(CacheRequest request, AsyncCallback callback, object state)
        {
            CacheResponse response = null;
            CacheAsyncResult cacheAsyncResult = new CacheAsyncResult { FragmentUrl = request.CanonicalUri.ToString() };
            cacheAsyncResult.Complete(response, true);

            return cacheAsyncResult;
        }

        public CacheResponse EndRetrieve(IAsyncResult asyncResult)
        {
            asyncResult.AsyncWaitHandle.WaitOne();

            CacheAsyncResult cacheAsyncResult = asyncResult as CacheAsyncResult;

            if (cacheAsyncResult != null && this.cache.ContainsKey(cacheAsyncResult.FragmentUrl) && !cacheAsyncResult.FragmentUrl.ToUpperInvariant().EndsWith(".ISML/MANIFEST") && !cacheAsyncResult.FragmentUrl.ToUpperInvariant().EndsWith(".ISM/MANIFEST")) 
            {
                string filename = (string)this.cache[cacheAsyncResult.FragmentUrl].CachedValue;

                Stream stream = this.persitenceService.Retrieve(filename);
                if (this.cache[cacheAsyncResult.FragmentUrl].AvoidDeserialization)
                {
                    return new CacheResponse(stream.Length, "text/xml", null, stream, HttpStatusCode.OK, "OK", DateTime.Now);
                }
                
                return new CacheResponse(stream, true);
            }

            return new CacheResponse(0, null, null, null, HttpStatusCode.NotFound, "Not Found", DateTime.Now);
        }

        public IAsyncResult BeginPersist(CacheRequest request, CacheResponse response, AsyncCallback callback, object state)
        {
            CacheAsyncResult cacheAsyncResult = new CacheAsyncResult();

            if (!this.cache.ContainsKey(request.CanonicalUri.ToString()) && !request.CanonicalUri.ToString().ToUpperInvariant().EndsWith(".ISML/MANIFEST") && !request.CanonicalUri.ToString().ToUpperInvariant().EndsWith(".ISM/MANIFEST"))
            {
                cacheAsyncResult.FragmentUrl = request.CanonicalUri.ToString();
                cacheAsyncResult.Complete(response, true);
                return cacheAsyncResult;
            }

            cacheAsyncResult.Complete(null, true);
            return cacheAsyncResult;
        }

        public bool EndPersist(IAsyncResult asyncResult)
        {
            asyncResult.AsyncWaitHandle.WaitOne();

            CacheAsyncResult cacheAsyncResult = asyncResult as CacheAsyncResult;

            if (cacheAsyncResult != null && cacheAsyncResult.Result != null)
            {
                CacheResponse cacheResponse = cacheAsyncResult.Result as CacheResponse;

                if (cacheResponse != null && cacheResponse.Response != null && !this.cache.ContainsKey(cacheAsyncResult.FragmentUrl) && cacheResponse.StatusCode == HttpStatusCode.OK)
                {
                    string fileGuid = Guid.NewGuid().ToString();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        cacheResponse.WriteTo(stream);
                        stream.Position = 0;

                        bool result = this.persitenceService.Persist(fileGuid, stream);

                        if (result)
                        {
                            CacheItem cacheItem = new CacheItem
                                {
                                    CachedValue = fileGuid, 
                                    Date = DateTime.Now
                                };

                            this.cache.Add(cacheAsyncResult.FragmentUrl, cacheItem);
                            this.OnCacheUpdated(cacheAsyncResult.FragmentUrl);

                            this.persitenceService.AddApplicationSettings(cacheAsyncResult.FragmentUrl, cacheItem);

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void OpenMedia(Uri manifestUri)
        {
        }

        public void CloseMedia(Uri manifestUri)
        {
        }

        private void OnStorageMaxSpaceReached(object sender, EventArgs e)
        {
            this.OnCacheMaxSizeReached();
        }

        private void OnCacheMaxSizeReached()
        {
            EventHandler handler = this.CacheMaxSizeReached;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnStorageRemoved(object sender, EventArgs e)
        {
            this.cache.Clear();
            this.OnCacheCleared();
        }

        private void OnCacheUpdated(string key)
        {
            EventHandler<DataEventArgs<string>> handler = this.CacheUpdated;
            if (handler != null)
            {
                handler(this, new DataEventArgs<string>(key));
            }
        }

        private void OnCacheCleared()
        {
            EventHandler handler = this.CacheCleared;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
