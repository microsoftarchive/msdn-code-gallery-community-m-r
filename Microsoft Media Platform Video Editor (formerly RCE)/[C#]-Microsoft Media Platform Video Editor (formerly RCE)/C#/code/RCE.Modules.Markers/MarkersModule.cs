// <copyright file="MarkersModule.cs" company="Microsoft Corporation">
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

namespace RCE.Modules.Markers
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Services;
    using RCE.Modules.Markers.Views;

    /// <summary>
    /// Class to load the MarkersModule Module.
    /// </summary>
    public class MarkersModule : IModule
    {
        /// <summary>
        /// The <see cref="IUnityContainer"/> to register the views and services.
        /// </summary>
        private readonly IUnityContainer container;

        private readonly IRegionViewRegistry regionViewRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkersModule"/> class.
        /// </summary>
        /// <param name="container">The instance of <see cref="IUnityContainer"/> interface.</param>
        [CLSCompliant(false)]
        public MarkersModule(IUnityContainer container, IRegionViewRegistry regionViewRegistry)
         {
             this.container = container;
             this.regionViewRegistry = regionViewRegistry;
         }

        /// <summary>
        /// Initializes the CommentsBarModule instance.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            this.regionViewRegistry.RegisterViewWithRegion("MarkersBrowserRegion", () => this.container.Resolve<IMarkersListViewModel>().View);

            ITimelineBarRegistry registry = this.container.Resolve<ITimelineBarRegistry>();

            registry.RegisterTimelineBarElement("Marker", () => this.container.Resolve<IMarkerEditBoxPresentationModel>());
        }

        /// <summary>
        /// Registers all the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<IMarkerViewPreview, MarkerView>();
            this.container.RegisterType<IMarkerEditBox, MarkerEditBox>();
            this.container.RegisterType<IMarkerEditBoxPresentationModel, MarkerEditBoxPresentationModel>();
            this.container.RegisterType<IMarkersListView, MarkersListView>();
            this.container.RegisterType<IMarkersListViewModel, MarkersListViewModel>();
        }
    }
}
