// <copyright file="LibraryViewPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LibraryViewPresentationModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library.Tests.Views
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Services;

    /// <summary>
    /// Test class for <see cref="LibraryViewPresentationModel"/>.
    /// </summary>
    [TestClass]
    public class LibraryViewPresentationModelFixture
    {
        /// <summary>
        /// Mock for <see cref="ILibraryView"/>.
        /// </summary>
        private MockLibraryView view;

        /// <summary>
        /// Mock for <see cref="IEventAggregator"/>.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="AddAssetEvent"/>.
        /// </summary>
        private MockAddAssetEvent addAssetEvent;

        /// <summary>
        /// Mock for <see cref="PlayerEvent"/>.
        /// </summary>
        private MockPlayerEvent playerEvent;

        /// <summary>
        /// Mock for <see cref="AssetsAvailableEvent"/>
        /// </summary>
        private MockAssetsAvailableEvent assetsAvailableEvent;

        /// <summary>
        /// Mock for <see cref="IRegionManager"/>.
        /// </summary>
        private MockRegionManager regionManager;

        /// <summary>
        /// Mock for <see cref="IConfigurationService"/>.
        /// </summary>
        private MockConfigurationService configurationService;

        /// <summary>
        /// Mock for <see cref="IRegion"/>.
        /// </summary>
        private MockRegion mainRegion;

        /// <summary>
        /// Mock for <see cref="ShowMetadataEvent"/>.
        /// </summary>
        private MockShowMetadataEvent showMetadataEvent;

        /// <summary>
        /// Mock for <see cref="IErrorView"/>.
        /// </summary>
        private MockErrorView errorView;

        private MockResetWindowsEvent resetWindowsEvent;

        private MockAssetsLoadingEvent assetsLoadingEvent;

        /// <summary>
        /// Sets up the initial values for the test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.playerEvent = new MockPlayerEvent();
            this.view = new MockLibraryView();
            this.regionManager = new MockRegionManager();
            this.configurationService = new MockConfigurationService();
            this.addAssetEvent = new MockAddAssetEvent();
            this.assetsAvailableEvent = new MockAssetsAvailableEvent();
            this.showMetadataEvent = new MockShowMetadataEvent();
            this.resetWindowsEvent = new MockResetWindowsEvent();
            this.assetsLoadingEvent = new MockAssetsLoadingEvent();
            this.mainRegion = new MockRegion();
            this.errorView = new MockErrorView();

            this.mainRegion.Name = RegionNames.MainRegion;
            this.regionManager.Regions.Add(this.mainRegion);

            this.eventAggregator = new MockEventAggregator();
            this.eventAggregator.AddMapping<AddAssetEvent>(this.addAssetEvent);
            this.eventAggregator.AddMapping<PlayerEvent>(this.playerEvent);
            this.eventAggregator.AddMapping<ShowMetadataEvent>(this.showMetadataEvent);
            this.eventAggregator.AddMapping<AssetsAvailableEvent>(this.assetsAvailableEvent);
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowsEvent);
            this.eventAggregator.AddMapping<AssetsLoadingEvent>(this.assetsLoadingEvent);
        }

        /// <summary>
        /// Tests if the <see cref="LibraryViewPresentationModel"/>
        /// is initlializing the <see cref="LibraryView"/>.
        /// </summary>
        [TestMethod]
        public void CanInitPresentationModel()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(this.view, presentationModel.View);
        }

        /// <summary>
        /// Tests if the <see cref="LibraryViewPresentationModel"/>
        /// is setting the Model in <see cref="LibraryView"/>.
        /// </summary>
        [TestMethod]
        public void ShouldSetPresentationModelIntoView()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreSame(presentationModel, this.view.Model);
        }

        /// <summary>
        /// Tests if the <see cref="LibraryViewPresentationModel"/>
        /// is showing the progress bar at the time of loading it.
        /// </summary>
        [TestMethod]
        public void ShouldCallToShowProgressBar()
        {
            Assert.IsFalse(this.view.ShowProgressBarCalled);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(this.view.ShowProgressBarCalled);
        }

        /// <summary>
        /// Tests if the <see cref="LibraryViewPresentationModel"/>
        /// is calling the <see cref="configurationService"/> to 
        /// get the metadata fields which will come in the list view.
        /// </summary>
        [TestMethod]
        public void ShouldCallToGetParameterValueOnTheConfigurationServiceWithMetadataFieldsAsParameter()
        {
            bool getMetadataCalled = false;
            this.configurationService.GetParameterValueReturnFunction = parameter =>
                                                                            {
                                                                                if (parameter == "MetadataFields")
                                                                                {
                                                                                    getMetadataCalled = true;
                                                                                }

                                                                                return string.Empty;
                                                                            };

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(getMetadataCalled);
        }
        
        /// <summary>
        /// Tests if OnPropertyChanged is raised when Asset
        /// property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenAssetsIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
                                                     {
                                                         propertyChangedRaised = true;
                                                         propertyChangedEventArgsArgument = e.PropertyName;
                                                     };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.Assets = new List<Asset>();

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Assets", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should show only images assets if 
        /// show images is true and show audio/video is false.
        /// </summary>
        [TestMethod]
        public void ShouldShowOnlyImagesAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            var assets = new List<Asset> { imageAsset, videoAsset, audioAsset };

            var presentationModel = this.CreatePresentationModel();

            this.assetsAvailableEvent.SubscribeArgumentAction.Invoke(new Infrastructure.DataEventArgs<List<Asset>>(assets));

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.ShowVideos = false;
            presentationModel.ShowAudio = false;

            Assert.AreEqual(1, presentationModel.Assets.Count);
            Assert.AreSame(imageAsset, presentationModel.Assets[0]);
        }

        /// <summary>
        /// Should show only images assets if 
        /// show videos is true and show audios/imgages is false.
        /// </summary>
        [TestMethod]
        public void ShouldShowOnlyVideosAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            var assets = new List<Asset> { imageAsset, videoAsset, audioAsset };

            var presentationModel = this.CreatePresentationModel();

            this.assetsAvailableEvent.SubscribeArgumentAction.Invoke(new Infrastructure.DataEventArgs<List<Asset>>(assets));

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.ShowImages = false;
            presentationModel.ShowAudio = false;

            Assert.AreEqual(1, presentationModel.Assets.Count);
            Assert.AreSame(videoAsset, presentationModel.Assets[0]);
        }

        /// <summary>
        /// Should show only images assets if 
        /// show audios is true and show images/video is false.
        /// </summary>
        [TestMethod]
        public void ShouldShowOnlyAudioAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            var assets = new List<Asset> { imageAsset, videoAsset, audioAsset };

            var presentationModel = this.CreatePresentationModel();

            this.assetsAvailableEvent.SubscribeArgumentAction.Invoke(new Infrastructure.DataEventArgs<List<Asset>>(assets));

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.ShowImages = false;
            presentationModel.ShowVideos = false;

            Assert.AreEqual(1, presentationModel.Assets.Count);
            Assert.AreSame(audioAsset, presentationModel.Assets[0]);
        }

        /// <summary>
        /// Show show nothing is show audios/videos/images is false.
        /// </summary>
        [TestMethod]
        public void ShouldShowNoAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            var assets = new List<Asset> { imageAsset, videoAsset, audioAsset };

            var presentationModel = this.CreatePresentationModel();

            this.assetsAvailableEvent.SubscribeArgumentAction.Invoke(new Infrastructure.DataEventArgs<List<Asset>>(assets));

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.ShowImages = false;
            presentationModel.ShowVideos = false;
            presentationModel.ShowAudio = false;

            Assert.AreEqual(0, presentationModel.Assets.Count);
        }

        /// <summary>
        /// Should show all assets if show audios/videos/images is true.
        /// </summary>
        [TestMethod]
        public void ShouldShowAllAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            var assets = new List<Asset> { imageAsset, videoAsset, audioAsset };

            var presentationModel = this.CreatePresentationModel();

            this.assetsAvailableEvent.SubscribeArgumentAction.Invoke(new Infrastructure.DataEventArgs<List<Asset>>(assets));

            presentationModel.ShowImages = false;
            presentationModel.ShowVideos = false;
            presentationModel.ShowAudio = false;

            Assert.AreEqual(0, presentationModel.Assets.Count);

            presentationModel.ShowImages = true;
            presentationModel.ShowVideos = true;
            presentationModel.ShowAudio = true;

            Assert.AreEqual(3, presentationModel.Assets.Count);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when ShowVideos property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenShowVideosIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.ShowVideos = false;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("ShowVideos", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when ShowAudios property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenShowAudioIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.ShowAudio = false;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("ShowAudio", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when ShowImages property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenShowImagesIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.ShowImages = false;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("ShowImages", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if AddAsset event is published when AddItem command executed.
        /// </summary>
        [TestMethod]
        public void ShouldPublishAddAssetEventWhenExecutingAddItemCommand()
        {
            var asset = new VideoAsset();
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.addAssetEvent.PublishCalled);

            presentationModel.Assets = new List<Asset> { asset };

            presentationModel.AddItemCommand.Execute(asset.Id);

            Assert.IsTrue(this.addAssetEvent.PublishCalled);
            Assert.AreEqual(asset, this.addAssetEvent.Asset);
        }

        /// <summary>
        /// Tests if the default value of scale is set while 
        /// initilizing the <see cref="LibraryViewPresentationModel"/>.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheScaleToSomeFixedValue()
        {
            var presentationModel = this.CreatePresentationModel();
            Assert.IsTrue(presentationModel.Scale != 0);
        }

        /// <summary>
        /// Tests if OnPropertyChanged event is raised when 
        /// Scale property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenScaleIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.Scale = 0.2;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Scale", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if it shows only the top level of assets 
        /// at start.
        /// </summary>
        [TestMethod]
        public void ShouldShowOnlyTheTopLevelAssetsAtStart()
        {
            VideoAsset videoAsset = new VideoAsset()
            {
                Title = "VideoAsset"
            };

            FolderAsset parentFolderAsset = new FolderAsset()
            {
                Title = "ParentFolder"
            };

            FolderAsset childFolderAsset1 = new FolderAsset()
            {
                Title = "Child1Folder",
                ParentFolder = parentFolderAsset
            };

            FolderAsset childFolderAsset2 = new FolderAsset()
            {
                Title = "Child1Folder",
                ParentFolder = parentFolderAsset
            };

            parentFolderAsset.Assets.Add(childFolderAsset1);
            parentFolderAsset.Assets.Add(childFolderAsset2);

            var presentationModel = this.CreatePresentationModel();

            presentationModel.Assets = new List<Asset> { videoAsset, parentFolderAsset };

            Assert.IsTrue(presentationModel.Assets.Count == 2);
        }

        /// <summary>
        /// Tests if <see cref="LibraryViewPresentationModel"/> publish
        /// <see cref="AddAssetEvent"/> when OnAddAsset is called.
        /// </summary>
        [TestMethod]
        public void ShouldPublishEventWhenCallingOnAddAsset()
        {
            var asset = new VideoAsset();
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.addAssetEvent.PublishCalled);
            Assert.IsNull(this.addAssetEvent.Asset);

            presentationModel.OnAddAsset(asset);

            Assert.IsTrue(this.addAssetEvent.PublishCalled);
            Assert.IsNotNull(this.addAssetEvent.Asset);
            Assert.AreEqual(this.addAssetEvent.Asset, asset);
        }

        /// <summary>
        /// Should not publish <see cref="addAssetEvent"/>
        /// when the asset is null.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishAddAssetEventWhenAssetIsNull()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.addAssetEvent.PublishCalled);
            Assert.IsNull(this.addAssetEvent.Asset);

            presentationModel.OnAddAsset(null);

            Assert.IsFalse(this.addAssetEvent.PublishCalled);
            Assert.IsNull(this.addAssetEvent.Asset);
        }

        /// <summary>
        /// Tests if <see cref="LibraryViewPresentationModel"/> publish
        /// <see cref="PlayerEvent"/> when OnAssetSelected is called.
        /// </summary>
        [TestMethod]
        public void ShouldPublishEventWhenCallingOnAssetSelected()
        {
            var asset = new ImageAsset();
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.OnAssetSelected(asset);

            Assert.IsTrue(this.playerEvent.PublishCalled);
            Assert.IsNotNull(this.playerEvent.PublishArgumentPayload);
            Assert.AreSame(asset, this.playerEvent.PublishArgumentPayload.Asset);
        }

        /// <summary>
        /// Should call AddMetadataField method of <see cref="LibraryView"/>
        /// when <see cref="LibraryViewPresentationModel"/> is initilized.
        /// </summary>
        [TestMethod]
        public void ShouldCallAddMetadataFields()
        {
            this.view.AddMetadataFieldsCalled = false;

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(this.view.AddMetadataFieldsCalled);
        }

        /// <summary>
        /// Tests if <see cref="LibraryViewPresentationModel"/> raises
        /// OnPropertyChanged event when IsOpen property is set.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenIsOpenIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.IsHelpWindowOpen = true;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("IsHelpWindowOpen", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should toggle the IsHelpWindowOpen property when HelpButton command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldSetIsHelpWindowOpenToTrueIfItIsFalseWhenHelpButtonCommandIsExecuted()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.IsHelpWindowOpen = false;

            presentationModel.HelpButtonCommand.Execute(null);

            Assert.IsTrue(presentationModel.IsHelpWindowOpen);
        }

        /// <summary>
        /// Should toggle the IsHelpWindowOpen property when HelpButton command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldSetIsHelpWindowOpenToFalseIfItIsTrueWhenHelpButtonCommandIsExecuted()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.IsHelpWindowOpen = true;

            presentationModel.HelpButtonCommand.Execute(null);

            Assert.IsFalse(presentationModel.IsHelpWindowOpen);
        }

        /// <summary>
        /// Should not execute the UpArrowCommand if the current folder 
        /// is the top level folder.
        /// </summary>
        [TestMethod]
        public void ShouldNotExecuteUpArrowCommandIfTheCurrentFolderIsTheRoot()
        {
            var presentationModel = this.CreatePresentationModel();

            var result = presentationModel.UpArrowCommand.CanExecute(string.Empty);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// <see cref="LibraryViewPresentationModel"/> IsActive property 
        /// should return true if the <see cref="LibraryView"/> is active view.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueIfTheViewIsActive()
        {
            var toolsRegion = new MockRegion { Name = "MainRegion" };

            this.regionManager.Regions.Add(toolsRegion);
            var presentationModel = this.CreatePresentationModel();

            var activeViews = new MockViewsCollection { presentationModel.View };
            toolsRegion.ActiveViews = activeViews;

            Assert.IsTrue(presentationModel.IsActive);
        }

        /// <summary>
        /// <see cref="LibraryViewPresentationModel"/> IsActive property 
        /// should return false if the <see cref="LibraryView"/> is not active view.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseIfTheViewIsNotActive()
        {
            var toolsRegion = new MockRegion { Name = "MainRegion" };

            this.regionManager.Regions.Add(toolsRegion);
            var presentationModel = this.CreatePresentationModel();

            var activeViews = new MockViewsCollection();
            toolsRegion.ActiveViews = activeViews;

            Assert.IsFalse(presentationModel.IsActive);
        }

        /// <summary>
        /// Should publish <see cref="PlayerEvent"/> when PlaySelectedAssetCommand is 
        /// executed and the asset is <see cref="VideoAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayerEventWhenExecutingPlaySelectedAssetCommandForVideoAsset()
        {
            var asset = new VideoAsset();
            
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.Assets = new List<Asset> { asset };
            presentationModel.PlaySelectedAssetCommand.Execute(asset.Id);

            Assert.IsTrue(this.playerEvent.PublishCalled);
            Assert.IsNotNull(this.playerEvent.PublishArgumentPayload);
            Assert.AreEqual(this.playerEvent.PublishArgumentPayload.Asset, asset);
        }

        /// <summary>
        /// Should publish <see cref="PlayerEvent"/> when PlaySelectedAssetCommand is 
        /// executed and the asset is <see cref="ImageAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayerEventWhenExecutingPlaySelectedAssetCommandForImageAsset()
        {
            var asset = new ImageAsset();

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.Assets = new List<Asset> { asset };

            presentationModel.PlaySelectedAssetCommand.Execute(asset.Id);

            Assert.IsTrue(this.playerEvent.PublishCalled);
            Assert.IsNotNull(this.playerEvent.PublishArgumentPayload);
            Assert.AreEqual(this.playerEvent.PublishArgumentPayload.Asset, asset);
        }

        /// <summary>
        /// Should publish <see cref="PlayerEvent"/> when PlaySelectedAssetCommand is 
        /// executed and the asset is <see cref="AudioAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayerEventWhenExecutingPlaySelectedAssetCommandForAudioAsset()
        {
            var asset = new AudioAsset();

            var presentationModel = this.CreatePresentationModel();
            
            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.Assets = new List<Asset> { asset };

            presentationModel.PlaySelectedAssetCommand.Execute(asset.Id);

            Assert.IsTrue(this.playerEvent.PublishCalled);
            Assert.IsNotNull(this.playerEvent.PublishArgumentPayload);
            Assert.AreEqual(this.playerEvent.PublishArgumentPayload.Asset, asset);
        }

        /// <summary>
        /// Should not publish <see cref="PlayerEvent"/> when PlaySelectedAssetCommand is 
        /// executed and the asset is <see cref="FolderAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishPlayerEventWhenExecutingPlaySelectedAssetCommandForFolderAsset()
        {
            var asset = new FolderAsset();

            var presentationModel = this.CreatePresentationModel();
            
            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.Assets = new List<Asset> { asset };
            
            presentationModel.PlaySelectedAssetCommand.Execute(asset.Id);

            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Tests if the Activate of Region is called when KeyboardAction is executed.
        /// </summary>
        [TestMethod]
        public void ShouldActivateTheViewIfKeyboardActionCommandIsExecuted()
        {
            this.mainRegion.SelectedItem = null;

            var presentationModel = this.CreatePresentationModel();

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.ActivateModel, default(object)));

            Assert.AreSame(this.view, this.mainRegion.SelectedItem);
        }

        /// <summary>
        /// Tests if the HeaderInfo property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderInfoResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderInfo;

            Assert.AreEqual("Media", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOff property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOffResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderIconOff;

            Assert.AreEqual("/RCE.Modules.Library;component/images/library_icon_off.png", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOn property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOnResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderIconOn;

            Assert.AreEqual("/RCE.Modules.Library;component/images/library_icon_on.png", result);
        }

        /// <summary>
        /// Tests that when executing the UpArrowCommand, assets of parent folder should become accesible through the Assets property.
        /// </summary>
        [TestMethod]
        public void ShouldShowAssetsOfParentFolderWhenUpArrowCommandIsExecuted()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            AudioAsset audioAsset = new AudioAsset { Title = "AudioAsset" };
            FolderAsset folderAsset = new FolderAsset { Title = "ParentFolder" };

            folderAsset.AddAssets(new ObservableCollection<Asset> { videoAsset, audioAsset });

            var presentationModel = this.CreatePresentationModel();

            this.assetsAvailableEvent.SubscribeArgumentAction.Invoke(new Infrastructure.DataEventArgs<List<Asset>>(new List<Asset> { folderAsset }));

            presentationModel.OnAssetSelected(folderAsset);

            Assert.AreEqual(2, presentationModel.Assets.Count);

            presentationModel.UpArrowCommand.Execute(null);

            Assert.AreEqual(1, presentationModel.Assets.Count);
        }

        /// <summary>
        /// Tests that when executing the UpArrowCommand, nothing should change 
        /// if there is no parent folder associated with the current folder.
        /// </summary>
        [TestMethod]
        public void ShouldNotChangeCurrentFolderIfParentFolderIsNullAndUpArrowCommandIsExecuted()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            FolderAsset folderAsset = new FolderAsset { Title = "ParentFolder", };

            folderAsset.AddAssets(new ObservableCollection<Asset> { videoAsset });

            var presentationModel = this.CreatePresentationModel();

            presentationModel.Assets = new List<Asset> { folderAsset };

            Assert.AreEqual(1, presentationModel.Assets.Count);

            presentationModel.UpArrowCommand.Execute(null);

            Assert.AreEqual(1, presentationModel.Assets.Count);
        }

        /// <summary>
        /// Should publish the <see cref="ShowMetadataEvent"/> when ShowMetadata is called
        /// and asset is video/audio/picture.
        /// </summary>
        [TestMethod]
        public void ShouldPublishShowMetadataEventWhenShowMetadataIsCalledAndAssetIsVideoAsset()
        {
            VideoAsset asset = new VideoAsset { Title = "Video" };
            var presentationModel = this.CreatePresentationModel();

            this.showMetadataEvent.PublishCalled = false;
            this.showMetadataEvent.Payload = null;

            var payload = new TimelineElement { Asset = asset };
            presentationModel.ShowMetadata(payload);

            Assert.IsTrue(this.showMetadataEvent.PublishCalled);
            Assert.IsTrue(this.showMetadataEvent.Payload == payload);
        }

        /// <summary>
        /// Should not publish the <see cref="ShowMetadataEvent"/> when ShowMetadata is called
        /// and asset is folder asset.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishShowMetadataEventWhenShowMetadataIsCalledAndAssetIsFolderAsset()
        {
            FolderAsset asset = new FolderAsset();
            var presentationModel = this.CreatePresentationModel();
            this.showMetadataEvent.PublishCalled = false;
            this.showMetadataEvent.Payload = null;

            var payload = new TimelineElement { Asset = asset };
            presentationModel.ShowMetadata(payload);

            Assert.IsFalse(this.showMetadataEvent.PublishCalled);
            Assert.IsNull(this.showMetadataEvent.Payload);
        }

        /// <summary>
        /// Should not publish the <see cref="ShowMetadataEvent"/> when ShowMetadata is called
        /// and asset is title asset.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishShowMetadataEventWhenShowMetadataIsCalledAndAssetIsTitleAssest()
        {
            TitleAsset asset = new TitleAsset();
            var presentationModel = this.CreatePresentationModel();
            this.showMetadataEvent.PublishCalled = false;
            this.showMetadataEvent.Payload = null;

            var payload = new TimelineElement { Asset = asset };
            presentationModel.ShowMetadata(payload);

            Assert.IsFalse(this.showMetadataEvent.PublishCalled);
            Assert.IsNull(this.showMetadataEvent.Payload);
        }

        /// <summary>
        /// Returns the new instance of <see cref="LibraryViewPresentationModel"/>.
        /// </summary>
        /// <returns>The <see cref="LibraryViewPresentationModel"/>.</returns>
        private ILibraryViewPresentationModel CreatePresentationModel()
        {
            return new LibraryViewPresentationModel(this.view, this.eventAggregator, this.regionManager, this.configurationService, () => this.errorView);
        }
    }
}
