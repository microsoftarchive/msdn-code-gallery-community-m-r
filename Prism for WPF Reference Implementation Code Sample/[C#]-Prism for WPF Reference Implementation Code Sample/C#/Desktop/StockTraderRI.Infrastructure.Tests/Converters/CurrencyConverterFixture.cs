// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Converters;

namespace StockTraderRI.Infrastructure.Tests.Converters
{
 
    [TestClass]
    public class CurrencyConverterFixture
    {
        [TestMethod]
        public void ShouldConvertDecimalToCurrency()
        {
            CurrencyConverter converter = new CurrencyConverter();
            decimal? numDecimal = 20;

            var convertedValue = converter.Convert(numDecimal, typeof(decimal?), null, null) as string;

            Assert.AreEqual("$20.00", convertedValue);

            convertedValue = converter.Convert(null, null, null, null) as string;

            Assert.AreEqual("$0.00", convertedValue);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ShouldThrowNotImplementedExceptionInConvertBack()
        {
            CurrencyConverter converter = new CurrencyConverter();

            var convertedValue = converter.ConvertBack(null, null, null, null);
        }
    }
}
