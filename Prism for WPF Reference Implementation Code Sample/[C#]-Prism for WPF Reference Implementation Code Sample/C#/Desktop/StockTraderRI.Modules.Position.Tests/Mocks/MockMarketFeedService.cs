// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using StockTraderRI.Infrastructure.Interfaces;

namespace StockTraderRI.Modules.Position.Tests.Mocks
{
    public class MockMarketFeedService : IMarketFeedService
    {

        Dictionary<string, decimal> feedData = new Dictionary<string, decimal>();

        internal void SetPrice(string tickerSymbol, decimal price)
        {
            feedData.Add(tickerSymbol, price);
        }


        internal void UpdatePrice(string tickerSymbol, decimal newPrice, long volume)
        {
            feedData[tickerSymbol] = newPrice;
        }

        #region IMarketFeedService Members

        public decimal GetPrice(string tickerSymbol)
        {
            if (feedData.ContainsKey(tickerSymbol))
                return feedData[tickerSymbol];
            return 0m;
        }

        public long GetVolume(string tickerSymbol)
        {
            throw new NotImplementedException();
        }

        #endregion

        public bool SymbolExists(string tickerSymbol)
        {
            throw new NotImplementedException();
        }
    }
}
