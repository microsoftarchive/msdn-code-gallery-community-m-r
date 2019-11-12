// <copyright file="CommentModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment
{
    using System;
    using Infrastructure;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// <see cref="IModule"/> class for the Comment module.
    /// </summary>
    public class CommentModule : IModule
    {
        /// <summary>
        /// The <seealso cref="IUnityContainer"/> container used to resolve dependencies.
        /// </summary>
        private readonly IUnityContainer container;

        private readonly IRegionViewRegistry regionViewRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="regionViewRegistry">The region view registry</param>
        [CLSCompliant(false)]
        public CommentModule(IUnityContainer container, IRegionViewRegistry regionViewRegistry)
        {
            this.container = container;
            this.regionViewRegistry = regionViewRegistry;
        }

        /// <summary>
        /// Registers the view and models in the container. Inserts the view in the tools region.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            this.regionViewRegistry.RegisterViewWithRegion("MarkersBrowserRegion", () => this.container.Resolve<ICommentViewPresentationModel>().View);

            ITimelineBarRegistry registry = this.container.Resolve<ITimelineBarRegistry>();

            registry.RegisterTimelineBarElement("Comment", () => this.container.Resolve<ICommentEditBoxPresentationModel>());
        }

        /// <summary>
        /// Registers the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<ICommentView, CommentView>();
            this.container.RegisterType<ICommentViewPresentationModel, CommentViewPresentationModel>();

            this.container.RegisterType<ICommentViewPreview, CommentViewPreview>();
            this.container.RegisterType<ICommentEditBox, CommentEditBox>();
            this.container.RegisterType<ICommentEditBoxPresentationModel, CommentEditBoxPresentationModel>();
        }
    }
}
