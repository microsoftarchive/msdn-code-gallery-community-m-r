// <copyright file="ToggleLinkElementCommandFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ToggleLinkElementCommandFixture.cs                     
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
    using SMPTETimecode;
    using Timeline.Commands;

    /// <summary>
    /// A class for testing the <see cref="ToggleLinkElementCommand"/>.
    /// </summary>
    [TestClass]
    public class ToggleLinkElementCommandFixture
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
        /// Tests that LinkPreviousElement method should not be called if there is no previous element.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallToLinkPreviousElementIfThereIsNoPreviousElement()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var currentElement = new TimelineElement
                                    {
                                        Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                                        InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                                        OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
                                    };

            track.Shots.Add(currentElement);

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.In);

            Assert.IsFalse(this.sequenceModel.LinkPreviousElementCalled);

            command.Execute();

            Assert.IsFalse(this.sequenceModel.LinkPreviousElementCalled);
        }

        /// <summary>
        /// Tests that LinkNextElement method should not be called if there is no next element.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallToLinkNextElementIfThereIsNoNextElement()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var currentElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(currentElement);

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.Out);

            Assert.IsFalse(this.sequenceModel.LinkNextElementCalled);

            command.Execute();

            Assert.IsFalse(this.sequenceModel.LinkNextElementCalled);
        }

        /// <summary>
        /// Tests that the LinkPreviousElement method should be called.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLinkPreviousElement()
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
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(previousElement);
            track.Shots.Add(currentElement);

            this.sequenceModel.IsElementLinkedToReturnValue = false;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position)
                {
                    return previousElement;
                }
                else
                {
                    return null;
                }
            };

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.In);

            Assert.IsFalse(this.sequenceModel.LinkPreviousElementCalled);

            command.Execute();

            Assert.IsTrue(this.sequenceModel.LinkPreviousElementCalled);
        }

        /// <summary>
        /// Tests that the UnlinkPreviousElement method should be called when the command is executed 
        /// if the elements are linked.
        /// </summary>
        [TestMethod]
        public void ShouldCallToUnlinkPreviousElementIfTheElementsAreLinked()
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
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(previousElement);
            track.Shots.Add(currentElement);

            this.sequenceModel.IsElementLinkedToReturnValue = true;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position)
                {
                    return previousElement;
                }
                else
                {
                    return null;
                }
            };

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.In);

            Assert.IsFalse(this.sequenceModel.UnlinkElementsCalled);

            command.Execute();

            Assert.IsTrue(this.sequenceModel.UnlinkElementsCalled);
        }

        /// <summary>
        /// Tests that the LinkNextElement method should be called.
        /// </summary>
        [TestMethod]
        public void ShouldCallToLinkNextElement()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var currentElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var nextElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(40, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(currentElement);
            track.Shots.Add(nextElement);

            this.sequenceModel.IsElementLinkedToReturnValue = false;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position + currentElement.Duration)
                {
                    return nextElement;
                }
                else
                {
                    return null;
                }
            };

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.Out);

            Assert.IsFalse(this.sequenceModel.LinkNextElementCalled);

            command.Execute();

            Assert.IsTrue(this.sequenceModel.LinkNextElementCalled);
        }

        /// <summary>
        /// Tests that the UnlinkNextElement method should be called when the command is executed 
        /// if the elements are linked.
        /// </summary>
        [TestMethod]
        public void ShouldCallToUnlinkNextElementIfTheElementsAreLinked()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var currentElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var nextElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(40, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(currentElement);
            track.Shots.Add(nextElement);

            this.sequenceModel.IsElementLinkedToReturnValue = true;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position + currentElement.Duration)
                {
                    return nextElement;
                }
                else
                {
                    return null;
                }
            };

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.Out);

            Assert.IsFalse(this.sequenceModel.UnlinkElementsCalled);

            command.Execute();

            Assert.IsTrue(this.sequenceModel.UnlinkElementsCalled);
        }

        /// <summary>
        /// Tests that the UnlinkPreviousElement method should be called when the command is unexecuted. 
        /// </summary>
        [TestMethod]
        public void ShouldCallToUnlinkPreviousElementIfTheElementsAreLinkedWhenUnExecuteCommand()
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
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(previousElement);
            track.Shots.Add(currentElement);

            this.sequenceModel.IsElementLinkedToReturnValue = false;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position)
                {
                    return previousElement;
                }
                else
                {
                    return null;
                }
            };

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.In);

            command.Execute();

            this.sequenceModel.IsElementLinkedToReturnValue = true;

            Assert.IsFalse(this.sequenceModel.UnlinkElementsCalled);

            command.UnExecute();

            Assert.IsTrue(this.sequenceModel.UnlinkElementsCalled);
        }

        /// <summary>
        /// Tests that the LinkPreviousElement method should be called when the command is unexecuted. 
        /// </summary>
        [TestMethod]
        public void ShouldCallToLinkPreviousElementIfTheElementsAreUnlinkedWhenUnExecuteCommand()
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
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(previousElement);
            track.Shots.Add(currentElement);

            this.sequenceModel.IsElementLinkedToReturnValue = true;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position)
                {
                    return previousElement;
                }
                else
                {
                    return null;
                }
            };

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.In);

            command.Execute();

            this.sequenceModel.IsElementLinkedToReturnValue = false;

            Assert.IsFalse(this.sequenceModel.LinkPreviousElementCalled);

            command.UnExecute();

            Assert.IsTrue(this.sequenceModel.LinkPreviousElementCalled);
        }

        /// <summary>
        /// Tests that the UnlinkNextElement method should be called when the command is unexecuted. 
        /// </summary>
        [TestMethod]
        public void ShouldCallToUnlinkNextElementIfTheElementsAreLinkedWhenUnExecuteCommand()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var currentElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var nextElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(40, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(currentElement);
            track.Shots.Add(nextElement);

            this.sequenceModel.IsElementLinkedToReturnValue = false;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position + currentElement.Duration)
                {
                    return nextElement;
                }
                else
                {
                    return null;
                }
            };

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.Out);

            command.Execute();

            this.sequenceModel.IsElementLinkedToReturnValue = true;

            Assert.IsFalse(this.sequenceModel.UnlinkElementsCalled);

            command.UnExecute();

            Assert.IsTrue(this.sequenceModel.UnlinkElementsCalled);
        }

        /// <summary>
        /// Tests that the LinkNextElement method should be called when the command is unexecuted. 
        /// </summary>
        [TestMethod]
        public void ShouldCallToLinkNextElementIfTheElementsAreUnlinkedWhenUnExecuteCommand()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var currentElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var nextElement = new TimelineElement
            {
                Position = TimeCode.FromAbsoluteTime(40, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            track.Shots.Add(currentElement);
            track.Shots.Add(nextElement);

            this.sequenceModel.IsElementLinkedToReturnValue = true;

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                if (this.sequenceModel.GetElementAtPositionPositionArgument == currentElement.Position + currentElement.Duration)
                {
                    return nextElement;
                }
                else
                {
                    return null;
                }
            };

            ToggleLinkElementCommand command = new ToggleLinkElementCommand(this.sequenceModel, track, currentElement, LinkPosition.Out);

            command.Execute();

            this.sequenceModel.IsElementLinkedToReturnValue = false;

            Assert.IsFalse(this.sequenceModel.LinkNextElementCalled);

            command.UnExecute();

            Assert.IsTrue(this.sequenceModel.LinkNextElementCalled);
        }
    }
}
