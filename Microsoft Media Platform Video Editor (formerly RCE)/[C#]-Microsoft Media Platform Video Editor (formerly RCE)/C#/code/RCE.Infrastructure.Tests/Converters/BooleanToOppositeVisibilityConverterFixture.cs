// <copyright file="BooleanToOppositeVisibilityConverterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BooleanToOppositeVisibilityConverterFixture.cs                     
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
    using System.Windows;
    using Infrastructure.Converters;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="BooleanToOppositeVisibilityConverterFixture"/>.
    /// </summary>
    [TestClass]
    public class BooleanToOppositeVisibilityConverterFixture
    {
        /// <summary>
        /// Should convert false to visible.
        /// </summary>
        [TestMethod]
        public void ShouldConvertFalseToVisible()
        {
            var converter = new BooleanToOppositeVisibilityConverter();
            var visibility = (Visibility)converter.Convert(false, null, null, null);

            Assert.AreEqual(Visibility.Visible, visibility);
        }

        /// <summary>
        /// Should convert true to collapsed.
        /// </summary>
        [TestMethod]
        public void ShouldConvertTrueToCollapsed()
        {
            var converter = new BooleanToOppositeVisibilityConverter();
            var visibility = (Visibility)converter.Convert(true, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, visibility);
        }

        /// <summary>
        /// Should convert null value.
        /// </summary>
        [TestMethod]
        public void ShouldConvertNullValue()
        {
            var converter = new BooleanToOppositeVisibilityConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Converts back should throw exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBackShouldThrow()
        {
            var converter = new BooleanToOppositeVisibilityConverter();

            converter.ConvertBack(null, null, null, null);
        }
    }
}