// <copyright file="IUndoableCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IUndoableCommand.cs                     
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
    /// Provides an interface for commands with support for undo/redo.
    /// </summary>
    public interface IUndoableCommand
    {
        /// <summary>
        /// Gets a value indicating whether a command was executed or not.
        /// </summary>
        /// <value>A <seealso cref="bool"/> value that indicates if the command was executed or not. true if was executed;false if not.</value>
        bool WasExecuted { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// UnExecutes the command.
        /// </summary>
        void UnExecute();
    }
}