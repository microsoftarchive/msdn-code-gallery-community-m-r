// <copyright file="SubClipModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClipModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip.Tests
{
    using System.Linq;

    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.SubClip.Tests.Mocks;
    using RCE.Modules.SubClip.Views;

    [TestClass]
    public class SubClipModuleFixture
    {
        private MockUnityContainer unityContainer;

        private MockRegionManager regionManager;

        private MockSubClipMenuButtonView menuButtonView;

        private MockWindowManager windowManager;

        [TestInitialize]
        public void TestInitialize()
        {
            this.unityContainer = new MockUnityContainer();
            this.regionManager = new MockRegionManager();
            this.menuButtonView = new MockSubClipMenuButtonView();
            this.windowManager = new MockWindowManager();
        }
        
        [TestMethod]
        public void ShouldRegisterMappingsInContainer()
        {
            var module = this.CreateModule(this.unityContainer);
            
            Assert.AreEqual(0, this.unityContainer.Types.Count);

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            this.regionManager.Regions.Add(mainRegion);
            this.regionManager.Regions.Add(menuRegion);

            this.unityContainer.Bag[typeof(ISubClipViewModel)] = new MockSubClipViewModel();
            this.unityContainer.Bag[typeof(IMenuButtonViewModel)] = new MockSubClipMenuButtonViewModel();

            module.Initialize();

            Assert.AreEqual(2, this.unityContainer.Types.Count);
            Assert.AreEqual(typeof(SubClipView), this.unityContainer.Types[typeof(ISubClipView)]);
            Assert.AreEqual(typeof(SubClipViewModel), this.unityContainer.Types[typeof(ISubClipViewModel)]);
        }

        [TestMethod]
        public void ShouldAddSubClipViewToMainRegion()
        {
            var module = this.CreateModule(this.unityContainer);

            ISubClipViewModel viewModel = new MockSubClipViewModel();

            this.unityContainer.Bag[typeof(ISubClipViewModel)] = viewModel;

            MockSubClipMenuButtonViewModel menuViewModel = new MockSubClipMenuButtonViewModel();
           
            this.unityContainer.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            this.regionManager.Regions.Add(mainRegion);
            this.regionManager.Regions.Add(menuRegion);

            Assert.AreEqual(0, mainRegion.AddedViews.Count());

            module.Initialize();

            Assert.AreEqual(1, mainRegion.AddedViews.Count());
            Assert.IsInstanceOfType(mainRegion.AddedViews.First(), typeof(ISubClipView));
            Assert.AreSame(viewModel.View, mainRegion.AddedViews.First());
        }

        [TestMethod]
        public void ShouldAddSubClipMenuButtonViewToMenuRegion()
        {
            var module = this.CreateModule(this.unityContainer);

            ISubClipViewModel viewModel = new MockSubClipViewModel();

            this.unityContainer.Bag[typeof(ISubClipViewModel)] = viewModel;

            MockSubClipMenuButtonViewModel menuViewModel = new MockSubClipMenuButtonViewModel();
            menuViewModel.View = this.menuButtonView;
           
            this.unityContainer.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            this.regionManager.Regions.Add(mainRegion);
            this.regionManager.Regions.Add(menuRegion);

            Assert.AreEqual(0, menuRegion.AddedViews.Count);

            module.Initialize();

            Assert.AreSame(viewModel.View, menuViewModel.ViewToDisplay);
            Assert.IsTrue(menuViewModel.IsViewActive);
            Assert.AreEqual(1, menuRegion.AddedViews.Count);
            Assert.IsNotNull(menuViewModel.View);
            Assert.AreSame(menuViewModel.View, menuRegion.AddedViews[0]);
            Assert.AreEqual("Sub-Clip", menuViewModel.Text);
        }

        private IModule CreateModule(IUnityContainer container)
        {
            return new SubClipModule(container, this.regionManager, this.windowManager);
        }
    }
}