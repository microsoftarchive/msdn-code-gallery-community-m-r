// <copyright file="ICache.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ICache.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Web.Media.SmoothStreaming;

    using RCE.Infrastructure.Models;

    /// <summary>
    /// Provide success to common members of caches.
    /// </summary>
    public interface ICache : ISmoothStreamingCache
    {
        /// <summary>
        /// Occurs when max size of cache is reached.
        /// </summary>
        event EventHandler CacheMaxSizeReached;

        /// <summary>
        /// Occurs when the cache is updated.
        /// </summary>
        event EventHandler<DataEventArgs<string>> CacheUpdated;

        /// <summary>
        /// Occurs when the cache is cleared
        /// </summary>
        event EventHandler CacheCleared;

        /// <summary>
        /// Gets CacheItemsKeys.
        /// </summary>
        IDictionary<string, CacheItem> CacheItems { get; }

        /// <summary>
        /// Removes an item from the collection.
        /// </summary>
        /// <param name="key">
        /// The key to remove.
        /// </param>
        void RemoveItem(string key);
    }
}