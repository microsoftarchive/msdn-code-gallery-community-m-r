// <copyright file="MetadataViewPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadataViewPresentationModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin.Tests.Views
{
    using System;
    using System.Xml.Linq;
    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Test class for <see cref="MetadataViewPresentationModel"/>.
    /// </summary>
    [TestClass]
    public class MetadataViewPresentationModelFixture
    {
        /// <summary>
        /// Mock for <see cref="IConfigurationService"/>.
        /// </summary>
        private MockConfigurationService configurationService;

        /// <summary>
        /// Mock for <see cref="IEventDataParser{T}"/>.
        /// </summary>
        private MockEventDataParser eventDataParser;

        /// <summary>
        /// Mock for <see cref="IEventDataParser{T}" />.
        /// </summary>
        private MockEventOffsetParser eventOffsetParser;

        /// <summary>
        /// Initilize the default values.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.configurationService = new MockConfigurationService();
            this.eventDataParser = new MockEventDataParser();
            this.eventOffsetParser = new MockEventOffsetParser();
        }

        /// <summary>
        /// Tests if the MetadataFilters are being populated when the presentation model is created.
        /// </summary>
        [TestMethod]
        public void ShouldPopulateMetadataFilters()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(presentationModel.MetadataFilters.Count > 0);
        }

        /// <summary>
        /// Tests if tthe SelectedMetadataFilter is being set when the presentation model is created.
        /// </summary>
        [TestMethod]
        public void ShouldSetSelectedMetadataFilter()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(presentationModel.MetadataFilters[0], presentationModel.SelectedMetadataFilter);
        }

        /// <summary>
        /// Tests if the ParseEventData method is being called when the in stream collection being set contains items.
        /// </summary>
        [TestMethod]
        public void ShouldAddEventDataWhenSettingInStreamData()
        {
            var presentationModel = this.CreatePresentationModel();

            MockLogEntryCollection collection = new MockLogEntryCollection();
            collection.ItemCollection.Add(new EventData("1", TimeSpan.FromSeconds(1), "Testing text 1"));
            collection.ItemCollection.Add(new EventData("2", TimeSpan.FromSeconds(2), "Testing text 2"));
            collection.ItemCollection.Add(new EventData("3", TimeSpan.FromSeconds(3), "Testing text 3"));

            Assert.AreEqual(0, presentationModel.Metadata.Count);
            presentationModel.SetInStreamData(collection);
            Assert.AreEqual(3, presentationModel.Metadata.Count);
            Assert.AreEqual("1", presentationModel.Metadata[0].Id);
            Assert.AreEqual(TimeSpan.FromSeconds(1), presentationModel.Metadata[0].Time);
            Assert.AreEqual("Testing text 1", presentationModel.Metadata[0].Text);
            Assert.AreEqual("2", presentationModel.Metadata[1].Id);
            Assert.AreEqual(TimeSpan.FromSeconds(2), presentationModel.Metadata[1].Time);
            Assert.AreEqual("Testing text 2", presentationModel.Metadata[1].Text);
            Assert.AreEqual("3", presentationModel.Metadata[2].Id);
            Assert.AreEqual(TimeSpan.FromSeconds(3), presentationModel.Metadata[2].Time);
            Assert.AreEqual("Testing text 3", presentationModel.Metadata[2].Text);
        }

        [TestMethod]
        public void ShouldAddEventDataWhenEventDataAddedEventIsRaised()
        {
            var presentationModel = this.CreatePresentationModel();

            MockLogEntryCollection collection = new MockLogEntryCollection();
            presentationModel.SetInStreamData(collection);

            Assert.AreEqual(0, presentationModel.Metadata.Count);

            collection.InvokeEventDataAdded(new EventData("1", TimeSpan.FromSeconds(1), "Testing text 1"));

            Assert.AreEqual("1", presentationModel.Metadata[0].Id);
            Assert.AreEqual(TimeSpan.FromSeconds(1), presentationModel.Metadata[0].Time);
            Assert.AreEqual("Testing text 1", presentationModel.Metadata[0].Text);
        }

        [TestMethod]
        public void ShouldRemoveEventDataWhenEventDataRemovedEventIsRaised()
        {
            var presentationModel = this.CreatePresentationModel();

            MockLogEntryCollection collection = new MockLogEntryCollection();
            var eventData = new EventData("1", TimeSpan.FromSeconds(1), "Testing text 1");
            collection.ItemCollection.Add(eventData);
            presentationModel.SetInStreamData(collection);

            Assert.AreEqual(1, presentationModel.Metadata.Count);

            collection.InvokeEventDataRemoved(eventData);

            Assert.AreEqual(0, presentationModel.Metadata.Count);
        }

        /// <summary>
        /// Creates the <see cref="MetadataViewPresentationModel"/>.
        /// </summary>
        /// <returns>The <see cref="MetadataViewPresentationModel"/>.</returns>
        private MetadataViewPresentationModel CreatePresentationModel()
        {
            return new MetadataViewPresentationModel(this.configurationService, this.eventDataParser, this.eventOffsetParser);
        }
    }
}
