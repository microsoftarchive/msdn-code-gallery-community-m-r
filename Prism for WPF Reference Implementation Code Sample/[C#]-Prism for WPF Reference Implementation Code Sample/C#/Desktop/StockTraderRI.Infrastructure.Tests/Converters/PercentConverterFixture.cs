// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Converters;

namespace StockTraderRI.Infrastructure.Tests.Converters
{
 
    [TestClass]
    public class PercentConverterFixture
    {
        [TestMethod]
        public void ShouldConvertDecimalToPercent()
        {
            PercentConverter converter = new PercentConverter();
            decimal? numDecimal = 20;

            var convertedValue = converter.Convert(numDecimal, typeof(decimal?), null, null) as string;

            Assert.AreEqual("20.0%", convertedValue);

            convertedValue = converter.Convert(null, null, null, null) as string;

            Assert.AreEqual("0.0%", convertedValue);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ShouldThrowNotImplementedExceptionInConvertBack()
        {
            PercentConverter converter = new PercentConverter();

            var convertedValue = converter.ConvertBack(null, null, null, null);
        }
    }
}
