// <copyright file="BrowsersModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BrowsersModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers.Tests
{
    using System.Linq;

    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Menu;
    using RCE.Modules.Browsers;
    using RCE.Modules.Browsers.Tests.Mocks;
    using RCE.Modules.Browsers.Views;

    [TestClass]
    public class BrowsersModuleFixture
    {
        private MockUnityContainer container;

        private MockRegionManager regionManager;

        private MockAssetBrowserMenuButtonView menuButtonView;

        private MockWindowManager windowManager;

        [TestInitialize]
        public void TestInitialize()
        {
            this.container = new MockUnityContainer();
            this.regionManager = new MockRegionManager();
            this.menuButtonView = new MockAssetBrowserMenuButtonView();
            this.windowManager = new MockWindowManager();
        }

        [TestMethod]
        public void ShouldRegisterTypeMappingsWhenInitializing()
        {
            var module = this.CreateModule();
            
            MockAssetBrowserViewModel viewModel = new MockAssetBrowserViewModel();

            this.container.Bag[typeof(IAssetBrowserViewModel)] = viewModel;

            MockAssetBrowserMenuButtonViewModel menuViewModel = new MockAssetBrowserMenuButtonViewModel();
            menuViewModel.View = this.menuButtonView;

            this.container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;
            
            MockProjectBrowserViewModel projectBrowserViewModel = new MockProjectBrowserViewModel();
            this.container.Bag[typeof(IProjectBrowserViewModel)] = projectBrowserViewModel;

            MockMarkerBrowserViewModel markerBrowserViewModel = new MockMarkerBrowserViewModel();
            this.container.Bag[typeof(IMarkerBrowserViewModel)] = markerBrowserViewModel;

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            this.regionManager.Regions.Add(mainRegion);
            this.regionManager.Regions.Add(menuRegion);

            Assert.AreEqual(0, this.container.Types.Count);

            module.Initialize();

            Assert.AreEqual(6, this.container.Types.Count);
            Assert.AreEqual(typeof(AssetBrowserViewModel), this.container.Types[typeof(IAssetBrowserViewModel)]);
            Assert.AreEqual(typeof(AssetBrowserView), this.container.Types[typeof(IAssetBrowserView)]);
            Assert.AreEqual(typeof(ProjectBrowserViewModel), this.container.Types[typeof(IProjectBrowserViewModel)]);
            Assert.AreEqual(typeof(ProjectBrowserView), this.container.Types[typeof(IProjectBrowserView)]);
            Assert.AreEqual(typeof(MarkerBrowserView), this.container.Types[typeof(IMarkerBrowserView)]);
            Assert.AreEqual(typeof(MarkerBrowserViewModel), this.container.Types[typeof(IMarkerBrowserViewModel)]);
        }

        [TestMethod]
        public void ShouldAddAssetBrowserViewToMainRegion()
        {
            var module = this.CreateModule();

            MockAssetBrowserViewModel viewModel = new MockAssetBrowserViewModel();

            this.container.Bag[typeof(IAssetBrowserViewModel)] = viewModel;

            MockAssetBrowserMenuButtonViewModel menuViewModel = new MockAssetBrowserMenuButtonViewModel();
            menuViewModel.View = this.menuButtonView;

            this.container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;

            MockProjectBrowserViewModel projectBrowserViewModel = new MockProjectBrowserViewModel();
            this.container.Bag[typeof(IProjectBrowserViewModel)] = projectBrowserViewModel;

            MockMarkerBrowserViewModel markerBrowserViewModel = new MockMarkerBrowserViewModel();
            this.container.Bag[typeof(IMarkerBrowserViewModel)] = markerBrowserViewModel;

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            this.regionManager.Regions.Add(mainRegion);
            this.regionManager.Regions.Add(menuRegion);

            Assert.AreEqual(0, mainRegion.AddedViews.Count());

            module.Initialize();

            Assert.AreEqual(2, mainRegion.AddedViews.Count());
            Assert.IsInstanceOfType(mainRegion.AddedViews.First(), typeof(IAssetBrowserView));
            Assert.AreSame(viewModel.View, mainRegion.AddedViews.First());
        }

        [TestMethod]
        public void ShouldAddProjectBrowserViewToMainRegion()
        {
            var module = this.CreateModule();

            MockAssetBrowserViewModel viewModel = new MockAssetBrowserViewModel();

            this.container.Bag[typeof(IAssetBrowserViewModel)] = viewModel;

            MockAssetBrowserMenuButtonViewModel menuViewModel = new MockAssetBrowserMenuButtonViewModel();
            menuViewModel.View = this.menuButtonView;

            this.container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;

            MockProjectBrowserViewModel projectBrowserViewModel = new MockProjectBrowserViewModel();
            this.container.Bag[typeof(IProjectBrowserViewModel)] = projectBrowserViewModel;

            MockMarkerBrowserViewModel markerBrowserViewModel = new MockMarkerBrowserViewModel();
            this.container.Bag[typeof(IMarkerBrowserViewModel)] = markerBrowserViewModel;

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            this.regionManager.Regions.Add(mainRegion);
            this.regionManager.Regions.Add(menuRegion);

            Assert.AreEqual(0, mainRegion.AddedViews.Count());

            module.Initialize();

            Assert.AreEqual(2, mainRegion.AddedViews.Count());
            Assert.IsInstanceOfType(mainRegion.AddedViews.Last(), typeof(IProjectBrowserView));
            Assert.AreSame(projectBrowserViewModel.View, mainRegion.AddedViews.Last());
        }

        private IModule CreateModule()
        {
            return new BrowsersModule(this.container, this.regionManager, this.windowManager);
        }
    }
}