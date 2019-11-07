// <copyright file="ServicesModule.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SettingsModule.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.Globalization;

    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Modularity;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Practices.Composite.UnityExtensions;
    using Microsoft.Practices.Unity;

    using RCE.Infrastructure.Services;
    using RCE.Modules.Services.Services;

    using Views;

    /// <summary>
    /// Provides an implementation of the <seealso cref="IModule"/>. Defines the services module.
    /// </summary>
    public class ServicesModule : IModule
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
        /// Initializes a new instance of the <see cref="ServicesModule"/> class. 
        /// </summary>
        /// <param name="container">The <seealso cref="IUnityContainer"/> container used to resolve dependencies.</param>
        /// <param name="regionManager">The <seealso cref="IRegionManager"/> used to get the <seealso cref="IRegion"/>s.</param>
        [CLSCompliant(false)]
        public ServicesModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        /// <summary>
        /// Initializes the Settings module.
        /// </summary>
        public void Initialize()
        {
            this.RegisterViewsAndServices();

            this.container.Resolve<IProjectService>();

            INotificationViewPresenter presenter = this.container.Resolve<INotificationViewPresenter>();

            this.regionManager.Regions[RegionNames.NotificationsRegion].Add(presenter.View);

            ICacheManager cacheManager = this.container.Resolve<ICacheManager>();
        }

        /// <summary>
        /// Registers the views and presentation models used by the module.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<INotificationView, NotificationView>();

            this.container.RegisterType<INotificationViewPresenter, NotificationViewPresenter>();

            this.RegisterTypeIfMissing<IDataServiceFacade, DataServiceFacade>();

            this.RegisterTypeIfMissing<IAssetsDataServiceFacade, AssetsDataServiceFacade>();

            this.RegisterTypeIfMissing<IEventDataParser<EventData>, DefaultEventDataParser>();

            this.RegisterTypeIfMissing<IEventDataParser<EventOffset>, DefaultEventOffsetParser>();

            this.RegisterTypeIfMissing<IThumbnailService, AssetsThumbnailService>(true);

            this.container.RegisterType<IProjectService, ProjectService>(new ContainerControlledLifetimeManager());

            this.container.RegisterType<ITimelineBarRegistry, TimelineBarRegistry>(new ContainerControlledLifetimeManager());

            this.container.RegisterType<ICodecPrivateDataParser, CodecPrivateDataParser>(new ContainerControlledLifetimeManager());

            //// this.RegisterTypeIfMissing<ICache, MemorySmoothStreamingCache>("PrimaryCache", true);

            this.RegisterTypeIfMissing<ICache, IsolatedStorageSmoothStreamingCache>("PrimaryCache", true);

            this.RegisterTypeIfMissing<ICache, CompositeSmoothStreamingCache>(true);

            this.RegisterTypeIfMissing<IPurgeStrategy, FifoPurgeStrategy>(true);

            this.RegisterTypeIfMissing<ICacheManager, CacheManager>(true);

            this.RegisterTypeIfMissing<KeyboardManagerService, DefaultKeyboardManagerService>("DefaultKeyboardManager", true);

            this.RegisterTypeIfMissing<KeyboardManagerService, XmlKeyboardManagerService>(true);

            this.RegisterTypeIfMissing<IUserSettingsService, UserSettingsService>(true);

            this.RegisterTypeIfMissing<ISequenceRegistry, SequenceRegistry>(true);

            IConfigurationService configurationService = this.container.Resolve<IConfigurationService>();

            string logEntriesRetrievalMode = configurationService.GetParameterValue("LogEntriesRetrievalMode");
            Type collectionType = GetCollectionType(logEntriesRetrievalMode);

            this.RegisterTypeIfMissing(typeof(ILogEntryCollection), collectionType);
        }

        private static Type GetCollectionType(string logEntriesRetrievalMode)
        {
            switch (logEntriesRetrievalMode.ToLower(CultureInfo.CurrentCulture))
            {
                case "polling":
                    return typeof(PollingLogEntryCollection);
                case "embedded":
                default:
                    return typeof(InManifestLogEntryCollection);
            }
        }

        /// <summary>
        /// Registers a type if is not already registered in the container.
        /// </summary>
        /// <typeparam name="TInterface">The interface.</typeparam>
        /// <typeparam name="TImplementation">The implemenentation.</typeparam>
        private void RegisterTypeIfMissing<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            this.RegisterTypeIfMissing<TInterface, TImplementation>(false);
        }

        /// <summary>
        /// Registers a type if is not already registered in the container.
        /// </summary>
        /// <typeparam name="TInterface">The interface.</typeparam>
        /// <typeparam name="TImplementation">The implemenentation.</typeparam>
        /// <param name="isSingleton">Defines if the type should be registered as singleton or not.</param>
        private void RegisterTypeIfMissing<TInterface, TImplementation>(bool isSingleton) 
            where TImplementation : TInterface
        {
            if (!this.container.IsTypeRegistered(typeof(TInterface)))
            {
                if (isSingleton)
                {
                    this.container.RegisterType<TInterface, TImplementation>(new ContainerControlledLifetimeManager());
                }
                else
                {
                    this.container.RegisterType<TInterface, TImplementation>();
                }
            }
        }

        /// <summary>
        /// Registers a type if is not already registered in the container.
        /// </summary>
        /// <typeparam name="TInterface">The interface.</typeparam>
        /// <typeparam name="TImplementation">The implemenentation.</typeparam>
        /// <param name="name">Defines if the name of the registered element.</param>
        /// <param name="isSingleton">Defines if the type should be registered as singleton or not.</param>
        private void RegisterTypeIfMissing<TInterface, TImplementation>(string name, bool isSingleton)
            where TImplementation : TInterface
        {
            if (!this.container.IsTypeRegistered(typeof(TInterface)))
            {
                if (isSingleton)
                {
                    this.container.RegisterType<TInterface, TImplementation>(name, new ContainerControlledLifetimeManager());
                }
                else
                {
                    this.container.RegisterType<TInterface, TImplementation>(name);
                }
            }
        }

        /// <summary>
        /// Registers a type if is not already registered in the container.
        /// </summary>
        /// <param name="interfaceType">The interface.</param>
        /// <param name="implementationType">The implemenentation.</param>
        /// <param name="isSingleton">Defines if the type should be registered as singleton or not.</param>
        private void RegisterTypeIfMissing(Type interfaceType, Type implementationType, bool isSingleton = false)
        {
            if (!this.container.IsTypeRegistered(interfaceType))
            {
                if (isSingleton)
                {
                    this.container.RegisterType(interfaceType, implementationType, new ContainerControlledLifetimeManager());
                }
                else
                {
                    this.container.RegisterType(interfaceType, implementationType);
                }
            }
        }
    }
}