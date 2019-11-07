// <copyright file="MockTimelineBarElement.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockTimelineBarElement.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar.Tests.Mocks
{
    using System;
    using Infrastructure.Models;

    public class MockTimelineBarElement : ITimelineBarElementModel
    {
        public event EventHandler<EventArgs> TimelineBarElementUpdated;

        public event EventHandler<EventArgs> Deleting;

        public bool SetPositionCalled { get; set; }

        public TimeSpan SetPositionArgument { get; set; }

        public bool RefreshPreviewCalled { get; set; }

        public double? RefreshPreviewArgument { get; set; }

        public object EditBox { get; set; }

        public object Preview { get; set; }

        public double Position { get; set; }

        public object DisplayBox
        {
            get
            {
                return null;
            }
        }

        public object ElementToReturn { get; set; }

        public void SetPosition(TimeSpan position)
        {
            this.SetPositionCalled = true;
            this.SetPositionArgument = position;
        }

        public void RefreshPreview(double refreshedWidth)
        {
            this.RefreshPreviewCalled = true;
            this.RefreshPreviewArgument = refreshedWidth;
        }

        public void SetElement(object value, CommentMode mode)
        {
            throw new NotImplementedException();
        }

        public void InvokeDeleting()
        {
            EventHandler<EventArgs> deleting = this.Deleting;
            if (deleting != null)
            {
                deleting(this, EventArgs.Empty);
            }
        }

        public void InvokeTimelineBarElementUpdated()
        {
            EventHandler<EventArgs> handler = this.TimelineBarElementUpdated;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public T GetElement<T>() where T : class
        {
            return this.ElementToReturn as T;
        }
    }
}