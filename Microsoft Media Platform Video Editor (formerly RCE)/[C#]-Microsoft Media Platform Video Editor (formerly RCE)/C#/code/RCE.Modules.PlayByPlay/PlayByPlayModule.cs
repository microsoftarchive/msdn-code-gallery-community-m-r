// <copyright file="PlayByPlayModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayByPlayModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.PlayByPlay
{
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Services;
    using RCE.Modules.PlayByPlay.Views.TimelineBar;
    using RCE.Modules.PlayByPlayMarker;

    /// <summary>
    /// Class to load the PlayByPlayModule Module.
    /// </summary>
    public class PlayByPlayModule : IModule
    {
        /// <summary>
        /// The <see cref="IUnityContainer"/> to register the views and services.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayByPlayModule"/> class.
        /// </summary>
        /// <param name="container">The instance of <see cref="IUnityContainer"/> interface.</param>
        public PlayByPlayModule(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Initializes the PlayByPlayModule instance.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            ITimelineBarRegistry registry = this.container.Resolve<ITimelineBarRegistry>();

            registry.RegisterTimelineBarElement("PlayByPlay", () => this.container.Resolve<IPlayByPlayBoxesPresentationModel>());
        }

        /// <summary>
        /// Registers all the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<IPlayByPlayViewPreview, PlayByPlayView>();
            this.container.RegisterType<IPlayByPlayDisplayBox, PlayByPlayDisplayBox>();
            this.container.RegisterType<IPlayByPlayEditBox, PlayByPlayEditBox>();
            this.container.RegisterType<IPlayByPlayBoxesPresentationModel, PlayByPlayBoxesPresentationModel>();
        }
    }
}
