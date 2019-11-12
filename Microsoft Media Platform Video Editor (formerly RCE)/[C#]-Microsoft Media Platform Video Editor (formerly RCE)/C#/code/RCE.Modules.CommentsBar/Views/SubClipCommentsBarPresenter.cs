// <copyright file="SubClipCommentsBarPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClipCommentsBarPresenter.cs                     
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

    public class SubClipCommentsBarPresenter : BaseCommentsBarPresenter
    {
        public SubClipCommentsBarPresenter(ICommentsBarView view, IEventAggregator eventAggregator, ISequenceRegistry sequenceRegistry, ITimelineBarRegistry timelineBarRegistry)
            : base(view, eventAggregator, sequenceRegistry, timelineBarRegistry)
        {
            eventAggregator.GetEvent<DurationChangedEvent>().Subscribe(this.OnDurationChanged, ThreadOption.PublisherThread, false);
            this.View.SetEditBoxMargins(0, -93, 0, 0);
            this.View.SetEditBoxZeeIndex(1000);
        }

        protected override CommentMode Mode
        {
            get
            {
                return CommentMode.SubClip;
            }
        }

        public void OnDurationChanged(DurationChangedEventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            this.View.SetDuration(args.Timecode);
        }

        protected override bool FilterAddPreviewEvent(AddPreviewPayload payload)
        {
            return this.IsSourceSubClip(payload.Source);
        }

        protected override bool FilterRemovePreviewEvent(RemovePreviewPayload payload)
        {
            return this.IsSourceSubClip(payload.Source);
        }

        protected override bool FilterPositionDoubleClickedEvent(PositionPayloadEventArgs payload)
        {
            return this.IsSourceSubClip(payload.Source);
        }

        protected override bool FilterDeleteAllPreviewsEvent(DeleteAllPreviewsPayload payload)
        {
            return this.IsSourceSubClip(payload.Source);
        }

        protected override bool ShouldRemovePreviewsWhenSequenceChanges()
        {
            return false;
        }

        protected override bool FilterRefreshElements(RefreshElementsEventArgs payload)
        {
            return this.IsSourceSubClip(payload.CommentMode);
        }

        protected override void SequenceDurationChanged(TimeCode newDuration)
        {
            // missing play by play marks workround: do not update duration if in sub-clip tool window
        }

        private bool IsSourceSubClip(CommentMode source)
        {
            return source == this.Mode;
        }
    }
}
