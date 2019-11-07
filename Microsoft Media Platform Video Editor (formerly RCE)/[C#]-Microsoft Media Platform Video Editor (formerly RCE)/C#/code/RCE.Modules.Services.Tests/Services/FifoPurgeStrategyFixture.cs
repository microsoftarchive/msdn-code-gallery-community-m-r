// <copyright file="FifoPurgeStrategyFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FifoPurgeStrategyFixture.cs                    
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

    [TestClass]
    public class FifoPurgeStrategyFixture
    {
        private MockCache cache;

        [TestInitialize]
        public void SetUp()
        {
            this.cache = new MockCache();
        }

        [TestMethod]
        public void ShouldCallRemoveItemOnCache()
        {
            CacheItem item1 = new CacheItem { Date = DateTime.Now };
            CacheItem item2 = new CacheItem { Date = DateTime.Now.AddDays(-1) };

            this.cache.CacheItems.Add("1", item1);
            this.cache.CacheItems.Add("2", item2);

            IPurgeStrategy purgeStrategy = new FifoPurgeStrategy();

            purgeStrategy.Purge(this.cache);

            Assert.AreEqual(1, this.cache.RemoveItemCalled.Count);
            Assert.IsTrue(this.cache.RemoveItemCalled["2"]);
        }

        [TestMethod]
        public void ShouldCallRemoveItemOnCacheTwice()
        {
            CacheItem item1 = new CacheItem { Date = DateTime.Now };
            CacheItem item2 = new CacheItem { Date = DateTime.Now.AddDays(-1) };
            CacheItem item3 = new CacheItem { Date = DateTime.Now.AddDays(-3) };
            CacheItem item4 = new CacheItem { Date = DateTime.Now.AddDays(-2) };

            this.cache.CacheItems.Add("1", item1);
            this.cache.CacheItems.Add("2", item2);
            this.cache.CacheItems.Add("3", item3);
            this.cache.CacheItems.Add("4", item4);

            IPurgeStrategy purgeStrategy = new FifoPurgeStrategy();

            purgeStrategy.Purge(this.cache);

            Assert.AreEqual(2, this.cache.RemoveItemCalled.Count);
            Assert.IsTrue(this.cache.RemoveItemCalled["3"]);
            Assert.IsTrue(this.cache.RemoveItemCalled["4"]);
        }

        [TestMethod]
        public void ShouldRaiseItemsPurgedEventWhenItemsArePurged()
        {
            IEnumerable<string> itemsPurged = null;
            bool itemsPurgedRaised = false;

            CacheItem item1 = new CacheItem { Date = DateTime.Now };
            CacheItem item2 = new CacheItem { Date = DateTime.Now.AddDays(-1) };
            CacheItem item3 = new CacheItem { Date = DateTime.Now.AddDays(-3) };
            CacheItem item4 = new CacheItem { Date = DateTime.Now.AddDays(-2) };

            this.cache.CacheItems.Add("1", item1);
            this.cache.CacheItems.Add("2", item2);
            this.cache.CacheItems.Add("3", item3);
            this.cache.CacheItems.Add("4", item4);

            IPurgeStrategy purgeStrategy = new FifoPurgeStrategy();
            purgeStrategy.ItemsPurged += (s, e) =>
                {
                    itemsPurged = e.Data;
                    itemsPurgedRaised = true;
                };
            purgeStrategy.Purge(this.cache);

            Assert.IsTrue(itemsPurgedRaised);
            Assert.AreEqual(2, itemsPurged.Count());
            Assert.AreEqual("3", itemsPurged.ElementAt(0));
            Assert.AreEqual("4", itemsPurged.ElementAt(1));
        }

        [TestMethod]
        public void ShouldNotRaiseItemsPurgedEventWhenItemsAreNotPurged()
        {
            bool itemsPurgedRaised = false;

            IPurgeStrategy purgeStrategy = new FifoPurgeStrategy();
            purgeStrategy.ItemsPurged += (s, e) => itemsPurgedRaised = true;

            purgeStrategy.Purge(this.cache);

            Assert.IsFalse(itemsPurgedRaised);
        }
    }
}
