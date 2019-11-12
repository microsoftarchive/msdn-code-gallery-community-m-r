// <copyright file="Caretaker.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Caretaker.cs                     
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
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Practices.Composite.Events;
    using RCE.Infrastructure.Events;

    /// <summary>
    /// A class that provides the implementation for the <see cref="ICaretaker"/> interface.
    /// </summary>
    public class Caretaker : ICaretaker
    {
        /// <summary>
        /// Defines the default undo level.
        /// </summary>
        private const int DefaultUndoLevel = 20;

        /// <summary>
        /// IEventAggregator instance, for publishing pushing and poping operations.
        /// </summary>
        private readonly IEventAggregator eventManager;

        /// <summary>
        /// Contains a list of commands for a redo operation.
        /// </summary>
        private readonly Stack<UndoableCommand> redoStack;

        /// <summary>
        /// Cintains a list of commands for an undo operation.
        /// </summary>
        private Stack<UndoableCommand> undoStack;

        /// <summary>
        /// Contains the current undo level.
        /// </summary>
        private int currentUndoLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Caretaker"/> class.
        /// </summary>
        public Caretaker(IEventAggregator eventAggregator)
        {
            this.undoStack = new Stack<UndoableCommand>();
            this.redoStack = new Stack<UndoableCommand>();
            this.currentUndoLevel = DefaultUndoLevel;
            this.eventManager = eventAggregator;
        }

        /// <summary>
        /// Sets the maximum undo level allowed by the application.
        /// </summary>
        /// <param name="undoLevel">The numbers of undo levels.</param>
        public void SetUndoLevel(int undoLevel)
        {
            this.currentUndoLevel = undoLevel;
        }

        /// <summary>
        /// Executes the <see cref="UndoableCommand"/> passed.
        /// </summary>
        /// <param name="command">The command beign executed.</param>
        public void ExecuteCommand(UndoableCommand command)
        {
            command.Execute();

            if (this.undoStack.Count == this.currentUndoLevel)
            {
                this.undoStack = new Stack<UndoableCommand>(this.undoStack.Take(this.undoStack.Count - 1));
            }

            this.undoStack.Push(command);
            this.eventManager.GetEvent<OperationExecutedInTimelineEvent>().Publish(null);
            this.redoStack.Clear();
        }

        /// <summary>
        /// Undo the last operation added.
        /// </summary>
        public void Undo()
        {
            if (this.undoStack.Count > 0)
            {
                UndoableCommand command = this.undoStack.Pop();

                command.UnExecute();

                this.redoStack.Push(command);
                this.eventManager.GetEvent<OperationUndoneInTimelineEvent>().Publish(null);
            }
        }

        /// <summary>
        /// Redo the last undo operation.
        /// </summary>
        public void Redo()
        {
            if (this.redoStack.Count > 0)
            {
                UndoableCommand command = this.redoStack.Pop();

                command.Execute();

                this.undoStack.Push(command);
                this.eventManager.GetEvent<OperationExecutedInTimelineEvent>().Publish(null);
            }
        }
    }
}
