// <copyright file="MarkersModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MarkersModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers.Tests
{
    using Infrastructure;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Services;
    using RCE.Modules.Markers.Views;

    /// <summary>
    /// Test class for <see cref="MarkersModuleFixture"/>.
    /// </summary>
    [TestClass]
    public class MarkersModuleFixture
    {
        /// <summary>
        /// Should register views.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableMarkersModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(5, container.Types.Count);
            Assert.AreEqual(typeof(MarkerView), container.Types[typeof(IMarkerViewPreview)]);
            Assert.AreEqual(typeof(MarkerEditBox), container.Types[typeof(IMarkerEditBox)]);
            Assert.AreEqual(typeof(MarkerEditBoxPresentationModel), container.Types[typeof(IMarkerEditBoxPresentationModel)]);
            Assert.AreEqual(typeof(MarkersListView), container.Types[typeof(IMarkersListView)]);
            Assert.AreEqual(typeof(MarkersListViewModel), container.Types[typeof(IMarkersListViewModel)]);
        }

        /// <summary>
        /// Should register the ad timeline bar element to the timeline bar registry.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterAdTimelineBarElementToTimelineBarRegistry()
        {
            var timelineBarRegistry = new MockTimelineBarRegistry();
            var container = new MockUnityResolver();

            container.Bag.Add(typeof(ITimelineBarRegistry), timelineBarRegistry);

            var module = new MarkersModule(container, new MockRegionViewRegistry());

            Assert.IsFalse(timelineBarRegistry.RegisterTimelineBarElementCalled);

            module.Initialize();

            Assert.IsTrue(timelineBarRegistry.RegisterTimelineBarElementCalled);
            Assert.AreEqual("Marker", timelineBarRegistry.RegisterTimelineBarElementKeyArgument);
            Assert.IsNotNull(timelineBarRegistry.RegisterTimelineBarElementValueArgument);
        }

        /// <summary>
        /// Testable class for <see cref="MarkersModule"/>.
        /// </summary>
        internal class TestableMarkersModule : MarkersModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableMarkersModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableMarkersModule(IUnityContainer container)
                : base(container, new MockRegionViewRegistry())
            {
            }

            /// <summary>
            /// Invokes the register views and services.
            /// </summary>
            public void InvokeRegisterViewsAndServices()
            {
                this.RegisterViewsAndServices();
            }
        }
    }
}
