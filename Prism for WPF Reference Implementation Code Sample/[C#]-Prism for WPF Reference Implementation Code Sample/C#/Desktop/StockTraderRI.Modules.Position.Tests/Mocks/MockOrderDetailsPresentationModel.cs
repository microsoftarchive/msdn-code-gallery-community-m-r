// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.Practices.Prism.Commands;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.Position.Models;
using StockTraderRI.Modules.Position.Orders;

namespace StockTraderRI.Modules.Position.Tests.Mocks
{
    public class MockOrderDetailsViewModel : IOrderDetailsViewModel
    {
        public bool DisposeCalled;

        public event EventHandler CloseViewRequested;

        #region IDisposable Members

        public void Dispose()
        {
            DisposeCalled = true;
        }

        #endregion

        internal void RaiseCloseViewRequested()
        {
            CloseViewRequested(this, EventArgs.Empty);
        }

        public TransactionInfo TransactionInfo { get; set; }

        public TransactionType TransactionType { get; set; }

        public string TickerSymbol { get; set; }

        public int? Shares { get; set; }

        public decimal? StopLimitPrice { get; set; }

        public DelegateCommand<object> SubmitCommand { get; set; }

        public DelegateCommand<object> CancelCommand { get; set; }

        #region IOrderDetailsViewModel Members

        #endregion
    }
}
