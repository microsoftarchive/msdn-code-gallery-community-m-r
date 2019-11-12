// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Modules.Watch.Services;
using StockTraderRI.Modules.Watch.AddWatch;
using Moq;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Input;

namespace StockTraderRI.Modules.WatchList.Tests.AddWatch
{
    [TestClass]
    public class AddWatchViewModelFixture
    {
        [TestMethod]
        public void WhenConstructed_IntializesValues()
        {
            // Prepare
            ICommand addWatchCommand = new DelegateCommand(() => {});

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();
            mockWatchListService.SetupGet(x => x.AddWatchCommand).Returns(addWatchCommand);

            IWatchListService watchListService = mockWatchListService.Object;

            // Act
            AddWatchViewModel actual = new AddWatchViewModel(watchListService);

            // Verify
            Assert.IsNotNull(actual);
            Assert.AreEqual(addWatchCommand, actual.AddWatchCommand);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullWatchListService_Throws()
        {
            // Prepare
            IWatchListService watchListService = null;

            // Act
            AddWatchViewModel actual = new AddWatchViewModel(watchListService);
            
            // Verify
        }

        [TestMethod]
        public void WhenStockSymbolSet_PropertyIsUpdated()
        {
            // Prepare
            string stockSymbol = "StockSymbol";

            Mock<IWatchListService> mockWatchListService = new Mock<IWatchListService>();

            IWatchListService watchListService = mockWatchListService.Object;

            AddWatchViewModel target = new AddWatchViewModel(watchListService);

            bool propertyChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "StockSymbol")
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            target.StockSymbol = stockSymbol;

            // Verify
            Assert.AreEqual(stockSymbol, target.StockSymbol);
            Assert.IsTrue(propertyChangedRaised);
        }
    }
}
