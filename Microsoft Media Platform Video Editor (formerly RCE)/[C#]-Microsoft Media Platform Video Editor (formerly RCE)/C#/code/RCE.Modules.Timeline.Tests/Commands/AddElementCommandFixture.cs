// <copyright file="AddElementCommandFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AddElementCommandFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Commands
{
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Services.Contracts;
    using SMPTETimecode;
    using Timeline.Commands;
    using Track = RCE.Infrastructure.Models.Track;
    using TrackType = RCE.Infrastructure.Models.TrackType;

    /// <summary>
    /// A class for testing the <see cref="AddElementCommand"/>.
    /// </summary>
    [TestClass]
    public class AddElementCommandFixture
    {
        /// <summary>
        /// The mocked timeline model.
        /// </summary>
        private MockSequenceModel sequenceModel;

        /// <summary>
        /// Initializes resources need it by the tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.sequenceModel = new MockSequenceModel
                                     {
                                         Duration = TimeCode.FromAbsoluteTime(10000, SmpteFrameRate.Smpte30)
                                     };
        }

        /// <summary>
        /// Tests that AddElement should be called on TimelineModel when the asset of the timeline element added is a video asset.
        /// </summary>
        [TestMethod]
        public void ShouldCallAddElementOnTimelineModelWhenAssetIsVideoAsset()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var position = TimeCode.FromAbsoluteTime(10, SmpteFrameRate.Smpte30);

            var element = new TimelineElement
                              {
                                  Position = position,
                                  Asset = new VideoAsset
                                      {
                                          Duration = TimeCode.FromAbsoluteTime(30, SmpteFrameRate.Smpte30),
                                          FrameRate = SmpteFrameRate.Smpte30,
                                          Title = "Test Video #1"
                                      },
                              };

            var command = new AddElementCommand(this.sequenceModel, track, element);

            Assert.IsFalse(this.sequenceModel.AddElementCalled);

            command.Execute();

            Assert.IsTrue(this.sequenceModel.AddElementCalled);
            Assert.AreEqual(element, this.sequenceModel.AddElementArgument);
        }

        /// <summary>
        /// Tests that AddElement should be called on TimelineModel when the asset of the timeline element added is an audio asset.
        /// </summary>
        [TestMethod]
        public void ShouldCallAddElementOnTimelineModelWhenAssetIsAudioAsset()
        {
            var track = new Track { TrackType = TrackType.Audio };
            this.sequenceModel.Tracks.Add(track);

            var position = TimeCode.FromAbsoluteTime(5, SmpteFrameRate.Smpte30);

            var element = new TimelineElement
                              {
                                  Position = position,
                                  Asset = new AudioAsset
                                              {
                                                  DurationInSeconds = 2,
                                                  Title = "Test Audio #1"
                                              }
                              };

            var command = new AddElementCommand(this.sequenceModel, track, element);

            Assert.IsFalse(this.sequenceModel.AddElementCalled);

            command.Execute();

            Assert.IsTrue(this.sequenceModel.AddElementCalled);
            Assert.AreEqual(element, this.sequenceModel.AddElementArgument);
        }

        /// <summary>
        /// Tests that AddElement should be called on TimelineModel when the asset of the timeline element added is an image asset.
        /// </summary>
        [TestMethod]
        public void ShouldCallAddElementOnTimelineModelWhenAssetIsImageAsset()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var position = TimeCode.FromAbsoluteTime(20, SmpteFrameRate.Smpte30);

            var element = new TimelineElement
                              {
                                  Position = position,
                                  Asset = new ImageAsset
                                              {
                                                  Title = "Test Image #1"
                                              }
                              };

            var command = new AddElementCommand(this.sequenceModel, track, element);

            Assert.IsFalse(this.sequenceModel.AddElementCalled);

            command.Execute();

            Assert.IsTrue(this.sequenceModel.AddElementCalled);
            Assert.AreEqual(element, this.sequenceModel.AddElementArgument);
        }

        /// <summary>
        /// Tests that RemoveElement should be called on TimelineModel when the UnExecuting the command.
        /// </summary>
        [TestMethod]
        public void ShouldCallRemoveElementOnTimelineModelWhenUnExecute()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var position = TimeCode.FromAbsoluteTime(10, SmpteFrameRate.Smpte30);

            var element = new TimelineElement
                              {
                                  Position = position,
                                  Asset = new VideoAsset
                                              {
                                                  Duration = TimeCode.FromAbsoluteTime(30, SmpteFrameRate.Smpte30),
                                                  FrameRate = SmpteFrameRate.Smpte30,
                                                  Title = "Test Video #1"
                                              }
                              };

            var command = new AddElementCommand(this.sequenceModel, track, element);

            command.Execute();

            var addedElement = this.sequenceModel.AddElementArgument;
            
            Assert.IsFalse(this.sequenceModel.RemoveElementCalled);

            command.UnExecute();

            Assert.IsTrue(this.sequenceModel.RemoveElementCalled);
            Assert.AreEqual(addedElement, this.sequenceModel.RemoveElementArgument);
        }
    }
}
