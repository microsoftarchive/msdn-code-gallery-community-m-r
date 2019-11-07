// <copyright file="CommentTypeToVisibilityConverterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentTypeToVisibilityConverterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Tests.Converters
{
    using System;
    using System.Windows;
    using Comment.Converters;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="CommentTypeToVisibilityConverter"/>.
    /// </summary>
    [TestClass]
    public class CommentTypeToVisibilityConverterFixture
    {
        /// <summary>
        /// Should convert ink comment type to visible.
        /// </summary>
        [TestMethod]
        public void ShouldConvertInkCommentTypeToVisible()
        {
            var converter = new CommentTypeToVisibilityConverter();
            var visibility = (Visibility)converter.Convert(CommentType.Ink, null, null, null);

            Assert.AreEqual(Visibility.Visible, visibility);
        }

        /// <summary>
        /// Should convert other comment type to collapsed.
        /// </summary>
        [TestMethod]
        public void ShouldConvertOtherCommentTypeToCollapsed()
        {
            var converter = new CommentTypeToVisibilityConverter();
            var visibility = (Visibility)converter.Convert(CommentType.Timeline, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, visibility);
        }

        /// <summary>
        /// Should convert null value.
        /// </summary>
        [TestMethod]
        public void ShouldConvertNullValue()
        {
            var converter = new CommentTypeToVisibilityConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Should convert non comment type to null.
        /// </summary>
        [TestMethod]
        public void ShouldConvertNotCommentTypeToNull()
        {
            var converter = new CommentTypeToVisibilityConverter();

            var result = converter.Convert(new object(), null, null, null);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Convertback returns exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBackShouldThrow()
        {
            var converter = new CommentTypeToVisibilityConverter();

            converter.ConvertBack(null, null, null, null);
        }
    }
}