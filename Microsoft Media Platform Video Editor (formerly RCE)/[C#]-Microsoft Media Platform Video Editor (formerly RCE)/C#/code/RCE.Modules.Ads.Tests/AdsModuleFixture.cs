// <copyright file="AdsModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AdsModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Ads.Tests
{
    using Infrastructure;

    using Microsoft.Practices.Composite.Presentation.Regions;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Services;
    using RCE.Modules.Ads.Views;

    /// <summary>
    /// Test class for <see cref="AdsModuleFixture"/>.
    /// </summary>
    [TestClass]
    public class AdsModuleFixture
    {
        /// <summary>
        /// Should register views.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableAdsModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(5, container.Types.Count);
            Assert.AreEqual(typeof(AdView), container.Types[typeof(IAdViewPreview)]);
            Assert.AreEqual(typeof(AdEditBox), container.Types[typeof(IAdEditBox)]);
            Assert.AreEqual(typeof(AdEditBoxPresentationModel), container.Types[typeof(IAdEditBoxPresentationModel)]);
            Assert.AreEqual(typeof(AdsListView), container.Types[typeof(IAdsListView)]);
            Assert.AreEqual(typeof(AdsListViewPresentationModel), container.Types[typeof(IAdsListViewPresentationModel)]);
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

            var module = new AdsModule(container, timelineBarRegistry, new MockRegionViewRegistry());

            Assert.IsFalse(timelineBarRegistry.RegisterTimelineBarElementCalled);

            module.Initialize();

            Assert.IsTrue(timelineBarRegistry.RegisterTimelineBarElementCalled);
            Assert.AreEqual("Ad", timelineBarRegistry.RegisterTimelineBarElementKeyArgument);
            Assert.IsNotNull(timelineBarRegistry.RegisterTimelineBarElementValueArgument);
        }

        /// <summary>
        /// Testable class for <see cref="AdsModule"/>.
        /// </summary>
        internal class TestableAdsModule : AdsModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableAdsModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableAdsModule(IUnityContainer container)
                : base(container, new MockTimelineBarRegistry(), new MockRegionViewRegistry())
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