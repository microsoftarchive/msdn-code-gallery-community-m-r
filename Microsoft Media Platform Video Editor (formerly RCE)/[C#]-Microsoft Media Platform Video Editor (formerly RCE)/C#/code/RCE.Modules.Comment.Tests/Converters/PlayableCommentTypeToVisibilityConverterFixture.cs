// <copyright file="PlayableCommentTypeToVisibilityConverterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayableCommentTypeToVisibilityConverterFixture.cs                     
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
    /// Test class for <see cref="PlayableCommentTypeToVisibilityConverter"/>.
    /// </summary>
    [TestClass]
    public class PlayableCommentTypeToVisibilityConverterFixture
    {
        /// <summary>
        /// Should return null if value is null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullIfValueIsNull()
        {
            var converter = new PlayableCommentTypeToVisibilityConverter();

            var result = converter.Convert(null, null, null, null);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Should return null if value is not of comment.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullIfValueIsNotCommentType()
        {
            var converter = new PlayableCommentTypeToVisibilityConverter();

            var result = converter.Convert(new object(), null, null, null);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Should return collapsed if comment type is global.
        /// </summary>
        [TestMethod]
        public void ShouldReturnCollapsedIfCommentTypeIsGlobal()
        {
            var converter = new PlayableCommentTypeToVisibilityConverter();

            var result = converter.Convert(CommentType.Global, null, null, null);

            Assert.AreEqual(Visibility.Collapsed, result);
        }

        /// <summary>
        /// Should return visible if comment type is not global.
        /// </summary>
        [TestMethod]
        public void ShouldReturnVisibleIfCommentTypeIsNotGlobal()
        {
            var converter = new PlayableCommentTypeToVisibilityConverter();

            var result = converter.Convert(CommentType.Ink, null, null, null);

            Assert.AreEqual(Visibility.Visible, result);
        }

        /// <summary>
        /// Convertback should throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ConvertBackShouldThrow()
        {
            var converter = new PlayableCommentTypeToVisibilityConverter();

            converter.ConvertBack(null, null, null, null);
        }
    }
}
