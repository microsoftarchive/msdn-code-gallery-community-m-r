// <copyright file="PlayerModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;
    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.Player.Models;
    using RCE.Modules.Player.Services;
    using RCE.Overlays.Infrastructure.Manager;
    using RCE.Overlays.Infrastructure.UI;
    using RCE.Plugins.RubberBanding.Manager;
    using RCE.Transitions.Infrastructure.Managers;
    using RCE.VolumeOrchestrator;

    /// <summary>
    /// The <see cref="IModule"/> for player module. 
    /// It registers the player view and model in
    ///  the container and inserts the view in the player region.
    /// </summary>
    public class PlayerModule : IModule
    {
        /// <summary>
        /// The <see cref="IUnityContainer"/> to register the objects.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// The <see cref="IRegionManager"/> to insert the view in the player region.
        /// </summary>
        private readonly IRegionManager regionManager;

        private readonly IWindowManager windowManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="windowManager">The window manager.</param>
        [CLSCompliant(false)]
        public PlayerModule(IUnityContainer container, IRegionManager regionManager, IWindowManager windowManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.windowManager = windowManager;
        }

        /// <summary>
        /// Registers the objects that are used in the player module and 
        /// insert the player view in the player region.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            IPlayerViewPresenter presenter = this.container.Resolve<IPlayerViewPresenter>();

            bool shouldDisplayPlayerWindow = this.windowManager.ShouldDisplayWindow(presenter.View.GetType().ToString(), true);

            if (shouldDisplayPlayerWindow)
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(presenter.View);
            }

            IMenuButtonViewModel menuViewModel = this.container.Resolve<IMenuButtonViewModel>();
            menuViewModel.ViewToDisplay = presenter.View;
            menuViewModel.Text = "Sequence Preview";
            menuViewModel.IsViewActive = shouldDisplayPlayerWindow;

            this.regionManager.Regions[RegionNames.MenuRegion].Add(menuViewModel.View);
        }

        /// <summary>
        /// Registers the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<IOutputServiceFacade, OutputServiceFacade>(new ContainerControlledLifetimeManager());
            this.container.RegisterType<IManifestMediaModel, ManifestMediaModel>();
            this.container.RegisterType<IPlayerView, PlayerView>();
            this.container.RegisterType<IPlayerViewPresenter, PlayerViewPresenter>();
            this.container.RegisterType<IPlaybackManifestGenerator, PlaybackManifestGenerator>();
            this.container.RegisterType<ITransitionsManager, TransitionsManager>();
            this.container.RegisterType<IOverlaysManager, OverlaysManager>();
            this.container.RegisterType<IRubberBandingManager, RubberBandingManager>();
            this.container.RegisterType<IOverlaysDisplayController, OverlaysDisplayController>();
        }
    }
}
