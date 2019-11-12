// <copyright file="ImageUriConverterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ImageUriConverterFixture.cs                     
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
    using System.Windows.Media.Imaging;
    using Infrastructure.Converters;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="ImageUriConverter"/>.
    /// </summary>
    [TestClass]
    public class ImageUriConverterFixture
    {
        /// <summary>
        /// Should convert time span to string.
        /// </summary>
        [TestMethod]
        public void ShouldConvertTimeSpanToString()
        {
            var uri = new Uri("http://www.microsoft.com");

            var converter = new ImageUriConverter();

            var result = converter.Convert(uri, null, null, null) as BitmapImage;

            Assert.IsNotNull(result);
            Assert.AreEqual(uri, result.UriSource);
        }

        /// <summary>
        /// Convert back should throw exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBackShouldThrow()
        {
            var converter = new ImageUriConverter();

            converter.ConvertBack(null, null, null, null);
        }

        /// <summary>
        /// Should convert null URI.
        /// </summary>
        [TestMethod]
        public void ShouldConvertNullUri()
        {
            var converter = new ImageUriConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.IsNull(result);
        }
    }
}
