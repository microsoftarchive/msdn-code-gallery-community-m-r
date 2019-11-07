// <copyright file="BrowsersModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BrowsersModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Browsers
{
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.Browsers.Views;

    public class BrowsersModule : IModule
    {
        private readonly IUnityContainer unityContainer;

        private readonly IRegionManager regionManager;

        private readonly IWindowManager windowManager;

        public BrowsersModule(IUnityContainer unityContainer, IRegionManager regionManager, IWindowManager windowManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
            this.windowManager = windowManager;
        }

        public void Initialize()
        {
            this.RegisterTypeMappings();

            var assetBrowserViewModel = this.unityContainer.Resolve<IAssetBrowserViewModel>();

            bool shouldDisplayAssetBrowserWindow = this.windowManager.ShouldDisplayWindow(assetBrowserViewModel.View.GetType().ToString(), true);

            if (shouldDisplayAssetBrowserWindow)
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(assetBrowserViewModel.View);
            }

            IMenuButtonViewModel assetBrowserMenuButtonViewModel = this.unityContainer.Resolve<IMenuButtonViewModel>();
            assetBrowserMenuButtonViewModel.IsViewActive = shouldDisplayAssetBrowserWindow;
            assetBrowserMenuButtonViewModel.ViewToDisplay = assetBrowserViewModel.View;
            assetBrowserMenuButtonViewModel.Text = "Asset Browser";

            this.regionManager.Regions[RegionNames.MenuRegion].Add(assetBrowserMenuButtonViewModel.View);

            var projectBrowserViewModel = this.unityContainer.Resolve<IProjectBrowserViewModel>();

            bool shouldDisplayProjectBrowserWindow = this.windowManager.ShouldDisplayWindow(projectBrowserViewModel.View.GetType().ToString(), true);

            if (shouldDisplayProjectBrowserWindow)
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(projectBrowserViewModel.View);
            }

            IMenuButtonViewModel projectBrowserMenuButtonViewModel = this.unityContainer.Resolve<IMenuButtonViewModel>();
            projectBrowserMenuButtonViewModel.IsViewActive = shouldDisplayProjectBrowserWindow;
            projectBrowserMenuButtonViewModel.ViewToDisplay = projectBrowserViewModel.View;
            projectBrowserMenuButtonViewModel.Text = "Project Browser";

            this.regionManager.Regions[RegionNames.MenuRegion].Add(projectBrowserMenuButtonViewModel.View);

            this.unityContainer.Resolve<IMarkerBrowserViewModel>();
        }

        private void RegisterTypeMappings()
        {
            this.unityContainer.RegisterType<IAssetBrowserViewModel, AssetBrowserViewModel>();
            this.unityContainer.RegisterType<IAssetBrowserView, AssetBrowserView>();
            this.unityContainer.RegisterType<IProjectBrowserViewModel, ProjectBrowserViewModel>();
            this.unityContainer.RegisterType<IProjectBrowserView, ProjectBrowserView>();
            this.unityContainer.RegisterType<IMarkerBrowserViewModel, MarkerBrowserViewModel>();
            this.unityContainer.RegisterType<IMarkerBrowserView, MarkerBrowserView>();
        }
    }
}
