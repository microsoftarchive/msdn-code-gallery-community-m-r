// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.Watch.Services;
using StockTraderRI.Modules.WatchList.Tests.Mocks;

namespace StockTraderRI.Modules.WatchList.Tests.Services
{
    /// <summary>
    /// Summary description for WatchListServiceFixture
    /// </summary>
    [TestClass]
    public class WatchListServiceFixture
    {
        [TestMethod]
        public void ServiceListensToAddWatchCommand()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());
            Assert.AreEqual(0, service.RetrieveWatchList().Count);

            service.AddWatchCommand.Execute("Stock999");

            Assert.AreEqual(1, service.RetrieveWatchList().Count);
        }

        [TestMethod]
        public void ServiceListensToAddWatchCommandAndReturnsCommandParamsInList()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute("STOCK00");
            service.AddWatchCommand.Execute("STOCK99");

            Assert.AreEqual(2, service.RetrieveWatchList().Count);
            Assert.AreEqual<string>("STOCK00", service.RetrieveWatchList()[0]);
            Assert.AreEqual<string>("STOCK99", service.RetrieveWatchList()[1]);
        }

        [TestMethod]
        public void GetWatchListShouldReturnObservableCollection()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute("Stock000");
            ObservableCollection<string> watchList = service.RetrieveWatchList();

            bool collectionChanged = false;
            watchList.CollectionChanged += delegate
            {
                collectionChanged = true;
            };

            service.AddWatchCommand.Execute("Stock111");

            Assert.AreEqual(true, collectionChanged);
        }

        [TestMethod]
        public void TickerSymbolGetsConvertedToUppercase()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute("StockInMixedCase");

            Assert.AreEqual<string>("StockInMixedCase".ToUpper(CultureInfo.InvariantCulture), service.RetrieveWatchList()[0]);
        }

        [TestMethod]
        public void NullOrEmptyStringIsNotAddedToList()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute(null);
            service.AddWatchCommand.Execute(string.Empty);

            Assert.AreEqual(0, service.RetrieveWatchList().Count);
        }

        [TestMethod]
        public void AddingSameSymbolTwiceOnlyAddsItOnceToTheList()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute("DUPE");
            service.AddWatchCommand.Execute("DUPE");

            Assert.AreEqual(1, service.RetrieveWatchList().Count);
        }

        [TestMethod]
        public void DoesNotAddWatchIfSymbolDoesNotExistInMarketFeed()
        {
            MockMarketFeedService marketFeedService = new MockMarketFeedService();
            marketFeedService.MockSymbolExists = false;

            WatchListService service = new WatchListService(marketFeedService);

            service.AddWatchCommand.Execute("INEXISTENT");

            Assert.AreEqual(0, service.RetrieveWatchList().Count);
            Assert.AreEqual<string>("INEXISTENT", marketFeedService.SymbolExistsArgumentTickerSymbol);
        }

        [TestMethod]
        public void SymbolWithLeadingBlankSpacesIsAddedToList()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute("    FUND0");

            Assert.AreEqual(1, service.RetrieveWatchList().Count);
            Assert.AreEqual("FUND0", service.RetrieveWatchList()[0]);
        }

        [TestMethod]
        public void SymbolWithTrailingBlankSpacesIsAddedToList()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute("FUND0    ");

            Assert.AreEqual(1, service.RetrieveWatchList().Count);
            Assert.AreEqual("FUND0", service.RetrieveWatchList()[0]);
        }


        [TestMethod]
        public void AddingSameSymbolOneWithLeadingBlankSpacesTwiceOnlyAddsItOnceToTheList()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute("    FUND0");
            service.AddWatchCommand.Execute("FUND0");

            Assert.AreEqual(1, service.RetrieveWatchList().Count);
            Assert.AreEqual("FUND0", service.RetrieveWatchList()[0]);
        }

        [TestMethod]
        public void AddingSameSymbolOneWithTrailingBlankSpacesTwiceOnlyAddsItOnceToTheList()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            service.AddWatchCommand.Execute("FUND0    ");
            service.AddWatchCommand.Execute("FUND0");

            Assert.AreEqual(1, service.RetrieveWatchList().Count);
            Assert.AreEqual("FUND0", service.RetrieveWatchList()[0]);
        }

        [TestMethod]
        public void ServiceExposesCommandInstance()
        {
            WatchListService service = new WatchListService(new MockMarketFeedService());

            Assert.AreEqual(0, service.RetrieveWatchList().Count);
            service.AddWatchCommand.Execute("testSymbol");
            Assert.AreEqual(1, service.RetrieveWatchList().Count);
            Assert.AreEqual("TESTSYMBOL", service.RetrieveWatchList()[0]);

        }


    }

    class TestableWatchListService : WatchListService
    {
        public TestableWatchListService(IMarketFeedService marketFeedService)
            : base(marketFeedService)
        {
        }
    }
}
