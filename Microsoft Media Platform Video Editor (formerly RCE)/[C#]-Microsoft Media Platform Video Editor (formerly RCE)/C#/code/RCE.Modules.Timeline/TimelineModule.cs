// <copyright file="TimelineModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Windows;
    using RCE.Modules.Timeline.Commands;
    using RCE.Modules.Timeline.Locking;
    using RCE.Modules.Timeline.Views;

    /// <summary>
    /// Provides an implementation of the <seealso cref="IModule"/>. Defines the timeline module.
    /// </summary>
    public class TimelineModule : IModule
    {
        /// <summary>
        /// The <seealso cref="IUnityContainer"/> container used to resolve dependencies.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// The <seealso cref="IRegionManager"/> used to get the <seealso cref="IRegion"/>s.
        /// </summary>
        private readonly IRegionManager regionManager;

        private readonly IRegionViewRegistry regionViewRegistry;

        private readonly IWindowManager windowManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineModule"/> class.
        /// </summary>
        /// <param name="container">The <seealso cref="IUnityContainer"/> container used to resolve dependencies.</param>
        /// <param name="regionManager">The <seealso cref="IRegionManager"/> used to get the <seealso cref="IRegion"/>s.</param>
        /// <param name="regionViewRegistry">The <seealso cref="IRegionViewRegistry"/> used to get the <seealso cref="IRegion"/>s.</param>
        /// <param name="windowManager">The <seealso cref="IWindowManager"/> used to get the windows visibility.</param>
        [CLSCompliant(false)]
        public TimelineModule(IUnityContainer container, IRegionManager regionManager, IRegionViewRegistry regionViewRegistry, IWindowManager windowManager)
        {
            this.container = container;
            this.regionManager = regionManager;
            this.windowManager = windowManager;
            this.regionViewRegistry = regionViewRegistry;
        }

        /// <summary>
        /// Initializes the Timeline module.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            ITimelinePresenter presenter = this.container.Resolve<ITimelinePresenter>();

            bool shouldDisplayTimelineWindow = this.windowManager.ShouldDisplayWindow(presenter.View.GetType().ToString(), true);

            if (shouldDisplayTimelineWindow)
            {
                this.regionManager.Regions[RegionNames.MainRegion].Add(presenter.View);
            }

            IMenuButtonViewModel menuViewModel = this.container.Resolve<IMenuButtonViewModel>();
            menuViewModel.ViewToDisplay = presenter.View;
            menuViewModel.Text = "Sequence";
            menuViewModel.IsViewActive = shouldDisplayTimelineWindow;

            this.regionManager.Regions[RegionNames.MenuRegion].Add(menuViewModel.View);

            this.regionViewRegistry.RegisterViewWithRegion(RegionNames.ProjectBrowserRegion, () => this.container.Resolve<ISequencesViewModel>().View);
        }

        /// <summary>
        /// Registers the views and presentation models used by the module.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<ICaretaker, Caretaker>();
            this.container.RegisterType<ITimelinePresenter, TimelinePresenter>();
            this.container.RegisterType<ITimelineView, TimelineView>();
            this.container.RegisterType<ILockGroupManager, LockGroupManager>();
            this.container.RegisterType<ISequencesView, SequencesView>();
            this.container.RegisterType<ISequencesViewModel, SequencesViewModel>();
        }
    }
}