// <copyright file="MockEventDataParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockEventDataParser.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin.Tests.Mocks
{
    using System.Xml.Linq;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    public class MockEventDataParser : IEventDataParser<EventData>
    {
        public bool ParseEventDataCalled { get; set; }

        public XElement ParseEventDataArgument { get; set; }

        public EventData ParseEventDataReturn { get; set; }

        public EventData ParseEventData(XElement eventData)
        {
            this.ParseEventDataCalled = true;
            this.ParseEventDataArgument = eventData;

            return this.ParseEventDataReturn;
        }
    }
}
