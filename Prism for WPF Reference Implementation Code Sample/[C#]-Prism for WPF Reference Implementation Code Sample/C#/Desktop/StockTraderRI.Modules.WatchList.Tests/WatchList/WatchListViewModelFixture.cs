// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Modules.Watch.WatchList;
using StockTraderRI.Modules.Watch.Services;
using StockTraderRI.Infrastructure.Interfaces;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Moq;
using System.Collections.ObjectModel;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.Watch;
using System.ComponentModel;

namespace StockTraderRI.Modules.WatchList.Tests.WatchList
{
    [TestClass]
    public class WatchListViewModelFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            // Prepare
            MockMarketPricesUpdatedEvent marketPricesUpdatedEvent = new MockMarketPricesUpdatedEvent();

            ObservableCollection<string> watchList = new ObservableCollection<string>();
            watchList.Add("A");
            watchList.Add("B");
            watchList.Add("C");

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();
            mockWatchListService.Setup(x => x.RetrieveWatchList()).Returns(watchList).Verifiable();

            Mock<IMarketFeedService> mockMarketFeedService = new Mock<IMarketFeedService>();
            mockMarketFeedService.Setup(x => x.GetPrice("A")).Returns(1);
            mockMarketFeedService.Setup(x => x.GetPrice("B")).Returns(2);
            mockMarketFeedService.Setup(x => x.GetPrice("C")).Returns(3);

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();

            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<MarketPricesUpdatedEvent>()).Returns(marketPricesUpdatedEvent);

            IWatchListService watchListService = mockWatchListService.Object;
            IMarketFeedService marketFeedService = mockMarketFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            // Act
            WatchListViewModel actual = new WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator);

            // Verify
            Assert.IsNotNull(actual);
            Assert.AreEqual("WATCH LIST", actual.HeaderInfo);
            Assert.AreEqual(3, actual.WatchListItems.Count);
            Assert.IsNull(actual.CurrentWatchItem);
            Assert.IsNotNull(actual.RemoveWatchCommand);
            mockWatchListService.VerifyAll();            
        }

        [TestMethod]
        public void WhenCurrentWatchItemSet_PropertyIsUpdated()
        {
            // Prepare
            MockMarketPricesUpdatedEvent marketPricesUpdatedEvent = new MockMarketPricesUpdatedEvent();
            Mock<TickerSymbolSelectedEvent> mockTickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>();

            ObservableCollection<string> watchList = new ObservableCollection<string>();
            watchList.Add("A");
            watchList.Add("B");
            watchList.Add("C");

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();
            mockWatchListService.Setup(x => x.RetrieveWatchList()).Returns(watchList).Verifiable();

            Mock<IMarketFeedService> mockMarketFeedService = new Mock<IMarketFeedService>();
            mockMarketFeedService.Setup(x => x.GetPrice("A")).Returns(1);
            mockMarketFeedService.Setup(x => x.GetPrice("B")).Returns(2);
            mockMarketFeedService.Setup(x => x.GetPrice("C")).Returns(3);

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();

            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<MarketPricesUpdatedEvent>()).Returns(marketPricesUpdatedEvent);
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(mockTickerSymbolSelectedEvent.Object);

            IWatchListService watchListService = mockWatchListService.Object;
            IMarketFeedService marketFeedService = mockMarketFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;
            
            WatchListViewModel target = new WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator);

            bool propertyChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "CurrentWatchItem")
                {
                    propertyChangedRaised = true;
                }
            };

            
            // Act
            target.CurrentWatchItem = target.WatchListItems[1];

            // Verify
            Assert.AreSame(target.WatchListItems[1], target.CurrentWatchItem);
            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void WhenCurrentWatchItemSet_TickerSymbolSelectedEventRaised()
        {
            // Prepare
            MockMarketPricesUpdatedEvent marketPricesUpdatedEvent = new MockMarketPricesUpdatedEvent();
            Mock<TickerSymbolSelectedEvent> mockTickerSymbolSelectedEvent = new Mock<TickerSymbolSelectedEvent>();
            mockTickerSymbolSelectedEvent.Setup(x => x.Publish("B")).Verifiable();

            ObservableCollection<string> watchList = new ObservableCollection<string>();
            watchList.Add("A");
            watchList.Add("B");
            watchList.Add("C");

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();
            mockWatchListService.Setup(x => x.RetrieveWatchList()).Returns(watchList).Verifiable();

            Mock<IMarketFeedService> mockMarketFeedService = new Mock<IMarketFeedService>();
            mockMarketFeedService.Setup(x => x.GetPrice("A")).Returns(1);
            mockMarketFeedService.Setup(x => x.GetPrice("B")).Returns(2);
            mockMarketFeedService.Setup(x => x.GetPrice("C")).Returns(3);

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();

            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<MarketPricesUpdatedEvent>()).Returns(marketPricesUpdatedEvent);
            mockEventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(mockTickerSymbolSelectedEvent.Object);

            IWatchListService watchListService = mockWatchListService.Object;
            IMarketFeedService marketFeedService = mockMarketFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            WatchListViewModel target = new WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator);

            // Act
            target.CurrentWatchItem = target.WatchListItems[1];

            // Verify
            mockTickerSymbolSelectedEvent.VerifyAll();
        }

        [TestMethod]
        public void WhenRemoveCommandExecuted_RemovesWatchEntry()
        {
            // Prepare
            MockMarketPricesUpdatedEvent marketPricesUpdatedEvent = new MockMarketPricesUpdatedEvent();

            ObservableCollection<string> watchList = new ObservableCollection<string>();
            watchList.Add("A");
            watchList.Add("B");
            watchList.Add("C");

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();
            mockWatchListService.Setup(x => x.RetrieveWatchList()).Returns(watchList).Verifiable();

            Mock<IMarketFeedService> mockMarketFeedService = new Mock<IMarketFeedService>();
            mockMarketFeedService.Setup(x => x.GetPrice("A")).Returns(1);
            mockMarketFeedService.Setup(x => x.GetPrice("B")).Returns(2);
            mockMarketFeedService.Setup(x => x.GetPrice("C")).Returns(3);

            Mock<IRegion> mockMainRegion = new Mock<IRegion>();

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();
            mockRegionManager.Setup(x => x.Regions[RegionNames.MainRegion]).Returns(mockMainRegion.Object);

            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<MarketPricesUpdatedEvent>()).Returns(marketPricesUpdatedEvent);

            IWatchListService watchListService = mockWatchListService.Object;
            IMarketFeedService marketFeedService = mockMarketFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            WatchListViewModel target = new WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator);

            // Act
            target.RemoveWatchCommand.Execute("A");

            // Verify
            Assert.AreEqual(2, target.WatchListItems.Count);
        }

        [TestMethod]
        public void WhenWatchListItemAdded_NavigatesToWatchListView()
        {
            // Prepare
            MockMarketPricesUpdatedEvent marketPricesUpdatedEvent = new MockMarketPricesUpdatedEvent();

            ObservableCollection<string> watchList = new ObservableCollection<string>();
            watchList.Add("A");
            watchList.Add("B");
            watchList.Add("C");

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();
            mockWatchListService.Setup(x => x.RetrieveWatchList()).Returns(watchList).Verifiable();

            Mock<IMarketFeedService> mockMarketFeedService = new Mock<IMarketFeedService>();
            mockMarketFeedService.Setup(x => x.GetPrice("A")).Returns(1);
            mockMarketFeedService.Setup(x => x.GetPrice("B")).Returns(2);
            mockMarketFeedService.Setup(x => x.GetPrice("C")).Returns(3);

            Mock<IRegion> mockMainRegion = new Mock<IRegion>();
            mockMainRegion.Setup(x => x.RequestNavigate(new Uri("/WatchListView", UriKind.RelativeOrAbsolute), It.IsAny<Action<NavigationResult>>())).Verifiable();            

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();
            mockRegionManager.Setup(x => x.Regions[RegionNames.MainRegion]).Returns(mockMainRegion.Object);

            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<MarketPricesUpdatedEvent>()).Returns(marketPricesUpdatedEvent);

            IWatchListService watchListService = mockWatchListService.Object;
            IMarketFeedService marketFeedService = mockMarketFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            WatchListViewModel target = new WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator);

            // Act
            target.WatchListItems.Add(new WatchItem("D", 20));

            // Verify
            mockMainRegion.Verify(x => x.RequestNavigate(new Uri("/WatchListView", UriKind.RelativeOrAbsolute), It.IsAny<Action<NavigationResult>>()), Times.Once());
        }

        [TestMethod]
        public void WhenMarketPriceNotAvailable_PriceSetToNull()
        {
            // Prepare
            MockMarketPricesUpdatedEvent marketPricesUpdatedEvent = new MockMarketPricesUpdatedEvent();

            ObservableCollection<string> watchList = new ObservableCollection<string>();
            watchList.Add("A");

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();
            mockWatchListService.Setup(x => x.RetrieveWatchList()).Returns(watchList).Verifiable();

            Mock<IMarketFeedService> mockMarketFeedService = new Mock<IMarketFeedService>();
            mockMarketFeedService.Setup(x => x.GetPrice("A")).Throws(new ArgumentException("tickerSymbol"));

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();

            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<MarketPricesUpdatedEvent>()).Returns(marketPricesUpdatedEvent);

            IWatchListService watchListService = mockWatchListService.Object;
            IMarketFeedService marketFeedService = mockMarketFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            // Act
            WatchListViewModel actual = new WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator);

            // Verify
            Assert.IsNotNull(actual);            
            Assert.AreEqual(1, actual.WatchListItems.Count);
            Assert.AreEqual("A", actual.WatchListItems[0].TickerSymbol);
            Assert.AreEqual(null, actual.WatchListItems[0].CurrentPrice);
        }

        [TestMethod]
        public void WhenMarketPriceChagnes_PriceUpdated()
        {
            // Prepare
            MockMarketPricesUpdatedEvent marketPricesUpdatedEvent = new MockMarketPricesUpdatedEvent();

            ObservableCollection<string> watchList = new ObservableCollection<string>();
            watchList.Add("A");
            watchList.Add("B");
            watchList.Add("C");

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();
            mockWatchListService.Setup(x => x.RetrieveWatchList()).Returns(watchList).Verifiable();

            Mock<IMarketFeedService> mockMarketFeedService = new Mock<IMarketFeedService>();
            mockMarketFeedService.Setup(x => x.GetPrice("A")).Returns(1);
            mockMarketFeedService.Setup(x => x.GetPrice("B")).Returns(2);
            mockMarketFeedService.Setup(x => x.GetPrice("C")).Returns(3);

            Mock<IRegionManager> mockRegionManager = new Mock<IRegionManager>();

            Mock<IEventAggregator> mockEventAggregator = new Mock<IEventAggregator>();
            mockEventAggregator.Setup(x => x.GetEvent<MarketPricesUpdatedEvent>()).Returns(marketPricesUpdatedEvent);

            IWatchListService watchListService = mockWatchListService.Object;
            IMarketFeedService marketFeedService = mockMarketFeedService.Object;
            IRegionManager regionManager = mockRegionManager.Object;
            IEventAggregator eventAggregator = mockEventAggregator.Object;

            WatchListViewModel target = new WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator);

            Dictionary<string, decimal> newPrices = new Dictionary<string,decimal>()
            {
                { "A", 10 },
                { "B", 20 },
                { "C", 30 },
            };

            // Act
            marketPricesUpdatedEvent.Publish(newPrices);

            // Verify
            Assert.AreEqual(3, target.WatchListItems.Count);
            Assert.AreEqual(10, target.WatchListItems[0].CurrentPrice);
            Assert.AreEqual(20, target.WatchListItems[1].CurrentPrice);
            Assert.AreEqual(30, target.WatchListItems[2].CurrentPrice);
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

            public override void Publish(IDictionary<string, decimal> payload)
            {
                this.SubscribeArgumentAction(payload);                
            }
        }

        private class MockTickerSymbolSelectedEvent : TickerSymbolSelectedEvent
        {
            public bool PublishCalled;
            public string PublishArg;

            public override void Publish(string payload)
            {
                PublishCalled = true;
                PublishArg = payload;
            }
        }
    }
}
