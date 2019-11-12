// <copyright file="ProjectsModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ProjectsModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Windows;

    /// <summary>
    /// Provides an implementation of the <seealso cref="IModule"/>. Defines the Project module.
    /// </summary>
    public class ProjectsModule : IModule
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
        /// Initializes a new instance of the <see cref="ProjectsModule"/> class.
        /// </summary>
        /// <param name="container">The <seealso cref="IUnityContainer"/> container used to resolve dependencies.</param>
        /// <param name="regionManager">The <seealso cref="IRegionManager"/> used to get the <seealso cref="IRegion"/>s.</param>
        [CLSCompliant(false)]
        public ProjectsModule(IUnityContainer container, IRegionManager regionManager, IWindowManager windowManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.windowManager = windowManager;
        }

        /// <summary>
        /// Notifies the module that it has be initialized. 
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            IProjectViewPresenter presenter = this.container.Resolve<IProjectViewPresenter>();

            bool shouldDisplayPlayerWindow = this.windowManager.ShouldDisplayWindow(presenter.View.GetType().ToString(), false);

            if (shouldDisplayPlayerWindow)
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(presenter.View);
            }

            IMenuButtonViewModel menuViewModel = this.container.Resolve<IMenuButtonViewModel>();
            menuViewModel.ViewToDisplay = presenter.View;
            menuViewModel.Text = "Project List";
            menuViewModel.IsViewActive = shouldDisplayPlayerWindow;

            this.regionManager.Regions[RegionNames.MenuRegion].Add(menuViewModel.View);
        }

        /// <summary>
        /// Registers the views and presentation models used by the module.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<IProjectView, ProjectView>();
            this.container.RegisterType<IProjectViewPresenter, ProjectViewPresenter>();
        }
    }
}