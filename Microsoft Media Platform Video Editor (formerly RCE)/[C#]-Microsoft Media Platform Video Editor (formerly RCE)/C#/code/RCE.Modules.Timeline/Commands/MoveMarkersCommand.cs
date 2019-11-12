// <copyright file="MoveMarkersCommand.cs" company="Microsoft Corporation">
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

    public class MoveMarkersCommand : UndoableCommand
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IDictionary<TimelineElement, double> offsetsByElement;
        
        public MoveMarkersCommand(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.offsetsByElement = new Dictionary<TimelineElement, double>();
        }

        public void UpdateOffset(TimelineElement element, double offset)
        {
            if (element.Asset is VideoAssetInOut)
            {
                if (this.offsetsByElement.ContainsKey(element))
                {
                    this.offsetsByElement[element] += offset;
                }
                else
                {
                    this.offsetsByElement[element] = offset;
                }
            }
        }

        protected override void ExecuteCommand()
        {
            foreach (KeyValuePair<TimelineElement, double> keyValuePair in this.offsetsByElement)
            {
                var videoInOut = keyValuePair.Key.Asset as VideoAssetInOut;
                if (videoInOut != null)
                {
                    videoInOut.MovePlayByPlayMarkers(TimeSpan.FromSeconds(keyValuePair.Value).Ticks);
                }
            }
            
            this.eventAggregator.GetEvent<RefreshElementsEvent>().Publish(new RefreshElementsEventArgs(null, CommentMode.Timeline));
        }

        protected override void UnExecuteCommand()
        {
            foreach (KeyValuePair<TimelineElement, double> keyValuePair in this.offsetsByElement)
            {
                var videoInOut = keyValuePair.Key.Asset as VideoAssetInOut;
                if (videoInOut != null)
                {
                    // substracting the ticks goes back to the previous position
                    videoInOut.MovePlayByPlayMarkers(-TimeSpan.FromSeconds(keyValuePair.Value).Ticks);
                }
            }

            this.eventAggregator.GetEvent<RefreshElementsEvent>().Publish(new RefreshElementsEventArgs(null, CommentMode.Timeline));
        }
    }
}
