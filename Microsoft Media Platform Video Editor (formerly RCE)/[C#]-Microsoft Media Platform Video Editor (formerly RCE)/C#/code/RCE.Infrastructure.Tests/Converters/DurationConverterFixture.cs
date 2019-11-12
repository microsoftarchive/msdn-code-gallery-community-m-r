// <copyright file="DurationConverterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DurationConverterFixture.cs                     
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

    using SMPTETimecode;

    /// <summary>
    /// Test class for <see cref="DurationConverter"/>.
    /// </summary>
    [TestClass]
    public class DurationConverterFixture
    {
        /// <summary>
        /// Should convert null duration.
        /// </summary>
        [TestMethod]
        public void ShouldConvertNullDuration()
        {
            var converter = new DurationConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.AreEqual("00:00:00", result);
        }

        /// <summary>
        /// Should convert time code to string.
        /// </summary>
        [TestMethod]
        public void ShouldConvertTimeCodeToString()
        {
            var time = new TimeCode(0, 30, 10, 0, SmpteFrameRate.Smpte2997NonDrop);

            var converter = new DurationConverter();

            var result = converter.Convert(time, null, null, null);

            Assert.AreEqual("00:30:10:00", result);
        }

        /// <summary>
        /// Should convert seconds to string.
        /// </summary>
        [TestMethod]
        public void ShouldConvertSecondsToString()
        {
            var time = 60;

            var converter = new DurationConverter();

            var result = converter.Convert(time, null, null, null);

            Assert.AreEqual("00:01:00", result);
        }

        /// <summary>
        /// Convert back should throw exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBackShouldThrow()
        {
            var converter = new DurationConverter();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
