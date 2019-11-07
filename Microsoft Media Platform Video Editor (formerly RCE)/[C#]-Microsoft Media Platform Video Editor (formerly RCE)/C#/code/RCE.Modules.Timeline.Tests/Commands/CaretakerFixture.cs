// <copyright file="CaretakerFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CaretakerFixture.cs                     
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Events;
    using RCE.Modules.Timeline.Tests.Mocks;
    using Timeline.Commands;

    /// <summary>
    /// A class for testing the <see cref="Caretaker"/>.
    /// </summary>
    [TestClass]
    public class CaretakerFixture
    {
        private MockOperationExecutedInTimelineEvent operationExecutedInTimelineEvent;

        private MockOperationUndoneInTimeline operationUndoneInTimeline;

        private MockEventAggregator eventAggregator;

        [TestInitialize]
        public void SetUp()
        {
            this.eventAggregator = new MockEventAggregator();
            this.operationExecutedInTimelineEvent = new MockOperationExecutedInTimelineEvent();
            this.operationUndoneInTimeline = new MockOperationUndoneInTimeline();

            this.eventAggregator.AddMapping<OperationExecutedInTimelineEvent>(this.operationExecutedInTimelineEvent);
            this.eventAggregator.AddMapping<OperationUndoneInTimelineEvent>(this.operationUndoneInTimeline);
        }

        /// <summary>
        /// Tests that the command is being executed.
        /// </summary>
        [TestMethod]
        public void ShouldExecuteCommand()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);

            var command = new TestableUndoableCommand();

            Assert.IsFalse(command.ExecuteCommandCalled);

            caretaker.ExecuteCommand(command);

            Assert.IsTrue(command.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the command is not executed if was already executed.
        /// </summary>
        [TestMethod]
        public void ShouldNotExecuteCommandIfWasAlreadyExecuted()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);

            var command = new TestableUndoableCommand();

            command.Execute();

            command.ExecuteCommandCalled = false;

            caretaker.ExecuteCommand(command);

            Assert.IsFalse(command.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that an undo operation can be performed.
        /// </summary>
        [TestMethod]
        public void ShouldUndoCommand()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);

            var command = new TestableUndoableCommand();

            caretaker.ExecuteCommand(command);

            Assert.IsFalse(command.UnExecuteCommandCalled);

            caretaker.Undo();

            Assert.IsTrue(command.UnExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that a command should not be undone if the undo was already performed.
        /// </summary>
        [TestMethod]
        public void ShouldNotUndoTheSameCommand()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);

            var command = new TestableUndoableCommand();

            caretaker.ExecuteCommand(command);

            caretaker.Undo();

            Assert.IsTrue(command.UnExecuteCommandCalled);

            command.UnExecuteCommandCalled = false;

            caretaker.Undo();

            Assert.IsFalse(command.UnExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that a redo operation can be performed.
        /// </summary>
        [TestMethod]
        public void ShouldRedoCommand()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);

            var command = new TestableUndoableCommand();

            caretaker.ExecuteCommand(command);

            command.ExecuteCommandCalled = false;
            
            caretaker.Undo();

            caretaker.Redo();

            Assert.IsTrue(command.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that a redo operation should not be performed if an undo operation was not performed.
        /// </summary>
        [TestMethod]
        public void ShouldNotRedoCommandIfWasNotUndo()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);

            var command = new TestableUndoableCommand();

            caretaker.ExecuteCommand(command);

            command.ExecuteCommandCalled = false;

            caretaker.Redo();

            Assert.IsFalse(command.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that an undo/redo operation can be performed.
        /// </summary>
        [TestMethod]
        public void ShouldUndoARedoOperation()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);

            var command = new TestableUndoableCommand();

            caretaker.ExecuteCommand(command);

            caretaker.Undo();

            caretaker.Redo();

            Assert.IsTrue(command.UnExecuteCommandCalled);

            command.UnExecuteCommandCalled = false;

            Assert.IsFalse(command.UnExecuteCommandCalled);

            caretaker.Undo();

            Assert.IsTrue(command.UnExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the redo stact is being cleaned when a new command is added.
        /// </summary>
        [TestMethod]
        public void ShouldNotRedoIfNewCommandWasAdded()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);

            var command = new TestableUndoableCommand();

            var anotherCommand = new TestableUndoableCommand();

            caretaker.ExecuteCommand(command);

            caretaker.Undo();

            caretaker.ExecuteCommand(anotherCommand);

            command.ExecuteCommandCalled = false;

            caretaker.Redo();
            
            Assert.IsFalse(command.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the undo stack is trimmed when the undo level limit is reached. 
        /// </summary>
        [TestMethod]
        public void ShouldTrimUndoStackIfUndoLevelLimitIsReached()
        {
            ICaretaker caretaker = new Caretaker(this.eventAggregator);
            caretaker.SetUndoLevel(1);

            var command = new TestableUndoableCommand();
            var anotherCommand = new TestableUndoableCommand();

            caretaker.ExecuteCommand(command);

            caretaker.ExecuteCommand(anotherCommand);

            caretaker.Undo();

            Assert.IsTrue(anotherCommand.UnExecuteCommandCalled);
            
            caretaker.Undo();

            Assert.IsFalse(command.UnExecuteCommandCalled);
        }

        /// <summary>
        /// Testable undoable command.
        /// </summary>
        private class TestableUndoableCommand : UndoableCommand
        {
            /// <summary>
            /// Gets or sets a value indicating whether the ExecuteCommand method was called.
            /// </summary>
            /// <value>A true if was called;otherwise false.</value>
            public bool ExecuteCommandCalled { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the UnExecuteCommand method was called.
            /// </summary>
            /// /// <value>A true if was called;otherwise false.</value>
            public bool UnExecuteCommandCalled { get; set; }

            /// <summary>
            /// Method to provide the execute logic.
            /// </summary>
            protected override void ExecuteCommand()
            {
                this.ExecuteCommandCalled = true;
            }

            /// <summary>
            /// Method to provide the unexecute logic.
            /// </summary>
            protected override void UnExecuteCommand()
            {
                this.UnExecuteCommandCalled = true;
            }
        }
    }
}
