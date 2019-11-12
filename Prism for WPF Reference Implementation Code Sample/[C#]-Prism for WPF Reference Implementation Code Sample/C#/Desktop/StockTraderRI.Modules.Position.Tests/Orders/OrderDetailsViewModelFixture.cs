// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.Position.Interfaces;
using StockTraderRI.Modules.Position.Models;
using StockTraderRI.Modules.Position.Orders;
using StockTraderRI.Modules.Position.Tests.Mocks;
using System.Threading.Tasks;

namespace StockTraderRI.Modules.Position.Tests.Orders
{
    [TestClass]
    public class OrderDetailsViewModelFixture
    {
        [TestMethod]
        public void ViewModelCreatesPublicSubmitCommand()
        {
            var viewModel = CreateViewModel(null);

            Assert.IsNotNull(viewModel.SubmitCommand);
        }

        [TestMethod]
        public void CanExecuteChangedIsRaisedForSubmitCommandWhenModelBecomesValid()
        {
            bool canExecuteChanged = false;
            var viewModel = CreateViewModel(null);
            viewModel.SubmitCommand.CanExecuteChanged += delegate { canExecuteChanged = true; };
            viewModel.Shares = 2;
            canExecuteChanged = false;

            viewModel.StopLimitPrice = 2;

            Assert.IsTrue(canExecuteChanged);
        }

        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void NonPositiveSharesThrows()
        {
            var viewModel = CreateViewModel(null);

            viewModel.Shares = 0;
        }

        [TestMethod]
        public void CannotSubmitWhenSharesIsNotPositive()
        {
            var viewModel = CreateViewModel(null);

            viewModel.Shares = 2;
            viewModel.StopLimitPrice = 2;
            Assert.IsTrue(viewModel.SubmitCommand.CanExecute(null));

            try
            {
                viewModel.Shares = 0;
            }
            catch (InputValidationException) { }

            Assert.IsFalse(viewModel.SubmitCommand.CanExecute(null));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task SubmitThrowsIfCanExecuteIsFalse()
        {
            var viewModel = CreateViewModel(new MockAccountPositionService());
            try
            {
                viewModel.Shares = 0;
            }
            catch (InputValidationException) { }

            Assert.IsFalse(viewModel.SubmitCommand.CanExecute(null));

            await viewModel.SubmitCommand.Execute(null);
        }

        [TestMethod]
        public void CancelRaisesCloseViewEvent()
        {
            bool closeViewRaised = false;

            var viewModel = CreateViewModel(null);
            viewModel.CloseViewRequested += delegate
            {
                closeViewRaised = true;
            };

            viewModel.CancelCommand.Execute(null);

            Assert.IsTrue(closeViewRaised);
        }

        [TestMethod]
        public void SubmitRaisesCloseViewEvent()
        {
            bool closeViewRaised = false;

            var viewModel = CreateViewModel(new MockAccountPositionService());
            viewModel.CloseViewRequested += delegate
            {
                closeViewRaised = true;
            };

            viewModel.TransactionType = TransactionType.Buy;
            viewModel.Shares = 1;
            viewModel.StopLimitPrice = 1;
            Assert.IsTrue(viewModel.SubmitCommand.CanExecute(null));
            viewModel.SubmitCommand.Execute(null);

            Assert.IsTrue(closeViewRaised);
        }

        [TestMethod]
        public void CannotSubmitOnSellWhenSharesIsHigherThanCurrentPosition()
        {
            var accountPositionService = new MockAccountPositionService();
            accountPositionService.AddPosition(new AccountPosition("TESTFUND", 10m, 15));
            var viewModel = CreateViewModel(accountPositionService);

            viewModel.TickerSymbol = "TESTFUND";
            viewModel.TransactionType = TransactionType.Sell;
            viewModel.Shares = 5;
            viewModel.StopLimitPrice = 1;
            Assert.IsTrue(viewModel.SubmitCommand.CanExecute(null));

            try
            {
                viewModel.Shares = 16;
            }
            catch (InputValidationException) { }
            Assert.IsFalse(viewModel.SubmitCommand.CanExecute(null));
        }

        [TestMethod]
        public void SharesIsHigherThanCurrentPositionOnSellThrows()
        {
            var accountPositionService = new MockAccountPositionService();
            accountPositionService.AddPosition(new AccountPosition("TESTFUND", 10m, 15));
            var viewModel = CreateViewModel(accountPositionService);

            viewModel.TickerSymbol = "TESTFUND";
            viewModel.TransactionType = TransactionType.Sell;

            try
            {
                viewModel.Shares = 16;
            }
            catch (InputValidationException)
            {
                return;
            }
            Assert.Fail("Exception not thrown.");
        }

        [TestMethod]
        public void ViewModelCreatesCallSetOrderTypes()
        {
            var viewModel = new OrderDetailsViewModel(null, null);

            Assert.IsNotNull(viewModel.AvailableOrderTypes);
            Assert.IsTrue(viewModel.AvailableOrderTypes.Count > 0);
            Assert.AreEqual(GetEnumCount(typeof(OrderType)), viewModel.AvailableOrderTypes.Count);
        }

        [TestMethod]
        public void ViewModelCreatesCallSetTimeInForce()
        {
            var viewModel = new OrderDetailsViewModel(null, null);
            Assert.IsNotNull(viewModel.AvailableTimesInForce);
            Assert.IsTrue(viewModel.AvailableTimesInForce.Count > 0);
            Assert.AreEqual(GetEnumCount(typeof(TimeInForce)), viewModel.AvailableTimesInForce.Count);
        }

        [TestMethod]
        public void SetTransactionInfoShouldUpdateTheModel()
        {
            var viewModel = new OrderDetailsViewModel(new MockAccountPositionService(), null);
            viewModel.TransactionInfo = new TransactionInfo { TickerSymbol = "T000", TransactionType = TransactionType.Sell };

            Assert.AreEqual("T000", viewModel.TickerSymbol);
            Assert.AreEqual(TransactionType.Sell, viewModel.TransactionType);
        }

        [TestMethod]
        public void SubmitCallsServiceWithCorrectOrder()
        {
            var ordersService = new MockOrdersService();
            var viewModel = new OrderDetailsViewModel(new MockAccountPositionService(), ordersService);
            viewModel.Shares = 2;
            viewModel.TickerSymbol = "AAAA";
            viewModel.TransactionType = TransactionType.Buy;
            viewModel.TimeInForce = TimeInForce.EndOfDay;
            viewModel.OrderType = OrderType.Limit;
            viewModel.StopLimitPrice = 15;

            Assert.IsNull(ordersService.SubmitArgumentOrder);
            viewModel.SubmitCommand.Execute(null);

            var submittedOrder = ordersService.SubmitArgumentOrder;
            Assert.IsNotNull(submittedOrder);
            Assert.AreEqual("AAAA", submittedOrder.TickerSymbol);
            Assert.AreEqual(TransactionType.Buy, submittedOrder.TransactionType);
            Assert.AreEqual(TimeInForce.EndOfDay, submittedOrder.TimeInForce);
            Assert.AreEqual(OrderType.Limit, submittedOrder.OrderType);
            Assert.AreEqual(15, submittedOrder.StopLimitPrice);
        }

        [TestMethod]
        public void VerifyTransactionInfoModificationsInOrderDetails()
        {
            var orderDetailsViewModel = new OrderDetailsViewModel(new MockAccountPositionService(), null);
            var transactionInfo = new TransactionInfo { TickerSymbol = "Fund0", TransactionType = TransactionType.Buy };
            orderDetailsViewModel.TransactionInfo = transactionInfo;
            orderDetailsViewModel.TransactionType = TransactionType.Sell;
            Assert.AreEqual(TransactionType.Sell, transactionInfo.TransactionType);

            orderDetailsViewModel.TickerSymbol = "Fund1";
            Assert.AreEqual("Fund1", transactionInfo.TickerSymbol);
        }

        [TestMethod]
        public void CannotSubmitIfStopLimitZero()
        {
            var accountPositionService = new MockAccountPositionService();
            accountPositionService.AddPosition(new AccountPosition("TESTFUND", 10m, 15));
            var viewModel = CreateViewModel(accountPositionService);

            viewModel.TickerSymbol = "TESTFUND";
            viewModel.TransactionType = TransactionType.Sell;
            viewModel.Shares = 5;
            viewModel.StopLimitPrice = 1;
            Assert.IsTrue(viewModel.SubmitCommand.CanExecute(null));

            try
            {
                viewModel.StopLimitPrice = 0;
            }
            catch (InputValidationException) { }

            Assert.IsFalse(viewModel.SubmitCommand.CanExecute(null));
        }

        [TestMethod]
        public void PropertyChangedIsRaisedWhenSharesIsChanged()
        {
            var viewModel = new OrderDetailsViewModel(null, null);
            viewModel.Shares = 5;

            bool sharesPropertyChangedRaised = false;
            viewModel.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Shares")
                    sharesPropertyChangedRaised = true;
            };
            viewModel.Shares = 1;
            Assert.IsTrue(sharesPropertyChangedRaised);
        }

        private static int GetEnumCount(Type enumType)
        {
            Array availableOrderTypes;
            availableOrderTypes = Enum.GetValues(enumType);

            return availableOrderTypes.Length;
        }

        private static OrderDetailsViewModel CreateViewModel(IAccountPositionService accountPositionService)
        {
            return new OrderDetailsViewModel(accountPositionService, new MockOrdersService());
        }
    }

    internal class MockOrdersService : IOrdersService
    {
        public Order SubmitArgumentOrder;

        public void Submit(Order order)
        {
            SubmitArgumentOrder = order;
        }
    }

}
