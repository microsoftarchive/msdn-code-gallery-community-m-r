// <copyright file="UndoableCommandFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UndoableCommandFixture.cs                     
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
    using Timeline.Commands;

    /// <summary>
    /// A class for testing the <see cref="UndoableCommand"/>.
    /// </summary>
    [TestClass]
    public class UndoableCommandFixture
    {
        /// <summary>
        /// Tests that the ExecuteCommand method should be called when executing the command.
        /// </summary>
        [TestMethod]
        public void ShouldCallExecuteCommandWhenExecuting()
        {
            var command = new TestableUndoableCommand();

            Assert.IsFalse(command.ExecuteCommandCalled);

            command.Execute();

            Assert.IsTrue(command.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the UnExecuteCommand method should be called when unexecuting the command.
        /// </summary>
        [TestMethod]
        public void ShouldCallUnExecuteCommandWhenUnExecuting()
        {
            var command = new TestableUndoableCommand();

            command.Execute();

            Assert.IsFalse(command.UnExecuteCommandCalled);

            command.UnExecute();

            Assert.IsTrue(command.UnExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the command should not call to ExecuteCommand when executing if was already executed.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallExecuteCommandIfCommandWasExecuted()
        {
            var command = new TestableUndoableCommand();

            Assert.IsFalse(command.ExecuteCommandCalled);

            command.Execute();

            Assert.IsTrue(command.ExecuteCommandCalled);

            command.ExecuteCommandCalled = false;

            command.Execute();

            Assert.IsFalse(command.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the command should noot call to UnExecute command when unexecuting if command was not already executed.
        /// </summary>
        [TestMethod]
        public void ShouldNotCallUnExecuteCommandIfCommandWasNotExecuted()
        {
            var command = new TestableUndoableCommand();

            Assert.IsFalse(command.UnExecuteCommandCalled);

            command.UnExecute();

            Assert.IsFalse(command.ExecuteCommandCalled);
        }

        /// <summary>
        /// Tests that the command is marked as executed after executing it.
        /// </summary>
        [TestMethod]
        public void ShouldMarkCommandAsExecutedWhenExecuting()
        {
            var command = new TestableUndoableCommand();

            Assert.IsFalse(command.WasExecuted);

            command.Execute();

            Assert.IsTrue(command.WasExecuted);
        }

        /// <summary>
        /// Tests that the command is unmarked as executed after unexecuting it.
        /// </summary>
        [TestMethod]
        public void ShouldUnMarkCommandAsExecutedWhenUnExecuting()
        {
            var command = new TestableUndoableCommand();

            command.Execute();

            Assert.IsTrue(command.WasExecuted);

            command.UnExecute();

            Assert.IsFalse(command.WasExecuted);
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
