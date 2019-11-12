// <copyright file="OverlaysViewModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlaysViewModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Overlays.Tests.Views
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Overlays.Tests.Mocks;
    using RCE.Modules.Overlays.Views;

    [TestClass]
    public class OverlaysViewModelFixture
    {
        private MockEventAggregator eventAggregator;

        private MockOverlaysView view;

        private MockAssetsAvailableEvent assetsAvailableEvent;

        [TestInitialize]
        public void SetUp()
        {
            this.eventAggregator = new MockEventAggregator();
            this.view = new MockOverlaysView();
            this.assetsAvailableEvent = new MockAssetsAvailableEvent();
            this.eventAggregator.AddMapping<AssetsAvailableEvent>(this.assetsAvailableEvent);
        }

        [TestMethod]
        public void ShouldSetViewModelToView()
        {
            var vm = this.CreateViewModel();

            Assert.AreSame(vm, this.view.ViewModelParameter);
        }

        [TestMethod]
        public void ShouldSetViewToViewModel()
        {
            var vm = this.CreateViewModel();

            Assert.AreSame(this.view, vm.View);
        }

        [TestMethod]
        public void ShouldPopulateOverlaysListWhenAssetsAvailableEventIsPublished()
        {
            var vm = this.CreateViewModel();

            OverlayAsset o1 = new OverlayAsset();
            OverlayAsset o2 = new OverlayAsset();
            AudioAsset a1 = new AudioAsset();
            VideoAsset v1 = new VideoAsset();

            var assets = new List<Asset> { o1, o2, a1, v1 };

            Assert.AreEqual(0, vm.Overlays.Count);

            this.assetsAvailableEvent.Publish(new DataEventArgs<List<Asset>>(assets));

            Assert.AreEqual(2, vm.Overlays.Count);
            CollectionAssert.Contains(vm.Overlays, o1);
            CollectionAssert.Contains(vm.Overlays, o2);
        }

        [TestMethod]
        public void ShouldClearOverlaysListBeforeAddingAssetsWhenAssetsAvailableEventIsPublished()
        {
            var vm = this.CreateViewModel();

            OverlayAsset o1 = new OverlayAsset();
            OverlayAsset o2 = new OverlayAsset();
            AudioAsset a1 = new AudioAsset();
            VideoAsset v1 = new VideoAsset();

            var assets = new List<Asset> { o1, o2, a1, v1 };

            Assert.AreEqual(0, vm.Overlays.Count);

            this.assetsAvailableEvent.Publish(new DataEventArgs<List<Asset>>(assets));

            Assert.AreEqual(2, vm.Overlays.Count);
            CollectionAssert.Contains(vm.Overlays, o1);
            CollectionAssert.Contains(vm.Overlays, o2);

            OverlayAsset o3 = new OverlayAsset();
            OverlayAsset o4 = new OverlayAsset();
            AudioAsset a2 = new AudioAsset();
            VideoAsset v2 = new VideoAsset();

            var newAssets = new List<Asset> { o3, o4, a2, v2 };

            this.assetsAvailableEvent.Publish(new DataEventArgs<List<Asset>>(newAssets));

            Assert.AreEqual(2, vm.Overlays.Count);
            CollectionAssert.Contains(vm.Overlays, o3);
            CollectionAssert.Contains(vm.Overlays, o4);
        }

        private OverlaysViewModel CreateViewModel()
        {
            return new OverlaysViewModel(this.view, this.eventAggregator);
        }
    }
}