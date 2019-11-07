// <copyright file="SettingsModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SettingsModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.Settings.Views;

    /// <summary>
    /// Provides an implementation of the <seealso cref="IModule"/>. Defines the settings module.
    /// </summary>
    public class SettingsModule : IModule
    {
        /// <summary>
        /// The <seealso cref="IUnityContainer"/> container used to resolve dependencies.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// The <seealso cref="IRegionManager"/> used to get the <seealso cref="IRegion"/>s.
        /// </summary>
        private readonly IRegionManager regionManager;

        private readonly IWindowManager windowManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsModule"/> class. 
        /// </summary>
        /// <param name="container">The <seealso cref="IUnityContainer"/> container used to resolve dependencies.</param>
        /// <param name="regionManager">The <seealso cref="IRegionManager"/> used to get the <seealso cref="IRegion"/>s.</param>
        [CLSCompliant(false)]
        public SettingsModule(IUnityContainer container, IRegionManager regionManager, IWindowManager windowManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.windowManager = windowManager;
        }

        /// <summary>
        /// Initializes the Settings module.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            ISettingsViewPresentationModel presentationModel = this.container.Resolve<ISettingsViewPresentationModel>();

            this.container.Resolve<IIncreasePersistenceQuotaViewModel>();

            bool shouldSettingsWindow = this.windowManager.ShouldDisplayWindow(presentationModel.View.GetType().ToString(), false);

            if (shouldSettingsWindow)
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(presentationModel.View);
            }

            IMenuButtonViewModel menuViewModel = this.container.Resolve<IMenuButtonViewModel>();
            menuViewModel.IsViewActive = shouldSettingsWindow;
            menuViewModel.Text = "Settings";
            menuViewModel.ViewToDisplay = presentationModel.View;
            this.regionManager.Regions[RegionNames.MenuRegion].Add(menuViewModel.View);
        }

        /// <summary>
        /// Registers the views and presentation models used by the module.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<ISettingsView, SettingsView>();
            this.container.RegisterType<ISettingsViewPresentationModel, SettingsViewPresentationModel>();

            this.container.RegisterType<IIncreasePersistenceQuotaViewModel, IncreasePersistenceQuotaViewModel>();
            this.container.RegisterType<IIncreasePersistenceQuotaDialog, IncreasePersistenceQuotaDialog>();
        }
    }
}