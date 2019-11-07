// <copyright file="OverlaysModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OverlaysModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Overlays
{
    using System;

    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure;
    using RCE.Modules.Overlays.Views;

    public class OverlaysModule : IModule
    {
        private readonly IUnityContainer unityContainer;

        private readonly IRegionViewRegistry regionViewRegistry;

        public OverlaysModule(IUnityContainer unityContainer, IRegionViewRegistry regionViewRegistry)
        {
            this.unityContainer = unityContainer;
            this.regionViewRegistry = regionViewRegistry;
        }

        public void Initialize()
        {
            this.RegisterMappings();
            var vm = this.unityContainer.Resolve<IOverlaysViewModel>();
            this.regionViewRegistry.RegisterViewWithRegion(RegionNames.AssetBrowserRegion, () => vm.View);
        }

        protected void RegisterMappings()
        {
            this.unityContainer.RegisterType<IOverlaysViewModel, OverlaysViewModel>();
            this.unityContainer.RegisterType<IOverlaysView, OverlaysView>();
        }
    }
}
