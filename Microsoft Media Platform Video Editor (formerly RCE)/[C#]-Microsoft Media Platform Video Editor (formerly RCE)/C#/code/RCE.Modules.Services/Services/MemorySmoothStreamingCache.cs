// <copyright file="MemorySmoothStreamingCache.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MemorySmoothStreamingCache.cs                     
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
    using System.Windows;

    using Microsoft.Web.Media.SmoothStreaming;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class MemorySmoothStreamingCache : ICache
    {
        private Dictionary<string, CacheItem> cache;

        public MemorySmoothStreamingCache()
        {
            this.cache = new Dictionary<string, CacheItem>();

            Application current = Application.Current;
            if (current != null)
            {
                current.Exit += this.ApplicationExitHandler;
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
            get { return this.cache; }
        }

        public void RemoveItem(string key)
        {
            if (this.cache.ContainsKey(key))
            {
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
                Stream stream = (Stream)this.cache[cacheAsyncResult.FragmentUrl].CachedValue;

                stream.Position = 0;

                if (this.cache[cacheAsyncResult.FragmentUrl].AvoidDeserialization)
                {
                    return new CacheResponse(stream.Length, "text/xml", null, stream, HttpStatusCode.OK, "OK", DateTime.Now);
                }
                else
                {
                    return new CacheResponse(stream, true);
                }
            }

            return new CacheResponse(0, null, null, null, System.Net.HttpStatusCode.NotFound, "Not Found", DateTime.Now, false);
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
                if (!this.cache.ContainsKey(cacheAsyncResult.FragmentUrl))
                {
                    Stream memoryStream = new MemoryStream();
                    CacheResponse cacheResponse = cacheAsyncResult.Result as CacheResponse;

                    if (cacheResponse != null && cacheResponse.StatusCode == HttpStatusCode.OK)
                    {
                        cacheResponse.WriteTo(memoryStream);
                        memoryStream.Position = 0;

                        CacheItem cacheItem = new CacheItem
                            {
                                CachedValue = memoryStream, 
                                Date = DateTime.Now
                            };

                        this.cache.Add(cacheAsyncResult.FragmentUrl, cacheItem);
                        this.OnCacheUpdated(cacheAsyncResult.FragmentUrl);

                        return true;
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

        private void OnCacheUpdated(string key)
        {
            EventHandler<DataEventArgs<string>> handler = this.CacheUpdated;
            if (handler != null)
            {
                handler(this, new DataEventArgs<string>(key));
            }
        }

        private void ApplicationExitHandler(object sender, EventArgs e)
        {
            Application current = Application.Current;
            if (current != null)
            {
                current.Exit -= this.ApplicationExitHandler;
                this.cache.Clear();
            }
        }
    }
}
