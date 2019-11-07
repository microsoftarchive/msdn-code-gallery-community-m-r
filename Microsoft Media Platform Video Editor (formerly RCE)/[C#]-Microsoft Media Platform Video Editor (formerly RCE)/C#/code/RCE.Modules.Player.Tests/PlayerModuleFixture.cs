// <copyright file="PlayerModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests
{
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Menu;
    using RCE.Modules.Player.Models;
    using RCE.Modules.Player.Services;
    using RCE.Overlays.Infrastructure.Manager;
    using RCE.Overlays.Infrastructure.UI;
    using RCE.Plugins.RubberBanding.Manager;

    /// <summary>
    /// Test class for <see cref="PlayerModule"/>.
    /// </summary>
    [TestClass]
    public class PlayerModuleFixture
    {
        /// <summary>
        /// Tests if the views and models are registerd.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var container = new MockUnityContainer();
            var module = new TestablePlayerModule(container);

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(9, container.Types.Count);
            Assert.AreEqual(typeof(PlayerView), container.Types[typeof(IPlayerView)]);
            Assert.AreEqual(typeof(PlayerViewPresenter), container.Types[typeof(IPlayerViewPresenter)]);
            Assert.AreEqual(typeof(OverlaysDisplayController), container.Types[typeof(IOverlaysDisplayController)]);
            Assert.AreEqual(typeof(OutputServiceFacade), container.Types[typeof(IOutputServiceFacade)]);
            Assert.AreEqual(typeof(ManifestMediaModel), container.Types[typeof(IManifestMediaModel)]);
            Assert.AreEqual(typeof(PlaybackManifestGenerator), container.Types[typeof(IPlaybackManifestGenerator)]);
            Assert.AreEqual(typeof(OverlaysManager), container.Types[typeof(IOverlaysManager)]);
            Assert.AreEqual(typeof(RubberBandingManager), container.Types[typeof(IRubberBandingManager)]);
            Assert.AreEqual(typeof(OverlaysDisplayController), container.Types[typeof(IOverlaysDisplayController)]);
        }

        /// <summary>
        /// Tests if the <see cref="PlayerView"/> is inserted in the player region.
        /// </summary>
        [TestMethod]
        public void ShouldAddPlayerViewToPlayerRegion()
        {
            var regionManager = new MockRegionManager();
            var container = new MockUnityResolver();
            var menuButtonView = new MockMenuButtonView();

            container.Bag.Add(typeof(IPlayerViewPresenter), new MockPlayerViewPresenter());

            MockSequencePreviewMenuButtonViewModel menuViewModel = new MockSequencePreviewMenuButtonViewModel();
            menuViewModel.View = menuButtonView;

            container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            regionManager.Regions.Add(mainRegion);
            regionManager.Regions.Add(menuRegion);

            var module = new PlayerModule(container, regionManager, new MockWindowManager());

            Assert.AreEqual(0, mainRegion.AddedViews.Count);

            module.Initialize();

            Assert.AreEqual(1, mainRegion.AddedViews.Count);
            Assert.IsInstanceOfType(mainRegion.AddedViews[0], typeof(IPlayerView));
        }

        [TestMethod]
        public void ShouldAddSequencePreviewMenuButtonViewToMenuRegion()
        {
            var container = new MockUnityResolver();
            var regionManager = new MockRegionManager();
            var module = new PlayerModule(container, regionManager, new MockWindowManager());
            var menuButtonView = new MockMenuButtonView();

            MockPlayerViewPresenter playerViewPresenter = new MockPlayerViewPresenter();

            container.Bag[typeof(IPlayerViewPresenter)] = playerViewPresenter;

            MockSequencePreviewMenuButtonViewModel menuViewModel = new MockSequencePreviewMenuButtonViewModel();
            menuViewModel.View = menuButtonView;

            container.Bag[typeof(IMenuButtonViewModel)] = menuViewModel;

            MockRegion mainRegion = new MockRegion { Name = "MainRegion" };
            MockRegion menuRegion = new MockRegion { Name = "MenuRegion" };

            regionManager.Regions.Add(mainRegion);
            regionManager.Regions.Add(menuRegion);

            Assert.AreEqual(0, menuRegion.AddedViews.Count);

            module.Initialize();

            Assert.AreSame(playerViewPresenter.View, menuViewModel.ViewToDisplay);
            Assert.IsTrue(menuViewModel.IsViewActive);
            Assert.AreEqual(1, menuRegion.AddedViews.Count);
            Assert.IsNotNull(menuViewModel.View);
            Assert.AreSame(menuViewModel.View, menuRegion.AddedViews[0]);
            Assert.AreEqual("Sequence Preview", menuViewModel.Text);
        }

        /// <summary>
        /// Testable class for <see cref="PlayerModule"/>.
        /// </summary>
        internal class TestablePlayerModule : PlayerModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestablePlayerModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestablePlayerModule(IUnityContainer container)
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