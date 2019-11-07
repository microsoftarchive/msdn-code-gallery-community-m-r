// <copyright file="AssetBrowserViewModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetBrowserViewModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Tests.Views
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Events;
    using RCE.Modules.Browsers.Tests.Mocks;
    using RCE.Modules.Browsers.Views;

    [TestClass]
    public class AssetBrowserViewModelFixture
    {
        private MockAssetBrowserView view;

        private MockEventAggregator eventAggregator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.view = new MockAssetBrowserView();
            this.eventAggregator = new MockEventAggregator();
            this.eventAggregator.AddMapping<ResetWindowsEvent>(new MockResetWindowsEvent());
        }

        [TestMethod]
        public void ShouldUseViewPassedThroughConstructor()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreEqual(this.view, viewModel.View);
        }

        [TestMethod]
        public void ShouldCallSetDataContextInView()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreSame(this.view.SetDataContextParameter, viewModel);
        }

        private AssetBrowserViewModel CreateViewModel()
        {
            return new AssetBrowserViewModel(this.view, this.eventAggregator);
        }
    }
}