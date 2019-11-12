// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.Practices.Prism.Commands;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.Position.Models;

namespace StockTraderRI.Modules.Position.Orders
{
    public interface IOrderDetailsViewModel
    {
        event EventHandler CloseViewRequested;  // TODO consider interaction request

        TransactionInfo TransactionInfo { get; set; }

        TransactionType TransactionType { get; }

        string TickerSymbol { get; }

        int? Shares { get; }

        decimal? StopLimitPrice { get; }

        DelegateCommand<object> SubmitCommand { get; }

        DelegateCommand<object> CancelCommand { get; }
    }
}
