// <copyright file="ElementLinkEventArgsFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ElementLinkEventArgsFixture.cs                     
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
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// A class for testing the <see cref="ElementLinkEventArgs"/>.
    /// </summary>
    [TestClass]
    public class ElementLinkEventArgsFixture
    {
        /// <summary>
        /// Tests that the the element passed via constructor can be retrieved used the Element property.
        /// </summary>
        [TestMethod]
        public void ShouldGetTimelineElement()
        {
            var timelineElement = new TimelineElement { Asset = new VideoAsset() };
            var eventArgs = new ElementLinkEventArgs(timelineElement);

            Assert.AreEqual(timelineElement, eventArgs.Element);
        }
    }
}
