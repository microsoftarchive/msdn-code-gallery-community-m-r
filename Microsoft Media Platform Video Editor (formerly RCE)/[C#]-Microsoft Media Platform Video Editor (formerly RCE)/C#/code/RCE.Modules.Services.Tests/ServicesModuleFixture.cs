// <copyright file="ServicesModuleFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TitlesModuleFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests
{
    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.Practices.Unity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mocks;
    using Modules.Services.Views;

    using RCE.Infrastructure.Services;
    using RCE.Modules.Services.Services;

    /// <summary>
    /// Test class for <see cref="ServicesModule"/>.
    /// </summary>
    [TestClass]
    public class ServicesModuleFixture
    {
        /// <summary>
        /// Should register the views and models.
        /// </summary>
        [TestMethod]
        public void ShouldRegisterViews()
        {
            var configurationService = new MockConfigurationService();
            var container = new MockUnityContainer();
            var module = new TestableServicesModule(container);

            container.Bag.Add(typeof(IConfigurationService), configurationService);

            configurationService.GetParameterValueReturnFunction = p => "polling";

            module.InvokeRegisterViewsAndServices();

            Assert.AreEqual(17, container.Types.Count);
            Assert.AreEqual(typeof(NotificationView), container.Types[typeof(INotificationView)]);
            Assert.AreEqual(typeof(NotificationViewPresenter), container.Types[typeof(INotificationViewPresenter)]);
            Assert.AreEqual(typeof(DataServiceFacade), container.Types[typeof(IDataServiceFacade)]);
            Assert.AreEqual(typeof(AssetsDataServiceFacade), container.Types[typeof(IAssetsDataServiceFacade)]);
            Assert.AreEqual(typeof(DefaultEventDataParser), container.Types[typeof(IEventDataParser<EventData>)]);
            Assert.AreEqual(typeof(DefaultEventOffsetParser), container.Types[typeof(IEventDataParser<EventOffset>)]);
            Assert.AreEqual(typeof(ProjectService), container.Types[typeof(IProjectService)]);
            Assert.AreEqual(typeof(TimelineBarRegistry), container.Types[typeof(ITimelineBarRegistry)]);
            Assert.AreEqual(typeof(CodecPrivateDataParser), container.Types[typeof(ICodecPrivateDataParser)]);
            Assert.AreEqual(typeof(CompositeSmoothStreamingCache), container.Types[typeof(ICache)]);
            Assert.AreEqual(typeof(FifoPurgeStrategy), container.Types[typeof(IPurgeStrategy)]);
            Assert.AreEqual(typeof(CacheManager), container.Types[typeof(ICacheManager)]);
            Assert.AreEqual(typeof(XmlKeyboardManagerService), container.Types[typeof(KeyboardManagerService)]);
            Assert.AreEqual(typeof(UserSettingsService), container.Types[typeof(IUserSettingsService)]);
            Assert.AreEqual(typeof(SequenceRegistry), container.Types[typeof(ISequenceRegistry)]);
            Assert.AreEqual(typeof(PollingLogEntryCollection), container.Types[typeof(ILogEntryCollection)]);

            Assert.AreEqual(2, container.MappingInformation.Count);

            Assert.AreEqual(typeof(IsolatedStorageSmoothStreamingCache), container.MappingInformation[typeof(ICache)].Item2);
            Assert.AreEqual("PrimaryCache", container.MappingInformation[typeof(ICache)].Item1);
            Assert.AreEqual(typeof(DefaultKeyboardManagerService), container.MappingInformation[typeof(KeyboardManagerService)].Item2);
            Assert.AreEqual("DefaultKeyboardManager", container.MappingInformation[typeof(KeyboardManagerService)].Item1);
        }

        /// <summary>
        /// Should add <see cref="NotificationView"/> to tools region.
        /// </summary>
        [TestMethod]
        public void ShouldAddNotificationViewToNotificationsRegion()
        {
            var toolsRegion = new MockRegion();
            var regionManager = new MockRegionManager();
            var container = new MockUnityContainer();
            var configurationService = new MockConfigurationService();

            container.Bag.Add(typeof(INotificationViewPresenter), new MockNotificationViewPresenter());
            container.Bag.Add(typeof(IProjectService), new MockProjectService());
            container.Bag.Add(typeof(ICacheManager), new MockCacheManager());
            container.Bag.Add(typeof(IConfigurationService), configurationService);

            toolsRegion.Name = RegionNames.NotificationsRegion;
            regionManager.Regions.Add(toolsRegion);
            configurationService.GetParameterValueReturnFunction = p => "polling";

            var module = new ServicesModule(container, regionManager);

            Assert.AreEqual(0, toolsRegion.AddedViews.Count);

            module.Initialize();

            Assert.AreEqual(1, toolsRegion.AddedViews.Count);
            Assert.IsInstanceOfType(toolsRegion.AddedViews[0], typeof(INotificationView));
        }

        /// <summary>
        /// Testable class for <see cref="ServicesModule"/>.
        /// </summary>
        private class TestableServicesModule : ServicesModule
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableServicesModule"/> class.
            /// </summary>
            /// <param name="container">The container.</param>
            public TestableServicesModule(IUnityContainer container)
                : base(container, new MockRegionManager())
            {
            }

            /// <summary>
            /// Invokes the register views and services.
            /// </summary>
            public void InvokeRegisterViewsAndServices()
            {
                this.RegisterViewsAndServices();
            }
        }
    }
}