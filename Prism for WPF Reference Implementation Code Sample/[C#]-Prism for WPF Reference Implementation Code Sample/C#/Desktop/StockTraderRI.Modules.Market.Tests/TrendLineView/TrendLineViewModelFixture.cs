// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Prism.PubSubEvents;
using Moq;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.Market.Tests.Mocks;
using StockTraderRI.Modules.Market.TrendLine;

namespace StockTraderRI.Modules.Market.Tests.TrendLine
{
    /// <summary>
    /// Unit tests for TrendLineViewModel
    /// </summary>
    [TestClass]
    public class TrendLineViewModelFixture
    {
        [TestMethod]
        public void CanInitViewModel()
        {
            var historyService = new MockMarketHistoryService();
            var eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(
                new MockTickerSymbolSelectedEvent());
            TrendLineViewModel viewModel = new TrendLineViewModel(historyService, eventAggregator.Object);

            Assert.IsNotNull(viewModel);
        }

        [TestMethod]
        public void ShouldUpdateModelWithDataFromServiceOnTickerSymbolSelected()
        {
            var historyService = new MockMarketHistoryService();
            var tickerSymbolSelected = new MockTickerSymbolSelectedEvent();
            var eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(
                tickerSymbolSelected);

            TrendLineViewModel viewModel = new TrendLineViewModel(historyService, eventAggregator.Object);

            tickerSymbolSelected.SubscribeArgumentAction("MyTickerSymbol");

            Assert.IsTrue(historyService.GetPriceHistoryCalled);
            Assert.AreEqual("MyTickerSymbol", historyService.GetPriceHistoryArgument);
            Assert.IsNotNull(viewModel.HistoryCollection);
            Assert.AreEqual(historyService.Data.Count, viewModel.HistoryCollection.Count);
            Assert.AreEqual(historyService.Data[0], viewModel.HistoryCollection[0]);
            Assert.AreEqual("MyTickerSymbol", viewModel.TickerSymbol);
        }
    }

    internal class MockTickerSymbolSelectedEvent : TickerSymbolSelectedEvent
    {
        public Action<string> SubscribeArgumentAction;
        public Predicate<string> SubscribeArgumentFilter;
        public override SubscriptionToken Subscribe(Action<string> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<string> filter)
        {
            SubscribeArgumentAction = action;
            SubscribeArgumentFilter = filter;
            return null;
        }
    }
}
