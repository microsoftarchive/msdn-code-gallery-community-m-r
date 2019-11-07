// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Modules.News.Services;

namespace StockTraderRI.Modules.News.Tests.Services
{
    [TestClass]
    public class NewsFeedServiceFixture
    {
        [TestMethod]
        public void HavingACurrentCultureDifferentThanEnglishShouldNotThrows()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-AR");

            NewsFeedService newsFeedService = new NewsFeedService();

            Thread.CurrentThread.CurrentCulture = currentCulture;
        }
    }
}
