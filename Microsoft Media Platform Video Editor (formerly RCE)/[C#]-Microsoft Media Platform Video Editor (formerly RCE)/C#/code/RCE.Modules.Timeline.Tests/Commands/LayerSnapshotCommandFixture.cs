// <copyright file="LayerSnapshotCommandFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LayerSnapshotCommandFixture.cs                     
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
    using System.Linq;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using SMPTETimecode;
    using Timeline.Commands;

    /// <summary>
    /// A class for testing the <see cref="LayerSnapshotCommand"/>.
    /// </summary>
    [TestClass]
    public class LayerSnapshotCommandFixture
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
            this.sequenceModel = new MockSequenceModel();
        }

        /// <summary>
        /// Tests that the layer is not being modified when the command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldNotModifyLayerWhenExecuteCommand()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var previousElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var currentElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(60, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(previousElement);
            track.Shots.Add(currentElement);

            var memento = track.GetMemento();

            previousElement.Position = TimeCode.FromAbsoluteTime(50, this.sequenceModel.Duration.FrameRate);
            currentElement.Position = TimeCode.FromAbsoluteTime(100, this.sequenceModel.Duration.FrameRate);

            var newMemento = track.GetMemento();

            Assert.AreNotEqual(memento, newMemento);

            LayerSnapshotCommand command = new LayerSnapshotCommand(this.sequenceModel, track, memento, newMemento);
            command.Execute();

            foreach (TimelineElement element in track.Shots)
            {
                TimelineElement mementoElement = newMemento.Where(x => x.Id == element.Id).FirstOrDefault();

                Assert.AreEqual(mementoElement.Position, element.Position);
                Assert.AreEqual(mementoElement.InPosition, element.InPosition);
                Assert.AreEqual(mementoElement.OutPosition, element.OutPosition);
            }
        }

        /// <summary>
        /// Tests that the memento is being rollback when unexecuting the command.
        /// </summary>
        [TestMethod]
        public void ShouldRollbackMementoWhenUnExecuteCommand()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var previousElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var currentElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(60, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(previousElement);
            track.Shots.Add(currentElement);

            var memento = track.GetMemento();

            previousElement.Position = TimeCode.FromAbsoluteTime(50, this.sequenceModel.Duration.FrameRate);
            currentElement.Position = TimeCode.FromAbsoluteTime(100, this.sequenceModel.Duration.FrameRate);

            var newMemento = track.GetMemento();

            Assert.AreNotEqual(memento, newMemento);

            LayerSnapshotCommand command = new LayerSnapshotCommand(this.sequenceModel, track, memento, newMemento);
            command.Execute();

            command.UnExecute();

            foreach (TimelineElement element in track.Shots)
            {
                TimelineElement mementoElement = memento.Where(x => x.Id == element.Id).FirstOrDefault();

                Assert.AreEqual(mementoElement.Position, element.Position);
                Assert.AreEqual(mementoElement.InPosition, element.InPosition);
                Assert.AreEqual(mementoElement.OutPosition, element.OutPosition);
            }
        }
    }
}
