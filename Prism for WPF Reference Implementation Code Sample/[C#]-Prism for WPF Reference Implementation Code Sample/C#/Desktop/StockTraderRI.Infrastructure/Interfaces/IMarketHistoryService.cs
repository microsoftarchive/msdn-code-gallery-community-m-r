// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Infrastructure.Interfaces
{
    public interface IMarketHistoryService
    {
        MarketHistoryCollection GetPriceHistory(string tickerSymbol);
    }
}
