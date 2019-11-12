// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Converters;

namespace StockTraderRI.Infrastructure.Tests.Converters
{
    [TestClass]
    public class DecimalToColorConverterFixture
    {
        [TestMethod]
        public void ShouldConvertFromDecimalToColorString()
        {
            DecimalToColorConverter converter = new DecimalToColorConverter();

            var convertedValue = converter.Convert(20m, null, null, null) as string;
            Assert.IsNotNull(convertedValue);
            Assert.AreEqual("#ff00cc00", convertedValue);

            convertedValue = converter.Convert(-20m, null, null, null) as string;
            Assert.IsNotNull(convertedValue);
            Assert.AreEqual("#ffff0000", convertedValue);
        }

        [TestMethod]
        public void ShouldReturnNullIfValueToConvertIsNullOrNotDecimal()
        {
            DecimalToColorConverter converter = new DecimalToColorConverter();

            var convertedValue = converter.Convert(null, null, null, null) as string;
            Assert.IsNull(convertedValue);

            convertedValue = converter.Convert("NotADecimal", null, null, null) as string;
            Assert.IsNull(convertedValue);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ShouldThrowNotImplementedExceptionInConvertBack()
        {
            DecimalToColorConverter converter = new DecimalToColorConverter();

            var convertedValue = converter.ConvertBack(null, null, null, null);
        }
    }
}
