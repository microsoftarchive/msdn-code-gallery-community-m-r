// <copyright file="TraceEventType.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TraceEventType.cs                     
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
    /// <summary>
    /// Identifies the severity of the information being logged.
    /// </summary>
    public enum TraceEventType : int
    {
        /// <summary>
        /// Indicates that a stop level error has occured and the system is now dead.
        /// </summary>
        Stop = 0,

        /// <summary>
        /// Indicates that an error has occured.
        /// </summary>
        Error = 1,

        /// <summary>
        /// Indicates that it is very important, but not an error yet.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Indicates that it is for informational purposes only.
        /// </summary>
        Information = 3,

        /// <summary>
        /// Indicates that this is to be used for debug purposes.
        /// </summary>
        Verbose = 4
    }
}