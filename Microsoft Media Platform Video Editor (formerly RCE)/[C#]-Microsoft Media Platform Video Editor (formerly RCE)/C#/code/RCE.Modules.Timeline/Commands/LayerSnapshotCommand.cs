// <copyright file="LayerSnapshotCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LayerSnapshotCommand.cs                     
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
    using Infrastructure.Models;

    /// <summary>
    /// A class that provides an implementation of the <seealso cref="UndoableCommand"/> class that takes a layer snapshot.
    /// </summary>
    public class LayerSnapshotCommand : UndoableCommand
    {
        /// <summary>
        /// The <seealso cref="ISequenceModel"/> instance to get a layer snapshot.
        /// </summary>
        private readonly ISequenceModel sequenceModel;

        /// <summary>
        /// The <seealso cref="Track"/> from where an snapshot is going to be taken.
        /// </summary>
        private readonly Track layer;

        /// <summary>
        /// The current state of the layer.
        /// </summary>
        private readonly IList<TimelineElement> currentMemento;

        /// <summary>
        /// The new state of the layer.
        /// </summary>
        private readonly IList<TimelineElement> newMemento;

        /// <summary>
        /// Initializes a new instance of the <see cref="LayerSnapshotCommand"/> class.
        /// </summary>
        /// <param name="sequenceModel">The timeline model.</param>
        /// <param name="layer">The layer where the snapshot is being took.</param>
        /// <param name="currentMemento">The current state of the layer.</param>
        /// <param name="newMemento">The new state of the layer.</param>
        public LayerSnapshotCommand(ISequenceModel sequenceModel, Track layer, IList<TimelineElement> currentMemento, IList<TimelineElement> newMemento)
        {
            this.sequenceModel = sequenceModel;
            this.layer = layer;
            this.currentMemento = currentMemento;
            this.newMemento = newMemento;
        }

        /// <summary>
        /// Sets the new memento to the layer.
        /// </summary>
        protected override void ExecuteCommand()
        {
            this.layer.SetMemento(this.newMemento);
            this.MoveLayerElements();
        }
        
        /// <summary>
        /// Sets the current memento to the layer.
        /// </summary>
        protected override void UnExecuteCommand()
        {
            this.layer.SetMemento(this.currentMemento);
            this.MoveLayerElements();
        }

        /// <summary>
        /// Moves the elements of the layer.
        /// </summary>
        private void MoveLayerElements()
        {
            foreach (TimelineElement element in this.layer.Shots)
            {
                this.sequenceModel.MoveElement(element, this.layer, element.Position);
            }
        }
    }
}
