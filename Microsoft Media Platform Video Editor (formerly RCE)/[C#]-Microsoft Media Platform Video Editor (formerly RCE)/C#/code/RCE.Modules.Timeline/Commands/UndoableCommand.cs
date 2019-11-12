// <copyright file="UndoableCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: UndoableCommand.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Commands
{
    /// <summary>
    /// A class that provides the implementation for the <see cref="IUndoableCommand"/> interface and should be used to create undoable commands.
    /// </summary>
    public abstract class UndoableCommand : IUndoableCommand
    {
        /// <summary>
        /// Gets a value indicating whether a command was executed or not.
        /// </summary>
        /// <value>A <seealso cref="bool"/> value that indicates if the command was executed or not. true if was executed;false if not.</value>
        public bool WasExecuted { get; private set; }

        /// <summary>
        /// Executes the command if was not already executed.
        /// </summary>
        public void Execute()
        {
            if (!this.WasExecuted)
            {
                this.WasExecuted = true;
                this.ExecuteCommand();
            }
        }

        /// <summary>
        /// UnExecutes the command if was already executed.
        /// </summary>
        public void UnExecute()
        {
            if (this.WasExecuted)
            {
                this.WasExecuted = false;
                this.UnExecuteCommand();
            }
        }

        /// <summary>
        /// Method to provide the execute logic.
        /// </summary>
        protected abstract void ExecuteCommand();

        /// <summary>
        /// Method to provide the unexecute logic.
        /// </summary>
        protected abstract void UnExecuteCommand();
    }
}
