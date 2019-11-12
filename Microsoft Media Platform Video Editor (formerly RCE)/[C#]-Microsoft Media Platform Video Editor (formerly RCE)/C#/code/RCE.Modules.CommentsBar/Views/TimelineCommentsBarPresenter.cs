// <copyright file="TimelineCommentsBarPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineCommentsBarPresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar
{
    using System;

    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using SMPTETimecode;

    public class TimelineCommentsBarPresenter : BaseCommentsBarPresenter
    {
        public TimelineCommentsBarPresenter(ICommentsBarView view, IEventAggregator eventAggregator, ISequenceRegistry sequenceRegistry, ITimelineBarRegistry timelineBarRegistry)
            : base(view, eventAggregator, sequenceRegistry, timelineBarRegistry)
        {
        }

        protected override CommentMode Mode
        {
            get
            {
                return CommentMode.Timeline;
            }
        }

        protected override bool FilterAddPreviewEvent(AddPreviewPayload payload)
        {
            return this.IsSourceTimeline(payload.Source);
        }

        protected override bool FilterRemovePreviewEvent(RemovePreviewPayload payload)
        {
            return this.IsSourceTimeline(payload.Source);
        }

        protected override bool FilterPositionDoubleClickedEvent(PositionPayloadEventArgs payload)
        {
            return this.IsSourceTimeline(payload.Source);
        }

        protected override bool FilterRefreshElements(RefreshElementsEventArgs payload) 
        {
            return this.IsSourceTimeline(payload.CommentMode);
        }

        protected override bool FilterDeleteAllPreviewsEvent(DeleteAllPreviewsPayload payload)
        {
            return this.IsSourceTimeline(payload.Source);
        }

        protected override bool ShouldRemovePreviewsWhenSequenceChanges()
        {
            return true;
        }

        protected override void SequenceDurationChanged(TimeCode newDuration)
        {
            this.View.SetDuration(newDuration);
        }

        private bool IsSourceTimeline(CommentMode source)
        {
            return source == this.Mode;
        }
    }
}
