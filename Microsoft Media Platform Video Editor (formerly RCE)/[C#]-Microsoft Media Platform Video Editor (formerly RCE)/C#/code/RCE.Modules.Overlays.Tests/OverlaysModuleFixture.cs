// <copyright file="OverlaysModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlaysModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Overlays.Tests
{
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure;
    using RCE.Modules.Overlays.Tests.Mocks;
    using RCE.Modules.Overlays.Views;

    /// <summary>
    /// Test class for <see cref="TitlesModule"/>.
    /// </summary>
    [TestClass]
    public class OverlaysModuleFixture
    {
        /// <summary>
        /// Should register the views and models.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableOverlaysModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(2, container.Types.Count);
            Assert.AreEqual(typeof(OverlaysView), container.Types[typeof(IOverlaysView)]);
            Assert.AreEqual(typeof(OverlaysViewModel), container.Types[typeof(IOverlaysViewModel)]);
        }

        /// <summary>
        /// Should add <see cref="OverlaysView"/> to tools region.
        /// </summary>
        [TestMethod]
        public void ShouldAddTitlesViewToMainRegion()
        {
            var regionViewRegistry = new MockRegionViewRegistry();
            var container = new MockUnityResolver();

            var vm = new MockOverlaysViewModel();
            container.Bag.Add(typeof(IOverlaysViewModel), vm);

            var module = new OverlaysModule(container, regionViewRegistry);

            Assert.AreEqual(0, regionViewRegistry.ViewsByRegion.Count);

            module.Initialize();

            Assert.AreEqual(1, regionViewRegistry.ViewsByRegion.Count);
            Assert.AreSame(vm.View, regionViewRegistry.ViewsByRegion[RegionNames.AssetBrowserRegion]);
        }

        /// <summary>
        /// Testable class for <see cref="OverlaysModule"/>.
        /// </summary>
        private class TestableOverlaysModule : OverlaysModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableOverlaysModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableOverlaysModule(IUnityContainer container)
                : base(container, new MockRegionViewRegistry())
            {
            }

            /// <summary>
            /// Invokes the register views and services.
            /// </summary>
            public void InvokeRegisterViewsAndServices()
            {
                this.RegisterMappings();
            }
        }
    }
}