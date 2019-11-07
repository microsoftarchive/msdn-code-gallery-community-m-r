// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Modules.Market.Tests.Mocks
{
    internal class MockMarketHistoryService : IMarketHistoryService
    {
        public bool GetPriceHistoryCalled;
        public string GetPriceHistoryArgument;
        public MarketHistoryCollection Data = new MarketHistoryCollection();

        public MockMarketHistoryService()
        {
            Data.Add(new MarketHistoryItem { DateTimeMarker = new DateTime(2008, 1, 1), Value = 10.00m });
            Data.Add(new MarketHistoryItem { DateTimeMarker = new DateTime(2008, 6, 1), Value = 15.00m });
        }

        public MarketHistoryCollection GetPriceHistory(string tickerSymbol)
        {
            GetPriceHistoryCalled = true;
            GetPriceHistoryArgument = tickerSymbol;

            return Data;
        }
    }
}
