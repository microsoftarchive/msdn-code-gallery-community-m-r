// <copyright file="EventData.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: EventData.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;

    public class EventData
    {
        public EventData(string id, TimeSpan time, string text)
            : this(id, time, text, false)
        {
        }

        public EventData(string id, TimeSpan time, string text, bool isRelativeTime)
        {
            this.Id = id;
            this.Time = time;
            this.Text = text;
            this.IsRelativeTime = isRelativeTime;
        }

        public string Id { get; private set; }

        public TimeSpan Time { get; set; }

        public string Text { get; private set; }

        public bool IsRelativeTime { get; private set; }
    }
}