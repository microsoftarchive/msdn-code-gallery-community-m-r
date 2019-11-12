// <copyright file="TimelineSnapshotCommandFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineSnapshotCommandFixture.cs                     
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
    using System.Collections.Generic;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Timeline.Commands;

    /// <summary>
    /// A class for testing the <see cref="TimelineSnapshotCommand"/>.
    /// </summary>
    [TestClass]
    public class TimelineSnapshotCommandFixture
    {
        /// <summary>
        /// Tests that the layer snapshot command should be executed when executing the timeline snapshot command.
        /// </summary>
        [TestMethod]
        public void ShouldExecuteLayerSnapshotCommandsOnExecute()
        {
            TestableLayerSnapshotCommand layerSnapshotCommand = new TestableLayerSnapshotCommand();
            TestableLayerSnapshotCommand anotherLayerSnapshotCommand = new TestableLayerSnapshotCommand();

            CompositeUndoableCommand<LayerSnapshotCommand> command = new CompositeUndoableCommand<LayerSnapshotCommand>(new List<LayerSnapshotCommand> { layerSnapshotCommand, anotherLayerSnapshotCommand });

            Assert.IsFalse(layerSnapshotCommand.WasExecuted);
            Assert.IsFalse(anotherLayerSnapshotCommand.WasExecuted);

            command.Execute();

            Assert.IsTrue(layerSnapshotCommand.WasExecuted);
            Assert.IsTrue(anotherLayerSnapshotCommand.WasExecuted);

            Assert.IsTrue(layerSnapshotCommand.ExecuteCommandCalled);
            Assert.IsTrue(anotherLayerSnapshotCommand.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the layer snapshot command should be unexecuted when unexecuting the timeline snapshot command.
        /// </summary>
        [TestMethod]
        public void ShouldUnExecuteLayerSnapshotCommandsOnUnExecute()
        {
            TestableLayerSnapshotCommand layerSnapshotCommand = new TestableLayerSnapshotCommand();
            TestableLayerSnapshotCommand anotherLayerSnapshotCommand = new TestableLayerSnapshotCommand();

            CompositeUndoableCommand<LayerSnapshotCommand> command = new CompositeUndoableCommand<LayerSnapshotCommand>(new List<LayerSnapshotCommand> { layerSnapshotCommand, anotherLayerSnapshotCommand });

            command.Execute();

            command.UnExecute();

            Assert.IsTrue(layerSnapshotCommand.UnExecuteCommandCalled);
            Assert.IsTrue(anotherLayerSnapshotCommand.UnExecuteCommandCalled);
        }

        /// <summary>
        /// Testable layer snapshot command.
        /// </summary>
        private class TestableLayerSnapshotCommand : LayerSnapshotCommand
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestableLayerSnapshotCommand"/> class.
            /// </summary>
            public TestableLayerSnapshotCommand()
                : this(null, null, null, null)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TestableLayerSnapshotCommand"/> class.
            /// </summary>
            /// <param name="sequenceModel">The timeline model.</param>
            /// <param name="layer">The layer where the snapshot is being took.</param>
            /// <param name="currentMemento">The current state of the layer.</param>
            /// <param name="newMemento">The new state of the layer.</param>
            public TestableLayerSnapshotCommand(ISequenceModel sequenceModel, Track layer, IList<TimelineElement> currentMemento, IList<TimelineElement> newMemento) : base(sequenceModel, layer, currentMemento, newMemento)
            {
            }

            /// <summary>
            /// Gets or sets a value indicating whether the ExecuteCommand method was called.
            /// </summary>
            /// <value>A true if was called;otherwise false.</value>
            public bool ExecuteCommandCalled { get; private set; }

            /// <summary>
            /// Gets or sets a value indicating whether the UnExecuteCommand method was called.
            /// </summary>
            /// /// <value>A true if was called;otherwise false.</value>
            public bool UnExecuteCommandCalled { get; private set; }

            /// <summary>
            /// Sets the new memento to the layer.
            /// </summary>
            protected override void ExecuteCommand()
            {
                this.ExecuteCommandCalled = true;
            }

            /// <summary>
            /// Sets the current memento to the layer.
            /// </summary>
            protected override void UnExecuteCommand()
            {
                this.UnExecuteCommandCalled = true;
            }
        }
    }
}
