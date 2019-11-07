// <copyright file="CacheManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CacheManager.cs                     
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
    using System.Linq;
    using System.Text.RegularExpressions;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class CacheManager : ICacheManager
    {
        private readonly ICache cache;
        private readonly IPurgeStrategy purgeStrategy;

        private readonly Regex timecodeRegex = new Regex(@"video=(\d+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private readonly IDictionary<string, CacheIndex> assetsCacheIndex;

        public CacheManager(ICache cache, IPurgeStrategy purgeStrategy)
        {
            this.cache = cache;
            this.purgeStrategy = purgeStrategy;
            this.assetsCacheIndex = new Dictionary<string, CacheIndex>();

            this.purgeStrategy.ItemsPurged += this.OnItemsPurged;
            this.cache.CacheUpdated += this.OnCacheUpdated;
            this.cache.CacheMaxSizeReached += this.OnCacheCacheMaxSizeReached;
            this.cache.CacheCleared += this.OnCacheCleared;
        }

        public event EventHandler<DataEventArgs<Tuple<double, double, Asset>>> CacheUpdated;

        public event EventHandler<DataEventArgs<Tuple<IDictionary<double, double>, Asset>>> CacheRebuilt;

        public IDictionary<double, double> RetrieveAssetCache(IAdaptiveAsset asset)
        {
            return this.RetrieveAssetCache(asset, false);
        }

        private static IDictionary<double, double> GenerateCacheSnapshot(IEnumerable<double> timecodes, double assetDuration)
        {
            IDictionary<double, double> cacheSnapshot = new Dictionary<double, double>();

            double? previousTimecode = null;
            double offset = 0;

            foreach (double timecode in timecodes)
            {
                double progress = timecode / assetDuration;

                if (cacheSnapshot.ContainsKey(progress))
                {
                    continue;
                }

                if (previousTimecode.HasValue && (timecode - previousTimecode) >= 4)
                {
                    offset = progress;
                }

                previousTimecode = timecode;

                cacheSnapshot.Add(progress, offset);
            }

            return cacheSnapshot;
        }

        private static IDictionary<double, double> GenerateCacheSnapshot(IDictionary<double, double> cacheSnapshot, IEnumerable<double> timecodes, double newTimecode, double assetDuration)
        {
            IDictionary<double, double> currentCache = new Dictionary<double, double>();

            IEnumerable<double> matchedTimecodes = timecodes.Where(x => x < newTimecode);

            double previousTimecode = newTimecode;

            if (matchedTimecodes.Count() > 0)
            {
                previousTimecode = matchedTimecodes.Max();
            }

            double progress = previousTimecode / assetDuration;

            if (cacheSnapshot.ContainsKey(progress))
            {
                double offset = cacheSnapshot[progress];
                progress = newTimecode / assetDuration;

                if (newTimecode - previousTimecode >= 4)
                {
                    offset = progress;
                }

                currentCache.Add(progress, offset);
            }
            else
            {
                double offset = 0;
                if (newTimecode - previousTimecode > 4)
                {
                    offset = progress;
                }

                currentCache.Add(progress, offset);                
            }

            return currentCache;
        }

        private IDictionary<double, double> RetrieveAssetCache(IAdaptiveAsset asset, bool removeExistingCache)
        {
            double assetDuration = asset.DurationInSeconds;

            string assetUri = asset.Source.ToString().ToUpperInvariant();

            int manifestIndex = assetUri.LastIndexOf("/MANIFEST");

            string cacheKey = assetUri.Substring(0, assetUri.Length - (assetUri.Length - manifestIndex));

            if (this.assetsCacheIndex.ContainsKey(cacheKey))
            {
                if (removeExistingCache)
                {
                    this.assetsCacheIndex.Remove(cacheKey);
                }
                else
                {
                    IDictionary<double, double> currentCache = this.assetsCacheIndex[cacheKey].Cache;

                    // if (asset == this.assetsCacheIndex[cacheKey].Asset && currentCache.Count > 0)
                    if (currentCache.Count > 0)
                    {
                        return currentCache;
                    }
                }
            }

            IEnumerable<string> cachedKeys = this.cache.CacheItems.Keys.Where(key => key.ToUpperInvariant().StartsWith(cacheKey) && key.ToUpperInvariant().Contains("/FRAGMENTS(VIDEO="));

            List<double> timecodes = new List<double>();

            foreach (string key in cachedKeys)
            {
                double? timecode = this.ExtractTimecode(key, asset.StartPosition);

                if (timecode.HasValue && !timecodes.Contains(timecode.Value))
                {
                    timecodes.Add(timecode.Value);
                }
            }

            timecodes.Sort();

            IDictionary<double, double> cacheSnapshot = GenerateCacheSnapshot(timecodes, assetDuration);

            this.assetsCacheIndex.Add(cacheKey, new CacheIndex(timecodes, cacheSnapshot, asset));

            return cacheSnapshot;
        }

        private double? ExtractTimecode(string key, double startPosition)
        {
            string[] result = this.timecodeRegex.Split(key);

            double timecode;

            if (result.Length > 1 && double.TryParse(result[1], out timecode))
            {
                return (timecode / 10000000) - startPosition;
            }

            return null;
        }

        private CacheIndex GetCacheIndex(string key)
        {
            string cacheKey = key.ToUpperInvariant();

            if (cacheKey.Contains("/FRAGMENTS(VIDEO="))
            {
                string cacheIndexKey = this.assetsCacheIndex.Keys.Where(cacheKey.StartsWith).SingleOrDefault();

                if (!string.IsNullOrEmpty(cacheIndexKey) && this.assetsCacheIndex.ContainsKey(cacheIndexKey))
                {
                    return this.assetsCacheIndex[cacheIndexKey];
                }
            }

            return null;
        }

        private void OnCacheCacheMaxSizeReached(object sender, EventArgs e)
        {
            this.cache.CacheMaxSizeReached -= this.OnCacheCacheMaxSizeReached;
            
            this.purgeStrategy.Purge(this.cache);

            this.cache.CacheMaxSizeReached += this.OnCacheCacheMaxSizeReached;
        }

        private void InvokeCacheUpdated(double progress, double offset, Asset asset)
        {
            EventHandler<DataEventArgs<Tuple<double, double, Asset>>> handler = this.CacheUpdated;
            if (handler != null)
            {
                handler(this, new DataEventArgs<Tuple<double, double, Asset>>(Tuple.Create(progress, offset, asset)));
            }
        }

        private void OnCacheUpdated(object sender, DataEventArgs<string> e)
        {
            CacheIndex cacheIndex = this.GetCacheIndex(e.Data);

            if (cacheIndex != null)
            {
                double? timecode = this.ExtractTimecode(e.Data, cacheIndex.Asset.StartPosition);

                if (timecode.HasValue && !cacheIndex.Timecodes.Contains(timecode.Value))
                {
                    IDictionary<double, double> cacheSnapshot = GenerateCacheSnapshot(
                        cacheIndex.Cache, cacheIndex.Timecodes, timecode.Value, cacheIndex.Asset.DurationInSeconds);

                    if (cacheSnapshot.Count == 1)
                    {
                        cacheIndex.Timecodes.Add(timecode.Value);
                        cacheIndex.Timecodes.Sort();

                        double progress = cacheSnapshot.Keys.ElementAt(0);
                        double offset = cacheSnapshot.Values.ElementAt(0);
                        cacheIndex.Cache.Add(progress, offset);

                        Asset asset = cacheIndex.Asset as Asset;

                        if (asset != null)
                        {
                            this.InvokeCacheUpdated(progress, offset, asset);
                        }
                    }
                }
            }
        }

        private void OnItemsPurged(object sender, DataEventArgs<IEnumerable<string>> e)
        {
            IDictionary<string, CacheIndex> updatedCache = new Dictionary<string, CacheIndex>();

            foreach (string key in e.Data)
            {
                string cacheKey = key.ToUpperInvariant();

                if (cacheKey.Contains("/FRAGMENTS(VIDEO="))
                {
                    string cacheIndexKey = this.assetsCacheIndex.Keys.Where(cacheKey.StartsWith).SingleOrDefault();

                    if (!string.IsNullOrEmpty(cacheIndexKey) && this.assetsCacheIndex.ContainsKey(cacheIndexKey))
                    {
                        CacheIndex cacheIndex = this.assetsCacheIndex[cacheIndexKey];

                        double? timecode = this.ExtractTimecode(cacheKey, cacheIndex.Asset.StartPosition);

                        if (timecode.HasValue && cacheIndex.Timecodes.Contains(timecode.Value))
                        {
                            IDictionary<double, double> cacheSnapshot = GenerateCacheSnapshot(cacheIndex.Cache, cacheIndex.Timecodes, timecode.Value, cacheIndex.Asset.DurationInSeconds);

                            if (cacheSnapshot.Count == 1)
                            {
                                cacheIndex.Timecodes.Remove(timecode.Value);

                                double progress = cacheSnapshot.Keys.ElementAt(0);
                                cacheIndex.Cache.Remove(progress);

                                if (!updatedCache.ContainsKey(cacheIndexKey))
                                {
                                    updatedCache.Add(cacheIndexKey, cacheIndex);
                                }
                            }
                        }
                    }
                }
            }

            if (updatedCache.Count > 0)
            {
                foreach (string key in updatedCache.Keys)
                {
                    CacheIndex cacheIndex = this.assetsCacheIndex[key];

                    IDictionary<double, double> newCache = GenerateCacheSnapshot(cacheIndex.Timecodes, cacheIndex.Asset.DurationInSeconds);

                    cacheIndex.SetCache(newCache);

                    this.OnCacheRebuilt(newCache, cacheIndex.Asset);
                }
            }
        }

        private void OnCacheRebuilt(IDictionary<double, double> newCache, IAdaptiveAsset adaptiveAsset)
        {
            Asset asset = adaptiveAsset as Asset;

            if (asset != null && newCache != null)
            {
                this.OnCacheRebuilt(new Tuple<IDictionary<double, double>, Asset>(newCache, asset));
            }
        }

        private void OnCacheRebuilt(Tuple<IDictionary<double, double>, Asset> tuple)
        {
            EventHandler<DataEventArgs<Tuple<IDictionary<double, double>, Asset>>> handler = this.CacheRebuilt;
            if (handler != null)
            {
                handler(this, new DataEventArgs<Tuple<IDictionary<double, double>, Asset>>(tuple));
            }
        }

        private void OnCacheCleared(object sender, EventArgs e)
        {
            IList<IAdaptiveAsset> assets = this.assetsCacheIndex.Keys.Select(key => this.assetsCacheIndex[key].Asset).ToList();

            foreach (IAdaptiveAsset adaptiveAsset in assets)
            {
                IDictionary<double, double> newCache = this.RetrieveAssetCache(adaptiveAsset, true);

                this.OnCacheRebuilt(newCache, adaptiveAsset);
            }
        }

        private class CacheIndex
        {
            public CacheIndex(List<double> timecodes, IDictionary<double, double> cache, IAdaptiveAsset asset)
            {
                this.Timecodes = timecodes;
                this.Cache = cache;
                this.Asset = asset;
            }

            public List<double> Timecodes { get; private set; }

            public IDictionary<double, double> Cache { get; private set; }

            public IAdaptiveAsset Asset { get; private set; }

            public void SetCache(IDictionary<double, double> newCache)
            {
                this.Cache = newCache;
            }
        }
    }
}
