// <copyright file="DefaultEventOffsetParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DefaultEventOffsetParser.cs                     
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
    using System.Xml.Linq;

    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// A no op event offset parser being registered as default.
    /// </summary>
    public class DefaultEventOffsetParser : IEventDataParser<EventOffset>
    {
        /// <summary>
        /// Parses the element and returns an EventOffset instance.
        /// </summary>
        /// <param name="element">The element being parsed.</param>
        /// <returns>An instance of EventOffset.</returns>
        public EventOffset ParseEventData(XElement element)
        {
            return null;
        }
    }
}