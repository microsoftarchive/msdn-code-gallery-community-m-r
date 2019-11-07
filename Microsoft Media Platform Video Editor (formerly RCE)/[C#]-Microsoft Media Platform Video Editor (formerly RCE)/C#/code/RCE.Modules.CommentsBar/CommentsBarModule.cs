// <copyright file="CommentsBarModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentsBarModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar
{
    using System;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure;

    /// <summary>
    /// Class to load the CommentsBar Module.
    /// </summary>
    public class CommentsBarModule : IModule
    {
        /// <summary>
        /// The <see cref="IUnityContainer"/> to register the views and services.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// The <see cref="IRegionManager"/> to insert the <see cref="CommentsBarView"/>.
        /// </summary>
        private readonly IRegionViewRegistry regionViewRegistry;

        private const string TimelineImplementationName = "Timeline";
        private const string SubClipImplementationName = "Subclip";

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsBarModule"/> class.
        /// </summary>
        /// <param name="container">The instance of <see cref="IUnityContainer"/> interface.</param>
        /// <param name="regionManager">The instance of <see cref="IRegionManager"/> interface.</param>
        [CLSCompliant(false)]
        public CommentsBarModule(IUnityContainer container, IRegionViewRegistry regionViewRegistry)
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

            this.RegisterTimelineCommentRegion();
            this.RegisterSubClipCommentRegion();
        }

        /// <summary>
        /// Registers all the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<ICommentsBarPresenter, SubClipCommentsBarPresenter>(SubClipImplementationName);
            this.container.RegisterType<ICommentsBarPresenter, TimelineCommentsBarPresenter>(TimelineImplementationName);
            this.container.RegisterType<ICommentsBarView, CommentsBarView>();
        }
        
        private void RegisterSubClipCommentRegion()
        {
            ICommentsBarPresenter presenter = this.container.Resolve<ICommentsBarPresenter>(SubClipImplementationName);
            this.regionViewRegistry.RegisterViewWithRegion(RegionNames.SubClipCommentsBarRegion, () => presenter.View);
        }

        private void RegisterTimelineCommentRegion()
        {
            ICommentsBarPresenter presenter = this.container.Resolve<ICommentsBarPresenter>(TimelineImplementationName);
            this.regionViewRegistry.RegisterViewWithRegion(RegionNames.CommentsBarRegion, () => presenter.View);
        }
    }
}
