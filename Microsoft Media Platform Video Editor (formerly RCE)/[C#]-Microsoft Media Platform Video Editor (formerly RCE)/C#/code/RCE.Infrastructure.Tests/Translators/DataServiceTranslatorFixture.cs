// <copyright file="DataServiceTranslatorFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DataServiceTranslatorFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Infrastructure.Models;
    using Infrastructure.Translators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Services.Contracts;

    using Project = RCE.Services.Contracts.Project;
    using TitleTemplate = RCE.Services.Contracts.TitleTemplate;

    /// <summary>
    /// A class for testing the <see cref="DataServiceTranslator"/>.
    /// </summary>
    [TestClass]
    public class DataServiceTranslatorFixture
    {
        /// <summary>
        /// Tests that an empty list of assets is returned when the container passed is null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnZeroAssetsWhenContainerIsNull()
        {
            List<Asset> assets = DataServiceTranslator.ConvertToAssets(null);

            Assert.AreEqual(0, assets.Count);
        }

        /// <summary>
        /// Tests that a container is being converted to a list of assets.
        /// </summary>
        [TestMethod]
        public void ShouldConvertContainerIntoAssets()
        {
            var resource1 = new Resource { Ref = "RefResource", ResourceType = "Master" };
            var resource2 = new Resource { Ref = "http://uri/test.ism/manifest", ResourceType = "SmoothStream" };

            var videoItem1 = new VideoItem
                                 {
                                     Id = new Uri("http://uri"),
                                     Title = "VideoTitle",
                                     Duration = 10,
                                     Resources = new ResourceCollection() { resource1 },
                                     Metadata = new List<MetadataField> { new MetadataField("TestName", "TestValue") },
                                     ThumbnailSource = "http://test1/test.png"
                                 };

            var videoItem2 = new VideoItem
                                 {
                                     Id = new Uri("http://uri2"),
                                     Title = "VideoAdaptiveStreamingTitle",
                                     Duration = 10,
                                     Creator = "test",
                                     Created = new DateTime(2009, 1, 1),
                                     Modified = new DateTime(2009, 1, 2),
                                     ModifiedBy = "test1",
                                     Resources = new ResourceCollection { resource2 }
                                 };

            var items = new ItemCollection() { videoItem1, videoItem2 };

            var container = new Container { Items = items };

            List<Asset> assets = DataServiceTranslator.ConvertToAssets(container);

            Assert.AreEqual(2, assets.Count);
            Assert.AreEqual(videoItem1.Id, assets[0].ProviderUri);
            Assert.AreEqual(videoItem1.Title, assets[0].Title);
            Assert.AreEqual(videoItem1.Creator, assets[0].Creator);
            Assert.AreEqual(videoItem1.Created, assets[0].Created);
            Assert.AreEqual(videoItem1.ModifiedBy, assets[0].ModifiedBy);
            Assert.AreEqual(videoItem1.Modified, assets[0].Modified);
            Assert.AreEqual(videoItem1.Metadata, assets[0].Metadata);
            Assert.AreEqual(videoItem1.ThumbnailSource, ((VideoAsset)assets[0]).ThumbnailSource);
            StringAssert.Contains(assets[0].Source.ToString(), resource1.Ref);
            Assert.AreEqual(assets[0].ResourceType, ResourceType.Master);
            Assert.AreEqual(videoItem2.Id, assets[1].ProviderUri);
            Assert.AreEqual(videoItem2.Title, assets[1].Title);
            Assert.AreEqual(videoItem2.Creator, assets[1].Creator);
            Assert.AreEqual(videoItem2.Created, assets[1].Created);
            Assert.AreEqual(videoItem2.ModifiedBy, assets[1].ModifiedBy);
            Assert.AreEqual(videoItem2.Modified, assets[1].Modified);
            StringAssert.Contains(assets[1].Source.ToString(), resource2.Ref);
            Assert.AreEqual(assets[1].ResourceType, ResourceType.SmoothStream);
        }

        /// <summary>
        /// Tests that child containers are being converted into folder assets.
        /// </summary>
        [TestMethod]
        public void ShouldConvertChildContainerIntoFolderAssets()
        {
            var resource = new Resource { Ref = "RefResource", ResourceType = "Master" };

            var videoItem = new VideoItem
                                {
                                    Id = new Uri("http://uri"),
                                    Title = "VideoTitle",
                                    Duration = 10,
                                    Resources = new ResourceCollection { resource }
                                };

            var childVideoItem = new VideoItem
                                     {
                                         Id = new Uri("http://uri1"),
                                         Title = "VideoTitle",
                                         Duration = 10,
                                         Resources = new ResourceCollection { resource }
                                     };

            var items = new ItemCollection { videoItem };

            var container = new Container { Items = items, Containers = new ContainerCollection() };

            var childContainer = new Container
                                     {
                                         Id = new Uri("http://childContainer"), 
                                         Title = "Folder", 
                                         Items = new ItemCollection { childVideoItem }
                                     };

            container.Containers.Add(childContainer);

            List<Asset> assets = DataServiceTranslator.ConvertToAssets(container);

            Assert.AreEqual(2, assets.Count);
            Assert.IsInstanceOfType(assets[1], typeof(FolderAsset));
            Assert.AreEqual(childContainer.Title, assets[1].Title);
            Assert.AreEqual(childContainer.Id, assets[1].ProviderUri);
            Assert.IsNull(((FolderAsset)assets[1]).ParentFolder);
            Assert.AreEqual(childVideoItem.Id, ((FolderAsset)assets[1]).Assets[0].ProviderUri);
            Assert.AreEqual(childVideoItem.Title, ((FolderAsset)assets[1]).Assets[0].Title);
            Assert.AreEqual(childVideoItem.Resources[0].ResourceType.ToUpper(), ((FolderAsset)assets[1]).Assets[0].ResourceType.ToString().ToUpper());
        }

        /// <summary>
        /// Tests that the Content Network Prefix is being appended to the ref if is not a valid absolute uri.
        /// </summary>
        [TestMethod]
        public void ShouldAppendContentNetworkPrefixIfRefIsNotAValidAbsoluteUri()
        {
            DataServiceTranslator.ContentNetworkPrefix = "http://prefix/";

            var resource = new Resource { Ref = "RefResource", ResourceType = "LiveSmoothStream" };

            var videoItem = new VideoItem
                                {
                                    Id = new Uri("http://uri"),
                                    Title = "VideoTitle",
                                    Duration = 10,
                                    Resources = new ResourceCollection { resource }
                                };

            var items = new ItemCollection { videoItem };

            var container = new Container { Items = items };

            List<Asset> assets = DataServiceTranslator.ConvertToAssets(container);

            Assert.AreEqual(string.Concat(DataServiceTranslator.ContentNetworkPrefix, videoItem.Resources[0].Ref), assets[0].Source.ToString());
        }

        /// <summary>
        /// Tests that the Content Network Prefix is not being appended to the ref if is a valid absolute uri.
        /// </summary>
        [TestMethod]
        public void ShouldNotAppendContentNetworkPrefixIfRexIsAValidAbsoluteUri()
        {
            DataServiceTranslator.ContentNetworkPrefix = "http://prefix/";

            var resource = new Resource { Ref = "http://www.microsoft.com/test.wmv", ResourceType = "Master" };

            var videoItem = new VideoItem
                                {
                                    Id = new Uri("http://uri"),
                                    Title = "VideoTitle",
                                    Duration = 10,
                                    Resources = new ResourceCollection { resource }
                                };

            var items = new ItemCollection { videoItem };

            var container = new Container { Items = items };

            List<Asset> assets = DataServiceTranslator.ConvertToAssets(container);

            Assert.AreEqual("http://www.microsoft.com/test.wmv", assets[0].Source.ToString());
        }

        /// <summary>
        /// Tests that an empty collection of assets is being returned if the container is emtpy.
        /// </summary>
        [TestMethod]
        public void ShouldReturnZeroAssetsWhenCollectionIsEmpty()
        {
            var container = new Container { Items = new ItemCollection() };

            List<Asset> assets = DataServiceTranslator.ConvertToAssets(container);

            Assert.AreEqual(0, assets.Count);
        }

        /// <summary>
        /// Tests that an empty collection is returned if the container is null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnZeroAssetsWhenCollectionIsNull()
        {
            var container = new Container { Items = null };

            List<Asset> assets = DataServiceTranslator.ConvertToAssets(container);

            Assert.AreEqual(0, assets.Count);
        }

        /// <summary>
        /// Tests that null is returned, if <see cref="Services.Contracts.Project"/> is null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullIfProjectContainerIsNull()
        {
            var result = DataServiceTranslator.ConvertToProject(null);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests that <see cref="Services.Contracts.Project"/> is converted to <see cref="Infrastructure.Models.Project"/> successfully.
        /// </summary>
        [TestMethod]
        public void ShouldConvertServiceProjectIntoAProject()
        {
            var serviceProject = TranslatorHelper.CreateServiceProject();

            var project = DataServiceTranslator.ConvertToProject(serviceProject);

            TranslatorHelper.AssertProject(serviceProject, project);
        }

        /// <summary>
        /// Tests that <see cref="Infrastructure.Models.Project"/> is converted to <see cref="Services.Contracts.Project"/> successfully.
        /// </summary>
        [TestMethod]
        public void ShouldConvertProjectIntoAServiceProject()
        {
            var project = TranslatorHelper.CreateProject();

            var serviceProject = DataServiceTranslator.ConvertToDataServiceProject(project);

            TranslatorHelper.AssertProject(project, serviceProject);
        }

        /// <summary>
        /// Tests that the collection of <see cref="Services.Contracts.Project"/> is converted to collection of <see cref="Infrastructure.Models.Project"/> successfully.
        /// </summary>
        [TestMethod]
        public void ShouldConvertCollectionOfServiceProjectsIntoAListProjects()
        {
            var serviceProject1 = TranslatorHelper.CreateServiceProject();
            var serviceProject2 = TranslatorHelper.CreateServiceProject();

            var projectsCollection = new ObservableCollection<Project> { serviceProject1, serviceProject2 };

            var projects = DataServiceTranslator.ConvertToProjects(projectsCollection);

            Assert.AreEqual(projectsCollection.Count, projects.Count);

            TranslatorHelper.AssertProject(projectsCollection[0], projects[0]);
            TranslatorHelper.AssertProject(projectsCollection[1], projects[1]);
        }

        /// <summary>
        /// Tests that collection of <see cref="Title"/> is converted to collection of <see cref="TitleAsset"/> successfully.
        /// </summary>
        [TestMethod]
        public void ShouldConvertCollectionOfTitlesIntoListOfTitleAssets()
        {
            var serviceTitles = new ObservableCollection<Title>();
            serviceTitles.Add(TranslatorHelper.CreateServiceTitle());
            serviceTitles.Add(TranslatorHelper.CreateServiceTitle());

            var titles = DataServiceTranslator.ConvertToTitles(serviceTitles);

            TranslatorHelper.AssertTitle(serviceTitles[0], titles[0]);
            TranslatorHelper.AssertTitle(serviceTitles[1], titles[1]);
        }

        /// <summary>
        /// Tests that collection of <see cref="Services.Contracts.TitleTemplate"/> is converted to collection of <see cref="Infrastructure.Models.TitleTemplate"/> successfully.
        /// </summary>
        [TestMethod]
        public void ShouldConvertCollectionOfTitleTemplatesIntoListOfTitleTemplates()
        {
            var serviceTitleTemplates = new ObservableCollection<TitleTemplate>();
            serviceTitleTemplates.Add(TranslatorHelper.CreateServiceTitleTemplate());
            serviceTitleTemplates.Add(TranslatorHelper.CreateServiceTitleTemplate());

            var titleTemplates = DataServiceTranslator.ConvertToTitleTemplates(serviceTitleTemplates);

            TranslatorHelper.AssertTitleTemplate(serviceTitleTemplates[0], titleTemplates[0]);
            TranslatorHelper.AssertTitleTemplate(serviceTitleTemplates[1], titleTemplates[1]);
        }

        /// <summary>
        /// Tests if the service mediabin is converted to client mediabin.
        /// </summary>
        [TestMethod]
        public void ShouldConvertToMediaBin()
        {
            var serviceContainer = TranslatorHelper.CreateServiceMediaBin();

            var container = DataServiceTranslator.ConvertToMediaBin(serviceContainer);

            TranslatorHelper.AssertContainer(serviceContainer, container);
        }
    }
}