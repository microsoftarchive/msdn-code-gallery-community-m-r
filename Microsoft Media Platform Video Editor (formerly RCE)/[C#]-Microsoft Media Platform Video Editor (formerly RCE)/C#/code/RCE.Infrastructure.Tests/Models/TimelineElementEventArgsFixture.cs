// <copyright file="TimelineElementEventArgsFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineElementEventArgsFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Models
{
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for <see cref="TimelineElementEventArgs"/>.
    /// </summary>
    [TestClass]
    public class TimelineElementEventArgsFixture
    {
        /// <summary>
        /// Should get timeline element.
        /// </summary>
        [TestMethod]
        public void ShouldGetTimelineElement()
        {
            var timelineElement = new TimelineElement { Asset = new VideoAsset() };

            var timelineElementEventArgs = new TimelineElementEventArgs(timelineElement);

            Assert.AreEqual(timelineElement, timelineElementEventArgs.Element);
        }
    }
}
