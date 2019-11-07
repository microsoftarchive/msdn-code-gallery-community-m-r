// <copyright file="SettingsModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SettingsModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Tests
{
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Menu;
    using RCE.Modules.Settings.Views;

    /// <summary>
    /// Test class for <see cref="SettingsModule"/>.
    /// </summary>
    [TestClass]
    public class SettingsModuleFixture
    {
        /// <summary>
        /// Should register the views.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestableSettingsModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(4, container.Types.Count);
            Assert.AreEqual(typeof(SettingsView), container.Types[typeof(ISettingsView)]);
            Assert.AreEqual(typeof(SettingsViewPresentationModel), container.Types[typeof(ISettingsViewPresentationModel)]);

            Assert.AreEqual(typeof(IncreasePersistenceQuotaViewModel), container.Types[typeof(IIncreasePersistenceQuotaViewModel)]);
            Assert.AreEqual(typeof(IncreasePersistenceQuotaDialog), container.Types[typeof(IIncreasePersistenceQuotaDialog)]);
        }

        [TestMethod]
        public void ShouldAddSettingsMenuButtonViewToMenuRegion()
        {
            var windowManager = new MockWindowManager();
            var container = new MockUnityResolver();
            var regionManager = new MockRegionManager();
            var module = new SettingsModule(container, regionManager, windowManager);
            var menuButtonView = new MockMenuButtonView();

            ISettingsViewPresentationModel settingsViewModel = new MockSettingsViewPresentationModel();
            container.Bag.Add(typeof(ISettingsViewPresentationModel), settingsViewModel);
            container.Bag.Add(typeof(IIncreasePersistenceQuotaViewModel), new MockIncreasePersistenceQuotaViewModel());

            MockSettingsMenuButtonViewModel menuViewModel = new MockSettingsMenuButtonViewModel();
            menuViewModel.View = menuButtonView;

            container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;

            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };
            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };

            regionManager.Regions.Add(menuRegion);
            regionManager.Regions.Add(mainRegion);

            Assert.AreEqual(0, menuRegion.AddedViews.Count);

            module.Initialize();

            Assert.AreSame(settingsViewModel.View, menuViewModel.ViewToDisplay);
            Assert.IsTrue(menuViewModel.IsViewActive);
            Assert.AreEqual(1, menuRegion.AddedViews.Count);
            Assert.IsNotNull(menuViewModel.View);
            Assert.AreSame(menuViewModel.View, menuRegion.AddedViews[0]);
            Assert.AreEqual("Settings", menuViewModel.Text);
        }

        /// <summary>
        /// Testable class for <see cref="SettingsModule"/>.
        /// </summary>
        internal class TestableSettingsModule : SettingsModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableSettingsModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableSettingsModule(IUnityContainer container)
                : base(container, new MockRegionManager(), new MockWindowManager())
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