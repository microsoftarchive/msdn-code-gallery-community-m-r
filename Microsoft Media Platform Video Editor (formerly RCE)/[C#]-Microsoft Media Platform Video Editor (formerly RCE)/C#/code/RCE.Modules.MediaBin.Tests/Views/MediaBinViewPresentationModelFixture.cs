// <copyright file="MediaBinViewPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MediaBinViewPresentationModelFixture.cs                     
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
    using System.Collections.ObjectModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Modules.MediaBin.Tests.Mocks;
    using Services.Contracts;
    using SMPTETimecode;

    /// <summary>
    /// Test class for <see cref="MediaBinViewPresentationModel"/>.
    /// </summary>
    [TestClass]
    public class MediaBinViewPresentationModelFixture
    {
        /// <summary>
        /// Mock for <see cref="MediaBinView"/>.
        /// </summary>
        private MockMediaBinView view;

        /// <summary>
        /// Mock for <see cref="IDataServiceFacade"/>.
        /// </summary>
        private MockAssetsDataServiceFacade assetsDataServiceFacade;

        /// <summary>
        /// Mock for <see cref="Microsoft.Practices.Composite.Events.IEventAggregator"/>.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="ILogger"/>.
        /// </summary>
        private MockLoggerFacade loggerFacade;

        /// <summary>
        /// Mock for <see cref="AddAssetEvent"/>.
        /// </summary>
        private MockAddAssetEvent addAssetEvent;

        /// <summary>
        /// Mock for <see cref="PlayerEvent"/>.
        /// </summary>
        private MockPlayerEvent playerEvent;

        /// <summary>
        /// Mock for <see cref="SmpteTimeCodeChangedEvent"/>.
        /// </summary>
        private MockSmpteTimecodeChangedEvent smpteTimecodeChangedEvent;

        /// <summary>
        /// Mock for <see cref="DeleteMediaBinAssetEvent"/>.
        /// </summary>
        private MockDeleteMediaBinAssetEvent deleteMediaBinAssetEvent;

        /// <summary>
        /// Mock for <see cref="AddAssetToTimelineEvent"/>.
        /// </summary>
        private MockAddAssetToTimelineEvent addAssetToTimelineEvent;

        /// <summary>
        /// Mock for <see cref="IProjectService"/>.
        /// </summary>
        private MockProjectService projectService;

        /// <summary>
        /// Mock for <see cref="IConfigurationService"/>.
        /// </summary>
        private MockConfigurationService configurationService;

        /// <summary>
        /// Mock for <see cref="IRegionManager"/>.
        /// </summary>
        private MockRegionManager regionManager;

        /// <summary>
        /// Mock for <see cref="IRegion"/>.
        /// </summary>
        private MockRegion mainRegion;

        /// <summary>
        /// Mock for <see cref="ShowMetadataEvent"/>.
        /// </summary>
        private MockShowMetadataEvent showMetadataEvent;

        private MockResetWindowsEvent resetWindowsEvent;

        /// <summary>
        /// Initilize the default values.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockMediaBinView();
            this.assetsDataServiceFacade = new MockAssetsDataServiceFacade();
            this.addAssetEvent = new MockAddAssetEvent();
            this.playerEvent = new MockPlayerEvent();
            this.addAssetToTimelineEvent = new MockAddAssetToTimelineEvent();
            this.smpteTimecodeChangedEvent = new MockSmpteTimecodeChangedEvent();
            this.deleteMediaBinAssetEvent = new MockDeleteMediaBinAssetEvent();
            this.resetWindowsEvent = new MockResetWindowsEvent();
            this.eventAggregator = new MockEventAggregator();
            this.showMetadataEvent = new MockShowMetadataEvent();
            this.eventAggregator.AddMapping<AddAssetEvent>(this.addAssetEvent);
            this.eventAggregator.AddMapping<PlayerEvent>(this.playerEvent);
            this.eventAggregator.AddMapping<SmpteTimeCodeChangedEvent>(this.smpteTimecodeChangedEvent);
            this.eventAggregator.AddMapping<DeleteMediaBinAssetEvent>(this.deleteMediaBinAssetEvent);
            this.eventAggregator.AddMapping<AddAssetToTimelineEvent>(this.addAssetToTimelineEvent);
            this.eventAggregator.AddMapping<ShowMetadataEvent>(this.showMetadataEvent);
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowsEvent);
            this.loggerFacade = new MockLoggerFacade();
            this.projectService = new MockProjectService();
            this.configurationService = new MockConfigurationService();
            this.regionManager = new MockRegionManager();
            this.mainRegion = new MockRegion();

            this.mainRegion.Name = RegionNames.MainRegion;
            this.regionManager.Regions.Add(this.mainRegion);
        }

        /// <summary>
        /// Tests if the presentation model is initilized.
        /// </summary>
        [TestMethod]
        public void CanInitPresentationModel()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(this.view, presentationModel.View);
        }

        /// <summary>
        /// Tests if presentation model is setting the view's Model property.
        /// </summary>
        [TestMethod]
        public void ShouldSetPresentationModelIntoView()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(presentationModel, this.view.Model);
        }

        /// <summary>
        /// Tests if presentation model call the progress bar to show the progress while initilization.
        /// </summary>
        [TestMethod]
        public void ShouldCallToShowProgressBar()
        {
            Assert.IsFalse(this.view.ShowProgressBarCalled);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(this.view.ShowProgressBarCalled);
        }

        /// <summary>
        /// Tests if presentation model get the metadata fields from <see cref="IConfigurationService"/>
        /// while initilization.
        /// </summary>
        [TestMethod]
        public void ShouldCallToGetMetadataFields()
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
        /// The <see cref="MediaBinViewPresentationModel"/> should load even if 
        /// the media bin uri is null.
        /// </summary>
        [TestMethod]
        public void ShouldLoadTheMediaBinWithZeroAssetIfTheMediaBinUriIsEmpty()
        {
            this.projectService.GetCurrentProject().MediaBin.ProviderUri = null;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(0, presentationModel.Assets.Count);
        }

        /// <summary>
        /// Should get the MediaBin from the current project.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheMediaBinFromTheCurrentProject()
        {
            Asset asset = new ImageAsset();

            Assert.IsFalse(this.projectService.GetCurrentProjectCalled);
            Assert.AreEqual(0, this.projectService.GetCurrentProject().MediaBin.Assets.Count);

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(asset);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(this.projectService.GetCurrentProjectCalled);
            Assert.AreEqual(1, presentationModel.Assets.Count);
            Assert.AreSame(asset, presentationModel.Assets[0]);
        }

        /// <summary>
        /// Should populate the show properties to true by default.
        /// </summary>
        [TestMethod]
        public void ShoulSetShowPropertyToTrue()
        {
            var presentationModel = this.CreatePresentationModel();

            // Check if the default value is true.
            Assert.IsTrue(presentationModel.ShowAudio);
            Assert.IsTrue(presentationModel.ShowVideos);
            Assert.IsTrue(presentationModel.ShowImages);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when Assets property is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenMediaBinAssetsIsUpdated()
        {
            bool propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.Assets = new ObservableCollection<Asset>();

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Assets", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should show only images if ShowVideo/ShowAudio is false and ShowImages is true.
        /// </summary>
        [TestMethod]
        public void ShouldShowOnlyImagesAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            // var assets = new ObservableCollection<Asset> { imageAsset, videoAsset, audioAsset };
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(imageAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(audioAsset);

            // this.assetsDataServiceFacade.Assets = assets;
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.ShowVideos = false;
            presentationModel.ShowAudio = false;

            Assert.AreEqual(1, presentationModel.Assets.Count);
            Assert.AreSame(imageAsset, presentationModel.Assets[0]);
        }

        /// <summary>
        /// Should show only images if ShowAudios/ShowImages is false and ShowVideos is true.
        /// </summary>
        [TestMethod]
        public void ShouldShowOnlyVideosAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            // var assets = new ObservableCollection<Asset> { imageAsset, videoAsset, audioAsset };
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(imageAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(audioAsset);

            // this.assetsDataServiceFacade.Assets = assets;
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.ShowImages = false;
            presentationModel.ShowAudio = false;

            Assert.AreEqual(1, presentationModel.Assets.Count);
            Assert.AreSame(videoAsset, presentationModel.Assets[0]);
        }

        /// <summary>
        /// Should show only images if ShowVideo/ShowImages is false and ShowImages is true.
        /// </summary>
        [TestMethod]
        public void ShouldShowOnlyAudioAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            // var assets = new ObservableCollection<Asset> { imageAsset, videoAsset, audioAsset };
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(imageAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(audioAsset);
            
            // this.assetsDataServiceFacade.Assets = assets;
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.ShowImages = false;
            presentationModel.ShowVideos = false;

            Assert.AreEqual(1, presentationModel.Assets.Count);
            Assert.AreSame(audioAsset, presentationModel.Assets[0]);
        }

        /// <summary>
        /// Should not show any asset if all the show properties is false.
        /// </summary>
        [TestMethod]
        public void ShouldShowNoAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            // var assets = new ObservableCollection<Asset> { imageAsset, videoAsset, audioAsset };
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(imageAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(audioAsset);
            
            // this.assetsDataServiceFacade.Assets = assets;
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.ShowImages = false;
            presentationModel.ShowVideos = false;
            presentationModel.ShowAudio = false;

            Assert.AreEqual(0, presentationModel.Assets.Count);
        }

        /// <summary>
        /// Should show all the assets if all the show properties is true.
        /// </summary>
        [TestMethod]
        public void ShouldShowAllAssets()
        {
            var imageAsset = new ImageAsset { Title = "Image" };
            var videoAsset = new VideoAsset { Title = "Video" };
            var audioAsset = new AudioAsset { Title = "Audio" };

            // var assets = new ObservableCollection<Asset> { imageAsset, videoAsset, audioAsset };
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(imageAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(audioAsset);

            // this.assetsDataServiceFacade.Assets = assets;
            var presentationModel = this.CreatePresentationModel();
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
        /// Should raise OnPropertyChanged event when show videos is updated.
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
        /// Should raise OnPropertyChanged event when show audios is updated.
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
        /// Should raise OnPropertyChanged event when show images is updated.
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
        /// Should filter assets when executing search command.
        /// </summary>
        [TestMethod]
        public void ShouldFilterAssetsWhenExecutingSearchCommand()
        {
            Asset imageAsset = new ImageAsset { Title = "Test" };
            Asset videoAsset = new VideoAsset() { Title = "Video" };
            
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(imageAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoAsset);
           
            // this.assetsDataServiceFacade.Assets.Add(imageAsset);
            // this.assetsDataServiceFacade.Assets.Add(videoAsset);
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(2, presentationModel.Assets.Count);
            
            presentationModel.SearchCommand.Execute("Test");

            Assert.AreEqual(1, presentationModel.Assets.Count);
        }

        /// <summary>
        /// Shoulds filter the assets when executing search command within the current folder.
        /// </summary>
        [TestMethod]
        public void ShouldFilterAssetsWhenExecutingSearchCommandWithinTheCurrentFolder()
        {
            Asset imageAsset = new ImageAsset { Title = "Test", };
            Asset videoAsset = new VideoAsset { Title = "Video" };
            Asset audioAsset = new AudioAsset { Title = "Test" };
            FolderAsset folderAsset = new FolderAsset { Title = "Folder" };

            folderAsset.Assets.Add(audioAsset);

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(imageAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);
            
            // this.assetsDataServiceFacade.Assets.Add(imageAsset);
            // this.assetsDataServiceFacade.Assets.Add(videoAsset);
            // this.assetsDataServiceFacade.Assets.Add(folderAsset);
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(3, presentationModel.Assets.Count);

            presentationModel.SearchCommand.Execute("Test");

            Assert.AreEqual(1, presentationModel.Assets.Count);
        }

        /// <summary>
        /// <see cref="MediaBinViewPresentationModel"/> Should subscribe to add asset event.
        /// </summary>
        [TestMethod]
        public void ShouldSubscribeToAddAssetEvent()
        {
            Assert.IsNull(this.addAssetEvent.SubscribeArgumentFilter);
            Assert.IsNull(this.addAssetEvent.SubscribeArgumentAction);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsNotNull(this.addAssetEvent.SubscribeArgumentAction);
            Assert.IsNotNull(this.addAssetEvent.SubscribeArgumentFilter);
            Assert.IsNotNull(this.addAssetEvent.SubscribeArgumentThreadOption);
        }

        /// <summary>
        /// Should add Video asset to collection when Video asset does not exist in the collection.
        /// </summary>
        [TestMethod]
        public void ShouldAddVideoAssetToCollectionWhenVideoAssetDoesNotExistInTheCollection()
        {
            var videoAsset = new VideoAsset { Title = "Video" };
            Assert.IsNull(this.addAssetEvent.SubscribeArgumentAction);

            var presentationModel = this.CreatePresentationModel();
            Assert.IsNotNull(this.addAssetEvent.SubscribeArgumentAction);

            Assert.IsTrue(presentationModel.Assets.Count == 0);
            this.addAssetEvent.SubscribeArgumentAction(videoAsset);

            Assert.IsTrue(presentationModel.Assets.Count == 1);
            Assert.AreSame(presentationModel.Assets[0], videoAsset);
        }

        /// <summary>
        /// Should add audio asset to collection when audio asset does not exist in the collection.
        /// </summary>
        [TestMethod]
        public void ShouldAddAudioAssetToCollectionWhenAudioAssetDoesNotExistInTheCollection()
        {
            var audioAsset = new AudioAsset() { Title = "Audio" };
            Assert.IsNull(this.addAssetEvent.SubscribeArgumentAction);

            var presentationModel = this.CreatePresentationModel();
            Assert.IsNotNull(this.addAssetEvent.SubscribeArgumentAction);

            Assert.IsTrue(presentationModel.Assets.Count == 0);
            this.addAssetEvent.SubscribeArgumentAction(audioAsset);

            Assert.IsTrue(presentationModel.Assets.Count == 1);
            Assert.AreSame(presentationModel.Assets[0], audioAsset);
        }

        /// <summary>
        /// Should add Image asset to collection when Image asset does not exist in the collection.
        /// </summary>
        [TestMethod]
        public void ShouldAddImageAssetToCollectionWhenImageAssetDoesNotExistInTheCollection()
        {
            var imageAsset = new ImageAsset() { Title = "Image" };
            Assert.IsNull(this.addAssetEvent.SubscribeArgumentAction);

            var presentationModel = this.CreatePresentationModel();
            Assert.IsNotNull(this.addAssetEvent.SubscribeArgumentAction);

            Assert.IsTrue(presentationModel.Assets.Count == 0);
            this.addAssetEvent.SubscribeArgumentAction(imageAsset);

            Assert.IsTrue(presentationModel.Assets.Count == 1);
            Assert.AreSame(presentationModel.Assets[0], imageAsset);
        }

        /// <summary>
        /// Should not pass filter if asset already exists.
        /// </summary>
        [TestMethod]
        public void ShouldNotPassFilterIfAssetAlreadyExists()
        {
            var videoAsset = new VideoAsset { ProviderUri = new Uri("http://test"), Title = "Video" };

            var videoInOutAsset = new VideoAssetInOut(videoAsset);

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoInOutAsset);

            var presentationModel = this.CreatePresentationModel();

            var result = this.addAssetEvent.SubscribeArgumentFilter(videoInOutAsset);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Should not pass filter if asset already exists in any of the inner folder.
        /// </summary>
        [TestMethod]
        public void ShouldNotPassFilterIfAssetAlreadyExistsInAInnerFolder()
        {
            var folderAsset = new FolderAsset();
            var innerFolderAsset = new FolderAsset();
            
            var videoAsset = new VideoAsset { ProviderUri = new Uri("http://test"), Title = "Video" };

            var videoInOutAsset = new VideoAssetInOut(videoAsset);

            innerFolderAsset.Assets.Add(videoInOutAsset);
            folderAsset.Assets.Add(innerFolderAsset);

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);

            var presentationModel = this.CreatePresentationModel();

            var result = this.addAssetEvent.SubscribeArgumentFilter(videoInOutAsset);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Should pass filter if asset doesn't exists in the currentAssets collection.
        /// </summary>
        [TestMethod]
        public void ShouldPassFilterIfAssetNotExists()
        {
            var videoAsset = new VideoAsset { Title = "Video" };
            var presentationModel = this.CreatePresentationModel();

            var result = this.addAssetEvent.SubscribeArgumentFilter(videoAsset);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Should not add asset to Assets if show video option is not set.
        /// </summary>
        [TestMethod]
        public void ShouldNotAddVideoAssetToAssetsIfShowVideoOptionIsNotSet()
        {
            var videoAsset = new VideoAsset { Title = "Video" };
            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(presentationModel.Assets.Count == 0);
            presentationModel.ShowVideos = false;
            this.addAssetEvent.SubscribeArgumentAction(videoAsset);

            Assert.IsTrue(presentationModel.Assets.Count == 0);
        }

        /// <summary>
        /// Should not add asset to Assets if show Audios option is not set.
        /// </summary>
        [TestMethod]
        public void ShouldNotAddAudioAssetToAssetIfShowAudioOptionIsNotSet()
        {
            var audioAsset = new AudioAsset { Title = "Audio" };

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(presentationModel.Assets.Count == 0);
            presentationModel.ShowAudio = false;
            this.addAssetEvent.SubscribeArgumentAction(audioAsset);

            Assert.IsTrue(presentationModel.Assets.Count == 0);
        }

        /// <summary>
        /// Should not add asset to Assets if show images option is not set.
        /// </summary>
        [TestMethod]
        public void ShouldNotAddImageAssetToAssetIfShowImagesOptionIsNotSet()
        {
            var imageAsset = new ImageAsset { Title = "Image" };

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(presentationModel.Assets.Count == 0);
            presentationModel.ShowImages = false;
            this.addAssetEvent.SubscribeArgumentAction(imageAsset);

            Assert.IsTrue(presentationModel.Assets.Count == 0);
        }

        /// <summary>
        /// Should set the default value of the scale while initilizing the <see cref="MediaBinViewPresentationModel"/>.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheScaleToSomeFixedValue()
        {
            var presentationModel = this.CreatePresentationModel();
            Assert.IsTrue(presentationModel.Scale != 0);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when Scale property is updated.
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
        /// Should increase the scale value ShiftSliderScaleCommand(Zoom In/Out) is executed.
        /// </summary>
        [TestMethod]
        public void ShouldIncreaseScaleValueWhenShiftSliderScaleCommandExecute()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.Scale = 0.5;

            Assert.AreEqual(0.5, presentationModel.Scale);

            presentationModel.ShiftSliderScaleCommand.Execute("+");

            Assert.IsTrue(presentationModel.Scale > 0.5);
        }

        /// <summary>
        /// Should increase the scale value ShiftSliderScaleCommand(Zoom In/Out) is executed.
        /// </summary>
        [TestMethod]
        public void ShouldDecreseScaleValueWhenShiftSliderScaleCommandExecute()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.Scale = 0.5;

            Assert.AreEqual(0.5, presentationModel.Scale);
            
            presentationModel.ShiftSliderScaleCommand.Execute("-");

            Assert.IsTrue(presentationModel.Scale < 0.5);
        }

        /// <summary>
        /// Shoulds not change scale value when user zoom in the slider and scale value is
        /// set to it's maximum(1) value.
        /// </summary>
        [TestMethod]
        public void ShouldNotChangeScaleValueWhenShiftSliderScaleCommandExecuteAndScaleValueIs1()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.Scale = 1;
            Assert.IsTrue(presentationModel.Scale == 1);
            presentationModel.ShiftSliderScaleCommand.Execute("+");

            Assert.IsTrue(presentationModel.Scale == 1);
        }

        /// <summary>
        /// Shoulds not change scale value when user zoom out the slider and scale value is
        /// set to it's minimum(0) value.
        /// </summary>
        [TestMethod]
        public void ShouldNotChangeScaleValueWhenShiftSliderScaleCommandExecuteAndScaleValueIs0()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.Scale = 0;
            Assert.IsTrue(presentationModel.Scale == 0);
            presentationModel.ShiftSliderScaleCommand.Execute("-");

            Assert.IsTrue(presentationModel.Scale == 0);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when Folder title is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenFolderTitleIsUpdated()
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

            presentationModel.FolderTitle = "Test";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("FolderTitle", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should set the default value of folder title while initilizing the <see cref="MediaBinViewPresentationModel"/>.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheDefaultValueOfFolderTitle()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(string.IsNullOrEmpty(presentationModel.FolderTitle));
        }

        /// <summary>
        /// Should add folder to mediabin when AddFolder command executed.
        /// </summary>
        [TestMethod]
        public void ShouldAddFolderOnAddFolderCommandExecute()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(presentationModel.Assets.Count == 0);

            presentationModel.AddFolderCommand.Execute("Folder");

            Assert.IsTrue(presentationModel.Assets.Count == 1);
            Assert.IsTrue(presentationModel.Assets[0].Title == "Folder");
        }

        /// <summary>
        /// Should add folder to mediabin when AddFolder command executed.
        /// </summary>
        [TestMethod]
        public void ShouldAddFolderInCurrentFolderAssetsIfItIsNotNullOnAddFolderCommandExecute()
        {
            FolderAsset folderAsset = new FolderAsset() { Title = "Folder", };
            this.assetsDataServiceFacade.Assets.Add(folderAsset);
            var presentationModel = this.CreatePresentationModel();
            presentationModel.OnAssetSelected(folderAsset);

            Assert.IsTrue(folderAsset.Assets.Count == 0);

            presentationModel.AddFolderCommand.Execute("ChildFolder");

            Assert.IsTrue(folderAsset.Assets.Count == 1);
            Assert.IsTrue(folderAsset.Assets[0].Title == "ChildFolder");
        }

        /// <summary>
        /// Should not add folder in mediabin if when AddFolder command executed 
        /// and a folder with the same name already exists in the mediabin.
        /// </summary>
        [TestMethod]
        public void ShouldNotAddFolderOnAddFolderCommandExecuteIfFolderWithTheSameNameExists()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(presentationModel.Assets.Count == 0);

            presentationModel.AddFolderCommand.Execute("Folder");

            Assert.IsTrue(presentationModel.Assets.Count == 1);
            Assert.IsTrue(presentationModel.Assets[0].Title == "Folder");

            presentationModel.AddFolderCommand.Execute("Folder");

            Assert.IsTrue(presentationModel.Assets.Count == 1);
        }

        /// <summary>
        /// Should trim the folder name before adding the folder.
        /// </summary>
        [TestMethod]
        public void ShouldTrimTheFolderWhileAddingTheFolderInTheCurrentFolder()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(presentationModel.Assets.Count == 0);

            presentationModel.AddFolderCommand.Execute("Folder          ");

            Assert.IsTrue(presentationModel.Assets.Count == 1);
            Assert.IsTrue(presentationModel.Assets[0].Title == "Folder");
        }

        /// <summary>
        /// Should show the top level assets at start. 
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

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(videoAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(parentFolderAsset);

            var presentationModel = this.CreatePresentationModel();

            // only two folders have parentfolderid as null.
            Assert.IsTrue(presentationModel.Assets.Count == 2);
        }

        /// <summary>
        /// Should not publish the <see cref="DeleteMediaBinAssetEvent"/> when 
        /// <see cref="FolderAsset"/> is deleted from the mediabin.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishDeleteMediaBinAssetEventIfAssetIsFolderAsset()
        {
            FolderAsset parentFolderAsset = new FolderAsset()
                                                {
                                                    Title = "ParentFolder"
                                                };

            this.assetsDataServiceFacade.Assets.Add(parentFolderAsset);
            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = parentFolderAsset;
            Assert.IsFalse(this.deleteMediaBinAssetEvent.PublishCalled);

            presentationModel.DeleteCurrentAsset();

            Assert.IsFalse(this.deleteMediaBinAssetEvent.PublishCalled);
        }

        /// <summary>
        /// Should delete the asset from the mediabin assets whien user deletes the asset.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveAssetFromCurrentAssetsWhenAssetIsDeleted()
        {
            FolderAsset parentFolderAsset = new FolderAsset
                                                {
                                                    Title = "ParentFolder"
                                                };

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(parentFolderAsset);

            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = parentFolderAsset;
            Assert.IsTrue(presentationModel.Assets.Count == 1);

            presentationModel.DeleteCurrentAsset();

            Assert.IsTrue(presentationModel.Assets.Count == 0);
        }

        /// <summary>
        /// Should publish <see cref="DeleteMediaBinAssetEvent"/> event if asset is video asset
        /// and user delted it.
        /// </summary>
        [TestMethod]
        public void ShouldPublishDeleteMediaBinAssetEventIfAssetIsVideoAsset()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };

            this.assetsDataServiceFacade.Assets.Add(videoAsset);

            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = videoAsset;
            Assert.IsFalse(this.deleteMediaBinAssetEvent.PublishCalled);

            presentationModel.DeleteCurrentAsset();

            Assert.IsTrue(this.deleteMediaBinAssetEvent.PublishCalled);
        }

        /// <summary>
        /// Shoud return empty array if there is no asset in the mediabin.
        /// </summary>
        [TestMethod]
        public void ShouldReturnEmptyArrayIfThereAreNoAssets()
        {
            var presentationModel = this.CreatePresentationModel();

           var result = presentationModel.GetMediaBin();

            Assert.AreEqual(0, result.Length);
        }

        /// <summary>
        /// Should return the empty array if there are assets with provider URI as null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnEmptyArrayIfThereAreAssetsWithProviderUriNull()
        {
            FolderAsset parentFolderAsset = new FolderAsset
            {
                Title = "ParentFolder"
            };

            this.assetsDataServiceFacade.Assets.Add(parentFolderAsset);

            var presentationModel = this.CreatePresentationModel();

            var result = presentationModel.GetMediaBin();

            Assert.AreEqual(0, result.Length);
        }

        /// <summary>
        /// Shoulds return the array of ids if there are assets with provider URI not null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnIdsArrayIfThereAreAssetsWithProviderUriNotNull()
        {
            FolderAsset parentFolderAsset = new FolderAsset
            {
                Title = "ParentFolder",
                ProviderUri = new Uri("http://test")
            };

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(parentFolderAsset);

            var presentationModel = this.CreatePresentationModel();

            var result = presentationModel.GetMediaBin();

            Assert.AreEqual(1, result.Length);
        }

        /// <summary>
        /// Should publish <see cref="AddAssetToTimelineEvent"/> event when 
        /// AddAssetToTimeline method is called with the given <see cref="Asset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldPublishAddAssetToTimelineEventIfAssetIsNotNull()
        {
            this.addAssetToTimelineEvent.Asset = null;
            this.addAssetToTimelineEvent.PublishCalled = false;
            var imageAsset = new ImageAsset { Title = "Image" };

            var presentationModel = this.CreatePresentationModel();
            presentationModel.AddAssetToTimeline(imageAsset);

            Assert.IsTrue(this.addAssetToTimelineEvent.PublishCalled);
            Assert.AreSame(imageAsset, this.addAssetToTimelineEvent.Asset);
        }

        /// <summary>
        /// Should not publish <see cref="AddAssetToTimelineEvent"/> event when 
        /// AddAssetToTimeline method is called and Asset is null.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishAddAssetToTimelineEventIfAssetNull()
        {
            this.addAssetToTimelineEvent.Asset = null;
            this.addAssetToTimelineEvent.PublishCalled = false;
            var imageAsset = new ImageAsset { Title = "Image" };

            var presentationModel = this.CreatePresentationModel();
            presentationModel.AddAssetToTimeline(null);

            Assert.IsFalse(this.addAssetToTimelineEvent.PublishCalled);
            Assert.IsTrue(this.addAssetToTimelineEvent.Asset == null);
        }

        /// <summary>
        /// Should not publish <see cref="AddAssetToTimelineEvent"/> event for 
        /// <see cref="FolderAsset"/> when AddAssetToTimeline method is called.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishAddAssetToTimelineEventIfAssetIsFolderAsset()
        {
            this.addAssetToTimelineEvent.Asset = null;
            this.addAssetToTimelineEvent.PublishCalled = false;
            FolderAsset folderAsset = new FolderAsset
            {
                Title = "Folder",
                ProviderUri = new Uri("http://test")
            };

            var presentationModel = this.CreatePresentationModel();
            presentationModel.AddAssetToTimeline(folderAsset);

            Assert.IsFalse(this.addAssetToTimelineEvent.PublishCalled);
            Assert.IsTrue(this.addAssetToTimelineEvent.Asset == null);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when IsOpen property is updated.
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
        /// Should set the IsHelpWindowOpen property to true if it is false 
        /// when HelpButtonCommand command is executed.
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
        /// Should set the IsHelpWindowOpen property to false if it is true 
        /// when HelpButtonCommand command is executed.
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
        /// Should not execute UpArrowCommand if the current folder is the top level folder.
        /// </summary>
        [TestMethod]
        public void ShouldNotExecuteUpArrowCommandIfTheCurrentFolderIsTheRoot()
        {
            var presentationModel = this.CreatePresentationModel();

            var result = presentationModel.UpArrowCommand.CanExecute(string.Empty);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Should return true if the view is active.
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
        /// Should return false if the view is not active.
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
        /// Should publish <see cref="PlayerEvent"/> event when executing 
        /// PlaySelectedAsset command for <see cref="VideoAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayerEventWhenExecutingPlaySelectedAssetCommandForVideoAsset()
        {
            var asset = new VideoAsset();

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(asset);

            var presentationModel = this.CreatePresentationModel();
            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.PlaySelectedAssetCommand.Execute(asset.Id);

            Assert.IsTrue(this.playerEvent.PublishCalled);
            Assert.IsNotNull(this.playerEvent.PublishArgumentPayload);
            Assert.AreEqual(this.playerEvent.PublishArgumentPayload.Asset, asset);
        }

        /// <summary>
        /// Should publish <see cref="PlayerEvent"/> event when executing 
        /// PlaySelectedAsset command for <see cref="ImageAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayerEventWhenExecutingPlaySelectedAssetCommandForImageAsset()
        {
            var asset = new ImageAsset();

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(asset);

            var presentationModel = this.CreatePresentationModel();
            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.PlaySelectedAssetCommand.Execute(asset.Id);

            Assert.IsTrue(this.playerEvent.PublishCalled);
            Assert.IsNotNull(this.playerEvent.PublishArgumentPayload);
            Assert.AreEqual(this.playerEvent.PublishArgumentPayload.Asset, asset);
        }

        /// <summary>
        /// Should publish <see cref="PlayerEvent"/> event when executing 
        /// PlaySelectedAsset command for <see cref="AudioAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayerEventWhenExecutingPlaySelectedAssetCommandForAudioAsset()
        {
            var asset = new AudioAsset();

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(asset);

            var presentationModel = this.CreatePresentationModel();
            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.PlaySelectedAssetCommand.Execute(asset.Id);

            Assert.IsTrue(this.playerEvent.PublishCalled);
            Assert.IsNotNull(this.playerEvent.PublishArgumentPayload);
            Assert.AreEqual(this.playerEvent.PublishArgumentPayload.Asset, asset);
        }

        /// <summary>
        /// Should not publish <see cref="PlayerEvent"/> event when executing 
        /// PlaySelectedAsset command for <see cref="FolderAsset"/>.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishPlayerEventWhenExecutingPlaySelectedAssetCommandForFolderAsset()
        {
            var asset = new FolderAsset();

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(asset);

            var presentationModel = this.CreatePresentationModel();
            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);

            presentationModel.PlaySelectedAssetCommand.Execute(asset.Id);

            Assert.IsFalse(this.playerEvent.PublishCalled);
            Assert.IsNull(this.playerEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when SelectedAsset property is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenSelectedAssetIsUpdated()
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

            presentationModel.SelectedAsset = new VideoAsset { Title = "VideoAsset" };

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("SelectedAsset", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when IsThumb property is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenIsThumbCheckedIsUpdated()
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

            presentationModel.IsThumbChecked = !presentationModel.IsThumbChecked;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("IsThumbChecked", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// CanDeleteAsset should return false if SelectedAsset is null.
        /// </summary>
        [TestMethod]
        public void CanDeleteAssetShouldReturnFalseIfSelectedAssetIsNull()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = null;

            Assert.IsFalse(presentationModel.DeleteAssetCommand.CanExecute(null));
        }

        /// <summary>
        /// CanDeleteAsset should return false for <see cref="FolderAsset"/>
        /// if it is not empty.
        /// </summary>
        [TestMethod]
        public void CanDeleteAssetShouldReturnFalseIfFolderAssetIsNotEmpty()
        {
            FolderAsset parentFolderAsset = new FolderAsset()
            {
                Title = "ParentFolder"
            };

            FolderAsset childFolderAsset = new FolderAsset()
            {
                Title = "Child1Folder",
                ParentFolder = parentFolderAsset
            };

            parentFolderAsset.Assets.Add(childFolderAsset);

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(childFolderAsset);
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(parentFolderAsset);

            // this.assetsDataServiceFacade.Assets.Add(childFolderAsset);
            // this.assetsDataServiceFacade.Assets.Add(parentFolderAsset);
            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = parentFolderAsset;

            // only two folders have parentfolderid as null.
            Assert.IsFalse(presentationModel.DeleteAssetCommand.CanExecute(null));
        }

        /// <summary>
        /// CanDeleteAsset should return true if SelectedAsset is not null.
        /// </summary>
        [TestMethod]
        public void CanDeleteAssetShouldReturnTrueIfSelectedAssetIsNotNull()
        {
            FolderAsset folderAsset = new FolderAsset()
            {
                Title = "Folder"
            };

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);

            // this.assetsDataServiceFacade.Assets.Add(folderAsset);
            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = folderAsset;

            // only two folders have parentfolderid as null.
            Assert.IsTrue(presentationModel.DeleteAssetCommand.CanExecute(null));
        }

        /// <summary>
        /// CanDeleteAsset should return false if SelectedAsset is not in the collection.
        /// </summary>
        [TestMethod]
        public void CanDeleteAssetShouldReturnFalseIfAssetIsNotInTheCollection()
        {
            FolderAsset folderAsset = new FolderAsset()
            {
                Title = "Folder"
            };

            AudioAsset audioAsset = new AudioAsset { Title = "Audio" };

            this.assetsDataServiceFacade.Assets.Add(folderAsset);

            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = audioAsset;

            var result = presentationModel.DeleteAssetCommand.CanExecute(string.Empty);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Should call GetDeleteAssetConfirmation method of view
        /// If CanDeleteAsset return true while executing DeleteAsset command.
        /// </summary>
        [TestMethod]
        public void ShouldCallGetDeleteAssetConfirmationMethodIfCanDeleteAssetReturnTrueWhenDeleteAssetCommandExecutes()
        {
            FolderAsset folderAsset = new FolderAsset
            {
                Title = "Folder"
            };

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);
            
            // this.assetsDataServiceFacade.Assets.Add(folderAsset);
            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = folderAsset;

            // only two folders have parentfolderid as null.
            presentationModel.DeleteAssetCommand.Execute(null);

            Assert.IsTrue(this.view.GetDeleteAssetConfirmationCalled);
        }

        /// <summary>
        /// Tests if the Activate of Region is called when Activate keyboard action is called.
        /// </summary>
        [TestMethod]
        public void ShouldActivateTheViewIfKeyboardActionCommandIsExecutedWithActivateAction()
        {
            this.mainRegion.SelectedItem = null;
            var presentationModel = this.CreatePresentationModel();

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.ActivateModel, default(object)));

            Assert.AreSame(this.view, this.mainRegion.SelectedItem);
        }

        [TestMethod]
        public void ShouldCallGetDeleteAssetConfirmationMethodIfCanDeleteAssetReturnTrueWhenKeyboardActionCommandExecutes()
        {
            FolderAsset folderAsset = new FolderAsset
            {
                Title = "Folder"
            };

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);

            // this.assetsDataServiceFacade.Assets.Add(folderAsset);
            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedAsset = folderAsset;

            // only two folders have parentfolderid as null.
            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.DeleteAsset, default(object)));

            Assert.IsTrue(this.view.GetDeleteAssetConfirmationCalled);
        }

        /// <summary>
        /// Should show the assets of parent folder when up arrow command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldShowAssetsOfParentFolderWhenUpArrowCommandIsExecuted()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            AudioAsset audioAsset = new AudioAsset { Title = "AudioAsset" };
            FolderAsset folderAsset = new FolderAsset { Title = "ParentFolder" };

            folderAsset.AddAssets(new ObservableCollection<Asset> { videoAsset, audioAsset });

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);

            var presentationModel = this.CreatePresentationModel();

            presentationModel.OnAssetSelected(folderAsset);

            Assert.AreEqual(2, presentationModel.Assets.Count);

            presentationModel.UpArrowCommand.Execute(null);

            Assert.AreEqual(1, presentationModel.Assets.Count);
        }

        /// <summary>
        /// Should not change the current folder if the parent folder of current folder
        /// is null.
        /// </summary>
        [TestMethod]
        public void ShouldNotChangeCurrentFolderIfParentFolderIsNullAndUpArrowCommandIsExecuted()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            FolderAsset folderAsset = new FolderAsset { Title = "ParentFolder", };

            folderAsset.AddAssets(new ObservableCollection<Asset> { videoAsset });

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);

            var presentationModel = this.CreatePresentationModel();

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
        /// Should add the given asset to the folder which have the given id.
        /// </summary>
        [TestMethod]
        public void ShouldAddAssetToCurrentFolderAndRemoveItfromItsParentFolderWhenKeyboardActionCommandIsExecuted()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            FolderAsset childFolderAsset = new FolderAsset { Title = "Child1Folder" };
            FolderAsset parentFolderAsset = new FolderAsset { Title = "ParentFolder", };

            childFolderAsset.ParentFolder = parentFolderAsset;
            
            parentFolderAsset.AddAssets(new ObservableCollection<Asset> { videoAsset, childFolderAsset });
            
            this.projectService.GetCurrentProject().MediaBin.Assets.Add(parentFolderAsset);

            // this.assetsDataServiceFacade.Assets.Add(parentFolderAsset);
            var presentationModel = this.CreatePresentationModel();

            presentationModel.SelectedAsset = videoAsset;

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.CutAsset, default(object)));

            presentationModel.SelectedAsset = childFolderAsset;

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.PasteAsset, default(object)));

            Assert.AreEqual(1, parentFolderAsset.Assets.Count);
            Assert.AreEqual(1, childFolderAsset.Assets.Count);
        }

        /// <summary>
        /// Should add the given asset at the top level if the current folder is null
        /// and remove it from it's parent folder when KeyboardActionCommand is called.
        /// </summary>
        [TestMethod]
        public void ShouldAddAudioAssetToCurrentFolderWhenItIsNotNullAndRemoveItfromItsParentFolderWhenKeyboardActionCommandIsExecuted()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            AudioAsset audioAsset = new AudioAsset { Title = "Audio" };

            FolderAsset childFolderAsset = new FolderAsset { Title = "Child1Folder" };
            childFolderAsset.AddAssets(new ObservableCollection<Asset> { audioAsset });
            FolderAsset parentFolderAsset = new FolderAsset() { Title = "ParentFolder" };

            childFolderAsset.ParentFolder = parentFolderAsset;
            
            parentFolderAsset.AddAssets(new ObservableCollection<Asset> { videoAsset, childFolderAsset });

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(parentFolderAsset);
            
            // this.assetsDataServiceFacade.Assets.Add(parentFolderAsset);
            var presentationModel = this.CreatePresentationModel();
            Assert.AreEqual(1, presentationModel.Assets.Count);

            presentationModel.OnAssetSelected(childFolderAsset);

            presentationModel.SelectedAsset = audioAsset;

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.CutAsset, default(object)));

            presentationModel.SelectedAsset = parentFolderAsset;

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.PasteAsset, default(object)));

            presentationModel.UpArrowCommand.Execute(null);

            Assert.AreEqual(3, presentationModel.Assets.Count);
            Assert.AreEqual(0, childFolderAsset.Assets.Count);
        }

        /// <summary>
        /// Should add the given asset in the current folder if it is not null
        /// and remove it from it's parent folder when KeyboardActionCommand is called.
        /// </summary>
        [TestMethod]
        public void ShouldAddAudioAssetToChildFolderWhenItIsNotNullAndRemoveItfromItsParentFolderWhenKeyboardActionCommandIsExecuted()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            AudioAsset audioAsset = new AudioAsset { Title = "Audio" };
            FolderAsset childFolderAsset = new FolderAsset { Title = "Child1Folder" };

            childFolderAsset.AddAssets(new ObservableCollection<Asset>() { videoAsset });
            FolderAsset parentFolderAsset = new FolderAsset { Title = "ParentFolder", };
            
            childFolderAsset.ParentFolder = parentFolderAsset;
            
            parentFolderAsset.AddAssets(new ObservableCollection<Asset>() { videoAsset, childFolderAsset });

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(parentFolderAsset);
            
            this.assetsDataServiceFacade.Assets.Add(parentFolderAsset);

            var presentationModel = this.CreatePresentationModel();

            presentationModel.SelectedAsset = audioAsset;

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.CutAsset, default(object)));

            presentationModel.SelectedAsset = childFolderAsset;

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.PasteAsset, default(object)));

            Assert.AreEqual(2, childFolderAsset.Assets.Count);
        }

        /// <summary>
        /// Tests if the CanShiftScale returns true when the scale value is less than
        /// one and user is zooming in.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueIfShiftTypeISZoomAndZoomInIsSelected()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.Scale = 0.5;
            Assert.IsTrue(presentationModel.ShiftSliderScaleCommand.CanExecute("+"));

            presentationModel.Scale = 1;
            Assert.IsFalse(presentationModel.ShiftSliderScaleCommand.CanExecute("+"));
        }
        
        /// <summary>
        /// Tests if the CanShiftScale returns true when the scale value is greater than
        /// zero and user is zooming out.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueIfShiftTypeISZoomAndZoomOutIsSelected()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.Scale = 0.5;
            Assert.IsTrue(presentationModel.ShiftSliderScaleCommand.CanExecute("-"));

            presentationModel.Scale = 0;
            Assert.IsFalse(presentationModel.ShiftSliderScaleCommand.CanExecute("-"));
        }

        /// <summary>
        /// CanAddFolder should return false if the folder name is emplty or null.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenCanAddFolderIsExecutedAndFolderNameIsEmptyOrNull()
        {
            var presentationModel = this.CreatePresentationModel();
            
            presentationModel.FolderTitle = string.Empty;

            var result = presentationModel.AddFolderCommand.CanExecute(null);

            Assert.IsFalse(result);

            result = presentationModel.AddFolderCommand.CanExecute(string.Empty);

            Assert.IsFalse(result);
        }

        /// <summary>
        /// CanAddFolder should return false if the current folder already contains a folder with the same name.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalseWhenCanAddFolderIsExecutedAndCurrentFolderAlreadyHavetheFolderWithTheSameName()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            FolderAsset folderAsset = new FolderAsset { Title = "Folder", };

            folderAsset.AddAssets(new ObservableCollection<Asset> { videoAsset });

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);

            var presentationModel = this.CreatePresentationModel();

            var result = presentationModel.AddFolderCommand.CanExecute("Folder");

            Assert.IsFalse(result);
        }

        /// <summary>
        /// CanAddFolder should return true if the current folder doesn't have any folder with the given name.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrueWhenCanAddFolderIsExecutedAndCurrentFolderThereIsNoFolderWithTheSameName()
        {
            VideoAsset videoAsset = new VideoAsset { Title = "VideoAsset" };
            FolderAsset folderAsset = new FolderAsset { Title = "Folder", };

            folderAsset.AddAssets(new ObservableCollection<Asset> { videoAsset });

            this.projectService.GetCurrentProject().MediaBin.Assets.Add(folderAsset);

            var presentationModel = this.CreatePresentationModel();

            var result = presentationModel.AddFolderCommand.CanExecute("Folder2");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests if the HeaderInfo property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderInfoResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderInfo;

            Assert.AreEqual("Clips", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOff property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOffResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderIconOff;

            Assert.AreEqual("/RCE.Modules.Library;component/images/mediabin_icon_off.png", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOn property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOnResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderIconOn;

            Assert.AreEqual("/RCE.Modules.Library;component/images/mediabin_icon_on.png", result);
        }

        /// <summary>
        /// Tests if the assets of the folder is loaded from the service if the user 
        /// selectes the folder and the asset of the foler is not yet loaded.
        /// </summary>
        [TestMethod]
        public void ShouldLoadTheAssetOfTheFolderFromServiceIfItIsNotLoadedAndUserSelectesTheFolder()
        {
            FolderAsset folderAsset = new FolderAsset { Title = "Folder", ProviderUri = new Uri("http://test") };

            this.assetsDataServiceFacade.Assets.Add(folderAsset);
            
            var presentationModel = this.CreatePresentationModel();
            this.view.ShowProgressBarCalled = false;

            presentationModel.OnAssetSelected(folderAsset);

            Assert.IsTrue(this.view.ShowProgressBarCalled);
            Assert.IsTrue(this.assetsDataServiceFacade.LoadAssetsByIdAsyncCalled);
        }

        /// <summary>
        /// Creates the <see cref="MediaBinViewPresentationModel"/>.
        /// </summary>
        /// <returns>The <see cref="MediaBinViewPresentationModel"/>.</returns>
        private IMediaBinViewPresentationModel CreatePresentationModel()
        {
            return new MediaBinViewPresentationModel(this.view, this.assetsDataServiceFacade, this.eventAggregator, this.loggerFacade, this.projectService, this.regionManager, this.configurationService);
        }
    }
}
