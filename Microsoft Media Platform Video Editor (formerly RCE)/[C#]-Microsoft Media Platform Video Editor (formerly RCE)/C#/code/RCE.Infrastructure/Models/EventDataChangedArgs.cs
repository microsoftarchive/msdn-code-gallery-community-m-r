// <copyright file="EventDataChangedArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: EventDataChangedArgs.cs                     
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

    public class EventDataChangedArgs : EventArgs
    {
        public EventDataChangedArgs(EventData newData, EventData oldData)
        {
            this.OldEventData = oldData;
            this.NewEventData = newData;
        }

        public EventData OldEventData { get; set; }

        public EventData NewEventData { get; set; }
    }
}
