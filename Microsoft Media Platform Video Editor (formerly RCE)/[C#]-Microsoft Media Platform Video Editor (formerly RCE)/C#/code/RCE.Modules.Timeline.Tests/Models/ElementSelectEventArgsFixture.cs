// <copyright file="ElementSelectEventArgsFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ElementSelectEventArgsFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Models
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure.Models;
    using Services.Contracts;
    using SMPTETimecode;

    /// <summary>
    /// A class for testing the <see cref="ElementSelectEventArgs"/>.
    /// </summary>
    [TestClass]
    public class ElementSelectEventArgsFixture
    {
        /// <summary>
        /// Tests that the timeline element can be retrieved.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheTimelineElement()
        {
            var element = new TimelineElement { Asset = new VideoAsset() };

            var eventArgs = new ElementSelectEventArgs { Element = element };

            Assert.AreEqual(element, eventArgs.Element);
        }

        /// <summary>
        /// Tests that the position can be retrieved.
        /// </summary>
        [TestMethod]
        public void ShouldGetThePosition()
        {
            var position = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);

            var eventArgs = new ElementSelectEventArgs { Position = position };

            Assert.AreEqual(position, eventArgs.Position);
        }
    }
}
