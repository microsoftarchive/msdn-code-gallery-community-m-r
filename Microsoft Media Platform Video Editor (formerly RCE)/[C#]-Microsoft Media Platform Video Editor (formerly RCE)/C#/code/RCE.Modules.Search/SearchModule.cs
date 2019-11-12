// <copyright file="SearchModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SearchModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Search
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;
    using RCE.Metadata.Strategies;
    using RCE.Modules.Search.Security;
    using RCE.Modules.Search.Services;
    using RCE.Services.Contracts;

    public class SearchModule : IModule
    {
        /// <summary>
        /// The <seealso cref="IUnityContainer"/> container used to resolve dependencies.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// The <seealso cref="IRegionManager"/> used to get the <seealso cref="IRegion"/>s.
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="regionManager">The region manager.</param>
        [CLSCompliant(false)]
        public SearchModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        /// <summary>
        /// Registers the view and models in the container. Inserts the view in the tools region.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            ISearchViewPresentationModel presentationModel = this.container.Resolve<ISearchViewPresentationModel>();

            this.regionManager.RegisterViewWithRegion(RegionNames.SearchRegion, () => presentationModel.View);
        }

        /// <summary>
        /// Registers the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<ISearchServiceBridge, SearchServiceBridge>();
            this.container.RegisterType<ISearchView, SearchView>();
            this.container.RegisterType<ISearchViewPresentationModel, SearchViewPresentationModel>();
            this.container.RegisterType<IMetadataStrategy, SmoothStreamingMetadataStrategy>();
            this.container.RegisterType<IXmlAssetsDataParser, XmlAssetsDataParser>();
            this.container.RegisterType<ICdnTokenGenerator, AkamaiCdnTokenGenerator>();
        }
    }
}