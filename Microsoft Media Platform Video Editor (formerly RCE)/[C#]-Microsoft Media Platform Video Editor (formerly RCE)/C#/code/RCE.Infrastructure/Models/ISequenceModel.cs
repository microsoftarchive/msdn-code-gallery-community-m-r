// <copyright file="ISequenceModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ISequenceModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>
namespace RCE.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using RCE.Services.Contracts;
    using SMPTETimecode;

    /// <summary>
    /// Defines an interface for a timeline model.
    /// </summary>
    public interface ISequenceModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being added to the timeline.
        /// </summary>
        event EventHandler<TimelineElementEventArgs> ElementAdded;

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being moved to other position.
        /// </summary>
        event EventHandler<TimelineElementEventArgs> ElementMoved;

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being removed from the timeline.
        /// </summary>
        event EventHandler<TimelineElementEventArgs> ElementRemoved;

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is linked to other <see cref="TimelineElement"/>.
        /// </summary>
        event EventHandler<TimelineElementEventArgs> ElementLinked;

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is unlinked to other <see cref="TimelineElement"/>.
        /// </summary>
        event EventHandler<TimelineElementEventArgs> ElementUnlinked;
        
        /// <summary>
        /// Gets or sets the duration of the timeline.
        /// </summary>
        /// <value>Indicates the duration of the timeline.</value>
        TimeCode Duration { get; set; }

        /// <summary>
        /// Gets or sets the current position of the timeline.
        /// </summary>
        /// <value>Indicates the current position of the timeline.</value>
        TimeCode CurrentPosition { get; set; }
        
        /// <summary>
        /// Gets the collection of <see cref="Track"/>s of the timeline.
        /// </summary>
        /// <value>The collection of tracks.</value>
        ObservableCollection<Track> Tracks { get; }

        /// <summary>
        /// Returns true if the sequence has any track that has gaps
        /// </summary>
        /// <value>True if the sequence has at least one track with a gap.</value>
        bool SequenceHasGap();

        /// <summary>
        /// Get the element within the specific range.
        /// </summary>
        /// <param name="startTimeCode">The start time to look to the element.</param>
        /// <param name="endTimeCode">The end time to look to the element.</param>
        /// <param name="layer">The track where the element should be searched.</param>
        /// <param name="elementToIgnore">The element being ignored in the search.</param>
        /// <returns>The element within the range or null.</returns>
        TimelineElement GetElementWithinRange(TimeCode startTimeCode, TimeCode endTimeCode, Track layer, TimelineElement elementToIgnore);

        /// <summary>
        /// Get the element that is within the range of the given <paramref name="position"/>.
        /// </summary>
        /// <param name="position">The position used to find the elements.</param>
        /// <param name="layer">The track where the element is going to be searched.</param>
        /// <param name="elementToIgnore">The element being ingnored in the search.</param>
        /// <returns>The element within the position or null.</returns>
        TimelineElement GetElementAtPosition(TimeCode position, Track layer, TimelineElement elementToIgnore);

        /// <summary>
        /// Gets the elements of the <paramref name="layer">track</paramref> that are within the range of the given <paramref name="position"/>.
        /// </summary>
        /// <param name="position">The position used to find the elements.</param>
        /// <param name="layer">The track where the elements are going to be searched.</param>
        /// <returns>A list of elements that are in the range of the <paramref name="position"/>.</returns>
        IList<TimelineElement> GetElementsAtPosition(TimeCode position, Track layer);

        /// <summary>
        /// Adds the given <paramref name="element"/> to the <paramref name="layer">track</paramref>.
        /// </summary>
        /// <param name="element">The element being added.</param>
        /// <param name="layer">The track where the element is going to be added.</param>
        void AddElement(TimelineElement element, Track layer);

        /// <summary>
        /// Moves the given <paramref name="element"/> to a <paramref name="newPosition"/>.
        /// </summary>
        /// <param name="element">The element being moved.</param>
        /// <param name="layer">The track where the element belongs.</param>
        /// <param name="newPosition">The new position of the element.</param>
        void MoveElement(TimelineElement element, Track layer, TimeCode newPosition);

        /// <summary>
        /// Removes the given <paramref name="element"/> from <paramref name="layer">track</paramref>.
        /// </summary>
        /// <param name="element">The element being removed.</param>
        /// <param name="layer">The track from where the element is being removed.</param>
        void RemoveElement(TimelineElement element, Track layer);
        
        /// <summary>
        /// Gets the <see cref="TimelineElementLink"/> of the element containing the previous and next element linked
        /// to the given <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element to retrieve the links.</param>
        /// <returns>The links of the element.</returns>
        TimelineElementLink GetElementLink(TimelineElement element);

        /// <summary>
        /// Tries to link the given <paramref name="element"/> to a next element in the timeline.
        /// </summary>
        /// <param name="element">The element being linked.</param>
        /// <param name="nextElement">The next element being linked to the element.</param>
        /// <returns>True if the link is effective;otherwise false.</returns>
        bool LinkNextElement(TimelineElement element, TimelineElement nextElement);

        /// <summary>
        /// Tries to link the given <paramref name="element"/> to a previous element in the timeline.
        /// </summary>
        /// <param name="element">The element being linked.</param>
        /// <param name="previousElement">The previous element being linked to the element.</param>
        /// <returns>True if the link is effective;otherwise false.</returns>
        bool LinkPreviousElement(TimelineElement element, TimelineElement previousElement);

        /// <summary>
        /// Clean the links between the given elements.
        /// </summary>
        /// <param name="elementA">The element being unlinked of the <paramref name="elementB"/>.</param>
        /// <param name="elementB">The element being unlinked of the <paramref name="elementA"/>.</param>
        void UnlinkElements(TimelineElement elementA, TimelineElement elementB);

        /// <summary>
        /// Removes any link that the given <paramref name="element"/> contains.
        /// </summary>
        /// <param name="element">The element to clean the links.</param>
        void UnlinkElement(TimelineElement element);

        /// <summary>
        /// Determines if two elements are linked or not.
        /// </summary>
        /// <param name="element">The element to verify is linked to other element.</param>
        /// <param name="linkedElement">The linked element to verify if is linked to the element.</param>
        /// <returns>True if the elements are linked;otherwise false.</returns>
        bool IsElementLinkedTo(TimelineElement element, TimelineElement linkedElement);

        /// <summary>
        /// Looks to the last element linked starting from the given <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element to start the search.</param>
        /// <param name="layer">The track where the element is being searched.</param>
        /// <returns>The last element linked.</returns>
        TimelineElement FindLastElementLinking(TimelineElement element, Track layer);

        /// <summary>
        /// Looks to the first element linked starting from the given <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element to start the search.</param>
        /// <param name="layer">The track where the element is being searched.</param>
        /// <returns>The first element linked.</returns>
        TimelineElement FindFirstElementLinking(TimelineElement element, Track layer);

        /// <summary>
        /// Adds the given <paramref name="track"/> to the tracks collection.
        /// </summary>
        /// <param name="track">The track being added.</param>
        void AddTrack(Track track);

        /// <summary>
        /// Gets the previous element of the given position.
        /// </summary>
        /// <param name="position">The position used to find the previous element.</param>
        /// <param name="layer">The layer to look for the previous element.</param>
        /// <returns>The element if exists;otherwise null.</returns>
        TimelineElement GetPreviousElement(TimeCode position, Track layer);

        /// <summary>
        /// Gets the next element of the given position.
        /// </summary>
        /// <param name="position">The position used to find the next element.</param>
        /// <param name="layer">The layer to look for the next element.</param>
        /// <returns>The element if exists;otherwise null.</returns>
        TimelineElement GetNextElement(TimeCode position, Track layer);
    }
}