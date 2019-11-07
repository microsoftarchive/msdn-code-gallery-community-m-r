// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


namespace StockTraderRI.Infrastructure.Interfaces
{
    public interface IMarketFeedService
    {
        decimal GetPrice(string tickerSymbol);
        long GetVolume(string tickerSymbol);
        bool SymbolExists(string tickerSymbol);
    }
}
