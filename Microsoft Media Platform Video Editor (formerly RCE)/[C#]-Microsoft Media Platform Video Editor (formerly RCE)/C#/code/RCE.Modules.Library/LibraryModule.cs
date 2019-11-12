// <copyright file="LibraryModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LibraryModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Menu;

    /// <summary>
    /// The module class for the library module.
    /// It inserts the library view in the tools regions and registers the 
    /// views and models in the container.
    /// </summary>
    public class LibraryModule : IModule
    {
        /// <summary>
        /// The <see cref="IUnityContainer"/> to register the views and models.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// The <see cref="IRegionManager"/> to insert the mediabin view.
        /// </summary>
        private readonly IRegionViewRegistry regionViewRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="regionManager">The region manager.</param>
        [CLSCompliant(false)]
        public LibraryModule(IUnityContainer container, IRegionViewRegistry regionViewRegistry)
        {
            this.container = container;
            this.regionViewRegistry = regionViewRegistry;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            ILibraryViewPresentationModel presentationModel = this.container.Resolve<ILibraryViewPresentationModel>();

            this.regionViewRegistry.RegisterViewWithRegion(RegionNames.AssetBrowserRegion, () => presentationModel.View);
        }

        /// <summary>
        /// Registers the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<ILibraryView, LibraryView>();
            this.container.RegisterType<ILibraryViewPresentationModel, LibraryViewPresentationModel>();
        }
    }
}
