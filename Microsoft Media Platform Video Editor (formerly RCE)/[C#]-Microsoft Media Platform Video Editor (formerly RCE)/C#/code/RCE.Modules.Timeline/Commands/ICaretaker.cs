// <copyright file="ICaretaker.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ICaretaker.cs                     
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
    /// Provides an interface for undo/redo operations.
    /// </summary>
    public interface ICaretaker
    {
        /// <summary>
        /// Sets the maximum undo level allowed by the application.
        /// </summary>
        /// <param name="undoLevel">The numbers of undo levels.</param>
        void SetUndoLevel(int undoLevel);

        /// <summary>
        /// Executes the <see cref="UndoableCommand"/> passed.
        /// </summary>
        /// <param name="command">The command beign executed.</param>
        void ExecuteCommand(UndoableCommand command);

        /// <summary>
        /// Undo the last operation added.
        /// </summary>
        void Undo();

        /// <summary>
        /// Redo the last undo operation.
        /// </summary>
        void Redo();
    }
}