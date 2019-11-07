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

namespace RCE.Modules.Services.Tests.Mocks
{
    using System;
    using Infrastructure.Models;

    public class MockTimelineBarElement : ITimelineBarElementModel
    {
        public event EventHandler<EventArgs> TimelineBarElementUpdated;

        public event EventHandler<EventArgs> Deleting;

        public object EditBox { get; private set; }

        public object Preview { get; private set; }

        public double Position { get; private set; }

        public object DisplayBox
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void SetPosition(TimeSpan position)
        {
            throw new NotImplementedException();
        }

        public void RefreshPreview(double refreshedWidth)
        {
            throw new NotImplementedException();
        }

        public void SetElement(object value, CommentMode mode)
        {
            throw new NotImplementedException();
        }

        public T GetElement<T>() where T : class
        {
            throw new NotImplementedException();
        }   
    }
}
