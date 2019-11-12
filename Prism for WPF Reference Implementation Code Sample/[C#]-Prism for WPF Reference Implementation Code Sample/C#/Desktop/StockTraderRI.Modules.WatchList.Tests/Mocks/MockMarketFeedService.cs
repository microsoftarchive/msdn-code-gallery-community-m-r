// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockTraderRI.Infrastructure.Interfaces;
using StockTraderRI.Infrastructure.Models;

namespace StockTraderRI.Modules.WatchList.Tests.Mocks
{
    public class MockMarketFeedService : IMarketFeedService
    {
        public bool MockSymbolExists = true;
        public string SymbolExistsArgumentTickerSymbol;

        public Dictionary<string, decimal> feedData = new Dictionary<string, decimal>();

        internal void SetPrice(string tickerSymbol, decimal price)
        {
            feedData.Add(tickerSymbol, price);
        }

        #region IMarketFeedService Members

        public decimal GetPrice(string tickerSymbol)
        {
            if (!MockSymbolExists)
                throw new ArgumentException();

            if (feedData.ContainsKey(tickerSymbol))
                return feedData[tickerSymbol];
            else
                return 0m;
        }

        public long GetVolume(string tickerSymbol)
        {
            throw new NotImplementedException();
        }

        public event EventHandler Updated = delegate { };

        #endregion

        public bool SymbolExists(string tickerSymbol)
        {
            SymbolExistsArgumentTickerSymbol = tickerSymbol;
            return MockSymbolExists;
        }
    }
}
