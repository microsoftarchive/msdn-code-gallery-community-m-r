// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.Position.PositionSummary;
using StockTraderRI.Modules.Position.Tests.Mocks;

namespace StockTraderRI.Modules.Position.Tests.PositionSummary
{
    [TestClass]
    public class PositionSummaryViewModelFixture
    {
        MockOrdersController ordersController;
        Mock<IEventAggregator> eventAggregator;
        MockObservablePosition observablePosition;

        [TestInitialize]
        public void SetUp()
        {
            ordersController = new MockOrdersController();
            observablePosition = new MockObservablePosition();
            eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(
                new MockTickerSymbolSelectedEvent());
        }

        [TestMethod]
        public void ShouldTriggerPropertyChangedEventOnCurrentPositionSummaryItemChange()
        {
            PositionSummaryViewModel viewModel = CreateViewModel();
            string changedPropertyName = string.Empty;

            viewModel.PropertyChanged += (o, e) =>
                                                     {
                                                         changedPropertyName = e.PropertyName;
                                                     };

            viewModel.CurrentPositionSummaryItem = new PositionSummaryItem("NewTickerSymbol", 0, 0, 0);

            Assert.AreEqual("CurrentPositionSummaryItem", changedPropertyName);
        }

        [TestMethod]
        public void TickerSymbolSelectedPublishesEvent()
        {
            var tickerSymbolSelectedEvent = new MockTickerSymbolSelectedEvent();
            eventAggregator.Setup(x => x.GetEvent<TickerSymbolSelectedEvent>()).Returns(tickerSymbolSelectedEvent);
            var viewModel = CreateViewModel();

            viewModel.CurrentPositionSummaryItem = new PositionSummaryItem("FUND0", 0, 0, 0);

            Assert.IsTrue(tickerSymbolSelectedEvent.PublishCalled);
            Assert.AreEqual("FUND0", tickerSymbolSelectedEvent.PublishArgumentPayload);
        }

        [TestMethod]
        public void ControllerCommandsSetIntoViewModel()
        {
            var viewModel = CreateViewModel();

            Assert.AreSame(viewModel.BuyCommand, ordersController.BuyCommand);
            Assert.AreSame(viewModel.SellCommand, ordersController.SellCommand);
        }

        private PositionSummaryViewModel CreateViewModel()
        {
            return new PositionSummaryViewModel(ordersController, eventAggregator.Object, observablePosition);

        }

        [TestMethod]
        public void CurrentItemNullDoesNotThrow()
        {
            var model = CreateViewModel();

            model.CurrentPositionSummaryItem = null;
        }
    }

    internal class MockTickerSymbolSelectedEvent : TickerSymbolSelectedEvent
    {
        public bool PublishCalled;
        public string PublishArgumentPayload;


        public override void Publish(string payload)
        {
            PublishCalled = true;
            PublishArgumentPayload = payload;
        }
    }

    internal class MockObservablePosition : IObservablePosition
    {
        public ObservableCollection<PositionSummaryItem> Items { get; set; }
    }
}

/*
 * 
 * market update with volume change should be reflected in view model
 */
