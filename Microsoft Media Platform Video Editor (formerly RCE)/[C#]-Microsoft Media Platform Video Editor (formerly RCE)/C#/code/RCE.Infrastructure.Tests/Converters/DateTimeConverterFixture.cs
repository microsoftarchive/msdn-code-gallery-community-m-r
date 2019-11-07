// <copyright file="DateTimeConverterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DateTimeConverterFixture.cs                     
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
    /// Test class for <see cref="DateTimeConverter"/>.
    /// </summary>
    [TestClass]
    public class DateTimeConverterFixture
    {
        /// <summary>
        /// Should convert date time to short date string.
        /// </summary>
        [TestMethod]
        public void ShouldConvertDateTimeToShortDateString()
        {
            var dateTime = new DateTime(2008, 12, 3);

            var converter = new DateTimeConverter();

            var result = converter.Convert(dateTime, null, null, null);

            Assert.AreEqual(DateTime.Parse("2008-12-03").ToShortDateString(), result);
        }

        /// <summary>
        /// Convert back should throw exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBackShouldThrow()
        {
            var converter = new DateTimeConverter();

            converter.ConvertBack(null, null, null, null);
        }

        /// <summary>
        /// Should convert null date time.
        /// </summary>
        [TestMethod]
        public void ShouldConvertNullDateTime()
        {
            var converter = new DateTimeConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.IsNull(result);
        }
    }
}
