// <copyright file="MockTimelineModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockTimelineModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Infrastructure.Models;
    using SMPTETimecode;

    public class MockTimelineModel : ITimelineModel
    {
        public MockTimelineModel()
        {
            this.Tracks = new ObservableCollection<Track>();
            this.CommentElements = new ObservableCollection<Comment>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<TimelineElementEventArgs> ElementAdded;

        public event EventHandler<TimelineElementEventArgs> ElementMoved;
        
        public event EventHandler<TimelineElementEventArgs> ElementRemoved;

        public event EventHandler<TimelineElementEventArgs> ElementLinked;

        public event EventHandler<TimelineElementEventArgs> ElementUnlinked;

        public Func<TimelineElement> GetElementAtPositionReturnFunction { get; set; }

        public TimeCode GetElementAtPositionPositionArgument { get; set; }

        public TimeCode Duration { get; set; }

        public TimeCode CurrentPosition { get; set; }

        public ObservableCollection<Track> Tracks { get; private set; }

        public ObservableCollection<Comment> CommentElements { get; private set; }

        public TimelineElement GetElementWithinRange(TimeCode startTimeCode, TimeCode endTimeCode, Track layer, TimelineElement elementToIgnore)
        {
            throw new System.NotImplementedException();
        }

        public TimelineElement GetElementAtPosition(TimeCode position, Track layer, TimelineElement elementToIgnore)
        {
            this.GetElementAtPositionPositionArgument = position;

            return this.GetElementAtPositionReturnFunction.Invoke();
        }

        public IList<TimelineElement> GetElementsAtPosition(TimeCode position, Track layer)
        {
            // TODO
            return new List<TimelineElement>();
        }

        public void AddElement(TimelineElement element, Track layer)
        {
            throw new NotImplementedException();
        }

        public void MoveElement(TimelineElement element, Track layer, TimeCode newPosition)
        {
            throw new NotImplementedException();
        }

        public void RemoveElement(TimelineElement element, Track layer)
        {
            throw new NotImplementedException();
        }

        public void AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void InvokeElementAdded(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementAddedHandler = this.ElementAdded;
            if (elementAddedHandler != null)
            {
                elementAddedHandler(this, new TimelineElementEventArgs(timelineElement));
            }
        }

        public void InvokeElementRemoved(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementRemovedHandler = this.ElementRemoved;
            if (elementRemovedHandler != null)
            {
                elementRemovedHandler(this, new TimelineElementEventArgs(timelineElement));
            }
        }

        public void InvokeElementMoved(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementMovedHandler = this.ElementMoved;
            if (elementMovedHandler != null)
            {
                elementMovedHandler(this, new TimelineElementEventArgs(timelineElement));
            }
        }

        public TimelineElementLink GetElementLink(TimelineElement element)
        {
            throw new NotImplementedException();
        }

        public bool LinkNextElement(TimelineElement element, TimelineElement nextElement)
        {
            throw new NotImplementedException();
        }

        public bool LinkPreviousElement(TimelineElement element, TimelineElement previousElement)
        {
            throw new NotImplementedException();
        }

        public void UnlinkElements(TimelineElement elementA, TimelineElement elementB)
        {
            throw new NotImplementedException();
        }

        public void UnlinkElement(TimelineElement element)
        {
            throw new NotImplementedException();
        }

        public bool IsElementLinkedTo(TimelineElement element, TimelineElement linkedElement)
        {
            throw new System.NotImplementedException();
        }

        public TimelineElement FindLastElementLinking(TimelineElement element, Track layer)
        {
            throw new System.NotImplementedException();
        }

        public TimelineElement FindFirstElementLinking(TimelineElement element, Track layer)
        {
            throw new System.NotImplementedException();
        }

        public void AddTrack(Track track)
        {
            throw new System.NotImplementedException();
        }

        public TimelineElement GetPreviousElement(TimeCode position, Track layer)
        {
            throw new NotImplementedException();
        }

        public TimelineElement GetNextElement(TimeCode position, Track layer)
        {
            throw new NotImplementedException();
        }
    }
}