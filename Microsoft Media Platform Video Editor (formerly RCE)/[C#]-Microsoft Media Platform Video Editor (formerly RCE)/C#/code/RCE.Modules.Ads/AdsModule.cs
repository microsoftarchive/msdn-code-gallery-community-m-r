// <copyright file="AdsModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AdsModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Ads
{
    using System;

    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Services;
    using RCE.Modules.Ads.Views;

    /// <summary>
    /// Class to load the AdsModule Module.
    /// </summary>
    public class AdsModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly ITimelineBarRegistry registry;
        private readonly IRegionViewRegistry regionViewRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdsModule"/> class.
        /// </summary>
        /// <param name="container">The instance of <see cref="IUnityContainer"/> interface.</param>
        /// <param name="registry">The instance of <see cref="ITimelineBarRegistry"/> interface.</param>
        /// <param name="regionViewRegistry">The instance of <see cref="IRegionViewRegistry"/> interface.</param>
        [CLSCompliant(false)]
        public AdsModule(IUnityContainer container, ITimelineBarRegistry registry, IRegionViewRegistry regionViewRegistry)
        {
            this.container = container;
            this.registry = registry;
            this.regionViewRegistry = regionViewRegistry;
        }

        /// <summary>
        /// Initializes the CommentsBarModule instance.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();
            this.regionViewRegistry.RegisterViewWithRegion(
                "MarkersBrowserRegion",
                () => this.container.Resolve<IAdsListViewPresentationModel>().View);
            this.registry.RegisterTimelineBarElement("Ad", () => this.container.Resolve<IAdEditBoxPresentationModel>());
        }

        /// <summary>
        /// Registers all the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<IAdViewPreview, AdView>();
            this.container.RegisterType<IAdEditBox, AdEditBox>();
            this.container.RegisterType<IAdEditBoxPresentationModel, AdEditBoxPresentationModel>();
            this.container.RegisterType<IAdsListView, AdsListView>();
            this.container.RegisterType<IAdsListViewPresentationModel, AdsListViewPresentationModel>();
        }
    }
}
