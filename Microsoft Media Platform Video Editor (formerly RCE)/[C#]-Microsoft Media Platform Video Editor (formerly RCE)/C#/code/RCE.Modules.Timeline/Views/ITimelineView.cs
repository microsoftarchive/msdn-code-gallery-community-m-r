// <copyright file="ITimelineView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ITimelineView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline
{
    using System;
    using System.Windows.Input;
    using Infrastructure;
    using Infrastructure.Events;
    using Infrastructure.Models;

    using RCE.Modules.Timeline.Models;

    using SMPTETimecode;

    /// <summary>
    /// Interface that defines a timeline view.
    /// </summary>
    public interface ITimelineView
    {
        /// <summary>
        /// Occurs when the position of the element change.
        /// </summary>
        event EventHandler<ElementPositionChangeEventArgs> ElementPositionChange;

        /// <summary>
        /// Occurs when an element is being selected.
        /// </summary>
        event EventHandler<ElementSelectEventArgs> SingleElementSelect;

        event EventHandler<ElementSelectEventArgs> MultipleElementSelect;

        event EventHandler<DataEventArgs<TimelineElement>> ElementDelete;

        /// <summary>
        /// Occurs when the position of the playhead change.
        /// </summary>
        event EventHandler<PositionChangeEventArgs> PositionChange;

        /// <summary>
        /// Occurs when an element is being entered.
        /// </summary>
        event EventHandler<ElementLinkEventArgs> ShowingLinks;

        /// <summary>
        /// Occurs when an element is being left.
        /// </summary>
        event EventHandler<ElementLinkEventArgs> HidingLinks;

        /// <summary>
        /// Occurs when two elements are being linked.
        /// </summary>
        event EventHandler<LinkElementEventArgs> LinkingElement;

        /// <summary>
        /// Occurs when the top bar is being double clicked.
        /// </summary>
        event EventHandler<PositionPayloadEventArgs> TopBarDoubleClicked;

        /// <summary>
        /// Occurs when the elements are being refreshed.
        /// </summary>
        event EventHandler<RefreshElementsEventArgs> RefreshingElements;

        /// <summary>
        /// Occurs when the playhead is being moved.
        /// </summary>
        event EventHandler MovingPlayHead;

        /// <summary>
        /// Occurs when elements start being moved.
        /// </summary>
        event EventHandler<DataEventArgs<TimelineElement>> StartMoving;

        /// <summary>
        /// Occurs when elements stop being moved.
        /// </summary>
        event EventHandler<DataEventArgs<TimelineElement>> StopMoving;

        event EventHandler<TrackEventArgs> BalanceChanged;

        event EventHandler<TrackEventArgs> VolumeChanged;

        event EventHandler<TrackMuteEventArgs> MuteChanged;

        event EventHandler<EventArgs> ElementLocked;

        event EventHandler<DataEventArgs<TimelineElement>> ElementUnlocked;

        /// <summary>
        /// Gets or sets the presenter associated with the view.
        /// </summary>
        /// <value>The presenter instance.</value>
        ITimelinePresenter Model { get; set; }

        TimeCode PlayHeadPosition { get; }

        /// <summary>
        /// Sets the duration of the timeline.
        /// </summary>
        /// <param name="value">The new duration of the timeline.</param>
        void SetDuration(TimeCode value);

        /// <summary>
        /// Resets the zoom of the timeline.
        /// </summary>
        void ResetZoom();

        /// <summary>
        /// Resolves the layer position based on relative position.
        /// </summary>
        /// <param name="e">The args contianing the relative position.</param>
        /// <returns>The layer position.</returns>
        LayerPosition ResolveLayerPositionFromRelativePosition(MouseEventArgs e);

        /// <summary>
        /// Sets the position of the playhead.
        /// </summary>
        /// <param name="timeCode">The new position of the playhead.</param>
        void SetPlayHeadPosition(TimeCode timeCode);

        /// <summary>
        /// Adds a <see cref="TimelineElement"/> to the timeline.
        /// </summary>
        /// <param name="element">The timeline element being added.</param>
        void AddElement(TimelineElement element);

        /// <summary>
        /// Removes a <see cref="TimelineElement"/> from the timeline.
        /// </summary>
        /// <param name="element">The timeline element being removed.</param>
        void RemoveElement(TimelineElement element);

        /// <summary>
        /// Refreshes the given ><seealso cref="TimelineElement"/>.
        /// </summary>
        /// <param name="element">The timeline element being refreshed.</param>
        void RefreshElement(TimelineElement element);

        /// <summary>
        /// Selects a timeline element on the timeline.
        /// </summary>
        /// <param name="element">The timeline element being selected.</param>
        void SelectElement(TimelineElement element);

        /// <summary>
        /// Unselects a timeline element on the timeline.
        /// </summary>
        /// <param name="element">The timeline element being unselected.</param>
        void UnselectElement(TimelineElement element);

        /// <summary>
        /// Shows a tooltip at an specific position.
        /// </summary>
        /// <param name="text">The text being displayed in the tooltip.</param>
        /// <param name="layerPosition">The layer position that defines the position and the layer where the tooltip is being showed.</param>
        void ShowTooltip(string text, LayerPosition layerPosition);

        /// <summary>
        /// Shows a warning tooltip at an specific position.
        /// </summary>
        /// <param name="text">The text being displayed in the tooltip.</param>
        /// <param name="layerPosition">The layer position that defines the position and the layer where the tooltip is being showed.</param>
        void ShowWarningTooltip(string text, LayerPosition layerPosition);

        /// <summary>
        /// Hides a tooltip.
        /// </summary>
        void HideTooltip();

        /// <summary>
        /// Shows a link of a timeline element.
        /// </summary>
        /// <param name="position">The position of the link being showed.</param>
        /// <param name="linked">If the link should be shown linked or not.</param>
        /// <param name="element">The element that contains the link being showed.</param>
        void ShowLink(LinkPosition position, bool linked, TimelineElement element);

        /// <summary>
        /// Hides a link of a timeline element.
        /// </summary>
        /// <param name="position">The position of the link.</param>
        /// <param name="element">The element that contains the link being hide.</param>
        void HideLink(LinkPosition position, TimelineElement element);

        /// <summary>
        /// Sets the start Timecode of the timeline.
        /// </summary>
        /// <param name="timeCode">The new start timecode.</param>
        void SetStartTimeCode(TimeCode timeCode);

        /// <summary>
        /// Sets if the timeline handlers should be enabled or not.
        /// </summary>
        /// <param name="timelineHandlersEnabled">A true if the timeline handlers should be enabled; otherwise false.</param>
        void UpdateTimelineHandlers(bool timelineHandlersEnabled);

        void ZoomHandler(TimelineView.Zoom zoom);

        void UpdateElementLockGroup(TimelineElement element, int groupId);
    }
}