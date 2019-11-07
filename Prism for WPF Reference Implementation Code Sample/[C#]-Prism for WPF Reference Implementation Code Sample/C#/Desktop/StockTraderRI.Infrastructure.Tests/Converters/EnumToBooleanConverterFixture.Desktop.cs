// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTraderRI.Infrastructure.Converters;

namespace StockTraderRI.Infrastructure.Tests.Converters
{
    [TestClass]
    public class EnumToBooleanConverterFixture
    {
        [TestMethod]
        public void EnumToBooleanConverterConverts()
        {
            EnumToBooleanConverter converter = new EnumToBooleanConverter();
            object value = converter.Convert(TransactionType.Buy, typeof(TransactionType), TransactionType.Buy, null);

            Assert.IsTrue((bool)value);
        }

        [TestMethod]
        public void EnumToBooleanConverterConvertsIfValueIsNull()
        {
            EnumToBooleanConverter converter = new EnumToBooleanConverter();
            object value = converter.Convert(null, typeof(TransactionType), TransactionType.Buy, null);

            Assert.IsFalse((bool)value);
        }

        [TestMethod]
        public void EnumToBooleanConverterConvertsBack()
        {
            EnumToBooleanConverter converter = new EnumToBooleanConverter();
            object value = converter.ConvertBack(true, typeof(TransactionType), TransactionType.Sell, null);

            Assert.AreEqual(TransactionType.Sell, value);
        }

        [TestMethod]
        public void EnumToBooleanConverterConvertsBackIfValueIsNull()
        {
            EnumToBooleanConverter converter = new EnumToBooleanConverter();
            object value = converter.ConvertBack(null, typeof(TransactionType), TransactionType.Sell, null);

            Assert.IsNull(value);
        }

        [TestMethod]
        public void EnumToBooleanConverterConvertsBackIfValueIsFalse()
        {
            EnumToBooleanConverter converter = new EnumToBooleanConverter();
            object value = converter.ConvertBack(false, typeof(TransactionType), TransactionType.Sell, null);

            Assert.IsNull(value);
        }
    }
}
