// <copyright file="TimelineModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineModel.cs                     
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
    using System.Linq;
    using SMPTETimecode;
    
    /// <summary>
    /// A class that model a timeline.
    /// </summary>
    public class TimelineModel : BaseModel, ITimelineModel
    {
        /// <summary>
        /// Contains the tracks of the timeline.
        /// </summary>
        private readonly ObservableCollection<Track> tracks;

        /// <summary>
        /// Containes the comments of the timeline.
        /// </summary>
        private readonly ObservableCollection<Comment> commentElements;

        /// <summary>
        /// Contains the link between elements of the timeline.
        /// </summary>
        private readonly IList<TimelineElementLink> elementLinks;

        private TimeCode duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineModel"/> class.
        /// </summary>
        public TimelineModel()
        {
            this.tracks = new ObservableCollection<Track>();
            this.commentElements = new ObservableCollection<Comment>();
            this.elementLinks = new List<TimelineElementLink>();
        }

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being added to the timeline.
        /// </summary>
        public event EventHandler<TimelineElementEventArgs> ElementAdded;

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being moved to other position.
        /// </summary>
        public event EventHandler<TimelineElementEventArgs> ElementMoved;

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being removed from the timeline.
        /// </summary>
        public event EventHandler<TimelineElementEventArgs> ElementRemoved;

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is linked to other <see cref="TimelineElement"/>.
        /// </summary>
        public event EventHandler<TimelineElementEventArgs> ElementLinked;

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is unlinked to other <see cref="TimelineElement"/>.
        /// </summary>
        public event EventHandler<TimelineElementEventArgs> ElementUnlinked;

        /// <summary>
        /// Gets or sets the duration of the timeline.
        /// </summary>
        /// <value>Indicates the duration of the timeline.</value>
        public TimeCode Duration
        {
            get
            {
                return this.duration;
            }

            set
            {
                this.duration = value;
                this.OnPropertyChanged("Duration");
            }
        }

        /// <summary>
        /// Gets or sets the current position of the timeline.
        /// </summary>
        /// <value>Indicates the current position of the timeline.</value>
        public TimeCode CurrentPosition { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="Track"/>s of the timeline.
        /// </summary>
        /// <value>The collection of tracks.</value>
        public ObservableCollection<Track> Tracks
        {
            get { return this.tracks; }
        }

        /// <summary>
        /// Gets the collection of <see cref="Comment"/> of the timeline.
        /// </summary>
        /// <value>The collection of comments.</value>
        public ObservableCollection<Comment> CommentElements
        {
            get
            {
                return this.commentElements;
            }
        }

        /// <summary>
        /// Get the element within the specific range.
        /// </summary>
        /// <param name="startTimeCode">The start time to look to the element.</param>
        /// <param name="endTimeCode">The end time to look to the element.</param>
        /// <param name="layer">The track where the element should be searched.</param>
        /// <param name="elementToIgnore">The element being ignored in the search.</param>
        /// <returns>The element within the range or null.</returns>
        public TimelineElement GetElementWithinRange(TimeCode startTimeCode, TimeCode endTimeCode, Track layer, TimelineElement elementToIgnore)
        {
            foreach (var entry in layer.Shots)
            {
                var entryStart = entry.Position;
                var entryEnd = entry.Position + entry.Duration;

                if (entry == elementToIgnore)
                {
                    continue;
                }

                if (startTimeCode == entryStart || endTimeCode == entryEnd)
                {
                    return entry;
                }

                if (startTimeCode > entryStart && startTimeCode < entryEnd)
                {
                    return entry;
                }

                if (endTimeCode > entryStart && endTimeCode < entryEnd)
                {
                    return entry;
                }

                if (startTimeCode < entryStart && endTimeCode > entryEnd)
                {
                    return entry;
                }

                if (startTimeCode > entryStart && startTimeCode < entryEnd)
                {
                    return entry;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the element that is within the range of the given <paramref name="position"/>.
        /// </summary>
        /// <param name="position">The position used to find the elements.</param>
        /// <param name="layer">The track where the element is going to be searched.</param>
        /// <param name="elementToIgnore">The element being ingnored in the search.</param>
        /// <returns>The element within the position or null.</returns>
        public TimelineElement GetElementAtPosition(TimeCode position, Track layer, TimelineElement elementToIgnore)
        {
            foreach (TimelineElement entry in layer.Shots)
            {
                if (entry == elementToIgnore)
                {
                    continue;
                }

                if (entry.Position <= position && (entry.Position + entry.Duration) >= position)
                {
                    return entry;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the elements of the <paramref name="layer">track</paramref> that are in the range of the given <paramref name="position"/>.
        /// </summary>
        /// <param name="position">The position used to find the elements.</param>
        /// <param name="layer">The track where the elements are going to be searched.</param>
        /// <returns>A list of elements that are in the range of the <paramref name="position"/>.</returns>
        public IList<TimelineElement> GetElementsAtPosition(TimeCode position, Track layer)
        {
            IList<TimelineElement> elements = new List<TimelineElement>();

            foreach (TimelineElement entry in layer.Shots)
            {
                if (entry.Position <= position && (entry.Position + entry.Duration) >= position)
                {
                    elements.Add(entry);
                }
            }

            return elements;
        }

        /// <summary>
        /// Gets the element of the <paramref name="layer">track</paramref> that is before the given <paramref name="position"/>.
        /// </summary>
        /// <param name="position">The position used to find the previous element.</param>
        /// <param name="layer">The track where the elements are going to be searched.</param>
        /// <returns>The element that is before the given position.</returns>
        public TimelineElement GetPreviousElement(TimeCode position, Track layer)
        {
            TimelineElement result = null;

            foreach (TimelineElement entry in layer.Shots)
            {
                if (entry.Position <= position && position >= (entry.Position + entry.Duration))
                {
                    if (result == null || entry.Position >= (result.Position + result.Duration))
                    {
                        result = entry;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the element of the <paramref name="layer">track</paramref> that is after the given <paramref name="position"/>.
        /// </summary>
        /// <param name="position">The position used to find the next element.</param>
        /// <param name="layer">The track where the elements are going to be searched.</param>
        /// <returns>The element that is after the given position.</returns>
        public TimelineElement GetNextElement(TimeCode position, Track layer)
        {
            TimelineElement result = null;

            foreach (TimelineElement entry in layer.Shots)
            {
                if (entry.Position > position)
                {
                    if (result == null || entry.Position < (result.Position + result.Duration))
                    {
                        result = entry;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Adds the given <paramref name="element"/> to the <paramref name="layer">track</paramref>.
        /// </summary>
        /// <param name="element">The element being added.</param>
        /// <param name="layer">The track where the element is going to be added.</param>
        public void AddElement(TimelineElement element, Track layer)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (element.Id == Guid.Empty)
            {
                throw new ArgumentException("The element Id cannot be empty.", "element");
            }

            TimelineElement prevElement = null;
            foreach (TimelineElement entry in layer.Shots)
            {
                if (entry.Position < element.Position)
                {
                    prevElement = entry;
                }
            }

            if (prevElement == null)
            {
                layer.Shots.Insert(0, element);
            }
            else
            {
                layer.Shots.Insert(layer.Shots.IndexOf(prevElement) + 1, element);
            }

            this.OnElementAdded(element);
        }

        /// <summary>
        /// Moves the given <paramref name="element"/> to a <paramref name="newPosition"/>.
        /// </summary>
        /// <param name="element">The element being moved.</param>
        /// <param name="layer">The track where the element belongs.</param>
        /// <param name="newPosition">The new position of the element.</param>
        public void MoveElement(TimelineElement element, Track layer, TimeCode newPosition)
        {
            element.Position = newPosition;

            layer.Shots.Sort(CompareElements);

            this.OnElementMoved(element);
        }

        /// <summary>
        /// Removes the given <paramref name="element"/> from <paramref name="layer">track</paramref>.
        /// </summary>
        /// <param name="element">The element being removed.</param>
        /// <param name="layer">The track from where the element is being removed.</param>
        public void RemoveElement(TimelineElement element, Track layer)
        {
            if (layer.Shots.Contains(element))
            {
                layer.Shots.Remove(element);
                this.UnlinkElement(element);
                this.OnElementRemoved(element);
            }
        }

        /// <summary>
        /// Adds the given <paramref name="comment"/> to the comment collection.
        /// </summary>
        /// <param name="comment">The comment being added.</param>
        public void AddComment(Comment comment)
        {
            Comment prevElement = null;
            foreach (var entry in this.commentElements)
            {
                if (entry.MarkIn < comment.MarkIn)
                {
                    prevElement = entry;
                }
            }

            if (prevElement == null)
            {
                this.commentElements.Insert(0, comment);
            }
            else
            {
                this.commentElements.Insert(this.commentElements.IndexOf(prevElement) + 1, comment);
            }
        }

        /// <summary>
        /// Tries to link the given <paramref name="element"/> to a next element in the timeline.
        /// </summary>
        /// <param name="element">The element being linked.</param>
        /// <param name="nextElement">The next element being linked to the element.</param>
        /// <returns>True if the link is effective;otherwise false.</returns>
        public bool LinkNextElement(TimelineElement element, TimelineElement nextElement)
        {
            if (element.Position + element.Duration != nextElement.Position)
            {
                // Elements are not in the correct position
                return false;
            }

            var link = this.elementLinks.Where(l => l.ElementId == element.Id).SingleOrDefault();
            if (link == null)
            {
                link = new TimelineElementLink(element.Id);
                this.elementLinks.Add(link);
            }

            link.NextElementId = nextElement.Id;

            link = this.elementLinks.Where(l => l.ElementId == nextElement.Id).SingleOrDefault();
            if (link == null)
            {
                link = new TimelineElementLink(nextElement.Id);
                this.elementLinks.Add(link);
            }

            link.PreviousElementId = element.Id;

            this.OnElementLinked(element);

            return true;
        }

        /// <summary>
        /// Tries to link the given <paramref name="element"/> to a previous element in the timeline.
        /// </summary>
        /// <param name="element">The element being linked.</param>
        /// <param name="previousElement">The previous element being linked to the element.</param>
        /// <returns>True if the link is effective;otherwise false.</returns>
        public bool LinkPreviousElement(TimelineElement element, TimelineElement previousElement)
        {
            return this.LinkNextElement(previousElement, element);
        }

        /// <summary>
        /// Clean the links between the given elements.
        /// </summary>
        /// <param name="elementA">The element being unlinked of the <paramref name="elementB"/>.</param>
        /// <param name="elementB">The element being unlinked of the <paramref name="elementA"/>.</param>
        public void UnlinkElements(TimelineElement elementA, TimelineElement elementB)
        {
            this.UnlinkElementImpl(elementA, elementB);
            this.UnlinkElementImpl(elementB, elementA);
            this.CleanEmptyLinks();

            this.OnElementUnlinked(elementA);
        }

        /// <summary>
        /// Removes any link that the given <paramref name="element"/> contains.
        /// </summary>
        /// <param name="element">The element to clean the links.</param>
        public void UnlinkElement(TimelineElement element)
        {
            TimelineElementLink link = this.elementLinks.Where(l => l.ElementId == element.Id).SingleOrDefault();
            if (link != null)
            {
                this.elementLinks.Remove(link);
                
                link = this.elementLinks.Where(l => l.NextElementId == element.Id).SingleOrDefault();
                
                if (link != null)
                {
                    link.NextElementId = Guid.Empty;
                }
                
                link = this.elementLinks.Where(l => l.PreviousElementId == element.Id).SingleOrDefault();
                
                if (link != null)
                {
                    link.PreviousElementId = Guid.Empty;
                }

                this.CleanEmptyLinks();
            }
        }

        /// <summary>
        /// Determines if two elements are linked or not.
        /// </summary>
        /// <param name="element">The element to verify is linked to other element.</param>
        /// <param name="linkedElement">The linked element to verify if is linked to the element.</param>
        /// <returns>True if the elements are linked;otherwise false.</returns>
        public bool IsElementLinkedTo(TimelineElement element, TimelineElement linkedElement)
        {
            if (element == null || linkedElement == null)
            {
                return false;
            }

            TimelineElementLink link = this.GetElementLink(element);

            return link.PreviousElementId == linkedElement.Id || link.NextElementId == linkedElement.Id;
        }

        /// <summary>
        /// Looks to the last element linked starting from the given <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element to start the search.</param>
        /// <param name="layer">The track where the element is being searched.</param>
        /// <returns>The last element linked.</returns>
        public TimelineElement FindLastElementLinking(TimelineElement element, Track layer)
        {
            TimelineElement lastElement = element;

            TimelineElementLink link = this.GetElementLink(element);

            TimelineElement linkedElement = layer.Shots.Where(e => e.Id == link.NextElementId).SingleOrDefault();

            while (linkedElement != null)
            {
                lastElement = linkedElement;
                link = this.GetElementLink(linkedElement);
                linkedElement = layer.Shots.Where(e => e.Id == link.NextElementId).SingleOrDefault();
            }

            return lastElement;
        }

        /// <summary>
        /// Looks to the first element linked starting from the given <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element to start the search.</param>
        /// <param name="layer">The track where the element is being searched.</param>
        /// <returns>The first element linked.</returns>
        public TimelineElement FindFirstElementLinking(TimelineElement element, Track layer)
        {
            TimelineElement firstElement = element;

            TimelineElementLink link = this.GetElementLink(element);

            TimelineElement linkedElement = layer.Shots.Where(e => e.Id == link.PreviousElementId).SingleOrDefault();

            while (linkedElement != null)
            {
                firstElement = linkedElement;
                link = this.GetElementLink(linkedElement);
                linkedElement = layer.Shots.Where(e => e.Id == link.PreviousElementId).SingleOrDefault();
            }

            return firstElement;
        }

        /// <summary>
        /// Gets the <see cref="TimelineElementLink"/> of the element containing the previous and next element linked
        /// to the given <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The element to retrieve the links.</param>
        /// <returns>The links of the element.</returns>
        public TimelineElementLink GetElementLink(TimelineElement element)
        {
            var link = this.elementLinks.Where(l => l.ElementId == element.Id).SingleOrDefault();
            if (link != null)
            {
                return link;
            }

            return new TimelineElementLink(element.Id);
        }

        /// <summary>
        /// Adds the given <paramref name="track"/> to the tracks collection.
        /// </summary>
        /// <param name="track">The track being added.</param>
        public void AddTrack(Track track)
        {
            this.Tracks.Add(track);
        }

        /// <summary>
        /// Compares the position of the given elements.
        /// </summary>
        /// <param name="element1">The first element.</param>
        /// <param name="element2">The second element.</param>
        /// <returns>Returns a value indicating which element should be first.</returns>
        private static int CompareElements(TimelineElement element1, TimelineElement element2)
        {
            return element1.Position.CompareTo(element2.Position);
        }

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being added to the timeline.
        /// </summary>
        /// <param name="timelineElement">The element being added.</param>
        private void OnElementAdded(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementAddedHandler = this.ElementAdded;
            if (elementAddedHandler != null)
            {
                elementAddedHandler(this, new TimelineElementEventArgs(timelineElement));
            }
        }

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being removed from the timeline.
        /// </summary>
        /// <param name="timelineElement">The element being removed.</param>
        private void OnElementRemoved(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementRemovedHandler = this.ElementRemoved;
            if (elementRemovedHandler != null)
            {
                elementRemovedHandler(this, new TimelineElementEventArgs(timelineElement));
            }
        }

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is being moved to other position.
        /// </summary>
        /// <param name="timelineElement">The element being moved.</param>
        private void OnElementMoved(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementMovedHandler = this.ElementMoved;
            if (elementMovedHandler != null)
            {
                elementMovedHandler(this, new TimelineElementEventArgs(timelineElement));
            }
        }

        /// <summary>
        /// Cleans the link between the given elements.
        /// </summary>
        /// <param name="elementA">The element to clean the links to <paramref name="elementB"/>.</param>
        /// <param name="elementB">The element to clean the links to <paramref name="elementA"/>.</param>
        private void UnlinkElementImpl(TimelineElement elementA, TimelineElement elementB)
        {
            var link = this.elementLinks.Where(l => l.ElementId == elementA.Id).SingleOrDefault();

            if (link != null)
            {
                if (link.NextElementId == elementB.Id)
                {
                    link.NextElementId = Guid.Empty;
                }

                if (link.PreviousElementId == elementB.Id)
                {
                    link.PreviousElementId = Guid.Empty;
                }
            }
        }

        /// <summary>
        /// Cleans any orphan link.
        /// </summary>
        private void CleanEmptyLinks()
        {
            var emptyLinks = this.elementLinks.Where(l => l.NextElementId == Guid.Empty && l.PreviousElementId == Guid.Empty).ToArray();
            
            foreach (var link in emptyLinks)
            {
                this.elementLinks.Remove(link);
            }
        }

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is linked to an element.
        /// </summary>
        /// <param name="element">The element being linked.</param>
        private void OnElementLinked(TimelineElement element)
        {
            EventHandler<TimelineElementEventArgs> elementLinkedHandler = this.ElementLinked;
            if (elementLinkedHandler != null)
            {
                elementLinkedHandler(this, new TimelineElementEventArgs(element));
            }
        }

        /// <summary>
        /// Occurs when a <see cref="TimelineElement"/> is unlinked from an element.
        /// </summary>
        /// <param name="element">The element being unlinked.</param>
        private void OnElementUnlinked(TimelineElement element)
        {
            EventHandler<TimelineElementEventArgs> elementUnlinkedHandler = this.ElementUnlinked;
            if (elementUnlinkedHandler != null)
            {
                elementUnlinkedHandler(this, new TimelineElementEventArgs(element));
            }
        }
    }
}