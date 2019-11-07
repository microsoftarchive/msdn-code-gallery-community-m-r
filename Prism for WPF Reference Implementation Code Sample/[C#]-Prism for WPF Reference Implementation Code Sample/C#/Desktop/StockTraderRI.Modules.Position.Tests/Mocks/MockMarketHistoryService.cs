// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Modules.Position.Tests.Mocks
{
    public class MockMarketHistoryService : IMarketHistoryService
    {
        #region IMarketHistoryService Members

        public MarketHistoryCollection GetPriceHistory(string tickerSymbol)
        {
            //return new MarketHistoryCollection { 
            //    new MarketHistoryItem { DateTimeMarker = new DateTime(1), Value = 1.00m }
            //    , new MarketHistoryItem { DateTimeMarker = new DateTime(2), Value =  2.00m}
            //};

            throw new NotImplementedException();
        }

        #endregion
    }
}
