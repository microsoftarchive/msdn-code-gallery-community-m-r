// <copyright file="SubClipModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClipModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip
{
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.SubClip.Views;

    public class SubClipModule : IModule
    {
        private readonly IUnityContainer unityContainer;

        private readonly IRegionManager regionManager;

        private readonly IWindowManager windowManager;

        public SubClipModule(IUnityContainer unityContainer, IRegionManager regionManager, IWindowManager windowManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;
            this.windowManager = windowManager;
        }

        public void Initialize()
        {
            this.RegisterViewsAndServices();
            ISubClipView subClipView = this.unityContainer.Resolve<ISubClipViewModel>().View;

            bool shouldSubClipWindow = this.windowManager.ShouldDisplayWindow(subClipView.GetType().ToString(), true);

            if (shouldSubClipWindow)
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(subClipView);
            }

            IMenuButtonViewModel menuViewModel = this.unityContainer.Resolve<IMenuButtonViewModel>();
            menuViewModel.IsViewActive = shouldSubClipWindow;
            menuViewModel.ViewToDisplay = subClipView;
            menuViewModel.Text = "Sub-Clip";

            this.regionManager.Regions[RegionNames.MenuRegion].Add(menuViewModel.View);
        }

        private void RegisterViewsAndServices()
        {
            this.unityContainer.RegisterType<ISubClipViewModel, SubClipViewModel>();
            this.unityContainer.RegisterType<ISubClipView, SubClipView>();
        }
    }
}
