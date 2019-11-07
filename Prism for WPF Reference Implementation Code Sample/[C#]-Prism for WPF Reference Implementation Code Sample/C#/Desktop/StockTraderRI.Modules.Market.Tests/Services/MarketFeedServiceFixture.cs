// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.Market.Services;
using StockTraderRI.Modules.Market.Tests.Properties;

namespace StockTraderRI.Modules.Market.Tests.Services
{
    [TestClass]
    public class MarketFeedServiceFixture
    {
        [TestMethod]
        public void CanGetPriceAndVolumeFromMarketFeed()
        {
            using (var marketFeed = new TestableMarketFeedService(new MockPriceUpdatedEventAggregator()))
            {
                marketFeed.TestUpdatePrice("STOCK0", 40.00m, 1234);

                Assert.AreEqual<decimal>(40.00m, marketFeed.GetPrice("STOCK0"));
                Assert.AreEqual<long>(1234, marketFeed.GetVolume("STOCK0"));
            }
        }

        [TestMethod]
        public void ShouldPublishUpdatedOnSinglePriceChange()
        {
            var eventAggregator = new MockPriceUpdatedEventAggregator();

            using (TestableMarketFeedService marketFeed = new TestableMarketFeedService(eventAggregator))
            {
                marketFeed.TestUpdatePrice("STOCK0", 30.00m, 1000);
            }

            Assert.IsTrue(eventAggregator.MockMarketPriceUpdatedEvent.PublishCalled);
        }

        [TestMethod]
        public void GetPriceOfNonExistingSymbolThrows()
        {
            using (var marketFeed = new MarketFeedService(new MockPriceUpdatedEventAggregator()))
            {
                try
                {
                    marketFeed.GetPrice("NONEXISTANT");
                    Assert.Fail("No exception thrown");
                }
                catch (Exception ex)
                {
                    Assert.IsInstanceOfType(ex, typeof(ArgumentException));
                    Assert.IsTrue(ex.Message.Contains("Symbol does not exist in market feed."));
                }
            }
        }

        [TestMethod]
        public void SymbolExistsWorksAsExpected()
        {
            using (var marketFeed = new MarketFeedService(new MockPriceUpdatedEventAggregator()))
            {
                Assert.IsTrue(marketFeed.SymbolExists("STOCK0"));
                Assert.IsFalse(marketFeed.SymbolExists("NONEXISTANT"));
            }
        }

        [TestMethod]
        public void ShouldUpdatePricesWithin5Points()
        {
            using (var marketFeed = new TestableMarketFeedService(new MockPriceUpdatedEventAggregator()))
            {
                decimal originalPrice = marketFeed.GetPrice("STOCK0");
                marketFeed.InvokeUpdatePrices();
                Assert.IsTrue(Math.Abs(marketFeed.GetPrice("STOCK0") - originalPrice) <= 5);
            }
        }

        [TestMethod]
        public void ShouldPublishUpdatedAfterUpdatingPrices()
        {
            var eventAggregator = new MockPriceUpdatedEventAggregator();

            using (var marketFeed = new TestableMarketFeedService(eventAggregator))
            {
                marketFeed.InvokeUpdatePrices();
            }
            Assert.IsTrue(eventAggregator.MockMarketPriceUpdatedEvent.PublishCalled);
        }


        [TestMethod]
        public void MarketServiceReadsIntervalFromXml()
        {
            var xmlMarketData = XDocument.Parse(Resources.TestXmlMarketData);
            using (var marketFeed = new TestableMarketFeedService(xmlMarketData, new MockPriceUpdatedEventAggregator()))
            {
                Assert.AreEqual<int>(5000, marketFeed.RefreshInterval);
            }
        }

        [TestMethod]
        public void UpdateShouldPublishWithinRefreshInterval()
        {
            var eventAggregator = new MockPriceUpdatedEventAggregator();

            using (var marketFeed = new TestableMarketFeedService(eventAggregator))
            {
                marketFeed.RefreshInterval = 500; // ms

                var callCompletedEvent = new System.Threading.ManualResetEvent(false);

                eventAggregator.MockMarketPriceUpdatedEvent.PublishCalledEvent +=
                        delegate { callCompletedEvent.Set(); };
                callCompletedEvent.WaitOne(5000, true); // Wait up to 5 seconds
            }
            Assert.IsTrue(eventAggregator.MockMarketPriceUpdatedEvent.PublishCalled);
        }

        [TestMethod]
        public void RefreshIntervalDefaultsTo10SecondsWhenNotSpecified()
        {
            var xmlMarketData = XDocument.Parse(Resources.TestXmlMarketData);
            xmlMarketData.Element("MarketItems").Attribute("RefreshRate").Remove();

            using (var marketFeed = new TestableMarketFeedService(xmlMarketData, new MockPriceUpdatedEventAggregator()))
            {
                Assert.AreEqual<int>(10000, marketFeed.RefreshInterval);
            }
        }

        [TestMethod]
        public void PublishedEventContainsTheUpdatedPriceList()
        {
            var eventAgregator = new MockPriceUpdatedEventAggregator();
            var marketFeed = new TestableMarketFeedService(eventAgregator);
            Assert.IsTrue(marketFeed.SymbolExists("STOCK0"));

            marketFeed.InvokeUpdatePrices();

            Assert.IsTrue(eventAgregator.MockMarketPriceUpdatedEvent.PublishCalled);
            var payload = eventAgregator.MockMarketPriceUpdatedEvent.PublishArgumentPayload;
            Assert.IsNotNull(payload);
            Assert.IsTrue(payload.ContainsKey("STOCK0"));
            Assert.AreEqual(marketFeed.GetPrice("STOCK0"), payload["STOCK0"]);
        }

    }

    class TestableMarketFeedService : MarketFeedService
    {
        public TestableMarketFeedService(MockPriceUpdatedEventAggregator eventAggregator)
            : base(eventAggregator)
        {

        }

        public TestableMarketFeedService(XDocument xmlDocument, MockPriceUpdatedEventAggregator eventAggregator)
            : base(xmlDocument, eventAggregator)
        {
        }

        public void TestUpdatePrice(string tickerSymbol, decimal price, long volume)
        {
            this.UpdatePrice(tickerSymbol, price, volume);
        }

        public void InvokeUpdatePrices()
        {
            base.UpdatePrices();
        }
    }


    class MockEventAggregator : IEventAggregator
    {
        Dictionary<Type, object> events = new Dictionary<Type, object>();
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            return (TEventType)events[typeof(TEventType)];
        }

        public void AddMapping<TEventType>(TEventType mockEvent)
        {
            events.Add(typeof(TEventType), mockEvent);
        }
    }

    class MockPriceUpdatedEventAggregator : MockEventAggregator
    {
        public MockMarketPricesUpdatedEvent MockMarketPriceUpdatedEvent = new MockMarketPricesUpdatedEvent();
        public MockPriceUpdatedEventAggregator()
        {
            AddMapping<MarketPricesUpdatedEvent>(MockMarketPriceUpdatedEvent);
        }

        public class MockMarketPricesUpdatedEvent : MarketPricesUpdatedEvent
        {
            public bool PublishCalled;
            public IDictionary<string, decimal> PublishArgumentPayload;
            public EventHandler PublishCalledEvent;

            private void OnPublishCalledEvent(object sender, EventArgs args)
            {
                if (PublishCalledEvent != null)
                    PublishCalledEvent(sender, args);
            }

            public override void Publish(IDictionary<string, decimal> payload)
            {
                PublishCalled = true;
                PublishArgumentPayload = payload;
                OnPublishCalledEvent(this, EventArgs.Empty);
            }
        }
    }
}
