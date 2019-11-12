// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Models;
using StockTraderRI.Modules.Position.Services;

namespace StockTraderRI.Modules.Position.Tests
{
    [TestClass]
    public class AccountPositionServiceFixture
    {
        [TestMethod]
        public void ShouldReturnDefaultPositions()
        {
            AccountPositionService model = new AccountPositionService();

            Assert.IsTrue(model.GetAccountPositions().Count > 0, "No account positions returned");
        }
    }
}
