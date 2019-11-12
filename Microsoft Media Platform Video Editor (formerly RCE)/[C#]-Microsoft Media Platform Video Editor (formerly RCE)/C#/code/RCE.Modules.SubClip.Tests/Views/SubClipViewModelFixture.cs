// <copyright file="SubClipViewModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClipViewModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.SubClip.Tests.Views
{
    using System.Windows.Input;

    using Microsoft.Practices.Composite.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.DragDrop;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Modules.SubClip.Models;
    using RCE.Modules.SubClip.Tests.Mocks;
    using RCE.Modules.SubClip.Views;

    [TestClass]
    public class SubClipViewModelFixture
    {
        private MockSubClipView view;

        private MockEventAggregator eventAggregator;

        private MockResetWindowsEvent resetWindowsEvent;

        private MockAddPreviewEvent addPreviewEvent;

        private MockConfigurationService configurationService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.view = new MockSubClipView();
            this.eventAggregator = new MockEventAggregator();
            this.resetWindowsEvent = new MockResetWindowsEvent();
            this.addPreviewEvent = new MockAddPreviewEvent();
            this.configurationService = new MockConfigurationService();
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowsEvent);
            this.eventAggregator.AddMapping<AddPreviewEvent>(this.addPreviewEvent);
        }

        [TestMethod]
        public void ShouldUseViewPassedThroughConstructor()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreEqual(this.view, viewModel.View);
        }

        [TestMethod]
        public void ShouldCallSetDataContextInView()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreSame(this.view.SetDataContextParameter, viewModel);
        }

        [TestMethod]
        public void ShouldUpdateCurrentAssetWhenExecutingDropCommand()
        {
            var viewModel = this.CreateViewModel();

            DropPayload dropPayload = new DropPayload();
            VideoAsset asset = new VideoAsset();

            dropPayload.DraggedItem = asset;
            
            Assert.IsNull(viewModel.Asset);

            viewModel.DropCommand.Execute(dropPayload);

            Assert.IsNotNull(viewModel.Asset);
            Assert.AreSame(asset, viewModel.Asset);
        }

        [TestMethod]
        public void ShouldUpdateHasAssetWhenExecutingDropCommand()
        {
            var viewModel = this.CreateViewModel();

            DropPayload dropPayload = new DropPayload();
            VideoAsset asset = new VideoAsset();

            dropPayload.DraggedItem = asset;

            Assert.IsFalse(viewModel.HasAsset);

            viewModel.DropCommand.Execute(dropPayload);

            Assert.IsTrue(viewModel.HasAsset);
        }

        [TestMethod]
        public void ShouldNotifyAssetPropertyChangedWhenAssetChanges()
        {
            var viewModel = this.CreateViewModel();
            VideoAsset asset1 = new VideoAsset();
            VideoAsset asset2 = new VideoAsset();

            int timesRaised = 0;

            viewModel.PropertyChanged += (s, a) =>
                {
                    if (a.PropertyName == "Asset")
                    {
                        timesRaised++;
                    }
                };

            viewModel.Asset = asset1;
            viewModel.Asset = asset1;
            viewModel.Asset = asset2;
            viewModel.Asset = asset2;

            Assert.AreEqual(2, timesRaised);
        }

        [TestMethod]
        public void ShouldNotifyHasAssetPropertyChangedWhenAssetChanges()
        {
            var viewModel = this.CreateViewModel();
            VideoAsset asset1 = new VideoAsset();
            VideoAsset asset2 = new VideoAsset();

            int timesRaised = 0;

            viewModel.PropertyChanged += (s, a) =>
                {
                    if (a.PropertyName == "HasAsset")
                    {
                        timesRaised++;
                    }
                };

            viewModel.Asset = asset1;
            viewModel.Asset = asset1;
            viewModel.Asset = asset2;
            viewModel.Asset = asset2;

            Assert.AreEqual(2, timesRaised);
        }

        [TestMethod]
        public void ShouldNotifyPropertyChangedWhenDropCommandChanges()
        {
            var viewModel = this.CreateViewModel();
            ICommand command1 = new MockCommand();
            ICommand command2 = new MockCommand();

            int timesRaised = 0;

            viewModel.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "DropCommand")
                {
                    timesRaised++;
                }
            };

            viewModel.DropCommand = command1;
            viewModel.DropCommand = command1;
            viewModel.DropCommand = command2;
            viewModel.DropCommand = command2;

            Assert.AreEqual(2, timesRaised);
        }

        [TestMethod]
        public void ShouldChangeVideoAssetInOutPreviewAudioStreamWhenExecutingAudioPreviewSelectionChangedCommand()
        {
            var viewModel = this.CreateViewModel();

            VideoAssetInOut videoInOut = new VideoAssetInOut(new VideoAsset());
            SmoothStreamingVideoAsset smoothStreamingVideoAsset = new SmoothStreamingVideoAsset();

            AudioStream spanishPreviewStream = new AudioStream("audio_es", true);
            AudioStream englishPreviewStream = new AudioStream("audio_es", true);

            smoothStreamingVideoAsset.AudioStreams.Add(spanishPreviewStream);
            smoothStreamingVideoAsset.AudioStreams.Add(englishPreviewStream);

            viewModel.VideoAssetInOut = videoInOut;
            viewModel.Asset = smoothStreamingVideoAsset;

            Assert.AreSame(spanishPreviewStream, viewModel.VideoAssetInOut.PreviewAudioStream);

            viewModel.AudioPreviewSelectionChangedCommand.Execute(
                new StreamOption { Name = "audio_es", PreviewSelected = false });

            Assert.IsNotNull(viewModel.VideoAssetInOut.PreviewAudioStream);
            Assert.AreEqual("audio_es", viewModel.VideoAssetInOut.PreviewAudioStream.Name);
            Assert.IsTrue(viewModel.VideoAssetInOut.PreviewAudioStream.IsStereo);
        }

        [TestMethod]
        public void ShouldChangeVideoAssetInOutPreviewVideoCameraWhenExecutingVideoPreviewSelectionChangedCommand()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = new SmoothStreamingVideoAsset();

            VideoAssetInOut videoInOut = new VideoAssetInOut(smoothStreamingVideoAsset);

            viewModel.VideoAssetInOut = videoInOut;
            
            smoothStreamingVideoAsset.VideoStreams.Add("camera1");
            smoothStreamingVideoAsset.VideoStreams.Add("camera2");
            
            viewModel.Asset = smoothStreamingVideoAsset;

            Assert.AreEqual("camera1", viewModel.VideoAssetInOut.PreviewVideoCamera);

            viewModel.VideoPreviewSelectionChangedCommand.Execute(new StreamOption { Name = "camera2" });

            Assert.IsNotNull(viewModel.VideoAssetInOut.PreviewVideoCamera);
            Assert.AreEqual("camera2", viewModel.VideoAssetInOut.PreviewVideoCamera);
        }

        [TestMethod]
        public void ShouldChangeSequenceVideoWhenExecutingVideoSequenceSelectionChangedCommand()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = new SmoothStreamingVideoAsset();

            VideoAssetInOut videoInOut = new VideoAssetInOut(smoothStreamingVideoAsset);

            viewModel.VideoAssetInOut = videoInOut;

            smoothStreamingVideoAsset.VideoStreams.Add("camera1");
            smoothStreamingVideoAsset.VideoStreams.Add("camera2");

            viewModel.Asset = smoothStreamingVideoAsset;

            Assert.AreEqual("camera1", viewModel.VideoAssetInOut.SequenceVideoCamera);

            viewModel.VideoSequenceSelectionChangedCommand.Execute(new StreamOption { Name = "camera2" });

            Assert.IsNotNull(viewModel.VideoAssetInOut.SequenceVideoCamera);
            Assert.AreEqual("camera2", viewModel.VideoAssetInOut.SequenceVideoCamera);
        }

        [TestMethod]
        public void ShouldAddAudioStreamToSequenceAudioStreamsWhenExecutingAudioSequenceSelectionChangedCommandWithPreviouslyNotSelectedAudio()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = new SmoothStreamingVideoAsset();

            VideoAssetInOut videoInOut = new VideoAssetInOut(smoothStreamingVideoAsset);

            AudioStream englishStream = new AudioStream("audio_en", false);
            AudioStream spanishStream = new AudioStream("audio_es", false);
            
            smoothStreamingVideoAsset.AudioStreams.Add(englishStream);
            smoothStreamingVideoAsset.AudioStreams.Add(spanishStream);

            viewModel.VideoAssetInOut = videoInOut;

            viewModel.Asset = smoothStreamingVideoAsset;

            Assert.AreEqual(1, viewModel.VideoAssetInOut.SequenceAudioStreams.Count);
            Assert.AreEqual("audio_en", viewModel.VideoAssetInOut.SequenceAudioStreams[0].Name);

            viewModel.AudioSequenceSelectionChangedCommand.Execute(
                new StreamOption { Name = "audio_es", SequenceSelected = true });

            Assert.AreEqual(2, viewModel.VideoAssetInOut.SequenceAudioStreams.Count);
            Assert.AreSame(spanishStream, viewModel.VideoAssetInOut.SequenceAudioStreams[1]);
        }

        [TestMethod]
        public void ShouldRemoveAudioStreamFromSequenceAudioStreamsWhenExecutingAudioSequenceSelectionChangedCommandWithPreviouslySelectedAudio()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = new SmoothStreamingVideoAsset();

            var spanishAudioStream = new AudioStream("audio_es", false);
            var englishAudioStream = new AudioStream("audio_en", false);

            smoothStreamingVideoAsset.AudioStreams.Add(spanishAudioStream);
            smoothStreamingVideoAsset.AudioStreams.Add(englishAudioStream);

            VideoAssetInOut videoInOut = new VideoAssetInOut(smoothStreamingVideoAsset);

            viewModel.VideoAssetInOut = videoInOut;
            viewModel.Asset = smoothStreamingVideoAsset;

            Assert.AreEqual(1, viewModel.VideoAssetInOut.SequenceAudioStreams.Count);

            viewModel.AudioSequenceSelectionChangedCommand.Execute(
                new StreamOption { Name = "audio_en", SequenceSelected = true });

            Assert.AreEqual(2, viewModel.VideoAssetInOut.SequenceAudioStreams.Count);

            viewModel.AudioSequenceSelectionChangedCommand.Execute(new StreamOption { Name = "audio_en", SequenceSelected = false });

            Assert.AreEqual(1, viewModel.VideoAssetInOut.SequenceAudioStreams.Count);
        }

        [TestMethod]
        public void ShouldAddMultipleAudioStreamsToSequenceAudioStreamsWhenExecutingAudioSequenceSelectionChangedCommandWithDifferentAudios()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = new SmoothStreamingVideoAsset();
            
            VideoAssetInOut videoInOut = new VideoAssetInOut(smoothStreamingVideoAsset);

            viewModel.VideoAssetInOut = videoInOut;

            var audioStreamEs = new AudioStream("audio_es", false);
            var audioStreamEn = new AudioStream("audio_en", false);
            var audioStreamDirector = new AudioStream("audio_director", false);

            smoothStreamingVideoAsset.AudioStreams.Add(audioStreamEs);
            smoothStreamingVideoAsset.AudioStreams.Add(audioStreamEn);
            smoothStreamingVideoAsset.AudioStreams.Add(audioStreamDirector);

            viewModel.Asset = smoothStreamingVideoAsset;

            Assert.AreEqual(1, viewModel.VideoAssetInOut.SequenceAudioStreams.Count);

            viewModel.AudioSequenceSelectionChangedCommand.Execute(
                new StreamOption { Name = "audio_en", SequenceSelected = true });

            Assert.AreEqual(2, viewModel.VideoAssetInOut.SequenceAudioStreams.Count);
            Assert.AreSame(audioStreamEn, viewModel.VideoAssetInOut.SequenceAudioStreams[1]);

            viewModel.AudioSequenceSelectionChangedCommand.Execute(new StreamOption { Name = "audio_director", SequenceSelected = true });

            Assert.AreEqual(3, viewModel.VideoAssetInOut.SequenceAudioStreams.Count);
            Assert.AreSame(audioStreamDirector, viewModel.VideoAssetInOut.SequenceAudioStreams[2]);
        }

        [TestMethod]
        public void ShouldUpdateTitleWhenAssetChanges()
        {
            var viewModel = this.CreateViewModel();

            Assert.AreEqual("Sub-Clip Tool", viewModel.Title);

            VideoAsset asset = new VideoAsset();
            asset.Title = "Asset Title";

            bool wasCalled = false;
            string expectedTitle = string.Format("Source: {0}", asset.Title);

            viewModel.TitleUpdated += (s, a) => { wasCalled = true; };
            
            viewModel.Asset = asset;
            
            Assert.AreEqual(expectedTitle, viewModel.Title);
            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
        public void ShouldAddDefaultOptionToAssetWithoutVideoStreams()
        {
            var viewModel = this.CreateViewModel();

            VideoAsset asset = new VideoAsset();

            viewModel.Asset = asset;

            Assert.AreEqual(1, viewModel.AvailableVideoStreams.Count);
            Assert.AreEqual("Default", viewModel.AvailableVideoStreams[0].Name);
        }

        [TestMethod]
        public void ShouldAddDefaultOptionToAssetWithSingleVideotreamWithEmptyName()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset();

            asset.VideoStreams.Add(string.Empty);

            viewModel.Asset = asset;

            Assert.AreEqual(1, viewModel.AvailableVideoStreams.Count);
            Assert.AreEqual("Default", viewModel.AvailableVideoStreams[0].Name);
        }

        [TestMethod]
        public void ShouldAddDefaultOptionToAssetWithSingleVideotreamWithNullName()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset();

            asset.VideoStreams.Add(null);

            viewModel.Asset = asset;

            Assert.AreEqual(1, viewModel.AvailableVideoStreams.Count);
            Assert.AreEqual("Default", viewModel.AvailableVideoStreams[0].Name);
        }

        [TestMethod]
        public void ShouldAddDefaultOptionToAssetWithoutAudioStreams()
        {
            var viewModel = this.CreateViewModel();

            VideoAsset asset = new VideoAsset();

            viewModel.Asset = asset;

            Assert.AreEqual(1, viewModel.AvailableAudioStreams.Count);
            Assert.AreEqual("Default", viewModel.AvailableAudioStreams[0].Name);
        }

        [TestMethod]
        public void ShouldAddDefaultOptionToAssetWithSingleAudioStreamWithEmptyName()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset();

            asset.AudioStreams.Add(new AudioStream(string.Empty, false));

            viewModel.Asset = asset;

            Assert.AreEqual(1, viewModel.AvailableAudioStreams.Count);
            Assert.AreEqual("Default", viewModel.AvailableAudioStreams[0].Name);
        }

        [TestMethod]
        public void ShouldAddDefaultOptionToAssetWithSingleAudioStreamWithNullName()
        {
            var viewModel = this.CreateViewModel();

            SmoothStreamingVideoAsset asset = new SmoothStreamingVideoAsset();

            asset.AudioStreams.Add(new AudioStream(null, false));

            viewModel.Asset = asset;

            Assert.AreEqual(1, viewModel.AvailableAudioStreams.Count);
            Assert.AreEqual("Default", viewModel.AvailableAudioStreams[0].Name);
        }

        public SubClipViewModel CreateViewModel()
        {
            return new SubClipViewModel(this.view, this.eventAggregator, this.configurationService);
        }
    }
}