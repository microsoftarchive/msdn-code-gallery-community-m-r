// <copyright file="EncoderOutputModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TitlesModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.EncoderOutput
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Windows;

    using Services;
    using Views;

    /// <summary>
    /// Class to load the EncoderOutput Module.
    /// </summary>
    public class EncoderOutputModule : IModule
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
        /// Initializes a new instance of the <see cref="EncoderOutputModule"/> class.
        /// </summary>
        /// <param name="container">The <see cref="IUnityContainer"/>.</param>
        /// <param name="regionManager">The <see cref="IRegionManager"/>.</param>
        /// /// <param name="windowManager">The <see cref="IWindowManager"/>.</param>
        [CLSCompliant(false)]
        public EncoderOutputModule(IUnityContainer container, IRegionManager regionManager, IWindowManager windowManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.windowManager = windowManager;
        }

        /// <summary>
        /// Initializes the TitlesModule instance.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            IEncoderSettingsPresentationModel presentationModel = this.container.Resolve<IEncoderSettingsPresentationModel>();
            bool shouldDisplayOutputWindow = this.windowManager.ShouldDisplayWindow(presentationModel.View.GetType().ToString(), false);

            if (shouldDisplayOutputWindow)
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(presentationModel.View);
            }

            IMenuButtonViewModel menuViewModel = this.container.Resolve<IMenuButtonViewModel>();
            menuViewModel.IsViewActive = shouldDisplayOutputWindow;
            menuViewModel.Text = "Output";
            menuViewModel.ViewToDisplay = presentationModel.View;
            this.regionManager.Regions[RegionNames.MenuRegion].Add(menuViewModel.View);
        }

        /// <summary>
        /// Registers all the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<IOutputServiceFacade, OutputServiceFacade>();
            this.container.RegisterType<IEncoderSettingsView, EncoderSettingsView>();
            this.container.RegisterType<IEncoderSettingsPresentationModel, EncoderSettingsPresentationModel>();
        }
    }
}