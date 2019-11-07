// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure;
using StockTraderRI.Modules.Position.Models;
using StockTraderRI.Modules.Position.Orders;
using StockTraderRI.Modules.Position.Services;
using StockTraderRI.Modules.Position.Tests.Mocks;

namespace StockTraderRI.Modules.Position.Tests.Services
{
    [TestClass]
    public class XmlOrdersServiceFixture
    {
        [TestMethod]
        public void SubmitSavesOrderToXml()
        {
            var logger = new MockLogger();
            var ordersService = new XmlOrdersService(logger);
            var document = new XDocument();

            var order = new Order
                              {
                                  OrderType = OrderType.Stop,
                                  Shares = 3,
                                  StopLimitPrice = 14,
                                  TickerSymbol = "TESTSTOCK",
                                  TimeInForce = TimeInForce.ThirtyDays,
                                  TransactionType = TransactionType.Buy
                              };

            ordersService.Submit(order, document);

            var root = document.Element("Orders");
            Assert.IsNotNull(root);
            Assert.AreEqual(1, root.Elements("Order").Count());
            var orderElement = root.Element("Order");
            Assert.IsNotNull(orderElement);
            Assert.IsTrue(orderElement.Attributes().Count() >= 6);
            Assert.AreEqual<string>(order.OrderType.ToString(), orderElement.Attribute("OrderType").Value);
            Assert.AreEqual<string>(order.Shares.ToString(), orderElement.Attribute("Shares").Value);
            Assert.AreEqual<string>(order.StopLimitPrice.ToString(CultureInfo.InvariantCulture), orderElement.Attribute("StopLimitPrice").Value);
            Assert.AreEqual<string>(order.TickerSymbol, orderElement.Attribute("TickerSymbol").Value);
            Assert.AreEqual<string>(order.TimeInForce.ToString(), orderElement.Attribute("TimeInForce").Value);
            Assert.AreEqual<string>(order.TransactionType.ToString(), orderElement.Attribute("TransactionType").Value);

            var dateElement = orderElement.Attribute("Date");
            Assert.IsNotNull(dateElement);
            var parsedDate = DateTime.Parse(dateElement.Value, CultureInfo.InvariantCulture);
            Assert.IsTrue(parsedDate < DateTime.Now.AddSeconds(1));
            Assert.IsTrue(parsedDate > DateTime.Now.AddSeconds(-10));
        }

        [TestMethod]
        public void SubmitLogsAnEntry()
        {
            var logger = new MockLogger();
            var ordersService = new XmlOrdersService(logger);
            var document = new XDocument();

            var order = new Order
            {
                OrderType = OrderType.Stop,
                Shares = 3,
                StopLimitPrice = 14,
                TickerSymbol = "TESTSTOCK",
                TimeInForce = TimeInForce.ThirtyDays,
                TransactionType = TransactionType.Buy
            };

            ordersService.Submit(order, document);

            StringAssert.Contains(logger.LastMessage, "An order has been submitted.");
            StringAssert.Contains(logger.LastMessage, "TESTSTOCK");
            StringAssert.Contains(logger.LastMessage, "3");
            StringAssert.Contains(logger.LastMessage, TransactionType.Buy.ToString());
        }
    }
}
