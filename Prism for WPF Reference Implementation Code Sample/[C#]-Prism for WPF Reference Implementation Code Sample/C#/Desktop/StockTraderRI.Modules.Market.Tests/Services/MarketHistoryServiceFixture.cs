// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Modules.Market.Services;

namespace StockTraderRI.Modules.Market.Tests.Services
{

    [TestClass]
    public class MarketHistoryServiceFixture
    {
        [TestMethod]
        public void ShouldInitializeMarketHistoryService()
        {
            var marketHistoryService = new MarketHistoryService();
            Assert.IsNotNull(marketHistoryService);
            var priceHistory = marketHistoryService.GetPriceHistory("STOCK2");
            Assert.IsNotNull(priceHistory);
            Assert.AreEqual(5, priceHistory.Count);
        }

    }
}
