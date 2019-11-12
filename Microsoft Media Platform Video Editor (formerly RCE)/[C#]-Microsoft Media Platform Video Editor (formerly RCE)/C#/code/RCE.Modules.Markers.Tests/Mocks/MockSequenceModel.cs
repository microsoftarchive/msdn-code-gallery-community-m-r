// <copyright file="MockSequenceModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSequenceModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    using RCE.Infrastructure.Models;
    using RCE.Services.Contracts;

    using SMPTETimecode;

    using Comment = RCE.Infrastructure.Models.Comment;
    using Track = RCE.Infrastructure.Models.Track;

    public class MockSequenceModel : ISequenceModel
    {
        public MockSequenceModel()
        {
            this.Tracks = new ObservableCollection<Track>();
            this.CommentElements = new ObservableCollection<Comment>();
            this.AddElementArguments = new List<TimelineElement>();
            this.MoveElementNewPositionArguments = new List<TimeCode>();
            this.MoveElementElementArguments = new List<TimelineElement>();
            this.GetElementAtPositionReturnFunction = () => null;
            this.GetNextElementReturnFunction = (p, t) => null;
            this.GetPreviousElementReturnFunction = (p, t) => null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<TimelineElementEventArgs> ElementAdded;

        public event EventHandler<TimelineElementEventArgs> ElementMoved;

        public event EventHandler<TimelineElementEventArgs> ElementRemoved;

        public event EventHandler<TimelineElementEventArgs> ElementLinked;

        public event EventHandler<TimelineElementEventArgs> ElementUnlinked;

        TimeCode ISequenceModel.Duration
        {
            get
            {
                return this.Duration;
            }

            set
            {
                this.Duration = value;
            }
        }

        TimeCode ISequenceModel.CurrentPosition
        {
            get
            {
                return this.CurrentPosition;
            }

            set
            {
                this.CurrentPosition = value;
            }
        }

        public TimeCode Duration { get; set; }

        public TimeCode CurrentPosition { get; set; }

        public ObservableCollection<Track> Tracks { get; private set; }

        public ObservableCollection<Comment> CommentElements { get; private set; }

        public ObservableCollection<AdOpportunity> AdOpportunities { get; private set; }

        public ObservableCollection<Marker> Markers { get; private set; }

        public bool RemoveElementCalled { get; set; }

        public TimelineElement RemoveElementArgument { get; set; }

        public bool AddElementCalled { get; set; }

        public TimelineElement AddElementArgument { get; set; }

        public IList<TimelineElement> AddElementArguments { get; set; }

        public bool MoveElementCalled { get; set; }

        public TimeCode MoveElementNewPositionArgument { get; set; }

        public IList<TimelineElement> GetElementsAtPositionReturnValue { get; set; }

        public Func<TimelineElement> GetElementAtPositionReturnFunction { get; set; }

        public Func<TimeCode, Track, TimelineElement> GetNextElementReturnFunction { get; set; }

        public Func<TimeCode, Track, TimelineElement> GetPreviousElementReturnFunction { get; set; }

        public TimelineElement MoveElementElementArgument { get; set; }

        public TimeCode GetElementAtPositionPositionArgument { get; set; }

        public bool LinkNextElementCalled { get; set; }
        
        public List<TimelineElement> MoveElementElementArguments { get; set; }

        public bool LinkPreviousElementCalled { get; set; }

        public bool IsElementLinkedToReturnValue { get; set; }

        public bool UnlinkElementsCalled { get; set; }

        public int MoveElementTimesCalled { get; set; }

        public List<TimeCode> MoveElementNewPositionArguments { get; set; }

        public TimelineElement ElementWithinRange { get; set; }

        public void AddElement(TimelineElement element, Track layer)
        {
            this.AddElementCalled = true;
            this.AddElementArgument = element;
            this.AddElementArguments.Add(element);
        }

        public void MoveElement(TimelineElement element, Track layer, TimeCode newPosition)
        {
            this.MoveElementTimesCalled++;
            this.MoveElementCalled = true;
            this.MoveElementElementArgument = element;
            this.MoveElementElementArguments.Add(element);
            this.MoveElementNewPositionArgument = newPosition;
            this.MoveElementNewPositionArguments.Add(newPosition);
            element.Position = newPosition;
        }

        public void RemoveElement(TimelineElement element, Track layer)
        {
            this.RemoveElementCalled = true;
            this.RemoveElementArgument = element;
            this.InvokeElementRemoved(element);
        }

        public void AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public TimelineElementLink GetElementLink(TimelineElement element)
        {
            return new TimelineElementLink(element.Id); // TODO
        }

        public bool LinkNextElement(TimelineElement element, TimelineElement nextElement)
        {
            this.LinkNextElementCalled = true;
            return false;
        }

        public bool LinkPreviousElement(TimelineElement element, TimelineElement previousElement)
        {
            this.LinkPreviousElementCalled = true;
            return false;
        }

        public void UnlinkElements(TimelineElement elementA, TimelineElement elementB)
        {
            this.UnlinkElementsCalled = true;
        }

        public void UnlinkElement(TimelineElement element)
        {
            throw new NotImplementedException();
        }

        public bool IsElementLinkedTo(TimelineElement element, TimelineElement linkedElement)
        {
            return this.IsElementLinkedToReturnValue;
        }

        public TimelineElement FindLastElementLinking(TimelineElement element, Track layer)
        {
            return element; // TODO
        }

        public TimelineElement FindFirstElementLinking(TimelineElement element, Track layer)
        {
            return element; // TODO
        }

        public void AddTrack(Track track)
        {
            this.Tracks.Add(track);
        }

        public TimelineElement GetPreviousElement(TimeCode position, Track layer)
        {
            throw new NotImplementedException();
        }

        public TimelineElement GetNextElement(TimeCode position, Track layer)
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

        public void InvokeElementMoved(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementMovedHandler = this.ElementMoved;
            if (elementMovedHandler != null)
            {
                elementMovedHandler(this, new TimelineElementEventArgs(timelineElement));
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

        public void InvokeElementLinked(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementLinkedHandler = this.ElementLinked;
            if (elementLinkedHandler != null)
            {
                elementLinkedHandler(this, new TimelineElementEventArgs(timelineElement));
            }
        }

        public void InvokeElementUnlinked(TimelineElement timelineElement)
        {
            EventHandler<TimelineElementEventArgs> elementUnlinkedHandler = this.ElementUnlinked;
            if (elementUnlinkedHandler != null)
            {
                elementUnlinkedHandler(this, new TimelineElementEventArgs(timelineElement));
            }
        }

        public void UpdatePlayByPlayMarkersPosition(TimelineElement element, TimeCode newPosition)
        {
            throw new NotImplementedException();
        }

        public bool SequenceHasGap()
        {
            throw new NotImplementedException();
        }

        public TimelineElement GetElementWithinRange(TimeCode startTimeCode, TimeCode endTimeCode, Track layer, TimelineElement elementToIgnore)
        {
            throw new NotImplementedException();
        }

        public TimelineElement GetElementAtPosition(TimeCode position, Track layer, TimelineElement elementToIgnore)
        {
            throw new NotImplementedException();
        }

        public IList<TimelineElement> GetElementsAtPosition(TimeCode position, Track layer)
        {
            throw new NotImplementedException();
        }
    }
}
