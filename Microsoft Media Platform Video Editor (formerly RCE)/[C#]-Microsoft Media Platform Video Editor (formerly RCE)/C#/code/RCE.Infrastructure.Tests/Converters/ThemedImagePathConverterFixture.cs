// <copyright file="ThemedImagePathConverterFixture.cs" company="Microsoft Corporation">
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure.Converters;

    [TestClass]
    public class ThemedImagePathConverterFixture
    {
        private const string InputPath = "/RCE.Modules.Library;component/images/library_icon_off.png";
        
        private const string WhiteThemePath = "/RCE.Modules.Library;component/images/Themes/White/library_icon_off.png";
        
        private const string BlackThemePath = "/RCE.Modules.Library;component/images/Themes/Black/library_icon_off.png";

        /// <summary>
        /// Should not convert path if param is null
        /// </summary>
        [TestMethod]
        public void ShouldNotConvertImagePathWithNullParameter()
        {
            var converter = new ThemedImagePathConverter();
            var result = converter.Convert(InputPath, null, null, null) as string;
            Assert.IsNotNull(result);
            Assert.AreEqual(result, InputPath);
        }

        /// <summary>
        /// Should not convert path if default
        /// </summary>
        [TestMethod]
        public void ShouldNotConvertImagePathWithDefaultParameter()
        {
            string parameter = "Default";
            var converter = new ThemedImagePathConverter();
            var result = converter.Convert(InputPath, null, parameter, null) as string;
            Assert.IsNotNull(result);
            Assert.AreEqual(result, InputPath);
        }

        /// <summary>
        /// Should convert path to white theme image path
        /// </summary>
        [TestMethod]
        public void ShouldConvertImagePathWithWhiteParameter()
        {
            string parameter = "White";
            var converter = new ThemedImagePathConverter();
            var result = converter.Convert(InputPath, null, parameter, null) as string;
            Assert.IsNotNull(result);
            Assert.AreEqual(result, WhiteThemePath);
        }

        /// <summary>
        /// Should convert path to black theme image path
        /// </summary>
        [TestMethod]
        public void ShouldConvertImagePathWithBlackParameter()
        {
            string parameter = "Black";
            var converter = new ThemedImagePathConverter();
            var result = converter.Convert(InputPath, null, parameter, null) as string;
            Assert.IsNotNull(result);
            Assert.AreEqual(result, BlackThemePath);
        }

        /// <summary>
        /// Convert back should throw exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBackShouldThrow()
        {
            var converter = new ThemedImagePathConverter();
            converter.ConvertBack(null, null, null, null);
        }
    }
}
