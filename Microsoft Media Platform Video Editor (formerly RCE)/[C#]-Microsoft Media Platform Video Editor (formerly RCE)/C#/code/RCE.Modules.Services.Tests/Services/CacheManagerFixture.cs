// <copyright file="CacheManagerFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CacheManagerFixture.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Modules.Services.Tests.Mocks;

    using SMPTETimecode;

    [TestClass]
    public class CacheManagerFixture
    {
        private MockCache cache;

        private MockPurgeStrategy purgeStrategy;

        [TestInitialize]
        public void SetUp()
        {
            this.cache = new MockCache();
            this.purgeStrategy = new MockPurgeStrategy();
        }

        [TestMethod]
        public void ShouldRetrieveEmptyAssetCacheIfThereAreNoFragmentsCached()
        {
            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset
                {
                    Duration = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte24),
                    Source = new Uri("http://test/video.ism/manifest")
                };

            ICacheManager cacheManager = this.CreateCacheManager();

            IDictionary<double, double> assetCache = cacheManager.RetrieveAssetCache(asset);

            Assert.AreEqual(0, assetCache.Count);
        }

        [TestMethod]
        public void ShouldRetrieveAssetCacheIfThereAreFragmentsCached()
        {
            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset
            {
                Duration = TimeCode.FromSeconds(8d, SmpteFrameRate.Smpte24),
                Source = new Uri("http://test/video.ism/manifest")
            };

            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=0)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=20000000)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=40000000)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=60000000)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=80000000)", new CacheItem());

            ICacheManager cacheManager = this.CreateCacheManager();

            IDictionary<double, double> assetCache = cacheManager.RetrieveAssetCache(asset);

            Assert.AreEqual(5, assetCache.Count);
            Assert.AreEqual(0, assetCache.Keys.ElementAt(0));
            Assert.AreEqual(0, assetCache.Values.ElementAt(0));
            
            Assert.AreEqual(0.25, assetCache.Keys.ElementAt(1));
            Assert.AreEqual(0, assetCache.Values.ElementAt(1));
            
            Assert.AreEqual(0.50, assetCache.Keys.ElementAt(2));
            Assert.AreEqual(0, assetCache.Values.ElementAt(2));

            Assert.AreEqual(0.75, assetCache.Keys.ElementAt(3));
            Assert.AreEqual(0, assetCache.Values.ElementAt(3));

            Assert.AreEqual(1, assetCache.Keys.ElementAt(4));
            Assert.AreEqual(0, assetCache.Values.ElementAt(4));
        }

        [TestMethod]
        public void ShouldRetrieveAssetCacheIfCachedItemsHasJumps()
        {
            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset
            {
                Duration = TimeCode.FromSeconds(8d, SmpteFrameRate.Smpte24),
                Source = new Uri("http://test/video.ism/manifest")
            };

            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=0)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=60000000)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=80000000)", new CacheItem());

            ICacheManager cacheManager = this.CreateCacheManager();

            IDictionary<double, double> assetCache = cacheManager.RetrieveAssetCache(asset);

            Assert.AreEqual(3, assetCache.Count);
            Assert.AreEqual(0, assetCache.Keys.ElementAt(0));
            Assert.AreEqual(0, assetCache.Values.ElementAt(0));

            Assert.AreEqual(0.75, assetCache.Keys.ElementAt(1));
            Assert.AreEqual(0.75, assetCache.Values.ElementAt(1));

            Assert.AreEqual(1, assetCache.Keys.ElementAt(2));
            Assert.AreEqual(0.75, assetCache.Values.ElementAt(2));
        }

        [TestMethod]
        public void ShouldUpdateAssetCacheWithNewOffsetsWhenCacheIsUpdated()
        {
            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset
            {
                Duration = TimeCode.FromSeconds(8d, SmpteFrameRate.Smpte24),
                Source = new Uri("http://test/video.ism/manifest")
            };

            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=0)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=60000000)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=80000000)", new CacheItem());

            ICacheManager cacheManager = this.CreateCacheManager();

            IDictionary<double, double> assetCache = cacheManager.RetrieveAssetCache(asset);

            Assert.AreEqual(3, assetCache.Count);

            this.cache.InvokeCacheUpdated("http://test/video.ism/QualityLevels(400000)/Fragments(video=40000000)");

            assetCache = cacheManager.RetrieveAssetCache(asset);

            Assert.AreEqual(4, assetCache.Count);

            Assert.AreEqual(0, assetCache.Keys.ElementAt(0));
            Assert.AreEqual(0, assetCache.Values.ElementAt(0));

            Assert.AreEqual(0.75, assetCache.Keys.ElementAt(1));
            Assert.AreEqual(0.75, assetCache.Values.ElementAt(1));

            Assert.AreEqual(1, assetCache.Keys.ElementAt(2));
            Assert.AreEqual(0.75, assetCache.Values.ElementAt(2));

            Assert.AreEqual(0.50, assetCache.Keys.ElementAt(3));
            Assert.AreEqual(0.50, assetCache.Values.ElementAt(3));
        }

        [TestMethod]
        public void ShouldRaiseCacheUpdatedEventWhenCacheIsUpdated()
        {
            bool cacheUpdatedRaised = false;
            Tuple<double, double, Asset> tuple = null;

            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset
            {
                Duration = TimeCode.FromSeconds(8d, SmpteFrameRate.Smpte24),
                Source = new Uri("http://test/video.ism/manifest")
            };

            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=0)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=60000000)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=80000000)", new CacheItem());

            ICacheManager cacheManager = this.CreateCacheManager();
            cacheManager.CacheUpdated += (s, e) =>
                {
                    cacheUpdatedRaised = true;
                    tuple = e.Data;
                };

            cacheManager.RetrieveAssetCache(asset);

            this.cache.InvokeCacheUpdated("http://test/video.ism/QualityLevels(400000)/Fragments(video=40000000)");

            Assert.IsTrue(cacheUpdatedRaised);
            Assert.AreEqual(0.50, tuple.Item1);
            Assert.AreEqual(0.50, tuple.Item2);
            Assert.AreEqual(asset, tuple.Item3);
        }

        [TestMethod]
        public void ShouldRaiseCacheRebuiltEventWhenCacheIsCleared()
        {
            bool cacheRebuiltRaised = false;

            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset
            {
                Duration = TimeCode.FromSeconds(8d, SmpteFrameRate.Smpte24),
                Source = new Uri("http://test/video.ism/manifest")
            };

            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=0)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=60000000)", new CacheItem());
            this.cache.CacheItems.Add(@"http://test/video.ism/QualityLevels(400000)/Fragments(video=80000000)", new CacheItem());

            ICacheManager cacheManager = this.CreateCacheManager();
            cacheManager.CacheRebuilt += (s, e) =>
            {
                cacheRebuiltRaised = true;
            };

            cacheManager.RetrieveAssetCache(asset);

            this.cache.InvokeCacheCleared();

            Assert.IsTrue(cacheRebuiltRaised);
        }

        private ICacheManager CreateCacheManager()
        {
            return new CacheManager(this.cache, this.purgeStrategy);
        }
    }
}
