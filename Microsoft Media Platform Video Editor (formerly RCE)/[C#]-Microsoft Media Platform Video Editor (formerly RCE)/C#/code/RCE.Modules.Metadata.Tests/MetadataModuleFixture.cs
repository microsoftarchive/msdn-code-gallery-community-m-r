// <copyright file="MetadataModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Tests
{
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure;
    using RCE.Modules.Metadata.Views;

    /// <summary>
    /// A class for testing the <see cref="MetadataModule"/>.
    /// </summary>
    [TestClass]
    public class MetadataModuleFixture
    {
        /// <summary>
        /// Tests that the views are being registered in the container.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableMetadataModuleModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(8, container.Types.Count);
            Assert.AreEqual(typeof(ClipMetadataView), container.Types[typeof(IClipMetadataView)]);
            Assert.AreEqual(typeof(ClipMetadataViewPresentationModel), container.Types[typeof(IClipMetadataViewPresentationModel)]);
            Assert.AreEqual(typeof(SequenceMetadataViewModel), container.Types[typeof(ISequenceMetadataViewModel)]);
            Assert.AreEqual(typeof(SequenceMetadataView), container.Types[typeof(ISequenceMetadataView)]);
            Assert.AreEqual(typeof(OverlayMetadataView), container.Types[typeof(IOverlayMetadataView)]);
            Assert.AreEqual(typeof(OverlayMetadataViewPresentationModel), container.Types[typeof(IOverlayMetadataViewPresentationModel)]);
            Assert.AreEqual(typeof(TransitionsMetadataView), container.Types[typeof(ITransitionsMetadataView)]);
            Assert.AreEqual(typeof(TransitionsMetadataViewPresentationModel), container.Types[typeof(ITransitionsMetadataViewPresentationModel)]);
        }

        /// <summary>
        /// Tests that the <see cref="ClipMetadataView"/> is being added to the <seealso cref="IRegion">Metadata Region</seealso>.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViewsInRegions()
        {
            var regionViewRegistry = new MockRegionViewRegistry();
            var container = new MockUnityResolver();

            var clipMetadataViewModel = new MockClipMetadataViewPresentationModel();
            var sequenceMetadataViewModel = new MockSequenceMetadataViewModel();
            var overlayMetadataViewModel = new MockOverlayMetadataViewPresentationModel();
            var transitionsMetadataViewModel = new MockTransitionsMetadataViewPresentationModel();
            
            container.Bag.Add(typeof(IClipMetadataViewPresentationModel), clipMetadataViewModel);
            container.Bag.Add(typeof(ISequenceMetadataViewModel), sequenceMetadataViewModel);
            container.Bag.Add(typeof(IOverlayMetadataViewPresentationModel), overlayMetadataViewModel);
            container.Bag.Add(typeof(ITransitionsMetadataViewPresentationModel), transitionsMetadataViewModel);

            var module = new MetadataModule(container, regionViewRegistry);

            Assert.AreEqual(0, regionViewRegistry.ViewsByRegion.Count);

            module.Initialize();

            Assert.AreEqual(3, regionViewRegistry.ViewsByRegion.Count);
            Assert.AreEqual(2, regionViewRegistry.ViewsByRegion[RegionNames.ClipMetadataRegion].Count);
            Assert.AreEqual(1, regionViewRegistry.ViewsByRegion[RegionNames.SequenceMetadataRegion].Count);
            Assert.AreSame(clipMetadataViewModel.View, regionViewRegistry.ViewsByRegion[RegionNames.ClipMetadataRegion][0]);
            Assert.AreSame(overlayMetadataViewModel.View, regionViewRegistry.ViewsByRegion[RegionNames.ClipMetadataRegion][1]);
            Assert.AreSame(sequenceMetadataViewModel.View, regionViewRegistry.ViewsByRegion[RegionNames.SequenceMetadataRegion][0]);
        }

        /// <summary>
        /// Defines a testable <see cref="MetadataModule"/>.
        /// </summary>
        internal class TestableMetadataModuleModule : MetadataModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableMetadataModuleModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableMetadataModuleModule(IUnityContainer container)
                : base(container, new MockRegionViewRegistry())
            {
            }

            /// <summary>
            /// Invokes the RegisterViewsAndServices.
            /// </summary>
            public void InvokeRegisterViewsAndServices()
            {
                this.RegisterViewsAndServices();
            }
        }
    }
}