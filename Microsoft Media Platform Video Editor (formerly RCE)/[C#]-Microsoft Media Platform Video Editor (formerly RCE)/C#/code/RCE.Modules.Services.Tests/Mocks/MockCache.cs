// <copyright file="MockCache.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCache.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Mocks
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Web.Media.SmoothStreaming;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class MockCache : ICache
    {
        public MockCache()
        {
            this.CacheItems = new Dictionary<string, CacheItem>();
            this.RemoveItemCalled = new Dictionary<string, bool>();
        }

        public event EventHandler CacheMaxSizeReached;

        public event EventHandler<DataEventArgs<string>> CacheUpdated;

        public event EventHandler CacheCleared;

        public IDictionary<string, CacheItem> CacheItems { get; set; }

        public IDictionary<string, bool> RemoveItemCalled { get; set; }

        public void RemoveItem(string key)
        {
            this.RemoveItemCalled.Add(key, true);
        }

        public IAsyncResult BeginRetrieve(CacheRequest request, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public CacheResponse EndRetrieve(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginPersist(CacheRequest request, CacheResponse response, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public bool EndPersist(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }

        public void OpenMedia(Uri manifestUri)
        {
            throw new NotImplementedException();
        }

        public void CloseMedia(Uri manifestUri)
        {
            throw new NotImplementedException();
        }

        public void InvokeCacheUpdated(string key)
        {
            EventHandler<DataEventArgs<string>> handler = this.CacheUpdated;
            if (handler != null)
            {
                handler(this, new DataEventArgs<string>(key));
            }
        }

        public void InvokeCacheCleared()
        {
            EventHandler handler = this.CacheCleared;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
