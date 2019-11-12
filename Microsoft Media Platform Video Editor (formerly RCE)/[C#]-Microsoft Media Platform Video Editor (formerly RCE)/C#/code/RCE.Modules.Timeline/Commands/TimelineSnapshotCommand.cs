  // <copyright file="TimelineSnapshotCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineSnapshotCommand.cs                     
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

    /// <summary>
    /// A class that provides an implementation of the <seealso cref="UndoableCommand"/> class to execute <seealso cref="LayerSnapshotCommand"/>s.
    /// </summary>
    public class TimelineSnapshotCommand : UndoableCommand
    {
        /// <summary>
        /// The collection of <see cref="layerSnapshotCommands"/> being executed.
        /// </summary>
        private readonly IList<LayerSnapshotCommand> layerSnapshotCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineSnapshotCommand"/> class.
        /// </summary>
        /// <param name="layerSnapshotCommands">The collection of <see cref="layerSnapshotCommands"/> being executed.</param>
        public TimelineSnapshotCommand(IList<LayerSnapshotCommand> layerSnapshotCommands)
        {
            this.layerSnapshotCommands = layerSnapshotCommands;
        }

        /// <summary>
        /// Executes the <see cref="LayerSnapshotCommand"/>s.
        /// </summary>
        protected override void ExecuteCommand()
        {
            foreach (LayerSnapshotCommand layerSnapshotCommand in this.layerSnapshotCommands)
            {
                layerSnapshotCommand.Execute();
            }
        }

        /// <summary>
        /// UnExecutes the <see cref="LayerSnapshotCommand"/>s.
        /// </summary>
        protected override void UnExecuteCommand()
        {
            foreach (LayerSnapshotCommand layerSnapshotCommand in this.layerSnapshotCommands)
            {
                layerSnapshotCommand.UnExecute();
            }
        }
    }
}
