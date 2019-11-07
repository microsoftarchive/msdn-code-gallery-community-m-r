// <copyright file="RemoveElementCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: RemoveElementCommand.cs                     
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
    using Infrastructure.Models;
    using SMPTETimecode;

    /// <summary>
    /// A class that provides an implementation of the <seealso cref="UndoableCommand"/> class to remove elements from the timeline model.
    /// </summary>
    public class RemoveElementCommand : UndoableCommand
    {
        /// <summary>
        /// The <seealso cref="ISequenceModel"/> instance where the element is being removed.
        /// </summary>
        private readonly ISequenceModel sequenceModel;
        
        /// <summary>
        /// The <seealso cref="Track"/> where the elements belongs.
        /// </summary>
        private readonly Track layer;

        /// <summary>
        /// The <seealso cref="EditMode"/> used to determine what operations should be performend during the execution of the command.
        /// </summary>
        private readonly EditMode editMode;

        /// <summary>
        /// The <seealso cref="TimelineElement"/> being added.
        /// </summary>
        private readonly TimelineElement timelineElement;

        /// <summary>
        /// The state of the elements being removed.
        /// </summary>
        private readonly IList<TimelineElement> memento;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveElementCommand"/> class.
        /// </summary>
        /// <param name="sequenceModel">The timeline model.</param>
        /// <param name="layer">The layer where the element is being removed.</param>
        /// <param name="editMode">The current <see cref="EditMode"/>.</param>
        /// <param name="timelineElement">The element being removed.</param>
        public RemoveElementCommand(ISequenceModel sequenceModel, Track layer, EditMode editMode, TimelineElement timelineElement)
        {
            this.sequenceModel = sequenceModel;
            this.layer = layer;
            this.editMode = editMode;
            this.timelineElement = timelineElement;
            this.memento = new List<TimelineElement>();
        }

        /// <summary>
        /// Removes a <see cref="TimelineElement"/> from the timeline model. If edit mode is <see cref="EditMode.Ripple"/> should move any adjacent elements.
        /// </summary>
        protected override void ExecuteCommand()
        {
            TimelineElement currentElement = this.timelineElement;
            this.sequenceModel.RemoveElement(currentElement, this.layer);

            TimelineElement previousElement = this.sequenceModel.GetElementAtPosition(currentElement.Position, this.layer, currentElement);
           
            if (this.editMode == EditMode.Ripple && previousElement != null)
            {
                // RIPPLE MODE, move any adjacent elements
                TimeCode offset = currentElement.Duration;
                TimelineElement nextElement = this.sequenceModel.GetElementAtPosition(currentElement.Position + currentElement.Duration, this.layer, currentElement) ??
                                              this.sequenceModel.GetElementWithinRange(currentElement.Position + currentElement.Duration, currentElement.Position + currentElement.Duration, this.layer, currentElement);

                // Move next elements backward
                while (nextElement != null)
                {
                    TimeCode previousPosition = nextElement.Position;
                    this.memento.Add(nextElement.GetMemento());
                    this.sequenceModel.MoveElement(nextElement, this.layer, TimeCode.FromAbsoluteTime(nextElement.Position.TotalSeconds - offset.TotalSeconds, nextElement.Position.FrameRate));
                    currentElement = nextElement;
                    nextElement = this.sequenceModel.GetElementAtPosition(previousPosition + currentElement.Duration, this.layer, currentElement);
                }
            }
        }

        /// <summary>
        /// Adds a <see cref="TimelineElement"/> to the timeline model.
        /// </summary>
        protected override void UnExecuteCommand()
        {
            this.memento.Reverse();

            foreach (TimelineElement element in this.memento)
            {
                TimelineElement originator = this.layer.Shots.Where(x => x.Id == element.Id).FirstOrDefault();

                if (originator != null)
                {
                    originator.SetMemento(element);
                    this.sequenceModel.MoveElement(originator, this.layer, originator.Position);
                }
            }

            this.sequenceModel.AddElement(this.timelineElement, this.layer);

            this.memento.Clear();
        }
    }
}
