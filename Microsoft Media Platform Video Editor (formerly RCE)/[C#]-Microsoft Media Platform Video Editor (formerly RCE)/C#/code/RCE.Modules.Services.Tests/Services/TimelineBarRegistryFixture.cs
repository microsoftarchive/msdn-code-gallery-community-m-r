// <copyright file="TimelineBarRegistryFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineBarRegistryFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Services
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    [TestClass]
    public class TimelineBarRegistryFixture
    {
        [TestMethod]
        public void ShouldRegisterTimelineBarElement()
        {
            var timelineBarElement = new MockTimelineBarElement();
            var timelineBarRegistry = new TimelineBarRegistry();

            timelineBarRegistry.RegisterTimelineBarElement("key", () => timelineBarElement);

            Assert.AreEqual(timelineBarElement, timelineBarRegistry.GetTimelineBarElement("key"));
        }

        [TestMethod]
        public void ShouldGetNullIfElementIsNotRegistered()
        {
            var timelineBarRegistry = new TimelineBarRegistry();

            var result = timelineBarRegistry.GetTimelineBarElement("key");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldGetAListOfRegisteredKeys()
        {
            var timelineBarRegistry = new TimelineBarRegistry();

            timelineBarRegistry.RegisterTimelineBarElement("key1", () => new MockTimelineBarElement());
            timelineBarRegistry.RegisterTimelineBarElement("key2", () => new MockTimelineBarElement());

            var result = timelineBarRegistry.GetTimelineBarElementKeys();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("key1", result[0]);
            Assert.AreEqual("key2", result[1]);
        }
    }
}
