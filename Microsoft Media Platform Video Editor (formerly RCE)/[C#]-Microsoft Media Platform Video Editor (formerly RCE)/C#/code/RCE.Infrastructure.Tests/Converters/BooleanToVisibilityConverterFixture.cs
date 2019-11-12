// <copyright file="BooleanToVisibilityConverterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BooleanToVisibilityConverterFixture.cs                     
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
    using System.Windows;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure.Converters;

    /// <summary>
    /// Test class for <see cref="BooleanToVisibilityConverter"/>.
    /// </summary>
    [TestClass]
    public class BooleanToVisibilityConverterFixture
    {
        /// <summary>
        /// Should convert false to collapsed.
        /// </summary>
        [TestMethod]
        public void ShouldConvertFalseToCollapsed()
        {
            var converter = new BooleanToVisibilityConverter();
            var visibility = (Visibility)converter.Convert(false, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, visibility);
        }

        /// <summary>
        /// Should convert true to visible.
        /// </summary>
        [TestMethod]
        public void ShouldConvertTrueToVisible()
        {
            var converter = new BooleanToVisibilityConverter();
            var visibility = (Visibility)converter.Convert(true, null, null, null);

            Assert.AreEqual(Visibility.Visible, visibility);
        }

        /// <summary>
        /// Should convert null value.
        /// </summary>
        [TestMethod]
        public void ShouldConvertNullValue()
        {
            var converter = new BooleanToVisibilityConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Should convert back collapsed to false.
        /// </summary>
        [TestMethod]
        public void ShouldConvertBackCollapsedToFalse()
        {
            var converter = new BooleanToVisibilityConverter();
            var value = (bool)converter.ConvertBack(Visibility.Collapsed, null, null, null);

            Assert.AreEqual(false, value);
        }

        /// <summary>
        /// Should convert back visible to true.
        /// </summary>
        [TestMethod]
        public void ShouldConvertBackVisibleToTrue()
        {
            var converter = new BooleanToVisibilityConverter();
            var value = (bool)converter.ConvertBack(Visibility.Visible, null, null, null);

            Assert.AreEqual(true, value);
        }

        /// <summary>
        /// Should convert back null value.
        /// </summary>
        [TestMethod]
        public void ShouldConvertBackNullValue()
        {
            var converter = new BooleanToVisibilityConverter();
            var value = (bool)converter.ConvertBack(null, null, null, null);

            Assert.AreEqual(false, value);
        }
    }
}
