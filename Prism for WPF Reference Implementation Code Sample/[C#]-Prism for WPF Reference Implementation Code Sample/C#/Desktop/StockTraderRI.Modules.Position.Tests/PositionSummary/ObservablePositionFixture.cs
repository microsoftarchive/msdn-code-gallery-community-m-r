// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.Position.PositionSummary;
using StockTraderRI.Modules.Position.Tests.Mocks;

namespace StockTraderRI.Modules.Position.Tests.PositionSummary
{
    [TestClass]
    public class ObservablePositionFixture
    {
        [TestMethod]
        public void GeneratesModelFromPositionAndMarketFeeds()
        {
            var accountPositionService = new MockAccountPositionService();
            var marketFeedService = new MockMarketFeedService();
            accountPositionService.AddPosition(new AccountPosition("FUND0", 300m, 1000));
            accountPositionService.AddPosition(new AccountPosition("FUND1", 200m, 100));
            marketFeedService.SetPrice("FUND0", 30.00m);
            marketFeedService.SetPrice("FUND1", 20.00m);

            ObservablePosition position = new ObservablePosition(accountPositionService, marketFeedService, CreateEventAggregator());

            Assert.AreEqual<decimal>(30.00m, position.Items.First(x => x.TickerSymbol == "FUND0").CurrentPrice);
            Assert.AreEqual<long>(1000, position.Items.First(x => x.TickerSymbol == "FUND0").Shares);
            Assert.AreEqual<decimal>(20.00m, position.Items.First(x => x.TickerSymbol == "FUND1").CurrentPrice);
            Assert.AreEqual<long>(100, position.Items.First(x => x.TickerSymbol == "FUND1").Shares);
        }

        [TestMethod]
        public void ShouldUpdateDataWithMarketUpdates()
        {
            var accountPositionService = new MockAccountPositionService();
            var marketFeedService = new MockMarketFeedService();
            var eventAggregator = CreateEventAggregator();
            var marketPricesUpdatedEvent = eventAggregator.GetEvent<MarketPricesUpdatedEvent>() as MockMarketPricesUpdatedEvent;
            marketFeedService.SetPrice("FUND0", 30.00m);
            accountPositionService.AddPosition("FUND0", 25.00m, 1000);
            marketFeedService.SetPrice("FUND1", 20.00m);
            accountPositionService.AddPosition("FUND1", 15.00m, 100);
            ObservablePosition position = new ObservablePosition(accountPositionService, marketFeedService, eventAggregator);

            var updatedPriceList = new Dictionary<string, decimal> { { "FUND0", 50.00m } };
            Assert.IsNotNull(marketPricesUpdatedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.UIThread, marketPricesUpdatedEvent.SubscribeArgumentThreadOption);

            marketPricesUpdatedEvent.SubscribeArgumentAction(updatedPriceList);

            Assert.AreEqual<decimal>(50.00m, position.Items.First(x => x.TickerSymbol == "FUND0").CurrentPrice);
        }

        [TestMethod]
        public void MarketUpdatesPositionUpdatesButCollectionDoesNot()
        {
            var accountPositionService = new MockAccountPositionService();
            var marketFeedService = new MockMarketFeedService();
            var eventAggregator = CreateEventAggregator();
            var marketPricesUpdatedEvent = eventAggregator.GetEvent<MarketPricesUpdatedEvent>() as MockMarketPricesUpdatedEvent;
            marketFeedService.SetPrice("FUND1", 20.00m);
            accountPositionService.AddPosition("FUND1", 15.00m, 100);

            ObservablePosition position = new ObservablePosition(accountPositionService, marketFeedService, eventAggregator);

            bool itemsCollectionUpdated = false;
            position.Items.CollectionChanged += delegate
            {
                itemsCollectionUpdated = true;
            };

            bool itemUpdated = false;
            position.Items.First(p => p.TickerSymbol == "FUND1").PropertyChanged += delegate
            {
                itemUpdated = true;
            };

            marketPricesUpdatedEvent.SubscribeArgumentAction(new Dictionary<string, decimal> { { "FUND1", 50m } });

            Assert.IsFalse(itemsCollectionUpdated);
            Assert.IsTrue(itemUpdated);
        }

        [TestMethod]
        public void AccountPositionModificationUpdatesPM()
        {
            var accountPositionService = new MockAccountPositionService();
            var marketFeedService = new MockMarketFeedService();
            marketFeedService.SetPrice("FUND0", 20.00m);
            accountPositionService.AddPosition("FUND0", 150.00m, 100);
            ObservablePosition position = new ObservablePosition(accountPositionService, marketFeedService, CreateEventAggregator());

            bool itemUpdated = false;
            position.Items.First(p => p.TickerSymbol == "FUND0").PropertyChanged += delegate
            {
                itemUpdated = true;
            };

            AccountPosition accountPosition = accountPositionService.GetAccountPositions().First<AccountPosition>(p => p.TickerSymbol == "FUND0");
            accountPosition.Shares += 11;
            accountPosition.CostBasis = 25.00m;

            Assert.IsTrue(itemUpdated);
            Assert.AreEqual(111, position.Items.First(p => p.TickerSymbol == "FUND0").Shares);
            Assert.AreEqual(25.00m, position.Items.First(p => p.TickerSymbol == "FUND0").CostBasis);
        }

        private static IEventAggregator CreateEventAggregator()
        {
            var eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<MarketPricesUpdatedEvent>()).Returns(new MockMarketPricesUpdatedEvent());

            return eventAggregator.Object;
        }

        private class MockMarketPricesUpdatedEvent : MarketPricesUpdatedEvent
        {
            public Action<IDictionary<string, decimal>> SubscribeArgumentAction;
            public Predicate<IDictionary<string, decimal>> SubscribeArgumentFilter;
            public ThreadOption SubscribeArgumentThreadOption;

            public override SubscriptionToken Subscribe(Action<IDictionary<string, decimal>> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<IDictionary<string, decimal>> filter)
            {
                SubscribeArgumentAction = action;
                SubscribeArgumentFilter = filter;
                SubscribeArgumentThreadOption = threadOption;
                return null;
            }
        }
    }

    /*
     * updates view when position added/removed
     * 
     * viewModel does NOT update view when market feed does not relate to positions (filtering)
     */
}
