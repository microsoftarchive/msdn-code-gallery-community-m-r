// <copyright file="TimeSpanConverterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimeSpanConverterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Converters
{
    using System;
    using Infrastructure.Converters;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="TimeSpanConverterFixture"/>.
    /// </summary>
    [TestClass]
    public class TimeSpanConverterFixture
    {
        /// <summary>
        /// Should convert time span to short representation string.
        /// </summary>
        [TestMethod]
        public void ShouldConvertTimeSpanToShortDateString()
        {
            var time = new TimeSpan(15, 10, 30);

            var converter = new TimeSpanConverter();

            var result = converter.Convert(time, null, null, null);

            Assert.AreEqual("15:10", result);

            time = new TimeSpan(1, 3, 15);

            result = converter.Convert(time, null, null, null);

            Assert.AreEqual("01:03", result);
        }

        /// <summary>
        /// Convert back should throw exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBackShouldThrow()
        {
            var converter = new TimeSpanConverter();

            converter.ConvertBack(null, null, null, null);
        }

        /// <summary>
        /// Should convert null value.
        /// </summary>
        [TestMethod]
        public void ShouldConvertNullValue()
        {
            var converter = new TimeSpanConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.IsNull(result);
        }
    }
}
