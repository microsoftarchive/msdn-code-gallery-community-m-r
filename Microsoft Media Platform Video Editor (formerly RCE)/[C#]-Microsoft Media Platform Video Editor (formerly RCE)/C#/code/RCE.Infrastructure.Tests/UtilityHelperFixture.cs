// <copyright file="UtilityHelperFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UtilityHelperFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests
{
    using System;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="UtilityHelper"/>.
    /// </summary>
    [TestClass]
    public class UtilityHelperFixture
    {
        /// <summary>
        /// Should return false if property does not exists on existing asset types.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseIfPropertyDoesNotExistsOnExistingAssetTypes()
        {
            var result = UtilityHelper.IsMetadataFieldExist("FieldNotExists");

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Should return true if property exists on video asset.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueIfPropertyExistsOnVideoAsset()
        {
            var result = UtilityHelper.IsMetadataFieldExist("FrameRate");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Should return true if property exists on audio asset.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueIfPropertyExistsOnAudioAsset()
        {
            var result = UtilityHelper.IsMetadataFieldExist("Duration");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Should return true if property exists on image asset.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueIfPropertyExistsOnImageAsset()
        {
            var result = UtilityHelper.IsMetadataFieldExist("Height");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Should return max size if height is 0.
        /// </summary>
        [TestMethod]
        public void ShouldReturnMaxSizeIfHeightIs0()
        {
            var result = UtilityHelper.GetBestFitSizeMaintainingAspectRatio(200, 150, 100, 0);

            Assert.AreEqual(200, result.Width);
            Assert.AreEqual(150, result.Height);
        }

        /// <summary>
        /// Should return max size if width is 0.
        /// </summary>
        [TestMethod]
        public void ShouldReturnMaxSizeIfWidthIs0()
        {
            var result = UtilityHelper.GetBestFitSizeMaintainingAspectRatio(200, 150, 0, 100);

            Assert.AreEqual(200, result.Width);
            Assert.AreEqual(150, result.Height);
        }

        /// <summary>
        /// Should return the duration of the return double click.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDoubleClickDuration()
        {
            var result = UtilityHelper.MouseDoubleClickDurationValue;

            Assert.AreEqual(5000000, result);
        }

        /// <summary>
        /// Tests that a 16:9 size is returned when selected aspect ratio is Wide.
        /// </summary>
        [TestMethod]
        public void ShouldReturnAWideSizeIfTheAspectRatioValuePassedIsWide()
        {
            var result = UtilityHelper.GetSelectedAspectRatio(AspectRatio.Wide);

            Assert.AreEqual(16, result.Width);
            Assert.AreEqual(9, result.Height);
        }

        /// <summary>
        /// Tests that a 4:3 size is returned when selected aspect ratio is Square.
        /// </summary>
        [TestMethod]
        public void ShouldReturnASquareSizeIfTheAspectRatioValuePassedIsWide()
        {
            var result = UtilityHelper.GetSelectedAspectRatio(AspectRatio.Square);

            Assert.AreEqual(4, result.Width);
            Assert.AreEqual(3, result.Height);
        }

        /// <summary>
        /// Tests that the exception message is being formmatted.
        /// </summary>
        [TestMethod]
        public void ShouldFormatExceptionMessage()
        {
            string exceptionMessage = null;
            string innerExceptionMessage = null;
            string innerExceptionStackTrace = null;

            try
            {
                var zero = 0;
                var result = 1 / zero;
            }
            catch (Exception ex)
            {
                Exception e = new Exception("RootMessage", ex);
                innerExceptionMessage = ex.Message;
                innerExceptionStackTrace = ex.StackTrace;
                exceptionMessage = UtilityHelper.FormatExceptionMessage(e);
            }

            Assert.IsNotNull(exceptionMessage);
            StringAssert.Contains(exceptionMessage, "Exception:");
            StringAssert.Contains(exceptionMessage, "RootMessage");
            StringAssert.Contains(exceptionMessage, "Stack Trace:");
            StringAssert.Contains(exceptionMessage, "Inner Exception:");
            StringAssert.Contains(exceptionMessage, innerExceptionMessage);
            StringAssert.Contains(exceptionMessage, "Inner Stack Trace:");
            StringAssert.Contains(exceptionMessage, innerExceptionStackTrace);
        }
    }
}
