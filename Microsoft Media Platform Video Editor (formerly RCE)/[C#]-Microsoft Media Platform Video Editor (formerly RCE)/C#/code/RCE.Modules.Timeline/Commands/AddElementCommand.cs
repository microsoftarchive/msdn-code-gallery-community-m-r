// <copyright file="AddElementCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AddElementCommand.cs                     
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
    using Infrastructure.Models;

    /// <summary>
    /// A class that provides an implementation of the <seealso cref="UndoableCommand"/> class to add elements to the timeline model.
    /// </summary>
    public class AddElementCommand : UndoableCommand
    {
        /// <summary>
        /// The <seealso cref="ISequenceModel"/> instance where the element is being added.
        /// </summary>
        private readonly ISequenceModel sequenceModel;

        /// <summary>
        /// The <seealso cref="Track"/> where the elements belongs.
        /// </summary>
        private readonly Track layer;

        /// <summary>
        /// The <seealso cref="TimelineElement"/> being added.
        /// </summary>
        private readonly TimelineElement element;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddElementCommand"/> class.
        /// </summary>
        /// <param name="sequenceModel">The timeline model.</param>
        /// <param name="layer">The layer where the element is beign added.</param>
        /// <param name="element">The element beign added.</param>
        public AddElementCommand(ISequenceModel sequenceModel, Track layer, TimelineElement element)
        {
            this.sequenceModel = sequenceModel;
            this.layer = layer;
            this.element = element;
        }

        /// <summary>
        /// Adds an <seealso cref="TimelineElement"/> to the timeline model.
        /// </summary>
        protected override void ExecuteCommand()
        {
            this.sequenceModel.AddElement(this.element, this.layer);
        }

        /// <summary>
        /// Removes an <seealso cref="TimelineElement"/> from the timeline model.
        /// </summary>
        protected override void UnExecuteCommand()
        {
            this.sequenceModel.RemoveElement(this.element, this.layer);
        }
    }
}
