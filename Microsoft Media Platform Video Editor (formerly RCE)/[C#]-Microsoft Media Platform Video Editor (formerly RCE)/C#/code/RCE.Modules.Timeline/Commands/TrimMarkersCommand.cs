// <copyright file="TrimMarkersCommand.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MoveMarkersCommand.cs                     
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
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Composite.Events;

    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;

    public class TrimMarkersCommand : UndoableCommand
    {
        private readonly IEventAggregator eventAggregator;

        private TimelineElement element;

        private double offset;

        private double startPosition;

        private ElementPositionType type;

        public TrimMarkersCommand(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public void UpdateInformation(TimelineElement e, double offsetUpdate, double updatedStartPosition, ElementPositionType positionType)
        {
            this.element = e;
            this.offset += offsetUpdate;
            this.startPosition = updatedStartPosition;
            this.type = positionType;
        }

        protected override void ExecuteCommand()
        {
            var videoAssetInOut = this.element.Asset as VideoAssetInOut;
            if (videoAssetInOut != null && videoAssetInOut.AddMarkersToSequence)
            {
                this.eventAggregator.GetEvent<DeleteAllPreviewsEvent>().Publish(
                      new DeleteAllPreviewsPayload(CommentMode.Timeline)
                      {
                          ItemsToErase = videoAssetInOut.PlayByPlayFilteredMarkers
                      });

                if (this.type == ElementPositionType.InPosition)
                {
                    videoAssetInOut.InPosition += this.offset;
                }

                if (this.type == ElementPositionType.OutPosition)
                {
                    videoAssetInOut.OutPosition += this.offset;
                }

                videoAssetInOut.UpdateFilter(this.startPosition);

                foreach (var playByPlay in videoAssetInOut.PlayByPlayFilteredMarkers)
                {
                    this.eventAggregator.GetEvent<AddPreviewEvent>().Publish(new AddPreviewPayload("PlayByPlay", playByPlay, CommentMode.Timeline));
                }
            }
        }

        protected override void UnExecuteCommand()
        {
            var videoAssetInOut = this.element.Asset as VideoAssetInOut;
            if (videoAssetInOut != null && videoAssetInOut.AddMarkersToSequence)
            {
                this.eventAggregator.GetEvent<DeleteAllPreviewsEvent>().Publish(
                      new DeleteAllPreviewsPayload(CommentMode.Timeline)
                      {
                          ItemsToErase = videoAssetInOut.PlayByPlayFilteredMarkers
                      });

                if (this.type == ElementPositionType.InPosition)
                {
                    videoAssetInOut.InPosition -= this.offset;
                    videoAssetInOut.UpdateFilter(this.startPosition - this.offset);
                }

                if (this.type == ElementPositionType.OutPosition)
                {
                    videoAssetInOut.OutPosition -= this.offset;
                    videoAssetInOut.UpdateFilter(this.startPosition);
                }

                foreach (var playByPlay in videoAssetInOut.PlayByPlayFilteredMarkers)
                {
                    this.eventAggregator.GetEvent<AddPreviewEvent>().Publish(new AddPreviewPayload("PlayByPlay", playByPlay, CommentMode.Timeline));
                }
            }
        }
    }
}