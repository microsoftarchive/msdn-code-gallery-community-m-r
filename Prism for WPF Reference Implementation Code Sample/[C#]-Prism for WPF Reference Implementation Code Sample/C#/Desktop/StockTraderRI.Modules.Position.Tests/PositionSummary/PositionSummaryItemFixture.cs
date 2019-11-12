// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.Position.PositionSummary;

namespace StockTraderRI.Modules.Position.Tests.PositionSummary
{
    /// <summary>
    /// Summary description for PositionSummaryItemFixture
    /// </summary>
    [TestClass]
    public class PositionSummaryItemFixture
    {
        [TestMethod]
        public void ChangingCurrentPriceFiresPropertyChangeNotificationEvent()
        {
            PositionSummaryItem positionSummary = new PositionSummaryItem("FUND0", 49.99M, 50, 52.99M);

            bool currentPriceChanged = false;
            positionSummary.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
                                                   {
                                                       if (e.PropertyName == "CurrentPrice")
                                                           currentPriceChanged = true;
                                                   };

            positionSummary.CurrentPrice -= 5;

            Assert.IsTrue(currentPriceChanged);
        }

        [TestMethod]
        public void ChangingCostBasisFiresPropertyChangeNotificationEvent()
        {
            PositionSummaryItem positionSummary = new PositionSummaryItem("FUND0", 49.99M, 50, 52.99M);

            bool costBasisPropertyChanged = false;
            positionSummary.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
                                                   {
                                                       if (e.PropertyName == "CostBasis")
                                                           costBasisPropertyChanged = true;
                                                   };

            positionSummary.CostBasis -= 5;

            Assert.IsTrue(costBasisPropertyChanged);
        }

        [TestMethod]
        public void ChangingSharesFiresPropertyChangeNotificationEvent()
        {
            PositionSummaryItem positionSummary = new PositionSummaryItem("FUND0", 49.99M, 50, 52.99M);

            bool sharesPropertyChanged = false;
            positionSummary.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
                                                   {
                                                       if (e.PropertyName == "Shares")
                                                           sharesPropertyChanged = true;
                                                   };

            positionSummary.Shares -= 5;

            Assert.IsTrue(sharesPropertyChanged);
        }

        [TestMethod]
        public void ChangingSymbolPropertyChangeNotificationEvent()
        {
            PositionSummaryItem positionSummary = new PositionSummaryItem("AAAA", 49.99M, 50, 52.99M);

            bool propertyChanged = false;
            string lastPropertyChanged = string.Empty;

            positionSummary.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
                                                   {
                                                       propertyChanged = true;
                                                       lastPropertyChanged = e.PropertyName;
                                                   };

            positionSummary.TickerSymbol = "XXXX";

            Assert.IsTrue(propertyChanged);
            Assert.AreEqual<string>("TickerSymbol", lastPropertyChanged);
        }

        [TestMethod]
        public void PositionSummaryCalculatesCurrentMarketValue()
        {
            decimal lastPrice = 52.99M;
            long numShares = 50;

            PositionSummaryItem positionSummary = new PositionSummaryItem("AAAA", 49.99M, numShares, lastPrice);

            Assert.AreEqual<decimal>(lastPrice * numShares, positionSummary.MarketValue);
        }

        [TestMethod]
        public void PositionSummaryCalculatesGainLossPercent()
        {
            decimal costBasis = 49.99M;
            decimal lastPrice = 52.99M;
            long numShares = 1000;

            PositionSummaryItem positionSummary = new PositionSummaryItem("AAAA", costBasis, numShares, lastPrice);

            Assert.AreEqual<decimal>(105901.2002M, Math.Round(positionSummary.GainLossPercent, 4));
        }

    }
}