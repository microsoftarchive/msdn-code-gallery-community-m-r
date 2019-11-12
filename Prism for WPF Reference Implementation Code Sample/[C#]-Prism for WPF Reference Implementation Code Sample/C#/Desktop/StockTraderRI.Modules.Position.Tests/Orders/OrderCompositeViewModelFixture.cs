// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Modules.Position.Models;
using StockTraderRI.Modules.Position.Orders;
using StockTraderRI.Modules.Position.Tests.Mocks;

namespace StockTraderRI.Modules.Position.Tests.Orders
{
    [TestClass]
    public class OrderCompositeViewModelFixture
    {
        [TestMethod]
        public void ShouldCreateOrderdetailsViewModel()
        {
            var detailsViewModel = new MockOrderDetailsViewModel();

            var composite = new OrderCompositeViewModel(detailsViewModel);

            composite.TransactionInfo = new TransactionInfo("FXX01", TransactionType.Sell);

            Assert.IsNotNull(detailsViewModel.TransactionInfo);
        }

        [TestMethod]
        public void ShouldAddDetailsViewAndControlsViewToContentArea()
        {
            var detailsViewModel = new MockOrderDetailsViewModel();

            var composite = new OrderCompositeViewModel(detailsViewModel);

            Assert.AreSame(detailsViewModel, composite.OrderDetails);
        }

        [TestMethod]
        public void ViewModelExposesChildOrderViewModelsCloseRequested()
        {
            var detailsViewModel = new MockOrderDetailsViewModel();

            var composite = new OrderCompositeViewModel(detailsViewModel);

            var closeViewRequestedFired = false;
            composite.CloseViewRequested += delegate
                                                {
                                                    closeViewRequestedFired = true;
                                                };

            detailsViewModel.RaiseCloseViewRequested();

            Assert.IsTrue(closeViewRequestedFired);

        }

        [TestMethod]
        public void TransactionInfoAndSharesAndCommandsAreTakenFromOrderDetails()
        {
            var orderDetailsPM = new MockOrderDetailsViewModel();
            var composite = new OrderCompositeViewModel(orderDetailsPM);
            orderDetailsPM.Shares = 100;

            Assert.AreEqual(orderDetailsPM.Shares, composite.Shares);
            Assert.AreSame(orderDetailsPM.SubmitCommand, composite.SubmitCommand);
            Assert.AreSame(orderDetailsPM.CancelCommand, composite.CancelCommand);
            Assert.AreSame(orderDetailsPM.TransactionInfo, composite.TransactionInfo);
        }

        // In the Silverlight version of the RI, header binding is done purely in the XAML separate textblocks (no MultiBinding available).
        [TestMethod]
        public void ShouldSetHeaderInfo()
        {
            var composite = new OrderCompositeViewModel(new MockOrderDetailsViewModel());

            composite.TransactionInfo = new TransactionInfo("FXX01", TransactionType.Sell);

            Assert.IsNotNull(composite.HeaderInfo);
            Assert.IsTrue(composite.HeaderInfo.Contains("FXX01"));
            Assert.IsTrue(composite.HeaderInfo.Contains("Sell"));
            Assert.AreEqual("Sell FXX01", composite.HeaderInfo);
        }

        [TestMethod]
        public void ShouldUpdateHeaderInfoWhenUpdatingTransactionInfo()
        {
            var orderDetailsPM = new MockOrderDetailsViewModel();
            var composite = new OrderCompositeViewModel(orderDetailsPM);

            composite.TransactionInfo = new TransactionInfo("FXX01", TransactionType.Sell);

            orderDetailsPM.TransactionInfo.TickerSymbol = "NEW_SYMBOL";
            Assert.AreEqual("Sell NEW_SYMBOL", composite.HeaderInfo);

            orderDetailsPM.TransactionInfo.TransactionType = TransactionType.Buy;
            Assert.AreEqual("Buy NEW_SYMBOL", composite.HeaderInfo);
        }
    }
}
