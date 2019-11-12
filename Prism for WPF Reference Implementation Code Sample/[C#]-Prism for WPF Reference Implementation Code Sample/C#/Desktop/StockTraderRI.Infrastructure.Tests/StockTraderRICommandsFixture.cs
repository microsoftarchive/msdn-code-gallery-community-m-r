// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StockTraderRI.Infrastructure.Tests
{
    [TestClass]
    public class StockTraderRICommandsFixture
    {

        [TestMethod]
        public void ShouldSetAndGetStockTraderRiCommands()
        {
            var cancelAllOrdersCommand = new CompositeCommand();
            var cancelOrderCommand = new CompositeCommand();
            var submitAllOrdersCommand = new CompositeCommand();
            var submitOrderCommand = new CompositeCommand();
            StockTraderRICommands.CancelAllOrdersCommand = cancelAllOrdersCommand;
            StockTraderRICommands.CancelOrderCommand = cancelOrderCommand;
            StockTraderRICommands.SubmitAllOrdersCommand = submitAllOrdersCommand;
            StockTraderRICommands.SubmitOrderCommand = submitOrderCommand;
            
            Assert.AreEqual(cancelAllOrdersCommand, StockTraderRICommands.CancelAllOrdersCommand);
            Assert.AreEqual(cancelOrderCommand, StockTraderRICommands.CancelOrderCommand);
            Assert.AreEqual(submitAllOrdersCommand, StockTraderRICommands.SubmitAllOrdersCommand);
            Assert.AreEqual(submitOrderCommand, StockTraderRICommands.SubmitOrderCommand);

            var stockTraderRiCommandProxy = new StockTraderRICommandProxy();

            Assert.AreEqual(StockTraderRICommands.CancelAllOrdersCommand, stockTraderRiCommandProxy.CancelAllOrdersCommand);
            Assert.AreEqual(StockTraderRICommands.CancelOrderCommand, stockTraderRiCommandProxy.CancelOrderCommand);
            Assert.AreEqual(StockTraderRICommands.SubmitAllOrdersCommand, stockTraderRiCommandProxy.SubmitAllOrdersCommand);
            Assert.AreEqual(StockTraderRICommands.SubmitOrderCommand, stockTraderRiCommandProxy.SubmitOrderCommand);
        }
    }
}
