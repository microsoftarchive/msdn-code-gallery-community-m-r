// <copyright file="MockTimelineView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockTimelineView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Infrastructure;
    using Infrastructure.Events;
    using Infrastructure.Models;

    using RCE.Modules.Timeline.Models;

    using SMPTETimecode;

    /// <summary>
    /// 
    /// </summary>
    public class MockTimelineView : ITimelineView
    {
        private TimeCode playHeadPosition;

        public MockTimelineView()
        {
            this.RefreshElementArgument = new List<TimelineElement>();
            this.RemoveElements = new List<TimelineElement>();
            this.AddElements = new List<TimelineElement>();
        }

        public event EventHandler<ElementPositionChangeEventArgs> ElementPositionChange;

        public event EventHandler<ElementSelectEventArgs> SingleElementSelect;

        public event EventHandler<ElementSelectEventArgs> MultipleElementSelect;

        public event EventHandler<DataEventArgs<TimelineElement>> ElementDelete;

        public event EventHandler<PositionChangeEventArgs> PositionChange;

        public event EventHandler<ElementLinkEventArgs> ShowingLinks;

        public event EventHandler<ElementLinkEventArgs> HidingLinks;

        public event EventHandler<LinkElementEventArgs> LinkingElement;

        public event EventHandler<PositionPayloadEventArgs> TopBarDoubleClicked;

        public event EventHandler<RefreshElementsEventArgs> RefreshingElements;

        public event EventHandler MovingPlayHead;

        public event EventHandler<DataEventArgs<TimelineElement>> StartMoving;

        public event EventHandler<DataEventArgs<TimelineElement>> StopMoving;

        public event EventHandler<TrackEventArgs> BalanceChanged;

        public event EventHandler<TrackEventArgs> VolumeChanged;

        public event EventHandler<EventArgs> ElementLocked;

        public event EventHandler<DataEventArgs<TimelineElement>> ElementUnlocked;

        public event EventHandler<TrackMuteEventArgs> MuteChanged;

        public ITimelinePresenter Model { get; set; }           

        public TimeCode SetPlayheadPostionArgument { get; set; }

        public bool SetDurationCalled { get; set; }

        public TimeCode SetDurationArgument { get; set; }

        public LayerPosition MockedResolvedLayerPosition { get; set; }

        public bool RemoveElementCalled { get; set; }

        public bool AddElementCalled { get; set; }

        public bool RefreshElementCalled { get; set; }

        public int RefreshElementCalledCount { get; set; }

        public IList<TimelineElement> RefreshElementArgument { get; set; }

        public bool ShowLinkCalled { get; set; }

        public bool HideLinkCalled { get; set; }

        public bool SetStartTimeCodeCalled { get; set; }

        public bool ShowAssetDownloadProgressCalled { get; set; }

        public TimeCode SetStartTimeCodeArgument { get; set; }

        public bool UnselectElementCalled { get; set; }

        public TimelineElement UnselectElementArgument { get; set; }

        public bool SelectElementCalled { get; set; }

        public TimelineElement SelectElementArgument { get; set; }

        public LinkPosition ShowLinkLinkPositionArgument { get; set; }

        public bool UpdateTimelineHandlersCalled { get; set; }

        public bool UpdateTimelineHandlersArgument { get; set; }

        public List<TimelineElement> RemoveElements { get; set; }

        public List<TimelineElement> AddElements { get; set; }

        public TimeCode PlayHeadPosition
        {
            get
            {
                return this.playHeadPosition;
            }
        }

        public LayerPosition ResolveLayerPositionFromRelativePosition(MouseEventArgs e)
        {
            return this.MockedResolvedLayerPosition;
        }

        public void SetDuration(TimeCode duration)
        {
            this.SetDurationCalled = true;
            this.SetDurationArgument = duration;
        }

        public void SetPlayHeadPosition(TimeCode timeCode)
        {
            this.SetPlayheadPostionArgument = timeCode;
            this.playHeadPosition = timeCode;
        }

        public void ResetZoom()
        {
        }

        public void AddElement(TimelineElement element)
        {
            this.AddElementCalled = true;
            this.AddElements.Add(element);
        }

        public void RemoveElement(TimelineElement element)
        {
            this.RemoveElements.Add(element);
            this.RemoveElementCalled = true;
        }

        public void RefreshElement(TimelineElement element)
        {
            this.RefreshElementCalledCount++;
            this.RefreshElementCalled = true;

            this.RefreshElementArgument.Add(element);
        }

        public void SelectElement(TimelineElement element)
        {
            this.SelectElementCalled = true;
            this.SelectElementArgument = element;
        }

        public void UnselectElement(TimelineElement element)
        {
            this.UnselectElementCalled = true;
            this.UnselectElementArgument = element;
        }

        public void ShowTooltip(string text, LayerPosition layerPosition)
        {
        }

        public void ShowWarningTooltip(string text, LayerPosition layerPosition)
        {
        }

        public void HideTooltip()
        {
        }

        public void ShowLink(LinkPosition position, bool linked, TimelineElement element)
        {
            this.ShowLinkLinkPositionArgument = position;
            this.ShowLinkCalled = true;
        }

        public void HideLink(LinkPosition position, TimelineElement element)
        {
            this.HideLinkCalled = true;
        }

        /// <summary>
        /// Shows the download progress bar.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="progress">The progress.</param>
        public void ShowAssetDownloadProgress(TimelineElement element, double progress, double offset)
        {
            this.ShowAssetDownloadProgressCalled = true;
        }

        public void UpdateTimelineHandlers(bool timelineHandlersEnabled)
        {
            this.UpdateTimelineHandlersCalled = true;
            this.UpdateTimelineHandlersArgument = timelineHandlersEnabled;
        }

        public void ZoomHandler(TimelineView.Zoom zoom)
        {
            throw new NotImplementedException();
        }

        public void UpdateElementLockGroup(TimelineElement element, int groupId)
        {
        }

        public void SetStartTimeCode(TimeCode timeCode)
        {
            this.SetStartTimeCodeCalled = true;
            this.SetStartTimeCodeArgument = timeCode;
        }

        public void InvokeElementSelect(TimelineElement element, TimeCode position)
        {
            EventHandler<ElementSelectEventArgs> elementSelectHandler = this.SingleElementSelect;
            if (elementSelectHandler != null)
            {
                elementSelectHandler(this, new ElementSelectEventArgs { Element = element, Position = position });
            }
        }

        public void InvokeElementPositionChange(TimeCode newPosition)
        {
            EventHandler<ElementPositionChangeEventArgs> elementPositionChangeHandler = this.ElementPositionChange;
            if (elementPositionChangeHandler != null)
            {
                elementPositionChangeHandler(
                    this,
                    new ElementPositionChangeEventArgs { NewPosition = newPosition, PositionType = ElementPositionType.Position });
            }
        }

        public void DoMoveElementMarkOut(TimeCode outPosition)
        {
            if (this.ElementPositionChange != null)
            {
                var args = new ElementPositionChangeEventArgs
                               {
                                   NewPosition = outPosition,
                                   PositionType = ElementPositionType.OutPosition
                               };

                this.ElementPositionChange(this, args);
            }
        }

        public void DoMoveElementMarkIn(TimeCode inPosition)
        {
            if (this.ElementPositionChange != null)
            {
                var args = new ElementPositionChangeEventArgs
                               {
                                   NewPosition = inPosition,
                                   PositionType = ElementPositionType.InPosition
                               };

                this.ElementPositionChange(this, args);
            }
        }

        public void InvokeMovingPlayhead()
        {
            EventHandler movingPlayheadHandler = this.MovingPlayHead;
            if (movingPlayheadHandler != null)
            {
                movingPlayheadHandler(this, EventArgs.Empty);
            }
        }

        public void InvokeStartMoving(TimelineElement element)
        {
            EventHandler<DataEventArgs<TimelineElement>> moving = this.StartMoving;
            if (moving != null)
            {
                moving(this, new DataEventArgs<TimelineElement>(element));
            }
        }

        public void InvokeStopMoving(TimelineElement element)
        {
            EventHandler<DataEventArgs<TimelineElement>> moving = this.StopMoving;
            if (moving != null)
            {
                moving(this, new DataEventArgs<TimelineElement>(element));
            }
        }

        public void InvokeTopBarDoubleClicked(PositionPayloadEventArgs e)
        {
            EventHandler<PositionPayloadEventArgs> clicked = this.TopBarDoubleClicked;
            if (clicked != null)
            {
                clicked(this, e);
            }
        }

        public void InvokeRefreshingElements(RefreshElementsEventArgs e)
        {
            EventHandler<RefreshElementsEventArgs> elements = this.RefreshingElements;
            if (elements != null)
            {
                elements(this, e);
            }
        }

        public void InvokeHidingLinks(TimelineElement timelineElement)
        {
            EventHandler<ElementLinkEventArgs> links = this.HidingLinks;
            if (links != null)
            {
                links(this, new ElementLinkEventArgs(timelineElement));
            }
        }

        public void InvokeShowingLinks(TimelineElement timelineElement)
        {
            EventHandler<ElementLinkEventArgs> links = this.ShowingLinks;
            if (links != null)
            {
                links(this, new ElementLinkEventArgs(timelineElement));
            }
        }

        public void InvokeLinkingElement(LinkElementEventArgs e)
        {
            EventHandler<LinkElementEventArgs> element = this.LinkingElement;
            if (element != null)
            {
                element(this, e);
            }
        }

        public void InvokeElementMultiSelect(TimelineElement element, TimeCode position)
        {
            EventHandler<ElementSelectEventArgs> elementSelectHandler = this.MultipleElementSelect;
            if (elementSelectHandler != null)
            {
                elementSelectHandler(this, new ElementSelectEventArgs { Element = element, Position = position });
            }
        }

        public void InvokeElementUnlock(TimelineElement element)
        {
            EventHandler<DataEventArgs<TimelineElement>> elementUnlocked = this.ElementUnlocked;
            if (elementUnlocked != null)
            {
                elementUnlocked(this, new DataEventArgs<TimelineElement>(element));
            }
        }
    }
}