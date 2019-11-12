// <copyright file="CompositeUndoableCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CompositeUndoableCommand.cs                     
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
    using System;
    using System.Collections.Generic;

    public class CompositeUndoableCommand<T> : UndoableCommand where T : UndoableCommand
    {
        /// <summary>
        /// The collection of <see cref="undoableCommands"/> being executed.
        /// </summary>
        private readonly IList<T> undoableCommands;

        public CompositeUndoableCommand()
            : this(new List<T>())
        {
        }

        public CompositeUndoableCommand(IList<T> undoableCommands)
        {
            this.undoableCommands = undoableCommands;
        }

        public void AddCommand(T command)
        {
            this.undoableCommands.Add(command);
        }

        public void RemoveCommand(T command)
        {
            this.undoableCommands.Remove(command);
        }

        /// <summary>
        /// Executes the <see cref="T"/>s.
        /// </summary>
        protected override void ExecuteCommand()
        {
            foreach (T undoableCommand in this.undoableCommands)
            {
                undoableCommand.Execute();
            }
        }

        /// <summary>
        /// UnExecutes the <see cref="T"/>s.
        /// </summary>
        protected override void UnExecuteCommand()
        {
            foreach (T undoableCommand in this.undoableCommands)
            {
                undoableCommand.UnExecute();
            }
        }
    }
}
