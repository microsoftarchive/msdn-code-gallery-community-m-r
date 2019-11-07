// <copyright file="IEventDataParser.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IEventDataParser.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System.Xml.Linq;

    /// <summary>
    /// Defines the interface used by the event data parsers.
    /// </summary>
    /// <typeparam name="T">The type of event being parsed.</typeparam>
    public interface IEventDataParser<T>
    {
        /// <summary>
        /// Parses the element and returns a T instance.
        /// </summary>
        /// <param name="element">The element being parsed.</param>
        /// <returns>An instance of T.</returns>
        T ParseEventData(XElement element);
    }
}