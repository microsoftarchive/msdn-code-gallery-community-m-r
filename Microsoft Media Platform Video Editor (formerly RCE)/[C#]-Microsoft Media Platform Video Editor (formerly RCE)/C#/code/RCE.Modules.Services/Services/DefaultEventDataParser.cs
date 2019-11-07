// <copyright file="DefaultEventDataParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DefaultEventDataParser.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.Xml.Linq;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// A no op event data parser being registered as default.
    /// </summary>
    public class DefaultEventDataParser : IEventDataParser<EventData>
    {
        private readonly IConfigurationService configurationService;

        private readonly string markerDataElementName;

        public DefaultEventDataParser(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
            this.markerDataElementName = this.configurationService.GetParameterValue("LogDataElementName");
        }

        /// <summary>
        /// Parses the element and returns an EventData instance.
        /// </summary>
        /// <param name="element">The element being parsed.</param>
        /// <returns>An instance of EventData.</returns>
        public EventData ParseEventData(XElement element)
        {
            EventData eventData = null;

            if (element != null && element.Name == this.markerDataElementName)
            {
                string id = element.Attribute("ID").GetValue();
                string text = element.GetValue();

                if (string.IsNullOrEmpty(text))
                {
                    text = element.Attribute("Text").GetValue();
                }

                long? time = element.Attribute("Time").GetValueAsLong();

                if (!time.HasValue)
                {
                    time = element.Attribute("RelativeTime").GetValueAsLong();
                }

                TimeSpan relativeTime = TimeSpan.FromTicks(time ?? 0);

                eventData = new EventData(id, relativeTime, text);
            }

            return eventData;
        }
    }
}
